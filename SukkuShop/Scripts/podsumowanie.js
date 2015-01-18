$("#zlozzamowienie").click(function () {
    $("#OrderSubmit").submit();
});

$(document).ready(function() {
    $("#quantity-cell.submit-error,#quantity-cell.remove-item-error").append("<div class='submit-error-tick'>&#10004;</div>");

    $(".submit-error").delegate(".submit-error-tick", "click", function () {
        $(this).removeClass("submit-error");
        $(this).children("#old-quantity").remove();
        $(this).children("#new-quantity").css("color", "black").css("font-weight", "normal");
        $(this).children(".submit-error-tick").remove();
    });

    $(".remove-item-error").delegate(".submit-error-tick", "click", function () {
        $(this).parent().parent().fadeOut(2000);
    });


});