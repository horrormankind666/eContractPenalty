function ResetFrmCalReportTableCalCapitalAndInterest() {
    GoToTopElement("html, body");

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
    var error = false;
    var msg;
    var focus;
    var capital = DelCommas("capital");
    var interest = DelCommas("interest");
    var pay = DelCommas("pay");
    var period = DelCommas("period");
    var paymentDate = $("#payment-date").val();
    var payLeast = DelCommas("pay-least-hidden");
    var periodLeast = DelCommas("period-least-hidden");

    if (error == false &&
        (capital.length == 0 || capital == "0.00")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินต้นคงเหลือยกมา";
        focus = "#capital";
    }

    if (error == false &&
        (interest.length == 0 || interest == "0.00")) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ย";
        focus = "#interest";
    }

    if (error == false &&
        parseFloat(interest) > 100) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ยไม่เกิน 100";
        focus = "#interest";
    }

    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "0") {
        error = true;
        msg = "กรุณาเลือกเงื่อนไขที่ใช้คำนวณ";
        focus = "#condition-tablecalcapitalandinterest";
    }

    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" &&
        (pay.length == 0 || pay == "0.00")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือน";
        focus = "#pay";
    }

    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" &&
        (period.length == 0 || period == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนงวดที่ต้องการชำระ";
        focus = "#pay";
    }

    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" &&
        (parseFloat(capital) - parseFloat(pay)) < 0) {
        error = true;
        msg = "<div>กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือนให้น้อยกว่าหรือเท่ากับ</div><div>จำนวนเงินต้นคงเหลือยกมา</div>";
        focus = "#pay";
    }

    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1" &&
        parseFloat(pay) < parseFloat(payLeast)) {
        error = true;
        msg = ("<div>กรุณาใส่จำนวนเงินต้นที่ต้องการชำระต่อเดือนไม่น้อยกว่า<div><div>" + $("#pay-least-hidden").val() + " บาท</div>");
        focus = "#pay";

    }
    if (error == false &&
        ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" &&
        parseInt(period) > parseInt(periodLeast)) {
        error = true;
        msg = ("<div>กรุณาใส่จำนวนงวดที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ<div><div>" + $("#period-least-hidden").val() + " งวด</div>");
        focus = "#period";
    }

    if (error == false &&
        paymentDate.length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่เริ่มชำระ";
        focus = "#payment-date";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalReportTableCalCapitalAndInterest() {
    $("#capital-old").val(DelCommas("capital"));
    $("#interest-old").val(DelCommas("interest"));

    if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "1")
        $("#pay-old").val(DelCommas("pay"));

    if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2")
        $("#period-old").val(DelCommas("period"));

    $("#payment-date-old").val($("#payment-date").val());

    if (ValidateCalCPReportTableCalCapitalAndInterest() == true) {
        var pay = (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" ? (parseFloat(DelCommas("period") == 1 ? DelCommas("capital") : (parseFloat((parseFloat(DelCommas("capital")) / parseFloat(DelCommas("period"))).toFixed(0)) + 1).toFixed(2))) : DelCommas("pay"));
        var send = new Array();
        send[send.length] = ("capital=" + DelCommas("capital"));
        send[send.length] = ("interest=" + DelCommas("interest"));
        send[send.length] = ("pay=" + pay);
        send[send.length] = ("paymentdate=" + $("#payment-date").val());

        SetMsgLoading("กำลังคำนวณ");

        CalculateFrm("reporttablecalcapitalandinterest", send, function (result) {
            var dataRecordCount = result.split("<recordcount>");
            var dataList = result.split("<list>");
            var dataSumPayCapital = result.split("<sumpaycapital>");
            var dataSumPayInterest = result.split("<sumpayinterest>");
            var dataSumTotalPay = result.split("<sumtotalpay>");

            $("#record-count-cal-table-cal-capital-and-interest").html("ทั้งหมด " + dataRecordCount[1] + " งวด");
            $("#list-table-cal-capital-and-interest").html(dataList[1]);
            $("#sum-pay-capital").html(dataSumPayCapital[1]);
            $("#sum-pay-interest").html(dataSumPayInterest[1]);
            $("#sum-total-pay").html(dataSumTotalPay[1]);
        });
    }
}

function ValidateExportCPReportTableCalCapitalAndInterest() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        $("#list-table-cal-capital-and-interest").html().length == 0) {
        error = true;
        msg = "กรุณาคำนวณเงินต้นและดอกเบี้ย";
        focus = "";
    }

    if (error == false &&
        $("#capital-old").val() != DelCommas("capital")) {
        error = true;
        msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง";
        focus = "";
    }

    if (error == false &&
        $("#interest-old").val() != DelCommas("interest")) {
        error = true;
        msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง";
        focus = "";
    }

    if (error == false &&
        $("#pay-old").val() != DelCommas("pay")) {
        error = true;
        msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง";
        focus = "";
    }

    if (error == false &&
        $("#payment-date-old").val() != $("#payment-date").val()) {
        error = true;
        msg = "กรุณาคำนวณเงินต้นและดอกเบี้ยใหม่อีกครั้ง";
        focus = "";
    }    

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }
    
    return true;
}

function ExportReportTableCalCapitalAndInterest() {    
    if (ValidateExportCPReportTableCalCapitalAndInterest() == true) {
        var pay = (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "2" ? (parseFloat(DelCommas("period")) == 1 ? DelCommas("capital") : (parseFloat((parseFloat(DelCommas("capital")) / parseFloat(DelCommas("period"))).toFixed(0)) + 1).toFixed(2)) : DelCommas("pay"));
        var send = new Array();
        send[send.length] = $("#cp2id").val();
        send[send.length] = DelCommas("capital");
        send[send.length] = DelCommas("interest");
        send[send.length] = pay;
        send[send.length] = $("#payment-date").val();
        
        Printing(send.join("&"), "reporttablecalcapitalandinterest", "pdf", "eCPPrinting.aspx", "export-target");
    }
}

function ViewReportStepOfWorkOnStatisticRepayByProgram(
    order,
    facultyCode,
    facultyName,
    programCode,
    programName,
    majorCode,
    groupNum
) {
    var facultyName = facultyName.replace("&", " ");
    var programName = programName.replace("&", " ")

    $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val(facultyCode + ";" + facultyName);
    $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val(programCode + ";" + programName + ";" + majorCode + ";" + groupNum);
    LoadForm(1, "reportstepofworkonstatisticrepaybyprogram", true, "", "", "report-statistic-repay-by-program" + order);
}

function ViewReportStatisticRepayByProgram(acadamicyear) {
    $("#acadamicyear-hidden").val(acadamicyear);
    OpenTab("link-tab2-cp-report-statistic-repay", "#tab2-cp-report-statistic-repay", ("สถิติการชำระหนี้ปีการศึกษา 25" + acadamicyear + " แยกตามหลักสูตร"), false, "", acadamicyear, "");
}

function ViewReportStatisticContractByProgram(acadamicyear) {
    $("#acadamicyear-hidden").val(acadamicyear);
    OpenTab("link-tab2-cp-report-statistic-contract", "#tab2-cp-report-statistic-contract", ("สถิติการทำสัญญาและการผิดสัญญาปีการศึกษา 25" + acadamicyear + " แยกตามหลักสูตร"), false, "", acadamicyear, "");
}

function ViewReportStudentOnStatisticContractContractByProgram(
    order,
    facultyCode,
    facultyName,
    programCode,
    programName,
    majorCode,
    groupNum
) {
    var facultyName = facultyName.replace("&", " ");
    var programName = programName.replace("&", " ")

    $("#faculty-report-student-on-statistic-contract-by-program-hidden").val(facultyCode + ";" + facultyName);
    $("#program-report-student-on-statistic-contract-by-program-hidden").val(programCode + ";" + programName + ";" + majorCode + ";" + groupNum);
    LoadForm(1, "reportstudentonstatisticcontractbyprogram", true, "", "", "report-statistic-contract-by-program" + order);
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

function PrintNoticeCheckForReimbursement(
    cp1id,
    action
) {
    var send = new Array();
    send[send.length] = cp1id;
    send[send.length] = action;

    Printing(send.join(":"), "reportnoticecheckforreimbursement", "pdf", "eCPPrinting.aspx", "export-target");
}

function PrintNoticeRepayComplete() {
    elem = $("input[name=print-notice-repay-complete]:checked");
    countSend = elem.length;

    if (countSend == 0) {
        DialogMessage("ไม่พบรายการที่ต้องการพิมพ์", "", false, "");
        return;
    }

    var valCheck = new Array();

    elem.each(function (i) {
        valCheck[i] = this.value;
    });

    Printing(valCheck.join(";"), "reportnoticerepaycomplete", "word", "eCPPrinting.aspx", "export-target", true);
}

function PrintNoticeClaimDebt(
    cp1id,
    time,
    previousRepayDateEnd
) {
    var send = new Array();
    send[send.length] = cp1id;
    send[send.length] = time;
    send[send.length] = previousRepayDateEnd

    Printing(send.join(":"), "reportnoticeclaimdebt", "word", "eCPPrinting.aspx", "export-target", true);
}

function Printing(
    send,
    order,
    type,
    frmAction,
    frmTarget,
    processWatch = false
) {
    console.log(order);
    $("#export-target").contents().find("body").html("");
    $("#export-send").val(send);
    $("#export-order").val(order);
    $("#export-type").val(type);
    $("#export-setvalue").attr("action", frmAction);
    $("#export-setvalue").attr("target", frmTarget);
    $("#export-setvalue").submit();

    DialogLoading("กำลังส่งออก...");

    var timeWatchExportComplete = setInterval(() => {
        DoWatchExportComplete();
    }, 1000);

    function DoWatchExportComplete() {
        if (processWatch == true) {
            var iframeBody = $("#export-target").contents().find("body");

            if (iframeBody.html() != null &&
                iframeBody.html().length > 0) {
                clearInterval(timeWatchExportComplete);

                var filename = iframeBody.find(".filename").html();
                var contenttype = iframeBody.find(".contenttype").html();

                iframeBody.find(".filename").remove();
                iframeBody.find(".contenttype").remove();

                var content = encodeURIComponent(iframeBody.html());
                var element = document.createElement("a");
                element.setAttribute("href", ("data:" + contenttype) + "," + content);
                element.setAttribute("download", filename);
                element.style.display = "none";

                element.click();
                element.remove();

                window.setTimeout(function () {
                    $("#dialog-loading").dialog("close");
                }, 500);
            }
        }
        else {
            clearInterval(timeWatchExportComplete);

            window.setTimeout(function () {
                $("#dialog-loading").dialog("close");
            }, 500);
        }        
    }
}

function ViewReportDebtorContractByProgram(
    facultyCode,
    facultyName,
    programCode,
    programName,
    majorCode,
    groupNum,
    dLevel,
    dLevelname
) {
    var facultyName = facultyName.replace("&", " ");
    var programName = programName.replace("&", " ")

    $("#faculty-report-debtor-contract-by-program-hidden").val(facultyCode + ";" + facultyName);
    $("#program-report-debtor-contract-by-program-hidden").val(programCode + ";" + programName + ";" + majorCode + ";" + groupNum);
    OpenTab("link-tab2-cp-report-debtor-contract", "#tab2-cp-report-debtor-contract", ("ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้ หลักสูตร " + programCode + " - " + programName + (groupNum != "0" ? (" ( กลุ่ม " + groupNum + " )") : "")), false, "", "", "");
}

function ViewTransPaymentReportDebtorContractByProgram(cp2id) {
    LoadForm(3, "viewtranspayment", true, "", (cp2id + ":" + ($("#date-start-report-debtor-contract-hidden").length > 0 ? $("#date-start-report-debtor-contract-hidden").val() : "") + ":" + ($("#date-end-report-debtor-contract-hidden").length > 0 ? $("#date-end-report-debtor-contract-hidden").val() : "")), "")
}

function PrintDebtorContract(tabOrder) {
    var error = false;
    var msg;

    if (error == false &&
        tabOrder == 1 &&
        $("#list-data-report-debtor-contract").html().length == 0) {
        error = true;
        msg = "ไม่พบรายการที่ต้องการส่งออก";
    }

    if (error == false &&
        tabOrder == 2 &&
        $("#list-data-report-debtor-contract-by-program").html().length == 0) {
        error = true;
        msg = "ไม่พบรายการที่ต้องการส่งออก";
    }

    if (error == true) {
        DialogMessage(msg, "", false, "");
        return;
    }
    
    var reportOrder = $("#report-debtor-contract-order").val();
    var faculty = ($("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val() : "");
    var program = ($("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val() : "");
    var formatPayment = ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val() : "");
    var send = new Array();
    send[send.length] = $("#date-start-report-debtor-contract-hidden").val();
    send[send.length] = $("#date-end-report-debtor-contract-hidden").val();
    send[send.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    send[send.length] = faculty;
    send[send.length] = program;
    send[send.length] = formatPayment;
    
    Printing(send.join(":"), reportOrder, "excel", "eCPPrinting.aspx", "export-target", true);
}

function PrintCertificateReimbursement(cp2id) {
    var send = new Array();
    send[send.length] = cp2id;

    Printing(send.join(":"), "reportcertificatereimbursement", "word", "eCPPrinting.aspx", "export-target", true);
}

function PrintFormRequestCreateAndUpdateDebtor(cp1id) {
    var send = new Array();
    send[send.length] = cp1id;

    Printing(send.join(":"), "reportformrequestcreateandupdatedebtor", "excel", "eCPPrinting.aspx", "export-target");
}

function PrintDebtorContractBreakRequireRepayPayment() {
    var error = false;
    var msg;

    if (error == false &&
        $("#list-data-trans-payment").html().length == 0) {
        error = true;
        msg = "ไม่พบรายการที่ต้องการส่งออก";
    }

    if (error == true) {
        DialogMessage(msg, "", false, "");
        return;
    }
    
    var send = new Array();
    send[send.length] = $("#paymentstatus-trans-payment-hidden").val();
    send[send.length] = $("#paymentrecordstatus-trans-payment-hidden").val();
    send[send.length] = $("#id-name-trans-payment-hidden").val();
    send[send.length] = $("#faculty-trans-payment-hidden").val();
    send[send.length] = $("#program-trans-payment-hidden").val();
    send[send.length] = $("#date-start-trans-repay1-reply-hidden").val();
    send[send.length] = $("#date-end-trans-repay1-reply-hidden").val();

    Printing(send.join(":"), "reportdebtorcontractbreakrequirerepaypayment", "excel", "eCPPrinting.aspx", "export-target");
}