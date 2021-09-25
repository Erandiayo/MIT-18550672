$.ajaxSetup({ cache: false });


$(function () {
   
    var objBranch = $('#BranchID');
    var objDept = $('#DepartmentID');
    var objSubDept = $('#SubDeptID');
    var objFullName = $('#FullName');
    var objTitle = $('#Title');
    var objGender = $('#Gender');


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
