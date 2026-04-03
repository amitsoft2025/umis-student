var app = angular.module('loanaccreportlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('loanaccreportCtrl', function ($scope, $http, cfpLoadingBar) {
    // debugger;
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.ClubData = {};
    $scope.jsonToExport = {};
    $scope.loanCategory = "";
    $scope.branch = "";
    $scope.fromdate = "";
    $scope.todate = "";
    $scope.Code = {};
    $scope.Name = {};
    $scope.App = {};
    $scope.Status = "";
    $scope.loanSubCategory = "";

    $scope.loanapplicationeportetails = function () {
        cfpLoadingBar.start();
        // debugger;
        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;
        var LoanSearching = $scope.loanCategory === undefined ? '' : $scope.loanCategory;
        var branch = $scope.branch === undefined ? '' : $scope.branch;
        var sfromdate = $scope.fromdate === undefined ? '' : $scope.fromdate;
        var stodate = $scope.todate === undefined ? '' : $scope.todate;
        var scustomerid = $scope.Code.search === undefined ? '' : $scope.Code.search;
        var sname = $scope.Name.search === undefined ? '' : $scope.Name.search;
        var sapp = $scope.App.search === undefined ? '' : $scope.App.search;
        var loanSubSearching = $scope.loanSubCategory === undefined ? '' : $scope.loanSubCategory;
        var Status = $scope.Status === undefined ? '' : $scope.Status;

        $http.get(Url + "api/LoanApplicationreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "&customerid=" + scustomerid + "&name=" + sname + "&fromdate=" + sfromdate + "&todate=" + stodate + "&loantypeid=" + LoanSearching + "&loanid=" + loanSubSearching + "&app=" + sapp + "&branch=" + branch + "&status=" + Status + "").then(
            function (response) {
                //debugger;
                console.log(response.data.cList);
                $scope.cList = response.data.cList;
                $scope.totalCount = response.data.totalCount;
                // $scope.jsonToExport = response.data.investmnetclosedetailsdataList;

            },
            function (err) {
                var error = err;
            });
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
            saveAs(bb, "LoanApplicationreport.xls");
        };


    }
    $scope.GetFilterUsers = function (ClubData) {
        $scope.loanapplicationeportetails();
    }

    //Loading employees list on first time
    $scope.loanapplicationeportetails();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.loanapplicationeportetails();
    };
    $scope.Getplantype = function (Searchtype) {
        $scope.loanapplicationeportetails();
    }
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.loanapplicationeportetails();
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
