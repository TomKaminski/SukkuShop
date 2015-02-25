function isValidPrice(price) {
    var pattern = new RegExp(/^[1-9][0-9]{0,2}$/);
    return pattern.test(price);
};

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

var adminApp = angular.module("adminApp", ['ngAnimate']);
var itemsPerPage = 12;

adminApp.controller("AdminCategoriesCtrl", ["$scope", "$http", '$filter', function ($scope, $http, $filter) {
    var orderBy = $filter('orderBy');
    $scope.newSubCategoryName = "";
    $scope.newEditorName = "";
    $scope.editCategoryNameActive = false;
    $scope.editCategoryDiscountActive = false;
    $scope.addCategoryActive = false;
    $scope.invalidCategoryForm = false;
    $scope.invalidDiscountForm = false;
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
    }

    $scope.StartNameEdit = function (id, text, upper) {
        if (!$scope.editCategoryNameActive) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            $scope.newEditorName = text;
            var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
            result2[0].editNameActive = true;
            $scope.editCategoryNameActive = true;
        }
    }

    $scope.discountEditChange = function (newdiscount, id, upper) {
        if (parseInt(newdiscount) > 100) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
            result2[0].NewPromotion = 100;
        }
            
    }

    $scope.discountMainEditChange = function (id, discount) {
        if (parseInt(discount) > 100) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
            result[0].Promotion = 100;
        }

    }

    $scope.addCategoryForm = function (upper) {
        var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
        result[0].newDiscount = 0;
        $scope.addCategoryActive = true;
    }

    $scope.cancelCategoryForm = function () {
        $scope.addCategoryActive = false;
        $scope.invalidCategoryForm = false;
        $scope.invalidDiscountForm = false;
    }

    $scope.CancelNameEdit = function (id, upper) {
        if ($scope.editCategoryNameActive) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
            result2[0].editNameActive = false;
            $scope.editCategoryNameActive = false;
        }
    }

    $scope.SubmitNameEdit = function (id, name, upper) {
        showAjaxLoader();
        $http.post('/Admin/Podkategorie/EditCategoryName', { name: name, id: id }).
            success(function (data) {
                if (data != false) {
                    var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
                    var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
                    result2[0].Name = name;
                    result2[0].editNameActive = false;
                    $scope.editCategoryNameActive = false;
                }
                hideAjaxLoader();
                showAjaxTick();
                fadeOutAjaxTick();
            }).
            error(function (data) {
            });
    }

    $scope.StartMainDiscountEdit = function (text, upper) {

            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            result[0].editDiscountActive = true;
        
    }

    $scope.StartDiscountEdit = function (id, text, upper) {
        if (!$scope.editCategoryDiscountActive) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
            result2[0].NewPromotion = text;
            result2[0].editDiscountActive = true;
            $scope.editCategoryDiscountActive = true;
        }
    }

    $scope.CancelDiscountEdit = function (id,upper) {
        if ($scope.editCategoryDiscountActive) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
            var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
            result2[0].editDiscountActive = false;
            $scope.editCategoryDiscountActive = false;
        }
    }

    $scope.CancelMainDiscountEdit = function (id) {
        var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
        result[0].editDiscountActive = false;
        
    }

    $scope.SubmitDiscountEdit = function (id, discount,upper) {
        showAjaxLoader();
        $http.post('/Admin/Podkategorie/EditCategoryDiscount', { discount: discount, id: id }).
            success(function (data) {
                if (data != false) {
                    var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
                    var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
                    result2[0].Promotion = discount;
                    result2[0].editDiscountActive = false;
                    $scope.editCategoryDiscountActive = false;
                }
                hideAjaxLoader();
                showAjaxTick();
                fadeOutAjaxTick();
            }).
            error(function (data) {
            });
    }

    $scope.SubmitMainDiscountEdit = function (id, discount) {
        showAjaxLoader();
        $http.post('/Admin/Podkategorie/EditCategoryDiscount', { discount: discount, id: id }).
            success(function (data) {
                if (data != false) {
                    var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
                    result[0].Promotion = discount;
                    result[0].editDiscountActive = false;
                }
                hideAjaxLoader();
                showAjaxTick();
                fadeOutAjaxTick();
            }).
            error(function (data) {
            });
    }

    $scope.keyPressPrice = function ($event) {
        if ($event.which != 8 && $event.which != 0 && ($event.which < 48 || $event.which > 57)) {
            $event.preventDefault();
            return false;
        } else {
            return true;
        }
    }

    $scope.validateCategoryDiscountForm = function (name, discount,id) {
        if (isValidPrice(discount)) {
            $scope.invalidDiscountForm = false;
        }
        if (name != '' && discount != '') {
            $scope.invalidCategoryForm = false;
        }
        if (discount > 100) {
            var result = $.grep($scope.categories, function (e) { return e.CategoryId === id; });
            result[0].newDiscount = 100;
        }
    }

    $scope.validateCategoryForm = function (name, discount) {
        if (name != '' && discount != '') {
            $scope.invalidCategoryForm = false;
        }
    }

    $scope.submitCategoryForm = function (upper, name, discount) {
        if (discount != '' && name != '' && isValidPrice(discount)) {
            showAjaxLoader();
            $http.post('/Admin/Podkategorie/AddSubCategory', { upperid: upper, name: name, discount: discount }).
                success(function(data) {
                    if (data != false) {
                        var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });  
                        result[0].subCategories.push(data);
                    }
                    $scope.addCategoryActive = false;
                    hideAjaxLoader();
                    showAjaxTick();
                    fadeOutAjaxTick();
                }).
                error(function(data) {
                });
        } else {
            $scope.invalidCategoryForm = true;
            if (!isValidPrice(price)) {
                $scope.invalidDiscountForm = true;
            }
        }
    }

    $scope.deleteCategoryAsk = function (id,upper) {
        var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
        var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
        if (result2.length == 0) {
        } else if (result2.length == 1) {
            result2[0].canDelete = true;
        } else {
            // multiple items found
        }        
    }

    $scope.noDeleteCategory = function (id,upper) {
        var result = $.grep($scope.categories, function (e) { return e.CategoryId === upper; });
        var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId === id; });
        if (result2.length == 0) {
        } else if (result2.length == 1) {
            result2[0].canDelete = false;
        } else {
            // multiple items found
        }
    }

    $scope.DeleteSubCategory = function (id, upper) {
            showAjaxLoader();
            $http.post('/Admin/Podkategorie/DeleteSubCategory', { id: id }).
          success(function (data) {
              if (data == true) {
                  var result = $.grep($scope.categories, function (e) { return e.CategoryId == upper; });
                  var result2 = $.grep(result[0].subCategories, function (e) { return e.CategoryId != id; });
                  result[0].subCategories = result2;
                  $scope.categories = $.grep($scope.categories, function (e) { return e.CategoryId != upper; });
                  $scope.categories.push(result[0]);
                  $scope.categories = orderBy($scope.categories, "CategoryId", false);
              }
              hideAjaxLoader();
              showAjaxTick();
              fadeOutAjaxTick();
          }).
          error(function (data) {
          });
        }
}]);