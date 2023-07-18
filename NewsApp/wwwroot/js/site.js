


showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}
/*var post = {
    init: function () {
        post.registerEvents();
    },
    registerEvents: function () {
        $('#btnCommentNew').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var postid = btn.data('productid');
            var userid = btn.data('userid');
            var commentmsg = document.getElementById('txtCommentNew');
            var rate = document.getElementById('ddlRate');
            if (commentmsg.value == "") {

                bootbox.alert("You haven't written your comment yet");
                return;
            }
            $.ajax({
                url: URL("Post","CreateComment"),
                data: {
                    postid: postid,
                    userid: userid,
                    commentmsg: commentmsg.value,
                    rate: rate.value
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        commentmsg.value = "";
                        bootbox.alert({
                            message: "Success!",
                            size: 'medium',
                            closeButton: false
                        });
                        $("#commentList").load("/Post/Details?id=" + postid);
                    }
                    else {
                        bootbox.alert("Error!");
                    }
                }
            });
        });

        


    }
}
post.init();*/