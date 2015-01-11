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
    $("div.validation-box div").fadeOut(5000);
}

function onError(xhr, status, error) {
    alert(error);
    console.log(xhr.responseText);
}


$(document).ready(function() {
    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "Nieprawidłowa wartość");

    $.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != param;
    }, "Proszę podać inną wartość");

    $(".details-container").delegate(".data-box input", "focus", function () {
        if ($(this).val() == "Nie podano")
            $(this).val("");
        if ($(this).siblings(".field-validation-error").html('') != "") {
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
    });

    changeuserInfo();
    changeFirmaInfo();
    changePasswordForm();

    $(".change-container").delegate("input[id=firmafalse]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Konto/ChangeUserInfoViewModel',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            error: function (xhr, status, error) {
                onError(xhr, status, error);
            },
            complete: function () { changeuserInfo(); }
        }).success(function (data) {
            setTimeout(hideAjaxLoader, 1000);
            $("#replace").empty().append(data);
        });
    });

    $(".change-container").delegate("input[id=firmatrue]", "click", function () {
        showAjaxLoader();
        $.ajax({
            url: '/Konto/ChangeUserFirmaInfoViewModel',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            error: function(xhr, status, error) {
                onError(xhr, status, error);
            },
            complete: function () { changeFirmaInfo(); }
        }).success(function (data) {
            setTimeout(hideAjaxLoader, 1000);
            $("#replace").empty().append(data);
        });
    });

    $("#ajax-processing").hide();

    $(document).bind('mousemove', function(e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $("#replace").delegate("#save-nofirma", "click", function () {
        $("#changeUserInfoForm").submit();
    });

    $("#replace").delegate("#save-firma", "click", function () {
        $("#changeFirmaInfoForm").submit();
    });

    $("#pass-replace").delegate("#save-pass", "click", function () {
        $("#changePasswordForm").submit();
    });

});


function changeFirmaInfo() {
    $("#changeFirmaInfoForm").validate({
        rules: {
            NazwaFirmy: {
                minlength: 2
            },
            Nip: {
                minlength: 10
            },
            Street: {
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/
            },
            Number: {
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$|^Nie podano$/
            },
            City: {
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/
            },
            Phone: {
                regex: /^[1-9][0-9]{8}$|^Nie podano$/
            },
            PostalCode: {
                regex: /^[0-9]{2}-[0-9]{3}$|^Nie podano$/
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            NazwaFirmy: {
                minlength: "Nazwa firmy musi zawierać conajmniej 2 znaki.",
            },
            Nip: {
                minlength: "Nip to 10 cyfr.",
            },
            Street: {
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Ulica jest niepoprawna."
            },
            Number: {
                regex: "Numer domu jest niepoprawny."
            },
            City: {
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne."
            },
            Phone: {
                regex: "Numer telefonu jest niepoprawny."
            },
            PostalCode: {
                regex: "Kod pocztowy jest niepoprawny."
            },
            Email: {
                required: "Email jest wymagany.",
                email: "Podaj poprawny adres email."
            }
        }
    });
}

function changeuserInfo() {
    $("#changeUserInfoForm").validate({
        rules: {
            Name: {
                minlength: 2
            },
            LastName: {
                minlength: 2
            },
            Street: {
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/
            },
            Number: {
                regex: /^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$|^Nie podano$/
            },
            City: {
                minlength: 2,
                regex: /^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$/
            },
            Phone: {
                regex: /^[1-9][0-9]{8}$|^Nie podano$/
            },
            PostalCode: {
                regex: /^[0-9]{2}-[0-9]{3}$|^Nie podano$/
            },
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Name: {
                minlength: "Nazwa firmy musi zawierać conajmniej 2 znaki.",
            },
            LastName: {
                minlength: "Nazwisko musi zawierać conajmniej 2 znaki.",
            },
            Street: {
                minlength: "Ulica musi zawierać conajmniej 2 znaki.",
                regex: "Ulica jest niepoprawna."
            },
            Number: {
                regex: "Numer domu jest niepoprawny."
            },
            City: {
                minlength: "Miasto musi zawierać conajmniej 2 znaki.",
                regex: "Miasto jest niepoprawne."
            },
            Phone: {
                regex: "Numer telefonu jest niepoprawny."
            },
            PostalCode: {
                regex: "Kod pocztowy jest niepoprawny."
            },
            Email: {
                required: "Email jest wymagany.",
                email: "Podaj poprawny adres email."
            }
        }
    });
}

function changePasswordForm() {
    $("#changePasswordForm").validate({
        rules: {
            OldPassword: {
                required: true, minlength: 6
            },
            NewPassword: {
                required: true, minlength: 6
            },
            ConfirmPassword: {
                equalTo: "#NewPassword"
            }
        },
        messages: {
            OldPassword: {
                required: "Wprowadź stare hasło.",
                minlength: "Stare hasło musi zawierać 6 lub więcej znaków."
            },
            NewPassword: {
                required: "Wprowadź nowe hasło.",
                minlength: "Nowe hasło musi zawierać 6 lub więcej znaków."
            },
            ConfirmPassword: {
                equalTo: "Nowe hasła muszą być takie same."
            }
        }
    });
}