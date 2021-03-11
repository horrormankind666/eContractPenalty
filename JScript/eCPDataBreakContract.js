function ResetFrmAddProfileStudent()
{
    var _titleNameDefault = $("#profile-student-titlename-hidden").val().length > 0 ? $("#profile-student-titlename-hidden").val() : "0";
    var _facultyProfileStudentDefault = $("#profile-student-faculty-hidden").val().length > 0 ? $("#profile-student-faculty-hidden").val() : "0";
    var _programProfileStudentDefault = $("#profile-student-program-hidden").val().length > 0 ? $("#profile-student-program-hidden").val() : "";
 
    $("#profile-student-id").val($("#profile-student-id-hidden").val());
    InitCombobox("titlename", "0", _titleNameDefault, 130, 155);
    $("#profile-student-firstname").val($("#profile-student-firstname-tha-hidden").val());
    $("#profile-student-lastname").val($("#profile-student-lastname-tha-hidden").val());
    InitCombobox("facultyprofilestudent", "0", _facultyProfileStudentDefault, 390, 415);
    $("#program-profile-student-hidden").val(_programProfileStudentDefault);
}

function ViewStudentInAddProfileStudent(_profileStudent)
{
    var _dataProfileStdent = _profileStudent.split(";");

    $("#profile-student-id").val(_dataProfileStdent[0]);
    ComboboxSetSelectedValue("titlename", _dataProfileStdent[1] + ";" + _dataProfileStdent[2] + ";" + _dataProfileStdent[3]);
    $("#profile-student-firstname").val(_dataProfileStdent[4]);
    $("#profile-student-lastname").val(_dataProfileStdent[5]);
    ComboboxSetSelectedValue("facultyprofilestudent", _dataProfileStdent[6] + ";" + _dataProfileStdent[7].replace(/\s/g, " "));
    LoadCombobox("programprofilestudent", "dlevel=&faculty=" + _dataProfileStdent[6], "list-program-profile-student", (_dataProfileStdent[8] + ";" + _dataProfileStdent[9].replace(/\s/g, " ") + ";" + _dataProfileStdent[10] + ";" + _dataProfileStdent[11] + ";" + _dataProfileStdent[12] + ";" + _dataProfileStdent[13]), 390, 415);
}

function ChkStudentTransBreakContract(_studentid, _callbackFunc)
{
    var _send = new Array();
    _send[0] = "studentid=" + _studentid;

    SetMsgLoading("");

    ViewData("studenttransbreakcontract", _send, function (_result) {
        var _dataError = _result.split("<error>");

        if (_dataError[1] == "1")
            DialogMessage("มีรายการของนักศึกษานี้อยู่ในระบบแล้ว", "", false, "");

        _callbackFunc(_dataError[1]);
    });
}

function FillStudentInTransBreakContract(_studentid)
{
    var _send = new Array();
    _send[0] = "studentid=" + _studentid;

    SetMsgLoading("");

    ViewData("profilestudent", _send, function (_result) {
        if (_result.length > 0)
        {
            var _dataList = (_result.split("<list>"))[1].split(";");

            $("#profile-student-id-hidden").val(_dataList[0]);
            $("#profile-student-titlename-hidden").val(_dataList[1] + ";" + _dataList[2] + ";" + _dataList[3]);
            $("#profile-student-firstname-tha-hidden").val(_dataList[6]);
            $("#profile-student-lastname-tha-hidden").val(_dataList[7]);
            $("#profile-student-firstname-eng-hidden").val(_dataList[4]);
            $("#profile-student-lastname-eng-hidden").val(_dataList[5]);
            $("#profile-student-faculty-hidden").val(_dataList[8] + ";" + _dataList[9]);
            $("#profile-student-program-hidden").val(_dataList[10] + ";" + _dataList[11] + ";" + _dataList[12] + ";" + _dataList[13] + ";" + _dataList[14] + ";" + _dataList[15]);

            $("#picture-student").html(_dataList[23].length > 0 ? "<img src='" + _dataList[23] + "' />" : "");
            $("#student-id").html(_dataList[0] + " " + _dataList[10].substring(0, 4) + " / " + _dataList[10].substring(4, 5));
            $("#student-fullname-tha").html(_dataList[3] + _dataList[6] + " " + _dataList[7]);
            $("#student-fullname-eng").html(_dataList[2] + " " + _dataList[4] + " " + _dataList[5]);
            $("#student-dlevel").html(_dataList[15]);
            $("#student-faculty").html(_dataList[8] + " - " + _dataList[9]);
            $("#student-program").html(_dataList[10] + " - " + _dataList[11] + (_dataList[13] != "0" ? " ( กลุ่ม " + _dataList[13] + " )" : ""));
            $("#education-date-start").val(_dataList[16]);
            $("#education-date-end").val(_dataList[17]);            
            $("#contract-date").val(_dataList[18]);
            $("#contract-date-agreement").val(_dataList[19]);
            $("#guarantor").val((_dataList[20] + _dataList[21] + _dataList[22]).length > 0 ? (_dataList[20] + _dataList[21] + " " + _dataList[22]) : "");
            $("#contract-force-date-start").val("");
            $("#contract-force-date-end").val("");
        }
        else
            {
                var _titleName = ComboboxGetSelectedValue("titlename").split(";");
                var _faculty = ComboboxGetSelectedValue("facultyprofilestudent").split(";");
                var _program = ComboboxGetSelectedValue("programprofilestudent").split(";");

                $("#profile-student-id-hidden").val($("#profile-student-id").val());
                $("#profile-student-titlename-hidden").val(_titleName[0] + ";" + _titleName[1] + ";" + _titleName[2]);
                $("#profile-student-firstname-tha-hidden").val($("#profile-student-firstname").val());
                $("#profile-student-lastname-tha-hidden").val($("#profile-student-lastname").val());
                $("#profile-student-firstname-eng-hidden").val("");
                $("#profile-student-lastname-eng-hidden").val("");
                $("#profile-student-faculty-hidden").val(_faculty[0] + ";" + _faculty[1]);
                $("#profile-student-program-hidden").val(_program[0] + ";" + _program[1] + ";" + _program[2] + ";" + _program[3] + ";" + _program[4] + ";" + _program[5]);

                $("#picture-student").html("");
                $("#student-id").html($("#profile-student-id").val() + " " + _program[0].substring(0, 4) + " / " + _program[0].substring(4, 5));
                $("#student-fullname-tha").html(_titleName[2] + $("#profile-student-firstname").val() + " " + $("#profile-student-lastname").val());
                $("#student-fullname-eng").html("-");
                $("#student-dlevel").html(_program[5]);
                $("#student-faculty").html(_faculty[0] + " - " + _faculty[1]);
                $("#student-program").html(_program[0] + " - " + _program[1] + (_program[3] != "0" ? " ( กลุ่ม " + _program[3] + " )" : ""));
                $("#education-date-start").val("");
                $("#education-date-end").val("");
                $("#contract-date").val("");
                $("#guarantor").val("");
                $("#contract-force-date-start").val("");
                $("#contract-force-date-end").val("");
            }

        CloseFrm(true, "");
        ViewScholarshipAndPayBreakContract($("#profile-student-faculty-hidden").val(), $("#profile-student-program-hidden").val(), ComboboxGetSelectedValue("scholar"), ComboboxGetSelectedValue("case-graduate-break-contract"))
    });
}

function ViewStudentInTransBreakContract()
{
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ($("#profile-student-id").val().length == 0)) { _error = true; _msg = "กรุณาใส่รหัสนักศึกษา"; _focus = "#profile-student-id"; }
    if (_error == false && ComboboxGetSelectedValue("titlename") == "0") { _error = true; _msg = "กรุณาเลือกคำนำหน้าชื่อ"; _focus = ".titlename-combobox-input" }
    if (_error == false && ($("#profile-student-firstname").val().length == 0)) { _error = true; _msg = "กรุณาใส่ชื่อ"; _focus = "#profile-student-firstname"; }
    if (_error == false && ($("#profile-student-lastname").val().length == 0)) { _error = true; _msg = "กรุณาใส่นามสกุล"; _focus = "#profile-student-lastname"; }
    if (_error == false && ComboboxGetSelectedValue("facultyprofilestudent") == "0") { _error = true; _msg = "กรุณาเลือกคณะ"; _focus = ".facultyprofilestudent-combobox-input" }
    if (_error == false && ComboboxGetSelectedValue("programprofilestudent") == "0") { _error = true; _msg = "กรุณาเลือกหลักสูตร"; _focus = ".programprofilestudent-combobox-input" }

    if (_error == true)
    {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _studentid = $("#profile-student-id").val();

    if ($("#studentid-hidden").val() != _studentid)
    {
        ChkStudentTransBreakContract(_studentid, function (_result) {
            if (_result == "0")
            {
                FillStudentInTransBreakContract(_studentid)
            }
        });
    }
    else
        FillStudentInTransBreakContract(_studentid)
}

function ResetFrmCPTransBreakContract(_disable)
{
    GoToElement("top-page");

    if (_disable == true)
    {
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

    $("#picture-student").html($("#picture-student-hidden").val().length > 0 ? "<img src='" + $("#picture-student-hidden").val() + "' />" : $("#picture-student-hidden").val());
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
    //InitCalendarFromTo("#contract-date", false, "#contract-date-agreement", false);
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
    $("#comment-message").text($("#comment-hidden").val()).html(function (index, old) { return old.replace(/\n/g, '<br />') });
    TextboxDisable("#comments");
    $("#button-style11").show();
    $("#button-style12").hide();

    $(".calendar").change(function () {
        if ($(this).attr("id") == "education-date-start")
        {
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1") $("#contract-force-date-start").val($("#education-date-start").val());
        }

        if ($(this).attr("id") == "education-date-end")
        {
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1") $("#contract-force-date-end").val($("#education-date-end").val());
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "2")
            {
                var _contractForceDateStart = $("#education-date-end").datepicker("getDate", "+1d");
                _contractForceDateStart.setDate(_contractForceDateStart.getDate() + 1);

                $("#contract-force-date-start").datepicker("setDate", _contractForceDateStart);
            }
        }

    });        

    /*$("#comment").slimScroll({
        height: "90px",
        alwaysVisible: true,
        wheelStep: 10,
        size: "10px",
        color: "#FFCC00",
        distance: "0px",
        railVisible: true,
        railColor: "#222",
        railOpacity: 0.3
    });*/
}

function ResetFrmCommentBreakContract()
{
    $("#comment-reject").val("");
}

function ViewScholarshipAndPayBreakContract(_faculty, _program, _scholar, _caseGraduateBreakContract)
{
    $("#scholarship-money").val("");
    $("#scholarship-year").val("");
    $("#scholarship-month").val("");
    $("#cal-date-condition").val("");
    $("#indemnitor-year").val("");
    $("#indemnitor-cash").val("");

    if ((_faculty.length > 0) && (_program.length > 0))
    {
        _faculty = _faculty.split(";");
        _program = _program.split(";");
        var _send = new Array();
        _send[0] = "scholar=" + _scholar;
        _send[1] = "casegraduate=" + _caseGraduateBreakContract;
        _send[2] = "dlevel=" + _program[4];
        _send[3] = "faculty=" + _faculty[0];
        _send[4] = "programcode=" + _program[0];
        _send[5] = "majorcode=" + _program[2];
        _send[6] = "groupnum=" + _program[3];

        SetMsgLoading("");

        ViewData("scholarshipandpaybreakcontract", _send, function (_result) {
            var _dataList = (_result.split("<list>"))[1].split(";");

            $("#scholarship-money").val((_dataList[0] != "0") ? _dataList[0] : "");
            $("#cal-date-condition").val(_dataList[1]);
            $("#indemnitor-year").val((_dataList[2] != "0") ? _dataList[2] : "");
            $("#indemnitor-cash").val(_dataList[3]);
        });
    }
}

function ViewScholarship(_faculty, _program, _scholar)
{
    $("#scholarship-money").val("");
    $("#scholarship-year").val("");
    $("#scholarship-month").val("");

    if ((_scholar == "1") && (_faculty.length > 0) && (_program.length > 0))
    {
        _faculty = _faculty.split(";");
        _program = _program.split(";");
        var _send = new Array();
        _send[0] = "dlevel=" + _program[4];
        _send[1] = "faculty=" + _faculty[0];
        _send[2] = "programcode=" + _program[0];
        _send[3] = "majorcode=" + _program[2];
        _send[4] = "groupnum=" + _program[3];

        SetMsgLoading("");

        ViewData("scholarship", _send, function (_result) {
            var _dataError = _result.split("<error>");
            var _dataList = _result.split("<list>");

            if (_dataError[1] == "0")
                $("#scholarship-money").val((_dataList[1] != "0") ? _dataList[1] : "");
        });
    }
}

function ViewPayBreakContract(_faculty, _program, _caseGraduateBreakContract)
{    
    $("#cal-date-condition").val("");
    $("#indemnitor-year").val("");
    $("#indemnitor-cash").val("");

    if ((_caseGraduateBreakContract != "0") && (_faculty.length > 0) && (_program.length > 0))
    {
        _faculty = _faculty.split(";");
        _program = _program.split(";");
        var _send = new Array();
        _send[0] = "casegraduate=" + _caseGraduateBreakContract;
        _send[1] = "dlevel=" + _program[4];
        _send[2] = "faculty=" + _faculty[0];
        _send[3] = "programcode=" + _program[0];
        _send[4] = "majorcode=" + _program[2];
        _send[5] = "groupnum=" + _program[3];

        SetMsgLoading("");

        ViewData("paybreakcontract", _send, function (_result) {
            var _dataError = _result.split("<error>");
            var _dataList = (_result.split("<list>"))[1].split(";");

            if (_dataError[1] == "0")
            {
                $("#cal-date-condition").val(_dataList[0]);
                $("#indemnitor-year").val((_dataList[1] != "0") ? _dataList[1] : "");
                $("#indemnitor-cash").val(_dataList[2]);
            }
        });
    }
}

function ConfirmActionCPTransBreakContract(_action)
{
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTransBreakContract(_action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }

    });
}

function AddUpdateCPTransBreakContract(_action, _send)
{
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    AddUpdateData(_action, _action + "cptransbreakcontract", _send, false, "", "", "", false, function (_result) {
        if (_result == "1")
        {
            GotoSignin();
            return;
        }

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
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

function ValidateCPTransBreakContract(_action)
{
    var _error = false;
    var _msg;
    var _focus;
    
    if (_error == false && ($("#profile-student-id-hidden").val().length == 0)) { _error = true; _msg = "กรุณาใส่ข้อมูลนักศึกษา"; _focus = "#add-student"; }
    if (_error == false && (($("#pursuant-book").val().length == 0) || ($("#pursuant").val().length == 0) || ($("#pursuant-book-date").val().length == 0) || ($("#location").val().length == 0) || ($("#input-date").val().length == 0) || ($("#state-location").val().length == 0) || ($("#state-location-date").val().length == 0))) { _error = true; _msg = "กรุณาใส่รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้นให้ครบถ้วน"; _focus = "#pursuant-book"; }
    if (_error == false && ($("#contract-date").val().length == 0)) { _error = true; _msg = "กรุณาใส่วันที่ของสัญญานักศึกษา"; _focus = "#contract-date"; }
    if (_error == false && ($("#contract-date-agreement").val().length == 0)) { _error = true; _msg = "กรุณาใส่วันที่ของสัญญาค้ำประกัน"; _focus = "#contract-date-agreement"; }
    if (_error == false && ($("#guarantor").val().length == 0)) { _error = true; _msg = "กรุณาใส่ผู้ค้ำประกัน"; _focus = "#guarantor"; }
    if (_error == false && ComboboxGetSelectedValue("scholar") == "0") { _error = true; _msg = "กรุณาเลือกสถานะการได้รับทุนการศึกษา"; _focus = ".scholar-combobox-input" }
    if (_error == false && ((ComboboxGetSelectedValue("scholar") == "1") && (($("#scholarship-money").val().length == 0) || ($("#scholarship-money").val() == "0")))) { _error = true; _msg = "กรุณาใส่จำนวนเงินทุนการศึกษา"; _focus = "#scholarship-money" }
    if (_error == false && ((ComboboxGetSelectedValue("scholar") == "1") && (($("#scholarship-year").val().length == 0) && ($("#scholarship-month").val().length == 0)))) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน"; _focus = "#scholarship-year" }
    if (_error == false && ((ComboboxGetSelectedValue("scholar") == "1") && (($("#scholarship-year").val() == "0") && ($("#scholarship-month").val() == "0")))) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน"; _focus = "#scholarship-year" }
    if (_error == false && ((ComboboxGetSelectedValue("scholar") == "1") && (($("#scholarship-year").val() == "0") && ($("#scholarship-month").val().length == 0)))) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน"; _focus = "#scholarship-year" }
    if (_error == false && ((ComboboxGetSelectedValue("scholar") == "1") && (($("#scholarship-year").val().length == 0) && ($("#scholarship-month").val() == "0")))) { _error = true; _msg = "กรุณาใส่ระยะเวลาที่ได้รับทุน"; _focus = "#scholarship-year" }
    if (_error == false && ComboboxGetSelectedValue("case-graduate-break-contract") == "0") { _error = true; _msg = "กรุณาเลือกสถานะการสำเร็จการศึกษา"; _focus = ".case-graduate-break-contract-combobox-input"; }
    if (_error == false && (($("#education-date-start").val().length == 0) || ($("#education-date-end").val().length == 0))) { _error = true; _msg = "กรุณาใส่วันที่เริ่มต้นเข้าศึกษาและวันที่สำเร็จการศึกษา หรือวันที่พ้นสภาพนักศึกษาให้ครบถ้วน"; _focus = "#education-date-start"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "2") && (ComboboxGetSelectedValue("civil") == "0"))) { _error = true; _msg = "กรุณาเลือกสถานะการปฏิบัติงานชดใช้"; _focus = ".civil-combobox-input"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "1") && (($("#contract-force-date-start").val().length == 0) || ($("#contract-force-date-end").val().length == 0)))) { _error = true; _msg = "กรุณาใส่วันที่สัญญามีผลบังคับใช้ให้ครบถ้วน"; _focus = "#contract-force-date-start"; }
    if (_error == false && (DateDiff(GetDateObject($("#education-date-start").val()), GetDateObject($("#contract-force-date-start").val()), "days") < 0)) { _error = true; _msg = "กรุณาใส่วันที่สัญญาเริ่มมีผลบังคับใช้ให้มากกว่าหรือเท่ากับวันที่เริ่มเข้าศึกษา"; _focus = "#contract-force-date-start"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "1") && (DateDiff(GetDateObject($("#education-date-end").val()), GetDateObject($("#contract-force-date-start").val()), "days") >= 0))) { _error = true; _msg = "กรุณาใส่วันที่สัญญาเริ่มมีผลบังคับใช้ให้น้อยกว่าวันที่พ้นสภาพนักศึกษา"; _focus = "#contract-force-date-start"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "1") && (DateDiff(GetDateObject($("#education-date-start").val()), GetDateObject($("#contract-force-date-end").val()), "days") < 0))) { _error = true; _msg = "กรุณาใส่วันที่สัญญาสิ้นสุดมีผลบังคับใช้ให้มากกว่าหรือเท่ากับวันที่เริ่มเข้าศึกษา"; _focus = "#contract-force-date-end"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "2") && ($("#contract-force-date-start").val().length == 0))) { _error = true; _msg = "กรุณาใส่วันที่สัญญามีผลบังคับใช้ให้ครบถ้วน"; _focus = "#contract-force-date-start"; }    
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate-break-contract") == "2") && (($("#indemnitor-year").val().length == 0) || ($("#indemnitor-cash").val().length == 0)))) { _error = true; _msg = "กรุณาใส่เวลาที่ทำงานชดใช้และจำนวนเงินที่ชดใช้"; _focus = "#indemnitor-year"; }
    if (_error == false && ($("#indemnitor-cash").val().length == 0)) { _error = true; _msg = "กรุณาใส่จำนวนเงินที่ชดใช้"; _focus = "#indemnitor-cash"; }
    
    if (_error == true)
    {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _titlename = $("#profile-student-titlename-hidden").val().split(";");
    var _faculty = $("#profile-student-faculty-hidden").val().split(";");
    var _program = $("#profile-student-program-hidden").val().split(";");           
    var _send = new Array();
    _send[0] = "cp1id=" + $("#cp1id").val();
    _send[1] = "studentid=" + $("#profile-student-id-hidden").val();
    _send[2] = "titlecode=" + _titlename[0];
    _send[3] = "titlenameeng=" + _titlename[1];
    _send[4] = "titlenametha=" + _titlename[2];
    _send[5] = "firstnameeng=" + $("#profile-student-firstname-eng-hidden").val();
    _send[6] = "lastnameeng=" + $("#profile-student-lastname-eng-hidden").val();
    _send[7] = "firstnametha=" + $("#profile-student-firstname-tha-hidden").val();
    _send[8] = "lastnametha=" + $("#profile-student-lastname-tha-hidden").val();
    _send[9] = "facultycode=" + _faculty[0];
    _send[10] = "facultyname=" + _faculty[1];
    _send[11] = "programcode=" + _program[0];
    _send[12] = "programname=" + _program[1];
    _send[13] = "majorcode=" + _program[2];
    _send[14] = "groupnum=" + _program[3];
    _send[15] = "dlevel=" + _program[4];
    _send[16] = "pursuantbook=" + $("#pursuant-book").val();
    _send[17] = "pursuant=" + $("#pursuant").val();
    _send[18] = "pursuantbookdate=" + $("#pursuant-book-date").val();
    _send[19] = "location=" + $("#location").val();
    _send[20] = "inputdate=" + $("#input-date").val();
    _send[21] = "statelocation=" + $("#state-location").val();
    _send[22] = "statelocationdate=" + $("#state-location-date").val();
    _send[23] = "contractdate=" + $("#contract-date").val();
    _send[24] = "contractdateagreement=" + $("#contract-date-agreement").val();
    _send[25] = "guarantor=" + $("#guarantor").val();
    _send[26] = "scholarflag=" + ComboboxGetSelectedValue("scholar");
    _send[27] = "scholarshipmoney=" + DelCommas("scholarship-money");
    _send[28] = "scholarshipyear=" + DelCommas("scholarship-year");
    _send[29] = "scholarshipmonth=" + DelCommas("scholarship-month");
    _send[30] = "educationdate=" + $("#education-date-start").val();
    _send[31] = "graduatedate=" + $("#education-date-end").val();
    _send[32] = "casegraduate=" + ComboboxGetSelectedValue("case-graduate-break-contract");
    _send[33] = "civilflag=" + ComboboxGetSelectedValue("civil");
    _send[34] = "caldatecondition=" + $("#cal-date-condition").val();
    _send[35] = "indemnitoryear=" + DelCommas("indemnitor-year");
    _send[36] = "indemnitorcash=" + DelCommas("indemnitor-cash");
    _send[37] = "trackingstatus=" + $("#trackingstatus").val();
    _send[38] = "contractforcedatestart=" + $("#contract-force-date-start").val();
    _send[39] = "contractforcedateend=" + $("#contract-force-date-end").val();

    if (_action == "add")
    {
        ChkStudentTransBreakContract($("#profile-student-id-hidden").val(), function (_result) {
            if (_result == "0")
                AddUpdateCPTransBreakContract(_action, _send);
        });
    }

    if (_action == "update")
    {
        ChkTrackingStatusViewTransBreakContract($("#cp1id").val(), $("#trackingstatus").val(), "", function (_result) {
            if (_result == "0")
                AddUpdateCPTransBreakContract(_action, _send);
        });
    }
}

function ChkTrackingStatusViewTransBreakContract(_cp1id, _trackingStatus, _idActive, _callbackFunc)
{
    if (_idActive.length > 0) $("#" + _idActive).addClass("active");
    
    var _send = new Array();
    _send[0] = "cp1id=" + _cp1id;

    SetMsgLoading("");

    ViewData("trackingstatustransbreakcontract", _send, function (_result) {
        var _dataTrackingStatus = _result.split("<trackingstatus>");

        if (_dataTrackingStatus[1].length <= 0)
        {            
            DialogMessage("ไม่พบข้อมูล", "", false, _idActive);
            _callbackFunc(1);
            return;
        }

        if (_trackingStatus != _dataTrackingStatus[1])
        {
            DialogMessage("สถานะรายการแจ้งนี้มีการเปลี่ยนแปลง", "", false, _idActive);
            _callbackFunc(2);
            return;
        }

        _callbackFunc(0);
    });
}

function ViewTrackingStatusViewTransBreakContract(_cp1id, _trackingStatus, _action)
{
    ChkTrackingStatusViewTransBreakContract(_cp1id, _trackingStatus, "trans-break-contract" + _cp1id, function (_result) {
        if (_result == "0")
        {
            var _frmIndex = $("#dialog-form1").is(":visible") == false ? 1 : 2;

            if (_action == "v1") LoadForm(_frmIndex, "detailcptransbreakcontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
            if (_action == "e1") OpenTab("link-tab3-cp-trans-break-contract", "#tab3-cp-trans-break-contract", "ปรับปรุงรายการแจ้ง", false, "update", _cp1id, _trackingStatus);
            if (_action == "e2") ViewRepayStatusViewTransRequireContract(_cp1id, "", _trackingStatus, "e");
            if (_action == "v2") LoadForm(_frmIndex, "detailcptransrequirecontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
            if (_action == "a1") LoadForm(_frmIndex, "receivercptransbreakcontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
            if (_action == "v3") LoadForm(_frmIndex, "detailcptransrequirerepaycontract", true, "", _cp1id, "trans-break-contract" + _cp1id);
        }
    });
}

function ValidateCommentBreakContract(_cp1id, _action, _from)
{
    var _error = false;
    var _msg;
    var _focus;
    var _comment = $("#comment-reject").val();

    if (_error == false && _action == "E" && (_comment.length <= 0)) { _error = true; _msg = "กรุณาใส่สาเหตุการส่งรายการแจ้งกลับไปแก้ไข"; _focus = "#comment-reject"; }
    if (_error == false && _action == "C" && (_comment.length <= 0)) { _error = true; _msg = "กรุณาใส่สาเหตุการยกเลิกรายการ"; _focus = "#comment-reject"; }

    if (_error == true)
    {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _send = new Array();
    _send[0] = "cp1id=" + _cp1id;
    _send[1] = "actioncomment=" + _action;
    _send[2] = "comment=" + _comment;

    AddUpdateData("update", "rejectcptransbreakcontract", _send, false, "", "", "", false, function (_result) {
        if (_result == "1")
        {
            GotoSignin();
            return;
        }

        DialogConfirm("บันทึกข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "E")
                    {
                        $("#dialog-form2").dialog("close");
                        $("#dialog-form1").dialog("close");
                        OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                    }

                    if (_action == "C")
                    {
                        switch (_from)
                        {
                            case "breakcontract"  : {
                                                        CloseFrm(true, "");
                                                        OpenTab("link-tab1-cp-trans-break-contract", "#tab1-cp-trans-break-contract", "", true, "", "", "");
                                                        break;
                                                    }
                            case "requirecontract": {
                                                        CloseFrm(true, "");
                                                        OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                                                        break;
                                                    }
                            case "repaycontract"  : {
                                                        CloseFrm(true, "");
                                                        CloseFrm(true, "");
                                                        OpenTab("link-tab2-cp-trans-require-contract", "#tab2-cp-trans-require-contract", "", true, "", "", "");
                                                        break;
                                                    }
                        }
                    }
                }
            }
        });
    });
}

function ConfirmAddCommentBreakContract(_cp1id, _action, _from)
{
    var _msgAction = (_action == "E" ? "ส่งกลับแก้ไขรายการ" : "ยกเลิกรายการ");

    DialogConfirm("ต้องการ" + _msgAction + "นี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCommentBreakContract(_cp1id, _action, _from);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }

    });
}

function ConfirmUpdateTrackingStatus(_cp1id, _status, _from)
{
    var _statusMsg = "";

    _statusMsg = (_status == "edit") ? "ส่งกลับแก้ไข" : _statusMsg;
    _statusMsg = (_status == "cancel") ? "ยกเลิก" : _statusMsg;

    DialogConfirm("ต้องการ" + _statusMsg + "รายการนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                UpdateTrackingStatus(_cp1id, _status, _from);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }

    });
}

function UpdateTrackingStatus(_cp1id, _status, _from)
{
    var _send = new Array();
    _send[0] = "cp1id=" + _cp1id;
    _send[1] = "status=" + _status;

    ChkTrackingStatusViewTransBreakContract(_cp1id, $("#trackingstatus").val(), "", function (_result) {
        if (_result == "0")
        {
            AddUpdateData("update", "updatetrackingstatusbreakcontract", _send, false, "", "", "", true, function (_result) {
                if (_result == "1") {
                    GotoSignin();
                    return;
                }

                if (_status == "edit") OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                if (_status == "cancel")
                {
                    switch (_from)
                    {
                        case "breakcontract"   : {
                                                    ResetFrmCPTransBreakContract(true);
                                                    break;
                                                 }
                        case "requirecontract" : {  
                                                    ResetFrmCPTransRequireContract(true);
                                                    break;
                                                 }
                        case "repaycontract"   : {
                                                    CloseFrm(true, "");
                                                    OpenTab("link-tab2-cp-trans-require-contract", "#tab2-cp-trans-require-contract", "", true, "", "", "");
                                                    break;
                                                 }
                    }
                }
            });
        }
    });
}

function ConfirmUpdateTrackingStatusBreakContract(_cp1id, _status)
{
    ConfirmUpdateTrackingStatus(_cp1id, _status, "breakcontract");
}

function ConfirmUpdateTrackingStatusRequireContract(_cp1id, _status)
{
    ConfirmUpdateTrackingStatus(_cp1id, _status, "requirecontract");
}

function ConfirmUpdateTrackingStatusRepayContract(_cp1id, _status)
{
    ConfirmUpdateTrackingStatus(_cp1id, _status, "repaycontract");
}

function ConfirmSendBreakContract()
{
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

function SendBreakContract()
{
    _elem = $("input[name=send-break-contract]:checked");
    _countSend = _elem.length;

    if (_countSend == 0)
    {
        DialogMessage("ไม่พบรายการแจ้งที่ต้องการส่ง", "", false, "");
        return;
    }

    var _valCheck = new Array();

    _elem.each(function(i) {
        _valCheck[i] = this.value;
    });

    var _send = new Array();
    _send[0] = "cp1id=" + _valCheck.join(";");

    AddUpdateData("update", "sendbreakcontract", _send, false, "", "", "", false, function (_result) {
        if (_result == "1")
        {
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