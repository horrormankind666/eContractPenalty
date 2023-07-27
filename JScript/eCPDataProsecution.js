function InitDocumentCopy(doc) {
    $("#" + doc + "-copy-linkpreview a").click(function () {
        DownloadDocumentCopy(doc);
    });

    $("#" + doc + "-copy-file").change(function () {
        $("#" + doc + "-copy").val("");
        $("#" + doc + "-copy-preview, " +
          "#" + doc + "-copy-nopreview, " +
          "#" + doc + "-copy-linkpreview").addClass("hidden");

        if (this.files &&
            this.files[0]) {
            var fd = new FormData();
            var files = this.files[0];

            if (files.type == "application/pdf") {
                if (files.size <= 4194304) {
                    fd.append("action", "preview");
                    fd.append("file", files);

                    $.ajax({
                        beforeSend: function () {
                            $("#" + doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                              "#" + doc + "-copy-input .uploadfile-container .form-discription-style, " +
                              "#" + doc + "-copy-preview").addClass("hidden");
                            $("#" + doc + "-copy-input .uploadfile-container .preloading-inline").removeClass("hidden");
                        },
                        async: true,
                        type: "POST",
                        url: "FileProcess.aspx",
                        data: fd,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            $("#" + doc + "-copy").val("data:" + files.type + ";base64," + result);

                            PreviewDocumentCopy(doc, function (success) {
                                if (success == true) {
                                    $("#" + doc + "-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                    $("#" + doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                                      "#" + doc + "-copy-input .uploadfile-container .form-discription-style, " +
                                      "#" + doc + "-copy-preview, " +
                                      "#" + doc + "-copy-linkpreview").removeClass("hidden");

                                    DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                                }
                                else {
                                    $("#" + doc + "-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                    $("#" + doc + "-copy-input .uploadfile-container .uploadfile-form, " +
                                      "#" + doc + "-copy-input .uploadfile-container .form-discription-style, " +
                                      "#" + doc + "-copy-nopreview, " +
                                      "#" + doc + "-copy-linkpreview").removeClass("hidden");

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

function DownloadDocumentCopy(doc) {
    DialogLoading("กำลังโหลด...");

    var file = btoa($("#" + doc + "-copy").val())

    $("#download-" + doc + "copy-form #file").val(file);
    $("#download-" + doc + "copy-form").submit();

    window.setTimeout(function () {
        $("#dialog-loading").dialog("close");
    }, 500);
}

function PreviewDocumentCopy(
    doc,
    callbackFunc
) {
    try {
        var pdfData = atob($("#" + doc + "-copy").val().split("base64,")[1]);
        var pdfjs = window["pdfjs-dist/build/pdf"];

        pdfjs.GlobalWorkerOptions.workerSrc = "https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.worker.min.js";

        var canvas = document.getElementById(doc + "-copy-preview");
        var context = canvas.getContext("2d");
        var scale = 0.5;

        pdfjs.getDocument({
            data: pdfData
        }).promise.then(function (pdf) {
            pdf.getPage(1).then(function (page) {
                var viewport = page.getViewport({
                    scale: scale
                });

                canvas.width = viewport.width;
                canvas.height = viewport.height;
                page.render({
                    canvasContext: context,
                    viewport: viewport
                });

                callbackFunc(true);
            });
        });
    }
    catch (e) {
        callbackFunc(false);
    };
}

function ResetFrmDocumentCopy(doc) {
    $("#" + doc + "-copy").val($("#" + doc + "-copy-hidden").val());

    if ($("#" + doc + "-copy").val().length > 0) {
        PreviewDocumentCopy(doc, function (success) {
            if (success == true) {
                $("#" + doc + "-copy-preview, " +
                  "#" + doc + "-copy-linkpreview").removeClass("hidden");
            }
            else {
                $("#" + doc + "-copy-nopreview, " +
                  "#" + doc + "-copy-linkpreview").removeClass("hidden");
            }
        });
    }
    else {
        $("#" + doc + "-copy-preview, " +
          "#" + doc + "-copy-nopreview, " +
          "#" + doc + "-copy-linkpreview").addClass("hidden");
    }
}

function InitCPTransProsecution() {
    var statusPayment = $("#statuspayment-hidden").val();
    
    if (statusPayment != "3") {
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

function ResetFrmCPTransProsecutionWithDoc(doc) {
    var formatPayment = $("#format-payment-hidden").val();
    var statusPayment = $("#statuspayment-hidden").val();

    $("#" + doc + "-lawyer").html($("#" + doc + "-lawyer-hidden").val());

    if (doc == "complaint") {        
        var complaintCapital = ""

        if (formatPayment == "1")
            complaintCapital = $("#totalpenalty-hidden").val();

        if (formatPayment == "2")
            complaintCapital = (statusPayment == "1" ? $("#totalpenalty-hidden").val() : $("#totalremain-hidden").val());

        var complaintActionDate = $("#complaint-actiondate-hidden").val();

        if (complaintActionDate.length > 0) {
            $("#complaint-actiondate-y").removeClass("hidden");
            $("#complaint-actiondate").html(complaintActionDate);
            $("#complaint-actiondate-n").addClass("hidden");
        }
        else {
            $("#complaint-actiondate-y").addClass("hidden");
            $("#complaint-actiondate").html("");
            $("#complaint-actiondate-n").removeClass("hidden");
        }

        $("#complaint-blackcaseno").val($("#complaint-blackcaseno-hidden").val());
        $("#complaint-capital").val($("#complaint-capital-hidden").val().length > 0 ? $("#complaint-capital-hidden").val() : complaintCapital);
        $("#complaint-interest").val($("#complaint-interest-hidden").val());
        TextboxDisable("#complaint-capital");
        TextboxDisable("#complaint-totalpayment");
        CalTotalPayment();
    }

    if (doc == "judgment") {
        var judgmentActionDate = $("#judgment-actiondate-hidden").val();

        if (judgmentActionDate.length > 0) {
            $("#judgment-actiondate-y").removeClass("hidden");
            $("#judgment-actiondate").html(judgmentActionDate);
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

    if (doc == "execution") {
        var executionActionDate = $("#execution-actiondate-hidden").val();

        if (executionActionDate.length > 0) {
            $("#execution-actiondate-y").removeClass("hidden");
            $("#execution-actiondate").html(executionActionDate);
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

    if (doc == "executionwithdraw") {
        var executionWithdrawActionDate = $("#executionwithdraw-actiondate-hidden").val();

        if (executionWithdrawActionDate.length > 0) {
            $("#executionwithdraw-actiondate-y").removeClass("hidden");
            $("#executionwithdraw-actiondate").html(executionWithdrawActionDate);
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
    var complaintCapital = DelCommas("complaint-capital");
    var complaintInterest = ($("#complaint-interest").val().length > 0 ? DelCommas("complaint-interest") : "0.00");    
    var complaintTotalPayment = (parseFloat(complaintCapital) + parseFloat(complaintInterest));

    $("#complaint-totalpayment").val(complaintTotalPayment.toLocaleString());
}

function ValidateCPTransProsecution(doc) {
    var error = false;
    var msg;
    var focus;

    if (doc == "complaint") {
        var complaintBlackCaseNo = $("#complaint-blackcaseno").val();
        var complaintInterest = DelCommas("complaint-interest");

        if (error == false &&
            complaintBlackCaseNo.length == 0) {
            error = true;
            msg = "กรุณาใส่คำฟ้อง - คดีหมายเลขดำที่";
            focus = "#complaint-blackcaseno";
        }

        if (error == false &&
            complaintInterest.length == 0) {
            error = true;
            msg = "กรุณาใส่คำฟ้อง - ดอกเบี้ยถึงวันฟ้อง";
            focus = "#complaint-interest";
        }
    }

    if (doc == "judgment") {
        var judgmentRedCaseNo = $("#judgment-redcaseno").val();
        var judgmentResult = $("input[name=judgment-result]:checked");

        if (error == false &&
            judgmentRedCaseNo.length == 0) {
            error = true;
            msg = "กรุณาใส่คำพิพากษา - คดีหมายเลขแดงที่";
            focus = "#judgment-redcaseno";
        }

        if (error == false &&
            judgmentResult.length == 0) {
            error = true;
            msg = "กรุณาใส่คำพิพากษา - ผลคำพิพากษา";
            focus = "#judgment-input";
        }
    }

    if (doc == "execution") {
        var executionDate = $("#execution-date").val();

        if (error == false &&
            executionDate.length == 0) {
            error = true;
            msg = "กรุณาใส่หมายบังคับคดี - ลงวันที่";
            focus = "#execution-date";
        }
    }

    if (doc == "executionwithdraw") {
        var executionWithdrawDate = $("#executionwithdraw-date").val();

        if (error == false &&
            executionWithdrawDate.length == 0) {
            error = true;
            msg = "กรุณาใส่หมายถอนการบังคับคดี - ลงวันที่";
            focus = "#executionwithdraw-date";
        }
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function ConfirmActionCPTransProsecution(doc) {
    DialogConfirm("ต้องการบันทึกข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                if (ValidateCPTransProsecution(doc)) {
                    var send = new Array();
                    send[send.length] = ("cp2id=" + $("#cp2id").val());
                    send[send.length] = ("document=" + doc);
                    send[send.length] = (doc + "lawyerfullname=" + $("#" + doc + "-lawyer-fullname-hidden").val());
                    send[send.length] = (doc + "lawyerphonenumber=" + $("#" + doc + "-lawyer-phonenumber-hidden").val());
                    send[send.length] = (doc + "lawyermobilenumber=" + $("#" + doc + "-lawyer-mobilenumber-hidden").val());
                    send[send.length] = (doc + "lawyeremail=" + $("#" + doc + "-lawyer-email-hidden").val());

                    if (doc == "complaint") {
                        send[send.length] = ("complaintblackcaseno=" + $("#complaint-blackcaseno").val());
                        send[send.length] = ("complaintcapital=" + DelCommas("complaint-capital"));
                        send[send.length] = ("complaintinterest=" + ($("#complaint-interest").val() != "0.00" ? DelCommas("complaint-interest") : ""));
                    }

                    if (doc == "judgment") {
                        send[send.length] = ("judgmentredcaseno=" + $("#judgment-redcaseno").val());
                        send[send.length] = ("judgmentverdict=" + $("input[name=judgment-result]:checked").val());
                        send[send.length] = ("judgmentcopy=" + ($("#judgment-copy").val().length > 0 ? btoa($("#judgment-copy").val()) : ""));
                        send[send.length] = ("judgmentremark=" + $("#judgment-remark").val());

                    }

                    if (doc == "execution") {
                        send[send.length] = ("executiondate=" + $("#execution-date").val());
                        send[send.length] = ("executioncopy=" + ($("#execution-copy").val().length > 0 ? btoa($("#execution-copy").val()) : ""));
                    }

                    if (doc == "executionwithdraw") {
                        send[send.length] = ("executionwithdrawdate=" + $("#executionwithdraw-date").val());
                        send[send.length] = ("executionwithdrawreason=" + $("#executionwithdraw-reason").val());
                        send[send.length] = ("executionwithdrawcopy=" + ($("#executionwithdraw-copy").val().length > 0 ? btoa($("#executionwithdraw-copy").val()) : ""));
                    }

                    AddUpdateCPTransProsecution(send, doc);
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddUpdateCPTransProsecution(
    send,
    doc
) {
    var RCID = $("#RCID-hidden").val();
    var action = (RCID.length > 0 ? "update" : "add");

    AddUpdateData(action, (action + "cptransprosecution"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    var dt = new Date();
                    var thLongDate = (dt.getDate() + " " + monthNames[dt.getMonth()] + " " + (dt.getFullYear() + 543));

                    if (action == "add")
                        $("#RCID-hidden").val($("#cp2id").val());

                    $("#" + doc + "-lawyer-hidden").val($("#" + doc + "-lawyer-fullname-hidden").val());
                    
                    if (doc == "complaint") {
                        $("#complaint-blackcaseno-hidden").val($("#complaint-blackcaseno").val());
                        $("#complaint-capital-hidden").val($("#complaint-capital").val());
                        $("#complaint-interest-hidden").val($("#complaint-interest").val());
                        $("#complaint-actiondate-hidden").val(thLongDate);
                    }

                    if (doc == "judgment") {
                        $("#judgment-redcaseno-hidden").val($("#judgment-redcaseno").val());
                        $("#judgment-result-hidden").val($("input[name=judgment-result]:checked").val());
                        $("#judgment-copy-hidden").val($("#judgment-copy").val());
                        $("#judgment-remark-hidden").val($("#judgment-remark").val());
                        $("#judgment-actiondate-hidden").val(thLongDate);
                    }

                    if (doc == "execution") {
                        $("#execution-date-hidden").val($("#execution-date").val());
                        $("#execution-copy-hidden").val($("#execution-copy").val());
                        $("#execution-actiondate-hidden").val(thLongDate);
                    }

                    if (doc == "executionwithdraw") {
                        $("#executionwithdraw-date-hidden").val($("#executionwithdraw-date").val());
                        $("#executionwithdraw-reason-hidden").val($("#executionwithdraw-reason").val());
                        $("#executionwithdraw-copy-hidden").val($("#executionwithdraw-copy").val());
                        $("#executionwithdraw-actiondate-hidden").val(thLongDate);
                    }

                    GoToElement(doc);
                    ResetFrmCPTransProsecutionWithDoc(doc);
                }
            }
        });
    });
}