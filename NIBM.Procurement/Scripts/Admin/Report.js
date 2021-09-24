$(function () {


    var objSelectAllVehicle = $('#SelectAllVehicles');
    
    var objVehicle = $('#VehicleID');
   
    var ischecked = objSelectAllVehicle.is(':checked');

    if (ischecked) {
        objVehicle.prop('disabled', true);
    }
    objSelectAllVehicle.change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {

            objVehicle.prop('disabled', true);
        }
        else {
            objVehicle.prop('disabled', false);

        }
    });

   

});

