$("#gameCreaturesBtn").click(function () {
    $.ajax({
        url: "/creatures/",
        type: "GET",
        success: function (result) {
            $("#gamePartialContainer").html(result);
            $("#gamePartialContainer").css("display", "block");
            $("#gameMain").css("display", "none");
        }
    });
});
$("#gameObstaclesBtn").click(function () {
    $.ajax({
        url: "/obstacles/",
        type: "GET",
        success: function (result) {
            $("#gamePartialContainer").html(result);
            $("#gamePartialContainer").css("display", "block");
            $("#gameMain").css("display", "none");
        }
    });
});
$("#gamePuzzlesBtn").click(function () {
    $.ajax({
        url: "/puzzles/",
        type: "GET",
        success: function (result) {
            $("#gamePartialContainer").html(result);
            $("#gamePartialContainer").css("display", "block");
            $("#gameMain").css("display", "none");
        }
    });
});
$("#gamePartialContainer").click(function (event) {
    if ($(event.target).is(".backBtn")) {
        $("#gamePartialContainer").css("display", "none");
        $("#gameMain").css("display", "block");
    }
});