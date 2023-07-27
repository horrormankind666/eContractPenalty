function ResetFrmSelectFormatPayment() {
    var formatPayment = $("input[name=format-payment]:radio");

    formatPayment.attr("checked", false);
}

function CaptionFormatPayment() {    
    var send = new Array();
    send[send.length] = ("formatpayment=" + $("#format-payment-hidden").val());

    SetMsgLoading("");

    ViewData("formatpayment", send, function (result) {
        $("#format-payment-input span").html(result);
    });
}

function ResetFormatPayment() {
    if ($("#format-payment-hidden").val().length == 0 ||
        $("#format-payment-hidden").val() == "0") {
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
            var statusPaymentRecord = $(this).val();
            var send = new Array();
            send[send.length] = ("cp2id=" + $("#cp2id").val());
            send[send.length] = ("statuspaymentrecord=" + (statusPaymentRecord == "C" ? "" : statusPaymentRecord));
            send[send.length] = ("statuspaymentrecordlawyerfullname=" + $("#statuspaymentrecord-lawyer-fullname-hidden").val());
            send[send.length] = ("statuspaymentrecordlawyerphonenumber=" + $("#statuspaymentrecord-lawyer-phonenumber-hidden").val());
            send[send.length] = ("statuspaymentrecordlawyermobilenumber=" + $("#statuspaymentrecord-lawyer-mobilenumber-hidden").val());
            send[send.length] = ("statuspaymentrecordlawyeremail=" + $("#statuspaymentrecord-lawyer-email-hidden").val());

            ResetTabAddDetailTransPayment(statusPaymentRecord);
            AddUpdateStatusPaymentRecord(send);
        });

        ResetFrmStatusPaymentRecord();
    }
}

function ResetFrmStatusPaymentRecord() {
    var statusPaymentRecord = ($("#statuspaymentrecord-hidden").val().length > 0 ? $("#statuspaymentrecord-hidden").val() : "C");
    
    if ($("input[name=status-payment-record]:radio").filter("[value=" + statusPaymentRecord + "]").length > 0)
        $("input[name=status-payment-record][value=" + statusPaymentRecord + "]").prop("checked", true);

    $("#status-payment-record-lawyer").html($("#statuspaymentrecord-lawyer-hidden").val());
    ResetTabAddDetailTransPayment(statusPaymentRecord);
}

function ResetTabAddDetailTransPayment(statusPaymentRecord) {
    if (statusPaymentRecord == "P")
        $("#tab2-adddetail-cp-trans-payment").hide();
    else
        $("#tab2-adddetail-cp-trans-payment").show();
}

function AddUpdateStatusPaymentRecord(send) {
    AddUpdateData("update", "updatestatuspaymentrecord", send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }
        
        $("#statuspaymentrecord-hidden").val($("input[name=status-payment-record]:checked").val());
        $("#statuspaymentrecord-lawyer-hidden").val($("#statuspaymentrecord-lawyer-fullname-hidden").val());
        ResetFrmStatusPaymentRecord();
    });
}

function ResetListTransPayment() {
    if ($("#statuspayment-hidden").val() == "3")
        $("#tab2-adddetail-cp-trans-payment").hide();

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
    var typeInterest = $("#type-interest-hidden").val();
    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var statusPayment = $("#statuspayment-hidden").val();
    var overpayment = $("#overpayment-hidden").val();
    var formatPayment = $("#format-payment-hidden").val();

    if (calInterestYesNo == "Y") {
        $("#cal-interest-" + typeInterest).show();

        if (typeInterest == "overpayment")
            $("#total-interest-overpayment").val("");

        if (typeInterest == "payrepay")
            $("#total-interest-pay-repay").val("");

        $("#total-interest").val("");
        $("#total-payment").val("");
        $("#pay").val($("#total-payment").val());

        if (statusPayment == "1") {
            if (parseInt(overpayment) > 0)
                InitCalendarFromTo("#overpayment-date-end", false, "#payment-date", false);
        }

        if (statusPayment == "2" ||
            (formatPayment == "2" && parseInt(overpayment) <= 0))
            InitCalendarFromTo("#pay-repay-date-end", false, "#payment-date", false);
    }

    if (calInterestYesNo == "N") {
        $("#cal-interest-" + typeInterest).hide();
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
        var payChannel = $("input[name=pay-channel]:checked").val();
        var payChannelIndex = $("input[name=pay-channel]:checked").index("input[name=pay-channel]");

        $("#pay-channel-index-hidden").val(payChannelIndex);

        if (payChannel != "1")
            LoadForm(1, "adddetailpaychannel", true, "", payChannel, "");
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
                            $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                              "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                              "#receipt-copy-preview").addClass("hidden");
                            $("#receipt-copy-input .uploadfile-container .preloading-inline").removeClass("hidden");
                        },
                        async: true,
                        type: "POST",
                        url: "FileProcess.aspx",
                        data: fd,
                        contentType: false,
                        processData: false,
                        success: function (result) {
                            $("#receipt-copy").val("data:" + files.type + ";base64," + result);

                            try {
                                var pdfData = atob(result);
                                var pdfjs = window["pdfjs-dist/build/pdf"];

                                pdfjs.GlobalWorkerOptions.workerSrc = "https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.worker.min.js";

                                var canvas = document.getElementById("receipt-copy-preview");
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
                                        
                                        $("#receipt-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                        $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                                          "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                                          "#receipt-copy-preview, " +
                                          "#receipt-copy-linkpreview").removeClass("hidden");

                                        DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                                    });
                                });
                            }
                            catch (e) {
                                $("#receipt-copy-input .uploadfile-container .preloading-inline").addClass("hidden");
                                $("#receipt-copy-input .uploadfile-container .uploadfile-form, " +
                                  "#receipt-copy-input .uploadfile-container .form-discription-style, " +
                                  "#receipt-copy-nopreview, " +
                                  "#receipt-copy-linkpreview").removeClass("hidden");

                                DialogMessage("อัพโหลดเอกสารเรียบร้อย", "", false, "");
                            };

                            /*
                            $("#receipt-copy-preview").attr("src", ("data:" + files.type + ";base64, " + result));
                            $("#receipt-copy-preview").load(function () {
                                var imgH = 217;

                                $(this).css({
                                    "display": ($(this).height() > imgH ? "block" : "table-cell"),
                                    "height": ($(this).height() > imgH ? "inherit" : "auto"),
                                    "vertical-align": ($(this).height() > imgH ? "top" : "middle"),
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
    var overpay = $("#overpay").val();

    capital = (capital.length === 0 || capital === "0.00" ? 0 : DelCommas("capital"));
    pay = (pay.length === 0 || pay === "0.00" ? 0 : DelCommas("pay"));
    overpay = (overpay.length === 0 || overpay === "0.00" ? 0 : DelCommas("overpay"));

    if (formatPayment === "1") {
        var totalInterestOverpayment = $("#total-interest-overpayment").val();

        totalInterestOverpayment = (totalInterestOverpayment.length === 0 || totalInterestOverpayment === "0.00" ? 0 : DelCommas("total-interest-overpayment"));

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

        totalAccruedInterest = (totalAccruedInterest.length === 0 || totalAccruedInterest === "0.00" ? 0 : DelCommas("total-accrued-interest"));
        totalInterestOverpaymentBefore = (totalInterestOverpaymentBefore.length === 0 || totalInterestOverpaymentBefore === "0.00" ? 0 : DelCommas("total-interest-overpayment-before"));
        totalInterestPayRepay = (totalInterestPayRepay.length === 0 || totalInterestPayRepay === "0.00" ? 0 : DelCommas("total-interest-pay-repay"));        
        totalInterestOverpayment = (totalInterestOverpayment.length === 0 || totalInterestOverpayment === "0.00" ? 0 : DelCommas("total-interest-overpayment"));        

        payCapital = (parseFloat(pay) - parseFloat(totalAccruedInterest) - parseFloat(totalInterestOverpaymentBefore) - parseFloat(totalInterestPayRepay) - parseFloat(totalInterestOverpayment));
        payCapital = (parseFloat(payCapital) < 0 ? 0 : payCapital);
        
        overpay = (parseFloat(payCapital) - parseFloat(capital));
        overpay = (parseFloat(overpay) < 0 ? 0 : overpay);
        $("#overpay").val(parseFloat(overpay).toFixed(2));
        Trim("overpay");
        AddCommas("overpay", 2);

        payCapital = (parseFloat(payCapital) - parseFloat(overpay));
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
        $("#overpay").val("");
        AddCommas("overpay", 2);
        TextboxDisable("#overpay");
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

    if (statusPayment == "2" ||
        (formatPayment == "2" && parseInt(overpayment) <= 0)) {
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
    var payChannelIndex = $("#pay-channel-index-hidden").val();

    $("input[name=pay-channel]:radio")[parseInt(payChannelIndex)].checked = true;
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
    var error = false;
    var msg;
    var focus;

    /*
    if (error == false &&
        $("#receipt-no").length > 0 &&
        $("#receipt-no").val().length == 0) {
        error = true;
        msg = "กรุณาใส่เลขที่ใบเสร็จ";
        focus = "#receipt-no";
    }

    if (error == false &&
        $("#receipt-book-no").length > 0 &&
        $("#receipt-book-no").val().length == 0) {
        error = true;
        msg = "กรุณาใส่เล่มที่";
        focus = "#receipt-book-no";
    }

    if (error == false &&
        $("#receipt-date").length > 0 &&
        $("#receipt-date").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่บนใบเสร็จ";
        focus = "#receipt-date";
    }
    */
    if (error == false &&
        $("#cheque-no").length > 0 &&
        $("#cheque-no").val().length == 0) {
        error = true;
        msg = "กรุณาใส่เลขที่เช็ค";
        focus = "#cheque-no";
    }

    if (error == false &&
        $("#cheque-bank").length > 0 &&
        $("#cheque-bank").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อธนาคาร";
        focus = "#cheque-bank";
    }

    if (error == false &&
        $("#cheque-bank-branch").length > 0 &&
        $("#cheque-bank-branch").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อสาขาของธนาคาร";
        focus = "#cheque-bank-branch";
    }

    if (error == false &&
        $("#cheque-date").length > 0 &&
        $("#cheque-date").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่บนเช็ค";
        focus = "#cheque-date";
    }

    if (error == false &&
        $("#cash-bank").length > 0 &&
        $("#cash-bank").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อธนาคาร";
        focus = "#cash-bank";
    }

    if (error == false &&
        $("#cash-bank-branch").length > 0 &&
        $("#cash-bank-branch").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อสาขาของธนาคาร";
        focus = "#cash-bank-branch";
    }

    if (error == false &&
        $("#cash-bank-account").length > 0 &&
        $("#cash-bank-account").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อบัญชี";
        focus = "#cash-bank-account";
    }

    if (error == false &&
        $("#cash-bank-account-no").length > 0 &&
        $("#cash-bank-account-no").val().length == 0) {
        error = true;
        msg = "กรุณาใส่เลขที่บัญชี";
        focus = "#cash-bank-account-no";
    }

    if (error == false &&
        $("#cash-bank-date").length > 0 &&
        $("#cash-bank-date").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่บนใบนำฝาก";
        focus = "#cash-bank-date";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
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

function ChkSelectFormatPayment(
    cp2id,
    statusPayment,
    formatPayment
) {
    if (statusPayment == "1" &&
        formatPayment == "0")
        LoadForm(1, "selectformatpayment", true, "", cp2id, ("trans-payment" + cp2id));

    if (statusPayment == "2" &&
        formatPayment == "2")
        OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", cp2id, "");

    if (statusPayment == "3" &&
        (formatPayment == "1" || formatPayment == "2"))
        OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", cp2id, "");;
}

function ValidateSelectFormatPayment() {
    var error = false;
    var msg;
    var focus;
    var formatPayment = $("input[name=format-payment]:checked");

    if (error == false &&
        formatPayment.length == 0) {
        error = true;
        msg = "กรุณาเลือกรูปแบบที่ต้องการชำระหนี้";
        focus = "#format-payment-full-repay-input";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    $("#dialog-form1").dialog("close");
    OpenTab("link-tab2-cp-trans-payment", "#tab2-cp-trans-payment", "บันทึกการชำระหนี้", false, "", $("#cp2id-hidden").val(), "");    
}

function ValidateCPTransPaymentFullRepay() {
    /*
    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var result = (calInterestYesNo == "Y" ? ValidateCalInterestOverpayment() : true);
    */
    var result = true;

    if (result == true) {   
        var error = false;
        var msg;
        var focus;
        /*
        var interestDateEnd = ($("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "");
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
            dayDiff = DateDiff(interestDateEnd, paymentDate, "days");
        }
        */

        if (error === false &&
            (totalPayment.length === 0 || parseFloat(totalPayment) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระ";
            focus = "#total-payment";
        }

        if (error === false &&
            (pay.length === 0 || parseFloat(pay) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้น";
            focus = "#pay";
        }

        if (error === false &&
            parseFloat(totalPayment) !== parseFloat((parseFloat(totalInterestOverpayment) + parseFloat(pay) + parseFloat(overpay)).toFixed(2))) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระให้ถูกต้อง";
            focus = "#total-payment";
        }
        /*
        if (error === false &&
            parseFloat(pay) !== parseFloat(totalPayment)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้นให้เท่ากับจำนวนเงินที่ชำระ";
            focus = "#pay";
        }
        */
        if (error === false &&
            (remainCapital.length === 0 || parseFloat(remainCapital) !== 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่หักชำระเงินต้นให้เท่ากับ<br />จำนวนเงินต้นคงเหลือยกมา";
            focus = "#pay";
        }

        if (error == false &&
            payChannel.length == 0) {
            error = true;
            msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้";
            focus = "#pay-channel1-input";
        }
        /*
        if (error == false &&
            payChannel.val() == 1 &&
            (receiptNo.length == 0 || receiptBookNo.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        */
        if (error == false &&
            payChannel.val() == 2 &&
            (chequeNo.length == 0 || chequeBank.length == 0 || chequeBankBranch.length == 0 || chequeDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";;
        }

        if (error == false &&
            payChannel.val() == 3 &&
            (cashBank.length == 0 || cashBankBranch.length == 0 || cashBankAccount.length == 0 || cashBankAccountNo.length == 0 || cashBankDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";;
        }
        /*
        if (error == false &&
            dayDiff < 0) {
            error = true;
            msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>";
            focus = "#payment-date";
        }
        */
        if (error == false &&
            receiptNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ";
            focus = "#receipt-no";
        }

        if (error == false &&
            receiptBookNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่";
            focus = "#receipt-book-no";
        }

        if (error == false &&
            receiptDate.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่";
            focus = "#receipt-date";
        }

        if (error == false &&
            receiptSendNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง";
            focus = "#receipt-send-no";
        }

        if (error == false &&
            receiptFund.length == 0) {
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
                    send[send.length] = ("cp2id=" + $("#cp2id").val());
                    /*
                    send[send.length] = ("overpayment=" + overpayment);
                    
                    if (parseInt(overpayment) > 0) {
                        var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();

                        send[send.length] = ("calinterestyesno=" + calInterestYesNo);

                        if (calInterestYesNo == "Y") {
                            send[send.length] = ("overpaymentdatestart=" + $("#overpayment-date-start").val());
                            send[send.length] = ("overpaymentdateend=" + $("#overpayment-date-end").val());
                            send[send.length] = ("overpaymentyear=" + DelCommas("overpayment-year"));
                            send[send.length] = ("overpaymentday=" + DelCommas("overpayment-day"));
                            send[send.length] = ("overpaymentinterest=" + DelCommas("overpayment-interest"));
                            send[send.length] = ("overpaymenttotalinterest=" + DelCommas("total-interest-overpayment"));
                        }
                    }
                    */
                                        
                    send[send.length] = ("capital=" + DelCommas("capital"));
                    /*
                    send[send.length] = ("totalaccruedinterest=" + (totalAccruedInterest.length > 0 ? totalAccruedInterest : "0.00"));
                    */
                    send[send.length] = ("totalpayment=" + DelCommas("total-payment"));
                    send[send.length] = ("overpaymenttotalinterest=" + DelCommas("total-interest-overpayment"));
                    send[send.length] = ("pay=" + DelCommas("pay"));
                    send[send.length] = ("overpay=" + DelCommas("overpay"));
                    send[send.length] = ("datetimepayment=" + $("#payment-date").val());
                    send[send.length] = ("channel=" + payChannel.val());
                    send[send.length] = ("receiptno=" + $("#receipt-no").val());
                    send[send.length] = ("receiptbookno=" + $("#receipt-book-no").val());
                    send[send.length] = ("receiptdate=" + $("#receipt-date").val());
                    send[send.length] = ("receiptsendno=" + $("#receipt-send-no").val());
                    send[send.length] = ("receiptfund=" + $("#receipt-fund").val());
                    send[send.length] = ("receiptcopy=" + receiptCopy);
                    
                    /*
                    if (payChannel.val() == 1) {
                        send[send.length] = ("receiptno=" + $("#receipt-no-hidden").val());
                        send[send.length] = ("receiptbookno=" + $("#receipt-book-no-hidden").val());
                        send[send.length] = ("receiptdate=" + $("#receipt-date-hidden").val());
                    }
                    */
                    
                    if (payChannel.val() == 2) {
                        send[send.length] = ("chequeno=" + $("#cheque-no-hidden").val());
                        send[send.length] = ("chequebank=" + $("#cheque-bank-hidden").val());
                        send[send.length] = ("chequebankbranch=" + $("#cheque-bank-branch-hidden").val());
                        send[send.length] = ("chequedate=" + $("#cheque-date-hidden").val());
                    }

                    if (payChannel.val() == 3) {
                        send[send.length] = ("cashbank=" + $("#cash-bank-hidden").val());
                        send[send.length] = ("cashbankbranch=" + $("#cash-bank-branch-hidden").val());
                        send[send.length] = ("cashbankaccount=" + $("#cash-bank-account-hidden").val());
                        send[send.length] = ("cashbankaccountno=" + $("#cash-bank-account-no-hidden").val());
                        send[send.length] = ("cashbankdate=" + $("#cash-bank-date-hidden").val());
                    }

                    send[send.length] = ("lawyerfullname=" + $("#lawyer-fullname-hidden").val());
                    send[send.length] = ("lawyerphonenumber=" + $("#lawyer-phonenumber-hidden").val());
                    send[send.length] = ("lawyermobilenumber=" + $("#lawyer-mobilenumber-hidden").val());
                    send[send.length] = ("lawyeremail=" + $("#lawyer-email-hidden").val());

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
        if (statusPayment == "1" &&
            parseInt(overpayment) > 0) {
            interestDateEnd = ($("#overpayment-date-end").length > 0 ? $("#overpayment-date-end").val() : "");
            result = ValidateCalInterestOverpayment();
        }

        if (statusPayment == "2" ||
            parseInt(overpayment) <= 0) {
            interestDateEnd = ($("#pay-repay-date-end").length > 0 ? $("#pay-repay-date-end").val() : "");
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
        if (calInterestYesNo == "Y" &&
            interestDateEnd.length > 0) {
            interestDateEnd = GetDateObject(interestDateEnd);
            paymentDate = GetDateObject(paymentDate);
            dayDiff = DateDiff(interestDateEnd, paymentDate, "days");
        }
        */
        if (error === false &&
            (pay.length == 0 || parseFloat(pay) === 0)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้";
            focus = "#pay";
        }

        if (error === false &&
            payCapital.length > 0 &&
            parseFloat(payCapital) > parseFloat(capital)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ให้ถูกต้อง<br />เนื่องจากจำนวนเงินต้นคงเหลือที่ยกมาต้องมากกว่าหรือเท่ากับ<br />จำนวนเงินที่หักชำระเงินต้นในงวดนี้";
            focus = "#pay";
        }

        if (error === false &&
            remainCapital.length > 0 &&
            parseFloat(remainCapital) < 0) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ให้ถูกต้อง<br />เนื่องจากจำนวนเงินต้นคงเหลือน้อยกว่า 0";
            focus = "#pay";
        }
        /*
        if (error == false &&
            (parseFloat(totalPayment) - parseFloat(pay)) < 0) {
            error = true;
            msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>";
            focus = "#pay";
        }
        */
        if (error == false &&
            parseFloat(pay) < parseFloat(payRepayLeast)) {
            error = true;
            msg = ("กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท");
            focus = "#pay";
        }

        if (error === false &&
            parseFloat(pay) < parseFloat(totalInterrest)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ชำระในงวดนี้ไม่น้อยกว่าจำนวนเงินที่หักชำระดอกเบี้ย";
            focus = "#pay";
        }
        /*
        if (error == false &&
            parseFloat(totalPayment) >= parseFloat(payRepayLeast) &&
            parseFloat(pay) < parseFloat(payRepayLeast)) {
            error = true;
            msg = ("กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท");
            focus = "#pay";
        }
        */
        if (error == false &&
            payChannel.length == 0) {
            error = true;
            msg = "กรุณาใส่ช่องทางหรือวิธีการชำระหนี้";
            focus = "#pay-channel1-input";
        }
        /*
        if (error == false &&
            payChannel.val() == 1 &&
            (receiptNo.length == 0 || receiptBookNo.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        */
        if (error == false &&
            payChannel.val() == 2 &&
            (chequeNo.length == 0 || chequeBank.length == 0 || chequeBankBranch.length == 0 || chequeDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }

        if (error == false &&
            payChannel.val() == 3 &&
            (cashBank.length == 0 || cashBankBranch.length == 0 || cashBankAccount.length == 0 || cashBankAccountNo.length == 0 || cashBankDate.length == 0)) {
            error = true;
            msg = "กรุณาใส่รายละเอียดช่องทางหรือวิธีการชำระหนี้";
            focus = "";
        }
        /*
        if (error == false &&
            dayDiff < 0) {
            error = true;
            msg = "<div>กรุณาใส่วันที่ทำการชำระหนี้ให้มากกว่าหรือเท่ากับ</div><div>วันที่สิ้นสุดการคิดดอกเบี้ย</div>";
            focus = "#payment-date";
        }
        */
        if (error == false &&
            receiptNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบเสร็จ";
            focus = "#receipt-no";
        }

        if (error == false &&
            receiptBookNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เล่มที่";
            focus = "#receipt-book-no";
        }

        if (error == false &&
            receiptDate.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - ลงวันที่";
            focus = "#receipt-date";
        }

        if (error == false &&
            receiptSendNo.length == 0) {
            error = true;
            msg = "กรุณาใส่รายละเอียดใบเสร็จ - เลขที่ใบนำส่ง";
            focus = "#receipt-send-no";
        }

        if (error == false &&
            receiptFund.length == 0) {
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
                    send[send.length] = ("cp2id=" + $("#cp2id").val());
                    send[send.length] = ("statuspayment=" + statusPayment);
                    /*
                    if (statusPayment == "1") {
                        var overpayment = $("#overpayment-hidden").val();

                        send[send.length] = ("overpayment=" + overpayment);

                        if (parseInt(overpayment) > 0) {
                            send[send.length] = ("calinterestyesno=" + calInterestYesNo);

                            if (calInterestYesNo == "Y") {
                                send[send.length] = ("overpaymentdatestart=" + $("#overpayment-date-start").val());
                                send[send.length] = ("overpaymentdateend=" + $("#overpayment-date-end").val());
                                send[send.length] = ("overpaymentyear=" + DelCommas("overpayment-year"));
                                send[send.length] = ("overpaymentday=" + DelCommas("overpayment-day"));
                                send[send.length] = ("overpaymentinterest=" + DelCommas("overpayment-interest"));
                                send[send.length] = ("overpaymenttotalinterest=" + DelCommas("total-interest-overpayment"));
                            }
                        }
                    }

                    if (statusPayment == "2" ||
                        parseInt(overpayment) <= 0) {
                        send[send.length] = ("calinterestyesno=" + calInterestYesNo);

                        if (calInterestYesNo == "Y") {
                            send[send.length] = ("payrepaydatestart=" + $("#pay-repay-date-start").val());
                            send[send.length] = ("payrepaydateend=" + $("#pay-repay-date-end").val());
                            send[send.length] = ("payrepayyear=" + DelCommas("pay-repay-year"));
                            send[send.length] = ("payrepayday=" + DelCommas("pay-repay-day"));
                            send[send.length] = ("payrepayinterest=" + DelCommas("pay-repay-interest"));
                            send[send.length] = ("payrepaytotalinterest=" + DelCommas("total-interest-pay-repay"));
                        }
                    }
                    */
                    send[send.length] = ("capital=" + DelCommas("capital"));
                    send[send.length] = ("totalaccruedinterest=" + DelCommas("total-accrued-interest"));
                    send[send.length] = ("pay=" + DelCommas("pay"));
                    send[send.length] = ("overpay=" + DelCommas("overpay"));
                    send[send.length] = ("overpaymenttotalinterestbefore=" + DelCommas("total-interest-overpayment-before"));
                    send[send.length] = ("payrepaytotalinterest=" + DelCommas("total-interest-pay-repay"));
                    send[send.length] = ("overpaymenttotalinterest=" + DelCommas("total-interest-overpayment"));
                    send[send.length] = ("remainaccruedinterest=" + DelCommas("remain-accrued-interest"));                    
                    /*
                    send[send.length] = ("totalpayment=" + DelCommas("total-payment"));
                    */                    
                    send[send.length] = ("datetimepayment=" + $("#payment-date").val());
                    send[send.length] = ("channel=" + payChannel.val());
                    send[send.length] = ("receiptno=" + $("#receipt-no").val());
                    send[send.length] = ("receiptbookno=" + $("#receipt-book-no").val());
                    send[send.length] = ("receiptdate=" + $("#receipt-date").val());
                    send[send.length] = ("receiptsendno=" + $("#receipt-send-no").val());
                    send[send.length] = ("receiptfund=" + $("#receipt-fund").val());
                    send[send.length] = ("receiptcopy=" + receiptCopy);
                    /*
                    if (payChannel.val() == 1) {
                        send[send.length] = ("receiptno=" + $("#receipt-no-hidden").val());
                        send[send.length] = ("receiptbookno=" + $("#receipt-book-no-hidden").val());
                        send[send.length] = ("receiptdate=" + $("#receipt-date-hidden").val());
                    }
                    */
                    
                    if (payChannel.val() == 2) {
                        send[send.length] = ("chequeno=" + $("#cheque-no-hidden").val());
                        send[send.length] = ("chequebank=" + $("#cheque-bank-hidden").val());
                        send[send.length] = ("chequebankbranch=" + $("#cheque-bank-branch-hidden").val());
                        send[send.length] = ("chequedate=" + $("#cheque-date-hidden").val());
                    }

                    if (payChannel.val() == 3) {
                        send[send.length] = ("cashbank=" + $("#cash-bank-hidden").val());
                        send[send.length] = ("cashbankbranch=" + $("#cash-bank-branch-hidden").val());
                        send[send.length] = ("cashbankaccount=" + $("#cash-bank-account-hidden").val());
                        send[send.length] = ("cashbankaccountno=" + $("#cash-bank-account-no-hidden").val());
                        send[send.length] = ("cashbankdate=" + $("#cash-bank-date-hidden").val());
                    }

                    send[send.length] = ("lawyerfullname=" + $("#lawyer-fullname-hidden").val());
                    send[send.length] = ("lawyerphonenumber=" + $("#lawyer-phonenumber-hidden").val());
                    send[send.length] = ("lawyermobilenumber=" + $("#lawyer-mobilenumber-hidden").val());
                    send[send.length] = ("lawyeremail=" + $("#lawyer-email-hidden").val());

                    AddCPTransPayment(send, "payrepay");
                }
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddCPTransPayment(
    send,
    formatPayment
) {
    AddUpdateData("add", ("addcptranspayment" + formatPayment), send, false, "", "", "", false, function (result) {
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

function ShowDetailTransPayment(
    cp2id,
    period
) {
    $("#period-hidden").val(period);

    LoadForm(($("#dialog-form1").dialog("isOpen") == true ? 2 : 1), "detailtranspayment", true, "", cp2id, ("detail-trans-payment" + cp2id));
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
    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var result = (calInterestYesNo == "Y" ? ValidateCalInterestOverpayment() : true);

    if (result == true) {
        var error = false;
        var msg;
        var focus;        
        var totalPayment = DelCommas("total-payment");
        var pay = DelCommas("pay");

        if (error == false &&
            (pay.length == 0 || pay == "0.00")) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ";
            focus = "#pay";
        }

        if (error == false &&
            (parseFloat(totalPayment) - parseFloat(pay)) > 0) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระให้เท่ากับยอดเงินที่ต้องชำระ";
            focus = "#pay";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
            return false;
        }

        LoadForm(1, "chkbalance", true, "", "", "");
    }
}

function ChkBalanceCPTransPaymentPayRepay() {
    var calInterestYesNo = $("input[name=cal-interest-yesno]:checked").val();
    var statusPayment = $("#statuspayment-hidden").val();
    var overpayment = $("#overpayment-hidden").val();
    var result = true;

    if (calInterestYesNo == "Y") {
        if (statusPayment == "1" &&
            parseInt(overpayment) > 0)
            result = ValidateCalInterestOverpayment();

        if (statusPayment == "2" ||
            parseInt(overpayment) <= 0)
            result = ValidateCalInterestPayRepay();
    }

    if (result == true) {
        var error = false;
        var msg;
        var focus;
        var totalPayment = DelCommas("total-payment");
        var pay = DelCommas("pay");
        var payRepayLeast = DelCommas("pay-repay-least-hidden");

        if (error == false &&
            (pay.length == 0 || pay == "0.00")) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระ";
            focus = "#pay";
        }
        /*
        if (error == false &&
            (parseFloat(totalPayment) - parseFloat(pay)) < 0) {
            error = true;
            msg = "<div>กรุณาใส่จำนวนเงินที่ต้องการชำระให้น้อยกว่าหรือเท่ากับ</div><div>ยอดเงินที่ต้องชำระ</div>";
            focus = "#pay";
        }
        */
        if (error == false &&
            parseFloat(totalPayment) >= parseFloat(payRepayLeast) &&
            parseFloat(pay) < parseFloat(payRepayLeast)) {
            error = true;
            msg = "กรุณาใส่จำนวนเงินที่ต้องการชำระไม่น้อยกว่า " + $("#pay-repay-least-hidden").val() + " บาท";
            focus = "#pay";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
            return false;
        }

        LoadForm(1, "chkbalance", true, "", "", "");
    }
}

function DownloadReceiptCopy(file) {
    DialogLoading("กำลังโหลด...");

    $("#download-receiptcopy-form #file").val(file);
    $("#download-receiptcopy-form").submit();

    window.setTimeout(function () {
        $("#dialog-loading").dialog("close");
    }, 500);
}

function doConfirmActionCPTransPayment(
    action,
    ecpTransPaymentID
) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
                               
                var statusPayment = $("#statuspayment-hidden").val();
                var period = $("#period-hidden").val();
                var periodTotal = $("#list-trans-payment .detail-trans-payment").length;                
                var send = new Array();
                send[send.length] = ("cp2id=" + $("#cp2id").val());
                send[send.length] = ("statuspayment=" + statusPayment);
                send[send.length] = ("ecpTransPaymentID=" + ecpTransPaymentID);
                send[send.length] = ("period=" + period);
                send[send.length] = ("periodtotal=" + periodTotal);

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
                    send[send.length] = ("cp2id=" + $("#cp2id").val());
                    send[send.length] = ("statuspayment=" + statusPayment);
                    send[send.length] = ("capital=" + DelCommas("capital"));
                    send[send.length] = ("totalaccruedinterest=" + DelCommas("total-accrued-interest"));
                    send[send.length] = ("pay=" + DelCommas("pay"));
                    send[send.length] = ("overpaymenttotalinterestbefore=" + DelCommas("total-interest-overpayment-before"));
                    send[send.length] = ("payrepaytotalinterest=" + DelCommas("total-interest-pay-repay"));
                    send[send.length] = ("overpaymenttotalinterest=" + DelCommas("total-interest-overpayment"));                    
                    send[send.length] = ("datetimepayment=" + $("#payment-date").val());
                    send[send.length] = ("channel=" + payChannel.val());
                    send[send.length] = ("receiptno=" + $("#receipt-no").val());
                    send[send.length] = ("receiptbookno=" + $("#receipt-book-no").val());
                    send[send.length] = ("receiptdate=" + $("#receipt-date").val());
                    send[send.length] = ("receiptsendno=" + $("#receipt-send-no").val());
                    send[send.length] = ("receiptfund=" + $("#receipt-fund").val());
                    send[send.length] = ("receiptcopy=" + receiptCopy);

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