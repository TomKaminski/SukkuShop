var adminApp = angular.module("adminApp", []);
var itemsPerPage = 10;

adminApp.controller("AdminProdCtrl", function ($scope, $http, $filter) {
    $scope.init = function () {
        $scope.selectedCategory = 0;
        $scope.currentPage = 1;
        $scope.currentFilter = 0;
        $scope.textFilter = "";
        $scope.published = false;
        $scope.isready = false;
        $scope.wrongmodel = false;

        $http.get("/Admin/AdminProduct/GetProductList").success(function (data) {
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
         $scope.pages = Math.ceil(products.length / 10);
        var pages = [];
        for (var j = 1; j <= $scope.pages; j++) {
            pages.push(j);
        }
        $scope.tableOfPages = pages;
        return products.slice($scope.currentPage * itemsPerPage - itemsPerPage, $scope.currentPage * itemsPerPage);
     }

});