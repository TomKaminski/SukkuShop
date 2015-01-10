function showAjaxLoader() {
    var loaderDiv = $("#ajax-processing");
    if (loaderDiv.length === 0) {
        $("body").append("<div id='ajax-processing'></div>");
        loaderDiv = $("#ajax-processing");
    }
    loaderDiv.show();
}

function SendEmailSuccess() {
    $("#ajax-processing").hide();
}

function hideAjaxLoader() {
    $("#ajax-processing").hide();
}

$(document).ready(function () {

    $("#ajax-processing").hide();

    $("#replace").delegate(".przypomnij", "click", function() {
        $("#ZapomnianeHasloForm").submit();
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

    $("#ZapomnianeHasloForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
        messages: {
            Email: {
                required: "Email jest wymagany.",
                email: "Podaj poprawny adres email."
            }
        }
    });
});