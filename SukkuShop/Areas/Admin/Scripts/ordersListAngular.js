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

adminApp.controller("AdminOrderCtrl", ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    var orderBy = $filter('orderBy');
    $scope.init = function () {
        $scope.selectedIndex = 1;
        $scope.currentPage = 1;
        $scope.przyjete = false;
        $scope.zakonczone = false;
        $scope.oczekujace = false;
        $scope.realizowane = false;
        $scope.textFilter = "";
        $http.get("/Admin/Zamowienia/GetOrdersList").success(function (data) {
            $scope.ordersTotal = data;
            $scope.ordersOperative = $scope.ordersTotal;
            $scope.ordersList = filterOrders($scope.ordersTotal);
        });
    };

    $scope.filterByName = function () {
        $scope.przyjete = false;
        $scope.zakonczone = false;
        $scope.oczekujace = false;
        $scope.realizowane = false;
        $scope.selectedIndex = 1;
        $scope.currentPage = 1;
        var filterByNameTab = $filter('filter')($scope.ordersTotal, { id: $scope.textFilter });
        $scope.ordersList = filterOrders(filterByNameTab);
    }

    $scope.DownloadInvoice = function() {
        showAjaxLoader();
        $http.get('/Admin/Zamowienia/DownloadInvoice').
            success(function () {               
                hideAjaxLoader();
                showAjaxTick();
                fadeOutAjaxTick();
            }).
            error(function (xhr, status, error) {
                alert(error);
                console.log(xhr.responseText);
            });
    }

    //$scope.ChangeOrderState = function (id, value) {
    //    showAjaxLoader();
    //    $http.post('/Admin/Zamowienia/ChangeOrderState', { id: id, value: value }).
    //        success(function (data) {
    //                var result = $.grep($scope.ordersTotal, function (e) { return e.id == id; });
    //                result[0].stan = value;
    //                result[0].orderOpts = data;
    //                result[0].selectedOpt = value;
                
    //            hideAjaxLoader();
    //            showAjaxTick();
    //            fadeOutAjaxTick();
    //            $scope.ordersList = filterOrders($scope.ordersTotal);
    //        }).
    //        error(function (xhr, status, error) {
    //            alert(error);
    //            console.log(xhr.responseText);
    //        });
    //}

    $scope.filterByCheckboxes = function () {
        $scope.currentPage = 1;
        $scope.ordersList = filterOrders($scope.ordersTotal);

    };

    $scope.setPage = function (page) {
        if (page <= 0)
            page = 1;
        if (page > $scope.pages)
            page = $scope.pages;
        $scope.currentPage = page;
        $scope.ordersList = filterOrders($scope.ordersOperative);
    }

    $scope.orderByMhm = function (mhm, reverse, index) {
        $scope.selectedIndex = index;
        $scope.currentPage = 1;
        var filtered = orderBy($scope.ordersTotal, mhm, reverse);
        $scope.ordersList = filterOrders(filtered);
    }

    function filterOrders(tab) {
        var ordersBeforeFilter = tab;
        var orders = [];
       

        for (var k = 0; k < ordersBeforeFilter.length; k++) {
            if ($scope.przyjete == false && $scope.zakonczone == false && $scope.realizowane == false && $scope.oczekujace == false) {
                orders.push(ordersBeforeFilter[k]);
            } else {
                if (($scope.przyjete && ordersBeforeFilter[k].stan == "Przyjęte") || ($scope.zakonczone && (ordersBeforeFilter[k].stan == "Anulowane" || ordersBeforeFilter[k].stan == "Wysłane")) || ($scope.realizowane && ordersBeforeFilter[k].stan == "Realizowane") || ($scope.oczekujace && ordersBeforeFilter[k].stan == "Oczekujące")) {
                    orders.push(ordersBeforeFilter[k]);
                }
            }
        }

        $scope.ordersOperative = orders;
        $scope.pages = Math.ceil(orders.length / itemsPerPage);
        var pages = [];
        for (var j = 1; j <= $scope.pages; j++) {
            pages.push(j);
        }
        $scope.tableOfPages = pages;
        return orders.slice($scope.currentPage * itemsPerPage - itemsPerPage, $scope.currentPage * itemsPerPage);
    }

}]);