/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๙/๐๘/๒๕๕๕>
Modify date : <๒๘/๐๕/๒๕๖๖>
Description : <สำหรับการแสดงฟอร์มการค้นหา>
=============================================
*/

using System;
using System.Globalization;
using System.Web;

public class eCPDataFormSearch {
    public static string SearchCPTabUser() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-tab-user'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-tab-user-keyword1-label'>" +
            "                   <div class='form-label-style'>ชื่อ</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ชื่อที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-tab-user-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='name-tab-user' onblur=Trim('name-tab-user'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPTabUser()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPTabUser('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string ListProfileStudent(string studentid) {
        string html = string.Empty;
        string[,] data = eCPDB.ListProfileStudent(studentid);

        if (data.GetLength(0) > 0) {
            html += (
                "<list>" +
                data[0, 1]  + ";" +
                data[0, 2]  + ";" +
                data[0, 3]  + ";" +
                data[0, 4]  + ";" +
                data[0, 5]  + ";" +
                data[0, 6]  + ";" +
                data[0, 7]  + ";" +
                data[0, 8]  + ";" +
                data[0, 9]  + ";" +
                data[0, 10] + ";" +
                data[0, 11] + ";" +
                data[0, 12] + ";" +
                data[0, 13] + ";" +
                data[0, 14] + ";" +
                data[0, 15] + ";" +
                data[0, 16] + ";" +
                data[0, 17] + ";" +
                data[0, 18] + ";" +
                data[0, 19] + ";" +
                data[0, 20] + ";" +
                data[0, 21] + ";" +
                data[0, 22] + ";" +
                data[0, 23] + ";" +
                (!string.IsNullOrEmpty(data[0, 24]) && !string.IsNullOrEmpty(data[0, 13]) ? (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + data[0, 25] + "/" + data[0, 24]) : string.Empty) +
                "<list>"
            );
        }

        return html;
    }

    public static string ListSearchStudentWithResult(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;
        int recordCount = eCPDB.CountStudent(c); ;
   
        if (recordCount > 0) {
            string[,] data = eCPDB.ListStudent(c);
            string highlight;
            string groupNum;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                groupNum = (!data[i, 12].Equals("0") ? (" ( กลุ่ม " + data[i, 12] + " )") : "");
                callFunc = ("ViewStudent('" + data[i, 1] + ";" + data[i, 2] + ";" + data[i, 3].Replace(" ", "&nbsp;") + ";" + data[i, 4].Replace(" ", "&nbsp;") + ";" + data[i, 5].Replace(" ", "&nbsp;") + ";" + data[i, 6].Replace(" ", "&nbsp;") + ";" + data[i, 7] + ";" + data[i, 8].Replace(" ", "&nbsp;") + ";" + data[i, 9] + ";" + data[i, 10].Replace(" ", "&nbsp;") + ";" + data[i, 11] + ";" + data[i, 12] + ";" + data[i, 13] + ";" + data[i, 14] + "')");              
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='student" + data[i, 1] + "'>" +
                    "   <li id='tab1-table-content-search-student-with-result-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-search-student-with-result-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-search-student-with-result-col3' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 4] + data[i, 5] + " " + data[i, 6]) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-search-student-with-result-col4' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='facultycode-col-s'>" + data[i, 7] + "</span>- " + data[i, 8] +
                    "       </div>" + 
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-search-student-with-result-col5' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col-s'>" + data[i, 9] + "</span>- " + (data[i, 10] + groupNum) +
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
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "studentwithresult", eCPUtil.ROW_PER_PAGE) + "</div>" +
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

    public static string SearchStudentWithResult() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-student-with-result'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-search-student' onblur=Trim('id-name-search-student'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-keyword23-label'>" +
            "                   <div class='form-label-style'>คณะและลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-keyword23-input'>" +
                                eCPUtil.ListFaculty(true, "facultysearchstudent") +
            "                   <div id='list-program-search-student'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchStudentWithResult()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmSearchStudentWithResult()'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "   <div id='list-search-student-with-result'>" +
            "       <div class='tab-line'></div>" +
            "       <div class='content-data-tab-content'>" +
            "           <div class='content-right'>" +
            "               <div class='content-data-tab-content-msg' id='record-count-student-with-result'>ค้นหาพบ 0 รายการ</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div class='tab-line'></div>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='tab1-table-head-search-student-with-result-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-search-student-with-result-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-search-student-with-result-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-search-student-with-result-col4'>" +
            "                       <div class='table-head-line1'>คณะ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-search-student-with-result-col5'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "       <div>" +
            "           <div id='box-list-data-search-student-with-result'>" +
            "               <div id='list-data-search-student-with-result'></div>" +
            "           </div>" +
            "           <div id='nav-page-search-student-with-result'></div>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPTransBreakContract() {
        string html = string.Empty;
        
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = (int.Parse(eCPCookie["Pid"]) - 1);
        int order = 0;

        if (eCPCookie["UserSection"].Equals("1"))
            order = 0;

        if (eCPCookie["UserSection"].Equals("2"))
            order = 1;

        if (eCPCookie["UserSection"].Equals("3"))
            order = 2;

        html += (
            "<div class='form-content' id='search-cp-trans-break-contract'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-break-contract-keyword1-label'>" +
            "                   <div class='form-label-style'>สถานะรายการแจ้ง</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกสถานะรายการแจ้งที่ต้องการ</div>" +
            "                       <div class='form-discription-line2-style'>ค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-break-contract-keyword1-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='trackingstatus-trans-break-contract'>" +
            "                           <option value='0'>เลือกสถานะรายการแจ้ง</option>"
        );

        Array trackingStatus = null;

        if (section.Equals(1) &&
            eCPUtil.pageOrder[order, pid].Equals("CPTransRequireContract"))
            trackingStatus = eCPUtil.trackingStatusORLA;

        if ((section.Equals(1) && eCPUtil.pageOrder[order, pid].Equals("CPTransBreakContract")) ||
            section.Equals(2))
            trackingStatus = eCPUtil.trackingStatusORAA;


        for (int i = 0; i < trackingStatus.GetLength(0); i++) {
            html += (
                "                       <option value='" + trackingStatus.GetValue(i, 1) + "'>" + trackingStatus.GetValue(i, 0) + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-break-contract-keyword2-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-break-contract-keyword2-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-trans-break-contract' onblur=Trim('id-name-trans-break-contract'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-break-contract-keyword34-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-break-contract-keyword34-input'>" +
                                eCPUtil.ListFaculty(true, "facultytransbreakcontract") +
            "                   <div id='list-program-trans-break-contract'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-break-contract-keyword5-label'>" +
            "                   <div class='form-label-style'>ช่วงวันที่" + (section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้ง</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่" + (section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้งนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ผิดสัญญาที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-break-contract-keyword5-input'>" +
            "                   <div class='content-left' id='date-start-trans-break-contract-label'>ระหว่างวันที่</div>" +
            "                   <div class='content-left' id='date-start-trans-break-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-start-trans-break-contract' readonly value='' />" +
            "                   </div>" +
            "                   <div class='content-left' id='date-end-trans-break-contract-label'>ถึงวันที่</div>" +
            "                   <div class='content-left' id='date-end-trans-break-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-end-trans-break-contract' readonly value='' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPTransBreakContract()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPTransBreakContract('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPTransRepayContract() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-trans-repay-contract'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-repay-contract-keyword1-label'>" +
            "                   <div class='form-label-style'>สถานะการแจ้งชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกสถานะการแจ้งชำระหนี้ที่ต้องการ</div>" +
            "                       <div class='form-discription-line2-style'>ค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-repay-contract-keyword1-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='repaystatus-trans-repay-contract'>" +
            "                           <option value='0'>เลือกสถานะการแจ้งชำระหนี้</option>"
        );

        for (int i = 0; i < eCPUtil.repayStatusDetail.GetLength(0); i++) {
            html += (
                "                       <option value='" + (i + 1) + "'>" + eCPUtil.repayStatusDetail[i] + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-repay-contract-keyword2-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-repay-contract-keyword2-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-trans-repay-contract' onblur=Trim('id-name-trans-repay-contract'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-repay-contract-keyword34-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-repay-contract-keyword34-input'>" +
                                eCPUtil.ListFaculty(true, "facultytransrepaycontract") +
            "                   <div id='list-program-trans-repay-contract'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-repay-contract-keyword5-label'>" +
            "                   <div class='form-label-style'>ช่วงวันที่รับรายการแจ้ง</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับรายการแจ้งนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ผิดสัญญาที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-repay-contract-keyword5-input'>" +
            "                   <div class='content-left' id='date-start-trans-repay-contract-label'>ระหว่างวันที่</div>" +
            "                   <div class='content-left' id='date-start-trans-repay-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-start-trans-repay-contract' readonly value='' />" +
            "                   </div>" +
            "                   <div class='content-left' id='date-end-trans-repay-contract-label'>ถึงวันที่</div>" +
            "                   <div class='content-left' id='date-end-trans-repay-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-end-trans-repay-contract' readonly value='' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPTransRepayContract()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPTransRepayContract('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPTransPayment() {
        string html = string.Empty;
        int i;

        html += (
            "<div class='form-content' id='search-cp-trans-payment'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-payment-keyword1-label'>" +
            "                   <div class='form-label-style'>สถานะการชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกสถานะการชำระหนี้ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-payment-keyword1-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='paymentstatus-trans-payment'>" +
            "                           <option value='0'>เลือกสถานะการชำระหนี้</option>"
        );

        for (i = 0; i < eCPUtil.paymentStatus.GetLength(0); i++) {
            html += (
                "                       <option value='" + (i + 1) + "'>" + eCPUtil.paymentStatus[i] + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-payment-keyword6-label'>" +
            "                   <div class='form-label-style'>สถานะการบันทึกข้อมูลการชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกสถานะการบันทึกข้อมูลการชำระหนี้ที่ต้องการ</div>" +
            "                       <div class='form-discription-line2-style'>ค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-payment-keyword6-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='paymentrecordstatus-trans-payment'>" +
            "                           <option value='0'>เลือกสถานะการบันทึกข้อมูลการชำระหนี้</option>"
        );

        for (i = 0; i < eCPUtil.paymentRecordStatus.GetLength(0); i++) {
            html += (
                "                       <option value='" + eCPUtil.paymentRecordStatus[i, 1] + "'>" + eCPUtil.paymentRecordStatus[i, 0] + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-payment-keyword2-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-payment-keyword2-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-trans-payment' onblur=Trim('id-name-trans-payment'); value='' style='width:357px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-payment-keyword34-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-payment-keyword34-input'>" +
                                eCPUtil.ListFaculty(true, "facultytranspayment") +
            "                   <div id='list-program-trans-payment'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-trans-payment-keyword5-label'>" +
            "                   <div class='form-label-style'>ช่วงวันที่รับเอกสารการทวงถามตอบกลับครั้งที่ 1</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับเอกสารการทวงถามตอบกลับครั้งที่ 1</div>" +
            "                       <div class='form-discription-line2-style'>หลังจากที่แจ้งชำระหนี้ให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
            "                       <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายทราบ</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-trans-payment-keyword5-input'>" +
            "                   <div class='content-left' id='date-start-trans-repay1-reply-label'>ระหว่างวันที่</div>" +
            "                   <div class='content-left' id='date-start-trans-repay1-reply-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-start-trans-repay1-reply' readonly value='' />" +
            "                   </div>" +
            "                   <div class='content-left' id='date-end-trans-repay1-reply-label'>ถึงวันที่</div>" +
            "                   <div class='content-left' id='date-end-trans-repay1-reply-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-end-trans-repay1-reply' readonly value='' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPTransPayment()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPTransPayment('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportTableCalCapitalAndInterest() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-table-cal-capital-and-interest'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-table-cal-capital-and-interest-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-table-cal-capital-and-interest-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-table-cal-capital-and-interest' onblur=Trim('id-name-report-table-cal-capital-and-interest'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-table-cal-capital-and-interest-keyword23-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-table-cal-capital-and-interest-keyword23-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreporttablecalcapitalandinterest") +
            "                   <div id='list-program-report-table-cal-capital-and-interest'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportTableCalCapitalAndInterest()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportTableCalCapitalAndInterest('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportStepOfWork() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-step-of-work'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-step-of-work-keyword1-label'>" +
            "                   <div class='form-label-style'>สถานะขั้นตอนการดำเนินงาน</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกสถานะขั้นตอนการดำเนินงาน</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-step-of-work-keyword1-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='stepofworkstatus-report-step-of-work'>" +
            "                           <option value='0'>เลือกสถานะขั้นตอนการดำเนินงาน</option>"
        );

        for (int i = 0; i < eCPUtil.stepOfWorkStatus.GetLength(0); i++) {
            html += (
                "                       <option value='" + eCPUtil.stepOfWorkStatus.GetValue(i, 1) + "'>" + eCPUtil.stepOfWorkStatus.GetValue(i, 0) + "</option>"
            );
        }
  
        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-step-of-work-keyword2-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-step-of-work-keyword2-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-step-of-work' onblur=Trim('id-name-report-step-of-work'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-step-of-work-keyword34-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-step-of-work-keyword34-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreportstepofwork") +
            "                   <div id='list-program-report-step-of-work'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportStepOfWork()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportStepOfWork('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportNoticeRepayComplete() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-notice-repay-complete'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-notice-repay-complete-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-notice-repay-complete-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-notice-repay-complete' onblur=Trim('id-name-report-notice-repay-complete'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-notice-repay-complete-keyword23-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-notice-repay-complete-keyword23-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreportnoticerepaycomplete") +
            "                   <div id='list-program-report-notice-repay-complete'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportNoticeRepayComplete()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportNoticeRepayComplete('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportNoticeClaimDebt() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-notice-claim-debt'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-notice-claim-debt-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-notice-claim-debt-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-notice-claim-debt' onblur=Trim('id-name-report-notice-claim-debt'); value='' style='width:411px' />" +
            "               </div>" +
            "          </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-notice-claim-debt-keyword23-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-notice-claim-debt-keyword23-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreportnoticeclaimdebt") +
            "                   <div id='list-program-report-notice-claim-debt'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportNoticeClaimDebt()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportNoticeClaimDebt('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportStatisticPaymentByDate() {
        string html = string.Empty;
        
        html += (
            "<div class='form-content' id='search-cp-report-statistic-payment-by-date'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-statistic-payment-by-date-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-statistic-payment-by-date' onblur=Trim('id-name-report-statistic-payment-by-date'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-statistic-payment-by-date-keyword23-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword23-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreportstatisticpaymentbydate") +
            "                   <div id='list-program-report-statistic-payment-by-date'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-statistic-payment-by-date-keyword4-label'>" +
            "                   <div class='form-label-style'>รูปแบบการชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกรูปการชำระหนี้ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword4-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='formatpaymentreportstatisticpaymentbydate'>" +
            "                           <option value='0'>เลือกรูปแบบการชำระหนี้</option>"
        );
                        
        for (int i = 0; i < eCPUtil.paymentFormat.GetLength(0); i++) {
            html += (
                "                       <option value='" + ((i + 1) + ";" + eCPUtil.paymentFormat[i]) + "'>" + eCPUtil.paymentFormat[i] + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-statistic-payment-by-date-keyword5-label'>" +
            "                   <div class='form-label-style'>ช่วงวันที่ชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่ชำระหนี้ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword5-input'>" +
            "                   <div class='content-left' id='date-start-report-statistic-payment-by-date-label'>ระหว่างวันที่</div>" +
            "                   <div class='content-left' id='date-start-report-statistic-payment-by-date-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-start-report-statistic-payment-by-date' readonly value='' />" +
            "                   </div>" +
            "                   <div class='content-left' id='date-end-report-statistic-payment-by-date-label'>ถึงวันที่</div>" +
            "                   <div class='content-left' id='date-end-report-statistic-payment-by-date-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-end-report-statistic-payment-by-date' readonly value='' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportStatisticPaymentByDate()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportStatisticPaymentByDate('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportEContract() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-e-contract'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-e-contract-keyword1-label'>" +
            "                   <div class='form-label-style'>ปีการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกปีการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-e-contract-keyword1-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='acadamicyear-report-e-contract'>" +
            "                           <option value='0'>เลือกปีการศึกษา</option>"
        );

        for (int i = 2550; i <= int.Parse(DateTime.Parse(Util.CurrentDate("MM/dd/yyyy")).ToString("yyyy", new CultureInfo("th-TH"))); i++) {
            html += (
                "                       <option value='" + i + "'>" + i + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-e-contract-keyword2-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-e-contract-keyword2-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-e-contract' onblur=Trim('id-name-report-e-contract'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-e-contract-keyword34-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-e-contract-keyword34-input'>" +
                                eCPUtil.ListFaculty(true, "facultyreportecontract") +
            "                   <div id='list-program-report-e-contract'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportEContract()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportEContract('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string SearchCPReportDebtorContract() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-cp-report-debtor-contract'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-cp-report-debtor-contract-keyword1-label'>" +
            "                   <div class='form-label-style'>" +
            "                       ช่วงวันที่<span class='report-debtor-contract-order1'>รับสภาพหนี้</span><span class='report-debtor-contract-order2'>ชำระหนี้</span>" +
            "                   </div>" +
            "                   <div class='form-discription-style'>" +
            "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับสภาพหนี้ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-cp-report-debtor-contract-keyword1-input'>" +
            "                   <div class='content-left' id='date-start-report-debtor-contract-label'>ระหว่างวันที่</div>" +
            "                   <div class='content-left' id='date-start-report-debtor-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-start-report-debtor-contract' readonly value='' />" +
            "                   </div>" +
            "                   <div class='content-left' id='date-end-report-debtor-contract-label'>ถึงวันที่</div>" +
            "                   <div class='content-left' id='date-end-report-debtor-contract-input'>" +
            "                       <input class='inputbox calendar' type='text' id='date-end-report-debtor-contract' readonly value='' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchCPReportDebtorContract()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchCPReportDebtorContract('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }    
    
    public static string SearchStudentDebtorContractByProgram() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='search-student-debtor-contract-by-program'>" +
            "   <div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-debtor-contract-by-program-faculty-label'>" +
            "                   <div class='form-label-style'>คณะ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-debtor-contract-by-program-faculty-input'>" +
            "                   <span id='student-debtor-contract-by-program-faculty'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-debtor-contract-by-program-program-label'>" +
            "                   <div class='form-label-style'>หลักสูตร</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-debtor-contract-by-program-program-input'>" +
            "                   <span id='student-debtor-contract-by-program-program'>&nbsp;</span>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-debtor-contract-by-program-keyword1-label'>" +
            "                   <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
            "                       <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-debtor-contract-by-program-keyword1-input'>" +
            "                   <input class='inputbox' type='text' id='id-name-report-debtor-contract-by-program' onblur=Trim('id-name-report-debtor-contract-by-program'); value='' style='width:411px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='search-student-debtor-contract-by-program-keyword2-label'>" +
            "                   <div class='form-label-style'>รูปแบบการชำระหนี้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกรูปการชำระหนี้ที่ต้องการค้นหา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='search-student-debtor-contract-by-program-keyword2-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='formatpaymentreportdebtorcontractbyprogram'>" +
            "                           <option value='0'>เลือกรูปแบบการชำระหนี้</option>"
        );
                        
        for (int i = 0; i < eCPUtil.paymentFormat.GetLength(0); i++) {
            html += (
                "                       <option value='" + ((i + 1) + ";" + eCPUtil.paymentFormat[i]) + "'>" + eCPUtil.paymentFormat[i] + "</option>"
            );
        }

        html += (
            "                       </select>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ValidateSearchStudentDebtorContractByProgram()'>ค้นหา</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ResetFrmSearchStudentDebtorContractByProgram('clear')>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}