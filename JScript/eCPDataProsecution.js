function InitDocumentCopy(_doc) {
    $("#" + _doc + "-copy-linkpreview a").click(function () {
        DownloadDocumentCopy(_doc);
    });

    $("#" + _doc + "-copy-file").change(function () {
        $("#" + _doc + "-copy").val("");
        $("#" + _doc + "-copy-preview, " +
          "#" + _doc + "-copy-nopreview, " +
          "#" + _doc + "-copy-linkpreview").addClass("hidden");

        if (this.files && this.files[0]) {
            var _fd = new FormData();
            var _files = this.files[0];

            if (_files.type == "application/pdf") {
                if (_files.size <= 4194304) {
                    _fd.append("action", "preview");
                    _fd.append("file", _files);

                    $.ajax({
                        beforeSend: function () {
                            $("#" + _doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                              "#" + _doc + "-copy-input .uploadfile-container .form-discription-style, " +
                              "#" + _doc + "-copy-preview").addClass("hidden");
                            $("#" + _doc + "-copy-input .uploadfile-container .preloading-inline").removeClass("hidden");
                        },
                        async: true,
                        type: "POST",
                        url: "FileProcess.aspx",
                        data: _fd,
                        contentType: false,
                        processData: false,
                        success: function (_result) {
                            $("#" + _doc + "-copy").val("data:" + _files.type + ";base64," + _result);

                            PreviewDocumentCopy(_doc, function (_success) {
                                if (_success == true) {
                                    $("#" + _doc + "-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                    $("#" + _doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                                      "#" + _doc + "-copy-input .uploadfile-container .form-discription-style, " +
                                      "#" + _doc + "-copy-preview, " +
                                      "#" + _doc + "-copy-linkpreview").removeClass("hidden");

                                    DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                                }
                                else {
                                    $("#" + _doc + "-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                    $("#" + _doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                                      "#" + _doc + "-copy-input .uploadfile-container .form-discription-style, " +
                                      "#" + _doc + "-copy-nopreview, " +
                                      "#" + _doc + "-copy-linkpreview").removeClass("hidden");

                                    DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                                }
                            });
                        }
                    });
                }
                else
                    DialogMessage("ขนาดไฟล์เกิน 4MB", "", false, "");
            }
            else
                DialogMessage("เฉพาะไฟล์นามสกุล .pdf", "", false, "");
        }
    });
}

function DownloadDocumentCopy(_doc) {
    DialogLoading("กำลังโหลด...");

    var _file = btoa($("#" + _doc + "-copy").val())

    $("#download-" + _doc + "copy-form #file").val(_file);
    $("#download-" + _doc + "copy-form").submit();

    window.setTimeout(function () {
        $("#dialog-loading").dialog("close");
    }, 500);
}

function PreviewDocumentCopy(_doc, _callbackFunc) {
    try {
        var _pdfData = atob($("#" + _doc + "-copy").val().split("base64,")[1]);
        var _pdfjs = window["pdfjs-dist/build/pdf"];

        _pdfjs.GlobalWorkerOptions.workerSrc = "https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.worker.min.js";

        var _canvas = document.getElementById(_doc + "-copy-preview");
        var _context = _canvas.getContext("2d");
        var _scale = 0.5;

        _pdfjs.getDocument({ data: _pdfData }).promise.then(function (_pdf) {
            _pdf.getPage(1).then(function (_page) {
                var _viewport = _page.getViewport({ scale: _scale });

                _canvas.width = _viewport.width;
                _canvas.height = _viewport.height;
                _page.render({
                    canvasContext: _context,
                    viewport: _viewport
                });

                _callbackFunc(true);
            });
        });
    }
    catch (_e) {
        _callbackFunc(false);
    };
}

function ResetFrmDocumentCopy(_doc) {
    $("#" + _doc + "-copy").val($("#" + _doc + "-copy-hidden").val());

    if ($("#" + _doc + "-copy").val().length > 0) {
        PreviewDocumentCopy(_doc, function (_success) {
            if (_success == true) {
                $("#" + _doc + "-copy-preview, " +
                  "#" + _doc + "-copy-linkpreview").removeClass("hidden");
            }
            else {
                $("#" + _doc + "-copy-nopreview, " +
                  "#" + _doc + "-copy-linkpreview").removeClass("hidden");
            }
        });
    }
    else {
        $("#" + _doc + "-copy-preview, " +
          "#" + _doc + "-copy-nopreview, " +
          "#" + _doc + "-copy-linkpreview").addClass("hidden");
    }
}

function InitCPTransProsecution() {
    var _statusPayment = $("#statuspayment-hidden").val();
    
    if (_statusPayment != "3") {
        InitDocumentCopy("judgment");
        InitDocumentCopy("execution");
        InitDocumentCopy("executionwithdraw");
    }
    else
        $("#addupdate-cp-trans-prosecution .overlay").removeClass("hidden");

    ResetFrmCPTransProsecution();
}

function ResetFrmCPTransProsecution() {
    GoToTopElement("html, body");

    ResetFrmCPTransProsecutionWithDoc("complaint");
    ResetFrmCPTransProsecutionWithDoc("judgment");
    ResetFrmCPTransProsecutionWithDoc("execution");
    ResetFrmCPTransProsecutionWithDoc("executionwithdraw");
}

function ResetFrmCPTransProsecutionWithDoc(_doc) {
    var _formatPayment = $("#format-payment-hidden").val();
    var _statusPayment = $("#statuspayment-hidden").val();

    $("#" + _doc + "-lawyer").html($("#" + _doc + "-lawyer-hidden").val());

    if (_doc == "complaint") {        
        var _complaintCapital = ""

        if (_formatPayment == "1")
            _complaintCapital = $("#totalpenalty-hidden").val();

        if (_formatPayment == "2")
            _complaintCapital = (_statusPayment == "1" ? $("#totalpenalty-hidden").val() : $("#totalremain-hidden").val());

        var _complaintActionDate = $("#complaint-actiondate-hidden").val();

        if (_complaintActionDate.length > 0) {
            $("#complaint-actiondate-y").removeClass("hidden");
            $("#complaint-actiondate").html(_complaintActionDate);
            $("#complaint-actiondate-n").addClass("hidden");
        }
        else {
            $("#complaint-actiondate-y").addClass("hidden");
            $("#complaint-actiondate").html("");
            $("#complaint-actiondate-n").removeClass("hidden");
        }

        $("#complaint-blackcaseno").val($("#complaint-blackcaseno-hidden").val());
        $("#complaint-capital").val($("#complaint-capital-hidden").val().length > 0 ? $("#complaint-capital-hidden").val() : _complaintCapital);
        $("#complaint-interest").val($("#complaint-interest-hidden").val());
        TextboxDisable("#complaint-capital");
        TextboxDisable("#complaint-totalpayment");
        CalTotalPayment();
    }

    if (_doc == "judgment") {
        var _judgmentActionDate = $("#judgment-actiondate-hidden").val();

        if (_judgmentActionDate.length > 0) {
            $("#judgment-actiondate-y").removeClass("hidden");
            $("#judgment-actiondate").html(_judgmentActionDate);
            $("#judgment-actiondate-n").addClass("hidden");
        }
        else {
            $("#judgment-actiondate-y").addClass("hidden");
            $("#judgment-actiondate").html("");
            $("#judgment-actiondate-n").removeClass("hidden");
        }

        $("#judgment-redcaseno").val($("#judgment-redcaseno-hidden").val());

        if ($("input[name=judgment-result]:radio").filter("[value=" + $("#judgment-result-hidden").val() + "]").length > 0)
            $("input[name=judgment-result][value=" + $("#judgment-result-hidden").val() + "]").prop("checked", true);
        else
            $("input[name=judgment-result]:radio").prop("checked", false);

        ResetFrmDocumentCopy("judgment");
        $("#judgment-remark").val($("#judgment-remark-hidden").val());
    }

    if (_doc == "execution") {
        var _executionActionDate = $("#execution-actiondate-hidden").val();

        if (_executionActionDate.length > 0) {
            $("#execution-actiondate-y").removeClass("hidden");
            $("#execution-actiondate").html(_executionActionDate);
            $("#execution-actiondate-n").addClass("hidden");
        }
        else {
            $("#execution-actiondate-y").addClass("hidden");
            $("#execution-actiondate").html("");
            $("#execution-actiondate-n").removeClass("hidden");
        }

        $("#execution-date").val($("#execution-date-hidden").val());
        InitCalendar("#execution-date");
        ResetFrmDocumentCopy("execution");
    }

    if (_doc == "executionwithdraw") {
        var _executionWithdrawActionDate = $("#executionwithdraw-actiondate-hidden").val();

        if (_executionWithdrawActionDate.length > 0) {
            $("#executionwithdraw-actiondate-y").removeClass("hidden");
            $("#executionwithdraw-actiondate").html(_executionWithdrawActionDate);
            $("#executionwithdraw-actiondate-n").addClass("hidden");
        }
        else {
            $("#executionwithdraw-actiondate-y").addClass("hidden");
            $("#executionwithdraw-actiondate").html("");
            $("#executionwithdraw-actiondate-n").removeClass("hidden");
        }

        $("#executionwithdraw-date").val($("#executionwithdraw-date-hidden").val());
        InitCalendar("#executionwithdraw-date");
        $("#executionwithdraw-reason").val($("#executionwithdraw-reason-hidden").val());
        ResetFrmDocumentCopy("executionwithdraw");
    }
}

function CalTotalPayment() {
    var _complaintCapital = DelCommas("complaint-capital");
    var _complaintInterest = ($("#complaint-interest").val().length > 0 ? DelCommas("complaint-interest") : "0.00");    
    var _complaintTotalPayment = (parseFloat(_complaintCapital) + parseFloat(_complaintInterest));

    $("#complaint-totalpayment").val(_complaintTotalPayment.toLocaleString());
}

function ValidateCPTransProsecution(_doc) {
    var _error = false;
    var _msg;
    var _focus;

    if (_doc == "complaint") {
        var _complaintBlackCaseNo = $("#complaint-blackcaseno").val();
        var _complaintInterest = DelCommas("complaint-interest");

        if (_error == false && _complaintBlackCaseNo.length == 0) {
            _error = true;
            _msg = "กรุณาใส่คำฟ้อง - คดีหมายเลขดำที่";
            _focus = "#complaint-blackcaseno";
        }

        if (_error == false && _complaintInterest.length == 0) {
            _error = true;
            _msg = "กรุณาใส่คำฟ้อง - ดอกเบี้ยถึงวันฟ้อง";
            _focus = "#complaint-interest";
        }
    }

    if (_doc == "judgment") {
        var _judgmentRedCaseNo = $("#judgment-redcaseno").val();
        var _judgmentResult = $("input[name=judgment-result]:checked");

        if (_error == false && _judgmentRedCaseNo.length == 0) {
            _error = true;
            _msg = "กรุณาใส่คำพิพากษา - คดีหมายเลขแดงที่";
            _focus = "#judgment-redcaseno";
        }

        if (_error == false && _judgmentResult.length == 0) {
            _error = true;
            _msg = "กรุณาใส่คำพิพากษา - ผลคำพิพากษา";
            _focus = "#judgment-input";
        }
    }

    if (_doc == "execution") {
        var _executionDate = $("#execution-date").val();

        if (_error == false && _executionDate.length == 0) {
            _error = true;
            _msg = "กรุณาใส่หมายบังคับคดี - ลงวันที่";
            _focus = "#execution-date";
        }
    }

    if (_doc == "executionwithdraw") {
        var _executionWithdrawDate = $("#executionwithdraw-date").val();

        if (_error == false && _executionWithdrawDate.length == 0) {
            _error = true;
            _msg = "กรุณาใส่หมายถอนการบังคับคดี - ลงวันที่";
            _focus = "#executionwithdraw-date";
        }
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return false;
    }

    return true;

}

function ConfirmActionCPTransProsecution(_doc) {
    DialogConfirm("ต้องการบันทึกข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                if (ValidateCPTransProsecution(_doc)) {
                    var _send = new Array();
                    _send[_send.length] = "cp2id=" + $("#cp2id").val();
                    _send[_send.length] = "document=" + _doc;
                    _send[_send.length] = (_doc + "lawyerfullname=" + $("#" + _doc + "-lawyer-fullname-hidden").val());
                    _send[_send.length] = (_doc + "lawyerphonenumber=" + $("#" + _doc + "-lawyer-phonenumber-hidden").val());
                    _send[_send.length] = (_doc + "lawyermobilenumber=" + $("#" + _doc + "-lawyer-mobilenumber-hidden").val());
                    _send[_send.length] = (_doc + "lawyeremail=" + $("#" + _doc + "-lawyer-email-hidden").val());

                    if (_doc == "complaint") {
                        _send[_send.length] = "complaintblackcaseno=" + $("#complaint-blackcaseno").val();
                        _send[_send.length] = "complaintcapital=" + DelCommas("complaint-capital");
                        _send[_send.length] = "complaintinterest=" + ($("#complaint-interest").val() != "0.00" ? DelCommas("complaint-interest") : "");
                    }

                    if (_doc == "judgment") {
                        _send[_send.length] = "judgmentredcaseno=" + $("#judgment-redcaseno").val();
                        _send[_send.length] = "judgmentverdict=" + $("input[name=judgment-result]:checked").val();
                        _send[_send.length] = "judgmentcopy=" + ($("#judgment-copy").val().length > 0 ? btoa($("#judgment-copy").val()) : "");
                        _send[_send.length] = "judgmentremark=" + $("#judgment-remark").val();

                    }

                    if (_doc == "execution") {
                        _send[_send.length] = "executiondate=" + $("#execution-date").val();
                        _send[_send.length] = "executioncopy=" + ($("#execution-copy").val().length > 0 ? btoa($("#execution-copy").val()) : "");
                    }

                    if (_doc == "executionwithdraw") {
                        _send[_send.length] = "executionwithdrawdate=" + $("#executionwithdraw-date").val();
                        _send[_send.length] = "executionwithdrawreason=" + $("#executionwithdraw-reason").val();
                        _send[_send.length] = "executionwithdrawcopy=" + ($("#executionwithdraw-copy").val().length > 0 ? btoa($("#executionwithdraw-copy").val()) : "");
                    }

                    AddUpdateCPTransProsecution(_send, _doc);
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddUpdateCPTransProsecution(_send, _doc) {
    var _RCID = $("#RCID-hidden").val();
    var _action = (_RCID.length > 0 ? "update" : "add");

    AddUpdateData(_action, _action + "cptransprosecution", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    var _dt = new Date();
                    var _thLongDate = (_dt.getDate() + " " + _monthNames[_dt.getMonth()] + " " + (_dt.getFullYear() + 543));

                    if (_action == "add")
                        $("#RCID-hidden").val($("#cp2id").val());

                    $("#" + _doc + "-lawyer-hidden").val($("#" + _doc + "-lawyer-fullname-hidden").val());
                    
                    if (_doc == "complaint") {
                        $("#complaint-blackcaseno-hidden").val($("#complaint-blackcaseno").val());
                        $("#complaint-capital-hidden").val($("#complaint-capital").val());
                        $("#complaint-interest-hidden").val($("#complaint-interest").val());
                        $("#complaint-actiondate-hidden").val(_thLongDate);
                    }

                    if (_doc == "judgment") {
                        $("#judgment-redcaseno-hidden").val($("#judgment-redcaseno").val());
                        $("#judgment-result-hidden").val($("input[name=judgment-result]:checked").val());
                        $("#judgment-copy-hidden").val($("#judgment-copy").val());
                        $("#judgment-remark-hidden").val($("#judgment-remark").val());
                        $("#judgment-actiondate-hidden").val(_thLongDate);
                    }

                    if (_doc == "execution") {
                        $("#execution-date-hidden").val($("#execution-date").val());
                        $("#execution-copy-hidden").val($("#execution-copy").val());
                        $("#execution-actiondate-hidden").val(_thLongDate);
                    }

                    if (_doc == "executionwithdraw") {
                        $("#executionwithdraw-date-hidden").val($("#executionwithdraw-date").val());
                        $("#executionwithdraw-reason-hidden").val($("#executionwithdraw-reason").val());
                        $("#executionwithdraw-copy-hidden").val($("#executionwithdraw-copy").val());
                        $("#executionwithdraw-actiondate-hidden").val(_thLongDate);
                    }

                    GoToElement(_doc);
                    ResetFrmCPTransProsecutionWithDoc(_doc);
                }
            }
        });
    });
}