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
jQuery(document).ready(function () {
    jQuery(document).bind('mousemove', function (e) {
        jQuery('#ajax-processing,#ajax-completetick').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });
    showAjaxLoader();
    hideAjaxLoader();
    showAjaxTick();
    hideAjaxTick();
});

var adminApp = angular.module("adminApp", []);
var itemsPerPage = 12;

adminApp.controller("AdminCategoriesCtrl", ["$scope", "$http", '$filter', function ($scope, $http) {
    $scope.newSubCategoryName = "";
    $scope.newSubCategoryPromotion = "";
    $scope.newEditorName = "";
    $scope.newEditorPromotion = "";
    $scope.editCategoryNameActive = false;
    $scope.editCategoryDiscountActive = false;
    $scope.init = function () {
        $http.get("/Admin/Podkategorie/GetCategoriesList").success(function (data) {
            $scope.categories = data;
        });
    };

    $scope.toggleSubCategories = function (id) {
        var result2 = $.grep($scope.categories, function (e) { return e.CategoryId !== id; });
        for (var i = 0; i < result2.length; i++) {
            result2[i].subCategoriesActive = false;
        }
        var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
        if (result[0].subCategoriesActive)
            result[0].subCategoriesActive = false;
        else
            result[0].subCategoriesActive = true;

        $scope.StartNameEdit = function(id, text) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
            result[0].Name = text;
            result[0].editNameActive = true;

        }
    }
}]);