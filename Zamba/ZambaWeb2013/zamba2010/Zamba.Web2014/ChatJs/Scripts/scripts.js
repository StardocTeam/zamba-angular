

$(document).ready(function () {
    $("#joinChatButton").click(function (e) {
        e.preventDefault();
        $("#loginModal").modal('show');
        $("#loginModal").on('shown', function () {
            $("#userName").focus();
        });
    });
    
    $("#leaveChatButton").click(function (e) {
        e.preventDefault();
        window.location = "/Home/LeaveChat";
    });

    $("#joinChatConclusionButton").click(function (e) {
        e.preventDefault();
        var $id = $("#id");
        //var $email = $("#email");

        // add validation error if not defined
        if (!$id.val() || $id.val() == $id.attr("placeHolder"))
            $id.closest(".control-group").addClass("error");
        else {
            window.location = "/Home/JoinChat?id=" + $id.val();
        }
    });
    
    // if the user presses anything, remove the validation error
    $("#userName").keypress(function() {
        $(this).closest(".control-group").removeClass("error");
    });
});