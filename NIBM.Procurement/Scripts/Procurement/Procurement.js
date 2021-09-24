$.ajaxSetup({ cache: false });

var objEndTime = $('#EndTime');
var objStartTime = $('#StartTime');
var objNoOfHours = $('#NoofHrs');
var objbtnPastTender = $('#btnPastTender');
var objTenderID = $('#TenderID');
var objProcessType = $('#ProcessType');


$(function () {
    EventDetailsDocReadyFunc();

    objbtnPastTender.click(function () {
        objTenderID.trigger('mousedown.PopUpSelector');
    });

    objTenderID.change(function () {
        if (objTenderID.val() == null || objTenderID.val() == 0) {
            objProcessType.val(null);
            SetComboReadonly(objProcessType, false);
        }
        else {
            objProcessType.val(3);
            SetComboReadonly(objProcessType, true);
        }
    });

    $('.toggleItemsTbl tbody tr td').find("button[name='toggleItems']").each(function () {
        var $this = $(this);
        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var vlecId = $btn.data("procreq-id");
            var trID = "tr#" + vlecId;
            var childDiv = $btn.closest("tr").siblings(trID);

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    $('#tblProuReqApp').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #rejBtn');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            objrejBtn.hide();

            if (!$('#SelectAllAppPRoc').is(':checked')) {
                var trFullCount = $('#tblProuReqApp').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblProuReqApp').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllAppPRoc').prop('checked', true);
                    $('#SelectAllAppPRoc').val(true);
                }
                else { $('#SelectAllAppPRoc').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllAppPRoc').is(':checked')) {
                $('#SelectAllAppPRoc').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            textArea.show();
            textAreaHead.show();
            $('#SelectAllAppPRoc').prop('checked', false);
            $('#SelectAllAppPRoc').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllAppPRoc').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblProuReqApp').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                    var textArea = $('#tblProuReqApp').find('tbody tr td div table tbody tr td#TextComment');
                    var textAreaHead = $('#tblProuReqApp').find('thead tr th table thead tr th#textHeading');

                    objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblProuReqApp').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                var textArea = $('#tblProuReqApp').find('tbody tr td div table tbody tr td#TextComment');
                var textAreaHead = $('#tblProuReqApp').find('thead tr th table thead tr th#textHeading');

                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblProuReqApp').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);
        var $Status = $('#tblProuReqRecommand').find('tbody tr td div table tbody tr td#status input[type="hidden"]').val();

        //if ($Status == "Procurement Deptartment Received") { $('#ProcRecommond').show(); }
        //else { $('#ProcRecommond').hide(); }

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });


    $('#tblReqAppNPendComple').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #rejBtn');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            //objrejBtn.hide();

            if (!$('#SelectAllAppPRoc').is(':checked')) {
                var trFullCount = $('#tblReqAppNPendComple').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblReqAppNPendComple').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllAppPRoc').prop('checked', true);
                    $('#SelectAllAppPRoc').val(true);
                }
                else { $('#SelectAllAppPRoc').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllAppPRoc').is(':checked')) {
                $('#SelectAllAppPRoc').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            $('#SelectAllAppPRoc').prop('checked', false);
            $('#SelectAllAppPRoc').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllAppPRoc').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblReqAppNPendComple').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                    //objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblReqAppNPendComple').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblReqAppNPendComple').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });


    $('#tblProuReqRecommand').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #rejBtn');
        var textArea = $('#tblProuReqRecommand').find('tbody tr td div table tbody tr td#TextComment');
        var textAreaHead = $('#tblProuReqRecommand').find('thead tr th table thead tr th#textHeading');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            objrejBtn.hide();

            if (!$('#SelectAllRecommond').is(':checked')) {
                var trFullCount = $('#tblProuReqRecommand').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblProuReqRecommand').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllRecommond').prop('checked', true);
                    $('#SelectAllRecommond').val(true);
                }
                else { $('#SelectAllRecommond').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllRecommond').is(':checked')) {
                $('#SelectAllRecommond').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            $('#SelectAllRecommond').prop('checked', false);
            $('#SelectAllRecommond').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllRecommond').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblProuReqRecommand').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');

                    objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblProuReqRecommand').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                var textArea = $('#tblProuReqRecommand').find('tbody tr td div table tbody tr td#TextComment');
                var textAreaHead = $('#tblProuReqRecommand').find('thead tr th table thead tr th#textHeading');

                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblProuReqRecommand').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    $('#tblNewRequests').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #Incompltebtn');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            objrejBtn.hide();

            if (!$('#SelectAllNewReq').is(':checked')) {
                var trFullCount = $('#tblNewRequests').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblNewRequests').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllNewReq').prop('checked', true);
                    $('#SelectAllNewReq').val(true);
                }
                else { $('#SelectAllNewReq').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllNewReq').is(':checked')) {
                $('#SelectAllNewReq').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            $('#SelectAllNewReq').prop('checked', false);
            $('#SelectAllNewReq').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllNewReq').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblNewRequests').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #Incompltebtn');
                    objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblNewRequests').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #Incompltebtn');
                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblNewRequests').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    $('#tblPendingApprovals').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    $('#tblCompleteReq').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    ////---Recommend AS DG-----////

    $('#tblRecAsDG').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #rejBtn');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            objrejBtn.hide();

            if (!$('#SelectAllRecomAsDG').is(':checked')) {
                var trFullCount = $('#tblRecAsDG').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblRecAsDG').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllRecomAsDG').prop('checked', true);
                    $('#SelectAllRecomAsDG').val(true);
                }
                else { $('#SelectAllRecomAsDG').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllRecomAsDG').is(':checked')) {
                $('#SelectAllRecomAsDG').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            $('#SelectAllRecomAsDG').prop('checked', false);
            $('#SelectAllRecomAsDG').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllRecomAsDG').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblRecAsDG').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                    objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblRecAsDG').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblRecAsDG').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });
    /////--- Tender Started ---/////

    $('#tblTenderStarted').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);
        var objrejBtn = $(this).parent().parent().find('td #rejBtn');

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
            //objrejBtn.hide();

            if (!$('#SelectAllTenderStarted').is(':checked')) {
                var trFullCount = $('#tblTenderStarted').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblTenderStarted').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllTenderStarted').prop('checked', true);
                    $('#SelectAllTenderStarted').val(true);
                }
                else { $('#SelectAllTenderStarted').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            objrejBtn.show();
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllTenderStarted').is(':checked')) {
                $('#SelectAllTenderStarted').prop('checked', false);
            }
        }
        else {
            objrejBtn.show();
            $('#SelectAllTenderStarted').prop('checked', false);
            $('#SelectAllTenderStarted').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllTenderStarted').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblTenderStarted').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                    //objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblTenderStarted').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');
                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblTenderStarted').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    ///////------- User Feedbacks -------///////

    $('#tblPendingFeedbacks').find('tbody tr td div table tbody tr td#select input[type="checkbox"]').click(function () {
        var cb = $(this);

        cb.siblings('input[type="hidden"]').val(cb.prop('checked') ? 1 : 0);

        if (cb.prop('checked')) {
            $(this).parent().css("--bs-table-accent-bg", "#fcdfa9");
            $(this).parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");

            if (!$('#SelectAllFeedbacks').is(':checked')) {
                var trFullCount = $('#tblPendingFeedbacks').find('tbody tr td div div[name="ProcDetDiv"]').length;
                var trCount = 0;
                $('#tblPendingFeedbacks').find('tbody tr').each(function () {
                    var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
                    if (chck) {
                        trCount++;
                    }
                });

                if (trCount == trFullCount) {
                    $('#SelectAllFeedbacks').prop('checked', true);
                    $('#SelectAllFeedbacks').val(true);
                }
                else { $('#SelectAllFeedbacks').prop('checked', false); }
            }
        }
        else if (cb.prop('checked', false)) {
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");

            if ($('#SelectAllFeedbacks').is(':checked')) {
                $('#SelectAllFeedbacks').prop('checked', false);
            }
        }
        else {
            $('#SelectAllFeedbacks').prop('checked', false);
            $('#SelectAllFeedbacks').val(false);
            $(this).parent().css("--bs-table-accent-bg", "white");
            $(this).parent().siblings().css("--bs-table-accent-bg", "white");
        }

    });

    $('#SelectAllFeedbacks').change(function () {
        var ischecked = $(this).is(':checked');
        if (ischecked) {
            $('#tblPendingFeedbacks').find('tbody tr').each(function () {

                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                if (cb.val() != undefined) {
                    var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');

                    objrejBtn.hide();
                    cb.prop('checked', true);
                    cb.parent().css("--bs-table-accent-bg", "#fcdfa9");
                    cb.parent().siblings().css("--bs-table-accent-bg", "#fcdfa9");
                }
            });
        }
        else {
            $('#tblPendingFeedbacks').find('tbody tr').each(function () {
                var cb = $(this).find('td div table tbody tr td#select input[type="checkbox"]');
                var objrejBtn = $(this).find('td div table tbody tr td #rejBtn');

                if (cb.val() != undefined) {
                    objrejBtn.show();
                    cb.prop('checked', false);
                    cb.parent().css("--bs-table-accent-bg", "white");
                    cb.parent().siblings().css("--bs-table-accent-bg", "white");
                }
            });
        }
    });

    $('#tblPendingFeedbacks').find("button[name='toggleBudgets']").each(function () {
        var $this = $(this);

        $this.off("click");
        $this.on("click", function (e) {
            var $btn = $(this);
            var childDiv = $btn.closest("table").siblings("div");

            if ($btn.data("shown")) {
                $btn.removeData("shown");
                childDiv.hide();
                $btn.find('span').attr('class', 'bi bi-chevron-down');
            }
            else {
                $btn.data("shown", true);
                $btn.find('span').attr('class', 'bi bi-chevron-up');
                childDiv.show();
            }
        });
    });

    var tabValue = $('#numericVar1').val();
    if (tabValue == 1 || tabValue == 0) {
        $("#Approval").children('a').addClass(" active");
        $("#tabApproval").addClass(" in active show");

        $('#OnProcess').children('a').removeClass(" active");
        $('#tabOnProcess').removeClass(" in active show");

        $('#Tender').children('a').removeClass(" active");
        $('#tabTender').removeClass(" in active show");

        $('#Completion').children('a').removeClass(" active");
        $('#tabCompletion').removeClass(" in active show");
    }
    if (tabValue == 2) {
        $("#OnProcess").children('a').addClass(" active");
        $("#tabOnProcess").addClass(" in active show");

        $('#Approval').children('a').removeClass(" active");
        $('#tabApproval').removeClass(" in active show");

        $('#Tender').children('a').removeClass(" active");
        $('#tabTender').removeClass(" in active show");

        $('#Completion').children('a').removeClass(" active");
        $('#tabCompletion').removeClass(" in active show");
    }
    if (tabValue == 3) {
        $("#Tender").children('a').addClass(" active");
        $("#tabTender").addClass(" in active show");

        $('#OnProcess').children('a').removeClass(" active");
        $('#tabOnProcess').removeClass(" in active show");

        $('#Approval').children('a').removeClass(" active");
        $('#tabApproval').removeClass(" in active show");

        $('#Completion').children('a').removeClass(" active");
        $('#tabCompletion').removeClass(" in active show");
    }
    if (tabValue == 4) {
        $("#Completion").children('a').addClass(" active");
        $("#tabCompletion").addClass(" in active show");

        $('#OnProcess').children('a').removeClass(" active");
        $('#tabOnProcess').removeClass(" in active show");

        $('#Tender').children('a').removeClass(" active");
        $('#tabTender').removeClass(" in active show");

        $('#Approval').children('a').removeClass(" active");
        $('#tabApproval').removeClass(" in active show");
    }

});

function RejectProcFromTile() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var RejID = btn.parent().parent().parent().parent().parent().parent().find('td#RejID input[type="hidden"]').val();

    if (RejID == 01) { SupervisorComment = btn.parent().parent().parent().parent().parent().parent().find('td textarea[name="TxtAreaSuperCmt"]').val(); }
    if (RejID == 02) { SupervisorComment = btn.parent().parent().parent().parent().parent().parent().find('td textarea[name="TxtArea"]').val(); }
    if (RejID == 03) { SupervisorComment = btn.parent().parent().parent().parent().parent().parent().find('td textarea[name="TxtArea"]').val(); }

    var ValId = btn.parent().parent().parent().parent().parent().find('td#ValId input[type="hidden"]').val();
    var frm = btn.closest('form');
    +frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + ' <input type="hidden" name="IsTile" value="' + true + '" />' + ' <input type="hidden" name="ValId" value="' + ValId + '" />' + ' <input type="hidden" name="SupervisorComment" value="' + SupervisorComment + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/Reject");
    frm.attr('method', "POST");
    frm.submit();

}

function Incomplete() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + '<input type="hidden" name="IsTile" value="' + true + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/Incomplete");
    frm.attr('method', "POST");
    frm.submit();
}

function DepartmentReceived() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + '<input type="hidden" name="IsTile" value="' + true + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/DepartmentRecieve");
    frm.attr('method', "POST");
    frm.submit();
}

function Print() {

    var btnl = $('#tblReqAppNPendComple').find('tbody tr td div table tbody tr td #Printbtn')
    var btn = $(window.curSubmitter);
    //var Comments = btn.parent().parent().parent().parent().find('td textarea[name="TxtArea"]').val();
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + ' <input type="hidden" name="IsTile" value="' + true + '" />' + ' <input type="hidden" name="Comments" value="' + Comments + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/Print");
    frm.attr('method', "POST");
    frm.submit();

}

function ProcReqApprove() {
    var btn = $(window.curSubmitter)
    var objTable = $('#tblProuReqApp');
    var ProReqID = objTable.find('td div table tbody tr td#ProReqID input[type="hidden"]').val();
    var ValId = objTable.find('td div table tbody tr td#ValId input[type="hidden"]').val();
    var SupervisorComment = objTable.find('td div table tbody tr td#TextComment textarea[name="TxtAreaSuperCmt"]').val();


    var frm = btn.closest('form');
    +frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + ' <input type="hidden" name="ValId" value="' + ValId + '" />' + ' <input type="hidden" name="SupervisorComment" value="' + SupervisorComment + '" />');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/ApproveSelectIndex");
    frm.attr('method', "POST");
    frm.submit();

}

function ProcReq2ndApprove() {
    var objDataJSON = $('#stringPara5, #ValId');
    var objTable = $('#tblProuReqRecommand');
    var arrJsn = [];

    objTable.find('tbody tr').each(function () {
        var objJsn = new Object();
        objJsn.id = $(this).data('pid');

        objJsn.ProReqID = $(this).find('td div table tbody tr td#ProReqID input[type="hidden"]').val();
        objJsn.ValId = $(this).find('td div table tbody tr td#ValId input[type="hidden"]').val();
        objJsn.Comments = $(this).find('td div table tbody tr td textarea[name="TxtArea"]').val();
        var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
        if (chck) {
            arrJsn.push(objJsn);
        }
    });
    objDataJSON.val(JSON.stringify(arrJsn));

    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/Approve");
    frm.attr('method', "POST");
    frm.submit();

}

function Recommend() {
    var objDataJSON = $('#stringPara5, #ValId');
    var objTable = $('#tblProuReqRecommand');
    var arrJsn = [];

    objTable.find('tbody tr').each(function () {
        var objJsn = new Object();
        objJsn.id = $(this).data('pid');

        objJsn.ProReqID = $(this).find('td div table tbody tr td#ProReqID input[type="hidden"]').val();
        objJsn.ValId = $(this).find('td div table tbody tr td#ValId input[type="hidden"]').val();
        objJsn.Comments = $(this).find('td div table tbody tr td textarea[name="TxtArea"]').val();
        var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
        if (chck) {
            arrJsn.push(objJsn);
        }
    });
    objDataJSON.val(JSON.stringify(arrJsn));

    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/Recommend");
    frm.attr('method', "POST");
    frm.submit();

}


function ItemsReceivedIndex() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var supComment = btn.closest('tr').find('textarea').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />'
        + '<input type="hidden" name="Comments" value="' + supComment + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/ItemsReceivedIndex");
    frm.attr('method', "POST");
    frm.submit();
}
function CloseTenderIndex() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + '<input type="hidden" name="IsTile" value="' + true + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/CloseTenderIndex");
    frm.attr('method', "POST");
    frm.submit();
}
function PaymentComplete() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var supComment = btn.closest('tr').find('textarea').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + '<input type="hidden" name="IsTile" value="' + true + '" />'
        + '<input type="hidden" name="Comments" value="' + supComment + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/PaymentComplete");
    frm.attr('method', "POST");
    frm.submit();
}

function TenderStarted() {
    var objDataJSON = $('#stringPara3');
    var objTable = $('#tblTenderStarted');
    var arrJsn = [];

    objTable.find('tbody tr').each(function () {
        var objJsn = new Object();
        objJsn.id = $(this).data('pid');

        objJsn.ProReqID = $(this).find('td div table tbody tr td#ProReqID input[type="hidden"]').val();
        objJsn.Comments = $(this).find('td div table tbody tr td textarea[name="TxtArea"]').val();
        var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
        if (chck) {
            arrJsn.push(objJsn);
        }
    });
    objDataJSON.val(JSON.stringify(arrJsn));

    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/TenderStarted");
    frm.attr('method', "POST");
    frm.submit();

}

function ApproveAsDG() {

    var btn = $(window.curSubmitter);
    var ProReqID = btn.parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var supComment = btn.closest('tr').find('textarea').val();
    var frm = btn.closest('form');
    frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + '<input type="hidden" name="IsTile" value="' + true + '" />'
        + '<input type="hidden" name="Comments" value="' + supComment + '" />');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/ApproveSelectIndex");
    frm.attr('method', "POST");
    frm.submit();
}

function SendForApproval() {
    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');

    frm.append('<input type="hidden" name="ProReqID" value="' + btn.data('event-id') + '" />' +
        '<input type="hidden" name="RowVersion" value="' + btn.data('row-version') + '" />' +
        '<input type="hidden" name="IsTile" value="' + true + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/SendForApproval");
    frm.attr('method', "POST");
    frm.submit();
}

function Approve() {
    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');

    frm.append('<input type="hidden" name="ProReqID" value="' + btn.data('proreq-id') + '" />' +
        '<input type="hidden" name="RowVersion" value="' + btn.data('row-version') + '" />');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/Approve");
    frm.attr('method', "POST");
    frm.submit();
}

function Reject() {
    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');

    frm.append('<input type="hidden" name="ProReqID" value="' + btn.data('proreq-id') + '" />' +
        '<input type="hidden" name="RowVersion" value="' + btn.data('row-version') + '" />');
    frm.attr('action', AppRoot + "Procurement/ApproveRequest/Reject");
    frm.attr('method', "POST");
    frm.submit();
}

function TabRedirect() {
    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');

    frm.append('<input type="hidden" name="numericVar1" value="' + btn.data('proreq-id') + '" />' +
        '<input type="hidden" name="RowVersion" value="' + btn.data('row-version') + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementProcess/ProcurementProcessIndex");
    frm.attr('method', "POST");
    frm.submit();
}

function AddUserFeedback() {
    var btn = $(window.curSubmitter)
    var ProReqID = btn.parent().parent().parent().parent().find('td#ProReqID input[type="hidden"]').val();
    var UserFeedback = btn.parent().parent().parent().parent().find('td#TextComment textarea[name="TxtAreaUserFeedback"]').val();


    var frm = btn.closest('form');
    +frm.append('<input type="hidden" name="ProReqID" value="' + ProReqID + '" />' + ' <input type="hidden" name="UserFeedback" value="' + UserFeedback + '" />');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/AddFeedbacks");
    frm.attr('method', "POST");
    frm.submit();

}

function AddUserFeedbackBulkSave() {
    var objDataJSON = $('#StringPara1');
    var objTable = $('#tblPendingFeedbacks');
    var arrJsn = [];

    objTable.find('tbody tr').each(function () {
        var objJsn = new Object();
        objJsn.id = $(this).data('pid');

        objJsn.ProReqID = $(this).find('td div table tbody tr td#ProReqID input[type="hidden"]').val();
        objJsn.Comments = $(this).find('td div table tbody tr td#TextComment textarea[name="TxtAreaUserFeedback"]').val();
        var chck = $(this).find('td div table tbody tr td#select input[type="checkbox"]').is(':checked');
        if (chck) {
            arrJsn.push(objJsn);
        }
    });
    objDataJSON.val(JSON.stringify(arrJsn));

    var btn = $(window.curSubmitter);
    var frm = btn.closest('form');
    frm.attr('action', AppRoot + "Procurement/ProcurementRequests/AddFeedbacksSelected");
    frm.attr('method', "POST");
    frm.submit();

}

function EventDetailsDocReadyFunc() {
    DocReadyFunc();

    $("a[data-popup-editor]").each(function () {
        var $this = $(this);
        var dlg = GetDialogObj($this);

        function winResFunc() { dlgOnResize(dlg, $this.data("popup-width")); }

        dlg.dialog({
            height: "auto",
            show: "clip",
            modal: true,
            autoOpen: false,
            open: function (event, ui) {
                var closeBtn = $('.ui-dialog-titlebar-close');
                closeBtn.html('<span class="ui-button-icon-primary ui-icon ui-icon-closethick"></span>');
                dlg_z_index++;
                $(".ui-dialog").css("z-index", dlg_z_index.toString());
                dlg.dialog("option", "position", { my: "center", at: "center", of: window });

                $("body").css({
                    overflow: 'hidden'
                });

                $(".ui-widget-overlay").bind('click', function () { dlg.dialog("close"); });

                winResFunc();
                window.addEventListener("resize", winResFunc);
            },
            beforeClose: function (event, ui) {
                $("body").css({ overflow: 'inherit' });
                window.removeEventListener("resize", winResFunc);
            }
        });

        function bindDlgEvents() {
            $('input[type="submit"][value="Save"]', dlg).closest("form").submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            closeDialogModal(dlg);
                            $('#ChildContent').load(result.url, function (response, status, xhr) {

                                if (status == "error") {
                                    AlertIt("ERROR: " + xhr.status + "-" + xhr.statusText);
                                }
                                else { EventDetailsDocReadyFunc(); }
                            });
                            EventDetailsDocReadyFunc();
                            //SetHeaderValues(result.hdrData);
                        } else {
                            dlg.html(result);
                            EventDetailsDocReadyFunc();
                            bindDlgEvents();
                        }
                    }
                });
                return false;
            });

            $('input[type="button"][value="Cancel"]', dlg).click(function () {
                closeDialogModal(dlg);
            });
        }

        $this.off(".ChildPopUp");
        $this.on("click.ChildPopUp", function (e) {
            e.preventDefault();
            var hrf = this.href;

            dlg.load(hrf, function (response, status, xhr) {
                if (status == "error") {
                    AlertIt("ERROR: " + xhr.status + "-" + xhr.statusText);
                }
                else {
                    EventDetailsDocReadyFunc();
                    bindDlgEvents();

                    dlg.dialog("option", "title", $this.data("title"));
                    winResFunc();
                    dlg.dialog("open");
                }
            });
        });
    });

    $("button[data-popup-delete]").each(function () {
        var $this = $(this);
        var dlg = GetDialogObj($this);

        $this.closest("form").off(".ChildPopUp");
        $this.closest("form").on("submit.ChildPopUp", function () {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    closeDialogModal(dlg);
                    if (result.url) {
                        $('#ChildContent').load(result.url, function (response, status, xhr) {
                            if (status == "error") {
                                AlertIt("ERROR: " + xhr.status + "-" + xhr.statusText);
                            }
                            else { EventDetailsDocReadyFunc(); }
                        });

                    }
                    else { AlertIt("ERROR: " + result.msg); }
                },
                error: function (data, status, jqXHR) {
                    if (IsJson(data.responseText)) { AlertIt("ERROR: " + JSON.parse(data.responseText).Message); }
                    else { AlertIt("ERROR: " + data.statusText); }
                }
            });
            objProg.hide();
            return false;
        });
    });
}
