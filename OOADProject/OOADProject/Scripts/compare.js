$(function () {
    $("#Button_Compare").hide();
    if ($("#Compare").val() != "") {
        $("#Compare").val($("#Compare").val() + "-");
        $("#Button_Compare").show();
    }
    $("input[id*='CheckBox_Car']").change(function () {
        if (this.checked) {
            $("#Compare").val($("#Compare").val() + this.name + "-");
        }
        else {
            $("#Compare").val($("#Compare").val().replace(this.name + "-", ''));
        }
        if ($("#Compare").val() != "") {
            $("#Button_Compare").show();
        }
        else
            $("#Button_Compare").hide();
    });
    $("#Button_Compare").click(function () {
        $("#Compare").val($("#Compare").val().slice(0, -1));
        count = $("#Compare").val().split("-");
        if (count.length < 2) {
            alert("請至少勾選兩台車");
            return false;
        }

    });
})