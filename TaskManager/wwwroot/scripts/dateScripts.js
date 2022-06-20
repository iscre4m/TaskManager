$(() => {
    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + 1);
    
    $("#endDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#endDate").attr("value", $("#endDate").attr("min"));
    $("#endDate").attr("max", maxDate.toISOString().split("T")[0]);
});