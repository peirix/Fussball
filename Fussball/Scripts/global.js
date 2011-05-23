$(function () {
    $(".backBtn").click(function () {
        history.go(-1);
    });

    $(".dataList td a").bigTarget({
        clickZone: 'tr',
        hoverClass: 'hover'
    });
});