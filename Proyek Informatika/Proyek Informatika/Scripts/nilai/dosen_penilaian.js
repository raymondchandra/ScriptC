$(function () {
    $("#penilaian_penguji").dialog({
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
    $("#penilaian_pembimbing").dialog({
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
    $("#nilai_skripsi2_pembimbing").click(function () {
        $("#penilaian_pembimbing").dialog("open");
    });
    $("#nilai_skripsi2_penguji").click(function () {
        $("#penilaian_pembimbing").dialog("open");
    });
    $("#nilai_uji").click(function () {
        alert("test");
    }
    );
});