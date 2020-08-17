$(document).ready(function() {
    $(document).mousemove(function(e) {
        var x, y;

        x = $(document).scrollLeft() + e.clientX;
        y = $(document).scrollTop() + e.clientY;

        // x += 10; // important: if the popup is where the mouse is, the hoverOver/hoverOut events flicker
        x = $(document).scrollLeft() + e.clientX;
        y = $(document).scrollTop() + e.clientY - 110;

        if ($(".showHover").length > 0) {

            //            var x_y = nudge(x, y); // avoids edge overflow


            // remember: the popup is still hidden
            $('#divTestHover2').css('top', y + 'px');
            $('#divTestHover2').css('left', x + 'px');
        }
    });
});
