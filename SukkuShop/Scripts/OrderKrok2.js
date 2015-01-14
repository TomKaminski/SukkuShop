/// <reference path="OrderKrok1.js" />
//Nie przeszła walidacja na serverze podczas tworzenia nowego konta
function clientDataAjaxSuccess() {
    hideAjaxLoader();
    if ($("input[id=NewAccount]").is(":checked")) {
        $("#newaddress-replace input[type=password]").attr("disabled", false);
    } else {
        $("#newaddress-replace input[type=password]").attr("disabled", true);
    }
}

//Nie przeszła walidacja na serverze podczas podawania nowego adresu dla zalogowanego usera
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

//function allFormDelegate() {
//    newClientForm();
//    newClientFormFirma();
//    newAddressForm();
//    newAddressFormFirma();
//}

function onError(xhr, status, error) {
    alert(error);
    console.log(xhr.responseText);
}



function newAddressCheckbox() {
    if ($("input[id=newaddress]").is(":checked")) {
        $("#radio-boxes").children("label").css("background-image", "url(/Content/Images/checkbox.png)");
        $("#radio-boxes").children("input").attr("disabled", false);
        $("#left-container-new-address").children().children("input[type=text]").each(function () {
            $(this).css("background-color", "white");
            $(this).attr("readonly", false);
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
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

    newAddressCheckbox();
    newAddressForm();
    newClientForm();
    newClientFormFirma();
    newAddressFormFirma();

    $("#ajax-processing").hide();

    $(document).bind('mousemove', function (e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $("#zaloguj").click(function() {
        $("#LoginOrderForm").submit();
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

    $(".client-data-container").delegate(".data-box input", "focus", function () {
        if ($(this).val() == "Nie podano" && !($(this).is("[disabled='disabled']")) && !($(this).is("[readonly='readonly']"))) {
            $(this).val("");
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
        if ($(this).siblings(".field-validation-error").html('') != "" && !($(this).is("[disabled='disabled']")) && !($(this).is("[readonly='readonly']"))) {
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
    });



    $("#newaddress-replace").delegate("input[id=firmafalse]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Zamowienie/NewAddressOrderPartial',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            complete: function () { newClientForm(); },
            error: function(xhr, status, error) {
                onError(xhr, status, error);
            }
        }).success(function (data) {
            setTimeout(hideAjaxLoader, 1000);
            $("#newaddress-replace").empty().append(data);
        });
    });


    $("#changeaddress-replace").delegate("input[id=firmafalse]", "click", function () {
        if (!$(this).is('[disabled="disabled"]')) {
            showAjaxLoader();
            $.ajax({
                url: '/Zamowienie/ChangeAddressPartial',
                contentType: 'application/html; charset=utf-8',
                type: 'GET',
                complete: function() {
                    newAddressForm();
                },
                error: function (xhr, status, error) {
                    onError(xhr, status, error);
                }
            }).success(function (data) {
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
                type: 'GET',
                complete: function() {
                    newAddressFormFirma();
                },
                error: function (xhr, status, error) {
                    onError(xhr, status, error);
                }
            }).success(function (data) {
                setTimeout(hideAjaxLoader, 1000);

                $("#changeaddress-replace").empty().append(data);
                $("input[id=newaddress]").attr("checked", true);
                newAddressCheckbox();
            });
        }
    });


    $("#newaddress-replace").delegate("input[id=firmatrue]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Zamowienie/NewAddressOrderFirmaPartial',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            complete: function() {
                newClientFormFirma();
            },
            error: function (xhr, status, error) {
                onError(xhr, status, error);
            }
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


    $("#LoginOrderForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            PasswordLogin: {
                required: true, minlength:6
            }
        },
        messages:{
            Email: {
                required: "Email jest wymagany.",
                email: "Podaj poprawny adres email."
            },
            PasswordLogin: {
                required: "Wprowadź hasło.",
                minlength: "Hasło musi zawierać conajmniej 6 znaków."
            }
        }
    });

    
    
    
    
});

function newAddressFormFirma() {
    $("#ChangeAddressFirmaForm").validate({
        rules: {
            NazwaFirmy: {
                required: true,
                minlength: 2,
                notEqual: "Nie podano"
            },
            Nip: {
                required: true,
                minlength: 10,
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
                notEqual: "Nie podano",
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$/
            },
            Miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}$/,
                notEqual: "Nie podano"
            },
            KodPocztowy: {
                required: true,
                regex: /^[0-9]{2}-[0-9]{3}$/,
                notEqual: "Nie podano"
            }

        },
        messages: {
            NazwaFirmy: {
                required: "Nazwa firmy jest wymagana.",
                minlength: "Nazwa firmy musi zawierać conajmniej 2 znaki.",
            },
            Nip: {
                required: "Numer NIP jest wymagany.",
                minlength: "Nip to 10 cyfr.",
            },
            Ulica: {
                required: "Ulica jest wymagana.",
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Ulica jest niepoprawna."
            },
            Numer: {
                required: "Numer domu jest wymagany.",
                regex: "Numer domu jest niepoprawny."
            },
            Miasto: {
                required: "Miasto jest wymagane.",
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne."
            },
            Telefon: {
                required: "Numer telefonu jest wymagany.",
                regex: "Numer telefonu jest niepoprawny."
            },
            KodPocztowy: {
                required: "Kod pocztowy jest wymagany.",
                regex: "Kod pocztowy jest niepoprawny."
            }
        }
    });
}

function newAddressForm() {
    $("#ChangeAddressForm").validate({
        rules: {
            Imie: {
                required: true,
                minlength: 2,
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
                notEqual: "Nie podano",
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$/
            },
            Miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}$/,
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
                required: "Imie jest wymagane.",
                minlength: "Imie musi zawierać conajmniej 2 znaki.",
                regex: "Imie jest niepoprawne",
            },
            Nazwisko: {
                required: "Nazwisko jest wymagane.",
                minlength: "Nazwisko musi zawierać conajmniej 2 znaki.",
                regex: "Nazwisko jest niepoprawne"
            },
            Ulica: {
                required: "Ulica jest wymagana.",
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Ulica jest niepoprawna"
            },
            Numer: {
                required: "Numer domu jest wymagany.",
                regex: "Numer domu jest niepoprawny."
            },
            Miasto: {
                required: "Miasto jest wymagane.",
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne"
            },
            Telefon: {
                required: "Numer telefonu jest wymagany.",
                regex: "Numer telefonu jest niepoprawny."
            },
            KodPocztowy: {
                required: "Kod pocztowy jest wymagany.",
                regex: "Kod pocztowy jest niepoprawny"
            }
        }
    });
}

function newClientFormFirma() {
    $("#NewClientFormFirma").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            },
            ConfirmPassword: {
                equalTo: "#Password"
            },
            NazwaFirmy: {
                required: true,
                regex: /^[A-Ża-ż]*$/,
                minlength: 2,
                notEqual: "Nie podano"
            },
            Nip: {
                required: true,
                minlength: 10,
                regex: /^[0-9]{10}$/,
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
                notEqual: "Nie podano",
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$/
            },
            Miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}$/,
                notEqual: "Nie podano"
            },
            KodPocztowy: {
                required: true,
                regex: /^[0-9]{2}-[0-9]{3}$/,
                notEqual: "Nie podano"
            }

        },
        messages: {
            Email: {
                required: "Email jest wymagany.",
                email: "Podaj poprawny adres email."
            },
            Password: {
                required: "Wprowadź hasło.",
                minlength: "Hasło musi zawierać conajmniej 6 znaków."
            },
            ConfirmPassword: {
                equalTo: "Hasła muszą być takie same."
            },
            NazwaFirmy: {
                required: "Nazwa firmy jest wymagana.",
                minlength: "Nazwa firmy musi zawierać conajmniej 2 znaki.",
                regex: "Nazwa firmy jest niepoprawna."
            },
            Nip: {
                required: "Numer NIP jest wymagant.",
                minlength: "NIP to 10 cyfrowa liczba.",
                regex: "NIP to 10 cyfrowa liczba."
            },
            Ulica: {
                required: "Ulica jest wymagana.",
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Ulica jest niepoprawna"
            },
            Numer: {
                required: "Numer domu jest wymagany.",
                regex: "Numer domu jest niepoprawny."
            },
            Miasto: {
                required: "Miasto jest wymagane.",
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne"
            },
            Telefon: {
                required: "Numer telefonu jest wymagany.",
                regex: "Numer telefonu jest niepoprawny."
            },
            KodPocztowy: {
                required: "Kod Pocztowy jest wymagany.",
                regex: "Kod pocztowy jest niepoprawny."
            }
        }
    });
}

function newClientForm() {
    $("#NewClientForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 6
            },
            ConfirmPassword: {
                equalTo: "#Password"
            },
            Imie: {
                required: true,
                minlength: 2,
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
                notEqual: "Nie podano",
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$/
            },
            Miasto: {
                required: true,
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/,
                notEqual: "Nie podano"
            },
            Telefon: {
                required: true,
                regex: /^[1-9][0-9]{8}$/,
                notEqual: "Nie podano"
            },
            KodPocztowy: {
                required: true,
                regex: /^[0-9]{2}-[0-9]{3}$/,
                notEqual: "Nie podano"
            }
        },
        messages: {
            Email: {
                required: "Email jest wymagany.",
                email: "Adres email jest niepoprawny."
            },
            Password: {
                required: "Wprowadź hasło.",
                minlength: "Hasło musi zawierać conajmniej 6 znaków."
            },
            ConfirmPassword: {
                equalTo: "Hasła muszą być takie same."
            },
            Imie: {
                required: "Imie jest wymagane.",
                minlength: "Imie musi zawierać conajmniej 2 znaki.",
                regex: "Imie jest niepoprawne."
            },
            Nazwisko: {
                required: "Nazwisko jest wymagane.",
                minlength: "Nazwisko musi zawierać conajmniej 2 znaki.",
                regex: "Nazwisko jest niepoprawne."
            },
            Ulica: {
                required: "Pole Ulica jest wymagane.",
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Telefon jest niepoprawny."
            },
            Numer: {
                required: "Numer domu jest wymagany.",
                regex: "Numer domu jest niepoprawny."
            },
            Miasto: {
                required: "Miasto jest wymagane.",
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne."
            },
            Telefon: {
                required: "Numer telefonu jest wymagany.",
                regex: "Numer telefonu jest niepoprawny."
            },
            KodPocztowy: {
                required: "Kod pocztowy jest wymagany.",
                regex: "Kod pocztowy jest niepoprawny."
            }
        }
    });
}
