
var app1 = angular.module('managerepresentativelistApp', ['ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});
app1.controller('managerepresentativelistCtrl', function ($scope, $http, cfpLoadingBar) {
   
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.BranchData = {};
    $scope.Name = {};
    $scope.Represantative = {};
    $scope.Contact = {};
    $scope.Status = "2";
    $scope.Date;
    $scope.DateTo;

    $scope.GetdsaList = function () {
        cfpLoadingBar.start();
        var SearchingM = $scope.Name.search === undefined ? '' : $scope.Name.search;
        var SearchingR = $scope.Represantative.search === undefined ? '' : $scope.Represantative.search;
        var SearchingC = $scope.Contact.search === undefined ? '' : $scope.Contact.search;
        var Status = $scope.Status === undefined ? '' : $scope.Status;

       
        var Date = $scope.Date === undefined ? '' : $scope.Date;
        var DateTo = $scope.DateTo === undefined ? '' : $scope.DateTo;
        $http.get(Url + "api/agentlistpagging?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&searchM=" + SearchingM + "&searchR=" + SearchingR + "&searchc=" + SearchingC + "&status=" + Status + "&date=" + Date + "&dateTo=" + DateTo + "").then(
            function (response) {
               
                console.log(response.data.dsAdataList);
                $scope.dsAdataList = response.data.dsAdataList;
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
                    saveAs(bb, "DSAManagementreport.xls");
                };
    
            
            },
            function (err) {
                var error = err;
            });
    }
   // console.log($scope.DSAdataList)
  
    $scope.GetFilterUsers = function (Name) {
        $scope.GetdsaList();
    }
    $scope.GetdsaList();

    $scope.GetStatusList = function (Status) {

        $scope.GetdsaList();
    }
    $scope.GetDateList = function (Date, DateTo) {

        $scope.GetdsaList();
    }

    //Loading employees list on first time
  
    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetdsaList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetdsaList();
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


