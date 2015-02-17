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

    $("#replace").delegate("a#submitOrderState", "click", function(event) {
        event.preventDefault();
        showAjaxLoader();
        $("#changeStateForm").submit();
    });
});