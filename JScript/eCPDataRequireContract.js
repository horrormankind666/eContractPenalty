function ReceiverCPTransBreakContract(_cp1id, _trackingStatus) {
  ChkTrackingStatusViewTransBreakContract(_cp1id, _trackingStatus, "", function (_result) {
    if (_result == "0") {
      $("#dialog-form1").dialog("close");
      OpenTab("link-tab3-cp-trans-require-contract", "#tab3-cp-trans-require-contract", "รับรายการแจ้ง", false, "add", _cp1id, _trackingStatus);
    }
  });
}
//ปรับปรุงเมื่อ ๐๔/๐๔/๒๕๖๒
//---------------------------------------------------------------------------------------------------
function InitStudyLeaveYesNo() {
  if ($("#study-leave-yesno").length > 0) {
    $("input[name=study-leave-yesno]:radio").click(function () {
      ResetFrmStudyLeaveYesNo();
    });
  }
}

function ResetFrmStudyLeaveYesNo() {
  var _studyLeaveYesNo = $("input[name=study-leave-yesno]:checked").val();

  $("#study-leave-yesno #study-leave-status-yes .calendar").val("");
  $("#study-leave-yesno #study-leave-status-no .calendar").val("");
  CalendarDisable("#study-leave-yesno #study-leave-status-yes .calendar");
  CalendarDisable("#study-leave-yesno #study-leave-status-no .calendar");

  if (_studyLeaveYesNo == "Y") CalendarEnable("#study-leave-yesno #study-leave-status-yes .calendar");
  if (_studyLeaveYesNo == "N") CalendarEnable("#study-leave-yesno #study-leave-status-no .calendar");

  $("#all-actual-date").val("");
  $("#actual-date").val("");
  $("#remain-date").val("");
  $("#subtotal-penalty").val("");
  $("#total-penalty").val("");
}
//---------------------------------------------------------------------------------------------------
function InitCPTransRequireContract() {
  $("#lawyer-phonenumber").inputmask("9-9999-9999");
  $("#lawyer-mobilenumber").inputmask("999-9999999");
  $("#lawyer-email").inputmask("email");
}

function ResetFrmCPTransRequireContract(_disable) {
  GoToElement("top-page");

  if (_disable == true) {
    TextboxDisable("#contract-date");
    TextboxDisable("#contract-date-agreement");
    TextboxDisable("#guarantor");
    TextboxDisable("#scholar");
    TextboxDisable("#scholarship-money");
    TextboxDisable("#scholarship-year");
    TextboxDisable("#scholarship-month");
    TextboxDisable("#education-date-start");
    TextboxDisable("#education-date-end");
    TextboxDisable("#case-graduate");
    TextboxDisable("#civil");
    TextboxDisable("#contract-force-date-start");
    TextboxDisable("#contract-force-date-end");
    TextboxDisable("#indemnitor-year");
    TextboxDisable("#indemnitor-cash");
    TextboxDisable("#indemnitor-address");
    ComboboxDisable("province");
    /*
    ก่อนปรับปรุง
    CalendarDisable("#require-date");
    CalendarDisable("#approve-date");
    */
    //ปรับปรุงเมื่อ ๐๙/๐๔/๒๕๖๒
    //---------------------------------------------------------------------------------------------------
    $("input[name=study-leave-yesno]:radio").prop("disabled", true);
    CalendarDisable("#study-leave-yesno #study-leave-status-yes .calendar");
    CalendarDisable("#study-leave-yesno #study-leave-status-no .calendar");
    //---------------------------------------------------------------------------------------------------
    ButtonDisable("#cal-contract-penalty-button", "button-style2-disable");
    ButtonDisable("#view-cal-date-button", "button-style2-disable");
    TextboxDisable("#all-actual-month-scholarship");
    TextboxDisable("#all-actual-scholarship");
    TextboxDisable("#total-pay-scholarship");
    TextboxDisable("#all-actual-month");
    TextboxDisable("#all-actual-day");
    TextboxDisable("#all-actual-date");
    TextboxDisable("#actual-date");
    TextboxDisable("#remain-date");
    TextboxDisable("#subtotal-penalty");
    TextboxDisable("#total-penalty");
    TextboxDisable("#lawyer-fullname");
    TextboxDisable("#lawyer-phonenumber");
    TextboxDisable("#lawyer-mobilenumber");
    TextboxDisable("#lawyer-email");
    $("#button-style11").hide();
    $("#button-style12").show();
    return;
  }

  TextboxDisable("#contract-date");
  TextboxDisable("#contract-date-agreement");
  TextboxDisable("#guarantor");
  TextboxDisable("#scholar");
  TextboxDisable("#scholarship-money");
  TextboxDisable("#scholarship-year");
  TextboxDisable("#scholarship-month");
  TextboxDisable("#education-date-start");
  TextboxDisable("#education-date-end");
  TextboxDisable("#case-graduate");
  TextboxDisable("#civil");
  TextboxDisable("#contract-force-date-start");
  TextboxDisable("#contract-force-date-end");
  $("#indemnitor-year").val($("#indemnitor-year-hidden").val());
  $("#indemnitor-cash").val($("#indemnitor-cash-hidden").val());

  if ($("#case-graduate-break-contract-hidden").val() == "1") {
    TextboxDisable("#indemnitor-year");       
    $("#all-actual-month-scholarship").val($("#all-actual-month-scholarship-hidden").val());
    if ($("#scholar-hidden").val() == "2") TextboxDisable("#all-actual-month-scholarship");
    $("#all-actual-scholarship").val($("#all-actual-scholarship-hidden").val());
    TextboxDisable("#all-actual-scholarship");
    $("#total-pay-scholarship").val($("#total-pay-scholarship-hidden").val() == "0.00" ? "" : $("#total-pay-scholarship-hidden").val());        
    TextboxDisable("#total-pay-scholarship");
    $("#all-actual-month").val($("#actual-month-hidden").val());        
    TextboxDisable("#all-actual-month");
    $("#all-actual-day").val($("#actual-day-hidden").val());
    TextboxDisable("#all-actual-day");
  }

  if ($("#case-graduate-break-contract-hidden").val() == "2") {
    if ($("#civil-hidden").val() == "1") {
      $("#indemnitor-address").val($("#indemnitor-address-hidden").val());
      InitCombobox("province", "0", $("#province-id-hidden").val(), 451, 476);
      //ปรับปรุงเมื่อ ๐๕/๐๔/๒๕๖๒
      //---------------------------------------------------------------------------------------------------
      $("input[name=study-leave-yesno]:radio").prop("checked", false);
      $("input[name=study-leave-yesno]:radio").filter("[value='" + $("#study-leave-hidden").val() + "']").prop("checked", true);
      ResetFrmStudyLeaveYesNo();
      //---------------------------------------------------------------------------------------------------
      $("#require-date").val($("#require-date-hidden").val());
      $("#approve-date").val($("#approve-date-hidden").val());
      InitCalendarFromTo("#require-date", false, "#approve-date", false);
      //ปรับปรุงเมื่อ ๐๔/๐๔/๒๕๖๒
      //---------------------------------------------------------------------------------------------------
      $("#before-study-leave-start-date").val($("#before-study-leave-start-date-hidden").val());
      $("#before-study-leave-end-date").val($("#before-study-leave-end-date-hidden").val());
      InitCalendarFromTo("#before-study-leave-start-date", false, "#before-study-leave-end-date", false);
      $("#study-leave-start-date").val($("#study-leave-start-date-hidden").val());
      $("#study-leave-end-date").val($("#study-leave-end-date-hidden").val());
      InitCalendarFromTo("#study-leave-start-date", false, "#study-leave-end-date", false);
      $("#after-study-leave-start-date").val($("#after-study-leave-start-date-hidden").val());
      $("#after-study-leave-end-date").val($("#after-study-leave-end-date-hidden").val());
      InitCalendarFromTo("#after-study-leave-start-date", false, "#after-study-leave-end-date", false);
      //---------------------------------------------------------------------------------------------------
    }
    $("#total-pay-scholarship").val($("#total-pay-scholarship-hidden").val() == "0.00" ? "" : $("#total-pay-scholarship-hidden").val());        
    TextboxDisable("#total-pay-scholarship");
    $("#all-actual-day").val($("#actual-day-hidden").val());
    TextboxDisable("#all-actual-day");
    $("#all-actual-date").val($("#all-actual-date-hidden").val());
    TextboxDisable("#all-actual-date");
    $("#actual-date").val($("#actual-date-hidden").val());
    TextboxDisable("#actual-date");
    $("#remain-date").val($("#remain-date-hidden").val());
    TextboxDisable("#remain-date");
  }
  
  if ($("#set-amt-indemnitor-year").val() == "N")
    TextboxDisable("#indemnitor-year");       
  
  $("#subtotal-penalty").val($("#subtotal-penalty-hidden").val());
  TextboxDisable("#subtotal-penalty");
  $("#total-penalty").val($("#total-penalty-hidden").val());
  TextboxDisable("#total-penalty");
  $("#lawyer-fullname").val($("#lawyer-fullname-hidden").val());
  $("#lawyer-phonenumber").val($("#lawyer-phonenumber-hidden").val());
  $("#lawyer-mobilenumber").val($("#lawyer-mobilenumber-hidden").val());
  $("#lawyer-email").val($("#lawyer-email-hidden").val());
  $("#button-style11").show();
  $("#button-style12").hide();
}

function ValidateLawyer() {
  var _error = false;
  var _msg;
  var _focus;

  if (_error == false && (($("#lawyer-fullname").val().length == 0) || (($("#lawyer-phonenumber").val().length == 0) && ($("#lawyer-mobilenumber").val().length == 0)) || ($("#lawyer-email").val().length == 0))) { _error = true; _msg = "กรุณาใส่นิติกรผูู้รับผิดชอบให้ครบถ้วน"; _focus = "#lawyer-fullname"; }
  if (_error == false && (($("#lawyer-phonenumber").val().length > 0) && ($("#lawyer-phonenumber").inputmask("isComplete") == false))) { _error = true; _msg = "กรุณาใส่หมายเลขโทรศัพท์ของนิติกรผู้รับผิดชอบให้ถูกต้อง"; _focus = "#lawyer-phonenumber"; }
  if (_error == false && (($("#lawyer-mobilenumber").val().length > 0) && ($("#lawyer-mobilenumber").inputmask("isComplete") == false))) { _error = true; _msg = "กรุณาใส่หมายโทรศัพท์มือถือของนิติกรผู้รับผิดชอบให้ถูกต้อง"; _focus = "#lawyer-mobilenumber"; }
  if (_error == false && ($("#lawyer-email").inputmask("isComplete") == false)) { _error = true; _msg = "กรุณาใส่อีเมล์ของนิติกรผู้รับผิดชอบให้ถูกต้อง"; _focus = "#lawyer-email"; }

  if (_error == true) {
    DialogMessage(_msg, _focus, false, "");
    return false;
  }

  return true;
}

function ValidateCPTransRequireContract() { 
  var _error = false;
  var _msg;
  var _focus;
  var _scholar = $("#scholar-hidden").val();
  var _caseGraduate = $("#case-graduate-break-contract-hidden").val();
  var _civil = $("#civil-hidden").val();
  var _setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();
  var _indemnitorYear = $("#indemnitor-year").val();
  var _indemnitorCash = $("#indemnitor-cash").val();
  var _allActualMonthScholarship = (_caseGraduate == "1" ? $("#all-actual-month-scholarship").val() : "");
  
  if (_error == false && (_setAmtIndemnitorYear == "Y") && (_indemnitorYear.length == 0 || _indemnitorYear == "0")) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ต้องปฏิบัติงานชดใช้"; _focus = "#indemnitor-year"; }
  if (_error == false && (_caseGraduate == "2") && (_indemnitorCash.length == 0 || _indemnitorCash == "0")) { _error = true; _msg = "กรุณาใส่จำนวนเงินต้องชดใช้ตามสัญญา"; _focus = "#indemnitor-cash"; }
  if (_error == false && (_indemnitorCash.length == 0 || _indemnitorCash == "0")) { _error = true; _msg = "กรุณาใส่จำนวนเงินต้องชดใช้ตามสัญญา"; _focus = "#indemnitor-cash"; }
  if (_error == false && (_caseGraduate == "2") && (_civil == "1") && (($("#indemnitor-address").val().length == 0) || (ComboboxGetSelectedValue("province") == "0") || ($("input[name=study-leave-yesno]:checked").length == 0))) { _error = true; _msg = "กรุณาใส่รายละเอียดข้อมูลการทำงานชดใช้ให้ครบถ้วน"; _focus = "#indemnitor-address"; }
  if (_error == false && (_caseGraduate == "2") && (_civil == "1") && (($("input[name=study-leave-yesno]:checked").val() == "N") && (($("#require-date").val().length == 0) || ($("#approve-date").val().length == 0)))) { _error = true; _msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีไม่มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ครบถ้วน"; _focus = "#require-date"; }    
  if (_error == false && (_caseGraduate == "2") && (_civil == "1") && (($("input[name=study-leave-yesno]:checked").val() == "Y") && (($("#before-study-leave-start-date").val().length == 0) || ($("#before-study-leave-end-date").val().length == 0) || ($("#study-leave-start-date").val().length == 0) || ($("#study-leave-end-date").val().length == 0) || ($("#after-study-leave-start-date").val().length == 0) || ($("#after-study-leave-end-date").val().length == 0)))) { _error = true; _msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีมีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ครบถ้วน"; _focus = "#before-study-leave-start-date"; }    
  if (_error == false && (_caseGraduate == "2") && (_civil == "1") && (($("#study-leave-start-date").datepicker("getDate") < $("#before-study-leave-end-date").datepicker("getDate")) || ($("#after-study-leave-start-date").datepicker("getDate") < $("#study-leave-end-date").datepicker("getDate")))) { _error = true; _msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีมีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ถูกต้อง"; _focus = "#before-study-leave-start-date"; }    
  if (_error == false && (_scholar == "1") && (_caseGraduate == "1") && (_allActualMonthScholarship.length == 0 || _allActualMonthScholarship == "0")) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ชดใช้ทุนการศึกษา"; _focus = "#all-actual-month-scholarship"; }

  if (_error == true) {
    FillCalPayScholarshipPenalty("");
    DialogMessage(_msg, _focus, false, "");
    return false;
  }

  return true;
}

function CalculatePayScholarshipAndPenalty() {
  if (ValidateCPTransRequireContract() == true) {
    SetMsgLoading("กำลังคำนวณ...");

    var _caseGraduate = $("#case-graduate-break-contract-hidden").val();
    var _civil = $("#civil-hidden").val();    
    var _dateStartID = "";
    var _dateEndID = "";

    if (_caseGraduate == "1") {
      _dateStartID  = "contract-force-date-start-hidden";
      _dateEndID    = "contract-force-date-end-hidden";
    }

    if (_caseGraduate == "2" && _civil == "1") {
      _dateStartID  = "require-date";
      _dateEndID    = "approve-date";
    }

    if (_caseGraduate == "2" && _civil == "2") {
      _dateStartID  = "education-date-start-hidden";
      _dateEndID    = "education-date-end-hidden";
    }

    var _send = new Array();
    _send[_send.length] = "scholar=" + $("#scholar-hidden").val();
    _send[_send.length] = "scholarshipmoney=" + DelCommas("scholarship-money");
    _send[_send.length] = "scholarshipyear=" + ($("#scholarship-year").val().length > 0 ? DelCommas("scholarship-year") : "0");
    _send[_send.length] = "scholarshipmonth=" + ($("#scholarship-month").val().length > 0 ? DelCommas("scholarship-month") : "0");
    _send[_send.length] = "allactualmonthscholarship=" + (_caseGraduate == "1" ? DelCommas("all-actual-month-scholarship") : "");
    _send[_send.length] = "casegraduate=" + _caseGraduate;
    _send[_send.length] = "educationdate=" + $("#education-date-start-hidden").val();
    _send[_send.length] = "graduatedate=" + $("#education-date-end-hidden").val();
    _send[_send.length] = "civil=" + _civil;
    _send[_send.length] = "datestart=" + (_dateStartID.length > 0 ? $("#" + _dateStartID).val() : "");
    _send[_send.length] = "dateend=" + (_dateEndID.length > 0 ? $("#" + _dateEndID).val() : "");
    _send[_send.length] = "indemnitoryear=" + DelCommas("indemnitor-year");
    _send[_send.length] = "indemnitorcash=" + DelCommas("indemnitor-cash");
    _send[_send.length] = "caldatecondition=" + $("#cal-date-condition-hidden").val();
    _send[_send.length] = "studyleave=" + $("input[name=study-leave-yesno]:checked").val();
    _send[_send.length] = "beforestudyleavestartdate=" + $("#before-study-leave-start-date").val();
    _send[_send.length] = "beforestudyleaveenddate=" + $("#before-study-leave-end-date").val();
    _send[_send.length] = "studyleavestartdate=" + $("#study-leave-start-date").val();
    _send[_send.length] = "studyleaveenddate=" + $("#study-leave-end-date").val();
    _send[_send.length] = "afterstudyleavestartdate=" + $("#after-study-leave-start-date").val();
    _send[_send.length] = "afterstudyleaveenddate=" + $("#after-study-leave-end-date").val();

    CalculateFrm("scholarshipandpenalty", _send, function (_result) {
      FillCalPayScholarshipPenalty(_result);
    });
  }
}

function FillCalPayScholarshipPenalty(_result) {
  var _setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();

  if (($("#all-actual-month-scholarship").length > 0) && ($("#all-actual-month-scholarship").val() == "0")) $("#all-actual-month-scholarship").val("");
  if ($("#all-actual-scholarship").length > 0) $("#all-actual-scholarship").val("");
  if ($("#total-pay-scholarship").length > 0) $("#total-pay-scholarship").val("");
  if ($("#all-actual-month").length > 0) $("#all-actual-month").val("");
  if ($("#all-actual-day").length > 0) $("#all-actual-day").val("");
  if ($("#all-actual-date").length > 0) $("#all-actual-date").val("");
  if ($("#actual-date").length > 0) $("#actual-date").val("");
  if ($("#remain-date").length > 0) $("#remain-date").val("");
  if ($("#subtotal-penalty").length > 0) $("#subtotal-penalty").val("");
  if ($("#total-penalty").length > 0) $("#total-penalty").val("");

  if (_result.length > 0) {
    var _dataActualScholarship = _result.split("<allactualscholarship>");
    var _dataTotalPayScholarship = _result.split("<totalpayscholarship>");
    var _dataMonth = _result.split("<month>");
    var _dataDay = _result.split("<day>");
    var _dataAllActual = _result.split("<allactual>");
    var _dataActual = _result.split("<actual>");
    var _dataRemain = _result.split("<remain>");
    var _dataSubtotal = _result.split("<totalpenalty>");
    var _dataTotal = _result.split("<total>");

    if (($("#all-actual-month-scholarship").length > 0) && ($("#all-actual-month-scholarship").val().length == 0)) $("#all-actual-month-scholarship").val("");
    if ($("#all-actual-scholarship").length > 0) $("#all-actual-scholarship").val(_dataActualScholarship[1] == "0.00" ? "" : _dataActualScholarship[1]);
    if ($("#total-pay-scholarship").length > 0) $("#total-pay-scholarship").val(_dataTotalPayScholarship[1] == "0.00" ? "" : _dataTotalPayScholarship[1]);
    if ($("#all-actual-month").length > 0) $("#all-actual-month").val(_dataMonth[1]);
    if ($("#all-actual-day").length > 0) $("#all-actual-day").val(_dataDay[1]);
    if ($("#case-graduate-break-contract-hidden").val() == "2") {
      if (_setAmtIndemnitorYear == "Y") {
        if (_dataAllActual[1] == "0" && _dataActual[1] == "0" && _dataRemain[1] == "0") {
          if ($("#all-actual-date").length > 0) $("#all-actual-date").val("");
          if ($("#actual-date").length > 0) $("#actual-date").val("");
          if ($("#remain-date").length > 0) $("#remain-date").val("");
        }
        else {
          if (parseInt(_dataRemain[1]) <= 0) {
            DialogMessage("ระยะเวลาที่ปฏิบัติงานชดใช้แล้วต้องน้อยกว่าระยะเวลาที่ต้องปฏิบัติงานชดใช้", "", false, "");
            return;
          }

          if ($("#all-actual-date").length > 0) $("#all-actual-date").val(_dataAllActual[1]);
          if ($("#actual-date").length > 0) $("#actual-date").val(_dataActual[1]);
          if ($("#remain-date").length > 0) $("#remain-date").val(_dataRemain[1]);
        }
      }

      if (_setAmtIndemnitorYear == "N") {
        if ($("#all-actual-day").length > 0) $("#all-actual-day").val(_dataAllActual[1]);
        if ($("#actual-date").length > 0) $("#actual-date").val(_dataActual[1]);
      }
    }
    if ($("#subtotal-penalty").length > 0) $("#subtotal-penalty").val(_dataSubtotal[1]);
    if ($("#total-penalty").length > 0) $("#total-penalty").val(_dataTotal[1]);
  }
}

function ConfirmActionCPTransRequireContract(_action) {
  var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

  DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
  $("#dialog-confirm").dialog({
    buttons: {
      "ตกลง": function () {
        $(this).dialog("close");

        if (ValidateCPTransRequireContract() == true && ValidateLawyer() == true) {
          var _caseGraduate = $("#case-graduate-break-contract-hidden").val();
          var _civil = $("#civil-hidden").val();
          var _setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();
          var _dateStartID = "";
          var _dateEndID = "";

          if (_caseGraduate == "1") {
            _dateStartID = "contract-force-date-start-hidden";
            _dateEndID = "contract-force-date-end-hidden";
          }

          if (_caseGraduate == "2" && _civil == "1") {
            _dateStartID = "require-date";
            _dateEndID = "approve-date";
          }

          if (_caseGraduate == "2" && _civil == "2") {
            _dateStartID = "education-date-start-hidden";
            _dateEndID = "education-date-end-hidden";
          }

          var _send = new Array();
          _send[_send.length] = "scholar=" + $("#scholar-hidden").val();
          _send[_send.length] = "scholarshipmoney=" + DelCommas("scholarship-money");
          _send[_send.length] = "scholarshipyear=" + ($("#scholarship-year").val().length > 0 ? DelCommas("scholarship-year") : "0");
          _send[_send.length] = "scholarshipmonth=" + ($("#scholarship-month").val().length > 0 ? DelCommas("scholarship-month") : "0");
          _send[_send.length] = "allactualmonthscholarship=" + (_caseGraduate == "1" ? DelCommas("all-actual-month-scholarship") : "");
          _send[_send.length] = "casegraduate=" + _caseGraduate;
          _send[_send.length] = "educationdate=" + $("#education-date-start-hidden").val();
          _send[_send.length] = "graduatedate=" + $("#education-date-end-hidden").val();
          _send[_send.length] = "civil=" + _civil;
          _send[_send.length] = "datestart=" + (_dateStartID.length > 0 ? $("#" + _dateStartID).val() : "");
          _send[_send.length] = "dateend=" + (_dateEndID.length > 0 ? $("#" + _dateEndID).val() : "");
          _send[_send.length] = "indemnitoryear=" + DelCommas("indemnitor-year");
          _send[_send.length] = "indemnitorcash=" + DelCommas("indemnitor-cash");
          _send[_send.length] = "caldatecondition=" + $("#cal-date-condition-hidden").val();
          _send[_send.length] = "studyleave=" + $("input[name=study-leave-yesno]:checked").val();
          _send[_send.length] = "beforestudyleavestartdate=" + $("#before-study-leave-start-date").val();
          _send[_send.length] = "beforestudyleaveenddate=" + $("#before-study-leave-end-date").val();
          _send[_send.length] = "studyleavestartdate=" + $("#study-leave-start-date").val();
          _send[_send.length] = "studyleaveenddate=" + $("#study-leave-end-date").val();
          _send[_send.length] = "afterstudyleavestartdate=" + $("#after-study-leave-start-date").val();
          _send[_send.length] = "afterstudyleaveenddate=" + $("#after-study-leave-end-date").val();

          CalculateFrm("scholarshipandpenalty", _send, function (_result) {
            FillCalPayScholarshipPenalty(_result);
                        
            var _send1 = new Array();
            _send1[_send1.length] = "cp1id=" + $("#cp1id").val();
            _send1[_send1.length] = "scholar=" + $("#scholar-hidden").val();
            _send1[_send1.length] = "casegraduate=" + _caseGraduate;
            _send1[_send1.length] = "civil=" + _civil;
            _send1[_send1.length] = "indemnitoryear=" + DelCommas("indemnitor-year");
            _send1[_send1.length] = "indemnitorcash=" + DelCommas("indemnitor-cash");
            _send1[_send1.length] = "trackingstatus=" + $("#trackingstatus").val();
            _send1[_send1.length] = "cp2id=" + $("#cp2id").val();

            if (_caseGraduate == "1") {
              _send1[_send1.length] = "actualmonthscholarship=" + DelCommas("all-actual-month-scholarship");
              _send1[_send1.length] = "actualscholarship=" + DelCommas("all-actual-scholarship");
              _send1[_send1.length] = "totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0");
              _send1[_send1.length] = "actualmonth=" + DelCommas("all-actual-month");
              _send1[_send1.length] = "actualday=" + DelCommas("all-actual-day");
              _send1[_send1.length] = "subtotalpenalty=" + DelCommas("subtotal-penalty");
              _send1[_send1.length] = "totalpenalty=" + DelCommas("total-penalty");
            }

            if (_caseGraduate == "2") {
              if (_civil == "1") {
                if (DelCommas("remain-date") > 0 || _setAmtIndemnitorYear == "N") {
                  _send1[_send1.length] = "indemnitoraddress=" + $("#indemnitor-address").val();
                  _send1[_send1.length] = "province=" + ComboboxGetSelectedValue("province");
                  _send1[_send1.length] = "studyleave=" + $("input[name=study-leave-yesno]:checked").val();
                  _send1[_send1.length] = "requiredate=" + $("#require-date").val();
                  _send1[_send1.length] = "approvedate=" + $("#approve-date").val();
                  _send1[_send1.length] = "beforestudyleavestartdate=" + $("#before-study-leave-start-date").val();
                  _send1[_send1.length] = "beforestudyleaveenddate=" + $("#before-study-leave-end-date").val();
                  _send1[_send1.length] = "studyleavestartdate=" + $("#study-leave-start-date").val();
                  _send1[_send1.length] = "studyleaveenddate=" + $("#study-leave-end-date").val();
                  _send1[_send1.length] = "afterstudyleavestartdate=" + $("#after-study-leave-start-date").val();
                  _send1[_send1.length] = "afterstudyleaveenddate=" + $("#after-study-leave-end-date").val();
                  _send1[_send1.length] = "totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0");

                  if (_setAmtIndemnitorYear == "Y") {
                    _send1[_send1.length] = "allactualdate=" + DelCommas("all-actual-date");
                    _send1[_send1.length] = "actualdate=" + DelCommas("actual-date");
                    _send1[_send1.length] = "remaindate=" + DelCommas("remain-date");
                  }
                  if (_setAmtIndemnitorYear == "N") {
                    _send1[_send1.length] = "actualday=" + DelCommas("all-actual-day");
                    _send1[_send1.length] = "actualdate=" + DelCommas("actual-date");
                  }

                  _send1[_send1.length] = "subtotalpenalty=" + DelCommas("subtotal-penalty");
                  _send1[_send1.length] = "totalpenalty=" + DelCommas("total-penalty");
                }
                else {
                  if (DelCommas("remain-date") <= 0) {
                    DialogMessage("ปฏิบัติงานชดใช้ครบตามสัญญาแล้ว", "#require-date", false, "");
                    return;
                  }
                }                
              }
              else {
                if (_setAmtIndemnitorYear == "N") {
                  _send1[_send1.length] = "actualday=" + DelCommas("all-actual-day");
                }

                _send1[_send1.length] = "totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0");
                _send1[_send1.length] = "subtotalpenalty=" + DelCommas("subtotal-penalty");
                _send1[_send1.length] = "totalpenalty=" + DelCommas("total-penalty");
              }
            }

            _send1[_send1.length] = "lawyerfullname=" + $("#lawyer-fullname").val();
            _send1[_send1.length] = "lawyerphonenumber=" + $("#lawyer-phonenumber").val();
            _send1[_send1.length] = "lawyermobilenumber=" + $("#lawyer-mobilenumber").val();
            _send1[_send1.length] = "lawyeremail=" + $("#lawyer-email").val();

            ChkTrackingStatusViewTransBreakContract($("#cp1id").val(), $("#trackingstatus").val(), "", function (_result) {
              if (_result == "0")
                  AddUpdateCPTransRequireContract(_action, _send1);
            });
          });
        }
      },
      "ยกเลิก": function () {
        $(this).dialog("close");
      }
    }
  });
}

function AddUpdateCPTransRequireContract(_action, _send) {
  var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

  AddUpdateData(_action, _action + "cptransrequirecontract", _send, false, "", "", "", false, function (_result) {
    if (_result == "1") {
      GotoSignin();
      return;
    }

    DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
    $("#dialog-confirm").dialog({
      buttons: {
        "ตกลง": function () {
          $(this).dialog("close");

          ResetFrmCPTransRequireContract(true);
        }
      }
    });
  });
}

function ChkRepayStatusViewTransRequireContract(_cp1id, _callbackFunc) {
  var _send = new Array();
  _send[_send.length] = "cp1id=" + _cp1id;

  SetMsgLoading("");

  ViewData("repaystatustransrequirecontract", _send, function (_result) {
    var _dataRepayStatus = _result.split("<repaystatus>");

    if (_dataRepayStatus[1].length <= 0) {
      DialogMessage("ไม่พบข้อมูล", "", false, "");
      _callbackFunc(_dataRepayStatus[1]);
      return;
    }

    _callbackFunc(_dataRepayStatus[1]);
  });
}

function ViewRepayStatusViewTransRequireContract(_cp1id, _cp2id, _trackingStatus, _action) {
  ChkRepayStatusViewTransRequireContract(_cp1id, function (_result) {
    if ((_result == "0") && (_action == "e")) OpenTab("link-tab3-cp-trans-require-contract", "#tab3-cp-trans-require-contract", "ปรับปรุงรายการรับแจ้ง", false, "update", _cp1id, _trackingStatus);
    if ((_result == "1") && (_action == "e")) LoadForm(1, "detailcptransrequirecontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
    if ((_result == "2") && (_action == "e")) LoadForm(1, "detailcptransrequirecontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
    if ((_result == "0") && (_action == "r")) LoadForm(1, "repaycptransrequirecontract", true, "", _cp1id, "repay" + _cp2id);
    if ((_result == "1") && (_action == "r")) LoadForm(1, "repaycptransrequirecontract", true, "", _cp1id, "repay" + _cp2id);
    if ((_result == "2") && (_action == "r")) LoadForm(1, "repaycptransrequirecontract", true, "", _cp1id, "repay" + _cp2id);
  });
}