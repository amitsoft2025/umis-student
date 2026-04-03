var app = angular.module('kycApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});


app.controller('kycCtrl', function ($scope, $http, cfpLoadingBar) {
    debugger;
    // var Url = 'http://localhost:30170/Common/';
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.getState = {};
    $scope.getkycdetailsslist = function () {
        cfpLoadingBar.start();

        //var countryId = $scope.Countrystate.ID == undefined ? '' : $scope.Countrystate.ID;

        //var countryCode = $scope.Countrystate.ShortName == undefined ? '' : $scope.Countrystate.ShortName;
        //var Name = $scope.Countrystate.CountryName == undefined ? '' : $scope.Countrystate.CountryName;
        //var isActive = $scope.Countrystate.IsActive == undefined ? '' : $scope.Countrystate.IsActive;
        $http.get(Url + "api/get-customer-kyclist?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "").then(
                       function (response) {
                           debugger;
                           console.log(response.data.kycdetailsslist);
                           $scope.KycDetails = response.data.kycdetailsslist;
                           $scope.totalCount = response.data.totalCount;

                           //   cfpLoadingBar.complete();
                           createCookie('admin', $scope.pageSizeSelected, 100);

                       },
                       function (err) {
                           var error = err;
                       });
    }


    $scope.GetFilterStates = function (responseData) {
        // showAlert(JSON.stringify(userData));
        $scope.getkycdetailsslist();
    }

    //Loading employees list on first time
    $scope.getkycdetailsslist();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.getkycdetailsslist();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getkycdetailsslist();
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
