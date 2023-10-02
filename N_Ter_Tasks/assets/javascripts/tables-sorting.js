jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "de_datetime-asc": function (a, b) {
        var x, y;
        if (jQuery.trim(a) !== '') {
            var deDatea = jQuery.trim(a).split(' ');
            var deTimea = deDatea[1].split(':');
            var deDatea2 = deDatea[0].split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[2] + deDatea2[0] + deDatea2[1] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[2] + deDatea2[0] + deDatea2[1] + deTimea[0] + deTimea[1]) * 1;
                }
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[0] + deDatea2[1] + deDatea2[2] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[0] + deDatea2[1] + deDatea2[2] + deTimea[0] + deTimea[1]) * 1;
                }
            }
            else {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[2] + deDatea2[1] + deDatea2[0] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[2] + deDatea2[1] + deDatea2[0] + deTimea[0] + deTimea[1]) * 1;
                }
            }
        } else {
            x = -Infinity; // = l'an 1000 ...
        }

        if (jQuery.trim(b) !== '') {
            var deDateb = jQuery.trim(b).split(' ');
            var deTimeb = deDateb[1].split(':');
            var deDateb2 = deDateb[0].split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[2] + deDateb2[0] + deDateb2[1] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[2] + deDateb2[0] + deDateb2[1] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[0] + deDateb2[1] + deDateb2[2] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[0] + deDateb2[1] + deDateb2[2] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
            else {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[2] + deDateb2[1] + deDateb2[0] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[2] + deDateb2[1] + deDateb2[0] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
        } else {
            y = -Infinity;
        }
        var z = ((x < y) ? -1 : ((x > y) ? 1 : 0));
        return z;
    },

    "de_datetime-desc": function (a, b) {
        var x, y;
        if (jQuery.trim(a) !== '') {
            var deDatea = jQuery.trim(a).split(' ');
            var deTimea = deDatea[1].split(':');
            var deDatea2 = deDatea[0].split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[2] + deDatea2[0] + deDatea2[1] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[2] + deDatea2[0] + deDatea2[1] + deTimea[0] + deTimea[1]) * 1;
                }
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[0] + deDatea2[1] + deDatea2[2] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[0] + deDatea2[1] + deDatea2[2] + deTimea[0] + deTimea[1]) * 1;
                }
            }
            else {
                if (typeof deTimea[2] != 'undefined') {
                    x = (deDatea2[2] + deDatea2[1] + deDatea2[0] + deTimea[0] + deTimea[1] + deTimea[2]) * 1;
                } else {
                    x = (deDatea2[2] + deDatea2[1] + deDatea2[0] + deTimea[0] + deTimea[1]) * 1;
                }
            }
        } else {
            x = Infinity;
        }

        if (jQuery.trim(b) !== '') {
            var deDateb = jQuery.trim(b).split(' ');
            var deTimeb = deDateb[1].split(':');
            var deDateb2 = deDateb[0].split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[2] + deDateb2[0] + deDateb2[1] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[2] + deDateb2[0] + deDateb2[1] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[0] + deDateb2[1] + deDateb2[2] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[0] + deDateb2[1] + deDateb2[2] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
            else {
                if (typeof deTimeb[2] != 'undefined') {
                    y = (deDateb2[2] + deDateb2[1] + deDateb2[0] + deTimeb[0] + deTimeb[1] + deTimeb[2]) * 1;
                } else {
                    y = (deDateb2[2] + deDateb2[1] + deDateb2[0] + deTimeb[0] + deTimeb[1]) * 1;
                }
            }
        } else {
            y = -Infinity;
        }
        var z = ((x < y) ? 1 : ((x > y) ? -1 : 0));
        return z;
    },

    "de_date-asc": function (a, b) {
        var x, y;
        if (jQuery.trim(a) !== '') {
            var deDatea = jQuery.trim(a).split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                x = (deDatea[2] + deDatea[0] + deDatea[1]) * 1;
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                x = (deDatea[0] + deDatea[1] + deDatea[2]) * 1;
            }
            else {
                x = (deDatea[2] + deDatea[1] + deDatea[0]) * 1;
            }
        } else {
            x = Infinity; // = l'an 1000 ...
        }

        if (jQuery.trim(b) !== '') {
            var deDateb = jQuery.trim(b).split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                y = (deDateb[2] + deDateb[0] + deDateb[1]) * 1;
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                y = (deDateb[0] + deDateb[1] + deDateb[2]) * 1;
            }
            else {
                y = (deDateb[2] + deDateb[1] + deDateb[0]) * 1;
            }
        } else {
            y = -Infinity;
        }
        var z = ((x < y) ? -1 : ((x > y) ? 1 : 0));
        return z;
    },

    "de_date-desc": function (a, b) {
        var x, y;
        if (jQuery.trim(a) !== '') {
            var deDatea = jQuery.trim(a).split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                x = (deDatea[2] + deDatea[0] + deDatea[1]) * 1;
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                x = (deDatea[0] + deDatea[1] + deDatea[2]) * 1;
            }
            else {
                x = (deDatea[2] + deDatea[1] + deDatea[0]) * 1;
            }
        } else {
            x = -Infinity;
        }

        if (jQuery.trim(b) !== '') {
            var deDateb = jQuery.trim(b).split('/');
            if (nter_dateformat == 'mm/dd/yyyy') {
                y = (deDateb[2] + deDateb[0] + deDateb[1]) * 1;
            }
            else if (nter_dateformat == "yyyy/mm/dd") {
                y = (deDateb[0] + deDateb[1] + deDateb[2]) * 1;
            }
            else {
                y = (deDateb[2] + deDateb[1] + deDateb[0]) * 1;
            }
        } else {
            y = Infinity;
        }
        var z = ((x < y) ? 1 : ((x > y) ? -1 : 0));
        return z;
    }
});

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "currency-pre": function (a) {
        a = ((a === "-") || (a === "&nbsp;")) ? 0 : a.replace(/[^\d\-\.]/g, "");
        return parseFloat(a);
    },

    "currency-asc": function (a, b) {
        return a - b;
    },

    "currency-desc": function (a, b) {
        return b - a;
    }
});