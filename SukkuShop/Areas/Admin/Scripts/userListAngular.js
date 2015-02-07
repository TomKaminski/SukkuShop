function showAjaxLoader() {
    var loaderDiv = jQuery("#ajax-processing");
    if (loaderDiv.length === 0) {
        jQuery("body").append("<div id='ajax-processing'></div>");
        loaderDiv = jQuery("#ajax-processing");
    }
    loaderDiv.show();
}

function showAjaxTick() {
    var loaderDivv = jQuery("#ajax-completetick");
    if (loaderDivv.length === 0) {
        jQuery("body").append("<div id='ajax-completetick'>&#10004;</div>");
        loaderDivv = jQuery("#ajax-completetick");
    }
    loaderDivv.show();
}

function fadeOutAjaxTick() {
    jQuery("#ajax-completetick").stop().fadeOut(1000);
}
function hideAjaxTick() {
    jQuery("#ajax-completetick").stop().hide();
}


function hideAjaxLoader() {
    jQuery("#ajax-processing").hide();
}

jQuery(document).bind('mousemove', function (e) {
    jQuery('#ajax-processing,#ajax-completetick').css({
        left: e.pageX + 20,
        top: e.pageY
    });
});
jQuery(document).ready(function () {
    
    showAjaxLoader();
    hideAjaxLoader();
    showAjaxTick();
    hideAjaxTick();
});

var adminApp = angular.module("adminApp", []);
var itemsPerPage = 10;

adminApp.controller("AdminUserCtrl",['$scope', '$http','$filter', function ($scope, $http, $filter) {
    var orderBy = $filter('orderBy');
    $scope.init = function () {
        $scope.selectedIndex = 1;
        $scope.currentPage = 1;
        $scope.textFilter = "";
        $http.get("/Admin/Klienci/GetUserList").success(function (data) {
            $scope.usersTotal = data;
            $scope.usersOperative = $scope.usersTotal;
            $scope.usersList = filterUsers($scope.usersTotal);
        });
    };

    $scope.keyPressRabat = function ($event) {
        if ($event.which != 8 && $event.which != 0 && ($event.which < 48 || $event.which > 57)) {
            $event.preventDefault();
            return false;
        }            
        return true;
    }

    $scope.validateInput = function (rabat,$index)
    {
        if (rabat > 100) 
            $scope.usersList[$index].Rabat = 100;        
    }

    $scope.setDiscount = function (id,rabat) {

            showAjaxLoader();
            $http.post('/Admin/Klienci/SetDiscount', { id: id, rabat: rabat }).
                success(function (data) {
                    if (data == true) {
                        var result = $.grep($scope.usersTotal, function (e) { return e.Id == id; });
                        result[0].Rabat = rabat;
                    }
                    hideAjaxLoader();
                    showAjaxTick();
                    fadeOutAjaxTick();
                    
                    $scope.usersList = filterUsers($scope.usersOperative);
                }).
                error(function (data) {

                });
        
    }

    $scope.setPage = function (page) {
        if (page <= 0)
            page = 1;
        if (page > $scope.pages)
            page = $scope.pages;
        $scope.currentPage = page;
        $scope.usersList = filterUsers($scope.usersOperative);
    }

    $scope.filterByName = function () {
        $scope.currentPage = 1;
        var filterByNameTab = $filter('filter')($scope.usersTotal, { Email: $scope.textFilter });
        $scope.usersList = filterUsers(filterByNameTab);
    }

    $scope.orderByMhm = function (mhm, reverse, index) {
        $scope.selectedIndex = index;
        $scope.currentPage = 1;
        var filtered = orderBy($scope.usersTotal, mhm, reverse);
        $scope.usersList = filterUsers(filtered);
    }

    function filterUsers(tab) {
        var usersBeforeFilter = [];
        var users = tab;
       
        if ($scope.textFilter)
            users = $filter('filter')(tab, { Email: $scope.textFilter });

        $scope.usersOperative = users;
        $scope.pages = Math.ceil(users.length / 10);
        var pages = [];
        for (var j = 1; j <= $scope.pages; j++) {
            pages.push(j);
        }
        $scope.tableOfPages = pages;
        return users.slice($scope.currentPage * itemsPerPage - itemsPerPage, $scope.currentPage * itemsPerPage);
    }

}]);