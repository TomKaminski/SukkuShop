$(document).ready(function () {

    //flying product
    $('.add-to-cart-icon').on('click', function () {
        var cart = $(this);
        var imgtodrag = $(this).parent().parent().parent().parent().find("img").eq(0);
        if (imgtodrag) {
            imgtodrag.clone()
                .offset({
                    top: imgtodrag.offset().top,
                    left: imgtodrag.offset().left
                })
                .addClass("flying-product-start")
                .appendTo($('body')).
                animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 10,
                    'width': '10px',
                    'height': '10px'
                }, 1000, function () {
                    $(this).animate({
                        'width': 0,
                        'height': 0
                    }, function () {
                        $(this).detach();
                    });
                });
        }
    });


});