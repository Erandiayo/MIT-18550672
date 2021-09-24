$.ajaxSetup({ cache: false });


$(function () {
   
    var objBranch = $('#BranchID');
    var objDept = $('#DepartmentID');
    var objSubDept = $('#SubDeptID');
    var objFullName = $('#FullName');
    var objNICNo = $('#NICNo');
    var objDOB = $('#DOB');
    var objTitle = $('#Title');
    var objGender = $('#Gender');

    objNICNo.keyup(function () {
        var NICNumber = objNICNo.val().trim();
        objDOB.val(FindBirthDay(NICNumber));
    });

    objTitle.change(function () {
        if (objTitle.val() == 3) { objGender.val(0).change(); }
        else if (objTitle.val() == 4) { objGender.val(1).change(); }
    });

    objBranch.change(function () {
        var para = objDept.data("para-json");
        para.branchID = objBranch.val();
        objDept.attr("data-para-json", JSON.stringify(para));
        objDept.empty().append('<option selected="selected" value="">' + objDept.data("empty-text") + '</option>');
    });

    objDept.change(function () {
        var para = objSubDept.data("para-json");
        para.deptID = Number(objDept.val());
        objSubDept.attr("data-para-json", JSON.stringify(para));
        objSubDept.empty().append('<option selected="selected" value="">' + objSubDept.data("empty-text") + '</option>');
    });

    objFullName.change(function () {
        var initials = "";
        var x = objFullName.val().split(' ');
        var a = x.length;
        $('#LastName').val(x[a - 1].toString());

        for (var i = 0; i < a - 1; i++) {
            initials += x[i].charAt(0).toUpperCase() + " ";
        }

        $('#Initials').val(initials);
    });


    ddlConTyp.change(function () {
        chkIsCon.prop("checked", ddlConTyp.val() != "");
    });

    var ObjStatus = $('#Status');
    var ObjInactiveRes = $('#InactiveReason');

    ObjStatus.change(function () {
        if (ObjStatus.val() == 1) {
            ObjInactiveRes.val("");
            SetComboReadonly(ObjInactiveRes, true);
        }
        else {
            SetComboReadonly(ObjInactiveRes, false);
        }
    });

});

function FindBirthDay(NICNo) {
    var dayText = 0;
    var year = "";
    var month = "";
    var day = "";
    if (NICNo.length != 10 && NICNo.length != 12) {
        //AlertIt("Invalid NIC NO");
    } else if (NICNo.length == 10 && !$.isNumeric(NICNo.substr(0, 9))) {
        //AlertIt("Invalid NIC NO");
    }
    else {
        // Year
        if (NICNo.length == 10) {
            year = "19" + NICNo.substr(0, 2);
            dayText = parseInt(NICNo.substr(2, 3));
        } else {
            year = NICNo.substr(0, 4);
            dayText = parseInt(NICNo.substr(4, 3));
        }

        //Gender
        if (dayText > 500) {
            $('#Gender').val(1);
            $('#Title').val(4);
            dayText = dayText - 500;
        }
        else {
            $('#Title').val(3);
            $('#Gender').val(0);
        }

        // Day Digit Validation
        if (dayText < 1 && dayText > 366) {
            //AlertIt("Invalid NIC NO");
        } else {

            //Month
            if (dayText > 335) {
                day = dayText - 335;
                month = "12";
            }
            else if (dayText > 305) {
                day = dayText - 305;
                month = "11";
            }
            else if (dayText > 274) {
                day = dayText - 274;
                month = "10";
            }
            else if (dayText > 244) {
                day = dayText - 244;
                month = "09";
            }
            else if (dayText > 213) {
                day = dayText - 213;
                month = "08";
            }
            else if (dayText > 182) {
                day = dayText - 182;
                month = "07";
            }
            else if (dayText > 152) {
                day = dayText - 152;
                month = "06";
            }
            else if (dayText > 121) {
                day = dayText - 121;
                month = "05";
            }
            else if (dayText > 91) {
                day = dayText - 91;
                month = "04";
            }
            else if (dayText > 60) {
                day = dayText - 60;
                month = "03";
            }
            else if (dayText < 32) {
                month = "01";
                day = dayText;
            }
            else if (dayText > 31) {
                day = dayText - 31;
                month = "02";
            }

        }
    }
    if (year == "" && month == "" && day == "") { return ""; }
    else {
        return (year + "-" + month + "-" + day);
    }
}