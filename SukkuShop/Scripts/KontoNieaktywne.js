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
    $("#send-email-box").empty().append("<div class='complete-text'>Wiadomość została wysłana pomyślnie, sprawdź swoją pocztę. &#10004;</div><a class='link-text2' href='/Home/Index'>Przejdź do strony głównej</a>");
}

function SendEmailFailure() {
    $("#ajax-processing").hide();
    $("#send-email-box").empty().append("<div class='complete-text' style='color:red'>Niestety nie mogliśmy wysłać wiadomości. &#10008;</div><a class='link-text2' href='/Home/Index'>Przejdź do strony głównej</a>");
}

$(document).ready(function() {

    $("#ajax-processing").hide();

    $(document).bind('mousemove', function(e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });
});