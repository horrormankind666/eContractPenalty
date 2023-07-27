/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๐๕/๐๗/๒๕๖๖>
Description : <สำหรับรวบรวมฟังก์ชั่นการทำงานทั่วไป>
=============================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

public class eCPUtil {    
    public static string URL_STUDENT_PICTURE_2_STREAM = "https://smartedu.mahidol.ac.th/eProfile/Content/Handler/eProfileStaff/ePFStaffHandler.ashx?action=studentpicture2stream";
    public const int WIDTH_PICTURE_STUDENT = 119;
    public const int HEIGHT_PICTURE_STUDENT = 120;
    public const int ROW_PER_PAGE = 50;
    public const double PAYMENT_AT_LEAST = 30;
    public const double PAY_REPAY_LEAST = 500;
    public const int PERIOD_REPAY_LEAST = 40;
    public const string DIRECTOR = "นายอวยชัย อิสรวิริยะสกุล";
    public const string USERTYPE_STAFF = "STAFF";

    private static string[,] pageOrder = new string[,] {
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
            "CPReportDebtorContractRemain",
            "CPReportDebtorContractBreakRequireRepayPayment"
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
            "",
            ""
        },
        {
            "Home",
            "CPTabUser",
            "CPReportDebtorContract",
            "CPReportDebtorContractPaid",
            "CPReportDebtorContractRemain",
            "CPReportDebtorContractBreakRequireRepayPayment",
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

    public static int[,] activeMenu = new int[,] {
        { 1, 2, 3, 3, 3, 3, 3, 4, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 },
        { 1, 2, 3, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 2, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    public static string[,,] actionTrackingStatus = new string[,,] {
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

    public static string[,] iconTrackingStatus = new string[,] {
        { "1111", "status-send" },
        { "1112", "status-cancel" },
        { "2111", "status-receiver-disable" },
        { "2121", "status-edit" },
        { "2122", "status-cancel" },
        { "2211", "status-receiver" },
        { "2212", "status-cancel" }
    };

    public static string[,] iconRepayStatus = new string[,] {
        { "status-repay-wait-disable", "status-repay-wait", "", "" },
        { "status-repay-count1-disable", "status-repay-count1-wait", "status-repay-count1-yes", "status-repay-count1-no" },
        {"status-repay-count2-disable", "status-repay-count2-wait", "status-repay-count2-yes", "status-repay-count2-no" },
        {"status-repay-success-disable", "status-repay-success-wait", "status-repay-success", "" }
    };

    public static string[] iconPaymentStatus = new string[] {
        "status-payment-disable",
        "status-payment-wait",
        "status-payment-success"
    };

    public static string[,] iconEContractStatus = new string[,] {
        { "0", "status-e-contract-uncomplete" },
        { "1", "status-e-contract-complete" }
    };

    public static string[,] dlevel = new string[,] {
        { "ต่ำกว่าปริญญาตรี", "U" },
        { "ปริญญาตรี", "B" }
    };

    public static string[,] caseGraduate = new string[,] {
        { "ก่อนสำเร็จการศึกษา", "ไม่สำเร็จการศึกษา" },
        { "หลังสำเร็จการศึกษา", "สำเร็จการศึกษา" }
    };

    public static string[] civil = new string[] {
        "ปฏิบัติงานชดใช้",
        "ไม่ปฏิบัติงานชดใช้"
    };

    public static string[] scholar = new string[] {
        "ได้รับทุนการศึกษา",
        "ไม่ได้รับทุนการศึกษา"
    };
    
    public static string[] studyLeave = new string[] {
        "ไม่มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุน",
        "มีการลาศึกษา / ลาฝึกอบรม ระหว่างการปฏิบัติงานชดใช้ทุน"
    };
    
    public static string[,] trackingStatusORAA = new string[,] {
        { "รอส่งรายการแจ้ง", "1" },
        { "รอรับรายการแจ้ง", "2" },
        { "รับรายการแจ้งแล้ว", "3" },
        { "ส่งกลับแก้ไข", "4" },
        { "รายการแจ้งถูกยกเลิก", "5" },
        { "รอส่งรายการแจ้ง, ส่งกลับแก้ไข, รายการแจ้งถูกยกเลิก", "6" }
    };

    public static string[,] trackingStatusORLA = new string[,] {
        { "รอรับรายการแจ้ง", "2" },
        { "รับรายการแจ้งแล้ว", "3" },
        { "ส่งกลับแก้ไข", "4" },
        { "รายการแจ้งถูกยกเลิก", "5" }
    };

    public static string[] repayStatus = new string[] {
        "รอแจ้งชำระหนี้",
        "แจ้งชำระหนี้ครั้งที่ 1",
        "แจ้งชำระหนี้ครั้งที่ 2"
    };

    public static string[] repayStatusDetail = new string[] {
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

    public static string[] resultReply = new string[] {
        "ผู้ผิดสัญญารับทราบให้ชำระหนี้",
        "ผู้ผิดสัญญาไม่รับทราบให้ชำระหนี้"
    };

    public static string[] paymentStatus = new string[]  { 
        "ยังไม่ได้ชำระหนี้",
        "อยู่ระหว่างการชำระหนี้",
        "ชำระหนี้เรียบร้อย"
    };

    public static string[,] paymentRecordStatus = new string[,] {
        { "หยุดบันทึกข้อมูลการชำระหนี้ชั่วคราว", "P" },
        { "บันทึกข้อมูลการชำระหนี้ต่อเนื่อง", "C" }
    };

    public static string[] paymentFormat = new string[] {
        "ชำระแบบเต็มจำนวน",
        "ชำระแบบผ่อนชำระ"
    };

    public static string[] payChannel = new string[] {
        "ชำระด้วยเงินสด",
        "ชำระด้วยเช็คขีดคร่อม",
        "นำฝากบัญชีเงินฝาก"
    };

    public static string[,] calInterestYesNo = new string[,] {
        { "คิดดอกเบี้ย", "Y" },
        { "ไม่คิดตอกเบี้ย", "N" }
    };

    public static string[,] stepOfWorkStatus = new string[,] {
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

    public static string[,] conditionTableCalCapitalAndInterest = new string[,] {
        { "จำนวนเงินต้นที่ต้องการชำระต่อเดือน", "1" },
        { "จำนวนงวดที่ต้องการชำระ", "2" }
    };

    public static string[,] longMonth = new string[,] {
        { "มกราคม", "ม.ค." },
        { "กุมภาพันธ์", "ก.พ." },
        { "มีนาคม", "มี.ค." },
        { "เมษายน", "เม.ย." },
        { "พฤษภาคม", "พ.ค." },
        { "มิถุนายน", "มิ.ย." },
        { "กรกฎาคม", "ก.ค." },
        { "สิงหาคม", "ส.ค." },
        { "กันยายน", "ก.ย." },
        { "ตุลาคม", "ต.ค." },
        { "พฤศจิกายน", "พ.ย." },
        { "ธันวาคม", "ธ.ค." }
    };

    public static string CurrentDate(string format) {
        return (DateTime.Today.ToString(format));
    }

    public static string ThaiLongDate(string dateEN) {
        if (!string.IsNullOrEmpty(dateEN)) {
            DateTime dt = DateTime.Parse(dateEN);

            return ((int.Parse(dt.ToString("dd"))).ToString() + " " + longMonth[dt.Month - 1, 0] + " " + (dt.Year + 543).ToString());
        }
        else
            return dateEN;
    }

    public static string MenuBar(bool loginResult) {
        string html = string.Empty;

        html +=(
            "<div class='menu-bar'>" +
            "   <div class='content-left'>" +
            "       <ul>"
        );

        if (loginResult) {
            HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            html += (
                "       <li class='have-link first-link'>" +
                "           <a class='link-msg' id='menu1' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",1)'>&nbsp;หน้าแรก&nbsp;</a>" +
                "       </li>"
            );

            if (eCPCookie["UserLevel"].Equals("Administrator"))
                html += (
                    "   <li class='have-link'>" +
                    "       <a class='link-msg' id='menu2' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",2)'>บัญชีผู้ใช้</a>" +
                    "   </li>"
                );

            if (eCPCookie["UserSection"].Equals("1")) {
                if (eCPCookie["UserLevel"].Equals("Administrator")) {
                    html += (
                        "<li class='have-link' id='submenu31'>" +
                        "   <div class='link-msg' id='menu3'>ตั้งค่า</div>" +
                        "   <ul>" +
                        "       <li>" +
                        "           <a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",3)'>กำหนดหลักสูตรที่ให้มีการทำสัญญาการศึกษา</a>" +
                        "       </li>" +
                        "       <li>" +
                        "           <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",4)'>เงื่อนไขการคิดระยะเวลาตามสัญญาและสูตรคำนวณเงินชดใช้ตามสัญญา</a>" +
                        "       </li>" +
                        "       <li>" +
                        "           <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",5)'>กำหนดดอกเบี้ยจากการผิดนัดชำระ</a>" +
                        "       </li>" +
                        "       <li>" +
                        "           <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",6)'>เกณฑ์การชดใช้ตามสัญญา</a>" +
                        "       </li>" +
                        "       <li>" +
                        "           <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",7)'>กำหนดทุนการศึกษาแต่ละหลักสูตร</a>" +
                        "       </li>" +
                        "   </ul>" +
                        "</li>"
                    );
                }

                html += (
                    "   <li class='have-link'>" +
                    "       <a class='link-msg' id='menu4' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",8)'>รับแจ้งผู้ผิดสัญญา / แจ้งชำระหนี้</a>" +
                    "   </li>" +
                    "   <li class='have-link'>" +
                    "       <a class='link-msg' id='menu5' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",9)'>ชำระหนี้ / ดอกเบี้ย</a>" +
                    "   </li>" +
                    "   <li class='have-link' id='submenu61'>" +
                    "       <div class='link-msg' id='menu6'>รายงาน</div>" +
                    "       <ul>" +
                    "           <li>" +
                    "               <a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",16)'>เอกสารสัญญาการเป็นนักศึกษา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",10)'>สถานะขั้นตอนการดำเนินงานของผู้ผิดสัญญา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",11)'>ตารางคำนวณเงินต้นและดอกเบี้ย</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",12)'>สถิติการผิดสัญญาและการชำระหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",15)'>สถิติการชำระหนี้ตามช่วงวันที่</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",14)'>สถิติการทำสัญญาและการผิดสัญญา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",13)'>หนังสือทวงถามผู้ผิดสัญญาและผู้ค้ำประกัน</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",17)'>ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",18)'>การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",19)'>ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",20)'>ลูกหนี้ผิดสัญญาคงค้าง ( กรณี Z600 ลูกหนี้นักศึกษา )</a>" +
                    "           </li>" +
                    "       </ul>" +
                    "   </li>"
                );
            }

            if (eCPCookie["UserSection"].Equals("2")) {
                html += (
                    "   <li class='have-link'>" +
                    "       <a class='link-msg' id='menu3' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",3)'>แจ้งผู้ผิดสัญญา</a>" +
                    "   </li>" +
                    "   <li class='have-link' id='submenu41'>" +
                    "       <div class='link-msg' id='menu4'>รายงาน</div>" +
                    "       <ul>" +
                    "           <li>" +
                    "               <a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ", 9)'>เอกสารสัญญาการเป็นนักศึกษา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",4)'>สถานะขั้นตอนการดำเนินงานของผู้ผิดสัญญา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",5)'>สถิติการผิดสัญญาและการชำระหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",8)'>สถิติการชำระหนี้ตามช่วงวันที่</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",7)'>สถิติการทำสัญญาและการผิดสัญญา</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",6)'>หนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณา</a>" +
                    "           </li>" +
                    "       </ul>" +
                    "   </li>"
                );
            }

            if (eCPCookie["UserSection"].Equals("3")) {
                html += (
                    "   <li class='have-link' id='submenu21'><div class='link-msg' id='menu2'>รายงาน</div>" +
                    "       <ul>" +
                    "           <li>" +
                    "               <a class='item-submenu-first' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",3)'>ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",4)'>การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",5)'>ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้</a>" +
                    "           </li>" +
                    "           <li>" +
                    "               <a class='item-submenu' href='javascript:void(0)' onclick='GoToPage(" + eCPCookie["UserSection"] + ",6)'>ลูกหนี้ผิดสัญญาคงค้าง ( กรณี Z600 ลูกหนี้นักศึกษา )</a>" +
                    "           </li>" +
                    "       </ul>" +
                    "   </li>"
                );
            }
        }

        html += (
            "       </ul>" +
            "   </div>" +
            "   <div class='content-right'>" +
            "       <ul>" +
            /*
            "           <li class='have-link first-link'>" +
            "               <a class='link-img' id='mu-icon1' href='http://www.mahidol.ac.th' target='_blank'></a>" +
            "           </li>" +
            "           <li class='have-link'>" +
            "               <a class='link-img' id='mu-icon2' href='http://webmail.mahidol.ac.th/' target='_blank'></a>" +
            "           </li>" +
            */
            "           <li>" +
            "               <div id='current-date'>&nbsp;&nbsp;" + Util.ShortDateTH(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd"))) + "&nbsp;&nbsp;</div>" +
            "           </li>"
        );

        if (loginResult) {
            HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string userid = GetUserID();
            string[,] data = eCPDB.ListDetailCPTabUser(userid, "", "", "");

            html += (
                "       <li>" +
                "           <div id='whois'>ผู้ใช้งาน : " + data[0, 3] + " ( " + eCPDB.userSection[int.Parse(eCPCookie["UserSection"]) - 1] + " )&nbsp;</div>" +
                "       </li>" +
                "       <li class='have-link'>" +
                "           <a class='link-msg' id='menu7' href='javascript:void(0)' onclick='ShowManual()'>คู่มือ</a>" +
                "       </li>" +
                "       <li class='have-link'>" +
                "           <a class='link-msg' id='menu8' href='javascript:void(0)' onclick='Signout()'>ออกจากระบบ</a>" +
                "       </li>"
            );
        }

        html += (
            "       </ul>" +
            "   </div>" +            
            "   <div class='clear'></div>" +
            "</div>"
        );

        return html;
    }

    public static string Head() {
        string html = string.Empty;
        string title = "orla";
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (eCPCookie != null) {
            if (eCPCookie["UserSection"].Equals("1"))
                title = "orla";

            if (eCPCookie["UserSection"].Equals("2"))
                title = "oraa";

            if (eCPCookie["UserSection"].Equals("3"))
                title = "orfa";
        }

        html += (
            "<div class='head-title-main'>" +
            "   <div class='head-title-" + title + "'>" +
            "       <div class='head-title-" + title + "-bg'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
        
    public static string Signin() {
        string html = string.Empty;

        html += (
            "<div class='signin-space'></div>" +
            "<div class='signin-layout'>" +
            "   <div class='frm-signin'>" +
            "       <form id='frm-signin' action='' onsubmit='return false' enctype='multipart/form-data'>" +
            "           <div id='colusername'>" +
            "               <input class='inputbox' type='text' id='username' onblur=Trim('username') value='' style='width:170px' />" +
            "           </div>" +
            "           <div id='colpassword'>" +
            "               <input class='inputbox' type='password' id='password' value='' onblur=Trim('password') style='width:170px' />" +
            "           </div>" +
            "       </form>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='Signin()'>เข้าสู่ระบบ</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrm()'>ล้าง</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div id='manual'>" +
            "           <a href='javascript:void(0)' onclick='ShowManual()'>คู่มือ</a>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string Manual() {
        string html = string.Empty;

        html += (
            "<div class='slide' id='slide-manual'>" +
            "   <div class='slide-tools' id='slide-tools-manual'>" +
            "       <div class='slide-tool-content' id='slide-tool-content-manual'></div>" +
            "       <div class='slide-tool' id='slide-tool-manual'></div>" +
            "   </div>" +
            "   <div class='slide-contents' id='slide-contents-manual'>" +
            "       <img src='#' />" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public string GenPage(int pid) {
        int order = 0;

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (eCPCookie["UserSection"].Equals("1"))
            order = 0;

        if (eCPCookie["UserSection"].Equals("2"))
            order = 1;

        if (eCPCookie["UserSection"].Equals("3"))
            order = 2;

        eCPCookie.Values.Remove("Pid");
        eCPCookie.Values.Add("Pid", (pid + 1).ToString());
        HttpContext.Current.Response.AppendCookie(eCPCookie);

        Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(pageOrder[order, pid]);
        string result = (string)theMethod.Invoke(this, null);
      
        return result;
    }
    
    public string Home() {
        string html = string.Empty;

        html += (
            "<div class='home'></div>"
        );

        return html;
    }

    public static string ContentTitle(string content) {
        string html = string.Empty;

        html += (
            "<div class='content-data-title'>" +
            "   <div class='content-data-title-" + content + "'></div>" +
            "</div>"
        );

        return html;
    }

    public string CPTabUser() {
        string html = string.Empty;

        html += (
            eCPDataUser.TabCPTabUser()
        );

        return html;
    }

    public string CPTabProgram() {
        string html = string.Empty;

        html += (
            eCPDataConfiguration.TabCPTabProgram()
        );

        return html;
    }

    public string CPTabCalDate() {
        string html = string.Empty;

        html += (
            eCPDataConfiguration.ListCPTabCalDate()
        );

        return html;
    }

    public string CPTabInterest() {
        string html = string.Empty;

        html += (
            eCPDataConfiguration.TabCPTabInterest()
        );

        return html;
    }

    public string CPTabPayBreakContract() {
        string html = string.Empty;

        html += (
            eCPDataConfiguration.TabCPTabPayBreakContract()
        );
        
        return html;
    }

    public string CPTabScholarship() {
        string html = string.Empty;

        html += (
            eCPDataConfiguration.TabCPTabScholarship()
        );

        return html;
    }

    public string CPTransBreakContract() {
        string html = string.Empty;

        html += (
            eCPDataBreakContract.TabCPTransBreakContract()
        );

        return html;
    }

    public string CPTransRequireContract() {
        string html = string.Empty;

        html += (
            eCPDataRequireContract.TabCPTransRequireContract()
        );

        return html;
    }

    public string CPTransPayment() {
        string html = string.Empty;

        html += (
            eCPDataPayment.TabPaymentOnCPTransRequireContract()
        );

        return html;
    }

    public string CPReportStepOfWork() {
        string html = string.Empty;

        html += (
            eCPDataReportStepOfWork.ListCPReportStepOfWork()
        );

        return html;
    }

    public string CPReportTableCalCapitalAndInterest() {
        string html = string.Empty;

        html += (
            eCPDataReportTableCalCapitalAndInterest.TabCPReportTableCalCapitalAndInterest()
        );

        return html;
    }

    public string CPReportStatisticRepay() {
        string html = string.Empty;

        html += (
            eCPDataReportStatisticRepay.TabCPReportStatisticRepay()
        );

        return html;
    }

    public string CPReportStatisticContract() {
        string html = string.Empty;

        html += (
            eCPDataReportStatisticContract.TabCPReportStatisticContract()
        );

        return html;
    }
    
    public string CPReportNoticeRepayComplete() {
        string html = string.Empty;

        html += (
            eCPDataReportNoticeRepayComplete.ListCPReportNoticeRepayComplete()
        );

        return html;
    }

    public string CPReportNoticeClaimDebt() {
        string html = string.Empty;

        html += (
            eCPDataReportNoticeClaimDebt.ListCPReportNoticeClaimDebt()
        );

        return html; ;
    }

    public string CPReportStatisticPaymentByDate() {
        string html = string.Empty;

        html += (
            eCPDataReportStatisticPaymentByDate.ListCPReportStatisticPaymentByDate()
        );

        return html;
    }

    public string CPReportEContract() {
        string html = string.Empty;

        html += (
            eCPDataReportEContract.ListCPReportEContract()
        );

        return html;
    }
    
    public string CPReportDebtorContract() {
        string html = string.Empty;

        html += (
            eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontract")
        );

        return html;
    }

    public string CPReportDebtorContractPaid() {
        string html = string.Empty;

        html += (
            eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontractpaid")
        );

        return html;
    }

    public string CPReportDebtorContractRemain() {
        string html = string.Empty;

        html += (
            eCPDataReportDebtorContract.TabCPReportDebtorContract("reportdebtorcontractremain")
        );

        return html;
    }

    public string CPReportDebtorContractBreakRequireRepayPayment() {
        string html = string.Empty;

        html += (
            eCPDataPayment.TabPaymentOnCPTransRequireContract()
        );

        return html;
    }

    public static string ListUser(string id) {
        string html = string.Empty;

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        html += (
        "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกผู้ใช้งาน</option>"
        );

        DataSet ds = eCPDB.ListCPTabUser();

        foreach (DataRow dr in ds.Tables[1].Rows) {
            html += (
                "   <option value='" + (dr["Name"].ToString() + ";" + dr["PhoneNumber"].ToString() + ";" + dr["MobileNumber"].ToString() + ";" + dr["Email"].ToString()) + "'>" + dr["Name"].ToString() + "</option>"
            );
        }

        ds.Dispose();

        html += (
            "   </select>" +
            "</div>"
        );

        return html;
    }

    public static string ListTitleName(string id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListTitlename();
        int recordCount = data.GetLength(0);

        html += (
            "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกคำนำหน้าชื่อ</option>"
        );

        for (int i = 0; i < recordCount; i++) {
            html += (
                "   <option value='" + (data[i, 0] + ";" + data[i, 1] + ";" + data[i, 2]) + "'>" + data[i, 2] + "</option>"
            );
        }

        html += (
            "   </select>" +
            "</div>"
        );

        return html.ToString();
    }

    public static string ListFaculty(
        bool cpTabProgram,
        string id
    ) {
        string html = string.Empty;
        string[,] data = eCPDB.ListFaculty(cpTabProgram);
        int recordCount = data.GetLength(0);

        html += (
            "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกคณะ</option>"
        );

        for (int i = 0; i < recordCount; i++) {
            html += (
                "   <option value='" + (data[i, 0] + ";" + data[i, 1]) + "'>" + (data[i, 0] + " - " + data[i, 1]) + "</option>"
            );
        }

        html += (
            "   </select>" +
            "</div>"
        );

        return html.ToString();
    }

    public static string ListProgram(
        bool cpTabProgram,
        string id,
        string dlevel,
        string faculty
    ) {
        string html = string.Empty;

        html += (
            "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกหลักสูตร</option>"
        );

        if (!faculty.Equals("0")) {
            string[,] data = eCPDB.ListProgram(cpTabProgram, dlevel, faculty);
            int recordCount = data.GetLength(0);
            string groupNum;

            for (int i = 0; i < recordCount; i++) {
                groupNum = (!data[i, 2].Equals("0") ? (" ( กลุ่ม " + data[i, 2] + " )") : "");

                html += (
                    "<option value='" + (data[i, 0] + ";" + data[i, 3] + ";" + data[i, 1] + ";" + data[i, 2] + ";" + data[i, 4] + ";" + data[i, 5]) + "'>" + (data[i, 0] + " - " + data[i, 3] + groupNum) + "</option>"
                );
            }
        }

        html += (
            "   </select>" +
            "</div>"
        );

        return ("<list>" + html + "<list>");
    }

    public static string ListCalDate(string id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabCalDate("");
        int recordCount = data.GetLength(0);

        html += (
            "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกวิธีคิดและคำนวณเงินชดใช้</option>"
        );
                    
        if (recordCount > 0) {
            for (int i = 0; i < recordCount; i++) {
                html += (
                    "<option value='" + data[i, 0] + "'>วิธีที่ " + data[i, 0] + "</option>"
                );
            }
        }
    
        html += (
            "   </select>" +
            "</div>"
        );

        return ("<list>" + html + "<list>");
    }

    public static string ListProvince(string id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListProvince();
        int recordCount = data.GetLength(0);

        html += (
            "<div class='combobox'>" +
            "   <select id='" + id + "'>" +
            "       <option value='0'>เลือกจังหวัด</option>"
        );
                    
        if (recordCount > 0) {
            for (int i = 0; i < recordCount; i++) {
                html += (
                    "<option value='" + data[i, 0] + "'>" + data[i, 1] + "</option>"
                );
            }
        }
    
        html += (
            "   </select>" +
            "</div>"
        );

        return ("<list>" + html + "<list>");
    }

    public static double[] CalPayScholarship(
        string scholar,
        string caseGraduate,
        string civil,
        string scholarshipMoney,
        string scholarshipYear,
        string scholarshipMonth,
        string allActualMonthScholarship
    ) {
        double[] result = new double[2];

        if (scholar.Equals("1")) {
            double sMoney = double.Parse(scholarshipMoney);
            double sYear = double.Parse(scholarshipYear);
            double sMonth = double.Parse(scholarshipMonth);

            sYear += (sMonth / 12);

            switch (caseGraduate) {
                case "1":
                    double aMonth = double.Parse(allActualMonthScholarship);        

                    result[0] = (sMoney / 12);
                    result[1] = (aMonth * result[0]);
                    break;
                case "2":
                    result[0] = 0;                            
                    /*
                    result[1] = (civil.Equals("1") ? (sMoney * sYear) : ((sMoney * sYear) * 2));
                    */
                    result[1] = (civil.Equals("1") ? sMoney : ((sMoney * sYear) * 2));
                    break;
            }
        }
        else {
            result[0] = 0;
            result[1] = 0;
        }

        return result;
    }

    public static double[] GetCalPenalty(
        string studyLeave,
        string beforeStudyLeaveStartDate,
        string beforeStudyLeaveEndDate,
        string afterStudyLeaveStartDate,
        string afterStudyLeaveEndDate,
        string scholar,
        string caseGraduate,
        string educationDate,
        string graduateDate,
        string civil,
        string totalPayScholarship,
        string scholarshipYear,
        string scholarshipMonth,
        string dateStart,
        string dateEnd,
        string indemnitorYear,
        string indemnitorCash,
        string calDateCondition
    ) {
        double[] result;

        if (!studyLeave.Equals("Y"))
            result = CalPenalty(scholar, caseGraduate, educationDate, graduateDate, civil, totalPayScholarship, scholarshipYear, scholarshipMonth, dateStart, dateEnd, indemnitorYear, indemnitorCash, calDateCondition, 0);
        else {
            dateStart = beforeStudyLeaveStartDate;
            dateEnd = beforeStudyLeaveEndDate;

            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            DateTime dateA = DateTime.Parse(afterStudyLeaveStartDate, provider);
            DateTime dateB = DateTime.Parse(afterStudyLeaveEndDate, provider);
            double[] totalDaysAfterStudyLeave = Util.CalcDate(dateA, dateB);

            result = CalPenalty(scholar, caseGraduate, educationDate, graduateDate, civil, totalPayScholarship, scholarshipYear, scholarshipMonth, dateStart, dateEnd, indemnitorYear, indemnitorCash, calDateCondition, totalDaysAfterStudyLeave[0]);
        }

        return result;
    }

    public static double[] CalPenalty(
        string scholar,
        string caseGraduate,
        string educationDate,
        string graduateDate,
        string civil,
        string totalPayScholarship,
        string scholarshipYear,
        string scholarshipMonth,
        string dateStart,
        string dateEnd,
        string indemnitorYear,
        string indemnitorCash,
        string calDateCondition,
        double addDays
    ) {
        double actual;
        double month;
        double day;
        int dayLastMonth;
        double allActual;
        double educationActual = 0;
        double totalPenalty = 0;
        double[] result = new double[15];
        int sYear = (!string.IsNullOrEmpty(scholarshipYear) ? int.Parse(scholarshipYear) : 0);
        int sMonth = (!string.IsNullOrEmpty(scholarshipMonth) ? int.Parse(scholarshipMonth) : 0);
        int iYear = (!string.IsNullOrEmpty(indemnitorYear) ? int.Parse(indemnitorYear) : 0);
        double sPayScholarship = double.Parse(totalPayScholarship);
        double iCash = double.Parse(indemnitorCash);
        int formular = int.Parse(calDateCondition);
        double[] resultCalcDate;

        if (scholar.Equals("1") &&
            caseGraduate.Equals("2") &&
            civil.Equals("1")) {
            iCash = (sPayScholarship + iCash);
            sPayScholarship = 0;
        }

        if (!string.IsNullOrEmpty(educationDate) &&
            !string.IsNullOrEmpty(graduateDate)) {
            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            DateTime dateA = DateTime.Parse(educationDate, provider);
            DateTime dateB = DateTime.Parse(graduateDate, provider);

            dateB = dateB.AddDays(addDays);
            resultCalcDate = Util.CalcDate(dateA, dateB);
            educationActual = (!resultCalcDate[0].Equals(0) ? resultCalcDate[0] : 0);
        }

        if (!string.IsNullOrEmpty(dateStart) &&
            !string.IsNullOrEmpty(dateEnd)) {
            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            DateTime dateA = DateTime.Parse(dateStart, provider);
            DateTime dateB = DateTime.Parse(dateEnd, provider);

            dateB = dateB.AddDays(addDays);
            allActual = ((dateA.AddYears(iYear + sYear)).AddMonths(sMonth) - dateA).TotalDays;
            resultCalcDate = Util.CalcDate(dateA, dateB);
            actual = (!resultCalcDate[0].Equals(0) ? resultCalcDate[0] : 0);
            month = (!resultCalcDate[1].Equals(0) ? resultCalcDate[1] : 0);
            day = (!resultCalcDate[2].Equals(0) ? resultCalcDate[2] : 0);
            dayLastMonth = DateTime.DaysInMonth(dateB.Year, dateB.Month);

            switch (formular) {
                case 1:
                    totalPenalty = CalPenaltyFormular1(iCash, resultCalcDate[3]);
                    break;
                case 2:
                    totalPenalty = CalPenaltyFormular2(iCash, resultCalcDate[1], resultCalcDate[2], dayLastMonth);
                    break;
                case 3:
                    if (caseGraduate.Equals("2") &&
                        civil.Equals("2")) {
                        resultCalcDate[0] = 0;
                        actual = 0;
                        month = 0;
                        day = 0;
                        totalPenalty = iCash;
                    }
                    else
                        totalPenalty = CalPenaltyFormular3(iCash, allActual, resultCalcDate[0]);
                    
                    break;
                case 4:
                    month = 0;
                    day = 0;
                    allActual = educationActual;
                    actual = (civil.Equals("1") ? actual : 0);
                    totalPenalty = CalPenaltyFormular4(iCash, educationActual, actual);
                    break;
            }

            result[0] = month;
            result[1] = day;
            result[2] = allActual;
            result[3] = actual;
            result[4] = (allActual - resultCalcDate[0]);
            result[5] = sPayScholarship;
            result[6] = totalPenalty;
            result[7] = Util.RoundStang(sPayScholarship + totalPenalty);
            result[8] = iCash;
            result[9] = resultCalcDate[3];
            result[10] = resultCalcDate[1];
            result[11] = resultCalcDate[2];
            result[12] = dayLastMonth;
            result[13] = resultCalcDate[0];
            result[14] = educationActual;

            return result;
        }

        result[0] = 0;
        result[1] = 0;
        result[2] = 0;
        result[3] = 0;
        result[4] = 0;
        result[5] = sPayScholarship;
        result[6] = iCash;
        result[7] = Util.RoundStang(sPayScholarship + iCash);
        result[8] = iCash;
        result[9] = 0;
        result[10] = 0;
        result[11] = 0;
        result[12] = 0;
        result[13] = 0;
        result[14] = educationActual;

        return result;
    }

    private static double CalPenaltyFormular1(
        double indemnitorCash,
        double educationDate
    ) {
        double total = (indemnitorCash * educationDate);

        return total;
    }

    public static string PenaltyFormular1ToString(
        double indemnitorCash,
        double educationDate
    ) {
        string result = (indemnitorCash.ToString("#,##0.00") + " X " + educationDate.ToString("#,###0"));

        return result;
    }

    private static double CalPenaltyFormular2(
        double indemnitorCash,
        double educationMonth,
        double educationDay,
        int dayLastMonth
    ) {
        double total = ((indemnitorCash * educationMonth) + ((indemnitorCash * educationDay) / dayLastMonth));

        return total;
    }

    public static string PenaltyFormular2ToString(
        double indemnitorCash,
        double educationMonth,
        double educationDay,
        int dayLastMonth
    ) {
        string result = ("( " + indemnitorCash.ToString("#,##0.00") + " X " + educationMonth.ToString("#,##0") + " ) + ;( " + indemnitorCash.ToString("#,##0.00") + " X " + educationDay.ToString("#,##0") + " );" + dayLastMonth.ToString("#,##0"));

        return result;
    }


    private static double CalPenaltyFormular3(
        double indemnitorCash,
        double allActual,
        double actual
    ) {
        double total = ((indemnitorCash * (allActual - actual)) / allActual);

        return total;
    }

    public static string PenaltyFormular3ToString(
        double indemnitorCash,
        double allActual,
        double actual
    ) {
        string result = (indemnitorCash.ToString("#,##0.00") + " X " + "( " + allActual.ToString("#,##0") + " - " + actual.ToString("#,##0") + " );" + allActual.ToString("#,##0"));

        return result;
    }

    private static double CalPenaltyFormular4(
        double indemnitorCash,
        double educationActual,
        double actual
    ) {
        double total = ((indemnitorCash * ((2 * educationActual) - actual)) / (2 * educationActual));

        return total;
    }

    public static string PenaltyFormular4ToString(
        double indemnitorCash,
        double educationActual,
        double actual
    ) {
        string result = (indemnitorCash.ToString("#,##0.00") + " X " + "(( 2 X " + educationActual.ToString("#,##0") + " ) - " + actual.ToString("#,##0") + " );( 2 X " + educationActual.ToString("#,##0") + " )");

        return result;
    }

    public static string[] RepayDate(string replyDate) {
        string[] repayDate = new string[3];
        double ad = 0;

        repayDate[0] = (!string.IsNullOrEmpty(replyDate) ? Util.ConvertDateTH((DateTime.Parse(replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(1)).ToString()) : string.Empty);

        if (!string.IsNullOrEmpty(repayDate[0])) {
            /*
            string dow = DateTime.Parse(repayDate[0], new System.Globalization.CultureInfo("th-TH")).AddDays(PAYMENT_AT_LEAST).DayOfWeek.ToString();
            */
            string dow = DateTime.Parse(replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(PAYMENT_AT_LEAST).DayOfWeek.ToString();
            /*
            if (dow.Equals("Saturday"))
                ad = 2;
            
            if (dow.Equals("Sunday"))
                ad = 1;

            repayDate[1] = Util.ConvertDateTH((DateTime.Parse(repayDate[0], new System.Globalization.CultureInfo("th-TH")).AddDays(PAYMENT_AT_LEAST + _ad)).ToString());
            */
            repayDate[1] = Util.ConvertDateTH((DateTime.Parse(replyDate, new System.Globalization.CultureInfo("th-TH")).AddDays(PAYMENT_AT_LEAST + ad)).ToString());
        }
        else
            repayDate[1] = string.Empty;

        repayDate[2] = (!string.IsNullOrEmpty(repayDate[1]) ? Util.ConvertDateTH((DateTime.Parse(repayDate[1], new System.Globalization.CultureInfo("th-TH")).AddDays(1)).ToString()) : string.Empty);

        return repayDate;
    }

    public static double CalInterestOverpayment(
        string capital,
        string overpaymentYear,
        string overpaymentDay,
        string overpaymentInterest,
        string overpaymentDateEnd
    ) {
        double opCapital = double.Parse(capital);
        double opOverpaymentYear = double.Parse(overpaymentYear);
        double opOverpaymentDay = double.Parse(overpaymentDay);
        double opOverpaymentInterest = double.Parse(overpaymentInterest);
        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime opOverpaymentDateEnd = DateTime.Parse(overpaymentDateEnd, provider);
        double[] dayLastYear = Util.CalcDate(DateTime.Parse(Util.ConvertDateTH("01/01/" + (opOverpaymentDateEnd.Year).ToString()), provider), DateTime.Parse(Util.ConvertDateTH("12/31/" + (opOverpaymentDateEnd.Year).ToString()), provider));
        double interestOverpaymentYear = 0;
        double interestOverpaymentDay = 0;

        if (!opOverpaymentYear.Equals(0)) {
            interestOverpaymentYear = (opCapital * opOverpaymentYear * (opOverpaymentInterest / 100));
        }

        if (!opOverpaymentDay.Equals(0)) {
            interestOverpaymentDay = (opCapital * (opOverpaymentDay / dayLastYear[0]) * (opOverpaymentInterest / 100));
        }

        /*
        double totalInterestOverpayment = Util.RoundStang(interestOverpaymentYear + interestOverpaymentDay);
        */
        double totalInterestOverpayment = (interestOverpaymentYear + interestOverpaymentDay);

        return totalInterestOverpayment;
    }

    public static string[] GetContractInterest() {
        string[] contractInterest = new string[2];
        string[,] data = eCPDB.ListSearchUseContractInterest();
        int recordCount = data.GetLength(0);

        if (recordCount > 0) {
            contractInterest[0] = data[0, 0];
            contractInterest[1] = data[0, 1];
        }

        return contractInterest;
    }

    public static string DoubleToString2Decimal(double d) {
        return d.ToString("#.00");
    }

    public static string[] CalChkBalance(
        string capital,
        string totalInterest,
        string totalAccruedInterest,
        string totalPayment,
        string pay
    ) {
        string[] result = new string[5];
        
        totalInterest = (!string.IsNullOrEmpty(totalInterest) ? totalInterest : string.Empty);
        totalAccruedInterest = (!string.IsNullOrEmpty(totalAccruedInterest) ? totalAccruedInterest : string.Empty);
        totalPayment = (!string.IsNullOrEmpty(totalPayment) ? totalPayment : string.Empty);
        pay = (!string.IsNullOrEmpty(pay) ? pay : string.Empty);

        if (!string.IsNullOrEmpty(totalPayment) &&
            !string.IsNullOrEmpty(pay)) {
            double capitalAll = double.Parse(capital);
            double totalInterestAll = double.Parse(totalInterest);
            double totalAccruedInterestAll = double.Parse(totalAccruedInterest);
            double totalPaymentAll = double.Parse(totalPayment);
            double payAll = double.Parse(pay);
            double payCapital;
            double payInterest;
            double payAccruedInterest;
            double accruedInterest;
            double remainPayAll;
            double remainCapital;
            double remainAccruedInterest;
            /*
            double totalRemain;
            */

            /*
            ก่อนปรับปรุง
            payInterest = (totalInterestAll <= payAll ? totalInterestAll : payAll); //ดอกเบี้ยรับชำระ
            remainPayAll = (payAll - payInterest); //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยรับชำระ
            payCapital = (capitalAll == remainPayAll ? capitalAll : (capitalAll > remainPayAll ? remainPayAll : capitalAll)); //เงินต้นรับชำระ
            totalRemain = (payAll - (payCapital + payInterest)); //เงินที่ต้องชำระคงเหลือ            
            accruedInterest = (totalInterestAll - payInterest); //ดอกเบี้ยค้างจ่ายงวดปัจจุบัน
            remainCapital = (capitalAll - payCapital); //เงินต้นคงเหลือ
            remainAccruedInterest = (totalRemain > 0 ? ((totalAccruedInterestAll - totalRemain) + accruedInterest) : (totalAccruedInterestAll + accruedInterest)); //ดอกเบี้ยจ่ายรวม
            */
            /*
            payAccruedInterest = (totalAccruedInterestAll <= _payAll ? totalAccruedInterestAll : payAll); //ดอกเบี้ยรับชำระ
            remainAccruedInterest = (totalAccruedInterestAll - payAccruedInterest); //ดอกเบี้ยค้างจ่าย
            remainPayAll = (payAll - payAccruedInterest); //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยค้างจ่าย
            payInterest = (totalInterestAll <= remainPayAll ? totalInterestAll : remainPayAll); //ดอกเบี้ยรับชำระ
            accruedInterest = (totalInterestAll - payInterest); //ดอกเบี้ยค้างจ่ายงวดปัจจุบัน
            remainPayAll = (remainPayAll - payInterest); //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยรับชำระ
            payCapital = (capitalAll == remainPayAll ? capitalAll : (capitalAll > remainPayAll ? remainPayAll : capitalAll)); //เงินต้นรับชำระ
            remainCapital = (capitalAll - payCapital); //เงินต้นคงเหลือ
            remainAccruedInterest = (remainAccruedInterest + accruedInterest); //ดอกเบี้ยค้างจ่าย
            */

            payAccruedInterest = (double.Parse(DoubleToString2Decimal(totalAccruedInterestAll)) <= double.Parse(DoubleToString2Decimal(payAll)) ? double.Parse(DoubleToString2Decimal(totalAccruedInterestAll)) : double.Parse(DoubleToString2Decimal(payAll))); //ดอกเบี้ยรับชำระ
            remainAccruedInterest = (double.Parse(DoubleToString2Decimal(totalAccruedInterestAll)) - double.Parse(DoubleToString2Decimal(payAccruedInterest))); //ดอกเบี้ยค้างจ่าย
            remainPayAll = (double.Parse(DoubleToString2Decimal(payAll)) - double.Parse(DoubleToString2Decimal(payAccruedInterest))); //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยค้างจ่าย
            payInterest = (double.Parse(DoubleToString2Decimal(totalInterestAll)) <= double.Parse(DoubleToString2Decimal(remainPayAll)) ? double.Parse(DoubleToString2Decimal(totalInterestAll)) : double.Parse(DoubleToString2Decimal(remainPayAll))); //ดอกเบี้ยรับชำระ
            accruedInterest = (double.Parse(DoubleToString2Decimal(totalInterestAll)) - double.Parse(DoubleToString2Decimal(payInterest))); //ดอกเบี้ยค้างจ่ายงวดปัจจุบัน
            remainPayAll = (double.Parse(DoubleToString2Decimal(remainPayAll)) - double.Parse(DoubleToString2Decimal(payInterest))); //เงินที่ต้องชำระเหลือจากหักดอกเบี้ยรับชำระ            
            payCapital = (double.Parse(DoubleToString2Decimal(capitalAll)) == double.Parse(DoubleToString2Decimal(remainPayAll)) ? double.Parse(DoubleToString2Decimal(capitalAll)) : (double.Parse(DoubleToString2Decimal(capitalAll)) > double.Parse(DoubleToString2Decimal(remainPayAll)) ? double.Parse(DoubleToString2Decimal(remainPayAll)) : double.Parse(DoubleToString2Decimal(capitalAll)))); //เงินต้นรับชำระ            
            remainCapital = (double.Parse(DoubleToString2Decimal(capitalAll)) - double.Parse(DoubleToString2Decimal(payCapital))); //เงินต้นคงเหลือ
            remainAccruedInterest = (double.Parse(DoubleToString2Decimal(remainAccruedInterest)) + double.Parse(DoubleToString2Decimal(accruedInterest))); //ดอกเบี้ยค้างจ่าย

            result[0] = payCapital.ToString();
            result[1] = (double.Parse(DoubleToString2Decimal(payAccruedInterest)) + double.Parse(DoubleToString2Decimal(payInterest))).ToString();
            result[2] = remainCapital.ToString();
            result[3] = accruedInterest.ToString();
            result[4] = (double.Parse(DoubleToString2Decimal(remainAccruedInterest)) < 0 ? "0" : remainAccruedInterest.ToString());
        }
        else {
            result[0] = string.Empty;
            result[1] = string.Empty;
            result[2] = string.Empty;
            result[3] = string.Empty;
            result[4] = string.Empty;
        }

        return result;
    }

    public static string EncodeToBase64(string str) {
        try {            
            byte[] encDataByte = Encoding.UTF8.GetBytes(str);
            string strEncode = Convert.ToBase64String(encDataByte);

            return strEncode;
        }
        catch {
            return string.Empty;
        }
    }


    public static string DecodeFromBase64(string strEncode) {
        string strDecode = string.Empty;

        try {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(strEncode);
            int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodedChar = new char[charCount];

            utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
            strDecode = new string(decodedChar);
        }
        catch {
        }

        return strDecode;
    }

    public static Dictionary<string, string> GetUsername() {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (eCPCookie != null)
            return GetUsername(eCPCookie["Authen"]);
        else {
            Dictionary<string, string> result = new Dictionary<string, string>() {
                { "Username", string.Empty },
                { "Password", string.Empty }
            };

            return result;
        }
    }

    public static Dictionary<string, string> GetUsername(string authen) {
        string[] auth = DecodeFromBase64(authen).Split('.');
        string username = string.Empty;
        string password = string.Empty;

        try {
            if (auth.Length.Equals(4)) {
                username = DecodeFromBase64(new string(auth[1].Reverse().ToArray()));
                password = DecodeFromBase64(new string(auth[2].Reverse().ToArray()));
            }
        }
        catch {
        }

        Dictionary<string, string> result = new Dictionary<string, string>() {
            { "Username", username },
            { "Password", password }
        };

        return result;
    }

    public static string GetUserID() {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        string userid = string.Empty;

        try {
            if (!string.IsNullOrEmpty(eCPCookie["Authen"]))
                userid = DecodeFromBase64(new string(DecodeFromBase64(eCPCookie["Authen"]).Reverse().ToArray()));
        }
        catch {
        }

        return userid;
    }

    public static string GetFileExtension(string contentType) {
        string fileExtension = string.Empty;
        
        switch (contentType) {
            case "image/gif": 
                fileExtension = "gif";
                break;
            case "image/jpeg":
                fileExtension = "jpg";
                break;
            case "image/png":
                fileExtension = "png";
                break;
            case "application/msword":
                fileExtension = "doc";
                break;
            case "application/pdf":
                fileExtension = "pdf";
                break;
        }

        return fileExtension;
    }

    public static int GetStartRow(string startRow) {
        return (!string.IsNullOrEmpty(startRow) ? int.Parse(startRow) : 1);
    }

    public static int GetEndRow(string endRow) {
        return (!string.IsNullOrEmpty(endRow) ? int.Parse(endRow) : ROW_PER_PAGE);
    }
}