$(function () {
    $("#photo").hide();
    var filter_flag = false;
    var Category = getParameterByName("c");
    var SeatAmount = getParameterByName("sa");
    var RentalCompany = getParameterByName("rc");
    var CarCompany = getParameterByName("cc");
    if (Category != "")
        $("#DropDownList_Category option[value='" + Category + "']").attr("selected", true);
    if (SeatAmount != "" && SeatAmount != 0)
        $("#DropDownList_SeatAmount option[value='" + SeatAmount + "']").attr("selected", true);
    if (RentalCompany != "" && RentalCompany != 0)
        $("#DropDownList_RentalCompany option[value='" + RentalCompany + "']").attr("selected", true);
    if (CarCompany != "")
        $("#DropDownList_CarCompany option[value='" + CarCompany + "']").attr("selected", true);
})
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
