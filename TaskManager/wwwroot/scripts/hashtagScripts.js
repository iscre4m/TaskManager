$(() => {
    if ($("#hashtagsList").children().length > 0) {
        $("#hashtagsList").addClass("mt-2");
    }

    $(".removeHashtag").click(function () {
        $(this).parent().parent().remove();

        if ($("#hashtagsList").children().length === 0) {
            $("#hashtagsList").removeClass("mt-2");
        }
    });

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

        $("#hashtagsList").addClass("mt-2");
        $("#hashtagsList").append(`<li class="list-group-item">
                                       <div class="d-flex justify-content-between">
                                           <input type="hidden" name="Hashtags[${hashtagsCount}].value" value="${$("#hashtag").val()}">
                                           ${$("#hashtag").val()}
                                           <input type="button" class="btn-close removeHashtag">
                                       </div>
                                   </li>`);

        $(".removeHashtag").click(function () {
            $(this).parent().parent().remove();

            if ($("#hashtagsList").children().length === 0) {
                $("#hashtagsList").removeClass("mt-2");
            }
        });

        $("#hashtag").val("");
    });
});