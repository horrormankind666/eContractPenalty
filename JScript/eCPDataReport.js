function ResetFrmCalReportTableCalCapitalAndInterest() {
    $("#capital").val($("#capital-hidden").val());
    $("#interest").val($("#interest-hidden").val());
    InitCombobox("condition-tablecalcapitalandinterest", "0", "0", 180, 205);
    $("#pay-period").val("");
    TextboxDisable("#pay-period");
    $("#pay").val("");
    $("#condition-select-1").hide();
    $("#period").val("");
    $("#condition-select-2").hide();
    $("#payment-date").val("");
    InitCalendar("#payment-date");
    $("#record-count-cal-table-cal-capital-and-interest").html("ทั้งหมด 0 งวด");
    $("#list-table-cal-capital-and-interest").html("");
    $("#sum-pay-capital").html("");
    $("#sum-pay-interest").html("");
    $("#sum-total-pay").html("");

    /*
    $("#list-table-cal-capital-and-interest").slimScroll({
        height: "300px",
        alwaysVisible: false,
        start: "top",
        wheelStep: 10,
        size: "10px",
        color: "#FFCC00",
        distance: "0px",
        railVisible: true,
        railColor: "#222",
        railOpacity: 0.3
    });
    */
}

function ValidateCalCPReportTableCalCapitalAndInterest() {
    var _error = false;
    var _msg;
    var _focus;
    var _capital = DelCommas("capital");
    var _interest = DelCommas("interest");
    var _pay = DelCommas("pay");
    var _period = DelCommas("period");
    var _paymentDate = $("#payment-date").val();
    var _payLeast = DelCommas("pay-least-hidden");
    var _periodLeast = DelCommas("period-least-hidden");

    if (_error == false && (_capital.length == 0 || _capital == "0.00")) { _error = true; _msg = "กรุณาใส่จำนวนเงินต้นคงเหลือยกมา"; _focus = "#capital"; }
    if (_error == false && (_interest.length == 0 || _interest == "0.00")) { _error = true; _msg = "กรุณาใส่อัตราดอกเบี้ย"; _focus = "#interest"; }
    if (_error == false && parseFloat(_interest) > 100) { _error = true; _msg = "กรุณาใส่อัตราดอกเบี้ยไม่เกิน 100"; _focus = "#interest"; }
    if (_error == false && ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "0") { _error = true; _msg = "กรุณาเลือกเงื่อนไขที่ใช้คำนวณ"; _focus = "#condition-tablecalcapitalandinterest"; }
    if (_error == false && (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" && (_pay.length == 0 || _pay == "0.00"))) { _error = true; _msg = "กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือน"; _focus = "#pay"; }
    if (_error == false && (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" && (_period.length == 0 || _period == "0"))) { _error = true; _msg = "กรุณาใส่จำนวนงวดที่ต้องการชำระ"; _focus = "#pay"; }
    if (_error == false && (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" && (parseFloat(_capital) - parseFloat(_pay)) < 0)) { _error = true; _msg = "<div>กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือนให้น้อยกว่าหรือเท่ากับ</div><div>จำนวนเงินต้นคงเหลือยกมา</div>"; _focus = "#pay"; }
    if (_error == false && (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" && (parseFloat(_pay) < parseFloat(_payLeast)))) { _error = true; _msg = "<div>กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือนไม่น้อยกว่า<div><div>" + $("#pay-least-hidden").val() + " บาท</div>"; _focus = "#pay"; }
    if (_error == false && (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" && (parseInt(_period) > parseInt(_periodLeast)))) { _error = true; _msg = "<div>กรุณาใส่จำนวนงวดที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ<div><div>" + $("#period-least-hidden").val() + " งวด</div>"; _focus = "#period"; }
    if (_error == false && _paymentDate.length == 0) { _error = true; _msg = "กรุณาใส่วันที่เริ่มชำระ"; _focus = "#payment-date"; }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return false;
    }

    return true;
}

function CalReportTableCalCapitalAndInterest() {
    $("#capital-old").val(DelCommas("capital"));
    $("#interest-old").val(DelCommas("interest"));
    if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1") $("#pay-old").val(DelCommas("pay"));
    if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2") $("#period-old").val(DelCommas("period"));
    $("#payment-date-old").val($("#payment-date").val());

    if (ValidateCalCPReportTableCalCapitalAndInterest() == true) {
        var _pay = ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" ? (parseFloat(DelCommas("period")) == 1 ? DelCommas("capital") : (parseFloat((parseFloat(DelCommas("capital")) / parseFloat(DelCommas("period"))).toFixed(0)) + 1).toFixed(2)) : DelCommas("pay");
        var _send = new Array();
        _send[_send.length] = "capital=" + DelCommas("capital");
        _send[_send.length] = "interest=" + DelCommas("interest");
        _send[_send.length] = "pay=" + _pay;
        _send[_send.length] = "paymentdate=" + $("#payment-date").val();

        SetMsgLoading("กำลังคำนวณ");

        CalculateFrm("reporttablecalcapitalandinterest", _send, function (_result) {
            var _dataRecordCount = _result.split("<recordcount>");
            var _dataList = _result.split("<list>");
            var _dataSumPayCapital = _result.split("<sumpaycapital>");
            var _dataSumPayInterest = _result.split("<sumpayinterest>");
            var _dataSumTotalPay = _result.split("<sumtotalpay>");

            $("#record-count-cal-table-cal-capital-and-interest").html("ทั้งหมด " + _dataRecordCount[1] + " งวด");
            $("#list-table-cal-capital-and-interest").html(_dataList[1]);
            $("#sum-pay-capital").html(_dataSumPayCapital[1]);
            $("#sum-pay-interest").html(_dataSumPayInterest[1]);
            $("#sum-total-pay").html(_dataSumTotalPay[1]);
        });
    }
}

function ValidateExportCPReportTableCalCapitalAndInterest() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && $("#list-table-cal-capital-and-interest").html().length == 0) { _error = true; _msg = "กรุณาคำนวณเงินต้นและดอกเบี้ย"; _focus = ""; }
    if (_error == false && ($("#capital-old").val() != DelCommas("capital"))) { _error = true; _msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง"; _focus = ""; }
    if (_error == false && ($("#interest-old").val() != DelCommas("interest"))) { _error = true; _msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง"; _focus = ""; }
    if (_error == false && ($("#pay-old").val() != DelCommas("pay"))) { _error = true; _msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง"; _focus = ""; }
    if (_error == false && ($("#payment-date-old").val() != $("#payment-date").val())) { _error = true; _msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง"; _focus = ""; }    

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return false;
    }
    
    return true;
}

function ExportReportTableCalCapitalAndInterest() {    
    if (ValidateExportCPReportTableCalCapitalAndInterest() == true) {
        var _pay = ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" ? (parseFloat(DelCommas("period")) == 1 ? DelCommas("capital") : (parseFloat((parseFloat(DelCommas("capital")) / parseFloat(DelCommas("period"))).toFixed(0)) + 1).toFixed(2)) : DelCommas("pay");
        var _send = new Array();
        _send[_send.length] = $("#cp2id").val();
        _send[_send.length] = DelCommas("capital");
        _send[_send.length] = DelCommas("interest");
        _send[_send.length] = _pay;
        _send[_send.length] = $("#payment-date").val();

        Printing(_send.join("&"), "reporttablecalcapitalandinterest", "pdf", "eCPPrinting.aspx", "export-target");
    }
}

function ViewReportStepOfWorkOnStatisticRepayByProgram(_order, _facultyCode, _facultyName, _programCode, _programName, _majorCode, _groupNum) {
    var _facultyName = _facultyName.replace("&", " ");
    var _programName = _programName.replace("&", " ")

    $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val(_facultyCode + ";" + _facultyName);
    $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val(_programCode + ";" + _programName + ";" + _majorCode + ";" + _groupNum);
    LoadForm(1, "reportstepofworkonstatisticrepaybyprogram", true, "", "", "report-statistic-repay-by-program" + _order);
}

function ViewReportStatisticRepayByProgram(_acadamicyear) {
    $("#acadamicyear-hidden").val(_acadamicyear);
    OpenTab("link-tab2-cp-report-statistic-repay", "#tab2-cp-report-statistic-repay", "สถิติการชำระหนี้ปีการศึกษา 25" + _acadamicyear + " แยกตามหลักสูตร", false, "", _acadamicyear, "");
}

function ViewReportStatisticContractByProgram(_acadamicyear) {
    $("#acadamicyear-hidden").val(_acadamicyear);
    OpenTab("link-tab2-cp-report-statistic-contract", "#tab2-cp-report-statistic-contract", "สถิติการทำสัญญาและการผิดสัญญาปีการศึกษา 25" + _acadamicyear + " แยกตามหลักสูตร", false, "", _acadamicyear, "");
}

function ViewReportStudentOnStatisticContractContractByProgram(_order, _facultyCode, _facultyName, _programCode, _programName, _majorCode, _groupNum) {
    var _facultyName = _facultyName.replace("&", " ");
    var _programName = _programName.replace("&", " ")

    $("#faculty-report-student-on-statistic-contract-by-program-hidden").val(_facultyCode + ";" + _facultyName);
    $("#program-report-student-on-statistic-contract-by-program-hidden").val(_programCode + ";" + _programName + ";" + _majorCode + ";" + _groupNum);
    LoadForm(1, "reportstudentonstatisticcontractbyprogram", true, "", "", "report-statistic-contract-by-program" + _order);
}

function ConfirmPrintNoticeRepayComplete() {
    DialogConfirm("ต้องการพิมพ์หนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณาหรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                PrintNoticeRepayComplete();
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function PrintNoticeCheckForReimbursement(_cp1id, _action) {
    var _send = new Array();
    _send[_send.length] = _cp1id;
    _send[_send.length] = _action;

    Printing(_send.join(":"), "reportnoticecheckforreimbursement", "pdf", "eCPPrinting.aspx", "export-target");
}

function PrintNoticeRepayComplete() {
    _elem = $("input[name=print-notice-repay-complete]:checked");
    _countSend = _elem.length;

    if (_countSend == 0) {
        DialogMessage("ไม่พบรายการที่ต้องการพิมพ์", "", false, "");
        return;
    }

    var _valCheck = new Array();

    _elem.each(function (i) {
        _valCheck[i] = this.value;
    });

    Printing(_valCheck.join(";"), "reportnoticerepaycomplete", "word", "eCPPrinting.aspx", "export-target");
}

function PrintNoticeClaimDebt(_cp1id, _time, _previousRepayDateEnd) {
    var _send = new Array();
    _send[_send.length] = _cp1id;
    _send[_send.length] = _time;
    _send[_send.length] = _previousRepayDateEnd

    Printing(_send.join(":"), "reportnoticeclaimdebt", "word", "eCPPrinting.aspx", "export-target");
}

function Printing(_send, _order, _type, _frmAction, _frmTarget) {
    DialogLoading("กำลังโหลด...");

    $("#export-send").val(_send);
    $("#export-order").val(_order);
    $("#export-type").val(_type);
    $("#export-setvalue").attr("action", _frmAction);
    $("#export-setvalue").attr("target", _frmTarget);
    $("#export-setvalue").submit();

    window.setTimeout(function () {
        $("#dialog-loading").dialog("close");
    }, 500);
}

function ViewReportDebtorContractByProgram(_facultyCode, _facultyName, _programCode, _programName, _majorCode, _groupNum, _dLevel, _dLevelname) {
    var _facultyName = _facultyName.replace("&", " ");
    var _programName = _programName.replace("&", " ")

    $("#faculty-report-debtor-contract-by-program-hidden").val(_facultyCode + ";" + _facultyName);
    $("#program-report-debtor-contract-by-program-hidden").val(_programCode + ";" + _programName + ";" + _majorCode + ";" + _groupNum);
    OpenTab("link-tab2-cp-report-debtor-contract", "#tab2-cp-report-debtor-contract", "ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้ หลักสูตร " + _programCode + " - " + _programName + (_groupNum != "0" ? " ( กลุ่ม " + _groupNum + " )" : ""), false, "", "", "");
}

function ViewTransPaymentReportDebtorContractByProgram(_cp2id) {
    LoadForm(3, "viewtranspayment", true, "", (_cp2id + ":" + ($("#date-start-report-debtor-contract-hidden").length > 0 ? $("#date-start-report-debtor-contract-hidden").val() : "") + ":" + ($("#date-end-report-debtor-contract-hidden").length > 0 ? $("#date-end-report-debtor-contract-hidden").val() : "")), "")
}

function PrintDebtorContract(_tabOrder) {
    var _error = false;
    var _msg;

    if (_error == false && _tabOrder == 1 && $("#list-data-report-debtor-contract").html().length == 0) { _error = true; _msg = "ไม่พบรายการที่ต้องการพิมพ์"; }
    if (_error == false && _tabOrder == 2 && $("#list-data-report-debtor-contract-by-program").html().length == 0) { _error = true; _msg = "ไม่พบรายการที่ต้องการพิมพ์"; }

    if (_error == true) {
        DialogMessage(_msg, "", false, "");
        return;
    }
    
    var _reportOrder = $("#report-debtor-contract-order").val();
    var _faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val() : "";
    var _program = $("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val() : "";
    var _formatPayment = $("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val() : "";
    var _send = new Array();
    _send[_send.length] = $("#date-start-report-debtor-contract-hidden").val();
    _send[_send.length] = $("#date-end-report-debtor-contract-hidden").val();
    _send[_send.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    _send[_send.length] = _faculty;
    _send[_send.length] = _program;
    _send[_send.length] = _formatPayment;
    
    Printing(_send.join(":"), _reportOrder, "excel", "eCPPrinting.aspx", "export-target");
}

function PrintCertificateReimbursement(_cp2id) {
    var _send = new Array();
    _send[_send.length] = _cp2id;

    Printing(_send.join(":"), "reportcertificatereimbursement", "word", "eCPPrinting.aspx", "export-target");
}