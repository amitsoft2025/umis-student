var app = angular.module('sharelistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('sharelistCtrl', function ($scope, $http, cfpLoadingBar) {
   
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.ClubData = {};
    $scope.GetHandsIds = function () {
        cfpLoadingBar.start();

        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;

        $http.get(Url + "api/sharealloetreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "").then(
        function (response) {
          
            console.log(response.data.sharedataList);
            $scope.cList = response.data.sharedataList;
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
                saveAs(bb, "ShareAllotListReport.xls");
            };

        },
                       function (err) {
                           var error = err;
                       });
    }

    $scope.GetFilterUsers = function (ClubData) {
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

app.filter('INR', function () {
    return function (input) {
        if (!isNaN(input)) {
            var currencySymbol = '';
            //var output = Number(input).toLocaleString('en-IN');   <-- This method is not working fine in all browsers!           
            var result = input.toString().split('.');

            var lastThree = result[0].substring(result[0].length - 3);
            var otherNumbers = result[0].substring(0, result[0].length - 3);
            if (otherNumbers != '')
                lastThree = ',' + lastThree;
            var output = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree;

            if (result.length > 1) {
                output += "." + result[1];
            }

            return currencySymbol + output;
        }
    }
});

