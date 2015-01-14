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
    $("#anuluj-box").delegate("#cancel-start","click",function () {
        $(this).empty().append("<span style='color:#f89b1d;'><div style='float:left;margin-right:10px;margin-left:120px;line-height:24px'>Czy jesteś pewien?</div> <div id='yes' class='button-options' style='background-color:transparent;color:green'>TAK &#10004;</div><div id='no' class='button-options' style='background-color:transparent;color:red'>NIE &#10008;</div></span>");
    });

    $(document).bind('mousemove', function (e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $("#anuluj-box").delegate("#yes","click",function () {
        showAjaxLoader();
        var id = $(this).parent().parent().parent().siblings("#id-box").children('p').html();
        $.ajax({
            url: '/Konto/AnulujZamówienie/' + id,
            contentType: 'application/html; charset=utf-8',
            type: 'POST'
        }).success(function (data) {
            if (data == true) {
                $("#cancel-start").empty().append("<span style='color:green;'>Zamówienie zostało pomyślnie anulowane &#10004;.</span>");
                $("#order-info").empty().html('ANULOWANO').css("color", "red");
            } else {
                $("#cancel-start").empty().append("<span style='color:red;'>Nie udało się anulować zamówienia, spróbuj później &#10004;.</span>");

            }
            hideAjaxLoader();
        });
    });

    $("#anuluj-box").delegate("#no", "click", function() {
        $("#anuluj-box").html("<p style='cursor:pointer' class='cancel' id='cancel-start'>ANULUJ ZAMÓWIENIE</p>");
    });
});