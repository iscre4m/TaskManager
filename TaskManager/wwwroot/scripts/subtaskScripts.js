$(function() {
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

    $("#editAddSubtask").click(function () {
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
});