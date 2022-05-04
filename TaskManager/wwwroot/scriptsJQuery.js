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
    
    let maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() + 1);
    
    $("#addEndDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#addEndDate").attr("value", $("#addEndDate").attr("min"));
    $("#addEndDate").attr("max", maxDate.toISOString().split("T")[0]);
    
    $("#editEndDate").attr("min", new Date().toISOString().split("T")[0]);
    $("#editEndDate").attr("value", $("#editEndDate").attr("min"));
    $("#editEndDate").attr("max", maxDate.toISOString().split("T")[0]);
    
    $("#addAddSubtask").click(function () {
        if ($("#addSubtask").val() === "") {
            return;
        }

        for (let item of $("#addSubtasksList").children()) {
            if (item.innerText === $("#addSubtask").val()) {
                return;
            }
        }

        let subtasksCount = $("#addSubtasksList").children().length;
        const closeButtonId = `addRemoveSubtask${subtasksCount}`;
        
        $("#addSubtasksList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            <input type="hidden" name="Subtasks[${subtasksCount}].description" value="${$("#addSubtask").val()}">
                                            ${$("#addSubtask").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#addSubtask").val("");
    });

    $("#addAddHashtag").click(function () {
        if ($("#addHashtag").val() === "") {
            return;
        }

        for (let item of $("#addHashtagsList").children()) {
            if (item.innerText === `#${$("#addHashtag").val()}`) {
                return;
            }
        }

        if (!/#/.test($("#addHashtag").val())) {
            $("#addHashtag").val(`#${$("#addHashtag").val()}`);
        }

        const closeButtonId = `addRemoveHashtag${$("#addHashtagsList").children().length}`;

        $("#addHashtagsList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            ${$("#addHashtag").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#addHashtag").val("");
    });

    $("#addAddAttachment").click(function () {
        if ($("#addAttachment").val() === "") {
            return;
        }

        for (let item of $("#addAttachmentsList").children()) {
            if (item.innerText === $("#addAttachment").val()) {
                return;
            }
        }

        const closeButtonId = `addRemoveAttachment${$("#addAttachmentsList").children().length}`;

        $("#addAttachmentsList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            ${$("#addAttachment").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#addAttachment").val("");
    });

    $("#editAddSubtask").click(function() {
        if ($("#editSubtask").val() === "") {
            return;
        }

        for (let item of $("#editSubtasksList").children()) {
            if (item.innerText === $("#editSubtask").val()) {
                return;
            }
        }

        let subtasksCount = $("#editSubtasksList").children().length;
        const closeButtonId = `editRemoveSubtask${subtasksCount}`;

        $("#editSubtasksList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            <input type="hidden" name="Subtasks[${subtasksCount}].description" value="${$("#editSubtask").val()}">
                                            ${$("#editSubtask").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#editSubtask").val("");
    });

    $("#editAddHashtag").click(function () {
        if ($("#editHashtag").val() === "") {
            return;
        }

        for (let item of $("#editHashtagsList").children()) {
            if (item.innerText === `#${$("#editHashtag").val()}`) {
                return;
            }
        }

        if (!/#/.test($("#editHashtag").val())) {
            $("#editHashtag").val(`#${$("#editHashtag").val()}`);
        }

        const closeButtonId = `editRemoveHashtag${$("#editHashtagsList").children().length}`;

        $("#editHashtagsList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            ${$("#editHashtag").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#editHashtag").val("");
    });

    $("#editAddAttachment").click(function () {
        if ($("#editAttachment").val() === "") {
            return;
        }

        for (let item of $("#editAttachmentsList").children()) {
            if (item.innerText === $("#editAttachment").val()) {
                return;
            }
        }

        const closeButtonId = `editRemoveAttachment${$("#editAttachmentsList").children().length}`;

        $("#editAttachmentsList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            ${$("#editAttachment").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);
        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();
        });

        $("#editAttachment").val("");
    });
});