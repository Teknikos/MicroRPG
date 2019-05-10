$(".enlargeImg img").click(function () {
    $(".modal > .imgContainer").html(this.cloneNode());
    $(".modal").css("display", "block");
})
$(".modal").click(function () {
    $(this).css("display", "none");
});