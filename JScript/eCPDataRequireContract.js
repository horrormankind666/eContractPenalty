function ReceiverCPTransBreakContract(
    cp1id,
    trackingStatus
) {
    ChkTrackingStatusViewTransBreakContract(cp1id, trackingStatus, "", function (result) {
        if (result == "0") {
            $("#dialog-form1").dialog("close");
            OpenTab("link-tab3-cp-trans-require-contract", "#tab3-cp-trans-require-contract", "รับรายการแจ้ง", false, "add", cp1id, trackingStatus);
        }
    });
}

function InitStudyLeaveYesNo() {
    if ($("#study-leave-yesno").length > 0) {
        $("input[name=study-leave-yesno]:radio").click(function () {
            ResetFrmStudyLeaveYesNo();
        });
    }
}

function ResetFrmStudyLeaveYesNo() {
    var studyLeaveYesNo = $("input[name=study-leave-yesno]:checked").val();

    $("#study-leave-yesno #study-leave-status-yes .calendar").val("");
    $("#study-leave-yesno #study-leave-status-no .calendar").val("");
    CalendarDisable("#study-leave-yesno #study-leave-status-yes .calendar");
    CalendarDisable("#study-leave-yesno #study-leave-status-no .calendar");

    if (studyLeaveYesNo == "Y")
        CalendarEnable("#study-leave-yesno #study-leave-status-yes .calendar");

    if (studyLeaveYesNo == "N")
        CalendarEnable("#study-leave-yesno #study-leave-status-no .calendar");

    $("#all-actual-date").val("");
    $("#actual-date").val("");
    $("#remain-date").val("");
    $("#subtotal-penalty").val("");
    $("#total-penalty").val("");
}

function InitCPTransRequireContract() {
    $("#lawyer-phonenumber").inputmask("9 9999 9999");
    $("#lawyer-mobilenumber").inputmask("99 9999 9999");
    $("#lawyer-email").inputmask("email");
}

function ResetFrmCPTransRequireContract(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
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
        $("input[name=study-leave-yesno]:radio").prop("disabled", true);
        CalendarDisable("#study-leave-yesno #study-leave-status-yes .calendar");
        CalendarDisable("#study-leave-yesno #study-leave-status-no .calendar");
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

        if ($("#scholar-hidden").val() == "2")
            TextboxDisable("#all-actual-month-scholarship");

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
            $("input[name=study-leave-yesno]:radio").prop("checked", false);
            $("input[name=study-leave-yesno]:radio").filter("[value='" + $("#study-leave-hidden").val() + "']").prop("checked", true);
            ResetFrmStudyLeaveYesNo();

            $("#require-date").val($("#require-date-hidden").val());
            $("#approve-date").val($("#approve-date-hidden").val());
            InitCalendarFromTo("#require-date", false, "#approve-date", false);
            $("#before-study-leave-start-date").val($("#before-study-leave-start-date-hidden").val());
            $("#before-study-leave-end-date").val($("#before-study-leave-end-date-hidden").val());
            InitCalendarFromTo("#before-study-leave-start-date", false, "#before-study-leave-end-date", false);
            $("#study-leave-start-date").val($("#study-leave-start-date-hidden").val());
            $("#study-leave-end-date").val($("#study-leave-end-date-hidden").val());
            InitCalendarFromTo("#study-leave-start-date", false, "#study-leave-end-date", false);
            $("#after-study-leave-start-date").val($("#after-study-leave-start-date-hidden").val());
            $("#after-study-leave-end-date").val($("#after-study-leave-end-date-hidden").val());
            InitCalendarFromTo("#after-study-leave-start-date", false, "#after-study-leave-end-date", false);
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
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        (
            $("#lawyer-fullname").val().length == 0 ||
            ($("#lawyer-phonenumber").val().length == 0 && $("#lawyer-mobilenumber").val().length == 0) ||
            $("#lawyer-email").val().length == 0
        )) {
        error = true;
        msg = "กรุณาใส่นิติกรผูู้รับผิดชอบให้ครบถ้วน";
        focus = "#lawyer-fullname";
    }

    if (error == false &&
        $("#lawyer-phonenumber").val().length > 0 &&
        $("#lawyer-phonenumber").inputmask("isComplete") == false) {
        error = true;
        msg = "กรุณาใส่หมายเลขโทรศัพท์ของนิติกรผู้รับผิดชอบให้ถูกต้อง";
        focus = "#lawyer-phonenumber";
    }

    if (error == false &&
        $("#lawyer-mobilenumber").val().length > 0 &&
        $("#lawyer-mobilenumber").inputmask("isComplete") == false) {
        error = true;
        msg = "กรุณาใส่หมายโทรศัพท์มือถือของนิติกรผู้รับผิดชอบให้ถูกต้อง";
        focus = "#lawyer-mobilenumber";
    }

    if (error == false &&
        $("#lawyer-email").inputmask("isComplete") == false) {
        error = true;
        msg = "กรุณาใส่อีเมล์ของนิติกรผู้รับผิดชอบให้ถูกต้อง";
        focus = "#lawyer-email";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function ValidateCPTransRequireContract() { 
    var error = false;
    var msg;
    var focus;
    var scholar = $("#scholar-hidden").val();
    var caseGraduate = $("#case-graduate-break-contract-hidden").val();
    var civil = $("#civil-hidden").val();
    var setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();
    var indemnitorYear = $("#indemnitor-year").val();
    var indemnitorCash = $("#indemnitor-cash").val();
    var allActualMonthScholarship = (caseGraduate == "1" ? $("#all-actual-month-scholarship").val() : "");
  
    if (error == false &&
        setAmtIndemnitorYear == "Y" &&
        (indemnitorYear.length == 0 || indemnitorYear == "0")) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ต้องปฏิบัติงานชดใช้";
        focus = "#indemnitor-year";
    }

    if (error == false &&
        caseGraduate == "2" &&
        (indemnitorCash.length == 0 || indemnitorCash == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินต้องชดใช้ตามสัญญา";
        focus = "#indemnitor-cash";
    }

    if (error == false &&
        (indemnitorCash.length == 0 || indemnitorCash == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินต้องชดใช้ตามสัญญา";
        focus = "#indemnitor-cash";
    }

    if (error == false &&
        caseGraduate == "2" &&
        civil == "1" &&
        ($("#indemnitor-address").val().length == 0 || ComboboxGetSelectedValue("province") == "0" || $("input[name=study-leave-yesno]:checked").length == 0)) {
        error = true;
        msg = "กรุณาใส่รายละเอียดข้อมูลการทำงานชดใช้ให้ครบถ้วน";
        focus = "#indemnitor-address";
    }

    if (error == false &&
        caseGraduate == "2" &&
        civil == "1" &&
        $("input[name=study-leave-yesno]:checked").val() == "N" &&
        ($("#require-date").val().length == 0 || $("#approve-date").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีไม่มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ครบถ้วน";
        focus = "#require-date";
    }

    if (error == false &&
        caseGraduate == "2" &&
        civil == "1" &&
        $("input[name=study-leave-yesno]:checked").val() == "Y" &&
        ($("#before-study-leave-start-date").val().length == 0 || $("#before-study-leave-end-date").val().length == 0 || $("#study-leave-start-date").val().length == 0 || $("#study-leave-end-date").val().length == 0 || $("#after-study-leave-start-date").val().length == 0 || $("#after-study-leave-end-date").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีมีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ครบถ้วน";
        focus = "#before-study-leave-start-date";
    }

    if (error == false &&
        caseGraduate == "2" &&
        civil == "1" &&
        ($("#study-leave-start-date").datepicker("getDate") < $("#before-study-leave-end-date").datepicker("getDate") || $("#after-study-leave-start-date").datepicker("getDate") < $("#study-leave-end-date").datepicker("getDate"))) {
        error = true;
        msg = "กรุณาใส่ช่วงวันที่ทำงานชดใช้ กรณีมีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุนให้ถูกต้อง";
        focus = "#before-study-leave-start-date";
    }  
    
    if (error == false &&
        scholar == "1" &&
        caseGraduate == "1" &&
        (allActualMonthScholarship.length == 0 || allActualMonthScholarship == "0")) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ชดใช้ทุนการศึกษา";
        focus = "#all-actual-month-scholarship";
    }

    if (error == true) {
        FillCalPayScholarshipPenalty("");
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalculatePayScholarshipAndPenalty() {
    if (ValidateCPTransRequireContract() == true) {
        SetMsgLoading("กำลังคำนวณ...");

        var caseGraduate = $("#case-graduate-break-contract-hidden").val();
        var civil = $("#civil-hidden").val();    
        var dateStartID = "";
        var dateEndID = "";

        if (caseGraduate == "1") {
            dateStartID = "contract-force-date-start-hidden";
            dateEndID = "contract-force-date-end-hidden";
        }

        if (caseGraduate == "2" &&
            civil == "1") {
            dateStartID = "require-date";
            dateEndID = "approve-date";
        }
        
        if (caseGraduate == "2" &&
            civil == "2") {
            dateStartID = "education-date-start-hidden";
            dateEndID = "education-date-end-hidden";
        }
        
        var send = new Array();
        send[send.length] = ("scholar=" + $("#scholar-hidden").val());
        send[send.length] = ("scholarshipmoney=" + DelCommas("scholarship-money"));
        send[send.length] = ("scholarshipyear=" + ($("#scholarship-year").val().length > 0 ? DelCommas("scholarship-year") : "0"));
        send[send.length] = ("scholarshipmonth=" + ($("#scholarship-month").val().length > 0 ? DelCommas("scholarship-month") : "0"));
        send[send.length] = ("allactualmonthscholarship=" + (caseGraduate == "1" ? DelCommas("all-actual-month-scholarship") : ""));
        send[send.length] = ("casegraduate=" + caseGraduate);
        send[send.length] = ("educationdate=" + $("#education-date-start-hidden").val());
        send[send.length] = ("graduatedate=" + $("#education-date-end-hidden").val());
        send[send.length] = ("civil=" + civil);
        send[send.length] = ("datestart=" + (dateStartID.length > 0 ? $("#" + dateStartID).val() : ""));
        send[send.length] = ("dateend=" + (dateEndID.length > 0 ? $("#" + dateEndID).val() : ""));
        send[send.length] = ("indemnitoryear=" + DelCommas("indemnitor-year"));
        send[send.length] = ("indemnitorcash=" + DelCommas("indemnitor-cash"));
        send[send.length] = ("caldatecondition=" + $("#cal-date-condition-hidden").val());
        send[send.length] = ("studyleave=" + $("input[name=study-leave-yesno]:checked").val());
        send[send.length] = ("beforestudyleavestartdate=" + $("#before-study-leave-start-date").val());
        send[send.length] = ("beforestudyleaveenddate=" + $("#before-study-leave-end-date").val());
        send[send.length] = ("studyleavestartdate=" + $("#study-leave-start-date").val());
        send[send.length] = ("studyleaveenddate=" + $("#study-leave-end-date").val());
        send[send.length] = ("afterstudyleavestartdate=" + $("#after-study-leave-start-date").val());
        send[send.length] = ("afterstudyleaveenddate=" + $("#after-study-leave-end-date").val());

        CalculateFrm("scholarshipandpenalty", send, function (result) {
            FillCalPayScholarshipPenalty(result);
        });
    }
}

function FillCalPayScholarshipPenalty(result) {
    var setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();

    if ($("#all-actual-month-scholarship").length > 0 &&
        $("#all-actual-month-scholarship").val() == "0")
        $("#all-actual-month-scholarship").val("");

    if ($("#all-actual-scholarship").length > 0)
        $("#all-actual-scholarship").val("");

    if ($("#total-pay-scholarship").length > 0)
        $("#total-pay-scholarship").val("");

    if ($("#all-actual-month").length > 0)
        $("#all-actual-month").val("");

    if ($("#all-actual-day").length > 0)
        $("#all-actual-day").val("");

    if ($("#all-actual-date").length > 0)
        $("#all-actual-date").val("");

    if ($("#actual-date").length > 0)
        $("#actual-date").val("");

    if ($("#remain-date").length > 0)
        $("#remain-date").val("");

    if ($("#subtotal-penalty").length > 0)
        $("#subtotal-penalty").val("");

    if ($("#total-penalty").length > 0)
        $("#total-penalty").val("");

    if (result.length > 0) {
        var dataActualScholarship = result.split("<allactualscholarship>");
        var dataTotalPayScholarship = result.split("<totalpayscholarship>");
        var dataMonth = result.split("<month>");
        var dataDay = result.split("<day>");
        var dataAllActual = result.split("<allactual>");
        var dataActual = result.split("<actual>");
        var dataRemain = result.split("<remain>");
        var dataSubtotal = result.split("<totalpenalty>");
        var dataTotal = result.split("<total>");

        if ($("#all-actual-month-scholarship").length > 0 &&
            $("#all-actual-month-scholarship").val().length == 0)
            $("#all-actual-month-scholarship").val("");

        if ($("#all-actual-scholarship").length > 0)
            $("#all-actual-scholarship").val(dataActualScholarship[1] == "0.00" ? "" : dataActualScholarship[1]);

        if ($("#total-pay-scholarship").length > 0)
            $("#total-pay-scholarship").val(dataTotalPayScholarship[1] == "0.00" ? "" : dataTotalPayScholarship[1]);

        if ($("#all-actual-month").length > 0)
            $("#all-actual-month").val(dataMonth[1]);

        if ($("#all-actual-day").length > 0)
            $("#all-actual-day").val(dataDay[1]);

        if ($("#case-graduate-break-contract-hidden").val() == "2") {
            if (setAmtIndemnitorYear == "Y") {
                if (dataAllActual[1] == "0" &&
                    dataActual[1] == "0" &&
                    dataRemain[1] == "0") {
                    if ($("#all-actual-date").length > 0)
                        $("#all-actual-date").val("");

                    if ($("#actual-date").length > 0)
                        $("#actual-date").val("");

                    if ($("#remain-date").length > 0)
                        $("#remain-date").val("");
                }
                else {
                    if (parseInt(dataRemain[1]) <= 0) {
                        DialogMessage("ระยะเวลาที่ปฏิบัติงานชดใช้แล้วต้องน้อยกว่าระยะเวลาที่ต้องปฏิบัติงานชดใช้", "", false, "");
                        return;
                    }

                    if ($("#all-actual-date").length > 0)
                        $("#all-actual-date").val(dataAllActual[1]);

                    if ($("#actual-date").length > 0)
                        $("#actual-date").val(dataActual[1]);

                    if ($("#remain-date").length > 0)
                        $("#remain-date").val(dataRemain[1]);
                }
            }

            if (setAmtIndemnitorYear == "N") {
                if ($("#all-actual-day").length > 0)
                    $("#all-actual-day").val(dataAllActual[1]);

                if ($("#actual-date").length > 0)
                    $("#actual-date").val(dataActual[1]);
            }
        }

        if ($("#subtotal-penalty").length > 0)
            $("#subtotal-penalty").val(dataSubtotal[1]);

        if ($("#total-penalty").length > 0)
            $("#total-penalty").val(dataTotal[1]);
    }
}

function ConfirmActionCPTransRequireContract(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
                
                if (ValidateCPTransRequireContract() == true &&
                    ValidateLawyer() == true) {
                    var caseGraduate = $("#case-graduate-break-contract-hidden").val();
                    var civil = $("#civil-hidden").val();
                    var setAmtIndemnitorYear = $("#set-amt-indemnitor-year").val();
                    var dateStartID = "";
                    var dateEndID = "";

                    if (caseGraduate == "1") {
                        dateStartID = "contract-force-date-start-hidden";
                        dateEndID = "contract-force-date-end-hidden";
                    }

                    if (caseGraduate == "2" &&
                        civil == "1") {
                        dateStartID = "require-date";
                        dateEndID = "approve-date";
                    }

                    if (caseGraduate == "2" &&
                        civil == "2") {
                        dateStartID = "education-date-start-hidden";
                        dateEndID = "education-date-end-hidden";
                    }

                    var send = new Array();
                    send[send.length] = ("scholar=" + $("#scholar-hidden").val());
                    send[send.length] = ("scholarshipmoney=" + DelCommas("scholarship-money"));
                    send[send.length] = ("scholarshipyear=" + ($("#scholarship-year").val().length > 0 ? DelCommas("scholarship-year") : "0"));
                    send[send.length] = ("scholarshipmonth=" + ($("#scholarship-month").val().length > 0 ? DelCommas("scholarship-month") : "0"));
                    send[send.length] = ("allactualmonthscholarship=" + (caseGraduate == "1" ? DelCommas("all-actual-month-scholarship") : ""));
                    send[send.length] = ("casegraduate=" + caseGraduate);
                    send[send.length] = ("educationdate=" + $("#education-date-start-hidden").val());
                    send[send.length] = ("graduatedate=" + $("#education-date-end-hidden").val());
                    send[send.length] = ("civil=" + civil);
                    send[send.length] = ("datestart=" + (dateStartID.length > 0 ? $("#" + dateStartID).val() : ""));
                    send[send.length] = ("dateend=" + (dateEndID.length > 0 ? $("#" + dateEndID).val() : ""));
                    send[send.length] = ("indemnitoryear=" + DelCommas("indemnitor-year"));
                    send[send.length] = ("indemnitorcash=" + DelCommas("indemnitor-cash"));
                    send[send.length] = ("caldatecondition=" + $("#cal-date-condition-hidden").val());
                    send[send.length] = ("studyleave=" + $("input[name=study-leave-yesno]:checked").val());
                    send[send.length] = ("beforestudyleavestartdate=" + $("#before-study-leave-start-date").val());
                    send[send.length] = ("beforestudyleaveenddate=" + $("#before-study-leave-end-date").val());
                    send[send.length] = ("studyleavestartdate=" + $("#study-leave-start-date").val());
                    send[send.length] = ("studyleaveenddate=" + $("#study-leave-end-date").val());
                    send[send.length] = ("afterstudyleavestartdate=" + $("#after-study-leave-start-date").val());
                    send[send.length] = ("afterstudyleaveenddate=" + $("#after-study-leave-end-date").val());

                    CalculateFrm("scholarshipandpenalty", send, function (result) {
                        FillCalPayScholarshipPenalty(result);
                        
                        var send1 = new Array();
                        send1[send1.length] = ("cp1id=" + $("#cp1id").val());
                        send1[send1.length] = ("scholar=" + $("#scholar-hidden").val());
                        send1[send1.length] = ("casegraduate=" + caseGraduate);
                        send1[send1.length] = ("civil=" + civil);
                        send1[send1.length] = ("indemnitoryear=" + DelCommas("indemnitor-year"));
                        send1[send1.length] = ("indemnitorcash=" + DelCommas("indemnitor-cash"));
                        send1[send1.length] = ("trackingstatus=" + $("#trackingstatus").val());
                        send1[send1.length] = ("cp2id=" + $("#cp2id").val());

                        if (caseGraduate == "1") {
                            send1[send1.length] = ("actualmonthscholarship=" + DelCommas("all-actual-month-scholarship"));
                            send1[send1.length] = ("actualscholarship=" + DelCommas("all-actual-scholarship"));
                            send1[send1.length] = ("totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0"));
                            send1[send1.length] = ("actualmonth=" + DelCommas("all-actual-month"));
                            send1[send1.length] = ("actualday=" + DelCommas("all-actual-day"));
                            send1[send1.length] = ("subtotalpenalty=" + DelCommas("subtotal-penalty"));
                            send1[send1.length] = ("totalpenalty=" + DelCommas("total-penalty"));
                        }

                        if (caseGraduate == "2") {
                            if (civil == "1") {
                                if (DelCommas("remain-date") > 0 ||
                                    setAmtIndemnitorYear == "N") {
                                    send1[send1.length] = ("indemnitoraddress=" + $("#indemnitor-address").val());
                                    send1[send1.length] = ("province=" + ComboboxGetSelectedValue("province"));
                                    send1[send1.length] = ("studyleave=" + $("input[name=study-leave-yesno]:checked").val());
                                    send1[send1.length] = ("requiredate=" + $("#require-date").val());
                                    send1[send1.length] = ("approvedate=" + $("#approve-date").val());
                                    send1[send1.length] = ("beforestudyleavestartdate=" + $("#before-study-leave-start-date").val());
                                    send1[send1.length] = ("beforestudyleaveenddate=" + $("#before-study-leave-end-date").val());
                                    send1[send1.length] = ("studyleavestartdate=" + $("#study-leave-start-date").val());
                                    send1[send1.length] = ("studyleaveenddate=" + $("#study-leave-end-date").val());
                                    send1[send1.length] = ("afterstudyleavestartdate=" + $("#after-study-leave-start-date").val());
                                    send1[send1.length] = ("afterstudyleaveenddate=" + $("#after-study-leave-end-date").val());
                                    send1[send1.length] = ("totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0"));

                                    if (setAmtIndemnitorYear == "Y") {
                                        send1[send1.length] = ("allactualdate=" + DelCommas("all-actual-date"));
                                        send1[send1.length] = ("actualdate=" + DelCommas("actual-date"));
                                        send1[send1.length] = ("remaindate=" + DelCommas("remain-date"));
                                    }

                                    if (setAmtIndemnitorYear == "N") {
                                        send1[send1.length] = ("actualday=" + DelCommas("all-actual-day"));
                                        send1[send1.length] = ("actualdate=" + DelCommas("actual-date"));
                                    }

                                    send1[send1.length] = ("subtotalpenalty=" + DelCommas("subtotal-penalty"));
                                    send1[send1.length] = ("totalpenalty=" + DelCommas("total-penalty"));
                                }
                                else {
                                    if (DelCommas("remain-date") <= 0) {
                                        DialogMessage("ปฏิบัติงานชดใช้ครบตามสัญญาแล้ว", "#require-date", false, "");
                                        return;
                                    }
                                }
                            }
                            else {
                                if (setAmtIndemnitorYear == "Y") {
                                    send1[send1.length] = ("allactualdate=" + DelCommas("all-actual-date"));
                                    send1[send1.length] = ("actualdate=" + DelCommas("actual-date"));
                                    send1[send1.length] = ("remaindate=" + DelCommas("remain-date"));
                                }

                                if (setAmtIndemnitorYear == "N") {
                                    send1[send1.length] = ("actualday=" + DelCommas("all-actual-day"));
                                }

                                send1[send1.length] = ("totalpayscholarship=" + ($("#total-pay-scholarship").val().length > 0 ? DelCommas("total-pay-scholarship") : "0"));
                                send1[send1.length] = ("subtotalpenalty=" + DelCommas("subtotal-penalty"));
                                send1[send1.length] = ("totalpenalty=" + DelCommas("total-penalty"));
                            }
                        }

                        send1[send1.length] = ("lawyerfullname=" + $("#lawyer-fullname").val());
                        send1[send1.length] = ("lawyerphonenumber=" + $("#lawyer-phonenumber").val());
                        send1[send1.length] = ("lawyermobilenumber=" + $("#lawyer-mobilenumber").val());
                        send1[send1.length] = ("lawyeremail=" + $("#lawyer-email").val());

                        ChkTrackingStatusViewTransBreakContract($("#cp1id").val(), $("#trackingstatus").val(), "", function (result) {
                            if (result == "0")
                                AddUpdateCPTransRequireContract(action, send1);
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

function AddUpdateCPTransRequireContract(
    action,
    send
) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");
    
    AddUpdateData(action, (action + "cptransrequirecontract"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }
    
        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
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

function ChkRepayStatusViewTransRequireContract(
    cp1id,
    callbackFunc
) {
    var send = new Array();
    send[send.length] = ("cp1id=" + cp1id);

    SetMsgLoading("");

    ViewData("repaystatustransrequirecontract", send, function (result) {
        var dataRepayStatus = result.split("<repaystatus>");

        if (dataRepayStatus[1].length <= 0) {
            DialogMessage("ไม่พบข้อมูล", "", false, "");
            callbackFunc(dataRepayStatus[1]);
            return;
        }

        callbackFunc(dataRepayStatus[1]);
    });
}

function ViewRepayStatusViewTransRequireContract(
    cp1id,
    cp2id,
    trackingStatus,
    action
) {
    ChkRepayStatusViewTransRequireContract(cp1id, function (result) {
        if (result == "0" &&
            action == "e")
            OpenTab("link-tab3-cp-trans-require-contract", "#tab3-cp-trans-require-contract", "ปรับปรุงรายการรับแจ้ง", false, "update", cp1id, trackingStatus);

        if (result == "1" &&
            action == "e")
            LoadForm(1, "detailcptransrequirecontract", true, "", cp1id, ("trans-break-contract" + cp1id));

        if (result == "2" &&
            action == "e")
                LoadForm(1, "detailcptransrequirecontract", true, "", cp1id, ("trans-break-contract" + cp1id));

        if (result == "0" &&
            action == "r")
            LoadForm(1, "repaycptransrequirecontract", true, "", cp1id, ("repay" + cp2id));

        if (result == "1" &&
            action == "r")
            LoadForm(1, "repaycptransrequirecontract", true, "", cp1id, ("repay" + cp2id));

        if (result == "2" &&
            action == "r")
            LoadForm(1, "repaycptransrequirecontract", true, "", cp1id, ("repay" + cp2id));
    });
}