var app = angular.module('InvaccreportlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('InvaccreportCtrl', function ($scope, $http, cfpLoadingBar) {
    
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.ClubData = {};
    $scope.Searchtype = 0;
    $scope.fromdate = "";
    $scope.todate = "";
    $scope.customerid = "";
    $scope.name = "";
    $scope.Searchplanname = "";
    $scope.Searchplantype = "";
    $scope.GetHandsIds = function () {
        cfpLoadingBar.start();

        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;
        var plantypeSearching = $scope.formSearchtype === undefined ? '' : $scope.formSearchtype;
        var sfromdate = $scope.fromdate === undefined ? '' : $scope.fromdate;
        var stodate = $scope.todate === undefined ? '' : $scope.todate;
        var scustomerid = $scope.customerid === undefined ? '' : $scope.customerid;
        var sname = $scope.name === undefined ? '' : $scope.name;
        var sSearchplanname = $scope.Searchplanname === undefined ? '' : $scope.Searchplanname;
        $http.get(Url + "api/closeinvaccountreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "&customerid=" + scustomerid + "&name=" + sname + "&fromdate=" + sfromdate + "&todate=" + stodate + "&plantypeid=" + plantypeSearching + "&planid=" + sSearchplanname + "").then(
        function (response) {
            //console.log(response.data.investmnetclosedetailsdataList);
            $scope.cList = response.data.investmnetclosedetailsdataList;
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
                saveAs(bb, "ClosedAccountreport.xls");
            };

        },
                       function (err) {
                           var error = err;
                       });
      
                        //// Bind Plan List Dropdown
                        //$http.get(Url + "/api/planlistbind?planid=" + plantypeSearching + "").success(function (data) {
                        //    $scope.formData = {};
                        //    $scope.formData.users = data;
                        //}).error(function (status)
                        //{
                        //    //showAlert(status);
                        //});
                        //// Bind Plan Type List Dropdown
                        //$http.get(Url + "/api/plantypelistbind?plantype=0").success(function (data) {
                   
                        //    $scope.formData1 = {};
                        //    $scope.formData1.users = data;
                        //   // console.log($scope.formData1[0]);
                        //}).error(function (status) {
                        //    //showAlert(status);
                        //});
    }

    $scope.GetFilterUsers = function (ClubData) {
        $scope.GetHandsIds();
    }
    $scope.Getplantype = function (Searchtype) {
        $scope.GetHandsIds();
    }
    //Loading employees list on first time
    $scope.GetHandsIds();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetHandsIds();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetHandsIds();
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
