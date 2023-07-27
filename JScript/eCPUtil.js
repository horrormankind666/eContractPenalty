var oldMenu = 0;
var manual = new Array();

function Signin() {
    var username = ($("#username").val($.trim($("#username").val()))).val();
    var password = ($("#password").val($.trim($("#password").val()))).val();
    var d = new Date();
    var authen = window.btoa(window.btoa(d.getDate() + d.getMonth() + d.getFullYear()) + "." + window.btoa(username).split("").reverse().join("") + "." + window.btoa(password).split("").reverse().join("") + "." + window.btoa(d.getHours() + d.getMinutes() + d.getSeconds() + d.getMilliseconds()));
    var send = new Array();
    send[send.length] = ("action=signin");
    send[send.length] = ("authen=" + authen);
    /*
    send[send.length] = ("username=" + ($("#username").val($.trim($("#username").val()))).val());
    send[send.length] = ("password=" + ($("#password").val($.trim($("#password").val()))).val());
    */

    var msg;
    var error = false;
    var focus;
  
    SetMsgLoading("กำลังเข้าสู่ระบบ...");

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
        var dataError = result.split("<error>");

        switch (dataError[1]) {
            case "1":
                error = true;
                msg = "ไม่พบผู้ใช้งานนี้";
                focus = "username";
                break;
        }

        if (error == true) {
            DialogMessage(msg, focus, false, "");
            return;
        }
        
        top.location.href = "index.aspx";
    });
}

function Signout() {
    if (oldMenu != 0)
        $("#menu" + oldMenu).removeClass("active").addClass("noactive");

    $("#menu8").removeClass("noactive").addClass("active");
        
    DialogConfirm("ต้องการออกจากระบบหรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
	        "ตกลง": function () {
		        $(this).dialog("close");
			            
                var send = new Array();
                send[send.length] = "action=signout";

			    SetMsgLoading("กำลังออกจากระบบ...");

			    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
			        GotoSignin();
			    });
		    },
		    "ยกเลิก": function () {
			    $(this).dialog("close");
		    }
	    },
        close:  function () {
            $("#menu8").removeClass("active").addClass("noactive");
            $("#menu" + oldMenu).removeClass("noactive").addClass("active");
        }
    });
}

function ShowManual() {
    if (oldMenu != 0)
        $("#menu" + oldMenu).removeClass("active").addClass("noactive");

    $("#menu7").removeClass("noactive").addClass("active");

    for (var i = 0; i < 50; i++) {
        manual[i] = ("Manual/UserManual" + (i + 1) + ".png");
    }

    LoadForm(1, "manual", true, "", "", "");
}

function SetPage() {
    var send = new Array();
    send[send.length] = "action=setpage";
    
    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (result) {
        var dataSection = result.split("<section>");
        var dataPage = result.split("<page>");

        GoToPage(parseInt(dataSection[1]), parseInt(dataPage[1]));
    });
}

function GoToPage(section, order) {
    var area;
    var pid;

    if (order == 0) {
        pid = 1;
        area = "all";
    }

    /*
    เจ้าหน้าที่กองกฏหมาย
    */
    if (order != 0 &&
        section == 1) {
        switch (order) {
            /*
            หน้าแรก
            */
            case 1:
                pid = 1;
                area = "sec";
                break;
            /*
            บัญชีผู้ใช้งาน
            */
            case 2:
                pid = 2;
                area = "sec";
                break;
            /*
            ตั้งค่าระบบ - กำหนดหลักสูตรที่ให้มีการทำสัญญาการศึกษา
            */
            case 3:
                pid = 3;
                area = "sec";
                break;
            /*
            ตั้งค่าระบบ - เงื่อนไขการคิดระยะเวลาตามสัญญาและสูตรคำนวณเงินชดใช้ตามสัญญา
            */
            case 4:
                pid = 4;
                area = "sec";
                break;
            /*
            ตั้งค่าระบบ - กำหนดดอกเบี้ยจากการผิดนัดชำระ
            */
            case 5:
                pid = 5;
                area = "sec";
                break;
            /*
            ตั้งค่าระบบ - เกณฑ์การชดใช้ตามสัญญา
            */
            case 6:
                pid = 6;
                area = "sec";
                break;
            /*
            ตั้งค่าระบบ - กำหนดทุนการศึกษาแต่ละหลักสูตร
            */
            case 7:
                pid = 7;
                area = "sec";
                break;
            /*
            รับแจ้งนักศึกษาผิดสัญญา
            */
            case 8:
                pid = 8;
                area = "sec";
                break;
            /*
            บันทึกการชำระหนี้ / ดอกเบี้ย
            */
            case 9:
                pid = 9;
                area = "sec";
                break;
            /*
            รายงาน - สถานะขั้นตอนการดำเงินงานของผู้ผิดสัญญา
            */
            case 10:
                pid = 10;
                area = "sec";
                break;
            /*
            รายงาน - ตารางคำนวณเงินต้นและดอกเบี้ย
            */
            case 11:
                pid = 11;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการชำระหนี้ของผู้ผิดสัญญา
            */
            case 12:
                pid = 12;
                area = "sec";
                break;
            /*
            รายงาน - หนังสือทวงถามผู้ผิดสัญญาและผู้ค้ำประกัน
            */
            case 13:
                pid = 13;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการทำสัญญาและการผิดสัญญาของนักศึกษา
            */
            case 14:
                pid = 14;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการชำระหนี้ตามช่วงวันที่
            */
            case 15:
                pid = 15;
                area = "sec";
                break;
            /*
            รายงาน - เอกสารสัญญาการเป็นนักศึกษา
            */
            case 16:
                pid = 16;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้
            */
            case 17:
                pid = 17;
                area = "sec";
                break;
            /*
            รายงาน - การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้
            */
            case 18:
                pid = 18;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้
            */
            case 19:
                pid = 19;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาคงค้าง ( กรณี Z600 ลูกหนี้นักศึกษา )
            */
            case 20:
                pid = 20;
                area = "sec";
                break;
        }
    }

    /*
    เจ้าหน้าที่กองบริหารการศึกษา
    */
    if (order != 0 &&
        section == 2) {
        switch (order) {
            /*
            หน้าแรก
            */
            case 1:
                pid = 1;
                area = "sec";
                break;
            /*
            บัญชีผู้ใช้งาน
            */
            case 2:
                pid = 2;
                area = "sec";
                break;
            /*
            แจ้งนักศึกษาผิดสัญญา
            */
            case 3:
                pid = 3;
                area = "sec";
                break;
            /*
            รายงาน - สถานะขั้นตอนการดำเงินงานของผู้ผิดสัญญา
            */
            case 4:
                pid = 4;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการชำระหนี้ของผู้ผิดสัญญา
            */
            case 5:
                pid = 5;
                area = "sec";
                break;
            /*
            รายงาน - หนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณา
            */
            case 6:
                pid = 6;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการทำสัญญาและการผิดสัญญาของนักศึกษา
            */
            case 7:
                pid = 7;
                area = "sec";
                break;
            /*
            รายงาน - สถิติการชำระหนี้ตามช่วงวันที่
            */
            case 8:
                pid = 8;
                area = "sec";
                break;
            /*
            รายงาน - เอกสารสัญญาการเป็นนักศึกษา
            */
            case 9:
                pid = 9;
                area = "sec";
                break;
        }
    }

    /*
    เจ้าหน้าที่กองคลัง
    */
    if (order != 0 &&
        section == 3) {
        switch (order) {
            /*
            หน้าแรก
            */
            case 1:
                pid = 1;
                area = "sec";
                break;
            /*
            บัญชีผู้ใช้งาน
            */
            case 2:
                pid = 2;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดล
            */
            case 3:
                pid = 3;
                area = "sec";
                break;
            /*
            รายงาน - การรับชำระเงินจากลูกหนี้ ตามการผิดสัญญาการศึกษามหาวิทยาลัยมหิดล
            */
            case 4:
                pid = 4;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดลคงค้าง
            */
            case 5:
                pid = 5;
                area = "sec";
                break;
            /*
            รายงาน - ลูกหนี้ผิดสัญญาคงค้าง ( กรณี Z600 ลูกหนี้นักศึกษา )
            */
            case 6:
                pid = 6;
                area = "sec";
                break;
        }
    }
    
    LoadPage(area, section, pid);
}

function GotoSignin() {
    DialogConfirm("กรุณาเข้าระบบนักศึกษา");
    /*
    DialogConfirm("เข้าสู่ระบบใหม่อีกครั้ง");
    */    
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            top.location.href = "https://smartedu.mahidol.ac.th/Authen/Staff/login.aspx"
            /*
            top.location.href = "Signin.aspx";
            */
        }
    });    
}

function LoadSignin() {
    $(".head").hide();
    $(".menu-bar-main").hide();

    var send = new Array();
    send[send.length] = "action=page";

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (result) {
        var dataContent = result.split("<content>");

        $("#content-content").html(dataContent[1]);

        InitTextSelect();
        /*
        GoToElement("top-page");
        */
        GoToTopElement("html, body");
    })
}

function LoadPage(
    area,
    section,
    pid
) {
    var send = new Array();
    send[send.length] = "action=page";
    send[send.length] = ("area=" + area);
    send[send.length] = ("section=" + section);
    send[send.length] = ("pid=" + pid);

    var error = false;
    var msg;
    
    if (oldMenu != 0)
        $("#menu" + oldMenu).removeClass("active").addClass("noactive");
  
    SetMsgLoading("กำลังโหลด...");
  
    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
        var dataError = result.split("<error>");
        var dataHead = result.split("<head>");
        var dataMenuBar = result.split("<menubar>");
        var dataMenu = result.split("<menu>");
        var dataContent = result.split("<content>");

        if (dataError[1] == "1")
            GotoSignin();

        if (dataError[1] == "2")
            DialogMessage("ไม่พบผู้ใช้งานนี้", "", false, "");

        
        $("#head-content").html(dataHead[1]);
        $("#menu-bar-content").html(dataMenuBar[1]);
        $("#menu" + dataMenu[1]).removeClass("noactive").addClass("active");

        oldMenu = dataMenu[1];

        if (dataHead[1].length > 0) {
            $(".head").show();
            $(".menu-bar-main").show();
        }
        else {
            $(".head").hide();
            $(".menu-bar-main").hide();
        }

        if (dataError[1] == "0") {
            $("#content-content").html(dataContent[1]);

            InitTab(false);

            if (section == 1) {
                switch (pid) {
                    case 2:
                        OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                        break;
                    case 8:
                        OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                        break;
                    case 9:
                    case 20:
                        OpenTab("link-tab1-cp-trans-payment", "#tab1-cp-trans-payment", "", true, "", "", "");
                        break;
                    case 10:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportStepOfWork();
                        break;
                    case 11:
                        OpenTab("link-tab1-cp-report-table-cal-capital-and-interest", "#tab1-cp-report-table-cal-capital-and-interest", "", true, "", "", "");
                        break;
                    case 12:
                        OpenTab("link-tab1-cp-report-statistic-repay", "#tab1-cp-report-statistic-repay", "", true, "", "", "");
                        break;
                    case 13:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportNoticeClaimDebt();
                        break;
                    case 14:
                        OpenTab("link-tab1-cp-report-statistic-contract", "#tab1-cp-report-statistic-contract", "", true, "", "", "");
                        break;
                    case 15:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportStatisticPaymentByDate();
                        break;
                    case 16:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportEContract();
                        break;
                    case 17:
                    case 18:
                    case 19:
                        OpenTab("link-tab1-cp-report-debtor-contract", "#tab1-cp-report-debtor-contract", "", true, "", "", "");
                        break;
                }
            }

            if (section == 2) {
                switch (pid) {
                    case 2:
                        OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                        break;
                    case 3:
                        OpenTab("link-tab1-cp-trans-break-contract", "#tab1-cp-trans-break-contract", "", true, "", "", "");
                        break;
                    case 4:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportStepOfWork();
                        break;
                    case 5:
                        OpenTab("link-tab1-cp-report-statistic-repay", "#tab1-cp-report-statistic-repay", "", true, "", "", "");
                        break;
                    case 6:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportNoticeRepayComplete();
                        break;
                    case 7:
                        OpenTab("link-tab1-cp-report-statistic-contract", "#tab1-cp-report-statistic-contract", "", true, "", "", "");
                        break;
                    case 8:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportStatisticPaymentByDate();
                        break;
                    case 9:
                        SetMsgLoading("กำลังโหลด...");
                        SearchReportEContract();
                        break;
                }
            }

            if (section == 3) {
                switch (pid) {
                    case 2:
                        OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                        break;
                    case 3:
                    case 4:
                    case 5:
                        OpenTab("link-tab1-cp-report-debtor-contract", "#tab1-cp-report-debtor-contract", "", true, "", "", "");
                        break;
                    case 6:
                        OpenTab("link-tab1-cp-trans-payment", "#tab1-cp-trans-payment", "", true, "", "", "");
                        break;

                }
            }

            GoToTopElement("html, body");
        }
    });
}

function LoadCombobox(
    listData,
    param,
    target,
    value,
    widthInput,
    widthList
) {
    var send = new Array();
    send[send.length] = "action=combobox";
    send[send.length] = ("list=" + listData);
    send[send.length] = param;

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (result) {
        var dataList = result.split("<list>");

        $("#" + target).html(dataList[1]);
        InitCombobox(listData, value, value, widthInput, widthList);
    });
}

function LoadList(
    listData,
    recordCountID,
    listID,
    navPageID
) {
    $("#" + recordCountID).html("");
    $("#" + listID).html("");

    if (navPageID.length > 0)
        $("#" + navPageID).html("");

    var send = new Array();
    send[send.length] = "action=list";
    send[send.length] = ("list=" + listData);
    
    SetMsgLoading("กำลังโหลด...");

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
        var dataRecordCount = result.split("<recordcount>");
        var dataList = result.split("<list>");
        var dataPageNav = result.split("<pagenav>");

        $("#" + recordCountID).html("ค้นหาพบ " + dataRecordCount[1] + " รายการ");
        $("#" + listID).html(dataList[1]);

        if (navPageID.length > 0) 
            $("#" + navPageID).html(dataPageNav[1]);
    });
}

function InitTab(subTab) {
    if (subTab == false)
        $(".tab-hidden").hide();

    var contentDataTab = (subTab == false ? "content-data-tabs" : "content-data-subtabs");

    $(document).ready(function () {
        $("." + contentDataTab + " ul li a").click(function () {
            var dropID = $(this).closest("a").attr("id");
            var linkTab = $(this).closest("a").attr("alt");
            var action = "";
            var id = "";
            var trackingStatus = "";

            if (subTab == false) {
                switch (dropID) { 
                    case "link-tab3-cp-tab-user":
                        action = $("#action").val();
                        id = $("#username-hidden").val() + ":" + $("#password-hidden").val();
                        trackingStatus = "";
                        break;
                    case "link-tab3-cp-tab-program":
                    case "link-tab3-cp-tab-interest":
                    case "link-tab3-cp-tab-pay-break-contract":
                    case "link-tab3-cp-tab-scholarship":
                        action = $("#action").val();
                        id = $("#cp1id").val();
                        trackingStatus = "";
                        break;
                    case "link-tab3-cp-trans-break-contract":
                        action = $("#action").val();
                        id = $("#cp1id").val();
                        trackingStatus = $("#trackingstatus").val();
                        break;
                    case "link-tab3-cp-trans-require-contract":
                        action = $("#action").val();
                        id = $("#cp1id").val();
                        trackingStatus = $("#trackingstatus").val();
                        break;
                    case "link-tab2-cp-trans-payment":
                    case "link-tab1-adddetail-cp-trans-payment": 
                    case "link-tab2-adddetail-cp-trans-payment":
                    case "link-tab3-adddetail-cp-trans-payment":
                    case "link-tab2-cp-report-table-cal-capital-and-interest":
                        action = "";
                        id = $("#cp2id").val();
                        trackingStatus = "";
                        break;
                }

                OpenTab(dropID, linkTab, "", true, action, id, trackingStatus);
            }

            if (subTab == true) {
                switch (dropID) {
                    case "link-tab1-adddetail-cp-trans-payment":
                    case "link-tab2-adddetail-cp-trans-payment":
                    case "link-tab3-adddetail-cp-trans-payment":
                        action = "";
                        id = $("#cp2id").val();
                        trackingStatus = "";
                        break;
                }

                OpenSubTab(dropID, linkTab, id);
            }
        });
    });
}

function OpenTab(
    dropID,
    linkTab,
    tabTitle,
    tabHidden,
    action,
    id,
    trackingStatus
) {
    GoToTopElement("html, body");

    $(".tab-content").hide();

    if ($("#" + dropID).hasClass("active") == false) {
        if (tabHidden == true)
            $(".tab-hidden").hide()
        else
            $(".tab-hidden").show();

        if (tabTitle.length > 0)            
            $("#" + dropID).html(tabTitle);

        $(".content-data-tabs ul li a").removeClass("active");
        $("#" + dropID).addClass("active");
    }

    $(linkTab + "-head").show();
    $(linkTab + "-contents").show();
    $(linkTab + "-content").show();

    switch (dropID) {
        case "link-tab1-cp-tab-user":
            SetMsgLoading("กำลังโหลด...");
            SearchCPTabUser();
            break;
        case "link-tab2-cp-tab-user":
            $(".addupdate-data-tab-user").html("");
            LoadForm(1, "addcptabuser", false, "add-data-tab-user", "", "");
            break;
        case "link-tab3-cp-tab-user":
            $(".addupdate-data-tab-user").html("");
            LoadForm(1, "updatecptabuser", false, "update-data-tab-user", id, "");
            break;
        case "link-tab1-cp-tab-program":
            LoadList("cpprogram", "record-count-program", "list-data-tab-program", "");
            break;
        case "link-tab2-cp-tab-program":
            $(".addupdate-data-tab-program").html("");
            LoadForm(1, "addcptabprogram", false, "add-data-tab-program", "", "");
            break;
        case "link-tab3-cp-tab-program":
            $(".addupdate-data-tab-program").html("");
            LoadForm(1, "updatecptabprogram", false, "update-data-tab-program", id, "");
            break;
        case "link-tab1-cp-tab-interest":
            LoadList("interest", "record-count-interest", "list-data-tab-interest", "");
            break;
        case "link-tab2-cp-tab-interest":
            $(".addupdate-data-tab-interest").html("");
            LoadForm(1, "addcptabinterest", false, "add-data-tab-interest", "", "");
            break;
        case "link-tab3-cp-tab-interest":
            $(".addupdate-data-tab-interest").html("");
            LoadForm(1, "updatecptabinterest", false, "update-data-tab-interest", id, "");
            break;
        case "link-tab1-cp-tab-pay-break-contract":
            LoadList("pay-break-contract", "record-count-pay-break-contract", "list-data-tab-pay-break-contract", "");
            break;
        case "link-tab2-cp-tab-pay-break-contract":
            $(".addupdate-data-tab-pay-break-contract").html("");
            LoadForm(1, "addcptabpaybreakcontract", false, "add-data-tab-pay-break-contract", "", "");
            break;
        case "link-tab3-cp-tab-pay-break-contract":
            $(".addupdate-data-tab-pay-break-contract").html("");
            LoadForm(1, "updatecptabpaybreakcontract", false, "update-data-tab-pay-break-contract", id, "");
            break;
        case "link-tab1-cp-tab-scholarship":
            LoadList("scholarship", "record-count-scholarship", "list-data-tab-scholarship", "");
            break;
        case "link-tab2-cp-tab-scholarship":
            $(".addupdate-data-tab-scholarship").html("");
            LoadForm(1, "addcptabscholarship", false, "add-data-tab-scholarship", "", "");
            break;
        case "link-tab3-cp-tab-scholarship":
            $(".addupdate-data-tab-scholarship").html("");
            LoadForm(1, "updatecptabscholarship", false, "update-data-tab-scholarship", id, "");
            break;
        case "link-tab1-cp-trans-break-contract":
        case "link-tab1-cp-trans-require-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchCPTransBreakContract();
            break;
        case "link-tab2-cp-trans-break-contract":
            $(".addupdate-data-trans-break-contract").html("");
            LoadForm(1, "addcptransbreakcontract", false, "add-data-trans-break-contract", "", "");
            break;
        case "link-tab3-cp-trans-break-contract":
            ChkTrackingStatusViewTransBreakContract(id, trackingStatus, ("trans-break-contract" + id), function (result) {
                if (result == "0") {
                    $(".addupdate-data-trans-break-contract").html("");
                    LoadForm(1, "updatecptransbreakcontract", false, "update-data-trans-break-contract", id, "");
                }        
            });
            break;
        case "link-tab2-cp-trans-require-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchRepay();
            break;
        case "link-tab3-cp-trans-require-contract":
            if (action == "add") {
                ChkTrackingStatusViewTransBreakContract(id, trackingStatus, ("trans-break-contract" + id), function (result) {
                    if (result == "0") {
                        $("#addupdate-data-trans-require-contract").html("");
                        LoadForm(1, "addcptransrequirecontract", false, "addupdate-data-trans-require-contract", id, "");
                    }            
                });
            }

            if (action == "update") {
                ChkTrackingStatusViewTransBreakContract(id, trackingStatus, ("trans-break-contract" + id), function (result) {
                    if (result == "0") {
                        $(".addupdate-data-trans-require-contract").html("");
                        LoadForm(1, "updatecptransrequirecontract", false, "addupdate-data-trans-require-contract", id, "");
                    }
                });
            }
            break;
        case "link-tab1-cp-trans-payment":
            SetMsgLoading("กำลังโหลด...");
            SearchPayment();
            break;
        case "link-tab2-cp-trans-payment":
            $("#adddetail-data-trans-payment").html("");
            LoadForm(1, "adddetailcptranspayment", false, "adddetail-data-trans-payment", id, "");
            break;
        case "link-tab1-cp-report-table-cal-capital-and-interest":
            SetMsgLoading("กำลังโหลด...");
            SearchReportTableCalCapitalAndInterest();
            break;
        case "link-tab2-cp-report-table-cal-capital-and-interest":
            $("#cal-data-report-table-cal-capital-and-interest").html("");
            LoadForm(1, "calreporttablecalcapitalandinterest", false, "cal-data-report-table-cal-capital-and-interest", id, "");
            break;
        case "link-tab1-cp-report-statistic-repay":
            LoadList("cpreportstatisticrepay", "record-count-cp-report-statistic-repay", "list-data-report-statistic-repay", "");
            break;
        case "link-tab2-cp-report-statistic-repay":
            SetMsgLoading("กำลังโหลด...");
            SearchReportStatisticRepayByProgram();
            break;
        case "link-tab1-cp-report-statistic-contract":
            LoadList("cpreportstatisticrecontract", "record-count-cp-report-statistic-contract", "list-data-report-statistic-contract", "");
            break;
        case "link-tab2-cp-report-statistic-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchReportStatisticContractByProgram();
            break;
        case "link-tab1-cp-report-debtor-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchReportDebtorContract();
            break;
        case "link-tab2-cp-report-debtor-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchReportDebtorContractByProgram();
            break;
    }
}

function OpenSubTab(
    dropID,
    linkTab,
    id
) {
    $(".subtab-content").hide();

    if ($("#" + dropID).hasClass("active") == false)  {
        $(".content-data-subtabs ul li a").removeClass("active");
        $("#" + dropID).addClass("active");
    }

    $(linkTab + "-head").show();
    $(linkTab + "-contents").show();
    $(linkTab + "-content").show();

    switch (dropID) {
        case "link-tab1-adddetail-cp-trans-payment":
            $(".adddetail-cp-trans-payment-content").html("");
            LoadForm(1, "detailcptranspayment", false, "detail-data-trans-payment", id, "");
            break;
        case "link-tab2-adddetail-cp-trans-payment":
            var formatPayment = $("#format-payment-hidden").val() == "1" ? "fullrepay" : "payrepay";
    
            $(".adddetail-cp-trans-payment-content").html("");
            LoadForm(1, "addcptranspayment" + formatPayment, false, "add-data-trans-payment", id, "");
            break;
        case "link-tab3-adddetail-cp-trans-payment":
            $(".adddetail-cp-trans-payment-content").html("");
            LoadForm(1, "addupdatecptransprosecution", false, "addupdate-data-trans-prosecution", id, "");
            break;
        case "link-tab1-report-student-on-statistic-contract-by-program":
        case "link-tab2-report-student-on-statistic-contract-by-program":
            SetMsgLoading("กำลังโหลด...");

            if ($("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true)
                SearchReportStudentOnStatisticContractByProgram(1);

            if ($("#link-tab2-report-student-on-statistic-contract-by-program").hasClass("active") == true)
                SearchReportStudentOnStatisticContractByProgram(2);
            break;
    }
}

function SetNullCombobox(id) {
    if (ComboboxGetSelectedValue(id) == null) {
        ComboboxSetSelectedValue(id, "0");
        SetSelectCombobox(id, "0");
    }
}

function SetSelectCombobox(
    id,
    value
) {
    if (id == "dlevel" ||
        id == "faculty" ||
        id == "facultycptabprogram" ||
        id == "facultytransbreakcontract" ||
        id == "facultytransrepaycontract" ||
        id == "facultytranspayment" ||
        id == "facultyreporttablecalcapitalandinterest" ||
        id == "facultyreportstepofwork" ||
        id == "facultysearchstudent" ||
        id == "facultyprofilestudent" ||
        id == "facultyreportnoticerepaycomplete" ||
        id == "facultyreportnoticeclaimdebt" ||
        id == "facultyreportstatisticpaymentbydate" ||
        id == "facultyreportecontract") {
        var dlevel;
        var facultyArray;
        var faculty;
        var program;
        var listProgram;
        var comboboxWidthInput = 390;
        var comboboxWidthList = 415;

        if (id == "dlevel") {
            dlevel = value;

            if ($("#faculty").length > 0) {
                facultyArray = ComboboxGetSelectedValue("faculty").split(";");
                faculty = facultyArray[0];
                program = "program";
                listProgram = "list-program";
            }

            if ($("#facultycptabprogram").length > 0) {
                facultyArray = ComboboxGetSelectedValue("facultycptabprogram").split(";");
                faculty = facultyArray[0];
                program = "programcptabprogram";
                listProgram = "list-program";
            }
        }
        
        if (id == "faculty" ||
            id == "facultycptabprogram" ||
            id == "facultytransbreakcontract" ||
            id == "facultytransrepaycontract" ||
            id == "facultytranspayment" ||
            id == "facultyreporttablecalcapitalandinterest" ||
            id == "facultyreportstepofwork" ||
            id == "facultysearchstudent" ||
            id == "facultyprofilestudent" ||
            id == "facultyreportnoticerepaycomplete" ||
            id == "facultyreportnoticeclaimdebt" ||
            id == "facultyreportstatisticpaymentbydate" ||
            id == "facultyreportecontract") {
            dlevel = ($("#dlevel").length > 0 ? ComboboxGetSelectedValue("dlevel") : "");
            faculty = value;
            
            if (id == "faculty") {
                program = "program";
                listProgram = "list-program";
            }

            if (id == "facultycptabprogram") {
                program = "programcptabprogram";
                listProgram = "list-program";
            }

            if (id == "facultytransbreakcontract") {
                program = "programtransbreakcontract";
                listProgram = "list-program-trans-break-contract";
            }

            if (id == "facultytransrepaycontract") {
                program = "programtransrepaycontract";
                listProgram = "list-program-trans-repay-contract";
            }

            if (id == "facultytranspayment") {
                program = "programtranspayment";
                listProgram = "list-program-trans-payment";        
                comboboxWidthInput = 336;
                comboboxWidthList = 361;        
            }

            if (id == "facultyreporttablecalcapitalandinterest") {
                program = "programreporttablecalcapitalandinterest";
                listProgram = "list-program-report-table-cal-capital-and-interest";
            }

            if (id == "facultyreportstepofwork") {
                program = "programreportstepofwork";
                listProgram = "list-program-report-step-of-work";
            }

            if (id == "facultysearchstudent") {                
                program = "programsearchstudent";
                listProgram = "list-program-search-student";
            }

            if (id == "facultyprofilestudent") {
                program = "programprofilestudent";
                listProgram = "list-program-profile-student";
            }

            if (id == "facultyreportnoticerepaycomplete") {
                program = "programreportnoticerepaycomplete";
                listProgram = "list-program-report-notice-repay-complete";
            }

            if (id == "facultyreportnoticeclaimdebt") {
                program = "programreportnoticeclaimdebt";
                listProgram = "list-program-report-notice-claim-debt";
            }

            if (id == "facultyreportstatisticpaymentbydate") {
                program = "programreportstatisticpaymentbydate";
                listProgram = "list-program-report-statistic-payment-by-date";
            }

            if (id == "facultyreportecontract") {
                program = "programreportecontract";
                listProgram = "list-program-report-e-contract";
            }
        }

        LoadCombobox(program, ("dlevel=" + dlevel + "&faculty=" + faculty), listProgram, "0", comboboxWidthInput, comboboxWidthList);
    }

    if (id == "case-graduate") {
        var caseGraduate = value;

        if (caseGraduate == "2")
            $("input[name=set-amt-indemnitor-year]:radio").prop("disabled", false);
        else {
            $("input[name=set-amt-indemnitor-year]:radio").prop({
                "checked": false,
                "disabled": true
            });
            $("#amt-indemnitor-year").val("");
            TextboxDisable("#amt-indemnitor-year");
        }
    }

    if (id == "scholar") {
        var faculty = $("#profile-student-faculty-hidden").val();
        var program = $("#profile-student-program-hidden").val();
        var scholar = value;

        if (scholar == "1") {
            TextboxEnable("#scholarship-money");
            TextboxEnable("#scholarship-year");
            TextboxEnable("#scholarship-month");
            ViewScholarship(faculty, program, scholar);
        }
        else {
            $("#scholarship-money").val("");
            $("#scholarship-year").val("");
            $("#scholarship-month").val("");
            TextboxDisable("#scholarship-money");
            TextboxDisable("#scholarship-year");
            TextboxDisable("#scholarship-month");
        }
    }

    if (id == "case-graduate-break-contract") {
        var faculty = $("#profile-student-faculty-hidden").val();
        var program = $("#profile-student-program-hidden").val();
        var caseGraduateBreakContract = value;

        ComboboxDisable("civil");
        $("#contract-force-date-start").val("");
        $("#contract-force-date-end").val("");
        CalendarDisable("#contract-force-date-start");
        CalendarDisable("#contract-force-date-end");

        if (caseGraduateBreakContract == "1" ||
            caseGraduateBreakContract == "2") {
            if (caseGraduateBreakContract == "2") {
                ComboboxEnable("civil")

                if ($("#education-date-end").val().length > 0) {
                    var contractForceDateStart = $("#education-date-end").datepicker("getDate", "+1d");
                    contractForceDateStart.setDate(contractForceDateStart.getDate() + 1);

                    $("#contract-force-date-start").datepicker("setDate", contractForceDateStart);
                }
                
                InitCalendar("#contract-force-date-start, #contract-force-date-end");
                CalendarEnable("#contract-force-date-start");
                CalendarDisable("#contract-force-date-end");
            }
            else {
                ComboboxSetSelectedValue("civil", "0");
                ComboboxDisable("civil");                    
                $("#contract-force-date-start").val($("#education-date-start").val());
                $("#contract-force-date-end").val($("#education-date-end").val());
                InitCalendarFromTo("#contract-force-date-start", false, "#contract-force-date-end", false);
                CalendarEnable("#contract-force-date-start");
                CalendarEnable("#contract-force-date-end");
            }
        }

        ViewPayBreakContract(faculty, program, caseGraduateBreakContract);
    }

    if (id == "condition-tablecalcapitalandinterest") {
        var conditionTableCalCapitalAndInterest = value;

        if (conditionTableCalCapitalAndInterest == "0") {
            $("#condition-select-0").show();
            $("#pay-period").val("");
            TextboxDisable("#pay-period");
            $("#condition-select-1").hide();
            $("#condition-select-2").hide();
        }
        else {
            $("#condition-select-0").hide();

            if (conditionTableCalCapitalAndInterest == "1") {
                $("#condition-select-1").show();
                $("#pay").val("");
                $("#condition-select-2").hide();
            }
            else {
                $("#condition-select-1").hide();
                $("#condition-select-2").show();
                $("#period").val("");
            }
        }
    }
}

function SetSelectDefaultCombobox(id) {
    if (id == "faculty" ||
        id == "facultycptabprogram" ||
        id == "facultytransbreakcontract" ||
        id == "facultytransrepaycontract" ||
        id == "facultytranspayment" ||
        id == "facultyreporttablecalcapitalandinterest" ||
        id == "facultyreportstepofwork" ||
        id == "facultysearchstudent" ||
        id == "facultyprofilestudent" ||
        id == "facultyreportnoticerepaycomplete" ||
        id == "facultyreportnoticeclaimdebt" ||
        id == "facultyreportstatisticpaymentbydate" ||
        id == "facultyreportecontract") {
        var dlevel = ($("#dlevel").length > 0 ? ComboboxGetSelectedValue("dlevel") : "");
        var facultyArray;
        var faculty;
        var program;
        var programValue;
        var listProgram;
        var comboboxWidthInput = 390;
        var comboboxWidthList = 415;

        if (id == "faculty") {
            facultyArray = ComboboxGetSelectedValue("faculty").split(";");
            faculty = facultyArray[0];
            program = "program";
            listProgram = "list-program";
            programValue = "program-hidden";
        }

        if (id == "facultycptabprogram") {
            facultyArray = ComboboxGetSelectedValue("facultycptabprogram").split(";");
            faculty = facultyArray[0];
            program = "programcptabprogram";
            listProgram = "list-program";
            programValue = "program-hidden";
        }

        if (id == "facultytransbreakcontract") {
            facultyArray = ComboboxGetSelectedValue("facultytransbreakcontract").split(";");
            faculty = facultyArray[0];
            program = "programtransbreakcontract";
            listProgram = "list-program-trans-break-contract";
            programValue = "program-trans-break-contract-hidden";
        }

        if (id == "facultytransrepaycontract") {
            facultyArray = ComboboxGetSelectedValue("facultytransrepaycontract").split(";");
            faculty = facultyArray[0];
            program = "programtransrepaycontract";
            listProgram = "list-program-trans-repay-contract";
            programValue = "program-trans-repay-contract-hidden";
        }

        if (id == "facultytranspayment") {
            facultyArray = ComboboxGetSelectedValue("facultytranspayment").split(";");
            faculty = facultyArray[0];
            program = "programtranspayment";
            listProgram = "list-program-trans-payment";
            programValue = "program-trans-payment-hidden";
            comboboxWidthInput = 336;
            comboboxWidthList = 361;
        }

        if (id == "facultyreporttablecalcapitalandinterest") {
            facultyArray = ComboboxGetSelectedValue("facultyreporttablecalcapitalandinterest").split(";");
            faculty = facultyArray[0];
            program = "programreporttablecalcapitalandinterest";
            listProgram = "list-program-report-table-cal-capital-and-interest";
            programValue = "program-report-table-cal-capital-and-interest-hidden";
        }

        if (id == "facultyreportstepofwork") {
            facultyArray = ComboboxGetSelectedValue("facultyreportstepofwork").split(";");
            faculty = facultyArray[0];
            program = "programreportstepofwork";
            listProgram = "list-program-report-step-of-work";
            programValue = "program-report-step-of-work-hidden";
        }

        if (id == "facultysearchstudent") {
            faculty = "0";
            program = "programsearchstudent";
            listProgram = "list-program-search-student";
            programValue = "";
        }

        if (id == "facultyprofilestudent") {
            facultyArray = ComboboxGetSelectedValue("facultyprofilestudent").split(";");
            faculty = facultyArray[0];
            program = "programprofilestudent";
            listProgram = "list-program-profile-student";
            programValue = "profile-student-program-hidden";
        }

        if (id == "facultyreportnoticerepaycomplete") {
            facultyArray = ComboboxGetSelectedValue("facultyreportnoticerepaycomplete").split(";");
            faculty = facultyArray[0];
            program = "programreportnoticerepaycomplete";
            listProgram = "list-program-report-notice-repay-complete";
            programValue = "program-report-notice-repay-complete-hidden";
        }

        if (id == "facultyreportnoticeclaimdebt") {
            facultyArray = ComboboxGetSelectedValue("facultyreportnoticeclaimdebt").split(";");
            faculty = facultyArray[0];
            program = "programreportnoticeclaimdebt";
            listProgram = "list-program-report-notice-claim-debt";
            programValue = "program-report-notice-claim-debt-hidden";
        }

        if (id == "facultyreportstatisticpaymentbydate") {
            facultyArray = ComboboxGetSelectedValue("facultyreportstatisticpaymentbydate").split(";");
            faculty = facultyArray[0];
            program = "programreportstatisticpaymentbydate";
            listProgram = "list-program-report-statistic-payment-by-date";
            programValue = "program-report-statistic-payment-by-date-hidden";
        }

        if (id == "facultyreportecontract") {
            facultyArray = ComboboxGetSelectedValue("facultyreportecontract").split(";");
            faculty = facultyArray[0];
            program = "programreportecontract";
            listProgram = "list-program-report-e-contract";
            programValue = "program-report-e-contract-hidden";
        }

        LoadCombobox(program, ("dlevel=" + dlevel + "&faculty=" + faculty), listProgram, (faculty != "0" ? ($("#" + programValue).val().length > 0 ? $("#" + programValue).val() : "0") : "0"), comboboxWidthInput, comboboxWidthList);
    }

    if (id == "case-graduate") {
        if (ComboboxGetSelectedValue("case-graduate") == "2") {
            $("input[name=set-amt-indemnitor-year]:radio").prop("disabled", false);
            TextboxEnable("#amt-indemnitor-year");        
        }
        else {
            $("input[name=set-amt-indemnitor-year]:radio").prop({
                "checked": false,
                "disabled": true
            });
            $("#amt-indemnitor-year").val("");
            TextboxDisable("#amt-indemnitor-year");
        }
    }

    if (id == "case-graduate-break-contract") {
        CalendarDisable("#contract-force-date-start");
        CalendarDisable("#contract-force-date-end");

        if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1" ||
            ComboboxGetSelectedValue("case-graduate-break-contract") == "2") {
            if (ComboboxGetSelectedValue("case-graduate-break-contract") == "2") {
                InitCalendar("#contract-force-date-start, #contract-force-date-end");
                CalendarEnable("#contract-force-date-start");
                CalendarDisable("#contract-force-date-end");
            }
            else {
                InitCalendarFromTo("#contract-force-date-start", false, "#contract-force-date-end", false);
                CalendarEnable("#contract-force-date-start");
                CalendarEnable("#contract-force-date-end");
            }
        }
    }

    if (id == "scholar") {
        if (ComboboxGetSelectedValue("scholar") == "1") {
            TextboxEnable("#scholarship-money");
            TextboxEnable("#scholarship-year");
            TextboxEnable("#scholarship-month");
        }
        else {
            $("#scholarship-money").val("");
            $("#scholarship-year").val("");
            TextboxDisable("#scholarship-money");
            TextboxDisable("#scholarship-year");
            TextboxDisable("#scholarship-month");
        }
    }

    if (id == "civil") {
        if (ComboboxGetSelectedValue("case-graduate-break-contract") == "2")
            ComboboxEnable("civil")
        else {
            ComboboxSetSelectedValue("civil", "0");
            ComboboxDisable("civil");
        }
    }

    if (id == "condition-tablecalcapitalandinterest") {
        if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "0") {
            $("#condition-select-0").show();
            $("#pay-period").val("");
            TextboxDisable("#pay-period");
            $("#condition-select-1").hide();
            $("#condition-select-2").hide();
        }
    }
}

function InitComboboxOnClick(id) {
    $(document).ready(function () {
        $("." + id + "-combobox-input").autocomplete({
            change: function (event, ui) {
                SetNullCombobox(id);
            },
            close: function (event, ui) {
                SetNullCombobox(id);
            }
        });

        $("#" + id).combobox({
            selected: function (event, ui) {
                SetSelectCombobox(id, ui.item.value);
            }
        });

        SetSelectDefaultCombobox(id)
    });
}

function LoadForm(
    frmIndex,
    frm,
    dialogFrm,
    frmID,
    id,
    idActive
) {
    if (idActive.length > 0)
        $("#" + idActive).addClass("active");

    if (dialogFrm == false)
        $("#" + frmID).html("");

    var send = new Array();
    send[send.length] = "action=form";
    send[send.length] = ("frm=" + frm);
    send[send.length] = ("id=" + id);
    
    SetMsgLoading("กำลังโหลด...");

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
        var dataForm = result.split("<form>");
        var dataWidth = result.split("<width>");
        var dataHeight = result.split("<height>");
        var dataTitle = result.split("<title>");
        var height = parseInt(dataHeight[1]);

        if (dataForm[1].length == 0) {
            DialogMessage("ไม่พบข้อมูล", "", false, idActive);

            /*
            if (idActive.length > 0)
                $("#" + idActive).removeClass("active");
            */

            return;
        }

        if (dialogFrm == true) {
            if (frm == "manual")
                height = ($(window).height() - 40);

            DialogForm(frmIndex, dataForm[1], parseInt(dataWidth[1]), height, dataTitle[1], idActive, "");

            if (frm == "manual") {
                $("#dialog-form" + frmIndex).dialog({
                    close: function () {
                        $(".dialog-overlay" + frmIndex).remove();
                        $("body").css("overflow", "auto");

                        $("#menu7").removeClass("active").addClass("noactive");
                        $("#menu" + oldMenu).removeClass("noactive").addClass("active");
                    }
                });    
            }
        }
        else
            $("#" + frmID).html(dataForm[1]);

        switch (frm) {
            case "searchcptabuser":
                ResetFrmSearchCPTabUser("");
                break;
            case "addcptabuser":
            case "updatecptabuser":
                InitCPTabUser();
                ResetFrmCPTabUser(false);
                break;
            case "addcptabprogram":
            case "updatecptabprogram":
                ResetFrmCPTabProgram(false);
                break;
            case "addcptabinterest":
            case "updatecptabinterest":
                ResetFrmCPTabInterest(false);
                break;
            case "addcptabpaybreakcontract":
            case "updatecptabpaybreakcontract":
                InitSetAmtIndemnitorYear();
                ResetFrmCPTabPayBreakContract(false);
                break;
            case "addcptabscholarship":
            case "updatecptabscholarship":
                ResetFrmCPTabScholarship(false);
                break;
            case "addprofilestudent":
                ResetFrmAddProfileStudent();
                break;
            case "searchstudentwithresult":
                ResetFrmSearchStudentWithResult();
                break;
            case "searchcptransbreakcontract":
                ResetFrmSearchCPTransBreakContract("");
                break;
            case "addcptransbreakcontract":
            case "updatecptransbreakcontract":
                ResetFrmCPTransBreakContract(false);
                break;
            case "addcptransrequirecontract":
            case "updatecptransrequirecontract":
                InitStudyLeaveYesNo();
                InitCPTransRequireContract();
                ResetFrmCPTransRequireContract(false);
                break;
            case "searchcptransrepaycontract":
                ResetFrmSearchCPTransRepayContract("");
                break;
            case "addupdaterepaycontract":
                ResetFrmCPTransRepayContract(false);
                break;
            case "calinterest":
                ResetFrmCalInterestOverpayment(false);
                break;
            case "selectformatpayment":
                ResetFrmSelectFormatPayment();
                break;
            case "adddetailcptranspayment":
                InitTab(true);
                InitStatusPaymentRecord();
                ResetFormatPayment();
                ResetListTransPayment();
                break;
            case "searchcptranspayment":
                ResetFrmSearchCPTransPayment("");
                break;
            case "detailcptranspayment":
                GoToTopElement("html, body");
                InitStatusPaymentRecord();
                ResetListTransPayment();
                break;
            case "addcptranspaymentfullrepay":
            case "addcptranspaymentpayrepay":
                InitCalInterestYesNo();
                InitPayChannel();
                InitReceiptCopy();
                ResetFrmAddCPTransPayment();
                break;
            case "addupdatecptransprosecution":
                InitCPTransProsecution();
                break;
            case "detailtranspayment":
                ResetDetailTransPayment();
                break;
            case "adddetailpaychannel":
                ResetFrmAddDetailPayChannel();
                break;
            case "chkbalance":
                CalculateChkBalance();
                break;
            case "calreporttablecalcapitalandinterest":
                GoToTopElement("html, body");
                ResetFrmCalReportTableCalCapitalAndInterest();
                break;
            case "searchcpreporttablecalcapitalandinterest":
                ResetFrmSearchCPReportTableCalCapitalAndInterest("");
                break;
            case "searchcpreportstepofwork":
                ResetFrmSearchCPReportStepOfWork("");
                break;
            case "reportstepofworkonstatisticrepaybyprogram":
                ResetFrmSearchReportStepOfWorkOnStatisticRepayByProgram();
                break;
            case "reportstudentonstatisticcontractbyprogram":
                InitTab(true);
                ResetFrmSearchReportStudentOnStatisticContractByProgram();
                break;
            case "searchcpreportnoticerepaycomplete":
                ResetFrmSearchCPReportNoticeRepayComplete("");
                break;
            case "searchcpreportnoticeclaimdebt":
                ResetFrmSearchCPReportNoticeClaimDebt("");
                break;
            case "searchcpreportpaymentbydate":
                ResetFrmSearchCPReportStatisticPaymentByDate("");
                break;
            case "manual":
                InitSlide(".class-dialog-form-manual .ui-dialog-content", manual, true, "Manual/UserManual.zip");
                break;
            case "searchcpreportecontract":
                ResetFrmSearchCPReportEContract("");
                break;
            case "searchcpreportdebtorcontract":
                ResetFrmSearchCPReportDebtorContract("");
                break;
            case "searchstudentdebtorcontractbyprogram":
                ResetFrmSearchStudentDebtorContractByProgram("");
                break;
        }

        InitTextSelect();
    });
}

function AddUpdateData(
    action,
    cmd,
    valueSend,
    listUpdate,
    recordCount,
    listData,
    navPage,
    closeFrm,
    callbackFunc
) {
    var msgAction = (action == "add" || action == "update" ? "บันทึก" : "ลบ");
    var send = new Array();
    send[send.length] = ("action=" + action);
    send[send.length] = ("cmd=" + cmd);

    if (valueSend.length > 0) {
        for (var i = 0; i < valueSend.length; i++) {
            send[send.length] = valueSend[i];
        }
    }

    SetMsgLoading("กำลัง" + msgAction + "...");

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (result) {
        var dataError = result.split("<error>");
        var dataRecordCount = result.split("<recordcount>");
        var dataList = result.split("<list>");
        var dataPageNav = result.split("<pagenav>");

        if (dataError[1] == "0") {
            if (closeFrm == true)
                $("#dialog-form1").dialog("close");

            if (listUpdate == true) {
                $("#" + recordCount).html("ค้นหาพบ " + dataRecordCount[1] + " รายการ");
                $("#" + listData).html(dataList[1]);

                if (navPage.length > 0)
                    $("#" + navPage).html(dataPageNav[1]);
            }
        }

        callbackFunc(dataError[1]);
    });
}

function SearchData(
    from,
    valueSend,
    recordSearch,
    listSearch,
    navPage
) {
    $("#" + recordSearch).html("");

    if (from != "studentwithresult")
        $("#" + listSearch).html("");

    $("#" + navPage).html("");

    var send = new Array();
    send[send.length] = "action=search";
    send[send.length] = ("from=" + from);
    
    if (valueSend.length > 0) {
        var j = 2;

        for (var i = 0; i < valueSend.length; i++) {
            send[j] = valueSend[i];
            j++;
        }
    }
    
    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", (msgLoading.length > 0 ? true : false), (msgLoading.length > 0 ? true : false), function (result) {
        var dataRecordCount = result.split("<recordcount>");
        var dataList = result.split("<list>");
        var dataPageNav = result.split("<pagenav>");

        $("#" + recordSearch).html("ค้นหาพบ " + dataRecordCount[1] + " รายการ");
        $("#" + listSearch).html(dataList[1]);
        $("#" + navPage).html(dataPageNav[1]);
    });
}

function ViewData(
    from,
    valueSend,
    callbackFunc
) {
    var send = new Array();
    send[send.length] = "action=search";
    send[send.length] = ("from=" + from);

    if (valueSend.length > 0) {
        var j = 2;

        for (var i = 0; i < valueSend.length; i++) {
            send[j] = valueSend[i];
            j++;
        }
    }

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", (msgLoading.length > 0 ? true : false), (msgLoading.length > 0 ? true : false), function (result) {
        callbackFunc(result);
    });
}

function PageNav(
    pageContent,
    currentPage,
    startRow,
    endRow
) {
    var send = new Array();
    send[send.length] = ("currentpage=" + currentPage);
    send[send.length] = ("startrow=" + startRow);
    send[send.length] = ("endrow=" + endRow);

    if (pageContent == "tabuser") {
        send[send.length] = ("name=" + $("#name-tab-user-hidden").val());

        SetMsgLoading("กำลังโหลด...");

        SearchData("tabuser", send, "record-count-cp-tab-user", "list-data-tab-user", "nav-page-tab-user");
    }
    
    if (pageContent == "studentwithresult") {
        var faculty = (ComboboxGetSelectedValue("facultysearchstudent") != "0" ? ComboboxGetSelectedValue("facultysearchstudent") : "");
        var program = (ComboboxGetSelectedValue("programsearchstudent") != "0" ? ComboboxGetSelectedValue("programsearchstudent") : "");

        faculty = (faculty.length > 0 ? faculty.split(";") : "");
        program = (program.length > 0 ? program.split(";") : "");

        send[send.length] = ("studentid=" + $("#id-name-search-student").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

        SetMsgLoading("กำลังโหลด...");

        SearchData("studentwithresult", send, "record-count-student-with-result", "list-data-search-student-with-result", "nav-page-search-student-with-result");
    }

    if (pageContent == "transbreakcontract") {
        var faculty = ($("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val().split(";") : "");
        var program = ($("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val().split(";") : "");

        send[send.length] = ("statussend=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "1" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "1" : "")));
        send[send.length] = ("statusreceiver=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "2" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "3" ? "2" : "")));
        send[send.length] = ("statusedit=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "4" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : "")));
        send[send.length] = ("statuscancel=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "5" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : "")));
        send[send.length] = ("studentid=" + $("#id-name-trans-break-contract-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("datestart=" + $("#date-start-trans-break-contract-hidden").val());
        send[send.length] = ("dateend=" + $("#date-end-trans-break-contract-hidden").val());

        SetMsgLoading("กำลังโหลด...");

        SearchData("transbreakcontract", send, "record-count-cp-trans-break-contract", "list-data-trans-break-contract", "nav-page-trans-break-contract");
    }

    if (pageContent == "transrepaycontract") {
        var faculty = ($("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val().split(";") : "");        
        var program = ($("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val().split(";") : "");
        var statusRepay = SetStatusRepay();

        send[send.length] = ("statusrepay=" + statusRepay[0]);
        send[send.length] = ("statusreply=" + statusRepay[1]);
        send[send.length] = ("replyresult=" + statusRepay[2]);
        send[send.length] = ("statuspayment=" + statusRepay[3]);
        send[send.length] = ("studentid=" + $("#id-name-trans-repay-contract-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("datestart=" + $("#date-start-trans-repay-contract-hidden").val());
        send[send.length] = ("dateend=" + $("#date-end-trans-repay-contract-hidden").val());

        SetMsgLoading("กำลังโหลด...");

        SearchData("transrepaycontract", send, "record-count-cp-trans-repay-contract", "list-data-trans-repay-contract", "nav-page-trans-repay-contract");
    }

    if (pageContent == "transpayment") {
        var faculty = ($("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val().split(";") : "");
        var program = ($("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val().split(";") : "");

        send[send.length] = ("statuspayment=" + $("#paymentstatus-trans-payment-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-trans-payment-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("datestart=" + $("#date-start-trans-repay1-reply-hidden").val());
        send[send.length] = ("dateend=" + $("#date-end-trans-repay1-reply-hidden").val());

        SetMsgLoading("กำลังโหลด...");

        SearchData("transpayment", send, "record-count-cp-trans-payment", "list-data-trans-payment", "nav-page-trans-payment");
    }

    if (pageContent == "reporttablecalcapitalandinterest") {
        var faculty = ($("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val().split(";") : "");
        var program = ($("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val().split(";") : "");

        send[send.length] = ("studentid=" + $("#id-name-report-table-cal-capital-and-interest-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

        SetMsgLoading("กำลังโหลด...");

        SearchData("reporttablecalcapitalandinterest", send, "record-count-cp-report-table-cal-capital-and-interest", "list-data-report-table-cal-capital-and-interest", "nav-page-report-table-cal-capital-and-interest");
    }

    if (pageContent == "reportstepofwork") {
        var faculty = ($("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val().split(";") : "");
        var program = ($("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val().split(";") : "");

        send[send.length] = ("statusstepofwork=" + $("#stepofworkstatus-report-step-of-work-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-report-step-of-work-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
       
        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstepofwork", send, "record-count-cp-report-step-of-work", "list-data-report-step-of-work", "nav-page-report-step-of-work");
    }

    if (pageContent == "reportstepofworkonstatisticrepaybyprogram") {
        var faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
        var program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");

        send[send.length] = ("acadamicyear=" + $("#acadamicyear-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-search-report-step-of-work").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

        SetMsgLoading("กำลังโหลด...");
        
        SearchData("reportstepofworkonstatisticrepaybyprogram", send, "record-count-report-step-of-work", "list-data-search-report-step-of-work", "nav-page-search-report-step-of-work");
    }

    if (pageContent == "reportstudentonstatisticcontractbyprogram") {
        var faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
        var program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");
        var searchTab = ($("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true ? 1 : 2);

        send[send.length] = ("acadamicyear=" + $("#acadamicyear-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-search-report-student-contract").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("searchtab=" + searchTab);

        var idRecordCount;
        var idListSearch;
        var idNavPageSearch;

        if (searchTab == 1) {
            idRecordCount = "record-count-student-sign-contract";
            idListSearch = "list-data-student-sign-contract";
            idNavPageSearch = "nav-page-student-sign-contract";
        }

        if (searchTab == 2) {
            idRecordCount = "record-count-student-contract-penalty";
            idListSearch = "list-data-student-contract-penalty";
            idNavPageSearch = "nav-page-student-contract-penalty";
        }

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstudentonstatisticcontractbyprogram", send, idRecordCount, idListSearch, idNavPageSearch);
    }

    if (pageContent == "reportnoticerepaycomplete") {
        var faculty = $("#faculty-report-notice-repay-complete-hidden").val().split(";");
        var program = $("#program-report-notice-repay-complete-hidden").val().split(";");

        send[send.length] = ("studentid=" + $("#id-name-report-notice-repay-complete-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportnoticerepaycomplete", send, "record-count-cp-report-notice-repay-complete", "list-data-report-notice-repay-complete", "nav-page-report-notice-repay-complete");
    }

    if (pageContent == "reportnoticeclaimdebt") {
        var faculty = $("#faculty-report-notice-claim-debt-hidden").val().split(";");
        var program = $("#program-report-notice-claim-debt-hidden").val().split(";");

        send[send.length] = ("studentid=" + $("#id-name-report-notice-claim-debt-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportnoticeclaimdebt", send, "record-count-cp-report-notice-claim-debt", "list-data-report-notice-claim-debt", "nav-page-report-notice-claim-debt");
    }

    if (pageContent == "reportstatisticpaymentbydate") {
        var faculty = $("#faculty-report-statistic-payment-by-date-hidden").val().split(";");
        var program = $("#program-report-statistic-payment-by-date-hidden").val().split(";");
        var formatPayment = $("#format-payment-report-statistic-payment-by-date-hidden").val().split(";");

        send[send.length] = ("studentid=" + $("#id-name-report-statistic-payment-by-date-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));
        send[send.length] = ("datestart=" + $("#date-start-report-statistic-payment-by-date-hidden").val());
        send[send.length] = ("dateend=" + $("#date-end-report-statistic-payment-by-date-hidden").val());

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstatisticpaymentbydate", send, "record-count-cp-report-statistic-payment-by-date", "list-data-report-statistic-payment-by-date", "nav-page-report-statistic-payment-by-date");
    }
    
    if (pageContent == "reportecontract") {
        var faculty = ($("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val().split(";") : "");
        var program = ($("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val().split(";") : "");

        send[send.length] = ("acadamicyear=" + $("#acadamicyear-report-e-contract-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-report-e-contract-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        /*
        send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));
        */

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportecontract", send, "record-count-cp-report-e-contract", "list-data-report-e-contract", "nav-page-report-e-contract");
    }

    if (pageContent == "reportdebtorcontractbyprogram") {
        var reportOrder = $("#report-debtor-contract-order").val();
        var faculty = ($("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val().split(";") : "");
        var program = ($("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val().split(";") : "");
        var formatPayment = ($("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "");

        send[send.length] = ("reportorder=" + reportOrder);
        send[send.length] = ("datestart=" + $("#date-start-report-debtor-contract-hidden").val());
        send[send.length] = ("dateend=" + $("#date-end-report-debtor-contract-hidden").val());
        send[send.length] = ("studentid=" + $("#id-name-report-debtor-contract-by-program-hidden").val());
        send[send.length] = ("faculty=" + (faculty[0] != null ? faculty[0] : ""));
        send[send.length] = ("programcode=" + (program[0] != null ? program[0] : ""));
        send[send.length] = ("majorcode=" + (program[2] != null ? program[2] : ""));
        send[send.length] = ("groupnum=" + (program[3] != null ? program[3] : ""));
        send[send.length] = ("formatpayment=" + (formatPayment[0] != null ? formatPayment[0] : ""));

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportdebtorcontractbyprogram", send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
    }
}

function CalculateFrm(
    cal,
    valueSend,
    callbackFunc
) {
    var send = new Array();
    send[send.length] = "action=calculate";
    send[send.length] = ("cal=" + cal);

    if (valueSend.length > 0) {
        var j = 2;

        for (var i = 0; i < valueSend.length; i++) {
            send[j] = valueSend[i];
            j++;
        }
    }

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", (msgLoading.length > 0 ? true : false), (msgLoading.length > 0 ? true : false), function (result) {
        callbackFunc(result);
    });
}

function CloseFrm(
    dialogFrm,
    frmClose
) {
    if (dialogFrm == true) {
        if ($("#dialog-form2").dialog("isOpen") == true) {
            $("#dialog-form2").dialog("close");
            return;
        }

        if ($("#dialog-form1").dialog("isOpen") == true) {
            $("#dialog-form1").dialog("close");
            return;
        }
    }

    switch (frmClose) {
        case "addupdate-cp-tab-user":
            $("#link-tab1-cp-tab-user").click();
            break;
        case "addupdate-cp-tab-program":
            $("#link-tab1-cp-tab-program").click();
            break;
        case "addupdate-cp-tab-interest":
            $("#link-tab1-cp-tab-interest").click();
            break;
        case "addupdate-cp-tab-pay-break-contract":
            $("#link-tab1-cp-tab-pay-break-contract").click();
            break;
        case "addupdate-cp-tab-scholarship":
            $("#link-tab1-cp-tab-scholarship").click();
            break;
        case "addupdate-cp-trans-break-contract":
            $("#link-tab1-cp-trans-break-contract").click();
            break;
        case "addupdate-cp-trans-require-contract":
            $("#link-tab1-cp-trans-require-contract").click();
            break;
        case "add-cp-trans-payment":
            $("#link-tab1-adddetail-cp-trans-payment").click();
            break;
        case "cal-report-table-cal-capital-and-interest":
            $("#link-tab1-cp-report-table-cal-capital-and-interest").click();
            break;
    }
}

function ValidateCalInterestOverpayment() {
    var error = false;
    var msg;
    var focus;
    var overpayment = $("#overpayment-hidden").val();

    if (parseInt(overpayment) > 0) {
        var totalInterestOverpayment = DelCommas("total-interest-overpayment");

        if (error == false &&
            (totalInterestOverpayment.length == 0 || totalInterestOverpayment == "0.00")) {
            error = true;
            msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระ";
            focus = "";
        }

        if (error == false &&
            $("#overpayment-date-start-old").val() != $("#overpayment-date-start").val()) {
            error = true;
            msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";
            focus = "";
        }

        if (error == false &&
            $("#overpayment-date-end-old").val() != $("#overpayment-date-end").val()) {
            error = true;
            msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";
            focus = "";
        }

        if (error == false &&
            $("#overpayment-interest-old").val() != DelCommas("overpayment-interest")) {
            error = true;
            msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";
            focus = "";
        }
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalInterestOverpayment() {
    if (CalculateInterestOverpayment() == true) {
        $("#overpayment-date-start-old").val($("#overpayment-date-start").val());
        $("#overpayment-date-end-old").val($("#overpayment-date-end").val());
        $("#overpayment-interest-old").val(DelCommas("overpayment-interest"));
    }
}

function ValidateCalculateInterestOverpayment() {
    var error = false;
    var msg;
    var focus;
    var repayDateEnd = GetDateObject($("#repay-date-end-hidden").val());
    var overpaymentDateStart = GetDateObject($("#overpayment-date-start").val());

    if (error == false &&
        DateDiff(repayDateEnd, overpaymentDateStart, "days") <= 0) {
        error = true;
        msg = "กรุณาใส่วันที่เริ่มผิดนัดชำระให้ถูกต้อง";
        focus = "#overpayment-date-start";
    }

    if (error == false &&
        ($("#overpayment-interest").val().length == 0 || $("#overpayment-interest").val() == "0.00")) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ยผิดนัดชำระ";
        focus = "#overpayment-interest";
    }

    if (error == false &&
        parseFloat($("#overpayment-interest").val()) > 100) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ยผิดนัดชำระไม่เกิน 100";
        focus = "#overpayment-interest";
    }

    if (error == true) {
        FillCalInterestOverpayment("");
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalculateInterestOverpayment() {
    if (ValidateCalculateInterestOverpayment() == true) {        
        var send = new Array();
        send[send.length] = ("capital=" + DelCommas("capital"));
        send[send.length] = ("overpaymentyear=" + DelCommas("overpayment-year"));
        send[send.length] = ("overpaymentday=" + DelCommas("overpayment-day"));
        send[send.length] = ("overpaymentinterest=" + DelCommas("overpayment-interest"));
        send[send.length] = ("overpaymentdatestart=" + $("#overpayment-date-start").val());
        send[send.length] = ("overpaymentdateend=" + $("#overpayment-date-end").val());
        send[send.length] = ("totalinterestpayrepay=" + ($("#total-interest-pay-repay").length > 0 ? ($("#total-interest-pay-repay").val().length > 0 ? DelCommas("total-interest-pay-repay") : "0.00") : "0.00"));
        send[send.length] = ("totalaccruedinterest=" + ($("#total-accrued-interest").length > 0 ? DelCommas("total-accrued-interest") : "0.00"));

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestoverpayment", send, function (result) {
            FillCalInterestOverpayment(result);
        });

        return true;
    }

    return false;
}

function FillCalInterestOverpayment(result) {
    if ($("#overpayment-year").length > 0)
        $("#overpayment-year").val("");

    if ($("#overpayment-day").length > 0)
        $("#overpayment-day").val("");

    if ($("#total-interest-overpayment").length > 0)
        $("#total-interest-overpayment").val("");

    if ($("#total-interest").length > 0)
        $("#total-interest").val("");

    if ($("#total-payment").length > 0)
        $("#total-payment").val("");

    if ($("#pay").length > 0)
        $("#pay").val("");

    if (result.length > 0) {
        var dataOverpaymentYear = result.split("<overpaymentyear>");
        var dataOverpaymentDay = result.split("<overpaymentday>");
        var dataTotalInterestOverpayment = result.split("<totalinterestoverpayment>");
        var dataTotalInterest = result.split("<totalinterest>");
        var dataTotalPayment = result.split("<totalpayment>");

        if ($("#overpayment-year").length > 0)
            $("#overpayment-year").val(dataOverpaymentYear[1]);

        if ($("#overpayment-day").length > 0)
            $("#overpayment-day").val(dataOverpaymentDay[1]);

        if ($("#total-interest-overpayment").length > 0)
            $("#total-interest-overpayment").val(dataTotalInterestOverpayment[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(dataTotalPayment[1]);

        if ($("#pay").length > 0)
            $("#pay").val(dataTotalPayment[1]);
    }
}

function ValidateCalInterestPayRepay() {
    var error = false;
    var msg;
    var focus;
    var totalInterestPayRepay = DelCommas("total-interest-pay-repay");

    if (error == false &&
        (totalInterestPayRepay.length == 0 || totalInterestPayRepay == "0.00")) {
        error = true;
        msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระ";
        focus = "";
    }

    if (error == false &&
        $("#pay-repay-date-end-old").val() != $("#pay-repay-date-end").val()) {
        error = true;
        msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระใหม่อีกครั้ง";
        focus = "";
    }

    if (error == false &&
        $("#pay-repay-interest-old").val() != DelCommas("pay-repay-interest")) {
        error = true;
        msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระใหม่อีกครั้ง";
        focus = "";
    }

    if (error == true) {
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalInterestPayRepay() {
    if (CalculateInterestPayRepay() == true) {
        $("#pay-repay-date-end-old").val($("#pay-repay-date-end").val());
        $("#pay-repay-interest-old").val(DelCommas("pay-repay-interest"));
    }
}

function ValidateCalculateInterestPayRepay() {
    var error = false;
    var msg;
    var focus;

    if (error == false &&
        ($("#pay-repay-interest").val().length == 0 || $("#pay-repay-interest").val() == "0.00")) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ยผ่อนชำระ";
        focus = "#pay-repay-interest";
    }

    if (error == false &&
        parseFloat($("#pay-repay-interest").val()) > 100) {
        error = true;
        msg = "กรุณาใส่อัตราดอกเบี้ยผ่อนชำระไม่เกิน 100";
        focus = "#pay-repay-interest";
    }

    if (error == true) {
        FillCalInterestPayRepay("");
        DialogMessage(msg, focus, false, "");
        return false;
    }

    return true;
}

function CalculateInterestPayRepay() {
    if (ValidateCalculateInterestPayRepay() == true) {        
        var send = new Array();
        send[send.length] = ("capital=" + DelCommas("capital"));
        send[send.length] = ("payrepayyear=" + DelCommas("pay-repay-year"));
        send[send.length] = ("payrepayday=" + DelCommas("pay-repay-day"));
        send[send.length] = ("payrepayinterest=" + DelCommas("pay-repay-interest"));
        send[send.length] = ("payrepaydatestart=" + $("#pay-repay-date-start").val());
        send[send.length] = ("payrepaydateend=" + $("#pay-repay-date-end").val());
        send[send.length] = ("totalinterestoverpayment=" + ($("#total-interest-overpayment").length > 0 ? ($("#total-interest-overpayment").val().length > 0 ? DelCommas("total-interest-overpayment") : "0.00") : "0.00"));
        send[send.length] = ("totalaccruedinterest=" + ($("#total-accrued-interest").length > 0 ? DelCommas("total-accrued-interest") : "0.00"));

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestpayrepay", send, function (result) {
            FillCalInterestPayRepay(result);
        });

        return true;
    }

    return false;
}

function FillCalInterestPayRepay(result) {
    if ($("#pay-repay-year").length > 0)
        $("#pay-repay-year").val("");

    if ($("#pay-repay-day").length > 0)
        $("#pay-repay-day").val("");

    if ($("#total-interest-pay-repay").length > 0)
        $("#total-interest-pay-repay").val("");

    if ($("#total-interest").length > 0)
        $("#total-interest").val("");

    if ($("#total-payment").length > 0)
        $("#total-payment").val("");

    if ($("#pay").length > 0)
        $("#pay").val("");

    if (result.length > 0) {
        var dataPayRepayYear = result.split("<payrepayyear>");
        var dataPayRepayDay = result.split("<payrepayday>");
        var dataTotalInterestPayRepay = result.split("<totalinterestpayrepay>");
        var dataTotalInterest = result.split("<totalinterest>");
        var dataTotalPayment = result.split("<totalpayment>");

        if ($("#pay-repay-year").length > 0)
            $("#pay-repay-year").val(dataPayRepayYear[1]);

        if ($("#pay-repay-day").length > 0)
            $("#pay-repay-day").val(dataPayRepayDay[1]);

        if ($("#total-interest-pay-repay").length > 0)
            $("#total-interest-pay-repay").val(dataTotalInterestPayRepay[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(dataTotalPayment[1]);

        if ($("#pay").length > 0)
            $("#pay").val(dataTotalPayment[1]);
    }
}

function CalculateInterestOverpaymentAndPayRepay() {
    if (ValidateCalculateInterestOverpayment() == true &&
        ValidateCalculateInterestPayRepay() == true) {        
        var send = new Array();
        send[send.length] = ("capital=" + DelCommas("capital"));
        send[send.length] = ("overpaymentyear=" + DelCommas("overpayment-year"));
        send[send.length] = ("overpaymentday=" + DelCommas("overpayment-day"));
        send[send.length] = ("overpaymentinterest=" + DelCommas("overpayment-interest"));
        send[send.length] = ("overpaymentdatestart=" + $("#overpayment-date-start").val());
        send[send.length] = ("overpaymentdateend=" + $("#overpayment-date-end").val());
        send[send.length] = ("payrepayyear=" + DelCommas("pay-repay-year"));
        send[send.length] = ("payrepayday=" + DelCommas("pay-repay-day"));
        send[send.length] = ("payrepayinterest=" + DelCommas("pay-repay-interest"));
        send[send.length] = ("payrepaydatestart=" + $("#pay-repay-date-start").val());
        send[send.length] = ("payrepaydateend=" + $("#pay-repay-date-end").val());

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestoverpaymentandpayrepay", send, function (result) {
            FillCalInterestOverpaymentAndPayRepay(result);
        });

        return true;
    }

    return false;
}

function FillCalInterestOverpaymentAndPayRepay(result) {
    if ($("#overpayment-year").length > 0)
        $("#overpayment-year").val("");

    if ($("#overpayment-day").length > 0)
        $("#overpayment-day").val("");

    if ($("#total-interest-overpayment").length > 0)
        $("#total-interest-overpayment").val("");

    if ($("#pay-repay-year").length > 0)
        $("#pay-repay-year").val("");

    if ($("#pay-repay-day").length > 0)
        $("#pay-repay-day").val("");

    if ($("#total-interest-pay-repay").length > 0)
        $("#total-interest-pay-repay").val("");

    if ($("#total-interest").length > 0)
        $("#total-interest").val("");

    if ($("#total-payment").length > 0)
        $("#total-payment").val("");

    if (result.length > 0) {
        var dataOverpaymentYear = result.split("<overpaymentyear>");
        var dataOverpaymentDay = result.split("<overpaymentday>");
        var dataTotalInterestOverpayment = result.split("<totalinterestoverpayment>");
        var dataPayRepayYear = result.split("<payrepayyear>");
        var dataPayRepayDay = result.split("<payrepayday>");
        var dataTotalInterestPayRepay = result.split("<totalinterestpayrepay>");
        var dataTotalInterest = result.split("<totalinterest>");
        var dataTotalPayment = result.split("<totalpayment>");

        if ($("#overpayment-year").length > 0)
            $("#overpayment-year").val(dataOverpaymentYear[1]);

        if ($("#overpayment-day").length > 0)
            $("#overpayment-day").val(dataOverpaymentDay[1]);

        if ($("#total-interest-overpayment").length > 0)
            $("#total-interest-overpayment").val(dataTotalInterestOverpayment[1]);

        if ($("#pay-repay-year").length > 0)
            $("#pay-repay-year").val(dataPayRepayYear[1]);

        if ($("#pay-repay-day").length > 0)
            $("#pay-repay-day").val(dataPayRepayDay[1]);

        if ($("#total-interest-pay-repay").length > 0)
            $("#total-interest-pay-repay").val(dataTotalInterestPayRepay[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(dataTotalPayment[1]);
    }
}

function CalculateChkBalance() {
    var capital = ($("#capital").length > 0 ? DelCommas("capital") : "");
    var totalInterest = ($("#total-interest").length > 0 ? DelCommas("total-interest") : "");
    var totalAccruedInterest = ($("#total-accrued-interest").length > 0 ? DelCommas("total-accrued-interest") : "");
    var totalPayment = ($("#total-payment").length > 0 ? DelCommas("total-payment") : "");
    var pay = ($("#pay").length > 0 ? DelCommas("pay") : "");
    var send = new Array();
    send[send.length] = ("capital=" + (capital.length > 0 ? capital : "0.00"));
    send[send.length] = ("totalinterest=" + (totalInterest.length > 0 ? totalInterest : "0.00"));
    send[send.length] = ("totalaccruedinterest=" + (totalAccruedInterest.length > 0 ? totalAccruedInterest : "0.00"));
    send[send.length] = ("totalpayment=" + (totalPayment.length > 0 ? totalPayment : "0.00"));
    send[send.length] = ("pay=" + (pay.length > 0 ? pay : "0.00"));

    SetMsgLoading("");

    CalculateFrm("chkbalance", send, function (result) {
        FillCalChkBalance(result);
    });
}

function FillCalChkBalance(result) {
    if (result.length > 0) {     
        var dataCapital = result.split("<capital>");
        var dataTotalInterest = result.split("<totalinterest>");
        var dataTotalAccruedInterest = result.split("<totalaccruedinterest>");
        var dataTotalPayment = result.split("<totalpayment>");
        var dataPay = result.split("<pay>");
        var dataPayCapital = result.split("<paycapital>");
        var dataPayInterest = result.split("<payinterest>");        
        var dataRemainCapital = result.split("<remaincapital>");
        var dataAccruedInterest = result.split("<accruedinterest>");
        var dataRemainAccruedInterest = result.split("<remainaccruedinterest>");

        if ($("#chk-balance-capital").length > 0) {
            $("#chk-balance-capital").html(dataCapital[1]);
            $("#chk-balance-capital-unit").show();
        }

        if ($("#chk-balance-total-interest").length > 0) {
            $("#chk-balance-total-interest").html(dataTotalInterest[1]);
            (dataTotalInterest[1].length > 0 ? $("#chk-balance-total-interest-unit").show() : "");
        }

        if ($("#chk-balance-total-accrued-interest").length > 0) {
            $("#chk-balance-total-accrued-interest").html(dataTotalAccruedInterest[1]);
            (dataTotalAccruedInterest[1].length > 0 ? $("#chk-balance-total-accrued-interest-unit").show() : "");
        }

        if ($("#chk-balance-total-payment").length > 0) {
            $("#chk-balance-total-payment").html(dataTotalPayment[1]);
            (dataTotalPayment[1].length > 0 ? $("#chk-balance-total-payment-unit").show() : "");
        }

        if ($("#chk-balance-pay").length > 0) {
            $("#chk-balance-pay").html(dataPay[1]);
            (dataPay[1].length > 0 ? $("#chk-balance-pay-unit").show() : "");
        }

        if ($("#chk-balance-pay-capital").length > 0) {
            $("#chk-balance-pay-capital").html(dataPayCapital[1]);
            (dataPayCapital[1].length > 0 ? $("#chk-balance-pay-capital-unit").show() : "");
        }

        if ($("#chk-balance-pay-interest").length > 0) {
            $("#chk-balance-pay-interest").html(dataPayInterest[1]);
            (dataPayInterest[1].length > 0 ? $("#chk-balance-pay-interest-unit").show() : "");
        }

        if ($("#chk-balance-remain-capital").length > 0) {
            $("#chk-balance-remain-capital").html(dataRemainCapital[1]);
            (dataRemainCapital[1].length > 0 ? $("#chk-balance-remain-capital-unit").show() : "");
        }

        if ($("#chk-balance-accrued-interest").length > 0) {
            $("#chk-balance-accrued-interest").html(dataAccruedInterest[1]);
            (dataAccruedInterest[1].length > 0 ? $("#chk-balance-accrued-interest-unit").show() : "");
        }

        if ($("#chk-balance-remain-accrued-interest").length > 0) {
            $("#chk-balance-remain-accrued-interest").html(dataRemainAccruedInterest[1]);
            (dataRemainAccruedInterest[1].length > 0 ? $("#chk-balance-remain-accrued-interest-unit").show() : "");
        }
    }
}

function CalculateTotalPayment() {
    var send = new Array();
    send[send.length] = ("capital=" + DelCommas("capital"));
    send[send.length] = ("totalinterest=" + DelCommas("total-interest"));
    send[send.length] = ("totalaccruedinterest=" + DelCommas("total-accrued-interest"));

    SetMsgLoading("");

    CalculateFrm("totalpayment", send, function (result) {
        var dataTotalPayment = result.split("<totalpayment>");

        if ($("#total-payment").length > 0) {
            $("#total-payment").val(dataTotalPayment[1]);

            if ($("#pay").length > 0)
                $("#pay").val($("#total-payment").val());
        }
    });
}

function BoxSearchCondition(
    countCondition,
    searchValue,
    id
) {
    var showCondition = new Array();
    var i, j = 0;

    for (i = 0; i < countCondition; i++) {        
        if (searchValue[i].length > 0) {
            showCondition[j] = i;
            j++;
        }

        $("#" + id + "-order" + (i + 1)).hide();
    }

    if (showCondition.length > 0) {
        $("#" + id).show();

        for (i = 0; i < showCondition.length; i++) {
            $("#" + id + "-order" + (showCondition[i] + 1)).show();
            $("#" + id + "-order" + (showCondition[i] + 1) + "-value").html(searchValue[showCondition[i]]);
        }
    }
    else
        $("#" + id).hide();
}

function ShowDocEContract(
    sid,
    path,
    file
) {
    $("#report-e-contract" + sid).addClass("active");
    $("#report-e-contract" + sid).removeClass("active");

    window.open((path + file), "_blank");
    
    /*
    var send = new Array();
    send[send.length] = "action=econtract";
    send[send.length] = ("path=" + path);
    send[send.length] = ("file=" + file);

    LoadAjax(send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (result) {
        var dataEContract = result.split("<econtract>");

        if (dataEContract[1] == "1") {
            DialogMessage("ไม่พบไฟล์เอกสาร", "", false, ("report-e-contract" + sid));
            return;
        }

        $("#report-e-contract" + sid).removeClass("active");

        window.open((path + file), "_blank");
    });
    */
}