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

adminApp.controller("AdminPayShipCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.newPaymentName = '';
    $scope.newPaymentDescription = '';
    $scope.newPaymentPrice = '';
    $scope.newShippingName = '';
    $scope.newShippingDescription = '';
    $scope.newShippingPrice = '';
    $scope.init = function () {
        $scope.onlyActiveShipping = false;
        $scope.onlyActivePayment = false;
        $scope.addShippingActive = false;
        $scope.addPaymentActive = false;        
        $http.get("/Admin/DostawyPlatnosci/GetDataJson").success(function (data) {
            $scope.shippingTotal = data.shipping;
            $scope.paymentTotal = data.payment;
            $scope.shippingList = $scope.shippingTotal;
            $scope.paymentList = $scope.paymentTotal;
        });
    };

    $scope.submitPaymentForm = function (name, description, price) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/AddPayment', { description: description, price: price, name: name }).
          success(function (data) {
              if (data !=false) {
                  $scope.paymentTotal.push(data);
                  filterPayment($scope.paymentTotal);
              }
              $scope.addShippingActive = false;
              hideAjaxLoader();
          }).
          error(function (data) {
          });
    }

    $scope.submitShippingForm = function (name,description,price) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/AddShipping', { description: description, price: price, name: name }).
          success(function (data) {
              if (data != false) {
                  $scope.shippingTotal.push(data);
                  filterShipping($scope.shippingTotal);
              }
              $scope.addShippingActive = false;
              hideAjaxLoader();
          }).
          error(function (data) {
          });
    }

    $scope.deletePayment = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/DeletePayment', { id: id }).
          success(function (data) {
              if (data == true) {
                  $scope.paymentTotal = $.grep($scope.paymentTotal, function (e) { return e.PaymentId != id; });
                  filterPayment($scope.paymentTotal);                  
              }
              
              hideAjaxLoader();
          }).
          error(function (data) {
          });
    }
    $scope.deleteShipping = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/DeleteShipping', { id: id }).
          success(function (data) {
              if (data == true) {
                  $scope.shippingTotal = $.grep($scope.shippingTotal, function (e) { return e.ShippingId != id; });
                  filterShipping($scope.shippingTotal);
              }
              hideAjaxLoader();
          }).
          error(function (data) {
          });        
    }

    $scope.addShippingForm = function() {
        $scope.addShippingActive = true;
    }

    $scope.addPaymentForm = function () {
        $scope.addPaymentActive = true;
    }

    $scope.cancelShippingForm = function () {
        $scope.addShippingActive = false;
    }

    $scope.cancelPaymentForm = function () {
        $scope.addPaymentActive = false;
    }

    $scope.activateShipping = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/ActivateShipping', { id: id }).
            success(function (data) {
                if (data == true) {
                    var result = $.grep($scope.shippingTotal, function (e) { return e.ShippingId == id; });
                    if (result.length == 0) {
                    } else if (result.length == 1) {
                        result[0].Active = true;
                    } else {
                        // multiple items found
                    }
                }
                hideAjaxLoader();
                filterShipping($scope.shippingTotal);
            }).
            error(function (data) {

            });        
    }
    $scope.deactivateShipping = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/DeactivateShipping', { id: id }).
            success(function (data) {
                if (data == true) {
                    var result = $.grep($scope.shippingTotal, function (e) { return e.ShippingId == id; });
                    if (result.length == 0) {
                    } else if (result.length == 1) {
                        result[0].Active = false;
                    } else {
                        // multiple items found
                    }
                }
                hideAjaxLoader();
                filterShipping($scope.shippingTotal);
            }).
            error(function (data) {
            });
    }

    $scope.activatePayment = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/ActivatePayment', { id: id }).
            success(function (data) {
                if (data == true) {
                    var result = $.grep($scope.paymentTotal, function (e) { return e.PaymentId == id; });
                    if (result.length == 0) {
                    } else if (result.length == 1) {
                        result[0].Active = true;
                    } else {
                        // multiple items found
                    }
                }
                hideAjaxLoader();
                filterPayment($scope.paymentTotal);
            }).
            error(function (data) {
            });
    }
    $scope.deactivatePayment = function (id) {
        showAjaxLoader();
        $http.post('/Admin/DostawyPlatnosci/DeactivatePayment', { id: id }).
            success(function (data) {
                if (data == true) {
                    var result = $.grep($scope.paymentTotal, function (e) { return e.PaymentId == id; });
                    if (result.length == 0) {
                    } else if (result.length == 1) {
                        result[0].Active = false;
                    } else {
                        // multiple items found
                    }
                }
                hideAjaxLoader();
                filterPayment($scope.paymentTotal);
            }).
            error(function (data) {
            });
    }

    $scope.filterShippingByCheckboxes = function () {
        filterShipping($scope.shippingTotal);
    };
    $scope.filterPaymentByCheckboxes = function () {
        filterPayment($scope.paymentTotal);
    };

    function filterShipping(tab) {
        var shippings = [];
        for (var k = 0; k < tab.length; k++) {
            if ($scope.onlyActiveShipping == false) {
                shippings.push(tab[k]);
            } else {
                if (($scope.onlyActiveShipping && tab[k].Active==true)) {
                    shippings.push(tab[k]);
                }
            }
        }
        $scope.shippingList = shippings;
    }
    function filterPayment(tab) {
        var payments = [];
        for (var k = 0; k < tab.length; k++) {
            if ($scope.onlyActivePayment == false) {
                payments.push(tab[k]);
            } else {
                if (($scope.onlyActivePayment && tab[k].Active == true)) {
                    payments.push(tab[k]);
                }
            }
        }
        $scope.paymentList = payments;
    }


}]);