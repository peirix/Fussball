$(function () {
    $(".backBtn").click(function () {
        history.go(-1);
    });

    $(".dataList td a").bigTarget({
        clickZone: 'tr',
        hoverClass: 'hover'
    });

    if ($("#StatisticsList tbody tr").length > 0) {
        $("#StatisticList").tablesorter({
            sortList: [[0, 0]]
        });
    }

    $(".dataList").tablesorter({
        sortList: [[0, 0]]
    });
});