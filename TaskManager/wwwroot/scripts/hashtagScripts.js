$(function() {
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
});