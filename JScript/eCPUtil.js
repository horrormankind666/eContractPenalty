var _oldMenu = 0;
var _manual = new Array();

function Signin() {
    var _username = ($("#username").val($.trim($("#username").val()))).val();
    var _password = ($("#password").val($.trim($("#password").val()))).val();
    var _d = new Date();
    var _authen = window.btoa(window.btoa(_d.getDate() + _d.getMonth() + _d.getFullYear()) + "." + window.btoa(_username).split("").reverse().join("") + "." + window.btoa(_password).split("").reverse().join("") + "." + window.btoa(_d.getHours() + _d.getMinutes() + _d.getSeconds() + _d.getMilliseconds()));
    var _send = new Array();
    _send[_send.length] = "action=signin";
    _send[_send.length] = "authen=" + _authen;
    /*
    _send[_send.length] = "username=" + ($("#username").val($.trim($("#username").val()))).val();
    _send[_send.length] = "password=" + ($("#password").val($.trim($("#password").val()))).val();
    */

    var _msg;
    var _error = false;
    var _focus;
  
    SetMsgLoading("กำลังเข้าสู่ระบบ...");

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
        var _dataError = _result.split("<error>");

        switch (_dataError[1]) {
            case "1":
                _error = true;
                _msg = "ไม่พบผู้ใช้งานนี้";
                _focus = "username";
                break;
        }

        if (_error == true) {
            DialogMessage(_msg, _focus, false, "");
            return;
        }
        
        top.location.href = "index.aspx";
    });
}

function Signout() {
    if (_oldMenu != 0) $("#menu" + _oldMenu).removeClass("active").addClass("noactive");
    $("#menu8").removeClass("noactive").addClass("active");
        
    DialogConfirm("ต้องการออกจากระบบหรือไม่");
    $("#dialog-confirm").dialog({
        buttons: {
	        "ตกลง": function () {
		        $(this).dialog("close");
			            
                var _send = new Array();
                _send[_send.length] = "action=signout";

			    SetMsgLoading("กำลังออกจากระบบ...");

			    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
			        GotoSignin();
			    });
		    },
		    "ยกเลิก": function () {
			    $(this).dialog("close");
		    }
	    },
        close:  function () {
            $("#menu8").removeClass("active").addClass("noactive");
            $("#menu" + _oldMenu).removeClass("noactive").addClass("active");
        }
    });
}

function ShowManual() {
    if (_oldMenu != 0)
        $("#menu" + _oldMenu).removeClass("active").addClass("noactive");

    $("#menu7").removeClass("noactive").addClass("active");

    for (var _i = 0; _i < 50; _i++) {
        _manual[_i] = "Manual/UserManual" + (_i + 1) + ".png";
    }

    LoadForm(1, "manual", true, "", "", "");
}

function SetPage() {
    var _send = new Array();
    _send[_send.length] = "action=setpage";
    
    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (_result) {
        var _dataSection = _result.split("<section>");
        var _dataPage = _result.split("<page>");

        GoToPage(parseInt(_dataSection[1]), parseInt(_dataPage[1]));
    });
}

function GoToPage(_section, _order) {
    var _area;
    var _pid;

    if (_order == 0) { _pid = 1; _area = "all"; }

    //เจ้าหน้าที่กองกฏหมาย
    if (_order != 0 && _section == 1) {
        switch (_order) {
            //หน้าแรก
            case 1:
                _pid = 1;
                _area = "sec";
                break;
            //บัญชีผู้ใช้งาน
            case 2:
                _pid = 2;
                _area = "sec";
                break;
            //ตั้งค่าระบบ - กำหนดหลักสูตรที่ให้มีการทำสัญญาการศึกษา
            case 3:
                _pid = 3;
                _area = "sec";
                break;
            //ตั้งค่าระบบ - เงื่อนไขการคิดระยะเวลาตามสัญญาและสูตรคำนวณเงินชดใช้ตามสัญญา
            case 4:
                _pid = 4;
                _area = "sec";
                break;
            //ตั้งค่าระบบ - กำหนดดอกเบี้ยจากการผิดนัดชำระ
            case 5:
                _pid = 5;
                _area = "sec";
                break;
            //ตั้งค่าระบบ - เกณฑ์การชดใช้ตามสัญญา
            case 6:
                _pid = 6;
                _area = "sec";
                break;
            //ตั้งค่าระบบ - กำหนดทุนการศึกษาแต่ละหลักสูตร
            case 7:
                _pid = 7;
                _area = "sec";
                break;
            //รับแจ้งนักศึกษาผิดสัญญา
            case 8:
                _pid = 8;
                _area = "sec";
                break;
            //บันทึกการชำระหนี้ / ดอกเบี้ย
            case 9:
                _pid = 9;
                _area = "sec";
                break;
            //รายงาน - สถานะขั้นตอนการดำเงินงานของผู้ผิดสัญญา
            case 10:
                _pid = 10;
                _area = "sec";
                break;
            //รายงาน - ตารางคำนวณเงินต้นและดอกเบี้ย
            case 11:
                _pid = 11;
                _area = "sec";
                break;
            //รายงาน - สถิติการชำระหนี้ของผู้ผิดสัญญา
            case 12:
                _pid = 12;
                _area = "sec";
                break;
            //รายงาน - หนังสือทวงถามผู้ผิดสัญญาและผู้ค้ำประกัน
            case 13:
                _pid = 13;
                _area = "sec";
                break;
            //รายงาน - สถิติการทำสัญญาและการผิดสัญญาของนักศึกษา
            case 14:
                _pid = 14;
                _area = "sec";
                break;
            //รายงาน - สถิติการชำระหนี้ตามช่วงวันที่
            case 15:
                _pid = 15;
                _area = "sec";
                break;
            //รายงาน - เอกสารสัญญาการเป็นนักศึกษา
            case 16:
                _pid = 16;
                _area = "sec";
                break;
            //รายงาน - ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้
            case 17:
                _pid = 17;
                _area = "sec";
                break;
            //รายงาน - การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้
            case 18:
                _pid = 18;
                _area = "sec";
                break;
            //รายงาน - ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้
            case 19:
                _pid = 19;
                _area = "sec";
                break;
        }
    }

    //เจ้าหน้าที่กองบริหารการศึกษา
    if (_order != 0 && _section == 2) {
        switch (_order) {
            //หน้าแรก
            case 1:
                _pid = 1;
                _area = "sec";
                break;
            //บัญชีผู้ใช้งาน
            case 2:
                _pid = 2;
                _area = "sec";
                break;
            //แจ้งนักศึกษาผิดสัญญา
            case 3:
                _pid = 3;
                _area = "sec";
                break;
            //รายงาน - สถานะขั้นตอนการดำเงินงานของผู้ผิดสัญญา
            case 4:
                _pid = 4;
                _area = "sec";
                break;            
            //รายงาน - สถิติการชำระหนี้ของผู้ผิดสัญญา
            case 5:
                _pid = 5;
                _area = "sec";
                break;
            //รายงาน - หนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณา
            case 6:
                _pid = 6;
                _area = "sec";
                break;
            //รายงาน - สถิติการทำสัญญาและการผิดสัญญาของนักศึกษา
            case 7:
                _pid = 7;
                _area = "sec";
                break;
            //รายงาน - สถิติการชำระหนี้ตามช่วงวันที่
            case 8:
                _pid = 8;
                _area = "sec";
                break;
            //รายงาน - เอกสารสัญญาการเป็นนักศึกษา
            case 9:
                _pid = 9;
                _area = "sec";
                break;
        }
    }

    //เจ้าหน้าที่กองคลัง
    if (_order != 0 && _section == 3) {
        switch (_order) {
            //หน้าแรก
            case 1:
                _pid = 1;
                _area = "sec";
                break;
            //บัญชีผู้ใช้งาน
            case 2:
                _pid = 2;
                _area = "sec";
                break;
            //รายงาน - ลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดล
            case 3:
                _pid = 3;
                _area = "sec";
                break;
            //รายงาน - การรับชำระเงินจากลูกหนี้ ตามการผิดสัญญาการศึกษามหาวิทยาลัยมหิดล
            case 4:
                _pid = 4;
                _area = "sec";
                break;
            //รายงาน - ลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดลคงค้าง
            case 5:
                _pid = 5;
                _area = "sec";
                break;
        }
    }
    
    LoadPage(_area, _section, _pid);
}

function GotoSignin() {
    DialogConfirm("เข้าสู่ระบบใหม่อีกครั้ง");
    $("#dialog-confirm").dialog({
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            top.location.href = "Signin.aspx";
        }
    });    
}

function LoadSignin() {
    $(".head").hide();
    $(".menu-bar-main").hide();

    var _send = new Array();
    _send[_send.length] = "action=page";

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (_result) {
        var _dataContent = _result.split("<content>");

        $("#content-content").html(_dataContent[1]);

        InitTextSelect();
        //GoToElement("top-page");
        GoToTopElement("html, body");
    })
}

function LoadPage(_area, _section, _pid) {
    var _send = new Array();
    _send[_send.length] = "action=page";
    _send[_send.length] = "area=" + _area;
    _send[_send.length] = "section=" + _section;
    _send[_send.length] = "pid=" + _pid;

    var _error = false;
    var _msg;
    
    if (_oldMenu != 0)
        $("#menu" + _oldMenu).removeClass("active").addClass("noactive");
  
    SetMsgLoading("กำลังโหลด...");
  
    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
        var _dataError = _result.split("<error>");
        var _dataHead = _result.split("<head>");
        var _dataMenuBar = _result.split("<menubar>");
        var _dataMenu = _result.split("<menu>");
        var _dataContent = _result.split("<content>");

        if (_dataError[1] == "1") {
            GotoSignin();
            return;
        }

        $("#head-content").html(_dataHead[1]);
        $("#menu-bar-content").html(_dataMenuBar[1]);
        $("#menu" + _dataMenu[1]).removeClass("noactive").addClass("active");
        _oldMenu = _dataMenu[1];

        if (_dataHead[1].length > 0) {
            $(".head").show();
            $(".menu-bar-main").show();
        }
        else {
            $(".head").hide();
            $(".menu-bar-main").hide();
        }

        $("#content-content").html(_dataContent[1]);
        
        //InitSticky();
        InitTab(false);

        if (_section == 1) {
            switch (_pid) {
                case 2:
                    OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                    break;
                case 8:
                    OpenTab("link-tab1-cp-trans-require-contract", "#tab1-cp-trans-require-contract", "", true, "", "", "");
                    break;
                case 9:
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
        
        if (_section == 2) {
            switch (_pid) {
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

        if (_section == 3) {
            switch (_pid) {
                case 2:
                    OpenTab("link-tab1-cp-tab-user", "#tab1-cp-tab-user", "", true, "", "", "");
                    break;
                case 3:
                case 4:
                case 5:
                    OpenTab("link-tab1-cp-report-debtor-contract", "#tab1-cp-report-debtor-contract", "", true, "", "", "");
                    break;
            }
        }
        
        GoToTopElement("html, body");
    });
}

function LoadCombobox(_listData, _param, _target, _value, _widthInput, _widthList) {
    var _send = new Array();
    _send[_send.length] = "action=combobox";
    _send[_send.length] = "list=" + _listData;
    _send[_send.length] = _param;

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (_result) {
        var _dataList = _result.split("<list>");

        $("#" + _target).html(_dataList[1]);
        InitCombobox(_listData, _value, _value, _widthInput, _widthList);
    });
}

function LoadList(_listData, _recordCountID, _listID, _navPageID) {
    $("#" + _recordCountID).html("");
    $("#" + _listID).html("");
    if (_navPageID.length > 0) $("#" + _navPageID).html("");

    var _send = new Array();
    _send[_send.length] = "action=list";
    _send[_send.length] = "list=" + _listData;
    
    SetMsgLoading("กำลังโหลด...");

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
        var _dataRecordCount = _result.split("<recordcount>");
        var _dataList = _result.split("<list>");
        var _dataPageNav = _result.split("<pagenav>");

        $("#" + _recordCountID).html("ค้นหาพบ " + _dataRecordCount[1] + " รายการ");
        $("#" + _listID).html(_dataList[1]);

        if (_navPageID.length > 0) 
            $("#" + _navPageID).html(_dataPageNav[1]);
    });
}

function InitTab(_subTab) {
    if (_subTab == false)
        $(".tab-hidden").hide();

    var _contentDataTab = (_subTab == false ? "content-data-tabs" : "content-data-subtabs");

    $(document).ready(function () {
        $("." + _contentDataTab + " ul li a").click(function () {
            var _dropID = $(this).closest("a").attr("id");
            var _linkTab = $(this).closest("a").attr("alt");
            var _action = "";
            var _id = "";
            var _trackingStatus = "";

            if (_subTab == false) {
                switch (_dropID) { 
                    case "link-tab3-cp-tab-user":
                        _action = $("#action").val();
                        _id = $("#username-hidden").val() + ":" + $("#password-hidden").val();
                        _trackingStatus = "";
                        break;
                    case "link-tab3-cp-tab-program":
                    case "link-tab3-cp-tab-interest":
                    case "link-tab3-cp-tab-pay-break-contract":
                    case "link-tab3-cp-tab-scholarship":
                        _action = $("#action").val();
                        _id = $("#cp1id").val();
                        _trackingStatus = "";
                        break;
                    case "link-tab3-cp-trans-break-contract":
                        _action = $("#action").val();
                        _id = $("#cp1id").val();
                        _trackingStatus = $("#trackingstatus").val();
                        break;
                    case "link-tab3-cp-trans-require-contract":
                        _action = $("#action").val();
                        _id = $("#cp1id").val();
                        _trackingStatus = $("#trackingstatus").val();
                        break;
                    case "link-tab2-cp-trans-payment":
                    case "link-tab1-adddetail-cp-trans-payment": 
                    case "link-tab2-adddetail-cp-trans-payment":
                    case "link-tab2-cp-report-table-cal-capital-and-interest":
                        _action = "";
                        _id = $("#cp2id").val();
                        _trackingStatus = "";
                        break;
                }

                OpenTab(_dropID, _linkTab, "", true, _action, _id, _trackingStatus);
            }

            if (_subTab == true) {
                switch (_dropID) {
                    case "link-tab1-adddetail-cp-trans-payment":
                    case "link-tab2-adddetail-cp-trans-payment":
                        _action = "";
                        _id = $("#cp2id").val();
                        _trackingStatus = "";
                        break;
                }

                OpenSubTab(_dropID, _linkTab, _id);
            }
        });
    });
}

function OpenTab(_dropID, _linkTab, _tabTitle, _tabHidden, _action, _id, _trackingStatus) {
    GoToTopElement("html, body");

    $(".tab-content").hide();
    if ($("#" + _dropID).hasClass("active") == false) {
        if (_tabHidden == true)
            $(".tab-hidden").hide()
        else
            $(".tab-hidden").show();

        if (_tabTitle.length > 0)
            $("#" + _dropID).html(_tabTitle);

        $(".content-data-tabs ul li a").removeClass("active");
        $("#" + _dropID).addClass("active");
    }

    $(_linkTab + "-head").show();
    $(_linkTab + "-contents").show();
    $(_linkTab + "-content").show();

    switch (_dropID) {
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
            LoadForm(1, "updatecptabuser", false, "update-data-tab-user", _id, "");
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
            LoadForm(1, "updatecptabprogram", false, "update-data-tab-program", _id, "");
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
            LoadForm(1, "updatecptabinterest", false, "update-data-tab-interest", _id, "");
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
            LoadForm(1, "updatecptabpaybreakcontract", false, "update-data-tab-pay-break-contract", _id, "");
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
            LoadForm(1, "updatecptabscholarship", false, "update-data-tab-scholarship", _id, "");
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
            ChkTrackingStatusViewTransBreakContract(_id, _trackingStatus, "trans-break-contract" + _id, function (_result) {
                if (_result == "0") {
                    $(".addupdate-data-trans-break-contract").html("");
                    LoadForm(1, "updatecptransbreakcontract", false, "update-data-trans-break-contract", _id, "");
                }        
            });
            break;
        case "link-tab2-cp-trans-require-contract":
            SetMsgLoading("กำลังโหลด...");
            SearchRepay();
            break;
        case "link-tab3-cp-trans-require-contract":
            if (_action == "add") {
                ChkTrackingStatusViewTransBreakContract(_id, _trackingStatus, "trans-break-contract" + _id, function (_result) {
                    if (_result == "0") {
                        $("#addupdate-data-trans-require-contract").html("");
                        LoadForm(1, "addcptransrequirecontract", false, "addupdate-data-trans-require-contract", _id, "");
                    }            
                });
            }

            if (_action == "update") {
                ChkTrackingStatusViewTransBreakContract(_id, _trackingStatus, "trans-break-contract" + _id, function (_result) {
                    if (_result == "0") {
                        $(".addupdate-data-trans-require-contract").html("");
                        LoadForm(1, "updatecptransrequirecontract", false, "addupdate-data-trans-require-contract", _id, "");
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
            LoadForm(1, "adddetailcptranspayment", false, "adddetail-data-trans-payment", _id, "");
            break;
        case "link-tab1-cp-report-table-cal-capital-and-interest":
            SetMsgLoading("กำลังโหลด...");
            SearchReportTableCalCapitalAndInterest();
            break;
        case "link-tab2-cp-report-table-cal-capital-and-interest":
            $("#cal-data-report-table-cal-capital-and-interest").html("");
            LoadForm(1, "calreporttablecalcapitalandinterest", false, "cal-data-report-table-cal-capital-and-interest", _id, "");
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

function OpenSubTab(_dropID, _linkTab, _id) {
    $(".subtab-content").hide();
    if ($("#" + _dropID).hasClass("active") == false)  {
        $(".content-data-subtabs ul li a").removeClass("active");
        $("#" + _dropID).addClass("active");
    }

    $(_linkTab + "-head").show();
    $(_linkTab + "-contents").show();
    $(_linkTab + "-content").show();

    switch (_dropID) {
        case "link-tab1-adddetail-cp-trans-payment":
            $(".adddetail-cp-trans-payment-content").html("");
            LoadForm(1, "detailcptranspayment", false, "detail-data-trans-payment", _id, "");
            break;
        case "link-tab2-adddetail-cp-trans-payment":
            var _formatPayment = $("#format-payment-hidden").val() == "1" ? "fullrepay" : "payrepay";
    
            $(".adddetail-cp-trans-payment-content").html("");
            LoadForm(1, "addcptranspayment" + _formatPayment, false, "add-data-trans-payment", _id, "");
            break;
        case "link-tab1-report-student-on-statistic-contract-by-program":
        case "link-tab2-report-student-on-statistic-contract-by-program":
            SetMsgLoading("กำลังโหลด...");
            if ($("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true) SearchReportStudentOnStatisticContractByProgram(1);
            if ($("#link-tab2-report-student-on-statistic-contract-by-program").hasClass("active") == true) SearchReportStudentOnStatisticContractByProgram(2);
            break;
    }
}

function SetNullCombobox(_id) {
    if (ComboboxGetSelectedValue(_id) == null) {
        ComboboxSetSelectedValue(_id, "0");
        SetSelectCombobox(_id, "0");
    }
}

function SetSelectCombobox(_id, _value) {
    if (_id == "dlevel" ||
        _id == "faculty" ||
        _id == "facultycptabprogram" ||
        _id == "facultytransbreakcontract" ||
        _id == "facultytransrepaycontract" ||
        _id == "facultytranspayment" ||
        _id == "facultyreporttablecalcapitalandinterest" ||
        _id == "facultyreportstepofwork" ||
        _id == "facultysearchstudent" ||
        _id == "facultyprofilestudent" ||
        _id == "facultyreportnoticerepaycomplete" ||
        _id == "facultyreportnoticeclaimdebt" ||
        _id == "facultyreportstatisticpaymentbydate" ||
        _id == "facultyreportecontract") {
        var _dlevel;
        var _facultyArray;
        var _faculty;
        var _program;
        var _listProgram;
        var _comboboxWidthInput = 390;
        var _comboboxWidthList = 415;

        if (_id == "dlevel") {
            _dlevel = _value;

            if ($("#faculty").length > 0) {
                _facultyArray = ComboboxGetSelectedValue("faculty").split(";");
                _faculty = _facultyArray[0];
                _program = "program";
                _listProgram = "list-program";
            }

            if ($("#facultycptabprogram").length > 0) {
                _facultyArray = ComboboxGetSelectedValue("facultycptabprogram").split(";");
                _faculty = _facultyArray[0];
                _program = "programcptabprogram";
                _listProgram = "list-program";
            }
        }
        
        if (_id == "faculty" ||
            _id == "facultycptabprogram" ||
            _id == "facultytransbreakcontract" ||
            _id == "facultytransrepaycontract" ||
            _id == "facultytranspayment" ||
            _id == "facultyreporttablecalcapitalandinterest" ||
            _id == "facultyreportstepofwork" ||
            _id == "facultysearchstudent" ||
            _id == "facultyprofilestudent" ||
            _id == "facultyreportnoticerepaycomplete" ||
            _id == "facultyreportnoticeclaimdebt" ||
            _id == "facultyreportstatisticpaymentbydate" ||
            _id == "facultyreportecontract") {
            _dlevel = $("#dlevel").length > 0 ? ComboboxGetSelectedValue("dlevel") : "";
            _faculty = _value;
            
            if (_id == "faculty") {
                _program = "program";
                _listProgram = "list-program";
            }

            if (_id == "facultycptabprogram") {
                _program = "programcptabprogram";
                _listProgram = "list-program";
            }

            if (_id == "facultytransbreakcontract") {
                _program = "programtransbreakcontract";
                _listProgram = "list-program-trans-break-contract";
            }

            if (_id == "facultytransrepaycontract") {
                _program = "programtransrepaycontract";
                _listProgram = "list-program-trans-repay-contract";
            }

            if (_id == "facultytranspayment") {
                _program = "programtranspayment";
                _listProgram = "list-program-trans-payment";        
                _comboboxWidthInput = 336;
                _comboboxWidthList = 361;        
            }

            if (_id == "facultyreporttablecalcapitalandinterest") {
                _program = "programreporttablecalcapitalandinterest";
                _listProgram = "list-program-report-table-cal-capital-and-interest";
            }

            if (_id == "facultyreportstepofwork") {
                _program = "programreportstepofwork";
                _listProgram = "list-program-report-step-of-work";
            }

            if (_id == "facultysearchstudent") {                
                _program = "programsearchstudent";
                _listProgram = "list-program-search-student";
            }

            if (_id == "facultyprofilestudent") {
                _program = "programprofilestudent";
                _listProgram = "list-program-profile-student";
            }

            if (_id == "facultyreportnoticerepaycomplete") {
                _program = "programreportnoticerepaycomplete";
                _listProgram = "list-program-report-notice-repay-complete";
            }

            if (_id == "facultyreportnoticeclaimdebt") {
                _program = "programreportnoticeclaimdebt";
                _listProgram = "list-program-report-notice-claim-debt";
            }

            if (_id == "facultyreportstatisticpaymentbydate") {
                _program = "programreportstatisticpaymentbydate";
                _listProgram = "list-program-report-statistic-payment-by-date";
            }

            if (_id == "facultyreportecontract") {
                _program = "programreportecontract";
                _listProgram = "list-program-report-e-contract";
            }
        }

        LoadCombobox(_program, "dlevel=" + _dlevel + "&faculty=" + _faculty, _listProgram, "0", _comboboxWidthInput, _comboboxWidthList);
    }

    if (_id == "case-graduate") {
        var _caseGraduate = _value;

        if (_caseGraduate == "2")
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

    if (_id == "scholar") {
        var _faculty = $("#profile-student-faculty-hidden").val();
        var _program = $("#profile-student-program-hidden").val();
        var _scholar = _value;

        if (_scholar == "1") {
            TextboxEnable("#scholarship-money");
            TextboxEnable("#scholarship-year");
            TextboxEnable("#scholarship-month");
            ViewScholarship(_faculty, _program, _scholar);
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

    if (_id == "case-graduate-break-contract") {
        var _faculty = $("#profile-student-faculty-hidden").val();
        var _program = $("#profile-student-program-hidden").val();
        var _caseGraduateBreakContract = _value;

        ComboboxDisable("civil");
        $("#contract-force-date-start").val("");
        $("#contract-force-date-end").val("");
        CalendarDisable("#contract-force-date-start");
        CalendarDisable("#contract-force-date-end");

        if (_caseGraduateBreakContract == "1" || _caseGraduateBreakContract == "2") {
            if (_caseGraduateBreakContract == "2") {
                ComboboxEnable("civil")

                if ($("#education-date-end").val().length > 0) {
                    var _contractForceDateStart = $("#education-date-end").datepicker("getDate", "+1d");
                    _contractForceDateStart.setDate(_contractForceDateStart.getDate() + 1);

                    $("#contract-force-date-start").datepicker("setDate", _contractForceDateStart);
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

        ViewPayBreakContract(_faculty, _program, _caseGraduateBreakContract);
    }

    if (_id == "condition-tablecalcapitalandinterest") {
        var _conditionTableCalCapitalAndInterest = _value;

        if (_conditionTableCalCapitalAndInterest == "0") {
            $("#condition-select-0").show();
            $("#pay-period").val("");
            TextboxDisable("#pay-period");
            $("#condition-select-1").hide();
            $("#condition-select-2").hide();
        }
        else {
            $("#condition-select-0").hide();

            if (_conditionTableCalCapitalAndInterest == "1") {
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

function SetSelectDefaultCombobox(_id) {    
    if (_id == "faculty" ||
        _id == "facultycptabprogram" ||
        _id == "facultytransbreakcontract" ||
        _id == "facultytransrepaycontract" ||
        _id == "facultytranspayment" ||
        _id == "facultyreporttablecalcapitalandinterest" ||
        _id == "facultyreportstepofwork" ||
        _id == "facultysearchstudent" ||
        _id == "facultyprofilestudent" ||
        _id == "facultyreportnoticerepaycomplete" ||
        _id == "facultyreportnoticeclaimdebt" ||
        _id == "facultyreportstatisticpaymentbydate" ||
        _id == "facultyreportecontract") {
        var _dlevel = $("#dlevel").length > 0 ? ComboboxGetSelectedValue("dlevel") : "";
        var _facultyArray;
        var _faculty;
        var _program;
        var _programValue;
        var _listProgram;
        var _comboboxWidthInput = 390;
        var _comboboxWidthList = 415;

        if (_id == "faculty") {
            _facultyArray = ComboboxGetSelectedValue("faculty").split(";");
            _faculty = _facultyArray[0];
            _program = "program";
            _listProgram = "list-program";
            _programValue = "program-hidden";
        }

        if (_id == "facultycptabprogram") {
            _facultyArray = ComboboxGetSelectedValue("facultycptabprogram").split(";");
            _faculty = _facultyArray[0];
            _program = "programcptabprogram";
            _listProgram = "list-program";
            _programValue = "program-hidden";
        }

        if (_id == "facultytransbreakcontract") {
            _facultyArray = ComboboxGetSelectedValue("facultytransbreakcontract").split(";");
            _faculty = _facultyArray[0];
            _program = "programtransbreakcontract";
            _listProgram = "list-program-trans-break-contract";
            _programValue = "program-trans-break-contract-hidden";
        }

        if (_id == "facultytransrepaycontract") {
            _facultyArray = ComboboxGetSelectedValue("facultytransrepaycontract").split(";");
            _faculty = _facultyArray[0];
            _program = "programtransrepaycontract";
            _listProgram = "list-program-trans-repay-contract";
            _programValue = "program-trans-repay-contract-hidden";
        }

        if (_id == "facultytranspayment") {
            _facultyArray = ComboboxGetSelectedValue("facultytranspayment").split(";");
            _faculty = _facultyArray[0];
            _program = "programtranspayment";
            _listProgram = "list-program-trans-payment";
            _programValue = "program-trans-payment-hidden";
            _comboboxWidthInput = 336;
            _comboboxWidthList = 361;
        }

        if (_id == "facultyreporttablecalcapitalandinterest") {
            _facultyArray = ComboboxGetSelectedValue("facultyreporttablecalcapitalandinterest").split(";");
            _faculty = _facultyArray[0];
            _program = "programreporttablecalcapitalandinterest";
            _listProgram = "list-program-report-table-cal-capital-and-interest";
            _programValue = "program-report-table-cal-capital-and-interest-hidden";
        }

        if (_id == "facultyreportstepofwork") {
            _facultyArray = ComboboxGetSelectedValue("facultyreportstepofwork").split(";");
            _faculty = _facultyArray[0];
            _program = "programreportstepofwork";
            _listProgram = "list-program-report-step-of-work";
            _programValue = "program-report-step-of-work-hidden";
        }

        if (_id == "facultysearchstudent") {
            _faculty = "0";
            _program = "programsearchstudent";
            _listProgram = "list-program-search-student";
            _programValue = "";
        }

        if (_id == "facultyprofilestudent") {
            _facultyArray = ComboboxGetSelectedValue("facultyprofilestudent").split(";");
            _faculty = _facultyArray[0];
            _program = "programprofilestudent";
            _listProgram = "list-program-profile-student";
            _programValue = "profile-student-program-hidden";
        }

        if (_id == "facultyreportnoticerepaycomplete") {
            _facultyArray = ComboboxGetSelectedValue("facultyreportnoticerepaycomplete").split(";");
            _faculty = _facultyArray[0];
            _program = "programreportnoticerepaycomplete";
            _listProgram = "list-program-report-notice-repay-complete";
            _programValue = "program-report-notice-repay-complete-hidden";
        }

        if (_id == "facultyreportnoticeclaimdebt") {
            _facultyArray = ComboboxGetSelectedValue("facultyreportnoticeclaimdebt").split(";");
            _faculty = _facultyArray[0];
            _program = "programreportnoticeclaimdebt";
            _listProgram = "list-program-report-notice-claim-debt";
            _programValue = "program-report-notice-claim-debt-hidden";
        }

        if (_id == "facultyreportstatisticpaymentbydate") {
            _facultyArray = ComboboxGetSelectedValue("facultyreportstatisticpaymentbydate").split(";");
            _faculty = _facultyArray[0];
            _program = "programreportstatisticpaymentbydate";
            _listProgram = "list-program-report-statistic-payment-by-date";
            _programValue = "program-report-statistic-payment-by-date-hidden";
        }

        if (_id == "facultyreportecontract") {
            _facultyArray = ComboboxGetSelectedValue("facultyreportecontract").split(";");
            _faculty = _facultyArray[0];
            _program = "programreportecontract";
            _listProgram = "list-program-report-e-contract";
            _programValue = "program-report-e-contract-hidden";
        }

        LoadCombobox(_program, "dlevel=" + _dlevel + "&faculty=" + _faculty, _listProgram, (_faculty != "0" ? ($("#" + _programValue).val().length > 0 ? $("#" + _programValue).val() : "0") : "0"), _comboboxWidthInput, _comboboxWidthList);
    }

    if (_id == "case-graduate") {
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

    if (_id == "case-graduate-break-contract") {
        CalendarDisable("#contract-force-date-start");
        CalendarDisable("#contract-force-date-end");

        if (ComboboxGetSelectedValue("case-graduate-break-contract") == "1" || ComboboxGetSelectedValue("case-graduate-break-contract") == "2") {
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

    if (_id == "scholar") {
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

    if (_id == "civil") {
        if (ComboboxGetSelectedValue("case-graduate-break-contract") == "2")
            ComboboxEnable("civil")
        else {
            ComboboxSetSelectedValue("civil", "0");
            ComboboxDisable("civil");
        }
    }

    if (_id == "condition-tablecalcapitalandinterest") {
        if (ComboboxGetSelectedValue("condition-tablecalcapitalandinterest") == "0") {
            $("#condition-select-0").show();
            $("#pay-period").val("");
            TextboxDisable("#pay-period");
            $("#condition-select-1").hide();
            $("#condition-select-2").hide();
        }
    }
}

function InitComboboxOnClick(_id) {
    $(document).ready(function () {
        $("." + _id + "-combobox-input").autocomplete({
            change: function (event, ui) {
                SetNullCombobox(_id);
            },
            close: function (event, ui) {
                SetNullCombobox(_id);
            }
        });

        $("#" + _id).combobox({
            selected: function (event, ui) {
                SetSelectCombobox(_id, ui.item.value);
            }
        });

        SetSelectDefaultCombobox(_id)
    });
}

function LoadForm(_frmIndex, _frm, _dialogFrm, _frmID, _id, _idActive) {
    if (_idActive.length > 0)
        $("#" + _idActive).addClass("active");

    if (_dialogFrm == false)
        $("#" + _frmID).html("");

    var _send = new Array();
    _send[_send.length] = "action=form";
    _send[_send.length] = "frm=" + _frm;
    _send[_send.length] = "id=" + _id;

    SetMsgLoading("กำลังโหลด...");

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
        var _dataForm = _result.split("<form>");
        var _dataWidth = _result.split("<width>");
        var _dataHeight = _result.split("<height>");
        var _dataTitle = _result.split("<title>");
        var _height = parseInt(_dataHeight[1]);

        if (_dataForm[1].length == 0) {
            DialogMessage("ไม่พบข้อมูล", "", false, _idActive);
            //if (_idActive.length > 0) $("#" + _idActive).removeClass("active");
            return;
        }

        if (_dialogFrm == true) {
            if (_frm == "manual") _height = ($(window).height() - 40);
                DialogForm(_frmIndex, _dataForm[1], parseInt(_dataWidth[1]), _height, _dataTitle[1], _idActive, "")

            if (_frm == "manual") {
                $("#dialog-form" + _frmIndex).dialog({
                    close: function () {
                        $(".dialog-overlay" + _frmIndex).remove();
                        $("body").css("overflow", "auto");

                        $("#menu7").removeClass("active").addClass("noactive");
                        $("#menu" + _oldMenu).removeClass("noactive").addClass("active");
                    }
                });    
            }
        }
        else
            $("#" + _frmID).html(_dataForm[1]);

        switch (_frm) {
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
                ResetFormatPayment();
                ResetListTransPayment();
                break;
            case "searchcptranspayment":
                ResetFrmSearchCPTransPayment("");
                break;
            case "detailcptranspayment":
                GoToTopElement("html, body");
                ResetListTransPayment();
                break;
            case "addcptranspaymentfullrepay":
            case "addcptranspaymentpayrepay":
                InitCalInterestYesNo();
                InitPayChannel();
                InitReceiptCopy();
                ResetFrmAddCPTransPayment();
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
                InitSlide(".class-dialog-form-manual .ui-dialog-content", _manual, true, "Manual/UserManual.zip");
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

function AddUpdateData(_action, _cmd, _valueSend, _listUpdate, _recordCount, _listData, _navPage, _closeFrm, _callbackFunc) {
    var _i;
    var _msgAction = (_action == "add" || _action == "update") ? "บันทึก" : "ลบ";
    var _send = new Array();
    _send[_send.length] = "action=" + _action;
    _send[_send.length] = "cmd=" + _cmd;

    if (_valueSend.length > 0) {
        for (_i = 0; _i < _valueSend.length; _i++) {
            _send[_send.length] = _valueSend[_i];
        }
    }

    SetMsgLoading("กำลัง" + _msgAction + "...");

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", true, true, function (_result) {
        var _dataError = _result.split("<error>");
        var _dataRecordCount = _result.split("<recordcount>");
        var _dataList = _result.split("<list>");
        var _dataPageNav = _result.split("<pagenav>");

        if (_dataError[1] == "0") {
            if (_closeFrm == true)
                $("#dialog-form1").dialog("close");

            if (_listUpdate == true) {
                $("#" + _recordCount).html("ค้นหาพบ " + _dataRecordCount[1] + " รายการ");
                $("#" + _listData).html(_dataList[1]);

                if (_navPage.length > 0)
                    $("#" + _navPage).html(_dataPageNav[1]);
            }
        }

        _callbackFunc(_dataError[1]);
    });
}

function SearchData(_from, _valueSend, _recordSearch, _listSearch, _navPage) {
    $("#" + _recordSearch).html("");

    if (_from != "studentwithresult")
        $("#" + _listSearch).html("");

    $("#" + _navPage).html("");

    var _i;
    var _j = 2;
    var _send = new Array();
    _send[_send.length] = "action=search";
    _send[_send.length] = "from=" + _from;

    if (_valueSend.length > 0) {
        for (_i = 0; _i < _valueSend.length; _i++) {
            _send[_j] = _valueSend[_i];
            _j++;
        }
    }
    
    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", (_msgLoading.length > 0 ? true : false), (_msgLoading.length > 0 ? true : false), function (_result) {
        var _dataRecordCount = _result.split("<recordcount>");
        var _dataList = _result.split("<list>");
        var _dataPageNav = _result.split("<pagenav>");

        $("#" + _recordSearch).html("ค้นหาพบ " + _dataRecordCount[1] + " รายการ");
        $("#" + _listSearch).html(_dataList[1]);
        $("#" + _navPage).html(_dataPageNav[1]);
    });
}

function ViewData(_from, _valueSend, _callbackFunc) {
    var _i;
    var _j = 2;
    var _send = new Array();
    _send[_send.length] = "action=search";
    _send[_send.length] = "from=" + _from;

    if (_valueSend.length > 0) {
        for (_i = 0; _i < _valueSend.length; _i++) {
            _send[_j] = _valueSend[_i];
            _j++;
        }
    }

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", (_msgLoading.length > 0 ? true : false), (_msgLoading.length > 0 ? true : false), function (_result) {
        _callbackFunc(_result);
    });
}

function PageNav(_pageContent, _currentPage, _startRow, _endRow) {
    var _send = new Array();
    _send[_send.length] = "currentpage=" + _currentPage;
    _send[_send.length] = "startrow=" + _startRow;
    _send[_send.length] = "endrow=" + _endRow;

    if (_pageContent == "tabuser") {
        _send[_send.length] = "name=" + $("#name-tab-user-hidden").val();

        SetMsgLoading("กำลังโหลด...");

        SearchData("tabuser", _send, "record-count-cp-tab-user", "list-data-tab-user", "nav-page-tab-user");
    }
    
    if (_pageContent == "studentwithresult") {
        var _faculty = ComboboxGetSelectedValue("facultysearchstudent") != "0" ? ComboboxGetSelectedValue("facultysearchstudent") : "";
        var _program = ComboboxGetSelectedValue("programsearchstudent") != "0" ? ComboboxGetSelectedValue("programsearchstudent") : "";

        _faculty = _faculty.length > 0 ? _faculty.split(";") : "";
        _program = _program.length > 0 ? _program.split(";") : "";

        _send[_send.length] = "studentid=" + $("#id-name-search-student").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

        SetMsgLoading("กำลังโหลด...");

        SearchData("studentwithresult", _send, "record-count-student-with-result", "list-data-search-student-with-result", "nav-page-search-student-with-result");
    }

    if (_pageContent == "transbreakcontract") {
        var _faculty = $("#faculty-trans-break-contract-hidden").val().length > 0 ? $("#faculty-trans-break-contract-hidden").val().split(";") : "";
        var _program = $("#program-trans-break-contract-hidden").val().length > 0 ? $("#program-trans-break-contract-hidden").val().split(";") : "";

        _send[_send.length] = "statussend=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "1" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "1" : ""));
        _send[_send.length] = "statusreceiver=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "2" ? "1" : ($("#trackingstatus-trans-break-contract-hidden").val() == "3" ? "2" : ""));
        _send[_send.length] = "statusedit=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "4" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : ""));
        _send[_send.length] = "statuscancel=" + ($("#trackingstatus-trans-break-contract-hidden").val() == "5" ? "2" : ($("#trackingstatus-trans-break-contract-hidden").val() == "6" ? "2" : ""));
        _send[_send.length] = "studentid=" + $("#id-name-trans-break-contract-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "datestart=" + $("#date-start-trans-break-contract-hidden").val();
        _send[_send.length] = "dateend=" + $("#date-end-trans-break-contract-hidden").val();

        SetMsgLoading("กำลังโหลด...");

        SearchData("transbreakcontract", _send, "record-count-cp-trans-break-contract", "list-data-trans-break-contract", "nav-page-trans-break-contract");
    }

    if (_pageContent == "transrepaycontract") {
        var _faculty = $("#faculty-trans-repay-contract-hidden").val().length > 0 ? $("#faculty-trans-repay-contract-hidden").val().split(";") : "";        
        var _program = $("#program-trans-repay-contract-hidden").val().length > 0 ? $("#program-trans-repay-contract-hidden").val().split(";") : "";
        var _statusRepay = SetStatusRepay();

        _send[_send.length] = "statusrepay=" + _statusRepay[0];
        _send[_send.length] = "statusreply=" + _statusRepay[1];
        _send[_send.length] = "replyresult=" + _statusRepay[2];
        _send[_send.length] = "statuspayment=" + _statusRepay[3];
        _send[_send.length] = "studentid=" + $("#id-name-trans-repay-contract-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "datestart=" + $("#date-start-trans-repay-contract-hidden").val();
        _send[_send.length] = "dateend=" + $("#date-end-trans-repay-contract-hidden").val();

        SetMsgLoading("กำลังโหลด...");

        SearchData("transrepaycontract", _send, "record-count-cp-trans-repay-contract", "list-data-trans-repay-contract", "nav-page-trans-repay-contract");
    }

    if (_pageContent == "transpayment") {
        var _faculty = $("#faculty-trans-payment-hidden").val().length > 0 ? $("#faculty-trans-payment-hidden").val().split(";") : "";
        var _program = $("#program-trans-payment-hidden").val().length > 0 ? $("#program-trans-payment-hidden").val().split(";") : "";

        _send[_send.length] = "statuspayment=" + $("#paymentstatus-trans-payment-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-trans-payment-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "datestart=" + $("#date-start-trans-repay1-reply-hidden").val();
        _send[_send.length] = "dateend=" + $("#date-end-trans-repay1-reply-hidden").val();

        SetMsgLoading("กำลังโหลด...");

        SearchData("transpayment", _send, "record-count-cp-trans-payment", "list-data-trans-payment", "nav-page-trans-payment");
    }

    if (_pageContent == "reporttablecalcapitalandinterest") {
        var _faculty = $("#faculty-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#faculty-report-table-cal-capital-and-interest-hidden").val().split(";") : "";
        var _program = $("#program-report-table-cal-capital-and-interest-hidden").val().length > 0 ? $("#program-report-table-cal-capital-and-interest-hidden").val().split(";") : "";

        _send[_send.length] = "studentid=" + $("#id-name-report-table-cal-capital-and-interest-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

        SetMsgLoading("กำลังโหลด...");

        SearchData("reporttablecalcapitalandinterest", _send, "record-count-cp-report-table-cal-capital-and-interest", "list-data-report-table-cal-capital-and-interest", "nav-page-report-table-cal-capital-and-interest");
    }

    if (_pageContent == "reportstepofwork") {
        var _faculty = $("#faculty-report-step-of-work-hidden").val().length > 0 ? $("#faculty-report-step-of-work-hidden").val().split(";") : "";
        var _program = $("#program-report-step-of-work-hidden").val().length > 0 ? $("#program-report-step-of-work-hidden").val().split(";") : "";

        _send[_send.length] = "statusstepofwork=" + $("#stepofworkstatus-report-step-of-work-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-report-step-of-work-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
       
        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstepofwork", _send, "record-count-cp-report-step-of-work", "list-data-report-step-of-work", "nav-page-report-step-of-work");
    }

    if (_pageContent == "reportstepofworkonstatisticrepaybyprogram") {
        var _faculty = $("#faculty-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");
        var _program = $("#program-report-step-of-work-on-statistic-repay-by-program-hidden").val().split(";");

        _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-search-report-step-of-work").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

        SetMsgLoading("กำลังโหลด...");
        
        SearchData("reportstepofworkonstatisticrepaybyprogram", _send, "record-count-report-step-of-work", "list-data-search-report-step-of-work", "nav-page-search-report-step-of-work");
    }

    if (_pageContent == "reportstudentonstatisticcontractbyprogram") {
        var _faculty = $("#faculty-report-student-on-statistic-contract-by-program-hidden").val().split(";");
        var _program = $("#program-report-student-on-statistic-contract-by-program-hidden").val().split(";");
        var _searchTab = $("#link-tab1-report-student-on-statistic-contract-by-program").hasClass("active") == true ? 1 : 2;

        _send[_send.length] = "acadamicyear=" + $("#acadamicyear-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-search-report-student-contract").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "searchtab=" + _searchTab;

        var _idRecordCount;
        var _idListSearch;
        var _idNavPageSearch;

        if (_searchTab == 1) {
            _idRecordCount = "record-count-student-sign-contract";
            _idListSearch = "list-data-student-sign-contract";
            _idNavPageSearch = "nav-page-student-sign-contract";
        }

        if (_searchTab == 2) {
            _idRecordCount = "record-count-student-contract-penalty";
            _idListSearch = "list-data-student-contract-penalty";
            _idNavPageSearch = "nav-page-student-contract-penalty";
        }

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstudentonstatisticcontractbyprogram", _send, _idRecordCount, _idListSearch, _idNavPageSearch);
    }

    if (_pageContent == "reportnoticerepaycomplete") {
        var _faculty = $("#faculty-report-notice-repay-complete-hidden").val().split(";");
        var _program = $("#program-report-notice-repay-complete-hidden").val().split(";");

        _send[_send.length] = "studentid=" + $("#id-name-report-notice-repay-complete-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : ""); ;
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportnoticerepaycomplete", _send, "record-count-cp-report-notice-repay-complete", "list-data-report-notice-repay-complete", "nav-page-report-notice-repay-complete");
    }

    if (_pageContent == "reportnoticeclaimdebt") {
        var _faculty = $("#faculty-report-notice-claim-debt-hidden").val().split(";");
        var _program = $("#program-report-notice-claim-debt-hidden").val().split(";");

        _send[_send.length] = "studentid=" + $("#id-name-report-notice-claim-debt-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportnoticeclaimdebt", _send, "record-count-cp-report-notice-claim-debt", "list-data-report-notice-claim-debt", "nav-page-report-notice-claim-debt");
    }

    if (_pageContent == "reportstatisticpaymentbydate") {
        var _faculty = $("#faculty-report-statistic-payment-by-date-hidden").val().split(";");
        var _program = $("#program-report-statistic-payment-by-date-hidden").val().split(";");
        var _formatPayment = $("#format-payment-report-statistic-payment-by-date-hidden").val().split(";");

        _send[_send.length] = "studentid=" + $("#id-name-report-statistic-payment-by-date-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");
        _send[_send.length] = "datestart=" + $("#date-start-report-statistic-payment-by-date-hidden").val();
        _send[_send.length] = "dateend=" + $("#date-end-report-statistic-payment-by-date-hidden").val();

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportstatisticpaymentbydate", _send, "record-count-cp-report-statistic-payment-by-date", "list-data-report-statistic-payment-by-date", "nav-page-report-statistic-payment-by-date");
    }
    
    if (_pageContent == "reportecontract") {
        var _faculty = $("#faculty-report-e-contract-hidden").val().length > 0 ? $("#faculty-report-e-contract-hidden").val().split(";") : "";
        var _program = $("#program-report-e-contract-hidden").val().length > 0 ? $("#program-report-e-contract-hidden").val().split(";") : "";

        _send[_send.length] = "acadamicyear=" + $("#acadamicyear-report-e-contract-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-report-e-contract-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        /*
        _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");
        */

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportecontract", _send, "record-count-cp-report-e-contract", "list-data-report-e-contract", "nav-page-report-e-contract");
    }

    if (_pageContent == "reportdebtorcontractbyprogram") {
        var _reportOrder = $("#report-debtor-contract-order").val();
        var _faculty = $("#faculty-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#faculty-report-debtor-contract-by-program-hidden").val().split(";") : "";
        var _program = $("#program-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#program-report-debtor-contract-by-program-hidden").val().split(";") : "";
        var _formatPayment = $("#format-payment-report-debtor-contract-by-program-hidden").val().length > 0 ? $("#format-payment-report-debtor-contract-by-program-hidden").val().split(";") : "";

        _send[_send.length] = "reportorder=" + _reportOrder;
        _send[_send.length] = "datestart=" + $("#date-start-report-debtor-contract-hidden").val();
        _send[_send.length] = "dateend=" + $("#date-end-report-debtor-contract-hidden").val();
        _send[_send.length] = "studentid=" + $("#id-name-report-debtor-contract-by-program-hidden").val();
        _send[_send.length] = "faculty=" + (_faculty[0] != null ? _faculty[0] : "");
        _send[_send.length] = "programcode=" + (_program[0] != null ? _program[0] : "");
        _send[_send.length] = "majorcode=" + (_program[2] != null ? _program[2] : "");
        _send[_send.length] = "groupnum=" + (_program[3] != null ? _program[3] : "");
        _send[_send.length] = "formatpayment=" + (_formatPayment[0] != null ? _formatPayment[0] : "");

        SetMsgLoading("กำลังโหลด...");

        SearchData("reportdebtorcontractbyprogram", _send, "record-count-cp-report-debtor-contract-by-program", "list-data-report-debtor-contract-by-program", "nav-page-report-debtor-contract-by-program");
    }
}

function CalculateFrm(_cal, _valueSend, _callbackFunc) {
    var _i;
    var _j = 2;
    var _send = new Array();
    _send[_send.length] = "action=calculate";
    _send[_send.length] = "cal=" + _cal;

    if (_valueSend.length > 0) {
        for (_i = 0; _i < _valueSend.length; _i++) {
            _send[_j] = _valueSend[_i];
            _j++;
        }
    }

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", (_msgLoading.length > 0 ? true : false), (_msgLoading.length > 0 ? true : false), function (_result) {
        _callbackFunc(_result);
    });
}

function CloseFrm(_dialogFrm, _frmClose) {
    if (_dialogFrm == true) {
        if ($("#dialog-form2").dialog("isOpen") == true) {
            $("#dialog-form2").dialog("close");
            return;
        }

        if ($("#dialog-form1").dialog("isOpen") == true) {
            $("#dialog-form1").dialog("close");
            return;
        }
    }

    switch (_frmClose) {
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
    var _error = false;
    var _msg;
    var _focus;
    var _overpayment = $("#overpayment-hidden").val();

    if (parseInt(_overpayment) > 0) {
        var _totalInterestOverpayment = DelCommas("total-interest-overpayment");

        if (_error == false && (_totalInterestOverpayment.length == 0 || _totalInterestOverpayment == "0.00")) {
            _error = true;
            _msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระ";
            _focus = "";
        }

        if (_error == false && ($("#overpayment-date-start-old").val() != $("#overpayment-date-start").val())) {
            _error = true;
            _msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";
            _focus = "";
        }

        if (_error == false && ($("#overpayment-date-end-old").val() != $("#overpayment-date-end").val())) {
            _error = true;
            _msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";_focus = "";
        }

        if (_error == false && ($("#overpayment-interest-old").val() != DelCommas("overpayment-interest"))) {
            _error = true;
            _msg = "กรุณาคำนวณดอกเบี้ยผิดนัดชำระใหม่อีกครั้ง";
            _focus = "";
        }
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _error = false;
    var _msg;
    var _focus;
    var _repayDateEnd = GetDateObject($("#repay-date-end-hidden").val());
    var _overpaymentDateStart = GetDateObject($("#overpayment-date-start").val());

    if (_error == false && (DateDiff(_repayDateEnd, _overpaymentDateStart, "days") <= 0)) {
        _error = true;
        _msg = "กรุณาใส่วันที่เริ่มผิดนัดชำระให้ถูกต้อง";
        _focus = "#overpayment-date-start";
    }

    if (_error == false && (($("#overpayment-interest").val().length == 0) || ($("#overpayment-interest").val() == "0.00"))) {
        _error = true;
        _msg = "กรุณาใส่อัตราดอกเบี้ยผิดนัดชำระ";
        _focus = "#overpayment-interest";
    }

    if (_error == false && parseFloat($("#overpayment-interest").val()) > 100) {
        _error = true;
        _msg = "กรุณาใส่อัตราดอกเบี้ยผิดนัดชำระไม่เกิน 100";
        _focus = "#overpayment-interest";
    }

    if (_error == true) {
        FillCalInterestOverpayment("");
        DialogMessage(_msg, _focus, false, "");
        return false;
    }

    return true;
}

function CalculateInterestOverpayment() {
    if (ValidateCalculateInterestOverpayment() == true) {        
        var _send = new Array();
        _send[_send.length] = "capital=" + DelCommas("capital");
        _send[_send.length] = "overpaymentyear=" + DelCommas("overpayment-year");
        _send[_send.length] = "overpaymentday=" + DelCommas("overpayment-day");
        _send[_send.length] = "overpaymentinterest=" + DelCommas("overpayment-interest");
        _send[_send.length] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
        _send[_send.length] = "overpaymentdateend=" + $("#overpayment-date-end").val();
        _send[_send.length] = "totalinterestpayrepay=" + ($("#total-interest-pay-repay").length > 0 ? ($("#total-interest-pay-repay").val().length > 0 ? DelCommas("total-interest-pay-repay") : "0.00") : "0.00");
        _send[_send.length] = "totalaccruedinterest=" + ($("#total-accrued-interest").length > 0 ? DelCommas("total-accrued-interest") : "0.00");

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestoverpayment", _send, function (_result) {
            FillCalInterestOverpayment(_result);
        });

        return true;
    }

    return false;
}

function FillCalInterestOverpayment(_result) {
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

    if (_result.length > 0) {
        var _dataOverpaymentYear = _result.split("<overpaymentyear>");
        var _dataOverpaymentDay = _result.split("<overpaymentday>");
        var _dataTotalInterestOverpayment = _result.split("<totalinterestoverpayment>");
        var _dataTotalInterest = _result.split("<totalinterest>");
        var _dataTotalPayment = _result.split("<totalpayment>");

        if ($("#overpayment-year").length > 0)
            $("#overpayment-year").val(_dataOverpaymentYear[1]);

        if ($("#overpayment-day").length > 0)
            $("#overpayment-day").val(_dataOverpaymentDay[1]);

        if ($("#total-interest-overpayment").length > 0)
            $("#total-interest-overpayment").val(_dataTotalInterestOverpayment[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(_dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(_dataTotalPayment[1]);

        if ($("#pay").length > 0)
            $("#pay").val(_dataTotalPayment[1]);
    }
}

function ValidateCalInterestPayRepay() {
    var _error = false;
    var _msg;
    var _focus;
    var _totalInterestPayRepay = DelCommas("total-interest-pay-repay");

    if (_error == false && (_totalInterestPayRepay.length == 0 || _totalInterestPayRepay == "0.00")) {
        _error = true;
        _msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระ";
        _focus = "";
    }

    if (_error == false && ($("#pay-repay-date-end-old").val() != $("#pay-repay-date-end").val())) {
        _error = true;
        _msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระใหม่อีกครั้ง";
        _focus = "";
    }

    if (_error == false && ($("#pay-repay-interest-old").val() != DelCommas("pay-repay-interest"))) {
        _error = true;
        _msg = "กรุณาคำนวณดอกเบี้ยผ่อนชำระใหม่อีกครั้ง";
        _focus = "";
    }

    if (_error == true) {
        DialogMessage(_msg, _focus, false, "");
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
    var _error = false;
    var _msg;
    var _focus;

    if (_error == false && (($("#pay-repay-interest").val().length == 0) || ($("#pay-repay-interest").val() == "0.00"))) {
        _error = true;
        _msg = "กรุณาใส่อัตราดอกเบี้ยผ่อนชำระ";
        _focus = "#pay-repay-interest";
    }

    if (_error == false && parseFloat($("#pay-repay-interest").val()) > 100) {
        _error = true;
        _msg = "กรุณาใส่อัตราดอกเบี้ยผ่อนชำระไม่เกิน 100";
        _focus = "#pay-repay-interest";
    }

    if (_error == true) {
        FillCalInterestPayRepay("");
        DialogMessage(_msg, _focus, false, "");
        return false;
    }

    return true;
}

function CalculateInterestPayRepay() {
    if (ValidateCalculateInterestPayRepay() == true) {        
        var _send = new Array();
        _send[_send.length] = "capital=" + DelCommas("capital");
        _send[_send.length] = "payrepayyear=" + DelCommas("pay-repay-year");
        _send[_send.length] = "payrepayday=" + DelCommas("pay-repay-day");
        _send[_send.length] = "payrepayinterest=" + DelCommas("pay-repay-interest");
        _send[_send.length] = "payrepaydatestart=" + $("#pay-repay-date-start").val();
        _send[_send.length] = "payrepaydateend=" + $("#pay-repay-date-end").val();
        _send[_send.length] = "totalinterestoverpayment=" + ($("#total-interest-overpayment").length > 0 ? ($("#total-interest-overpayment").val().length > 0 ? DelCommas("total-interest-overpayment") : "0.00") : "0.00");
        _send[_send.length] = "totalaccruedinterest=" + ($("#total-accrued-interest").length > 0 ? DelCommas("total-accrued-interest") : "0.00");

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestpayrepay", _send, function (_result) {
            FillCalInterestPayRepay(_result);
        });

        return true;
    }

    return false;
}

function FillCalInterestPayRepay(_result) {
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

    if (_result.length > 0) {
        var _dataPayRepayYear = _result.split("<payrepayyear>");
        var _dataPayRepayDay = _result.split("<payrepayday>");
        var _dataTotalInterestPayRepay = _result.split("<totalinterestpayrepay>");
        var _dataTotalInterest = _result.split("<totalinterest>");
        var _dataTotalPayment = _result.split("<totalpayment>");

        if ($("#pay-repay-year").length > 0)
            $("#pay-repay-year").val(_dataPayRepayYear[1]);

        if ($("#pay-repay-day").length > 0)
            $("#pay-repay-day").val(_dataPayRepayDay[1]);

        if ($("#total-interest-pay-repay").length > 0)
            $("#total-interest-pay-repay").val(_dataTotalInterestPayRepay[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(_dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(_dataTotalPayment[1]);

        if ($("#pay").length > 0)
            $("#pay").val(_dataTotalPayment[1]);
    }
}

function CalculateInterestOverpaymentAndPayRepay() {
    if (ValidateCalculateInterestOverpayment() == true && ValidateCalculateInterestPayRepay() == true) {        
        var _send = new Array();
        _send[_send.length] = "capital=" + DelCommas("capital");
        _send[_send.length] = "overpaymentyear=" + DelCommas("overpayment-year");
        _send[_send.length] = "overpaymentday=" + DelCommas("overpayment-day");
        _send[_send.length] = "overpaymentinterest=" + DelCommas("overpayment-interest");
        _send[_send.length] = "overpaymentdatestart=" + $("#overpayment-date-start").val();
        _send[_send.length] = "overpaymentdateend=" + $("#overpayment-date-end").val();
        _send[_send.length] = "payrepayyear=" + DelCommas("pay-repay-year");
        _send[_send.length] = "payrepayday=" + DelCommas("pay-repay-day");
        _send[_send.length] = "payrepayinterest=" + DelCommas("pay-repay-interest");
        _send[_send.length] = "payrepaydatestart=" + $("#pay-repay-date-start").val();
        _send[_send.length] = "payrepaydateend=" + $("#pay-repay-date-end").val();

        SetMsgLoading("กำลังคำนวณ...");

        CalculateFrm("interestoverpaymentandpayrepay", _send, function (_result) {
            FillCalInterestOverpaymentAndPayRepay(_result);
        });

        return true;
    }

    return false;
}

function FillCalInterestOverpaymentAndPayRepay(_result) {
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

    if (_result.length > 0) {
        var _dataOverpaymentYear = _result.split("<overpaymentyear>");
        var _dataOverpaymentDay = _result.split("<overpaymentday>");
        var _dataTotalInterestOverpayment = _result.split("<totalinterestoverpayment>");
        var _dataPayRepayYear = _result.split("<payrepayyear>");
        var _dataPayRepayDay = _result.split("<payrepayday>");
        var _dataTotalInterestPayRepay = _result.split("<totalinterestpayrepay>");
        var _dataTotalInterest = _result.split("<totalinterest>");
        var _dataTotalPayment = _result.split("<totalpayment>");

        if ($("#overpayment-year").length > 0)
            $("#overpayment-year").val(_dataOverpaymentYear[1]);

        if ($("#overpayment-day").length > 0)
            $("#overpayment-day").val(_dataOverpaymentDay[1]);

        if ($("#total-interest-overpayment").length > 0)
            $("#total-interest-overpayment").val(_dataTotalInterestOverpayment[1]);

        if ($("#pay-repay-year").length > 0)
            $("#pay-repay-year").val(_dataPayRepayYear[1]);

        if ($("#pay-repay-day").length > 0)
            $("#pay-repay-day").val(_dataPayRepayDay[1]);

        if ($("#total-interest-pay-repay").length > 0)
            $("#total-interest-pay-repay").val(_dataTotalInterestPayRepay[1]);

        if ($("#total-interest").length > 0)
            $("#total-interest").val(_dataTotalInterest[1]);

        if ($("#total-payment").length > 0)
            $("#total-payment").val(_dataTotalPayment[1]);
    }
}

function CalculateChkBalance() {
    var _send = new Array();
    _send[_send.length] = "capital=" + DelCommas("capital");
    _send[_send.length] = "totalinterest=" + DelCommas("total-interest");
    _send[_send.length] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");
    _send[_send.length] = "totalpayment=" + DelCommas("total-payment");
    _send[_send.length] = "pay=" + DelCommas("pay");

    SetMsgLoading("");

    CalculateFrm("chkbalance", _send, function (_result) {
        FillCalChkBalance(_result);        
    });
}

function FillCalChkBalance(_result) {
    if (_result.length > 0) {     
        var _dataCapital = _result.split("<capital>");
        var _dataTotalInterest = _result.split("<totalinterest>");
        var _dataTotalAccruedInterest = _result.split("<totalaccruedinterest>");
        var _dataTotalPayment = _result.split("<totalpayment>");
        var _dataPay = _result.split("<pay>");
        var _dataPayCapital = _result.split("<paycapital>");
        var _dataPayInterest = _result.split("<payinterest>");        
        var _dataRemainCapital = _result.split("<remaincapital>");
        var _dataAccruedInterest = _result.split("<accruedinterest>");
        var _dataRemainAccruedInterest = _result.split("<remainaccruedinterest>");

        if ($("#chk-balance-capital").length > 0) {
            $("#chk-balance-capital").html(_dataCapital[1]);
            $("#chk-balance-capital-unit").show();
        }

        if ($("#chk-balance-total-interest").length > 0) {
            $("#chk-balance-total-interest").html(_dataTotalInterest[1]);
            (_dataTotalInterest[1].length > 0 ? $("#chk-balance-total-interest-unit").show() : "");
        }

        if ($("#chk-balance-total-accrued-interest").length > 0) {
            $("#chk-balance-total-accrued-interest").html(_dataTotalAccruedInterest[1]);
            (_dataTotalAccruedInterest[1].length > 0 ? $("#chk-balance-total-accrued-interest-unit").show() : "");
        }

        if ($("#chk-balance-total-payment").length > 0) {
            $("#chk-balance-total-payment").html(_dataTotalPayment[1]);
            (_dataTotalPayment[1].length > 0 ? $("#chk-balance-total-payment-unit").show() : "");
        }

        if ($("#chk-balance-pay").length > 0) {
            $("#chk-balance-pay").html(_dataPay[1]);
            (_dataPay[1].length > 0 ? $("#chk-balance-pay-unit").show() : "");
        }

        if ($("#chk-balance-pay-capital").length > 0) {
            $("#chk-balance-pay-capital").html(_dataPayCapital[1]);
            (_dataPayCapital[1].length > 0 ? $("#chk-balance-pay-capital-unit").show() : "");
        }

        if ($("#chk-balance-pay-interest").length > 0) {
            $("#chk-balance-pay-interest").html(_dataPayInterest[1]);
            (_dataPayInterest[1].length > 0 ? $("#chk-balance-pay-interest-unit").show() : "");
        }

        if ($("#chk-balance-remain-capital").length > 0) {
            $("#chk-balance-remain-capital").html(_dataRemainCapital[1]);
            (_dataRemainCapital[1].length > 0 ? $("#chk-balance-remain-capital-unit").show() : "");
        }

        if ($("#chk-balance-accrued-interest").length > 0) {
            $("#chk-balance-accrued-interest").html(_dataAccruedInterest[1]);
            (_dataAccruedInterest[1].length > 0 ? $("#chk-balance-accrued-interest-unit").show() : "");
        }

        if ($("#chk-balance-remain-accrued-interest").length > 0) {
            $("#chk-balance-remain-accrued-interest").html(_dataRemainAccruedInterest[1]);
            (_dataRemainAccruedInterest[1].length > 0 ? $("#chk-balance-remain-accrued-interest-unit").show() : "");
        }
    }
}

function CalculateTotalPayment() {
    var _send = new Array();
    _send[_send.length] = "capital=" + DelCommas("capital");
    _send[_send.length] = "totalinterest=" + DelCommas("total-interest");
    _send[_send.length] = "totalaccruedinterest=" + DelCommas("total-accrued-interest");

    SetMsgLoading("");

    CalculateFrm("totalpayment", _send, function (_result) {
        var _dataTotalPayment = _result.split("<totalpayment>");

        if ($("#total-payment").length > 0) {
            $("#total-payment").val(_dataTotalPayment[1]);

            if ($("#pay").length > 0)
                $("#pay").val($("#total-payment").val());
        }
    });
}

function BoxSearchCondition(_countCondition, _searchValue, _id) {
    var _showCondition = new Array();
    var _i, _j = 0;

    for (_i = 0; _i < _countCondition; _i++) {        
        if (_searchValue[_i].length > 0) {
            _showCondition[_j] = _i;
            _j++;
        }

        $("#" + _id + "-order" + (_i + 1)).hide();
    }

    if (_showCondition.length > 0) {
        $("#" + _id).show();

        for (_i = 0; _i < _showCondition.length; _i++) {
            $("#" + _id + "-order" + (_showCondition[_i] + 1)).show();
            $("#" + _id + "-order" + (_showCondition[_i] + 1) + "-value").html(_searchValue[_showCondition[_i]]);
        }
    }
    else
        $("#" + _id).hide();
}

function ShowDocEContract(_sid, _path, _file) {
    $("#report-e-contract" + _sid).addClass("active");

    var _send = new Array();
    _send[_send.length] = "action=econtract";
    _send[_send.length] = "path=" + _path;
    _send[_send.length] = "file=" + _file;

    LoadAjax(_send.join("&"), "Handler/eCPHandler.ashx", "POST", false, false, function (_result) {
        var _dataEContract = _result.split("<econtract>");

        if (_dataEContract[1] == "1") {
            DialogMessage("ไม่พบไฟล์เอกสาร", "", false, "report-e-contract" + _sid);
            return;
        }

        $("#report-e-contract" + _sid).removeClass("active");

        window.open(_path + _file, "_blank");
    });
}