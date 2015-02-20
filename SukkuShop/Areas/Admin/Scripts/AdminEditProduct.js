function onFileSelected(event) {
    var selectedFile = event.target.files[0];
    var reader = new FileReader();
    reader.onload = function (eventt) {
        var img = new Image();
        img.src = eventt.target.result;
        if (img.width > 500 && img.height > 500 && img.height == img.width) {
            $("#trueimg-box").hide();
            $(".bad-image").css("visibility", "hidden");
            $("#image-container").append("<img id='loadedImageBig' />");
            $("#loadedImageBig").attr("src", eventt.target.result);
            $(".left-container-edit").append("<div id='delete-img' class='danger-text' style='z-index:100;top:0px;left:0'>USUŃ ZDJĘCIE</div>");
            $("#image-container").css("border-color", "#999999");
        } else {
            $(".bad-image").css("visibility", "visible");
            $("#ImageBig").val('');
            //e.stopImmediatePropagation();
        }

    };
    reader.readAsDataURL(selectedFile);
}


$("#start-upload").on('click', function () {
    if ($(".css-checkbox").is(":checked"))
        if ($("#packing-textbox").val() == "" || $("#MainCategoryList").val() == "0" || $("#price-textbox").val() == "") {
            $(".error-plz").css("visibility", "visible");
            return false;
        }
    $(".error-plz").css("visibility", "hidden");
    $("#CreateProductForm").submit();
});

$("#image-container").click(function () {
    if ($(this).children("img#loadedImageBig").length==0 && !$("img#trueimg").is(":visible"))
        $("#ImageBig").click();
});

$(".left-container-edit").delegate("#hide-img", "click", function (e) {
    $("#trueimg").hide();
    $(".default-img").show();
    $(this).hide();
    $("#TrueImageDeleted").val('True');
    $("#tekst-noimg").show().html("<div class='bubble'><p>Wstaw zdjęcie <br /><span style='font-size: 30px;text-transform: none;'>MIN. 500x500</span></p></div>");
    e.stopImmediatePropagation();
});

$(".default-img").click(function(e) {
    $("#trueimg-box").show();
    $("#loadedImageBig").remove();
    $("#delete-img").remove();
    $("#trueimg").show();
    $("#TrueImageDeleted").val('False');
    $("#tekst-noimg").hide();
    $(this).hide();
    $("#hide-img").show();
    $(".bad-image").css("visibility", "hidden");
    e.stopImmediatePropagation();
});

$(".left-container-edit").delegate("#delete-img", "click", function (e) {
    $("#ImageBig").val('');
    $("#loadedImageBig").remove();
    $("#tekst-noimg").show();
    $("#trueimg-box").show();
    $("#delete-img").remove();
    $(this).remove();
    e.stopImmediatePropagation();
});
$(document).ready(function () {
    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "Nieprawidłowa wartość");

    $('#prom-textbox').on('change keyup paste', function () {
        if ($('#prom-textbox').val() > 100) {
            $(this).val(100);
        }
    });

    $(".default-img").hide();
    mainselectchange();
    $("#TrueImageDeleted").val('False');
    $(".textbox-container input").each(function() {
        if ($(this).val() == "") {
            $(this).siblings('.icon-pencil').css('visibility', 'visible');
        } else {
            $(this).siblings('.icon-pencil').css('visibility', 'hidden');
        }
    });

    $(".textbox-container input").keyup(function () {
        if ($(this).val() == "")
            $(this).siblings('.icon-pencil').css('visibility', 'visible');
        else
            $(this).siblings('.icon-pencil').css('visibility', 'hidden');
    });

    $("#price-textbox,#prom-textbox,#weight-textbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && e.which != 44 && e.which != 46 && (e.which < 48 || e.which > 57))
            return false;
    });

    $("#quantity-textbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
            return false;
        if ($('#quantity-textbox').val().length == 0 && e.which == 48)
            return false;
    });

    $("select#MainCategoryList").change(function () {
        $.getJSON("/Admin/Produkty/GetSubCategoryList", { id: $(this).val(), ajax: 'true' }, function (j) {
            var options = '';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].Value + '">' + j[i].Text + '</option>';
            }
            $("select#SubCategoryList").html(options);
        });
    });

    $("#CreateProductForm").validate({
        rules: {
            Title: {
                required: true,
                minlength: 2
            },
            Price: {
                regex: /^[0-9][0-9]*[,.]{0,1}[0-9]{0,2}$|^$/
            },
            Packing: {
                minlength: 2,
                regex: /^[0-9][0-9]*[A-Ża-ż]{1,2}$|^$/,
            },
            Promotion: {
                min: 0,
                max: 100,
            },
            Quantity: {
                min: 0
            },
            Weight: {
                regex: /^[0-9][0-9]*[,.]{0,1}[0-9]{0,2}$|^$/
            }
        },
        messages: {
            Title: {
                required: "Nazwa produktu jest wymagana.",
                minlength: "Nazwa produktu musi zawierać conajmniej 2 znaki.",
            },
            Price: {
                regex: "Zły format!"
            },
            Packing: {
                minlength: "Sposób pakowania musi zawierać conajmniej 2 znaki.",
                regex: "Zły format!"
            },
            Promotion: {
                min: "Promocja powinna zawierać się w przedziale 0-100",
                max: "Promocja powinna zawierać się w przedziale 0-100"
            },
            Quantity: {
                min: "Minimalna wartość to 0"
            },
            Weight: {
                regex: "Zły format!"
            }
        }
    });
});


function mainselectchange() {
    var plz = $("#MainCategoryList").val();
    if (plz != "0") {
        $("#sublist").removeClass("plz");
        $("#sublist").css("background", "url('../../../../Areas/Admin/Content/Images/selectboxbtn.png') no-repeat right center");
        $("#sublist select").css("color", "#f89b1d");
        $("#sublist select").attr("disabled", false);
        $.getJSON("/Admin/Produkty/GetSubCategoryList", { id: $("select#MainCategoryList").val(), ajax: 'true' }, function (j) {
            var options = '';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].Value + '">' + j[i].Text + '</option>';
            }
            $("select#SubCategoryList").html(options);
        });
    } else {
        $("#sublist").removeAttr("style");
        $("#sublist select").removeAttr("style");
        $("#sublist select").attr("disabled", true);
        $("#sublist").addClass("plz");
    }

}