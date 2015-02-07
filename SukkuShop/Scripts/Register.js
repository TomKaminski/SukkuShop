$(document).ready(function () {
    $(".zarejestruj").click(function (e) {
            e.preventDefault();
        $("#RegisterForm").submit();
    });

    $("input").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("#RegisterForm").submit();
        }
    });

    $(".data-box input").click(function () {
        if ($(this).siblings(".field-validation-error").html('') != "") {
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
    });

    $("#RegisterForm").validate({
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