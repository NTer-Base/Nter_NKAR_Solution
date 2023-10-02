$(function () {
    $(window).on('load', function () {
        adjustHelpScroll();
    });

    $(window).on('resize', function () {
        adjustHelpScroll();
    });
});

$(function () {
    window.addEventListener("load", function () {
        $('#help_panel_toggler').click(function () {
            $('#help_panel').toggleClass('open');
            return false;
        });
    });
});

var slsr;

function adjustHelpScroll() {
    if (slsr != null) {
        $('#helpcontent').slimScroll({ destroy: true });
    }

    var hh = $(window).height();
    $('#helpcontent').css('height', hh - 150);
    if ($('#helpcontent').prop('scrollHeight') > $('#helpcontent').prop('clientHeight')) {
        slsr = $('#helpcontent').slimScroll({ height: hh - 150 });
    }
    else {
        $('#helpcontent').css('height', 'auto');
    }
    var wh = $(window).width();
    if (wh > 700) {
        $('#help_panel').css('width', 600);
        $('#help_panel').css('right', -600);
    }
    else {
        $('#help_panel').css('width', wh - 100);
        $('#help_panel').css('right', -(wh - 100));
    }
}