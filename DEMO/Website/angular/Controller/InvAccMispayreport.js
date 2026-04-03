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
    $scope.Customer = {};
    $scope.Code = {};
    $scope.Name = {};
    $scope.Date;
    $scope.DateTo;
    $scope.planlistType = {};
    $scope.GetHandsIds = function () {
        cfpLoadingBar.start();
        debugger;
        var Searching = $scope.Customer.search === undefined ? '' : $scope.Customer.search;
        var SearchingC = $scope.Code.search === undefined ? '' : $scope.Code.search;
        var Date = $scope.Date === undefined ? '' : $scope.Date;
        var DateTo = $scope.DateTo === undefined ? '' : $scope.DateTo;
        var SearchingN = $scope.Name.search === undefined ? '' : $scope.Name.search;

       
        $http.get(Url + "api/misreturninvaccountreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "&searchC=" + SearchingC + "&searchN=" + SearchingN + "&date=" + Date + "&dateTo=" + DateTo + "").then(
                    function (response) {

              
            console.log(response.data.investmnetclosedetailsdataList);
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
                            saveAs(bb, "MisreturnPayReport.xls");
                        };

        },
                       function (err) {
                           var error = err;
                       });
    }

    $scope.GetFilterUsers = function (Name) {
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
