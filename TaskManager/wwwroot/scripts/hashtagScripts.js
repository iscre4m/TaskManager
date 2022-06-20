$(() => {
    $("#addHashtagButton").click(function () {
        if ($("#hashtag").val() === "") {
            return;
        }

        for (let item of $("#hashtagsList").children()) {
            if (item.innerText === `#${$("#hashtag").val()}`) {
                return;
            }
        }

        if (!/#/.test($("#hashtag").val())) {
            $("#hashtag").val(`#${$("#hashtag").val()}`);
        }

        const hashtagsCount = $("#hashtagsList").children().length;
        const closeButtonId = `removeHashtag${$("#hashtagsList").children().length}`;

        $("#hashtagsList").addClass("mt-2");
        $("#hashtagsList").append(`<li class="list-group-item">
                                       <div class="d-flex justify-content-between">
                                           <input type="hidden" name="Hashtags[${hashtagsCount}].value" value="${$("#hashtag").val()}">
                                           ${$("#hashtag").val()}
                                           <input id="${closeButtonId}" type="button" class="btn-close">
                                       </div>
                                   </li>`);

        $(`#${closeButtonId}`).click(function () {
            $(this).parent().parent().remove();

            if ($("#hashtagsList").children().length === 0) {
                $("#subtasksList").removeClass("mt-2");
            }
        });

        $("#hashtag").val("");
    });
});