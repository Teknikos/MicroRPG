$(document).click(function (event) {
    if ($(event.target).is(".getDetails")) {
        $(".modal").css("display", "block");
        $.ajax({
            url: "/" + $(".modal").attr('data-route') + "/" + $(event.target).attr('alt'),
            type: "GET",
            success: function (result) {
                $(".modal > *").html(result);
                $(".modal > * .closeBtn").click(function () {
                    $(".modal").css("display", "none");
                });
            }
        });
    }
});

$(".modal").click(function (event) {
    if ($(event.target).is(".modal")) {
        $(".modal").css("display", "none");
    }
});