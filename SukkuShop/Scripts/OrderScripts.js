﻿$(document).ready(function () {

    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "Nieprawidłowa wartość");

    $.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != param;
    }, "Proszę podać inną wartość");

    $(".cart-title-ending").click(function () {
        if ($("#newaddress").length)
            $("#NewAddressForm").submit();
        else {
            $("#NewClientForm").submit();
        }
    });

    $("#newaddress").click(function() {
        if ($(this).is(":checked")) {
            $(".left-container").children().children("input[type=text]").each(function () {
                $(this).css("background-color", "white");
                $(this).attr("readonly", false);
            });
            
        } else {
            $(".left-container").children().children("input[type=text]").each(function () {
                $(this).css("background-color", "#ebebeb");
                $(this).attr("readonly", true);
            });
        }
    });

    priceSummary();
    $("#OrderLoginForm").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            password: {
                required: true
            }
        },
        messages:{
            email: {
                required: "Pole Email jest wymagane.",
                email: "Podaj poprawny adres email."
            },
            password: {
                required: "Wprowadź hasło."
            }
        }
    });

    $("#NewClientForm").validate({
        rules: {
            regulamin: "required",
            daneosobowe:"required",
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6
            },
            confirmPassword: {
                equalTo: "#password"
            },
            imie: {
                required: true,
                regex: /^[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            nazwisko: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            ulica: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            numer: {
                required: true,
                notEqual: "Nie podano"
            },
            miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$/,
                notEqual: "Nie podano"
            },
            kodpocztowy: {
                required: true,
                regex: /^[0-9]{2}-[0-9]{3}$/,
                notEqual: "Nie podano"
            }

        },
        messages: {
            email: {
                required: "Pole Email jest wymagane.",
                email: "Podaj poprawny adres email."
            },
            password: {
                required: "Wprowadź hasło.",
                minlength: "Hasło musi zawierać przynajmniej 6 znaków."
            },
            confirmPassword: {
                equalTo: "Hasła muszą być takie same"
            },
            imie: {
                required: "Pole Imie jest wymagane.",
                minlength: "Imie musi się składać z przynajmniej 2 znaków.",
                regex: "Imie jest niepoprawne"
            },
            nazwisko: {
                required: "Pole Nazwisko jest wymagane.",
                minlength: "Nazwisko musi się składać z przynajmniej 2 znaków.",
                regex: "Nazwisko jest niepoprawne"
            },
            ulica: {
                required: "Pole Ulica jest wymagane.",
                minlength: "Ulica musi się składać z przynajmniej 2 znaków.",
                regex: "Telefon jest niepoprawny"
            },
            numer: {
                required: "Pole Numer jest wymagane."
            },
            miasto: {
                required: "Pole Miasto jest wymagane.",
                minlength: "Miasto musi się składać z przynajmniej 2 znaków.",
                regex: "Miasto jest niepoprawne"
            },
            telefon: {
                required: "Pole Telefon jest wymagane.",
                regex: "Telefon jest niepoprawny"
            },
            kodpocztowy: {
                required: "Pole Kod Pocztowy jest wymagane.",
                regex: "Kod pocztowy jest niepoprawny"
            },
            regulamin: "Wymagana jest akceptacja regulaminu",
            daneosobowe: "Wymagana jest akceptacja informacji o przetwarzaniu danych osobowych"
        }
    });

    $("#NewAddressForm").validate({
        rules: {
            Imie: {
                required: true,
                regex: /^[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Nazwisko: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Ulica: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Numer: {
                required: true,
                notEqual: "Nie podano"
            },
            Miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$/,
                notEqual: "Nie podano"
            },
            KodPocztowy: {
                required: true,
                regex: /^[0-9]{2}-[0-9]{3}$/,
                notEqual: "Nie podano"
            }

        },
        messages: {            
            Imie: {
                required: "Pole Imie jest wymagane.",
                minlength: "Imie musi się składać z przynajmniej 2 znaków.",
                regex: "Imie jest niepoprawne"
            },
            Nazwisko: {
                required: "Pole Nazwisko jest wymagane.",
                minlength: "Nazwisko musi się składać z przynajmniej 2 znaków.",
                regex: "Nazwisko jest niepoprawne"
            },
            Ulica: {
                required: "Pole Ulica jest wymagane.",
                minlength: "Ulica musi się składać z przynajmniej 2 znaków.",
                regex: "Ulica jest niepoprawna"
            },
            Numer: {
                required: "Pole Numer jest wymagane."
            },
            Miasto: {
                required: "Pole Miasto jest wymagane.",
                minlength: "Miasto musi się składać z przynajmniej 2 znaków.",
                regex: "Miasto jest niepoprawne"
            },
            Telefon: {
                required: "Pole Telefon jest wymagane.",
                regex: "Telefon jest niepoprawny"
            },
            KodPocztowy: {
                required: "Pole Kod Pocztowy jest wymagane.",
                regex: "Kod pocztowy jest niepoprawny"
            }
        }
    });
});

$("#nextStep").click(function () {
    var shiping = false;
    var payment = false;
    $("input[name=shipping]").each(function() {
        if ($(this).is(':checked'))
            shiping = true;
    });
    $("input[name=payment]").each(function () {
        if ($(this).is(':checked'))
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
    var value1 = $('#price-box-json').text().replace(' zł', '').replace(',', '.');
    var value2 = $('#dostawa-box-summary').text().replace(' zł', '');
    var value3 = $('#payment-box-summary').text().replace(' zł', '');
    var sum = Number(currency(value1)) + Number(currency(value2)) + Number(currency(value3));
    $("#total-price-summary").empty().append(currency(sum) + " zł");
}

$("input:radio[name=shipping]").click(function () {
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
    priceSummary();
});

$("input:radio[name=payment]").click(function () {
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