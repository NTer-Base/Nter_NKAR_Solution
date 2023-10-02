$(document).ready(function () {
    //$('.dataTable>thead>tr>th').each(function () {
    //    alert('aaa');
    //});
    $('#divLoading').addClass('hide');    
});

$(function () {
    $(window).on('load', function () {
        ArrangeDatePickers();
        ArrangeTypable();
        ArrangeTypable2();
        ArrangeFileUploads();
        $('.nav-tabs a').on('shown.bs.tab', function () {
            AdjustGridResp();
        });
        AdjustPopupSize(50, 300, 'at_wait_model');
    });

    $(window).on('resize', function () {
        AdjustGridResp();
        AdjustPopupSize(50, 300, 'at_wait_model');
    });
});

$('input').on("wheel", function (e) {
    $(this).blur();
});

$(function () {
    $('.checkboxlist input').switcher({
        on_state_content: '<span class="fa fa-check"></span>',
        off_state_content: '<span class="fa fa-times"></span>'
    });
});

function AdjustGridResp() {
    $(".table-responsive").each(function () {
        $(this).removeClass('responsive_scroll');
        $(this).removeClass('responsive_border');

        if ($(this).children().children('table').data('size') == 'full_width_table' && $(this).children().children('table').hasClass('full_width_table') == false) {
            $(this).children().children('table').addClass('full_width_table');
        }

        if ($(this).children().children('table').data('size') == 'non_full_width_table' && $(this).children().children('table').hasClass('non_full_width_table') == false) {
            $(this).children().children('table').addClass('non_full_width_table');
        }

        if ($(this).children().children().children('table').data('size') == 'full_width_table' && $(this).children().children().children('table').hasClass('full_width_table') == false) {
            $(this).children().children().children('table').addClass('full_width_table');
        }

        if ($(this).children().children().children('table').data('size') == 'non_full_width_table' && $(this).children().children().children('table').hasClass('non_full_width_table') == false) {
            $(this).children().children().children('table').addClass('non_full_width_table');
        }

        if ($(this).prop('scrollWidth') - 2 > $(this).prop('clientWidth')) {
            $(this).addClass('responsive_scroll');
            $(this).children().children('table').removeClass('full_width_table');
            $(this).children().children('table').removeClass('non_full_width_table');
            $(this).children().children().children('table').removeClass('full_width_table');
            $(this).children().children().children('table').removeClass('non_full_width_table');
        }
        else if ($(this).prop('scrollWidth') > $(this).prop('clientWidth')) {
            $(this).addClass('responsive_border');
        }
    });
}

function AdjustPopupSize(popupHeight, popupWidth, popupName) {
    var h = $(window).height();
    var w = $(window).width();

    if (popupWidth > w - 20) {
        $("#" + popupName + "_indent").css('width', w - 20);
        $("#" + popupName + "_indent").css('margin-left', -((w - 20) / 2));
        $("#" + popupName + "_inner_indent").css('width', w - 20);
    }
    else {
        $("#" + popupName + "_indent").css('width', popupWidth);
        $("#" + popupName + "_indent").css('margin-left', -(popupWidth / 2));
        $("#" + popupName + "_inner_indent").css('width', popupWidth);
    }

    if (popupHeight > h - 140) {
        $("#" + popupName + "_content").css('height', h - 140);
        $("#" + popupName + "_content").css('max-height', 'initial');
    }
    else {
        $("#" + popupName + "_content").css('max-height', h - 140);
        $("#" + popupName + "_content").css('height', 'initial');
    }
}

function AdjustPopupSizeSpecial(popupHeight, popupWidth, extra_margin, popupName) {
    var h = $(window).height();
    var w = $(window).width();

    if (popupWidth > w - 20) {
        $("#" + popupName + "_indent").css('width', w - 20);
        $("#" + popupName + "_indent").css('margin-left', -((w - 20) / 2));
        $("#" + popupName + "_inner_indent").css('width', w - 20);
    }
    else {
        $("#" + popupName + "_indent").css('width', popupWidth);
        $("#" + popupName + "_indent").css('margin-left', -(popupWidth / 2));
        $("#" + popupName + "_inner_indent").css('width', popupWidth);
    }

    if (popupHeight > h - (140 + extra_margin)) {
        $("#" + popupName + "_content").css('height', h - (140 + extra_margin));
        $("#" + popupName + "_content").css('max-height', 'initial');
    }
    else {
        $("#" + popupName + "_content").css('max-height', h - (140 + extra_margin));
        $("#" + popupName + "_content").css('height', 'initial');
    }
}

function ArrangeDatePickers() {
    var options = {
        format: nter_dateformat,
        autoclose: true,
        todayHighlight: true
    };
    $('.dtPicker').datepicker(options);
    $(".dtPicker").mask("99/99/9999");

    $(".dtMask").mask("99/99/9999");

    var options2 = {
        minuteStep: 1,
        showSeconds: false,
        showMeridian: false,
        showInputs: false,
        orientation: $('body').hasClass('right-to-left') ? { x: 'right', y: 'auto' } : { x: 'auto', y: 'auto' }
    };
    $('.tmPicker').timepicker(options2);
    $(".tmPicker").mask("99:99");

    $(".tmMask").mask("99:99");
}

function ArrangeTypable() {
    $(".typable").each(function () {
        $(this).select2({
            allowClear: true,
            placeholder: "Select an Item"
        });
    });
    $('.typable').on('select2-opening', function (e) {
        $(".datepicker-dropdown").remove();
    });
}

function ArrangeTypable2() {
    $(".typable2").each(function () {
        $(this).select2({
            allowClear: true,
            placeholder: "Select an Item",
            dropdownCssClass: "select2-typable2"
        });
    });
    $('.typable2').on('select2-opening', function (e) {
        $(".datepicker-dropdown").remove();
    });
    $(document).click(function () {
        $(".typable2").select2('close');
    });
}

function ArrangeFileUploads() {
    $('.st_file_upload').pixelFileInput({ placeholder: 'No file selected...' });
}

function onPleaseWait() {
    $find('mpuPleaseWait').show();
    return false;
}

function offPleaseWait() {
    $find('mpuPleaseWait').hide();
    return false;
}

function GetGreeting() {
    var today = new Date();
    var Hours = today.getHours();
    if (Hours < 12) {
        return "Good Morning";
    }
    else if (Hours < 17) {
        return "Good Afternoon";
    }
    else {
        return "Good Evening";
    }
}

function LoadValuesFailed(result) {
    ShowError('Error in Retrieving Data, Please Restart the Application');
    offPleaseWait();
}

function openNewTab(pageName) {
    window.open(pageName, "_new");
    return false;
}

function openPage(pageName) {
    window.open(pageName, "_self");
    return false;
}

function ShowError(Message) {
    $.growl.error({ title: 'N-Ter-Tasks', message: Message });
}

function ShowSuccess(Message) {
    $.growl.notice({ title: 'N-Ter-Tasks', message: Message });
}

function ShowWarning(Message) {
    $.growl.warning({ title: 'N-Ter-Tasks', message: Message });
}

function onCheckBox(checkBox) {
    $(checkBox).prop('checked', true);
    $(checkBox).parent().addClass('checked');
}

function offCheckBox(checkBox) {
    $(checkBox).prop('checked', false);
    $(checkBox).parent().removeClass('checked');
}

function enableCheckBox(checkBox) {
    $(checkBox).prop('disabled', false);
    $(checkBox).switcher('enable');
}

function disableCheckBox(checkBox) {
    $(checkBox).prop('disabled', true);
    $(checkBox).switcher('disable');
}

function clearCheckBoxRange(checkboxClass) {
    $("." + checkboxClass).each(function () {
        $(this).children().children('input').each(function () {
            offCheckBox(this);
        });
    });
}

function selectCheckBoxRange(checkboxClass) {
    $("." + checkboxClass).each(function () {
        $(this).children().children('input').each(function () {
            onCheckBox(this);
        });
    });
}

function clearCheckListBox(ListContorols) {
    for (var i = 0; i < ListContorols.length; i++) {
        $('#' + ListContorols[i] + ' input').each(function () {
            offCheckBox(this);
        });
    }
}

function clearCheckBox(ListContorols) {
    for (i = 0; i < ListContorols.length; i++) {
        offCheckBox('#' + ListContorols[i]);
    }
}

function clearTextBox(TextContorols) {
    for (i = 0; i < TextContorols.length; i++) {
        $('#' + TextContorols[i]).val('');
    }
}

function clearTextBoxRange(TextBoxClass) {
    $("." + TextBoxClass).each(function () {
        $(this).val('');
    });
}

function clearLabel(LabelContorols) {
    for (i = 0; i < LabelContorols.length; i++) {
        $('#' + TextContorols[i]).text('');
    }
}

function clearFleUpload(FileUploadContorols) {
    for (i = 0; i < FileUploadContorols.length; i++) {
        $('#' + FileUploadContorols[i]).val('');
    }
}

function clearDateTextBox(TextContorols) {
    for (i = 0; i < TextContorols.length; i++) {
        $('#' + TextContorols[i]).val(formattedDate(Date()));
    }
}

function clearDropDown(ComboContorols) {
    for (i = 0; i < ComboContorols.length; i++) {
        $('#' + ComboContorols[i]).prop('selectedIndex', 0);
    }
}

function saveGridCheckValues(control) {
    if (control.$checkbox.parent().parent().attr('data-id') != undefined) {
        var selectedid = control.$checkbox.parent().parent().attr('data-id');
        var hnd = control.$checkbox.parent().parent().attr('data-hnd');
        var hndval = $("#" + hnd).val();
        if (hndval.startsWith(selectedid + '|')) {
            hndval = hndval.replace(selectedid + '|', '');
        } else if (hndval.endsWith('|' + selectedid)) {
            hndval = hndval.replace('|' + selectedid, '');
        } else if (hndval.indexOf('|' + selectedid + '|') > -1) {
            hndval = hndval.replace('|' + selectedid + '|', '|');
        } else {
            hndval = hndval + selectedid + '|';
        }
        $("#" + hnd).val(hndval);
    }
}

function filterInt(value) {
    if (/^(\-|\+)?([0-9]+|Infinity)$/.test(value))
        return Number(value);
    return NaN;
}

function formattedDate(date) {
    var d = new Date(date || Date.now()),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}

function isPositiveInteger(n) {
    return n >>> 0 === parseFloat(n);
}

function validatedate(inputText) {
    if (nter_dateformat == 'mm/dd/yyyy') {
        return validatedateType2(inputText);
    }
    else if (nter_dateformat == 'yyyy/mm/dd') {
        return validatedateType3(inputText);
    }
    else {
        return validatedateType1(inputText);
    }
}

function validatedateType1(inputText) {
    var ret = true;
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    if (inputText.match(dateformat)) {
        var pdate = inputText.split('/');
        if (pdate.length == 3) {
            var dd = parseInt(pdate[0]);
            var mm = parseInt(pdate[1]);
            var yy = parseInt(pdate[2]);
            var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (mm == 1 || mm > 2) {
                if (dd > ListofDays[mm - 1]) {
                    ret = false;
                }
            }
            if (mm == 2) {
                var lyear = false;
                if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                    lyear = true;
                }
                if ((lyear == false) && (dd >= 29)) {
                    ret = false;
                }
                if ((lyear == true) && (dd > 29)) {
                    ret = false;
                }
            }
        }
        else {
            ret = false;
        }
    }
    else {
        ret = false;
    }
    return ret;
}

function validatedateType2(inputText) {
    var ret = true;
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    if (inputText.match(dateformat)) {
        var pdate = inputText.split('/');
        if (pdate.length == 3) {
            var dd = parseInt(pdate[1]);
            var mm = parseInt(pdate[0]);
            var yy = parseInt(pdate[2]);
            var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (mm == 1 || mm > 2) {
                if (dd > ListofDays[mm - 1]) {
                    ret = false;
                }
            }
            if (mm == 2) {
                var lyear = false;
                if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                    lyear = true;
                }
                if ((lyear == false) && (dd >= 29)) {
                    ret = false;
                }
                if ((lyear == true) && (dd > 29)) {
                    ret = false;
                }
            }
        }
        else {
            ret = false;
        }
    }
    else {
        ret = false;
    }
    return ret;
}

function validatedateType3(inputText) {
    var ret = true;
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    if (inputText.match(dateformat)) {
        var pdate = inputText.split('/');
        if (pdate.length == 3) {
            var dd = parseInt(pdate[2]);
            var mm = parseInt(pdate[1]);
            var yy = parseInt(pdate[0]);
            var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (mm == 1 || mm > 2) {
                if (dd > ListofDays[mm - 1]) {
                    ret = false;
                }
            }
            if (mm == 2) {
                var lyear = false;
                if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                    lyear = true;
                }
                if ((lyear == false) && (dd >= 29)) {
                    ret = false;
                }
                if ((lyear == true) && (dd > 29)) {
                    ret = false;
                }
            }
        }
        else {
            ret = false;
        }
    }
    else {
        ret = false;
    }
    return ret;
}

function validateTime(inputText) {
    re = /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/;
    if (inputText.match(re)) {
        return true;
    }
    else {
        return false;
    }
}

function round(value, decimals) {
    return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
}

function printPageArea(frameTitle, height, width, areaID) {
    var printContent = document.getElementById(areaID);
    var WinPrint = window.open('abcd', '', 'width=' + width + ',height=' + height);
    WinPrint.document.write('<html><head><title>' + frameTitle + '</title></head><body>' + printContent.outerHTML + '</body></html>');
    WinPrint.document.close();
    WinPrint.focus();
    return false;
}

function remove_field_erros() {
    $('.req_error').removeClass('req_error');
}