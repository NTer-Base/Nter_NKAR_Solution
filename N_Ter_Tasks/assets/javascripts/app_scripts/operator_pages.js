function SelectOperators(fieldType, operator) {
    var Field_ID = $('#' + fieldType).val().trim().split('_')[0];
    if (Field_ID == "-1" || Field_ID == "-2") {
        $('#' + operator).children("option[value = '1']").hide();
        $('#' + operator).children("option[value = '2']").hide();
        $('#' + operator).children("option[value = '4']").hide();
        $('#' + operator).children("option[value = '5']").hide();
        $('#' + operator).val('3');
    }
    else {
        var FieldType = $('#' + fieldType).val().trim().split('_')[1];
        if (FieldType == '1' || FieldType == '2' || FieldType == '3' || FieldType == '5' || FieldType == '10' || FieldType == '13') {
            $('#' + operator).children("option[value = '1']").hide();
            $('#' + operator).children("option[value = '2']").hide();
            $('#' + operator).children("option[value = '4']").hide();
            $('#' + operator).children("option[value = '5']").hide();
            $('#' + operator).val('3');
        }
        else {
            $('#' + operator).children("option[value = '1']").show();
            $('#' + operator).children("option[value = '2']").show();
            $('#' + operator).children("option[value = '4']").show();
            $('#' + operator).children("option[value = '5']").show();
            $('#' + operator).val('1');
        }
    }
}

function CheckConditions(ctrl_condition, ctrl_field, ctrl_operator, ctrl_criteria, crtl_display, ctrl_drp, ctrl_date, display_text, reload_drps) {
    if ($('#' + ctrl_condition).prop('checked') == true) {
        $('#' + crtl_display).removeClass('hide');
    }
    else {
        $('#' + crtl_display).addClass('hide');
        clearDropDown([ctrl_field]);
    }
    SelectOperators(ctrl_field, ctrl_operator);
    DisplayConditionText(ctrl_field, ctrl_criteria, ctrl_drp, ctrl_date, display_text, reload_drps);
}

function InitialteCondControls(ctrl_text, dateCSS, ctrl_drp, ctrl_date, ctrl_field) {
    var options = {
        format: nter_dateformat,
        autoclose: true,
        todayHighlight: true
    };
    $('.' + dateCSS).datepicker(options).on("changeDate", function () {
        $('#' + ctrl_text).val($('#' + ctrl_date).val());
    });
    $('#' + ctrl_date).keyup(function () {
        $('#' + ctrl_text).val($('#' + ctrl_date).val());
    });
    $('#' + ctrl_drp).change(function () {
        $('#' + ctrl_text).val($("#" + ctrl_drp + " option:selected").text());
    });
    $('#' + ctrl_field).change(function () {
        DisplayConditionText(ctrl_field, ctrl_text, ctrl_drp, ctrl_date, "", true);
    });
}

function DisplayConditionText(ctrl_field, ctrl_text, ctrl_drp, ctrl_date, display_text, reload_drps) {
    var fieldType = $('#' + ctrl_field).val().trim().split('_')[1];

    var fieldID = $('#' + ctrl_field).val().trim().split('_')[0];
    if (fieldID == "-1" || fieldID == "-2") {
        if (fieldType == "1") {
            fieldType = "2";
        }
        else if (fieldType == "2" || fieldType == "3") {
            fieldType = "1";
        }
    }

    if (fieldType == "6") {
        $('#' + ctrl_text).addClass('hide');
        $('#' + ctrl_drp).addClass('hide');
        $('#' + ctrl_date).removeClass('hide');

        if (display_text.trim() == "") {
            clearDateTextBox([ctrl_date]);
        }
        else {
            $('#' + ctrl_date).val(display_text);
        }
        $('#' + ctrl_text).val($('#' + ctrl_date).val());
    }
    else if (fieldType == "2" || fieldType == "3" || fieldType == "4" || fieldType == "8" || fieldType == "9") {
        $('#' + ctrl_text).removeClass('hide');
        $('#' + ctrl_drp).addClass('hide');
        $('#' + ctrl_date).addClass('hide');

        $('#' + ctrl_text).val(display_text);
    }
    else if (fieldType == "1" || fieldType == "5" || fieldType == "7" || fieldType == "10" || fieldType == "12" || fieldType == "13") {
        $('#' + ctrl_text).addClass('hide');
        $('#' + ctrl_drp).removeClass('hide');
        $('#' + ctrl_date).addClass('hide');

        $('#' + ctrl_drp).empty();
        if (reload_drps) {
            $.ajax({
                type: "GET",
                async: true,
                url: "api/tasks/GetFieldData",
                data: { Field_Data: $('#' + ctrl_field).val() },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus, jQxhr) {
                    for (var i = 0; i < data.length; i++) {
                        $('#' + ctrl_drp).append(`<option value="${data[i].Item_ID}">${data[i].Item_Name}</option>`);
                    }
                    if (display_text.trim() == "") {
                        clearDropDown([ctrl_drp]);
                    }
                    else {
                        $("#" + ctrl_drp + " > option").each(function () {
                            if ($(this).html().trim() == display_text.trim()) {
                                $('#' + ctrl_drp).val($(this).val());
                            }
                        });
                    }
                    $('#' + ctrl_text).val($("#" + ctrl_drp + " option:selected").text());
                },
                failure: function (jqXhr, textStatus, errorThrown) { }
            });
        }
    }
}