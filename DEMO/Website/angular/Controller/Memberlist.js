var app = angular.module('MemberlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('memberCtrl', function ($scope, $http, cfpLoadingBar) {
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.

    $scope.GetMemberList = function () {
        cfpLoadingBar.start();




        $http.get(Url + "api/memberList?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected /*+ "&search=" + Searching */ + "").then(
            function (response) {
                //debugger;

                console.log(response.data.memberList);
                $scope.memberList = response.data.memberList;
                $scope.totalCount = response.data.totalCount;

            },
            function (err) {
                var error = err;
            });
    }

    $scope.GetFilterUsers = function (Name) {
        $scope.GetMemberList();
    }

    //Loading employees list on first time
    $scope.GetMemberList();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetMemberList();

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
