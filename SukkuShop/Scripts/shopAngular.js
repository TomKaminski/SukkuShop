var shopApp = angular.module("shopApp", []);
var itemsPerPage = 12;

shopApp.controller("ShopCtrl", ['$scope', '$http','$filter',function($scope, $http, $filter) {
    $scope.init = function (category) {
        $scope.sortMethod = "Nowosc";
        $scope.currentPage = 1;
        $scope.category = category;
        $scope.onlyavailable = false;
        if ($scope.category == "") {
            $http.get("/Sklep/GetProductByCategory").success(function(data) {
                $scope.products = data;
                $scope.subCategoryList = $scope.products.subcategoryList;
                $scope.selectedIndex = $scope.subCategoryList.length - 1;
                $scope.productList = filterProducts();
            }).
              error(function(xhr, status, error) {
                  alert(error);
                  console.log(xhr);
              });
            $scope.imgName = "inne";
        } else {
            $http.get("/Sklep/GetProductByCategory/" + $scope.category).success(function(data) {
                $scope.products = data;
                $scope.subCategoryList = $scope.products.subcategoryList;
                $scope.selectedIndex = $scope.subCategoryList.length - 1;
                $scope.productList = filterProducts();
                if ($scope.products.categoryId != 0)
                    $scope.imgName = category.toLowerCase();
                else
                    $scope.imgName = "inne";
            }).
              error(function (xhr, status, error) {
                  alert(error);
                  console.log(xhr);
              });
            
        }
    };
    $scope.AddToCart = function (id) {
        $http.post('/Koszyk/AddToCart/SetQuantity', { id: id, quantity: 1 }).
            success(function (data) {
                var plza = $("#cart-price-header").html().toLowerCase().replace('&nbsp;', '').replace('zł', '').replace('koszyk', '').replace(' ', '');
                var result = $.grep($scope.products.productList, function (e) { return e.Id == data.id; });

                if (parseFloat(plza) != parseFloat(data.value)) {
                    result[0].CartAmount++;
                    $("#cart-price-header").html("KOSZYK " + data.value + " zł");
                } else {
                    $("#addinfo" + id).html("&#10005;<span style='font-size:20px'>" + result[0].CartAmount + "</span>");
                    jQuery("#addinfo" + id).css("color", "red");
                    jQuery("#addinfo" + id).fadeOut(1000,function() {
                        jQuery(this).css("color", "green").html("&#10003;<span style='font-size:20px'>" + result[0].CartAmount + "</span>").fadeIn(1000);

                    });
                }
                    
            }).
            error(function (xhr, status, error) {
                alert(error);
                console.log(xhr.responseText);
            });
    }

    $scope.itemClicked = function($index) {
        $scope.selectedIndex = $index;
        $scope.currentPage = 1;
        $scope.productList = filterProducts();
    }

    $scope.setSortMethod = function(sortMethod) {
        $scope.sortMethod = sortMethod;
        $scope.currentPage = 1;
        $scope.productList = filterProducts();
    }

    $scope.setPage = function (page) {
        if (page <= 0)
            page = 1;
        if (page > $scope.pages)
            page = $scope.pages;
        $scope.currentPage = page;
        $scope.productList = filterProducts();
    }

    $scope.filterByCheckboxes = function () {
        $scope.currentPage = 1;
        $scope.productList = filterProducts();
    };

     function filterProducts() {
        var filteredProducts;
        if ($scope.selectedIndex != $scope.subCategoryList.length - 1)
            filteredProducts=$filter('filter')($scope.products.productList, { Category: $scope.subCategoryList[$scope.selectedIndex] });
        else
            filteredProducts = $scope.products.productList;

        if ($scope.onlyavailable) {
            var available = [];
            for (var j = 0; j < filteredProducts.length; j++) {
                if (filteredProducts[j].QuantityInStock != 0)
                    available.push(filteredProducts[j]);
            }
            filteredProducts = available;
        }
        var orderBy = $filter('orderBy');
        switch($scope.sortMethod) {
            case "Nowosc":
            {
                filteredProducts = orderBy(filteredProducts, "DateAdded", false);
                break;
            }
            case "Popularnosc":
            {
                filteredProducts = orderBy(filteredProducts, "OrdersCount", true);
                break;
            }
            case "CenaRosnaco":
            {
                filteredProducts = orderBy(filteredProducts, "PriceAfterDiscount", false);
                break;
            }
            case "CenaMalejaco":
            {
                filteredProducts = orderBy(filteredProducts, "PriceAfterDiscount", true);
                break;
            }
            case "Promocja":
            {
                filteredProducts = orderBy(filteredProducts, "Promotion", true);
                break;
            }

        }
        $scope.pages = Math.ceil(filteredProducts.length / 12);
        var pages = [];
        for (var i = 1; i <= $scope.pages; i++) {
            pages.push(i);
        }
        $scope.tableOfPages = pages;
        return filteredProducts.slice($scope.currentPage * 12 - 12, $scope.currentPage * 12);
     }

}]);

function currency(n) {
    n = parseFloat(n);
    return isNaN(n) ? false : n.toFixed(2);
}

jQuery(document).ready(function () {

    jQuery(".product-title").each(function() {
        var plz = jQuery(this);
        $clamp(plz, { clamp: 2 });
    });

    jQuery('#add-to-cart-info').hide();
    jQuery("ul.sort-menu div.plz").click(function () {
        jQuery("ul.sort-menu div.plz").css("opacity", "0.6");
        jQuery("ul.sort-menu li.sort-box #sort-text").css("font-weight", "normal");
        jQuery(this).css("opacity", "1");
        jQuery(this).siblings("#sort-text").css("font-weight", "bold");
        jQuery(this).parent().siblings("#sort-text").css("font-weight", "bold");
    });
});
