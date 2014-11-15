$(document).ready(function () {
    //Zoom
    $('#product-image-details').elevateZoom({
        zoomType: "inner",
        cursor: "crosshair",
        zoomWindowFadeIn: 500,
        zoomWindowFadeOut: 750
    });

    
    //Counter --
    $('#decrease_quantity').click(function () {
        var value = $('#quantity_counter').val();
        if (value > 1)
            value--;
        $('#quantity_counter').val(value);
    });

    

    //Counter key control
    $("#quantity_counter").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
            return false;
        if ($('#quantity_counter').val().length == 0 && e.which == 48)
            return false;
    });
});