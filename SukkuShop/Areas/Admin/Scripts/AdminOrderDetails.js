function showAjaxLoader() {
    var loaderDiv = jQuery("#ajax-processing");
    if (loaderDiv.length === 0) {
        jQuery("body").append("<div id='ajax-processing'></div>");
        loaderDiv = jQuery("#ajax-processing");
    }
    loaderDiv.show();
}

function showAjaxTick() {
    var loaderDivv = jQuery("#ajax-completetick");
    if (loaderDivv.length === 0) {
        jQuery("body").append("<div id='ajax-completetick'>&#10004;</div>");
        loaderDivv = jQuery("#ajax-completetick");
    }
    loaderDivv.show();
}

function fadeOutAjaxTick() {
    jQuery("#ajax-completetick").stop().fadeOut(1000);
}
function hideAjaxTick() {
    jQuery("#ajax-completetick").stop().hide();
}


function hideAjaxLoader() {
    jQuery("#ajax-processing").hide();
}

jQuery(document).bind('mousemove', function (e) {
    jQuery('#ajax-processing,#ajax-completetick').css({
        left: e.pageX + 20,
        top: e.pageY
    });
});

function endAjaxLoader() {
    hideAjaxLoader();
    showAjaxTick();
    hideAjaxTick();
}

jQuery(document).ready(function () {

    showAjaxLoader();
    hideAjaxLoader();
    showAjaxTick();
    hideAjaxTick();

    $("#replace").delegate("#stateDropdown", "change", function() {
        if ($("select#stateDropdown").val() == "Wysłane")
            $("#number-box").css("visibility", "visible");
        else {
            $("#number-box").css("visibility", "hidden");
            $("#numberAlert").css("visibility", "hidden");
        }
    });

    $("#replace").delegate("a#submitOrderState", "click", function(event) {
        event.preventDefault();
        if ($("#stateDropdown").val() == "Wysłane" && $("#packageNumber").val() == "") {
            $("#packageNumber").css("border", "1px solid red");
            $("#numberAlert").css("visibility", "visible");
        } else {
            showAjaxLoader();
            $("#changeStateForm").submit();
            $("#numberAlert").css("visibility", "hidden");
        }

    });
});