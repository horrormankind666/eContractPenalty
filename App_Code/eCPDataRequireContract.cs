/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๑๑/๐๑/๒๕๖๗>
Description : <สำหรับการรับรายการแจ้ง>
=============================================
*/

using System;
using System.Web;

public class eCPDataRequireContract {
    private static string AddUpdateCPTransRequireContract(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string cp1id = (action.Equals("update") ? data[0, 2] : data[0, 1]);
        string studentIDDefault = (action.Equals("update") ? data[0, 19] : data[0, 2]);
        string titleNameDefault = (action.Equals("update") ? data[0, 20] : data[0, 5]);
        string firstNameDefault = (action.Equals("update") ? data[0, 21] : data[0, 8]);
        string lastNameDefault = (action.Equals("update") ? data[0, 22] : data[0, 9]);
        string facultyCodeDefault = (action.Equals("update") ? data[0, 26] : data[0, 13]);
        string facultyNameDefault = (action.Equals("update") ? data[0, 27] : data[0, 14]);
        string programCodeDefault = (action.Equals("update") ? data[0, 23] : data[0, 10]);
        string programNameDefault = (action.Equals("update") ? data[0, 24] : data[0, 11]);
        string groupNumDefault = (action.Equals("update") ? data[0, 28] : data[0, 15]);
        string dlevelDefault = (action.Equals("update") ? data[0, 30] : data[0, 17]);
        string pictureFileNameDefault = (action.Equals("update") ? data[0, 56] : data[0, 44]);
        string pictureFolderNameDefault = (action.Equals("update") ? data[0, 57] : data[0, 45]);
        string contractDateDefault = (action.Equals("update") ? data[0, 38] : data[0, 25]);
        string contractDateAgreementDefault = (action.Equals("update") ? data[0, 61] : data[0, 46]);
        string guarantorDefault = (action.Equals("update") ? data[0, 39] : data[0, 26]);
        string scholarDefault = (action.Equals("update") ? data[0, 40] : data[0, 27]);
        string scholarshipMoneyDefault = (action.Equals("update") ? (!data[0, 41].Equals("0") ? double.Parse(data[0, 41]).ToString("#,##0") : string.Empty) : (!data[0, 28].Equals("0") ? double.Parse(data[0, 28]).ToString("#,##0") : string.Empty));
        string scholarshipYearDefault = (action.Equals("update") ? (!data[0, 42].Equals("0") ? double.Parse(data[0, 42]).ToString("#,##0") : string.Empty) : (!data[0, 29].Equals("0") ? double.Parse(data[0, 29]).ToString("#,##0") : string.Empty));
        string scholarshipMonthDefault = (action.Equals("update") ? (!data[0, 43].Equals("0") ? double.Parse(data[0, 43]).ToString("#,##0") : string.Empty) : (!data[0, 30].Equals("0") ? double.Parse(data[0, 30]).ToString("#,##0") : string.Empty));
        string educationDateStartDefault = (action.Equals("update") ? data[0, 44] : data[0, 31]);
        string educationDateEndDefault = (action.Equals("update") ? data[0, 45] : data[0, 32]);
        string caseGraduateBreakContractDefault = (action.Equals("update") ? data[0, 46] : data[0, 33]);
        string civilDefault = (action.Equals("update") ? data[0, 47] : data[0, 34]);
        string contractForceDateStartDefault = (action.Equals("update") ? data[0, 62] : data[0, 47]);
        string contractForceDateEndDefault = (action.Equals("update") ? data[0, 63] : data[0, 48]);
        string calDateCondition = (action.Equals("update") ? data[0, 48] : data[0, 35]);
        string setAmtIndemnitorYear = (action.Equals("update") ? data[0, 77] : data[0, 51]);
        string indemnitorYearDefault = (action.Equals("update") ? (!data[0, 49].Equals("0") ? double.Parse(data[0, 49]).ToString("#,##0") : string.Empty) : (!data[0, 36].Equals("0") ? double.Parse(data[0, 36]).ToString("#,##0") : string.Empty));
        string indemnitorCashDefault = (action.Equals("update") ? double.Parse(data[0, 50]).ToString("#,##0") : double.Parse(data[0, 37]).ToString("#,##0"));
        string trackingStatus = (action.Equals("update") ? (data[0, 52] + data[0, 53] + data[0, 54] + data[0, 55]) : (data[0, 40] + data[0, 41] + data[0, 42] + data[0, 43]));
        string cp2id = (action.Equals("update") ? data[0, 1] : string.Empty);
        string indemnitorAddressDefault = (action.Equals("update") ? data[0, 3] : string.Empty);
        string provinceIDDefault = (action.Equals("update") ? data[0, 4] : "0");
        string studyLeaveDefault = (action.Equals("update") ? data[0, 66] : string.Empty);
        string requireDateDefault = (action.Equals("update") ? data[0, 6] : string.Empty);
        string approveDateDefault = (action.Equals("update") ? data[0, 7] : string.Empty);
        string beforeStudyLeaveStartDateDefault = (action.Equals("update") ? data[0, 67] : string.Empty);
        string beforeStudyLeaveEndDateDefault = (action.Equals("update") ? data[0, 68] : string.Empty);
        string studyLeaveStartDateDefault = (action.Equals("update") ? data[0, 69] : string.Empty);
        string studyLeaveEndDateDefault = (action.Equals("update") ? data[0, 70] : string.Empty);
        string afterStudyLeaveStartDateDefault = (action.Equals("update") ? data[0, 71] : string.Empty);
        string afterStudyLeaveEndDateDefault = (action.Equals("update") ? data[0, 72] : string.Empty);
        string actualMonthScholarshipDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 8]) ? double.Parse(data[0, 8]).ToString("#,##0") : string.Empty) : string.Empty);
        string actualScholarshipDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 9]) ? double.Parse(data[0, 9]).ToString("#,##0.00") : string.Empty) : string.Empty);
        string totalPayScholarshipDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 10]) ? double.Parse(data[0, 10]).ToString("#,##0.00") : string.Empty) : string.Empty);
        string actualMonthDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 11]) ? double.Parse(data[0, 11]).ToString("#,##0") : string.Empty) : string.Empty);
        string actualDayDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 12]) ? double.Parse(data[0, 12]).ToString("#,##0") : string.Empty) : string.Empty);
        string allActualDateDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 13]) ? double.Parse(data[0, 13]).ToString("#,##0") : string.Empty) : string.Empty);
        string actualDateDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 14]) ? double.Parse(data[0, 14]).ToString("#,##0") : string.Empty) : string.Empty);
        string remainDateDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 15]) ? double.Parse(data[0, 15]).ToString("#,##0") : string.Empty) : string.Empty);
        string subtotalPenaltyDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 16]) ? double.Parse(data[0, 16]).ToString("#,##0.00") : string.Empty) : string.Empty);
        string totalPenaltyDefault = (action.Equals("update") ? (!string.IsNullOrEmpty(data[0, 17]) ? double.Parse(data[0, 17]).ToString("#,##0.00") : string.Empty) : string.Empty);
        string lawyerFullnameDefault = (action.Equals("update") ? data[0, 73] : string.Empty);
        string lawyerPhoneNumberDefault = (action.Equals("update") ? data[0, 74] : string.Empty);
        string lawyerMobileNumberDefault = (action.Equals("update") ? data[0, 75] : string.Empty);
        string lawyerEmailDefault = (action.Equals("update") ? data[0, 76] : string.Empty);
        string statusRepay = (action.Equals("update") ? data[0, 18] : string.Empty);

        if (action.Equals("add")) {
            string userid = eCPUtil.GetUserID();
            string[,] data1 = eCPDB.ListDetailCPTabUser(userid, "", "", "");

            lawyerFullnameDefault = data1[0, 3];
            lawyerPhoneNumberDefault = data1[0, 6];
            lawyerMobileNumberDefault = data1[0, 7];
            lawyerEmailDefault = data1[0, 8];
        }

        html += (
            "<div class='form-content' id='" + action + "-cp-trans-require-contract'>" +
            "   <div id='addupdate-cp-trans-require-contract'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='cp1id' value='" + cp1id + "' />" +
            "       <input type='hidden' id='scholar-hidden' value='" + scholarDefault + "' />" +
            "       <input type='hidden' id='education-date-start-hidden' value='" + educationDateStartDefault + "' />" +
            "       <input type='hidden' id='education-date-end-hidden' value='" + educationDateEndDefault + "' />" +
            "       <input type='hidden' id='case-graduate-break-contract-hidden' value='" + caseGraduateBreakContractDefault + "' />" +
            "       <input type='hidden' id='civil-hidden' value='" + civilDefault + "' />" +
            "       <input type='hidden' id='contract-force-date-start-hidden' value='" + contractForceDateStartDefault + "' />" +
            "       <input type='hidden' id='contract-force-date-end-hidden' value='" + contractForceDateEndDefault + "' />" +
            "       <input type='hidden' id='set-amt-indemnitor-year' value='" + setAmtIndemnitorYear + "' />" +
            "       <input type='hidden' id='indemnitor-year-hidden' value='" + indemnitorYearDefault + "' />" +
            "       <input type='hidden' id='indemnitor-cash-hidden' value='" + indemnitorCashDefault + "' />" +
            "       <input type='hidden' id='cal-date-condition-hidden' value='" + calDateCondition + "' />" +
            "       <input type='hidden' id='trackingstatus' value='" + trackingStatus + "' />" +
            "       <input type='hidden' id='cp2id' value='" + cp2id + "' />" +
            "       <input type='hidden' id='study-leave-hidden' value='" + studyLeaveDefault + "' />" +
            "       <input type='hidden' id='indemnitor-address-hidden' value='" + indemnitorAddressDefault + "' />" +
            "       <input type='hidden' id='province-id-hidden' value='" + provinceIDDefault + "' />" +
            "       <input type='hidden' id='require-date-hidden' value='" + requireDateDefault + "' />" +
            "       <input type='hidden' id='approve-date-hidden' value='" + approveDateDefault + "' />" +
            "       <input type='hidden' id='before-study-leave-start-date-hidden' value='" + beforeStudyLeaveStartDateDefault + "' />" +
            "       <input type='hidden' id='before-study-leave-end-date-hidden' value='" + beforeStudyLeaveEndDateDefault + "' />" +
            "       <input type='hidden' id='study-leave-start-date-hidden' value='" + studyLeaveStartDateDefault + "' />" +
            "       <input type='hidden' id='study-leave-end-date-hidden' value='" + studyLeaveEndDateDefault + "' />" +
            "       <input type='hidden' id='after-study-leave-start-date-hidden' value='" + afterStudyLeaveStartDateDefault + "' />" +
            "       <input type='hidden' id='after-study-leave-end-date-hidden' value='" + afterStudyLeaveEndDateDefault + "' />" +
            "       <input type='hidden' id='all-actual-month-scholarship-hidden' value='" + actualMonthScholarshipDefault + "' />" +
            "       <input type='hidden' id='all-actual-scholarship-hidden' value='" + actualScholarshipDefault + "' />" +
            "       <input type='hidden' id='total-pay-scholarship-hidden' value='" + totalPayScholarshipDefault + "' />" +
            "       <input type='hidden' id='actual-month-hidden' value='" + actualMonthDefault + "' />" +
            "       <input type='hidden' id='actual-day-hidden' value='" + actualDayDefault + "' />" +
            "       <input type='hidden' id='all-actual-date-hidden' value='" + allActualDateDefault + "' />" +
            "       <input type='hidden' id='actual-date-hidden' value='" + actualDateDefault + "' />" +
            "       <input type='hidden' id='remain-date-hidden' value='" + remainDateDefault + "' />" +
            "       <input type='hidden' id='subtotal-penalty-hidden' value='" + subtotalPenaltyDefault + "' />" +
            "       <input type='hidden' id='total-penalty-hidden' value='" + totalPenaltyDefault + "' />" +
            "       <input type='hidden' id='lawyer-fullname-hidden' value='" + lawyerFullnameDefault + "' />" +
            "       <input type='hidden' id='lawyer-phonenumber-hidden' value='" + lawyerPhoneNumberDefault + "' />" +
            "       <input type='hidden' id='lawyer-mobilenumber-hidden' value='" + lawyerMobileNumberDefault + "' />" +
            "       <input type='hidden' id='lawyer-email-hidden' value='" + lawyerEmailDefault + "' />" +
            "       <input type='hidden' id='repaystatus' value='" + statusRepay + "' />" +
            "       <div>" +
            "           <div id='profile-student'>" +
            "               <div class='content-left' id='picture-student'>" +
            "                   <div>" +
            "                       <img src='" + (eCPUtil.URL_STUDENT_PICTURE_2_STREAM + "&f=/" + pictureFolderNameDefault + "/" + pictureFileNameDefault) + "' />" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-left' id='profile-student-label'>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>รหัสนักศึกษา</div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>ชื่อ - นามสกุล</div>" +
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
            "                           <span>" + studentIDDefault + "&nbsp;" + programCodeDefault.Substring(0, 4) + " / " + programCodeDefault.Substring(4, 1) + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span>" + titleNameDefault + firstNameDefault + " " + lastNameDefault + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span>" + dlevelDefault + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div class='form-label-style'>" +
            "                           <span>" + facultyCodeDefault + " - " + facultyNameDefault + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-label-discription-style clear-bottom'>" +
            "                       <div class='form-label-style'>" +
            "                           <span>" + programCodeDefault + " - " + programNameDefault + (!groupNumDefault.Equals("0") ? " ( กลุ่ม " + groupNumDefault + " )" : "") + "</span>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='box3'></div>" +
            "           <div id='break-contract'>" +
            "               <div>" +
            "                   <div class='form-label-discription-style clear-bottom'>" +
            "                       <div id='break-contract-label'>" +
            "                           <div class='form-label-style'>รายละเอียดการผิดสัญญาการศึกษา</div>" +
            "                           <div class='form-discription-style'>" +
            "                               <div class='form-discription-line1-style'>รายละเอียดการผิดสัญญาการศึกษาของนักศึกษา และเงื่อนไข</div>" +
            "                               <div class='form-discription-line2-style'>การชดใช้ตามสัญญา</div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style clear-bottom'>" +
            "                       <div class='form-input-content' id='break-contract-input'>" +
            "                           <div>" +
            "                               <div class='content-left' id='contract-date-label'>สัญญานักศึกษาลงวันที่</div>" +
            "                               <div class='content-left' id='contract-date-input'>" +
            "                                   <input class='inputbox' type='text' id='contract-date' value='" + Util.LongDateTH(contractDateDefault) + "' style='width:120px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='contract-date-agreement-label'>สัญญาค้ำประกันลงวันที่</div>" +
            "                               <div class='content-left' id='contract-date-agreement-input'>" +
            "                                   <input class='inputbox' type='text' id='contract-date-agreement' value='" + Util.LongDateTH(contractDateAgreementDefault) + "' style='width:120px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='guarantor-label'>ผู้ค้ำประกัน</div>" +
            "                               <div class='content-left' id='guarantor-input'>" +
            "                                   <input class='inputbox' type='text' id='guarantor' value='" + guarantorDefault + "' style='width:278px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='scholar-label'>ได้รับ / ไม่ได้รับทุนการศึกษา</div>" +
            "                               <div class='content-left' id='scholar-input'>" +
            "                                   <input class='inputbox' type='text' id='scholar' value='" + eCPUtil.scholar[int.Parse(scholarDefault) - 1] + "' style='width:120px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-money-label'>จำนวนเงิน</div>" +
            "                               <div class='content-left' id='scholarship-money-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-money' value='" + scholarshipMoneyDefault + "' style='width:60px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-money-unit-label'>บาท / หลักสูตร</div>" +
            "                               <div class='content-left' id='scholarship-year-label'>ระยะเวลา</div>" +
            "                               <div class='content-left' id='scholarship-year-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-year' value='" + scholarshipYearDefault + "' style='width:20px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-year-unit-label'>ปี</div>" +
            "                               <div class='content-left' id='scholarship-month-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='scholarship-month' value='" + scholarshipMonthDefault + "' style='width:20px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='scholarship-month-unit-label'>เดือน</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='case-graduate-label'>สำเร็จ / ไม่สำเร็จการศึกษา</div>" +
            "                               <div class='content-left' id='case-graduate-input'>" +
            "                                   <input class='inputbox' type='text' id='case-graduate' value='" + eCPUtil.caseGraduate[int.Parse(caseGraduateBreakContractDefault) - 1, 1] + "' style='width:120px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='education-date-start-label'>เริ่มต้นเข้าศึกษาเมื่อวันที่</div>" +
            "                               <div class='content-left' id='education-date-start-input'>" +
            "                                   <input class='inputbox' type='text' id='education-date-start' value='" + Util.LongDateTH(educationDateStartDefault) + "' style='width:120px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='education-date-end-label'>จนถึงวันที่</div>" +
            "                               <div class='content-left' id='education-date-end-input'>" +
            "                                   <input class='inputbox' type='text' id='education-date-end' value='" + Util.LongDateTH(educationDateEndDefault) + "' style='width:117px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='civil-label'>ปฏิบัติ / ไม่ปฏิบัติงานชดใช้</div>" +
            "                               <div class='content-left' id='civil-input'>" +
            "                                   <input class='inputbox' type='text' id='civil' value='" + (!civilDefault.Equals("0") ? eCPUtil.civil[int.Parse(civilDefault) - 1] : "") + "' style='width:120px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='contract-force-date-start-label'>สัญญาเริ่มมีผลบังคับใช้เมื่อวันที่</div>" +
            "                               <div class='content-left' id='contract-force-date-start-input'>" +
            "                                   <input class='inputbox' type='text' id='contract-force-date-start' value='" + Util.LongDateTH(contractForceDateStartDefault) + "' style='width:120px' />" +
            "                               </div>" +
            "                               <div class='content-left' id='contract-force-date-end-label'>จนถึงวันที่</div>" +
            "                               <div class='content-left' id='contract-force-date-end-input'>" +
            "                                   <input class='inputbox' type='text' id='contract-force-date-end' value='" + Util.LongDateTH(contractForceDateEndDefault) + "' style='width:117px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='indemnitor-year-label'>ระยะเวลาที่ต้องปฏิบัติงานชดใช้</div>" +
            "                               <div class='content-left' id='indemnitor-year-input'>"
        );

        if (setAmtIndemnitorYear.Equals("Y"))
            html += (
                "                               <input class='inputbox textbox-numeric' type='text' id='indemnitor-year' onblur=Trim('indemnitor-year');AddCommas('indemnitor-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' />"
            );
        else
            html += (
                "                              <input class='inputbox textbox-numeric' type='text' id='indemnitor-year' value='' style='width:120px' />"
            );

        html += (
            "                               </div>" +
            "                               <div class='content-left' id='indemnitor-year-unit-label'>ปี</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='indemnitor-cash-label'>จำนวนเงินต้องชดใช้ตามสัญญา</div>" +
            "                               <div class='content-left' id='indemnitor-cash-input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='indemnitor-cash' onblur=Trim('indemnitor-cash');AddCommas('indemnitor-cash',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' />" +
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

        if (caseGraduateBreakContractDefault.Equals("1")) {
            html += (
                "       <div class='box3'></div>" +
                "       <div id='cal-contract-penalty1'>" +
                "           <div>" +
                "               <div class='content-left cal-contract-penalty-label' id='cal-contract-penalty-label'>" +
                "                   <div class='form-label-discription-style clear-bottom'>" +
                "                       <div class='form-label-style'>คำนวณเงินชดใช้</div>" +
                "                       <div class='form-discription-style'>" +
                "                           <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณเงินทุนการศึกษาที่</div>" +
                "                           <div class='form-discription-line2-style'>ต้องชดใช้กรณีนักศึกษารับทุนการศึกษา และคำนวณเงิน</div>" +
                "                           <div class='form-discription-line3-style'>ที่ต้องชดใช้ตามระยะเวลาที่เข้าศึกษา</div>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "               <div class='content-left cal-contract-penalty-input' id='cal-contract-penalty-input'>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-button'>" +
                "                       <div class='button-style2'>" +
                "                           <ul>" +
                "                               <li>" +
                "                                   <a href='javascript:void(0)' onclick='CalculatePayScholarshipAndPenalty()'>คำนวณ</a>" +
                "                               </li>" +
                "                           </ul>" +
                "                       </div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-scholarship'>" +
                "                       <div>" +
                "                           <div class='content-left' id='all-actual-month-scholarship-label'>ระยะเวลาที่ชดใช้ทุนการศึกษา</div>" +
                "                           <div class='content-left' id='all-actual-month-scholarship-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='all-actual-month-scholarship' onblur=Trim('all-actual-month-scholarship');AddCommas('all-actual-month-scholarship',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='all-actual-month-scholarship-unit-label'>เดือน</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                       <div>" +
                "                           <div class='content-left' id='all-actual-scholarship-label'>จำนวนเงินทุนการศึกษาที่ชดใช้</div>" +
                "                           <div class='content-left' id='all-actual-scholarship-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='all-actual-scholarship' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='all-actual-scholarship-unit-label'>บาท / เดือน</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                       <div>" +
                "                           <div class='content-left' id='total-pay-scholarship-label'>ยอดเงินทุนการศึกษาที่ชดใช้</div>" +
                "                           <div class='content-left' id='total-pay-scholarship-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='total-pay-scholarship' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='total-pay-scholarship-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-actual'>" +
                "                       <div id='view-cal-date-button'>" +
                "                           <div class='button-style2'>" +
                "                               <ul>" +
                "                                   <li>" +
                "                                       <a href='javascript:void(0)' onclick=ViewCalDate('" + calDateCondition + "')>ดูสูตรคำนวณ</a>" +
                "                                   </li>" +
                "                               </ul>" +
                "                           </div>" +
                "                       </div>" +
                "                       <div>" +
                "                           <div class='content-left' id='all-actual-date-label'>ระยะเวลาที่ใช้ในการศึกษา</div>" +
                "                           <div class='content-left' id='all-actual-month-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='all-actual-month' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='all-actual-month-unit-label'>เดือน</div>" +
                "                           <div class='content-left' id='all-actual-day-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='all-actual-day' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='all-actual-day-unit-label'>วัน</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                       <div>" +
                "                           <div class='content-left' id='subtotal-penalty-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
                "                           <div class='content-left' id='subtotal-penalty-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='subtotal-penalty' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='subtotal-penalty-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style clear-bottom' id='cal-contract-penalty-total'>" +
                "                       <div>" +
                "                           <div class='content-left' id='total-penalty-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
                "                           <div class='content-left' id='total-penalty-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='total-penalty' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='total-penalty-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "               </div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>"
            );
        }
    
        if (caseGraduateBreakContractDefault.Equals("2")) {
            if (civilDefault.Equals("1")) {
                html += (
                    "   <div class='box3'></div>" +
                    "   <div id='indemnitor-work'>" +
                    "       <div>" +
                    "           <div class='form-label-discription-style clear-bottom'>" +
                    "               <div id='indemnitor-work-label'>" +
                    "                   <div class='form-label-style'>รายละเอียดข้อมูลการทำงานชดใช้</div>" +
                    "                   <div class='form-discription-style'>" +
                    "                       <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดข้อมูลการทำงานชดใช้</div>" +
                    "                   </div>" +
                    "               </div>" +
                    "           </div>" +
                    "           <div class='form-input-style clear-bottom'>" +
                    "               <div class='form-input-content' id='indemnitor-work-input'>" +
                    "                   <div>" +
                    "                       <div class='content-left' id='indemnitor-address-label'>ตรวจสอบแล้วได้ทำงานชดใช้ที่</div>" +
                    "                       <div class='content-left' id='indemnitor-address-input'>" +
                    "                           <input class='inputbox' type='text' id='indemnitor-address' onblur=Trim('indemnitor-address'); value='' style='width:472px' />" +
                    "                       </div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>" +
                    "                   <div>" +
                    "                       <div class='content-left' id='province-label'>สถานที่ทำงานชดใช้อยู่จังหวัด</div>" +
                    "                       <div class='content-left' id='province-input'>" +
                                                eCPUtil.ListProvince("province") +
                    "                       </div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>" +
                    "                   <div id='study-leave-yesno'>" +
                    "                       <div class='content-left' id='study-leave-yesno-label'>ช่วงวันที่ทำงานชดใช้</div>" +
                    "                       <div class='content-left' id='study-leave-yesno-input'>" +
                    "                           <div>" +
                    "                               <div class='content-left' id='study-leave-status-no-input'>" +
                    "                                   <input class='radio' type='radio' name='study-leave-yesno' value='N' />" +
                    "                               </div>" +
                    "                               <div class='content-left' id='study-leave-status-no-label'>" +
                                                        eCPUtil.studyLeave[0] +
                    "                               </div>" +
                    "                           </div>" +
                    "                           <div class='clear'></div>" +
                    "                           <div id='study-leave-status-no'>" +
                    "                               <div class='content-left' id='require-date-label'>ตั้งแต่วันที่</div>" +
                    "                               <div class='content-left' id='require-date-input'>" +
                    "                                   <input class='inputbox calendar' type='text' id='require-date' readonly value='' />" +
                    "                               </div>" +
                    "                               <div class='content-left' id='approve-date-label'>ถึงวันที่</div>" +
                    "                               <div class='content-left' id='approve-date-input'>" +
                    "                                   <input class='inputbox calendar' type='text' id='approve-date' readonly value='' />" +
                    "                               </div>" +
                    "                           </div>" +
                    "                           <div class='clear'></div>" +
                    "                           <div>" +
                    "                               <div class='content-left' id='study-leave-status-yes-input'>" +
                    "                                   <input class='radio' type='radio' name='study-leave-yesno' value='Y' />" +
                    "                               </div>" +
                    "                               <div class='content-left' id='study-leave-status-yes-label'>" +
                                                        eCPUtil.studyLeave[1] +
                    "                               </div>" +
                    "                           </div>" +
                    "                           <div class='clear'></div>" +
                    "                           <div id='study-leave-status-yes'>" +
                    "                               <div id='before-study-leave'>" +
                    "                                   <div><strong>(1)</strong> การปฏิบัติงานก่อนการลาศึกษา / ลาฝึกอบรม</div>" +
                    "                                   <div id='before-study-leave-input'>" +
                    "                                       <div class='content-left' id='before-study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                    "                                       <div class='content-left' id='before-study-leave-start-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='before-study-leave-start-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                       <div class='content-left' id='before-study-leave-end-date-label'>ถึงวันที่</div>" +
                    "                                       <div class='content-left' id='before-study-leave-end-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='before-study-leave-end-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                   </div>" +
                    "                                   <div class='clear'></div>" +
                    "                               </div>" +
                    "                               <div id='study-leave'>" +
                    "                                   <div><strong>(2)</strong> การลาศึกษา / ลาฝึกอบรม ( ไม่นับเป็นระยะเวลาการปฏิบัติงานชดใช้ทุน )</div>" +
                    "                                   <div id='study-leave-input'>" +
                    "                                       <div class='content-left' id='study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                    "                                       <div class='content-left' id='study-leave-start-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='study-leave-start-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                       <div class='content-left' id='study-leave-end-date-label'>ถึงวันที่</div>" +
                    "                                       <div class='content-left' id='study-leave-end-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='study-leave-end-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                   </div>" +
                    "                                   <div class='clear'></div>" +
                    "                               </div>" +
                    "                               <div id='after-study-leave'>" +
                    "                                   <div><strong>(3)</strong> การกลับเข้าปฏิบัติงานภายหลังจากการลาศึกษา / ลาฝึกอบรม</div>" +
                    "                                   <div id='after-study-leave-input'>" +
                    "                                       <div class='content-left' id='after-study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                    "                                       <div class='content-left' id='after-study-leave-start-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='after-study-leave-start-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                       <div class='content-left' id='after-study-leave-end-date-label'>ถึงวันที่</div>" +
                    "                                       <div class='content-left' id='after-study-leave-end-date-input'>" +
                    "                                           <input class='inputbox calendar' type='text' id='after-study-leave-end-date' readonly value='' />" +
                    "                                       </div>" +
                    "                                   </div>" +
                    "                                   <div class='clear'></div>" +
                    "                               </div>" +
                    "                           </div>"  +
                    "                       </div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>" +
                    "               </div>" +
                    "           </div>" +
                    "       </div>" +
                    "       <div class='clear'></div>" +
                    "   </div>"
                );
            }

            html += (
                "       <div class='box3'></div>" +
                "       <div id='cal-contract-penalty2'>" +
                "           <div>" +
                "               <div class='content-left cal-contract-penalty-label' id='cal-contract-penalty-label-civil-" + civilDefault + "-set-" + setAmtIndemnitorYear.ToLower() + "'>" +
                "                   <div class='form-label-discription-style clear-bottom'>" +
                "                       <div class='form-label-style'>คำนวณเงินชดใช้</div>" +
                "                       <div class='form-discription-style'>" +
                "                           <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณเงินทุนการศึกษาที่ต้อง</div>" +
                "                           <div class='form-discription-line2-style'>ชดใช้กรณีนักศึกษารับทุนการศึกษา และคำนวณเงินที่ต้อง</div>" +
                "                           <div class='form-discription-line3-style'>ชดใช้แทนการปฏิบัติงานส่วนที่ขาด</div>" +
                "                       </div>" +
                "                   </div>" +
                "               </div>" +
                "               <div class='content-left clear-bottom cal-contract-penalty-input' id='cal-contract-penalty-input-civil-" + civilDefault + "-set-" + setAmtIndemnitorYear.ToLower() + "'>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-button'>" +
                "                       <div class='button-style2'>" +
                "                           <ul>" +
                "                               <li>" +
                "                                   <a href='javascript:void(0)' onclick='CalculatePayScholarshipAndPenalty()'>คำนวณ</a>" +
                "                               </li>" +
                "                           </ul>" +
                "                       </div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-scholarship'>" +
                "                       <div>" +
                "                           <div class='content-left' id='total-pay-scholarship-label'>ยอดเงินทุนการศึกษาที่ชดใช้</div>" +
                "                           <div class='content-left' id='total-pay-scholarship-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='total-pay-scholarship' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='total-pay-scholarship-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style' id='cal-contract-penalty-actual'>" +
                "                       <div id='view-cal-date-button'>" +
                "                           <div class='button-style2'>" +
                "                               <ul>" +
                "                                   <li>" +
                "                                       <a href='javascript:void(0)' onclick=ViewCalDate('" + calDateCondition + "')>ดูสูตรคำนวณ</a>" +
                "                                   </li>" +
                "                               </ul>" +
                "                           </div>" +
                "                       </div>"
            );

            if (setAmtIndemnitorYear.Equals("Y")) {
                html += (
                    "                   <div>" +
                    "                       <div class='content-left' id='all-actual-date-label'>ระยะเวลาที่ต้องปฏิบัติงานชดใช้</div>" +
                    "                       <div class='content-left' id='all-actual-date-input'>" +
                    "                           <input class='inputbox textbox-numeric' type='text' id='all-actual-date' value='' style='width:120px' />" +
                    "                       </div>" +
                    "                       <div class='content-left' id='all-actual-date-unit-label'>วัน</div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>" +
                    "                   <div>" +
                    "                       <div class='content-left' id='actual-date-label'>ระยะเวลาที่ปฏิบัติงานชดใช้แล้ว</div>" +
                    "                       <div class='content-left' id='actual-date-input'>" +
                    "                           <input class='inputbox textbox-numeric' type='text' id='actual-date' value='' style='width:120px' />" +
                    "                       </div>" +
                    "                       <div class='content-left' id='actual-date-unit-label'>วัน</div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>" +
                    "                   <div>" +
                    "                       <div class='content-left' id='remain-date-label'>ระยะเวลาปฏิบัติงานชดใช้ที่ขาด</div>" +
                    "                       <div class='content-left' id='remain-date-input'>" +
                    "                           <input class='inputbox textbox-numeric' type='text' id='remain-date' value='' style='width:120px' />" +
                    "                       </div>" +
                    "                       <div class='content-left' id='remain-date-unit-label'>วัน</div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>"
                );
            }
      
            if (setAmtIndemnitorYear.Equals("N")) {
                html += (
                    "                   <div>" +
                    "                       <div class='content-left' id='all-actual-day-label'>ระยะเวลาที่ใช้ในการศึกษา</div>" +
                    "                       <div class='content-left' id='all-actual-day-input'>" +
                    "                           <input class='inputbox textbox-numeric' type='text' id='all-actual-day' value='' style='width:120px' />" +
                    "                       </div>" +
                    "                       <div class='content-left' id='all-actual-day-unit-label'>วัน</div>" +
                    "                   </div>" +
                    "                   <div class='clear'></div>"
                );

                if (civilDefault.Equals("1")) {
                    html += (
                        "               <div>" +
                        "                   <div class='content-left' id='actual-date-label'>ระยะเวลาที่ปฏิบัติงานชดใช้</div>" +
                        "                   <div class='content-left' id='actual-date-input'>" +
                        "                       <input class='inputbox textbox-numeric' type='text' id='actual-date' value='' style='width:120px' />" +
                        "                   </div>" +
                        "                   <div class='content-left' id='actual-date-unit-label'>วัน</div>" +
                        "               </div>" +
                        "               <div class='clear'></div>"
                    );
                }
            }

            html += (
                "                       <div>" +
                "                           <div class='content-left' id='subtotal-penalty-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
                "                           <div class='content-left' id='subtotal-penalty-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='subtotal-penalty' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='subtotal-penalty-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "                   <div class='form-label-discription-style clear-bottom' id='cal-contract-penalty-total'>" +
                "                       <div>" +
                "                           <div class='content-left' id='total-penalty-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
                "                           <div class='content-left' id='total-penalty-input'>" +
                "                               <input class='inputbox textbox-numeric' type='text' id='total-penalty' value='' style='width:120px' />" +
                "                           </div>" +
                "                           <div class='content-left' id='total-penalty-unit-label'>บาท</div>" +
                "                       </div>" +
                "                       <div class='clear'></div>" +
                "                   </div>" +
                "               </div>" +
                "               <div class='clear'></div>" +
                "           </div>" +
                "           <div class='clear'></div>" +
                "       </div>"
            );
        }

        html += (
            "           <div class='box3'></div>" +
            "           <div id='lawyer'>" +
            "               <div>" +
            "                   <div class='form-label-discription-style'>" +
            "                       <div id='lawyer-label'>" +
            "                           <div class='form-label-style'>นิติกรผู้รับผิดชอบ</div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='form-input-style'>" +
            "                       <div class='form-input-content' id='lawyer-input'>" +
            "                           <div>" +
            "                               <div class='content-left' id='fullname-label'>ชื่อ - นามสกุล</div>" +
            "                               <div class='content-left' id='fullname-input'>" +
            "                                   <input class='inputbox' type='text' id='lawyer-fullname' onblur=Trim('lawyer-fullname') value='" + lawyerFullnameDefault + "' style='width:254px' />" +
            "                                   <div class='form-discription-style'>" +
            "                                       <div class='form-discription-line1-style'>ไม่ต้องระบุคำนำหน้าชื่อ</div>" +
            "                                   </div>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='phonenumber-label'>หมายเลขโทรศัพท์</div>" +
            "                               <div class='content-left' id='phonenumber-input'>" +
            "                                   <input class='inputbox' type='text' id='lawyer-phonenumber' onblur=Trim('lawyer-phonenumber') value='" + lawyerPhoneNumberDefault + "' style='width:120px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='mobilenumber-label'>หมายเลขโทรศัพท์มือถือ</div>" +
            "                               <div class='content-left' id='mobilenumber-input'>" +
            "                                   <input class='inputbox' type='text' id='lawyer-mobilenumber' onblur=Trim('lawyer-mobilenumber')  value='" + lawyerMobileNumberDefault + "' style='width:120px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='email-label'>อีเมล์</div>" +
            "                               <div class='content-left' id='email-input'>" +
            "                                   <input class='inputbox' type='text' id='lawyer-email' onblur=Trim('lawyer-email') value='" + lawyerEmailDefault + "' style='width:254px' />" +
            "                               </div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='clear'></div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTransRequireContract('" + action + "')>บันทึก</a>" +
            "               </li>"
        );
    
        if (action.Equals("update"))
            html += (
                "           <li>" +
                "               <a href='javascript:void(0)' onclick=LoadForm(1,'addcommentcancelrequirecontract',true,'','" + cp1id + "','')>ยกเลิกรายการ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTransRequireContract(false)'>ล้าง</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-require-contract')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li id='button-status-p'>" +
            "                   <a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + cp1id + "','v2')>พิมพ์แบบตรวจสอบ</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-require-contract')>ปิด</a>" +
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

        return html;
    }

    public static string DetailCPTransRequireContract(
        string cp1id,
        string status
    ) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransRequireContract(cp1id);

        if (data.GetLength(0) > 0)
            html += eCPDataBreakContract.DetailCPTransBreakRequireContract(cp1id, data, status);

        return html;
    }
    
    public static string AddCPTransRequireContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransBreakContract(cp1id);

        if (data.GetLength(0) > 0)
            html += AddUpdateCPTransRequireContract("add", data);

        return html;
    }

    public static string UpdateCPTransRequireContract(string cp1id) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTransRequireContract(cp1id);

        if (data.GetLength(0) > 0)
            html += AddUpdateCPTransRequireContract("update", data);

        return html;
    }

    public static string ListSearchRepayStatusCPTransRequireContract(string cp1id) {
        string repayStatus = eCPDB.ChkRepayStatusCPTransRequireContract(cp1id);

        return ("<repaystatus>" + repayStatus + "<repaystatus>");
    }

    public static string TabCPTransRequireContract() {
        string html = string.Empty;
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        Array trackingStatus = (section.Equals(1) ? eCPUtil.trackingStatusORLA : eCPUtil.trackingStatusORAA);

        html += (
            "<div id='cp-trans-require-contract-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-trans-require-contract") +
            "       <div class='content-data-tabs' id='tabs-cp-trans-require-contract'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-trans-require-contract' alt='#tab1-cp-trans-require-contract' href='javascript:void(0)'>ตรวจสอบรายการแจ้ง</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-trans-require-contract' alt='#tab2-cp-trans-require-contract' href='javascript:void(0)'>แจ้งชำระหนี้</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-trans-require-contract' alt='#tab3-cp-trans-require-contract' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-trans-require-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-trans-break-contract' value=''>" +
            "                   <input type='hidden' id='trackingstatus-trans-break-contract-hidden' value='2'>" +
            "                   <input type='hidden' id='trackingstatus-trans-break-contract-text-hidden' value='" + trackingStatus.GetValue(0, 0) + "'>" +
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
            "       <div class='tab-content' id='tab2-cp-trans-require-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-trans-repay-contract' value=''>" +
            "                   <input type='hidden' id='repaystatus-trans-repay-contract-hidden' value=''>" +
            "                   <input type='hidden' id='repaystatus-trans-repay-contract-text-hidden' value=''>" +
            "                   <input type='hidden' id='id-name-trans-repay-contract-hidden' value=''>" +
            "                   <input type='hidden' id='faculty-trans-repay-contract-hidden' value=''>" +
            "                   <input type='hidden' id='program-trans-repay-contract-hidden' value=''>" +
            "                   <input type='hidden' id='date-start-trans-repay-contract-hidden' value=''>" +
            "                   <input type='hidden' id='date-end-trans-repay-contract-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchcptransrepaycontract',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-trans-repay-contract'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-trans-repay-contract-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>สถานะการแจ้งชำระหนี้</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order2'>" +
            "                   <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order2-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order3'>" +
            "                   <div class='box-search-condition-order-title'>คณะ</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order3-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order4'>" +
            "                   <div class='box-search-condition-order-title'>หลักสูตร</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order4-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "               <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order5'>" +
            "                   <div class='box-search-condition-order-title'>ช่วงวันที่รับรายการแจ้ง</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order5-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-trans-require-contract-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-trans-require-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='tab1-table-head-cp-trans-require-contract-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col4'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col5'>" +
            "                       <div class='table-head-line1'>ส่งรายการแจ้ง</div>" +
            "                       <div>เมื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col6'>" +
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
            "   <div class='tab-content' id='tab2-cp-trans-require-contract-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='tab2-table-head-repay-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col2'>" +
            "                       <div class='table-head-line1'>รหัส</div>" +
            "                       <div>นักศึกษา</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ - นามสกุล</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col4'>" +
            "                       <div class='table-head-line1'>หลักสูตร</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col5'>" +
            "                       <div class='table-head-line1'>รับรายการแจ้ง</div>" +
            "                       <div>เมื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col6'>" +
            "                       <div class='table-head-line1'>ระยะเวลา</div>" +
            "                       <div>ผิดนัดชำระ</div>" +
            "                       <div>( วัน )</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab2-table-head-repay-col7'>" +
            "                       <div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div>" +
            "                       <div>&nbsp;</div>" +
            "                       <div>" +
            "                           <a class='text-underline' href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>ความหมาย</a>" +
            "                       </div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-trans-require-contract-contents'></div>" +
            "</div>" +
            "<div id='cp-trans-require-contract-content'>" +
            "   <div class='tab-content' id='tab1-cp-trans-require-contract-content'>" +
            "       <div class='box4' id='list-data-trans-break-contract'></div>" +
            "       <div id='nav-page-trans-break-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-trans-require-contract-content'>" +
            "       <div class='box4' id='list-data-trans-repay-contract'></div>" +
            "       <div id='nav-page-trans-repay-contract'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-trans-require-contract-content'>" +
            "       <div class='box1' id='addupdate-data-trans-require-contract'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}