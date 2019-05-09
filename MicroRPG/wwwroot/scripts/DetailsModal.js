$(document).click(function (event) {
    if ($(event.target).is(".getDetails")) {
        $(".modal").css("display", "block");
        $.ajax({
            url: $(event.target).attr('data-route'),
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