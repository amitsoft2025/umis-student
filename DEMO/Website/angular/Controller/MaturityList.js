var app = angular.module('MaturitylistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('MaturityCtrl', function ($scope, $http, cfpLoadingBar) {
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
    $scope.GetMaturityList = function () {
        cfpLoadingBar.start();
        
        var Searching = $scope.Customer.search === undefined ? '' : $scope.Customer.search;
        var SearchingC = $scope.Code.search === undefined ? '' : $scope.Code.search;
        var Date = $scope.Date === undefined ? '' : $scope.Date;
        var DateTo = $scope.DateTo === undefined ? '' : $scope.DateTo;
        var SearchingN = $scope.Name.search === undefined ? '' : $scope.Name.search;


        $http.get(Url + "api/maturityList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching /*+ "&searchC=" + SearchingC + "&searchN=" + SearchingN + "&date=" + Date + "&dateTo=" + DateTo*/ + "").then(
            function (response) {

                
                console.log(response.data.maturityStructureList);
                $scope.cList = response.data.maturityStructureList;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }

    $scope.GetFilterUsers = function (Name) {
        $scope.GetMaturityList();
    }

    //Loading employees list on first time
    $scope.GetMaturityList();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetMaturityList();
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
