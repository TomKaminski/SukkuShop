var adminApp = angular.module("adminApp", []);
var itemsPerPage = 10;

adminApp.controller("AdminUserCtrl", function ($scope, $http, $filter) {
    var orderBy = $filter('orderBy');
    $scope.init = function () {
        $scope.selectedIndex = 1;
        $scope.currentPage = 1;
        $scope.textFilter = "";
        $http.get("/Admin/AdminUser/GetUserList").success(function (data) {
            $scope.usersTotal = data;
            $scope.usersOperative = $scope.usersTotal;
            $scope.usersList = filterUsers($scope.usersTotal);
        });
    };

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

});