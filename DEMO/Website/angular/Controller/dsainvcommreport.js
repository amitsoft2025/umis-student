
var app1 = angular.module('manageweightbusinesstApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app1.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});


//app.filter('Demofilter', function () {
//    return function (input) {
//        return input + " Tutorial"
//    }
//});
app1.controller('manageweightbusinessCtrl', function ($scope, $http, cfpLoadingBar) {
   
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 100; // Maximum number of items per page.
    $scope.BranchData = {};
    $scope.Name = {};
    $scope.Represantative = {};
    $scope.Contact = {};
    $scope.Status = "true";
    $scope.Date;
    $scope.DateTo;
    $scope.totalbusinessAmount=0;
    $scope.totalcommissionAmount=0;
    $scope.GetdsaList = function () {
        cfpLoadingBar.start();
        var SearchingM = $scope.Name.search === undefined ? '' : $scope.Name.search;
        var SearchingR = $scope.Represantative.search === undefined ? '' : $scope.Represantative.search;
        var SearchingC = $scope.Contact.search === undefined ? '' : $scope.Contact.search;
        var Status = $scope.Status === undefined ? '' : $scope.Status;

        //debugger;
        var Date = $scope.Date === undefined ? '' : $scope.Date;
        var DateTo = $scope.DateTo === undefined ? '' : $scope.DateTo;
        $http.get(Url + "api/dsainvestmnetcommreport?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&Representativecode=" + SearchingR + "&fromdate=" + Date + "&toddate=" + DateTo + "&branchid=" + 0 + "").then(
            function (response) {
                //debugger;
                console.log(response.data.dsAdataList);
                $scope.dsAdataList = response.data.dsAdataList;
                $scope.totalCount = response.data.totalCount;
                $scope.totalbusinessAmount = 0;
                $scope.totalcommissionAmount = 0;
                for (var i = 0; i < parseInt($scope.dsAdataList.length) ; i++) {

                    $scope.totalbusinessAmount += parseFloat(parseFloat($scope.dsAdataList[i].businessAmount));
                    $scope.totalcommissionAmount += parseFloat(parseFloat($scope.dsAdataList[i].commissionAmount));
                  

                }
                //   cfpLoadingBar.complete();
                // createCookie('admin', $scope.pageSizeSelected, 100);
             
            },
            function (err) {
                var error = err;
            });
    }
   // console.log($scope.DSAdataList)
  
    $scope.GetFilterUsers = function (Name) {
        $scope.GetdsaList();
    }
    $scope.GetdsaList();

    $scope.GetStatusList = function (Status) {

        $scope.GetdsaList();
    }
    $scope.GetDateList = function (Date, DateTo) {

        $scope.GetdsaList();
    }

    //Loading employees list on first time
  
    //  //This method is calling from pagination number
    $scope.pageChanged = function () {
        $scope.GetdsaList();
    };
    $scope.dataTableOpt = {
        //custom datatable options 
        // or load data through ajax call also
        "aLengthMenu": [[10, 50, 100, -1], [10, 50, 100, 'All']],
    };
    //This method is calling from dropDown
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.GetdsaList();
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

