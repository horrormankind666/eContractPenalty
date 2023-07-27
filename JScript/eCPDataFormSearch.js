function ResetFrmSearchCPTabUser(action) {
    var nameTabUserDefault = (action != "clear" ? ($("#name-tab-user-hidden").val().length > 0 ? $("#name-tab-user-hidden").val() : "") : "");

    $("#name-tab-user").val(nameTabUserDefault);
}

function ValidateSearchCPTabUser() {
    var error = false;
    var msg;
    var focus;

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-tab-user").val("search");
    $("#name-tab-user-hidden").val($("#name-tab-user").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchCPTabUser();
}

function SearchCPTabUser() {
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#name-tab-user-hidden").val();

    BoxSearchCondition(1, searchValue, "search-tab-user-condition");

    var send = new Array();
    send[send.length] = ("name=" + $("#name-tab-user-hidden").val());

    SearchData("tabuser", send, "record-count-cp-tab-user", "list-data-tab-user", "nav-page-tab-user");
}

function ResetFrmSearchStudentWithResult() {
    $("#id-name-search-student").val("");
    InitCombobox("facultysearchstudent", "0", "0", 390, 415);

    $("#record-count-student-with-result").html("ค้นหาพบ 0 รายการ");
    $("#list-data-search-student-with-result").html("");
    $("#nav-page-search-student-with-result").html("");

    /*
    ResetListSearchStudentWithResult();
    */
}

function ResetListSearchStudentWithResult() {
    $("#list-data-search-student-with-result").slimScroll({
        height: "220px",
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
}

function ValidateSearchStudentWithResult() {
    SetMsgLoading("กำลังค้นหา...");

    SearchStudentWithResult();
}

function SearchStudentWithResult() {
    var faculty = (ComboboxGetSelectedValue("facultysearchstudent") != "0" ? ComboboxGetSelectedValue("facultysearchstudent") : "");
    var program = (ComboboxGetSelectedValue("programsearchstudent") != "0" ? ComboboxGetSelectedValue("programsearchstudent") : "");

    faculty = (faculty.length > 0 ? faculty.split(";") : "");
    program = (program.length > 0 ? program.split(";") : "");

    var send = new Array();
    send[send.length] = ("studentid=" + $("#id-name-search-student").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
 
    SearchData("studentwithresult", send, "record-count-student-with-result", "list-data-search-student-with-result", "nav-page-search-student-with-result");
}

function ViewStudent(profileStudent) {
    CloseFrm(true, "");
    ViewStudentInAddProfileStudent(profileStudent);
}

function ResetFrmSearchCPTransBreakContract(action) {
    var trackingStatusTransBreakContractDefault = (action != "clear" ? ($("#trackingstatus-trans-break-contract-hidden").val().length > 0 ? $("#trackingstatus-trans-break-contract-hidden").val() : "0") : "0");
    var idNameTransBreakContractDefault = (action != "clear" ? ($("#id-name-trans-break-contract-hidden").val().length > 0 ? $("#id-name-trans-break-contract-hidden").val() : "") : "");
    var facultyTransBreakContractDefault = (action != "clear" ? ($("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val() : "0") : "0");
    var programTransBreakContractDefault = (action != "clear" ? ($("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val() : "") : "");
    var dateStartTransBreakContractDefault = (action != "clear" ? ($("#date-start-trans-break-contract-hidden").val().length > 0 ? $("#date-start-trans-break-contract-hidden").val() : "") : "");
    var dateEndTransBreakContractDefault = (action != "clear" ? ($("#date-end-trans-break-contract-hidden").val().length > 0 ? $("#date-end-trans-break-contract-hidden").val() : "") : "");

    InitCombobox("trackingstatus-trans-break-contract", "0", trackingStatusTransBreakContractDefault, 390, 415);
    $("#id-name-trans-break-contract").val(idNameTransBreakContractDefault);
    InitCombobox("facultytransbreakcontract", "0", facultyTransBreakContractDefault, 390, 415);
    $("#program-trans-break-contract-hidden").val(programTransBreakContractDefault);
    $("#date-start-trans-break-contract").val(dateStartTransBreakContractDefault);
    $("#date-end-trans-break-contract").val(dateEndTransBreakContractDefault);
    InitCalendarFromTo("#date-start-trans-break-contract", false, "#date-end-trans-break-contract", false);
}

function ValidateSearchCPTransBreakContract() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            ($("#date-start-trans-break-contract").val().length > 0 && $("#date-end-trans-break-contract").val().length == 0) ||
            ($("#date-start-trans-break-contract").val().length == 0 && $("#date-end-trans-break-contract").val().length > 0)
        )) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        focus = "#date-start-trans-break-contract";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-trans-break-contract").val("search");
    $("#trackingstatus-trans-break-contract-hidden").val(ComboboxGetSelectedValue("trackingstatus-trans-break-contract") != "0" ? ComboboxGetSelectedValue("trackingstatus-trans-break-contract") : "");
    $("#trackingstatus-trans-break-contract-text-hidden").val(ComboboxGetSelectedValue("trackingstatus-trans-break-contract") != "0" ? $("#trackingstatus-trans-break-contract option:selected").text() : "");
    $("#id-name-trans-break-contract-hidden").val($("#id-name-trans-break-contract").val());
    $("#faculty-trans-break-contract-hidden").val(ComboboxGetSelectedValue("facultytransbreakcontract") != "0" ? ComboboxGetSelectedValue("facultytransbreakcontract") : "");
    $("#program-trans-break-contract-hidden").val(ComboboxGetSelectedValue("programtransbreakcontract") != "0" ? ComboboxGetSelectedValue("programtransbreakcontract") : "");
    $("#date-start-trans-break-contract-hidden").val($("#date-start-trans-break-contract").val());
    $("#date-end-trans-break-contract-hidden").val($("#date-end-trans-break-contract").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchCPTransBreakContract();
}

function SearchCPTransBreakContract() {
    UncheckRoot("check-uncheck-all");

    var faculty = ($("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val().split(";") : "");
    var program = ($("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#trackingstatus-trans-break-contract-text-hidden").val();
    searchValue[searchValue.length] = $("#id-name-trans-break-contract-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");
    searchValue[searchValue.length] = ($("#date-start-trans-break-contract-hidden").val().length > 0 && $("#date-end-trans-break-contract-hidden").val().length > 0 ? ($("#date-start-trans-break-contract-hidden").val() + " - " + $("#date-end-trans-break-contract-hidden").val()) : "");

    BoxSearchCondition(5, searchValue, "search-trans-break-contract-condition");
    
    var send = new Array();
    send[send.length] = ("statussend=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "1" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "1" : "")));
    send[send.length] = ("statusreceiver=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "2" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "3" ? "2" : "")));
    send[send.length] = ("statusedit=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "4" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : "")));
    send[send.length] = ("statuscancel=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "5" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : "")));
    send[send.length] = ("studentid=" + $("#id-name-trans-break-contract-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("datestart=" + $("#date-start-trans-break-contract-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-trans-break-contract-hidden").val());

    SearchData("transbreakcontract", send, "record-count-cp-trans-break-contract", "list-data-trans-break-contract", "nav-page-trans-break-contract");
}

function ResetFrmSearchCPTransRepayContract(action) {
    var repayStatusTransRepayContractDefault = (action != "clear" ? ($("#repaystatus-trans-repay-contract-hidden").val().length > 0 ? (parseInt($("#repaystatus-trans-repay-contract-hidden").val()) + 1) : "0") : "0");
    var idNameTransRepayContractDefault = (action != "clear" ? ($("#id-name-trans-repay-contract-hidden").val().length > 0 ? $("#id-name-trans-repay-contract-hidden").val() : "") : "");
    var facultyTransRepayContractDefault = (action != "clear" ? ($("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val() : "0") : "0");
    var programTransRepayContractDefault = (action != "clear" ? ($("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val() : "") : "");
    var dateStartTransRepayContractDefault = (action != "clear" ? ($("#date-start-trans-repay-contract-hidden").val().length > 0 ? $("#date-start-trans-repay-contract-hidden").val() : "") : "");
    var dateEndTransRepayContractDefault = (action != "clear" ? ($("#date-end-trans-repay-contract-hidden").val().length > 0 ? $("#date-end-trans-repay-contract-hidden").val() : "") : "");

    InitCombobox("repaystatus-trans-repay-contract", "0", repayStatusTransRepayContractDefault, 390, 415);
    $("#id-name-trans-repay-contract").val(idNameTransRepayContractDefault);
    InitCombobox("facultytransrepaycontract", "0", facultyTransRepayContractDefault, 390, 415);
    $("#program-trans-repay-contract-hidden").val(programTransRepayContractDefault);
    $("#date-start-trans-repay-contract").val(dateStartTransRepayContractDefault);
    $("#date-end-trans-repay-contract").val(dateEndTransRepayContractDefault);
    InitCalendarFromTo("#date-start-trans-repay-contract", false, "#date-end-trans-repay-contract", false);
}

function ValidateSearchCPTransRepayContract() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            ($("#date-start-trans-repay-contract").val().length > 0 && $("#date-end-trans-repay-contract").val().length == 0) ||
            ($("#date-start-trans-repay-contract").val().length == 0 && $("#date-end-trans-repay-contract").val().length > 0)
        )) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        focus = "#date-start-trans-repay-contract";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-trans-repay-contract").val("search");
    $("#repaystatus-trans-repay-contract-hidden").val(ComboboxGetSelectedValue("repaystatus-trans-repay-contract") != "0" ? (ComboboxGetSelectedValue("repaystatus-trans-repay-contract") - 1) : "");
    $("#repaystatus-trans-repay-contract-text-hidden").val(ComboboxGetSelectedValue("repaystatus-trans-repay-contract") != "0" ? $("#repaystatus-trans-repay-contract option:selected").text() : "");
    $("#id-name-trans-repay-contract-hidden").val($("#id-name-trans-repay-contract").val());
    $("#faculty-trans-repay-contract-hidden").val(ComboboxGetSelectedValue("facultytransrepaycontract") != "0" ? ComboboxGetSelectedValue("facultytransrepaycontract") : "");
    $("#program-trans-repay-contract-hidden").val(ComboboxGetSelectedValue("programtransrepaycontract") != "0" ? ComboboxGetSelectedValue("programtransrepaycontract") : "");
    $("#date-start-trans-repay-contract-hidden").val($("#date-start-trans-repay-contract").val());
    $("#date-end-trans-repay-contract-hidden").val($("#date-end-trans-repay-contract").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchRepay();
}

function SearchRepay() {
    var faculty = ($("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val().split(";") : "");
    var program = ($("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#repaystatus-trans-repay-contract-text-hidden").val();
    searchValue[searchValue.length] = $("#id-name-trans-repay-contract-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");
    searchValue[searchValue.length] = ($("#date-start-trans-repay-contract-hidden").val().length > 0 && $("#date-end-trans-repay-contract-hidden").val().length > 0 ? ($("#date-start-trans-repay-contract-hidden").val() + " - " + $("#date-end-trans-repay-contract-hidden").val()) : "");

    BoxSearchCondition(5, searchValue, "search-trans-repay-contract-condition");

    var statusRepay = SetStatusRepay();
    var send = new Array();
    send[send.length] = ("statusrepay=" + statusRepay[0]);
    send[send.length] = ("statusreply=" + statusRepay[1]);
    send[send.length] = ("replyresult=" + statusRepay[2]);
    send[send.length] = ("statuspayment=" + statusRepay[3]);
    send[send.length] = ("studentid=" + $("#id-name-trans-repay-contract-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("datestart=" + $("#date-start-trans-repay-contract-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-trans-repay-contract-hidden").val());

    SearchData("transrepaycontract", send, "record-count-cp-trans-repay-contract", "list-data-trans-repay-contract", "nav-page-trans-repay-contract");
}

function ResetFrmSearchCPTransPayment(action) {
    var paymentStatusTransPaymentDefault = (action != "clear" ? ($("#paymentstatus-trans-payment-hidden").val().length > 0 ? $("#paymentstatus-trans-payment-hidden").val() : "0") : "0");
    var paymentRecordStatusTransPaymentDefault = (action != "clear" ? ($("#paymentrecordstatus-trans-payment-hidden").val().length > 0 ? $("#paymentrecordstatus-trans-payment-hidden").val() : "0") : "0");
    var idNameTransPaymentDefault = (action != "clear" ? ($("#id-name-trans-payment-hidden").val().length > 0 ? $("#id-name-trans-payment-hidden").val() : "") : "");
    var facultyTransPaymentDefault = (action != "clear" ? ($("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val() : "0") : "0");
    var programTransPaymentDefault = (action != "clear" ? ($("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val() : "") : "");
    var dateStartTransRepay1ReplyDefault = (action != "clear" ? ($("#date-start-trans-repay1-reply-hidden").val().length > 0 ? $("#date-start-trans-repay1-reply-hidden").val() : "") : "");
    var dateEndTransRepay1ReplyDefault = (action != "clear" ? ($("#date-end-trans-repay1-reply-hidden").val().length > 0 ? $("#date-end-trans-repay1-reply-hidden").val() : "") : "");

    InitCombobox("paymentstatus-trans-payment", "0", paymentStatusTransPaymentDefault, 336, 361);
    InitCombobox("paymentrecordstatus-trans-payment", "0", paymentRecordStatusTransPaymentDefault, 336, 361);
    $("#id-name-trans-payment").val(idNameTransPaymentDefault);
    InitCombobox("facultytranspayment", "0", facultyTransPaymentDefault, 336, 361);
    $("#program-trans-payment-hidden").val(programTransPaymentDefault);    
    $("#date-start-trans-repay1-reply").val(dateStartTransRepay1ReplyDefault);
    $("#date-end-trans-repay1-reply").val(dateEndTransRepay1ReplyDefault);
    InitCalendarFromTo("#date-start-trans-repay1-reply", false, "#date-end-trans-repay1-reply", false);
}

function ValidateSearchCPTransPayment() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            ($("#date-start-trans-repay1-reply").val().length > 0 && $("#date-end-trans-repay1-reply").val().length == 0) ||
            ($("#date-start-trans-repay1-reply").val().length == 0 && $("#date-end-trans-repay1-reply").val().length > 0)
        )) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        focus = "#date-start-trans-repay1-reply";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-trans-payment").val("search");
    $("#paymentstatus-trans-payment-hidden").val(ComboboxGetSelectedValue("paymentstatus-trans-payment") != "0" ? ComboboxGetSelectedValue("paymentstatus-trans-payment") : "");
    $("#paymentstatus-trans-payment-text-hidden").val(ComboboxGetSelectedValue("paymentstatus-trans-payment") != "0" ? $("#paymentstatus-trans-payment option:selected").text() : "");
    $("#paymentrecordstatus-trans-payment-hidden").val(ComboboxGetSelectedValue("paymentrecordstatus-trans-payment") != "0" ? ComboboxGetSelectedValue("paymentrecordstatus-trans-payment") : "");
    $("#paymentrecordstatus-trans-payment-text-hidden").val(ComboboxGetSelectedValue("paymentrecordstatus-trans-payment") != "0" ? $("#paymentrecordstatus-trans-payment option:selected").text() : "");
    $("#id-name-trans-payment-hidden").val($("#id-name-trans-payment").val());
    $("#faculty-trans-payment-hidden").val(ComboboxGetSelectedValue("facultytranspayment") != "0" ? ComboboxGetSelectedValue("facultytranspayment") : "");
    $("#program-trans-payment-hidden").val(ComboboxGetSelectedValue("programtranspayment") != "0" ? ComboboxGetSelectedValue("programtranspayment") : "");
    $("#date-start-trans-repay1-reply-hidden").val($("#date-start-trans-repay1-reply").val());
    $("#date-end-trans-repay1-reply-hidden").val($("#date-end-trans-repay1-reply").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchPayment();
}

function SearchPayment() {
    var faculty = ($("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val().split(";") : "");
    var program = ($("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#paymentstatus-trans-payment-text-hidden").val();
    searchValue[searchValue.length] = $("#id-name-trans-payment-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");
    searchValue[searchValue.length] = ($("#date-start-trans-repay1-reply-hidden").val().length > 0 && $("#date-end-trans-repay1-reply-hidden").val().length > 0 ? ($("#date-start-trans-repay1-reply-hidden").val() + " - " + $("#date-end-trans-repay1-reply-hidden").val()) : "");
    searchValue[searchValue.length] = $("#paymentrecordstatus-trans-payment-text-hidden").val();

    BoxSearchCondition(6, searchValue, "search-trans-payment-condition");

    var send = new Array();
    send[send.length] = ("statuspayment=" + $("#paymentstatus-trans-payment-hidden").val());
    send[send.length] = ("statuspaymentrecord=" + $("#paymentrecordstatus-trans-payment-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-trans-payment-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("datestart=" + $("#date-start-trans-repay1-reply-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-trans-repay1-reply-hidden").val());

    SearchData("transpayment", send, "record-count-cp-trans-payment", "list-data-trans-payment", "nav-page-trans-payment");
}

function ResetFrmSearchCPReportTableCalCapitalAndInterest(action) {
    var idNameReportTableCalCapitalAndInterestDefault = (action != "clear" ? ($("#id-name-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#id-name-report-table-cal-capital-and-interest-hidden").val() : "") : "");
    var facultyReportTableCalCapitalAndInteresttDefault = (action != "clear" ? ($("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val() : "0") : "0");
    var programReportTableCalCapitalAndInterestDefault = (action != "clear" ? ($("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val() : "") : "");

    $("#id-name-report-table-cal-capital-and-interest").val(idNameReportTableCalCapitalAndInterestDefault);
    InitCombobox("facultyreporttablecalcapitalandinterest", "0", facultyReportTableCalCapitalAndInteresttDefault, 390, 415);
    $("#program-report-table-cal-capital-and-interest-hidden").val(programReportTableCalCapitalAndInterestDefault);
}

function ValidateSearchCPReportTableCalCapitalAndInterest() {
    var error = false;
    var msg;
    var focus;

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-table-cal-capital-and-interest").val("search");
    $("#id-name-report-table-cal-capital-and-interest-hidden").val($("#id-name-report-table-cal-capital-and-interest").val());
    $("#faculty-report-table-cal-capital-and-interest-hidden").val(ComboboxGetSelectedValue("facultyreporttablecalcapitalandinterest") != "0" ? ComboboxGetSelectedValue("facultyreporttablecalcapitalandinterest") : "");
    $("#program-report-table-cal-capital-and-interest-hidden").val(ComboboxGetSelectedValue("programreporttablecalcapitalandinterest") != "0" ? ComboboxGetSelectedValue("programreporttablecalcapitalandinterest") : "");

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportTableCalCapitalAndInterest();
}

function SearchReportTableCalCapitalAndInterest() {
    var faculty = ($("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val().split(";") : "");
    var program = ($("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#id-name-report-table-cal-capital-and-interest-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");

    BoxSearchCondition(3, searchValue, "search-report-table-cal-capital-and-interest-condition");

    var send = new Array();
    send[send.length] = ("studentid=" + $("#id-name-report-table-cal-capital-and-interest-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

    SearchData("reporttablecalcapitalandinterest", send, "record-count-cp-report-table-cal-capital-and-interest", "list-data-report-table-cal-capital-and-interest", "nav-page-report-table-cal-capital-and-interest");
}

function ResetFrmSearchCPReportStepOfWork(action) {
    var stepOfWorkStatusReportStepOfWorkDefault = (action != "clear" ? ($("#stepofworkstatus-report-step-of-work-hidden").val().length > 0 ? $("#stepofworkstatus-report-step-of-work-hidden").val() : "0") : "0");
    var idNameReportStepOfWorkDefault = (action != "clear" ? ($("#id-name-report-step-of-work-hidden").val().length > 0 ? $("#id-name-report-step-of-work-hidden").val() : "") : "");
    var facultyReportStepOfWorkDefault = (action != "clear" ? ($("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val() : "0") : "0");
    var programReportStepOfWorkDefault = (action != "clear" ? ($("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val() : "") : "");

    InitCombobox("stepofworkstatus-report-step-of-work", "0", stepOfWorkStatusReportStepOfWorkDefault, 390, 415);
    $("#id-name-report-step-of-work").val(idNameReportStepOfWorkDefault);
    InitCombobox("facultyreportstepofwork", "0", facultyReportStepOfWorkDefault, 390, 415);
    $("#program-report-step-of-work-hidden").val(programReportStepOfWorkDefault);
}

function ValidateSearchCPReportStepOfWork() {
    var error = false;
    var msg;
    var focus;

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-step-of-work").val("search");
    $("#stepofworkstatus-report-step-of-work-hidden").val(ComboboxGetSelectedValue("stepofworkstatus-report-step-of-work") != "0" ? ComboboxGetSelectedValue("stepofworkstatus-report-step-of-work") : "");
    $("#stepofworkstatus-report-step-of-work-text-hidden").val(ComboboxGetSelectedValue("stepofworkstatus-report-step-of-work") != "0" ? $("#stepofworkstatus-report-step-of-work option:selected").text() : "");
    $("#id-name-report-step-of-work-hidden").val($("#id-name-report-step-of-work").val());
    $("#faculty-report-step-of-work-hidden").val(ComboboxGetSelectedValue("facultyreportstepofwork") != "0" ? ComboboxGetSelectedValue("facultyreportstepofwork") : "");
    $("#program-report-step-of-work-hidden").val(ComboboxGetSelectedValue("programreportstepofwork") != "0" ? ComboboxGetSelectedValue("programreportstepofwork") : "");

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportStepOfWork();
}

function SearchReportStepOfWork() {
    var faculty = ($("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val().split(";") : "");
    var program = ($("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");    
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#stepofworkstatus-report-step-of-work-text-hidden").val();
    searchValue[searchValue.length] = $("#id-name-report-step-of-work-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");

    BoxSearchCondition(4, searchValue, "search-report-step-of-work-condition");

    var send = new Array();
    send[send.length] = ("statusstepofwork=" + $("#stepofworkstatus-report-step-of-work-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-report-step-of-work-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

    SearchData("reportstepofwork", send, "record-count-cp-report-step-of-work", "list-data-report-step-of-work", "nav-page-report-step-of-work");
}

function ResetFrmSearchReportStepOfWorkOnStatisticRepayByProgram() {
    var faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");

    $("#statistic-repay-by-program-acadamicyear").html("25" + $("#acadamicyear-hidden").val());
    $("#statistic-repay-by-program-faculty").html(faculty[0] + " - " + faculty[1])
    $("#statistic-repay-by-program-program").html(program[0] + " - " + program[1] + (program[3] != "0" ? (" ( กลุ่ม " + program[3] + " )") : ""));

    $("#id-name-search-report-step-of-work").val("");
    $("#record-count-report-step-of-work").html("ค้นหาพบ 0 รายการ");
    $("#list-data-search-report-step-of-work").html("");
    $("#nav-page-search-report-step-of-work").html("");

    /*
    $("#list-data-search-report-step-of-work").slimScroll({
        height: "220px",
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

    SetMsgLoading("");

    SearchReportStepOfWorkOnStatisticRepayByProgram();
}

function ValidateSearchReportStepOfWorkOnStatisticRepayByProgram() {
    SetMsgLoading("กำลังค้นหา...");

    SearchReportStepOfWorkOnStatisticRepayByProgram();
}

function SearchReportStepOfWorkOnStatisticRepayByProgram() {
    var faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var send = new Array();
    send[send.length] = ("acadamicyear=" + $("#acadamicyear-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-search-report-step-of-work").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

    SearchData("reportstepofworkonstatisticrepaybyprogram", send, "record-count-report-step-of-work", "list-data-search-report-step-of-work", "nav-page-search-report-step-of-work");
}

function SearchReportStatisticRepayByProgram() {
    var send = new Array();
    send[send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();

    SearchData("reportstatisticrepaybyprogram", send, "record-count-cp-report-statistic-repay-by-program", "list-data-report-statistic-repay-by-program", "");
}

function ResetFrmSearchReportStudentOnStatisticContractByProgram() {
    var faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");

    $("#statistic-contract-by-program-acadamicyear").html("25" + $("#acadamicyear-hidden").val());
    $("#statistic-contract-by-program-faculty").html(faculty[0] + " - " + faculty[1])
    $("#statistic-contract-by-program-program").html(program[0] + " - " + program[1] + (program[3] != "0" ? (" ( กลุ่ม " + program[3] + " )") : ""));

    $("#id-name-search-report-student-contract").val("");
    $("#record-count-student-sign-contract").html("ค้นหาพบ 0 รายการ");
    $("#list-data-student-sign-contract").html("");
    $("#nav-page-student-sign-contract").html("");
    $("#record-count-student-contract-penalty").html("ค้นหาพบ 0 รายการ");
    $("#list-data-student-contract-penalty").html("");
    $("#nav-page-student-contract-penalty").html("");

    /*
    $("#list-data-student-sign-contract, #list-data-student-contract-penalty").slimScroll({
        height: "210px",
        alwaysVisible: true,
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

    SetMsgLoading("");

    if ($("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true)
        SearchReportStudentOnStatisticContractByProgram(1);

    if ($("#link-tab2-report-student-on-statistic-contract-by-program").hasClass("active") == true)
        SearchReportStudentOnStatisticContractByProgram(2);
}

function ValidateSearchReportStudentOnStatisticContractByProgram() {
    SetMsgLoading("กำลังค้นหา...");

    if ($("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true)
        SearchReportStudentOnStatisticContractByProgram(1);

    if ($("#link-tab2-report-student-on-statistic-contract-by-program").hasClass("active") == true)
        SearchReportStudentOnStatisticContractByProgram(2);
}

function SearchReportStudentOnStatisticContractByProgram(searchTab) {
    var faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var send = new Array();
    send[send.length] = ("acadamicyear=" + $("#acadamicyear-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-search-report-student-contract").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("searchtab=" + searchTab);

    var idRecordCount;
    var idListSearch;
    var idNavPageSearch;

    if (searchTab == 1) {
        idRecordCount = "record-count-student-sign-contract";
        idListSearch = "list-data-student-sign-contract";
        idNavPageSearch = "nav-page-student-sign-contract";
    }

    if (searchTab == 2) {
        idRecordCount = "record-count-student-contract-penalty";
        idListSearch = "list-data-student-contract-penalty";
        idNavPageSearch = "nav-page-student-contract-penalty";
    }

    SearchData("reportstudentonstatisticcontractbyprogram", send, idRecordCount, idListSearch, idNavPageSearch);
}

function SearchReportStatisticContractByProgram() {
    var send = new Array();
    send[send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();

    SearchData("reportstatisticcontractbyprogram", send, "record-count-cp-report-statistic-contract-by-program", "list-data-report-statistic-contract-by-program", "");
}

function ResetFrmSearchCPReportNoticeRepayComplete(action) {
    var idNameReportNoticeRepayCompleteDefault = (action != "clear" ? ($("#id-name-report-notice-repay-complete-hidden").val().length > 0 ? $("#id-name-report-notice-repay-complete-hidden").val() : "") : "");
    var facultyReportNoticeRepayCompleteDefault = (action != "clear" ? ($("#faculty-report-notice-repay-complete-hidden").val().length > 0 ? $("#faculty-report-notice-repay-complete-hidden").val() : "0") : "0");
    var programReportNoticeRepayCompleteDefault = (action != "clear" ? ($("#program-report-notice-repay-complete-hidden").val().length > 0 ? $("#program-report-notice-repay-complete-hidden").val() : "") : "");

    $("#id-name-report-notice-repay-complete").val(idNameReportNoticeRepayCompleteDefault);
    InitCombobox("facultyreportnoticerepaycomplete", "0", facultyReportNoticeRepayCompleteDefault, 390, 415);
    $("#program-report-notice-repay-complete-hidden").val(programReportNoticeRepayCompleteDefault);
}

function ValidateSearchCPReportNoticeRepayComplete() {
    var error = false;
    var msg;
    var focus;

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-notice-repay-complete").val("search");
    $("#id-name-report-notice-repay-complete-hidden").val($("#id-name-report-notice-repay-complete").val());
    $("#faculty-report-notice-repay-complete-hidden").val(ComboboxGetSelectedValue("facultyreportnoticerepaycomplete") != "0" ? ComboboxGetSelectedValue("facultyreportnoticerepaycomplete") : "");
    $("#program-report-notice-repay-complete-hidden").val(ComboboxGetSelectedValue("programreportnoticerepaycomplete") != "0" ? ComboboxGetSelectedValue("programreportnoticerepaycomplete") : "");

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportNoticeRepayComplete();
}

function SearchReportNoticeRepayComplete() {
    UncheckRoot("check-uncheck-all");

    var faculty = ($("#faculty-report-notice-repay-complete-hidden").val().length > 0 ? $("#faculty-report-notice-repay-complete-hidden").val().split(";") : "");
    var program = ($("#program-report-notice-repay-complete-hidden").val().length > 0 ? $("#program-report-notice-repay-complete-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#id-name-report-notice-repay-complete-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");

    BoxSearchCondition(3, searchValue, "search-report-notice-repay-complete-condition");

    var send = new Array();
    send[send.length] = ("studentid=" + $("#id-name-report-notice-repay-complete-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    
    SearchData("reportnoticerepaycomplete", send, "record-count-cp-report-notice-repay-complete", "list-data-report-notice-repay-complete", "nav-page-report-notice-repay-complete");
}

function ResetFrmSearchCPReportNoticeClaimDebt(action) {
    var idNameReportNoticeClaimDebtDefault = (action != "clear" ? ($("#id-name-report-notice-claim-debt-hidden").val().length > 0 ? $("#id-name-report-notice-claim-debt-hidden").val() : "") : "");
    var facultyReportNoticeClaimDebtDefault = (action != "clear" ? ($("#faculty-report-notice-claim-debt-hidden").val().length > 0 ? $("#faculty-report-notice-claim-debt-hidden").val() : "0") : "0");
    var programReportNoticeClaimDebtDefault = (action != "clear" ? ($("#program-report-notice-claim-debt-hidden").val().length > 0 ? $("#program-report-notice-claim-debt-hidden").val() : "") : "");

    $("#id-name-report-notice-claim-debt").val(idNameReportNoticeClaimDebtDefault);
    InitCombobox("facultyreportnoticeclaimdebt", "0", facultyReportNoticeClaimDebtDefault, 390, 415);
    $("#program-report-notice-claim-debt-hidden").val(programReportNoticeClaimDebtDefault);
}

function ValidateSearchCPReportNoticeClaimDebt() {
    var error = false;
    var msg;
    var focus;

    if (error == true) {
      DialogMessage(msg, focus, false, "");
      return;
    }

    $("#search-report-notice-claim-debt").val("search");
    $("#id-name-report-notice-claim-debt-hidden").val($("#id-name-report-notice-claim-debt").val());
    $("#faculty-report-notice-claim-debt-hidden").val(ComboboxGetSelectedValue("facultyreportnoticeclaimdebt") != "0" ? ComboboxGetSelectedValue("facultyreportnoticeclaimdebt") : "");
    $("#program-report-notice-claim-debt-hidden").val(ComboboxGetSelectedValue("programreportnoticeclaimdebt") != "0" ? ComboboxGetSelectedValue("programreportnoticeclaimdebt") : "");

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportNoticeClaimDebt();
}

function SearchReportNoticeClaimDebt() {
    var faculty = ($("#faculty-report-notice-claim-debt-hidden").val().length > 0 ? $("#faculty-report-notice-claim-debt-hidden").val().split(";") : "");
    var program = ($("#program-report-notice-claim-debt-hidden").val().length > 0 ? $("#program-report-notice-claim-debt-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#id-name-report-notice-claim-debt-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");

    BoxSearchCondition(3, searchValue, "search-report-notice-claim-debt-condition");

    var send = new Array();
    send[send.length] = ("studentid=" + $("#id-name-report-notice-claim-debt-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

    SearchData("reportnoticeclaimdebt", send, "record-count-cp-report-notice-claim-debt", "list-data-report-notice-claim-debt", "nav-page-report-notice-claim-debt");
}

function SearchReportStatisticPaymentByDate() {
    var faculty = ($("#faculty-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#faculty-report-statistic-payment-by-date-hidden").val().split(";") : "");
    var program = ($("#program-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#program-report-statistic-payment-by-date-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var formatPayment = ($("#format-payment-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#format-payment-report-statistic-payment-by-date-hidden").val().split(";") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#id-name-report-statistic-payment-by-date-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");
    searchValue[searchValue.length] = (formatPayment[0] != null ? formatPayment[1] : "");
    searchValue[searchValue.length] = ($("#date-start-report-statistic-payment-by-date-hidden").val().length > 0 && $("#date-end-report-statistic-payment-by-date-hidden").val().length > 0 ? ($("#date-start-report-statistic-payment-by-date-hidden").val() + " - " + $("#date-end-report-statistic-payment-by-date-hidden").val()) : "");

    BoxSearchCondition(5, searchValue, "search-report-statistic-payment-by-date-condition");

    var send = new Array();
    send[send.length] = ("studentid=" + $("#id-name-report-statistic-payment-by-date-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));
    send[send.length] = ("datestart=" + $("#date-start-report-statistic-payment-by-date-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-report-statistic-payment-by-date-hidden").val());
    
    SearchData("reportstatisticpaymentbydate", send, "record-count-cp-report-statistic-payment-by-date", "list-data-report-statistic-payment-by-date", "nav-page-report-statistic-payment-by-date");
}

function ResetFrmSearchCPReportStatisticPaymentByDate(action) {
    var idNameReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#id-name-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#id-name-report-statistic-payment-by-date-hidden").val() : "") : "");
    var facultyReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#faculty-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#faculty-report-statistic-payment-by-date-hidden").val() : "0") : "0");
    var programReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#program-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#program-report-statistic-payment-by-date-hidden").val() : "") : "");
    var formatPaymentReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#format-payment-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#format-payment-report-statistic-payment-by-date-hidden").val() : "0") : "0");
    var dateStartReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#date-start-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#date-start-report-statistic-payment-by-date-hidden").val() : "") : "");
    var dateEndReportStatisticPaymentByDateDefault = (action != "clear" ? ($("#date-end-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#date-end-report-statistic-payment-by-date-hidden").val() : "") : "");

    $("#id-name-report-statistic-payment-by-date").val(idNameReportStatisticPaymentByDateDefault);
    InitCombobox("facultyreportstatisticpaymentbydate", "0", facultyReportStatisticPaymentByDateDefault, 390, 415);
    $("#program-report-statistic-payment-by-date-hidden").val(programReportStatisticPaymentByDateDefault);
    InitCombobox("formatpaymentreportstatisticpaymentbydate", "0", formatPaymentReportStatisticPaymentByDateDefault, 390, 415);
    $("#date-start-report-statistic-payment-by-date").val(dateStartReportStatisticPaymentByDateDefault);
    $("#date-end-report-statistic-payment-by-date").val(dateEndReportStatisticPaymentByDateDefault);
    InitCalendarFromTo("#date-start-report-statistic-payment-by-date", false, "#date-end-report-statistic-payment-by-date", false);
}

function ValidateSearchCPReportStatisticPaymentByDate() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            ($("#date-start-report-statistic-payment-by-date").val().length > 0 && $("#date-end-report-statistic-payment-by-date").val().length == 0) ||
            ($("#date-start-report-statistic-payment-by-date").val().length == 0 && $("#date-end-report-statistic-payment-by-date").val().length > 0)
        )) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        focus = "#date-start-report-statistic-payment-by-date";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-statistic-payment-by-date").val("search");
    $("#id-name-report-statistic-payment-by-date-hidden").val($("#id-name-report-statistic-payment-by-date").val());
    $("#faculty-report-statistic-payment-by-date-hidden").val(ComboboxGetSelectedValue("facultyreportstatisticpaymentbydate") != "0" ? ComboboxGetSelectedValue("facultyreportstatisticpaymentbydate") : "");
    $("#program-report-statistic-payment-by-date-hidden").val(ComboboxGetSelectedValue("programreportstatisticpaymentbydate") != "0" ? ComboboxGetSelectedValue("programreportstatisticpaymentbydate") : "");
    $("#format-payment-report-statistic-payment-by-date-hidden").val(ComboboxGetSelectedValue("formatpaymentreportstatisticpaymentbydate") != "0" ? ComboboxGetSelectedValue("formatpaymentreportstatisticpaymentbydate") : "")
    $("#date-start-report-statistic-payment-by-date-hidden").val($("#date-start-report-statistic-payment-by-date").val());
    $("#date-end-report-statistic-payment-by-date-hidden").val($("#date-end-report-statistic-payment-by-date").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportStatisticPaymentByDate();
}

function SearchReportEContract() {
    var faculty = ($("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val().split(";") : "");
    var program = ($("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val().split(";") : "");
    var groupNum = (program[3] != null && program[3] != "0" ? (" ( กลุ่ม " + program[3] + " ) ") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = $("#acadamicyear-report-e-contract-hidden").val();
    searchValue[searchValue.length] = $("#id-name-report-e-contract-hidden").val();
    searchValue[searchValue.length] = (faculty[0] != null ? (faculty[0] + " - " + faculty[1]) : "");
    searchValue[searchValue.length] = (program[0] != null ? (program[0] + " - " + program[1] + groupNum) : "");

    BoxSearchCondition(4, searchValue, "search-report-e-contract-condition");

    var send = new Array();
    send[send.length] = ("acadamicyear=" + $("#acadamicyear-report-e-contract-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-report-e-contract-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    
    SearchData("reportecontract", send, "record-count-cp-report-e-contract", "list-data-report-e-contract", "nav-page-report-e-contract");
}

function ResetFrmSearchCPReportEContract(action) {
    var acadamicYearReportEContractDefault = (action != "clear" ? ($("#acadamicyear-report-e-contract-hidden").val().length > 0 ? $("#acadamicyear-report-e-contract-hidden").val() : "0") : "0");
    var idNameReportEContractDefault = (action != "clear" ? ($("#id-name-report-e-contract-hidden").val().length > 0 ? $("#id-name-report-e-contract-hidden").val() : "") : "");
    var facultyReportEContractDefault = (action != "clear" ? ($("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val() : "0") : "0");
    var programReportEContractDefault = (action != "clear" ? ($("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val() : "") : "");

    InitCombobox("acadamicyear-report-e-contract", "0", acadamicYearReportEContractDefault, 390, 415);
    $("#id-name-report-e-contract").val(idNameReportEContractDefault);
    InitCombobox("facultyreportecontract", "0", facultyReportEContractDefault, 390, 415);
    $("#program-report-e-contract-hidden").val(programReportEContractDefault);
}

function ValidateSearchCPReportEContract() {
    var error = false;
    var msg;
    var focus;

    if (error == true) { 
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-e-contract").val("search");
    $("#acadamicyear-report-e-contract-hidden").val(ComboboxGetSelectedValue("acadamicyear-report-e-contract") != "0" ? ComboboxGetSelectedValue("acadamicyear-report-e-contract") : "");
    $("#id-name-report-e-contract-hidden").val($("#id-name-report-e-contract").val());
    $("#faculty-report-e-contract-hidden").val(ComboboxGetSelectedValue("facultyreportecontract") != "0" ? ComboboxGetSelectedValue("facultyreportecontract") : "");
    $("#program-report-e-contract-hidden").val(ComboboxGetSelectedValue("programreportecontract") != "0" ? ComboboxGetSelectedValue("programreportecontract") : "");

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportEContract();
}

function SearchReportDebtorContract() {
    $("#search-report-debtor-contract-by-program-condition").hide();
    $("#id-name-report-debtor-contract-by-program-hidden").val("");
    $("#faculty-report-debtor-contract-by-program-hidden").val("");
    $("#program-report-debtor-contract-by-program-hidden").val("");
    $("#format-payment-report-debtor-contract-by-program-hidden").val("");

    var reportOrder = $("#report-debtor-contract-order").val();        
    var searchValue = new Array();
    searchValue[searchValue.length] = ($("#date-start-report-debtor-contract-hidden").val().length > 0 && $("#date-end-report-debtor-contract-hidden").val().length > 0 ? ($("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val()) : "");

    BoxSearchCondition(1, searchValue, "search-report-debtor-contract-condition");

    var send = new Array();
    send[send.length] = ("reportorder=" + reportOrder);
    send[send.length] = ("datestart=" + $("#date-start-report-debtor-contract-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-report-debtor-contract-hidden").val());

    SearchData("reportdebtorcontract", send, "record-count-cp-report-debtor-contract", "list-data-report-debtor-contract", "");
}

function ResetFrmSearchCPReportDebtorContract(action) {
    var dateStartReportDebtorContractDefault = (action != "clear" ? ($("#date-start-report-debtor-contract-hidden").val().length > 0 ? $("#date-start-report-debtor-contract-hidden").val() : "01/01/2556") : "01/01/2556");
    var dateEndReportDebtorContractDefault = (action != "clear" ? ($("#date-end-report-debtor-contract-hidden").val().length > 0 ? $("#date-end-report-debtor-contract-hidden").val() : "") : "");

    $("#date-start-report-debtor-contract").val(dateStartReportDebtorContractDefault);
    $("#date-end-report-debtor-contract").val(dateEndReportDebtorContractDefault);
    InitCalendarFromTo("#date-start-report-debtor-contract", false, "#date-end-report-debtor-contract", false);
}

function ValidateSearchCPReportDebtorContract() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            ($("#date-start-report-debtor-contract").val().length > 0 && $("#date-end-report-debtor-contract").val().length == 0) ||
            ($("#date-start-report-debtor-contract").val().length == 0 && $("#date-end-report-debtor-contract").val().length > 0)
        )) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่รับสภาพหนี้ให้ครบถ้วน";
        focus = "#date-start-report-debtor-contract";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#search-report-debtor-contract").val("search");
    $("#date-start-report-debtor-contract-hidden").val($("#date-start-report-debtor-contract").val());
    $("#date-end-report-debtor-contract-hidden").val($("#date-end-report-debtor-contract").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchReportDebtorContract();
}

function SearchReportDebtorContractByProgram() {
    var formatPayment = ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = ($("#date-start-report-debtor-contract-hidden").val().length > 0 && $("#date-end-report-debtor-contract-hidden").val().length > 0 ? ($("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val()) : "");
    searchValue[searchValue.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    searchValue[searchValue.length] = (formatPayment[0] != null ? formatPayment[1] : "");

    BoxSearchCondition(3, searchValue, "search-report-debtor-contract-by-program-condition");

    var reportOrder = $("#report-debtor-contract-order").val();
    var faculty = ($("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val().split(";") : "");
    var program = ($("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val().split(";") : "");
    var send = new Array();
    send[send.length] = ("reportorder=" + reportOrder);
    send[send.length] = ("datestart=" + $("#date-start-report-debtor-contract-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-report-debtor-contract-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-report-debtor-contract-by-program-hidden").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));
    
    SearchData("reportdebtorcontractbyprogram", send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
}

function SearchStudentDebtorContractByProgram() {
    var formatPayment = ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "");
    var searchValue = new Array();
    searchValue[searchValue.length] = ($("#date-start-report-debtor-contract-hidden").val().length > 0 && $("#date-end-report-debtor-contract-hidden").val().length > 0 ? ($("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val()) : "");
    searchValue[searchValue.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    searchValue[searchValue.length] = (formatPayment[0] != null ? formatPayment[1] : "");

    BoxSearchCondition(3, searchValue, "search-report-debtor-contract-by-program-condition");

    var reportOrder = $("#report-debtor-contract-order").val();
    var faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().split(";");
    var program = $("#program-report-debtor-contract-by-program-hidden").val().split(";");
    var send = new Array();
    send[send.length] = ("reportorder=" + reportOrder);
    send[send.length] = ("datestart=" + $("#date-start-report-debtor-contract-hidden").val());
    send[send.length] = ("dateend=" + $("#date-end-report-debtor-contract-hidden").val());
    send[send.length] = ("studentid=" + $("#id-name-report-debtor-contract-by-program").val());
    send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
    send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
    send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
    send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
    send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));
    
    SearchData("reportdebtorcontractbyprogram", send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
}

function ResetFrmSearchStudentDebtorContractByProgram(action) {
    var faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().split(";");
    var program = $("#program-report-debtor-contract-by-program-hidden").val().split(";");
    var idNameReportDebtorContractByProgramDefault = (action != "clear" ? ($("#id-name-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#id-name-report-debtor-contract-by-program-hidden").val() : "") : "");
    var formatPaymentReportDebtorContractByProgramDefault = (action != "clear" ? ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val() : "0") : "0");

    $("#student-debtor-contract-by-program-faculty").html(faculty[0] + " - " + faculty[1])
    $("#student-debtor-contract-by-program-program").html(program[0] + " - " + program[1] + (program[3] != "0" ? (" ( กลุ่ม " + program[3] + " )") : ""));
    $("#id-name-report-debtor-contract-by-program").val(idNameReportDebtorContractByProgramDefault);
    InitCombobox("formatpaymentreportdebtorcontractbyprogram", "0", formatPaymentReportDebtorContractByProgramDefault, 278, 303);
}

function ValidateSearchStudentDebtorContractByProgram() {
    $("#search-report-debtor-contract-by-program").val("search");
    $("#id-name-report-debtor-contract-by-program-hidden").val($("#id-name-report-debtor-contract-by-program").val());
    $("#format-payment-report-debtor-contract-by-program-hidden").val(ComboboxGetSelectedValue("formatpaymentreportdebtorcontractbyprogram") != "0" ? ComboboxGetSelectedValue("formatpaymentreportdebtorcontractbyprogram") : "")

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchStudentDebtorContractByProgram();
}