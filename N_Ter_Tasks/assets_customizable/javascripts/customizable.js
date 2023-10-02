function ValidateSeason(s_year, season_name) {
    var Validated = true;
    if (isPositiveInteger($('#' + s_year).val().trim()) == false) {
        ShowError('Invalid Year');
        Validated = false;
    }
    if ($('#' + season_name).val().trim() == '') {
        ShowError('Name is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateMillAllocation(season, client, mill) {
    var Validated = true;
    if ($('#' + season).val().trim() == '0') {
        ShowError('Season is Required');
        Validated = false;
    }
    if ($('#' + client).val().trim() == '0') {
        ShowError('Client is Required');
        Validated = false;
    }
    if ($('#' + mill).val().trim() == '') {
        ShowError('Select a File to Upload');
        Validated = false;
    }
    return Validated;
}

function ValidateSupplier(sup_name, main_email, identifier) {
    var Validated = true;
    if ($('#' + sup_name).val().trim() == '') {
        ShowError('Supplier Name is Required');
        Validated = false;
    }
    if ($('#' + main_email).val().trim() == '') {
        ShowError('Main Email is Required');
        Validated = false;
    }
    else if ($("#" + main_email).val().trim().split('@').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    else if ($("#" + main_email).val().trim().split('@')[1].split('.').length < 2) {
        ShowError('Invalid E-mail Address');
        Validated = false;
    }
    if ($('#' + identifier).val().trim() == '') {
        ShowError('Common Identifier is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateShipToPartyLocation(CountyCode, DestionationName, ShipTo, SoldTo, DestinationCity, GroupName, GroupCode)
{
    var Validated = true;
    if ($('#' + CountyCode).val().trim() == '') {
        ShowError('County Code is Required');
        Validated = false;
    }
    if ($('#' + CountyCode).val().trim().length > 3) {
        ShowError('County Code is Invalid');
        Validated = false;
    }
    if ($('#' + DestionationName).val().trim() == '') {
        ShowError('Destionation Name is Required');
        Validated = false;
    }
    if ($('#' + ShipTo).val().trim() == '') {
        ShowError('Ship To is Required');
        Validated = false;
    }
    if ($('#' + SoldTo).val().trim() == '') {
        ShowError('Sold To is Required');
        Validated = false;
    }
    if ($('#' + DestinationCity).val().trim() == '') {
        ShowError('Destination City is Required');
        Validated = false;
    }
    if ($('#' + GroupName).val().trim() == '') {
        ShowError('Group Name is Required');
        Validated = false;
    }
    if ($('#' + GroupCode).val().trim() == '') {
        ShowError('Group Code is Required');
        Validated = false;
    }
    return Validated;
}

function ValidateShipToParty(ReceivedSTP, ActualSTP, CompanyCode, DeptVSDClass, VSX, SoldTo, DChannel)
{
    var Validated = true;
    if ($('#' + ReceivedSTP).val().trim() == '') {
        ShowError('Received STP is Required');
        Validated = false;
    }
    if ($('#' + ActualSTP).val().trim() == '') {
        ShowError('Actual STP is Required');
        Validated = false;
    }
    if ($('#' + CompanyCode).val().trim() == '') {
        ShowError('Company Code is Required');
        Validated = false;
    }
    if ($('#' + DeptVSDClass).val().trim() == '') {
        ShowError('Dept VSD Class is Required');
        Validated = false;
    }
    if ($('#' + VSX).val().trim() == '') {
        ShowError('VSX is Required');
        Validated = false;
    }
    if ($('#' + SoldTo).val().trim() == '') {
        ShowError('Sold To is Required');
        Validated = false;
    }
    if ($('#' + DChannel).val().trim() == '') {
        ShowError('D Channel is Required');
        Validated = false;
    }
    return Validated;
}

