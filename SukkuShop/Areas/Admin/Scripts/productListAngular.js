function showAjaxLoader() {
    var loaderDiv = jQuery("#ajax-processing");
    if (loaderDiv.length === 0) {
        jQuery("body").append("<div id='ajax-processing'></div>");
        loaderDiv = jQuery("#ajax-processing");
    }
    loaderDiv.show();
}
function hideAjaxLoader() {
    jQuery("#ajax-processing").hide();
}
jQuery(document).ready(function() {
    jQuery(document).bind('mousemove', function(e) {
        jQuery('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });
});

var adminApp = angular.module("adminApp", []);
var itemsPerPage = 12;

adminApp.controller("AdminProdCtrl", ['$scope', '$http','$filter',function ($scope, $http, $filter) {
    var orderBy = $filter('orderBy');

    $scope.init = function (name,id) {
        $scope.selectedIndex = 1;
        $scope.selectedCategory = parseInt(id);
        $scope.currentPage = 1;
        $scope.currentFilter = 0;
        $scope.textFilter = name;
        $scope.published = false;
        $scope.isready = false;
        $scope.wrongmodel = false;
        $scope.infoboxActive = false;
        $http.get("/Admin/Produkty/GetProductList").success(function (data) {
            $scope.productsTotal = data.products;
            $scope.productsOperative = $scope.productsTotal;
            $scope.categories = data.categories;
            $scope.productsList = filterProducts($scope.productsTotal);
        });
    };

    $scope.filterByCheckboxes = function () {
        $scope.currentPage = 1;
        $scope.productsList = filterProducts($scope.productsTotal);

    };

    $scope.orderByMhm = function (mhm, reverse, index) {
        $scope.selectedIndex = index;
        $scope.currentPage = 1;
        var filtered = orderBy($scope.productsTotal, mhm, reverse);
        $scope.productsList = filterProducts(filtered);
    }

    $scope.deleteitemask = function (id) {
        if ($scope.infoboxActive == false) {
            var result = $.grep($scope.productsTotal, function(e) { return e.ProductId == id; });
            if (result.length == 0) {
            } else if (result.length == 1) {
                result[0].canDelete = true;
            } else {
                // multiple items found
            }
        }
    }

    $scope.nodeleteitem = function (id) {
        var result = $.grep($scope.productsTotal, function (e) { return e.ProductId == id; });
        if (result.length == 0) {
        } else if (result.length == 1) {
            result[0].canDelete = false;
        } else {
            // multiple items found
        }
    }

    $scope.deleteitem = function (id) {
        if ($scope.infoboxActive == false) {                    
        showAjaxLoader();
            $http.post('/Admin/Produkty/DeleteProduct', { id: id }).
          success(function (data) {
              if (data == true) {
                  $scope.productsTotal = $.grep($scope.productsTotal, function (e) { return e.ProductId != id; });
                  $scope.productsList=filterProducts($scope.productsTotal);
              }
              hideAjaxLoader();
          }).
          error(function (data) {
          });
        }
    }

    $scope.getinfo = function (id) {
        if ($scope.infoboxActive == false) {
            $scope.infoboxActive = true;
            var result = $.grep($scope.productsTotal, function(e) { return e.ProductId == id; });
            if (result.length == 0) {
            } else if (result.length == 1) {
                result[0].showinfo = true;
            } else {
                // multiple items found
            }
        }
    }

    $scope.closeinfobox = function (id, $event) {
        $scope.infoboxActive = false;
        var result = $.grep($scope.productsTotal, function (plz) { return plz.ProductId == id; });
        if (result.length == 0) {
        } else if (result.length == 1) {
            result[0].showinfo = false;
        } else {
            // multiple items found
        }
        if ($event)
            $event.stopPropagation();
    }

    $scope.itemClicked = function (id, $event) {
        $scope.textFilter = "";
        if (id == $scope.selectedCategory) 
            $scope.selectedCategory = 0;
        else
            $scope.selectedCategory = id;
        $scope.currentPage = 1;
        $scope.productsList = filterProducts($scope.productsTotal);
        if ($event) 
            $event.stopPropagation();
    }

    $scope.publish = function (id) {
        if ($scope.infoboxActive == false) {
            showAjaxLoader();
            $http.post('/Admin/Produkty/PublishProduct', { id: id }).
                success(function(data) {
                    if (data == true) {
                        var result = $.grep($scope.productsTotal, function(e) { return e.ProductId == id; });
                        if (result.length == 0) {
                        } else if (result.length == 1) {
                            result[0].Published = true;
                        } else {
                            // multiple items found
                        }
                    }
                    hideAjaxLoader();
                    $scope.productsList = filterProducts($scope.productsOperative);
                }).
                error(function(data) {

                });
        }
    }

    $scope.unpublish = function (id) {
        if ($scope.infoboxActive == false) {
            showAjaxLoader();
            $http.post('/Admin/Produkty/UnpublishProduct', { id: id }).
                success(function(data) {
                    if (data == true) {
                        var result = $.grep($scope.productsTotal, function(e) { return e.ProductId == id; });
                        if (result.length == 0) {
                        } else if (result.length == 1) {
                            result[0].Published = false;
                        } else {
                            // multiple items found
                        }
                    }
                    $scope.productsList = filterProducts($scope.productsOperative);
                    hideAjaxLoader();
                }).
                error(function(data) {

                });
        }
    }

    $scope.setPage = function (page) {
        if (page <= 0)
            page = 1;
        if (page > $scope.pages)
            page = $scope.pages;
        $scope.currentPage = page;
        $scope.productsList = filterProducts($scope.productsOperative);
    }

    $scope.filterByName = function () {
        $scope.published = false;
        $scope.isready = false;
        $scope.wrongmodel = false;
        $scope.selectedCategory = 0;
        $scope.currentPage = 1;
        var filterByNameTab = $filter('filter')($scope.productsTotal, { Name: $scope.textFilter });
        $scope.productsList = filterProducts(filterByNameTab);
    }

    function filterProducts(tab) {
         var productsBeforeFilter = [];
         var products = [];
         if ($scope.selectedCategory != 0) {
             for (var i = 0; i < tab.length; i++) {
                 if (tab[i].CategoryId == $scope.selectedCategory || tab[i].upper == $scope.selectedCategory)
                     productsBeforeFilter.push(tab[i]);
             }
         } else {
             productsBeforeFilter = tab;
         }

         for (var k = 0; k < productsBeforeFilter.length; k++) {
             if ($scope.published == false && $scope.isready == false && $scope.wrongmodel == false) {
                 products.push(productsBeforeFilter[k]);
             } else {
                 if (($scope.published && productsBeforeFilter[k].Published) || ($scope.isready && productsBeforeFilter[k].WrongModel == false && productsBeforeFilter[k].Published == false) || ($scope.wrongmodel && productsBeforeFilter[k].WrongModel == true)) {
                     products.push(productsBeforeFilter[k]);
                 }
             }
         }
         if ($scope.textFilter)
             products = $filter('filter')(products, { Name: $scope.textFilter });

         $scope.productsOperative = products;
         $scope.pages = Math.ceil(products.length / itemsPerPage);
        var pages = [];
        for (var j = 1; j <= $scope.pages; j++) {
            pages.push(j);
        }
        $scope.tableOfPages = pages;
        return products.slice($scope.currentPage * itemsPerPage - itemsPerPage, $scope.currentPage * itemsPerPage);
     }
}]);