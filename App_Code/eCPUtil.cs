/*
Description         : สำหรับรวบรวมฟังก์ชั่นการทำงานทั่วไป
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๒๔/๐๖/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

public class eCPUtil
{  
    public const string URL_PICTURE_STUDENT = "http://intranet.student.mahidol/studentweb/resources/images/";
    public const int WIDTH_PICTURE_STUDENT = 119;
    public const int HEIGHT_PICTURE_STUDENT = 120;
    public const int ROW_PER_PAGE = 50;
    public const double PAYMENT_AT_LEAST = 30;
    public const double PAY_REPAY_LEAST = 500;
    public const int PERIOD_REPAY_LEAST = 40;
        
    private static string[,] _pageOrder = new string[,]
    {
        {
            "Home",
            "CPTabUser",
            "CPTabProgram",
            "CPTabCalDate",
            "CPTabInterest",
            "CPTabPayBreakContract",
            "CPTabScholarship",
            "CPTransRequireContract",
            "CPTransPayment",
            "CPReportStepOfWork",
            "CPReportTableCalCapitalAndInterest",
            "CPReportStatisticRepay",
            "CPReportNoticeClaimDebt",
            "CPReportStatisticContract",
            "CPReportStatisticPaymentByDate",
            "CPReportEContract",
            "CPReportDebtorContract",
            "CPReportDebtorContractPaid",
            "CPReportDebtorContractRemain"
        },
        {
            "Home",
            "CPTabUser",
            "CPTransBreakContract",
            "CPReportStepOfWork",
            "CPReportStatisticRepay",
            "CPReportNoticeRepayComplete",
            "CPReportStatisticContract",
            "CPReportStatisticPaymentByDate",
            "CPReportEContract",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        },
        {
            "Home",
            "CPReportDebtorContract",
            "CPReportDebtorContractPaid",
            "CPReportDebtorContractRemain",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        }
    };

    public static int[,] _activeMenu = new int[,] 
    {
        { 1, 2, 3, 3, 3, 3, 3, 4, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
        { 1, 2, 3, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    public static string[,,] _actionTrackingStatus = new string[,,]
    {
        {
            { "1111", "v1" }, 
            { "1112", "v1" },
            { "2111", "a1" },
            { "2121", "v1" },
            { "2122", "v1" },
            { "2211", "e2" },
            { "2212", "v2" }
        },
        {
            { "1111", "e1" },
            { "1112", "v1" },
            { "2111", "v1" },
            { "2121", "e1" },
            { "2122", "v1" },
            { "2211", "v2" },
            { "2212", "v2" }
        },
        {
            { "1111", "v1" },
            { "1112", "v1" },
            { "2111", "v1" },
            { "2121", "v1" },
            { "2122", "v1" },
            { "2211", "v2" },
            { "2212", "v2" }
        }
    };

    public static string[,] _iconTrackingStatus = new string[,]
    {
        { "1111", "status-send" },
        { "1112", "status-cancel" },
        { "2111", "status-receiver-disable" },
        { "2121", "status-edit" },
        { "2122", "status-cancel" },
        { "2211", "status-receiver" },
        { "2212", "status-cancel" }
    };

    public static string[,] _iconRepayStatus = new string[,]
    {
        { "status-repay-wait-disable", "status-repay-wait", "", "" },
        { "status-repay-count1-disable", "status-repay-count1-wait", "status-repay-count1-yes", "status-repay-count1-no" },
        {"status-repay-count2-disable", "status-repay-count2-wait", "status-repay-count2-yes", "status-repay-count2-no" },
        {"status-repay-success-disable", "status-repay-success-wait", "status-repay-success", "" }
    };

    public static string[] _iconPaymentStatus = new string[]
    {
        "status-payment-disable",
        "status-payment-wait",
        "status-payment-success"
    };

    public static string[,] _iconEContractStatus = new string[,] 
    {
        { "0", "status-e-contract-uncomplete" },
        { "1", "status-e-contract-complete" }
    };

    public static string[,] _dlevel = new string[,]
    {
        { "ต่ำกว่าปริญญาตรี", "U" },
        { "ปริญญาตรี", "B" }
    };

    public static string[,] _caseGraduate = new string[,]
    {
        { "ก่อนสำเร็จการศึกษา", "ไม่สำเร็จการศึกษา" },
        { "หลังสำเร็จการศึกษา", "สำเร็จการศึกษา" }
    };

    public static string[] _civil = new string[]
    {
        "ปฏิบัติงานชดใช้",
        "ไม่ปฏิบัติงานชดใช้"
    };

    public static string[] _scholar = new string[]
    {
        "ได้รับทุนการศึกษา",
        "ไม่ได้รับทุนการศึกษา"
    };
    
    public static string[] _studyLeave = new string[]
    {
        "ไม่มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุน",
        "มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุน"
    };
    
    public static string[,] _trackingStatusORAA = new string[,]
    {
        { "รอส่งรายการแจ้ง", "1" },
        { "รอรับรายการแจ้ง", "2" },
        { "รับรายการแจ้งแล้ว", "3" },
        { "ส่งกลับแก้ไข", "4" },
        { "รายการแจ้งถูกยกเลิก", "5" },
        { "รอส่งรายการแจ้ง, ส่งกลับแก้ไข, รายการแจ้งถูกยกเลิก", "6" }
    };

    public static string[,] _trackingStatusORLA = new string[,]
    {
        { "รอรับรายการแจ้ง", "2" },
        { "รับรายการแจ้งแล้ว", "3" },
        { "ส่งกลับแก้ไข", "4" },
        { "รายการแจ้งถูกยกเลิก", "5" }
    };

    public static string[] _repayStatus = new string[]
    {
        "รอแจ้งชำระหนี้",
        "แจ้งชำระหนี้ครั้งที่ 1",
        "แจ้งชำระหนี้ครั้งที่ 2"
    };

    public static string[] _repayStatusDetail = new string[]
    {
        "รอแจ้งชำระหนี้",
        "แจ้งชำระหนี้ครั้งที่ 1 ( รอเอกสารตอบกลับ )",
        "แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )",
        "แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )",
        "แจ้งชำระหนี้ครั้งที่ 2 ( รอเอกสารตอบกลับ )",
        "แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )",
        "แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )",
        "อยู่ระหว่างการชำระหนี้",
        "ชำระหนี้เรียบร้อย"
    };

    public static string[] _resultReply = new string[] 
    {
        "ผู้ผิดสัญญารับทราบให้ชำระหนี้",
        "ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้"
    };

    public static string[] _paymentStatus = new string[] 
    { 
        "ยังไม่ได้ชำระหนี้",
        "อยู่ระหว่างการชำระหนี้",
        "ชำระหนี้เรียบร้อย"
    };

    public static string[] _paymentFormat = new string[]
    {
        "ชำระแบบเต็มจำนวน",
        "ชำระแบบผ่อนชำระ"
    };

    public static string[] _payChannel = new string[] 
    {
        "ชำระด้วยเงินสด",
        "ชำระด้วยเช็คขีดคร่อม",
        "นำฝากบัญชีเงินฝาก"
    };

    public static string[,] _calInterestYesNo = new string[,]
    {
        { "คิดดอกเบี้ย", "Y" },
        { "ไม่คิดตอกเบี้ย", "N" }
    };

    public static string[,] _stepOfWorkStatus = new string[,]
    {
        { "รอส่งรายการแจ้ง", "1" },
        { "รอรับรายการแจ้ง", "2" },
        { "รับรายการแจ้งแล้ว", "3" },
        { "ส่งกลับแก้ไข", "4" },
        { "รายการแจ้งถูกยกเลิก", "5" },
        { "รอแจ้งชำระหนี้", "6" },
        { "แจ้งชำระหนี้ครั้งที่ 1 ( รอเอกสารตอบกลับ )", "7" },
        { "แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )", "8" },
        { "แจ้งชำระหนี้ครั้งที่ 1 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )", "9" },
        { "แจ้งชำระหนี้ครั้งที่ 2 ( รอเอกสารตอบกลับ )", "10" },
        { "แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญารับทราบให้ชำระหนี้ )", "11" },
        { "แจ้งชำระหนี้ครั้งที่ 2 ( ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้ )", "12" },
        { "ยังไม่ได้ชำระหนี้", "13" },
        { "อยู่ระหว่างการชำระหนี้", "14" },
        { "ชำระหนี้เรียบร้อย", "15" }
    };

    public static string[,] _conditionTableCalCapitalAndInterest = new string[,]
    {
        { "จำนวนเงินต้นที่ต้องการชำระต่อเดือน", "1" },
        { "จำนวนงวดที่ต้องการชำระ", "2" }
    };

    public static string MenuBar(bool _loginResult)
    {
        string _html = String.Empty;
        string[,] _data;

        _html += "<div class='menu-bar'>" +
                 "  <div class='content-left'>" +
                 "      <ul>";

        if (_loginResult)
        {
            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            _html += "      <li class='have-link first-link'><a class='link-msg' id='menu1' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",1)'>&nbsp;หน้าแรก&nbsp;</a></li>";

            if (_eCPCookie["UserLevel"].Equals("Administrator"))
                _html += "  <li class='have-link'><a class='link-msg' id='menu2' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",2)'>บัญชีผู้ใช้</a></li>";

            if (_eCPCookie["UserSection"].Equals("1"))
            {
                if (_eCPCookie["UserLevel"].Equals("Administrator"))
                {
                    _html += "<li class='have-link' id='submenu31'><div class='link-msg' id='menu3'>ตั้งค่า</div>" +
                             "  <ul>" +
                             "      <li><a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",3)'>กำหนดหลักสูตรที่ให้มีการทำสัญญาการศึกษา</a></li>" +
                             "      <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",4)'>เงื่อนไขการคิดระยะเวลาตามสัญญาและสูตรคำนวณเงินชดใช้ตามสัญญา</a></li>" +
                             "      <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",5)'>กำหนดดอกเบี้ยจากการผิดนัดชำระ</a></li>" +
                             "      <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",6)'>เกณฑ์การชดใช้ตามสัญญา</a></li>" +
                             "      <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",7)'>กำหนดทุนการศึกษาแต่ละหลักสูตร</a></li>" +
                             "  </ul>" +
                             "</li>";
                }

                _html += "  <li class='have-link'><a class='link-msg' id='menu4' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",8)'>รับแจ้งผู้ผิดสัญญา / แจ้งชำระหนี้</a></li>" +
                         "  <li class='have-link'><a class='link-msg' id='menu5' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",9)'>ชำระหนี้ / ดอกเบี้ย</a></li>" +
                         "  <li class='have-link' id='submenu61'><div class='link-msg' id='menu6'>รายงาน</div>" +
                         "      <ul>" +
                         "          <li><a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",16)'>เอกสารสัญญาการเป็นนักศึกษา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",10)'>สถานะขั้นตอนการดำเนินงานของผู้ผิดสัญญา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",11)'>ตารางคำนวณเงินต้นและดอกเบี้ย</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",12)'>สถิติการผิดสัญญาและการชำระหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",15)'>สถิติการชำระหนี้ตามช่วงวันที่</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",14)'>สถิติการทำสัญญาและการผิดสัญญา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",13)'>หนังสือทวงถามผู้ผิดสัญญาและผู้ค้ำประกัน</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",17)'>ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",18)'>การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",19)'>ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้</a></li>" +
                         "      </ul>" +
                         "  </li>";
            }

            if (_eCPCookie["UserSection"].Equals("2"))
            {
                _html += "  <li class='have-link'><a class='link-msg' id='menu3' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",3)'>แจ้งผู้ผิดสัญญา</a></li>" +
                         "  <li class='have-link' id='submenu41'><div class='link-msg' id='menu4'>รายงาน</div>" +
                         "      <ul>" +
                         "          <li><a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",9)'>เอกสารสัญญาการเป็นนักศึกษา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",4)'>สถานะขั้นตอนการดำเนินงานของผู้ผิดสัญญา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",5)'>สถิติการผิดสัญญาและการชำระหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",8)'>สถิติการชำระหนี้ตามช่วงวันที่</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",7)'>สถิติการทำสัญญาและการผิดสัญญา</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",6)'>หนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณา</a></li>" +
                         "      </ul>" +
                         "  </li>";
            }

            if (_eCPCookie["UserSection"].Equals("3"))
            {
                _html += "  <li class='have-link' id='submenu21'><div class='link-msg' id='menu2'>รายงาน</div>" +
                         "      <ul>" +
                         "          <li><a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",2)'>ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",3)'>การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a></li>" +
                         "          <li><a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + _eCPCookie["UserSection"] + ",4)'>ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้</a></li>" +
                         "      </ul>" +
                         "  </li>";
            }
        }

        _html += "      </ul>" +
                 "  </div>" +
                 "  <div class='content-right'>" +
                 "      <ul>" +
                 /*
                 "          <li class='have-link first-link'><a class='link-img' id='mu-icon1' href='http://www.mahidol.ac.th' target='_blank'></a></li>" +
                 "          <li class='have-link'><a class='link-img' id='mu-icon2' href='http://webmail.mahidol.ac.th/' target='_blank'></a></li>" +
                 */
                 "          <li><div id='current-date'>&nbsp;&nbsp;" + Util.ShortDateTH(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"))) + "&nbsp;&nbsp;</div>";

        if (_loginResult)
        {
            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string _userid = GetUserID();

            _data = eCPDB.ListDetailCPTabUser(_userid, "", "", "");

            _html += "      <li><div id='whois'>ผู้ใช้งาน : " + _data[0, 3] + " ( " + eCPDB._userSection[int.Parse(_eCPCookie["UserSection"]) - 1] + " )&nbsp;</div></li>" +
                     "      <li class='have-link'><a class='link-msg' id='menu7' href='javascript:void(0)' onclick='ShowManual()'>คู่มือ</a></li>" +
                     "      <li class='have-link'><a class='link-msg' id='menu8' href='javascript:void(0)' onclick='Signout()'>ออกจากระบบ</a></li>";
        }

        _html += "      </ul>" +
                 "  </div>" +            
                 "  <div class='clear'></div>" +
                 "</div>";

        return _html;
    }

    public static string Head()
    {
        string _html = String.Empty;
        string _title = String.Empty;
        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (_eCPCookie["UserSection"].Equals("1"))
            _title = "orla";

        if (_eCPCookie["UserSection"].Equals("2"))
            _title = "oraa";

        if (_eCPCookie["UserSection"].Equals("3"))
            _title = "orfa";
        
        _html += "<div class='head-title-main'>" +
                 "  <div class='head-title-" + _title + "'>" +
                 "      <div class='head-title-" + _title + "-bg'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
        
    public static string Signin()
    {
        string _html = String.Empty;

        _html += "<div class='signin-space'></div>" +
                 "<div class='signin-layout'>" +
                 "  <div class='frm-signin'>" +
                 "      <form id='frm-signin' action='' onsubmit='return false' enctype='multipart/form-data'>" +
                 "          <div id='colusername'><input class='inputbox' type='text' id='username' onblur=Trim('username') value='' style='width:170px' /></div>" +
                 "          <div id='colpassword'><input class='inputbox' type='password' id='password' value='' onblur=Trim('password') style='width:170px' /></div>" +
                 "      </form>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='Signin()'>เข้าสู่ระบบ</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrm()'>ล้าง</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div id='manual'><a href='javascript:void(0)' onclick='ShowManual()'>คู่มือ</a></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string Manual()
    {
        string _html = String.Empty;

        _html += "<div class='slide' id='slide-manual'>" +
                 "  <div class='slide-tools' id='slide-tools-manual'>" +
                 "      <div class='slide-tool-content' id='slide-tool-content-manual'></div>" +
                 "      <div class='slide-tool' id='slide-tool-manual'></div>" +
                 "  </div>" +
                 "  <div class='slide-contents' id='slide-contents-manual'><img src='#' /></div>" +
                 "</div>";

        return _html;
    }

    public string GenPage(bool _loginResult, int _pid)
    {
        int _order = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (_eCPCookie["UserSection"].Equals("1"))
            _order = 0;

        if (_eCPCookie["UserSection"].Equals("2"))
            _order = 1;

        if (_eCPCookie["UserSection"].Equals("3"))
            _order = 2;

        Type _thisType = this.GetType();
        MethodInfo _theMethod = _thisType.GetMethod(_pageOrder[_order, _pid]);
        string _result = (string)_theMethod.Invoke(this, null);

        _eCPCookie.Values.Remove("Pid");
        _eCPCookie.Values.Add("Pid", (_pid + 1).ToString());
        HttpContext.Current.Response.AppendCookie(_eCPCookie);
        
        return _result;
    }
    
    public string Home()
    {
        string _html = String.Empty;

        _html += "<div class='home'></div>";

        return _html;
    }

    public static string ContentTitle(string _content)
    {
        string _html = String.Empty;

        _html += "<div class='content-data-title'>" +
                 "  <div class='content-data-title-" + _content + "'></div>" +
                 "</div>";

        return _html;
    }

    public string CPTabUser()
    {
        string _html = String.Empty;

        _html = eCPDataUser.TabCPTabUser();

        return _html;
    }

    public string CPTabProgram()
    {
        string _html = String.Empty;

        _html = eCPDataConfiguration.TabCPTabProgram();

        return _html;
    }

    public string CPTabCalDate()
    {
        string _html = String.Empty;

        _html = eCPDataConfiguration.ListCPTabCalDate();

        return _html;
    }

    public string CPTabInterest()
    {
        string _html = String.Empty;

        _html = eCPDataConfiguration.TabCPTabInterest();

        return _html;
    }

    public string CPTabPayBreakContract()
    {
        string _html = String.Empty;

        _html = eCPDataConfiguration.TabCPTabPayBreakContract();
        
        return _html;
    }

    public string CPTabScholarship()
    {
        string _html = String.Empty;

        _html = eCPDataConfiguration.TabCPTabScholarship();

        return _html;
    }

    public string CPTransBreakContract()
    {
        string _html = String.Empty;

        _html += eCPDataBreakContract.TabCPTransBreakContract();

        return _html;
    }

    public string CPTransRequireContract()
    {
        string _html = String.Empty;

        _html += eCPDataRequireContract.TabCPTransRequireContract();

        return _html;
    }

    public string CPTransPayment()
    {
        string _html = String.Empty;

        _html += eCPDataPayment.TabPaymentOnCPTransRequireContract();

        return _html;
    }

    public string CPReportStepOfWork()
    {
        string _html = String.Empty;

        _html += eCPDataReportStepOfWork.ListCPReportStepOfWork();

        return _html;
    }

    public string CPReportTableCalCapitalAndInterest()
    {
        string _html = String.Empty;

        _html += eCPDataReportTableCalCapitalAndInterest.TabCPReportTableCalCapitalAndInterest();

        return _html;
    }

    public string CPReportStatisticRepay()
    {
        string _html = String.Empty;

        _html += eCPDataReportStatisticRepay.TabCPReportStatisticRepay();

        return _html;
    }

    public string CPReportStatisticContract()
    {
        string _html = String.Empty;

        _html += eCPDataReportStatisticContract.TabCPReportStatisticContract();

        return _html;
    }
    
    public string CPReportNoticeRepayComplete()
    {
        string _html = String.Empty;

        _html += eCPDataReportNoticeRepayComplete.ListCPReportNoticeRepayComplete();

        return _html;
    }

    public string CPReportNoticeClaimDebt()
    {
        string _html = String.Empty;

        _html += eCPDataReportNoticeClaimDebt.ListCPReportNoticeClaimDebt();

        return _html;
    }

    public string CPReportStatisticPaymentByDate()
    {
        string _html = String.Empty;

        _html += eCPDataReportStatisticPaymentByDate.ListCPReportStatisticPaymentByDate();

        return _html;
    }

    public string CPReportEContract()
    {
        string _html = String.Empty;

        _html += eCPDataReportEContract.ListCPReportEContract();

        return _html;
    }
    
    public string CPReportDebtorContract()
    {
        string _html = String.Empty;

        _html += eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontract");

        return _html;
    }

    public string CPReportDebtorContractPaid()
    {
        string _html = String.Empty;

        _html += eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontractpaid");

        return _html;
    }

    public string CPReportDebtorContractRemain()
    {
        string _html = String.Empty;

        _html += eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontractremain");

        return _html;
    }

    public static string ListTitleName(string _id)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _i;

        _html += "<div class='combobox'>" +
                 "  <select id='" + _id + "'>" +
                 "      <option value='0'>เลือกคำนำหน้าชื่อ</option>";

        _data = eCPDB.ListTitlename();
        _recordCount = _data.GetLength(0);

        for (_i = 0; _i < _recordCount; _i++)
        {
            _html += "  <option value='" + _data[_i, 0] + ";" + _data[_i, 1] + ";" + _data[_i, 2] + "'>" + _data[_i, 2] + "</option>";
        }

        _html += "  </select>" +
                 "</div>";

        return _html;
    }

    public static string ListFaculty(bool _cpTabProgram, string _id)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _i;

        _html += "<div class='combobox'>" +
                 "  <select id='" + _id + "'>" +
                 "      <option value='0'>เลือกคณะ</option>";

        _data = eCPDB.ListFaculty(_cpTabProgram);
        _recordCount = _data.GetLength(0);

        for (_i = 0; _i < _recordCount; _i++)
        {
            _html += "  <option value='" + _data[_i, 0] + ";" + _data[_i, 1] + "'>" + _data[_i, 0] + " - " + _data[_i, 1] + "</option>";
        }

        _html += "  </select>" +
                 "</div>";

        return _html;
    }

    public static string ListProgram(bool _cpTabProgram, string _id, string _dlevel, string _faculty)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string[,] _data;
        int _recordCount;
        int _i;

        _html += "<div class='combobox'>" +
                 "  <select id='" + _id + "'>" +
                 "      <option value='0'>เลือกหลักสูตร</option>";

        if (!_faculty.Equals("0"))
        {
            _data = eCPDB.ListProgram(_cpTabProgram, _dlevel, _faculty);
            _recordCount = _data.GetLength(0);                            

            for (_i = 0; _i < _recordCount; _i++)
            {
                _groupNum = !_data[_i, 2].Equals("0") ? " ( กลุ่ม " + _data[_i, 2] + " )" : "";
                _html += "<option value='" + _data[_i, 0] + ";" + _data[_i, 3] + ";" + _data[_i, 1] + ";" + _data[_i, 2] + ";" + _data[_i, 4] + ";" + _data[_i, 5] + "'>" + _data[_i, 0] + " - " + _data[_i, 3] + _groupNum + "</option>";
            }
        }

        _html += "  </select>" +
                 "</div>";

        return "<list>" + _html + "<list>";
    }

    public static string ListCalDate(string _id)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _i;

        _html += "<div class='combobox'>" +
                 "  <select id='" + _id + "'>" +
                 "      <option value='0'>เลือกวิธีคิดและคำนวณเงินชดใช้</option>";
                    
        _data = eCPDB.ListCPTabCalDate("");
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            for (_i = 0; _i < _recordCount; _i++)
            {
                _html += "<option value='" + _data[_i, 0] + "'>วิธีที่ " + _data[_i, 0] + "</option>";
            }
        }
    
        _html += "  </select>" +
                 "</div>";

        return "<list>" + _html + "<list>";
    }

    public static string ListProvince(string _id)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _i;

        _html += "<div class='combobox'>" +
                 "  <select id='" + _id + "'>" +
                 "      <option value='0'>เลือกจังหวัด</option>";
                    
        _data = eCPDB.ListProvince();
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            for (_i = 0; _i < _recordCount; _i++)
            {
                _html += "<option value='" + _data[_i, 0] + "'>" + _data[_i, 1] + "</option>";
            }
        }
    
        _html += "  </select>" +
                 "</div>";

        return "<list>" + _html + "<list>";
    }

    public static double[] CalPayScholarship(string _scholar, string _caseGraduate, string _civil, string _scholarshipMoney, string _scholarshipYear, string _scholarshipMonth, string _allActualMonthScholarship)
    {
        double[] _result = new double[2];

        if (_scholar.Equals("1"))
        {
            double _sMoney = double.Parse(_scholarshipMoney);
            double _sYear = double.Parse(_scholarshipYear);
            double _sMonth = double.Parse(_scholarshipMonth);

            _sYear = _sYear + (_sMonth / 12);

            switch (_caseGraduate)
            {
                case "1":
                    double _aMonth = double.Parse(_allActualMonthScholarship);        

                    _result[0] = _sMoney / 12;
                    _result[1] = _aMonth * _result[0];
                    break;
                case "2":
                    _result[0] = 0;                            
                    //_result[1] = _civil.Equals("1") ? (_sMoney * _sYear) : ((_sMoney * _sYear) * 2);
                    _result[1] = _civil.Equals("1") ? _sMoney : ((_sMoney * _sYear) * 2);
                    break;
            }
        }
        else
        {
            _result[0] = 0;
            _result[1] = 0;
        }

        return _result;
    }

    public static double[] GetCalPenalty(string _studyLeave, string _beforeStudyLeaveStartDate, string _beforeStudyLeaveEndDate, string _afterStudyLeaveStartDate, string _afterStudyLeaveEndDate, string _scholar, string _caseGraduate, string _educationDate, string _graduateDate, string _civil, string _totalPayScholarship, string _scholarshipYear, string _scholarshipMonth, string _dateStart, string _dateEnd, string _indemnitorYear, string _indemnitorCash, string _calDateCondition)
    {
        double[] _result = new double[15];

        if (!_studyLeave.Equals("Y"))
            _result = CalPenalty(_scholar, _caseGraduate, _educationDate, _graduateDate, _civil, _totalPayScholarship, _scholarshipYear, _scholarshipMonth, _dateStart, _dateEnd, _indemnitorYear, _indemnitorCash, _calDateCondition, 0);
        else
        {
            _dateStart = _beforeStudyLeaveStartDate;
            _dateEnd = _beforeStudyLeaveEndDate;

            IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
            DateTime _dateA = DateTime.Parse(_afterStudyLeaveStartDate, _provider);
            DateTime _dateB = DateTime.Parse(_afterStudyLeaveEndDate, _provider);
            double[] _totalDaysAfterStudyLeave = Util.CalcDate(_dateA, _dateB);

            _result = CalPenalty(_scholar, _caseGraduate, _educationDate, _graduateDate, _civil, _totalPayScholarship, _scholarshipYear, _scholarshipMonth, _dateStart, _dateEnd, _indemnitorYear, _indemnitorCash, _calDateCondition, _totalDaysAfterStudyLeave[0]);
        }

        return _result;
    }

    public static double[] CalPenalty(string _scholar, string _caseGraduate, string _educationDate, string _graduateDate, string _civil, string _totalPayScholarship, string _scholarshipYear, string _scholarshipMonth, string _dateStart, string _dateEnd, string _indemnitorYear, string _indemnitorCash, string _calDateCondition, double _addDays)
    {
        double _actual;
        double _month;
        double _day;
        int _dayLastMonth;
        double _allActual;
        double _educationActual = 0;
        double _totalPenalty = 0;
        double[] _result = new double[15];
        int _sYear = !String.IsNullOrEmpty(_scholarshipYear) ? int.Parse(_scholarshipYear) : 0;
        int _sMonth = !String.IsNullOrEmpty(_scholarshipMonth) ? int.Parse(_scholarshipMonth) : 0;
        int _iYear = !String.IsNullOrEmpty(_indemnitorYear) ? int.Parse(_indemnitorYear) : 0;
        double _sPayScholarship = double.Parse(_totalPayScholarship);
        double _iCash = double.Parse(_indemnitorCash);
        int _formular = int.Parse(_calDateCondition);
        double[] _resultCalcDate;

        if (_scholar.Equals("1") && _caseGraduate.Equals("2") && _civil.Equals("1"))
        {
            _iCash = _sPayScholarship + _iCash;
            _sPayScholarship = 0;
        }

        if (!String.IsNullOrEmpty(_educationDate) && !String.IsNullOrEmpty(_graduateDate))
        {
            IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
            DateTime _dateA = DateTime.Parse(_educationDate, _provider);
            DateTime _dateB = DateTime.Parse(_graduateDate, _provider);

            _dateB = _dateB.AddDays(_addDays);
            _resultCalcDate = Util.CalcDate(_dateA, _dateB);
            _educationActual = !_resultCalcDate[0].Equals(0) ? _resultCalcDate[0] : 0;
        }

        if (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd))
        {
            IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
            DateTime _dateA = DateTime.Parse(_dateStart, _provider);
            DateTime _dateB = DateTime.Parse(_dateEnd, _provider);

            _dateB = _dateB.AddDays(_addDays);
            _allActual = ((_dateA.AddYears(_iYear + _sYear)).AddMonths(_sMonth) - _dateA).TotalDays;
            _resultCalcDate = Util.CalcDate(_dateA, _dateB);
            _actual = !_resultCalcDate[0].Equals(0) ? _resultCalcDate[0] : 0;
            _month = !_resultCalcDate[1].Equals(0) ? _resultCalcDate[1] : 0;
            _day = !_resultCalcDate[2].Equals(0) ? _resultCalcDate[2] : 0;
            _dayLastMonth = DateTime.DaysInMonth(_dateB.Year, _dateB.Month);

            switch (_formular)
            {
                case 1:
                    _totalPenalty = CalPenaltyFormular1(_iCash, _resultCalcDate[3]);
                    break;
                case 2:
                    _totalPenalty = CalPenaltyFormular2(_iCash, _resultCalcDate[1], _resultCalcDate[2], _dayLastMonth);
                    break;
                case 3:
                    if (_caseGraduate.Equals("2") && _civil.Equals("2"))
                    {
                        _resultCalcDate[0] = 0;
                        _actual = 0;
                        _month = 0;
                        _day = 0;
                        _totalPenalty = _iCash;
                    }
                    else
                        _totalPenalty = CalPenaltyFormular3(_iCash, _allActual, _resultCalcDate[0]);
                    
                    break;
                case 4:
                    _month = 0;
                    _day = 0;
                    _allActual = _educationActual;
                    _actual = (_civil.Equals("1") ? _actual : 0);
                    _totalPenalty = CalPenaltyFormular4(_iCash, _educationActual, _actual);
                    break;
            }

            _result[0] = _month;
            _result[1] = _day;
            _result[2] = _allActual;
            _result[3] = _actual;
            _result[4] = (_allActual - _resultCalcDate[0]);
            _result[5] = _sPayScholarship;
            _result[6] = _totalPenalty;
            _result[7] = Util.RoundStang(_sPayScholarship + _totalPenalty);
            _result[8] = _iCash;
            _result[9] = _resultCalcDate[3];
            _result[10] = _resultCalcDate[1];
            _result[11] = _resultCalcDate[2];
            _result[12] = _dayLastMonth;
            _result[13] = _resultCalcDate[0];
            _result[14] = _educationActual;

            return _result;
        }

        _result[0] = 0;
        _result[1] = 0;
        _result[2] = 0;
        _result[3] = 0;
        _result[4] = 0;
        _result[5] = _sPayScholarship;
        _result[6] = _iCash;
        _result[7] = Util.RoundStang(_sPayScholarship + _iCash);
        _result[8] = _iCash;
        _result[9] = 0;
        _result[10] = 0;
        _result[11] = 0;
        _result[12] = 0;
        _result[13] = 0;
        _result[14] = _educationActual;

        return _result;
    }

    private static double CalPenaltyFormular1(double _indemnitorCash, double _educationDate)
    {
        double _total;

        _total = _indemnitorCash * _educationDate;

        return _total;
    }

    public static string PenaltyFormular1ToString(double _indemnitorCash, double _educationDate)
    {
        string _result = String.Empty;

        _result = (_indemnitorCash.ToString("#,##0.00") + " X " + _educationDate.ToString("#,###0"));

        return _result;
    }

    private static double CalPenaltyFormular2(double _indemnitorCash, double _educationMonth, double _educationDay, int _dayLastMonth)
    {
        double _total;

        _total = (_indemnitorCash * _educationMonth) + ((_indemnitorCash * _educationDay) / _dayLastMonth);

        return _total;
    }

    public static string PenaltyFormular2ToString(double _indemnitorCash, double _educationMonth, double _educationDay, int _dayLastMonth)
    {
        string _result = String.Empty;

        _result = ("( " + _indemnitorCash.ToString("#,##0.00") + " X " + _educationMonth.ToString("#,##0") + " ) + ;( " + _indemnitorCash.ToString("#,##0.00") + " X " + _educationDay.ToString("#,##0") + " );" + _dayLastMonth.ToString("#,##0"));

        return _result;
    }


    private static double CalPenaltyFormular3(double _indemnitorCash, double _allActual, double _actual)
    {
        double _total;

        _total = (_indemnitorCash * (_allActual - _actual)) / _allActual;

        return _total;
    }

    public static string PenaltyFormular3ToString(double _indemnitorCash, double _allActual, double _actual)
    {
        string _result = String.Empty;

        _result = (_indemnitorCash.ToString("#,##0.00") + " X " + "( " + _allActual.ToString("#,##0") + " - " + _actual.ToString("#,##0") + " );" + _allActual.ToString("#,##0"));

        return _result;
    }

    private static double CalPenaltyFormular4(double _indemnitorCash, double _educationActual, double _actual)
    {
        double _total;

        _total = (_indemnitorCash * ((2 * _educationActual) - _actual)) / (2 * _educationActual);

        return _total;
    }

    public static string PenaltyFormular4ToString(double _indemnitorCash, double _educationActual, double _actual)
    {
        string _result = String.Empty;

        _result = (_indemnitorCash.ToString("#,##0.00") + " X " + "(( 2 X " + _educationActual.ToString("#,##0") + " ) - " + _actual.ToString("#,##0") + " );( 2 X " + _educationActual.ToString("#,##0") + " )");

        return _result;
    }

    public static string[] RepayDate(string _replyDate)
    {
        string[] _repayDate = new string[3];
        string _dow = String.Empty;
        double _ad = 0;

        _repayDate[0] = !String.IsNullOrEmpty(_replyDate) ? Util.ConvertDateTH((DateTime.Parse(_replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(1)).ToString()) : String.Empty;

        if (!String.IsNullOrEmpty(_repayDate[0]))
        {
            //_dow = DateTime.Parse(_repayDate[0], new System.Globalization.CultureInfo("th-TH")).AddDays(eCPUtil.PAYMENT_AT_LEAST).DayOfWeek.ToString();
            _dow = DateTime.Parse(_replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(eCPUtil.PAYMENT_AT_LEAST).DayOfWeek.ToString();
            /*
            if (_dow.Equals("Saturday"))
                _ad = 2;
            
            if (_dow.Equals("Sunday"))
            _ad = 1;
            */

            //_repayDate[1] = Util.ConvertDateTH((DateTime.Parse(_repayDate[0], new System.Globalization.CultureInfo("th-TH")).AddDays(eCPUtil.PAYMENT_AT_LEAST + _ad)).ToString());
            _repayDate[1] = Util.ConvertDateTH((DateTime.Parse(_replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(eCPUtil.PAYMENT_AT_LEAST + _ad)).ToString());
        }
        else
            _repayDate[1] = String.Empty;

        _repayDate[2] = !String.IsNullOrEmpty(_repayDate[1]) ? Util.ConvertDateTH((DateTime.Parse(_repayDate[1], new System.Globalization.CultureInfo("th-TH")).AddDays(1)).ToString()) : String.Empty;

        return _repayDate;
    }

    public static double CalInterestOverpayment(string _capital, string _overpaymentYear, string _overpaymentDay, string _overpaymentInterest, string _overpaymentDateEnd)
    {
        double _opCapital = double.Parse(_capital);
        double _opOverpaymentYear = double.Parse(_overpaymentYear);
        double _opOverpaymentDay = double.Parse(_overpaymentDay);
        double _opOverpaymentInterest = double.Parse(_overpaymentInterest);
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _opOverpaymentDateEnd = DateTime.Parse(_overpaymentDateEnd, _provider);
        double[] _dayLastYear = Util.CalcDate(DateTime.Parse(Util.ConvertDateTH("01/01/" + (_opOverpaymentDateEnd.Year).ToString()), _provider), DateTime.Parse(Util.ConvertDateTH("12/31/" + (_opOverpaymentDateEnd.Year).ToString()), _provider));
        double _interestOverpaymentYear = 0;
        double _interestOverpaymentDay = 0;
        double _totalInterestOverpayment = 0;

        if (!_opOverpaymentYear.Equals(0))
        {
            _interestOverpaymentYear = _opCapital * _opOverpaymentYear * (_opOverpaymentInterest / 100);
        }

        if (!_opOverpaymentDay.Equals(0))
        {
            _interestOverpaymentDay = _opCapital * (_opOverpaymentDay / _dayLastYear[0]) * (_opOverpaymentInterest / 100);
        }

        //_totalInterestOverpayment = Util.RoundStang(_interestOverpaymentYear + _interestOverpaymentDay);
        _totalInterestOverpayment = (_interestOverpaymentYear + _interestOverpaymentDay);

        return _totalInterestOverpayment;
    }

    public static string[] GetContractInterest()
    {
        string[,] _data;
        string[] _contractInterest = new string[2];
        int _recordCount;
        
        _data = eCPDB.ListSearchUseContractInterest();
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _contractInterest[0] = _data[0, 0];
            _contractInterest[1] = _data[0, 1];
        }

        return _contractInterest;
    }

    public static string[] CalChkBalance(string _capital, string _totalInterest, string _totalAccruedInterest, string _totalPayment, string _pay)
    {
        string[] _result = new string[5];
        
        _totalInterest = !String.IsNullOrEmpty(_totalInterest) ? _totalInterest : String.Empty;
        _totalAccruedInterest = !String.IsNullOrEmpty(_totalAccruedInterest) ? _totalAccruedInterest : String.Empty;
        _totalPayment = !String.IsNullOrEmpty(_totalPayment) ? _totalPayment : String.Empty;
        _pay = !String.IsNullOrEmpty(_pay) ? _pay : String.Empty;

        if (!String.IsNullOrEmpty(_totalPayment) && !String.IsNullOrEmpty(_pay))
        {
            double _capitalAll = double.Parse(_capital);
            double _totalInterestAll = double.Parse(_totalInterest);
            double _totalAccruedInterestAll = double.Parse(_totalAccruedInterest);
            double _totalPaymentAll = double.Parse(_totalPayment);
            double _payAll = double.Parse(_pay);
            double _payCapital = 0;
            double _payInterest = 0;
            double _payAccruedInterest = 0;
            double _accruedInterest = 0;
            double _remainPayAll = 0;
            double _remainCapital = 0;
            double _remainAccruedInterest = 0;
            double _totalRemain = 0;

            /*
            ก่อนปรับปรุง
            _payInterest = (_totalInterestAll <= _payAll) ? _totalInterestAll : _payAll; //ดอกเบี้ยรับชำระ
            _remainPayAll = _payAll - _payInterest; //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยรับชำระ
            _payCapital = (_capitalAll == _remainPayAll) ? _capitalAll : (_capitalAll > _remainPayAll ? _remainPayAll : _capitalAll); //เงินต้นรับชำระ
            _totalRemain = _payAll - (_payCapital + _payInterest); //เงินที่ต้องชำระคงเหลือ            
            _accruedInterest = _totalInterestAll - _payInterest; //ดอกเบี้ยค้างจ่ายงวดปัจจุบัน
            _remainCapital = _capitalAll - _payCapital; //เงินต้นคงเหลือ
            _remainAccruedInterest = (_totalRemain > 0) ? (_totalAccruedInterestAll - _totalRemain) + _accruedInterest : (_totalAccruedInterestAll + _accruedInterest); //ดอกเบี้ยจ่ายรวม
            */

            _payAccruedInterest = (_totalAccruedInterestAll <= _payAll) ? _totalAccruedInterestAll : _payAll; //ดอกเบี้ยรับชำระ
            _remainAccruedInterest = _totalAccruedInterestAll - _payAccruedInterest; //ดอกเบี้ยค้างจ่าย
            _remainPayAll = _payAll - _payAccruedInterest; //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยค้างจ่าย
            _payInterest = (_totalInterestAll <= _remainPayAll) ? _totalInterestAll : _remainPayAll; //ดอกเบี้ยรับชำระ
            _accruedInterest = _totalInterestAll - _payInterest; //ดอกเบี้ยค้างจ่ายงวดปัจจุบัน
            _remainPayAll = _remainPayAll - _payInterest; //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยรับชำระ
            _payCapital = (_capitalAll == _remainPayAll) ? _capitalAll : (_capitalAll > _remainPayAll ? _remainPayAll : _capitalAll); //เงินต้นรับชำระ
            _remainCapital = _capitalAll - _payCapital; //เงินต้นคงเหลือ
            _remainAccruedInterest = _remainAccruedInterest + _accruedInterest; //ดอกเบี้ยค้างจ่าย

            _result[0] = _payCapital.ToString();
            _result[1] = (_payAccruedInterest + _payInterest).ToString();            
            _result[2] = _remainCapital.ToString();
            _result[3] = _accruedInterest.ToString();
            _result[4] = _remainAccruedInterest < 0 ? "0" : _remainAccruedInterest.ToString();
        }
        else
        {
            _result[0] = String.Empty;
            _result[1] = String.Empty;
            _result[2] = String.Empty;
            _result[3] = String.Empty;
            _result[4] = String.Empty;
        }

        return _result;
    }

    public static string EncodeToBase64(string _str)
    {
        try
        {
            string _strEncode = String.Empty;
            byte[] _encDataByte = new byte[_str.Length];

            _encDataByte = Encoding.UTF8.GetBytes(_str);
            _strEncode = Convert.ToBase64String(_encDataByte);

            return _strEncode;
        }
        catch
        {
            return String.Empty;
        }
    }


    public static string DecodeFromBase64(string _strEncode)
    {
        string _strDecode = String.Empty;

        try
        {
            UTF8Encoding _encoder = new UTF8Encoding();
            Decoder _utf8Decode = _encoder.GetDecoder();
            byte[] _todecodeByte = Convert.FromBase64String(_strEncode);
            int _charCount = _utf8Decode.GetCharCount(_todecodeByte, 0, _todecodeByte.Length);
            char[] _decodedChar = new char[_charCount];

            _utf8Decode.GetChars(_todecodeByte, 0, _todecodeByte.Length, _decodedChar, 0);
            _strDecode = new String(_decodedChar);
        }
        catch
        {
        }

        return _strDecode;
    }

    public static Dictionary<string, string> GetUsername()
    {
        Dictionary<string, string> _result = new Dictionary<string, string>();
        string _username = String.Empty;
        string _password = String.Empty;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (_eCPCookie != null)
            return GetUsername(_eCPCookie["Authen"]);
        else
        {
            _result.Add("Username", _username);
            _result.Add("Password", _password);

            return _result;
        }
    }

    public static Dictionary<string, string> GetUsername(string _authen)
    {
        Dictionary<string, string> _result = new Dictionary<string, string>();
        string[] _auth = DecodeFromBase64(_authen).Split('.');
        string _username = String.Empty;
        string _password = String.Empty;

        try
        {
            if (_auth.Length.Equals(4))
            {
                _username = eCPUtil.DecodeFromBase64(new String(_auth[1].Reverse().ToArray()));
                _password = eCPUtil.DecodeFromBase64(new String(_auth[2].Reverse().ToArray()));
            }
        }
        catch
        {
        }

        _result.Add("Username", _username);
        _result.Add("Password", _password);

        return _result;
    }

    public static string GetUserID()
    {
        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        string _userid = String.Empty;

        try
        {
            if (!String.IsNullOrEmpty(_eCPCookie["Authen"]))
                _userid = eCPUtil.DecodeFromBase64(new String(eCPUtil.DecodeFromBase64(_eCPCookie["Authen"]).Reverse().ToArray()));
        }
        catch
        {
        }

        return _userid;
    }

    public static string GetFileExtension(string _contentType)
    {
        string _fileExtension = String.Empty;
        
        switch (_contentType)
        {
            case "image/gif": 
                _fileExtension = "gif";
                break;
            case "image/jpeg":
                _fileExtension = "jpg";
                break;
            case "image/png":
                _fileExtension = "png";
                break;
            case "application/msword":
                _fileExtension = "doc";
                break;
            case "application/pdf":
                _fileExtension = "pdf";
                break;
        }

        return _fileExtension;
    }

    public static int GetStartRow(string startRow)
    {
        return (!String.IsNullOrEmpty(startRow) ? int.Parse(startRow) : 1);
    }

    public static int GetEndRow(string endRow)
    {
        return (!String.IsNullOrEmpty(endRow) ? int.Parse(endRow) : ROW_PER_PAGE);
    }
}