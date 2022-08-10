function ResetFrmSelectFormatPayment() {
    var formatPayment = $("input[name=format-payment]:radio");

    formatPayment.attr("checked", false);
}

function CaptionFormatPayment() {    
    var send = new Array();
    send[send.length] = "formatpayment=" + $("#format-payment-hidden").val();

    SetMsgLoading("");

    ViewData("formatpayment", send, function (result) {
        $("#format-payment-input span").html(result);
    });
}

function ResetFormatPayment() {
    if (($("#format-payment-hidden").val().length == 0) || ($("#format-payment-hidden").val() == "0")) {
        var formatPayment = $("input[name=format-payment]:checked");
        var valCheck;
        
        formatPayment.each(function (i) {
            valCheck = this.value;
        });

        $("#format-payment-hidden").val(valCheck);
    }
}

function InitStatusPaymentRecord() {
    if ($("#status-payment-record").length > 0) {
        $("input[name=status-payment-record]:radio").click(function () {            
            var _statusPaymentRecord = $(this).val();
            var _send = new Array();
            _send[_send.length] = "cp2id=" + $("#cp2id").val();
            _send[_send.length] = "statuspaymentrecord=" + (_statusPaymentRecord == "C" ? "" : _statusPaymentRecord);
            _send[_send.length] = "statuspaymentrecordlawyerfullname=" + $("#statuspaymentrecord-lawyer-fullname-hidden").val();
            _send[_send.length] = "statuspaymentrecordlawyerphonenumber=" + $("#statuspaymentrecord-lawyer-phonenumber-hidden").val();
            _send[_send.length] = "statuspaymentrecordlawyermobilenumber=" + $("#statuspaymentrecord-lawyer-mobilenumber-hidden").val();
            _send[_send.length] = "statuspaymentrecordlawyeremail=" + $("#statuspaymentrecord-lawyer-email-hidden").val();

            ResetTabAddDetailTransPayment(_statusPaymentRecord);
            AddUpdateStatusPaymentRecord(_send);
        });

        ResetFrmStatusPaymentRecord();
    }
}

function ResetFrmStatusPaymentRecord() {
    var _statusPaymentRecord = ($("#statuspaymentrecord-hidden").val().length > 0 ? $("#statuspaymentrecord-hidden").val() : "C");
    
    if ($("input[name=status-payment-record]:radio").filter("[value=" + _statusPaymentRecord + "]").length > 0)
        $("input[name=status-payment-record][value=" + _statusPaymentRecord + "]").prop("checked", true);

    $("#status-payment-record-lawyer").html($("#statuspaymentrecord-lawyer-hidden").val());
    ResetTabAddDetailTransPayment(_statusPaymentRecord);
}

function ResetTabAddDetailTransPayment(_statusPaymentRecord) {
    if (_statusPaymentRecord == "P")
        $("#tab2-adddetail-cp-trans-payment").hide();
    else
        $("#tab2-adddetail-cp-trans-payment").show();
}

function AddUpdateStatusPaymentRecord(_send) {
    AddUpdateData("update", "updatestatuspaymentrecord", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }
        
        $("#statuspaymentrecord-hidden").val($("input[name=status-payment-record]:checked").val());
        $("#statuspaymentrecord-lawyer-hidden").val($("#statuspaymentrecord-lawyer-fullname-hidden").val());
        ResetFrmStatusPaymentRecord();
    });
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

        if (_typeInterest == "overpayment")
            $("#total-interest-overpayment").val("");

        if (_typeInterest == "payrepay")
            $("#total-interest-pay-repay").val("");

        $("#total-interest").val("");
        $("#total-payment").val("");
        $("#pay").val($("#total-payment").val());

        if (_statusPayment == "1") {
            if (parseInt(_overpayment) > 0)
                InitCalendarFromTo("#overpayment-date-end", false, "#payment-date", false);
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

        if (_payChannel != "1")
            LoadForm(1, "adddetailpaychannel", true, "", _payChannel, "");
    });
}

function InitReceiptCopy() {
    $("#receipt-copy-linkpreview a").click(function () {
        DownloadReceiptCopy(btoa($("#receipt-copy").val()));
    });

    $("#receipt-copy-file").change(function () {
        $("#receipt-copy").val("");
        $("#receipt-copy-preview, " +
          "#receipt-copy-nopreview, " +
          "#receipt-copy-linkpreview").addClass("hidden");

        if (this.files && this.files[0]) {
            var _fd = new FormData();
            var _files = this.files[0];

            if (_files.type == "application/pdf") {
                if (_files.size <= 4194304) {
                    _fd.append("action", "preview");
                    _fd.append("file", _files);

                    $.ajax({
                        beforeSend: function () {
                            $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                              "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                              "#receipt-copy-preview").addClass("hidden");
                            $("#receipt-copy-input .uploadfile-container .preloading-inline").removeClass("hidden");
                        },
                        async: true,
                        type: "POST",
                        url: "FileProcess.aspx",
                        data: _fd,
                        contentType: false,
                        processData: false,
                        success: function (_result) {
                            $("#receipt-copy").val("data:" + _files.type + ";base64," + _result);

                            try {
                                var _pdfData = atob(_result);
                                var _pdfjs = window["pdfjs-dist/build/pdf"];

                                _pdfjs.GlobalWorkerOptions.workerSrc = "https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.worker.min.js";

                                var _canvas = document.getElementById("receipt-copy-preview");
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
                                        
                                        $("#receipt-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                        $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                                          "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                                          "#receipt-copy-preview, " +
                                          "#receipt-copy-linkpreview").removeClass("hidden");

                                        DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                                    });
                                });
                            }
                            catch (_e) {
                                $("#receipt-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                                  "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                                  "#receipt-copy-nopreview, " +
                                  "#receipt-copy-linkpreview").removeClass("hidden");

                                DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                            };

                            /*
                            $("#receipt-copy-preview").attr("src", ("data:" + _files.type + ";base64, " + _result));
                            $("#receipt-copy-preview").load(function () {
                                var _imgH = 217;

                                $(this).css({
                                    "display": ($(this).height() > _imgH ? "block" : "table-cell"),
                                    "height": ($(this).height() > _imgH ? "inherit" : "auto"),
                                    "vertical-align": ($(this).height() > _imgH ? "top" : "middle"),
                                });
                            });
                            */
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

function doCalculatePayment() {
    var formatPayment = $("#format-payment-hidden").val();
    var capital = $("#capital").val();    
    var pay = $("#pay").val();

    capital = ((capital.length === 0 || capital === "0.00") ? 0 : DelCommas("capital"));
    pay = ((pay.length === 0 || pay === "0.00") ? 0 : DelCommas("pay"));

    if (formatPayment === "1") {
        var totalInterestOverpayment = $("#total-interest-overpayment").val();
        var overpay = $("#overpay").val();

        totalInterestOverpayment = ((totalInterestOverpayment.length === 0 || totalInterestOverpayment === "0.00") ? 0 : DelCommas("total-interest-overpayment"));
        overpay = ((overpay.length === 0 || overpay === "0.00") ? 0 : DelCommas("overpay"));

        $("#total-payment").val((parseFloat(totalInterestOverpayment) + parseFloat(pay) + parseFloat(overpay)).toFixed(2));
        Trim("total-payment");
        AddCommas("total-payment", 2);

        $("#remain-capital").val((parseFloat(capital) - parseFloat(pay)).toFixed(2));
        Trim("remain-capital");
        AddCommas("remain-capital", 2);
    }

    if (formatPayment === "2") {
        var totalAccruedInterest = $("#total-accrued-interest").val();
        var totalInterestOverpaymentBefore = $("#total-interest-overpayment-before").val();
        var totalInterestPayRepay = $("#total-interest-pay-repay").val();
        var totalInterestOverpayment = $("#total-interest-overpayment").val();        
        var payCapital = 0;
        var remainAccruedInterest = 0;

        totalAccruedInterest = ((totalAccruedInterest.length === 0 || totalAccruedInterest === "0.00") ? 0 : DelCommas("total-accrued-interest"));
        totalInterestOverpaymentBefore = ((totalInterestOverpaymentBefore.length === 0 || totalInterestOverpaymentBefore === "0.00") ? 0 : DelCommas("total-interest-overpayment-before"));
        totalInterestPayRepay = ((totalInterestPayRepay.length === 0 || totalInterestPayRepay === "0.00") ? 0 : DelCommas("total-interest-pay-repay"));        
        totalInterestOverpayment = ((totalInterestOverpayment.length === 0 || totalInterestOverpayment === "0.00") ? 0 : DelCommas("total-interest-overpayment"));        

        payCapital = (parseFloat(pay) - parseFloat(totalAccruedInterest) - parseFloat(totalInterestOverpaymentBefore) - parseFloat(totalInterestPayRepay) - parseFloat(totalInterestOverpayment));
        payCapital = (parseFloat(payCapital) < 0 ? 0 : payCapital);
        $("#pay-capital").val(parseFloat(payCapital).toFixed(2));
        Trim("pay-capital");
        AddCommas("pay-capital", 2);

        $("#remain-capital").val((parseFloat(capital) - parseFloat(payCapital)).toFixed(2));
        Trim("remain-capital");
        AddCommas("remain-capital", 2);
        /*        
        remainAccruedInterest = ((parseFloat(totalAccruedInterest) + parseFloat(totalInterestOverpaymentBefore) + parseFloat(totalInterestPayRepay) + parseFloat(totalInterestOverpayment)) - parseFloat(pay));
        remainAccruedInterest = (parseFloat(remainAccruedInterest) < 0 ? 0 : remainAccruedInterest);
        $("#remain-accrued-interest").val(parseFloat(remainAccruedInterest).toFixed(2));
        */
        Trim("remain-accrued-interest");
        AddCommas("remain-accrued-interest", 2);
    }
}

function ResetFrmAddCPTransPayment() {
    GoToTopElement("html, body");
    /*
    var statusPayment = $("#statuspayment-hidden").val();
    var overpayment = $("#overpayment-hidden").val();
    */
    var formatPayment = $("#format-payment-hidden").val();

    CaptionFormatPayment();

    $("#capital").val($("#capital-hidden").val());
    TextboxDisable("#capital");
    $("#total-payment").val($("#total-payment-hidden").val());
    $("#pay").val($("#total-payment-hidden").val());
    $("#remain-capital").val("");
    TextboxDisable("#remain-capital");
    $("#payment-date").val($("#payment-date-hidden").val());
    InitCalendar("#payment-date");

    if (formatPayment === "1") {
        $("#total-interest-overpayment").val($("#total-interest-overpayment-hidden").val());
        $("#overpay").val("");
        AddCommas("overpay", 2);
    }

    if (formatPayment === "2") {
        $("#total-accrued-interest").val($("#total-accrued-interest-hidden").val());
        TextboxDisable("#total-accrued-interest");
        $("#total-interest-overpayment-before").val($("#total-interest-overpayment-before-hidden").val());
        $("#total-interest-pay-repay").val($("#total-interest-pay-repay-hidden").val());
        $("#total-interest-overpayment").val($("#total-interest-overpayment-hidden").val());        
        $("#pay-capital").val("");
        TextboxDisable("#pay-capital");
        $("#remain-accrued-interest").val("");
        /*
        TextboxDisable("#remain-accrued-interest");
        */
    }

    doCalculatePayment();

    /*
    if ($("#cal-interest-yesno").length > 0) {
        $("input[name=cal-interest-yesno]:radio")[0].checked = true;
        ResetFrmCalInterestYesNo();
    }
    
    if (statusPayment == "1") {
        if (parseInt(overpayment) > 0) {
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
            if (formatPayment == "1") {
                $("#payment-date").val($("#payment-date-hidden").val());
                InitCalendar("#payment-date");
            }
        }
    }

    if (statusPayment == "2" || (formatPayment == "2" && parseInt(overpayment) <= 0)) {
        $("#pay-repay-date-start").val($("#pay-repay-date-start-hidden").val());
        $("#pay-repay-date-end").val($("#pay-repay-date-end-hidden").val());
        InitCalendarFromTo("#pay-repay-date-start", false, "#pay-repay-date-end", false);

        if (statusPayment == "2")
            CalendarDisable("#pay-repay-date-start");

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
    */
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
    $("#receipt-copy").val("");
    $("#receipt-copy-preview, #receipt-copy-nopreview, #receipt-copy-linkpreview").addClass("hidden");
    /*
    $(".calendar").change(function () {
        if ($(this).attr("id") == "overpayment-date-end")
            $("#payment-date").val($("#overpayment-date-end").val());

        if ($(this).attr("id") == "pay-repay-date-end")
            $("#payment-date").val($("#pay-repay-date-end").val());
    });
    */
}

function ResetFrmAddDetailPayChannel() {
    var _payChannelIndex = $("#pay-channel-index-hidden").val();

    $("input[name=pay-channel]:radio")[parseInt(_payChannelIndex)].checked = true
    /*
    if ($("#receipt-no").length > 0)
        $("#receipt-no").val($("#receipt-no-hidden").val());

    if ($("#receipt-book-no").length > 0)
        $("#receipt-book-no").val($("#receipt-book-no-hidden").val());

    if ($("#receipt-date").length > 0) {
        InitCalendar("#receipt-date");
        $("#receipt-date").val($("#receipt-date-hidden").val());
    }
    */
    if ($("#cheque-no").length > 0)
        $("#cheque-no").val($("#cheque-no-hidden").val());

    if ($("#cheque-bank").length > 0)
        $("#cheque-bank").val($("#cheque-bank-hidden").val());

    if ($("#cheque-bank-branch").length > 0)
        $("#cheque-bank-branch").val($("#cheque-bank-branch-hidden").val());

    if ($("#cheque-date").length > 0) {
        InitCalendar("#cheque-date");
        $("#cheque-date").val($("#cheque-date-hidden").val());
    }

    if ($("#cash-bank").length > 0)
        $("#cash-bank").val($("#cash-bank-hidden").val());

    if ($("#cash-bank-branch").length > 0)
        $("#cash-bank-branch").val($("#cash-bank-branch-hidden").val());

    if ($("#cash-bank-account").length > 0)
        $("#cash-bank-account").val($("#cash-bank-account-hidden").val());

    if ($("#cash-bank-account-no").length > 0)
        $("#cash-bank-account-no").val($("#cash-bank-account-no-hidden").val());

    if ($("#cash-bank-date").length > 0) {
        InitCalendar("#cash-bank-date");
        $("#cash-bank-date").val($("#cash-bank-date-hidden").val());
    }
}

function ValidateAddDetailPayChannel() {
    var _error = false;
    var _msg;
    var _focus;

    /*
    if (_error == false && $("#receipt-no").length > 0 && $("#receipt-no").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่เลขที่ใบเสร็จ";
        _focus = "#receipt-no";
    }

    if (_error == false && $("#receipt-book-no").length > 0 && $("#receipt-book-no").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่เล่มที่";
        _focus = "#receipt-book-no";
    }

    if (_error == false && $("#receipt-date").length > 0 && $("#receipt-date").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่วันที่บนใบเสร็จ";
        _focus = "#receipt-date";
    }
    */
    if (_error == false && $("#cheque-no").length > 0 && $("#cheque-no").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่เลขที่เช็ค";
        _focus = "#cheque-no";
    }

    if (_error == false && $("#cheque-bank").length > 0 && $("#cheque-bank").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่ชื่อธนาคาร";
        _focus = "#cheque-bank";
    }

    if (_error == false && $("#cheque-bank-branch").length > 0 && $("#cheque-bank-branch").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่ชื่อสาขาของธนาคาร";
        _focus = "#cheque-bank-branch";
    }

    if (_error == false && $("#cheque-date").length > 0 && $("#cheque-date").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่วันที่บนเช็ค";
        _focus = "#cheque-date";
    }

    if (_error == false && $("#cash-bank").length > 0 && $("#cash-bank").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่ชื่อธนาคาร";
        _focus = "#cash-bank";
    }

    if (_error == false && $("#cash-bank-branch").length > 0 && $("#cash-bank-branch").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่ชื่อสาขาของธนาคาร";
        _focus = "#cash-bank-branch";
    }

    if (_error == false && $("#cash-bank-account").length > 0 && $("#cash-bank-account").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่ชื่อบัญชี";
        _focus = "#cash-bank-account";
    }

    if (_error == false && $("#cash-bank-account-no").length > 0 && $("#cash-bank-account-no").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่เลขที่บัญชี";
        _focus = "#cash-bank-account-no";
    }

    if (_error == false && $("#cash-bank-date").length > 0 && $("#cash-bank-date").val().length == 0) {
        _error = true;
        _msg = "กรุณาใส่วันที่บนใบนำฝาก";
        _focus = "#cash-bank-date";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    /*
    if ($("#receipt-no").length > 0)
        $("#receipt-no-hidden").val($("#receipt-no").val());

    if ($("#receipt-book-no").length > 0)
        $("#receipt-book-no-hidden").val($("#receipt-book-no").val());

    if ($("#receipt-date").length > 0)
        $("#receipt-date-hidden").val($("#receipt-date").val());
    */
    if ($("#cheque-no").length > 0)
        $("#cheque-no-hidden").val($("#cheque-no").val());

    if ($("#cheque-bank").length > 0)
        $("#cheque-bank-hidden").val($("#cheque-bank").val());

    if ($("#cheque-bank-branch").length > 0)
        $("#cheque-bank-branch-hidden").val($("#cheque-bank-branch").val());

    if ($("#cheque-date").length > 0)
        $("#cheque-date-hidden").val($("#cheque-date").val());

    if ($("#cash-bank").length > 0)
        $("#cash-bank-hidden").val($("#cash-bank").val());

    if ($("#cash-bank-branch").length > 0)
        $("#cash-bank-branch-hidden").val($("#cash-bank-branch").val());

    if ($("#cash-bank-account").length > 0)
        $("#cash-bank-account-hidden").val($("#cash-bank-account").val());

    if ($("#cash-bank-account-no").length > 0)
        $("#cash-bank-account-no-hidden").val($("#cash-bank-account-no").val());

    if ($("#cash-bank-date").length > 0)
        $("#cash-bank-date-hidden").val($("#cash-bank-date").val());

    $("#dialog-form1").dialog("close");
}

function ChkSelectFormatPayment(_cp2id, _statusPayment, _formatPayment) {
    if (_statusPayment == "1" && _formatPayment == "0")
        LoadForm(1, "selectformatpayment", true, "", _cp2id, "trans-payment" + _cp2id);

    if (_statusPayment == "2" && _formatPayment == "2")
        OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", _cp2id, "");

    if (_statusPayment == "3" && (_formatPayment == "1" || _formatPayment == "2"))
        OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", _cp2id, "");;
}

function ValidateSelectFormatPayment() {
    var _error = false;
    var _msg;
    var _focus;
    var _formatPayment = $("input[name=format-payment]:checked");

    if (_error == false && _formatPayment.length == 0) {
        _error = true;
        _msg = "กรุณาเลือกรูปแบบที่ต้องการชำระหนี้";
        _focus = "#format-payment-full-repay-input";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    $("#dialog-form1").dialog("close");
    OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", $("#cp2id-hidden").val(), "");    
}

function ValidateCPTransPaymentFullRepay() {
    /*
    var _calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var _result = (_calInterestYesNo == "Y" ? ValidateCalInterestOverpayment() : true);
    */
    var result = true;

    if (result == true) {   
        var error = false;
        var msg;
        var focus;
        /*
        var interestDateEnd = $("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "";
        var paymentDate = $("#payment-date").val();
        var dayDiff = 0;
        */
        var totalPayment = DelCommas("total-payment");
        var totalInterestOverpayment = DelCommas("total-interest-overpayment");
        var pay = DelCommas("pay");
        var overpay = DelCommas("overpay");
        var remainCapital = DelCommas("remain-capital");
        var payChannel = $("input[name=pay-channel]:checked");
        var chequeNo = $("#cheque-no-hidden").val();
        var chequeBank = $("#cheque-bank-hidden").val();
        var chequeBankBranch = $("#cheque-bank-branch-hidden").val();
        var chequeDate = $("#cheque-date-hidden").val();
        var cashBank = $("#cash-bank-hidden").val();
        var cashBankBranch = $("#cash-bank-branch-hidden").val();
        var cashBankAccount = $("#cash-bank-account-hidden").val();
        var cashBankAccountNo = $("#cash-bank-account-no-hidden").val();
        var cashBankDate = $("#cash-bank-date-hidden").val();
        var receiptNo = $("#receipt-no").val();
        var receiptBookNo = $("#receipt-book-no").val();
        var receiptDate = $("#receipt-date").val();
        var receiptSendNo = $("#receipt-send-no").val();
        var receiptFund = $("#receipt-fund").val();        
        /*
        if (interestDateEnd.length > 0) {
            interestDateEnd = GetDateObject(interestDateEnd);
            paymentDate = GetDateObject(paymentDate);
            dayDiff = DateDiff(interestDateEnd, _paymentDate, "days");
        }
        */

        if (error === false && (totalPayment.length === 0 || parseFloat(totalPayment) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระ";
            focus = "#total-payment";
        }

        if (error === false && (pay.length === 0 || parseFloat(pay) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้น";
            focus = "#pay";
        }

        if (error === false && (parseFloat(totalPayment) !== parseFloat((parseFloat(totalInterestOverpayment) + parseFloat(pay) + parseFloat(overpay)).toFixed(2)))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระให้ถูกต้อง";
            focus = "#total-payment";
        }
        /*
        if (error === false && (parseFloat(pay) !== parseFloat(totalPayment))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้นให้เท่ากับจำนวนเงินที่ชำระ";
            focus = "#pay";
        }
        */
        if (error === false && (remainCapital.length === 0 || parseFloat(remainCapital) !== 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้นให้เท่ากับ<br />จำนวนเงินต้นคงเหลือยกมา";
            focus = "#pay";
        }

        if (error == false && payChannel.length == 0) {
            error = true;
            msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้";
            focus = "#pay-channel1-input";
        }
        /*
        if (error == false && payChannel.val() == 1 && (receiptNo.length == 0 || receiptBookNo.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        */
        if (error == false && payChannel.val() == 2 && (chequeNo.length == 0 || chequeBank.length == 0 || chequeBankBranch.length == 0 || chequeDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";;
        }

        if (error == false && payChannel.val() == 3 && (cashBank.length == 0 || cashBankBranch.length == 0 || cashBankAccount.length == 0 || cashBankAccountNo.length == 0 || cashBankDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";;
        }
        /*
        if (error == false && dayDiff < 0) {
            error = true;
            msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>";
            focus = "#payment-date";
        }
        */
        if (error == false && receiptNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ";
            focus = "#receipt-no";
        }

        if (error == false && receiptBookNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่";
            focus = "#receipt-book-no";
        }

        if (error == false && receiptDate.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่";
            focus = "#receipt-date";
        }

        if (error == false && receiptSendNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง";
            focus = "#receipt-send-no";
        }

        if (error == false && receiptFund.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เข้ากองทุน";
            focus = "#receipt-fund";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
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
                    /*
                    var overpayment = $("#overpayment-hidden").val();
                    */
                    var payChannel = $("input[name=pay-channel]:checked");
                    var receiptCopy = $("#receipt-copy").val();

                    if (receiptCopy.length > 0)
                        receiptCopy = btoa(receiptCopy);

                    var send = new Array();
                    send[send.length] = "cp2id=" + $("#cp2id").val();
                    /*
                    send[send.length] = "overpayment=" + overpayment;
                    
                    if (parseInt(overpayment) > 0) {
                        var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();

                        send[send.length] = "calinterestyesno=" + calInterestYesNo;

                        if (calInterestYesNo == "Y") {
                            send[_send.length] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
                            send[_send.length] = "overpaymentdateend=" + $("#overpayment-date-end").val();
                            send[_send.length] = "overpaymentyear=" + DelCommas("overpayment-year");
                            send[_send.length] = "overpaymentday=" + DelCommas("overpayment-day");
                            send[_send.length] = "overpaymentinterest=" + DelCommas("overpayment-interest");
                            send[_send.length] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");
                        }
                    }
                    */
                                        
                    send[send.length] = "capital=" + DelCommas("capital");
                    /*
                    send[send.length] = "totalaccruedinterest=" + (totalAccruedInterest.length > 0 ? totalAccruedInterest : "0.00");
                    */
                    send[send.length] = "totalpayment=" + DelCommas("total-payment");
                    send[send.length] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");
                    send[send.length] = "pay=" + DelCommas("pay");
                    send[send.length] = "overpay=" + DelCommas("overpay");
                    send[send.length] = "datetimepayment=" + $("#payment-date").val();
                    send[send.length] = "channel=" + payChannel.val();
                    send[send.length] = "receiptno=" + $("#receipt-no").val();
                    send[send.length] = "receiptbookno=" + $("#receipt-book-no").val();
                    send[send.length] = "receiptdate=" + $("#receipt-date").val();
                    send[send.length] = "receiptsendno=" + $("#receipt-send-no").val();
                    send[send.length] = "receiptfund=" + $("#receipt-fund").val();
                    send[send.length] = "receiptcopy=" + receiptCopy;
                    
                    /*
                    if (payChannel.val() == 1) {
                        send[send.length] = "receiptno=" + $("#receipt-no-hidden").val();
                        send[send.length] = "receiptbookno=" + $("#receipt-book-no-hidden").val();
                        send[send.length] = "receiptdate=" + $("#receipt-date-hidden").val();
                    }
                    */
                    
                    if (payChannel.val() == 2) {
                        send[send.length] = "chequeno=" + $("#cheque-no-hidden").val();
                        send[send.length] = "chequebank=" + $("#cheque-bank-hidden").val();
                        send[send.length] = "chequebankbranch=" + $("#cheque-bank-branch-hidden").val();
                        send[send.length] = "chequedate=" + $("#cheque-date-hidden").val();
                    }

                    if (payChannel.val() == 3) {
                        send[send.length] = "cashbank=" + $("#cash-bank-hidden").val();
                        send[send.length] = "cashbankbranch=" + $("#cash-bank-branch-hidden").val();
                        send[send.length] = "cashbankaccount=" + $("#cash-bank-account-hidden").val();
                        send[send.length] = "cashbankaccountno=" + $("#cash-bank-account-no-hidden").val();
                        send[send.length] = "cashbankdate=" + $("#cash-bank-date-hidden").val();
                    }

                    send[send.length] = "lawyerfullname=" + $("#lawyer-fullname-hidden").val();
                    send[send.length] = "lawyerphonenumber=" + $("#lawyer-phonenumber-hidden").val();
                    send[send.length] = "lawyermobilenumber=" + $("#lawyer-mobilenumber-hidden").val();
                    send[send.length] = "lawyeremail=" + $("#lawyer-email-hidden").val();

                    AddCPTransPayment(send, "fullrepay");
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTransPaymentPayRepay() {
    /*
    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var statusPayment = $("#statuspayment-hidden").val();
    var overpayment = $("#overpayment-hidden").val();
    var interestDateEnd;
    var paymentDate = $("#payment-date").val();
    var dayDiff = 0;
    */
    var result = true;
    /*
    if (calInterestYesNo == "Y") {
        if (statusPayment == "1" && parseInt(overpayment) > 0) {
            interestDateEnd = $("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "";
            result = ValidateCalInterestOverpayment();
        }

        if (statusPayment == "2" || parseInt(overpayment) <= 0) {
            interestDateEnd = $("#pay-repay-date-end").length > 0 ? $("#pay-repay-date-end").val() : "";        
            result = ValidateCalInterestPayRepay();
        }
    }
    */
    if (result == true) {
        var error = false;
        var msg;
        var focus;
        var capital = DelCommas("capital");
        var pay = DelCommas("pay");
        var totalAccruedInterest = DelCommas("total-accrued-interest");
        var totalInterestOverpaymentBefore = DelCommas("total-interest-overpayment-before");
        var totalInterestPayRepay = DelCommas("total-interest-pay-repay");
        var totalInterestOverpayment = DelCommas("total-interest-overpayment");
        var totalInterrest = parseFloat((parseFloat(totalAccruedInterest) + parseFloat(totalInterestOverpaymentBefore) + parseFloat(totalInterestPayRepay) + parseFloat(totalInterestOverpayment)).toFixed(2));
        var payCapital = DelCommas("pay-capital");
        var remainCapital = DelCommas("remain-capital");
        var payRepayLeast = DelCommas("pay-repay-least-hidden");
        var payChannel = $("input[name=pay-channel]:checked");
        var chequeNo = $("#cheque-no-hidden").val();
        var chequeBank = $("#cheque-bank-hidden").val();
        var chequeBankBranch = $("#cheque-bank-branch-hidden").val();
        var chequeDate = $("#cheque-date-hidden").val();
        var cashBank = $("#cash-bank-hidden").val();
        var cashBankBranch = $("#cash-bank-branch-hidden").val();
        var cashBankAccount = $("#cash-bank-account-hidden").val();
        var cashBankAccountNo = $("#cash-bank-account-no-hidden").val();
        var cashBankDate = $("#cash-bank-date-hidden").val();
        var receiptNo = $("#receipt-no").val();
        var receiptBookNo = $("#receipt-book-no").val();
        var receiptDate = $("#receipt-date").val();
        var receiptSendNo = $("#receipt-send-no").val();
        var receiptFund = $("#receipt-fund").val();        
        /*
        if (calInterestYesNo == "Y" && interestDateEnd.length > 0) {
            interestDateEnd = GetDateObject(_interestDateEnd);
            paymentDate = GetDateObject(_paymentDate);
            dayDiff = DateDiff(interestDateEnd, paymentDate, "days");
        }
        */
        if (error == false && (pay.length == 0 || parseFloat(pay) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้";
            focus = "#pay";
        }

        if (error === false && (payCapital.length > 0 && parseFloat(payCapital) > parseFloat(capital))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ให้ถูกต้อง<br />เนื่องจากจำนวนเงินต้นคงเหลือที่ยกมาต้องมากกว่าหรือเท่ากับ<br />จำนวนเงินที่หักชำระเงินต้นในงวดนี้";
            focus = "#pay";
        }

        if (error === false && (remainCapital.length > 0 && parseFloat(remainCapital) < 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ให้ถูกต้อง<br />เนื่องจากจำนวนเงินต้นคงเหลือน้อยกว่า 0";
            focus = "#pay";
        }
        /*
        if (error == false && ((parseFloat(totalPayment) - parseFloat(pay)) < 0)) {
            error = true;
            msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>";
            focus = "#pay";
        }
        */
        if (error == false && (parseFloat(pay) < parseFloat(payRepayLeast))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท";
            focus = "#pay";
        }

        if (error === false && (parseFloat(pay) < parseFloat(totalInterrest))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ไม่น้อยกว่าจำนวนเงินที่หักชำระดอกเบี้ย";
            focus = "#pay";
        }
        /*
        if (error == false && ((parseFloat(totalPayment) >= parseFloat(payRepayLeast)) && (parseFloat(pay) < parseFloat(payRepayLeast)))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท";
            focus = "#pay";
        }
        */
        if (error == false && payChannel.length == 0) {
            error = true;
            msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้";
            focus = "#pay-channel1-input";
        }
        /*
        if (error == false && payChannel.val() == 1 && (receiptNo.length == 0 || receiptBookNo.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        */
        if (error == false && payChannel.val() == 2 && (chequeNo.length == 0 || chequeBank.length == 0 || chequeBankBranch.length == 0 || chequeDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }

        if (error == false && payChannel.val() == 3 && (cashBank.length == 0 || cashBankBranch.length == 0 || cashBankAccount.length == 0 || cashBankAccountNo.length == 0 || cashBankDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        /*
        if (error == false && dayDiff < 0) {
            error = true;
            msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>";
            focus = "#payment-date";
        }
        */
        if (error == false && receiptNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ";
            focus = "#receipt-no";
        }

        if (error == false && receiptBookNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่";
            focus = "#receipt-book-no";
        }

        if (error == false && receiptDate.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่";
            focus = "#receipt-date";
        }

        if (error == false && receiptSendNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง";
            focus = "#receipt-send-no";
        }

        if (error == false && receiptFund.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เข้ากองทุน";
            focus = "#receipt-fund";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
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
                    /*
                    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
                    */
                    var statusPayment = $("#statuspayment-hidden").val();
                    var payChannel = $("input[name=pay-channel]:checked");
                    var receiptCopy = $("#receipt-copy").val();

                    if (receiptCopy.length > 0)
                        receiptCopy = btoa(receiptCopy);

                    var send = new Array();
                    send[send.length] = "cp2id=" + $("#cp2id").val();
                    send[send.length] = "statuspayment=" + statusPayment;
                    /*
                    if (statusPayment == "1") {
                        var overpayment = $("#overpayment-hidden").val();

                        send[send.length] = "overpayment=" + overpayment;

                        if (parseInt(overpayment) > 0) {
                            send[send.length] = "calinterestyesno=" + calInterestYesNo;

                            if (calInterestYesNo == "Y") {
                                send[send.length] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
                                send[send.length] = "overpaymentdateend=" + $("#overpayment-date-end").val();
                                send[send.length] = "overpaymentyear=" + DelCommas("overpayment-year");
                                send[send.length] = "overpaymentday=" + DelCommas("overpayment-day");
                                send[send.length] = "overpaymentinterest=" + DelCommas("overpayment-interest");
                                send[send.length] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");
                            }
                        }
                    }

                    if (statusPayment == "2" || parseInt(overpayment) <= 0) {
                        send[send.length] = "calinterestyesno=" + calInterestYesNo;

                        if (calInterestYesNo == "Y") {
                            send[send.length] = "payrepaydatestart=" + $("#pay-repay-date-start").val();
                            send[send.length] = "payrepaydateend=" + $("#pay-repay-date-end").val();
                            send[send.length] = "payrepayyear=" + DelCommas("pay-repay-year");
                            send[send.length] = "payrepayday=" + DelCommas("pay-repay-day");
                            send[send.length] = "payrepayinterest=" + DelCommas("pay-repay-interest");
                            send[send.length] = "payrepaytotalinterest=" + DelCommas("total-interest-pay-repay");
                        }
                    }
                    */
                    send[send.length] = "capital=" + DelCommas("capital");
                    send[send.length] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");
                    send[send.length] = "pay=" + DelCommas("pay");
                    send[send.length] = "overpaymenttotalinterestbefore=" + DelCommas("total-interest-overpayment-before");
                    send[send.length] = "payrepaytotalinterest=" + DelCommas("total-interest-pay-repay");
                    send[send.length] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");
                    send[send.length] = "remainaccruedinterest=" + DelCommas("remain-accrued-interest");                    
                    /*
                    send[send.length] = "totalpayment=" + DelCommas("total-payment");
                    */                    
                    send[send.length] = "datetimepayment=" + $("#payment-date").val();
                    send[send.length] = "channel=" + payChannel.val();
                    send[send.length] = "receiptno=" + $("#receipt-no").val();
                    send[send.length] = "receiptbookno=" + $("#receipt-book-no").val();
                    send[send.length] = "receiptdate=" + $("#receipt-date").val();
                    send[send.length] = "receiptsendno=" + $("#receipt-send-no").val();
                    send[send.length] = "receiptfund=" + $("#receipt-fund").val();
                    send[send.length] = "receiptcopy=" + receiptCopy;                    
                    /*
                    if (payChannel.val() == 1) {
                        send[send.length] = "receiptno=" + $("#receipt-no-hidden").val();
                        send[send.length] = "receiptbookno=" + $("#receipt-book-no-hidden").val();
                        send[send.length] = "receiptdate=" + $("#receipt-date-hidden").val();
                    }
                    */
                    
                    if (payChannel.val() == 2) {
                        send[send.length] = "chequeno=" + $("#cheque-no-hidden").val();
                        send[send.length] = "chequebank=" + $("#cheque-bank-hidden").val();
                        send[send.length] = "chequebankbranch=" + $("#cheque-bank-branch-hidden").val();
                        send[send.length] = "chequedate=" + $("#cheque-date-hidden").val();
                    }

                    if (payChannel.val() == 3) {
                        send[send.length] = "cashbank=" + $("#cash-bank-hidden").val();
                        send[send.length] = "cashbankbranch=" + $("#cash-bank-branch-hidden").val();
                        send[send.length] = "cashbankaccount=" + $("#cash-bank-account-hidden").val();
                        send[send.length] = "cashbankaccountno=" + $("#cash-bank-account-no-hidden").val();
                        send[send.length] = "cashbankdate=" + $("#cash-bank-date-hidden").val();
                    }

                    send[send.length] = "lawyerfullname=" + $("#lawyer-fullname-hidden").val();
                    send[send.length] = "lawyerphonenumber=" + $("#lawyer-phonenumber-hidden").val();
                    send[send.length] = "lawyermobilenumber=" + $("#lawyer-mobilenumber-hidden").val();
                    send[send.length] = "lawyeremail=" + $("#lawyer-email-hidden").val();

                    AddCPTransPayment(send, "payrepay");
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddCPTransPayment(send, formatPayment) {
    AddUpdateData("add", "addcptranspayment" + formatPayment, send, false, "", "", "", false, function (result) {
        var data = result.split("<cmd>");

        if (result === "1") {
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
    var period = $("#period-hidden").val();
    var periodTotal = $("#list-trans-payment .detail-trans-payment").length;

    $("#dialog-form" + ($("#dialog-form2").is(":visible") ? "2" : "1") + " #period").html(period);
    $("#detail-trans-payment .button-style1").hide();
    $("#detail-trans-payment .button-style1." + (parseInt(period) === parseInt(periodTotal) ? "period-last" : "period-not-last")).show();
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

        if (_error == false && (_pay.length == 0 || _pay == "0.00")) {
            _error = true;
            _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ";
            _focus = "#pay";
        }

        if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) > 0)) {
            _error = true;
            _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระให้เท่ากับยอดเงินที่ต้องชำระ";
            _focus = "#pay";
        }

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
        if (_statusPayment == "1" && parseInt(_overpayment) > 0)
            _result = ValidateCalInterestOverpayment();

        if (_statusPayment == "2" || parseInt(_overpayment) <= 0)
            _result = ValidateCalInterestPayRepay();
    }

    if (_result == true) {
        var _error = false;
        var _msg;
        var _focus;
        var _totalPayment = DelCommas("total-payment");
        var _pay = DelCommas("pay");
        var _payRepayLeast = DelCommas("pay-repay-least-hidden");

        if (_error == false && (_pay.length == 0 || _pay == "0.00")) {
            _error = true;
            _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ";
            _focus = "#pay";
        }
        /*
        if (_error == false && ((parseFloat(_totalPayment) - parseFloat(_pay)) < 0)) {
            _error = true;
            _msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>";
            _focus = "#pay";
        }
        */
        if (_error == false && ((parseFloat(_totalPayment) >= parseFloat(_payRepayLeast)) && (parseFloat(_pay) < parseFloat(_payRepayLeast)))) {
            _error = true;
            _msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท";
            _focus = "#pay";
        }

        if (_error == true) {
            DialogMessage(_msg, _focus, false, "");
            return false;
        }

        LoadForm(1, "chkbalance", true, "", "", "");
    }
}

function DownloadReceiptCopy(_file) {
    DialogLoading("กำลังโหลด...");

    $("#download-receiptcopy-form #file").val(_file);
    $("#download-receiptcopy-form").submit();

    window.setTimeout(function () {
        $("#dialog-loading").dialog("close");
    }, 500);
}

function doConfirmActionCPTransPayment(action, ecpTransPaymentID) {
    var actionMsg = (action == "add" || action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
                               
                var statusPayment = $("#statuspayment-hidden").val();
                var period = $("#period-hidden").val();
                var periodTotal = $("#list-trans-payment .detail-trans-payment").length;                
                var send = new Array();
                send[send.length] = "cp2id=" + $("#cp2id").val();
                send[send.length] = "statuspayment=" + statusPayment;
                send[send.length] = "ecpTransPaymentID=" + ecpTransPaymentID;
                send[send.length] = "period=" + period;
                send[send.length] = "periodtotal=" + periodTotal;

                AddUpdateData(action, (action + "cptranspayment"), send, false, "", "", "", false, function (result) {
                    if (result == "1") {
                        GotoSignin();
                        return;
                    }

                    DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
                    $("#dialog-confirm").dialog({
                        buttons: {
                            "ตกลง": function () {
                                $(this).dialog("close");

                                CloseFrm(true, '');

                                if (parseInt(periodTotal) === 1)
                                    GoToPage(1, 9);
                                else                                   
                                    LoadForm(1, "adddetailcptranspayment", false, "adddetail-data-trans-payment", $("#cp2id").val(), "");
                            }
                        }
                    });
                });    
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });

    /*
    DialogConfirm("ต้องการลบข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");


                if (ValidateCPTransPaymentPayRepay() == true) {
                    var statusPayment = $("#statuspayment-hidden").val();
                    var payChannel = $("input[name=pay-channel]:checked");
                    var receiptCopy = $("#receipt-copy").val();

                    if (receiptCopy.length > 0)
                        receiptCopy = btoa(receiptCopy);

                    var send = new Array();
                    send[send.length] = "cp2id=" + $("#cp2id").val();
                    send[send.length] = "statuspayment=" + statusPayment;
                    send[send.length] = "capital=" + DelCommas("capital");
                    send[send.length] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");
                    send[send.length] = "pay=" + DelCommas("pay");
                    send[send.length] = "overpaymenttotalinterestbefore=" + DelCommas("total-interest-overpayment-before");
                    send[send.length] = "payrepaytotalinterest=" + DelCommas("total-interest-pay-repay");
                    send[send.length] = "overpaymenttotalinterest=" + DelCommas("total-interest-overpayment");                    
                    send[send.length] = "datetimepayment=" + $("#payment-date").val();
                    send[send.length] = "channel=" + payChannel.val();
                    send[send.length] = "receiptno=" + $("#receipt-no").val();
                    send[send.length] = "receiptbookno=" + $("#receipt-book-no").val();
                    send[send.length] = "receiptdate=" + $("#receipt-date").val();
                    send[send.length] = "receiptsendno=" + $("#receipt-send-no").val();
                    send[send.length] = "receiptfund=" + $("#receipt-fund").val();
                    send[send.length] = "receiptcopy=" + receiptCopy;                    

                    AddCPTransPayment(send, "payrepay");
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
    */
}