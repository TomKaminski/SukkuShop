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
            type: 'GET'
        }).success(function(data) {
            var value = target.text();
            if (value > 1)
                value--;
            target.empty().append(value);
            var totalpriceTarget = $('#quantity-total' + id);
            totalpriceTarget.empty().append(currency(data) + " PLN");
            totalPriceJson();
            priceSummary();
            dostawaSummary();

        });
    });

    $("#remove").on("click", "#plzarrowsup", function () {
        var id = $(this).parent().siblings("#quantity_counter").children().attr("id").replace("quantity", '');
        var target = $(this).parent().siblings("#quantity_counter").children();
        $.ajax({
            url: '/Koszyk/IncreaseQuantity/' + id,
            contentType: 'application/html; charset=utf-8',
            type: 'GET'
        }).success(function(data) {
            var value = target.text();
            value++;
            target.empty().append(value);
            totalPriceJson();
            priceSummary();
            dostawaSummary();
            var totalpriceTarget = $('#quantity-total' + id);
            totalpriceTarget.empty().append(currency(data) + " PLN");
        });

    });


    //price summary na dole, modyfikacja
    function priceSummary() {
        var value1 = $('#price-box-json').text().replace(' PLN', '').replace(',', '.');
        var value2 = $('#dostawa-box-summary').text().replace(' PLN', '');
        var value3 = $('#payment-box-summary').text().replace(' PLN', '');
        var sum = Number(currency(value1)) + Number(currency(value2)) + Number(currency(value3));
        $("#total-price-summary").empty().append(currency(sum) + " PLN");
    }

    //summary po dostawie, modyfikacja
    function dostawaSummary() {
        var value2 = $('#dostawa-box-summary').text().replace(' PLN', '');
        var value1 = $('#price-box-json').text().replace(' PLN', '');
        var sum = Number(currency(value1)) + Number(currency(value2));
        $("#shipping-price-summary").empty().append(currency(sum) + " PLN");
    }


    $("input:radio[name=shipping]").click(function() {
        var value = $(this).siblings(".shipping-price").text().replace(' PLN', '').replace(',', '.');
        var productsPrice = $('#products-price-sum').text().replace(' zł', '').replace(',', '.');
        var sum = Number(currency(value)) + Number(currency(productsPrice));
        $('#dostawa-box-summary').empty().append(value + ' PLN');
        var target = $('#shipping-price-summary');
        target.empty();
        target.append(currency(sum) + " PLN");
        priceSummary();
    });

    $("input:radio[name=payment]").click(function() {
        var value = $(this).siblings(".shipping-price").text().replace(' PLN', '').replace(',', '.');
        var sum = currency(value);
        $('#payment-box-summary').empty().append(sum + ' PLN');
        priceSummary();
    });


    //total price po byle jakich kliknięciach
    function totalPriceJson() {
        $.ajax({
                url: '/Koszyk/TotalPriceJson',
                contentType: 'application/html; charset=utf-8',
                type: 'GET'
            })
            .success(function(data) {
                var target3 = $('#cart-price-header');
                target3.empty();
                target3.append("koszyk " + currency(data) + " PLN");
                var target2 = $('#products-price-sum');
                target2.empty();
                target2.append(currency(data) + " PLN");
                var target = $('#price-box-json');
                target.empty();
                target.append(currency(data) + " PLN");
                priceSummary();
                dostawaSummary();
            });
    }


    $(document).on("click", ".cart-delete-text", function() {
        var url = $('#removeform').attr("action");
        var formData = $('#removeform').serialize();
        $.ajax({
            url: url,
            type: "GET",
            data: formData
        }).success(function(data) {
            var target = $('#remove');
            target.empty();
            target.append(data);
            totalPriceJson();
        });
    });
});
