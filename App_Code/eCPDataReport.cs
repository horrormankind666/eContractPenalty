/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๙/๐๘/๒๕๕๕>
Modify date : <๑๓/๐๓/๒๕๖๗>
Description : <สำหรับการแสดงรายงาน>
=============================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NExport2PDF;
using OfficeOpenXml;
using System.Web.UI;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

public class eCPDataReport {
    public static string ReplaceFacultyToShortProgram(string faculty) {
        faculty = faculty.Replace("คณะ", "");

        return faculty;
    }

    public static string ReplaceProgramToShortProgram(string program) {
        program = program.Replace("ประกาศนียบัตร", "");
        program = program.Replace("บัณฑิต", "");
        program = program.Replace("ศาสตร", "ศาสตร์");

        return program;
    }
}

public class eCPDataReportStatisticRepay {
    public static string ListReportStepOfWorkOnStatisticRepayByProgram(HttpContext c) {
        string html = string.Empty;        
        string pageHtml = string.Empty;                
        int recordCount = eCPDB.CountReportStepOfWorkOnStatisticRepayByProgram(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportStepOfWorkOnStatisticRepayByProgram(c);
            string highlight;
            string groupNum;
            string trackingStatus;
            string iconStatus1;
            string[] iconStatus2;
            string iconStatus3;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                trackingStatus = (data[i, 10] + data[i, 11] + data[i, 12] + data[i, 13]);
                iconStatus1 = eCPUtil.iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil.iconTrackingStatus, trackingStatus) - 1, 1];
                iconStatus2 = data[i, 14].Split(new char[] { ';' });
                iconStatus3 = eCPUtil.iconPaymentStatus[(!string.IsNullOrEmpty(data[i, 15]) ? int.Parse(data[i, 15]) - 1 : 0)];
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");                
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','" + data[i, 16] + "')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-report-step-of-work-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col4' onclick=" + callFunc + ">" +
                    "       <div class='icon-status1-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus1 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col5' onclick=" + callFunc + ">" +
                    "       <div class='icon-status2-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus2[1] + "'></li>" +
                    "               <li class='" + iconStatus2[2] + "'></li>" +
                    "               <li class='" + iconStatus2[3] + "'></li>" +
                    "               <li class='" + iconStatus2[4] + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col6' onclick=" + callFunc + ">" +
                    "       <div class='icon-status3-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus3 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportstepofworkonstatisticrepaybyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListReportStepOfWorkOnStatisticRepayByProgram() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='list-report-step-of-work-on-statistic-repay-by-program'>" +
            "   <div id='detail-statistic-repay-by-program'>" +
            "       <div class='content-left' id='detail-statistic-repay-by-program-label'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>ปีการศึกษา</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>คณะ</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>หลักสูตร</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='detail-statistic-repay-by-program-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-repay-by-program-acadamicyear'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-repay-by-program-faculty'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-repay-by-program-program'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div id='search-step-of-work'>" +
            "       <div class='form-label-discription-style'>" +
            "           <div id='search-step-of-work-keyword1-label'>" +
            "               <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                   <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='form-input-style'>" +
            "           <div class='form-input-content' id='search-step-of-work-keyword1-input'>" +
            "               <input class='inputbox' type='text' id='id-name-search-report-step-of-work' onblur=Trim('id-name-search-report-step-of-work'); value='' style='width:411px' />" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchReportStepOfWorkOnStatisticRepayByProgram()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmSearchReportStepOfWorkOnStatisticRepayByProgram()'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "   <div id='list-report-step-of-work'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'></div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-report-step-of-work'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-report-step-of-work-col1'>" +
            "                       <div class='table-head-line1'>ลำดับที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div><div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col4'>" +
            "                       <div class='table-head-line1'>สถานะรายการแจ้ง</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailtrackingstatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col5'>" +
            "                       <div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailrepaystatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col6'>" +
            "                       <div class='table-head-line1'>สถานะการชำระหนี้</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailpaymentstatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "       <div>" +
            "           <div id='box-list-data-search-report-step-of-work'>" +
            "               <div id='list-data-search-report-step-of-work'></div>" +
            "           </div>" +
            "           <div id='nav-page-search-report-step-of-work'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string ListCPReportStatisticRepayByProgram(string acadamicyear) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPReportStatisticRepayByProgram(acadamicyear);
        int recordCount = data.GetLength(0);
        
        if (recordCount > 0) {
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < recordCount; i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 7].Equals("0") ? (" ( กลุ่ม " + data[i, 7] + " )") : "");
                callFunc = ("ViewReportStepOfWorkOnStatisticRepayByProgram('" + data[i, 0] + "','" + data[i, 2] + "','" + data[i, 3].Replace(" ", "&") + "','" + data[i, 4] + "','" + data[i, 5].Replace(" ", "&") + "','" + data[i, 6] + "','" + data[i, 7] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-statistic-repay-by-program" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-report-statistic-repay-by-program-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col2' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 4] + "</span>- " + (data[i, 5] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col3' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 8]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 9]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col6' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 11]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col7' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 12]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col8' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 13]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col9' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 14]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav><pagenav>"
        );
    }

    public static string ListCPReportStatisticRepay(string[,] data) {
        string html = string.Empty;
        int recordCount = data.GetLength(0);

        if (recordCount > 0) {
            string highlight;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < recordCount; i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                callFunc = ("ViewReportStatisticRepayByProgram('" + data[i, 1] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-statistic-repay" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-statistic-repay-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col2' onclick=" + callFunc + ">" +
                    "       <div>25" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col3' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 2]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 3]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 4]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col6' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 5]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col7' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 6]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col8' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 7]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col9' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 8]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-repay-col10' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 9]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );
        }

        return html;
    }

    public static string ListUpdateCPReportStatisticRepay() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPReportStatisticRepay();
        int recordCount = data.GetLength(0);

        html += (
            ListCPReportStatisticRepay(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPReportStatisticRepay() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-statistic-repay-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-statistic-repay") +
            "       <div class='content-data-tabs' id='tabs-cp-report-statistic-repay'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-report-statistic-repay' alt='#tab1-cp-report-statistic-repay' href='javascript:void(0)'>สถิติการผิดสัญญาและการชำระหนี้</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab2-cp-report-statistic-repay' alt='#tab2-cp-report-statistic-repay' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-report-statistic-repay-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'></div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-repay'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-report-statistic-repay-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='acadamicyear-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-report-step-of-work-on-statistic-repay-by-program-hidden' value=''>" +
            "                   <input type='hidden' id='program-report-step-of-work-on-statistic-repay-by-program-hidden' value=''>" +
            "               </div>" +
            "               <div class='content-right'>" + 
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-repay-by-program'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-report-statistic-repay-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-statistic-repay-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col2'>" +
            "                       <div class='table-head-line1'>ปี</div>" +
            "                       <div>การศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col3'>" +
            "                       <div class='table-head-line1'>จำนวน</div><div>หลักสูตรที่มี</div>" +
            "                       <div>ผู้ผิดสัญญา</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col4'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col5'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่ยัง</div>" +
            "                       <div>ไม่ชำระหนี้</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col6'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่</div>" +
            "                       <div>ชำระหนี้ครบ</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col7'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่ยัง</div>" +
            "                       <div>ชำระหนี้ไม่ครบ</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col8'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col9'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-col10'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>คงเหลือ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-statistic-repay-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-statistic-repay-by-program-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col2'>" +
            "                       <div class='table-head-line1'>หลักสูตรที่มีผู้ผิดสัญญา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col3'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col4'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่ยัง</div>" +
            "                       <div>ไม่ชำระหนี้</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col5'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่</div>" +
            "                       <div>ชำระหนี้ครบ</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col6'>" +
            "                       <div class='table-head-line1'>จำนวน</div>" +
            "                       <div>ผู้ผิดสัญญาที่ยัง</div>" +
            "                       <div>ชำระหนี้ไม่ครบ</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col7'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col8'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col9'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>คงเหลือ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-statistic-repay-content'>" +
            "   <div class='tab-content' id='tab1-cp-report-statistic-repay-content'>" +
            "       <div class='box4' id='list-data-report-statistic-repay'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-statistic-repay-content'>" +
            "       <div class='box4' id='list-data-report-statistic-repay-by-program'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportStatisticContract {
    private static string ListReportStudentSignContract(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountReportStudentSignContract(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportStudentSignContract(c);
            string highlight;
            string callFunc = string.Empty;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                /*
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','" + data[i, 16] + "')");
                */
                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-student-sign-contract" + data[i, 0] + "'>" +
                    "   <li id='table-content-report-student-sign-contract-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-student-sign-contract-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-student-sign-contract-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 2] + data[i, 3] + " " + data[i, 4]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-student-sign-contract-col4' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 5] + data[i, 6] + " " + data[i, 7]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-student-sign-contract-col5' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 8] + " " + data[i, 9]) + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);
            
            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportstudentonstatisticcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    private static string ListReportStudentContractPenalty(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountReportStepOfWorkOnStatisticRepayByProgram(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportStepOfWorkOnStatisticRepayByProgram(c);
            string highlight;
            string callFunc;
            string trackingStatus;
            string iconStatus1;
            string[] iconStatus2;
            string iconStatus3;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                trackingStatus = (data[i, 10] + data[i, 11] + data[i, 12] + data[i, 13]);
                iconStatus1 = eCPUtil.iconTrackingStatus[(Util.FindIndexArray2D(0, eCPUtil.iconTrackingStatus, trackingStatus) - 1), 1];
                iconStatus2 = data[i, 14].Split(new char[] { ';' });
                iconStatus3 = eCPUtil.iconPaymentStatus[(!string.IsNullOrEmpty(data[i, 15]) ? (int.Parse(data[i, 15]) - 1) : 0)];                
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','" + data[i, 16] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-report-step-of-work-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col4' onclick=" + callFunc + ">" +
                    "       <div class='icon-status1-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus1 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col5' onclick=" + callFunc + ">" +
                    "       <div class='icon-status2-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus2[1] + "'></li>" +
                    "               <li class='" + iconStatus2[2] + "'></li>" +
                    "               <li class='" + iconStatus2[3] + "'></li>" +
                    "               <li class='" + iconStatus2[4] + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-report-step-of-work-col6' onclick=" + callFunc + ">" +
                    "       <div class='icon-status3-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus3 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportstudentonstatisticcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListReportStudentOnStatisticContractByProgram(HttpContext c) {
        string result = string.Empty;
        int searchTab = int.Parse(c.Request["searchtab"]);

        if (searchTab.Equals(1))
            result = ListReportStudentSignContract(c);

        if (searchTab.Equals(2))
            result = ListReportStudentContractPenalty(c);

        return result;
    }

    public static string ListReportStudentOnStatisticContractByProgram() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='list-report-student-on-statistic-contract-by-program-head'>" +
            "   <div id='detail-statistic-contract-by-program'>" +
            "       <div class='content-left' id='detail-statistic-contract-by-program-label'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>ปีการศึกษา</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>คณะ</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>หลักสูตร</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='detail-statistic-contract-by-program-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-contract-by-program-acadamicyear'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-contract-by-program-faculty'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <span id='statistic-contract-by-program-program'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div id='search-student-contract'>" +
            "       <div class='form-label-discription-style'>" +
            "           <div id='search-student-contract-keyword1-label'>" +
            "               <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                   <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='form-input-style'>" +
            "           <div class='form-input-content' id='search-student-contract-keyword1-input'>" +
            "               <input class='inputbox' type='text' id='id-name-search-report-student-contract' onblur=Trim('id-name-search-report-student-contract'); value='' style='width:411px' />" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchReportStudentOnStatisticContractByProgram()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmSearchReportStudentOnStatisticContractByProgram()'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-head'>" +
            "       <div class='content-data-tabs content-data-subtabs' id='tabs-report-student-on-statistic-contract-by-program'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li class='first-tab'>" +
            "                       <a class='active' id='link-tab1-report-student-on-statistic-contract-by-program' alt='#tab1-report-student-on-statistic-contract-by-program' href='javascript:void(0)'>นักศึกษาที่ทำสัญญาเสร็จสมบูรณ์ผ่านระบบ e-Contract</a>" +
            "                   </li>" +
            "                   <li id='tab2-report-student-on-statistic-contract-by-program'>" +
            "                       <a id='link-tab2-report-student-on-statistic-contract-by-program' alt='#tab2-report-student-on-statistic-contract-by-program' href='javascript:void(0)'>นักศึกษาที่ผิดสัญญา</a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div>" +
            "       <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'></div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-student-sign-contract'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'></div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-student-contract-penalty'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-report-student-sign-contract-col1'>" +
            "                       <div class='table-head-line1'>ลำดับที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-student-sign-contract-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div><div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-student-sign-contract-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-student-sign-contract-col4'>" +
            "                       <div class='table-head-line1'>ผู้ค้ำประกัน</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-student-sign-contract-col5'>" +
            "                       <div class='table-head-line1'>วันที่ทำสัญญา</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-report-step-of-work-col1'>" +
            "                       <div class='table-head-line1'>ลำดับที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div><div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col4'>" +
            "                       <div class='table-head-line1'>สถานะรายการแจ้ง</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailtrackingstatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col5'>" +
            "                       <div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailrepaystatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-report-step-of-work-col6'>" +
            "                       <div class='table-head-line1'>สถานะการชำระหนี้</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(2,'detailpaymentstatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<div id='list-report-student-on-statistic-contract-by-program-content'>" +
            "   <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-content'>" +
            "       <div class='box4' id='box-list-data-student-sign-contract'>" +
            "           <div id='list-data-student-sign-contract'></div>" +
            "       </div>" +
            "       <div id='nav-page-student-sign-contract'></div>" +
            "   </div>" +
            "   <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-content'>" +
            "       <div class='box4' id='box-list-data-student-contract-penalty'>" +
            "           <div id='list-data-student-contract-penalty'></div>" +
            "       </div>" +
            "       <div id='nav-page-student-contract-penalty'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string ListCPReportStatisticContractByProgram(string acadamicyear) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPReportStatisticContractByProgram(acadamicyear);
        int recordCount = data.GetLength(0);

        if (recordCount > 0) {
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < recordCount; i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 7].Equals("0") ? (" ( กลุ่ม " + data[i, 7] + " )") : "");
                callFunc = ("ViewReportStudentOnStatisticContractContractByProgram('" + data[i, 0] + "','" + data[i, 2] + "','" + data[i, 3].Replace(" ", "&") + "','" + data[i, 4] + "','" + data[i, 5].Replace(" ", "&") + "','" + data[i, 6] + "','" + data[i, 7] + "')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-statistic-contract-by-program" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-report-statistic-contract-by-program-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col2' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 4] + "</span>- " + (data[i, 5] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col3' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 8]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 9]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav><pagenav>"
        );
    }

    public static string ListCPReportStatisticContract(string[,] data) {
        string html = string.Empty;
        int recordCount = data.GetLength(0);

        if (recordCount > 0) {
            string highlight;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < recordCount; i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                callFunc = ("ViewReportStatisticContractByProgram('" + data[i, 1] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-statistic-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-statistic-contract-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-col2' onclick=" + callFunc + ">" +
                    "       <div>25" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-col3' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 2]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 3]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-contract-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 4]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );
        }

        return html;
    }

    public static string ListUpdateCPReportStatisticContract() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPReportStatisticContract();
        int recordCount = data.GetLength(0);

        html += (
            ListCPReportStatisticContract(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPReportStatisticContract() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-statistic-contract-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-statistic-contract") +
            "       <div class='content-data-tabs' id='tabs-cp-report-statistic-contract'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-report-statistic-contract' alt='#tab1-cp-report-statistic-contract' href='javascript:void(0)'>สถิติการทำสัญญาและการผิดสัญญา</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab2-cp-report-statistic-contract' alt='#tab2-cp-report-statistic-contract' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-report-statistic-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'></div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-contract'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-report-statistic-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='acadamicyear-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-report-student-on-statistic-contract-by-program-hidden' value=''>" +
            "                   <input type='hidden' id='program-report-student-on-statistic-contract-by-program-hidden' value=''>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-contract-by-program'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-report-statistic-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-statistic-contract-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-col2'>" +
            "                       <div class='table-head-line1'>ปีการศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-col3'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ต้องทำสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-col4'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ทำสัญญาเสร็จสมบูรณ์</div>" +
            "                       <div>ผ่านระบบ e-Contract</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-col5'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ผิดสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-statistic-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-statistic-contract-by-program-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col2'>" +
            "                       <div class='table-head-line1'>หลักสูตรที่ให้มีการทำสัญญา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col3'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ต้องทำสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col4'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ทำสัญญาเสร็จสมบูรณ์</div>" +
            "                       <div>ผ่านระบบ e-Contract</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col5'>" +
            "                       <div class='table-head-line1'>จำนวนนักศึกษา</div>" +
            "                       <div>ที่ผิดสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-statistic-contract-content'>" +
            "   <div class='tab-content' id='tab1-cp-report-statistic-contract-content'>" +
            "       <div class='box4'>" +
            "           <div id='list-data-report-statistic-contract'></div>" +
            "   </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-statistic-contract-content'>" +
            "       <div class='box4' id='list-data-report-statistic-contract-by-program'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportStepOfWork {
    public static string ListCPReportStepOfWork(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;       
        int recordCount = eCPDB.CountCPReportStepOfWork(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPReportStepOfWork(c);
            string highlight;
            string trackingStatus;
            string iconStatus1;
            string[] iconStatus2;
            string iconStatus3;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                trackingStatus = (data[i, 10] + data[i, 11] + data[i, 12] + data[i, 13]);
                iconStatus1 = eCPUtil.iconTrackingStatus[(Util.FindIndexArray2D(0, eCPUtil.iconTrackingStatus, trackingStatus) - 1), 1];
                iconStatus2 = data[i, 14].Split(new char[] { ';' });
                iconStatus3 = eCPUtil.iconPaymentStatus[(!string.IsNullOrEmpty(data[i, 15]) ? (int.Parse(data[i, 15]) - 1) : 0)];
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','" + data[i, 16] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-step-of-work-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + (data[i, 8] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col5' onclick=" + callFunc + ">" +
                    "       <div class='icon-status1-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus1 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col6' onclick=" + callFunc + ">" +
                    "       <div class='icon-status2-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus2[1] + "'></li>" +
                    "               <li class='" + iconStatus2[2] + "'></li>" +
                    "               <li class='" + iconStatus2[3] + "'></li>" +
                    "               <li class='" + iconStatus2[4] + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-step-of-work-col7' onclick=" + callFunc + ">" +
                    "       <div class='icon-status3-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus3 + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);
            
            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportstepofwork", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportStepOfWork() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-step-of-work-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-step-of-work") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'>" +
            "               <input type='hidden' id='search-report-step-of-work' value=''>" +
            "               <input type='hidden' id='stepofworkstatus-report-step-of-work-hidden' value=''>" +
            "               <input type='hidden' id='stepofworkstatus-report-step-of-work-text-hidden' value=''>" +
            "               <input type='hidden' id='id-name-report-step-of-work-hidden' value=''>" +
            "               <input type='hidden' id='faculty-report-step-of-work-hidden' value=''>" +
            "               <input type='hidden' id='program-report-step-of-work-hidden' value=''>" +
            "               <div class='button-style2'>" +
            "                   <ul>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportstepofwork',true,'','','')>ค้นหา</a>" +
            "                       </li>" +
            "                   </ul>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-cp-report-step-of-work'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box-search-condition' id='search-report-step-of-work-condition'>" +
            "           <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "           <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order1'>" +
            "               <div class='box-search-condition-order-title'>สถานะขั้นตอนการดำเนินงาน</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order1-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order2'>" +
            "               <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order2-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order3'>" +
            "               <div class='box-search-condition-order-title'>คณะ</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order3-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order4'>" +
            "               <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order4-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-report-step-of-work-col1'>" +
            "                   <div class='table-head-line1'>ลำดับ</div>" +
            "                   <div>ที่</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col2'>" +
            "                   <div class='table-head-line1'>รหัส</div>" +
            "                   <div>นักศึกษา</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col3'>" +
            "                   <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col4'>" +
            "                   <div class='table-head-line1'>หลักสูตร</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col5'>" +
            "                   <div class='table-head-line1'>สถานะรายการแจ้ง</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailtrackingstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col6'>" +
            "                   <div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-step-of-work-col7'>" +
            "                   <div class='table-head-line1'>สถานะการชำระหนี้</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailpaymentstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-step-of-work-content'>" +
            "   <div class='box4' id='list-data-report-step-of-work'></div>" +
            "   <div id='nav-page-report-step-of-work'></div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportTableCalCapitalAndInterest {
    public static void ExportCPReportTableCalCapitalAndInterest(string exportSend) {
        char[] separator = new char[] { '&', '.' };
        string[] send = exportSend.Split(separator[0]);
        string cp2id = send[0];
        string[] capital = new string[2];
        string[] interest = new string[2];
        string[] pay = new string[2];
        string paymentDate = send[4];
        
        capital[0] = send[1];        
        interest[0] = send[2];        
        pay[0] = send[3];
        
        string[] capitalSplitDec = capital[0].Split(separator[1]);
        string[] interestSplitDec = interest[0].Split(separator[1]);
        string[] paySplitDec = pay[0].Split(separator[1]);

        capital[1] = (double.Parse(capitalSplitDec[0]).ToString("#,##0") + (capitalSplitDec[1].Equals("00") ? ".-" : ("." + ((capitalSplitDec[1].Substring(capitalSplitDec[1].Length - 1, 1)).Equals("0") ? capitalSplitDec[1].Substring(0, 1) : capitalSplitDec[1]))));
        interest[1] = (double.Parse(interestSplitDec[0]).ToString("#,##0") + (interestSplitDec[1].Equals("00") ? "" : ("." + ((interestSplitDec[1].Substring(interestSplitDec[1].Length - 1, 1)).Equals("0") ? interestSplitDec[1].Substring(0, 1) : interestSplitDec[1]))));
        pay[1] = (double.Parse(paySplitDec[0]).ToString("#,##0") + (paySplitDec[1].Equals("00") ? ".-" : ("." + (paySplitDec[1].Substring(paySplitDec[1].Length - 1, 1).Equals("0") ? paySplitDec[1].Substring(0, 1) : paySplitDec[1]))));

        string[,] data = eCPDB.ListDetailCPReportTableCalCapitalAndInterest(cp2id);
        string studentIDDefault = data[0, 3];
        string titleNameDefault = data[0, 4];
        string firstNameDefault = data[0, 5];
        string lastNameDefault = data[0, 6];
        string programNameDefault = data[0, 8];
        string groupNumDefault = data[0, 12];
        int startRow = 1, maxRow = 28, row = 1;

        string[,] data1 = eCPDB.ListCalCPReportTableCalCapitalAndInterest(capital[0], interest[0], pay[0], paymentDate);
        int[] page = PageNavigate.CalPage(data1.GetLength(0) - 1, 1, maxRow);

        string pdfFont = "Font/THSarabun.ttf";
        string template = "ExportTemplate/Blank.pdf";
        string saveFile = ("TableCalCapitalAndInterest" + studentIDDefault + ".pdf");

        ExportToPdf exportToPdf = new ExportToPdf();
        exportToPdf.ExportToPdfConnect(template, "pdf", saveFile);

        float topCell = 0;
        float topText = 0;
        float borderBottom;

        for (int i = 1; i <= page[1]; i++) {
            if (i > 1)
                exportToPdf.PDFNewPage();
            
            exportToPdf.FillForm(pdfFont, 14, 1, "ตารางคำนวณเงินต้นและดอกเบี้ย", 50, 810, 500, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, ("ผู้ชดใช้เงินกรณีผิดสัญญาการศึกษา" + programNameDefault + (!groupNumDefault.Equals("0") ? (" ( กลุ่ม " + groupNumDefault + " )") : "")), 50, 792, 500, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, (titleNameDefault + firstNameDefault + " " + lastNameDefault), 50, 774, 500, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, ("จากเงินต้น " + capital[1] + " บาท อัตราดอกเบี้ยร้อยละ " + interest[1] + " ต่อปี"), 50, 756, 500, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, ("ชำระเงินต้นเดือนละ " + pay[1] + " บาททุกวันที่ " + double.Parse(paymentDate.Substring(0, 2)).ToString("#0") + " ของเดือน"), 50, 738, 500, 0);

            exportToPdf.CreateTable(50, 702, 50, 42, 1, 1, 1, 1);
            exportToPdf.CreateTable(100, 702, 80, 42, 0, 1, 1, 1);
            exportToPdf.CreateTable(180, 702, 80, 42, 0, 1, 1, 1);
            exportToPdf.CreateTable(260, 702, 80, 42, 0, 1, 1, 1);
            exportToPdf.CreateTable(340, 702, 80, 42, 0, 1, 1, 1);
            exportToPdf.CreateTable(420, 702, 65, 42, 0, 1, 1, 1);
            exportToPdf.CreateTable(485, 702, 65, 42, 0, 1, 1, 1);

            exportToPdf.FillForm(pdfFont, 14, 1, "งวดที่ชำระ", 50, 702, 50, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "เงินต้น", 100, 702, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "คงเหลือ", 100, 684, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "รับชำระ", 180, 702, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "เงินต้น", 180, 684, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "จำนวนเงิน", 260, 702, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "ดอกเบี้ยรับ", 260, 684, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "รวมจำนวนเงิน", 340, 702, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "รับชำระ", 340, 684, 80, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "กำหนดชำระ", 420, 702, 65, 0);
            exportToPdf.FillForm(pdfFont, 14, 1, "วันที่รับชำระ", 485, 702, 65, 0);

            topCell = 660;
            topText = 662;
            int j;

            for (j = startRow; (j <= (data1.GetLength(0) - 1) && row <= maxRow); j++, row++) {
                borderBottom = (j.Equals(data1.GetLength(0) - 1) || row.Equals(maxRow) ? 1 : 0);

                exportToPdf.CreateTable(50, topCell, 50, 21, 1, 0, 1, borderBottom);
                exportToPdf.CreateTable(100, topCell, 80, 21, 0, 0, 1, borderBottom);
                exportToPdf.CreateTable(180, topCell, 80, 21, 0, 0, 1, borderBottom);
                exportToPdf.CreateTable(260, topCell, 80, 21, 0, 0, 1, borderBottom);
                exportToPdf.CreateTable(340, topCell, 80, 21, 0, 0, 1, borderBottom);
                exportToPdf.CreateTable(420, topCell, 65, 21, 0, 0, 1, borderBottom);
                exportToPdf.CreateTable(485, topCell, 65, 21, 0, 0, 1, borderBottom);

                exportToPdf.FillForm(pdfFont, 14, 1, double.Parse(data1[(j - 1), 0]).ToString("#,##0"), 50, topText, 50, 0);
                exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(j - 1), 1]).ToString("#,##0.00"), 100, topText, 75, 0);
                exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(j - 1), 2]).ToString("#,##0.00"), 180, topText, 75, 0);
                exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(j - 1), 3]).ToString("#,##0.00"), 260, topText, 75, 0);
                exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(j - 1), 4]).ToString("#,##0.00"), 340, topText, 75, 0);
                exportToPdf.FillForm(pdfFont, 14, 1, Util.ShortDateTH(data1[(j - 1), 5]), 420, topText, 65, 0);

                topCell -= 21;
                topText -= 21;
            }

            startRow = j;
            row = 1;

            exportToPdf.FillForm(pdfFont, 11, 0, ("วันที่ " + Util.ThaiLongDate(Util.CurrentDate("yyyy-MM-dd"))), 50, 30, 250, 0);
            exportToPdf.FillForm(pdfFont, 11, 2, ("หน้า " + i), 350, 30, 200, 0);
        }

        exportToPdf.CreateTable(179, topCell, 81, 21, 1, 0, 1, 1);
        exportToPdf.CreateTable(260, topCell, 80, 21, 0, 0, 1, 1);
        exportToPdf.CreateTable(340, topCell, 80, 21, 0, 0, 1, 1);

        exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(data1.GetLength(0) - 1), 6]).ToString("#,##0.00"), 179, topText, 76, 0);
        exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(data1.GetLength(0) - 1), 7]).ToString("#,##0.00"), 260, topText, 75, 0);
        exportToPdf.FillForm(pdfFont, 14, 2, double.Parse(data1[(data1.GetLength(0) - 1), 8]).ToString("#,##0.00"), 340, topText, 75, 0);

        exportToPdf.ExportToPdfDisconnect();
    }
    
    public static string ListTableCalCapitalAndInterest(string[,] data) {
        string html = string.Empty;
        string highlight;

        html += (
            "<div class='table-content'>"
        );

        for (int i = 0; i < (data.GetLength(0) - 1); i++) {
            highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");

            html += (
                "<ul class='table-row-content " + highlight + "'>" +
                "   <li id='table-content-report-table-cal-capital-and-interest-col1'>" +
                "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col2'>" +
                "       <div>" + double.Parse(data[i, 1]).ToString("###,##0.00") + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col3'>" +
                "       <div>" + double.Parse(data[i, 2]).ToString("#,##0.00") + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col4'>" +
                "       <div>" + double.Parse(data[i, 3]).ToString("#,##0.00") + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col5'>" +
                "       <div>" + double.Parse(data[i, 4]).ToString("#,##0.00") + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col6'>" +
                "       <div>" + data[i, 5] + "</div>" +
                "   </li>" +
                "   <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col7'>" +
                "       <div>666</div>" +
                "   </li>" +
                "</ul>"
            );
        }

        html += (
            "</div>"
        );

        return html;
    }

    public static string CalReportTableCalCapitalAndInterest(string cp2id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPReportTableCalCapitalAndInterest(cp2id);

        if (data.GetLength(0) > 0) {
            string studentIDDefault = data[0, 3];
            string titleNameDefault = data[0, 4];
            string firstNameDefault = data[0, 5];
            string lastNameDefault = data[0, 6];
            string facultyCodeDefault = data[0, 10];
            string facultyNameDefault = data[0, 11];
            string programCodeDefault = data[0, 7];
            string programNameDefault = data[0, 8];
            string groupNumDefault = data[0, 12];
            string dlevelDefault = data[0, 14];
            string pictureFileNameDefault = data[0, 15];
            string pictureFolderNameDefault = data[0, 16];
            string capital = (!string.IsNullOrEmpty(data[0, 21]) ? double.Parse(data[0, 21]).ToString("#,##0.00") : double.Parse(data[0, 20]).ToString("#,##0.00"));
            string lawyerFullnameDefault = data[0, 22];
            string lawyerPhoneNumberDefault = data[0, 23];
            string lawyerMobileNumberDefault = data[0, 24];
            string lawyerEmailDefault = data[0, 25];
            string lawyerDefault = string.Empty;

            ArrayList lawyerPhoneNumber = new ArrayList();

            if (!string.IsNullOrEmpty(lawyerPhoneNumberDefault))
                lawyerPhoneNumber.Add(lawyerPhoneNumberDefault);

            if (!string.IsNullOrEmpty(lawyerMobileNumberDefault))
                lawyerPhoneNumber.Add(lawyerMobileNumberDefault);

            if (!string.IsNullOrEmpty(lawyerFullnameDefault) &&
                (!string.IsNullOrEmpty(lawyerPhoneNumberDefault) || !string.IsNullOrEmpty(lawyerMobileNumberDefault) && !string.IsNullOrEmpty(lawyerEmailDefault))) {
                lawyerDefault += (
                    "คุณ<span>" + lawyerFullnameDefault + "</span>" + (lawyerPhoneNumber.Count > 0 ? (" ( <span>" + string.Join(", ", lawyerPhoneNumber.ToArray()) + "</span> )") : string.Empty) +
                    " อีเมล์ <span>" + lawyerEmailDefault + "</span>"
                );
            }

            string[] contractInterest = eCPUtil.GetContractInterest();

            html += (
                "<div class='form-content' id='cal-report-table-cal-capital-and-interest'>" +
                "   <input type='hidden' id='cp2id' value='" + cp2id + "'>" +
                "   <input type='hidden' id='capital-hidden' value='" + double.Parse(capital).ToString("#,##0.00") + "' />" +
                "   <input type='hidden' id='interest-hidden' value='" + double.Parse(contractInterest[1]).ToString("#,##0.00") + "' />" +
                "   <input type='hidden' id='capital-old' value='' />" +
                "   <input type='hidden' id='interest-old' value='' />" +
                "   <input type='hidden' id='pay-old' value='' />" +
                "   <input type='hidden' id='period-old' value='' />" +
                "   <input type='hidden' id='payment-date-old' value='' />" +
                "   <input type='hidden' id='pay-least-hidden' value='" + eCPUtil.PAY_REPAY_LEAST.ToString("#,##0") + "' />" +
                "   <input type='hidden' id='period-least-hidden' value='" + eCPUtil.PERIOD_REPAY_LEAST.ToString("#,##0") + "' />" +
                "   <div id='profile-student'>" +
                "       <div class='content-left' id='picture-student'>" +
                "           <div>" +
                "               <img src='" + (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + pictureFolderNameDefault + "/" + pictureFileNameDefault) + "' width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "/>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='profile-student-label'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>รหัสนักศึกษา</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>ชื่อ - นามสกุล</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>ระดับการศึกษา</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>คณะ</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>หลักสูตร</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>นิติกรผู้รับผิดชอบ</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='profile-student-input'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + studentIDDefault + "&nbsp;" + programCodeDefault.Substring(0, 4) + " / " + programCodeDefault.Substring(4, 1) + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + titleNameDefault + firstNameDefault + " " + lastNameDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + dlevelDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + facultyCodeDefault + " - " + facultyNameDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + programCodeDefault + " - " + programNameDefault + (!groupNumDefault.Equals("0") ? " ( กลุ่ม " + groupNumDefault + " )" : "") + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>" + lawyerDefault + "</div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "   <div class='box3'></div>" +
                "   <div id='set-condition'>" +
                "       <div class='form-label-discription-style'>" +
                "           <div id='set-condition-label'>" +
                "               <div class='form-label-style'>กำหนดเงื่อนไขการคำนวณเงินต้นและดอกเบี้ย</div>" +
                "               <div class='form-discription-style'>" +
                "                   <div class='form-discription-line1-style'>กรุณาใส่เงื่อนไขเพื่อใช้แสดงตารางคำนวณเงินต้นและดอกเบี้ย</div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='form-input-style'>" +
                "           <div class='form-input-content' id='set-condition-input'>" +
                "               <div>" +
                "                   <div class='content-left' id='capital-label'>จำนวนเงินต้นคงเหลือยกมา</div>" +
                "                   <div class='content-left' id='capital-input'>" +
                "                       <input class='inputbox textbox-numeric' type='text' id='capital' onblur=Trim('capital');AddCommas('capital',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' />" +
                "                   </div>" +
                "                   <div class='content-left' id='capital-unit-label'>บาท</div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "               <div>" +
                "                   <div class='content-left' id='interest-label'>อัตราดอกเบี้ยร้อยละ</div>" +
                "                   <div class='content-left' id='interest-input'>" +
                "                       <input class='inputbox textbox-numeric' type='text' id='interest' onblur=Trim('interest');AddCommas('interest',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' />" +
                "                   </div>" +
                "                   <div class='content-left' id='interest-unit-label'>ต่อปี</div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "               <div>" +
                "                   <div class='content-left' id='condition-tablecalcapitalandinterest-label'>เงื่อนไขที่ใช้คำนวณ</div>" +
                "                   <div class='content-left' id='condition-tablecalcapitalandinterest-input'>" +
                "                       <div id='condition-select' class='combobox'>" +
                "                           <select id='condition-tablecalcapitalandinterest'>" +
                "                               <option value='0'>เลือกเงื่อนไขที่ใช้คำนวณ</option>"
            );

            for (int i = 0; i < eCPUtil.conditionTableCalCapitalAndInterest.GetLength(0); i++) { 
                html += (
                    "                           <option value='" + eCPUtil.conditionTableCalCapitalAndInterest[i, 1] + "'>" + eCPUtil.conditionTableCalCapitalAndInterest[i, 0] + "</option>"
                );
            }

            html += (
                "                           </select>" +
                "                       </div>" +
                "                       <div id='condition-input'>" +
                "                           <div id='condition-select-0'>" +
                "                               <input class='inputbox' type='text' id='pay-period' value='' style='width:100px' />" +
                "                           </div>" +
                "                           <div id='condition-select-1'>" +
                "                               <div class='content-left' id='pay-input'>" +
                "                                   <input class='inputbox textbox-numeric' type='text' id='pay' onblur=Trim('pay');AddCommas('pay',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' />" +
                "                               </div>" +
                "                               <div class='content-left' id='pay-unit-label'>บาท</div>" +
                "                           </div>" +
                "                           <div class='clear'></div>" +
                "                           <div id='condition-select-2'>" +
                "                               <div class='content-left' id='period-input'>" +
                "                                   <input class='inputbox textbox-numeric' type='text' id='period' onblur=Trim('period');AddCommas('period',0); onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' />" +
                "                               </div>" +
                "                               <div class='content-left' id='period-unit-label'>งวด</div>" +
                "                           </div>" +
                "                           <div class='clear'></div>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "               <div>" +
                "                   <div class='content-left' id='payment-date-label'>เริ่มชำระตั้งแต่วันที่</div>" +
                "                   <div class='content-left' id='payment-date-input'>" +
                "                       <input class='inputbox calendar' type='text' id='payment-date' readonly value='' />" +
                "                   </div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "   </div>" +
                "   <div class='button' id='button-style11'>" +
                "       <div class='button-style1'>" +
                "           <ul>" +
                "               <li>" +
                "                   <a href='javascript:void(0)' onclick='CalReportTableCalCapitalAndInterest()'>คำนวณ</a>" +
                "               </li>" +
                "               <li>" +
                "                   <a href='javascript:void(0)' onclick=ResetFrmCalReportTableCalCapitalAndInterest()>ล้าง</a>" +
                "               </li>" +
                "           </ul>" +
                "       </div>" +
                "   </div>" +
                "   <div id='list-cp-report-table-cal-capital-and-interest'>" +
                "       <div class='tab-line'></div>" +
                "       <div class='content-data-tab-content'>" +
                "           <div class='content-left'>" +
                "               <div class='content-data-tab-content-msg'>ตารางคำนวณเงินต้นและดอกเบี้ย</div>" +
                "           </div>" +
                "           <div class='content-right'>" +
                "               <div class='content-data-tab-content-msg' id='record-count-cal-table-cal-capital-and-interest'>ทั้งหมด 0 งวด</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "       <div class='tab-line'></div>" +
                "       <div class='box3'>" +
                "           <div class='table-head'>" +
                "               <ul>" +
                "                   <li id='table-head-report-table-cal-capital-and-interest-col1'>" +
                "                       <div class='table-head-line1'>งวดที่ชำระ</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col2'>" +
                "                       <div class='table-head-line1'>เงินต้นคงเหลือ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col3'>" +
                "                       <div class='table-head-line1'>ชำระเงินต้น</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col4'>" +
                "                       <div class='table-head-line1'>ชำระดอกเบี้ยรับ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col5'>" +
                "                       <div class='table-head-line1'>รวมเงินที่รับชำระ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col6'>" +
                "                       <div class='table-head-line1'>กำหนดชำระ</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col7'>" +
                "                       <div class='table-head-line1'>วันที่รับชำระ</div>" +
                "                   </li>" +
                "               </ul>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>" +
                "       <div id='box-list-table-cal-capital-and-interest'>" +
                "           <div id='list-table-cal-capital-and-interest'></div>" +
                "       </div>" +
                "       <div id='sumtotal-table-cal-capital-and-interest'>" +
                "           <div class='table-content'>" +
                "               <ul class='table-row-content'>" +
                "                   <li id='table-content-sumtotal-table-cal-capital-and-interest-col1'>" +
                "                       <div>รวมทั้งสิ้น</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col2'>" +
                "                       <div id='sum-pay-capital'></div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col3'>" +
                "                       <div id='sum-pay-interest'></div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col4'>" +
                "                       <div id='sum-total-pay'></div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col5'>" +
                "                       <div>&nbsp;</div>" +
                "                   </li>" +
                "               </ul>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='button' id='button-style12'>" +
                "       <div class='button-style1'>" +
                "           <ul>" +
                "               <li>" +
                "                   <a href='javascript:void(0)' onclick='ExportReportTableCalCapitalAndInterest()'>พิมพ์</a>" +
                "               </li>" +
                "               <li>" +
                "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'cal-report-table-cal-capital-and-interest')>ปิด</a>" +
                "               </li>" +
                "           </ul>" +
                "       </div>" +
                "   </div>" +
                "</div>" +
                "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                "<form id='export-setvalue' method='post' target='export-target'>" +
                "   <input id='export-send' name='export-send' value='' type='hidden' />" +
                "   <input id='export-order' name='export-order' value='' type='hidden' />" +
                "   <input id='export-type' name='export-type' value='' type='hidden' />" +
                "</form>"
            );
        }

        return html;
    }

    public static string ListCPReportTableCalCapitalAndInterest(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;                        
        int recordCount = eCPDB.CountCPReportTableCalCapitalAndInterest(c);
        
        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPReportTableCalCapitalAndInterest(c);
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                callFunc = ("OpenTab('link-tab2-cp-report-table-cal-capital-and-interest','#tab2-cp-report-table-cal-capital-and-interest','ตารางคำนวณ',false,'','" + data[i, 1] + "','')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-table-cal-capital-and-interest" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-table-cal-capital-and-interest-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + (data[i, 8] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 17]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col6' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 18]) ? double.Parse(data[i, 18]).ToString("#,##0.00") : "0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col7' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 19]) ? double.Parse(data[i, 19]).ToString("#,##0.00") : double.Parse(data[i, 17]).ToString("#,##0.00")) + "</div></li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reporttablecalcapitalandinterest", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list><pagenav>" + pageHtml + "<pagenav>"
        );
    }
    
    public static string TabCPReportTableCalCapitalAndInterest() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-table-cal-capital-and-interest-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-table-cal-capital-and-interest") +
            "       <div class='content-data-tabs' id='tabs-cp-report-table-cal-capital-and-interest'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-report-table-cal-capital-and-interest' alt='#tab1-cp-report-table-cal-capital-and-interest' href='javascript:void(0)'>รายการชำระหนี้</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab2-cp-report-table-cal-capital-and-interest' alt='#tab2-cp-report-table-cal-capital-and-interest' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-report-table-cal-capital-and-interest' value=''>" +
            "                   <input type='hidden' id='id-name-report-table-cal-capital-and-interest-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-report-table-cal-capital-and-interest-hidden' value=''>" +
            "                   <input type='hidden' id='program-report-table-cal-capital-and-interest-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreporttablecalcapitalandinterest',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-table-cal-capital-and-interest'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-report-table-cal-capital-and-interest-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order2'>" +
            "                   <div class='box-search-condition-order-title'>คณะ</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order2-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order3'>" +
            "                   <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order3-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-table-cal-capital-and-interest-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col4'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col5'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col6'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col7'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้คงเหลือ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-contents'></div>" +
            "</div>" +
            "<div id='cp-report-table-cal-capital-and-interest-content'>" +
            "   <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-content'>" +
            "       <div class='box4' id='list-data-report-table-cal-capital-and-interest'></div>" +
            "       <div id='nav-page-report-table-cal-capital-and-interest'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-content'>" +
            "       <div class='box1' id='cal-data-report-table-cal-capital-and-interest'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportNoticeRepayComplete {
    private static string ExportCPReportNoticeRepayCompleteSection(
        int section,
        string font,
        string[,] data
    ) {
        string html = string.Empty;

        if (section.Equals(1)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. ๐ ๒๘๔๙ ๔๕๗๓ โทรสาร ๐ ๒๘๔๙ ๔๕๕๘</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;ศธ ๐๕๑๗/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>การชดใช้เงินแทนการปฏิบัติงานชดใช้ทุน</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(2)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;บัดนี้ " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ซึ่งสำเร็จการศึกษาจาก" + data[0, 7] + " เมื่อวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 11])) + " " +
                "ได้ชดใช้เงินแทนการปฏิบัติงานตามสัญญาฯ ซึ่งมหาวิทยาลัยคิดคำนวณแล้วเป็นเงินทั้งสิ้น " + Util.NumberArabicToThai(double.Parse(data[0, 14]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 14]) + ") " +
                "โดย" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ได้นำเงินดังกล่าวมาชำระให้กับมหาวิทยาลัยมหิดลเรียบร้อยแล้ว" +
                "       </p>" +
                "   </td>" +
                "</tr>"
            );
        }
                    
        if (section.Equals(3)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดทราบ" +
                "       </p>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='200'></td>" +
                "               <td width='400'>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ขอแสดงความนับถือ</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>(ชื่อ)</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ตำแหน่ง</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ตำแหน่ง</div>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        return html;
    }

    public static void ExportCPReportNoticeRepayComplete(string exportSend) {
        string html = string.Empty;
        char[] separator = new char[] { ';' };
        string[] cp1id = exportSend.Split(separator);

        for (int i = 1; i <= cp1id.Length; i++) {
            string[,] data = eCPDB.ListDetailReportNoticeRepayComplete(cp1id[i - 1]);
            string width = "600";
            string font = "TH SarabunPSK, TH Sarabun New";

            html += (
                "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                "   <tr>" +
                "       <td width='" + width + "' valign='top'>" +
                "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeRepayCompleteSection(1, font, data) +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div>" +
                "                           <table border='0' cellpadding='0' cellspacing='0'>" +
                "                               <tr>" +
                "                                   <td width='50'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
                "                                   </td>" +
                "                                   <td width='550'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>ผู้อำนวยการ" + data[0, 13] + "</div>" +
                "                                   </td>" +
                "                               </tr>" +
                "                           </table>" +
                "                       </div>" +
                "                       <div>" +
                "                           <table border='0' cellpadding='0' cellspacing='0'>" +
                "                               <tr>" +
                "                                   <td width='50' valign='top'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
                "                                   </td>" +
                "                                   <td width='550'>" +
                "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือ" + data[0, 13] + "</div>" +
                "                                   </td>" +
                "                               </tr>" +
                "                           </table>" +
                "                       </div>" +
                "                       <div>" +
                "                           <table border='0' cellpadding='0' cellspacing='0'>" +
                "                               <tr>" +
                "                                   <td width='98' valign='top'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>สิ่งที่ส่งมาด้วย</div>" +
                "                                   </td>" +
                "                                   <td width='502'>" +
                "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาใบเสร็จรับเงิน จำนวน ๑ ฉบับ</div>" +
                "                                   </td>" +
                "                               </tr>" +
                "                           </table>" +
                "                       </div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง " + data[0, 13] + " แจ้งการลาออกจากการปฏิบัติงานในระหว่างปฏิบัติงาน" +
                "ชดใช้ทุนของ " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ได้ทำสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + "ไว้กับมหาวิทยาลัยมหิดล ตามสัญญาฯ เมื่อสำเร็จการศึกษาหลักสูตร" + data[0, 9] + "แล้ว " +
                "ต้องปฏิบัติงานชดใช้ทุนเป็นเวลา " + (!string.IsNullOrEmpty(data[0, 12]) && !data[0, 12].Equals("0") ? Util.NumberArabicToThai(data[0, 12]) : string.Empty) + " ปี นั้น" +
                "                       </p>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                                ExportCPReportNoticeRepayCompleteSection(2, font, data) +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                                ExportCPReportNoticeRepayCompleteSection(3, font, data) +
                "           </table>" +
                "       </td>" +
                "   </tr>" +
                "   <tr>" +
                "       <td width='" + width + "' valign='top'>" +
                "           <table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeRepayCompleteSection(1, font, data) +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='" + width + "'>" +
                "                       <div>" +
                "                           <table border='0' cellpadding='0' cellspacing='0'>" +
                "                               <tr>" +
                "                                   <td width='50'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
                "                                   </td>" +
                "                                   <td width='550'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>คณะกรรมการพิจารณาจัดสรรนักศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + "</div>" +
                "                                   </td>" +
                "                               </tr>" +
                "                           </table>" +
                "                       </div>" +
                "                       <div>" +
                "                           <table border='0' cellpadding='0' cellspacing='0'>" +
                "                               <tr>" +
                "                                   <td width='98' valign='top'>" +
                "                                       <div style='font:normal 15pt " + font + ";'>สิ่งที่ส่งมาด้วย</div>" +
                "                                   </td>" +
                "                                   <td width='502'>" +
                "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาใบเสร็จรับเงิน จำนวน ๑ ฉบับ</div>" +
                "                                   </td>" +
                "                               </tr>" +
                "                           </table>" +
                "                       </div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่ " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ได้ทำสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + "ไว้กับมหาวิทยาลัยมหิดล " +
                "ซึ่งตามสัญญาฯ เมื่อสำเร็จการศึกษาหลักสูตร" + data[0, 9] + "แล้ว ต้องปฏิบัติงานชดใช้ทุนเป็นเวลา " + (!string.IsNullOrEmpty(data[0, 12]) && !data[0, 12].Equals("0") ? Util.NumberArabicToThai(data[0, 12]) : string.Empty) + " ปี นั้น" +
                "                       </p>" +
                "                   </td>" +
                "               </tr>" +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                                ExportCPReportNoticeRepayCompleteSection(2, font, data) +
                "               <tr>" +
                "                   <td width='100%'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   </td>" +
                "               </tr>" +
                                ExportCPReportNoticeRepayCompleteSection(3, font, data) +
                "           </table>" +
                "       </td>" +
                "   </tr>" +
                "</table>"
            );
        }

        html += (
            "<div class='filename hidden'>NoticeRepayComplete.doc</div>" +
            "<div class='contenttype hidden'>application/msword</div>"
        );

        /*
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=NoticeRepayComplete.doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        */
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);
    }

    public static string ListCPReportNoticeRepayComplete(HttpContext c) {
        string html = string.Empty;        
        string pageHtml = string.Empty;                
        int recordCount = eCPDB.CountCPReportNoticeRepayComplete(c);
        
        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportNoticeRepayComplete(c);
            string highlight;
            string groupNum;
            string iconStatus;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                iconStatus = eCPUtil.iconPaymentStatus[(!string.IsNullOrEmpty(data[i, 11]) ? (int.Parse(data[i, 11]) - 1) : 0)];
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-notice-repay-complete-col1'>" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col2'>" +
                    "       <div>" +
                    "           <input class='checkbox' type='checkbox' name='print-notice-repay-complete' onclick=UncheckRoot('check-uncheck-all') value='" + data[i, 1] + "' />" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col3'>" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col4'>" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col5'>" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + (data[i, 8] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col6'>" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-repay-complete-col7'>" +
                    "       <div class='icon-status-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus + "'></li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportnoticerepaycomplete", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportNoticeRepayComplete() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-notice-repay-complete-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-notice-repay-complete") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'>" +
            "               <input type='hidden' id='search-report-notice-repay-complete' value=''>" +
            "               <input type='hidden' id='id-name-report-notice-repay-complete-hidden' value=''>" +
            "               <input type='hidden' id='faculty-report-notice-repay-complete-hidden' value=''>" +
            "               <input type='hidden' id='program-report-notice-repay-complete-hidden' value=''>" +
            "               <div class='button-style2'>" +
            "                   <ul>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportnoticerepaycomplete',true,'','','')>ค้นหา</a>" +
            "                       </li>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick='ConfirmPrintNoticeRepayComplete()'>พิมพ์</a>" +
            "                       </li>" +
            "                   </ul>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-cp-report-notice-repay-complete'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box-search-condition' id='search-report-notice-repay-complete-condition'>" +
            "           <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "           <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order1'>" +
            "               <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order1-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order2'>" +
            "               <div class='box-search-condition-order-title'>คณะ</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order2-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order3'>" +
            "               <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order3-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-report-notice-repay-complete-col1'>" +
            "                   <div class='table-head-line1'>ลำดับ</div>" +
            "                   <div>ที่</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col2'>" +
            "                   <div class='table-head-line1'>เลือก</div>" +
            "                   <div>" +
            "                       <input class='checkbox' type='checkbox' name='check-uncheck-all' id='check-uncheck-all' onclick=CheckUncheckAll('check-uncheck-all','print-notice-repay-complete') />" +
            "                   </div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col3'>" +
            "                   <div class='table-head-line1'>รหัส</div>" +
            "                   <div>นักศึกษา</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col4'>" +
            "                   <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col5'>" +
            "                   <div class='table-head-line1'>หลักสูตร</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col6'>" +
            "                   <div class='table-head-line1'>ยอดเงินต้นที่ชดใช้</div>" +
            "                   <div>( บาท )</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-repay-complete-col7'>" +
            "                   <div class='table-head-line1'>สถานะการชำระหนี้</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailpaymentstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>" +
            "<div id='cp-report-notice-repay-complete-content'>" +
            "   <div class='box4' id='list-data-report-notice-repay-complete'></div>" +
            "   <div id='nav-page-report-notice-repay-complete'></div>" +
            "</div>"
        );                 

        return html;
    }
}

public class eCPDataReportNoticeClaimDebt {
    private static string ExportCPReportNoticeClaimDebtSection(
        int section,
        string font,
        Dictionary<string, string> lawyer
    ) {
        string html = string.Empty;
        string lawyerFullname = string.Empty;
        string lawyerFullnameWithoutNamePrefix = string.Empty;
        string lawyerPhoneNumber = string.Empty;

        if (lawyer != null) {
            lawyerFullname = lawyer["Fullname"];
            lawyerFullnameWithoutNamePrefix = lawyer["FullnameWithoutNamePrefix"];
            lawyerPhoneNumber = lawyer["PhoneNumber"];
        }

        if (section.Equals(1)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. " + Util.NumberArabicToThai(lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>ขอให้ชดใช้เงิน</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(2)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดดำเนินการ" +
                "       </p>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='200'></td>" +
                "               <td width='400'>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ขอแสดงความนับถือ</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>(" + eCPUtil.DIRECTOR + ")</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ปฏิบัติหน้าที่แทนอธิการบดีมหาวิทยาลัยมหิดล</div>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(3)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>กองกฎหมาย สำนักงานอธิการบดี</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. " + Util.NumberArabicToThai(lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘.๐๑๙/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(4)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='98'></td>" +
                "               <td width='502' style='text-align: center'>" +
                "                   <div style='font:normal 15pt " + font + ";'>(นาย/นาง/นางสาว" + lawyerFullname + ")</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>นิติกร</div>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(5)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>งานกฎหมายและนิติกรรมสัญญา</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>กองกฎหมาย สำนักงานอธิการบดี</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. ๐ ๒๘๔๙ ๖๒๖๐ โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘.๐๑๙/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>ขอความอนุเคราะห์บันทึกบัญชีลูกหนี้ผิดสัญญา</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(6)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดพิจารณาและดำเนินการในส่วนที่เกี่ยวข้องต่อไป" +
                "       </p>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='200'></td>" +
                "               <td width='400'>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>(" + eCPUtil.DIRECTOR + ")</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' style='text-align: right;'>" +
                "       <div style='font:normal 12pt " + font + ";'>ผู้รับผิดชอบ : " + lawyerFullnameWithoutNamePrefix + "</div>" +
                "       <div style='font:normal 12pt " + font + ";'>ผู้ตรวจ : พัศนาภรณ์</div>" +
                "   </td>" +
                "</tr>"
            );
        }

        return html;
    }
    
    private static void ExportCPReportNoticeClaimDebtTime1(
        string template,
        string[,] data
    ) {
        /*
        string html = string.Empty;
        string width = "600";
        string font = "TH SarabunPSK, TH Sarabun New";
        */
        string pdfFontRegular = "Font/THSarabunNew.ttf";
        string pdfFontBold = "Font/THSarabunNewBold.ttf";
        string saveFile = template;
        string resignationDate = string.Empty;
        int fontSize = 15;
        
        if (data[0, 19].Equals("N"))
            resignationDate = data[0, 16];

        if (data[0, 19].Equals("Y"))
            resignationDate = data[0, 20];

        Dictionary<string, string> lawyer = new Dictionary<string, string>();
        lawyer.Add("Fullname", (!string.IsNullOrEmpty(data[0, 21]) ? data[0, 21] : string.Empty));
        lawyer.Add("FullnameWithoutNamePrefix", (!string.IsNullOrEmpty(data[0, 21]) ? data[0, 21].Replace("นาย", "").Replace("นางสาว", "").Replace("นาง", "") : string.Empty));
        lawyer.Add("FirstName", (!string.IsNullOrEmpty(data[0, 21]) ? (data[0, 21].Split(' '))[0] : string.Empty));
        lawyer.Add("PhoneNumber", (!string.IsNullOrEmpty(data[0, 22]) ? data[0, 22] : data[0, 23]));
        lawyer.Add("Email", (!string.IsNullOrEmpty(data[0, 24]) ? data[0, 24] : string.Empty));

        Export2PDF export2PDF = new Export2PDF();
        export2PDF.ExportToPDFConnect(saveFile);

        export2PDF.PDFConnectTemplate(("ExportTemplate/" + template), "pdf");        
        export2PDF.PDFAddTemplate("pdf", 1, 1);
        
        if (template.Equals("NoticeClaimDebt@DTDAU-0U.0.2.1.Y.af.63.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty) , 436, 623, 139, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 447, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 430, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 237, 430, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 328, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 328, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 240, 260, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDAU-0U.0.2.2.af.63.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 436, 623, 139, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 362, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 362, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 240, 294, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDSB-0B.0.2.1.Y.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 395, 634, 145, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 452, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 436, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 237, 436, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 340, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 340, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 276, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDSB-0B.0.2.1.Y.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 367, 634, 172, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 436, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 420, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 237, 420, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 324, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 324, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 240, 260, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDSB-0B.0.2.2.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 413, 623, 127, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDSB-0B.0.2.2.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 715, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 682, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 642, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 367, 625, 172, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 334, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 334, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 266, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDTU-0U.0.2.1.Y.af.62.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 717, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 684, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 645, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 389, 628, 150, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 457, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 441, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 237, 441, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 342, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 342, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 276, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@DTDTU-0U.0.2.2.af.62.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 389, 623, 150, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@NSNSB-0B.0.1.0.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 623, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(data[0, 27]) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(data[0, 27])).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 69, 464, 105, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 447, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 447, 295, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@NSNSB-0B.0.2.1.Y.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 634, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 452, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 436, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 436, 297, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 340, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 340, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 276, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@NSNSB-0B.0.2.2.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 623, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@PYPYB-2B.0.2.1.Y.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 81, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 345, 634, 195, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 436, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 420, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 420, 297, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 324, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 324, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 260, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@PYPYB-2B.0.2.2.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 345, 623, 194, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 328, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 328, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 260, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@RAMDB-0B.0.2.1.Y.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 717, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 684, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 645, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 394, 628, 145, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 441, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 424, 72, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 424, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 325, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 325, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 259, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@RAMDB-0B.0.2.1.Y.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 348, 634, 192, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 436, 108, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 420, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 420, 297, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 324, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 324, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 260, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@RAMDB-0B.0.2.2.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 394, 623, 146, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@RAMDB-0B.0.2.2.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 715, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 682, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 642, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 348, 625, 192, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 334, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 334, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 266, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@RANSB-0B.0.1.0.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 623, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(data[0, 27]) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(data[0, 27])).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 69, 464, 105, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 447, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 447, 295, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@RANSB-0B.0.2.1.Y.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 720, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 688, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 650, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 634, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 452, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 436, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 436, 295, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 340, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 340, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 276, 165, 0);
        }

        if (template.Equals("NoticeClaimDebt@RANSB-0B.0.2.2.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 430, 623, 108, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 154, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIMDB-0B.0.2.1.Y.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 717, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 684, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 645, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 394, 628, 145, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 441, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 424, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 424, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 325, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 325, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 259, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIMDB-0B.0.2.1.Y.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 717, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 684, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 645, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 348, 628, 191, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 441, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 424, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 424, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 325, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 325, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 259, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIMDB-0B.0.2.2.af.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 411, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 443, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 394, 623, 146, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIMDB-0B.0.2.2.bf.65.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 715, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 682, 410, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 642, 442, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 348, 625, 191, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 334, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 334, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 266, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIPNU-0U.2.1.0.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 395, 623, 143, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(data[0, 27]) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(data[0, 27])).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 69, 464, 105, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 447, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 447, 295, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIPNU-0U.2.2.1.Y.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 395, 623, 143, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(eCPUtil.ConvertDateEN(resignationDate)).AddDays(1).ToString()).Replace("พ.ศ. ", string.Empty) : ""), 189, 447, 107, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 139, 430, 71, 0);
            export2PDF.FillForm(pdfFontBold, fontSize, 0, Util.ThaiBaht(data[0, 18]), 238, 430, 298, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 328, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 328, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 260, 164, 0);
        }

        if (template.Equals("NoticeClaimDebt@SIPNU-0U.2.2.2.pdf")) {
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 351, 714, 80, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 680, 409, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 97, 640, 441, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])).Replace("พ.ศ. ", string.Empty), 395, 623, 143, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FullnameWithoutNamePrefix"], 85, 345, 155, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 245, 345, 79, 0);
            export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["Email"], 241, 277, 164, 0);
        }

        string programCode = data[0, 8];
        string contractName = string.Empty;

        switch (programCode) {
            case "SIMDB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาแพทยศาสตร์ฯ";
                break;
            case "RAMDB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาแพทยศาสตร์ฯ";
                break;
            case "RANSB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาในหลักสูตรวิชาพยาบาลศาสตรบัณฑิตฯ";
                break;
            case "NSNSB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาในหลักสูตรวิชาพยาบาลศาสตรบัณฑิตฯ";
                break;
            case "DTDSB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาทันตแพทยศาสตร์ฯ";
                break;
            case "PYPYB":
                contractName = "สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาเภสัชศาสตร์ฯ";
                break;
            case "SIPNU":
                contractName = "สัญญาการเป็นนักศึกษาหลักสูตรประกาศนียบัตรผู้ช่วยพยาบาลฯ";
                break;
            case "DTDAU":
                contractName = "สัญญาการเป็นนักศึกษาหลักสูตรประกาศนียบัตรผู้ช่วยทันตแพทย์ขั้นสูงฯ";
                break;
            case "DTDTU":
                contractName = "สัญญาเข้าศึกษาหลักสูตรประกาศนียบัตรวิชาช่างทันตกรรมฯ";
                break;
        }

        export2PDF.PDFConnectTemplate("ExportTemplate/FormRequestRecordAccountDebtor.pdf", "pdf", true);
        export2PDF.PDFAddTemplate("pdf", 1, 1);

        export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(lawyer["PhoneNumber"]), 345, 686, 90, 0);
        export2PDF.FillForm(pdfFontRegular, fontSize, 0, (Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString())), 129, 646, 409, 0);
        export2PDF.FillForm(pdfFontRegular, fontSize, 0, (data[0, 3] + data[0, 4] + " " + data[0, 5]), 368, 570, 201, 0);
        export2PDF.FillForm(pdfFontRegular, fontSize, 0, contractName, 115, 550, 322, 0);
        export2PDF.FillForm(pdfFontRegular, fontSize, 0, Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")), 134, 530, 143, 0);        

        fontSize = 9;
        export2PDF.FillForm(pdfFontRegular, fontSize, 0, lawyer["FirstName"], 498, 278, 70, 0);

        export2PDF.ExportToPdfDisconnect();

        /*
        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportNoticeClaimDebtSection(1, font, lawyer) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" + 
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>" + (data[0, 3] + data[0, 4] + " " + data[0, 5]) + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50' valign='top'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + " ฉบับลงวันที่ " +  Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(data[0, 13])) + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่ท่านได้ทำสัญญาที่อ้างถึงผูกพันไว้กับมหาวิทยาลัยมหิดลว่าภายหลังจากสำเร็จการศึกษาตามหลักสูตร " +
            "ท่านยินยอมปฏิบัติตามคำสั่งของสำนักงานคณะกรรมการข้าราชการพลเรือนและหรือ" +
            "คณะกรรมการพิจารณาจัดสรรนักศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + "ผู้สำเร็จการศึกษาไปปฏิบัติงานในส่วนราชการหรือ" +
            "องค์การของรัฐบาลต่าง ๆ ได้สั่งให้เข้ารับราชการหรือทำงาน และจะรับราชการหรือทำงานอยู่ต่อไปเป็นเวลา" +
            "ไม่น้อยกว่า" + Util.ThaiNum(data[0, 11]) + "ปีติดต่อกันไปนับตั้งแต่วันที่ได้กำหนดในคำสั่ง หากท่านไม่รับราชการหรือทำงาน ท่านยินยอม" +
            "รับผิดชดใช้เงินจำนวน " + Util.NumberArabicToThai(double.Parse(data[0, 12]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 12]) + ") หากรับราชการหรือทำงานไม่ครบตามกำหนดเวลา " +
            "ท่านยินยอมรับผิดชดใช้เงินให้แก่มหาวิทยาลัยตามระยะเวลาที่ขาดโดยคิดคำนวณลดลงตามส่วนเฉลี่ยจากเงิน" +
            "ที่ต้องชดใช้ดังกล่าว นั้น" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า การที่ท่านขอลาออกจากราชการ/การปฏิบัติงานตั้งแต่วันที่ " +
            (!string.IsNullOrEmpty(resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(Util.ConvertDateEN(resignationDate)).AddDays(1).ToString()) : "") + " ถือว่าเป็นการปฏิบัติราชการ/ปฏิบัติงานไม่ครบกำหนดตามสัญญาที่ให้ไว้ เป็นเหตุให้ท่าน" +
            "ต้องรับผิดชอบชดใช้เงินแก่มหาวิทยาลัยมหิดลเป็นจำนวนทั้งสิ้น <strong>" + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 18]) + ")</strong> " +
            "ดังนั้นจึงขอให้ท่านนำเงินจำนวนดังกล่าวมาชำระภายใน ๓๐ วัน นับถัดจากวันที่ได้รับหนังสือฉบับนี้ โดยดำเนินการ ดังนี้" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๑. ชำระเงิน โดยนำฝากเงินเข้าบัญชี</strong>ธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" " +
            "ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ หรือ<strong>โอนเงินเข้าบัญชี</strong>ธนาคารไทยพาณิชย์ " +
            "ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี ๐๑๖-๒-๑๐๓๒๒-๓" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๒. กรณีชำระเงินเกินกำหนดระยะเวลาดังกล่าว</strong> (มีดอกเบี้ยผิดนัดชำระ) ให้ท่านติดต่อนิติกร" +
            "ผู้รับผิดชอบ: คุณ" + lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(lawyer["PhoneNumber"]) + ") เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ก่อนแล้วจึงชำระเงิน" + 
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๓. กรณีมีความจำเป็นที่ไม่อาจดำเนินการตามข้อ ๑. และ ๒ ได้</strong> ให้ท่านติดต่อขอชำระเงินสด " +
            "ผ่านกองกฎหมาย สำนักงานอธิการบดี มหาวิทยาลัยมหิดล ภายในเวลา ๑๕.๐๐ น. ในวันทำการ" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ให้ท่านจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร (Fax) " +
            "หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + lawyer["Email"] + " หากล่วงเลย" +
            "ระยะเวลาดังกล่าว มหาวิทยาลัยจำต้องคิดดอกเบี้ยผิดนัด และดำเนินการตามกฎหมายต่อไป" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportNoticeClaimDebtSection(2, font, null) +
            "           </table>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportNoticeClaimDebtSection(5, font, null) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองคลัง</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ด้วยมหาวิทยาลัยได้ดำเนินการตรวจสอบข้อผูกพันและเรียกให้ " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ซึ่งเป็น" +
            "ผู้ผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + " ฉบับลงวันที่ " +  Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(data[0, 13])) + " ที่ได้ทำไว้กับมหาวิทยาลัย ชดใช้เงินจำนวน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท ให้แก่" +
            "มหาวิทยาลัย" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "              </tr>" +
            "              <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;กองกฎหมายขอเรียนว่า เพื่อให้การบันทึกบัญชีลูกหนี้ผิดสัญญาเป็นไปตามเกณฑ์คงค้างที่มหาวิทยาลัย" +
            "กำหนดไว้ในนโยบายบัญชี และเพื่อให้การแสดงสินทรัพย์ที่ควรได้รับในงบทางการเงินของมหาวิทยาลัยเป็นไปอย่างถูกต้อง " +
            "ในการนี้ กองกฎหมายจึงขอความอนุเคราะห์มายังกองคลังเพื่อดำเนินการบันทึกบัญชีลูกหนี้ผิดสัญญารายนี้ โดยได้จัดทำ" +
            "แบบขอสร้างและปรับปรุงข้อมูลหลักลูกหนี้ พร้อมแนบเอกสารหลักฐานที่เกี่ยวข้องเรียบร้อยแล้ว รายละเอียดปรากฏตาม" +
            "เอกสารที่แนบมานี้" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportNoticeClaimDebtSection(6, font, lawyer) +
            "           </table>" +
            "       </td>" +
            "   </tr>" +
            "</table>"
        );

        return html;
        */
    }

    private static string ExportCPReportNoticeClaimDebtTime2(
        string[,] data,
        string overpaymentDateStart
    ) {
        string html = string.Empty;
        string width = "600";
        string font = "TH SarabunPSK, TH Sarabun New";
        string[] contractInterest = eCPUtil.GetContractInterest();
        string cp2id = data[0, 1];
        string statusRepayDefault = data[0, 25];
        string replyDate = string.Empty;
        string pursuant = string.Empty;
        string pursuantBookDate = string.Empty;

        Dictionary<string, string> lawyer = new Dictionary<string, string>();
        lawyer.Add("Fullname", (!string.IsNullOrEmpty(data[0, 21]) ? data[0, 21] : string.Empty));
        lawyer.Add("FullnameWithoutNamePrefix", (!string.IsNullOrEmpty(data[0, 21]) ? data[0, 21].Replace("นาย", "").Replace("นางสาว", "").Replace("นาง", "") : string.Empty));
        lawyer.Add("PhoneNumber", (!string.IsNullOrEmpty(data[0, 22]) ? data[0, 22] : data[0, 23]));
        lawyer.Add("Email", (!string.IsNullOrEmpty(data[0, 24]) ? data[0, 24] : string.Empty));

        string[,] data1 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, statusRepayDefault);

        if (data1.GetLength(0) > 0) {
            replyDate = data1[0, 5];
            pursuant = data1[0, 6];
            pursuantBookDate = data1[0, 7];
        }

        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0' style='margin: 0; padding: 0; top: 0'>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportNoticeClaimDebtSection(1, font, lawyer) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>" + (data[0, 3] + data[0, 4] + " " + data[0, 5]) + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50' valign='top'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!string.IsNullOrEmpty(pursuant) ? Util.NumberArabicToThai(pursuant) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(pursuantBookDate)) : "") + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา " +
            "เพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท " +
            "(" + Util.ThaiBaht(data[0, 18]) + ") ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือ" +
            "ดังกล่าว และท่านได้รับหนังสือดังกล่าวแล้วเมื่อวันที่ " + (!string.IsNullOrEmpty(replyDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(replyDate)) : "") + " ความละเอียดทราบแล้ว นั้น" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า บัดนี้ได้ล่วงเลยระยะเวลาตามที่กำหนดแล้ว ท่านยังมิได้ชำระเงินดังกล่าวแต่อย่างใด " +
            "ในการนี้ จึงขอให้ท่านเร่งนำเงินจำนวน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 18]) + ") " +
            "พร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(contractInterest[1]) + " ต่อปีของต้นเงินจำนวนข้างต้น " +
            "นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(overpaymentDateStart)) + " ซึ่งเป็นวันผิดนัด จนถึงวันที่ท่านชำระเสร็จสิ้น มาชำระให้แก่มหาวิทยาลัยมหิดลโดยเร็ว " +
            "มิเช่นนั้นมหาวิทยาลัยมหิดลจำต้องดำเนินการตามกฎหมายต่อไป" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ขอให้ท่านติดต่อนิติกรผู้รับผิดชอบ: คุณ" + lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(lawyer["PhoneNumber"]) + ") " +
            "เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ให้แก่มหาวิทยาลัยมหิดล แล้วจึงชำระเงินจำนวนดังกล่าวโดยนำฝากเงินเข้าบัญชีธนาคาร" +
            "ไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ " +
            "หรือโอนเงินเข้าบัญชีธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี " +
            "๐๑๖-๒-๑๐๓๒๒-๓ และจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร(Fax) " +
            "หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + lawyer["Email"] +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportNoticeClaimDebtSection(2, font, null) +
            "           </table>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportNoticeClaimDebtSection(1, font, lawyer) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>" + data[0, 14] + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50' valign='top'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>สัญญาค้ำประกัน ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 15])) + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='98' valign='top'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>สิ่งที่ส่งมาด้วย</div>" +
            "                                   </td>" +
            "                                   <td width='502'>" +
            "                                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาหนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!string.IsNullOrEmpty(pursuant) ? Util.NumberArabicToThai(pursuant) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(pursuantBookDate)) : "") + "</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามสัญญาที่อ้างถึง ท่านได้ทำสัญญาค้ำประกันผูกพันไว้ต่อมหาวิทยาลัยมหิดลว่า " +
            "ถ้า" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ต้องรับผิดชดใช้เงินตามสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])) + " " + 
            "แก่มหาวิทยาลัยแล้ว ท่านยินยอมชดใช้เงินตามจำนวนที่" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " " +
            "ต้องรับผิดจนครบถ้วน ความละเอียดทราบแล้ว นั้น" +  
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " ได้ปฏิบัติงานไม่ครบกำหนดตามสัญญาการเป็นนักศึกษาฯ " +
            "เป็นเหตุให้ต้องรับผิดชดใช้เงินให้แก่มหาวิทยาลัยมหิดล เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 18]) + ") " +
            "และมหาวิทยาลัยมหิดลได้มีหนังสือทวงถามให้" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " " +
            "ชดใช้เงินภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือ รายละเอียดปรากฏตามสิ่งที่ส่งมาด้วย ซึ่ง" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " " +
            "ได้รับหนังสือแล้วแต่กลับเพิกเฉยไม่ชำระเงินภายในกำหนด เป็นเหตุให้ท่านซึ่งเป็นผู้ค้ำประกันต้องรับผิดชดใช้เงินให้แก่มหาวิทยาลัยมหิดล " + 
            "ในการนี้ จึงขอให้ท่านนำเงินจำนวน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 18]) + ") " +
            "พร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(contractInterest[1]) + " ต่อปีของต้นเงินจำนวนดังกล่าว นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(overpaymentDateStart)) + " " +
            "ซึ่งเป็นวันผิดนัด จนถึงวันที่ท่านชำระเสร็จสิ้น มาชำระให้แก่มหาวิทยาลัยมหิดลโดยเร็ว " +
            "มิเช่นนั้นมหาวิทยาลัยมหิดลจำต้องดำเนินการตามกฎหมายต่อไป" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ขอให้ท่านติดต่อนิติกรผู้รับผิดชอบ: คุณ" + lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(lawyer["PhoneNumber"]) + ") " +
            "เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ให้แก่มหาวิทยาลัยมหิดล แล้วจึงชำระเงินจำนวนดังกล่าวโดยนำฝากเงินเข้าบัญชีธนาคารไทยพาณิชย์ " + 
            "ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ " +
            "หรือโอนเงินเข้าบัญชีธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี ๐๑๖-๒-๑๐๓๒๒-๓ " +
            "และจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร(Fax) " +
            "หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + lawyer["Email"] +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportNoticeClaimDebtSection(2, font, null) +
            "           </table>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportNoticeClaimDebtSection(3, font, lawyer) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>ขอให้ชดใช้เงินผิดสัญญาการเป็นนักศึกษาฯ ราย " + data[0, 3] + data[0, 4] + " " + data[0, 5] + " (ทวงถามครั้งที่ ๒)</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่มหาวิทยาลัยได้มีหนังสือที่ อว ๗๘/ " + (!string.IsNullOrEmpty(pursuant) ? Util.NumberArabicToThai(pursuant) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(pursuantBookDate)) : "") + " " +
            "ถึง" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " เพื่อขอให้ชดใช้เงินกรณีปฏิบัติงานไม่ครบกำหนดตามสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data[0, 13])) + " " +
            "เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data[0, 18]) + ") " +
            "ให้แก่มหาวิทยาลัย ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว นั้น" +  
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;งานกฎหมายและนิติกรรมสัญญา ขอเรียนว่า หนังสือดังกล่าวมีผู้รับไว้โดยชอบแล้วเมื่อวันที่ " + (!string.IsNullOrEmpty(replyDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(replyDate)) : "") + " " +
            "รายละเอียดปรากฏตามใบตอบรับไปรษณีย์ลงทะเบียนในประเทศ บัดนี้ได้ล่วงเลยกำหนดระยะเวลาการชำระเงินแล้ว ยังไม่ปรากฏว่า" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " " +
            "ได้ชำระเงินให้แก่มหาวิทยาลัยแต่อย่างใด ในการนี้ งานกฎหมายและนิติกรรมสัญญาจึงเห็นควรให้มหาวิทยาลัยมีหนังสือทวงถามถึง" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " (ครั้งที่ ๒) และ" + data[0, 14] + " ในฐานะผู้ค้ำประกัน " +
            "เพื่อดำเนินการชำระเงินจำนวนดังกล่าวพร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(contractInterest[1]) + " ต่อปีของต้นเงิน นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(overpaymentDateStart)) + " " +
            "ซึ่งเป็นวันผิดนัดจนถึงวันที่ชำระเสร็จสิ้น ให้แก่มหาวิทยาลัยมหิดลโดยเร็ว" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดพิจารณา หากเห็นชอบ " +
            "ขอได้โปรดลงนามในหนังสือถึง" + data[0, 3] + data[0, 4] + " " + data[0, 5] + " และ" + data[0, 14] + " ตามที่เสนอมาพร้อมนี้" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportNoticeClaimDebtSection(4, font, lawyer) +
            "           </table>" +
            "       </td>" +
            "   </tr>" +
            "</table>"
        );

        return html;
    }

    public static void ExportCPReportNoticeClaimDebt(string exportSend) {
        /*
        string html = string.Empty;
        */
        char[] separator = new char[] { ':' };
        string[] value = exportSend.Split(separator);
        string cp1id = value[0];        
        int time = int.Parse(value[1]);
        string previousRepayDateEnd = value[2];        
        string[,] data = eCPDB.ListDetailReportNoticeClaimDebt(cp1id);

        switch (time) {
            case 1:
                string template = value[3];

                ExportCPReportNoticeClaimDebtTime1(template, data);
                break;
            case 2:
                string html = string.Empty;
                string overpaymentDateStart = string.Empty;

                if (!string.IsNullOrEmpty(previousRepayDateEnd)) {
                    string[] repayDate = eCPUtil.RepayDate(previousRepayDateEnd);

                    overpaymentDateStart = repayDate[2];
                }

                html += (
                    ExportCPReportNoticeClaimDebtTime2(data, overpaymentDateStart) +
                    "<div class='filename hidden'>NoticeClaimDebtTime" + time.ToString() + ".doc</div>" +
                    "<div class='contenttype hidden'>application/msword</div>"
                );

                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Write(html);
                break;
        }
        /*
        html += (
            "<div class='filename hidden'>NoticeClaimDebtTime" + time.ToString() + ".doc</div>" +
            "<div class='contenttype hidden'>application/msword</div>"
        );

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=NoticeClaimDebtTime" + time.ToString() + ".doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";

        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);
        */
    }

    public static string ListCPReportNoticeClaimDebt(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountCPReportNoticeClaimDebt(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportNoticeClaimDebt(c);
            string highlight;
            string groupNum;
            string[] iconStatus;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                iconStatus = data[i, 11].Split(new char[] { ';' });
                callFunc = ("LoadForm(1,'repaycptransrequirecontract1',true,''," + data[i, 1] + ",'repay" + data[i, 2] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='repay" + data[i, 2] + "'>" +
                    "   <li id='table-content-cp-report-notice-claim-debt-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-claim-debt-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-claim-debt-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-claim-debt-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + data[i, 8] + groupNum +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-claim-debt-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-notice-claim-debt-col6' onclick=" + callFunc + ">" +
                    "       <div class='icon-status-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus[1] + "'></li>" +
                    "               <li class='" + iconStatus[2] + "'></li>" +
                    "               <li class='" + iconStatus[3] + "'></li>" +
                    "               <li class='" + iconStatus[4] + "'></li>" +
                    "          </ul>" +
                    "      </div>" +
                    "  </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);
            
            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportnoticeclaimdebt", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "  <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportNoticeClaimDebt() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-notice-claim-debt-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-notice-claim-debt") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'>" +
            "               <input type='hidden' id='search-report-notice-claim-debt' value=''>" +
            "               <input type='hidden' id='id-name-report-notice-claim-debt-hidden' value=''>" +
            "               <input type='hidden' id='faculty-report-notice-claim-debt-hidden' value=''>" +
            "               <input type='hidden' id='program-report-notice-claim-debt-hidden' value=''>" +
            "               <div class='button-style2'>" +
            "                   <ul>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportnoticeclaimdebt',true,'','','')>ค้นหา</a>" +
            "                       </li>" +
            "                   </ul>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-cp-report-notice-claim-debt'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box-search-condition' id='search-report-notice-claim-debt-condition'>" +
            "           <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "           <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order1'>" +
            "               <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order1-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order2'>" +
            "               <div class='box-search-condition-order-title'>คณะ</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order2-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order3'>" +
            "               <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order3-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-report-notice-claim-debt-col1'>" +
            "                   <div class='table-head-line1'>ลำดับ</div>" +
            "                   <div>ที่</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-claim-debt-col2'>" +
            "                   <div class='table-head-line1'>รหัส</div>" +
            "                   <div>นักศึกษา</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-claim-debt-col3'>" +
            "                   <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-claim-debt-col4'>" +
            "                   <div class='table-head-line1'>หลักสูตร</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-claim-debt-col5'>" +
            "                   <div class='table-head-line1'>ยอดเงินต้นที่ชดใช้</div>" +
            "                   <div>( บาท )</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-notice-claim-debt-col6'>" +
            "                   <div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            /*
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>" +           
            */
            "<div id='cp-report-notice-claim-debt-content'>" +
            "   <div class='box4' id='list-data-report-notice-claim-debt'></div>" +
            "   <div id='nav-page-report-notice-claim-debt'></div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportNoticeCheckForReimbursement {
    private static void ExportCPReportNoticeCheckForReimbursementV1(string cp1id) {
        string pdfFont = "Font/THSarabunBold.ttf";
        string template = string.Empty;
        string saveFile = "NoticeCheckForReimbursement.pdf";
        string[,] data = eCPDB.ListDetailCPTransBreakContract(cp1id);
        string caseGraduate = data[0, 33];
        string civil = data[0, 34];

        if (caseGraduate.Equals("1"))
            template = "ExportTemplate/NoticeCheckForReimbursement.NotGraduate.pdf";

        if (caseGraduate.Equals("2") &&
            civil.Equals("1"))
            template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.Work.Resign.pdf";

        if (caseGraduate.Equals("2") &&
            civil.Equals("2"))
            template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.NotWorking.pdf";

        ExportToPdf exportToPdf = new ExportToPdf();
        exportToPdf.ExportToPdfConnect(template, "pdf", saveFile);
        exportToPdf.FillForm(pdfFont, 11, 0, data[0, 18], 105, 809, 190, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 19], 309, 811, 138, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 20])), 470, 811, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 21], 154, 791, 45, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 22])), 221, 791, 77, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 23], 404, 791, 45, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 24])), 470, 791, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (data[0, 5] + data[0, 8] + " " + data[0, 9]), 63, 698, 158, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 2], 281, 698, 115, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, eCPDataReport.ReplaceProgramToShortProgram(data[0, 11]), 67, 678, 120, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, eCPDataReport.ReplaceFacultyToShortProgram(data[0, 14]), 210, 678, 114, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 25])), 471, 678, 82, 0);
        exportToPdf.FillForm(pdfFont, 11, 0, data[0, 26], 107, 656, 92, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (int.Parse(DateTime.Parse(eCPUtil.ConvertDateEN(data[0, 31])).ToString("yyyy")) + 543).ToString(), 313, 658, 48, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 32])), 460, 658, 94, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (civil.Equals("1") && !data[0, 36].Equals("0") ? (data[0, 36] + " ปี") : string.Empty), 150, 638, 132, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (double.Parse(data[0, 37]).ToString("#,##0.00") + " บาท"), 366, 638, 187, 0);
        exportToPdf.ExportToPdfDisconnect();
    }

    public static void ExportCPReportNoticeCheckForReimbursementV2(string cp1id) {
        string pdfFont = "Font/THSarabunBold.ttf";
        string template = string.Empty;
        string saveFile = "NoticeCheckForReimbursement.pdf";
        string[,] data = eCPDB.ListDetailCPTransRequireContract(cp1id);
        string scholar = data[0, 40];
        string scholarshipMoney = data[0, 41];
        string scholarshipYear = data[0, 42];
        string scholarshipMonth = data[0, 43];
        string allActualMonthScholarship = data[0, 8];
        string caseGraduate = data[0, 46];
        string educationDate = data[0, 44];
        string graduateDate = data[0, 45];
        string civil = data[0, 47];
        string dateStart = string.Empty;
        string dateEnd = string.Empty;
        string indemnitorYear = data[0, 49];
        string indemnitorCash = data[0, 50];
        string calDateCondition = data[0, 48];
        string studyLeave = data[0, 66];
        string beforeStudyLeaveStartDate = data[0, 67];
        string beforeStudyLeaveEndDate = data[0, 68];
        string studyLeaveStartDate = data[0, 69];
        string studyLeaveEndDate = data[0, 70];
        string afterStudyLeaveStartDate = data[0, 71];
        string afterStudyLeaveEndDate = data[0, 72];
        
        if (caseGraduate.Equals("1")) {
            template = "ExportTemplate/NoticeCheckForReimbursement.NotGraduate.pdf";
            dateStart = data[0, 62];
            dateEnd = data[0, 63];
        }

        if (caseGraduate.Equals("2") &&
            civil.Equals("1")) {
            template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.Work.Resign.pdf";
            dateStart = data[0, 6];
            dateEnd = data[0, 7];
        }

        if (caseGraduate.Equals("2") &&
            civil.Equals("2")) {
            template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.NotWorking.pdf";
            dateStart = data[0, 44];
            dateEnd = data[0, 45];
        }

        ExportToPdf exportToPdf = new ExportToPdf();
        exportToPdf.ExportToPdfConnect(template, "pdf", saveFile);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 31], 105, 811, 190, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 32], 309, 811, 138, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 33])), 470, 811, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 34], 154, 791, 45, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 35])), 221, 791, 77, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 36], 404, 791, 45, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 37])), 470, 791, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (data[0, 20] + data[0, 21] + " " + data[0, 22]), 63, 698, 158, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 19], 281, 698, 115, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, eCPDataReport.ReplaceProgramToShortProgram(data[0, 24]), 67, 678, 120, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, eCPDataReport.ReplaceFacultyToShortProgram(data[0, 27]), 210, 678, 114, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 38])), 471, 678, 82, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 39], 107, 658, 92, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (int.Parse(DateTime.Parse(eCPUtil.ConvertDateEN(data[0, 44])).ToString("yyyy")) + 543).ToString(), 313, 658, 48, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 45])), 460, 658, 94, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (civil.Equals("1") && !data[0, 49].Equals("0") ? (data[0, 49] + " ปี") : string.Empty), 150, 638, 132, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (double.Parse(data[0, 50]).ToString("#,##0.00") + " บาท"), 366, 638, 187, 0);

        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 44])), 261, 479, 87, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, Util.ThaiLongDate(eCPUtil.ConvertDateEN(data[0, 45])), 461, 479, 91, 0);
        exportToPdf.FillForm(pdfFont, 13, 1, ((!string.IsNullOrEmpty(data[0, 11]) && !data[0, 11].Equals("0") ? (data[0, 11] + " เดือน") : string.Empty) + " " + (!string.IsNullOrEmpty(data[0, 12]) && !data[0, 12].Equals("0") ? data[0, 12] : string.Empty)), 203, 459, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 1, (!string.IsNullOrEmpty(data[0, 13]) ? double.Parse(data[0, 13]).ToString("#,##0") : string.Empty), 458, 459, 80, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 3], 209, 438, 344, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, data[0, 5], 100, 418, 105, 0);

        if (data[0, 66].Equals("N")) {
            exportToPdf.FillForm(pdfFont, 13, 0, Util.LongDateTH(data[0, 6]), 248, 418, 125, 0);
            exportToPdf.FillForm(pdfFont, 13, 0, Util.LongDateTH(data[0, 7]), 408, 418, 145, 0);
        }

        if (data[0, 66].Equals("Y")) {
            exportToPdf.FillForm(pdfFont, 13, 0, Util.LongDateTH(data[0, 67]), 248, 418, 125, 0);
            exportToPdf.FillForm(pdfFont, 13, 0, Util.LongDateTH(data[0, 68]), 408, 418, 145, 0);
        }
        
        exportToPdf.FillForm(pdfFont, 13, 1, (!string.IsNullOrEmpty(data[0, 14]) && !data[0, 14].Equals("0") ? double.Parse(data[0, 14]).ToString("#,##0") : string.Empty), 212, 398, 58, 0);
        exportToPdf.FillForm(pdfFont, 13, 1, (!string.IsNullOrEmpty(data[0, 15]) ? double.Parse(data[0, 15]).ToString("#,##0") : string.Empty), 465, 398, 73, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (data[0, 20] + data[0, 21] + " " + data[0, 22]), 258, 379, 296, 0);

        double[] resultPayScholarship = eCPUtil.CalPayScholarship(scholar, caseGraduate, civil, scholarshipMoney, scholarshipYear, scholarshipMonth, allActualMonthScholarship);
        double[] resultPenalty = eCPUtil.GetCalPenalty(studyLeave, beforeStudyLeaveStartDate, beforeStudyLeaveEndDate, afterStudyLeaveStartDate, afterStudyLeaveEndDate, scholar, caseGraduate, educationDate, graduateDate, civil, resultPayScholarship[1].ToString(), scholarshipYear, scholarshipMonth, dateStart, dateEnd, indemnitorYear, indemnitorCash, calDateCondition);
        double iCash;
        double allActual;
        double actual;
        double actualMonth;
        double educationActual;
        double educationMonth;
        double educationDay;
        int dayLastMonth;
        int formular = int.Parse(calDateCondition);
        string[] penaltyFormularString;

        if (formular.Equals(1)) {
            iCash = resultPenalty[8];
            actualMonth = resultPenalty[9];
            penaltyFormularString = eCPUtil.PenaltyFormular1ToString(iCash, actualMonth).Split(';');

            exportToPdf.FillForm(pdfFont, 13, 1, penaltyFormularString[0], 139, 359, 186, 0);
        }

        if (formular.Equals(2)) {
            iCash = resultPenalty[8];
            educationMonth = resultPenalty[10];
            educationDay = resultPenalty[11];
            dayLastMonth = int.Parse(resultPenalty[12].ToString());
            penaltyFormularString = eCPUtil.PenaltyFormular2ToString(iCash, educationMonth, educationDay, dayLastMonth).Split(';');

            exportToPdf.FillForm(pdfFont, 12, 1, penaltyFormularString[0], 139, 367, 98, 0);
            exportToPdf.FillForm(pdfFont, 12, 1, penaltyFormularString[1], 237, 367, 87, 0);
            exportToPdf.CreateTable(237, 351, 87, 1, 0, 1, 0, 0);
            exportToPdf.FillForm(pdfFont, 12, 1, penaltyFormularString[2], 237, 357, 87, 0);
        }

        if (formular.Equals(3)) {
            iCash = resultPenalty[8];
            allActual = resultPenalty[2];
            actual = resultPenalty[13];
            penaltyFormularString = eCPUtil.PenaltyFormular3ToString(iCash, allActual, actual).Split(';');

            exportToPdf.FillForm(pdfFont, 13, 1, penaltyFormularString[0], 139, 359, 186, 0);
            exportToPdf.FillForm(pdfFont, 13, 1, penaltyFormularString[1], 139, 347, 186, 0);
        }

        if (formular.Equals(4)) {
            iCash = resultPenalty[8];
            educationActual = resultPenalty[14];
            actual = resultPenalty[3];
            penaltyFormularString = eCPUtil.PenaltyFormular4ToString(iCash, educationActual, actual).Split(';');

            exportToPdf.FillForm(pdfFont, 13, 1, penaltyFormularString[0], 139, 359, 186, 0);
            exportToPdf.FillForm(pdfFont, 13, 1, penaltyFormularString[1], 139, 347, 186, 0);
        }

        exportToPdf.FillForm(pdfFont, 13, 1, (!string.IsNullOrEmpty(data[0, 16]) ? double.Parse(data[0, 16]).ToString("#,##0.00") : string.Empty), 335, 359, 195, 0);
        exportToPdf.FillForm(pdfFont, 15, 1, Util.ThaiBaht(data[0, 16]), 78, 320, 465, 0);
        exportToPdf.FillForm(pdfFont, 13, 0, (data[0, 20] + data[0, 21] + " " + data[0, 22]), 144, 298, 275, 0);
        exportToPdf.FillForm(pdfFont, 13, 1, data[0, 73], 322, 138, 136, 0);
        exportToPdf.ExportToPdfDisconnect();
    }
    
    public static void ExportCPReportNoticeCheckForReimbursement(string exportSend) {
        char[] separator = new char[] { ':' };
        string[] cp1idAction = exportSend.Split(separator);
        string cp1id = cp1idAction[0];
        string action = cp1idAction[1];
        
        switch (action) {
            case "v1":
                ExportCPReportNoticeCheckForReimbursementV1(cp1id);
                break;
            case "v2":
                ExportCPReportNoticeCheckForReimbursementV2(cp1id);
                break;
        }
    }
}

public class eCPDataReportFormRequestCreateAndUpdateDebtor {
    public static void ExportCPReportFormRequestCreateAndUpdateDebtor(string exportSend) { 
        /*
        string pdfFont = "Font/THSarabunBold.ttf";
        string template = "ExportTemplate/FormRequestCreateAndUpdateDebtor.pdf";
        string saveFile = "FormRequestCreateAndUpdateDebtor.pdf";
        */
        string fileName = "FormRequestCreateAndUpdateDebtor";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string cp1id = exportSendValue[0];
        string[,] data1 = eCPDB.ListDetailCPTransRequireContract(cp1id);
        string currentDate = (Util.CurrentDate("dd") + " " + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " " + (int.Parse(Util.CurrentDate("yyyy")) + 543).ToString());
        string studentCode = string.Empty;
        string idCard = string.Empty;
        string titleName = string.Empty;
        string firstName = string.Empty;
        string lastName = string.Empty;
        string subdistrict = string.Empty;
        string district = string.Empty;
        string province = string.Empty;
        string zipCode = string.Empty;
        string lawyerFullname = string.Empty;
        string lawyerPhoneNumber = string.Empty;
        string lawyerMobileNumber = string.Empty;
        ArrayList addressList = new ArrayList();
        ArrayList roadSoiList = new ArrayList();
        ArrayList phoneNumberList = new ArrayList();
        ArrayList lawyerPhoneNumberList = new ArrayList();

        if (data1.GetLength(0) > 0) {
            studentCode = data1[0, 19];
            titleName = data1[0, 20];
            firstName = data1[0, 21];
            lastName = data1[0, 22];
            lawyerFullname = data1[0, 73];
            lawyerPhoneNumber = data1[0, 74];
            lawyerMobileNumber = data1[0, 75];

            if (!string.IsNullOrEmpty(lawyerPhoneNumber))
                lawyerPhoneNumberList.Add(lawyerPhoneNumber);

            if (!string.IsNullOrEmpty(lawyerMobileNumber))
                lawyerPhoneNumberList.Add(lawyerMobileNumber);

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(eCPDB.GetPersonRecordsAddress(studentCode));
            JArray data2 = jsonObject;

            if (data2.Count > 0) {
                JObject dr = jsonObject[0];
                dynamic addressTypePermanent = dr["addressTypePermanent"];

                string village = addressTypePermanent["village"];
                string no = addressTypePermanent["no"];
                string moo = addressTypePermanent["moo"];
                string road = addressTypePermanent["road"];
                string soi = addressTypePermanent["soi"];
                string phoneNumber = addressTypePermanent["phoneNumber"];
                string mobileNumber = addressTypePermanent["mobileNumber"];

                idCard = dr["idCard"].ToString();

                if (!string.IsNullOrEmpty(village))
                    addressList.Add("หมู่บ้าน" + village);

                if (!string.IsNullOrEmpty(no))
                    addressList.Add("บ้านเลขที่ " + no);

                if (!string.IsNullOrEmpty(moo))
                    addressList.Add("หมู่ที่ " + moo);

                if (!string.IsNullOrEmpty(road))
                    roadSoiList.Add(road);

                if (!string.IsNullOrEmpty(soi))
                    roadSoiList.Add("ซ." + soi);

                subdistrict = addressTypePermanent["subdistrict"];
                district = addressTypePermanent["district"];
                province = addressTypePermanent["province"];
                zipCode = addressTypePermanent["zipCode"];

                if (!string.IsNullOrEmpty(phoneNumber))
                    phoneNumberList.Add(phoneNumber);

                if (!string.IsNullOrEmpty(mobileNumber))
                    phoneNumberList.Add(mobileNumber);
            }
        }

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + (fileName + studentCode + ".xlsx"));
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;

        using (ExcelPackage xls = new ExcelPackage(new FileInfo(HttpContext.Current.Server.MapPath(@"ExportTemplate/" + fileName + ".xlsx")))) {
            ExcelWorksheet ws = xls.Workbook.Worksheets[0];
            MemoryStream ms = new MemoryStream();

            ws.Cells["H4"].Value = currentDate;
            ws.Cells["B21"].Value = (titleName + firstName + " " + lastName);
            ws.Cells["B29"].Value = string.Join(" ", addressList.ToArray());
            ws.Cells["B31"].Value = string.Join(" ", roadSoiList.ToArray());
            ws.Cells["B33"].Value = subdistrict;
            ws.Cells["B35"].Value = district;
            ws.Cells["B37"].Value = province;
            ws.Cells["I37"].Value = zipCode;
            ws.Cells["B39"].Value = string.Join(", ", phoneNumberList.ToArray());
            ws.Cells["G44"].Value = idCard;
            ws.Cells["B69"].Value = ("(" + lawyerFullname + ")");
            ws.Cells["B70"].Value = "นิติกร";
            ws.Cells["B71"].Value = (": " + currentDate);
            ws.Cells["B72"].Value = (": " + string.Join(", ", lawyerPhoneNumberList.ToArray()));
            ws.Cells["H71"].Value = (": " + currentDate);

            xls.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);

            ms.Close();
            ms.Dispose();
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        /*
        Export2PDF export2PDF = new Export2PDF();
        export2PDF.ExportToPDFConnect(saveFile);
        export2PDF.PDFConnectTemplate(template, "pdf");

        export2PDF.PDFAddTemplate("pdf", 1, 1);
        export2PDF.FillForm(pdfFont, 12, 0, currentDate, 360, 716, 154, 0);
        export2PDF.FillForm(pdfFont, 12, 0, (titleName + firstName + " " + lastName), 91, 464, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, string.Join(" ", addressList.ToArray()), 91, 357, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, string.Join(" ", roadSoiList.ToArray()), 91, 329, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, subdistrict, 91, 300, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, district, 91, 271, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, province, 91, 243, 260, 0);
        export2PDF.FillForm(pdfFont, 12, 0, zipCode, 400, 243, 180, 0);
        export2PDF.FillForm(pdfFont, 12, 0, string.Join(", ", phoneNumberList.ToArray()), 91, 188, 489, 0);
        export2PDF.FillForm(pdfFont, 12, 0, idCard, 317, 97, 263, 0);

        export2PDF.PDFAddTemplate("pdf", 2, 1);
        export2PDF.FillForm(pdfFont, 12, 1, ("(" + lawyerFullname + ")"), 96, 433, 127, 0);
        export2PDF.FillForm(pdfFont, 12, 1, "นิติกร", 96, 419, 127, 0);
        export2PDF.FillForm(pdfFont, 12, 0, currentDate, 94, 404, 223, 0);
        export2PDF.FillForm(pdfFont, 12, 0, string.Join(", ", lawyerPhoneNumberList.ToArray()), 94, 388, 223, 0);
        export2PDF.FillForm(pdfFont, 12, 0, currentDate, 362, 404, 223, 0);
        export2PDF.ExportToPdfDisconnect();
        */
    }
}

public class eCPDataReportStatisticPaymentByDate {
    public static string ViewTransPaymentByDate(string cp2idDate) {
        string html = string.Empty;
        char[] separator = new char[] { ':' };
        string[] cp2idDate1 = cp2idDate.Split(separator);
        string cp2id = cp2idDate1[0];
        string dateStart = cp2idDate1[1];
        string dateEnd = cp2idDate1[2];
        string[,] data = eCPDB.ListDetailPaymentOnCPTransRequireContract(cp2id);
        
        if (data.GetLength(0) > 0) {
            string statusPayment = data[0, 7];
            string formatPayment = data[0, 8];
            string studentIDDefault = data[0, 9];
            string titleNameDefault = data[0, 10];
            string firstNameDefault = data[0, 11];
            string lastNameDefault = data[0, 12];
            string facultyCodeDefault = data[0, 16];
            string facultyNameDefault = data[0, 17];
            string programCodeDefault = data[0, 13];
            string programNameDefault = data[0, 14];
            string groupNumDefault = data[0, 18];
            string dlevelDefault = data[0, 20];
            string pictureFileNameDefault = data[0, 21];
            string pictureFolderNameDefault = data[0, 22];
            string lawyerFullnameDefault = data[0, 29];
            string lawyerPhoneNumberDefault = data[0, 30];
            string lawyerMobileNumberDefault = data[0, 31];
            string lawyerEmailDefault = data[0, 32];
            string lawyerDefault = string.Empty;

            ArrayList lawyerPhoneNumber = new ArrayList();

            if (!string.IsNullOrEmpty(lawyerPhoneNumberDefault))
                lawyerPhoneNumber.Add(lawyerPhoneNumberDefault);

            if (!string.IsNullOrEmpty(lawyerMobileNumberDefault))
                lawyerPhoneNumber.Add(lawyerMobileNumberDefault);

            if (!string.IsNullOrEmpty(lawyerFullnameDefault) &&
                (!string.IsNullOrEmpty(lawyerPhoneNumberDefault) || !string.IsNullOrEmpty(lawyerMobileNumberDefault) && !string.IsNullOrEmpty(lawyerEmailDefault))) {
                lawyerDefault += (
                    "คุณ<span>" + lawyerFullnameDefault + "</span>" + (lawyerPhoneNumber.Count > 0 ? (" ( <span>" + string.Join(", ", lawyerPhoneNumber.ToArray()) + "</span> )") : string.Empty) +
                    " อีเมล์ <span>" + lawyerEmailDefault + "</span>"
                );
            }

            string[,] data1 = eCPDB.ListTransPayment(cp2id, dateStart, dateEnd);
            int recordCount = data1.GetLength(0);
            
            html += (
                "<div class='form-content' id='view-trans-payment-by-date-head'>" +
                "   <input type='hidden' id='period-hidden' value=''>" +
                "   <div id='profile-student'>" +
                "       <div class='content-left' id='picture-student'>" +
                "           <div>" +
                "               <img src='" + (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + pictureFolderNameDefault + "/" + pictureFileNameDefault) + "' />" +
                "           </div>" +
                "       </div>" +
                "       <div class='content-left' id='profile-student-label'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>รหัสนักศึกษา</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>ชื่อ - นามสกุล</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>ระดับการศึกษา</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>คณะ</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>หลักสูตร</div>" +
                "           </div>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>นิติกรผู้รับผิดชอบ</div>" +
                "           </div>" +
                "      </div>" +
                "      <div class='content-left' id='profile-student-input'>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + studentIDDefault + "&nbsp;" + programCodeDefault.Substring(0, 4) + " / " + programCodeDefault.Substring(4, 1) + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + titleNameDefault + firstNameDefault + " " + lastNameDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + dlevelDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + facultyCodeDefault + " - " + facultyNameDefault + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style'>" +
                "               <div class='form-label-style'>" +
                "                   <span>" + programCodeDefault + " - " + programNameDefault + (!groupNumDefault.Equals("0") ? (" ( กลุ่ม " + groupNumDefault + " )") : "") + "</span>" +
                "               </div>" +
                "           </div>" +
                "           <div class='form-label-discription-style clear-bottom'>" +
                "               <div class='form-label-style'>" + lawyerDefault + "</div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "</div>" +
                "<div id='view-trans-payment-by-date-content'>" +
                "   <div id='view-trans-payment-by-date'>" +
                "       <div class='tab-line'></div>" +
                "       <div class='content-data-tab-content'>" +
                "           <div class='content-left'>" +
                "               <div class='content-data-tab-content-msg'>ช่วงวันที่ชำระหนี้ " + (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd) ? ("ระหว่างวันที่ " + dateStart + " ถึงวันที่ " + dateEnd) : "ไม่กำหนด") + "</div>" +
                "           </div>" +
                "           <div class='content-right'>" +
                "               <div class='content-data-tab-content-msg'>ค้นหาพบ " + recordCount + " รายการ</div>" +
                "           </div>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "       <div class='tab-line'></div>" +
                "       <div class='box3'>" +
                "           <div class='table-head'>" +
                "               <ul>" +
                "                   <li id='table-head-trans-payment-col1'>" +
                "                       <div class='table-head-line1'>งวดที่</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col2'>" +
                "                       <div class='table-head-line1'>เงินต้น</div>" +
                "                       <div>&nbsp;</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col3'>" +
                "                       <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                       <div>&nbsp;</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col4'>" +
                "                       <div class='table-head-line1'>เงินต้น</div>" +
                "                       <div>รับชำระ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col5'>" +
                "                       <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                       <div>รับชำระ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col6'>" +
                "                       <div class='table-head-line1'>ยอดเงิน</div>" +
                "                       <div>รับชำระ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col7'>" +
                "                       <div class='table-head-line1'>เงินต้น</div>" +
                "                       <div>คงเหลือ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col8'>" +
                "                       <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                       <div>คงเหลือ</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col9'>" +
                "                       <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                       <div>ค้างจ่าย</div>" +
                "                       <div>( บาท )</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col10'>" +
                "                       <div class='table-head-line1'>วันเดือนปี</div>" +
                "                       <div>ที่รับชำระหนี้</div>" +
                "                   </li>" +
                "                   <li class='table-col' id='table-head-trans-payment-col11'>" +
                "                       <div class='table-head-line1'>จ่ายชำระด้วย</div>" +
                "                   </li>" +
                "               </ul>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>" +
                "       <div id='box-list-trans-payment-by-date'>" +
                "           <div id='list-trans-payment-by-date'>" + eCPDataPayment.ListTransPayment(data1) + "</div>" +
                "       </div>" +
                "   </div>" +
                "</div>"
            );
        }

        return html;
    }
    
    public static string ListCPReportStatisticPaymentByDate(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;       
        int recordCount = eCPDB.CountCPReportStatisticPaymentByDate(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListReportStatisticPaymentByDate(c);
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : "");
                callFunc = ("LoadForm(1,'viewtranspaymentbydate',true,'','" + (data[i, 2] + ":" + c.Request["datestart"] + ":" + c.Request["dateend"]) + "','report-statistic-payment-by-date" + data[i, 0] + "')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-statistic-payment-by-date" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-report-statistic-payment-by-date-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 7] + "</span>- " + (data[i, 8] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col6' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 11]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col7' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 13]) ? data[i, 13] : "-") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);
            
            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportstatisticpaymentbydate", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportStatisticPaymentByDate() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-statistic-payment-by-date-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-statistic-payment-by-date") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'>" +
            "               <input type='hidden' id='search-report-statistic-payment-by-date' value=''>" +
            "               <input type='hidden' id='id-name-report-statistic-payment-by-date-hidden' value=''>" +
            "               <input type='hidden' id='faculty-report-statistic-payment-by-date-hidden' value=''>" +
            "               <input type='hidden' id='program-report-statistic-payment-by-date-hidden' value=''>" +
            "               <input type='hidden' id='format-payment-report-statistic-payment-by-date-hidden' value=''>" +
            "               <input type='hidden' id='date-start-report-statistic-payment-by-date-hidden' value=''>" +
            "               <input type='hidden' id='date-end-report-statistic-payment-by-date-hidden' value=''>" +
            "               <div class='button-style2'>" +
            "                   <ul>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportpaymentbydate',true,'','','')>ค้นหา</a>" +
            "                       </li>" +
            "                   </ul>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-payment-by-date'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box-search-condition' id='search-report-statistic-payment-by-date-condition'>" +
            "           <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "           <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order1'>" +
            "               <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order1-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order2'>" +
            "               <div class='box-search-condition-order-title'>คณะ</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order2-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order3'>" +
            "               <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order3-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order4'>" +
            "               <div class='box-search-condition-order-title'>รูปแบบการชำะหนี้</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order4-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order5'>" +
            "               <div class='box-search-condition-order-title'>ช่วงวันที่ชำระหนี้</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order5-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-report-statistic-payment-by-date-col1'>" +
            "                   <div class='table-head-line1'>ลำดับ</div>" +
            "                   <div>ที่</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col2'>" +
            "                   <div class='table-head-line1'>รหัส</div>" +
            "                   <div>นักศึกษา</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col3'>" +
            "                   <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col4'>" +
            "                   <div class='table-head-line1'>หลักสูตร</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col5'>" +
            "                   <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                   <div>ที่ต้องชดใช้</div>" +
            "                   <div>( บาท )</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col6'>" +
            "                   <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                   <div>ที่รับชำระ</div>" +
            "                   <div>( บาท )</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col7'>" +
            "                   <div class='table-head-line1'>รูปแบบ</div>" +
            "                   <div>การชำระหนี้</div>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-statistic-payment-by-date-content'>" +
            "   <div class='box4' id='list-data-report-statistic-payment-by-date'></div>" +
            "   <div id='nav-page-report-statistic-payment-by-date'></div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportEContract {    
    public static string ListCPReportEContract(HttpContext c) {
        string html = string.Empty;        
        string pageHtml = string.Empty;        
        int recordCount = eCPDB.CountCPReportEContract(c);
    
        if (recordCount > 0) {
            string[,]  data = eCPDB.ListCPReportEContract(c);
            string highlight;
            string groupNum;
            string iconStatus1;
            string iconStatus2;
            string iconStatus3;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 7].Equals("0") ? (" ( กลุ่ม " + data[i, 7] + " )") : "");
                iconStatus1 = eCPUtil.iconEContractStatus[(Util.FindIndexArray2D(0, eCPUtil.iconEContractStatus, data[i, 8]) - 1), 1];
                iconStatus2 = eCPUtil.iconEContractStatus[(Util.FindIndexArray2D(0, eCPUtil.iconEContractStatus, data[i, 9]) - 1), 1];
                iconStatus3 = eCPUtil.iconEContractStatus[(Util.FindIndexArray2D(0, eCPUtil.iconEContractStatus, data[i, 10]) - 1), 1];

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-e-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-e-contract-col1' onclick>" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col2' onclick>" +
                    "       <div>" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col3' onclick>" +
                    "       <div>" + (data[i, 2] + data[i, 3] + " " + data[i, 4]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col4' onclick>" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 5] + "</span>- " + (data[i, 6] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col5' onclick>" +
                    "       <div class='icon-status1-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus1 + "'>" +
                                        (data[i, 8].Equals("1") ? ("<a href='javascript:void(0)' onclick=ShowDocEContract('" + data[i, 1] + "','" + data[i, 11] + "','" + data[i, 12] + "')></a>") : "") +
                    "               </li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col6' onclick>" +
                    "       <div class='icon-status2-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus2 + "'>" +
                                        (data[i, 9].Equals("1") ? ("<a href='javascript:void(0)' onclick=ShowDocEContract('" + data[i, 1] + "','" + data[i, 11] + "','" + data[i, 13] + "')></a>") : "") +
                    "               </li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-e-contract-col7' onclick>" +
                    "       <div class='icon-status3-style'>" +
                    "           <ul>" +
                    "               <li class='" + iconStatus3 + "'>" +
                                        (data[i, 10].Equals("1") ? ("<a href='javascript:void(0)' onclick=ShowDocEContract('" + data[i, 1] + "','" + data[i, 11] + "','" + data[i, 14] + "')></a>") : "") +
                    "               </li>" +
                    "           </ul>" +
                    "       </div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportecontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }
        
        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportEContract() {
        string html = string.Empty;

        html += (
            "<div id='cp-report-e-contract-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-report-e-contract") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-left'>" +
            "               <input type='hidden' id='search-report-e-contract' value=''>" +
            "               <input type='hidden' id='acadamicyear-report-e-contract-hidden' value='" + int.Parse(DateTime.Parse(Util.CurrentDate("MM/dd/yyyy")).ToString("yyyy", new CultureInfo("th-TH"))) + "'>" +
            "               <input type='hidden' id='id-name-report-e-contract-hidden' value=''>" +
            "               <input type='hidden' id='faculty-report-e-contract-hidden' value=''>" +
            "               <input type='hidden' id='program-report-e-contract-hidden' value=''>" +
            "               <div class='button-style2'>" +
            "                   <ul>" +
            "                       <li>" +
            "                           <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportecontract',true,'','','')>ค้นหา</a>" +
            "                       </li>" +
            "                   </ul>" +
            "               </div>" +
            "           </div>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-cp-report-e-contract'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box-search-condition' id='search-report-e-contract-condition'>" +
            "           <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "           <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order1'>" +
            "               <div class='box-search-condition-order-title'>ปีการศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order1-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order2'>" +
            "               <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order2-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order3'>" +
            "               <div class='box-search-condition-order-title'>คณะ</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order3-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order4'>" +
            "               <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "               <div class='box-search-condition-split-title-value'>:</div>" +
            "               <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order4-value'></div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-report-e-contract-col1'>" +
            "                   <div class='table-head-line1'>ลำดับ</div>" +
            "                   <div>ที่</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col2'>" +
            "                   <div class='table-head-line1'>รหัส</div>" +
            "                   <div>นักศึกษา</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col3'>" +
            "                   <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col4'>" +
            "                   <div class='table-head-line1'>หลักสูตร</div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col5'>" +
            "                   <div class='table-head-line1'>สัญญานักศึกษา</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col6'>" +
            "                   <div class='table-head-line1'>หนังสือยินยอม ฯ</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "               <li class='table-col' id='table-head-cp-report-e-contract-col7'>" +
            "                   <div class='table-head-line1'>สัญญาค้ำประกัน</div>" +
            "                   <div>" +
            "                       <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>ความหมาย</a>" +
            "                   </div>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-e-contract-content'>" +
            "   <div class='box4' id='list-data-report-e-contract'></div>" +
            "   <div id='nav-page-report-e-contract'></div>" +
            "</div>"
        );

        return html;
    }
}

public class eCPDataReportDebtorContract {
    private static string ExportCPReportDebtorContractSearchCondition(
        string exportSend,
        string reportOrder
    ) {
        string html = string.Empty;
        string font = "Cordia New";
        string fontSize = "13";
        int colSpan = 0;
        char[] separator;

        separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];
        string idName = exportSendValue[2];

        separator = new char[] { ';' };
        string[] faculty = (!string.IsNullOrEmpty(exportSendValue[3]) ? exportSendValue[3].Split(separator) : new string[0]);
        string[] program = (!string.IsNullOrEmpty(exportSendValue[4]) ? exportSendValue[4].Split(separator) : new string[0]);
        string[] formatPayment = (!string.IsNullOrEmpty(exportSendValue[5]) ? exportSendValue[5].Split(separator) : new string[0]);

        switch (reportOrder) {
            case "reportdebtorcontract":
                colSpan = 18;
                break;
            case "reportdebtorcontractpaid":
                colSpan = 19;
                break;
            case "reportdebtorcontractremain":
                colSpan = 21;
                break; 
        }

        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>"
        );

        if (!string.IsNullOrEmpty(idName) ||
            faculty.GetLength(0) > 0 ||
            program.GetLength(0) > 0) {
            html += (
                "<tr>" +
                "   <td align='left' colspan='" + colSpan + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>ค้นหาตามเงื่อนไข</div>" +
                "   </td>" +
                "</tr>"
            );
            
            if (!string.IsNullOrEmpty(idName)) {
                html += (
                    "<tr>" +
                    "   <td align='left' colspan='2'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                    "   </td>" +
                    "   <td align='left' colspan='" + (colSpan - 2) + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>: " + idName + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }

            if (faculty.GetLength(0) > 0) {
                html += (
                    "<tr>" +
                    "   <td align='left' colspan='2'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>คณะ</div>" +
                    "   </td>" +
                    "   <td align='left' colspan='" + (colSpan - 2) + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>: " + faculty[0] + " - " + faculty[1] + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }

            if (program.GetLength(0) > 0) {
                html += (
                    "<tr>" +
                    "   <td align='left' colspan='2'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>หลักสูตร</div>" +
                    "   </td>" +
                    "   <td align='left' colspan='" + (colSpan - 2) + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>: " + program[0] + " - " + program[1] + (!program[3].Equals("0") ? (" ( กลุ่ม " + program[3] + " )") : "") + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }

            if (formatPayment.GetLength(0) > 0) {
                html += (
                    "<tr>" +
                    "   <td align='left' colspan='2'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>รูปแบบการชำระหนี้</div>" +
                    "   </td>" +
                    "   <td align='left' colspan='" + (colSpan - 2) + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>: " + formatPayment[1] + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }

            html += (
                "<tr>" +
                "   <td align='center' colspan='" + colSpan + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>"
            );
        }

        html += (
            "   <tr>" +
            "       <td align='right' colspan='" + colSpan + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รายงาน ณ วันที่ " + Util.ConvertDateTH(Util.CurrentDate("MM/dd/yyyy")) + " เวลา " + Util.ConvertTimeTH((DateTime.Now).ToString()) + "</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>"
        );

        return html;
    }

    public static void ExportCPReportDebtorContractRemain(string exportSend) {
        string html = string.Empty;
        string font = "Cordia New";
        string fontSize = "13";
        string borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        string borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle3 = "border-left:thin solid #000000;";
        string borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle7 = "border-top:thin solid #000000;";
        string borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle9 = "border:thin solid #000000;";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];

        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='21'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รายงานลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดลคงค้าง</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='21'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(dateStart) + " ถึงวันที่ " + Util.LongDateTH(dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='21'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            ExportCPReportDebtorContractSearchCondition(exportSend, "reportdebtorcontractremain") +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลำดับที่</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ชื่อ - สกุล</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>หลักสูตร</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงื่อนไขกำหนดชดใช้ตามสัญญา</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลาปฏิบัติงานชดใช้</div>" +
            "       </td>" +
            "       <td align='center' colspan='4' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คงเหลือระยะเวลาปฏิบัติงานตามสัญญา</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คิดเป็นเงินที่ต้องชดใช้จำนวน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>วันเดือนปีรับสภาพหนี้</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินที่ต้องชดใช้คงเหลือ</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle2 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รูปแบบการชำระหนี้</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลา</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลา</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>จำนวนเงิน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เริ่มต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>สิ้นสุด</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คำนวณวัน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เริ่มต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>สิ้นสุด</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คำนวณวัน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คงเหลือ</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวมจำนวนเงิน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( ตามที่ได้รับหนังสือ )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวม</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle4 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( ปี )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle6 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>"
        );

        string[,] data = eCPDB.ListExportReportDebtorContractRemain(exportSend);            

        if (data.GetLength(0) > 0) {
            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            string order = string.Empty;
            string fullName = string.Empty;
            string program = string.Empty;
            string allActualDate = string.Empty;
            string workDateStart = string.Empty;
            string workDateEnd = string.Empty;
            string requireDate = string.Empty;
            string approveDate = string.Empty;
            string actualDate = string.Empty;
            string remainDate = string.Empty;
            int i;
            int j = 1;

            for (i = 0; i < (data.GetLength(0) - 1); i++) {
                order = (j++).ToString("#,##0");
                fullName = (data[i, 1] + data[i, 2] + " " + data[i, 3]);
                program = (data[i, 6] + " - " + data[i, 7] + (!data[i, 9].Equals("0") ? ("( กลุ่ม " + data[i, 9] + " )") : ""));
                allActualDate = (data[i, 12].Equals("1") ? data[i, 14] : string.Empty);

                if (data[i, 12].Equals("1")) {
                    allActualDate = data[i, 14];
                    workDateStart = data[i, 16];
                    workDateEnd = (!string.IsNullOrEmpty(workDateStart) ? DateTime.Parse(workDateStart, provider).AddDays(double.Parse(allActualDate)).ToString() : string.Empty);
                    requireDate = data[i, 16];
                    approveDate = data[i, 17];
                    actualDate = data[i, 18];
                    remainDate = data[i, 19];
                }
                else {
                    allActualDate = string.Empty;
                    workDateStart = string.Empty;
                    workDateEnd = string.Empty;
                    requireDate = string.Empty;
                    approveDate = string.Empty;
                    actualDate = string.Empty;
                    remainDate = string.Empty;
                }

                html += (
                    "<tr>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + order + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + fullName + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + program + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 13]) ? double.Parse(data[i, 13]).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(allActualDate) ? double.Parse(allActualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 15]) ? double.Parse(data[i, 15]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(workDateStart) ? Util.ConvertDateTH(DateTime.Parse(workDateStart, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(workDateEnd) ? Util.ConvertDateTH(workDateEnd) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(allActualDate) ? double.Parse(allActualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(requireDate) ? Util.ConvertDateTH(DateTime.Parse(requireDate, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(approveDate) ? Util.ConvertDateTH(DateTime.Parse(approveDate, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(actualDate) ? double.Parse(actualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(remainDate) ? double.Parse(remainDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 20]) ? double.Parse(data[i, 20]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>0</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 20]) ? double.Parse(data[i, 20]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 21]) ? Util.ConvertDateTH(DateTime.Parse(data[i, 21], provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 22]) ? double.Parse(data[i, 22]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 24]) ? double.Parse(data[i, 24]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 25]) ? double.Parse(data[i, 25]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle4 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 27]) ? data[i, 27] : "-") + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }
        
            html += (
                "<tr>" +
                "   <td align='right' colspan='13' style='" + borderStyle7 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>รวม</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 32]) ? double.Parse(data[i, 32]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>0</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle9 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 32]) ? double.Parse(data[i, 32]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='center' style='" + borderStyle7 + "'></td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 33]) ? double.Parse(data[i, 33]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 35]) ? double.Parse(data[i, 35]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle9 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 36]) ? double.Parse(data[i, 36]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='center' style='" + borderStyle7 + "' style='border-right:0px;border-bottom:0px'></td>" +
                "</tr>"
            );
        }

        html += (
            "</table>" +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            "<div class='filename hidden'>DebtorContractRemain.xls</div>" +
            "<div class='contenttype hidden'>application/msexcel</div>"
        );
        /*
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContractRemain.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        */
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);
    }

    public static void ExportCPReportDebtorContractPaid(string exportSend) {
        string html = string.Empty;
        string font = "Cordia New";
        string fontSize = "13";
        string borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        string borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle3 = "border-left:thin solid #000000;";
        string borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle7 = "border-top:thin solid #000000;";
        string borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle9 = "border:thin solid #000000;";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];        

        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='19'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รายงานการรับชำระเงินจากลูกหนี้ตามการผิดสัญญาการศึกษามหาวิทยาลัยมหิดล</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='19'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(dateStart) + " ถึงวันที่ " + Util.LongDateTH(dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='19'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            ExportCPReportDebtorContractSearchCondition(exportSend, "reportdebtorcontractpaid") +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลำดับที่</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ชื่อ - สกุล</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>หลักสูตร</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>วันเดือนปีรับสภาพหนี้</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>วันเดือนปีที่ชำระเงิน</div>" +
            "       </td>" +
            "       <td align='center' colspan='2' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คิดเป็นเงินที่ต้องชดใช้ยกมา</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวมจำนวนเงิน</div>" +
            "       </td>" +
            "       <td align='center' colspan='5' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รับชำระเงินชดใช้ ( รายละเอียดตามใบเสร็จรับเงิน )</div>" +
            "       </td>" +
            "       <td align='center' colspan='2' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>กองคลังรับ</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินที่ต้องชดใช้คงเหลือยกไป</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle2 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รูปแบบการชำระหนี้</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( ตามที่ได้รับหนังสือ )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ที่ต้องชดใช้</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>วันเดือนปี</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เล่มที่ / เลขที่</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวมรับชำระ</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เลขที่ใบสำคัญรับ</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>นำเข้ากองทุน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวม</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle4 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle6 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>"
        );

        string[,] data = eCPDB.ListExportReportDebtorContractPaid(exportSend);        

        if (data.GetLength(0) > 0) {
            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            string studentIDOld = string.Empty;
            string order = string.Empty;
            string fullName = string.Empty;
            string program = string.Empty;
            int i;
            int j = 1;

            for (i = 0; i < (data.GetLength(0) - 1); i++) {
                if (!data[i, 0].Equals(studentIDOld)) {
                    order = (j++).ToString("#,##0");
                    fullName = (data[i, 1] + data[i, 2] + " " + data[i, 3]);
                    program = (data[i, 6] + " - " + data[i, 7] + (!data[i, 9].Equals("0") ? ("( กลุ่ม " + data[i, 9] + " )") : ""));
                }
                else {
                    order = "&nbsp;";
                    fullName = "&nbsp;";
                    program = "&nbsp;";
                }

                html += (
                    "<tr>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + order + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + fullName + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + program + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 12]) ? Util.ConvertDateTH(DateTime.Parse(data[i, 12], provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 13]) ? Util.ConvertDateTH(DateTime.Parse(data[i, 13], provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 14]) ? double.Parse(data[i, 14]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 17]) ? double.Parse(data[i, 17]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 18]) ? double.Parse(data[i, 18]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 19]) ? Util.ConvertDateTH(DateTime.Parse(data[i, 19], provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 20]) && !string.IsNullOrEmpty(data[i, 21]) ? (data[i, 20] + " / " + data[i, 21]) : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 22]) ? double.Parse(data[i, 22]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 23]) ? double.Parse(data[i, 23]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 24]) ? double.Parse(data[i, 24]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 25]) ? data[i, 25] : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 26]) ? data[i, 26] : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 27]) ? double.Parse(data[i, 27]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 29]) ? double.Parse(data[i, 29]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 30]) ? double.Parse(data[i, 30]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle4 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 32]) ? data[i, 32] : "-") + "</div>" +
                    "   </td>" +
                    "</tr>"
                );

                studentIDOld = data[i, 0];
            }

            html += (
                "<tr>" +
                "   <td align='right' colspan='5' style='" + borderStyle7 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>รวม</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 37]) ? double.Parse(data[i, 37]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 38]) ? double.Parse(data[i, 38]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle9 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 39]) ? double.Parse(data[i, 39]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' colspan='2' style='" + borderStyle7 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 40]) ? double.Parse(data[i, 40]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 41]) ? double.Parse(data[i, 41]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle9 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 42]) ? double.Parse(data[i, 42]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' colspan='2' style='" + borderStyle7 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 43]) ? double.Parse(data[i, 43]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 44]) ? double.Parse(data[i, 44]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 45]) ? double.Parse(data[i, 45]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='center' style='" + borderStyle1 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>"
            );
        }

        html += (
            "</table>" +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='19'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='19'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div>" +
            "       </td>" +
            "       <td align='center' colspan='12'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='12'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div>" +
            "       </td>" +
            "       <td align='center' colspan='12'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='12'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            "<div class='filename hidden'>DebtorContractPaid.xls</div>" +
            "<div class='contenttype hidden'>application/msexcel</div>"
        );

        /*
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContractPaid.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        */
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);
    }

    public static void ExportCPReportDebtorContract(string exportSend) {
        string html = string.Empty;
        string font = "Cordia New";
        string fontSize = "13";
        string borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        string borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle3 = "border-left:thin solid #000000;";
        string borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        string borderStyle7 = "border-top:thin solid #000000;";
        string borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        string borderStyle9 = "border:thin solid #000000;";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];
        
        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รายงานลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดล</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(dateStart) + " ถึงวันที่ " + Util.LongDateTH(dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            ExportCPReportDebtorContractSearchCondition(exportSend, "reportdebtorcontract") +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลำดับที่</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ชื่อ - สกุล</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>หลักสูตร</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงื่อนไขกำหนดชดใช้ตามสัญญา</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลาปฏิบัติงานชดใช้</div>" +
            "       </td>" +
            "       <td align='center' colspan='4' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คงเหลือระยะเวลาปฏิบัติงานตามสัญญา</div>" +
            "       </td>" +
            "       <td align='center' colspan='3' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คิดเป็นเงินที่ต้องชดใช้จำนวน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>วันเดือนปีรับสภาพหนี้</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle2 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รูปแบบการชำระหนี้</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลา</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ระยะเวลา</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>จำนวนเงิน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เริ่มต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>สิ้นสุด</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คำนวณวัน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เริ่มต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>สิ้นสุด</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คำนวณวัน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>คงเหลือ</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>เงินต้น</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ดอกเบี้ย</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle1 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>รวมจำนวนเงิน</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle3 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( ตามที่ได้รับหนังสือ )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle4 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( ปี )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( วัน )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>( บาท )</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle5 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='center' style='" + borderStyle6 + "'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>"
        );

        string[,] data = eCPDB.ListExportReportDebtorContract(exportSend);

        if (data.GetLength(0) > 0) {
            IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
            string order = string.Empty;
            string fullName = string.Empty;
            string program = string.Empty;
            string allActualDate = string.Empty;
            string workDateStart = string.Empty;
            string workDateEnd = string.Empty;
            string requireDate = string.Empty;
            string approveDate = string.Empty;
            string actualDate = string.Empty;
            string remainDate = string.Empty;
            int i;
            int j = 1;

            for (i = 0; i < (data.GetLength(0) - 1); i++) {
                order = (j++).ToString("#,##0");
                fullName = (data[i, 1] + data[i, 2] + " " + data[i, 3]);
                program = (data[i, 6] + " - " + data[i, 7] + (!data[i, 9].Equals("0") ? (" ( กลุ่ม " + data[i, 9] + " )") : ""));
                allActualDate = (data[i, 12].Equals("1") ? data[i, 14] : string.Empty);
                            
                if (data[i, 12].Equals("1")) {
                    allActualDate = data[i, 14];
                    workDateStart = data[i, 16];
                    workDateEnd = (!string.IsNullOrEmpty(workDateStart) ? DateTime.Parse(workDateStart, provider).AddDays(double.Parse(allActualDate)).ToString() : string.Empty);
                    requireDate = data[i, 16];
                    approveDate = data[i, 17];
                    actualDate = data[i, 18];
                    remainDate = data[i, 19];
                }
                else {
                    allActualDate = string.Empty;
                    workDateStart = string.Empty;
                    workDateEnd = string.Empty;
                    requireDate = string.Empty;
                    approveDate = string.Empty;
                    actualDate = string.Empty;
                    remainDate = string.Empty;
                }

                html += (
                    "<tr>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + order + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + fullName + "</div>" +
                    "   </td>" +
                    "   <td align='left' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + program + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 13]) ? double.Parse(data[i, 13]).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(allActualDate) ? double.Parse(allActualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 15]) ? double.Parse(data[i, 15]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(workDateStart) ? Util.ConvertDateTH(DateTime.Parse(workDateStart, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(workDateEnd) ? Util.ConvertDateTH(workDateEnd) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(allActualDate) ? double.Parse(allActualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(requireDate) ? Util.ConvertDateTH(DateTime.Parse(requireDate, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(approveDate) ? Util.ConvertDateTH(DateTime.Parse(approveDate, provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(actualDate) ? double.Parse(actualDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(remainDate) ? double.Parse(remainDate).ToString("#,##0") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 20]) ? double.Parse(data[i, 20]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>0</div>" +
                    "   </td>" +
                    "   <td align='right' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 20]) ? double.Parse(data[i, 20]).ToString("#,##0.00") : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle3 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 21]) ? Util.ConvertDateTH(DateTime.Parse(data[i, 21], provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div>" +
                    "   </td>" +
                    "   <td align='center' style='" + borderStyle4 + "'>" +
                    "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 23]) ? data[i, 23] : "-") + "</div>" +
                    "   </td>" +
                    "</tr>"
                );
            }
        
            html += (
                "<tr>" +
                "   <td align='right' colspan='13' style='" + borderStyle7 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>รวม</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 28]) ? double.Parse(data[i, 28]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle8 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>0</div>" +
                "   </td>" +
                "   <td align='right' style='" + borderStyle9 + "'>" +
                "       <div style='font:normal " + fontSize + "pt " + font + ";'>" + (!string.IsNullOrEmpty(data[i, 28]) ? double.Parse(data[i, 28]).ToString("#,##0.00") : "-") + "</div>" +
                "   </td>" +
                "   <td align='center' style='" + borderStyle7 + "' colspan='2' style='border-right:0px;border-bottom:0px'></td>" +
                "</tr>"
            );
        }

        html += (
            "</table>" +
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center' colspan='18'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "   <tr>" +
            "       <td align='center'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "       <td align='left' colspan='6'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div>" +
            "       </td>" +
            "       <td align='center' colspan='11'>" +
            "           <div style='font:normal " + fontSize + "pt " + font + ";'>&nbsp;</div>" +
            "       </td>" +
            "   </tr>" +
            "</table>" +
            "<div class='filename hidden'>DebtorContract.xls</div>" +
            "<div class='contenttype hidden'>application/msexcel</div>"
        );
        /*
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContract.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";     
        */
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);
    }
 
    public static string ViewTransPayment(string cp2idDate) {
        string html = string.Empty;
        char[] separator = new char[] { ':' };
        string[] cp2idDate1 = cp2idDate.Split(separator);
        string cp2id = cp2idDate1[0];
        string dateStart = cp2idDate1[1];
        string dateEnd = cp2idDate1[2];
        string[,] data = eCPDB.ListTransPayment(cp2id, "", "");
        int recordCount = data.GetLength(0);
        
        if (recordCount > 0) {
            html += (
                "<div id='view-trans-payment'>" +
                "   <input type='hidden' id='period-hidden' value=''>" +
                "   <div class='tab-line'></div>" +
                "   <div class='content-data-tab-content'>" +
                "       <div class='content-left'>" +
                "           <div class='content-data-tab-content-msg'>ช่วงวันที่รับสภาพหนี้ " + (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd) ? ("ระหว่างวันที่ " + dateStart + " ถึงวันที่ " + dateEnd) : "ไม่กำหนด") + "</div>" +
                "       </div>" +
                "       <div class='content-right'>" +
                "           <div class='content-data-tab-content-msg'>ค้นหาพบ " + recordCount + " รายการ</div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='clear'></div>" +
                "   <div class='tab-line'></div>" +
                "   <div class='box3'>" +
                "       <div class='table-head'>" +
                "           <ul>" +
                "               <li id='table-head-trans-payment-col1'>" +
                "                   <div class='table-head-line1'>งวดที่</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col2'>" +
                "                   <div class='table-head-line1'>เงินต้น</div>" +
                "                   <div>&nbsp;</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col3'>" +
                "                   <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                   <div>&nbsp;</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col4'>" +
                "                   <div class='table-head-line1'>เงินต้น</div>" +
                "                   <div>รับชำระ</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col5'>" +
                "                   <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                   <div>รับชำระ</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col6'>" +
                "                   <div class='table-head-line1'>ยอดเงิน</div>" +
                "                   <div>รับชำระ</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col7'>" +
                "                   <div class='table-head-line1'>เงินต้น</div>" +
                "                   <div>คงเหลือ</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col8'>" +
                "                   <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                   <div>คงเหลือ</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col9'>" +
                "                   <div class='table-head-line1'>ดอกเบี้ย</div>" +
                "                   <div>ค้างจ่าย</div>" +
                "                   <div>( บาท )</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col10'>" +
                "                   <div class='table-head-line1'>วันเดือนปี</div>" +
                "                   <div>ที่รับชำระหนี้</div>" +
                "               </li>" +
                "               <li class='table-col' id='table-head-trans-payment-col11'>" +
                "                   <div class='table-head-line1'>จ่ายชำระด้วย</div>" +
                "               </li>" +
                "           </ul>" +
                "       </div>" +
                "       <div class='clear'></div>" +
                "   </div>" +
                "   <div id='box-list-trans-payment'>" +
                "       <div id='list-trans-payment'>" + eCPDataPayment.ListTransPayment(data) + "</div>" +
                "   </div>" +
                "</div>"
            );
        }

        return html;
    }

    public static string ListCPReportDebtorContractByProgram(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountCPReportDebtorContractByProgram(c);
       
        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPReportDebtorContractByProgram(c);
            string highlight;
            string trackingStatus;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                trackingStatus = (data[i, 18] + data[i, 19] + data[i, 20] + data[i, 21]);                
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','v3')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                    "   <li id='table-content-cp-report-debtor-contract-by-program-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col4' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 15]) ? data[i, 15] : "-") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 11]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col6' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 12]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col7' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 13]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col8' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 14]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col9' onclick=" + callFunc + ">" +
                    "       <div>" + (!string.IsNullOrEmpty(data[i, 17]) ? data[i, 17] : "-") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "reportdebtorcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                "   <div class='clear'></div>" +
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav>" + pageHtml + "<pagenav>"
        );
    }

    public static string ListCPReportDebtorContract(HttpContext c) {
        string html = string.Empty;
        int recordCount = eCPDB.CountCPReportDebtorContract(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPReportDebtorContract(c);
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 6].Equals("0") ? (" ( กลุ่ม " + data[i, 6] + " )") : "");
                callFunc = ("ViewReportDebtorContractByProgram('" + data[i, 1] + "','" + data[i, 2].Replace(" ", "&") + "','" + data[i, 3] + "','" + data[i, 4].Replace(" ", "&") + "','" + data[i, 5] + "','" + data[i, 6] + "','" + data[i, 7] + "','" + data[i, 8] + "')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='report-debtor-contract" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-report-debtor-contract-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col2' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 3] + "</span>- " + (data[i, 4] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col3' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 9]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 10]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col5' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 11]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col6' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 12]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-report-debtor-contract-col7' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 13]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );
        }

        return (
            "<recordcount>" + recordCount.ToString("#,##0") + "<recordcount>" +
            "<list>" + html + "<list>" +
            "<pagenav><pagenav>"
        );
    }

    public static string TabCPReportDebtorContract(string reportOrder) {
        string html = string.Empty;
        string tabName = string.Empty;
        string title = string.Empty;

        switch (reportOrder) {
            case "reportdebtorcontract":
                tabName = "ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้";
                title = "cp-report-debtor-contract";
                break;
            case "reportdebtorcontractpaid":
                tabName = "การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้";
                title = "cp-report-debtor-contract-paid";
                break;
            case "reportdebtorcontractremain":
                tabName = "ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้";
                title = "cp-report-debtor-contract-remain";
                break;
        }

        html += (
            "<div id='cp-report-debtor-contract-head'>" +
            "   <input type='hidden' id='report-debtor-contract-order' value='" + reportOrder + "'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle(title) +
            "       <div class='content-data-tabs' id='tabs-cp-report-debtor-contract'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-report-debtor-contract' alt='#tab1-cp-report-debtor-contract' href='javascript:void(0)'>" + tabName + "</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab2-cp-report-debtor-contract' alt='#tab2-cp-report-debtor-contract' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-report-debtor-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-report-debtor-contract' value=''>" +
            "                   <input type='hidden' id='date-start-report-debtor-contract-hidden' value=''>" +
            "                   <input type='hidden' id='date-end-report-debtor-contract-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportdebtorcontract',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=PrintDebtorContract(1)>ส่งออก</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-debtor-contract'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-report-debtor-contract-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-report-debtor-contract-condition-order' id='search-report-debtor-contract-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>ช่วงวันที่รับสภาพหนี้</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-debtor-contract-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-report-debtor-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-report-debtor-contract-by-program' value=''>" +
            "                   <input type='hidden' id='id-name-report-debtor-contract-by-program-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-report-debtor-contract-by-program-hidden' value=''>" +
            "                   <input type='hidden' id='program-report-debtor-contract-by-program-hidden' value=''>" +
            "                   <input type='hidden' id='format-payment-report-debtor-contract-by-program-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchstudentdebtorcontractbyprogram',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=PrintDebtorContract(2)>ส่งออก</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-report-debtor-contract-by-program'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-report-debtor-contract-by-program-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>ช่วงวันที่รับสภาพหนี้</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order2'>" +
            "                   <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order2-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order3'>" +
            "                   <div class='box-search-condition-order-title'>รูปแบบการชำระหนี้</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order3-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-report-debtor-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-debtor-contract-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col2'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col3'>" +
            "                       <div class='table-head-line1'>จำนวนลูกหนี้</div>" +
            "                       <div>ผิดสัญญา</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( คน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col4'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col5'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col6'>" +
            "                       <div class='table-head-line1'>ยอดดอกเบี้ย</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div><div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-col7'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>คงเหลือ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-debtor-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-report-debtor-contract-by-program-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col4'>" +
            "                       <div class='table-head-line1'>รับสภาพหนี้</div>" +
            "                       <div>เมื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col5'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col6'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col7'>" +
            "                       <div class='table-head-line1'>ยอดดอกเบี้ย</div>" +
            "                       <div>ที่รับชำระ</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col8'>" +
            "                       <div class='table-head-line1'>ยอดเงินต้น</div>" +
            "                       <div>ที่ต้องชดใช้</div>" +
            "                       <div>คงเหลือ</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col9'>" +
            "                       <div class='table-head-line1'>รูปแบบ</div>" +
            "                       <div>การชำระหนี้</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-report-debtor-contract-content'>" +
            "   <div class='tab-content' id='tab1-cp-report-debtor-contract-content'>" +
            "       <div class='box4' id='list-data-report-debtor-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-report-debtor-contract-content'>" +
            "       <div class='box4' id='list-data-report-debtor-contract-by-program'></div>" +
            "       <div id='nav-page-report-debtor-contract-by-program'></div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>"
        );

        return html;
    }

    public static void ExportCPReportDebtorContractBreakRequireRepayPayment(string exportSend) {
        string fileName = "ReportDebtorContractBreakRequireRepayPayment";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[5];
        string dateEnd = exportSendValue[6];

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xlsx");
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;

        using (ExcelPackage xls = new ExcelPackage(new FileInfo(HttpContext.Current.Server.MapPath(@"ExportTemplate/" + fileName + ".xlsx")))) {
            ExcelWorksheet ws = xls.Workbook.Worksheets[0];
            MemoryStream ms = new MemoryStream();

            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(eCPDB.ListExportReportDebtorContractBreakRequireRepayPayment(exportSend));
            JArray data = jsonObject;

            string header = ("&16&\"TH Sarabun New,Bold\"รายงานลูกหนี้ผิดสัญญาคงค้าง (กรณี Z600 ลูกหนี้นักศึกษา)\nประจำเดือน " + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + " พ.ศ. " + (int.Parse(Util.CurrentDate("yyyy")) + 543).ToString());

            ws.HeaderFooter.OddHeader.CenteredText = header;
            ws.HeaderFooter.EvenHeader.CenteredText = header;
            ws.Cells["A1"].Value = ("ช่วงวันที่รับเอกสารการทวงถามครั้งที่ 1 ตั้งแต่วันที่ " + (!string.IsNullOrEmpty(dateStart) ? dateStart : "( ไม่ระบุ )") + " - วันที่ " + (!string.IsNullOrEmpty(dateEnd) ? dateEnd : "( ไม่ระบุ )"));

            if (data.Count > 0) {
                int i = 1;
                int j = 3;

                foreach (JObject dr in jsonObject) {
                    ws.Cells["A" + j.ToString()].Value = i.ToString("#,##0");
                    ws.Cells["B" + j.ToString()].Value = (dr["titleName"].ToString() + dr["firstName"].ToString() + " " + dr["lastName"].ToString());
                    ws.Cells["C" + j.ToString()].Value = dr["studentCode"].ToString();
                    ws.Cells["D" + j.ToString()].Value = (dr["programCode"].ToString() + "-" + dr["programName"].ToString() + (!dr["groupNum"].ToString().Equals("0") ? (" ( กลุ่ม " + dr["groupNum"].ToString() + " )") : ""));
                    ws.Cells["E" + j.ToString()].Value = dr["facultyName"].ToString();
                    ws.Cells["F" + j.ToString()].Value = ("สัญญาการเป็นนักศึกษา" + eCPDataReport.ReplaceProgramToShortProgram(dr["programName"].ToString()) + " ฉบับลงวันที่ " + Util.ThaiLongDate(eCPUtil.ConvertDateEN(dr["contractDate"].ToString())));
                    ws.Cells["G" + j.ToString()].Value = Util.ConvertDateTH(dr["sendDate"].ToString());
                    ws.Cells["H" + j.ToString()].Value = Util.ConvertDateTH(dr["receiverDate"].ToString());
                    ws.Cells["I" + j.ToString()].Value = (dr["replyDateHistory"].ToString().Replace(",", "\n"));
                    ws.Cells["J" + j.ToString()].Value = double.Parse(dr["subtotalPenalty"].ToString()).ToString("#,##0.00");
                    ws.Cells["K" + j.ToString()].Value = double.Parse(dr["totalPayCapital"].ToString()).ToString("#,##0.00");
                    ws.Cells["L" + j.ToString()].Value = double.Parse(dr["totalPayInterest"].ToString()).ToString("#,##0.00");
                    ws.Cells["M" + j.ToString()].Value = double.Parse(dr["totalPay"].ToString()).ToString("#,##0.00");
                    ws.Cells["N" + j.ToString()].Value = double.Parse(dr["totalRemain"].ToString()).ToString("#,##0.00");
                    ws.Cells["O" + j.ToString()].Value = double.Parse(dr["remainAccruedInterest"].ToString()).ToString("#,##0.00");
                    ws.Cells["P" + j.ToString()].Value = eCPUtil.paymentStatus[int.Parse(dr["statusPayment"].ToString()) - 1];
                    ws.Cells["Q" + j.ToString()].Value = (dr["statusPaymentRecord"].ToString().Equals("P") ? (eCPUtil.paymentRecordStatus[0, 0] + " เนื่องจากอยู่ระหว่างการฟ้องร้องบังคับคดี") : string.Empty);

                    i++;
                    j++;
                }
            }

            xls.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);

            ms.Close();
            ms.Dispose();
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
}

public class eCPDataReportCertificateReimbursement {
    private static string ExportCPReportCertificateReimbursementSection(
        int section,
        string font,
        Dictionary<string, string> lawyer
    ) {
        string html = string.Empty;
        string lawyerFullname = string.Empty;
        string lawyerPhoneNumber = string.Empty;
    
        if (lawyer != null) {
            lawyerFullname = lawyer["Fullname"];
            lawyerPhoneNumber = lawyer["PhoneNumber"];
        }

        if (section.Equals(1)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>กองกฎหมาย สำนักงานอธิการบดี</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. " + Util.NumberArabicToThai(lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘.๐๑๙/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>การชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(2)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดพิจารณาลงนามในหนังสือ ที่เสนอมาพร้อมนี้" +
                "       </p>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(3)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='98'></td>" +
                "               <td width='502' style='text-align: center'>" +
                "                   <div style='font:normal 15pt " + font + ";'>(" + lawyerFullname + ")</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>นิติกร</div></td>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(4)) {
            html += (
                "<tr>" +
                "   <td width='100%' height='110' align='center'>" +
                "       <img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' />" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%' align='right'>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                "       <div align='right' style='font:normal 15pt " + font + ";'>โทร. " + Util.NumberArabicToThai(lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘/</div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>วันที่</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "       <div>" +
                "           <table border='0' cellpadding='0' cellspacing='0'>" +
                "               <tr>" +
                "                   <td width='50'>" +
                "                       <div style='font:normal 15pt " + font + ";'>เรื่อง</div>" +
                "                   </td>" +
                "                   <td width='550'>" +
                "                       <div style='font:normal 15pt " + font + ";'>การชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา</div>" +
                "                   </td>" +
                "               </tr>" +
                "           </table>" +
                "       </div>" +
                "   </td>" +
                "</tr>"
            );
        }

        if (section.Equals(5)) {
            html += (
                "<tr>" +
                "   <td width='100%'>" +
                "       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                "           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดทราบ" +
                "       </p>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "   </td>" +
                "</tr>" +
                "<tr>" +
                "   <td width='100%'>" +
                "       <table border='0' cellpadding='0' cellspacing='0'>" +
                "           <tr>" +
                "               <td width='200'></td>" +
                "               <td width='400'>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ขอแสดงความนับถือ</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>(" + eCPUtil.DIRECTOR + ")</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
                "                   <div align='center' style='font:normal 15pt " + font + ";'>ปฏิบัติหน้าที่แทนอธิการบดีมหาวิทยาลัยมหิดล</div>" +
                "               </td>" +
                "           </tr>" +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        return html;
    }

    public static void ExportCPReportCertificateReimbursement(string exportSend) {
        string html = string.Empty;
        string width = "600";
        string font = "TH SarabunPSK, TH Sarabun New";
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string cp2id = exportSendValue[0];    
        string[,] data1 = eCPDB.ListDetailPaymentOnCPTransRequireContract(cp2id);
    
        Dictionary<string, string> lawyer = new Dictionary<string, string>();
        lawyer.Add("Fullname", (!string.IsNullOrEmpty(data1[0, 29]) ? data1[0, 29] : string.Empty));
        lawyer.Add("PhoneNumber", (!string.IsNullOrEmpty(data1[0, 30]) ? data1[0, 30] : data1[0, 31]));
        lawyer.Add("Email", (!string.IsNullOrEmpty(data1[0, 32]) ? data1[0, 32] : string.Empty));

        html += (
            "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
            "   <tr>" +
            "       <td width='" + width + "' valign='top'>" +
            "           <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportCertificateReimbursementSection(1, font, lawyer) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div>" +
            "                           <table border='0' cellpadding='0' cellspacing='0'>" +
            "                               <tr>" +
            "                                   <td width='50'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
            "                                   </td>" +
            "                                   <td width='550'>" +
            "                                       <div style='font:normal 15pt " + font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
            "                                   </td>" +
            "                               </tr>" +
            "                           </table>" +
            "                       </div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่" + data1[0, 10] + data1[0, 11] + " " + data1[0, 12] + " " +
            "                           ได้ชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data1[0, 14]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data1[0, 28])) + " " +
            "                           ให้แก่มหาวิทยาลัยครบถ้วนแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงินที่แนบมานี้ นั้น" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
            "                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ในการนี้ งานกฎหมายและนิติกรรมสัญญาจึงเห็นควรให้มหาวิทยาลัยออกหนังสือเพื่อเป็นหลักฐานการพ้นภาระผูกพันตามสัญญาฯ " +
            "                           ให้แก่บุคคลดังกล่าวด้วย" +
            "                       </p>" +
            "                   </td>" +
            "               </tr>" +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportCertificateReimbursementSection(2, font, null) +
            "               <tr>" +
            "                   <td width='100%'>" +
            "                       <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
            "                   </td>" +
            "               </tr>" +
                            ExportCPReportCertificateReimbursementSection(3, font, lawyer) +
            "           </table>" +
            "       </td>" +
            "   </tr>"
        );

        string formatPayment = data1[0, 8];
        string pursuant = string.Empty;
        string pursuantBookDate = string.Empty;
        string[,] data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(cp2id, "2");

        if (data2.GetLength(0) > 0) {
            pursuant = data2[0, 6];
            pursuantBookDate = data2[0, 7];
        }
            
        bool overpayment = false;
        string receiptNo = string.Empty;
        string receiptBookNo = string.Empty;
        string receiptDate = string.Empty;
        string[,] data3 = eCPDB.ListTransPayment(cp2id, "", "");
        string[,] data4;

        if (data3.GetLength(0) > 0) {
            html += (
                "<tr>" +
                "   <td width='" + width + "' valign='top'>" +
                "       <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                            ExportCPReportCertificateReimbursementSection(4, font, lawyer) +
                "           <tr>" +
                "               <td width='100%'>" +
                "                   <div>" +
                "                       <table border='0' cellpadding='0' cellspacing='0'>" +
                "                           <tr>" +
                "                               <td width='50'>" +
                "                                   <div style='font:normal 15pt " + font + ";'>เรียน</div>" +
                "                               </td>" +
                "                               <td width='550'>" +
                "                                   <div style='font:normal 15pt " + font + ";'>" + data1[0, 10] + data1[0, 11] + " " + data1[0, 12] + "</div>" +
                "                               </td>" +
                "                           </tr>" +
                "                       </table>" +
                "                   </div>"
            );
                     
            if (formatPayment.Equals("1")) {
                html += (
                    "               <div>" +
                    "                   <table border='0' cellpadding='0' cellspacing='0'>" +
                    "                       <tr>" +
                    "                           <td width='50' valign='top'>" +
                    "                               <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
                    "                           </td>" +
                    "                           <td width='550'>" +
                    "                               <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!string.IsNullOrEmpty(pursuant) ? Util.NumberArabicToThai(pursuant) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(pursuantBookDate)) : "") + "</div>" +
                    "                           </td>" +
                    "                       </tr>" +
                    "                   </table>" +
                    "               </div>"
                );
                                
                data4 = eCPDB.ListDetailTransPayment(data3[0, 1]);

                if (data4.GetLength(0) > 0) {
                    overpayment = (!string.IsNullOrEmpty(data4[0, 8]) ? true : false);
                    receiptNo = (!string.IsNullOrEmpty(data4[0, 28]) ? data4[0, 28] : string.Empty);
                    receiptBookNo = (!string.IsNullOrEmpty(data4[0, 29]) ? data4[0, 29] : string.Empty);
                    receiptDate = (!string.IsNullOrEmpty(data4[0, 30]) ? data4[0, 30] : string.Empty);

                    html += (
                        "           <div>" +
                        "               <table border='0' cellpadding='0' cellspacing='0'>" +
                        "                   <tr>" +
                        "                       <td width='98' valign='top'>" +
                        "                           <div style='font:normal 15pt " + font + ";'>สิ่งที่ส่งมาด้วย</div>" +
                        "                       </td>" +
                        "                       <td width='502'>" +
                        "                           <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>ใบเสร็จรับเงิน เล่มที่ " + (!string.IsNullOrEmpty(receiptBookNo) ? Util.NumberArabicToThai(receiptBookNo) : "") + " เลขที่ " + (!string.IsNullOrEmpty(receiptNo) ? Util.NumberArabicToThai(receiptNo) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(receiptDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(receiptDate)) : "") + "</div>" +
                        "                       </td>" +
                        "                   </tr>" +
                        "               </table>" +
                        "           </div>"
                    );
                }

                html += (
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>" +
                    "               <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>" +
                    "               <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                    "                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data1[0, 14]) + " " +
                    "                   ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data1[0, 28])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(data1[0, 4]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data1[0, 4]) + ") " +
                    "                   ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว นั้น" +
                    "               </p>" +
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>"
                );

                if (overpayment.Equals(false)) {
                    html += (
                        "           <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                        "               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นมาชำระให้แก่มหาวิทยาลัยมหิดลจนครบถ้วน " +
                        "               เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามสิ่งที่ส่งมาด้วย" +
                        "           </p>"
                    );
                }
                else {
                    html += (
                        "           <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                        "               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นพร้อมทั้งดอกเบี้ยผิดนัดมาชำระให้แก่มหาวิทยาลัยจนครบถ้วน " +
                        "               เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงิน ที่แนบมาพร้อมนี้" +
                        "           </p>"
                    );
                }

                html += (
                    "           </td>" +
                    "       </tr>"
                );
            }

            if (formatPayment.Equals("2")) {
                html += (
                    "       <tr>" +
                    "           <td>" +
                    "               <div>" +
                    "                   <table border='0' cellpadding='0' cellspacing='0'>" +
                    "                       <tr>" +
                    "                           <td width='50' valign='top'>" +
                    "                               <div style='font:normal 15pt " + font + ";'>อ้างถึง</div>" +
                    "                           </td>" +
                    "                           <td width='550'>" +
                    "                               <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>๑. หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!string.IsNullOrEmpty(pursuant) ? Util.NumberArabicToThai(pursuant) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(pursuantBookDate)) : "") + "</div>" +
                    "                               <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>๒. หนังสือรับสภาพหนี้ ฉบับลงวันที่</div>" +
                    "                           </td>" +
                    "                       </tr>" +
                    "                   </table>" +
                    "               </div>" +
                    "               <div>" +
                    "                   <table border='0' cellpadding='0' cellspacing='0'>" +
                    "                       <tr>" +
                    "                           <td width='98' valign='top'>" +
                    "                               <div style='font:normal 15pt " + font + ";'>สิ่งที่ส่งมาด้วย</div>" +
                    "                           </td>" +
                    "                           <td width='502'>"
                );

                for (int i = 0; i < data3.GetLength(0); i++) {
                    data4 = eCPDB.ListDetailTransPayment(data3[i, 1]);

                    if (data4.GetLength(0) > 0) {
                        receiptNo = (!string.IsNullOrEmpty(data4[0, 28]) ? data4[0, 28] : string.Empty);
                        receiptBookNo = (!string.IsNullOrEmpty(data4[0, 29]) ? data4[0, 29] : string.Empty);
                        receiptDate = (!string.IsNullOrEmpty(data4[0, 30]) ? data4[0, 30] : string.Empty);

                        html += (
                            "                       <div style='font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" + Util.NumberArabicToThai((i + 1).ToString()) + ". สำเนาใบเสร็จรับเงิน เล่มที่ " + (!string.IsNullOrEmpty(receiptBookNo) ? Util.NumberArabicToThai(receiptBookNo) : "") + " เลขที่ " + (!string.IsNullOrEmpty(receiptNo) ? Util.NumberArabicToThai(receiptNo) : "") + " ลงวันที่ " + (!string.IsNullOrEmpty(receiptDate) ? Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(receiptDate)) : "") + "</div>"
                        );
                    }
                }

                html += (
                    "                           </td>" +
                    "                       </tr>" +
                    "                   </table>" +
                    "               </div>" +
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>" +
                    "               <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>" +
                    "               <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                    "                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง ๑. มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(data1[0, 14]) + " " +
                    "                   ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(eCPUtil.ConvertDateEN(data1[0, 28])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(data1[0, 4]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(data1[0, 4]) + ") " +
                    "                   ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว และท่านได้ตกลงผ่อนชำระเงินให้แก่มหาวิทยาลัยตามหนังสือที่อ้างถึง ๒. นั้น" +
                    "               </p>" +
                    "           </td>" +
                    "       </tr>" +
                    "       <tr>" +
                    "           <td width='100%'>" +
                    "               <p style='text-wrap:normal;font:normal 15pt " + font + ";text-align:justify;text-justify:inter-cluster;'>" +
                    "                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นพร้อมทั้งดอกเบี้ยการผ่อนชำระเงินมาชำระให้แก่มหาวิทยาลัยจนครบถ้วน " +
                    "                   เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงิน ที่แนบมาพร้อมนี้" +
                    "               </p>" +
                    "           </td>" +
                    "       </tr>"
                );
            }

            html += (
                "           <tr>" +
                "               <td width='100%'>" +
                "                   <div style='font:normal 15pt " + font + ";'>&nbsp;</div>" +
                "               </td>" +
                "           </tr>" +
                            ExportCPReportCertificateReimbursementSection(5, font, null) +
                "       </table>" +
                "   </td>" +
                "</tr>"
            );
        }

        html += (
            "</table>" +
            "<div class='filename hidden'>CertificateReimbursement.doc</div>" +
            "<div class='contenttype hidden'>application/msword</div>"
        );

        /*
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=CertificateReimbursement.doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        */
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Write(html);    
    }
} 

