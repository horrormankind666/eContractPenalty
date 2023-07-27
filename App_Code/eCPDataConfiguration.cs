/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๒๘/๐๕/๒๕๖๖>
Description : <สำหรับการตั้งค่าระบบ>
=============================================
*/

using System.Web;

public class eCPDataConfiguration {
    public static string AddUpdateCPTabProgram(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string cp1id = (action.Equals("update") ? data[0, 0] : string.Empty);
        string dlevelDefault = (action.Equals("update") ? data[0, 7] : "0");
        string facultyDefault = (action.Equals("update") ? (data[0, 1] + ";" + data[0, 2]) : "0");
        string programDefault = (action.Equals("update") ? (data[0, 3] + ";" + data[0, 4] + ";" + data[0, 5] + ";" + data[0, 6] + ";" + data[0, 7] + ";" + data[0, 8]) : "0");

        html += (
            "<div class='form-content' id='" + action + "-cp-tab-program'>" +
            "   <div id='addupdate-cp-tab-program'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='dlevel-hidden' value='" + dlevelDefault + "' />" +
            "       <input type='hidden' id='faculty-hidden' value='" + facultyDefault + "' />" +
            "       <input type='hidden' id='program-hidden' value='" + programDefault + "' />" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='dlevel-label'>" +
            "                   <div class='form-label-style'>ระดับการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='dlevel-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='dlevel'>" +
            "                           <option value='0'>เลือกระดับการศึกษา</option>"
        );

        for (int i = 0; i < eCPUtil.dlevel.GetLength(0); i++) {
            html += (
                "                       <option value='" + eCPUtil.dlevel[i, 1] + "'>" + eCPUtil.dlevel[i, 0] + "</option>"
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
            "               <div id='faculty-program-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตรที่ให้มีการทำสัญญาการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ให้มีการทำสัญญาการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='faculty-program-input'>" +
                                eCPUtil.ListFaculty(false, "faculty") +
            "                   <div id='list-program'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTabProgram('" + action + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTabProgram(false)'>ล้าง</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li class='hide-button'>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTabProgram('del')>ลบ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-program')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-program')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string AddCPTabProgram() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += (
            AddUpdateCPTabProgram("add", data)
        );

        return html;
    }

    public static string UpdateCPTabProgram(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabProgram(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTabProgram("update", data)
            );

        return html;
    }

    public static string ListCPTabProgram(string[,] data) {
        string html = string.Empty;
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
                groupNum = (!data[i, 6].Equals("0") ? (" ( กลุ่ม " + data[i, 6] + " )") : "");
                callFunc = ("OpenTab('link-tab3-cp-tab-program','#tab3-cp-tab-program','ปรับปรุงหลักสูตร',false,'update','" + data[i, 0] + "','')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='program" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-tab-program-col1' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 8] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-program-col2' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='facultycode-col'>" + data[i, 1] + "</span>- " + data[i, 2] +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-program-col3' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 3] + "</span>- " + (data[i, 4] + groupNum) +
                    "       </div>" +
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

    public static string ListUpdateCPTabProgram() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabProgram("");
        int recordCount = data.GetLength(0);

        html += (
            ListCPTabProgram(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPTabProgram() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabProgram("");
        int recordCount = data.GetLength(0);

        html += (
            "<div id='cp-tab-program-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-program") +
            "       <div class='content-data-tabs' id='tabs-cp-tab-program'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-tab-program' alt='#tab1-cp-tab-program' href='javascript:void(0)'>แสดงหลักสูตร</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-tab-program' alt='#tab2-cp-tab-program' href='javascript:void(0)'>เพิ่มหลักสูตร</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-tab-program' alt='#tab3-cp-tab-program' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-tab-program-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-program'>ค้นหาพบ " + recordCount.ToString("#,##0") + " รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-tab-program-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-tab-program-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-tab-program-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-tab-program-col1'>ระดับการศึกษา</li>" +
            "                   <li class='table-col' id='table-head-cp-tab-program-col2'>คณะ</li>" +
            "                   <li class='table-col' id='table-head-cp-tab-program-col3'>หลักสูตร</li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-program-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-tab-program-contents'></div>" +
            "</div>" +
            "<div id='cp-tab-program-content'>" +
            "   <div class='tab-content' id='tab1-cp-tab-program-content'>" +
            "       <div class='box4' id='list-data-tab-program'>" + ListCPTabProgram(data) + "</div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-program-content'>" +
            "       <div class='box1 addupdate-data-tab-program' id='add-data-tab-program'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-tab-program-content'>" +
            "       <div class='box1 addupdate-data-tab-program' id='update-data-tab-program'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string DetailCPTabCalDate(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabCalDate(cp1id);

        if (data.GetLength(0) > 0) {
            html += (
                "<div class='form-content' id='detail-cp-tab-cal-date'>" +
                "   <div>" +
                "       <div class='form-label-discription-style'>" +
                "           <div class='form-label-style'>" + data[0, 1] + "</div>" +
                "       </div>" +
                "       <div class='form-input-style'>" +
                "           <img src='Image/CalDateFormula" + data[0, 2] + ".png' />" +
                "       </div>" +
                "   </div>" +
                "   <div class='button'>" +
                "       <div class='button-style1'>" +
                "           <ul>" +
                "               <li>" +
                "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
                "               </li>" +
                "           </ul>" +
                "       </div>" +
                "   </div>" +
                "</div>"
            );
        }

        return html;
    }

    public static string ListCPTabCalDate(string[,] data) {
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
                callFunc = ("LoadForm(1,'detailcptabcaldate',true,'','" + data[i, 0] + "','cal-date" + data[i, 0] + "')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='cal-date" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-tab-cal-date-col1' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 1] + "</div>" + 
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-cal-date-col2' onclick=" + callFunc + ">" +
                    "       <div>สูตรที่ " + data[i, 2] + "</div>" +
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

    public static string ListCPTabCalDate() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabCalDate("");
        int recordCount = data.GetLength(0);

        html += (
            "<div id='cp-tab-cal-date-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-cal-date") +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div id='tab1-cp-trans-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'></div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count'>ค้นหาพบ " + recordCount.ToString("#,##0") + " รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'>" +
            "       <div class='table-head'>" +
            "           <ul>" +
            "               <li id='table-head-cp-tab-cal-date-col1'>เงื่อนไขการคิดระยะเวลาตามสัญญา</li>" +
            "               <li class='table-col' id='table-head-cp-tab-cal-date-col2'>สูตรคำนวณเงินชดใช้ตามสัญญา</li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "</div>" +
            "<div id='cp-tab-cal-date-content'>" +
            "   <div class='box4' id='list-data'>" + ListCPTabCalDate(data) + "</div>" +
            "</div>"
        );

        return html;
    }

    private static string AddUpdateCPTabInterest(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string cp1id = (action.Equals("update") ? data[0, 0] : string.Empty);
        string inContractInterestDefault = (action.Equals("update") ? double.Parse(data[0, 1]).ToString("#,##0.00") : string.Empty);
        string outContractInterestDefault = (action.Equals("update") ? double.Parse(data[0, 2]).ToString("#,##0.00") : string.Empty);

        html += (
            "<div class='form-content' id='" + action + "-cp-tab-interest'>" +
            "   <div id='addupdate-cp-tab-interest'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='in-contract-interest-hidden' value='" + inContractInterestDefault + "' />" +
            "       <input type='hidden' id='out-contract-interest-hidden' value='" + outContractInterestDefault + "' />" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='in-contract-interest-label'>" +
            "                   <div class='form-label-style'>ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ดอกเบี้ยเป็นอัตราร้อยละ ( ต่อปี )</div>" +
            "                       <div class='form-discription-line2-style'>ใส่เป็นตัวเลขจุดทศนิยม 2 ตำแหน่ง</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='in-contract-interest-input'>" +
            "                   <input class='inputbox textbox-numeric' type='text' id='in-contract-interest' onblur=Trim('in-contract-interest');AddCommas('in-contract-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:200px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div id='clear-bottom'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='out-contract-interest-label'>" +
            "                   <div class='form-label-style'>ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ดอกเบี้ยเป็นอัตราร้อยละ ( ต่อปี )</div>" +
            "                       <div class='form-discription-line2-style'>ใส่เป็นตัวเลขจุดทศนิยม 2 ตำแหน่ง</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='out-contract-interest-input'>" +
            "                   <input class='inputbox textbox-numeric' type='text' id='out-contract-interest' onblur=Trim('out-contract-interest');AddCommas('out-contract-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:200px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTabInterest('" + action + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTabInterest(false)'>ล้าง</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTabInterest('del')>ลบ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-interest')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-interest')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string AddCPTabInterest() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += (
            AddUpdateCPTabInterest("add", data)
        );

        return html;
    }

    public static string UpdateCPTabInterest(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabInterest(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTabInterest("update", data)
            );

        return html;
    }

    public static string ListCPTabInterest(string[,] data) {
        string html = string.Empty;
        int recordCount = data.GetLength(0);

        if (recordCount > 0) {
            string highlight;
            string check;
            string useContractInterest;
            string callFunc;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < recordCount; i++) {
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");                
                useContractInterest = (int.Parse(data[i, 3]).Equals(1) ? "&radic;" : "&nbsp;");
                check = ((int.Parse(data[i, 3])).Equals(1) ? "checked" : string.Empty);
                callFunc = ("OpenTab('link-tab3-cp-tab-interest','#tab3-cp-tab-interest','ปรับปรุงรายการดอกเบี้ย',false,'update','" + data[i, 0] + "','')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='contract-interest" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-tab-interest-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 1]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-interest-col2' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 2]).ToString("#,##0.00") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-interest-col3'>" +
                    "       <div>" + 
                    "           <input class='checkbox' type='checkbox' name='use-contract-interest' " + check + " onclick=SingleSelectCheckbox(this);UpdateUseContractInterest('" + data[i, 0] + "') />" +
                    "       </div>" +
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

    public static string ListUpdateCPTabInterest() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabInterest("");
        int recordCount = data.GetLength(0);

        html += (
            ListCPTabInterest(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPTabInterest() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabInterest("");
        int recordCount = data.GetLength(0);

        html += (
            "<div id='cp-tab-interest-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-interest") +
            "       <div class='content-data-tabs' id='tabs-cp-tab-interest'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-tab-interest' alt='#tab1-cp-tab-interest' href='javascript:void(0)'>แสดงรายการดอกเบี้ย</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-tab-interest' alt='#tab2-cp-tab-interest' href='javascript:void(0)'>เพิ่มรายการดอกเบี้ย</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-tab-interest' alt='#tab3-cp-tab-interest' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-tab-interest-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-interest'>ค้นหาพบ " + recordCount.ToString("#,##0") + " รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-tab-interest-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-tab-interest-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-tab-interest-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-tab-interest-col1'>" +
            "                       <div class='table-head-line1'>ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา</div>" +
            "                       <div>อัตราร้อยละ ( ต่อปี )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-interest-col2'>" +
            "                       <div class='table-head-line1'>ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา</div>" +
            "                       <div>อัตราร้อยละ ( ต่อปี )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-interest-col3'>" +
            "                       <div class='table-head-line1'>สถานะการใช้งาน</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-interest-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-tab-interest-contents'></div>" +
            "</div>" +
            "<div id='cp-tab-interest-content'>" +
            "   <div class='tab-content' id='tab1-cp-tab-interest-content'>" +
            "       <div class='box4' id='list-data-tab-interest'>" + ListCPTabInterest(data) + "</div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-interest-content'>" +
            "       <div class='box1 addupdate-data-tab-interest' id='add-data-tab-interest'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-tab-interest-content'>" +
            "       <div class='box1 addupdate-data-tab-interest' id='update-data-tab-interest'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string AddUpdateCPTabPayBreakContract(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string cp1id = (action.Equals("update") ? data[0, 0] : string.Empty);
        string dlevelDefault = (action.Equals("update") ? data[0, 8] : "0");
        string caseGgraduateDefault = (action.Equals("update") ? data[0, 10] : "0");
        string facultyDefault = (action.Equals("update") ? data[0, 1] + ";" + data[0, 2] : "0");
        string programDefault = (action.Equals("update") ? (data[0, 3] + ";" + data[0, 4] + ";" + data[0, 5] + ";" + data[0, 6] + ";" + data[0, 8] + ";" + data[0, 9]) : "0");
        string amountCashDefault = (action.Equals("update") ? double.Parse(data[0, 7]).ToString("#,##0") : string.Empty);
        string amtIndemnitorYearDefault = (action.Equals("update") && !data[0, 15].Equals("0") ? double.Parse(data[0, 15]).ToString("#,##0") : string.Empty);
        string calDateConditionDefault = (action.Equals("update") ? data[0, 12] : "0");
        int i;

        html += (
            "<div class='form-content' id='" + action + "-cp-tab-pay-break-contract'>" +
            "   <div id='addupdate-cp-tab-pay-break-contract'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='dlevel-hidden' value='" + dlevelDefault + "' />" +
            "       <input type='hidden' id='case-graduate-hidden' value='" + caseGgraduateDefault + "' />" +
            "       <input type='hidden' id='faculty-hidden' value='" + facultyDefault + "' />" +
            "       <input type='hidden' id='program-hidden' value='" + programDefault + "' />" +
            "       <input type='hidden' id='amount-cash-hidden' value='" + amountCashDefault + "' />" +
            "       <input type='hidden' id='amt-indemnitor-year-hidden' value='" + amtIndemnitorYearDefault + "' />" +
            "       <input type='hidden' id='cal-date-condition-hidden' value='" + calDateConditionDefault + "' />" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='dlevel-label'>" +
            "                   <div class='form-label-style'>ระดับการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='dlevel-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='dlevel'>" +
            "                           <option value='0'>เลือกระดับการศึกษา</option>"
        );

        for (i = 0; i < eCPUtil.dlevel.GetLength(0); i++) {
            html += (
                "                       <option value='" + eCPUtil.dlevel[i, 1] + "'>" + eCPUtil.dlevel[i, 0] + "</option>"
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
            "               <div id='case-graduate-label'>" +
            "                   <div class='form-label-style'>การชดใช้ตามสัญญากรณีก่อน / หลังสำเร็จการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกกรณีการชดใช้ตามสัญญา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='case-graduate-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='case-graduate'>" +
            "                           <option value='0'>เลือกกรณีการชดใช้ตามสัญญา</option>"
        );

        for (i = 0; i < eCPUtil.caseGraduate.GetLength(0); i++) {
            html += (
                "                       <option value='" + (i + 1) + "'>" + eCPUtil.caseGraduate[i, 0] + "</option>"
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
            "               <div id='faculty-program-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตรที่ต้องทำสัญญา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องทำสัญญา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='faculty-program-input'>" +
                                eCPUtil.ListFaculty(true, "facultycptabprogram") +
            "                   <div id='list-program'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='amount-cash-label'>" +
            "                   <div class='form-label-style'>จำนวนเงินชดใช้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่จำนวนเงินชดใช้ ( บาท )</div>" +
            "                       <div class='form-discription-line2-style'>กรณีก่อนสำเร็จการศึกษา ใส่จำนวนเงินชดใช้ต่อเดือน</div>" +
            "                       <div class='form-discription-line3-style'>กรณีหลังสำเร็จการศึกษา ใส่จำนวนเงินชดใช้ตามสัญญา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='amount-cash-input'>" +
            "                   <input class='inputbox textbox-numeric' type='text' id='amount-cash' onblur=Trim('amount-cash');AddCommas('amount-cash',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='amt-indemnitor-year-label'>" +
            "                   <div class='form-label-style'>ระยะเวลาทำงานชดใช้หลังสำเร็จการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ระยะเวลาทำงานชดใช้ ( ปี )</div>" +
            "                       <div class='form-discription-line2-style'>กรณีหลังสำเร็จการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='amt-indemnitor-year-input'>" +
            "                   <div id='set-amt-indemnitor-year'>" +
            "                       <div>" +
            "                           <div class='content-left' id='amt-indemnitor-year-no-input'>" +
            "                               <input class='radio' type='radio' name='set-amt-indemnitor-year' value='N' />" +
            "                           </div>" +
            "                           <div class='content-left' id='amt-indemnitor-year-no-label'>ไม่กำหนด</div>" +
            "                       </div>" +
            "                       <div class='clear'></div>" +
            "                       <div>" +
            "                           <div class='content-left' id='amt-indemnitor-year-yes-input'>" +
            "                               <input class='radio' type='radio' name='set-amt-indemnitor-year' value='Y' />" +
            "                           </div>" +
            "                           <div class='content-left' id='amt-indemnitor-year-yes-label'>กำหนด</div>" +
            "                       </div>" +
            "                       <div class='clear'></div>" +
            "                   </div>" +
            "                   <input class='inputbox textbox-numeric' type='text' id='amt-indemnitor-year' onblur=Trim('amt-indemnitor-year');AddCommas('amt-indemnitor-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div id='clear-bottom'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='cal-date-condition-label'>" +
            "                   <div class='form-label-style'>วิธีคิดและคำนวณเงินชดใช้</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกเงื่อนไขการคิดระยะเวลาตามสัญญาและ</div>" +
            "                       <div class='form-discription-line2-style'>สูตรคำนวณเงินชดใช้ตามสัญญา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='cal-date-condition-input'>" +                                    
            "                   <div>" +
            "                       <div id='input-cal-date'>" + eCPUtil.ListCalDate("cal-date-condition") + "</div>" +
            "                       <div id='view-cal-date'>" +
            "                           <div class='button-style2'>" +
            "                               <ul>" +
            "                                   <li>" +
            "                                       <a href='javascript:void(0)' onclick=ViewCalDate('')>ดูสูตรคำนวณ</a>" +
            "                                   </li>" +
            "                               </ul>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTabPayBreakContract('" + action + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTabPayBreakContract(false)'>ล้าง</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTabPayBreakContract('del')>ลบ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-pay-break-contract')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-pay-break-contract')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string AddCPTabPayBreakContract() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += (
            AddUpdateCPTabPayBreakContract("add", data)
        );

        return html;
    }

    public static string UpdateCPTabPayBreakContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabPayBreakContract(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTabPayBreakContract("update", data)
            );

        return html;
    }

    public static string ListSearchCPTabPayBreakContract(HttpContext c) {
        string html = string.Empty;
        string[,] data = eCPDB.ListSearchCPTabPayBreakContract(c);
        int recordCount = data.GetLength(0);
        int error;

        if (recordCount > 0) {
            error = 0;
            html += (
                (data[0, 1].ToString() + ";" + double.Parse(data[0, 2]).ToString("#,##0") + ";" + double.Parse(data[0, 3]).ToString("#,##0"))
            );
        }
        else
            error = 1;

        return (
            "<error>" + error + "<error>" +
            "<list>" + html + "<list>"
        );
    }

    public static string ListCPTabPayBreakContract(string[,] data) {
        string html = string.Empty;
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
                groupNum = (!data[i, 6].Equals("0") ? (" ( กลุ่ม " + data[i, 6] + " )") : "");
                callFunc = ("OpenTab('link-tab3-cp-tab-pay-break-contract','#tab3-cp-tab-pay-break-contract','ปรับปรุงเกณฑ์การชดใช้',false,'update','" + data[i, 0] + "','')");
                
                html += (
                    "<ul class='table-row-content " + highlight + "' id='pay-break-contract" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-tab-pay-break-contract-col1' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 11] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-pay-break-contract-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 9] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-pay-break-contract-col3' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 3] + "</span>- " + (data[i, 4] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-pay-break-contract-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 7]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-pay-break-contract-col5' onclick=" + callFunc + ">" +
                    "       <div>" + (data[i, 15].Equals("0") ? "-" : double.Parse(data[i, 15]).ToString("#,##0")) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-pay-break-contract-col6' onclick=" + callFunc + ">" +
                    "       <div>วิธีคิดที่ " + data[i, 12] + "</div>" +
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

    public static string ListUpdateCPTabPayBreakContract() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabPayBreakContract("");
        int recordCount = data.GetLength(0);

        html += (
            ListCPTabPayBreakContract(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPTabPayBreakContract() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabPayBreakContract("");
        int recordCount = data.GetLength(0);        

        html += (
            "<div id='cp-tab-pay-break-contract-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-pay-break-contract") +
            "       <div class='content-data-tabs' id='tabs-cp-tab-pay-break-contract'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-tab-pay-break-contract' alt='#tab1-cp-tab-pay-break-contract' href='javascript:void(0)'>แสดงเกณฑ์การชดใช้</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-tab-pay-break-contract' alt='#tab2-cp-tab-pay-break-contract' href='javascript:void(0)'>เพิ่มเกณฑ์การชดใช้</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-tab-pay-break-contract' alt='#tab3-cp-tab-pay-break-contract' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-tab-pay-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-pay-break-contract'>ค้นหาพบ " + recordCount.ToString("#,##0") + " รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-tab-pay-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-tab-pay-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-tab-pay-break-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-tab-pay-break-contract-col1'>" +
            "                       <div class='table-head-line1'>ก่อน / หลัง</div>" +
            "                       <div>สำเร็จการศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-pay-break-contract-col2'>" +
            "                       <div class='table-head-line1'>ระดับการศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-pay-break-contract-col3'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-pay-break-contract-col4'>" +
            "                       <div class='table-head-line1'>จำนวนเงินชดใช้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>( บาท )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-pay-break-contract-col5'>" +
            "                       <div class='table-head-line1'>ระยะเวลาชดใช้</div>" +
            "                       <div>หลังสำเร็จการศึกษา</div>" +
            "                       <div>( ปี )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-pay-break-contract-col6'>" +
            "                       <div class='table-head-line1'>วิธีคิดและ</div>" +
            "                       <div>คำนวณเงินชดใช้</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-pay-break-contract-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-tab-pay-break-contract-contents'></div>" +
            "</div>" +
            "<div id='cp-tab-pay-break-contract-content'>" +
            "   <div class='tab-content' id='tab1-cp-tab-pay-break-contract-content'>" +
            "       <div class='box4' id='list-data-tab-pay-break-contract'>" + ListCPTabPayBreakContract(data) + "</div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-pay-break-contract-content'>" +
            "       <div class='box1 addupdate-data-tab-pay-break-contract' id='add-data-tab-pay-break-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-tab-pay-break-contract-content'>" +
            "       <div class='box1 addupdate-data-tab-pay-break-contract' id='update-data-tab-pay-break-contract'></div>" +
            "   </div>" +                 
            "</div>"
        );

        return html;
    }

    public static string AddUpdateCPTabScholarship(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string cp1id = (action.Equals("update") ? data[0, 0] : string.Empty);
        string dlevelDefault = (action.Equals("update") ? data[0, 8] : "0");
        string facultyDefault = (action.Equals("update") ? (data[0, 1] + ";" + data[0, 2]) : "0");
        string programDefault = (action.Equals("update") ? (data[0, 3] + ";" + data[0, 4] + ";" + data[0, 5] + ";" + data[0, 6] + ";" + data[0, 8] + ";" + data[0, 9]) : "0");
        string scholarshipMoneyDefault = (action.Equals("update") ? double.Parse(data[0, 7]).ToString("#,##0") : string.Empty);

        html += (
            "<div class='form-content' id='" + action + "-cp-tab-scholarship'>" +
            "   <div id='addupdate-cp-tab-scholarship'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='dlevel-hidden' value='" + dlevelDefault + "' />" +
            "       <input type='hidden' id='faculty-hidden' value='" + facultyDefault + "' />" +
            "       <input type='hidden' id='program-hidden' value='" + programDefault + "' />" +
            "       <input type='hidden' id='scholarship-money-hidden' value='" + scholarshipMoneyDefault + "' />" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='dlevel-label'>" +
            "                   <div class='form-label-style'>ระดับการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='dlevel-input'>" +
            "                   <div class='combobox'>" +
            "                       <select id='dlevel'>" +
            "                           <option value='0'>เลือกระดับการศึกษา</option>"
        );

        for (int i = 0; i < eCPUtil.dlevel.GetLength(0); i++) {
            html += (
                "                       <option value='" + eCPUtil.dlevel[i, 1] + "'>" + eCPUtil.dlevel[i, 0] + "</option>"
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
            "               <div id='faculty-program-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตรที่ให้ทุนการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ให้ทุนการศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='faculty-program-input'>" +
                                eCPUtil.ListFaculty(true, "facultycptabprogram") +
            "                   <div id='list-program'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div id='clear-bottom'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='scholarship-money-label'>" +
            "                   <div class='form-label-style'>จำนวนเงินทุนการศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่จำนวนเงินทุนการศึกษา ( บาท / หลักสูตร )</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='scholarship-money-input'>" +
            "                   <input class='inputbox textbox-numeric' type='text' id='scholarship-money' onblur=Trim('scholarship-money');AddCommas('scholarship-money',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTabScholarship('" + action + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTabScholarship(false)'>ล้าง</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li class='hide-button'>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTabScholarship('del')>ลบ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-scholarship')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-scholarship')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    public static string AddCPTabScholarship() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += (
            AddUpdateCPTabScholarship("add", data)
        );

        return html;
    }

    public static string UpdateCPTabScholarship(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabScholarship(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTabScholarship("update", data)
            );

        return html;
    }

    public static string ListSearchCPTabScholarship(HttpContext c) {
        string html = string.Empty;
        string[,] data = eCPDB.ListSearchCPTabScholarship(c);
        int recordCount = data.GetLength(0);
        int error;

        if (recordCount > 0) {
            error = 0;
            html += (
                double.Parse(data[0, 1]).ToString("#,##0")
            );
        }
        else
            error = 1;

        return (
            "<error>" + error + "<error>" +
            "<list>" + html + "<list>"
        );
    }

    public static string ListCPTabScholarship(string[,] data) {
        string html = string.Empty;
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
                groupNum = (!data[i, 6].Equals("0") ? (" ( กลุ่ม " + data[i, 6] + " )") : "");
                callFunc = ("OpenTab('link-tab3-cp-tab-scholarship','#tab3-cp-tab-scholarship','ปรับปรุงรายการทุนการศึกษา',false,'update','" + data[i, 0] + "','')");
               
                html += (
                    "<ul class='table-row-content " + highlight + "' id='scholarship" + data[i, 0] + "'>" +
                    "   <li id='table-content-cp-tab-scholarship-col1' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 9] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-scholarship-col2' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='facultycode-col'>" + data[i, 1] + "</span>- " + data[i, 2] +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-scholarship-col3' onclick=" + callFunc + ">" +
                    "       <div>" +
                    "           <span class='programcode-col'>" + data[i, 3] + "</span>- " + (data[i, 4] + groupNum) +
                    "       </div>" +
                    "   </li>" +
                    "   <li class='table-col' id='table-content-cp-tab-scholarship-col4' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 7]).ToString("#,##0") + "</div>" +
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
    
    public static string ListUpdateCPTabScholarship() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabScholarship("");
        int recordCount = data.GetLength(0);

        html += (
            ListCPTabScholarship(data)
        );

        return (
            "<recordcount>" + recordCount + "<recordcount>" +
            "<list>" + html + "<list>"
        );
    }

    public static string TabCPTabScholarship() {
        string html = string.Empty;
        string[,] data = eCPDB.ListCPTabScholarship("");
        int recordCount = data.GetLength(0);

        html += (
            "<div id='cp-tab-scholarship-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-scholarship") +
            "       <div class='content-data-tabs' id='tabs-cp-tab-scholarship'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-tab-scholarship' alt='#tab1-cp-tab-scholarship' href='javascript:void(0)'>แสดงรายการทุนการศึกษา</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-tab-scholarship' alt='#tab2-cp-tab-scholarship' href='javascript:void(0)'>เพิ่มรายการทุนการศึกษา</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-tab-scholarship' alt='#tab3-cp-tab-scholarship' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-tab-scholarship-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-scholarship'>ค้นหาพบ " + recordCount.ToString("#,##0") + " รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-tab-scholarship-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-tab-scholarship-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-tab-scholarship-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='table-head-cp-tab-scholarship-col1'>" +
            "                       <div class='table-head-line1'>ระดับการศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-scholarship-col2'>" +
            "                       <div class='table-head-line1'>คณะ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-scholarship-col3'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='table-head-cp-tab-scholarship-col4'>" +
            "                       <div class='table-head-line1'>จำนวนเงินทุนการศึกษา</div>" +
            "                       <div>( บาท / หลักสูตร )</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-scholarship-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-tab-scholarship-contents'></div>" +
            "</div>" +
            "<div id='cp-tab-scholarship-content'>" +
            "   <div class='tab-content' id='tab1-cp-tab-scholarship-content'>" +
            "       <div class='box4' id='list-data-tab-scholarship'>" + ListCPTabScholarship(data) + "</div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-scholarship-content'>" +
            "       <div class='box1 addupdate-data-tab-scholarship' id='add-data-tab-scholarship'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-tab-scholarship-content'>" +
            "       <div class='box1 addupdate-data-tab-scholarship' id='update-data-tab-scholarship'></div>" +
            "   </div>" +
            "</div>"
        );        

        return html;
    }

    public static string ListSearchScholarshipAndPayBreakContract(HttpContext c) {
        string html = string.Empty;
        string[,] data;
        int recordCount;
        string result = ";";

        if (c.Request["scholar"].Equals("1")) {
            data = eCPDB.ListSearchCPTabScholarship(c);
            recordCount = data.GetLength(0);          
            result = (recordCount > 0 ? (double.Parse(data[0, 1]).ToString("#,##0") + ";") : result);
        }
        
        html += (
            result
        );
        
        result = ";;";

        if (!c.Request["casegraduate"].Equals("0")) {
            data = eCPDB.ListSearchCPTabPayBreakContract(c);
            recordCount = data.GetLength(0);
            result = (recordCount > 0 ? (data[0, 1].ToString() + ";" + double.Parse(data[0, 2]).ToString("#,##0") + ";" + double.Parse(data[0, 3]).ToString("#,##0")) : result);
        }

        html += (
            result
        );

        return ("<list>" + html + "<list>");
    }
}