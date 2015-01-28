

$(document).ready(function() {
    $(".hexagon").mouseenter(function() {
        var id = $(this).attr("id");
        $(".mini-box #" + id).addClass("selected-mini");
    }).mouseleave(function() {
        $(".hexagon-mini").removeClass("selected-mini");
    });

});