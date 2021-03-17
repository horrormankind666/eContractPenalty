function ResetFrmSelectFormatPayment() {
  var _formatPayment = $("input[name=format-payment]:radio");

  _formatPayment.attr("checked", false);        
}

function CaptionFormatPayment() {
  var _send = new Array();
  _send[0] = "formatpayment=" + $("#format-payment-hidden").val();

  SetMsgLoading("");

  ViewData("formatpayment", _send, function (_result) {
    $("#format-payment-input span").html(_result);
  });
}

function ResetFormatPayment() {    
  if (($("#format-payment-hidden").val().length == 0) || ($("#format-payment-hidden").val() == "0")) {
    var _formatPayment = $("input[name=format-payment]:checked");
    var _valCheck;

    _formatPayment.each(function (i) {
      _valCheck = this.value;
    });

    $("#format-payment-hidden").val(_valCheck);
  }
}

function ResetListTransPayment() {    
  if ($("#statuspayment-hidden").val() == "3") $("#tab2-adddetail-cp-trans-payment").hide();

  CaptionFormatPayment();

  /*
  $("#list-trans-payment").slimScroll({
	  height: "220px",
	  alwaysVisible: false,
	  start: "bottom",
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

function InitCalInterestYesNo() {
  if ($("#cal-interest-yesno").length > 0) {
    $("input[name=cal-interest-yesno]:radio").click(function () {
      ResetFrmCalInterestYesNo();
    });
  }
}

function ResetFrmCalInterestYesNo() {
  var _typeInterest = $("#type-interest-hidden").val();
  var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
  var _statusPayment = $("#statuspayment-hidden").val();
  var _overpayment = $("#overpayment-hidden").val();
  var _formatPayment = $("#format-payment-hidden").val();

  if (_calInterestYesNo == "Y") {
    $("#cal-interest-" + _typeInterest).show();
    if (_typeInterest == "overpayment") $("#total-interest-overpayment").val("");
    if (_typeInterest == "payrepay") $("#total-interest-pay-repay").val("");
    $("#total-interest").val("");
    $("#total-payment").val("");
    $("#pay").val($("#total-payment").val());

    if (_statusPayment == "1") {
      if (parseInt(_overpayment) > 0) InitCalendarFromTo("#overpayment-date-end", false, "#payment-date", false);
    }

    if (_statusPayment == "2" || (_formatPayment == "2" && parseInt(_overpayment) <= 0))
      InitCalendarFromTo("#pay-repay-date-end", false, "#payment-date", false);
  }

  if (_calInterestYesNo == "N") {
    $("#cal-interest-" + _typeInterest).hide();
    $("#total-interest").val("0.00");
    $("#total-payment").val("");
    $("#pay").val($("#total-payment").val());
    $("#payment-date").val($("#payment-date-hidden").val());
    InitCalendar("#payment-date");

    CalculateTotalPayment();
  }
}

function InitPayChannel() {
  $("input[name=pay-channel]:radio").click(function () {
    var _payChannel = $("input[name=pay-channel]:checked").val();
    var _payChannelIndex = $("input[name=pay-channel]:checked").index("input[name=pay-channel]");

    $("#pay-channel-index-hidden").val(_payChannelIndex);
    if (_payChannel != "1") LoadForm(1, "adddetailpaychannel", true, "", _payChannel, "");
  });
}

function ResetFrmAddCPTransPayment() {
  GoToElement("top-page");

  var _statusPayment = $("#statuspayment-hidden").val();
  var _overpayment = $("#overpayment-hidden").val();
  var _formatPayment = $("#format-payment-hidden").val();

  CaptionFormatPayment();

  if ($("#cal-interest-yesno").length > 0) {
    $("input[name=cal-interest-yesno]:radio")[0].checked = true;
    ResetFrmCalInterestYesNo();
  }

  if (_statusPayment == "1") {
    if (parseInt(_overpayment) > 0) {
      $("#overpayment-date-start").val($("#overpayment-date-start-hidden").val());
      $("#overpayment-date-end").val($("#overpayment-date-end-hidden").val());
      InitCalendarFromTo("#overpayment-date-start", false, "#overpayment-date-end", false);
      $("#overpayment-year").val($("#overpayment-year-hidden").val());
      TextboxDisable("#overpayment-year");
      $("#overpayment-day").val($("#overpayment-day-hidden").val());
      TextboxDisable("#overpayment-day");
      $("#overpayment-interest").val($("#overpayment-interest-hidden").val());
      $("#total-interest-overpayment").val("");
      TextboxDisable("#total-interest-overpayment");
      $("#payment-date").val($("#payment-date-hidden").val());
      InitCalendarFromTo("#overpayment-date-end", false, "#payment-date", false);
    }
    else {
      if (_formatPayment == "1") {
        $("#payment-date").val($("#payment-date-hidden").val());
        InitCalendar("#payment-date");
      }
    }
  }

  if (_statusPayment == "2" || (_formatPayment == "2" && parseInt(_overpayment) <= 0)) {
    $("#pay-repay-date-start").val($("#pay-repay-date-start-hidden").val());
    $("#pay-repay-date-end").val($("#pay-repay-date-end-hidden").val());
    InitCalendarFromTo("#pay-repay-date-start", false, "#pay-repay-date-end", false);
    if (_statusPayment == "2") CalendarDisable("#pay-repay-date-start");
    $("#pay-repay-year").val($("#pay-repay-year-hidden").val());
    TextboxDisable("#pay-repay-year");
    $("#pay-repay-day").val($("#pay-repay-day-hidden").val());
    TextboxDisable("#pay-repay-day");
    $("#pay-repay-interest").val($("#pay-repay-interest-hidden").val());
    $("#total-interest-pay-repay").val("");
    TextboxDisable("#total-interest-pay-repay");
    $("#payment-date").val($("#payment-date-hidden").val());
    InitCalendarFromTo("#pay-repay-date-end", false, "#payment-date", false);
  }

  $("#capital").val($("#capital-hidden").val());
  TextboxDisable("#capital");
  $("#total-interest").val($("#total-interest-hidden").val());
  TextboxDisable("#total-interest");
  $("#total-accrued-interest").val($("#total-accrued-interest-hidden").val());
  TextboxDisable("#total-accrued-interest");    
  $("#total-payment").val($("#total-payment-hidden").val());
  TextboxDisable("#total-payment");
  $("#pay").val($("#total-payment").val());
  $("input[name=pay-channel]:radio").attr("checked", false);    
  $("#cheque-no-hidden").val("");
  $("#cheque-bank-hidden").val("");
  $("#cheque-bank-branch-hidden").val("");
  $("#cheque-date-hidden").val("");
  $("#cash-bank-hidden").val("ไทยพาณิชย์ จำกัด ( มหาชน )");
  $("#cash-bank-branch-hidden").val("ศิริราช");
  $("#cash-bank-account-hidden").val("มหาวิทยาลัยมหิดล");
  $("#cash-bank-account-no-hidden").val("0163003256");
  $("#cash-bank-date-hidden").val("");
  $("#receipt-no").val("");
  $("#receipt-book-no").val("");
  $("#receipt-date").val("");
  InitCalendar("#receipt-date");
  $("#receipt-send-no").val("");
  $("#receipt-fund").val("");

  $(".calendar").change(function () {
    if ($(this).attr("id") == "overpayment-date-end") $("#payment-date").val($("#overpayment-date-end").val());
    if ($(this).attr("id") == "pay-repay-date-end") $("#payment-date").val($("#pay-repay-date-end").val());
  });        
}

function ResetFrmAddDetailPayChannel() {
  var _payChannelIndex = $("#pay-channel-index-hidden").val();

  $("input[name=pay-channel]:radio")[parseInt(_payChannelIndex)].checked = true
  /*if ($("#receipt-no").length > 0) $("#receipt-no").val($("#receipt-no-hidden").val());
  if ($("#receipt-book-no").length > 0) $("#receipt-book-no").val($("#receipt-book-no-hidden").val());
  if ($("#receipt-date").length > 0) { InitCalendar("#receipt-date"); $("#receipt-date").val($("#receipt-date-hidden").val()); }*/
  if ($("#cheque-no").length > 0) $("#cheque-no").val($("#cheque-no-hidden").val());
  if ($("#cheque-bank").length > 0) $("#cheque-bank").val($("#cheque-bank-hidden").val());
  if ($("#cheque-bank-branch").length > 0) $("#cheque-bank-branch").val($("#cheque-bank-branch-hidden").val());
  if ($("#cheque-date").length > 0) { InitCalendar("#cheque-date"); $("#cheque-date").val($("#cheque-date-hidden").val()); }
  if ($("#cash-bank").length > 0) $("#cash-bank").val($("#cash-bank-hidden").val());
  if ($("#cash-bank-branch").length > 0) $("#cash-bank-branch").val($("#cash-bank-branch-hidden").val());
  if ($("#cash-bank-account").length > 0) $("#cash-bank-account").val($("#cash-bank-account-hidden").val());
  if ($("#cash-bank-account-no").length > 0) $("#cash-bank-account-no").val($("#cash-bank-account-no-hidden").val());
  if ($("#cash-bank-date").length > 0) { InitCalendar("#cash-bank-date"); $("#cash-bank-date").val($("#cash-bank-date-hidden").val()); }
}

function ValidateAddDetailPayChannel() {
  var _error = false;
  var _msg;
  var _focus;

  /*if (_error == false && $("#receipt-no").length > 0 && $("#receipt-no").val().length == 0) { _error = true; _msg = "กรุณาใส่เลขที่ใบเสร็จ"; _focus = "#receipt-no"; }
  if (_error == false && $("#receipt-book-no").length > 0 && $("#receipt-book-no").val().length == 0) { _error = true; _msg = "กรุณาใส่เล่มที่"; _focus = "#receipt-book-no"; }
  if (_error == false && $("#receipt-date").length > 0 && $("#receipt-date").val().length == 0) { _error = true; _msg = "กรุณาใส่วันที่บนใบเสร็จ"; _focus = "#receipt-date"; }*/
  if (_error == false && $("#cheque-no").length > 0 && $("#cheque-no").val().length == 0) { _error = true; _msg = "กรุณาใส่เลขที่เช็ค"; _focus = "#cheque-no"; }
  if (_error == false && $("#cheque-bank").length > 0 && $("#cheque-bank").val().length == 0) { _error = true; _msg = "กรุณาใส่ชื่อธนาคาร"; _focus = "#cheque-bank"; }
  if (_error == false && $("#cheque-bank-branch").length > 0 && $("#cheque-bank-branch").val().length == 0) { _error = true; _msg = "กรุณาใส่ชื่อสาขาของธนาคาร"; _focus = "#cheque-bank-branch"; }
  if (_error == false && $("#cheque-date").length > 0 && $("#cheque-date").val().length == 0) { _error = true; _msg = "กรุณาใส่วันที่บนเช็ค"; _focus = "#cheque-date"; }
  if (_error == false && $("#cash-bank").length > 0 && $("#cash-bank").val().length == 0) { _error = true; _msg = "กรุณาใส่ชื่อธนาคาร"; _focus = "#cash-bank"; }
  if (_error == false && $("#cash-bank-branch").length > 0 && $("#cash-bank-branch").val().length == 0) { _error = true; _msg = "กรุณาใส่ชื่อสาขาของธนาคาร"; _focus = "#cash-bank-branch"; }
  if (_error == false && $("#cash-bank-account").length > 0 && $("#cash-bank-account").val().length == 0) { _error = true; _msg = "กรุณาใส่ชื่อบัญชี"; _focus = "#cash-bank-account"; }
  if (_error == false && $("#cash-bank-account-no").length > 0 && $("#cash-bank-account-no").val().length == 0) { _error = true; _msg = "กรุณาใส่เลขที่บัญชี"; _focus = "#cash-bank-account-no"; }
  if (_error == false && $("#cash-bank-date").length > 0 && $("#cash-bank-date").val().length == 0) { _error = true; _msg = "กรุณาใส่วันที่บนใบนำฝาก"; _focus = "#cash-bank-date"; }

  if (_error == true) {
    DialogMessage(_msg, _focus, false, "");
    return;
  }

  /*if ($("#receipt-no").length > 0) $("#receipt-no-hidden").val($("#receipt-no").val());
  if ($("#receipt-book-no").length > 0) $("#receipt-book-no-hidden").val($("#receipt-book-no").val());
  if ($("#receipt-date").length > 0) $("#receipt-date-hidden").val($("#receipt-date").val());*/
  if ($("#cheque-no").length > 0) $("#cheque-no-hidden").val($("#cheque-no").val());
  if ($("#cheque-bank").length > 0) $("#cheque-bank-hidden").val($("#cheque-bank").val());
  if ($("#cheque-bank-branch").length > 0) $("#cheque-bank-branch-hidden").val($("#cheque-bank-branch").val());
  if ($("#cheque-date").length > 0) $("#cheque-date-hidden").val($("#cheque-date").val());
  if ($("#cash-bank").length > 0) $("#cash-bank-hidden").val($("#cash-bank").val());
  if ($("#cash-bank-branch").length > 0) $("#cash-bank-branch-hidden").val($("#cash-bank-branch").val());
  if ($("#cash-bank-account").length > 0) $("#cash-bank-account-hidden").val($("#cash-bank-account").val());
  if ($("#cash-bank-account-no").length > 0) $("#cash-bank-account-no-hidden").val($("#cash-bank-account-no").val());
  if ($("#cash-bank-date").length > 0) $("#cash-bank-date-hidden").val($("#cash-bank-date").val());

  $("#dialog-form1").dialog("close");
}

function ChkSelectFormatPayment(_cp2id, _statusPayment, _formatPayment) {
  if (_statusPayment == "1" && _formatPayment == "0") LoadForm(1, "selectformatpayment", true, "", _cp2id, "trans-payment" + _cp2id);
  if (_statusPayment == "2" && _formatPayment == "2") OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", _cp2id, "");
  if (_statusPayment == "3" && (_formatPayment == "1" || _formatPayment == "2")) OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", _cp2id, ""); ;
}

function ValidateSelectFormatPayment() {
  var _error = false;
  var _msg;
  var _focus;
  var _formatPayment = $("input[name=format-payment]:checked");

  if (_error == false && _formatPayment.length == 0) { _error = true; _msg = "กรุณาเลือกรูปแบบที่ต้องการชำระหนี้"; _focus = "#format-payment-full-repay-input"; }

  if (_error == true) {
    DialogMessage(_msg, _focus, false, "");
    return;
  }

  $("#dialog-form1").dialog("close");
  OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", $("#cp2id-hidden").val(), "");    
}

function ValidateCPTransPaymentFullRepay() {
  var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
  var _result = (_calInterestYesNo == "Y" ? ValidateCalInterestOverpayment() : true);

  if (_result == true) {   
    var _error = false;
    var _msg;
    var _focus;
    var _interestDateEnd = $("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "";
    var _paymentDate = $("#payment-date").val();
    var _dayDiff = 0;
    var _totalPayment = DelCommas("total-payment");
    var _pay = DelCommas("pay");
    var _payChannel = $("input[name=pay-channel]:checked");
    var _chequeNo = $("#cheque-no-hidden").val();
    var _chequeBank = $("#cheque-bank-hidden").val();
    var _chequeBankBranch = $("#cheque-bank-branch-hidden").val();
    var _chequeDate = $("#cheque-date-hidden").val();
    var _cashBank = $("#cash-bank-hidden").val();
    var _cashBankBranch = $("#cash-bank-branch-hidden").val();
    var _cashBankAccount = $("#cash-bank-account-hidden").val();
    var _cashBankAccountNo = $("#cash-bank-account-no-hidden").val();
    var _cashBankDate = $("#cash-bank-date-hidden").val();
    var _receiptNo = $("#receipt-no").val();
    var _receiptBookNo = $("#receipt-book-no").val();
    var _receiptDate = $("#receipt-date").val();
    var _receiptSendNo = $("#receipt-send-no").val();
    var _receiptFund = $("#receipt-fund").val();        

    if (_interestDateEnd.length > 0) {
      _interestDateEnd = GetDateObject(_interestDateEnd);
      _paymentDate = GetDateObject(_paymentDate);
      _dayDiff = DateDiff(_interestDateEnd, _paymentDate, "days");
    }

    if (_error == false && (_pay.length == 0 || _pay == "0.00")) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ"; _focus = "#pay"; }
    if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) > 0)) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระให้เท่ากับยอดเงินที่ต้องชำระ"; _focus = "#pay"; }
    if (_error == false && _payChannel.length == 0) { _error = true; _msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้"; _focus = "#pay-channel1-input"; }
    //if (_error == false && _payChannel.val() == 1 && (_receiptNo.length == 0 || _receiptBookNo.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _payChannel.val() == 2 && (_chequeNo.length == 0 || _chequeBank.length == 0 || _chequeBankBranch.length == 0 || _chequeDate.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _payChannel.val() == 3 && (_cashBank.length == 0 || _cashBankBranch.length == 0 || _cashBankAccount.length == 0 || _cashBankAccountNo.length == 0 || _cashBankDate.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _dayDiff < 0) { _error = true; _msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>"; _focus = "#payment-date"; }
    if (_error == false && _receiptNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ"; _focus = "#receipt-no"; }
    if (_error == false && _receiptBookNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่"; _focus = "#receipt-book-no"; }
    if (_error == false && _receiptDate.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่"; _focus = "#receipt-date"; }
    if (_error == false && _receiptSendNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง"; _focus = "#receipt-send-no"; }
    if (_error == false && _receiptFund.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เข้ากองทุน"; _focus = "#receipt-fund"; }

    if (_error == true) {
      DialogMessage(_msg, _focus, false, "");
      return false;
    }

    return true;
  }

  return false;
}

function ConfirmActionCPTransPaymentFullRepay() {
  DialogConfirm("ต้องการบันทึกข้อมูลนี้หรือไม่");
  $("#dialog-confirm").dialog({
    buttons: {
      "ตกลง": function () {
        $(this).dialog("close");

        if (ValidateCPTransPaymentFullRepay() == true) {
          var _i = 2;
          var _overpayment = $("#overpayment-hidden").val();
          var _payChannel = $("input[name=pay-channel]:checked");

          var _send = new Array();
          _send[0] = "cp2id=" + $("#cp2id").val();
          _send[1] = "overpayment=" + _overpayment;

          if (parseInt(_overpayment) > 0) {
            var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
            _i = 3;

            _send[2] = "calinterestyesno=" + _calInterestYesNo;

            if (_calInterestYesNo == "Y") {
              _send[_i] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
              _send[_i + 1] = "overpaymentdateend=" + $("#overpayment-date-end").val();
              _send[_i + 2] = "overpaymentyear=" + DelCommas("overpayment-year");
              _send[_i + 3] = "overpaymentday=" + DelCommas("overpayment-day");
              _send[_i + 4] = "overpaymentinterest=" + DelCommas("overpayment-interest");
              _send[_i + 5] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");
                            
              _i = _i + 6;
            }
          }

          _send[_i] = "datetimepayment=" + $("#payment-date").val();
          _send[_i + 1] = "capital=" + DelCommas("capital");
          _send[_i + 2] = "totalinterest=" + DelCommas("total-interest");
          _send[_i + 3] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");                    
          _send[_i + 4] = "totalpayment=" + DelCommas("total-payment");
          _send[_i + 5] = "pay=" + DelCommas("pay");
          _send[_i + 6] = "channel=" + _payChannel.val();
          _send[_i + 7] = "receiptno=" + $("#receipt-no").val();
          _send[_i + 8] = "receiptbookno=" + $("#receipt-book-no").val();
          _send[_i + 9] = "receiptdate=" + $("#receipt-date").val();
          _send[_i + 10] = "receiptsendno=" + $("#receipt-send-no").val();
          _send[_i + 11] = "receiptfund=" + $("#receipt-fund").val();

          /*
          if (_payChannel.val() == 1) {                        
            _send[_i + 7] = "receiptno=" + $("#receipt-no-hidden").val();
            _send[_i + 8] = "receiptbookno=" + $("#receipt-book-no-hidden").val();
            _send[_i + 9] = "receiptdate=" + $("#receipt-date-hidden").val();
          }
          */

          if (_payChannel.val() == 2) {
            _send[_i + 12] = "chequeno=" + $("#cheque-no-hidden").val();
            _send[_i + 13] = "chequebank=" + $("#cheque-bank-hidden").val();
            _send[_i + 14] = "chequebankbranch=" + $("#cheque-bank-branch-hidden").val();
            _send[_i + 15] = "chequedate=" + $("#cheque-date-hidden").val();
          }

          if (_payChannel.val() == 3) {
            _send[_i + 12] = "cashbank=" + $("#cash-bank-hidden").val();
            _send[_i + 13] = "cashbankbranch=" + $("#cash-bank-branch-hidden").val();
            _send[_i + 14] = "cashbankaccount=" + $("#cash-bank-account-hidden").val();
            _send[_i + 15] = "cashbankaccountno=" + $("#cash-bank-account-no-hidden").val();
            _send[_i + 16] = "cashbankdate=" + $("#cash-bank-date-hidden").val();
          }
                    
          AddCPTransPayment(_send, "fullrepay");
        }
      },
      "ยกเลิก": function () {
        $(this).dialog("close");
      }
    }
  });
}

function ValidateCPTransPaymentPayRepay() {
  var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
  var _statusPayment = $("#statuspayment-hidden").val();
  var _overpayment = $("#overpayment-hidden").val();
  var _interestDateEnd;
  var _paymentDate = $("#payment-date").val();
  var _dayDiff = 0;
  var _result = true;

  if (_calInterestYesNo == "Y") {
    if (_statusPayment == "1" && parseInt(_overpayment) > 0) {
      _interestDateEnd = $("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "";
      _result = ValidateCalInterestOverpayment();
    }

    if (_statusPayment == "2" || parseInt(_overpayment) <= 0) {
      _interestDateEnd = $("#pay-repay-date-end").length > 0 ? $("#pay-repay-date-end").val() : "";        
      _result = ValidateCalInterestPayRepay();
    }
  }

  if (_result == true) {
    var _error = false;
    var _msg;
    var _focus;
    var _totalPayment = DelCommas("total-payment");
    var _pay = DelCommas("pay");
    var _payRepayLeast = DelCommas("pay-repay-least-hidden");
    var _payChannel = $("input[name=pay-channel]:checked");
    var _chequeNo = $("#cheque-no-hidden").val();
    var _chequeBank = $("#cheque-bank-hidden").val();
    var _chequeBankBranch = $("#cheque-bank-branch-hidden").val();
    var _chequeDate = $("#cheque-date-hidden").val();
    var _cashBank = $("#cash-bank-hidden").val();
    var _cashBankBranch = $("#cash-bank-branch-hidden").val();
    var _cashBankAccount = $("#cash-bank-account-hidden").val();
    var _cashBankAccountNo = $("#cash-bank-account-no-hidden").val();
    var _cashBankDate = $("#cash-bank-date-hidden").val();
    var _receiptNo = $("#receipt-no").val();
    var _receiptBookNo = $("#receipt-book-no").val();
    var _receiptDate = $("#receipt-date").val();
    var _receiptSendNo = $("#receipt-send-no").val();
    var _receiptFund = $("#receipt-fund").val();        

    if (_calInterestYesNo == "Y" && _interestDateEnd.length > 0) {
      _interestDateEnd = GetDateObject(_interestDateEnd);
      _paymentDate = GetDateObject(_paymentDate);
      _dayDiff = DateDiff(_interestDateEnd, _paymentDate, "days");
    }

    if (_error == false && (_pay.length == 0 || _pay == "0.00")) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ"; _focus = "#pay"; }
    //if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) < 0)) { _error = true; _msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>"; _focus = "#pay"; }
    if (_error == false && ((parseFloat(_totalPayment) >= parseFloat(_payRepayLeast)) && (parseFloat(_pay) < parseFloat(_payRepayLeast)))) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท"; _focus = "#pay"; }
    if (_error == false && _payChannel.length == 0) { _error = true; _msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้"; _focus = "#pay-channel1-input"; }
    //if (_error == false && _payChannel.val() == 1 && (_receiptNo.length == 0 || _receiptBookNo.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _payChannel.val() == 2 && (_chequeNo.length == 0 || _chequeBank.length == 0 || _chequeBankBranch.length == 0 || _chequeDate.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _payChannel.val() == 3 && (_cashBank.length == 0 || _cashBankBranch.length == 0 || _cashBankAccount.length == 0 || _cashBankAccountNo.length == 0 || _cashBankDate.length == 0)) { _error = true; _msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้"; _focus = ""; ; }
    if (_error == false && _dayDiff < 0) { _error = true; _msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>"; _focus = "#payment-date"; }
    if (_error == false && _receiptNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ"; _focus = "#receipt-no"; }
    if (_error == false && _receiptBookNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่"; _focus = "#receipt-book-no"; }
    if (_error == false && _receiptDate.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่"; _focus = "#receipt-date"; }
    if (_error == false && _receiptSendNo.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง"; _focus = "#receipt-send-no"; }
    if (_error == false && _receiptFund.length == 0) { _error = true; _msg = "กรุณาใส่รายละเอียดใบเสร็จ - เข้ากองทุน"; _focus = "#receipt-fund"; }

    if (_error == true) {
      DialogMessage(_msg, _focus, false, "");
      return false;
    }

    return true;
  }

  return false;
}

function ConfirmActionCPTransPaymentPayRepay() {
  DialogConfirm("ต้องการบันทึกข้อมูลนี้หรือไม่");
  $("#dialog-confirm").dialog({
    buttons: {
      "ตกลง": function () {
        $(this).dialog("close");

        if (ValidateCPTransPaymentPayRepay() == true) {
          var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
          var _statusPayment = $("#statuspayment-hidden").val();
          var _i = 2;
          var _payChannel = $("input[name=pay-channel]:checked");

          var _send = new Array();
          _send[0] = "cp2id=" + $("#cp2id").val();
          _send[1] = "statuspayment=" + _statusPayment;

          if (_statusPayment == "1") {
            var _overpayment = $("#overpayment-hidden").val();                        
            _i = 3;

            _send[2] = "overpayment=" + _overpayment;

            if (parseInt(_overpayment) > 0) {                                                                                    
              _i = 4;
              _send[3] = "calinterestyesno=" + _calInterestYesNo;

              if (_calInterestYesNo == "Y") {
                _send[_i] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
                _send[_i + 1] = "overpaymentdateend=" + $("#overpayment-date-end").val();
                _send[_i + 2] = "overpaymentyear=" + DelCommas("overpayment-year");
                _send[_i + 3] = "overpaymentday=" + DelCommas("overpayment-day");
                _send[_i + 4] = "overpaymentinterest=" + DelCommas("overpayment-interest");
                _send[_i + 5] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");

                _i = _i + 6;
              }
            }
          }

          if (_statusPayment == "2" || parseInt(_overpayment) <= 0) {
            _send[_i] = "calinterestyesno=" + _calInterestYesNo;
            _i = _i + 1;

            if (_calInterestYesNo == "Y") {
              _send[_i] = "payrepaydatestart=" + $("#pay-repay-date-start").val();
              _send[_i + 1] = "payrepaydateend=" + $("#pay-repay-date-end").val();
              _send[_i + 2] = "payrepayyear=" + DelCommas("pay-repay-year");
              _send[_i + 3] = "payrepayday=" + DelCommas("pay-repay-day");
              _send[_i + 4] = "payrepayinterest=" + DelCommas("pay-repay-interest");
              _send[_i + 5] = "payrepaytotalinterest=" + DelCommas("total-interest-pay-repay");

              _i = _i + 6;
            }
          }

          _send[_i] = "datetimepayment=" + $("#payment-date").val();
          _send[_i + 1] = "capital=" + DelCommas("capital");
          _send[_i + 2] = "totalinterest=" + DelCommas("total-interest");
          _send[_i + 3] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");
          _send[_i + 4] = "totalpayment=" + DelCommas("total-payment");
          _send[_i + 5] = "pay=" + DelCommas("pay");
          _send[_i + 6] = "channel=" + _payChannel.val();
          _send[_i + 7] = "receiptno=" + $("#receipt-no").val();
          _send[_i + 8] = "receiptbookno=" + $("#receipt-book-no").val();
          _send[_i + 9] = "receiptdate=" + $("#receipt-date").val();
          _send[_i + 10] = "receiptsendno=" + $("#receipt-send-no").val();
          _send[_i + 11] = "receiptfund=" + $("#receipt-fund").val();

          /*
          if (_payChannel.val() == 1) {
            _send[_i + 7] = "receiptno=" + $("#receipt-no-hidden").val();
            _send[_i + 8] = "receiptbookno=" + $("#receipt-book-no-hidden").val();
            _send[_i + 9] = "receiptdate=" + $("#receipt-date-hidden").val();
          }
          */

          if (_payChannel.val() == 2) {
            _send[_i + 12] = "chequeno=" + $("#cheque-no-hidden").val();
            _send[_i + 13] = "chequebank=" + $("#cheque-bank-hidden").val();
            _send[_i + 14] = "chequebankbranch=" + $("#cheque-bank-branch-hidden").val();
            _send[_i + 15] = "chequedate=" + $("#cheque-date-hidden").val();
          }

          if (_payChannel.val() == 3) {
            _send[_i + 12] = "cashbank=" + $("#cash-bank-hidden").val();
            _send[_i + 13] = "cashbankbranch=" + $("#cash-bank-branch-hidden").val();
            _send[_i + 14] = "cashbankaccount=" + $("#cash-bank-account-hidden").val();
            _send[_i + 15] = "cashbankaccountno=" + $("#cash-bank-account-no-hidden").val();
            _send[_i + 16] = "cashbankdate=" + $("#cash-bank-date-hidden").val();
          }

          AddCPTransPayment(_send, "payrepay");
        }
      },
      "ยกเลิก": function () {
        $(this).dialog("close");
      }
    }

  });
}

function AddCPTransPayment(_send, _formatPayment) {
  AddUpdateData("add", "addcptranspayment" + _formatPayment, _send, false, "", "", "", false, function (_result) {
    if (_result == "1") {
      GotoSignin();
      return;
    }

    DialogConfirm("บันทึกข้อมูลเรียบร้อย");
    $("#dialog-confirm").dialog({
      buttons: {
        "ตกลง": function () {
          $(this).dialog("close");

          $("#dialog-form1").dialog("close");
          OpenSubTab("link-tab1-adddetail-cp-trans-payment", "#tab1-adddetail-cp-trans-payment", $("#cp2id").val());
        }
      }
    });
  });
}

function ShowDetailTransPayment(_cp2id, _period) {
  $("#period-hidden").val(_period);
    
  LoadForm(($("#dialog-form1").dialog("isOpen") == true ? 2 : 1), "detailtranspayment", true, "", _cp2id, "detail-trans-payment" + _cp2id);
}

function ResetDetailTransPayment() {
  GoToTopElement("#box-detail-trans-payment")
    
  $("#period").html($("#period-hidden").val());
  $("#button-style11").show();
  $("#button-style12").hide();
  InitCalendar("#pursuant-book-date, #input-date, #state-location-date, #contract-date");
  InitCalendarFromTo("#education-date-start", false, "#education-date-end", false);

  /*
  $("#channel-detail").slimScroll({
    height: "90px",
    alwaysVisible: true,
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

function ChkBalanceCPTransPaymentFullRepay() {
  var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
  var _result = (_calInterestYesNo == "Y" ? ValidateCalInterestOverpayment() : true);

  if (_result == true) {
    var _error = false;
    var _msg;
    var _focus;        
    var _totalPayment = DelCommas("total-payment");
    var _pay = DelCommas("pay");

    if (_error == false && (_pay.length == 0 || _pay == "0.00")) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ"; _focus = "#pay"; }
    if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) > 0)) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระให้เท่ากับยอดเงินที่ต้องชำระ"; _focus = "#pay"; }

    if (_error == true) {
      DialogMessage(_msg, _focus, false, "");
      return false;
    }

    LoadForm(1, "chkbalance", true, "", "", "");
  }
}

function ChkBalanceCPTransPaymentPayRepay() {
  var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
  var _statusPayment = $("#statuspayment-hidden").val();
  var _overpayment = $("#overpayment-hidden").val();
  var _result = true;

  if (_calInterestYesNo == "Y") {
    if (_statusPayment == "1" && parseInt(_overpayment) > 0) _result = ValidateCalInterestOverpayment();
    if (_statusPayment == "2" || parseInt(_overpayment) <= 0) _result = ValidateCalInterestPayRepay();
  }

  if (_result == true) {
    var _error = false;
    var _msg;
    var _focus;
    var _totalPayment = DelCommas("total-payment");
    var _pay = DelCommas("pay");
    var _payRepayLeast = DelCommas("pay-repay-least-hidden");

    if (_error == false && (_pay.length == 0 || _pay == "0.00")) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ"; _focus = "#pay"; }
    //if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) < 0)) { _error = true; _msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>"; _focus = "#pay"; }
    if (_error == false && ((parseFloat(_totalPayment) >= parseFloat(_payRepayLeast)) && (parseFloat(_pay) < parseFloat(_payRepayLeast)))) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท"; _focus = "#pay"; }

    if (_error == true) {
      DialogMessage(_msg, _focus, false, "");
      return false;
    }

    LoadForm(1, "chkbalance", true, "", "", "");
  }
}