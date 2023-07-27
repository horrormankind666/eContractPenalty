function ResetFrmAddProfileStudent() {
    var titleNameDefault = ($("#profile-student-titlename-hidden").val().length > 0 ? $("#profile-student-titlename-hidden").val() : "0");
    var facultyProfileStudentDefault = ($("#profile-student-faculty-hidden").val().length > 0 ? $("#profile-student-faculty-hidden").val() : "0");
    var programProfileStudentDefault = ($("#profile-student-program-hidden").val().length > 0 ? $("#profile-student-program-hidden").val() : "");
 
    $("#profile-student-id").val($("#profile-student-id-hidden").val());
    InitCombobox("titlename", "0", titleNameDefault, 130, 155);
    $("#profile-student-firstname").val($("#profile-student-firstname-tha-hidden").val());
    $("#profile-student-lastname").val($("#profile-student-lastname-tha-hidden").val());
    InitCombobox("facultyprofilestudent", "0", facultyProfileStudentDefault, 390, 415);
    $("#program-profile-student-hidden").val(programProfileStudentDefault);
}

function ViewStudentInAddProfileStudent(profileStudent) {
    var dataProfileStdent = profileStudent.split(";");

    $("#profile-student-id").val(dataProfileStdent[0]);
    ComboboxSetSelectedValue("titlename", (dataProfileStdent[1] + ";" + dataProfileStdent[2] + ";" + dataProfileStdent[3]));
    $("#profile-student-firstname").val(dataProfileStdent[4]);
    $("#profile-student-lastname").val(dataProfileStdent[5]);
    ComboboxSetSelectedValue("facultyprofilestudent", (dataProfileStdent[6] + ";" + dataProfileStdent[7].replace(/\s/g, " ")));
    LoadCombobox("programprofilestudent", ("dlevel=&faculty=" + dataProfileStdent[6]), "list-program-profile-student", (dataProfileStdent[8] + ";" + dataProfileStdent[9].replace(/\s/g, " ") + ";" + dataProfileStdent[10] + ";" + dataProfileStdent[11] + ";" + dataProfileStdent[12] + ";" + dataProfileStdent[13]), 390, 415);
}

function ChkStudentTransBreakContract(
    studentid,
    callbackFunc
) {
    var send = new Array();
    send[send.length] = ("studentid=" + studentid);

    SetMsgLoading("");

    ViewData("studenttransbreakcontract", send, function (result) {
        var dataError = result.split("<error>");

        if (dataError[1] == "1")
            DialogMessage("มีรายการของนักศึกษานี้อยู่ในระบบแล้ว", "", false, "");

        callbackFunc(dataError[1]);
    });
}

function FillStudentInTransBreakContract(studentid) {
    var send = new Array();
    send[send.length] = ("studentid=" + studentid);

    SetMsgLoading("");

    ViewData("profilestudent", send, function (result) {
        if (result.length > 0) {
            var dataList = (result.split("<list>"))[1].split(";");

            $("#profile-student-id-hidden").val(dataList[0]);
            $("#profile-student-titlename-hidden").val(dataList[1] + ";" + dataList[2] + ";" + dataList[3]);
            $("#profile-student-firstname-tha-hidden").val(dataList[6]);
            $("#profile-student-lastname-tha-hidden").val(dataList[7]);
            $("#profile-student-firstname-eng-hidden").val(dataList[4]);
            $("#profile-student-lastname-eng-hidden").val(dataList[5]);
            $("#profile-student-faculty-hidden").val(dataList[8] + ";" + dataList[9]);
            $("#profile-student-program-hidden").val(dataList[10] + ";" + dataList[11] + ";" + dataList[12] + ";" + dataList[13] + ";" + dataList[14] + ";" + dataList[15]);

            $("#picture-student").html(dataList[23].length > 0 ? ("<img src='" + dataList[23] + "' />") : "");
            $("#student-id").html(dataList[0] + " " + dataList[10].substring(0, 4) + " / " + dataList[10].substring(4, 5));
            $("#student-fullname-tha").html(dataList[3] + dataList[6] + " " + dataList[7]);
            $("#student-fullname-eng").html(dataList[2] + " " + dataList[4] + " " + dataList[5]);
            $("#student-dlevel").html(dataList[15]);
            $("#student-faculty").html(dataList[8] + " - " + dataList[9]);
            $("#student-program").html(dataList[10] + " - " + dataList[11] + (dataList[13] != "0" ? (" ( กลุ่ม " + dataList[13] + " )") : ""));
            $("#education-date-start").val(dataList[16]);
            $("#education-date-end").val(dataList[17]);            
            $("#contract-date").val(dataList[18]);
            $("#contract-date-agreement").val(dataList[19]);
            $("#guarantor").val((dataList[20] + dataList[21] + dataList[22]).length > 0 ? (dataList[20] + dataList[21] + " " + dataList[22]) : "");
            $("#contract-force-date-start").val("");
            $("#contract-force-date-end").val("");
        }
        else {
            var titleName = ComboboxGetSelectedValue("titlename").split(";");
            var faculty = ComboboxGetSelectedValue("facultyprofilestudent").split(";");
            var program = ComboboxGetSelectedValue("programprofilestudent").split(";");

            $("#profile-student-id-hidden").val($("#profile-student-id").val());
            $("#profile-student-titlename-hidden").val(titleName[0] + ";" + titleName[1] + ";" + titleName[2]);
            $("#profile-student-firstname-tha-hidden").val($("#profile-student-firstname").val());
            $("#profile-student-lastname-tha-hidden").val($("#profile-student-lastname").val());
            $("#profile-student-firstname-eng-hidden").val("");
            $("#profile-student-lastname-eng-hidden").val("");
            $("#profile-student-faculty-hidden").val(faculty[0] + ";" + faculty[1]);
            $("#profile-student-program-hidden").val(program[0] + ";" + program[1] + ";" + program[2] + ";" + program[3] + ";" + program[4] + ";" + program[5]);

            $("#picture-student").html("");
            $("#student-id").html($("#profile-student-id").val() + " " + program[0].substring(0, 4) + " / " + program[0].substring(4, 5));
            $("#student-fullname-tha").html(titleName[2] + $("#profile-student-firstname").val() + " " + $("#profile-student-lastname").val());
            $("#student-fullname-eng").html("-");
            $("#student-dlevel").html(program[5]);
            $("#student-faculty").html(faculty[0] + " - " + faculty[1]);
            $("#student-program").html(program[0] + " - " + program[1] + (program[3] != "0" ? (" ( กลุ่ม " + program[3] + " )") : ""));
            $("#education-date-start").val("");
            $("#education-date-end").val("");
            $("#contract-date").val("");
            $("#guarantor").val("");
            $("#contract-force-date-start").val("");
            $("#contract-force-date-end").val("");
        }

        CloseFrm(true, "");
        ViewScholarshipAndPayBreakContract($("#profile-student-faculty-hidden").val(), $("#profile-student-program-hidden").val(), ComboboxGetSelectedValue("scholar"), ComboboxGetSelectedValue("case-graduate-break-contract"));
    });
}

function ViewStudentInTransBreakContract() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        $("#profile-student-id").val().length == 0) {
        error = true;
        msg = "กรุณาใส่รหัสนักศึกษา";
        focus = "#profile-student-id";
    }

    if (error == false &&
        ComboboxGetSelectedValue("titlename") == "0") {
        error = true;
        msg = "กรุณาเลือกคำนำหน้าชื่อ";
        focus = ".titlename-combobox-input";
    }

    if (error == false &&
        $("#profile-student-firstname").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ชื่อ";
        focus = "#profile-student-firstname";
    }

    if (error == false &&
        $("#profile-student-lastname").val().length == 0) {
        error = true;
        msg = "กรุณาใส่นามสกุล";
        focus = "#profile-student-lastname";
    }

    if (error == false &&
        ComboboxGetSelectedValue("facultyprofilestudent") == "0") {
        error = true;
        msg = "กรุณาเลือกคณะ";
        focus = ".facultyprofilestudent-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("programprofilestudent") == "0") {
        error = true;
        msg = "กรุณาเลือกหลักสูตร";
        focus = ".programprofilestudent-combobox-input";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var studentid = $("#profile-student-id").val();

    if ($("#studentid-hidden").val() != studentid) {
        $("#add-profile-students .button .button-style1").hide();
        $("#add-profile-students .button .button-style2").show();

        ChkStudentTransBreakContract(studentid, function (result) {
            if (result == "0")
                FillStudentInTransBreakContract(studentid);
        });
    }
    else
        FillStudentInTransBreakContract(studentid);
}

function ResetFrmCPTransBreakContract(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        LinkDisable("#link-add-student");
        TextboxDisable("#pursuant-book");
        TextboxDisable("#pursuant");
        CalendarDisable("#pursuant-book-date");
        TextboxDisable("#location");
        CalendarDisable("#input-date");
        TextboxDisable("#state-location");
        CalendarDisable("#state-location-date");
        CalendarDisable("#contract-date");
        CalendarDisable("#contract-date-agreement");
        TextboxDisable("#guarantor");
        ComboboxDisable("scholar");
        TextboxDisable("#scholarship-money");
        TextboxDisable("#scholarship-year");
        TextboxDisable("#scholarship-month");
        CalendarDisable("#education-date-start");
        CalendarDisable("#education-date-end");
        ComboboxDisable("case-graduate-break-contract");
        ComboboxDisable("civil");
        CalendarDisable("#contract-force-date-start");
        CalendarDisable("#contract-force-date-end");
        TextboxDisable("#indemnitor-year");
        TextboxDisable("#indemnitor-cash");
        TextboxDisable("#comment");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }
    
    $("#profile-student-id-hidden").val($("#studentid-hidden").val());
    $("#profile-student-titlename-hidden").val($("#titlename-hidden").val());
    $("#profile-student-firstname-tha-hidden").val($("#firstname-tha-hidden").val());
    $("#profile-student-lastname-tha-hidden").val($("#lastname-tha-hidden").val());
    $("#profile-student-firstname-eng-hidden").val($("#firstname-eng-hidden").val());
    $("#profile-student-lastname-eng-hidden").val($("#lastname-eng-hidden").val());
    $("#profile-student-faculty-hidden").val($("#faculty-hidden").val());
    $("#profile-student-program-hidden").val($("#program-hidden").val());
    $("#profile-student-dlevel-hidden").val($("#dlevel-hidden").val());

    $("#picture-student").html($("#picture-student-hidden").val().length > 0 ? ("<img src='" + $("#picture-student-hidden").val() + "' />") : $("#picture-student-hidden").val());
    $("#student-id").html($("#student-fullid-hidden").val());
    $("#student-fullname-tha").html($("#student-fullname-tha-hidden").val())
    $("#student-fullname-eng").html($("#student-fullname-eng-hidden").val())
    $("#student-dlevel").html($("#student-dlevel-hidden").val())
    $("#student-faculty").html($("#student-faculty-hidden").val())
    $("#student-program").html($("#student-program-hidden").val())
    $("#pursuant-book").val($("#pursuant-book-hidden").val());
    $("#pursuant").val($("#pursuant-hidden").val());
    $("#pursuant-book-date").val($("#pursuant-book-date-hidden").val());
    $("#location").val($("#location-hidden").val());
    $("#input-date").val($("#input-date-hidden").val());
    $("#state-location").val($("#state-location-hidden").val());
    $("#state-location-date").val($("#state-location-date-hidden").val());
    $("#contract-date").val($("#contract-date-hidden").val());
    $("#contract-date-agreement").val($("#contract-date-agreement-hidden").val());
    InitCalendar("#pursuant-book-date, #input-date, #state-location-date, #contract-date, #contract-date-agreement");
    /*
    InitCalendarFromTo("#contract-date", false, "#contract-date-agreement", false);
    */
    $("#guarantor").val($("#guarantor-hidden").val());
    InitCombobox("scholar", "0", $("#scholar-hidden").val(), 219, 244);
    $("#scholarship-money").val($("#scholarship-money-hidden").val());
    $("#scholarship-year").val($("#scholarship-year-hidden").val());
    $("#scholarship-month").val($("#scholarship-month-hidden").val());
    InitCombobox("case-graduate-break-contract", "0", $("#case-graduate-break-contract-hidden").val(), 219, 244);
    $("#education-date-start").val($("#education-date-start-hidden").val());
    $("#education-date-end").val($("#education-date-end-hidden").val());
    InitCalendarFromTo("#education-date-start", false, "#education-date-end", false);
    InitCombobox("civil", "0", $("#civil-hidden").val(), 219, 244);
    $("#contract-force-date-start").val($("#contract-force-date-start-hidden").val());
    $("#contract-force-date-end").val($("#contract-force-date-end-hidden").val());
    InitCalendar("#contract-force-date-start, #contract-force-date-end");    
    $("#cal-date-condition").val($("#cal-date-condition-hidden").val());
    $("#indemnitor-year").val($("#indemnitor-year-hidden").val());
    $("#indemnitor-cash").val($("#indemnitor-cash-hidden").val());
    TextboxDisable("#indemnitor-year");
    TextboxDisable("#indemnitor-cash");
    $("#comment-message").text($("#comment-hidden").val()).html(function (index, old) {
        return old.replace(/\n/g, '<br />');
    });
    TextboxDisable("#comments");
    $("#button-style11").show();
    $("#button-style12").hide();

    $(".calendar").change(function () {
        if ($(this).attr("id") == "education-date-start") {
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1")
                $("#contract-force-date-start").val($("#education-date-start").val());
        }

        if ($(this).attr("id") == "education-date-end") {
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1")
                $("#contract-force-date-end").val($("#education-date-end").val());

            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "2") {
                var contractForceDateStart = $("#education-date-end").datepicker("getDate", "+1d");
                contractForceDateStart.setDate(contractForceDateStart.getDate() + 1);

                $("#contract-force-date-start").datepicker("setDate", contractForceDateStart);
            }
        }
    });        

    /*
    $("#comment").slimScroll({
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

function ResetFrmCommentBreakContract() {
    $("#comment-reject").val("");
}

function ViewScholarshipAndPayBreakContract(
    faculty,
    program,
    scholar,
    caseGraduateBreakContract
) {
    $("#scholarship-money").val("");
    $("#scholarship-year").val("");
    $("#scholarship-month").val("");
    $("#cal-date-condition").val("");
    $("#indemnitor-year").val("");
    $("#indemnitor-cash").val("");

    if (faculty.length > 0 &&
        program.length > 0) {
        faculty = faculty.split(";");
        program = program.split(";");

        var send = new Array();
        send[send.length] = ("scholar=" + scholar);
        send[send.length] = ("casegraduate=" + caseGraduateBreakContract);
        send[send.length] = ("dlevel=" + program[4]);
        send[send.length] = ("faculty=" + faculty[0]);
        send[send.length] = ("programcode=" + program[0]);
        send[send.length] = ("majorcode=" + program[2]);
        send[send.length] = ("groupnum=" + program[3]);

        SetMsgLoading("");

        ViewData("scholarshipandpaybreakcontract", send, function (result) {
            var dataList = (result.split("<list>"))[1].split(";");

            $("#scholarship-money").val(dataList[0] != "0" ? dataList[0] : "");
            $("#cal-date-condition").val(dataList[1]);
            $("#indemnitor-year").val(dataList[2] != "0" ? dataList[2] : "");
            $("#indemnitor-cash").val(dataList[3]);
        });
    }
}

function ViewScholarship(
    faculty,
    program,
    scholar
) {
    $("#scholarship-money").val("");
    $("#scholarship-year").val("");
    $("#scholarship-month").val("");

    if (scholar == "1" &&
        faculty.length > 0 &&
        program.length > 0) {
        faculty = faculty.split(";");
        program = program.split(";");

        var send = new Array();
        send[send.length] = ("dlevel=" + program[4]);
        send[send.length] = ("faculty=" + faculty[0]);
        send[send.length] = ("programcode=" + program[0]);
        send[send.length] = ("majorcode=" + program[2]);
        send[send.length] = ("groupnum=" + program[3]);

        SetMsgLoading("");

        ViewData("scholarship", send, function (result) {
            var dataError = result.split("<error>");
            var dataList = result.split("<list>");

            if (dataError[1] == "0")
                $("#scholarship-money").val(dataList[1] != "0" ? dataList[1] : "");
        });
    }
}

function ViewPayBreakContract(
    faculty,
    program,
    caseGraduateBreakContract
) {
    $("#cal-date-condition").val("");
    $("#indemnitor-year").val("");
    $("#indemnitor-cash").val("");

    if (caseGraduateBreakContract != "0" &&
        faculty.length > 0 &&
        program.length > 0) {
        faculty = faculty.split(";");
        program = program.split(";");

        var send = new Array();
        send[send.length] = ("casegraduate=" + caseGraduateBreakContract);
        send[send.length] = ("dlevel=" + program[4]);
        send[send.length] = ("faculty=" + faculty[0]);
        send[send.length] = ("programcode=" + program[0]);
        send[send.length] = ("majorcode=" + program[2]);
        send[send.length] = ("groupnum=" + program[3]);

        SetMsgLoading("");

        ViewData("paybreakcontract", send, function (result) {
            var dataError = result.split("<error>");
            var dataList = (result.split("<list>"))[1].split(";");

            if (dataError[1] == "0") {
                $("#cal-date-condition").val(dataList[0]);
                $("#set-amt-indemnitor-year").val(dataList[1] != "0" ? "Y" : "N");
                $("#indemnitor-year").val(dataList[1] != "0" ? dataList[1] : "");
                $("#indemnitor-cash").val(dataList[2]);
            }
        });
    }
}

function ConfirmActionCPTransBreakContract(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTransBreakContract(action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function AddUpdateCPTransBreakContract(
    action,
    send
) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");
    AddUpdateData(action, (action + "cptransbreakcontract"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({ 
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    OpenTab("link-tab1-cp-trans-break-contract", "#tab1-cp-trans-break-contract", "", true, "", "", "");
                }
            }
        });
    });
}

function ValidateCPTransBreakContract(action) {
    var error = false;
    var msg;
    var focus;
    
    if (error == false &&
        $("#profile-student-id-hidden").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ข้อมูลนักศึกษา";
        focus = "#add-student";
    }

    if (error == false &&
        (
            $("#pursuant-book").val().length == 0 ||
            $("#pursuant").val().length == 0 ||
            $("#pursuant-book-date").val().length == 0 ||
            $("#location").val().length == 0 ||
            $("#input-date").val().length == 0 ||
            $("#state-location").val().length == 0 ||
            $("#state-location-date").val().length == 0
        )) {
        error = true;
        msg = "กรุณาใส่รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้นให้ครบถ้วน";
        focus = "#pursuant-book";
    }

    if (error == false &&
        $("#contract-date").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่ของสัญญานักศึกษา";
        focus = "#contract-date";
    }

    if (error == false &&
        $("#contract-date-agreement").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่ของสัญญาค้ำประกัน";
        focus = "#contract-date-agreement";
    }

    if (error == false &&
        $("#guarantor").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ผู้ค้ำประกัน";
        focus = "#guarantor";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "0") {
        error = true;
        msg = "กรุณาเลือกสถานะการได้รับทุนการศึกษา";
        focus = ".scholar-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "1" &&
        ($("#scholarship-money").val().length == 0 || $("#scholarship-money").val() == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินทุนการศึกษา";
        focus = "#scholarship-money";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "1" &&
        ($("#scholarship-year").val().length == 0 && $("#scholarship-month").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน";
        focus = "#scholarship-year";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "1" &&
        ($("#scholarship-year").val() == "0" && $("#scholarship-month").val() == "0")) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน";
        focus = "#scholarship-year";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "1" &&
        ($("#scholarship-year").val() == "0" && $("#scholarship-month").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน";
        focus = "#scholarship-year";
    }

    if (error == false &&
        ComboboxGetSelectedValue("scholar") == "1" &&
        ($("#scholarship-year").val().length == 0 && $("#scholarship-month").val() == "0")) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน";
        focus = "#scholarship-year";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "0") {
        error = true;
        msg = "กรุณาเลือกสถานะการสำเร็จการศึกษา";
        focus = ".case-graduate-break-contract-combobox-input";
    }

    if (error == false &&
        ($("#education-date-start").val().length == 0 || $("#education-date-end").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่วันที่เริ่มต้นเข้าศึกษาและวันที่สำเร็จการศึกษา หรือวันที่พ้นสภาพนักศึกษาให้ครบถ้วน";
        focus = "#education-date-start";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "2" &&
        ComboboxGetSelectedValue("civil") == "0") {
        error = true;
        msg = "กรุณาเลือกสถานะการปฏิบัติงานชดใช้";
        focus = ".civil-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "1" &&
        ($("#contract-force-date-start").val().length == 0 || $("#contract-force-date-end").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่วันที่สัญญามีผลบังคับใช้ให้ครบถ้วน";
        focus = "#contract-force-date-start";
    }

    if (error == false &&
        DateDiff(GetDateObject($("#education-date-start").val()), GetDateObject($("#contract-force-date-start").val()), "days") < 0) {
        error = true;
        msg = "กรุณาใส่วันที่สัญญาเริ่มมีผลบังคับใช้ให้มากกว่าหรือเท่ากับวันที่เริ่มเข้าศึกษา";
        focus = "#contract-force-date-start";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "1" &&
        DateDiff(GetDateObject($("#education-date-end").val()), GetDateObject($("#contract-force-date-start").val()), "days") >= 0) {
        error = true;
        msg = "กรุณาใส่วันที่สัญญาเริ่มมีผลบังคับใช้ให้น้อยกว่าวันที่พ้นสภาพนักศึกษา";
        focus = "#contract-force-date-start";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "1" &&
        DateDiff(GetDateObject($("#education-date-start").val()), GetDateObject($("#contract-force-date-end").val()), "days") < 0) {
        error = true;
        msg = "กรุณาใส่วันที่สัญญาสิ้นสุดมีผลบังคับใช้ให้มากกว่าหรือเท่ากับวันที่เริ่มเข้าศึกษา";
        focus = "#contract-force-date-end";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate-break-contract") == "2" &&
        $("#contract-force-date-start").val().length == 0) {
        error = true;
        msg = "กรุณาใส่วันที่สัญญามีผลบังคับใช้ให้ครบถ้วน";
        focus = "#contract-force-date-start";
    }
    
    if (error == false &&
        $("#set-amt-indemnitor-year").val() == "Y" &&
        ($("#indemnitor-year").val().length == 0 || $("#indemnitor-cash").val().length == 0)) {
        error = true;
        msg = "กรุณาใส่เวลาที่ทำงานชดใช้และจำนวนเงินที่ชดใช้";
        focus = "#indemnitor-year";
    }

    if (error == false &&
        $("#indemnitor-cash").val().length == 0) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินที่ชดใช้";
        focus = "#indemnitor-cash";
    }
    
    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var titlename = $("#profile-student-titlename-hidden").val().split(";");
    var faculty = $("#profile-student-faculty-hidden").val().split(";");
    var program = $("#profile-student-program-hidden").val().split(";");           
    var send = new Array();
    send[send.length] = ("cp1id=" + $("#cp1id").val());
    send[send.length] = ("studentid=" + $("#profile-student-id-hidden").val());
    send[send.length] = ("titlecode=" + titlename[0]);
    send[send.length] = ("titlenameeng=" + titlename[1]);
    send[send.length] = ("titlenametha=" + titlename[2]);
    send[send.length] = ("firstnameeng=" + $("#profile-student-firstname-eng-hidden").val());
    send[send.length] = ("lastnameeng=" + $("#profile-student-lastname-eng-hidden").val());
    send[send.length] = ("firstnametha=" + $("#profile-student-firstname-tha-hidden").val());
    send[send.length] = ("lastnametha=" + $("#profile-student-lastname-tha-hidden").val());
    send[send.length] = ("facultycode=" + faculty[0]);
    send[send.length] = ("facultyname=" + faculty[1]);
    send[send.length] = ("programcode=" + program[0]);
    send[send.length] = ("programname=" + program[1]);
    send[send.length] = ("majorcode=" + program[2]);
    send[send.length] = ("groupnum=" + program[3]);
    send[send.length] = ("dlevel=" + program[4]);
    send[send.length] = ("pursuantbook=" + $("#pursuant-book").val());
    send[send.length] = ("pursuant=" + $("#pursuant").val());
    send[send.length] = ("pursuantbookdate=" + $("#pursuant-book-date").val());
    send[send.length] = ("location=" + $("#location").val());
    send[send.length] = ("inputdate=" + $("#input-date").val());
    send[send.length] = ("statelocation=" + $("#state-location").val());
    send[send.length] = ("statelocationdate=" + $("#state-location-date").val());
    send[send.length] = ("contractdate=" + $("#contract-date").val());
    send[send.length] = ("contractdateagreement=" + $("#contract-date-agreement").val());
    send[send.length] = ("guarantor=" + $("#guarantor").val());
    send[send.length] = ("scholarflag=" + ComboboxGetSelectedValue("scholar"));
    send[send.length] = ("scholarshipmoney=" + DelCommas("scholarship-money"));
    send[send.length] = ("scholarshipyear=" + DelCommas("scholarship-year"));
    send[send.length] = ("scholarshipmonth=" + DelCommas("scholarship-month"));
    send[send.length] = ("educationdate=" + $("#education-date-start").val());
    send[send.length] = ("graduatedate=" + $("#education-date-end").val());
    send[send.length] = ("casegraduate=" + ComboboxGetSelectedValue("case-graduate-break-contract"));
    send[send.length] = ("civilflag=" + ComboboxGetSelectedValue("civil"));
    send[send.length] = ("caldatecondition=" + $("#cal-date-condition").val());
    send[send.length] = ("indemnitoryear=" + DelCommas("indemnitor-year"));
    send[send.length] = ("indemnitorcash=" + DelCommas("indemnitor-cash"));
    send[send.length] = ("trackingstatus=" + $("#trackingstatus").val());
    send[send.length] = ("contractforcedatestart=" + $("#contract-force-date-start").val());
    send[send.length] = ("contractforcedateend=" + $("#contract-force-date-end").val());
  
    if (action == "add") {
        ChkStudentTransBreakContract($("#profile-student-id-hidden").val(), function (result) {
            if (result == "0")
                AddUpdateCPTransBreakContract(action, send);
        });
    }

    if (action == "update") {
        ChkTrackingStatusViewTransBreakContract($("#cp1id").val(), $("#trackingstatus").val(), "", function (result) {
            if (result == "0")
                AddUpdateCPTransBreakContract(action, send);
        });
    } 
}

function ChkTrackingStatusViewTransBreakContract(
    cp1id,
    trackingStatus,
    idActive,
    callbackFunc
) {
    if (idActive.length > 0)
        $("#" + idActive).addClass("active");
    
    var send = new Array();
    send[send.length] = ("cp1id=" + cp1id);

    SetMsgLoading("");

    ViewData("trackingstatustransbreakcontract", send, function (result) {
        var dataTrackingStatus = result.split("<trackingstatus>");

        if (dataTrackingStatus[1].length <= 0) {            
            DialogMessage("ไม่พบข้อมูล", "", false, idActive);
            callbackFunc(1);
            return;
        }

        if (trackingStatus != dataTrackingStatus[1]) {
            DialogMessage("สถานะรายการแจ้งนี้มีการเปลี่ยนแปลง", "", false, idActive);
            callbackFunc(2);
            return;
        }

        callbackFunc(0);
    });
}

function ViewTrackingStatusViewTransBreakContract(
    cp1id,
    trackingStatus,
    action
) {
    ChkTrackingStatusViewTransBreakContract(cp1id, trackingStatus, ("trans-break-contract" + cp1id), function (result) {
        if (result == "0") {
            var frmIndex = ($("#dialog-form1").is(":visible") == false ? 1 : 2);

            if (action == "v1")
                LoadForm(frmIndex, "detailcptransbreakcontract", true, "", cp1id, ("trans-break-contract" + cp1id));

            if (action == "e1")
                OpenTab("link-tab3-cp-trans-break-contract", "#tab3-cp-trans-break-contract", "ปรับปรุงรายการแจ้ง", false, "update", cp1id, trackingStatus);

            if (action == "e2")
                ViewRepayStatusViewTransRequireContract(cp1id, "", trackingStatus, "e");

            if (action == "v2")
                LoadForm(frmIndex, "detailcptransrequirecontract", true, "", cp1id, ("trans-break-contract" + cp1id));

            if (action == "a1")
                LoadForm(frmIndex, "receivercptransbreakcontract", true, "", cp1id, ("trans-break-contract" + cp1id));

            if (action == "v3")
                LoadForm(frmIndex, "detailcptransrequirerepaycontract", true, "", cp1id, ("trans-break-contract" + cp1id));
        }
    });
}

function ValidateCommentBreakContract(
    cp1id,
    action,
    from
) {
    var error = false;
    var msg;
    var focus;
    var comment = $("#comment-reject").val();

    if (error == false &&
        action == "E" &&
        comment.length <= 0) {
        error = true;
        msg = "กรุณาใส่สาเหตุการส่งรายการแจ้งกลับไปแก้ไข";
        focus = "#comment-reject";
    }

    if (error == false &&
        action == "C" &&
        comment.length <= 0) {
        error = true;
        msg = "กรุณาใส่สาเหตุการยกเลิกรายการ";
        focus = "#comment-reject";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var send = new Array();
    send[send.length] = ("cp1id=" + cp1id);
    send[send.length] = ("actioncomment=" + action);
    send[send.length] = ("comment=" + comment);

    AddUpdateData("update", "rejectcptransbreakcontract", send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "E") {
                        $("#dialog-form2").dialog("close");
                        $("#dialog-form1").dialog("close");
                        OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                    }

                    if (action == "C") {
                        switch (from) {
                            case "breakcontract":
                                CloseFrm(true, "");
                                OpenTab("link-tab1-cp-trans-break-contract", "#tab1-cp-trans-break-contract", "", true, "", "", "");
                                break;
                            case "requirecontract":
                                CloseFrm(true, "");
                                OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                                break;
                            case "repaycontract":
                                CloseFrm(true, "");
                                CloseFrm(true, "");
                                OpenTab("link-tab2-cp-trans-require-contract", "#tab2-cp-trans-require-contract", "", true, "", "", "");
                                break;
                        }
                    }
                }
            }
        });
    });
}

function ConfirmAddCommentBreakContract(
    cp1id,
    action,
    from
) {
    var msgAction = (action == "E" ? "ส่งกลับแก้ไขรายการ" : "ยกเลิกรายการ");

    DialogConfirm("ต้องการ" + msgAction + "นี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCommentBreakContract(cp1id, action, from);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ConfirmUpdateTrackingStatus(
    cp1id,
    status,
    from
) {
    var statusMsg = "";

    statusMsg = (status == "edit" ? "ส่งกลับแก้ไข" : statusMsg);
    statusMsg = (status == "cancel" ? "ยกเลิก" : statusMsg);

    DialogConfirm("ต้องการ" + statusMsg + "รายการนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                UpdateTrackingStatus(cp1id, status, from);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function UpdateTrackingStatus(
    cp1id,
    status,
    from
) {
    var send = new Array();
    send[send.length] = ("cp1id=" + cp1id);
    send[send.length] = ("status=" + status);

    ChkTrackingStatusViewTransBreakContract(cp1id, $("#trackingstatus").val(), "", function (result) {
        if (result == "0") {
            AddUpdateData("update", "updatetrackingstatusbreakcontract", send, false, "", "", "", true, function (result) {
                if (result == "1") {
                    GotoSignin();
                    return;
                }

                if (status == "edit")
                    OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");

                if (status == "cancel") {
                    switch (from) {
                        case "breakcontract":
                            ResetFrmCPTransBreakContract(true);
                            break;
                        case "requirecontract":
                            ResetFrmCPTransRequireContract(true);
                            break;
                        case "repaycontract":
                            CloseFrm(true, "");
                            OpenTab("link-tab2-cp-trans-require-contract", "#tab2-cp-trans-require-contract", "", true, "", "", "");
                            break;
                    }
                }
            });
        }
    });
}

function ConfirmUpdateTrackingStatusBreakContract(
    cp1id,
    status
) {
    ConfirmUpdateTrackingStatus(cp1id, status, "breakcontract");
}

function ConfirmUpdateTrackingStatusRequireContract(
    cp1id,
    status
) {
    ConfirmUpdateTrackingStatus(cp1id, status, "requirecontract");
}

function ConfirmUpdateTrackingStatusRepayContract(
    cp1id,
    status
) {
    ConfirmUpdateTrackingStatus(cp1id, status, "repaycontract");
}

function ConfirmSendBreakContract() {
    DialogConfirm("ต้องการส่งรายการแจ้งนักศึกษาผิดสัญญาหรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                SendBreakContract();
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function SendBreakContract() {
    elem = $("input[name=send-break-contract]:checked");
    countSend = elem.length;

    if (countSend == 0) {
        DialogMessage("ไม่พบรายการแจ้งที่ต้องการส่ง", "", false, "");
        return;
    }

    var valCheck = new Array();

    elem.each(function(i) {
        valCheck[i] = this.value;
    });

    var send = new Array();
    send[send.length] = ("cp1id=" + valCheck.join(";"));

    AddUpdateData("update", "sendbreakcontract", send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    SetMsgLoading("");
                    SearchCPTransBreakContract();
                }
            }
        });
    });
}