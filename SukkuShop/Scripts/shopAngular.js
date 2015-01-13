function currency(n) {
    n = parseFloat(n);
    return isNaN(n) ? false : n.toFixed(2);
}

function plz(data) {
    var plza = parseFloat($("#cart-price-header").html().toLowerCase().replace('&nbsp;', ' ').replace('zł', '').replace('koszyk', '').replace(',', '.').replace(' ', ''));
    if (plza != data.value) {
        var obj = $("#img" + data.id).parent().parent().parent().parent().children('div.add-to-cart-info');
        obj.css('color', 'green');
        obj.html('&#10004;');
        obj.stop().show("fast", function () {
            obj.fadeOut(2500);
        });
        $("#cart-price-header").html('koszyk ' + data.value + ' zł');
        //var cart = $("#img" + data.id);
        //var imgtodrag = cart.parent().parent().parent().parent().parent().find("img").eq(0);
        //if (imgtodrag) {
        //    imgtodrag.clone()
        //        .offset({
        //            top: imgtodrag.offset().top,
        //            left: imgtodrag.offset().left
        //        })
        //        .addClass("flying-product-start")
        //        .css('position', 'absolute')
        //        .appendTo(jQuery('body')).
        //        animate({
        //            'top': cart.offset().top + 10,
        //            'left': cart.offset().left + 10,
        //            'width': '10px',
        //            'height': '10px',
        //            'position': 'absolute'
        //        }, 1000, function() {
        //            jQuery(this).animate({
        //                'width': 0,
        //                'height': 0
        //            }, function() {
        //                jQuery(this).detach();
        //            });
        //        });
        //}
    } else {
        var obj2 = $("#img" + data.id).parent().parent().parent().parent().children('div.add-to-cart-info');

        obj2.css('color', 'red');
        obj2.html('&#10008;');
        obj2.stop().show("fast", function () {
            obj2.fadeOut(2500);
        });
    }
    
};


var shopApp = angular.module("shopApp", []);
var itemsPerPage = 12;

shopApp.controller("ShopCtrl", function($scope, $http, $filter) {
    $scope.init = function (category) {
        $scope.sortMethod = "Nowosc";
        $scope.currentPage = 1;
        $scope.category = category;
        if ($scope.category == "") {
            $http.get("/Sklep/GetProductByCategory").success(function(data) {
                $scope.products = data;
                $scope.subCategoryList = $scope.products.subcategoryList;
                $scope.selectedIndex = $scope.subCategoryList.length - 1;
                $scope.productList = filterProducts();
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
            });
            
        }
    };

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


     function filterProducts() {
        var filteredProducts;
        if ($scope.selectedIndex != $scope.subCategoryList.length - 1)
            filteredProducts=$filter('filter')($scope.products.productList, { Category: $scope.subCategoryList[$scope.selectedIndex] });
        else
            filteredProducts = $scope.products.productList;
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

});


jQuery(document).ready(function () {
    jQuery('#add-to-cart-info').hide();
    jQuery("ul.sort-menu div.plz").click(function () {
        jQuery("ul.sort-menu div.plz").css("opacity", "0.6");
        jQuery("ul.sort-menu li.sort-box #sort-text").css("font-weight", "normal");
        jQuery(this).css("opacity", "1");
        jQuery(this).siblings("#sort-text").css("font-weight", "bold");
        jQuery(this).parent().siblings("#sort-text").css("font-weight", "bold");
    });
});
