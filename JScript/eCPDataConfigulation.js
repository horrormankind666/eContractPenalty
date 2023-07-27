function ResetFrmCPTabProgram(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        ComboboxDisable("dlevel");
        ComboboxDisable("faculty");
        ComboboxDisable("program");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    InitCombobox("dlevel", "0", $("#dlevel-hidden").val(), 390, 415);
    InitCombobox("faculty", "0", $("#faculty-hidden").val(), 390, 415);
    $("#button-style11").show();
    $("#button-style12").hide();
}

function ConfirmActionCPTabProgram(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabProgram(action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabProgram(action) {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        ComboboxGetSelectedValue("dlevel") == "0") {
        error = true;
        msg = "กรุณาเลือกระดับการศึกษา";
        focus = ".dlevel-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("faculty") == "0") {
        error = true;
        msg = "กรุณาเลือกคณะ";
        focus = ".faculty-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("program") == "0") {
        error = true;
        msg = "กรุณาเลือกหลักสูตร";
        focus = ".program-combobox-input";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var faculty = ComboboxGetSelectedValue("faculty").split(";");
    var program = ComboboxGetSelectedValue("program").split(";");
    var send = new Array();
    send[send.length] = ("cp1id=" + $("#cp1id").val());
    send[send.length] = ("dlevel=" + ComboboxGetSelectedValue("dlevel"));
    send[send.length] = ("faculty=" + faculty[0]);
    send[send.length] = ("programcode=" + program[0]);
    send[send.length] = ("majorcode=" + program[2]);
    send[send.length] = ("groupnum=" + program[3]);

    AddUpdateData(action, (action + "cptabprogram"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        if (result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "add")
                        ResetFrmCPTabProgram(false);

                    if (action == "update")
                        ResetFrmCPTabProgram(true);

                    if (action == "del")
                        OpenTab("link-tab1-cp-tab-program", "#tab1-cp-tab-program", "", true, "", "", "");
                }
            }
        });
    });
}

function ResetFrmCPTabInterest(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        TextboxDisable("#in-contract-interest");
        TextboxDisable("#out-contract-interest");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    $("#in-contract-interest").val($("#in-contract-interest-hidden").val());
    $("#out-contract-interest").val($("#out-contract-interest-hidden").val());
    $("#button-style11").show();
    $("#button-style12").hide();
}

function ConfirmActionCPTabInterest(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabInterest(action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabInterest(action) {        
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        $("#in-contract-interest").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา";
        focus = "#in-contract-interest";
    }

    if (error == false &&
        $("#out-contract-interest").val().length == 0) {
        error = true;
        msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา";
        focus = "#out-contract-interest";
    }

    if (error == false &&
        $("#in-contract-interest").val() == "0.00" &&
        $("#out-contract-interest").val() == "0.00") {
        error = true;
        msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระ";
        focus = "#in-contract-interest";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }
    
    var send = new Array();
    send[send.length] = ("cp1id=" + $("#cp1id").val());
    send[send.length] = ("incontractinterest=" + DelCommas("in-contract-interest"));
    send[send.length] = ("outcontractinterest=" + DelCommas("out-contract-interest"));

    AddUpdateData(action, (action + "cptabinterest"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "add")
                        ResetFrmCPTabInterest(false);

                    if (action == "update")
                        ResetFrmCPTabInterest(true);

                    if (action == "del")
                        OpenTab("link-tab1-cp-tab-interest", "#tab1-cp-tab-interest", "", true, "", "", "");
                }
            }
        });
    });
}

function UpdateUseContractInterest(cp1id) {
    var send = new Array();
    send[send.length] = ("cp1id=" + cp1id);
    send[send.length] = ("usecontractinterest=" + ($("input[name='use-contract-interest']:checked").val() != null ? 1 : 0));

    AddUpdateData("update", "updateusecontractinterest", send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }
    });
}

function InitSetAmtIndemnitorYear() {
    $("input[name=set-amt-indemnitor-year]:radio").click(function () {
        ResetSetAmtIndemnitorYear();
    });
}

function ResetSetAmtIndemnitorYear() {
    var result = $("input[name=set-amt-indemnitor-year]:checked").val();

    if (result == "Y")
        TextboxEnable("#amt-indemnitor-year")
    else {
        $("#amt-indemnitor-year").val("");
        TextboxDisable("#amt-indemnitor-year");
    }
}

function ResetFrmCPTabPayBreakContract(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        ComboboxDisable("dlevel");
        ComboboxDisable("case-graduate");
        ComboboxDisable("facultycptabprogram");
        ComboboxDisable("programcptabprogram");
        TextboxDisable("#amount-cash");
        $("input[name=set-amt-indemnitor-year]:radio").prop("disabled", true);
        TextboxDisable("#amt-indemnitor-year");
        ComboboxDisable("cal-date-condition");
        ButtonDisable("#addupdate-cp-tab-pay-break-contract #view-cal-date .button-style2", "button-style2-disable");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    InitCombobox("dlevel", "0", $("#dlevel-hidden").val(), 390, 415);
    InitCombobox("case-graduate", "0", $("#case-graduate-hidden").val(), 390, 415);
    InitCombobox("facultycptabprogram", "0", $("#faculty-hidden").val(), 390, 415);
    $("#amount-cash").val($("#amount-cash-hidden").val());
    $("#amt-indemnitor-year").val($("#amt-indemnitor-year-hidden").val());
    $("input[name=set-amt-indemnitor-year][value=Y]").prop("checked", ($("#amt-indemnitor-year").val().length > 0 ? true : false));
    ResetSetAmtIndemnitorYear();
    InitCombobox("cal-date-condition", "0", $("#cal-date-condition-hidden").val(), 200, 225);
    $("#button-style11").show();
    $("#button-style12").hide();
}

function ViewCalDate(calDateCondition) {
    if (calDateCondition.length <= 0) {
        calDateCondition = ComboboxGetSelectedValue("cal-date-condition");

        if (calDateCondition == null ||
            calDateCondition == "0") {
            DialogMessage("กรุณาเลือกวิธีคิดและคำนวณเงินชดใช้", "", false, "");
            return;
        }
    }

    LoadForm(1, "detailcptabcaldate", true, "", calDateCondition, "");
}

function ConfirmActionCPTabPayBreakContract(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabPayBreakContract(action)
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabPayBreakContract(action) {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        ComboboxGetSelectedValue("dlevel") == "0") {
        error = true;
        msg = "กรุณาเลือกระดับการศึกษา";
        focus = ".dlevel-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate") == "0") {
        error = true;
        msg = "กรุณาเลือกกรณีการชดใช้ตามสัญญา";
        focus = ".case-graduate-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("facultycptabprogram") == "0") {
        error = true;
        msg = "กรุณาเลือกคณะ";
        focus = ".facultycptabprogram-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("programcptabprogram") == "0") {
        error = true;
        msg = "กรุณาเลือกหลักสูตร";
        focus = ".programcptabprogram-combobox-input";
    }

    if (error == false &&
        ($("#amount-cash").val().length == 0 || $("#amount-cash").val() == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินชดใช้";
        focus = "#amount-cash";
    }

    if (error == false &&
        ComboboxGetSelectedValue("case-graduate") == "2" &&
        $("input[name=set-amt-indemnitor-year]:checked").val() == "Y" &&
        ($("#amt-indemnitor-year").val().length == 0 || $("#amt-indemnitor-year").val() == "0")) {
        error = true;
        msg = "กรุณาใส่ระยะเวลาทำงานชดใช้หลังสำเร็จการศึกษา";
        focus = "#amt-indemnitor-year";
    }

    if (error == false &&
        ComboboxGetSelectedValue("cal-date-condition") == "0") {
        error = true;
        msg = "กรุณาเลือกวิธีคิดและคำนวณเงินชดใช้";
        focus = ".cal-date-condition-combobox-input";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var faculty = ComboboxGetSelectedValue("facultycptabprogram").split(";");
    var program = ComboboxGetSelectedValue("programcptabprogram").split(";");
    var send = new Array();
    send[send.length] = ("cp1id=" + $("#cp1id").val());
    send[send.length] = ("dlevel=" + ComboboxGetSelectedValue("dlevel"));
    send[send.length] = ("casegraduate=" + ComboboxGetSelectedValue("case-graduate"));
    send[send.length] = ("faculty=" + faculty[0]);
    send[send.length] = ("programcode=" + program[0]);
    send[send.length] = ("majorcode=" + program[2]);
    send[send.length] = ("groupnum=" + program[3]);
    send[send.length] = ("amountcash=" + DelCommas("amount-cash"));
    send[send.length] = ("amtindemnitoryear=" + DelCommas("amt-indemnitor-year"));
    send[send.length] = ("caldatecondition=" + ComboboxGetSelectedValue("cal-date-condition"));
  
    AddUpdateData(action, (action + "cptabpaybreakcontract"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        if (result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "add")
                        ResetFrmCPTabPayBreakContract(false);

                    if (action == "update")
                        ResetFrmCPTabPayBreakContract(true);

                    if (action == "del")
                        OpenTab("link-tab1-cp-tab-pay-break-contract", "#tab1-cp-tab-pay-break-contract", "", true, "", "", "");
                }
            }
        });
    });
}

function ResetFrmCPTabScholarship(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        ComboboxDisable("dlevel");
        ComboboxDisable("facultycptabprogram");
        ComboboxDisable("programcptabprogram");
        TextboxDisable("#scholarship-money");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    InitCombobox("dlevel", "0", $("#dlevel-hidden").val(), 390, 415);
    InitCombobox("facultycptabprogram", "0", $("#faculty-hidden").val(), 390, 415);
    $("#scholarship-money").val($("#scholarship-money-hidden").val());
    $("#button-style11").show();
    $("#button-style12").hide();
}

function ConfirmActionCPTabScholarship(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabScholarship(action)
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabScholarship(action) {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        ComboboxGetSelectedValue("dlevel") == "0") {
        error = true;
        msg = "กรุณาเลือกระดับการศึกษา";
        focus = ".dlevel-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("facultycptabprogram") == "0") {
        error = true;
        msg = "กรุณาเลือกคณะ";
        focus = ".faculty-combobox-input";
    }

    if (error == false &&
        ComboboxGetSelectedValue("programcptabprogram") == "0") {
        error = true;
        msg = "กรุณาเลือกหลักสูตร";
        focus = ".program-combobox-input";
    }

    if (error == false &&
        ($("#scholarship-money").val().length == 0 || $("#scholarship-money").val() == "0")) {
        error = true;
        msg = "กรุณาใส่จำนวนเงินทุนการศึกษา";
        focus = "#scholarship-money";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return;
    }

    var faculty = ComboboxGetSelectedValue("facultycptabprogram").split(";");
    var program = ComboboxGetSelectedValue("programcptabprogram").split(";");
    var send = new Array();
    send[send.length] = ("cp1id=" + $("#cp1id").val());
    send[send.length] = ("dlevel=" + ComboboxGetSelectedValue("dlevel"));
    send[send.length] = ("faculty=" + faculty[0]);
    send[send.length] = ("programcode=" + program[0]);
    send[send.length] = ("majorcode=" + program[2]);
    send[send.length] = ("groupnum=" + program[3]);
    send[send.length] = ("scholarshipmoney=" + DelCommas("scholarship-money"));

    AddUpdateData(action, (action + "cptabscholarship"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        if (result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "add")
                        ResetFrmCPTabScholarship(false);

                    if (action == "update")
                        ResetFrmCPTabScholarship(true);

                    if (action == "del")
                        OpenTab("link-tab1-cp-tab-scholarship", "#tab1-cp-tab-scholarship", "", true, "", "", "");
                }
            }
        });
    });
}