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
function onError(xhr, status, error) {
    alert(error);
    console.log(xhr.responseText);
}

$(document).ready(function () {
    $("#anuluj-box").delegate("#cancel-start","click",function () {
        $(this).empty().append("<span style='color:#f89b1d;'><div style='float:left;margin-right:10px;margin-left:120px;'>Czy jesteś pewien?</div> <div id='yes' class='button-options' style='background-color:transparent;color:green'>TAK &#10004;</div><div id='no' class='button-options' style='background-color:transparent;color:red'>NIE &#10008;</div></span>");
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
            type: 'POST',
            error: function (xhr, status, error) {
                onError(xhr, status, error);
            }
        }).success(function (data) {
            if (data == true) {
                $("#anuluj-box").empty().append("<p class='cancel' style='color:green'>Zamówienie zostało pomyślnie anulowane &#10004;.</p>");
                $("#order-info").empty().html('ANULOWANO').css("color", "red");
            } else {
                $("#anuluj-box").empty().append("<p class='cancel' style='color:red'>Nie udało się anulować zamówienia, spróbuj później &#10004;.</p>");
            }
            hideAjaxLoader();
        });
    });

    $("#anuluj-box").delegate("#no", "click", function() {
        $("#anuluj-box").html("<p style='cursor:pointer' class='cancel' id='cancel-start'>ANULUJ ZAMÓWIENIE</p>");
    });
});