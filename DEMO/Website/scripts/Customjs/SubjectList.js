var app = angular.module('subjectListApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});


app.controller('subjectListCtrl', function ($scope, $http, cfpLoadingBar) {

    // var Url = 'http://localhost:30170/Common/';
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 50; // Maximum number of items per page.   
    $scope.ClubData = {};
    $scope.Code = {};
    $scope.Name = {};
    $scope.Practical = "2";
    $scope.Compulsory = "2";
   
    

    $scope.GetSubjectListing = function () {
        cfpLoadingBar.start();
        var Searching = $scope.ClubData.search === undefined ? '' : $scope.ClubData.search;
        var SearchingCode = $scope.Code.search === undefined ? '' : $scope.Code.search;
        var SearchingName = $scope.Name.search === undefined ? '' : $scope.Name.search;
        console.log(Searching);
        console.log(SearchingCode);
        console.log(SearchingName);
        console.log($scope.Practical);
        console.log($scope.Compulsory);
        debugger;
        $http.get(Url + "api/getSubjectList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&search=" + Searching + "&searchcode=" + SearchingCode  + "&searchname=" + SearchingName + "&practical=" + $scope.Practical + "&compulsory=" + $scope.Compulsory + "").then(
            function (response) {
               
                $scope.qlist = response.data.qlist;
                $scope.totalCount = response.data.totalCount;

              


            },
            function (err) {
                var error = err;
            });
    }
   
    $scope.GetFilterUsers = function (ClubData) {
        $scope.GetSubjectListing();
    }
   

    //Loading employees list on first time
    $scope.GetSubjectListing();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetSubjectListing();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetSubjectListing();
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

