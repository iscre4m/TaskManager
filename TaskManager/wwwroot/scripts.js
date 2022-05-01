function resetForms() {
    $(document).find("#signInForm")[0].reset();
    $(document).find("#signUpForm")[0].reset();
    $(document).find("#addForm")[0].reset();
    $(this).find("span").empty();
};

$(function() {
    $("#signUpModal").on("hidden.bs.modal", resetForms);
    $("#signInModal").on("hidden.bs.modal", resetForms);
    $("#addModal").on("hidden.bs.modal", resetForms)
    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + 1);
    $("#addEndDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#addEndDate").attr("max", maxDate.toISOString().split("T")[0]);
});