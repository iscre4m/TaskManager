$(() => {
    $(".removeAttachment").click(function () {
        $(this).parent().parent().remove();
    });

    $("#attachments").change(function () {
        $("#attachmentsList").children().remove();
        let index = 0;
        for (let file of $("#attachments").get(0).files) {
            const closeButtonId = `removeAttachment${$("#attachmentsList").children().length}`;

            $("#attachmentsList").append(`<li class="list-group-item">
                                              <div class="d-flex justify-content-between">
                                                  <input type="hidden" name="Attachments[${index}].path" value="${file.name}">
                                                  ${file.name}
                                                  <input type="button" class="btn-close removeAttachment">
                                              </div>
                                          </li>`);

            $(".removeAttachment").click(function () {
                $(this).parent().parent().remove();
            });
        }
    });
});