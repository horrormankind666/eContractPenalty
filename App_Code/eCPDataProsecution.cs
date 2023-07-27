/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๔/๑๑/๒๕๖๔>
Modify date : <๒๙/๐๕/๒๕๖๖>
Description : <สำหรับการบันทึกข้อมูลรายละเอียดการฟ้องคดี>
=============================================
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class eCPDataProsecution {
    public static string AddUpdateCPTransProsecution(string cp2id) {
        string html = string.Empty;
        string RCID = string.Empty;
        string totalPenalty = string.Empty;
        string totalRemain = string.Empty;
        string statusPayment = string.Empty;
        string complaintLawyerDefault = string.Empty;
        string complaintBlackCaseNo = string.Empty;
        string complaintCapital = string.Empty;
        string complaintInterest = string.Empty;
        string complaintActionDate = string.Empty;
        string judgmentLawyerDefault = string.Empty;
        string judgmentRedCaseNo = string.Empty;
        string judgmentResult = string.Empty;
        string judgmentCopy = string.Empty;
        string judgmentRemark = string.Empty;
        string judgmentActionDate = string.Empty;
        string executionLawyerDefault = string.Empty;        
        string executionDate = string.Empty;
        string executionCopy = string.Empty;
        string executionActionDate = string.Empty;
        string executionWithdrawLawyerDefault = string.Empty;
        string executionWithdrawDate = string.Empty;
        string executionWithdrawReason = string.Empty;
        string executionWithdrawCopy = string.Empty;
        string executionWithdrawActionDate = string.Empty;
                
        dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(eCPDB.ListCPTransProsecution(cp2id));
        JArray data = jsonObject;

		if (data.Count > 0) {
			JObject dr = jsonObject[0];
            dynamic eCPTransRequireContract = dr["eCPTransRequireContract"];
            dynamic eCPTransProsecution = dr["eCPTransProsecution"];
            dynamic eCPTransPayment = dr["eCPTransPayment"];

            dynamic lawyer = eCPTransRequireContract["lawyer"];
            dynamic complaint = eCPTransProsecution["complaint"];
            dynamic complaintLawyer = complaint["lawyer"];
            dynamic judgment = eCPTransProsecution["judgment"];
            dynamic judgmentLawyer = judgment["lawyer"];
            dynamic execution = eCPTransProsecution["execution"];
            dynamic executionLawyer = execution["lawyer"];
            dynamic executionWithdraw = eCPTransProsecution["executionWithdraw"];
            dynamic executionWithdrawLawyer = executionWithdraw["lawyer"];
            dynamic lastPayment = eCPTransPayment["lastPayment"];

            RCID = eCPTransProsecution["RCID"];
            totalPenalty = eCPTransRequireContract["subtotalPenalty"];
            totalRemain = lastPayment["totalRemain"];
            statusPayment = eCPTransRequireContract["statusPayment"];
            complaintLawyerDefault = complaintLawyer["fullName"];
            complaintBlackCaseNo = complaint["blackCaseNo"];
            complaintCapital = complaint["capital"];
            complaintInterest = complaint["interest"];
            complaintActionDate = complaint["actionDate"];
            judgmentLawyerDefault = judgmentLawyer["fullName"];
            judgmentRedCaseNo = judgment["redCaseNo"];
            judgmentResult = judgment["verdict"];
            judgmentCopy = judgment["copy"];
            judgmentRemark = judgment["remark"];
            judgmentActionDate = judgment["actionDate"];
            executionLawyerDefault = executionLawyer["fullName"];
            executionDate = execution["date"];
            executionCopy = execution["copy"];
            executionActionDate = execution["actionDate"];
            executionWithdrawLawyerDefault = executionWithdrawLawyer["fullName"];
            executionWithdrawDate = executionWithdraw["date"];
            executionWithdrawReason = executionWithdraw["reason"];
            executionWithdrawCopy = executionWithdraw["copy"];
            executionWithdrawActionDate = executionWithdraw["actionDate"];
        }

        string userid = eCPUtil.GetUserID();
        string[,] data1 = eCPDB.ListDetailCPTabUser(userid, "", "", "");
        string complaintLawyerFullname = data1[0, 3];
        string complaintLawyerPhoneNumber = data1[0, 6];
        string complaintLawyerMobileNumber = data1[0, 7];
        string complaintLawyerEmail = data1[0, 8];
        string judgmentLawyerFullname = data1[0, 3];
        string judgmentLawyerPhoneNumber = data1[0, 6];
        string judgmentLawyerMobileNumber = data1[0, 7];
        string judgmentLawyerEmail = data1[0, 8];
        string executionLawyerFullname = data1[0, 3];
        string executionLawyerPhoneNumber = data1[0, 6];
        string executionLawyerMobileNumber = data1[0, 7];
        string executionLawyerEmail = data1[0, 8];
        string executionWithdrawLawyerFullname = data1[0, 3];
        string executionWithdrawLawyerPhoneNumber = data1[0, 6];
        string executionWithdrawLawyerMobileNumber = data1[0, 7];
        string executionWithdrawLawyerEmail = data1[0, 8];

        html += (
            "<div class='form-content' id='addupdate-cp-trans-prosecution'>" +
            "   <input type='hidden' id='statuspayment-hidden' value='" + statusPayment + "' />" +
            "   <input type='hidden' id='totalpenalty-hidden' value='" + (!string.IsNullOrEmpty(totalPenalty) ? double.Parse(totalPenalty).ToString("#,##0.00") : string.Empty) + "' />" +
            "   <input type='hidden' id='totalremain-hidden' value='" + (!string.IsNullOrEmpty(totalRemain) ? double.Parse(totalRemain).ToString("#,##0.00") : string.Empty) + "' />" +
            "   <input type='hidden' id='RCID-hidden' value='" + RCID + "' />" +
            "   <input type='hidden' id='complaint-lawyer-hidden' value='" + (!string.IsNullOrEmpty(complaintLawyerDefault) ? complaintLawyerDefault : complaintLawyerFullname) + "' />" +
            "   <input type='hidden' id='complaint-lawyer-fullname-hidden' value='" + complaintLawyerFullname + "' />" +
            "   <input type='hidden' id='complaint-lawyer-phonenumber-hidden' value='" + complaintLawyerPhoneNumber + "' />" +
            "   <input type='hidden' id='complaint-lawyer-mobilenumber-hidden' value='" + complaintLawyerMobileNumber + "' />" +
            "   <input type='hidden' id='complaint-lawyer-email-hidden' value='" + complaintLawyerEmail + "' />" +
            "   <input type='hidden' id='complaint-blackcaseno-hidden' value='" + complaintBlackCaseNo + "' />" +
            "   <input type='hidden' id='complaint-capital-hidden' value='" + (!string.IsNullOrEmpty(complaintCapital) ? double.Parse(complaintCapital).ToString("#,##0.00") : string.Empty) + "' />" +
            "   <input type='hidden' id='complaint-interest-hidden' value='" + (!string.IsNullOrEmpty(complaintInterest) ? double.Parse(complaintInterest).ToString("#,##0.00") : "0.00") + "' />" +
            "   <input type='hidden' id='complaint-actiondate-hidden' value='" + complaintActionDate + "' />" +
            "   <input type='hidden' id='judgment-lawyer-hidden' value='" + (!string.IsNullOrEmpty(judgmentLawyerDefault) ? judgmentLawyerDefault : judgmentLawyerFullname) + "' />" +
            "   <input type='hidden' id='judgment-lawyer-fullname-hidden' value='" + judgmentLawyerFullname + "' />" +
            "   <input type='hidden' id='judgment-lawyer-phonenumber-hidden' value='" + judgmentLawyerPhoneNumber + "' />" +
            "   <input type='hidden' id='judgment-lawyer-mobilenumber-hidden' value='" + judgmentLawyerMobileNumber + "' />" +
            "   <input type='hidden' id='judgment-lawyer-email-hidden' value='" + judgmentLawyerEmail + "' />" +
            "   <input type='hidden' id='judgment-redcaseno-hidden' value='" + judgmentRedCaseNo + "' />" +
            "   <input type='hidden' id='judgment-result-hidden' value='" + judgmentResult + "' />" +
            "   <input type='hidden' id='judgment-copy-hidden' value='" + judgmentCopy + "' />" +
            "   <input type='hidden' id='judgment-remark-hidden' value='" + judgmentRemark + "' />" +
            "   <input type='hidden' id='judgment-actiondate-hidden' value='" + judgmentActionDate + "' />" +
            "   <input type='hidden' id='execution-lawyer-hidden' value='" + (!string.IsNullOrEmpty(executionLawyerDefault) ? executionLawyerDefault : executionLawyerFullname) + "' />" +
            "   <input type='hidden' id='execution-lawyer-fullname-hidden' value='" + executionLawyerFullname + "' />" +
            "   <input type='hidden' id='execution-lawyer-phonenumber-hidden' value='" + executionLawyerPhoneNumber + "' />" +
            "   <input type='hidden' id='execution-lawyer-mobilenumber-hidden' value='" + executionLawyerMobileNumber + "' />" +
            "   <input type='hidden' id='execution-lawyer-email-hidden' value='" + executionLawyerEmail + "' />" +
            "   <input type='hidden' id='execution-date-hidden' value='" + executionDate + "' />" +
            "   <input type='hidden' id='execution-copy-hidden' value='" + executionCopy + "' />" +
            "   <input type='hidden' id='execution-actiondate-hidden' value='" + executionActionDate + "' />" +
            "   <input type='hidden' id='executionwithdraw-lawyer-hidden' value='" + (!string.IsNullOrEmpty(executionWithdrawLawyerDefault) ? executionWithdrawLawyerDefault : executionWithdrawLawyerFullname) + "' />" +
            "   <input type='hidden' id='executionwithdraw-lawyer-fullname-hidden' value='" + executionWithdrawLawyerFullname + "' />" +
            "   <input type='hidden' id='executionwithdraw-lawyer-phonenumber-hidden' value='" + executionWithdrawLawyerPhoneNumber + "' />" +
            "   <input type='hidden' id='executionwithdraw-lawyer-mobilenumber-hidden' value='" + executionWithdrawLawyerMobileNumber + "' />" +
            "   <input type='hidden' id='executionwithdraw-lawyer-email-hidden' value='" + executionWithdrawLawyerEmail + "' />" +
            "   <input type='hidden' id='executionwithdraw-date-hidden' value='" + executionWithdrawDate + "' />" +
            "   <input type='hidden' id='executionwithdraw-reason-hidden' value='" + executionWithdrawReason + "' />" +
            "   <input type='hidden' id='executionwithdraw-copy-hidden' value='" + executionWithdrawCopy + "' />" +
            "   <input type='hidden' id='executionwithdraw-actiondate-hidden' value='" + executionWithdrawActionDate + "' />" +
            "   <div class='overlay hidden'></div>" +
            "   <div id='complaint'>" +
            "       <div class='content-left' id='complaint-label'>" +
            "           <div class='form-label-discription-style clear-bottom'>" +
            "               <div class='form-label-style'>คำฟ้อง</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>" +
            "                       <span class='hidden' id='complaint-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='complaint-actiondate'></span></span>" +
            "                       <span class='hidden' id='complaint-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
            "                   </div>" +
            "                   <div class='form-discription-line2-style'>" +
            "                       นิติกรผู้รับผิดชอบ คุณ<span id='complaint-lawyer'></span>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='complaint-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <div>" +
            "                       <div class='content-left' id='black-case-no-label'>คดีหมายเลขดำที่</div>" +
            "                       <div class='content-left' id='black-case-no-input'>" +
            "                           <input class='inputbox' type='text' id='complaint-blackcaseno' onblur=Trim('complaint-blackcaseno'); value='' style='width:190px' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div id='capital-summary'>" +
            "                       <div class='content-left' id='capital-summary-label'>ทุนทรัพย์</div>" +
            "                       <div class='content-left' id='capital-summary-input'>" +
            "                           <div>" +
            "                               <div class='content-left label'>เงินต้นคงค้าง</div>" +
            "                               <div class='content-left input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='complaint-capital' value='' style='width:121px' />" +
            "                               </div>" +
            "                               <div class='content-left unit'>บาท</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left label'>ดอกเบี้ยถึงวันฟ้อง</div>" +
            "                               <div class='content-left input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='complaint-interest' onblur=Trim('complaint-interest');AddCommas('complaint-interest',2);CalTotalPayment() onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:121px' />" +
            "                               </div>" +
            "                               <div class='content-left unit'>บาท</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left label'>รวม</div>" +
            "                               <div class='content-left input'>" +
            "                                   <input class='inputbox textbox-numeric' type='text' id='complaint-totalpayment' value='' style='width:121px' />" +
            "                               </div>" +
            "                               <div class='content-left unit'>บาท</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +                 
            "           <div class='form-input-style button-action clear-bottom'>" +
            "               <div class='form-input-content'>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('complaint')>บันทึก</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=GoToElement('complaint'); ResetFrmCPTransProsecutionWithDoc('complaint')>ล้าง</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='box3'></div>" +
            "   <div id='judgment'>" +
            "       <div class='content-left' id='judgment-label'>" +
            "           <div class='form-label-discription-style clear-bottom'>" +
            "               <div class='form-label-style'>คำพิพากษา</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>" +
            "                       <span class='hidden' id='judgment-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='judgment-actiondate'></span></span>" +
            "                       <span class='hidden' id='judgment-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
            "                   </div>" +
            "                   <div class='form-discription-line2-style'>" +
            "                       นิติกรผู้รับผิดชอบ คุณ<span id='judgment-lawyer'></span>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='judgment-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <div>" +
            "                       <div class='content-left' id='red-case-no-label'>คดีหมายเลขแดงที่</div>" +
            "                       <div class='content-left' id='red-case-no-input'>" +
            "                           <input class='inputbox' type='text' id='judgment-redcaseno' onblur=Trim('judgment-redcaseno'); value='' style='width:190px' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='judgment-result-label'>ผลคำพิพากษา</div>" +
            "                       <div class='content-left' id='judgment-result-input'>" +
            "                           <div>" +
            "                               <div class='content-left' id='judgment-result-dismiss-input'>" +
            "                                   <input class='radio' type='radio' name='judgment-result' value='D' />" +
            "                               </div>" +
            "                               <div class='content-left' id='judgment-result-dismiss-label'>ยกฟ้อง</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='judgment-result-lawful-input'>" +
            "                                   <input class='radio' type='radio' name='judgment-result' value='L' />" +
            "                               </div>" +
            "                               <div class='content-left' id='judgment-result-lawful-label'>ตามฟ้อง</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                           <div>" +
            "                               <div class='content-left' id='judgment-result-lawful-some-input'>" +
            "                                   <input class='radio' type='radio' name='judgment-result' value='LS' />" +
            "                               </div>" +
            "                               <div class='content-left' id='judgment-result-lawful-some-label'>ตามบางส่วน</div>" +
            "                           </div>" +
            "                           <div class='clear'></div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='judgment-copy-label'>อัพโหลดไฟล์</div>" +
            "                       <div class='content-left' id='judgment-copy-input'>" +
            "                           <input type='hidden' id='judgment-copy' value=''>" +
            "                           <div class='uploadfile-container'>" +
            "                               <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
            "                               <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
            "                                   <div class='uploadfile-button browse'>" +
            "                                       <span>" +
            "                                           <a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='judgment-copy-file' /></a>" +
            "                                       </span>" +
            "                                   </div>" +
            "                               </form>" +
            "                               <div class='form-discription-style'>" +
            "                                   <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div id='judgment-copy-preview-container'>" +
            "                               <canvas class='hidden' id='judgment-copy-preview'></canvas>" +
            "                               <div class='hidden' id='judgment-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
            "                               <div class='hidden' id='judgment-copy-linkpreview'>" +
            "                                   <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
            "                                   <form id='download-judgmentcopy-form' action='FileProcess.aspx' method='POST' target='download-judgmentcopy'>" +
            "                                       <input type='hidden' id='action' name='action' value='download' />" +
            "                                       <input type='hidden' id='filename' name='filename' value='JudgmentCopy' />" +
            "                                       <input type='hidden' id='file' name='file' value='' />" +
            "                                   </form>" +
            "                                   <iframe class='export-target' id='download-judgmentcopy' name='download-judgmentcopy'></iframe>" +
            "                               </div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div id='judgment-remark-container'>" +
            "                       <div class='content-left' id='judgment-remark-label'>ข้อมูลเพิ่มเติม ( ถ้ามี )</div>" +
            "                       <div class='content-left' id='judgment-remark-input'>" +
            "                           <textarea class='textareabox' id='judgment-remark' onblur=Trim('judgment-remark') style='width:380px;height:90px;'></textarea>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style button-action clear-bottom'>" +
            "               <div class='form-input-content'>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('judgment')>บันทึก</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=GoToElement('judgment'); ResetFrmCPTransProsecutionWithDoc('judgment')>ล้าง</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='box3'></div>" +
            "   <div id='execution'>" +
            "       <div class='content-left' id='execution-label'>" +
            "           <div class='form-label-discription-style clear-bottom'>" +
            "               <div class='form-label-style'>หมายบังคับคดี</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>" +
            "                       <span class='hidden' id='execution-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='execution-actiondate'></span></span>" +
            "                       <span class='hidden' id='execution-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
            "                   </div>" +
            "                   <div class='form-discription-line2-style'>" +
            "                       นิติกรผู้รับผิดชอบ คุณ<span id='execution-lawyer'></span>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='execution-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <div>" +
            "                       <div class='content-left' id='execution-date-label'>ลงวันที่</div>" +
            "                       <div class='content-left' id='execution-date-input'>" +
            "                           <input class='inputbox calendar' type='text' id='execution-date' readonly value='' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='execution-copy-label'>อัพโหลดไฟล์</div>" +
            "                       <div class='content-left' id='execution-copy-input'>" +
            "                           <input type='hidden' id='execution-copy' value=''>" +
            "                           <div class='uploadfile-container'>" +
            "                               <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
            "                               <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
            "                                   <div class='uploadfile-button browse'>" +
            "                                       <span>" +
            "                                           <a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='execution-copy-file' /></a>" +
            "                                       </span>" +
            "                                   </div>" +
            "                               </form>" +
            "                               <div class='form-discription-style'>" +
            "                                   <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div id='execution-copy-preview-container'>" +
            "                               <canvas class='hidden' id='execution-copy-preview'></canvas>" +
            "                               <div class='hidden' id='execution-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
            "                               <div class='hidden' id='execution-copy-linkpreview'>" +
            "                                   <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
            "                                   <form id='download-executioncopy-form' action='FileProcess.aspx' method='POST' target='download-executioncopy'>" +
            "                                       <input type='hidden' id='action' name='action' value='download' />" +
            "                                       <input type='hidden' id='filename' name='filename' value='ExecutionCopy' />" +
            "                                       <input type='hidden' id='file' name='file' value='' />" +
            "                                   </form>" +
            "                                   <iframe class='export-target' id='download-executioncopy' name='download-executioncopy'></iframe>" +
            "                               </div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style button-action clear-bottom'>" +
            "               <div class='form-input-content'>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('execution')>บันทึก</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=GoToElement('execution'); ResetFrmCPTransProsecutionWithDoc('execution')>ล้าง</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "   <div class='box3'></div>" +
            "   <div id='executionwithdraw'>" +
            "       <div class='content-left' id='executionwithdraw-label'>" +
            "           <div class='form-label-discription-style clear-bottom'>" +
            "               <div class='form-label-style'>หมายถอนการบังคับคดี</div>" +
            "               <div class='form-discription-style'>" +
            "                   <div class='form-discription-line1-style'>" +
            "                       <span class='hidden' id='executionwithdraw-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='executionwithdraw-actiondate'></span></span>" +
            "                       <span class='hidden' id='executionwithdraw-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
            "                   </div>" +
            "                   <div class='form-discription-line2-style'>" +
            "                       นิติกรผู้รับผิดชอบ คุณ<span id='executionwithdraw-lawyer'></span>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='content-left' id='executionwithdraw-input'>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content'>" +
            "                   <div>" +
            "                       <div class='content-left' id='executionwithdraw-date-label'>ลงวันที่</div>" +
            "                       <div class='content-left' id='executionwithdraw-date-input'>" +
            "                           <input class='inputbox calendar' type='text' id='executionwithdraw-date' readonly value='' />" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div>" +
            "                       <div class='content-left' id='executionwithdraw-reason-label'>เหตุ</div>" +
            "                       <div class='content-left' id='executionwithdraw-reason-input'>" +
            "                           <textarea class='textareabox' id='executionwithdraw-reason' onblur=Trim('executionwithdraw-reason') style='width:380px;height:90px;'></textarea>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "                   <div id='executionwithdraw-copy-container'>" +
            "                       <div class='content-left' id='executionwithdraw-copy-label'>อัพโหลดไฟล์</div>" +
            "                       <div class='content-left' id='executionwithdraw-copy-input'>" +
            "                           <input type='hidden' id='executionwithdraw-copy' value=''>" +
            "                           <div class='uploadfile-container'>" +
            "                               <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
            "                               <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
            "                                   <div class='uploadfile-button browse'>" +
            "                                       <span>" +
            "                                           <a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='executionwithdraw-copy-file' /></a>" +
            "                                       </span>" +
            "                                   </div>" +
            "                               </form>" +
            "                               <div class='form-discription-style'>" +
            "                                   <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
            "                               </div>" +
            "                           </div>" +
            "                           <div id='executionwithdraw-copy-preview-container'>" +
            "                               <canvas class='hidden' id='executionwithdraw-copy-preview'></canvas>" +
            "                               <div class='hidden' id='executionwithdraw-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
            "                               <div class='hidden' id='executionwithdraw-copy-linkpreview'>" +
            "                                   <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
            "                                   <form id='download-executionwithdrawcopy-form' action='FileProcess.aspx' method='POST' target='download-executionwithdrawcopy'>" +
            "                                       <input type='hidden' id='action' name='action' value='download' />" +
            "                                       <input type='hidden' id='filename' name='filename' value='ExecutionWithdrawCopy' />" +
            "                                       <input type='hidden' id='file' name='file' value='' />" +
            "                                   </form>" +
            "                                   <iframe class='export-target' id='download-executionwithdrawcopy' name='download-executionwithdrawcopy'></iframe>" +
            "                               </div>" +
            "                           </div>" +
            "                       </div>" +
            "                   </div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style button-action clear-bottom'>" +
            "               <div class='form-input-content'>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('executionwithdraw')>บันทึก</a>" +
            "                           </li>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=GoToElement('executionwithdraw'); ResetFrmCPTransProsecutionWithDoc('executionwithdraw')>ล้าง</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='clear'></div>" +
            "</div>"
        );

        return html;
    }
}