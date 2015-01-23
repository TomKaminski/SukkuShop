function onFileSelected(event) {
    var selectedFile = event.target.files[0];
    var reader = new FileReader();
    var readerTarget = event.target.id;
    reader.onload = function (eventt) {
        var img = new Image();
        if (readerTarget == "ImageBig") {
            img.src = eventt.target.result;
            if (img.width > 500 && img.height > 500 && img.height == img.width) {
                $(".bad-image").css("visibility", "hidden");
                $("#image-container").empty().append("<span class='helper'></span><img id='loadedImageBig' />");
                $("#loadedImageBig").attr("src", eventt.target.result);
                $("#image-container").append("<div id='delete-img' style='z-index:100;position:absolute;width:25px;height:25px;background-color:red;color:white;line-height:25px;font-family:Segoe UI;text-align:center;top:0;right:0'>X</div>");
                $("#image-container").css("border-color", "#999999");
            } else {
                $(".bad-image").css("visibility","visible");
                $("#ImageBig").val('');
                //e.stopImmediatePropagation();
            }
        }
    };
    reader.readAsDataURL(selectedFile);
}

$("#start-upload").on('click', function () {
    $("#CreateProductForm").submit();
});

$("#image-container").click(function () {
    if ($(this).children("img").length == 0)
        $("#ImageBig").click();
});

$("#image-container").delegate("#delete-img", "click", function (e) {
    $("#ImageBig").val('');
    $(this).parent().html("<div class='bubble'><p>Wstaw zdjęcie <br /><span style='font-size: 30px;text-transform: none;'>MIN. 500x500</span></p></div>");
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

    mainselectchange();

    $(".textbox-container input").keyup(function () {
        if ($(this).val() == "")
            $(this).siblings('.icon-pencil').css('visibility', 'visible');
        else
            $(this).siblings('.icon-pencil').css('visibility', 'hidden');
    });

    $("#price-textbox,#prom-textbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0 && e.which != 44 && e.which != 46 && (e.which < 48 || e.which > 57))
            return false;
        if ($('#price-textbox').val().length == 0 && e.which == 48)
            return false;
    });

    $("#quantity-textbox").keypress(function (e) {
        if (e.which != 8 && e.which != 0  && (e.which < 48 || e.which > 57))
            return false;
        if ($('#quantity-textbox').val().length == 0 && e.which == 48)
            return false;
    });

    $("select#MainCategoryList").change(function () {
        $.getJSON("/Admin/AdminProduct/GetSubCategoryList", { id: $(this).val(), ajax: 'true' }, function (j) {
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
            },
            Packing: {
                minlength: 2,
                regex: /^[1-9][0-9]*[A-Ża-ż]{2}$|^$/,
            },
            Promotion: {
                min: 0,
                max: 100,
            },
            Quantity: {
                min: 0
            }
        },
        messages: {
            Title: {
                required: "Nazwa produktu jest wymagana.",
                minlength: "Nazwa produktu musi zawierać conajmniej 2 znaki.",
            },
            Price: {
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
        $("#sublist select").attr("disabled",false);
    } else {
        $("#sublist").removeAttr("style");
        $("#sublist select").removeAttr("style");
        $("#sublist select").attr("disabled", true);
        $("#sublist").addClass("plz");
    }
        
}