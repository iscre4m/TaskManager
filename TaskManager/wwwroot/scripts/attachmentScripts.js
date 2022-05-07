$(function() {
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