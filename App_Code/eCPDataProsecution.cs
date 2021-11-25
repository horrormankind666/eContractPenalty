/*
Description         : สำหรับการบันทึกข้อมูลรายละเอียดการฟ้องคดี
Date Created        : ๐๔/๑๑/๒๕๖๔
Last Date Modified  : ๒๒/๑๑/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class eCPDataProsecution
{
    public static string AddUpdateCPTransProsecution(string _cp2id)
    {
        string _html = String.Empty;
        string _RCID = String.Empty;
        string _totalPenalty = String.Empty;
        string _totalRemain = String.Empty;
        string _statusPayment = String.Empty;
        string _complaintLawyerDefault = String.Empty;
        string _complaintLawyerFullname = String.Empty;
        string _complaintLawyerPhoneNumber = String.Empty;
        string _complaintLawyerMobileNumber = String.Empty;
        string _complaintLawyerEmail = String.Empty;
        string _complaintBlackCaseNo = String.Empty;
        string _complaintCapital = String.Empty;
        string _complaintInterest = String.Empty;
        string _complaintActionDate = String.Empty;
        string _judgmentLawyerDefault = String.Empty;
        string _judgmentLawyerFullname = String.Empty;
        string _judgmentLawyerPhoneNumber = String.Empty;
        string _judgmentLawyerMobileNumber = String.Empty;
        string _judgmentLawyerEmail = String.Empty;
        string _judgmentRedCaseNo = String.Empty;
        string _judgmentResult = String.Empty;
        string _judgmentCopy = String.Empty;
        string _judgmentRemark = String.Empty;
        string _judgmentActionDate = String.Empty;
        string _executionLawyerDefault = String.Empty;
        string _executionLawyerFullname = String.Empty;
        string _executionLawyerPhoneNumber = String.Empty;
        string _executionLawyerMobileNumber = String.Empty;
        string _executionLawyerEmail = String.Empty;
        string _executionDate = String.Empty;
        string _executionCopy = String.Empty;
        string _executionActionDate = String.Empty;
        string _executionWithdrawLawyerDefault = String.Empty;
        string _executionWithdrawLawyerFullname = String.Empty;
        string _executionWithdrawLawyerPhoneNumber = String.Empty;
        string _executionWithdrawLawyerMobileNumber = String.Empty;
        string _executionWithdrawLawyerEmail = String.Empty;
        string _executionWithdrawDate = String.Empty;
        string _executionWithdrawReason = String.Empty;
        string _executionWithdrawCopy = String.Empty;
        string _executionWithdrawActionDate = String.Empty;
                
        dynamic _jsonObject = JsonConvert.DeserializeObject<dynamic>(eCPDB.ListCPTransProsecution(_cp2id));
        JArray _data = _jsonObject;

		if (_data.Count > 0)
		{
			JObject _dr = _jsonObject[0];
            dynamic _eCPTransRequireContract = _dr["eCPTransRequireContract"];
            dynamic _eCPTransProsecution = _dr["eCPTransProsecution"];
            dynamic _eCPTransPayment = _dr["eCPTransPayment"];

            dynamic _lawyer = _eCPTransRequireContract["lawyer"];
            dynamic _complaint = _eCPTransProsecution["complaint"];
            dynamic _complaintLawyer = _complaint["lawyer"];
            dynamic _judgment = _eCPTransProsecution["judgment"];
            dynamic _judgmentLawyer = _judgment["lawyer"];
            dynamic _execution = _eCPTransProsecution["execution"];
            dynamic _executionLawyer = _execution["lawyer"];
            dynamic _executionWithdraw = _eCPTransProsecution["executionWithdraw"];
            dynamic _executionWithdrawLawyer = _executionWithdraw["lawyer"];
            dynamic _lastPayment = _eCPTransPayment["lastPayment"];

            _RCID = _eCPTransProsecution["RCID"];
            _totalPenalty = _eCPTransRequireContract["subtotalPenalty"];
            _totalRemain = _lastPayment["totalRemain"];
            _statusPayment = _eCPTransRequireContract["statusPayment"];
            _complaintLawyerDefault = _complaintLawyer["fullName"];
            _complaintBlackCaseNo = _complaint["blackCaseNo"];
            _complaintCapital = _complaint["capital"];
            _complaintInterest = _complaint["interest"];
            _complaintActionDate = _complaint["actionDate"];
            _judgmentLawyerDefault = _judgmentLawyer["fullName"];
            _judgmentRedCaseNo = _judgment["redCaseNo"];
            _judgmentResult = _judgment["verdict"];
            _judgmentCopy = _judgment["copy"];
            _judgmentRemark = _judgment["remark"];
            _judgmentActionDate = _judgment["actionDate"];
            _executionLawyerDefault = _executionLawyer["fullName"];
            _executionDate = _execution["date"];
            _executionCopy = _execution["copy"];
            _executionActionDate = _execution["actionDate"];
            _executionWithdrawLawyerDefault = _executionWithdrawLawyer["fullName"];
            _executionWithdrawDate = _executionWithdraw["date"];
            _executionWithdrawReason = _executionWithdraw["reason"];
            _executionWithdrawCopy = _executionWithdraw["copy"];
            _executionWithdrawActionDate = _executionWithdraw["actionDate"];
        }

        string _userid = eCPUtil.GetUserID();
        string[,] _data1 = eCPDB.ListDetailCPTabUser(_userid, "", "", "");
        _complaintLawyerFullname = _data1[0, 3];
        _complaintLawyerPhoneNumber = _data1[0, 6];
        _complaintLawyerMobileNumber = _data1[0, 7];
        _complaintLawyerEmail = _data1[0, 8];
        _judgmentLawyerFullname = _data1[0, 3];
        _judgmentLawyerPhoneNumber = _data1[0, 6];
        _judgmentLawyerMobileNumber = _data1[0, 7];
        _judgmentLawyerEmail = _data1[0, 8];
        _executionLawyerFullname = _data1[0, 3];
        _executionLawyerPhoneNumber = _data1[0, 6];
        _executionLawyerMobileNumber = _data1[0, 7];
        _executionLawyerEmail = _data1[0, 8];
        _executionWithdrawLawyerFullname = _data1[0, 3];
        _executionWithdrawLawyerPhoneNumber = _data1[0, 6];
        _executionWithdrawLawyerMobileNumber = _data1[0, 7];
        _executionWithdrawLawyerEmail = _data1[0, 8];

        _html += "<div class='form-content' id='addupdate-cp-trans-prosecution'>" +
                 "  <input type='hidden' id='statuspayment-hidden' value='" + _statusPayment + "' />" +
                 "  <input type='hidden' id='totalpenalty-hidden' value='" + (!String.IsNullOrEmpty(_totalPenalty) ? double.Parse(_totalPenalty).ToString("#,##0.00") : String.Empty) + "' />" +
                 "  <input type='hidden' id='totalremain-hidden' value='" + (!String.IsNullOrEmpty(_totalRemain) ? double.Parse(_totalRemain).ToString("#,##0.00") : String.Empty) + "' />" +
                 "  <input type='hidden' id='RCID-hidden' value='" + _RCID + "' />" +
                 "  <input type='hidden' id='complaint-lawyer-hidden' value='" + (!String.IsNullOrEmpty(_complaintLawyerDefault) ? _complaintLawyerDefault : _complaintLawyerFullname) + "' />" +
                 "  <input type='hidden' id='complaint-lawyer-fullname-hidden' value='" + _complaintLawyerFullname + "' />" +
                 "  <input type='hidden' id='complaint-lawyer-phonenumber-hidden' value='" + _complaintLawyerPhoneNumber + "' />" +
                 "  <input type='hidden' id='complaint-lawyer-mobilenumber-hidden' value='" + _complaintLawyerMobileNumber + "' />" +
                 "  <input type='hidden' id='complaint-lawyer-email-hidden' value='" + _complaintLawyerEmail + "' />" +
                 "  <input type='hidden' id='complaint-blackcaseno-hidden' value='" + _complaintBlackCaseNo + "' />" +
                 "  <input type='hidden' id='complaint-capital-hidden' value='" + (!String.IsNullOrEmpty(_complaintCapital) ? double.Parse(_complaintCapital).ToString("#,##0.00") : String.Empty) + "' />" +
                 "  <input type='hidden' id='complaint-interest-hidden' value='" + (!String.IsNullOrEmpty(_complaintInterest) ? double.Parse(_complaintInterest).ToString("#,##0.00") : "0.00") + "' />" +
                 "  <input type='hidden' id='complaint-actiondate-hidden' value='" + _complaintActionDate + "' />" +
                 "  <input type='hidden' id='judgment-lawyer-hidden' value='" + (!String.IsNullOrEmpty(_judgmentLawyerDefault) ? _judgmentLawyerDefault : _judgmentLawyerFullname) + "' />" +
                 "  <input type='hidden' id='judgment-lawyer-fullname-hidden' value='" + _judgmentLawyerFullname + "' />" +
                 "  <input type='hidden' id='judgment-lawyer-phonenumber-hidden' value='" + _judgmentLawyerPhoneNumber + "' />" +
                 "  <input type='hidden' id='judgment-lawyer-mobilenumber-hidden' value='" + _judgmentLawyerMobileNumber + "' />" +
                 "  <input type='hidden' id='judgment-lawyer-email-hidden' value='" + _judgmentLawyerEmail + "' />" +
                 "  <input type='hidden' id='judgment-redcaseno-hidden' value='" + _judgmentRedCaseNo + "' />" +
                 "  <input type='hidden' id='judgment-result-hidden' value='" + _judgmentResult + "' />" +
                 "  <input type='hidden' id='judgment-copy-hidden' value='" + _judgmentCopy + "' />" +
                 "  <input type='hidden' id='judgment-remark-hidden' value='" + _judgmentRemark + "' />" +
                 "  <input type='hidden' id='judgment-actiondate-hidden' value='" + _judgmentActionDate + "' />" +
                 "  <input type='hidden' id='execution-lawyer-hidden' value='" + (!String.IsNullOrEmpty(_executionLawyerDefault) ? _executionLawyerDefault : _executionLawyerFullname) + "' />" +
                 "  <input type='hidden' id='execution-lawyer-fullname-hidden' value='" + _executionLawyerFullname + "' />" +
                 "  <input type='hidden' id='execution-lawyer-phonenumber-hidden' value='" + _executionLawyerPhoneNumber + "' />" +
                 "  <input type='hidden' id='execution-lawyer-mobilenumber-hidden' value='" + _executionLawyerMobileNumber + "' />" +
                 "  <input type='hidden' id='execution-lawyer-email-hidden' value='" + _executionLawyerEmail + "' />" +
                 "  <input type='hidden' id='execution-date-hidden' value='" + _executionDate + "' />" +
                 "  <input type='hidden' id='execution-copy-hidden' value='" + _executionCopy + "' />" +
                 "  <input type='hidden' id='execution-actiondate-hidden' value='" + _executionActionDate + "' />" +
                 "  <input type='hidden' id='executionwithdraw-lawyer-hidden' value='" + (!String.IsNullOrEmpty(_executionWithdrawLawyerDefault) ? _executionWithdrawLawyerDefault : _executionWithdrawLawyerFullname) + "' />" +
                 "  <input type='hidden' id='executionwithdraw-lawyer-fullname-hidden' value='" + _executionWithdrawLawyerFullname + "' />" +
                 "  <input type='hidden' id='executionwithdraw-lawyer-phonenumber-hidden' value='" + _executionWithdrawLawyerPhoneNumber + "' />" +
                 "  <input type='hidden' id='executionwithdraw-lawyer-mobilenumber-hidden' value='" + _executionWithdrawLawyerMobileNumber + "' />" +
                 "  <input type='hidden' id='executionwithdraw-lawyer-email-hidden' value='" + _executionWithdrawLawyerEmail + "' />" +
                 "  <input type='hidden' id='executionwithdraw-date-hidden' value='" + _executionWithdrawDate + "' />" +
                 "  <input type='hidden' id='executionwithdraw-reason-hidden' value='" + _executionWithdrawReason + "' />" +
                 "  <input type='hidden' id='executionwithdraw-copy-hidden' value='" + _executionWithdrawCopy + "' />" +
                 "  <input type='hidden' id='executionwithdraw-actiondate-hidden' value='" + _executionWithdrawActionDate + "' />" +
                 "  <div class='overlay hidden'></div>" +
                 "  <div id='complaint'>" +
                 "      <div class='content-left' id='complaint-label'>" +
                 "          <div class='form-label-discription-style clear-bottom'>" +
                 "              <div class='form-label-style'>คำฟ้อง</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>" +
                 "                      <span class='hidden' id='complaint-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='complaint-actiondate'></span></span>" +
                 "                      <span class='hidden' id='complaint-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
                 "                  </div>" +
                 "                  <div class='form-discription-line2-style'>นิติกรผู้รับผิดชอบ คุณ<span id='complaint-lawyer'></span></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='content-left' id='complaint-input'>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='black-case-no-label'>คดีหมายเลขดำที่</div>" +
                 "                      <div class='content-left' id='black-case-no-input'><input class='inputbox' type='text' id='complaint-blackcaseno' onblur=Trim('complaint-blackcaseno'); value='' style='width:190px' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div id='capital-summary'>" +
                 "                      <div class='content-left' id='capital-summary-label'>ทุนทรัพย์</div>" +
                 "                      <div class='content-left' id='capital-summary-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left label'>เงินต้นคงค้าง</div>" +
                 "                              <div class='content-left input'><input class='inputbox textbox-numeric' type='text' id='complaint-capital' value='' style='width:121px' /></div>" +
                 "                              <div class='content-left unit'>บาท</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left label'>ดอกเบี้ยถึงวันฟ้อง</div>" +
                 "                              <div class='content-left input'><input class='inputbox textbox-numeric' type='text' id='complaint-interest' onblur=Trim('complaint-interest');AddCommas('complaint-interest',2);CalTotalPayment() onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:121px' /></div>" +
                 "                              <div class='content-left unit'>บาท</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left label'>รวม</div>" +
                 "                              <div class='content-left input'><input class='inputbox textbox-numeric' type='text' id='complaint-totalpayment' value='' style='width:121px' /></div>" +
                 "                              <div class='content-left unit'>บาท</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +                 
                 "          <div class='form-input-style button-action clear-bottom'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('complaint')>บันทึก</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=GoToElement('complaint'); ResetFrmCPTransProsecutionWithDoc('complaint')>ล้าง</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='box3'></div>" +
                 "  <div id='judgment'>" +
                 "      <div class='content-left' id='judgment-label'>" +
                 "          <div class='form-label-discription-style clear-bottom'>" +
                 "              <div class='form-label-style'>คำพิพากษา</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>" +
                 "                      <span class='hidden' id='judgment-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='judgment-actiondate'></span></span>" +
                 "                      <span class='hidden' id='judgment-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
                 "                  </div>" +
                 "                  <div class='form-discription-line2-style'>นิติกรผู้รับผิดชอบ คุณ<span id='judgment-lawyer'></span></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='content-left' id='judgment-input'>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='red-case-no-label'>คดีหมายเลขแดงที่</div>" +
                 "                      <div class='content-left' id='red-case-no-input'><input class='inputbox' type='text' id='judgment-redcaseno' onblur=Trim('judgment-redcaseno'); value='' style='width:190px' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='judgment-result-label'>ผลคำพิพากษา</div>" +
                 "                      <div class='content-left' id='judgment-result-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='judgment-result-dismiss-input'><input class='radio' type='radio' name='judgment-result' value='D' /></div>" +
                 "                              <div class='content-left' id='judgment-result-dismiss-label'>ยกฟ้อง</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='judgment-result-lawful-input'><input class='radio' type='radio' name='judgment-result' value='L' /></div>" +
                 "                              <div class='content-left' id='judgment-result-lawful-label'>ตามฟ้อง</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='judgment-result-lawful-some-input'><input class='radio' type='radio' name='judgment-result' value='LS' /></div>" +
                 "                              <div class='content-left' id='judgment-result-lawful-some-label'>ตามบางส่วน</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='judgment-copy-label'>อัพโหลดไฟล์</div>" +
                 "                      <div class='content-left' id='judgment-copy-input'>" +
                 "                          <input type='hidden' id='judgment-copy' value=''>" +
                 "                          <div class='uploadfile-container'>" +
                 "                              <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
                 "                              <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
                 "                                  <div class='uploadfile-button browse'>" +
                 "                                      <span><a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='judgment-copy-file' /></a></span>" +
                 "                                  </div>" +
                 "                              </form>" +
                 "                              <div class='form-discription-style'>" +
                 "                                  <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div id='judgment-copy-preview-container'>" +
                 "                              <canvas class='hidden' id='judgment-copy-preview'></canvas>" +
                 "                              <div class='hidden' id='judgment-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
                 "                              <div class='hidden' id='judgment-copy-linkpreview'>" +
                 "                                  <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
                 "                                  <form id='download-judgmentcopy-form' action='FileProcess.aspx' method='POST' target='download-judgmentcopy'>" +
                 "                                      <input type='hidden' id='action' name='action' value='download' />" +
                 "                                      <input type='hidden' id='filename' name='filename' value='JudgmentCopy' />" +
                 "                                      <input type='hidden' id='file' name='file' value='' />" +
                 "                                  </form>" +
                 "                                  <iframe class='export-target' id='download-judgmentcopy' name='download-judgmentcopy'></iframe>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div id='judgment-remark-container'>" +
                 "                      <div class='content-left' id='judgment-remark-label'>ข้อมูลเพิ่มเติม ( ถ้ามี )</div>" +
                 "                      <div class='content-left' id='judgment-remark-input'><textarea class='textareabox' id='judgment-remark' onblur=Trim('judgment-remark') style='width:380px;height:90px;'></textarea></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style button-action clear-bottom'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('judgment')>บันทึก</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=GoToElement('judgment'); ResetFrmCPTransProsecutionWithDoc('judgment')>ล้าง</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='box3'></div>" +
                 "  <div id='execution'>" +
                 "      <div class='content-left' id='execution-label'>" +
                 "          <div class='form-label-discription-style clear-bottom'>" +
                 "              <div class='form-label-style'>หมายบังคับคดี</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>" +
                 "                      <span class='hidden' id='execution-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='execution-actiondate'></span></span>" +
                 "                      <span class='hidden' id='execution-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
                 "                  </div>" +
                 "                  <div class='form-discription-line2-style'>นิติกรผู้รับผิดชอบ คุณ<span id='execution-lawyer'></span></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='content-left' id='execution-input'>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='execution-date-label'>ลงวันที่</div>" +
                 "                      <div class='content-left' id='execution-date-input'><input class='inputbox calendar' type='text' id='execution-date' readonly value='' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='execution-copy-label'>อัพโหลดไฟล์</div>" +
                 "                      <div class='content-left' id='execution-copy-input'>" +
                 "                          <input type='hidden' id='execution-copy' value=''>" +
                 "                          <div class='uploadfile-container'>" +
                 "                              <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
                 "                              <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
                 "                                  <div class='uploadfile-button browse'>" +
                 "                                      <span><a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='execution-copy-file' /></a></span>" +
                 "                                  </div>" +
                 "                              </form>" +
                 "                              <div class='form-discription-style'>" +
                 "                                  <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div id='execution-copy-preview-container'>" +
                 "                              <canvas class='hidden' id='execution-copy-preview'></canvas>" +
                 "                              <div class='hidden' id='execution-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
                 "                              <div class='hidden' id='execution-copy-linkpreview'>" +
                 "                                  <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
                 "                                  <form id='download-executioncopy-form' action='FileProcess.aspx' method='POST' target='download-executioncopy'>" +
                 "                                      <input type='hidden' id='action' name='action' value='download' />" +
                 "                                      <input type='hidden' id='filename' name='filename' value='ExecutionCopy' />" +
                 "                                      <input type='hidden' id='file' name='file' value='' />" +
                 "                                  </form>" +
                 "                                  <iframe class='export-target' id='download-executioncopy' name='download-executioncopy'></iframe>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style button-action clear-bottom'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('execution')>บันทึก</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=GoToElement('execution'); ResetFrmCPTransProsecutionWithDoc('execution')>ล้าง</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='box3'></div>" +
                 "  <div id='executionwithdraw'>" +
                 "      <div class='content-left' id='executionwithdraw-label'>" +
                 "          <div class='form-label-discription-style clear-bottom'>" +
                 "              <div class='form-label-style'>หมายถอนการบังคับคดี</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>" +
                 "                      <span class='hidden' id='executionwithdraw-actiondate-y'>บันทึกข้อมูลเมื่อวันที่ <span id='executionwithdraw-actiondate'></span></span>" +
                 "                      <span class='hidden' id='executionwithdraw-actiondate-n'>ไม่พบการบันทึกข้อมูล</span>" +
                 "                  </div>" +
                 "                  <div class='form-discription-line2-style'>นิติกรผู้รับผิดชอบ คุณ<span id='executionwithdraw-lawyer'></span></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='content-left' id='executionwithdraw-input'>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='executionwithdraw-date-label'>ลงวันที่</div>" +
                 "                      <div class='content-left' id='executionwithdraw-date-input'><input class='inputbox calendar' type='text' id='executionwithdraw-date' readonly value='' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='executionwithdraw-reason-label'>เหตุ</div>" +
                 "                      <div class='content-left' id='executionwithdraw-reason-input'><textarea class='textareabox' id='executionwithdraw-reason' onblur=Trim('executionwithdraw-reason') style='width:380px;height:90px;'></textarea></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "                  <div id='executionwithdraw-copy-container'>" +
                 "                      <div class='content-left' id='executionwithdraw-copy-label'>อัพโหลดไฟล์</div>" +
                 "                      <div class='content-left' id='executionwithdraw-copy-input'>" +
                 "                          <input type='hidden' id='executionwithdraw-copy' value=''>" +
                 "                          <div class='uploadfile-container'>" +
                 "                              <img class='preloading-inline hidden' src='Image/PreloadingInline.gif' />" +
                 "                              <form class='uploadfile-form' method='post' enctype='multipart/form-data'>" +
                 "                                  <div class='uploadfile-button browse'>" +
                 "                                      <span><a class='text-underline' href='javascript:void(0)'>เลือกเอกสาร<input type='file' id='executionwithdraw-copy-file' /></a></span>" +
                 "                                  </div>" +
                 "                              </form>" +
                 "                              <div class='form-discription-style'>" +
                 "                                  <div class='form-discription-line1-style'>( เฉพาะไฟล์นามสกุล .pdf )</div>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div id='executionwithdraw-copy-preview-container'>" +
                 "                              <canvas class='hidden' id='executionwithdraw-copy-preview'></canvas>" +
                 "                              <div class='hidden' id='executionwithdraw-copy-nopreview'>ไม่สามารถแสดงตัวอย่างเอกสาร</div>" +
                 "                              <div class='hidden' id='executionwithdraw-copy-linkpreview'>" +
                 "                                  <a class='text-underline' href='javascript:void(0)'>ดูเอกสาร</a>" +
                 "                                  <form id='download-executionwithdrawcopy-form' action='FileProcess.aspx' method='POST' target='download-executionwithdrawcopy'>" +
                 "                                      <input type='hidden' id='action' name='action' value='download' />" +
                 "                                      <input type='hidden' id='filename' name='filename' value='ExecutionWithdrawCopy' />" +
                 "                                      <input type='hidden' id='file' name='file' value='' />" +
                 "                                  </form>" +
                 "                                  <iframe class='export-target' id='download-executionwithdrawcopy' name='download-executionwithdrawcopy'></iframe>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style button-action clear-bottom'>" +
                 "              <div class='form-input-content'>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransProsecution('executionwithdraw')>บันทึก</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=GoToElement('executionwithdraw'); ResetFrmCPTransProsecutionWithDoc('executionwithdraw')>ล้าง</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "</div>";

        return _html;
    }
}