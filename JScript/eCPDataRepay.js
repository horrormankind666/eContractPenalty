function ResetFrmCPTransRepayContract(_disable)
{
    GoToElement("top-page");

    if (_disable == true)
    {
        return;
    }

    $("#repay-date").val($("#repay-date-hidden").val());
    if ($("#action").val() == "update")
    {
        var _replyResult = $("input[name=reply-result]:radio");

        $("#reply-date").val($("#reply-date-hidden").val());        
        InitCalendarFromTo("#repay-date", false, "#reply-date", false);
        if ($("#previous-reply-date").length > 0) InitCalendarFromTo("#previous-reply-date", true, "#repay-date", false);
        _replyResult.attr("checked", false);
        return;
    }
    
    if ($("#previous-reply-date").length > 0)
    {
        InitCalendarFromTo("#previous-reply-date", true, "#repay-date", false);
        return;
    }

    InitCalendar("#repay-date");
}

function ResetFrmCalInterestOverpayment(_disable)
{
    GoToElement("top-page");

    if (_disable == true)
    {
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

function SetStatusRepay()
{
    var _result = new Array();

    switch ($("#repaystatus-trans-repay-contract-hidden").val())
    {
        case "0": { _result[0] = "0"; _result[1] = ""; _result[2] = ""; _result[3] = ""; break; }
        case "1": { _result[0] = "1"; _result[1] = "1"; _result[2] = ""; _result[3] = ""; break; }
        case "2": { _result[0] = "1"; _result[1] = "2"; _result[2] = "1"; _result[3] = ""; break; }
        case "3": { _result[0] = "1"; _result[1] = "2"; _result[2] = "2"; _result[3] = ""; break; }
        case "4": { _result[0] = "2"; _result[1] = "1"; _result[2] = ""; _result[3] = ""; break; }
        case "5": { _result[0] = "2"; _result[1] = "2"; _result[2] = "1"; _result[3] = ""; break; }
        case "6": { _result[0] = "2"; _result[1] = "2"; _result[2] = "2"; _result[3] = ""; break; }
        case "7": { _result[0] = ""; _result[1] = ""; _result[2] = ""; _result[3] = "2"; break; }
        case "8": { _result[0] = ""; _result[1] = ""; _result[2] = ""; _result[3] = "3"; break; }
        default : { _result[0] = ""; _result[1] = ""; _result[2] = ""; _result[3] = ""; break; }
    }

    return _result;
}

function ConfirmActionCPTransRepayContract(_action)
{
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTransRepayContract(_action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTransRepayContract(_action)
{
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ($("#repay-date").val().length == 0)) { _error = true; _msg = "กรุณาใส่วันที่แจังให้ผู้ผิดสัญญาชำระหนี้"; _focus = "#repay-date"; }
    if (_action == "update")
    {
        var _replyResult = $("input[name=reply-result]:checked");

        if (_error == false && ($("#reply-date").val().length == 0 && _replyResult.length > 0)) { _error = true; _msg = "กรุณาใส่วันที่รับเอกสารตอบกลับจากไปรษณีย์"; _focus = "#reply-date"; }
        if (_error == false && ($("#reply-date").val().length > 0 && _replyResult.length == 0)) { _error = true; _msg = "กรุณาใส่ผลการรับทราบการแจ้งชำระหนี้"; _focus = "#reply-yes-input"; }
    }

    if (_error == true)
    {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _send = new Array();
    _send[0] = "cp2id=" + $("#cp2id").val();
    _send[1] = "statusrepay=" + $("#status-repay-hidden").val();
    _send[2] = "repaydate=" + $("#repay-date").val();

    if (_action == "update")
    {
        _send[3] = "replydate=" + $("#reply-date").val();

        var _valCheck = "";

        if (_replyResult.length > 0)
        {
            _replyResult.each(function (i) {
                _valCheck = this.value;
            });
        }

        _send[4] = "replyresult=" + _valCheck;
    }

    AddUpdateData(_action, _action + "cptransrepaycontract", _send, false, "", "", "", false, function (_result) {
        if (_result == "1")
        {
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

function ChkRepayStatusCalInterest(_cp2id, _callbackFunc)
{
    var _send = new Array();
    _send[0] = "cp2id=" + _cp2id;

    SetMsgLoading("");

    ViewData("repaystatuscalinterest", _send, function (_result) {
        var _error = false;
        var _msg;

        if (_error == false && _result == "1") { _error = true; _msg = "ยังไม่ได้แจ้งชำระหนี้"; }
        if (_error == false && _result == "2") { _error = true; _msg = "อยู่ระหว่างการชำระหนี้"; }
        if (_error == false && _result == "3") { _error = true; _msg = "ชำระหนี้เรียบร้อยแล้ว"; }
        if (_error == false && _result == "4") { _error = true; _msg = "กำลังรอเอกสารตอบกลับ"; }
        if (_error == false && _result == "5") { _error = true; _msg = "ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้"; }
        if (_error == false && _result == "6") { _error = true; _msg = "กำลังรอเอกสารตอบกลับ"; }
        if (_error == false && _result == "7") { _error = true; _msg = "ยังไม่ผิดนัดชำระ"; }        

        if (_error == true)
        {
            DialogMessage(_msg, "", false, "");
            _callbackFunc(_result);
            return;
        }

        _callbackFunc(_result);
    });
}

function ViewCalInterestOverpayment(_cp2id)
{
    ChkRepayStatusCalInterest(_cp2id, function (_result) {
        if (_result == "0") LoadForm(2, "calinterest", true, "", _cp2id, "");
    });
}