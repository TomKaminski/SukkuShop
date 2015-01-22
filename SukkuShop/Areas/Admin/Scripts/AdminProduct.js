function onFileSelected(event) {
    var selectedFile = event.target.files[0];
    var reader = new FileReader();
    var readerTarget = event.target.id;
    reader.onload = function (eventt) {
        var img = new Image();
        if (readerTarget == "ImageBig") {
            img.src = eventt.target.result;
            if (img.width > 500 && img.height > 500 && img.height == img.width) {
                $("#image-container").empty().append("<span class='helper'></span><img id='loadedImageBig' />");
                $("#loadedImageBig").attr("src", eventt.target.result);
                $("#image-container").append("<div id='delete-img' style='z-index:100;position:absolute;width:25px;height:25px;background-color:red;color:white;line-height:25px;font-family:Segoe UI;text-align:center;top:0;right:0'>X</div>");
                $("#image-container").css("border-color", "royalblue");
            } else {
                $("#ImageBig").val('');
                $("#image-container").css("border-color", "red");
                $("#image-container").html("<div style='color:red;font-weight: bold;text-align: center;line-height: 500px;font-family: Segoe UI;font-size: 25px'>Załaduj poprawny obrazek (500x500)</div>");
                e.stopImmediatePropagation();
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
    $(this).parent().html("<div style='color:royalblue;font-weight: bold;text-align: center;line-height: 500px;font-family: Segoe UI;font-size: 25px'>Załaduj obrazek</div>");
    e.stopImmediatePropagation();
});
$(document).ready(function () {
    $.validator.addMethod("regex", function (value, element, regexpr) {
        return regexpr.test(value);
    }, "Nieprawidłowa wartość");

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
                regex: /^[1-9][0-9]*[,.][0-9]{2}$/
            },
            Packing: {
                minlength: 2,
                regex: /^[1-9]{1}[0-9]*[a-żA-Ż]+$/,
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
                regex: "Zły format ceny!"
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

function CreateProductForm() {
    
}