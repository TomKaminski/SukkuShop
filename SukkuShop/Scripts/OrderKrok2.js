function clientDataAjaxSuccess() {
    hideAjaxLoader();
    if ($("input[id=NewAccount]").is(":checked")) {
        $("#newaddress-replace input[type=password]").attr("disabled", false);
    } else {
        $("#newaddress-replace input[type=password]").attr("disabled", true);

    }
}

function onError(xhr, status, error) {
    console.log(xhr.responseText);
}

function clientDataAjaxSuccessChangeAddress() {
    hideAjaxLoader();
    if ($("input[id=newaddress]").is(":checked")) {
        $("#radio-boxes").children("label").css("background-image", "url(/Content/Images/checkbox.png)");
        $("#radio-boxes").children("input").attr("disabled", false);
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "white");
            $(this).attr("readonly", false);
        });
    } else {
        $("#radio-boxes").children("label").css("background-image", "url(/Content/Images/checkbox-nonactive.png)");
        $("#radio-boxes").children("input").attr("disabled", true);
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "#ebebeb");
            $(this).attr("readonly", true);
        });

    }
}

function newAddressCheckbox() {
    if ($("input[id=newaddress]").is(":checked")) {
        $("#radio-boxes").children("label").css("background-image", "url(/Content/Images/checkbox.png)");
        $("#radio-boxes").children("input").attr("disabled", false);
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "white");
            $(this).attr("readonly", false);
        });

    } else {
        $("#radio-boxes").children("label").css("background-image", "url(/Content/Images/checkbox-nonactive.png)");
        $("#radio-boxes").children("input").attr("disabled", true);
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            
            $(this).css("background-color", "#ebebeb");
            $(this).attr("readonly", true);
        });
    }
}

function changeAddressTextboxes() {
    if ($("#newaddress").is(":checked")) {
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "white");
            $(this).attr("readonly", false);
        });

    } else {
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "#ebebeb");
            $(this).attr("readonly", true);
        });
    }
}

function showAjaxLoader() {
    var loaderDiv = $("#ajax-processing");
    if (loaderDiv.length === 0) {
        $("body").append("<div id='ajax-processing'></div>");
        loaderDiv = $("#ajax-processing");
    }
    loaderDiv.show();
}

function hideAjaxLoader() {
    $("#ajax-processing").hide();
}


$(document).ready(function () {
    $("#newaddress-replace").delegate("input[id=firmafalse]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Zamowienie/NewAddressOrderPartial',
            contentType: 'application/html; charset=utf-8',
            type: 'GET'
        }).success(function (data) {
            setTimeout(hideAjaxLoader, 1000);
            $("#newaddress-replace").empty().append(data);
        });
    });

    newAddressCheckbox();
    changeAddressTextboxes();

    $("#changeaddress-replace").delegate("input[id=firmafalse]", "click", function () {
        if (!$(this).is('[disabled="disabled"]')) {

            showAjaxLoader();
            $.ajax({
                url: '/Zamowienie/ChangeAddressPartial',
                contentType: 'application/html; charset=utf-8',
                type: 'GET'
            }).success(function(data) {
                setTimeout(hideAjaxLoader, 1000);

                $("#changeaddress-replace").empty().append(data);
                $("input[id=newaddress]").attr("checked", true);
                newAddressCheckbox();
            });
        }
    });

    $("#changeaddress-replace").delegate("input[id=firmatrue]", "click", function () {
        if (!$(this).is('[disabled="disabled"]')) {
            showAjaxLoader();
            $.ajax({
                url: '/Zamowienie/ChangeAddressFirmaPartial',
                contentType: 'application/html; charset=utf-8',
                type: 'GET'
            }).success(function(data) {
                setTimeout(hideAjaxLoader, 1000);

                $("#changeaddress-replace").empty().append(data);
                $("input[id=newaddress]").attr("checked", true);
                newAddressCheckbox();
            });
        }
    });

    $("#ajax-processing").hide();

    $(document).bind('mousemove', function (e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $("#newaddress-replace").delegate("input[id=firmatrue]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Zamowienie/NewAddressOrderFirmaPartial',
            contentType: 'application/html; charset=utf-8',
            type: 'GET'
        }).success(function (data) {
            setTimeout(hideAjaxLoader, 1000);
            $("#newaddress-replace").empty().append(data);
        });
    });

    $("#newaddress-replace").delegate("input[id=NewAccount]", "click", function () {
        if ($("input[id=NewAccount]").is(":checked"))
            $("#newaddress-replace input[type=password]").attr("disabled", false);
        else {
            $("#newaddress-replace input[type=password]").attr("disabled", true);
            $("input[id=Password]").siblings(".field-validation-error").empty();
        }

    });

    
    

    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "Nieprawidłowa wartość");

    $.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != param;
    }, "Proszę podać inną wartość");

    $("#finalizuj").click(function () {
        if ($("#newaddress").length)
            if ($("input[id=firmafalse]").is(":checked"))
                $("#ChangeAddressForm").submit();
            else {
                $("#ChangeAddressFirmaForm").submit();
            }
        else {
            if ($("input[id=firmafalse]").is(":checked"))
                $("#NewClientForm").submit();
            else {
                $("#NewClientFormFirma").submit();
            }
        }
    });

    $("#changeaddress-replace").delegate("input[id=newaddress]", "click", function () {
        newAddressCheckbox();
    });



    //$("#OrderLoginForm").validate({
    //    rules: {
    //        email: {
    //            required: true,
    //            email: true
    //        },
    //        password: {
    //            required: true
    //        }
    //    },
    //    messages:{
    //        email: {
    //            required: "Pole Email jest wymagane.",
    //            email: "Podaj poprawny adres email."
    //        },
    //        password: {
    //            required: "Wprowadź hasło."
    //        }
    //    }
    //});

    //$("#NewClientForm").validate({
    //    rules: {
    //        regulamin: "required",
    //        daneosobowe:"required",
    //        email: {
    //            required: true,
    //            email: true
    //        },
    //        password: {
    //            required: true,
    //            minlength: 6
    //        },
    //        confirmPassword: {
    //            equalTo: "#password"
    //        },
    //        Imie: {
    //            required: true,
    //            regex: /^[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Nazwisko: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Ulica: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Numer: {
    //            required: true,
    //            notEqual: "Nie podano"
    //        },
    //        Miasto: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Telefon: {
    //            required: true,
    //            regex: /^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$/,
    //            notEqual: "Nie podano"
    //        },
    //        KodPocztowy: {
    //            required: true,
    //            regex: /^[0-9]{2}-[0-9]{3}$/,
    //            notEqual: "Nie podano"
    //        }

    //    },
    //    messages: {
    //        email: {
    //            required: "Pole Email jest wymagane.",
    //            email: "Podaj poprawny adres email."
    //        },
    //        password: {
    //            required: "Wprowadź hasło.",
    //            minlength: "Hasło musi zawierać przynajmniej 6 znaków."
    //        },
    //        confirmPassword: {
    //            equalTo: "Hasła muszą być takie same"
    //        },
    //        Imie: {
    //            required: "Pole Imie jest wymagane.",
    //            minlength: "Minimum 2 znaki.",
    //            regex: "Imie jest niepoprawne"
    //        },
    //        Nazwisko: {
    //            required: "Pole Nazwisko jest wymagane.",
    //            minlength: "Minimum 2 znaki.",
    //            regex: "Nazwisko jest niepoprawne"
    //        },
    //        Ulica: {
    //            required: "Pole Ulica jest wymagane.",
    //            minlength: "Minimum 2 znaki.",
    //            regex: "Telefon jest niepoprawny"
    //        },
    //        Numer: {
    //            required: "Pole Numer jest wymagane."
    //        },
    //        Miasto: {
    //            required: "Pole Miasto jest wymagane.",
    //            minlength: "Minimum 2 znaki.",
    //            regex: "Miasto jest niepoprawne"
    //        },
    //        Telefon: {
    //            required: "Pole Telefon jest wymagane.",
    //            regex: "Telefon jest niepoprawny"
    //        },
    //        KodPocztowy: {
    //            required: "Pole Kod Pocztowy jest wymagane.",
    //            regex: "Kod pocztowy jest niepoprawny"
    //        },
    //        regulamin: "Wymagana jest akceptacja regulaminu",
    //        daneosobowe: "Wymagana jest akceptacja informacji o przetwarzaniu danych osobowych"
    //    }
    //});

    //$("#NewAddressForm").validate({
    //    rules: {
    //        Imie: {
    //            required: true,
    //            regex: /^[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Nazwisko: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Ulica: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Numer: {
    //            required: true,
    //            notEqual: "Nie podano"
    //        },
    //        Miasto: {
    //            required: true,
    //            minlength: 2,
    //            regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
    //            notEqual: "Nie podano"
    //        },
    //        Telefon: {
    //            required: true,
    //            regex: /^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$/,
    //            notEqual: "Nie podano"
    //        },
    //        KodPocztowy: {
    //            required: true,
    //            regex: /^[0-9]{2}-[0-9]{3}$/,
    //            notEqual: "Nie podano"
    //        }

    //    },
    //    messages: {            
    //        Imie: {
    //            required: "Pole Imie jest wymagane.",
    //            minlength: "Imie musi się składać z przynajmniej 2 znaków.",
    //            regex: "Imie jest niepoprawne"
    //        },
    //        Nazwisko: {
    //            required: "Pole Nazwisko jest wymagane.",
    //            minlength: "Nazwisko musi się składać z przynajmniej 2 znaków.",
    //            regex: "Nazwisko jest niepoprawne"
    //        },
    //        Ulica: {
    //            required: "Pole Ulica jest wymagane.",
    //            minlength: "Ulica musi się składać z przynajmniej 2 znaków.",
    //            regex: "Ulica jest niepoprawna"
    //        },
    //        Numer: {
    //            required: "Pole Numer jest wymagane."
    //        },
    //        Miasto: {
    //            required: "Pole Miasto jest wymagane.",
    //            minlength: "Miasto musi się składać z przynajmniej 2 znaków.",
    //            regex: "Miasto jest niepoprawne"
    //        },
    //        Telefon: {
    //            required: "Pole Telefon jest wymagane.",
    //            regex: "Telefon jest niepoprawny"
    //        },
    //        KodPocztowy: {
    //            required: "Pole Kod Pocztowy jest wymagane.",
    //            regex: "Kod pocztowy jest niepoprawny"
    //        }
    //    }
    //});
});

