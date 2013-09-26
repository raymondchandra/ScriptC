$(function () {
    $("#tool_kartubimbingan").dialog({
        autoOpen: false,
        width: 800,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });
    $("#btn_buka").click(function () {
        $("#tool_kartubimbingan").dialog("open");
    });
    $("#tool_pengumpulanfile").dialog({
        autoOpen: false,
        width: 800,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });
    $("#btn_buka_file").click(function () {
        $("#tool_pengumpulanfile").dialog("open");
    });
});
