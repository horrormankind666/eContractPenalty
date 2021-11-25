function ResetFrmSearchCPTabUser(_action) {
    var _nameTabUserDefault = (_action != "clear") ? ($("#name-tab-user-hidden").val().length > 0 ? $("#name-tab-user-hidden").val() : "") : "";

    $("#name-tab-user").val(_nameTabUserDefault);
}

function ValidateSearchCPTabUser() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    $("#search-tab-user").val("search");
    $("#name-tab-user-hidden").val($("#name-tab-user").val());

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchCPTabUser();
}

function SearchCPTabUser() {
    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#name-tab-user-hidden").val();

    BoxSearchCondition(1, _searchValue, "search-tab-user-condition");

    var _send = new Array();
    _send[_send.length] = "name=" + $("#name-tab-user-hidden").val();

    SearchData("tabuser", _send, "record-count-cp-tab-user", "list-data-tab-user", "nav-page-tab-user");
}

function ResetFrmSearchStudentWithResult() {
    $("#id-name-search-student").val("");
    InitCombobox("facultysearchstudent", "0", "0", 390, 415);

    $("#record-count-student-with-result").html("ค้นหาพบ 0 รายการ");
    $("#list-data-search-student-with-result").html("");
    $("#nav-page-search-student-with-result").html("");

    //ResetListSearchStudentWithResult();
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
    var _faculty = ComboboxGetSelectedValue("facultysearchstudent") != "0" ? ComboboxGetSelectedValue("facultysearchstudent") : "";
    var _program = ComboboxGetSelectedValue("programsearchstudent") != "0" ? ComboboxGetSelectedValue("programsearchstudent") : "";

    _faculty = _faculty.length > 0 ? _faculty.split(";") : "";
    _program = _program.length > 0 ? _program.split(";") : "";

    var _send = new Array();
    _send[_send.length] = "studentid=" + $("#id-name-search-student").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
 
    SearchData("studentwithresult", _send, "record-count-student-with-result", "list-data-search-student-with-result", "nav-page-search-student-with-result");
}

function ViewStudent(_profileStudent) {
    CloseFrm(true, "");
    ViewStudentInAddProfileStudent(_profileStudent);
}

function ResetFrmSearchCPTransBreakContract(_action) {
    var _trackingStatusTransBreakContractDefault = (_action != "clear") ? ($("#trackingstatus-trans-break-contract-hidden").val().length > 0 ? $("#trackingstatus-trans-break-contract-hidden").val() : "0") : "0";
    var _idNameTransBreakContractDefault = (_action != "clear") ? ($("#id-name-trans-break-contract-hidden").val().length > 0 ? $("#id-name-trans-break-contract-hidden").val() : "") : "";
    var _facultyTransBreakContractDefault = (_action != "clear") ? ($("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val() : "0") : "0";
    var _programTransBreakContractDefault = (_action != "clear") ? ($("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val() : "") : "";
    var _dateStartTransBreakContractDefault = (_action != "clear") ? ($("#date-start-trans-break-contract-hidden").val().length > 0 ? $("#date-start-trans-break-contract-hidden").val() : "") : "";
    var _dateEndTransBreakContractDefault = (_action != "clear") ? ($("#date-end-trans-break-contract-hidden").val().length > 0 ? $("#date-end-trans-break-contract-hidden").val() : "") : "";

    InitCombobox("trackingstatus-trans-break-contract", "0", _trackingStatusTransBreakContractDefault, 390, 415);
    $("#id-name-trans-break-contract").val(_idNameTransBreakContractDefault);
    InitCombobox("facultytransbreakcontract", "0", _facultyTransBreakContractDefault, 390, 415);
    $("#program-trans-break-contract-hidden").val(_programTransBreakContractDefault);
    $("#date-start-trans-break-contract").val(_dateStartTransBreakContractDefault);
    $("#date-end-trans-break-contract").val(_dateEndTransBreakContractDefault);
    InitCalendarFromTo("#date-start-trans-break-contract", false, "#date-end-trans-break-contract", false);
}

function ValidateSearchCPTransBreakContract() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ((($("#date-start-trans-break-contract").val().length > 0) && ($("#date-end-trans-break-contract").val().length == 0))) || (($("#date-start-trans-break-contract").val().length == 0) && ($("#date-end-trans-break-contract").val().length > 0))) {
        _error = true;
        _msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        _focus = "#date-start-trans-break-contract";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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

    var _faculty = $("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val().split(";") : "";
    var _program = $("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#trackingstatus-trans-break-contract-text-hidden").val();
    _searchValue[_searchValue.length] = $("#id-name-trans-break-contract-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");
    _searchValue[_searchValue.length] = (($("#date-start-trans-break-contract-hidden").val().length > 0) && ($("#date-end-trans-break-contract-hidden").val().length > 0) ? $("#date-start-trans-break-contract-hidden").val() + " - " + $("#date-end-trans-break-contract-hidden").val() : "");

    BoxSearchCondition(5, _searchValue, "search-trans-break-contract-condition");
    
    var _send = new Array();
    _send[_send.length] = "statussend=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "1" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "1" : ""));
    _send[_send.length] = "statusreceiver=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "2" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "3" ? "2" : ""));
    _send[_send.length] = "statusedit=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "4" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : ""));
    _send[_send.length] = "statuscancel=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "5" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : ""));
    _send[_send.length] = "studentid=" + $("#id-name-trans-break-contract-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "datestart=" + $("#date-start-trans-break-contract-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-trans-break-contract-hidden").val();

    SearchData("transbreakcontract", _send, "record-count-cp-trans-break-contract", "list-data-trans-break-contract", "nav-page-trans-break-contract");
}

function ResetFrmSearchCPTransRepayContract(_action) {
    var _repayStatusTransRepayContractDefault = (_action != "clear") ? ($("#repaystatus-trans-repay-contract-hidden").val().length > 0 ? parseInt($("#repaystatus-trans-repay-contract-hidden").val()) + 1 : "0") : "0";
    var _idNameTransRepayContractDefault = (_action != "clear") ? ($("#id-name-trans-repay-contract-hidden").val().length > 0 ? $("#id-name-trans-repay-contract-hidden").val() : "") : "";
    var _facultyTransRepayContractDefault = (_action != "clear") ? ($("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val() : "0") : "0";
    var _programTransRepayContractDefault = (_action != "clear") ? ($("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val() : "") : "";
    var _dateStartTransRepayContractDefault = (_action != "clear") ? ($("#date-start-trans-repay-contract-hidden").val().length > 0 ? $("#date-start-trans-repay-contract-hidden").val() : "") : "";
    var _dateEndTransRepayContractDefault = (_action != "clear") ? ($("#date-end-trans-repay-contract-hidden").val().length > 0 ? $("#date-end-trans-repay-contract-hidden").val() : "") : "";

    InitCombobox("repaystatus-trans-repay-contract", "0", _repayStatusTransRepayContractDefault, 390, 415);
    $("#id-name-trans-repay-contract").val(_idNameTransRepayContractDefault);
    InitCombobox("facultytransrepaycontract", "0", _facultyTransRepayContractDefault, 390, 415);
    $("#program-trans-repay-contract-hidden").val(_programTransRepayContractDefault);
    $("#date-start-trans-repay-contract").val(_dateStartTransRepayContractDefault);
    $("#date-end-trans-repay-contract").val(_dateEndTransRepayContractDefault);
    InitCalendarFromTo("#date-start-trans-repay-contract", false, "#date-end-trans-repay-contract", false);
}

function ValidateSearchCPTransRepayContract() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ((($("#date-start-trans-repay-contract").val().length > 0) && ($("#date-end-trans-repay-contract").val().length == 0))) || (($("#date-start-trans-repay-contract").val().length == 0) && ($("#date-end-trans-repay-contract").val().length > 0))) {
        _error = true;
        _msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        _focus = "#date-start-trans-repay-contract";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    $("#search-trans-repay-contract").val("search");
    $("#repaystatus-trans-repay-contract-hidden").val(ComboboxGetSelectedValue("repaystatus-trans-repay-contract") != "0" ? ComboboxGetSelectedValue("repaystatus-trans-repay-contract") - 1 : "");
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
    var _faculty = $("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val().split(";") : "";
    var _program = $("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#repaystatus-trans-repay-contract-text-hidden").val();
    _searchValue[_searchValue.length] = $("#id-name-trans-repay-contract-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");
    _searchValue[_searchValue.length] = (($("#date-start-trans-repay-contract-hidden").val().length > 0) && ($("#date-end-trans-repay-contract-hidden").val().length > 0) ? $("#date-start-trans-repay-contract-hidden").val() + " - " + $("#date-end-trans-repay-contract-hidden").val() : "");

    BoxSearchCondition(5, _searchValue, "search-trans-repay-contract-condition");

    var _statusRepay = SetStatusRepay();
    var _send = new Array();
    _send[_send.length] = "statusrepay=" + _statusRepay[0];
    _send[_send.length] = "statusreply=" + _statusRepay[1];
    _send[_send.length] = "replyresult=" + _statusRepay[2];
    _send[_send.length] = "statuspayment=" + _statusRepay[3];
    _send[_send.length] = "studentid=" + $("#id-name-trans-repay-contract-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "datestart=" + $("#date-start-trans-repay-contract-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-trans-repay-contract-hidden").val();

    SearchData("transrepaycontract", _send, "record-count-cp-trans-repay-contract", "list-data-trans-repay-contract", "nav-page-trans-repay-contract");
}

function ResetFrmSearchCPTransPayment(_action) {
    var _paymentStatusTransPaymentDefault = (_action != "clear") ? ($("#paymentstatus-trans-payment-hidden").val().length > 0 ? $("#paymentstatus-trans-payment-hidden").val() : "0") : "0";
    var _paymentRecordStatusTransPaymentDefault = (_action != "clear") ? ($("#paymentrecordstatus-trans-payment-hidden").val().length > 0 ? $("#paymentrecordstatus-trans-payment-hidden").val() : "0") : "0";
    var _idNameTransPaymentDefault = (_action != "clear") ? ($("#id-name-trans-payment-hidden").val().length > 0 ? $("#id-name-trans-payment-hidden").val() : "") : "";
    var _facultyTransPaymentDefault = (_action != "clear") ? ($("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val() : "0") : "0";
    var _programTransPaymentDefault = (_action != "clear") ? ($("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val() : "") : "";
    var _dateStartTransRepay1ReplyDefault = (_action != "clear") ? ($("#date-start-trans-repay1-reply-hidden").val().length > 0 ? $("#date-start-trans-repay1-reply-hidden").val() : "") : "";
    var _dateEndTransRepay1ReplyDefault = (_action != "clear") ? ($("#date-end-trans-repay1-reply-hidden").val().length > 0 ? $("#date-end-trans-repay1-reply-hidden").val() : "") : "";

    InitCombobox("paymentstatus-trans-payment", "0", _paymentStatusTransPaymentDefault, 336, 361);
    InitCombobox("paymentrecordstatus-trans-payment", "0", _paymentRecordStatusTransPaymentDefault, 336, 361);
    $("#id-name-trans-payment").val(_idNameTransPaymentDefault);
    InitCombobox("facultytranspayment", "0", _facultyTransPaymentDefault, 336, 361);
    $("#program-trans-payment-hidden").val(_programTransPaymentDefault);    
    $("#date-start-trans-repay1-reply").val(_dateStartTransRepay1ReplyDefault);
    $("#date-end-trans-repay1-reply").val(_dateEndTransRepay1ReplyDefault);
    InitCalendarFromTo("#date-start-trans-repay1-reply", false, "#date-end-trans-repay1-reply", false);
}

function ValidateSearchCPTransPayment() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ((($("#date-start-trans-repay1-reply").val().length > 0) && ($("#date-end-trans-repay1-reply").val().length == 0))) || (($("#date-start-trans-repay1-reply").val().length == 0) && ($("#date-end-trans-repay1-reply").val().length > 0))) {
        _error = true;
        _msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        _focus = "#date-start-trans-repay1-reply";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _faculty = $("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val().split(";") : "";
    var _program = $("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#paymentstatus-trans-payment-text-hidden").val();
    _searchValue[_searchValue.length] = $("#id-name-trans-payment-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");
    _searchValue[_searchValue.length] = (($("#date-start-trans-repay1-reply-hidden").val().length > 0) && ($("#date-end-trans-repay1-reply-hidden").val().length > 0) ? $("#date-start-trans-repay1-reply-hidden").val() + " - " + $("#date-end-trans-repay1-reply-hidden").val() : "");
    _searchValue[_searchValue.length] = $("#paymentrecordstatus-trans-payment-text-hidden").val();

    BoxSearchCondition(6, _searchValue, "search-trans-payment-condition");

    var _send = new Array();
    _send[_send.length] = "statuspayment=" + $("#paymentstatus-trans-payment-hidden").val();
    _send[_send.length] = "statuspaymentrecord=" + $("#paymentrecordstatus-trans-payment-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-trans-payment-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "datestart=" + $("#date-start-trans-repay1-reply-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-trans-repay1-reply-hidden").val();

    SearchData("transpayment", _send, "record-count-cp-trans-payment", "list-data-trans-payment", "nav-page-trans-payment");
}

function ResetFrmSearchCPReportTableCalCapitalAndInterest(_action) {
    var _idNameReportTableCalCapitalAndInterestDefault = (_action != "clear") ? ($("#id-name-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#id-name-report-table-cal-capital-and-interest-hidden").val() : "") : "";
    var _facultyReportTableCalCapitalAndInteresttDefault = (_action != "clear") ? ($("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val() : "0") : "0";
    var _programReportTableCalCapitalAndInterestDefault = (_action != "clear") ? ($("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val() : "") : "";

    $("#id-name-report-table-cal-capital-and-interest").val(_idNameReportTableCalCapitalAndInterestDefault);
    InitCombobox("facultyreporttablecalcapitalandinterest", "0", _facultyReportTableCalCapitalAndInteresttDefault, 390, 415);
    $("#program-report-table-cal-capital-and-interest-hidden").val(_programReportTableCalCapitalAndInterestDefault);
}

function ValidateSearchCPReportTableCalCapitalAndInterest() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _faculty = $("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val().split(";") : "";
    var _program = $("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#id-name-report-table-cal-capital-and-interest-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");

    BoxSearchCondition(3, _searchValue, "search-report-table-cal-capital-and-interest-condition");

    var _send = new Array();
    _send[_send.length] = "studentid=" + $("#id-name-report-table-cal-capital-and-interest-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

    SearchData("reporttablecalcapitalandinterest", _send, "record-count-cp-report-table-cal-capital-and-interest", "list-data-report-table-cal-capital-and-interest", "nav-page-report-table-cal-capital-and-interest");
}

function ResetFrmSearchCPReportStepOfWork(_action) {
    var _stepOfWorkStatusReportStepOfWorkDefault = (_action != "clear") ? ($("#stepofworkstatus-report-step-of-work-hidden").val().length > 0 ? $("#stepofworkstatus-report-step-of-work-hidden").val() : "0") : "0";
    var _idNameReportStepOfWorkDefault = (_action != "clear") ? ($("#id-name-report-step-of-work-hidden").val().length > 0 ? $("#id-name-report-step-of-work-hidden").val() : "") : "";
    var _facultyReportStepOfWorkDefault = (_action != "clear") ? ($("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val() : "0") : "0";
    var _programReportStepOfWorkDefault = (_action != "clear") ? ($("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val() : "") : "";

    InitCombobox("stepofworkstatus-report-step-of-work", "0", _stepOfWorkStatusReportStepOfWorkDefault, 390, 415);
    $("#id-name-report-step-of-work").val(_idNameReportStepOfWorkDefault);
    InitCombobox("facultyreportstepofwork", "0", _facultyReportStepOfWorkDefault, 390, 415);
    $("#program-report-step-of-work-hidden").val(_programReportStepOfWorkDefault);
}

function ValidateSearchCPReportStepOfWork() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _faculty = $("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val().split(";") : "";
    var _program = $("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");
    
    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#stepofworkstatus-report-step-of-work-text-hidden").val();
    _searchValue[_searchValue.length] = $("#id-name-report-step-of-work-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");

    BoxSearchCondition(4, _searchValue, "search-report-step-of-work-condition");

    var _send = new Array();
    _send[_send.length] = "statusstepofwork=" + $("#stepofworkstatus-report-step-of-work-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-report-step-of-work-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

    SearchData("reportstepofwork", _send, "record-count-cp-report-step-of-work", "list-data-report-step-of-work", "nav-page-report-step-of-work");
}

function ResetFrmSearchReportStepOfWorkOnStatisticRepayByProgram() {
    var _faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var _program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");

    $("#statistic-repay-by-program-acadamicyear").html("25" + $("#acadamicyear-hidden").val());
    $("#statistic-repay-by-program-faculty").html(_faculty[0] + " - " + _faculty[1])
    $("#statistic-repay-by-program-program").html(_program[0] + " - " + _program[1] + (_program[3] != "0" ? " ( กลุ่ม " + _program[3] + " )" : ""));

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
    var _faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var _program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
    var _send = new Array();
    _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-search-report-step-of-work").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

    SearchData("reportstepofworkonstatisticrepaybyprogram", _send, "record-count-report-step-of-work", "list-data-search-report-step-of-work", "nav-page-search-report-step-of-work");
}

function SearchReportStatisticRepayByProgram() {
    var _send = new Array();
    _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();

    SearchData("reportstatisticrepaybyprogram", _send, "record-count-cp-report-statistic-repay-by-program", "list-data-report-statistic-repay-by-program", "");
}

function ResetFrmSearchReportStudentOnStatisticContractByProgram() {
    var _faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var _program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");

    $("#statistic-contract-by-program-acadamicyear").html("25" + $("#acadamicyear-hidden").val());
    $("#statistic-contract-by-program-faculty").html(_faculty[0] + " - " + _faculty[1])
    $("#statistic-contract-by-program-program").html(_program[0] + " - " + _program[1] + (_program[3] != "0" ? " ( กลุ่ม " + _program[3] + " )" : ""));

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

function SearchReportStudentOnStatisticContractByProgram(_searchTab) {
    var _faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var _program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");
    var _send = new Array();
    _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-search-report-student-contract").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "searchtab=" + _searchTab;

    var _idRecordCount;
    var _idListSearch;
    var _idNavPageSearch;

    if (_searchTab == 1) {
        _idRecordCount = "record-count-student-sign-contract";
        _idListSearch = "list-data-student-sign-contract";
        _idNavPageSearch = "nav-page-student-sign-contract";
    }

    if (_searchTab == 2) {
        _idRecordCount = "record-count-student-contract-penalty";
        _idListSearch = "list-data-student-contract-penalty";
        _idNavPageSearch = "nav-page-student-contract-penalty";
    }

    SearchData("reportstudentonstatisticcontractbyprogram", _send, _idRecordCount, _idListSearch, _idNavPageSearch);
}

function SearchReportStatisticContractByProgram() {
    var _send = new Array();
    _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();

    SearchData("reportstatisticcontractbyprogram", _send, "record-count-cp-report-statistic-contract-by-program", "list-data-report-statistic-contract-by-program", "");
}

function ResetFrmSearchCPReportNoticeRepayComplete(_action) {
    var _idNameReportNoticeRepayCompleteDefault = (_action != "clear") ? ($("#id-name-report-notice-repay-complete-hidden").val().length > 0 ? $("#id-name-report-notice-repay-complete-hidden").val() : "") : "";
    var _facultyReportNoticeRepayCompleteDefault = (_action != "clear") ? ($("#faculty-report-notice-repay-complete-hidden").val().length > 0 ? $("#faculty-report-notice-repay-complete-hidden").val() : "0") : "0";
    var _programReportNoticeRepayCompleteDefault = (_action != "clear") ? ($("#program-report-notice-repay-complete-hidden").val().length > 0 ? $("#program-report-notice-repay-complete-hidden").val() : "") : "";

    $("#id-name-report-notice-repay-complete").val(_idNameReportNoticeRepayCompleteDefault);
    InitCombobox("facultyreportnoticerepaycomplete", "0", _facultyReportNoticeRepayCompleteDefault, 390, 415);
    $("#program-report-notice-repay-complete-hidden").val(_programReportNoticeRepayCompleteDefault);
}

function ValidateSearchCPReportNoticeRepayComplete() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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

    var _faculty = $("#faculty-report-notice-repay-complete-hidden").val().length > 0 ? $("#faculty-report-notice-repay-complete-hidden").val().split(";") : "";
    var _program = $("#program-report-notice-repay-complete-hidden").val().length > 0 ? $("#program-report-notice-repay-complete-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#id-name-report-notice-repay-complete-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");

    BoxSearchCondition(3, _searchValue, "search-report-notice-repay-complete-condition");

    var _send = new Array();
    _send[_send.length] = "studentid=" + $("#id-name-report-notice-repay-complete-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    
    SearchData("reportnoticerepaycomplete", _send, "record-count-cp-report-notice-repay-complete", "list-data-report-notice-repay-complete", "nav-page-report-notice-repay-complete");
}

function ResetFrmSearchCPReportNoticeClaimDebt(_action) {
    var _idNameReportNoticeClaimDebtDefault = (_action != "clear") ? ($("#id-name-report-notice-claim-debt-hidden").val().length > 0 ? $("#id-name-report-notice-claim-debt-hidden").val() : "") : "";
    var _facultyReportNoticeClaimDebtDefault = (_action != "clear") ? ($("#faculty-report-notice-claim-debt-hidden").val().length > 0 ? $("#faculty-report-notice-claim-debt-hidden").val() : "0") : "0";
    var _programReportNoticeClaimDebtDefault = (_action != "clear") ? ($("#program-report-notice-claim-debt-hidden").val().length > 0 ? $("#program-report-notice-claim-debt-hidden").val() : "") : "";

    $("#id-name-report-notice-claim-debt").val(_idNameReportNoticeClaimDebtDefault);
    InitCombobox("facultyreportnoticeclaimdebt", "0", _facultyReportNoticeClaimDebtDefault, 390, 415);
    $("#program-report-notice-claim-debt-hidden").val(_programReportNoticeClaimDebtDefault);
}

function ValidateSearchCPReportNoticeClaimDebt() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) {
      DialogMessage(_msg, _focus, false, "");
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
    var _faculty = $("#faculty-report-notice-claim-debt-hidden").val().length > 0 ? $("#faculty-report-notice-claim-debt-hidden").val().split(";") : "";
    var _program = $("#program-report-notice-claim-debt-hidden").val().length > 0 ? $("#program-report-notice-claim-debt-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#id-name-report-notice-claim-debt-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");

    BoxSearchCondition(3, _searchValue, "search-report-notice-claim-debt-condition");

    var _send = new Array();
    _send[_send.length] = "studentid=" + $("#id-name-report-notice-claim-debt-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

    SearchData("reportnoticeclaimdebt", _send, "record-count-cp-report-notice-claim-debt", "list-data-report-notice-claim-debt", "nav-page-report-notice-claim-debt");
}

function SearchReportStatisticPaymentByDate() {
    var _faculty = $("#faculty-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#faculty-report-statistic-payment-by-date-hidden").val().split(";") : "";
    var _program = $("#program-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#program-report-statistic-payment-by-date-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");
    var _formatPayment = $("#format-payment-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#format-payment-report-statistic-payment-by-date-hidden").val().split(";") : "";

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#id-name-report-statistic-payment-by-date-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");
    _searchValue[_searchValue.length] = (_formatPayment[0] != null ? _formatPayment[1] : "");
    _searchValue[_searchValue.length] = (($("#date-start-report-statistic-payment-by-date-hidden").val().length > 0) && ($("#date-end-report-statistic-payment-by-date-hidden").val().length > 0) ? $("#date-start-report-statistic-payment-by-date-hidden").val() + " - " + $("#date-end-report-statistic-payment-by-date-hidden").val() : "");

    BoxSearchCondition(5, _searchValue, "search-report-statistic-payment-by-date-condition");

    var _send = new Array();
    _send[_send.length] = "studentid=" + $("#id-name-report-statistic-payment-by-date-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");
    _send[_send.length] = "datestart=" + $("#date-start-report-statistic-payment-by-date-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-report-statistic-payment-by-date-hidden").val();
    
    SearchData("reportstatisticpaymentbydate", _send, "record-count-cp-report-statistic-payment-by-date", "list-data-report-statistic-payment-by-date", "nav-page-report-statistic-payment-by-date");
}

function ResetFrmSearchCPReportStatisticPaymentByDate(_action) {
    var _idNameReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#id-name-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#id-name-report-statistic-payment-by-date-hidden").val() : "") : "";
    var _facultyReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#faculty-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#faculty-report-statistic-payment-by-date-hidden").val() : "0") : "0";
    var _programReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#program-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#program-report-statistic-payment-by-date-hidden").val() : "") : "";
    var _formatPaymentReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#format-payment-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#format-payment-report-statistic-payment-by-date-hidden").val() : "0") : "0";
    var _dateStartReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#date-start-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#date-start-report-statistic-payment-by-date-hidden").val() : "") : "";
    var _dateEndReportStatisticPaymentByDateDefault = (_action != "clear") ? ($("#date-end-report-statistic-payment-by-date-hidden").val().length > 0 ? $("#date-end-report-statistic-payment-by-date-hidden").val() : "") : "";

    $("#id-name-report-statistic-payment-by-date").val(_idNameReportStatisticPaymentByDateDefault);
    InitCombobox("facultyreportstatisticpaymentbydate", "0", _facultyReportStatisticPaymentByDateDefault, 390, 415);
    $("#program-report-statistic-payment-by-date-hidden").val(_programReportStatisticPaymentByDateDefault);
    InitCombobox("formatpaymentreportstatisticpaymentbydate", "0", _formatPaymentReportStatisticPaymentByDateDefault, 390, 415);
    $("#date-start-report-statistic-payment-by-date").val(_dateStartReportStatisticPaymentByDateDefault);
    $("#date-end-report-statistic-payment-by-date").val(_dateEndReportStatisticPaymentByDateDefault);
    InitCalendarFromTo("#date-start-report-statistic-payment-by-date", false, "#date-end-report-statistic-payment-by-date", false);
}

function ValidateSearchCPReportStatisticPaymentByDate() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ((($("#date-start-report-statistic-payment-by-date").val().length > 0) && ($("#date-end-report-statistic-payment-by-date").val().length == 0))) || (($("#date-start-report-statistic-payment-by-date").val().length == 0) && ($("#date-end-report-statistic-payment-by-date").val().length > 0))) {
        _error = true;
        _msg = "กรุณาใส่ช่วงวันที่ให้ครบถ้วน";
        _focus = "#date-start-report-statistic-payment-by-date";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _faculty = $("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val().split(";") : "";
    var _program = $("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val().split(";") : "";
    var _groupNum = (_program[3] != null && _program[3] != "0" ? " ( กลุ่ม " + _program[3] + " ) " : "");

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = $("#acadamicyear-report-e-contract-hidden").val();
    _searchValue[_searchValue.length] = $("#id-name-report-e-contract-hidden").val();
    _searchValue[_searchValue.length] = (_faculty[0] != null ? _faculty[0] + " - " + _faculty[1] : "");
    _searchValue[_searchValue.length] = (_program[0] != null ? _program[0] + " - " + _program[1] + _groupNum : "");

    BoxSearchCondition(4, _searchValue, "search-report-e-contract-condition");

    var _send = new Array();
    _send[_send.length] = "acadamicyear=" + $("#acadamicyear-report-e-contract-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-report-e-contract-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    
    SearchData("reportecontract", _send, "record-count-cp-report-e-contract", "list-data-report-e-contract", "nav-page-report-e-contract");
}

function ResetFrmSearchCPReportEContract(_action) {
    var _acadamicYearReportEContractDefault = (_action != "clear") ? ($("#acadamicyear-report-e-contract-hidden").val().length > 0 ? $("#acadamicyear-report-e-contract-hidden").val() : "0") : "0";
    var _idNameReportEContractDefault = (_action != "clear") ? ($("#id-name-report-e-contract-hidden").val().length > 0 ? $("#id-name-report-e-contract-hidden").val() : "") : "";
    var _facultyReportEContractDefault = (_action != "clear") ? ($("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val() : "0") : "0";
    var _programReportEContractDefault = (_action != "clear") ? ($("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val() : "") : "";

    InitCombobox("acadamicyear-report-e-contract", "0", _acadamicYearReportEContractDefault, 390, 415);
    $("#id-name-report-e-contract").val(_idNameReportEContractDefault);
    InitCombobox("facultyreportecontract", "0", _facultyReportEContractDefault, 390, 415);
    $("#program-report-e-contract-hidden").val(_programReportEContractDefault);
}

function ValidateSearchCPReportEContract() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == true) { 
        DialogMessage(_msg, _focus, false, "");
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

    var _reportOrder = $("#report-debtor-contract-order").val();    
    
    var _searchValue = new Array();
    _searchValue[_searchValue.length] = (($("#date-start-report-debtor-contract-hidden").val().length > 0) && ($("#date-end-report-debtor-contract-hidden").val().length > 0) ? $("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val() : "");

    BoxSearchCondition(1, _searchValue, "search-report-debtor-contract-condition");

    var _send = new Array();
    _send[_send.length] = "reportorder=" + _reportOrder;
    _send[_send.length] = "datestart=" + $("#date-start-report-debtor-contract-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-report-debtor-contract-hidden").val();

    SearchData("reportdebtorcontract", _send, "record-count-cp-report-debtor-contract", "list-data-report-debtor-contract", "");
}

function ResetFrmSearchCPReportDebtorContract(_action) {
    var _dateStartReportDebtorContractDefault = (_action != "clear") ? ($("#date-start-report-debtor-contract-hidden").val().length > 0 ? $("#date-start-report-debtor-contract-hidden").val() : "01/01/2556") : "01/01/2556";
    var _dateEndReportDebtorContractDefault = (_action != "clear") ? ($("#date-end-report-debtor-contract-hidden").val().length > 0 ? $("#date-end-report-debtor-contract-hidden").val() : "") : "";

    $("#date-start-report-debtor-contract").val(_dateStartReportDebtorContractDefault);
    $("#date-end-report-debtor-contract").val(_dateEndReportDebtorContractDefault);
    InitCalendarFromTo("#date-start-report-debtor-contract", false, "#date-end-report-debtor-contract", false);
}

function ValidateSearchCPReportDebtorContract() {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ((($("#date-start-report-debtor-contract").val().length > 0) && ($("#date-end-report-debtor-contract").val().length == 0))) || (($("#date-start-report-debtor-contract").val().length == 0) && ($("#date-end-report-debtor-contract").val().length > 0))) {
        _error = true;
        _msg = "กรุณาใส่ช่วงวันที่รับสภาพหนี้ให้ครบถ้วน";
        _focus = "#date-start-report-debtor-contract";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _formatPayment = $("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "";

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = (($("#date-start-report-debtor-contract-hidden").val().length > 0) && ($("#date-end-report-debtor-contract-hidden").val().length > 0) ? $("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val() : "");
    _searchValue[_searchValue.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    _searchValue[_searchValue.length] = (_formatPayment[0] != null ? _formatPayment[1] : "");

    BoxSearchCondition(3, _searchValue, "search-report-debtor-contract-by-program-condition");

    var _reportOrder = $("#report-debtor-contract-order").val();
    var _faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val().split(";") : "";
    var _program = $("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val().split(";") : "";

    var _send = new Array();
    _send[_send.length] = "reportorder=" + _reportOrder;
    _send[_send.length] = "datestart=" + $("#date-start-report-debtor-contract-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-report-debtor-contract-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-report-debtor-contract-by-program-hidden").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");
    
    SearchData("reportdebtorcontractbyprogram", _send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
}

function SearchStudentDebtorContractByProgram() {
    var _formatPayment = $("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "";

    var _searchValue = new Array();
    _searchValue[_searchValue.length] = (($("#date-start-report-debtor-contract-hidden").val().length > 0) && ($("#date-end-report-debtor-contract-hidden").val().length > 0) ? $("#date-start-report-debtor-contract-hidden").val() + " - " + $("#date-end-report-debtor-contract-hidden").val() : "");
    _searchValue[_searchValue.length] = $("#id-name-report-debtor-contract-by-program-hidden").val();
    _searchValue[_searchValue.length] = (_formatPayment[0] != null ? _formatPayment[1] : "");

    BoxSearchCondition(3, _searchValue, "search-report-debtor-contract-by-program-condition");

    var _reportOrder = $("#report-debtor-contract-order").val();
    var _faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().split(";");
    var _program = $("#program-report-debtor-contract-by-program-hidden").val().split(";");
    var _send = new Array();
    _send[_send.length] = "reportorder=" + _reportOrder;
    _send[_send.length] = "datestart=" + $("#date-start-report-debtor-contract-hidden").val();
    _send[_send.length] = "dateend=" + $("#date-end-report-debtor-contract-hidden").val();
    _send[_send.length] = "studentid=" + $("#id-name-report-debtor-contract-by-program").val();
    _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
    _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
    _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
    _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
    _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");
    
    SearchData("reportdebtorcontractbyprogram", _send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
}

function ResetFrmSearchStudentDebtorContractByProgram(_action) {
    var _faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().split(";");
    var _program = $("#program-report-debtor-contract-by-program-hidden").val().split(";");
    var _idNameReportDebtorContractByProgramDefault = (_action != "clear") ? ($("#id-name-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#id-name-report-debtor-contract-by-program-hidden").val() : "") : "";
    var _formatPaymentReportDebtorContractByProgramDefault = (_action != "clear") ? ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val() : "0") : "0";

    $("#student-debtor-contract-by-program-faculty").html(_faculty[0] + " - " + _faculty[1])
    $("#student-debtor-contract-by-program-program").html(_program[0] + " - " + _program[1] + (_program[3] != "0" ? " ( กลุ่ม " + _program[3] + " )" : ""));
    $("#id-name-report-debtor-contract-by-program").val(_idNameReportDebtorContractByProgramDefault);
    InitCombobox("formatpaymentreportdebtorcontractbyprogram", "0", _formatPaymentReportDebtorContractByProgramDefault, 278, 303);
}

function ValidateSearchStudentDebtorContractByProgram() {
    $("#search-report-debtor-contract-by-program").val("search");
    $("#id-name-report-debtor-contract-by-program-hidden").val($("#id-name-report-debtor-contract-by-program").val());
    $("#format-payment-report-debtor-contract-by-program-hidden").val(ComboboxGetSelectedValue("formatpaymentreportdebtorcontractbyprogram") != "0" ? ComboboxGetSelectedValue("formatpaymentreportdebtorcontractbyprogram") : "")

    $("#dialog-form1").dialog("close");

    SetMsgLoading("กำลังค้นหา...");

    SearchStudentDebtorContractByProgram();
}