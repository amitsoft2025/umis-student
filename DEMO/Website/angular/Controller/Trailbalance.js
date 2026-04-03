
var app = angular.module('InvaccreportlistApp', ['chieffancypants.loadingBar', 'ngAnimate', 'ui.bootstrap']);
app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
    cfpLoadingBarProvider.latencyThreshold = 500;
});

app.controller('InvaccreportCtrl', function ($scope, $http, cfpLoadingBar) {
   // debugger;
    $scope.maxSize = 5;     // Limit number for pagination display number.
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->
    $scope.pageSizeSelected = 25; // Maximum number of items per page.
    $scope.ClubData = {};
    $scope.jsonToExport = {};
    $scope.formSearchtype  ="";
    $scope.fromdate = "";
    $scope.todate = "";
    $scope.customerid = "";
    $scope.name = "";
    $scope.Searchplanname = "";
    $scope.Searchplantype = "";
    $scope.GetHandsIds = function () {
        cfpLoadingBar.start();
        debugger;
        var sfromdate = $scope.fromdate === undefined ? '' : $scope.fromdate;
        var stodate = $scope.todate === undefined ? '' : $scope.todate;
       
        var sSearchplanname = $scope.Searchplanname === undefined ? '0' : $scope.Searchplanname;
      
 
        $http.get(Url + "api/trialbalance?PageIndex=" + $scope.pageIndex + "&PageSize=" + $scope.pageSizeSelected + "&branchid=" + sSearchplanname + "&fromdate=" + sfromdate + "&todate=" + stodate + "").then(
        function (response) {
           // console.log(response.data.accountdataList);
            $scope.cList = response.data.accountdataList;
            $scope.totalCount = response.data.totalCount;
         
            var total = 0;
            for (var i = 0; i < parseInt($scope.cList.length) ; i++)
            {
                var balance= (parseFloat($scope.cList[i].openopeningbalance) + parseFloat($scope.cList[i].cr)) - parseFloat($scope.cList[i].dr);
                if(balance>0)
                {
                    $scope.cList[i].groupName =  Math.abs(balance).toFixed(2);
                    $scope.cList[i].groupPrintname = "CR. " ;
                }
                else
                {
                    $scope.cList[i].groupName =  Math.abs(balance).toFixed(2);
                    $scope.cList[i].groupPrintname = "DR. " ;
                }
               
            }
           // console.log(response.data.accountdataList);
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
    $scope.Getplantype = function (Searchtype) {
        $scope.GetHandsIds();
    }
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