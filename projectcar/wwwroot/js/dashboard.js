$(document).ready(function () {
    $(".content").css({ "background-color": "#F2EDF3" });
    $(".text-shadow").css({ "text-shadow": "#464646 1px 2px 10px" });
    $(".btn-back").css({ "background-color": "#4D34BC", "color": "#fff"});

    $(".active_link").click(function () {
        $(".active_link").removeClass("active");
        $(this).addClass("active");
    });
    $("#toggle-link").click(function () {
        $("#arrow-car").toggleClass("fa-angle-right");
        $("#arrow-car").toggleClass("fa-angle-down");
    });
    $("#btn-content").click(function () {
        $(this).toggleClass("fa-angle-down");
        $(this).toggleClass("fa-angle-up");
    });
});