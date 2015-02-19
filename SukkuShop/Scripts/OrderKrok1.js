$("#nextStep").click(function () {
    var shiping = false;
    var payment = false;
    $("ul#shipping-list li input:radio").each(function () {
        if ($(this).is(':checked') && !$(this).is(":disabled"))
            shiping = true;
    });
    $("ul#payment-list li input:radio").each(function () {
        if ($(this).is(':checked')&& !$(this).is(":disabled"))
            payment = true;
    });
    if (payment && shiping)
        return true;
    else {
        if (!shiping)
            $("#validShipment").css("visibility", "visible");
        else {
            $("#validShipment").css("visibility", "hidden");
        }
        if (!payment)
            $("#validPayment").css("visibility", "visible");
        else {
            $("#validPayment").css("visibility", "hidden");
        }
        return false;
    }
});

//price summary na dole, modyfikacja
function priceSummary() {
    var value1 = $('#price-box-json').text().replace('/&nbsp;/g', '').replace(' zł', '').replace(',', '.');
    var value2 = $('#dostawa-box-summary').text().replace(' zł', '');
    var value3 = $('#payment-box-summary').text().replace(' zł', '');
    var value4 = $('#rabat-box').text().replace(' zł', '').replace('-','');
    var sum = Number(currency(value1)) + Number(currency(value2)) + Number(currency(value3)-Number(currency(value4)));
    $("#total-price-summary").empty().append(currency(sum) + " zł");
}

$("ul#shipping-list li input:radio").click(function () {
    var id = parseInt($(this).attr("id").replace('ship', ''));
    $.ajax({
        url: '/Zamowienie/SetShipping/' + id,
        contentType: 'application/html; charset=utf-8',
        type: 'GET'
    });
    var value = $(this).siblings(".shipping-price").text().replace(' zł', '').replace(',', '.');
    var productsPrice = $('#products-price-sum').text().replace(' zł', '').replace(',', '.');
    var sum = Number(currency(value)) + Number(currency(productsPrice));
    $('#dostawa-box-summary').empty().append(value + ' zł');
    var target = $('#shipping-price-summary');
    target.empty().append(currency(sum) + " zł");
    if (id == 5) {
        
        $("input:radio[id=pay4]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay2]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay3]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay4]").attr('disabled', false);
        $("input:radio[id=pay3]").attr('disabled', true);
        $("input:radio[id=pay2]").attr('disabled', true);
    } else {
        
        $("input:radio[id=pay4]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay2]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay3]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay4]").attr('disabled', true);
        $("input:radio[id=pay3]").attr('disabled', false);
        $("input:radio[id=pay2]").attr('disabled', false);

    }
    priceSummary();
});

$(document).ready(function() {
    var id = -1;
    $("ul#shipping-list li input:radio").each(function () {
        if ($(this).is(":checked")) {
            id = parseInt($(this).attr("id").replace('ship', ''));
            var value = $(this).siblings(".shipping-price").text().replace(' zł', '').replace(',', '.');
            var productsPrice = $('#products-price-sum').text().replace(' zł', '').replace(',', '.');
            var sum = Number(currency(value)) + Number(currency(productsPrice));
            $('#dostawa-box-summary').empty().append(value + ' zł');
            var target = $('#shipping-price-summary');
            target.empty().append(currency(sum) + " zł");
        }
            
    });
    if (id == 5) {

        $("input:radio[id=pay4]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay2]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay3]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay4]").attr('disabled', false);
        $("input:radio[id=pay3]").attr('disabled', true);
        $("input:radio[id=pay2]").attr('disabled', true);
    } else if (id >0 && id!=5) {

        $("input:radio[id=pay4]").siblings('label').css('background-image', 'url(/Content/Images/checkbox-nonactive.png)');
        $("input:radio[id=pay2]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay3]").siblings('label').css('background-image', 'url(/Content/Images/checkbox.png)');
        $("input:radio[id=pay4]").attr('disabled', true);
        $("input:radio[id=pay3]").attr('disabled', false);
        $("input:radio[id=pay2]").attr('disabled', false);

    }
    $("ul#payment-list li input:radio").each(function () {
        if ($(this).is(":checked")) {
            var valuee = $(this).siblings(".shipping-price").text().replace(' zł', '').replace(',', '.');
            var summ = currency(valuee);
            $('#payment-box-summary').empty().append(summ + ' zł');
            priceSummary();
        }
    });    
});

$("ul#payment-list li input:radio").click(function () {
    var id = parseInt($(this).attr("id").replace('pay', ''));
    $.ajax({
        url: '/Zamowienie/SetPayment/' + id,
        contentType: 'application/html; charset=utf-8',
        type: 'GET'
    });
    var value = $(this).siblings(".shipping-price").text().replace(' zł', '').replace(',', '.');
    var sum = currency(value);
    $('#payment-box-summary').empty().append(sum + ' zł');
    priceSummary();
});

function currency(n) {
    n = parseFloat(n);
    return isNaN(n) ? false : n.toFixed(2);
}