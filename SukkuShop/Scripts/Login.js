$(document).ready(function() {
    $(".zaloguj").click(function() {
            $("#LoginForm").submit();
        }
    );

    $(".data-box input").click(function () {
        if ($(this).siblings(".field-validation-error").html('') != "") {
            $(this).siblings(".field-validation-error").html('');
            $(this).removeClass("input-validation-error");
        }
    });

    $("#LoginForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            PasswordLogin: {
                required: true, minlength: 6
            }
        },
        messages: {
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