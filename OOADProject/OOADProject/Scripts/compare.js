$(function () {
    if ($("#Compare").val() != "")
        $("#Compare").val($("#Compare").val() + "-");
    $("input[id*='CheckBox_Car']").change(function () {
        if (this.checked) {
            $("#Compare").val($("#Compare").val() + this.name+"-");
        }
        else {
            $("#Compare").val($("#Compare").val().replace(this.name+"-",''));
        }
    });
    $("#Button_Compare").click(function () {
        $("#Compare").val($("#Compare").val().slice(0, -1));
        count = $("#Compare").val().split("-");
        if (count.length < 2)
        {
            alert("請勾選至少兩台車");
            return false;
        }
            
    });
})