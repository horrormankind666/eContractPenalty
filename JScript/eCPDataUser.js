function InitCPTabUser() {
    $("#phonenumber").inputmask("9 9999 9999");
    $("#mobilenumber").inputmask("99 9999 9999");
    $("#email").inputmask("email");
}

function ResetFrmCPTabUser(disable) {
    GoToTopElement("html, body");

    if (disable == true) {
        TextboxDisable("#username");
        /*
        TextboxDisable("#password");
        */
        TextboxDisable("#name");
        TextboxDisable("#phonenumber");
        TextboxDisable("#mobilenumber");
        TextboxDisable("#email");
        $("#button-style11").hide();
        $("#button-style12").show();
        return;
    }

    $("#username").val($("#username-hidden").val());
    /*
    $("#password").val($("#password-hidden").val());
    */
    $("#name").val($("#name-hidden").val());
    $("#phonenumber").val($("#phonenumber-hidden").val());
    $("#mobilenumber").val($("#mobilenumber-hidden").val());
    $("#email").val($("#email-hidden").val());
    $("#button-style11").show();
    $("#button-style12").hide();

    $("#username").blur(function () {
        $("#username").val($("#username").val().replace(/\:/g, ""));
    });

    /*
    $("#password").blur(function () {
        $("#password").val($("#password").val().replace(/\:/g, ""));
    });
    */
}

function ConfirmActionCPTabUser(action) {
    var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

    DialogConfirm("ต้องการ" + actionMsg + "ข้อมูลนี้หรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");

                ValidateCPTabUser(action);
            },
            "ยกเลิก": function () {
                $(this).dialog("close");
            }
        }
    });
}

function ValidateCPTabUser(action) {
    var error = false;
    var msg;
    var focus;
    
    if (("add, update").includes(action) == true) {
        if (error == false &&
            $("#username").val().length == 0) {
            error = true;
            msg = "กรุณาใส่ Username";
            focus = "#username";
        }
        /*
        if (error == false &&
            $("#password").val().length == 0) {
            error = true;
            msg = "กรุณาใส่ Password";
            focus = "#password";
        }
        */
        if (error == false &&
            $("#name").val().length == 0) {
            error = true;
            msg = "กรุณาใส่ชื่อ";
            focus = "#name";
        }

        if (error == false &&
            $("#phonenumber").val().length == 0 &&
            $("#mobilenumber").val().length == 0) {
            error = true;
            msg = "กรุณาใส่หมายเลขโทรศัพท์";
            focus = "#phonenumber";
        }

        if (error == false &&
            $("#phonenumber").val().length > 0 &&
            $("#phonenumber").inputmask("isComplete") == false) {
            error = true;
            msg = "กรุณาใส่หมายเลขโทรศัพท์ให้ถูกต้อง";
            focus = "#phonenumber";
        }

        if (error == false &&
            $("#mobilenumber").val().length > 0 &&
            $("#mobilenumber").inputmask("isComplete") == false) {
            error = true;
            msg = "กรุณาใส่หมายโทรศัพท์มือถือให้ถูกต้อง";
            focus = "#mobilenumber";
        }

        if (error == false &&
            $("#email").val().length == 0) {
            error = true;
            msg = "กรุณาใส่อีเมล์";
            focus = "#email";
        }

        if (error == false &&
            $("#email").inputmask("isComplete") == false) {
            error = true;
            msg = "กรุณาใส่อีเมล์ให้ถูกต้อง";
            focus = "#email";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
            return;
        }
    }
    
    var send = new Array();
    send[send.length] = ("userid=" + $("#userid-hidden").val());
    send[send.length] = ("usernameold=" + $("#username-hidden").val());
    /*
    send[send.length] = ("passwordold=" + $("#password-hidden").val());
    */
    send[send.length] = ("username=" + $("#username").val());
    /*
    send[send.length] = ("password=" + $("#password").val());
    */
    send[send.length] = ("name=" + $("#name").val());
    send[send.length] = ("phonenumber=" + $("#phonenumber").val());
    send[send.length] = ("mobilenumber=" + $("#mobilenumber").val());
    send[send.length] = ("email=" + $("#email").val());

    AddUpdateData(action, (action + "cptabuser"), send, false, "", "", "", false, function (result) {
        if (result == "1") {
            GotoSignin();
            return;
        }

        error = false;

        if (error == false &&
            result == "2") {
            error = true;
            msg = "ชื่อผู้ใช้งานนี้มีอยู่แล้ว";
        }

        if (error == false &&
            result == "3") {
            error = true;
            msg = "รหัสผ่านนี้มีอยู่แล้ว";
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
            return;
        }

        var actionMsg = (action == "add" || action == "update" ? "บันทึก" : "ลบ");

        DialogConfirm(actionMsg + "ข้อมูลเรียบร้อย");
        $("#dialog-confirm").dialog({
            buttons: {
                "ตกลง": function () {
                    $(this).dialog("close");

                    if (action == "add")
                        ResetFrmCPTabUser(false);

                    if (action == "update") {
                        $("#username-hidden").val($("#username").val());
                        /*
                        $("#password-hidden").val($("#password").val());
                        */

                        ResetFrmCPTabUser(true);
                    }

                    if (action == "del")
                        OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                }
            }
        });
    });
}