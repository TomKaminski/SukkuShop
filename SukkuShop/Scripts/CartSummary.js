$(document).ready(function() {
    function currency(n) {
        n = parseFloat(n);
        return isNaN(n) ? false : n.toFixed(2);
    }


    $("#remove").on("click", "#plzarrowsdown", function () {
        var id = $(this).parent().siblings("#quantity_counter").children().attr("id").replace("quantity", '');
        var target = $(this).parent().siblings("#quantity_counter").children();
        $.ajax({
            url: '/Koszyk/DecreaseQuantity/' + id,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            cache: false
        }).success(function(data) {
            var value = target.text();
            if (value > 1)
                value--;
            target.empty().append(value);
            var totalpriceTarget = $('#quantity-total' + id);
            totalpriceTarget.empty().append(currency(data) + " zł");
            totalPriceJson();
            //dostawaSummary();

        });
    });

    $("#remove").on("click", "#plzarrowsup", function () {
        var id = $(this).parent().siblings("#quantity_counter").children().attr("id").replace("quantity", '');
        var target = $(this).parent().siblings("#quantity_counter").children();
        $.ajax({
            url: '/Koszyk/IncreaseQuantity/' + id,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            cache: false
        }).success(function(data) {
            var value = target.text();
            value++;
            target.empty().append(value);
            totalPriceJson();
            //dostawaSummary();
            var totalpriceTarget = $('#quantity-total' + id);
            totalpriceTarget.empty().append(currency(data) + " zł");
        });

    });
    //total price po byle jakich kliknięciach
    function totalPriceJson() {
        $.ajax({
                url: '/Koszyk/TotalPriceJson',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                cache: false
            }).success(function (data) {
                var curr = currency(data);
                var target3 = $('#cart-price-header');
                target3.empty().append("koszyk " + curr + " zł");
                var target2 = $('#products-price-sum');
                target2.empty().append(curr + " zł");
                var target = $('#price-box-json');
                target.empty().append(curr + " zł");
                //priceSummary();
                //dostawaSummary();
            });
    }


    $("#remove").on("click", ".cart-delete-text", function() {
        var url = $(this).parent().attr("action");
        var formData = $(this).parent().serialize();
        $.ajax({
            url: url,
            type: "GET",
            data: formData,
            cache: false
        }).success(function(data) {
            var target = $('#remove');
            target.empty().append(data);
            totalPriceJson();
        });
    });
});
