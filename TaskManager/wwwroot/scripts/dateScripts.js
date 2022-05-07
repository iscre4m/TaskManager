$(function() {
    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + 1);
    
    $("#addEndDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#addEndDate").attr("value", $("#addEndDate").attr("min"));
    $("#addEndDate").attr("max", maxDate.toISOString().split("T")[0]);
    
    $("#editEndDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#editEndDate").attr("value", $("#editEndDate").attr("min"));
    $("#editEndDate").attr("max", maxDate.toISOString().split("T")[0]);
});