$(document).ready(function() {
    //Zoom
    $('#product-image-details').elevateZoom({
        zoomType: "inner",
        cursor: "crosshair",
        zoomWindowFadeIn: 500,
        zoomWindowFadeOut: 750
    });

    $(document).bind('mousemove', function (e) {
        $('#ajax-processing').css({
            left: e.pageX + 20,
            top: e.pageY
        });
    });

    $(".ask-for-product").click(function (event) {
        $("#dialog").dialog("open");
        event.preventDefault();
    });

    $("span.ui-button-text").html("x");
    //Counter --
    $('#decrease_quantity').click(function() {
        var value = $('#quantity_counter').val();
        if (value > 1)
            value--;
        $('#quantity_counter').val(value);
    });

    $('#add-to-cart-animation').hide();

    //Counter key control
    $("#quantity_counter").keypress(function(e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
            return false;
        if ($('#quantity_counter').val().length == 0 && e.which == 48)
            return false;
    });

    $('#submitdialog').click(function (event) {
        
        event.preventDefault();
        if (!$("input#email").hasClass("error") && $("input#email").val()!="") {
            showAjaxLoader();
            var url = $('#dialogform').attr("action");
            var formData = $('#dialogform').serialize();
            $.ajax({
                url: url,
                type: "POST",
                data: formData
            }).success(function (data) {
                hideAjaxLoader();
                if (data == true) {
                    $("#dialogsuccess").empty().append("<div style='color:green;text-align: center;font-family: Segoe UI'>Dziękujemy za zgłoszenie</div>");
                } else {
                    $("#dialogsuccess").empty().append("<div style='color:red;text-align: center;font-family: Segoe UI'>Wsytąpił błąd, spróbuj ponownie</div>");
                }
            });
        }
        
    });

    //Ajax add to cart from details
    $('.add-to-cart-button').click(function () {
        var url = $('#addtocartform').attr("action");
        var formData = $('#addtocartform').serialize();
        $.ajax({
            url: url,
            type: "POST",
            data: formData
        }).success(function (data) {
            var plza = $("#cart-price-header").html().toLowerCase().replace('&nbsp;', ' ').replace('zł', '').replace('koszyk', '').replace(' ', '');
            if (parseFloat(plza) != parseFloat(data.value)) {
                $("#add-to-cart-animation").css("color", "green");
                $("#add-to-cart-animation").html("Produkt został dodany do koszyka &#10003;");
                $('#add-to-cart-animation').stop().show("slow", function() {
                    $('#add-to-cart-animation').fadeOut(3000);
                });
                $("#cart-price-header").html('koszyk ' + data.value + ' zł');
            } else {
                $("#add-to-cart-animation").css("color", "red");
                $("#add-to-cart-animation").html("Aktualnie nie dysponujemy wiekszą iloscią tego produktu &#10006;");
                $('#add-to-cart-animation').stop().show("slow", function () {
                    $('#add-to-cart-animation').fadeOut(3000);
                });
            }
        });
    });
});

$("#dialog").dialog({
    autoOpen: false,
    width: 600,
    modal: true,
    resizable: false
});

function hideAjaxLoader() {
    $("#ajax-processing").hide();
}
function showAjaxLoader() {
    var loaderDiv = $("#ajax-processing");
    if (loaderDiv.length === 0) {
        $("body").append("<div id='ajax-processing'></div>");
        loaderDiv = $("#ajax-processing");
    }
    loaderDiv.show();
}
function plz(data) {
    var plza = parseFloat($("#cart-price-header").html().toLowerCase().replace('&nbsp;', ' ').replace('zł', '').replace('koszyk', '').replace(',', '.').replace(' ', ''));
    if (plza != data.value) {
        var obj = $("#img" + data.id).parent().parent().parent().children('div.add-to-cart-info');
        obj.css('color', 'green');
        obj.html('&#10003;');
        obj.stop().show("fast", function () {
            obj.fadeOut(2500);
        });
        $("#cart-price-header").html('koszyk ' + data.value + ' zł');
        //var cart = $("#img" + data.id);
        //var imgtodrag = cart.parent().parent().parent().parent().parent().find("img").eq(0);
        //if (imgtodrag) {
        //    imgtodrag.clone()
        //        .offset({
        //            top: imgtodrag.offset().top,
        //            left: imgtodrag.offset().left
        //        })
        //        .addClass("flying-product-start")
        //        .css('position', 'absolute')
        //        .appendTo(jQuery('body')).
        //        animate({
        //            'top': cart.offset().top + 10,
        //            'left': cart.offset().left + 10,
        //            'width': '10px',
        //            'height': '10px',
        //            'position': 'absolute'
        //        }, 1000, function() {
        //            jQuery(this).animate({
        //                'width': 0,
        //                'height': 0
        //            }, function() {
        //                jQuery(this).detach();
        //            });
        //        });
        //}
    } else {
        var obj2 = $("#img" + data.id).parent().parent().parent().children('div.add-to-cart-info');
        obj2.css('color', 'red');
        obj2.html('&#10006;');
        obj2.stop().show("fast", function () {
            obj2.fadeOut(2500);
        });
    }

};

$("#dialogform").validate({
    rules: {
        email: {
            required: true,
            email: true
        }
    },
    messages: {
        email: {
            required: "Email jest wymagany.",
            email: "Podaj poprawny adres email."
        }
    }
});