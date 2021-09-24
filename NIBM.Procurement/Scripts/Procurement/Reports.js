$(function () {
    var objBranch = $('#BranchID');
    var objDept = $('#DepartmentID');

    objBranch.change(function () {
        var para = objDept.data("para-json");
        para.branchID = Number(objBranch.val());
        objDept.attr("data-para-json", JSON.stringify(para));
        objDept.empty().append('<option selected="selected" value="">' + objDept.data("empty-text") + '</option>');
    });


    $("form").submit(function (e) {
        var frm = $(this);

        $.ajax({
            type: "POST",
            url: frm[0].action,
            data: frm.serialize(),
            success: function (data, textStatus, jqXHR) {
                var ct = jqXHR.getResponseHeader("content-type") || "";
                if (ct.indexOf('ms-excel') > -1) {
                    objProg.hide();
                    var win = window.open(frm[0].action + '?' + frm.serialize(), '_blank');
                    if (win) {
                        win.focus();
                    }
                }
                else if (ct.indexOf('json') > -1) {
                    objProg.hide();
                    var win = window.open(frm[0].action + '?' + $.param(data), '_blank');
                    if (win) {
                        win.focus();
                    }
                }
                else {
                    var w = document.open("text/html", "replace");
                    w.write(data);
                    w.close();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                AlertIt(textStatus);
            }
        });

        e.preventDefault();
    });
});
