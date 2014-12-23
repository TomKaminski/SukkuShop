var shopApp = angular.module("shopApp", []);
var currentPage = 1;
var itemsPerPage = 12;
var products;

shopApp.controller("ShopCtrl", function ($scope,$http) {
    $scope.init = function (category) {
        $scope.category = category;
        if ($scope.category == "") {
            $http.get("/Sklep/GetProductByCategory").success(function(data) {
                $scope.products = data;
                $scope.productList = $scope.products.productList.slice(0, 12);
            });
            $scope.imgName = "inne";
        } else {
            $http.get("/Sklep/GetProductByCategory/" + $scope.category).success(function(data) {
                $scope.products = data;
                $scope.productList = $scope.products.productList.slice(0, 12);

            });
            $scope.imgName = category.toLowerCase();
        }

    };
});
