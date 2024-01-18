/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๑๑/๐๑/๒๕๖๗>
Description : <สำหรับการทำรายการแจ้ง>
=============================================
*/

using System;
using System.Collections;
using System.Web;

public class eCPDataBreakContract {
    public static string AddCommentBreakContract(
        string cp1id,
        string action,
        string from
    ) {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='add-comment-break-contract'>" +
            "   <div class='form-input-style'>" +
            "       <div class='form-input-content' id='comment-reject-input'>" +
            "           <textarea class='textareabox' id='comment-reject' onblur=Trim('comment-reject') style='width:452px;height:90px;'></textarea>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmAddCommentBreakContract('" + cp1id + "','" + action + "','" + from + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCommentBreakContract()'>ล้าง</a>" +
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

    public static string AddProfileStudent() {
        string html = string.Empty;

        html += (
            "<div class='form-content' id='add-profile-students'>" +
            "   <div id='add-profile-student'>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='profile-student-id-label'>" +
            "                   <div class='form-label-style'>รหัสนักศึกษา</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่รหัสนักศึกษาที่ต้องการ หรือกดปุ่ม</div>" +
            "                       <div class='form-discription-line2-style'>ค้นหานักศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='profile-student-id-input'>" +
            "                   <div>" +
            "                       <input class='inputbox' type='text' id='profile-student-id' onblur=Trim('profile-student-id') onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' maxlength='7' value='' style='width:84px' />" +
            "                   </div>" +
            "                   <div id='profile-student-search'>" +
            "                       <div class='button-style2'>" +
            "                           <ul>" +
            "                               <li>" +
            "                                   <a href='javascript:void(0)' onclick=LoadForm(2,'searchstudentwithresult',true,'','','')>ค้นหานักศึกษา</a>" +
            "                               </li>" +
            "                           </ul>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='profile-student-fullname-label'>" +
            "                   <div class='form-label-style'>ชื่อ - นามสกุล</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคำนำหน้าชื่อ และใส่ชื่อกับนามสกุล</div>" +
            "                       <div class='form-discription-line2-style'>ของนักศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='profile-student-fullname-input'>" +
            "                   <div>" +
            "                       <div class='content-left' id='profile-student-titlename-input'>" + eCPUtil.ListTitleName("titlename") + "</div>" +
            "                       <div class='content-left' id='profile-student-firstname-label'>ชื่อ</div>" +
            "                       <div class='content-left' id='profile-student-firstname-input'>" +
            "                           <input class='inputbox' type='text' id='profile-student-firstname' onblur=Trim('profile-student-firstname'); value='' style='width:163px' />" +
            "                       </div>" +
            "                       <div class='content-left' id='profile-student-lastname-label'>นามสกุล</div>" +
            "                       <div class='content-left' id='profile-student-lastname-input'>" +
            "                           <input class='inputbox' type='text' id='profile-student-lastname' onblur=Trim('profile-student-lastname'); value='' style='width:154px' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='profile-student-faculty-program-label'>" +
            "                   <div class='form-label-style'>คณะและหลักสูตร</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรของนักศึกษา</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='profile-student-faculty-program-input'>" +
                                eCPUtil.ListFaculty(true, "facultyprofilestudent") +
            "                   <div id='list-program-profile-student'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ViewStudentInTransBreakContract()'>ตกลง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmAddProfileStudent()'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style2'>" +
            "           <ul>" +
            "               <li>กำลังตรวจสอบ...</li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }

    private static string AddUpdateCPTransBreakContract(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        int i;       
        string cp1id = (action.Equals("update") ? data[0, 1] : string.Empty);
        string studentFullIDDefault = (action.Equals("update") ? (data[0, 2] + " " + data[0, 10].Substring(0, 4) + " / " + data[0, 10].Substring(4, 1)) : string.Empty);
        string studentFullnameThaDefault = (action.Equals("update") ? (data[0, 5] + data[0, 8] + " " + data[0, 9]) : string.Empty);
        string studentFullnameEngDefault = (action.Equals("update") ? ((!string.IsNullOrEmpty(data[0, 6]) && !string.IsNullOrEmpty(data[0, 7])) ? (data[0, 4] + data[0, 6] + " " + data[0, 7]) : "-") : string.Empty);
        string studentDLevelDefault = (action.Equals("update") ? data[0, 17] : string.Empty);
        string studentFacultyDefault = (action.Equals("update") ? (data[0, 13] + " - " + data[0, 14]) : string.Empty);
        string studentProgramDefault = (action.Equals("update") ? (data[0, 10] + " - " + data[0, 11] + (!data[0, 15].Equals("0") ? (" ( กลุ่ม " + data[0, 15] + " )") : "")) : string.Empty);
        string studentIDDefault = (action.Equals("update") ? data[0, 2] : string.Empty);
        string titleNameDefault = (action.Equals("update") ? (data[0, 3] + ";" + data[0, 4] + ";" + data[0, 5]) : string.Empty);
        string firstNameThaDefault = (action.Equals("update") ? data[0, 8] : string.Empty);
        string lastNameThaDefault = (action.Equals("update") ? data[0, 9] : string.Empty);
        string firstNameEngDefault = (action.Equals("update") ? data[0, 6] : string.Empty);
        string lastNameEngDefault = (action.Equals("update") ? data[0, 7] : string.Empty);
        string facultyDefault = (action.Equals("update") ? (data[0, 13] + ";" + data[0, 14]) : string.Empty);
        string programDefault = (action.Equals("update") ? (data[0, 10] + ";" + data[0, 11] + ";" + data[0, 12] + ";" + data[0, 15] + ";" + data[0, 16] + ";" + data[0, 17]) : string.Empty);
        string majorCodeDefault = (action.Equals("update") ? data[0, 12] : string.Empty);
        string groupNumDefault = (action.Equals("update") ? data[0, 15] : string.Empty);
        string dlevelDefault = (action.Equals("update") ? data[0, 16] : string.Empty);
        string pictureFileNameDefault = (action.Equals("update") ? data[0, 44] : string.Empty);
        string pictureFolderNameDefault = (action.Equals("update") ? data[0, 45] : string.Empty);
        string pursuantBookDefault = (action.Equals("update") ? data[0, 18] : string.Empty);
        string pursuantDefault = (action.Equals("update") ? data[0, 19] : string.Empty);
        string pursuantBookDateDefault = (action.Equals("update") ? data[0, 20] : string.Empty);
        string locationDefault = (action.Equals("update") ? data[0, 21] : string.Empty);
        string inputDateDefault = (action.Equals("update") ? data[0, 22] : string.Empty);
        string stateLocationDefault = (action.Equals("update") ? data[0, 23] : string.Empty);
        string stateLocationDateDefault = (action.Equals("update") ? data[0, 24] : string.Empty);
        string contractDateDefault = (action.Equals("update") ? data[0, 25] : string.Empty);
        string contractDateAgreementDefault = (action.Equals("update") ? data[0, 46] : string.Empty);
        string guarantorDefault = (action.Equals("update") ? data[0, 26] : string.Empty);
        string scholarDefault = (action.Equals("update") ? data[0, 27] : "0");
        string scholarshipMoneyDefault = ((action.Equals("update") && !data[0, 28].Equals("0")) ? double.Parse(data[0, 28]).ToString("#,##0") : string.Empty);
        string scholarshipYearDefault = ((action.Equals("update") && !data[0, 29].Equals("0")) ? double.Parse(data[0, 29]).ToString("#,##0") : string.Empty);
        string scholarshipMonthDefault = ((action.Equals("update") && !data[0, 30].Equals("0")) ? double.Parse(data[0, 30]).ToString("#,##0") : string.Empty);
        string educationDateStartDefault = (action.Equals("update") ? data[0, 31] : string.Empty);
        string educationDateEndDefault = (action.Equals("update") ? data[0, 32] : string.Empty);
        string caseGraduateBreakContractDefault = (action.Equals("update") ? data[0, 33] : "0");
        string civilDefault = (action.Equals("update") ? data[0, 34] : "0");
        string contractForceDateStartDefault = (action.Equals("update") ? data[0, 47] : string.Empty);
        string contractForceDateEndDefault = (action.Equals("update") ? data[0, 48] : string.Empty);
        string calDateCondition = (action.Equals("update") ? data[0, 35] : string.Empty);
        string setAmtIndemnitorYear = (action.Equals("update") ? data[0, 51] : string.Empty);
        string indemnitorYearDefault = ((action.Equals("update") && !data[0, 36].Equals("0")) ? double.Parse(data[0, 36]).ToString("#,##0") : string.Empty);
        string indemnitorCashDefault = (action.Equals("update") ? double.Parse(data[0, 37]).ToString("#,##0") : string.Empty);
        string commentEditDefault = (action.Equals("update") ? data[0, 38] : string.Empty);
        string rejectEditDateDefault = (action.Equals("update") ? data[0, 49] : string.Empty);
        string statusEdit = (action.Equals("update") ? data[0, 42] : string.Empty);
        string trackingStatus = (action.Equals("update") ? (data[0, 40] + data[0, 41] + data[0, 42] + data[0, 43]) : string.Empty);

        html += (
            "<div class='form-content' id='" + action + "-cp-trans-break-contract'>" +
            "   <div id='addupdate-cp-trans-break-contract'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='picture-student-hidden' value='" + (!string.IsNullOrEmpty(pictureFileNameDefault) ? (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + pictureFolderNameDefault + "/" + pictureFileNameDefault) : string.Empty) + "' />" +
            "       <input type='hidden' id='student-fullid-hidden' value='" + studentFullIDDefault + "' />" +
            "       <input type='hidden' id='student-fullname-tha-hidden' value='" + studentFullnameThaDefault + "' />" +
            "       <input type='hidden' id='student-fullname-eng-hidden' value='" + studentFullnameEngDefault + "' />" +
            "       <input type='hidden' id='student-dlevel-hidden' value='" + studentDLevelDefault + "' />" +
            "       <input type='hidden' id='student-faculty-hidden' value='" + studentFacultyDefault + "' />" +
            "       <input type='hidden' id='student-program-hidden' value='" + studentProgramDefault + "' />" +
            "       <input type='hidden' id='studentid-hidden' value='" + studentIDDefault + "' />" +
            "       <input type='hidden' id='titlename-hidden' value='" + titleNameDefault + "' />" +
            "       <input type='hidden' id='firstname-tha-hidden' value='" + firstNameThaDefault + "' />" +
            "       <input type='hidden' id='lastname-tha-hidden' value='" + lastNameThaDefault + "' />" +
            "       <input type='hidden' id='firstname-eng-hidden' value='" + firstNameEngDefault + "' />" +
            "       <input type='hidden' id='lastname-eng-hidden' value='" + lastNameEngDefault + "' />" +
            "       <input type='hidden' id='faculty-hidden' value='" + facultyDefault + "' />" +
            "       <input type='hidden' id='program-hidden' value='" + programDefault + "' />" +
            "       <input type='hidden' id='dlevel-hidden' value='" + dlevelDefault + "' />" +
            "       <input type='hidden' id='profile-student-id-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-titlename-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-firstname-tha-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-lastname-tha-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-firstname-eng-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-lastname-eng-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-faculty-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-program-hidden' value='' />" +
            "       <input type='hidden' id='profile-student-dlevel-hidden' value='' />" +
            "       <input type='hidden' id='trackingstatus' value='" + trackingStatus + "' />" +
            "       <input type='hidden' id='pursuant-book-hidden' value='" + pursuantBookDefault + "' />" +
            "       <input type='hidden' id='pursuant-hidden' value='" + pursuantDefault + "' />" +
            "       <input type='hidden' id='pursuant-book-date-hidden' value='" + pursuantBookDateDefault + "' />" +
            "       <input type='hidden' id='location-hidden' value='" + locationDefault + "' />" +
            "       <input type='hidden' id='input-date-hidden' value='" + inputDateDefault + "' />" +
            "       <input type='hidden' id='state-location-hidden' value='" + stateLocationDefault + "' />" +
            "       <input type='hidden' id='state-location-date-hidden' value='" + stateLocationDateDefault + "' />" +
            "       <input type='hidden' id='contract-date-hidden' value='" + contractDateDefault + "' />" +
            "       <input type='hidden' id='contract-date-agreement-hidden' value='" + contractDateAgreementDefault + "' />" +
            "       <input type='hidden' id='guarantor-hidden' value='" + guarantorDefault + "' />" +
            "       <input type='hidden' id='scholar-hidden' value='" + scholarDefault + "' />" +
            "       <input type='hidden' id='scholarship-money-hidden' value='" + scholarshipMoneyDefault + "' />" +
            "       <input type='hidden' id='scholarship-year-hidden' value='" + scholarshipYearDefault + "' />" +
            "       <input type='hidden' id='scholarship-month-hidden' value='" + scholarshipMonthDefault + "' />" +
            "       <input type='hidden' id='education-date-start-hidden' value='" + educationDateStartDefault + "' />" +
            "       <input type='hidden' id='education-date-end-hidden' value='" + educationDateEndDefault + "' />" +
            "       <input type='hidden' id='case-graduate-break-contract-hidden' value='" + caseGraduateBreakContractDefault + "' />" +
            "       <input type='hidden' id='civil-hidden' value='" + civilDefault + "' />" +
            "       <input type='hidden' id='contract-force-date-start-hidden' value='" + contractForceDateStartDefault + "' />" +
            "       <input type='hidden' id='contract-force-date-end-hidden' value='" + contractForceDateEndDefault + "' />" +
            "       <input type='hidden' id='cal-date-condition-hidden' value='" + calDateCondition + "' />" +
            "       <input type='hidden' id='set-amt-indemnitor-year' value='" + setAmtIndemnitorYear + "' />" +
            "       <input type='hidden' id='indemnitor-year-hidden' value='" + indemnitorYearDefault + "' />" +
            "       <input type='hidden' id='indemnitor-cash-hidden' value='" + indemnitorCashDefault + "' />" +
            "       <input type='hidden' id='comment-hidden' value='" + commentEditDefault + "' />" +
            "       <div>" +
            "           <div id='profile-student'>" +
            "               <div class='content-left' id='picture-add-student'>" +
            "                   <div id='picture-student'></div>" +
            "                   <div id='add-student'>" +
            "                       <a id='link-add-student' href='javascript:void(0)' onclick=LoadForm(1,'addprofilestudent',true,'','','')>บันทึกข้อมูลนักศึกษา</a>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-left' id='profile-student-label'>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>รหัสนักศึกษา</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>ชื่อ - นามสกุล ( ภาษาไทย )</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>ชื่อ - นามสกุล ( ภาษาอังกฤษ )</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>ระดับการศึกษา</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>คณะ</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style clear-bottom'>" +
            "                       <div class='form-label-style'>หลักสูตร</div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-left' id='profile-student-input'>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-id'></span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-fullname-tha'></span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-fullname-eng'></span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-dlevel'></span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-faculty'></span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style clear-bottom'>" +
            "                       <div class='form-label-style'>" +
            "                           <span id='student-program'></span>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='box3'></div>" +
            "           <div id='pursuant-detail'>" +
            "               <div>" +
            "                   <div class='form-label-discription-style clear-bottom'>" +
            "                       <div id='pursuant-detail-label'>" +
            "                           <div class='form-label-style'>รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้น</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้น</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style clear-bottom'>" +
            "                       <div class='form-input-content' id='pursuant-detail-input'>" +
            "                           <div>" +
            "                               <div class='content-left' id='pursuant-book-label'>ตามหนังสือ</div>" +
            "                               <div class='content-left' id='pursuant-book-input'>" +
            "                                   <input class='inputbox' type='text' id='pursuant-book' onblur=Trim('pursuant-book'); value='' style='width:376px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='pursuant-label'>เลขที่หนังสือ</div>" +
            "                               <div class='content-left' id='pursuant-input'>" +
            "                                   <input class='inputbox' type='text' id='pursuant' onblur=Trim('pursuant'); value='' style='width:250px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='pursuant-book-date-label'>วันที่</div>" +
            "                               <div class='content-left' id='pursuant-book-date-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='pursuant-book-date' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='location-label'>ม.มหิดลรับที่</div>" +
            "                               <div class='content-left' id='location-input'>" +
            "                                   <input class='inputbox' type='text' id='location' onblur=Trim('location'); value='' style='width:250px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='input-date-label'>วันที่</div>" +
            "                               <div class='content-left' id='input-date-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='input-date' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='state-location-label'>กบศ.รับที่</div>" +
            "                               <div class='content-left' id='state-location-input'>" +
            "                                   <input class='inputbox' type='text' id='state-location' onblur=Trim('state-location'); value='' style='width:250px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='state-location-date-label'>วันที่</div>" +
            "                               <div class='content-left' id='state-location-date-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='state-location-date' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "           <div class='box3'></div>" +
            "           <div id='break-contract'>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='contract-date-guarantor-label'>" +
            "                           <div class='form-label-style'>สัญญานักศึกษา / สัญญาค้ำประกัน</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาใส่วันที่ของสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาในคณะข้างต้น และกรุณาใส่</div>" +
            "                               <div class='form-discription-line2-style'>วันที่ของสัญญาค้ำประกันและชื่อผู้ค้ำประกัน</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='contract-date-guarantor-input'>" +
            "                           <div>" +
            "                               <div class='content-left' id='contract-date-label'>สัญญานักศึกษาลงวันที่</div>" +
            "                               <div class='content-left' id='contract-date-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='contract-date' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='contract-date-agreement-label'>สัญญาค้ำประกันลงวันที่</div>" +
            "                               <div class='content-left' id='contract-date-agreement-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='contract-date-agreement' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='guarantor-label'>ชื่อผู้ค้ำประกัน คือ</div>" +
            "                               <div class='content-left' id='guarantor-input'>" +
            "                                   <input class='inputbox' type='text' id='guarantor' onblur=Trim('guarantor'); value='' style='width:320px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                       <div class='clear'></div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='scholar-label'>" +
            "                           <div class='form-label-style'>สถานะการได้รับทุนการศึกษา</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาเลือกสถานะการได้รับทุนการศึกษา และใส่รายละเอียดของทุนการศึกษา กรณี</div>" +
            "                               <div class='form-discription-line2-style'>ที่ได้รับทุนการศึกษา</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='scholar-input'>" +
            "                           <div id='status-scholar-input'>" +
            "                               <div class='combobox'>" +
            "                                   <select id='scholar'>" +
            "                                       <option value='0'>เลือกสถานะการได้รับทุนการศึกษา</option>"
        );

        for (i = 0; i < eCPUtil.scholar.GetLength(0); i++) {
            html += (
                "                                   <option value='" + (i + 1) + "'>" + eCPUtil.scholar[i] + "</option>"
            );
        }

        html += (
            "                                   </select>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div>" +
            "                               <div class='content-left' id='scholarship-money-label'>จำนวนเงินทุนการศึกษา</div>" +
            "                               <div class='content-left' id='scholarship-money-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-money' onblur=Trim('scholarship-money');AddCommas('scholarship-money',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:115px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-money-unit-label'>บาท / หลักสูตร</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='scholarship-year-month-label'>ระยะเวลาที่ได้รับทุน</div>" +
            "                               <div class='content-left' id='scholarship-year-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-year' onblur=Trim('scholarship-year');AddCommas('scholarship-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:43px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-year-unit-label'>ปี</div>" +
            "                               <div class='content-left' id='scholarship-month-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-month' onblur=Trim('scholarship-month');AddCommas('scholarship-month',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:43px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-month-unit-label'>เดือน</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='case-graduate-label'>" +
            "                           <div class='form-label-style'>สถานะการสำเร็จการศึกษา</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาเลือกสถานะการสำเร็จการศึกษา และใส่วันที่เริ่มต้นเข้าศึกษาและวันที่สำเร็จ</div>" +
            "                               <div class='form-discription-line2-style'>การศึกษา หรือวันที่พ้นสภาพนักศึกษา</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='case-graduate-input'>" +
            "                           <div id='case-graduate-break-contract-input'>" +
            "                               <div class='combobox'>" +
            "                                   <select id='case-graduate-break-contract'>" +
            "                                       <option value='0'>เลือกสถานะการสำเร็จการศึกษา</option>"
        );

        for (i = 0; i < eCPUtil.caseGraduate.GetLength(0); i++) {
            html += (
                "                                   <option value='" + (i + 1) + "'>" + eCPUtil.caseGraduate[i, 1] + "</option>"
            );
        }

        html += (
            "                                   </select>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div>" +
            "                               <div class='content-left' id='education-date-start-label'>เริ่มต้นเข้าศึกษาเมื่อวันที่</div>" +
            "                               <div class='content-left' id='education-date-start-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='education-date-start' readonly value='' />" +
            "                               </div>" +
            "                               <div class='content-left' id='education-date-end-label'>ถึงวันที่</div>" +
            "                               <div class='content-left' id='education-date-end-input'>" +
            "                                   <input class='inputbox calendar' type='text' id='education-date-end' readonly value='' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='case-civil-label'>" +
            "                           <div class='form-label-style'>สถานะการรับราชการ</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาเลือกสถานะการปฏิบัติงานชดใช้ กรณีสำเร็จการศึกษา</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='case-civil-input'>" +
            "                           <div id='case-civil-break-contract-input'>" +
            "                               <div class='combobox'>" +
            "                                   <select id='civil'>" +
            "                                       <option value='0'>เลือกสถานะการปฏิบัติงานชดใช้</option>"
        );

        for (i = 0; i < eCPUtil.civil.GetLength(0); i++) {
            html += (
                "                                   <option value='" + (i + 1) + "'>" + eCPUtil.civil[i] + "</option>"
            );
        }

        html += (
            "                                   </select>" +
            "                               </div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='contract-force-date-label'>" +
            "                           <div class='form-label-style'>วันที่สัญญามีผลบังคับใช้</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>กรุณาใส่วันที่สัญญามีผลบังคับใช้ ( เพื่อนำไปคำนวณระยะเวลาที่ใช้ศึกษา กรณีไม่</div>" +
            "                               <div class='form-discription-line2-style'>สำเร็จการศึกษา กรณีนักศึกษาหลักสูตรพยาบาลศาสตรบัณฑิต ถ้าไม่สำเร็จการ</div>" +
            "                               <div class='form-discription-line3-style'>ศึกษาให้สัญญามีผลบังคับตั้งแต่ปี 3 )</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='contract-force-date-input'>" +
            "                           <div class='content-left' id='contract-force-date-start-label'>สัญญามีผลบังคับใช้วันที่</div>" +
            "                           <div class='content-left' id='contract-force-date-start-input'>" +
            "                               <input class='inputbox calendar' type='text' id='contract-force-date-start' readonly value='' />" +
            "                           </div>" +
            "                           <div class='content-left' id='contract-force-date-end-label'>ถึงวันที่</div>" +
            "                           <div class='content-left' id='contract-force-date-end-input'>" +
            "                               <input class='inputbox calendar' type='text' id='contract-force-date-end' readonly value='' />" +
            "                           </div>" +
            "                       </div>" +
            "                       <div class='clear'></div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "               <div>" +
            "                   <div class='form-label-discription-style " + (statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
            "                       <div id='indemnitor-label'>" +
            "                           <div class='form-label-style'>การขอชดใช้ตามสัญญา</div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style " + (statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
            "                       <div class='form-input-content' id='indemnitor-input'>" +
            "                           <div>" +
            "                               <input type='hidden' id='cal-date-condition' value='' />" +
            "                               <div class='content-left' id='indemnitor-year-label'>ทำงานชดใช้เป็นเวลา</div>" +
            "                               <div class='content-left' id='indemnitor-year-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='indemnitor-year' value='' style='width:43px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='indemnitor-year-unit-label'>ปี</div>" +
            "                               <div class='content-left' id='indemnitor-cash-label'>หรือชดใช้เป็นเงินจำนวน</div>" +
            "                               <div class='content-left' id='indemnitor-cash-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='indemnitor-cash' value='' style='width:121px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='indemnitor-cash-unit-label'>บาท</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>"
        );                 

        if (statusEdit.Equals("2")) {
            html += (
                "       <div class='box3'></div>" +
                "       <div id='comment-detail'>" +
                "           <div>" +
                "               <div class='form-label-discription-style'>" +
                "                   <div id='comment-label'>" +
                "                       <div class='form-label-style'>สาเหตุการส่งกลับแก้ไขรายการแจ้ง</div>" +
                "                       <div class='form-discription-style'>" +
                "                           <div class='form-discription-line1-style'>รายงานสาเหตุหรือเหตุผลที่ส่งรายการแจ้งกลับมาแก้ไข</div>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "               <div class='form-input-style'>" +
                "                   <div class='form-input-content' id='comment-input'>" +
                "                       <div class='textareabox' id='comments'>" +
                "                           <div id='comment'>" +
                "                               ส่งกลับแก้ไขรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(rejectEditDateDefault)) + " สาเหตุ<span id='comment-message'></span>" +
                "                           </div>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>"
            );
        }

        html += (
            "       </div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTransBreakContract('" + action + "')>บันทึก</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(1,'addcommentcancelbreakcontract',true,'','" + cp1id + "','')>ยกเลิกรายการ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTransBreakContract(false)'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-break-contract')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li id='button-status-p'>" +
            "                   <a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + cp1id + "','v1')>พิมพ์แบบตรวจสอบ</a></li>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-break-contract')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
            "<form id='export-setvalue' method='post' target='export-target'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>"
        );

        return html;
    }

    public static string AddCPTransBreakContract() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += (
            AddUpdateCPTransBreakContract("add", data)
        );

        return html;
    }

    public static string UpdateCPTransBreakContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransBreakContract(cp1id);

        if (data.GetLength(0) > 0)
            html += (
                AddUpdateCPTransBreakContract("update", data)
            );

        return html;
    }

    public static string DetailCPTransBreakRequireContract(
        string cp1id,
        string[,] data,
        string status
    ) {
        string html = string.Empty;
        string studentIDDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 2] : data[0, 19];
        string titleNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 5] : data[0, 20];
        string firstNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 8] : data[0, 21];
        string lastNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 9] : data[0, 22];
        string facultyCodeDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 13] : data[0, 26];
        string facultyNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 14] : data[0, 27];
        string programCodeDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 10] : data[0, 23];
        string programNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 11] : data[0, 24];
        string groupNumDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 15] : data[0, 28];
        string dlevelDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 17] : data[0, 30];
        string pictureFileNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 44] : data[0, 56];
        string pictureFolderNameDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 45] : data[0, 57];
        string pursuantBookDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 18] : data[0, 31];
        string pursuantDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 19] : data[0, 32];
        string pursuantBookDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 20] : data[0, 33];
        string locationDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 21] : data[0, 34];
        string inputDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 22] : data[0, 35];
        string stateLocationDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 23] : data[0, 36];
        string stateLocationDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 24] : data[0, 37];
        string contractDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 25] : data[0, 38];
        string contractDateAgreementDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 46] : data[0, 61];
        string guarantorDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 26] : data[0, 39];
        string scholarDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 27] : data[0, 40];
        string scholarshipMoneyDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 28] : data[0, 41];
        string scholarshipYearDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 29] : data[0, 42];
        string scholarshipMonthDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 30] : data[0, 43];
        string educationDateStartDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 31] : data[0, 44];
        string educationDateEndDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 32] : data[0, 45];
        string caseGraduateBreakContractDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 33] : data[0, 46];
        string civilDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 34] : data[0, 47];
        string contractForceDateStartDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 47] : data[0, 62];
        string contractForceDateEndDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 48] : data[0, 63];
        string indemnitorYearDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 36] : data[0, 49];
        string indemnitorCashDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 37] : data[0, 50];
        string commentEditDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 38] : data[0, 59];
        string rejectEditDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 49] : data[0, 64];
        string commentCancelDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 39] : data[0, 60];
        string rejectCancelDateDefault = (status.Equals("v1") || status.Equals("a")) ? data[0, 50] : data[0, 65];
        string statusEdit = (status.Equals("v1") || status.Equals("a")) ? data[0, 42] : data[0, 54];
        string statusCancel = (status.Equals("v1") || status.Equals("a")) ? data[0, 43] : data[0, 55];
        string trackingStatus = (status.Equals("v1") || status.Equals("a")) ? (data[0, 40] + data[0, 41] + data[0, 42] + data[0, 43]) : (data[0, 52] + data[0, 53] + data[0, 54] + data[0, 55]);
        string cp2id = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 1] : string.Empty;
        string setAmtIndemnitorYear = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 77] : string.Empty;
        string indemnitorAddressDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 3] : string.Empty;
        string provinceTNameDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 5] : string.Empty;
        string studyLeaveDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 66] : string.Empty;
        string requireDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 6] : string.Empty;
        string approveDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 7] : string.Empty;
        string beforeStudyLeaveStartDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 67] : string.Empty;
        string beforeStudyLeaveEndDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 68] : string.Empty;
        string studyLeaveStartDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 69] : string.Empty;
        string studyLeaveEndDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 70] : string.Empty;
        string afterStudyLeaveStartDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 71] : string.Empty;
        string afterStudyLeaveEndDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 72] : string.Empty;
        string actualMonthScholarshipDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 8] : string.Empty;
        string actualScholarshipDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 9] : string.Empty;
        string totalPayScholarshipDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 10] : string.Empty;
        string actualMonthDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 11] : string.Empty;
        string actualDayDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 12] : string.Empty;
        string allActualDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 13] : string.Empty;
        string actualDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 14] : string.Empty;
        string remainDateDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 15] : string.Empty;
        string subtotalPenaltyDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 16] : string.Empty;
        string totalPenaltyDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 17] : string.Empty;
        string lawyerFullnameDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 73] : string.Empty;
        string lawyerPhoneNumberDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 74] : string.Empty;
        string lawyerMobileNumberDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 75] : string.Empty;
        string lawyerEmailDefault = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 76] : string.Empty;
        string lawyerDefault = string.Empty;
        string statusRepay = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 18] : string.Empty;
        string statusPayment = (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1")) ? data[0, 58] : string.Empty;
        string[] statusRepayCurrent;

        ArrayList lawyerPhoneNumber = new ArrayList();

        if (!string.IsNullOrEmpty(lawyerPhoneNumberDefault))
            lawyerPhoneNumber.Add(lawyerPhoneNumberDefault);

        if (!string.IsNullOrEmpty(lawyerMobileNumberDefault))
            lawyerPhoneNumber.Add(lawyerMobileNumberDefault);


        if (!string.IsNullOrEmpty(lawyerFullnameDefault) &&
            (!string.IsNullOrEmpty(lawyerPhoneNumberDefault) || !string.IsNullOrEmpty(lawyerMobileNumberDefault) && !string.IsNullOrEmpty(lawyerEmailDefault))
        ) {
            lawyerDefault += (
                "คุณ<span>" + lawyerFullnameDefault + "</span>" + (lawyerPhoneNumber.Count > 0 ? (" ( <span>" + string.Join(", ", lawyerPhoneNumber.ToArray()) + "</span> )") : string.Empty) +
                " อีเมล์ <span>" + lawyerEmailDefault + "</span>"
            );
        }

        html += (
            "<div class='form-content' id='detail-cp-trans-break-require-contract'>" +
            "   <input type='hidden' id='trackingstatus' value='" + trackingStatus + "' />" +
            "   <div id='pursuant-detail-detail'>" +
            "       <div class='form-input-style'>" +
            "           <div class='form-input-content form-label-style'>" +
            "               <div class='content-left' id='pursuant-book-detail-label'>ตามหนังสือ</div>" +
            "               <div class='content-left' id='pursuant-book-detail-input'>" +
            "                   <span>" + pursuantBookDefault + "</span>" +
            "               </div>" +
            "               <div class='content-left' id='pursuant-detail-label'>ที่</div>" +
            "               <div class='content-left' id='pursuant-detail-input'>" +
            "                   <span>" + pursuantDefault + "</span>" +
            "               </div>" +
            "               <div class='content-left' id='pursuant-book-date-detail-label'>วันที่</div>" +
            "               <div class='content-left' id='pursuant-book-date-detail-input'>" +
            "                   <span>" + Util.LongDateTH(pursuantBookDateDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "       <div class='form-input-style'>" +
            "           <div class='form-input-content form-label-style'>" +
            "               <div class='content-left' id='location-detail-label'>ม.มหิดลรับที่</div>" +
            "               <div class='content-left' id='location-detail-input'>" +
            "                   <span>" + locationDefault + "</span>" +
            "               </div>" +
            "               <div class='content-left' id='input-date-detail-label'>วันที่</div>" +
            "               <div class='content-left' id='input-date-detail-input'>" +
            "                   <span>" + Util.LongDateTH(inputDateDefault) + "</span>" +
            "               </div>" +
            "               <div class='content-left' id='state-location-detail-label'>กบศ. รับที่</div>" +
            "               <div class='content-left' id='state-location-detail-input'>" +
            "                   <span>" + stateLocationDefault + "</span>" +
            "               </div>" +
            "              <div class='content-left' id='state-location-date-detail-label'>วันที่</div>" +
            "              <div class='content-left' id='state-location-date-input'>" +
            "                   <span>" + Util.LongDateTH(stateLocationDateDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='box3'></div>" +
            "   <div id='profile-student'>" +
            "       <div class='content-left " + (status.Equals("a") ? "status-a" : string.Empty) + "' id='picture-student'>" +
            "           <div>" +
            "               <img src='" + (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + pictureFolderNameDefault + "/" + pictureFileNameDefault) + "' />" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left " + (status.Equals("a") ? "status-a" : string.Empty) + "' id='profile-student-label'>" +
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
            "           <div class='form-label-discription-style " + (status.Equals("a") ? "clear-bottom" : string.Empty) + "'>" +
            "               <div class='form-label-style'>หลักสูตร</div>" +
            "           </div>"
        );

        if (!status.Equals("a"))
            html += (
                "       <div class='form-label-discription-style clear-bottom'>" +
                "           <div class='form-label-style'>นิติกรผู้รับผิดชอบ</div>" +
                "       </div>"
            );

        html += (
            "       </div>" +
            "       <div class='content-left' id='profile-student-input'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (studentIDDefault + "&nbsp;" + programCodeDefault.Substring(0, 4) + " / " + programCodeDefault.Substring(4, 1)) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (titleNameDefault + firstNameDefault + " " + lastNameDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + dlevelDefault + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (facultyCodeDefault + " - " + facultyNameDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style " + (status.Equals("a") ? "clear-bottom" : string.Empty) + "'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (programCodeDefault + " - " + programNameDefault + (!groupNumDefault.Equals("0") ? (" ( กลุ่ม " + groupNumDefault + " )") : "")) + "</span>" +
            "               </div>" +
            "           </div>"
        );

        if (!status.Equals("a"))
            html += (
                "       <div class='form-label-discription-style clear-bottom'>" +
                "           <div class='form-label-style'>" + lawyerDefault + "</div>" +
                "       </div>"
            );

        html += (
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='box3'></div>" +
            "   <div id='break-contract-detail'>" +
            "       <div class='content-left' id='break-contract-detail-label'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>สัญญานักศึกษาลงวันที่</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>สัญญาค้ำประกันลงวันที่ / ผู้ค้ำประกัน</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>สถานะการได้รับทุนการศึกษา</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>สถานะการสำเร็จการศึกษา</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>วันที่สัญญามีผลบังคับใช้</div>" +
            "           </div>" +
            "           <div class='form-label-discription-style " + (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1") || statusCancel.Equals("2") || statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
            "               <div class='form-label-style'>สถานะการปฏิบัติงานชดใช้ / การขอชดใช้ตามสัญญา</div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='break-contract-detail-input'>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + Util.LongDateTH(contractDateDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (!string.IsNullOrEmpty(contractDateAgreementDefault) ? Util.LongDateTH(contractDateAgreementDefault) : "-") + "</span> / <span>" + guarantorDefault + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + eCPUtil.scholar[int.Parse(scholarDefault) - 1] + "</span> " + (scholarDefault.Equals("1") ? "จำนวนเงิน <span>" + double.Parse(scholarshipMoneyDefault).ToString("#,##0") + "</span> บาท / หลักสูตร ระยะเวลา <span>" + (!scholarshipYearDefault.Equals("0") ? double.Parse(scholarshipYearDefault).ToString("#,##0") : "-") + "</span> ปี <span>" + (!scholarshipMonthDefault.Equals("0") ? double.Parse(scholarshipMonthDefault).ToString("#,##0") : "-") + "</span> เดือน" : "") +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + eCPUtil.caseGraduate[int.Parse(caseGraduateBreakContractDefault) - 1, 1] + "</span> เริ่มเข้าศึกษาเมื่อวันที่ <span>" + Util.LongDateTH(educationDateStartDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(educationDateEndDefault) + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div class='form-label-style'>" +
            "                   สัญญาเริ่มมีผลบังคับใช้เมื่อวันที่ <span>" + (!string.IsNullOrEmpty(contractForceDateStartDefault) ? Util.LongDateTH(contractForceDateStartDefault) : "-") + "</span> ถึงวันที่ <span>" + (!string.IsNullOrEmpty(contractForceDateEndDefault) ? Util.LongDateTH(contractForceDateEndDefault) : "-") + "</span>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-label-discription-style " + (status.Equals("v2") || status.Equals("v3") || status.Equals("r") || status.Equals("r1") || statusCancel.Equals("2") || statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
            "               <div class='form-label-style'>" +
            "                   <span>" + (!civilDefault.Equals("0") ? eCPUtil.civil[int.Parse(civilDefault) - 1] : "-") + "</span> / ทำงานชดใช้เป็นเวลา <span>" + (!indemnitorYearDefault.Equals("0") ? double.Parse(indemnitorYearDefault).ToString("#,##0") : "-") + "</span> ปี หรือชดใช้เงินจำนวน <span>" + double.Parse(indemnitorCashDefault).ToString("#,##0") + "</span> บาท" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>"
        );

        if (status.Equals("v2") ||
            status.Equals("v3") ||
            status.Equals("r") ||
            status.Equals("r1")) {
            statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(cp2id, statusRepay, statusPayment)).Split(new char[] { ';' });

            if (caseGraduateBreakContractDefault.Equals("1")) {
                html += (
                    "<div class='box3'></div>" +
                    "<div id='cal-contract-penalty1'>" +
                    "   <div class='content-left' id='cal-contract-penalty1-label'>" +
                    "       <div class='form-label-discription-style'>" +
                    "           <div class='form-label-style'>เงินชดใช้</div>" +
                    "           <div class='form-discription-style'>" +
                    "               <div class='form-discription-line1-style'>คำนวณเงินทุนการศึกษาที่ต้องชดใช้กรณีนักศึกษารับทุนการ</div>" +
                    "               <div class='form-discription-line2-style'>ศึกษา และคำนวณเงินที่ต้องชดใช้ตามระยะเวลาที่เข้าศึกษา</div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class='content-left' id='cal-contract-penalty1-input'>" +
                    "       <div class='form-input-style'>" +
                    "           <div class='form-input-content'>" +
                    "               <div>" +
                    "                   ระยะเวลาที่ชดใช้ทุนการศึกษา <span>" + (!string.IsNullOrEmpty(actualMonthScholarshipDefault) ? double.Parse(actualMonthScholarshipDefault).ToString("#,##0") : "-") + "</span> เดือน เป็นเงิน <span>" + (!string.IsNullOrEmpty(actualScholarshipDefault) ? double.Parse(actualScholarshipDefault).ToString("#,##0.00") : "-") + "</span> บาท / เดือน" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ยอดเงินทุนการศึกษาที่ชดใช้ <span>" + double.Parse(totalPayScholarshipDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ระยะเวลาที่ใช้ในการศึกษา <span>" + double.Parse(actualMonthDefault).ToString("#,##0") + "</span> เดือน <span>" + double.Parse(actualDayDefault).ToString("#,##0") + "</span> วัน" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ยอดเงินค่าปรับผิดสัญญา <span>" + double.Parse(subtotalPenaltyDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ยอดเงินที่ต้องรับผิดชอบชดใช้ <span>" + double.Parse(totalPenaltyDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>" +
                    "          </div>" +
                    "      </div>" +
                    "  </div>" +
                    "</div>" +
                    "<div class='clear'></div>"
                );
            }

            if (caseGraduateBreakContractDefault.Equals("2")) {
                html +=(
                    "<div class='box3'></div>"
                );

                if (civilDefault.Equals("1")) {
                    string startDate = string.Empty;
                    string endDate = string.Empty;
                    string afterStudyLeave = string.Empty;

                    if (studyLeaveDefault.Equals("N")) {
                        startDate = requireDateDefault;
                        endDate = approveDateDefault;
                    }

                    if (studyLeaveDefault.Equals("Y")) {
                        startDate = beforeStudyLeaveStartDateDefault;
                        endDate = beforeStudyLeaveEndDateDefault;
                        afterStudyLeave = (
                            "<div class='form-input-content-line'>" + eCPUtil.studyLeave[1] + " และกลับเข้าปฏิบัติงาน</div>" +
                            "<div class='form-input-content-line'>ตั้งแต่วันที่ <span>" + Util.LongDateTH(afterStudyLeaveStartDateDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(afterStudyLeaveEndDateDefault) + "</span></div>"
                        );
                    }

                    html += (
                        "<div id='indemnitor-work'>" +
                        "   <div class='content-left study-leave-" + studyLeaveDefault.ToLower() + "' id='indemnitor-work-label'>" +
                        "       <div class='form-label-discription-style'>" +
                        "           <div class='form-label-style'>รายละเอียดข้อมูลการทำงานชดใช้</div>" +
                        "       </div>" +
                        "   </div>" +
                        "   <div class='content-left study-leave-" + studyLeaveDefault.ToLower() + "' id='indemnitor-work-input'>" +
                        "       <div class='form-input-style'>" +
                        "           <div class='form-input-content'>" +
                        "               <div>" +
                        "                   ทำงานชดใช้ที่ <span>" + indemnitorAddressDefault + "</span>" +
                        "               </div>" +
                        "               <div class='form-input-content-line'>" +
                        "                   จังหวัด <span>" + provinceTNameDefault + "</span> ตั้งแต่วันที่ <span>" + Util.LongDateTH(startDate) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(endDate) + "</span>" +
                        "               </div>" +
                                        afterStudyLeave +
                        "          </div>" +
                        "      </div>" +
                        "  </div>" +
                        "</div>" +
                        "<div class='clear'></div>"
                    );
                }

                html += (
                    "<div id='cal-contract-penalty2'>" +
                    "   <div class='content-left' id='cal-contract-penalty2-label'>" +
                    "       <div class='form-label-discription-style'>" +
                    "           <div class='form-label-style'>เงินชดใช้</div>" +
                    "           <div class='form-discription-style'>" +
                    "               <div class='form-discription-line1-style'>คำนวณเงินทุนการศึกษาที่ต้องชดใช้ กรณีนักศึกษารับทุนการ</div>" +
                    "               <div class='form-discription-line2-style'>ศึกษาและคำนวณเงินที่ต้องชดใช้แทนการปฏิบัติงานส่วนที่ขาด</div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>" +
                    "   <div class='content-left' id='cal-contract-penalty2-input'>" +
                    "       <div class='form-input-style'>" +
                    "           <div class='form-input-content'>" +
                    "               <div>" +
                    "                   ยอดเงินทุนการศึกษาที่ชดใช้ <span>" + double.Parse(totalPayScholarshipDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>"
                );

                if (setAmtIndemnitorYear.Equals("Y"))
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               ระยะเวลาที่ต้องปฏิบัติงานชดใช้ <span>" + (!string.IsNullOrEmpty(allActualDateDefault) ? double.Parse(allActualDateDefault).ToString("#,##0") : "-") + "</span> วัน ปฏิบัติงานชดใช้แล้ว <span>" + (!string.IsNullOrEmpty(actualDateDefault) ? double.Parse(actualDateDefault).ToString("#,##0") : "-") + "</span> วัน ขาด <span>" + (!string.IsNullOrEmpty(remainDateDefault) ? double.Parse(remainDateDefault).ToString("#,##0") : "-") + "</span> วัน" +
                        "           </div>"
                    );

                if (setAmtIndemnitorYear.Equals("N")) {
                    html += (
                        "           <div class='form-input-content-line'>" +
                        "               ระยะเวลาที่ใช้ในการศึกษา <span>" + (!string.IsNullOrEmpty(actualDayDefault) ? double.Parse(actualDayDefault).ToString("#,##0") : "-") + "</span> วัน" +
                        "           </div>"
                    );

                    if (civilDefault.Equals("1"))
                        html += (
                            "       <div class='form-input-content-line'>" +
                            "           ระยะเวลาที่ปฏิบัติงานชดใช้ <span>" + (!string.IsNullOrEmpty(actualDateDefault) ? double.Parse(actualDateDefault).ToString("#,##0") : "-") + "</span> วัน" +
                            "       </div>"
                        );
                }

                html += (
                    "               <div class='form-input-content-line'>" +
                    "                   ยอดเงินค่าปรับผิดสัญญา <span>" + double.Parse(subtotalPenaltyDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>" +
                    "               <div class='form-input-content-line'>" +
                    "                   ยอดเงินที่ต้องรับผิดชอบชดใช้ <span>" + double.Parse(totalPenaltyDefault).ToString("#,##0.00") + "</span> บาท" +
                    "               </div>" +
                    "           </div>" +
                    "       </div>" +
                    "   </div>" +
                    "</div>" +
                    "<div class='clear'></div>"
                );
            }

            html += (
                "<div id='status-repay'>" +
                "   <div class='content-left' id='status-repay-label'>" +
                "       <div class='form-label-discription-style " + (statusCancel.Equals("2") || statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
                "           <div class='form-label-style'>สถานะการแจ้งชำระหนี้</div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='content-left' id='status-repay-input'>" +
                "       <div class='form-label-discription-style " + (statusCancel.Equals("2") || statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
                "           <div class='form-label-style'>" +
                "               <span>" + eCPUtil.repayStatusDetail[int.Parse(statusRepayCurrent[0])] + "</span>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "</div>" +
                "<div class='clear'></div>"
            );
        }

        if (statusCancel.Equals("2") ||
            statusEdit.Equals("2")) {
            html += (
                "<div class='box3'></div>" +
                "<div id='commenteditcancel-detail'>" +
                "   <div class='content-left' id='commenteditcancel-label'>" +
                "       <div class='form-label-discription-style'>" +
                "           <div class='form-label-style'>" + (statusCancel.Equals("2") ? "สาเหตุการยกเลิกรายการแจ้ง" : (statusEdit.Equals("2") ? "สาเหตุการแก้ไขรายการแจ้ง" : "")) + "</div>" +
                "       </div>" +
                "   </div>" +
                "   <div class='content-left' id='commenteditcancel-input'>" +
                "       <div class='form-label-discription-style'>" +
                "           <div class='form-label-style'>" +
                "               <div class='textareabox' id='commentseditcancel'>" +
                "                   <div id='commenteditcancel'>" + (statusCancel.Equals("2") ? ("ยกเลิกรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(rejectCancelDateDefault)) + " สาเหตุ" + commentCancelDefault) : (statusEdit.Equals("2") ? ("ส่งกลับแก้ไขรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(rejectEditDateDefault)) + " สาเหตุ" + commentEditDefault) : "")) + "</div>" +
                "               </div>" +
                "           </div>" +
                "       </div>" +
                "   </div>" +
                "</div>" +
                "<div class='clear'></div>"
            );
        }

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        html += (
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style1-" + (eCPCookie["UserSection"] + status + statusEdit + statusCancel + statusPayment) + "'>" +
            "           <ul>"
        );

        if (status.Equals("a")) {
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=ReceiverCPTransBreakContract('" + cp1id + "','" + trackingStatus + "')>รับรายการ</a>" +
                "           </li>" +
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(2,'addcommenteditbreakcontract',true,'','" + cp1id + "','')>ส่งกลับแก้ไข</a>" +
                "           </li>"
            );
        }

        if (status.Equals("r")) {
            html += (
                "           <li id='button-status-r'>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(2,'addupdaterepaycontract',true,'','" + cp1id + "','')>รายละเอียดการแจ้งชำระหนี้</a>" +
                "           </li>" +
                "           <li id='button-status-i'>" +
                "               <a href='javascript:void(0)' onclick=ViewCalInterestOverpayment('" + cp2id + "')>คำนวณดอกเบี้ย</a>" +
                "           </li>" +
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(2,'addcommentcancelrepaycontract',true,'','" + cp1id + "','')>ยกเลิกรายการ</a>" +
                "           </li>"
            );
        }

        if (status.Equals("r1"))
            html += (
                "           <li id='button-status-r'>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(2,'viewrepaycontract',true,'','" + cp1id + "','')>รายละเอียดการแจ้งชำระหนี้</a>" +
                "           </li>"
            );

        if (eCPCookie["UserSection"].Equals("1") &&
            statusEdit.Equals("1") &&
            statusCancel.Equals("1") &&
            (status.Equals("v1") || status.Equals("v2")))
            html += (
                "           <li id='button-status-" + status + "'>" +
                "               <a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + cp1id + "','" + status + "')>พิมพ์แบบตรวจสอบ</a>" +
                "           </li>"
            );

        if (eCPCookie["UserSection"].Equals("2") &&
            statusCancel.Equals("1") &&
            status.Equals("v1"))
            html += (
                "           <li id='button-status-" + status + "'>" +
                "               <a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + cp1id + "','" + status + "')>พิมพ์แบบตรวจสอบ</a>" +
                "           </li>"
            );

        if (status.Equals("v3"))
            html += (
                "           <li id='button-status-v3'>" +
                "               <a href='javascript:void(0)' onclick=ViewTransPaymentReportDebtorContractByProgram('" + cp2id + "')>รายละเอียดการชำระหนี้</a>" +
                "           </li>"
            );
  
        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>" +
            "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
            "<form id='export-setvalue' method='post'>" +
            "   <input id='export-send' name='export-send' value='' type='hidden' />" +
            "   <input id='export-order' name='export-order' value='' type='hidden' />" +
            "   <input id='export-type' name='export-type' value='' type='hidden' />" +
            "</form>"
        );

        return html;
    }

    public static string DetailCPTransBreakContract(
        string cp1id,
        string status
    ) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransBreakContract(cp1id);

        if (data.GetLength(0) > 0) {
            html += (
                DetailCPTransBreakRequireContract(cp1id, data, status)
            );
        }

        return html;
    }

    public static string ListSearchStudentCPTransBreakContract(string studentid) {
        string[,] data = eCPDB.ListSearchStudentCPTransBreakContract(studentid);
        int recordCount = data.GetLength(0);
        int error;

        if (recordCount > 0)
            error = 1;
        else
            error = 0;

        return ("<error>" + error + "<error>");
    }

    public static string ListSearchTrackingStatusCPTransBreakContract(string cp1id) {
        string trackingStatus = eCPDB.ChkTrackingStatusCPTransBreakContract(cp1id);

        return ("<trackingstatus>" + trackingStatus + "<trackingstatus>");
    }

    public static string ListCPTransBreakContract(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        int recordCount = eCPDB.CountCPTransBreakContract(c);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPTransBreakContract(c);
            string groupNum;
            string trackingStatus;
            string iconStatus;
            string highlight;
            string callFunc;
            string page = eCPUtil.pageOrder[(section - 1), (pid - 1)];
            int i;

            html += (
                "<div class='table-content'>"
            );

            for (i = 0; i < data.GetLength(0); i++) {
                groupNum = (!data[i, 8].Equals("0") ? " ( กลุ่ม " + data[i, 8] + " )" : "");
                trackingStatus = (data[i, 9] + data[i, 10] + data[i, 11] + data[i, 12]);
                iconStatus = eCPUtil.iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil.iconTrackingStatus, trackingStatus) - 1, 1];
                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                callFunc = ("ViewTrackingStatusViewTransBreakContract('" + data[i, 1] + "','" + trackingStatus + "','" + data[i, 15] + "')");                                

                if (section.Equals(1) &&
                    !page.Equals("CPTransBreakContract")) {
                    html += (
                        "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                        "   <li id='tab1-table-content-cp-trans-require-contract-col1' onclick=" + callFunc + ">" +
                        "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col2' onclick=" + callFunc + ">" +
                        "       <div>" + data[i, 2] + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col3' onclick=" + callFunc + ">" +
                        "       <div>" + (data[i, 3] + data[i, 4] + " " + data[i, 5]) + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col4' onclick=" + callFunc + ">" +
                        "       <div>" +
                        "           <span class='programcode-col'>" + data[i, 6] + "</span>- " + (data[i, 7] + groupNum) +
                        "       </div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col5' onclick=" + callFunc + ">" +
                        "       <div>" + data[i, 14] + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col6' onclick=" + callFunc + ">" +
                        "       <div class='icon-status-style'>" +
                        "           <ul>" +
                        "               <li class='" + iconStatus + "'></li>" +
                        "           </ul>" +
                        "       </div>" +
                        "   </li>" +
                        "</ul>"
                    );
                }

                if ((section.Equals(1) && page.Equals("CPTransBreakContract")) ||
                    section.Equals(2)) {
                    html += (
                        "<ul class='table-row-content " + highlight + "' id='trans-break-contract" + data[i, 1] + "'>" +
                        "   <li id='tab2-table-content-cp-trans-break-contract-col1' onclick=" + callFunc + ">" +
                        "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col2' " + (!trackingStatus.Equals("1111") ? "onclick=" + callFunc : "") + ">" +
                        "       <div>" + 
                                    (trackingStatus.Equals("1111") ? ("<input class='checkbox' type='checkbox' name='send-break-contract' onclick=UncheckRoot('check-uncheck-all') value='" + data[i, 1] + "' />") : "") +
                        "       </div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col3' onclick=" + callFunc + ">" +
                        "       <div>" + data[i, 2] + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col4' onclick=" + callFunc + ">" +
                        "       <div>" + (data[i, 3] + data[i, 4] + " " + data[i, 5]) + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col5' onclick=" + callFunc + ">" +
                        "       <div>" +
                        "           <span class='programcode-col'>" + data[i, 6] + "</span>- " + (data[i, 7] + groupNum) +
                        "       </div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col6' onclick=" + callFunc + ">" +
                        "       <div>" + data[i, 13] + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col7' onclick=" + callFunc + ">" +
                        "       <div>" + (!string.IsNullOrEmpty(data[i, 14]) ? data[i, 14] : "-") + "</div>" +
                        "   </li>" +
                        "   <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col8' onclick=" + callFunc + ">" +
                        "       <div class='icon-status-style'>" +
                        "           <ul>" +
                        "               <li class='" + iconStatus + "'></li>" +
                        "           </ul>" +
                        "       </div>" +
                        "   </li>" +
                        "</ul>"
                    );
                }
            }

            html += (
                "</div>"
            );

            int currentPage = string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]);
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);
                
            pageHtml +=(
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "transbreakcontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
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

    public static string TabCPTransBreakContract() {
        string html = string.Empty;

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        string page = eCPUtil.pageOrder[(section - 1), (pid - 1)];
        Array trackingStatus = null;

        if (section.Equals(1) &&
            !page.Equals("CPTransBreakContract"))
            trackingStatus = eCPUtil.trackingStatusORLA;

        if ((section.Equals(1) && page.Equals("CPTransBreakContract")) ||
            section.Equals(2))
            trackingStatus = eCPUtil.trackingStatusORAA;

        html += (
            "<div id='cp-trans-break-contract-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-trans-break-contract") +
            "       <div class='content-data-tabs' id='tabs-cp-trans-break-contract'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-trans-break-contract' alt='#tab1-cp-trans-break-contract' href='javascript:void(0)'>ตรวจสอบรายการแจ้ง</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-trans-break-contract' alt='#tab2-cp-trans-break-contract' href='javascript:void(0)'>เพิ่มรายการแจ้ง</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-trans-break-contract' alt='#tab3-cp-trans-break-contract' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-trans-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-trans-break-contract' value=''>" +
            "                   <input type='hidden' id='trackingstatus-trans-break-contract-hidden' value='6'>" +
            "                   <input type='hidden' id='trackingstatus-trans-break-contract-text-hidden' value='" + trackingStatus.GetValue(5, 0) + "'>" +
            "                   <input type='hidden' id='id-name-trans-break-contract-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-trans-break-contract-hidden' value=''>" +
            "                   <input type='hidden' id='program-trans-break-contract-hidden' value=''>" +
            "                   <input type='hidden' id='date-start-trans-break-contract-hidden' value=''>" +
            "                   <input type='hidden' id='date-end-trans-break-contract-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchcptransbreakcontract',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a id='button-send' href='javascript:void(0)' onclick='ConfirmSendBreakContract()'>ส่ง</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-trans-break-contract'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-trans-break-contract-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>สถานะรายการแจ้ง</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order2'>" +
            "                   <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order2-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order3'>" +
            "                   <div class='box-search-condition-order-title'>คณะ</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order3-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order4'>" +
            "                   <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order4-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order5'>" +
            "                   <div class='box-search-condition-order-title'>ช่วงวันที่" + (section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้ง</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order5-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-trans-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-trans-break-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-trans-break-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='tab2-table-head-cp-trans-break-contract-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col2'>" +
            "                       <div class='table-head-line1'>ส่ง</div>" +
            "                       <div>" +
            "                           <input class='checkbox' type='checkbox' name='check-uncheck-all' id='check-uncheck-all' onclick=CheckUncheckAll('check-uncheck-all','send-break-contract') />" +
            "                       </div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col3'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col4'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col5'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col6'>" +
            "                       <div class='table-head-line1'>ทำรายการแจ้ง</div>" +
            "                       <div>เมื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col7'>" +
            "                       <div class='table-head-line1'>ส่งรายการแจ้ง</div>" +
            "                       <div>เมื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col8'>" +
            "                       <div class='table-head-line1'>สถานะรายการแจ้ง</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailtrackingstatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-trans-break-contract-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-trans-break-contract-contents'></div>" +
            "</div>" +
            "<div id='cp-trans-break-contract-content'>" +
            "   <div class='tab-content' id='tab1-cp-trans-break-contract-content'>" +
            "       <div class='box4' id='list-data-trans-break-contract'></div>" +
            "       <div id='nav-page-trans-break-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-trans-break-contract-content'>" +
            "       <div class='box1 addupdate-data-trans-break-contract' id='add-data-trans-break-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-trans-break-contract-content'>" +
            "       <div class='box1 addupdate-data-trans-break-contract' id='update-data-trans-break-contract'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}
