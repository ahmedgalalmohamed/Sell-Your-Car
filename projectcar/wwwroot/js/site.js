

$(document).ready(function () {
    $(".btn-back").css({ "background-color": "#4D34BC", "color": "#fff"});
    $("a").css({
        "cursor": "pointer"
    });
    var deletebtn = $(".delete");
    deletebtn.click(function (e) {
        var con = prompt("Please enter 'y' to delete");
        if (con === "y") {
            return;
        }
        e.preventDefault();
    })

    var navbar = $(".navbarDarkDropdownMenuLink");
    var showlistPages = $(".showlist");
    navbar.hover(function () {
        showlistPages.addClass("show")
        $(this).addClass("btn-top")
    }, function () {
        showlistPages.hover(function () { }, function () {
            showlistPages.removeClass("show")
            navbar.removeClass("btn-top")
        });
    });
});


