
function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
}

function jsFunctions() {

    $('select[name*=ddlWagesWorkType]').change(function () {
        updateCategories(this);
    });

    $('select[name*=ddlWagesStartTime]').change(function () {
        updateNumbers(this);
    })

    $('select[name*=ddlWagesEndTime]').change(function () {
        updateNumbers(this);
    })

    function getNumber(number, defaultNumber) {
        return isNaN(parseFloat(number)) ? defaultNumber : parseFloat(number);
    }

    function updateNumbers(object) {

        //start time
        var st = $('#' + object.id.substring(0, 14) + 'ddlWagesStartMonth_' + object.id.slice(-1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartDay_' + object.id.slice(-1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartYear_' + object.id.slice(-1) + ' option:selected').val() + ' ' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartTime_' + object.id.slice(-1) + ' option:selected').val();

        //end time
        var et = $('#' + object.id.substring(0, 14) + 'ddlWagesEndMonth_' + object.id.slice(-1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndDay_' + object.id.slice(-1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndYear_' + object.id.slice(-1) + ' option:selected').val() + ' ' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndTime_' + object.id.slice(-1) + ' option:selected').val();

        // hours difference for total hours
        var diff = Math.abs(new Date(et) - new Date(st));
        var seconds = Math.floor(diff / 1000);
        var minutes = Math.floor(seconds / 60);
        seconds = seconds % 60;
        var hours = Math.floor(minutes / 60);
        minutes = minutes % 60;

        var totalHours = 0;
        switch (minutes) {
            case 0:
                totalHours = hours;
                break;
            case 15:
                totalHours = hours + '.25';
                break;
            case 30:
                totalHours = hours + '.50';
                break;
            case 45:
                totalHours = hours + '.75';
                break;
            case 60:
                totalHours = hours + 1;
                break;
        }

        //set total hours box
        $('#' + object.id.substring(0, 14) + 'totalHours_' + object.id.slice(-1)).val(totalHours);

        //calculate logged hours select boxes
        var remainingHours = 0;
        $('#' + object.id.substring(0, 14) + 'rptWagesWorkTypeContainer_' + object.id.slice(-1) + ' option:selected').each(function () {
            remainingHours = getNumber(remainingHours, 0) + getNumber(this.value, 0);
        });

        //set remaining hours box
        $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.slice(-1)).val(totalHours - remainingHours);

        //set style of remaining hours based on value - 0 is gray/black .... anything other than 0 is red/yellow
        if ((totalHours - remainingHours) == 0) {
            $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.slice(-1)).css({ "background-color": "#EBEBE4", "color": "black" });
        }
        else {
            $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.slice(-1)).css({ "background-color": "red", "color": "yellow" });
        }
    }


    function updateCategories(object) {

        //start time
        var st = $('#' + object.id.substring(0, 14) + 'ddlWagesStartMonth_' + object.id.substr(31, 1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartDay_' + object.id.substr(31, 1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartYear_' + object.id.substr(31, 1) + ' option:selected').val() + ' ' + $('#' + object.id.substring(0, 14) + 'ddlWagesStartTime_' + object.id.substr(31, 1) + ' option:selected').val();

        //end time
        var et = $('#' + object.id.substring(0, 14) + 'ddlWagesEndMonth_' + object.id.substr(31, 1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndDay_' + object.id.substr(31, 1) + ' option:selected').val() + '/' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndYear_' + object.id.substr(31, 1) + ' option:selected').val() + ' ' + $('#' + object.id.substring(0, 14) + 'ddlWagesEndTime_' + object.id.substr(31, 1) + ' option:selected').val();

        // hours difference for total hours
        var diff = Math.abs(new Date(et) - new Date(st));
        var seconds = Math.floor(diff / 1000);
        var minutes = Math.floor(seconds / 60);
        seconds = seconds % 60;
        var hours = Math.floor(minutes / 60);
        minutes = minutes % 60;

        var totalHours = 0;
        switch (minutes) {
            case 0:
                totalHours = hours;
                break;
            case 15:
                totalHours = hours + '.25';
                break;
            case 30:
                totalHours = hours + '.50';
                break;
            case 45:
                totalHours = hours + '.75';
                break;
            case 60:
                totalHours = hours + 1;
                break;
        }

        //set total hours box
        $('#' + object.id.substring(0, 14) + 'totalHours_' + object.id.substr(31, 1)).val(totalHours);

        //calculate logged hours select boxes
        var remainingHours = 0;
        $('#' + object.id.substring(0, 14) + 'rptWagesWorkTypeContainer_' + object.id.substr(31, 1) + ' option:selected').each(function () {
            remainingHours = getNumber(remainingHours, 0) + getNumber(this.value, 0);
        });

        //set remaining hours box
        $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.substr(31, 1)).val(totalHours - remainingHours);

        //set style of remaining hours based on value - 0 is gray/black .... anything other than 0 is red/yellow
        if ((totalHours - remainingHours) == 0) {
            $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.substr(31, 1)).css({ "background-color": "#EBEBE4", "color": "black" });
        }
        else {
            $('#' + object.id.substring(0, 14) + 'remainingHours_' + object.id.substr(31, 1)).css({ "background-color": "red", "color": "yellow" });
        }
    }

}


