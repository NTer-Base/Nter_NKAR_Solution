function ValidateImageUpload(fileUploadName) {
    var FileUploadPath = $("#" + fileUploadName).val();

    if (FileUploadPath.trim() == '') {
        ShowError('Please select an image to upload');
        return false;
    }
    else {
        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
        if (Extension == "jpg" || Extension == "jpeg" || Extension == "gif" || Extension == "bmp" || Extension == "png") {
            return true;
        }
        else {
            ShowError('The file format is not compatible');
            return false;
        }
    }
}

function ValidateFileUpload(fileUploadName) {
    var FileUploadPath = $("#" + fileUploadName).val();

    if (FileUploadPath.trim() == '') {
        ShowError('Please select a file to upload');
        return false;
    }
    else {
        return true;
    }
}

function ValidateReplaceFile(uploadType, fileSource, fileResult, fileUpload) {
    var ret = true;
    if ($("#" + uploadType).val().trim() == '0') {
        if ($("#" + fileSource).val().trim() == '0') {
            ShowError('Please Select a file to Replace');
            ret = false;
        }
    }
    else {
        if ($("#" + fileResult).val().trim() == '0') {
            ShowError('Please Select a file to Replace');
            ret = false;
        }
    }
    if ($("#" + fileUpload).val().trim() == '') {
        ShowError('Please select a file to upload');
        ret = false;
    }
    return ret;
}

function ValidateExtraFieldUpdate(extraField) {
    if ($("#" + extraField).val().trim() == '') {
        ShowError('Field Values are Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateExtraField2Update(extraField) {
    if ($("#" + extraField2).val().trim() == '') {
        ShowError('Field Values are Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateDueDate(dueDate) {
    var ret = true;
    if (validatedate($("#" + dueDate).val().trim()) == false) {
        ShowError('Invalid Due Date');
        ret = false;
    }
    return ret;
}

function ValidateStartTask(workflowID, companyID, deadlineType, scheduleID, dueDate, el2Name) {
    var ret = true;
    if ($('#' + workflowID).val().trim() == '') {
        ShowError('Invalid Workflow Selection');
        ret = false;
    }
    else if (Number($('#' + workflowID).val().trim().split('|')[0]) <= 0) {
        ShowError('Invalid Workflow Selection');
        ret = false;
    }
    if ($('#' + companyID).val().trim() == '') {
        ShowError('Invalid ' + el2Name + ' Selection');
        ret = false;
    }
    else if (Number($('#' + companyID).val().trim()) <= 0) {
        ShowError('Invalid ' + el2Name + ' Selection');
        ret = false;
    }
    if (validatedate($("#" + dueDate).val().trim()) == false) {
        ShowError('Invalid Due Date');
        ret = false;
    }
    else {
        if ($('#' + deadlineType).val().trim() == '1') {
            var txtSelectedDate = $("#" + dueDate).val().trim().split('/');
            var dtSelectedDate;
            if (nter_dateformat == 'mm/dd/yyyy') {
                dtSelectedDate = new Date(Number(txtSelectedDate[2]), Number(txtSelectedDate[0]) - 1, Number(txtSelectedDate[1]));
            }
            else if (nter_dateformat == 'yyyy/mm/dd') {
                dtSelectedDate = new Date(Number(txtSelectedDate[0]), Number(txtSelectedDate[1]) - 1, Number(txtSelectedDate[2]));
            }
            else {
                dtSelectedDate = new Date(Number(txtSelectedDate[2]), Number(txtSelectedDate[1]) - 1, Number(txtSelectedDate[0]));
            }
            var dtToday = new Date();
            var dtTodayDate = new Date(dtToday.getFullYear(), dtToday.getMonth(), dtToday.getDate());
            if (dtSelectedDate < dtTodayDate) {
                ShowError('Invalid Date Selection - Past Date');
                ret = false;
            }
        }
        else if ($('#' + deadlineType).val().trim() == '2') {
            if ($('#' + scheduleID).val().trim() == '') {
                ShowError('Invalid Schedule Selection');
                ret = false;
            }
            else if (Number($('#' + scheduleID).val().trim()) <= 0) {
                ShowError('Invalid Schedule Selection');
                ret = false;
            }
        }
    }
    return ret;
}

function ValidateStartTaskGuest(deadlineType, scheduleID, dueDate) {
    var ret = true;
    if (deadlineType.trim() == '1') {
        if (validatedate($("#" + dueDate).val().trim()) == false) {
            ShowError('Invalid Due Date');
            ret = false;
        }
        else {
            var txtSelectedDate = $("#" + dueDate).val().trim().split('/');
            var dtSelectedDate;
            if (nter_dateformat == 'mm/dd/yyyy') {
                dtSelectedDate = new Date(Number(txtSelectedDate[2]), Number(txtSelectedDate[0]) - 1, Number(txtSelectedDate[1]));
            }
            else if (nter_dateformat == 'yyyy/mm/dd') {
                dtSelectedDate = new Date(Number(txtSelectedDate[0]), Number(txtSelectedDate[1]) - 1, Number(txtSelectedDate[2]));
            }
            else {
                dtSelectedDate = new Date(Number(txtSelectedDate[2]), Number(txtSelectedDate[1]) - 1, Number(txtSelectedDate[0]));
            }
            var dtToday = new Date();
            var dtTodayDate = new Date(dtToday.getFullYear(), dtToday.getMonth(), dtToday.getDate());
            if (dtSelectedDate < dtTodayDate) {
                ShowError('Invalid Date Selection - Past Date');
                ret = false;
            }
        }
    }
    else if (deadlineType.trim() == '2') {
        if ($('#' + scheduleID).val().trim() == '') {
            ShowError('Invalid Schedule Selection');
            ret = false;
        }
        else if (Number($('#' + scheduleID).val().trim()) <= 0) {
            ShowError('Invalid Schedule Selection');
            ret = false;
        }
    }
    return ret;
}

function ValidateComment(commentText) {
    if ($("#" + commentText).val().trim() == '') {
        ShowError('Comment is Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateUserGroup(userGroupName) {
    if ($("#" + userGroupName).val().trim() == '') {
        ShowError('User Group Name is Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateEL2(DisplayName, LegalName, EmailAddress, FolderName, EL2Code, EL1ID, EL2ID, ParentID, Validate_Simillar_EL2) {
    var Validated = true;
    onPleaseWait();
    if ($("#" + EL2Code).val().trim() == '') {
        ShowError('Entity Code is Required');
        Validated = false;
    }
    else if (EL2CodeExist($("#" + EL2Code).val().trim(), $("#" + EL2ID).val().trim()) == true) {
        ShowError('Entity Code already exist');
        Validated = false;
    }
    if ($("#" + FolderName).val().trim() == '') {
        ShowError('Folder Name is Required');
        Validated = false;
    }
    else if (EL2FolderExist($("#" + FolderName).val().trim(), $("#" + EL2ID).val().trim()) == true) {
        ShowError('Folder Name already exist');
        Validated = false;
    }
    if ($("#" + DisplayName).val().trim() == '') {
        ShowError('Display Name is Required');
        Validated = false;
    }
    if ($("#" + LegalName).val().trim() == '') {
        ShowError('Legal Name is Required');
        Validated = false;
    }
    if ($("#" + EmailAddress).val().trim() == '') {
        ShowError('E-Mail Address is Required');
        Validated = false;
    }
    else if ($("#" + EmailAddress).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + EmailAddress).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    if ($("#" + EL1ID).val().trim() == '0') {
        ShowError('Main Entity is Required');
        Validated = false;
    }
    if ($("#" + EL2ID).val().trim() != "0" && $("#" + EL2ID).val().trim() == $("#" + ParentID).val().trim()) {
        ShowError('Current Element and Parent cannot be the same');
        Validated = false;
    }
    if (Validated == true && Validate_Simillar_EL2 == true) {
        if (EL2SimilarNameExist($("#" + DisplayName).val().trim(), $("#" + LegalName).val().trim(), $("#" + EL2ID).val().trim()) == true) {
            Validated = false;
        }
    }
    else {
        offPleaseWait();
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function EL2FolderExist(foldername, EL2id) {
    var hasCode = '0';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/EL2FolderodeExist",
        data: { FolderName: foldername, Entity_L2_ID: EL2id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            hasCode = data;
        },
        failure: function (jqXhr, textStatus, errorThrown) {

        }
    });
    return (parseInt(hasCode) > 0);
}

function EL2CodeExist(EL2code, EL2id) {
    var hasCode = '0';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/EL2CodeExist",
        data: { EL2Code: EL2code, Entity_L2_ID: EL2id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            hasCode = data;
        },
        failure: function (jqXhr, textStatus, errorThrown) {

        }
    });
    return (parseInt(hasCode) > 0);
}

function EL2SimilarNameExist(displayName, legalName, EL2id) {
    $('#cboSimilarEL2').empty();
    var SimilarName = '';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/EL2SimilarNameExist",
        data: { DiaplayName: displayName, LegalName: legalName, Entity_L2_ID: EL2id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            offPleaseWait();
            SimilarName = JSON.stringify(data);
            if (JSON.stringify(data).trim() != "") {
                var resultObject = data;
                for (var i = 0; i < resultObject.length; i++) {
                    $('#cboSimilarEL2').append(
                        $('<option>', {
                            value: resultObject[i].Entity_L2_ID.trim(),
                            text: resultObject[i].Display_Name.trim()
                        }, '<option/>'))
                }
                $find('mpuSimilarName').show();
            }
        },
        failure: function (jqXhr, textStatus, errorThrown) {
            offPleaseWait();
        }
    });
    if (SimilarName.trim() == "") {
        return false;
    }
    else {
        return true;
    }
}

function ValidateEL1(DisplayName, EL2Name, EmailAddress) {
    var Validated = true;

    if ($("#" + EL2Name).val().trim() == '') {
        ShowError('Entity Name is Required');
        Validated = false;
    }
    if ($("#" + DisplayName).val().trim() == '') {
        ShowError('Display Name is Required');
        Validated = false;
    }
    if ($("#" + EmailAddress).val().trim() == '') {
        ShowError('E-Mail Address is Required');
        Validated = false;
    }
    else if ($("#" + EmailAddress).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + EmailAddress).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateWorkflow(WorkflowName, DeadlineType, NumberOfDays, ScheduleID, WorkflowCat) {
    var Validated = true;
    if ($("#" + WorkflowName).val().trim() == '') {
        ShowError('Workflow Name is Required');
        Validated = false;
    }
    if ($("#" + WorkflowCat).val().trim() == '0') {
        ShowError('Workflow Category is Required');
        Validated = false;
    }
    if ($("#" + DeadlineType).val().trim() == '2' && $("#" + ScheduleID).val().trim() == '0') {
        ShowError('Please Select a Schedule');
        Validated = false;
    }
    if ($("#" + DeadlineType).val().trim() == '3') {
        if (isPositiveInteger($("#" + NumberOfDays).val().trim()) == false) {
            ShowError('Invalid Number of Days to Complete a Task');
            Validated = false;
        }
        else if (Number($("#" + NumberOfDays).val().trim()) <= 0) {
            ShowError('Invalid Number of Days to Complete a Task');
            Validated = false;
        }
    }
    return Validated;
}

function ValidateDocProject(docProjectName) {
    if ($("#" + docProjectName).val().trim() == '') {
        ShowError('Document Project Name is Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateWorkFlowFieldCat(catName) {
    if ($("#" + catName).val().trim() == '') {
        ShowError('Workflow Step Field Category is Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateWorkflowSchedules(ScheduleName, ScheduleEntityName, ScheduleEntityDateName) {
    var Validated = true;
    if ($("#" + ScheduleName).val().trim() == '') {
        ShowError('Schedule Name is Required');
        Validated = false;
    }
    else if ($("#" + ScheduleEntityName).val().trim() == '') {
        ShowError('Schedule Entity Name is Required');
        Validated = false;
    }
    else if ($("#" + ScheduleEntityDateName).val().trim() == '') {
        ShowError('Schedule Entity Date Name is Required');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateWorkflowScheduleData(EntityName, EntityDate, EntityTime) {
    var Validated = true;
    if ($("#" + EntityName).val().trim() == '') {
        ShowError('Entity Name is Required');
        Validated = false;
    }
    else if ($("#" + EntityDate).val().trim() == '') {
        ShowError('Date is Required');
        Validated = false;
    }
    else if ($("#" + EntityTime).val().trim() == '') {
        ShowError('Time is Required');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateWorkflowStep(WorkflowName, Step_Weight) {
    var ret = true;
    if ($("#" + Step_Weight).val().trim() == '') {
        ShowError('Step Waight is Required');
        ret = false;
    }
    else if (isPositiveInteger($("#" + Step_Weight).val().trim()) == false) {
        ShowError('Invalid Step Weight');
        ret = false;
    }
    if ($("#" + WorkflowName).val().trim() == '') {
        ShowError('Workflow Step Name is Required');
        ret = false;
    }
    return ret;
}

function ValidateWorkFlowField(WorkflowFieldName, fieldType, AutoSubmitStep, DefaultText, DefaultTextMemo, MasterTable) {
    var ret = true;
    if ($("#" + WorkflowFieldName).val().trim() == '') {
        ShowError('Workflow Step Field Name is Required');
        ret = false;
    }

    if ($("#" + fieldType).val().trim() == '10') {
        if ($("#" + MasterTable).val().trim() == '0') {
            ShowError('Master Table is Required');
            ret = false;
        }
    }
    else {
        $("#" + MasterTable).val('0');
    }

    if ($("#" + AutoSubmitStep).prop('checked') == true) {
        if ($("#" + fieldType).val().trim() != '2' || $("#" + DefaultText).val().trim() == '') {
            ShowError('This is an AutoSubmit Step and cannot add Fields with Selected Type');
            ret = false;
        }
    }
    if (ret == true) {
        if ($("#" + fieldType).val().trim() == '2')
        {
            $("#" + DefaultTextMemo).val($("#" + DefaultText).val());
        }
    }
    return ret;
}

function ValidateAddonField(FieldName, fieldType, MasterTable, DefaultText, DefaultTextMemo) {
    var ret = true;
    if ($("#" + FieldName).val().trim() == '') {
        ShowError('Addon Field Name is Required');
        ret = false;
    }

    if ($("#" + fieldType).val().trim() == '10') {
        if ($("#" + MasterTable).val().trim() == '0') {
            ShowError('Master Table is Required');
            ret = false;
        }
    }
    else {
        $("#" + MasterTable).val('0');
    }
    if (ret == true) {
        if ($("#" + fieldType).val().trim() == '2') {
            $("#" + DefaultTextMemo).val($("#" + DefaultText).val());
        }
    }
    return ret;
}

function ValidateAddon(AddonName) {
    var ret = true;
    if ($("#" + AddonName).val().trim() == '') {
        ShowError('Addon Name is Required');
        ret = false;
    }

    return ret;
}

function ValidateDocProjectTag(docProjectTagName) {
    var ret = true;
    if ($("#" + docProjectTagName).val().trim() == '') {
        ShowError('Document Repo Tag Name is Required');
        ret = false;
    }
    return ret;
}

function ValidateWorkFlowCondition(EvaluateWith) {
    if ($("#" + EvaluateWith).val().trim() == '') {
        ShowError('Evaluation Criteria is Required');
        return false;
    }
    else {
        return true;
    }
}

function ValidateWorkFlowFormula(f_Type, t1_Date_1, t1_Date_2, t2_Date_1, t2_Result, t3_Number_1, t3_Number_2, t3_Result) {
    var Validated = true;
    var field_Type = $("#" + f_Type).val();
    if (field_Type == "1") {
        if ($("#" + t1_Date_1).val().trim() == $("#" + t1_Date_2).val().trim()) {
            ShowError('Invalid Selection of Date Fields');
            Validated = false;
        }
    }
    else if (field_Type == "2") {
        if ($("#" + t2_Date_1).val().trim() == $("#" + t2_Result).val().trim()) {
            ShowError('Invalid Selection of Date Fields');
            Validated = false;
        }
    }
    else if (field_Type == "3") {
        if ($("#" + t3_Number_1).val().trim() == $("#" + t3_Number_2).val().trim()) {
            ShowError('Invalid Selection of Number Fields');
            Validated = false;
        }
        else if ($("#" + t3_Number_2).val().trim() == $("#" + t3_Result).val().trim()) {
            ShowError('Invalid Selection of Number Fields');
            Validated = false;
        }
        else if ($("#" + t3_Number_1).val().trim() == $("#" + t3_Result).val().trim()) {
            ShowError('Invalid Selection of Number Fields');
            Validated = false;
        }
    }
    return Validated;
}

function ValidateChangePW(currentPW, newPW, reNewPW) {
    var Validated = true;
    if ($("#" + currentPW).val().trim() == '') {
        ShowError('Current Password is Required');
        Validated = false;
    }
    if ($("#" + newPW).val().trim() == '') {
        ShowError('New Password is Required');
        Validated = false;
    }
    else if (ValidatePWPolicy($("#" + newPW).val().trim()) == false) {
        ShowError('Password does not match with Password Policy');
        Validated = false;
    }
    if ($("#" + reNewPW).val().trim() == '') {
        ShowError('Retype Password is Required');
        Validated = false;
    }
    else if ($("#" + reNewPW).val().trim() != $("#" + newPW).val().trim()) {
        ShowError('Retype Password does not Match');
        Validated = false;
    }
    return Validated;
}

function ValidatePWPolicy(password) {
    var Eligible = '0';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/ValidatePWPolicy",
        data: { EnteredPW: password },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            Eligible = data;
        },
        failure: function (jqXhr, textStatus, errorThrown) {  }
    });
    if (Eligible == '1') {
        return true;
    }
    else {
        return false;
    }
}

function ValidateUser(Username, Password, RePassword, FirstName, UserCode, hndUserID) {
    var Validated = true;
    onPleaseWait();
    var User_ID = $("#" + hndUserID).val();

    if ($("#" + Username).val().trim() == '') {
        ShowError('E-mail Address is Required');
        Validated = false;
    }
    else if ($("#" + Username).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + Username).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if (UserNameExist($("#" + Username).val().trim(), User_ID) == true) {
        ShowError('Username already exist');
        Validated = false;
    }
    if (User_ID == "0") {
        if ($("#" + Password).val().trim() == '') {
            ShowError('Password is Required');
            Validated = false;
        }
    }
    if ($("#" + Password).val().trim() != '') {
        if (ValidatePWPolicy($("#" + Password).val().trim()) == false) {
            ShowError('Password does not match with Password Policy');
            Validated = false;
        }
        else if ($("#" + Password).val().trim() != $("#" + RePassword).val().trim()) {
            ShowError('Missmatch in Password');
            Validated = false;
        }
    }
    if ($("#" + UserCode).val().trim() == '') {
        ShowError('User Code is Required');
        Validated = false;
    }
    else if (UserCodeExist($("#" + UserCode).val().trim(), User_ID) == true) {
        ShowError('User Code already exist');
        Validated = false;
    }
    if ($("#" + FirstName).val().trim() == '') {
        ShowError('First Name is Required');
        Validated = false;
    }
    offPleaseWait();
    return Validated;
}

function UserNameExist(username, userid) {
    var hasCode = '0';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/UserNameExist",
        data: { Username: username, User_ID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            hasCode = data;
        },
        failure: function (jqXhr, textStatus, errorThrown) {

        }
    });
    return (parseInt(hasCode) > 0);
}

function UserCodeExist(usercode, userid) {
    var hasCode = '0';
    $.ajax({
        type: "GET",
        async: false,
        url: "api/tasks/UserCodeExist",
        data: { UserCode: usercode, User_ID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus, jQxhr) {
            hasCode = data;
        },
        failure: function (jqXhr, textStatus, errorThrown) {

        }
    });
    return (parseInt(hasCode) > 0);
}

function ValidateMessage(ThreadID, MessageTitle, MessageBody) {
    var Validated = true;

    if ($("#" + ThreadID).val().trim() == "0") {
        if ($("#" + MessageTitle).val().trim() == '') {
            ShowError('Message Title is Required');
            Validated = false;
        }
    }
    if ($("#" + MessageBody).val().trim() == '') {
        ShowError('Message is Required');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateWF_Cat(CatName) {
    var Validated = true;
    if ($("#" + CatName).val().trim() == '') {
        ShowError('Workflow Category Name is Required');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateTaskMessage(ThreadID, MessageUser, MessageTitle, MessageBody) {
    var Validated = true;

    if ($("#" + ThreadID).val().trim() == "0") {
        if ($("#" + MessageTitle).val().trim() == '') {
            ShowError('Message Title is Required');
            Validated = false;
        }
        if ($("#" + MessageUser).val().trim() == '0') {
            ShowError('Message Receiver is Required');
            Validated = false;
        }
    }
    if ($("#" + MessageBody).val().trim() == '') {
        ShowError('Message is Required');
        Validated = false;
    }
    if (Validated == true) {
        return true;
    }
    else {
        return false;
    }
}

function ValidateDPEmailTemplate(EmailName, EmailBody, EmailAddress) {
    var Validated = true;

    if ($("#" + EmailName).val().trim() == '') {
        ShowError('E-Mail Template Name is Required');
        Validated = false;
    }
    if ($("#" + EmailBody).val().trim() == '') {
        ShowError('E-Mail Body is Required');
        Validated = false;
    }
    if ($("#" + EmailAddress).val().trim() == '') {
        ShowError('To Email Address is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFEmailTemplate(EmailName, EmailBody, EmailAddress, Workflow_Steps) {
    var Validated = true;

    if ($("#" + EmailName).val().trim() == '') {
        ShowError('E-Mail Template Name is Required');
        Validated = false;
    }
    if ($("#" + EmailBody).val().trim() == '') {
        ShowError('E-Mail Body is Required');
        Validated = false;
    }
    if ($("#" + EmailAddress).val().trim() == '') {
        ShowError('To Email Address is Required');
        Validated = false;
    }
    if ($("#" + Workflow_Steps + " input:checked").length == 0) {
        ShowError('Workflow Step is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFDocTemplate(DocName) {
    var Validated = true;

    if ($("#" + DocName).val().trim() == '') {
        ShowError('Document Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFFileTemplate(FileName) {
    var Validated = true;

    if ($("#" + FileName).val().trim() == '') {
        ShowError('File Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateDuplicateWF(WF_Name) {
    var Validated = true;

    if ($("#" + WF_Name).val().trim() == '') {
        ShowError('New Workflow Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateDuplicateStep(Step_Name) {
    var Validated = true;

    if ($("#" + Step_Name).val().trim() == '') {
        ShowError('New Step Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateMailbox(mailboxName, emailAddress, portNumber, sendTo, el2, workflow, noOfDays) {
    var Validated = true;

    if ($("#" + mailboxName).val().trim() == '') {
        ShowError('Mailbox Name is Required');
        Validated = false;
    }
    if ($("#" + emailAddress).val().trim() == '') {
        ShowError('E-mail Address is Required');
        Validated = false;
    }
    else if ($("#" + emailAddress).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + emailAddress).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    if (isPositiveInteger($("#" + portNumber).val().trim()) == false) {
        ShowError('Invalid Port Number');
        Validated = false;
    }
    if ($("#" + sendTo).val().trim() != '') {
        if ($("#" + sendTo).val().trim().split('@').length < 2) {
            ShowError('Invalid Send To Address');
            Validated = false;
        }
        else if ($("#" + sendTo).val().trim().split('@')[1].split('.').length < 2) {
            ShowError('Invalid Send To Address');
            Validated = false;
        }
    }
    if ($("#" + el2).val() != "0") {
        if ($("#" + workflow).val() == "0") {
            ShowError('Invalid Workflow Selection');
            Validated = false;
        }
        if ($("#" + noOfDays).val().trim() == "") {
            ShowError('Invalid No of Days to Complete');
            Validated = false;
        }
        else if (isPositiveInteger($("#" + noOfDays).val().trim()) == false) {
            ShowError('Invalid No of Days to Complete');
            Validated = false;
        }
        else if (Number($("#" + noOfDays).val().trim()) == 0) {
            ShowError('Invalid No of Days to Complete');
            Validated = false;
        }
    }
    return Validated;
}

function ValidateRule(NoOfDays) {
    var Validated = true;
    if ($("#" + NoOfDays).val().trim() == '') {
        ShowError('Days to Finish the Task is Required');
        Validated = false;
    }
    else if (isPositiveInteger($("#" + NoOfDays).val().trim()) == false) {
        ShowError('Invalid Days to Finish the Task');
        Validated = false;
    }
    return Validated;
}

function ValidateFolderRule(folderName, NoOfDays) {
    var Validated = true;
    if ($("#" + folderName).val().trim() == '0') {
        ShowError('Folder Name is Required');
        Validated = false;
    }
    if ($("#" + NoOfDays).val().trim() == '') {
        ShowError('Days to Finish the Task is Required');
        Validated = false;
    }
    else if (isPositiveInteger($("#" + NoOfDays).val().trim()) == false) {
        ShowError('Invalid Days to Finish the Task');
        Validated = false;
    }
    return Validated;
}

function ValidateWorkflowMilestone(msName, msWeight) {
    var Validated = true;
    if ($("#" + msName).val().trim() == '') {
        ShowError('Milestone Name is Required');
        Validated = false;
    }
    if ($("#" + msWeight).val().trim() == '') {
        ShowError('Milestone Weight is Required');
        Validated = false;
    }
    else if (Number($('#' + msWeight).val().trim()) <= 0) {
        ShowError('Invalid Milestone Weight');
        Validated = false;
    }
    return Validated;
}

function ValidateEmail(emailAddress) {
    var Validated = true;

    if ($("#" + emailAddress).val().trim() == '') {
        ShowError('E-mail Address is Required');
        Validated = false;
    }
    else if ($("#" + emailAddress).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + emailAddress).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    return Validated;
}

function ValidatePeriodicTask(jobName, jobFrequency, startDate, daysToComplete) {
    var Validated = true;

    if ($("#" + jobName).val().trim() == '') {
        ShowError('Job Name is Required');
        Validated = false;
    }
    if (isPositiveInteger($("#" + jobFrequency).val().trim()) == false) {
        ShowError('Invalid Task Frequency');
        Validated = false;
    }
    if (validatedate($("#" + startDate).val().trim()) == false) {
        ShowError('Invalid Start Date');
        ret = false;
    }
    if (isPositiveInteger($("#" + daysToComplete).val().trim()) == false) {
        ShowError('Invalid No of Days to Complete');
        Validated = false;
    }
    return Validated;
}

function ValidateCalender(calName, userGroup, subject, description) {
    var Validated = true;

    if ($("#" + calName).val().trim() == '') {
        ShowError('Calendar Name is Required');
        Validated = false;
    }
    if ($("#" + userGroup).val().trim() == '0') {
        ShowError('User Group is Required');
        Validated = false;
    }
    if ($("#" + subject).val().trim() == '') {
        ShowError('Default Subject is Required');
        Validated = false;
    }
    if ($("#" + description).val().trim() == '') {
        ShowError('Default Description is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFCelender(calName, startDate, startTime, duration) {
    var Validated = true;

    if ($('#' + calName).length == 0) {
        ShowError('There are no Calendars to Add');
        Validated = false;
    }
    if ($('#' + startDate).length == 0) {
        ShowError('There are no Fields for Start Date');
        Validated = false;
    }
    if ($('#' + startTime).length == 0) {
        ShowError('There are no Fields for Start Time');
        Validated = false;
    }
    if ($('#' + duration).length == 0) {
        ShowError('There are no Fields for Duration');
        Validated = false;
    }
    return Validated;
}

function ValidateSubTaskCreation(NoOfTasks, txtExtra1, txtExtra2, ex1Name, ex2Name) {
    var Validated = true;
    if ($('#' + NoOfTasks).val().trim() != "" && isPositiveInteger($('#' + NoOfTasks).val().trim())) {
        if (ex1Name.trim() != "" && $('#' + txtExtra1).val().trim() == '') {
            ShowError(ex1Name + ' is Required');
            Validated = false;
        }
        if (ex2Name.trim() != "" && $('#' + txtExtra2).val().trim() == '') {
            ShowError(ex2Name + ' is Required');
            Validated = false;
        }
    }
    else {
        ShowError('Invalid Number of Sub Tasks');
        Validated = false;
    }
    return Validated;
}

function ValidateTemplateName(template_name) {
    var Validated = true;

    if ($("#" + template_name).val().trim() == '') {
        ShowError('Template Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateQueue(queue_name) {
    var Validated = true;

    if ($("#" + queue_name).val().trim() == '') {
        ShowError('Queue Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFSH(sch_Name, rec_Type, rec_Frec) {
    var Validated = true;

    if ($("#" + sch_Name).val().trim() == '0') {
        ShowError('Schedule Name is Required');
        Validated = false;
    }
    if ($("#" + rec_Type).val().trim() == '0') {
        ShowError('Deadline Type is Required');
        Validated = false;
    }
    if ($("#" + rec_Frec).val().trim() == '0') {
        ShowError('Deadline Cccurrence is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFDP(doc_Repo, save_Control) {
    var Validated = true;

    if ($("#" + doc_Repo).val().trim() == '0') {
        ShowError('Document Repo is Required');
        Validated = false;
    }
    var TagsOK = true;
    $(".cboTag").each(function () {
        if ($(this).val().trim() == "0") {
            TagsOK = false;
        }
    });
    if (TagsOK == false) {
        ShowError('Please Select fields for all Tags');
        Validated = false;
    }

    if (Validated == true) {
        var save_Text = '';        
        $(".cboTag").each(function () {
            save_Text = save_Text + $(this).val().trim() + '^';
        });
        $('#' + save_Control).val(save_Text);
    }
    return Validated;
}

function ValidateDPWF(workflow) {
    var Validated = true;

    if ($("#" + workflow).val().trim() == '0') {
        ShowError('Workflow is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateSummaryEmail(receiverType, eventType, noOfDays, userGroup, users, receiver_support) {
    var Validated = true;

    if ($("#" + eventType).val().trim() == '0') {
        ShowError('Event Type is Required');
        Validated = false;
    }
    else if ($("#" + eventType).val() == '2') {
        if (isPositiveInteger($("#" + noOfDays).val()) == false) {
            ShowError('Invalid Number of Days');
            Validated = false;
        }
        else if (Number($("#" + noOfDays).val()) <= 0) {
            ShowError('Invalid Number of Days');
            Validated = false;
        }
    }
    if ($("#" + receiverType).val().trim() == '0') {
        ShowError('Email Receiver Type Type is Required');
        Validated = false;
    }
    else if ($("#" + receiverType).val() == '5' || $("#" + receiverType).val() == '6' || $("#" + receiverType).val() == '7') {
        if ($("#" + userGroup).val().trim() == '0') {
            ShowError('User Group is Required');
            Validated = false;
        }
    }
    else if ($("#" + receiverType).val() == '8') {
        if ($("#" + users).val().trim() == '0') {
            ShowError('User is Required');
            Validated = false;
        }
    }
    else if ($("#" + receiverType).val() == '9') {
        if ($("#" + receiver_support).val().trim() == '') {
            ShowError('Receiver Emails are Required');
            Validated = false;
        }
        else {
            var e_splits = $("#" + receiver_support).val().trim().split(',');
            var e_error = false;
            for (var i = 0; i < e_splits.length; i++) {
                if (e_splits[i].trim().split('@').length < 2) {
                    e_error = true;
                }
                else if (e_splits[i].trim().split('@')[1].split('.').length < 2) {
                    e_error = true;
                }
            }
            if (e_error == true) {
                ShowError('Invalid Email Addresses');
                Validated = false;
            }
        }
    }
    return Validated;
}

function ValidateNotice(noticeTitle) {
    var Validated = true;

    if ($("#" + noticeTitle).val().trim() == '') {
        ShowError('Notice Title is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateAPICAll(api_name, wf_steps) {
    var Validated = true;

    if ($("#" + api_name).val().trim() == '') {
        ShowError('API Name is Required');
        Validated = false;
    }
    if ($("#" + wf_steps + " input:checked").length == 0) {
        ShowError('Workflow Step is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateWFSubWF(subWorkflow, startStep, NoOfDays) {
    var Validated = true;

    if ($("#" + subWorkflow).val().trim() == '0') {
        ShowError('Sub Workflow is Required');
        Validated = false;
    }
    if ($("#" + startStep).val().trim() == '0') {
        ShowError('Start on Step is Required');
        Validated = false;
    }
    if (isPositiveInteger($("#" + NoOfDays).val()) == false) {
        ShowError('Invalid Number of Days');
        Validated = false;
    }
    return Validated;
}

function ValidateParentPost(parent_wf, wf_steps, parent_save, post_data_save) {
    var Validated = true;
    var post_data_text = '';
    if ($("#" + parent_wf).val().trim() == '0') {
        ShowError('Parent Workflow is Required');
        Validated = false;
    }
    else if ($('#sltParentStep').val().trim() == '0') {
        ShowError('Parent Workflow Step is Required');
        Validated = false;
    }
    else {
        var postOK = true;
        $(".post-data").each(function () {
            if ($(this).val().trim() == '0') {
                postOK = false;
            }
            else {
                post_data_text = post_data_text + $(this).val().trim() + '^';
            }
        });
        if (postOK == false) {
            ShowError('Post Data Missing');
            Validated = false;
        }
    }
    if ($("#" + wf_steps + " input:checked").length == 0) {
        ShowError('Workflow Step is Required');
        Validated = false;
    }
    if (Validated == true) {
        $('#' + parent_save).val($('#sltParentStep').val().trim());
        $('#' + post_data_save).val(post_data_text);
    }
    return Validated;
}

function ValidateStepDeadlines() {
    var Validated = true;
    var OutText = "";
    $('.tskdl').each(function () {
        if (validatedate($(this).val())) {
            OutText += $(this).data('id') + "^" + $(this).val() + "|";
        }
        else {
            Validated = false;
        }
    });
    if (Validated == false) {
        ShowError('Invalied Date Selections');
    }
    else {
        $('#hndStepDeadline').val(OutText);
    }
    return Validated;
}