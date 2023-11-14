
    $(document).ready(function() {
        var y = $(document.body).height();
    var x = $(document).width();
    parent.$.colorbox.resize({innerWidth:x, innerHeight:y});

    $("#button_cancel").click(function() {
        parent.$.colorbox.close();
    return false;
        })
    });