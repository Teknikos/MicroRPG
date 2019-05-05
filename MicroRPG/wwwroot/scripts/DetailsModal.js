
$(".getDetails").click(function () {
    $(".modal").css("display", "block");
    $.ajax({
        url: "/" + $(".modal").attr('data-route') + "/" + $(this).attr('name'),
        type: "GET",
        success: function (result) {
            $(".modal > *").html(result);
            $(".modal > * .closeBtn").click(function () {
                $(".modal").css("display", "none");
            });
        }
    });
});

$(".modal").click(function (event) {
    if ($(event.target).is(".modal")) {
        $(".modal").css("display", "none");
    }
});