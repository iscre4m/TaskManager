$(function() {
    $("#addSubtaskButton").click(function () {
        if ($("#subtask").val() === "") {
            return;
        }

        for (let item of $("#subtasksList").children()) {
            if (item.innerText === $("#subtask").val()) {
                return;
            }
        }

        let subtasksCount = $("#subtasksList").children().length;
        const closeButtonId = `removeSubtask${subtasksCount}`;

        $("#subtasksList").addClass("mt-2");
        $("#subtasksList").append(`<li class="list-group-item">
                                        <div class="d-flex justify-content-between">
                                            <input type="hidden" name="Subtasks[${subtasksCount}].description" value="${$("#subtask").val()}">
                                            ${$("#subtask").val()}
                                            <input id="${closeButtonId}" type="button" class="btn-close">
                                        </div>
                                    </li>`);

        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();

            if ($("#subtasksList").children().length === 0) {
                $("#subtasksList").removeClass("mt-2");
            }
        });

        $("#subtask").val("");
    });
});