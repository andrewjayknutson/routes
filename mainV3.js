
function load() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jsFunctions);
}

function jsFunctions() {

    $('select[name$=ddlWagesWorkType]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlTruckHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlClericalHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlSupportHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlMeetingHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlTrainingHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlPointHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlFleetHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlWarehouseHours]').change(function () {
        updateNumbers(this);
    });

    $('select[name$=ddlWagesStartTime]').change(function () {
        updateNumbers(this);
    })

    $('select[name$=ddlWagesEndTime]').change(function () {
        updateNumbers(this);
    })


    function getNumber(number, defaultNumber) {
        return isNaN(parseFloat(number)) ? defaultNumber : parseFloat(number);
    }

    function updateNumbers(object) {

        //start time
        //var st = $('#' + object.id.substring(0, 26) + 'ddlWagesStartMonth option:selected').val() + '/' + $('#' + object.id.substring(0, 26) + 'ddlWagesStartDay option:selected').val() + '/' + $('#' + object.id.substring(0, 26) + 'ddlWagesStartYear option:selected').val() + ' ' + $('#' + object.id.substring(0, 26) + 'ddlWagesStartTime option:selected').val();
        //var st = $('#' + object.id.substring(0, 11) + 'txtRouteDate').val() + ' ' + $('#' + object.id.substring(0, 30) + 'ddlWagesStartTime option:selected').val();
        var st = $('#' + object.id.substring(0, 11) + 'txtRouteDate').val() + ' ' + $('#' + object.id.substring(0, 30) + 'ddlWagesStartTime option:selected').val();

        //end time
        //var et = $('#' + object.id.substring(0, 26) + 'ddlWagesEndMonth option:selected').val() + '/' + $('#' + object.id.substring(0, 26) + 'ddlWagesEndDay option:selected').val() + '/' + $('#' + object.id.substring(0, 26) + 'ddlWagesEndYear option:selected').val() + ' ' + $('#' + object.id.substring(0, 26) + 'ddlWagesEndTime option:selected').val();
        var et = $('#' + object.id.substring(0, 11) + 'txtRouteDate').val() + ' ' + $('#' + object.id.substring(0, 30) + 'ddlWagesEndTime option:selected').val();

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
        $('#' + object.id.substring(0, 30) + 'txtTotalHours').val(totalHours);

        //calculate logged hours select boxes
        var remainingHours = 0;

        //Truck Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlTruckHours option:selected').val(), 0);

        //Clerical Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlClericalHours option:selected').val(), 0);

        //Meeting Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlMeetingHours option:selected').val(), 0);

        //Support Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlSupportHours option:selected').val(), 0);

        //Training Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlTrainingHours option:selected').val(), 0);

        //Point Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlPointHours option:selected').val(), 0);

        //Fleet Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlFleetHours option:selected').val(), 0);

        //Warehouse Hours
        remainingHours = getNumber(remainingHours, 0) + getNumber($('#' + object.id.substring(0, 30) + 'ddlWarehouseHours option:selected').val(), 0);

        //set remaining hours box
        $('#' + object.id.substring(0, 30) + 'txtRemainingHours').val(totalHours - remainingHours);

        //set style of remaining hours based on value - 0 is gray/black .... anything other than 0 is red/yellow
        if ((totalHours - remainingHours) == 0) {
            $('#' + object.id.substring(0, 30) + 'txtRemainingHours').css({ "background-color": "#EBEBE4", "color": "black" });
        }
        else {
            $('#' + object.id.substring(0, 30) + 'txtRemainingHours').css({ "background-color": "red", "color": "yellow" });
        }
    }

}


