$(() => {
    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + 1);
    
    $("#endDate").attr("min", new Date().toISOString().split("T")[0]);
    if ($("#endDate").val() == "") {
        $("#endDate").attr("value", new Date().toISOString().split("T")[0]);
    }
    $("#endDate").attr("max", maxDate.toISOString().split("T")[0]);
});