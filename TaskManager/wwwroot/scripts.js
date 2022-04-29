function resetForms () {
    $(document).find("form")[0].reset();
    $(document).find("form")[1].reset();
    $(this).find("span").empty();
};

$(function () {
    $("#signUpModal").on("hidden.bs.modal", resetForms);
    $("#signInModal").on("hidden.bs.modal", resetForms);
});