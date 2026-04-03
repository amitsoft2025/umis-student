var app = angular.module('guestuserlistApp', ['chieffancypants.loadingBar','ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('guestuserlistCtrl', function ($scope, $http, cfpLoadingBar) {
   

    $scope.UserType = [{ id: 0, Name: "Facebook User" }, { id: 1, Name: "Guest" }];
 
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected =25; // Maximum number of items per page.
    $scope.UserData = {};
    $scope.getEmployeeList = function () {
        cfpLoadingBar.start();    
        var userype = $scope.UserData.Usertype == undefined ? '' : $scope.UserData.Usertype;
        var playerID = $scope.UserData.PlayerId == undefined ? '' : $scope.UserData.PlayerId;
        var Emailid = $scope.UserData.EmailId == undefined ? '' : $scope.UserData.EmailId;
        var Name = $scope.UserData.UserName == undefined ? '' : $scope.UserData.UserName;
        var point = $scope.UserData.Points == undefined ? '' : $scope.UserData.Points;
        $http.get(ServiceUrl + "admin/UserStatuspage?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&userype=" + userype + "&plyaerid=" + playerID + "&Emailid=" + Emailid + "&point=" + point + "").then(
                       function (response) {
                           console.log(response.data.GameUsers);
                           $scope.employees = response.data.GameUsers;
                           $scope.totalCount = response.data.totalCount;
                        //   cfpLoadingBar.complete();
                         
                       },
                       function (err) {
                           var error = err;
                       });
    }

    $scope.GetFilterUsers = function(userData)
    {
       // showAlert(JSON.stringify(userData));
        $scope.getEmployeeList();
    }

    //Loading employees list on first time
    $scope.getEmployeeList();

  //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.getEmployeeList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getEmployeeList();
    };

    // Sorting by Table head
    $scope.propertyName =null;
    $scope.reverse = true;


    $scope.sortBy = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };
    //


    $scope.numDifferentiation = function (val)
    {
        if (val >= 10000000) val = (val / 10000000).toFixed(2) + ' Cr';
        else if (val >= 100000) val = (val / 100000).toFixed(2) + ' Lac';
        else if (val >= 1000) val = (val / 1000).toFixed(2) + ' K';
        return val;
    }

 
});
