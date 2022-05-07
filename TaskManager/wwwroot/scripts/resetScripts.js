function resetForms() {
    $(document).find("#signInForm")[0].reset();
    $(document).find("#signUpForm")[0].reset();
    $(document).find("#addForm")[0].reset();
    $(document).find("#editForm")[0].reset();
    $(this).find("span").empty();
    $(this).find("ul").empty();
};

$(function() {
    $("#signUpModal").on("hidden.bs.modal", resetForms);
    $("#signInModal").on("hidden.bs.modal", resetForms);
    $("#addModal").on("hidden.bs.modal", resetForms);
    $("#editModal").on("hidden.bs.modal", resetForms);
});