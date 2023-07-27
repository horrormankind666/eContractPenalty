function ResetFrmCPTransRepayContract(disable) {
    GoToElement("top-page");

    if (disable == true) {
        return;
    }

    $("#repay-date").val($("#repay-date-hidden").val());
    $("#pursuant").val($("#pursuant-hidden").val());
    $("#pursuant-book-date").val($("#pursuant-book-date-hidden").val());
    InitCalendar("#pursuant-book-date");

    if ($("#action").val() == "update") {
        var replyResult = $("input[name=reply-result]:radio");

        $("#reply-date").val($("#reply-date-hidden").val());        
        InitCalendarFromTo("#repay-date", false, "#reply-date", false);

        if ($("#previous-reply-date").length > 0)
            InitCalendarFromTo("#previous-reply-date", true, "#repay-date", false);

        replyResult.attr("checked", false);
        return;
    }
    
    if ($("#previous-reply-date").length > 0) {
        InitCalendarFromTo("#previous-reply-date", true, "#repay-date", false);
        return;
    }

    InitCalendar("#repay-date");
 }

function ResetFrmCalInterestOverpayment(disable) {
    GoToElement("top-page");

    if (disable == true) {
        return;
    }

    $("#overpayment-date-start").val($("#overpayment-date-start-hidden").val());
    $("#overpayment-date-end").val($("#overpayment-date-end-hidden").val());
    InitCalendarFromTo("#overpayment-date-start", false, "#overpayment-date-end", false);
    $("#overpayment-year").val($("#overpayment-year-hidden").val());
    TextboxDisable("#overpayment-year");
    $("#overpayment-day").val($("#overpayment-day-hidden").val());
    TextboxDisable("#overpayment-day");
    $("#overpayment-interest").val($("#overpayment-interest-hidden").val());
    $("#capital").val($("#capital-hidden").val());
    TextboxDisable("#capital");
    $("#total-interest-overpayment").val("");
    TextboxDisable("#total-interest-overpayment");
    $("#total-payment").val("");
    TextboxDisable("#total-payment");    
}

function SetStatusRepay() {
    var result = new Array();

    switch ($("#repaystatus-trans-repay-contract-hidden").val()) {
        case "0":
            result[0] = "0";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            break;
        case "1":
            result[0] = "1";
            result[1] = "1";
            result[2] = "";
            result[3] = "";
            break;
        case "2":
            result[0] = "1";
            result[1] = "2";
            result[2] = "1";
            result[3] = "";
            break;
        case "3":
            result[0] = "1";
            result[1] = "2";
            result[2] = "2";
            result[3] = "";
            break;
        case "4":
            result[0] = "2";
            result[1] = "1";
            result[2] = "";
            result[3] = "";
            break;
        case "5":
            result[0] = "2";
            result[1] = "2";
            result[2] = "1";
            result[3] = "";
            break;
        case "6":
            result[0] = "2";
            result[1] = "2";
            result[2] = "2";
            result[3] = "";
            break;
        case "7":
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "2";
            break;
        case "8":
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "3";
            break;
        default:
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            break;
    }

    return result;
}

function ConfirmActionCPTransRepayContract(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTransRepayContract(action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTransRepayContract(action) {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        $("#repay-date").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่แจังให้ผู้ผิดสัญญาชำระหนี้";
        focus = "#repay-date";
    }  

    if (action == "update") {
        var replyResult = $("input[name=reply-result]:checked");

        if (error == false &&
            (
                ($("#pursuant").val().length > 0 && $("#pursuant-book-date").val().length == 0) ||
                ($("#pursuant").val().length == 0 && $("#pursuant-book-date").val().length > 0)
            )) {
            error = true;
            msg = "กรุณาใส่ข้อมูลเกี่ยวกับหนังสือขอให้ชดใช้เงินให้ครบถ้วน";
            focus = "#pursuant";
        }

        if (error == false &&
            $("#reply-date").val().length > 0 &&
            replyResult.length > 0 &&
            $("#pursuant").val().length == 0 &&
            $("#pursuant-book-date").val().length == 0) {
            error = true;
            msg = "กรุณาใส่ข้อมูลเกี่ยวกับหนังสือขอให้ชดใช้เงิน";
            focus = "#pursuant";
        }

        if (error == false &&
            $("#reply-date").val().length == 0 &&
            replyResult.length > 0) {
            error = true;
            msg = "กรุณาใส่วันที่รับเอกสารตอบกลับจากไปรษณีย์";
            focus = "#reply-date";
        }

        if (error == false &&
            $("#reply-date").val().length > 0 &&
            replyResult.length == 0) {
            error = true;
            msg = "กรุณาใส่ผลการรับทราบการแจ้งชำระหนี้";
            focus = "#reply-yes-input";
        }
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var send = new Array();
    send[send.length] = ("cp2id=" + $("#cp2id").val());
    send[send.length] = ("statusrepay=" + $("#status-repay-hidden").val());
    send[send.length] = ("repaydate=" + $("#repay-date").val());
    send[send.length] = ("pursuant=" + $("#pursuant").val());
    send[send.length] = ("pursuantbookdate=" + $("#pursuant-book-date").val());

    if (action == "update") {
        send[send.length] = ("replydate=" + $("#reply-date").val());

        var valCheck = "";

        if (replyResult.length > 0) {
            replyResult.each(function (i) {
                valCheck = this.value;
            });
        }

        send[send.length] = ("replyresult=" + valCheck);
    }

    AddUpdateData(action, (action + "cptransrepaycontract"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    $("#dialog-form2").dialog("close");
                    $("#dialog-form1").dialog("close");
                    OpenTab("link-tab2-cp-trans-require-contract", "#tab2-cp-trans-require-contract", "", true, "", "", "");        
                }
            }
        });
    });
}

function ChkRepayStatusCalInterest(
    cp2id,
    callbackFunc
) {
    var send = new Array();
    send[send.length] = ("cp2id=" + cp2id);

    SetMsgLoading("");

    ViewData("repaystatuscalinterest", send, function (result) {
        var error = false;
        var msg;

        if (error == false &&
            result == "1") {
            error = true;
            msg = "ยังไม่ได้แจ้งชำระหนี้";
        }

        if (error == false &&
            result == "2") {
            error = true;
            msg = "อยู่ระหว่างการชำระหนี้";
        }

        if (error == false &&
            result == "3") {
            error = true;
            msg = "ชำระหนี้เรียบร้อยแล้ว";
        }

        if (error == false &&
            result == "4") {
            error = true;
            msg = "กำลังรอเอกสารตอบกลับ";
        }

        if (error == false &&
            result == "5") {
            error = true;
            msg = "ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้";
        }

        if (error == false &&
            result == "6") {
            error = true;
            msg = "กำลังรอเอกสารตอบกลับ";
        }

        if (error == false &&
            result == "7") {
            error = true;
            msg = "ยังไม่ผิดนัดชำระ";
        }        

        if (error == true) {
            DialogMessage(msg, "", false, "");
            callbackFunc(result);
            return;
        }

        callbackFunc(result);
    });
}

function ViewCalInterestOverpayment(cp2id) {
    ChkRepayStatusCalInterest(cp2id, function (result) {
        if (result == "0")
            LoadForm(2, "calinterest", true, "", cp2id, "");
    });
}