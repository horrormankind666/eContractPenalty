function InitCPTabUser() {
    $("#phonenumber").inputmask("9 9999 9999");
    $("#mobilenumber").inputmask("99 9999 9999");
    $("#email").inputmask("email");
}

function ResetFrmCPTabUser(_disable) {
    GoToTopElement("html, body");

    if (_disable == true) {
        TextboxDisable("#username");
        TextboxDisable("#password");
        TextboxDisable("#name");
        TextboxDisable("#phonenumber");
        TextboxDisable("#mobilenumber");
        TextboxDisable("#email");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    $("#username").val($("#username-hidden").val());
    $("#password").val($("#password-hidden").val());
    $("#name").val($("#name-hidden").val());
    $("#phonenumber").val($("#phonenumber-hidden").val());
    $("#mobilenumber").val($("#mobilenumber-hidden").val());
    $("#email").val($("#email-hidden").val());
    $("#button-style11").show();
    $("#button-style12").hide();

    $("#username").blur(function () {
        $("#username").val($("#username").val().replace(/\:/g, ""));
    });

    $("#password").blur(function () {
        $("#password").val($("#password").val().replace(/\:/g, ""));
    });
}

function ConfirmActionCPTabUser(_action) {
    var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

    DialogConfirm("ต้องการ" + _actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabUser(_action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabUser(_action) {
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && ($("#username").val().length == 0)) {
        _error = true;
        _msg = "กรุณาใส่ Username";
        _focus = "#username";
    }

    if (_error == false && ($("#password").val().length == 0)) {
        _error = true;
        _msg = "กรุณาใส่ Password";
        _focus = "#password";
    }

    if (_error == false && ($("#name").val().length == 0)) {
        _error = true;
        _msg = "กรุณาใส่ชื่อ";
        _focus = "#name";
    }

    if (_error == false && (($("#phonenumber").val().length == 0) && ($("#mobilenumber").val().length == 0))) {
        _error = true;
        _msg = "กรุณาใส่หมายเลขโทรศัพท์";
        _focus = "#phonenumber";
    }

    if (_error == false && (($("#phonenumber").val().length > 0) && ($("#phonenumber").inputmask("isComplete") == false))) {
        _error = true;
        _msg = "กรุณาใส่หมายเลขโทรศัพท์ให้ถูกต้อง";
        _focus = "#phonenumber";
    }

    if (_error == false && (($("#mobilenumber").val().length > 0) && ($("#mobilenumber").inputmask("isComplete") == false))) {
        _error = true;
        _msg = "กรุณาใส่หมายโทรศัพท์มือถือให้ถูกต้อง";
        _focus = "#mobilenumber";
    }

    if (_error == false && ($("#email").val().length == 0)) {
        _error = true;
        _msg = "กรุณาใส่อีเมล์";
        _focus = "#email";
    }

    if (_error == false && ($("#email").inputmask("isComplete") == false)) {
        _error = true;
        _msg = "กรุณาใส่อีเมล์ให้ถูกต้อง";
        _focus = "#email";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
        return;
    }
    
    var _send = new Array();
    _send[_send.length] = "userid=" + $("#userid-hidden").val();
    _send[_send.length] = "usernameold=" + $("#username-hidden").val();
    _send[_send.length] = "passwordold=" + $("#password-hidden").val();
    _send[_send.length] = "username=" + $("#username").val();
    _send[_send.length] = "password=" + $("#password").val();
    _send[_send.length] = "name=" + $("#name").val();
    _send[_send.length] = "phonenumber=" + $("#phonenumber").val();
    _send[_send.length] = "mobilenumber=" + $("#mobilenumber").val();
    _send[_send.length] = "email=" + $("#email").val();

    AddUpdateData(_action, _action + "cptabuser", _send, false, "", "", "", false, function (_result) {        
        if (_result == "1") {
            GotoSignin();
            return;
        }

        _error = false;

        if (_error == false && _result == "2") {
            _error = true;
            _msg = "ชื่อผู้ใช้งานนี้มีอยู่แล้ว";
        }

        if (_error == false && _result == "3") {
            _error = true;
            _msg = "รหัสผ่านนี้มีอยู่แล้ว";
        }

        if (_error == true) {
            DialogMessage(_msg, _focus, false, "");
            return;
        }

        var _actionMsg = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";

        DialogConfirm(_actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (_action == "add")
                        ResetFrmCPTabUser(false);

                    if (_action == "update") {
                        $("#username-hidden").val($("#username").val());
                        $("#password-hidden").val($("#password").val());

                        ResetFrmCPTabUser(true);
                    }

                    if (_action == "del") OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                }
            }
        });
    });
}