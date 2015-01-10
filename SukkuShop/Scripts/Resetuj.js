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

function SendEmailSuccess() {
    $("#ajax-processing").hide();
}

function hideAjaxLoader() {
    $("#ajax-processing").hide();
}

$(document).ready(function () {

    $("#ajax-processing").hide();

    $(".resetuj-cont").delegate(".resetuj", "click", function () {
        $("#ResetujHasloForm").submit();
    });

    $(document).bind('mousemove', function (e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $(".data-box input").click(function () {
        if ($(this).siblings(".field-validation-error").html('') != "") {
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
    });

    $("#ResetujHasloForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true, minlength: 6
            },
            ConfirmPassword: {
                equalTo: "#Password"
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
            }
        }
    });
});