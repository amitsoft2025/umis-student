var app = angular.module('LoanSummarylistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;

});

app.controller('LoanSummaryCtrl', function ($scope, $http, cfpLoadingBar) {
    // debugger;
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.ClubData = {};
    $scope.jsonToExport = {};
    $scope.loanCategory = "";
    $scope.loanSubCategory = "";
    $scope.branch = "";

    $scope.loandSummaryReport = function () {
        cfpLoadingBar.start();
        // debugger;
        var branch = $scope.branch === undefined ? '' : $scope.branch;
        var LoanSearching = $scope.loanCategory === undefined ? '' : $scope.loanCategory;
        var loanSubSearching = $scope.loanSubCategory === undefined ? '' : $scope.loanSubCategory;
        $http.get(Url + "api/LoanSummaryreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&loantypeid=" + LoanSearching + "&loanid=" + loanSubSearching + "&branch=" + branch + "").then(
            function (response) {
                //debugger;
                console.log(response.data.cList);

                $scope.cList = response.data.cList;
                $scope.totalCount = response.data.totalCount;
         
                $scope.getTotal = function () {
                    //debugger;
                    var total = 0;
                    for (var i = 0; i < parseInt($scope.totalCount); i++) {
                        var product = $scope.cList[i];
                        if (product.approved_LoanAmt != null) {
                            total += parseInt(product.approved_LoanAmt);
                        }
                    }

                    return total;
                }
                $scope.getTotalPrincipal = function () {

                    var total = 0;
                    for (var i = 0; i < parseInt($scope.totalCount); i++) {
                        var product = $scope.cList[i];
                        if (product.totalrecivedamt != null) {
                            total += parseFloat(product.totalrecivedamt);
                        }
                    }

                    return total.toFixed(2);
                }
                $scope.getTotalInt = function () {

                    var total = 0;
                    for (var i = 0; i < parseInt($scope.totalCount); i++) {
                        var product = $scope.cList[i];
                        if (product.totalrecivedInt != null) {
                            total += parseFloat(product.totalrecivedInt);
                        }
                    }
                    return total.toFixed(2);
                }
                $scope.getTotaldueInt = function () {

                    var total = 0;
                    for (var i = 0; i < parseInt($scope.totalCount); i++) {
                        var product = $scope.cList[i];
                        if (product.totaldueInt != null) {
                            total += parseFloat(product.totaldueInt);
                        }
                    }
                    return total.toFixed(2);
                }
                $scope.getTotalduePri = function () {

                    var total = 0;
                    for (var i = 0; i < parseInt($scope.totalCount); i++) {
                        var product = $scope.cList[i];
                        if (product.totalduepri != null) {
                            total += parseFloat(product.totalduepri);
                        }
                    }
                    return total.toFixed(2);
                }
                //$scope.exportData = function () {
                   
                //    var blob = new Blob([document.getElementById('export').innerHTML], {
                //        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
                //    });
                //    saveAs(blob, "LoanSummaryreport.xls");
                //};

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
                    saveAs(bb, "LoanSummaryreport.xls");
                                };
               

            },
            function (err) {
                var error = err;
            });



    }

    $scope.GetFilterUsers = function (ClubData) {
        $scope.loandSummaryReport();
    }

    
    $scope.loandSummaryReport();

   
    $scope.pageChanged = function () {
        $scope.loandSummaryReport();
    };
    $scope.Getplantype = function (Searchtype) {
        $scope.loandSummaryReport();
    }
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.loandSummaryReport();
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














