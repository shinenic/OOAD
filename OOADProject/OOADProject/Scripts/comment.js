$(function () {
    $("#Comment_button").click(function () {
        if ($("#UserId").val()=="")
        {
            alert("請先登入");
            return false;
        }
        if($("input[type='radio']:checked").val()==0)
        {
            alert("請勾選評價");
            return false;
        }
        if ($("#Content").val() == "") {
            alert("請填寫內容");
            return false;
        }
            
    });
})