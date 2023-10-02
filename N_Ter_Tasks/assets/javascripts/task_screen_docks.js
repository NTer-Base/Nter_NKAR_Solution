$(function () {
    $(window).on('load', function () {
        adjustDockScroll();
    });

    $(window).on('resize', function () {
        adjustDockScroll();
    });
});

$(function () {
    window.addEventListener("load", function () {
        $('#divSourceDoc_toggler').click(function () {
            $('#divSourceDoc').toggleClass('open');
            return false;
        });
        $('#divOutDoc_toggler').click(function () {
            $('#divOutDoc').toggleClass('open');
            return false;
        });
        $('#divHistory_toggler').click(function () {
            $('#divHistory').toggleClass('open');
            return false;
        });
    });
});

function adjustDockScroll() {
    var hh = $(window).height();
    var wh = $(window).width();
    $('#divSourceDoc_content').css('max-height', hh - 155);
    $('#divSourceDoc_content').css('height', hh - 155);
    if (wh > 700) {
        $('#divSourceDoc').css('width', 620);
        $('#divSourceDoc').css('right', -620);
    }
    else {
        $('#divSourceDoc').css('width', wh - 80);
        $('#divSourceDoc').css('right', -(wh - 80));
    }

    $('#divOutDoc_content').css('max-height', hh - 155);
    $('#divOutDoc_content').css('height', hh - 155);
    if (wh > 700) {
        $('#divOutDoc').css('width', 610);
        $('#divOutDoc').css('right', -610);
    }
    else {
        $('#divOutDoc').css('width', wh - 90);
        $('#divOutDoc').css('right', -(wh - 90));
    }

    $('#divHistory_content').css('max-height', hh - 155);
    $('#divHistory_content').css('height', hh - 155);
    if (wh > 700) {
        $('#divHistory').css('width', 600);
        $('#divHistory').css('right', -600);
    }
    else {
        $('#divHistory').css('width', wh - 100);
        $('#divHistory').css('right', -(wh - 100));
    }
}