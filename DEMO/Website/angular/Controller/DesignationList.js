var app = angular.module('DesignationlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;

});

app.controller('DesignationCtrl', function ($scope, $http, cfpLoadingBar) {
    // debugger;
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.IsVisible = false;

    $scope.GetDesignationList = function () {
        cfpLoadingBar.start();
        // debugger;
       
        $http.get(Url + "api/Designationreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected +  "").then(
            function (response) {
                //debugger;
                console.log(response.data.dList);

                $scope.dList = response.data.dList;
                $scope.totalCount = response.data.totalCount;
               
              
            },
            function (err) {
                var error = err;
            });



    }

    $scope.GetFilterUsers = function (ClubData) {
        $scope.GetDesignationList();
    }
    $scope.Editdata = function (Id) {
        $scope.IsVisible = $scope.IsVisible ? false : true;
    }
    //Loading employees list on first time
    $scope.GetDesignationList();

    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetDesignationList();
    };
    $scope.Getplantype = function (Searchtype) {
        $scope.GetDesignationList();
    }
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetDesignationList();
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
