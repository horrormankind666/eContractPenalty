function ResetFrmCPTabProgram(_disable) {
    GoToElement("top-page");

    if (_disable == true) {
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

function ConfirmActionCPTabProgram(_action) {
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabProgram(_action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabProgram(_action) {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ComboboxGetSelectedValue("dlevel") == "0") { _error = true; _msg = "กรุณาเลือกระดับการศึกษา"; _focus = ".dlevel-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("faculty") == "0") { _error = true; _msg = "กรุณาเลือกคณะ"; _focus = ".faculty-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("program") == "0") { _error = true; _msg = "กรุณาเลือกหลักสูตร"; _focus = ".program-combobox-input"; }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _faculty = ComboboxGetSelectedValue("faculty").split(";");
    var _program = ComboboxGetSelectedValue("program").split(";");
    var _send = new Array();
    _send[_send.length] = "cp1id=" + $("#cp1id").val();
    _send[_send.length] = "dlevel=" + ComboboxGetSelectedValue("dlevel");
    _send[_send.length] = "faculty=" + _faculty[0];
    _send[_send.length] = "programcode=" + _program[0];
    _send[_send.length] = "majorcode=" + _program[2];
    _send[_send.length] = "groupnum=" + _program[3];

    AddUpdateData(_action, _action + "cptabprogram", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }

        if (_result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "add") ResetFrmCPTabProgram(false);
                    if (_action == "update") ResetFrmCPTabProgram(true);
                    if (_action == "del") OpenTab("link-tab1-cp-tab-program", "#tab1-cp-tab-program", "", true, "", "", "");
                }
            }
        });
    });
}

function ResetFrmCPTabInterest(_disable) {
    GoToElement("top-page");

    if (_disable == true) {
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

function ConfirmActionCPTabInterest(_action) {
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabInterest(_action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabInterest(_action) {        
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && $("#in-contract-interest").val().length == 0) { _error = true; _msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา"; _focus = "#in-contract-interest"; }
    if (_error == false && $("#out-contract-interest").val().length == 0) { _error = true; _msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา"; _focus = "#out-contract-interest"; }
    if (_error == false && $("#in-contract-interest").val() == "0.00" && $("#out-contract-interest").val() == "0.00") { _error = true; _msg = "กรุณาใส่ดอกเบี้ยจากการผิดนัดชำระ"; _focus = "#in-contract-interest"; }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }
    
    var _send = new Array();
    _send[_send.length] = "cp1id=" + $("#cp1id").val();
    _send[_send.length] = "incontractinterest=" + DelCommas("in-contract-interest");
    _send[_send.length] = "outcontractinterest=" + DelCommas("out-contract-interest");

    AddUpdateData(_action, _action + "cptabinterest", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }

        var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "add") ResetFrmCPTabInterest(false);
                    if (_action == "update") ResetFrmCPTabInterest(true);
                    if (_action == "del") OpenTab("link-tab1-cp-tab-interest", "#tab1-cp-tab-interest", "", true, "", "","");
                }
            }
        });
    });
}

function UpdateUseContractInterest(_cp1id) {
    var _send = new Array();
    _send[_send.length] = "cp1id=" + _cp1id;
    _send[_send.length] = "usecontractinterest=" + ($("input[name='use-contract-interest']:checked").val() != null ? 1 : 0);

    AddUpdateData("update", "updateusecontractinterest", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
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
    var _result = $("input[name=set-amt-indemnitor-year]:checked").val();

    if (_result == "Y")
        TextboxEnable("#amt-indemnitor-year")
    else {
        $("#amt-indemnitor-year").val("");
        TextboxDisable("#amt-indemnitor-year");
    }
}

function ResetFrmCPTabPayBreakContract(_disable) {
    GoToElement("top-page");

    if (_disable == true) {
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

function ViewCalDate(_calDateCondition) {
    if (_calDateCondition.length <= 0) {
        _calDateCondition = ComboboxGetSelectedValue("cal-date-condition");

        if ((_calDateCondition == null) || (_calDateCondition == "0")) {
            DialogMessage("กรุณาเลือกวิธีคิดและคำนวณเงินชดใช้", "", false, "");
            return;
        }
    }

    LoadForm(1, "detailcptabcaldate", true, "", _calDateCondition, "");
}

function ConfirmActionCPTabPayBreakContract(_action) {
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabPayBreakContract(_action)
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabPayBreakContract(_action) {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ComboboxGetSelectedValue("dlevel") == "0") { _error = true; _msg = "กรุณาเลือกระดับการศึกษา"; _focus = ".dlevel-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("case-graduate") == "0") { _error = true; _msg = "กรุณาเลือกกรณีการชดใช้ตามสัญญา"; _focus = ".case-graduate-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("facultycptabprogram") == "0") { _error = true; _msg = "กรุณาเลือกคณะ"; _focus = ".facultycptabprogram-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("programcptabprogram") == "0") { _error = true; _msg = "กรุณาเลือกหลักสูตร"; _focus = ".programcptabprogram-combobox-input"; }
    if (_error == false && (($("#amount-cash").val().length == 0) || ($("#amount-cash").val() == "0"))) { _error = true; _msg = "กรุณาใส่จำนวนเงินชดใช้"; _focus = "#amount-cash"; }
    if (_error == false && ((ComboboxGetSelectedValue("case-graduate") == "2") && ($("input[name=set-amt-indemnitor-year]:checked").val() == "Y") && (($("#amt-indemnitor-year").val().length == 0) || ($("#amt-indemnitor-year").val() == "0")))) { _error = true; _msg = "กรุณาใส่ระยะเวลาทำงานชดใช้หลังสำเร็จการศึกษา"; _focus = "#amt-indemnitor-year"; }
    if (_error == false && ComboboxGetSelectedValue("cal-date-condition") == "0") { _error = true; _msg = "กรุณาเลือกวิธีคิดและคำนวณเงินชดใช้"; _focus = ".cal-date-condition-combobox-input"; }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _faculty = ComboboxGetSelectedValue("facultycptabprogram").split(";");
    var _program = ComboboxGetSelectedValue("programcptabprogram").split(";");
    var _send = new Array();
    _send[_send.length] = "cp1id=" + $("#cp1id").val();
    _send[_send.length] = "dlevel=" + ComboboxGetSelectedValue("dlevel");
    _send[_send.length] = "casegraduate=" + ComboboxGetSelectedValue("case-graduate");
    _send[_send.length] = "faculty=" + _faculty[0];
    _send[_send.length] = "programcode=" + _program[0];
    _send[_send.length] = "majorcode=" + _program[2];
    _send[_send.length] = "groupnum=" + _program[3];
    _send[_send.length] = "amountcash=" + DelCommas("amount-cash");
    _send[_send.length] = "amtindemnitoryear=" + DelCommas("amt-indemnitor-year");
    _send[_send.length] = "caldatecondition=" + ComboboxGetSelectedValue("cal-date-condition");
  
    AddUpdateData(_action, _action + "cptabpaybreakcontract", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }

        if (_result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "add") ResetFrmCPTabPayBreakContract(false);
                    if (_action == "update") ResetFrmCPTabPayBreakContract(true);
                    if (_action == "del") OpenTab("link-tab1-cp-tab-pay-break-contract", "#tab1-cp-tab-pay-break-contract", "", true, "", "","");
                }
            }
        });
    });
}

function ResetFrmCPTabScholarship(_disable) {
    GoToElement("top-page");

    if (_disable == true) {
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

function ConfirmActionCPTabScholarship(_action) {
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabScholarship(_action)
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabScholarship(_action) {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ComboboxGetSelectedValue("dlevel") == "0") { _error = true; _msg = "กรุณาเลือกระดับการศึกษา"; _focus = ".dlevel-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("facultycptabprogram") == "0") { _error = true; _msg = "กรุณาเลือกคณะ"; _focus = ".faculty-combobox-input"; }
    if (_error == false && ComboboxGetSelectedValue("programcptabprogram") == "0") { _error = true; _msg = "กรุณาเลือกหลักสูตร"; _focus = ".program-combobox-input"; }
    if (_error == false && (($("#scholarship-money").val().length == 0) || ($("#scholarship-money").val() == "0"))) { _error = true; _msg = "กรุณาใส่จำนวนเงินทุนการศึกษา"; _focus = "#scholarship-money"; }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }

    var _faculty = ComboboxGetSelectedValue("facultycptabprogram").split(";");
    var _program = ComboboxGetSelectedValue("programcptabprogram").split(";");
    var _send = new Array();
    _send[_send.length] = "cp1id=" + $("#cp1id").val();
    _send[_send.length] = "dlevel=" + ComboboxGetSelectedValue("dlevel");
    _send[_send.length] = "faculty=" + _faculty[0];
    _send[_send.length] = "programcode=" + _program[0];
    _send[_send.length] = "majorcode=" + _program[2];
    _send[_send.length] = "groupnum=" + _program[3];
    _send[_send.length] = "scholarshipmoney=" + DelCommas("scholarship-money");

    AddUpdateData(_action, _action + "cptabscholarship", _send, false, "", "", "", false, function (_result) {
        if (_result == "1") {
            GotoSignin();
            return;
        }

        if (_result == "2") {
            DialogMessage("มีข้อมูลนี้อยู่ในระบบแล้ว", "", false, "");
            return;
        }

        var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "add") ResetFrmCPTabScholarship(false);
                    if (_action == "update") ResetFrmCPTabScholarship(true);
                    if (_action == "del") OpenTab("link-tab1-cp-tab-scholarship", "#tab1-cp-tab-scholarship", "", true, "", "", "");
                }
            }
        });
    });
}