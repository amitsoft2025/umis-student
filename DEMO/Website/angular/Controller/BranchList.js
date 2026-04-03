var app = angular.module('branchlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});


app.controller('branchlistCtrl', function ($scope, $http, cfpLoadingBar) {

    // var Url = 'http://localhost:30170/Common/';
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.BranchData = {};
    $scope.ClubData = {};
    $scope.Status = 0;
    $scope.ClubData1 = {};
    $scope.getBranchList = function () {
        cfpLoadingBar.start();
        var Searching1 = $scope.ClubData1.search === undefined ? '' : $scope.ClubData1.search;
        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;
        
        $http.get(Url + "api/getbranchlistApi?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "&status=" + $scope.Status +"").then(
            function (response) {
                debugger;
                console.log(response.data.branchlist);
                $scope.branches = response.data.branchlist;
                $scope.totalCount = response.data.totalCount;
                       $scope.exportData = function () {

                           var blob = new Blob([document.getElementById('headers').innerHTML], {
                               type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
                           });
                           var blob1 = new Blob([document.getElementById('tbody').innerHTML], {
                               type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
                           });
                           var MyBlobBuilder = function () {
                               this.parts = [];
                           }

                           MyBlobBuilder.prototype.append = function (part) {
                               this.parts.push(part);
                               this.blob = undefined; // Invalidate the blob
                           };

                           MyBlobBuilder.prototype.getBlob = function () {
                               if (!this.blob) {
                                   this.blob = new Blob(this.parts, { type: "text/plain" });
                               }
                               return this.blob;
                           };

                           var myBlobBuilder = new MyBlobBuilder();

                           myBlobBuilder.append('<html><head><style>body { border:solid 0.1pt #CCCCCC; }</style></head><body><table>');

                           myBlobBuilder.append(blob);
                           myBlobBuilder.append(blob1);
                           myBlobBuilder.append("</table></body>");
                           var bb = myBlobBuilder.getBlob();
                           //    debugger;
                           saveAs(bb, "BranchListReport.xls");
                       };

            },
            function (err) {
                var error = err;
            });
    }
    $scope.GetFilterUsers = function (ClubData) {
        $scope.getBranchList();
    }
    $scope.GetStatusUsers = function (Status) {
   
        $scope.getBranchList();
    }

    $scope.GetFilterBranch = function (userData) {
        // showAlert(JSON.stringify(userData));
        $scope.getBranchList();
    }

    //Loading employees list on first time
    $scope.getBranchList();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.getBranchList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getBranchList();
    };

    // Sorting by Table head
    $scope.propertyName = null;
    $scope.reverse = true;


    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };
    //


    $scope.numDifferentiation = function (val) {
        if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
        else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
        else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
        return val;
    }


});
