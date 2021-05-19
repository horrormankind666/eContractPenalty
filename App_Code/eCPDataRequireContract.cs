/*
Description         : สำหรับการรับรายการแจ้ง
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๑๐/๐๕/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Web;

public class eCPDataRequireContract
{
    private static string AddUpdateCPTransRequireContract(string _action, string[,] _data)
    {
        string _html = String.Empty;
        string _cp1id = _action.Equals("update") ? _data[0, 2] : _data[0, 1];
        string _studentIDDefault = _action.Equals("update") ? _data[0, 19] : _data[0, 2];
        string _titleNameDefault = _action.Equals("update") ? _data[0, 20] : _data[0, 5];
        string _firstNameDefault = _action.Equals("update") ? _data[0, 21] : _data[0, 8];
        string _lastNameDefault = _action.Equals("update") ? _data[0, 22] :_data[0, 9];
        string _facultyCodeDefault = _action.Equals("update") ? _data[0, 26] : _data[0, 13];
        string _facultyNameDefault = _action.Equals("update") ? _data[0, 27] : _data[0, 14];
        string _programCodeDefault = _action.Equals("update") ? _data[0, 23] : _data[0, 10];
        string _programNameDefault = _action.Equals("update") ? _data[0, 24] : _data[0, 11];
        string _groupNumDefault = _action.Equals("update") ? _data[0, 28] : _data[0, 15];
        string _dlevelDefault = _action.Equals("update") ? _data[0, 30] : _data[0, 17];
        string _pictureFileNameDefault = _action.Equals("update") ? _data[0, 56] : _data[0, 44];
        string _pictureFolderNameDefault = _action.Equals("update") ? _data[0, 57] : _data[0, 45];
        string _contractDateDefault = _action.Equals("update") ? _data[0, 38] : _data[0, 25];
        string _contractDateAgreementDefault = _action.Equals("update") ? _data[0, 61] : _data[0, 46];
        string _guarantorDefault = _action.Equals("update") ? _data[0, 39] : _data[0, 26];
        string _scholarDefault = _action.Equals("update") ? _data[0, 40] : _data[0, 27];
        string _scholarshipMoneyDefault = _action.Equals("update") ? (!_data[0, 41].Equals("0") ? double.Parse(_data[0, 41]).ToString("#,##0") : String.Empty) : (!_data[0, 28].Equals("0") ? double.Parse(_data[0, 28]).ToString("#,##0") : String.Empty);
        string _scholarshipYearDefault = _action.Equals("update") ? (!_data[0, 42].Equals("0") ? double.Parse(_data[0, 42]).ToString("#,##0") : String.Empty) : (!_data[0, 29].Equals("0") ? double.Parse(_data[0, 29]).ToString("#,##0") : String.Empty);
        string _scholarshipMonthDefault = _action.Equals("update") ? (!_data[0, 43].Equals("0") ? double.Parse(_data[0, 43]).ToString("#,##0") : String.Empty) : (!_data[0, 30].Equals("0") ? double.Parse(_data[0, 30]).ToString("#,##0") : String.Empty);
        string _educationDateStartDefault = _action.Equals("update") ? _data[0, 44] : _data[0, 31];
        string _educationDateEndDefault = _action.Equals("update") ? _data[0, 45] : _data[0, 32];
        string _caseGraduateBreakContractDefault = _action.Equals("update") ? _data[0, 46] : _data[0, 33];
        string _civilDefault = _action.Equals("update") ? _data[0, 47] : _data[0, 34];
        string _contractForceDateStartDefault = _action.Equals("update") ? _data[0, 62] : _data[0, 47];
        string _contractForceDateEndDefault = _action.Equals("update") ? _data[0, 63] : _data[0, 48];
        string _calDateCondition = _action.Equals("update") ? _data[0, 48] : _data[0, 35];
        string _setAmtIndemnitorYear = _action.Equals("update") ? _data[0, 77] : _data[0, 51];
        string _indemnitorYearDefault = _action.Equals("update") ? (!_data[0, 49].Equals("0") ? double.Parse(_data[0, 49]).ToString("#,##0") : String.Empty) : (!_data[0, 36].Equals("0") ? double.Parse(_data[0, 36]).ToString("#,##0") : String.Empty);
        string _indemnitorCashDefault = _action.Equals("update") ? double.Parse(_data[0, 50]).ToString("#,##0") : double.Parse(_data[0, 37]).ToString("#,##0");
        string _trackingStatus = _action.Equals("update") ? (_data[0, 52] + _data[0, 53] + _data[0, 54] + _data[0, 55]) : (_data[0, 40] + _data[0, 41] + _data[0, 42] + _data[0, 43]);
        string _cp2id = _action.Equals("update") ? _data[0, 1] : String.Empty;        
        string _indemnitorAddressDefault = _action.Equals("update") ? _data[0, 3] : String.Empty;
        string _provinceIDDefault = _action.Equals("update") ? _data[0, 4] : "0";
        string _studyLeaveDefault = _action.Equals("update") ? _data[0, 66] : String.Empty;
        string _requireDateDefault = _action.Equals("update") ? _data[0, 6] : String.Empty;
        string _approveDateDefault = _action.Equals("update") ? _data[0, 7] : String.Empty;
        string _beforeStudyLeaveStartDateDefault = _action.Equals("update") ? _data[0, 67] : String.Empty;
        string _beforeStudyLeaveEndDateDefault = _action.Equals("update") ? _data[0, 68] : String.Empty;
        string _studyLeaveStartDateDefault = _action.Equals("update") ? _data[0, 69] : String.Empty;
        string _studyLeaveEndDateDefault = _action.Equals("update") ? _data[0, 70] : String.Empty;
        string _afterStudyLeaveStartDateDefault = _action.Equals("update") ? _data[0, 71] : String.Empty;
        string _afterStudyLeaveEndDateDefault = _action.Equals("update") ? _data[0, 72] : String.Empty;
        string _actualMonthScholarshipDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 8]) ? double.Parse(_data[0, 8]).ToString("#,##0") : String.Empty) : String.Empty;
        string _actualScholarshipDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 9]) ? double.Parse(_data[0, 9]).ToString("#,##0.00") : String.Empty) : String.Empty;
        string _totalPayScholarshipDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 10]) ? double.Parse(_data[0, 10]).ToString("#,##0.00") : String.Empty) : String.Empty;
        string _actualMonthDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 11]) ? double.Parse(_data[0, 11]).ToString("#,##0") : String.Empty) : String.Empty;
        string _actualDayDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 12]) ? double.Parse(_data[0, 12]).ToString("#,##0") : String.Empty) : String.Empty;
        string _allActualDateDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 13]) ? double.Parse(_data[0, 13]).ToString("#,##0") : String.Empty) : String.Empty;
        string _actualDateDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 14]) ? double.Parse(_data[0, 14]).ToString("#,##0") : String.Empty) : String.Empty;
        string _remainDateDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 15]) ? double.Parse(_data[0, 15]).ToString("#,##0") : String.Empty) : String.Empty;
        string _subtotalPenaltyDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 16]) ? double.Parse(_data[0, 16]).ToString("#,##0.00") : String.Empty) : String.Empty;
        string _totalPenaltyDefault = _action.Equals("update") ? (!String.IsNullOrEmpty(_data[0, 17]) ? double.Parse(_data[0, 17]).ToString("#,##0.00") : String.Empty) : String.Empty;
        string _lawyerFullnameDefault = _action.Equals("update") ? _data[0, 73] : String.Empty;
        string _lawyerPhoneNumberDefault = _action.Equals("update") ? _data[0, 74] : String.Empty;
        string _lawyerMobileNumberDefault = _action.Equals("update") ? _data[0, 75] : String.Empty;
        string _lawyerEmailDefault = _action.Equals("update") ? _data[0, 76] : String.Empty;
        string _statusRepay = _action.Equals("update") ? _data[0, 18] : String.Empty;

        if (_action.Equals("add"))
        {
            string _userid = eCPUtil.GetUserID();
            string[,] _data1 = eCPDB.ListDetailCPTabUser(_userid, "", "", "");

            _lawyerFullnameDefault = _data1[0, 3];
            _lawyerPhoneNumberDefault = _data1[0, 6];
            _lawyerMobileNumberDefault = _data1[0, 7];
            _lawyerEmailDefault = _data1[0, 8];
        }


        _html += "<div class='form-content' id='" + _action + "-cp-trans-require-contract'>" +
                 "  <div id='addupdate-cp-trans-require-contract'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +
                 "      <input type='hidden' id='scholar-hidden' value='" + _scholarDefault + "' />" +
                 "      <input type='hidden' id='education-date-start-hidden' value='" + _educationDateStartDefault + "' />" +
                 "      <input type='hidden' id='education-date-end-hidden' value='" + _educationDateEndDefault + "' />" +
                 "      <input type='hidden' id='case-graduate-break-contract-hidden' value='" + _caseGraduateBreakContractDefault + "' />" +
                 "      <input type='hidden' id='civil-hidden' value='" + _civilDefault + "' />" +
                 "      <input type='hidden' id='contract-force-date-start-hidden' value='" + _contractForceDateStartDefault + "' />" +
                 "      <input type='hidden' id='contract-force-date-end-hidden' value='" + _contractForceDateEndDefault + "' />" +
                 "      <input type='hidden' id='set-amt-indemnitor-year' value='" + _setAmtIndemnitorYear + "' />" +
                 "      <input type='hidden' id='indemnitor-year-hidden' value='" + _indemnitorYearDefault + "' />" +
                 "      <input type='hidden' id='indemnitor-cash-hidden' value='" + _indemnitorCashDefault + "' />" +
                 "      <input type='hidden' id='cal-date-condition-hidden' value='" + _calDateCondition + "' />" +
                 "      <input type='hidden' id='trackingstatus' value='" + _trackingStatus + "' />" +
                 "      <input type='hidden' id='cp2id' value='" + _cp2id + "' />" +
                 "      <input type='hidden' id='study-leave-hidden' value='" + _studyLeaveDefault + "' />" +
                 "      <input type='hidden' id='indemnitor-address-hidden' value='" + _indemnitorAddressDefault + "' />" +
                 "      <input type='hidden' id='province-id-hidden' value='" + _provinceIDDefault + "' />" +
                 "      <input type='hidden' id='require-date-hidden' value='" + _requireDateDefault + "' />" +
                 "      <input type='hidden' id='approve-date-hidden' value='" + _approveDateDefault + "' />" +
                 "      <input type='hidden' id='before-study-leave-start-date-hidden' value='" + _beforeStudyLeaveStartDateDefault + "' />" +
                 "      <input type='hidden' id='before-study-leave-end-date-hidden' value='" + _beforeStudyLeaveEndDateDefault + "' />" +
                 "      <input type='hidden' id='study-leave-start-date-hidden' value='" + _studyLeaveStartDateDefault + "' />" +
                 "      <input type='hidden' id='study-leave-end-date-hidden' value='" + _studyLeaveEndDateDefault + "' />" +
                 "      <input type='hidden' id='after-study-leave-start-date-hidden' value='" + _afterStudyLeaveStartDateDefault + "' />" +
                 "      <input type='hidden' id='after-study-leave-end-date-hidden' value='" + _afterStudyLeaveEndDateDefault + "' />" +
                 "      <input type='hidden' id='all-actual-month-scholarship-hidden' value='" + _actualMonthScholarshipDefault + "' />" +
                 "      <input type='hidden' id='all-actual-scholarship-hidden' value='" + _actualScholarshipDefault + "' />" +
                 "      <input type='hidden' id='total-pay-scholarship-hidden' value='" + _totalPayScholarshipDefault + "' />" +
                 "      <input type='hidden' id='actual-month-hidden' value='" + _actualMonthDefault + "' />" +
                 "      <input type='hidden' id='actual-day-hidden' value='" + _actualDayDefault + "' />" +
                 "      <input type='hidden' id='all-actual-date-hidden' value='" + _allActualDateDefault + "' />" +
                 "      <input type='hidden' id='actual-date-hidden' value='" + _actualDateDefault + "' />" +
                 "      <input type='hidden' id='remain-date-hidden' value='" + _remainDateDefault + "' />" +
                 "      <input type='hidden' id='subtotal-penalty-hidden' value='" + _subtotalPenaltyDefault + "' />" +
                 "      <input type='hidden' id='total-penalty-hidden' value='" + _totalPenaltyDefault + "' />" +
                 "      <input type='hidden' id='lawyer-fullname-hidden' value='" + _lawyerFullnameDefault + "' />" +
                 "      <input type='hidden' id='lawyer-phonenumber-hidden' value='" + _lawyerPhoneNumberDefault + "' />" +
                 "      <input type='hidden' id='lawyer-mobilenumber-hidden' value='" + _lawyerMobileNumberDefault + "' />" +
                 "      <input type='hidden' id='lawyer-email-hidden' value='" + _lawyerEmailDefault + "' />" +
                 "      <input type='hidden' id='repaystatus' value='" + _statusRepay + "' />" +
                 "      <div>" +
                 "          <div id='profile-student'>" +
                 "              <div class='content-left' id='picture-student'><div><img src='Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "' /></div></div>" +
                 "              <div class='content-left' id='profile-student-label'>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                 "                  <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>หลักสูตร</div></div>" +
                 "              </div>" +
                 "              <div class='content-left' id='profile-student-input'>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span>" + _studentIDDefault + "&nbsp;" + _programCodeDefault.Substring(0, 4) + " / " + _programCodeDefault.Substring(4, 1) + "</span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span>" + _titleNameDefault + _firstNameDefault + " " + _lastNameDefault + "</span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span>" + _dlevelDefault + "</span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span>" + _facultyCodeDefault + " - " + _facultyNameDefault + "</span></div></div>" +
                 "                  <div class='form-label-discription-style clear-bottom'><div class='form-label-style'><span>" + _programCodeDefault + " - " + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : "") + "</span></div></div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='box3'></div>" +
                 "          <div id='break-contract'>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style clear-bottom'>" +
                 "                      <div id='break-contract-label'>" +
                 "                          <div class='form-label-style'>รายละเอียดการผิดสัญญาการศึกษา</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>รายละเอียดการผิดสัญญาการศึกษาของนักศึกษา และเงื่อนไข</div>" +
                 "                              <div class='form-discription-line2-style'>การชดใช้ตามสัญญา</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style clear-bottom'>" +
                 "                      <div class='form-input-content' id='break-contract-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='contract-date-label'>สัญญานักศึกษาลงวันที่</div>" +
                 "                              <div class='content-left' id='contract-date-input'><input class='inputbox' type='text' id='contract-date' value='" + Util.LongDateTH(_contractDateDefault) + "' style='width:120px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='contract-date-agreement-label'>สัญญาค้ำประกันลงวันที่</div>" +
                 "                              <div class='content-left' id='contract-date-agreement-input'><input class='inputbox' type='text' id='contract-date-agreement' value='" + Util.LongDateTH(_contractDateAgreementDefault) + "' style='width:120px' /></div>" +
                 "                              <div class='content-left' id='guarantor-label'>ผู้ค้ำประกัน</div>" +
                 "                              <div class='content-left' id='guarantor-input'><input class='inputbox' type='text' id='guarantor' value='" + _guarantorDefault + "' style='width:278px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='scholar-label'>ได้รับ / ไม่ได้รับทุนการศึกษา</div>" +
                 "                              <div class='content-left' id='scholar-input'><input class='inputbox' type='text' id='scholar' value='" + eCPUtil._scholar[int.Parse(_scholarDefault) - 1] + "' style='width:120px' /></div>" +
                 "                              <div class='content-left' id='scholarship-money-label'>จำนวนเงิน</div>" +
                 "                              <div class='content-left' id='scholarship-money-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-money' value='" + _scholarshipMoneyDefault + "' style='width:60px' /></div>" +
                 "                              <div class='content-left' id='scholarship-money-unit-label'>บาท / หลักสูตร</div>" +
                 "                              <div class='content-left' id='scholarship-year-label'>ระยะเวลา</div>" +
                 "                              <div class='content-left' id='scholarship-year-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-year' value='" + _scholarshipYearDefault + "' style='width:20px' /></div>" +
                 "                              <div class='content-left' id='scholarship-year-unit-label'>ปี</div>" +
                 "                              <div class='content-left' id='scholarship-month-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-month' value='" + _scholarshipMonthDefault + "' style='width:20px' /></div>" +
                 "                              <div class='content-left' id='scholarship-month-unit-label'>เดือน</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='case-graduate-label'>สำเร็จ / ไม่สำเร็จการศึกษา</div>" +
                 "                              <div class='content-left' id='case-graduate-input'><input class='inputbox' type='text' id='case-graduate' value='" + eCPUtil._caseGraduate[int.Parse(_caseGraduateBreakContractDefault) - 1, 1] + "' style='width:120px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='education-date-start-label'>เริ่มต้นเข้าศึกษาเมื่อวันที่</div>" +
                 "                              <div class='content-left' id='education-date-start-input'><input class='inputbox' type='text' id='education-date-start' value='" + Util.LongDateTH(_educationDateStartDefault) + "' style='width:120px' /></div>" +
                 "                              <div class='content-left' id='education-date-end-label'>จนถึงวันที่</div>" +
                 "                              <div class='content-left' id='education-date-end-input'><input class='inputbox' type='text' id='education-date-end' value='" + Util.LongDateTH(_educationDateEndDefault) + "' style='width:117px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='civil-label'>ปฏิบัติ / ไม่ปฏิบัติงานชดใช้</div>" +
                 "                              <div class='content-left' id='civil-input'><input class='inputbox' type='text' id='civil' value='" + (!_civilDefault.Equals("0") ? eCPUtil._civil[int.Parse(_civilDefault) - 1] : "") + "' style='width:120px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='contract-force-date-start-label'>สัญญาเริ่มมีผลบังคับใช้เมื่อวันที่</div>" +
                 "                              <div class='content-left' id='contract-force-date-start-input'><input class='inputbox' type='text' id='contract-force-date-start' value='" + Util.LongDateTH(_contractForceDateStartDefault) + "' style='width:120px' /></div>" +
                 "                              <div class='content-left' id='contract-force-date-end-label'>จนถึงวันที่</div>" +
                 "                              <div class='content-left' id='contract-force-date-end-input'><input class='inputbox' type='text' id='contract-force-date-end' value='" + Util.LongDateTH(_contractForceDateEndDefault) + "' style='width:117px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='indemnitor-year-label'>ระยะเวลาที่ต้องปฏิบัติงานชดใช้</div>" +
                 "                              <div class='content-left' id='indemnitor-year-input'>";

        if (_setAmtIndemnitorYear.Equals("Y"))
            _html += "                              <input class='inputbox textbox-numeric' type='text' id='indemnitor-year' onblur=Trim('indemnitor-year');AddCommas('indemnitor-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' />";
        else
            _html += "                              <input class='inputbox textbox-numeric' type='text' id='indemnitor-year' value='' style='width:120px' />";

        _html += "                              </div>" +
                 "                              <div class='content-left' id='indemnitor-year-unit-label'>ปี</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='indemnitor-cash-label'>จำนวนเงินต้องชดใช้ตามสัญญา</div>" +
                 "                              <div class='content-left' id='indemnitor-cash-input'><input class='inputbox textbox-numeric' type='text' id='indemnitor-cash' onblur=Trim('indemnitor-cash');AddCommas('indemnitor-cash',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' /></div>" +
                 "                              <div class='content-left' id='indemnitor-cash-unit-label'>บาท</div>" +
                 "                          </div>" +       
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>";

        if (_caseGraduateBreakContractDefault.Equals("1"))
        {
            _html += "      <div class='box3'></div>" +
                     "      <div id='cal-contract-penalty1'>" +
                     "          <div>" +
                     "              <div class='content-left cal-contract-penalty-label' id='cal-contract-penalty-label'>" +
                     "                  <div class='form-label-discription-style clear-bottom'>" +
                     "                      <div class='form-label-style'>คำนวณเงินชดใช้</div>" +
                     "                      <div class='form-discription-style'>" +
                     "                          <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณเงินทุนการศึกษาที่</div>" +
                     "                          <div class='form-discription-line2-style'>ต้องชดใช้กรณีนักศึกษารับทุนการศึกษา และคำนวณเงิน</div>" +
                     "                          <div class='form-discription-line3-style'>ที่ต้องชดใช้ตามระยะเวลาที่เข้าศึกษา</div>" +
                     "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "              <div class='content-left cal-contract-penalty-input' id='cal-contract-penalty-input'>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-button'>" +
                     "                      <div class='button-style2'>" +
                     "                          <ul>" +
                     "                              <li><a href='javascript:void(0)' onclick='CalculatePayScholarshipAndPenalty()'>คำนวณ</a></li>" +
                     "                          </ul>" +
                     "                      </div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-scholarship'>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='all-actual-month-scholarship-label'>ระยะเวลาที่ชดใช้ทุนการศึกษา</div>" +
                     "                          <div class='content-left' id='all-actual-month-scholarship-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-month-scholarship' onblur=Trim('all-actual-month-scholarship');AddCommas('all-actual-month-scholarship',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='all-actual-month-scholarship-unit-label'>เดือน</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='all-actual-scholarship-label'>จำนวนเงินทุนการศึกษาที่ชดใช้</div>" +
                     "                          <div class='content-left' id='all-actual-scholarship-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-scholarship' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='all-actual-scholarship-unit-label'>บาท / เดือน</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-label'>ยอดเงินทุนการศึกษาที่ชดใช้</div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-input'><input class='inputbox textbox-numeric' type='text' id='total-pay-scholarship' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-actual'>" +
                     "                      <div id='view-cal-date-button'>" +
                     "                          <div class='button-style2'>" +
                     "                              <ul>" +
                     "                                  <li><a href='javascript:void(0)' onclick=ViewCalDate('" + _calDateCondition + "')>ดูสูตรคำนวณ</a></li>" +
                     "                              </ul>" +
                     "                          </div>" +
                     "                      </div>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='all-actual-date-label'>ระยะเวลาที่ใช้ในการศึกษา</div>" +
                     "                          <div class='content-left' id='all-actual-month-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-month' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='all-actual-month-unit-label'>เดือน</div>" +
                     "                          <div class='content-left' id='all-actual-day-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-day' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='all-actual-day-unit-label'>วัน</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='subtotal-penalty-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
                     "                          <div class='content-left' id='subtotal-penalty-input'><input class='inputbox textbox-numeric' type='text' id='subtotal-penalty' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='subtotal-penalty-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style clear-bottom' id='cal-contract-penalty-total'>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='total-penalty-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
                     "                          <div class='content-left' id='total-penalty-input'><input class='inputbox textbox-numeric' type='text' id='total-penalty' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='total-penalty-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>";
        }
    
        if (_caseGraduateBreakContractDefault.Equals("2"))
        {
            if (_civilDefault.Equals("1"))
            {
                _html += "  <div class='box3'></div>" +
                         "  <div id='indemnitor-work'>" +
                         "      <div>" +
                         "          <div class='form-label-discription-style clear-bottom'>" +
                         "              <div id='indemnitor-work-label'>" +
                         "                  <div class='form-label-style'>รายละเอียดข้อมูลการทำงานชดใช้</div>" +
                         "                  <div class='form-discription-style'>" +
                         "                      <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดข้อมูลการทำงานชดใช้</div>" +
                         "                  </div>" +
                         "              </div>" +
                         "          </div>" +
                         "          <div class='form-input-style clear-bottom'>" +
                         "              <div class='form-input-content' id='indemnitor-work-input'>" +
                         "                  <div>" +
                         "                      <div class='content-left' id='indemnitor-address-label'>ตรวจสอบแล้วได้ทำงานชดใช้ที่</div>" +
                         "                      <div class='content-left' id='indemnitor-address-input'><input class='inputbox' type='text' id='indemnitor-address' onblur=Trim('indemnitor-address'); value='' style='width:472px' /></div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>" +
                         "                  <div>" +
                         "                      <div class='content-left' id='province-label'>สถานที่ทำงานชดใช้อยู่จังหวัด</div>" +
                         "                      <div class='content-left' id='province-input'>" +
                                                    eCPUtil.ListProvince("province") +
                         "                      </div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>" +
                         "                  <div id='study-leave-yesno'>" +
                         "                      <div class='content-left' id='study-leave-yesno-label'>ช่วงวันที่ทำงานชดใช้</div>" +
                         "                      <div class='content-left' id='study-leave-yesno-input'>" +
                         "                          <div>" +
                         "                              <div class='content-left' id='study-leave-status-no-input'><input class='radio' type='radio' name='study-leave-yesno' value='N' /></div>" +
                         "                              <div class='content-left' id='study-leave-status-no-label'>" + eCPUtil._studyLeave[0] + "</div>" +
                         "                          </div>" +
                         "                          <div class='clear'></div>" +
                         "                          <div id='study-leave-status-no'>" +
                         "                              <div class='content-left' id='require-date-label'>ตั้งแต่วันที่</div>" +
                         "                              <div class='content-left' id='require-date-input'><input class='inputbox calendar' type='text' id='require-date' readonly value='' /></div>" +
                         "                              <div class='content-left' id='approve-date-label'>ถึงวันที่</div>" +
                         "                              <div class='content-left' id='approve-date-input'><input class='inputbox calendar' type='text' id='approve-date' readonly value='' /></div>" +
                         "                          </div>" +
                         "                          <div class='clear'></div>" +
                         "                          <div>" +
                         "                              <div class='content-left' id='study-leave-status-yes-input'><input class='radio' type='radio' name='study-leave-yesno' value='Y' /></div>" +
                         "                              <div class='content-left' id='study-leave-status-yes-label'>" + eCPUtil._studyLeave[1] + "</div>" +
                         "                          </div>" +
                         "                          <div class='clear'></div>" +
                         "                          <div id='study-leave-status-yes'>" +
                         "                              <div id='before-study-leave'>" +
                         "                                  <div><strong>(1)</strong> การปฏิบัติงานก่อนการลาศึกษา / ลาฝึกอบรม</div>" +
                         "                                  <div id='before-study-leave-input'>" +
                         "                                      <div class='content-left' id='before-study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                         "                                      <div class='content-left' id='before-study-leave-start-date-input'><input class='inputbox calendar' type='text' id='before-study-leave-start-date' readonly value='' /></div>" +
                         "                                      <div class='content-left' id='before-study-leave-end-date-label'>ถึงวันที่</div>" +
                         "                                      <div class='content-left' id='before-study-leave-end-date-input'><input class='inputbox calendar' type='text' id='before-study-leave-end-date' readonly value='' /></div>" +
                         "                                  </div>" +
                         "                                  <div class='clear'></div>" +
                         "                              </div>" +
                         "                              <div id='study-leave'>" +
                         "                                  <div><strong>(2)</strong> การลาศึกษา / ลาฝึกอบรม ( ไม่นับเป็นระยะเวลาการปฏิบัติงานชดใช้ทุน )</div>" +
                         "                                  <div id='study-leave-input'>" +
                         "                                      <div class='content-left' id='study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                         "                                      <div class='content-left' id='study-leave-start-date-input'><input class='inputbox calendar' type='text' id='study-leave-start-date' readonly value='' /></div>" +
                         "                                      <div class='content-left' id='study-leave-end-date-label'>ถึงวันที่</div>" +
                         "                                      <div class='content-left' id='study-leave-end-date-input'><input class='inputbox calendar' type='text' id='study-leave-end-date' readonly value='' /></div>" +
                         "                                  </div>" +
                         "                                  <div class='clear'></div>" +
                         "                              </div>" +
                         "                              <div id='after-study-leave'>" +
                         "                                  <div><strong>(3)</strong> การกลับเข้าปฏิบัติงานภายหลังจากการลาศึกษา / ลาฝึกอบรม</div>" +
                         "                                  <div id='after-study-leave-input'>" +
                         "                                      <div class='content-left' id='after-study-leave-start-date-label'>ตั้งแต่วันที่</div>" +
                         "                                      <div class='content-left' id='after-study-leave-start-date-input'><input class='inputbox calendar' type='text' id='after-study-leave-start-date' readonly value='' /></div>" +
                         "                                      <div class='content-left' id='after-study-leave-end-date-label'>ถึงวันที่</div>" +
                         "                                      <div class='content-left' id='after-study-leave-end-date-input'><input class='inputbox calendar' type='text' id='after-study-leave-end-date' readonly value='' /></div>" +
                         "                                  </div>" +
                         "                                  <div class='clear'></div>" +
                         "                              </div>" +
                         "                          </div>"  +
                         "                      </div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>" +
                         "              </div>" +
                         "          </div>" +
                         "      </div>" +
                         "      <div class='clear'></div>" +
                         "  </div>";
            }

            _html += "      <div class='box3'></div>" +
                     "      <div id='cal-contract-penalty2'>" +
                     "          <div>" +
                     "              <div class='content-left cal-contract-penalty-label' id='cal-contract-penalty-label-civil-" + _civilDefault + "-set-" + _setAmtIndemnitorYear.ToLower() + "'>" +
                     "                  <div class='form-label-discription-style clear-bottom'>" +
                     "                      <div class='form-label-style'>คำนวณเงินชดใช้</div>" +
                     "                      <div class='form-discription-style'>" +
                     "                          <div class='form-discription-line1-style'>กรุณากดปุ่มคำนวณเพื่อทำการคำนวณเงินทุนการศึกษาที่ต้อง</div>" +
                     "                          <div class='form-discription-line2-style'>ชดใช้กรณีนักศึกษารับทุนการศึกษา และคำนวณเงินที่ต้อง</div>" +
                     "                          <div class='form-discription-line3-style'>ชดใช้แทนการปฏิบัติงานส่วนที่ขาด</div>" +
                     "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "              <div class='content-left clear-bottom cal-contract-penalty-input' id='cal-contract-penalty-input-civil-" + _civilDefault + "-set-" + _setAmtIndemnitorYear.ToLower() + "'>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-button'>" +
                     "                      <div class='button-style2'>" +
                     "                          <ul>" +
                     "                              <li><a href='javascript:void(0)' onclick='CalculatePayScholarshipAndPenalty()'>คำนวณ</a></li>" +
                     "                          </ul>" +
                     "                      </div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-scholarship'>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-label'>ยอดเงินทุนการศึกษาที่ชดใช้</div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-input'><input class='inputbox textbox-numeric' type='text' id='total-pay-scholarship' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='total-pay-scholarship-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style' id='cal-contract-penalty-actual'>" +
                     "                      <div id='view-cal-date-button'>" +
                     "                          <div class='button-style2'>" +
                     "                              <ul>" +
                     "                                  <li><a href='javascript:void(0)' onclick=ViewCalDate('" + _calDateCondition + "')>ดูสูตรคำนวณ</a></li>" +
                     "                              </ul>" +
                     "                          </div>" +
                     "                      </div>";

            if (_setAmtIndemnitorYear.Equals("Y"))
            {
                _html += "                  <div>" +
                         "                      <div class='content-left' id='all-actual-date-label'>ระยะเวลาที่ต้องปฏิบัติงานชดใช้</div>" +
                         "                      <div class='content-left' id='all-actual-date-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-date' value='' style='width:120px' /></div>" +
                         "                      <div class='content-left' id='all-actual-date-unit-label'>วัน</div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>" +
                         "                  <div>" +
                         "                      <div class='content-left' id='actual-date-label'>ระยะเวลาที่ปฏิบัติงานชดใช้แล้ว</div>" +
                         "                      <div class='content-left' id='actual-date-input'><input class='inputbox textbox-numeric' type='text' id='actual-date' value='' style='width:120px' /></div>" +
                         "                      <div class='content-left' id='actual-date-unit-label'>วัน</div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>" +
                         "                  <div>" +
                         "                      <div class='content-left' id='remain-date-label'>ระยะเวลาปฏิบัติงานชดใช้ที่ขาด</div>" +
                         "                      <div class='content-left' id='remain-date-input'><input class='inputbox textbox-numeric' type='text' id='remain-date' value='' style='width:120px' /></div>" +
                         "                      <div class='content-left' id='remain-date-unit-label'>วัน</div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>";
            }
      
            if (_setAmtIndemnitorYear.Equals("N"))
            {
                _html += "                  <div>" +
                         "                      <div class='content-left' id='all-actual-day-label'>ระยะเวลาที่ใช้ในการศึกษา</div>" +
                         "                      <div class='content-left' id='all-actual-day-input'><input class='inputbox textbox-numeric' type='text' id='all-actual-day' value='' style='width:120px' /></div>" +
                         "                      <div class='content-left' id='all-actual-day-unit-label'>วัน</div>" +
                         "                  </div>" +
                         "                  <div class='clear'></div>";

                if (_civilDefault.Equals("1"))
                {
                    _html += "              <div>" +
                             "                  <div class='content-left' id='actual-date-label'>ระยะเวลาที่ปฏิบัติงานชดใช้</div>" +
                             "                  <div class='content-left' id='actual-date-input'><input class='inputbox textbox-numeric' type='text' id='actual-date' value='' style='width:120px' /></div>" +
                             "                  <div class='content-left' id='actual-date-unit-label'>วัน</div>" +
                             "              </div>" +
                             "              <div class='clear'></div>";
                }
            }

            _html += "                      <div>" +
                     "                          <div class='content-left' id='subtotal-penalty-label'>ยอดเงินค่าปรับผิดสัญญา</div>" +
                     "                          <div class='content-left' id='subtotal-penalty-input'><input class='inputbox textbox-numeric' type='text' id='subtotal-penalty' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='subtotal-penalty-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "                  <div class='form-label-discription-style clear-bottom' id='cal-contract-penalty-total'>" +
                     "                      <div>" +
                     "                          <div class='content-left' id='total-penalty-label'>ยอดเงินที่ต้องรับผิดชอบชดใช้</div>" +
                     "                          <div class='content-left' id='total-penalty-input'><input class='inputbox textbox-numeric' type='text' id='total-penalty' value='' style='width:120px' /></div>" +
                     "                          <div class='content-left' id='total-penalty-unit-label'>บาท</div>" +
                     "                      </div>" +
                     "                      <div class='clear'></div>" +
                     "                  </div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>";
        }

        _html += "          <div class='box3'></div>" +
                 "          <div id='lawyer'>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='lawyer-label'>" +
                 "                          <div class='form-label-style'>นิติกรผู้รับผิดชอบ</div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='lawyer-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='fullname-label'>ชื่อ - นามสกุล</div>" +
                 "                              <div class='content-left' id='fullname-input'>" +
                 "                                  <input class='inputbox' type='text' id='lawyer-fullname' value='" + _lawyerFullnameDefault + "' style='width:254px' />" +
                 "                                  <div class='form-discription-style'>" +
                 "                                      <div class='form-discription-line1-style'>ไม่ต้องระบุคำนำหน้าชื่อ</div>" +
                 "                                  </div>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='phonenumber-label'>หมายเลขโทรศัพท์</div>" +
                 "                              <div class='content-left' id='phonenumber-input'><input class='inputbox' type='text' id='lawyer-phonenumber' value='" + _lawyerPhoneNumberDefault + "' style='width:120px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='mobilenumber-label'>หมายเลขโทรศัพท์มือถือ</div>" +
                 "                              <div class='content-left' id='mobilenumber-input'><input class='inputbox' type='text' id='lawyer-mobilenumber' value='" + _lawyerMobileNumberDefault + "' style='width:120px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='email-label'>อีเมล์</div>" +
                 "                              <div class='content-left' id='email-input'><input class='inputbox' type='text' id='lawyer-email' value='" + _lawyerEmailDefault + "' style='width:254px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransRequireContract('" + _action + "')>บันทึก</a></li>";
    
        if (_action.Equals("update"))
            _html += "          <li><a href='javascript:void(0)' onclick=LoadForm(1,'addcommentcancelrequirecontract',true,'','" + _cp1id + "','')>ยกเลิกรายการ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTransRequireContract(false)'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-require-contract')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li id='button-status-p'><a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + _cp1id + "','v2')>พิมพ์แบบตรวจสอบ</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-require-contract')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }

    public static string DetailCPTransRequireContract(string _cp1id, string _status)
    {
        string _html = String.Empty;
        string _trackingStatus = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransRequireContract(_cp1id);

        if (_data.GetLength(0) > 0)
        {
            _html += eCPDataBreakContract.DetailCPTransBreakRequireContract(_cp1id, _data, _status);
        }

        return _html;
    }
    
    public static string AddCPTransRequireContract(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransBreakContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTransRequireContract("add", _data);

        return _html;
    }

    public static string UpdateCPTransRequireContract(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransRequireContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTransRequireContract("update", _data);

        return _html;
    }

    public static string ListSearchRepayStatusCPTransRequireContract(string _cp1id)
    {
        string _repayStatus = String.Empty;

        _repayStatus = eCPDB.ChkRepayStatusCPTransRequireContract(_cp1id);

        return "<repaystatus>" + _repayStatus + "<repaystatus>";
    }

    public static string TabCPTransRequireContract()
    {
        string _html = String.Empty;
        int _section;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        Array _trackingStatus = (_section.Equals(1) ? eCPUtil._trackingStatusORLA : eCPUtil._trackingStatusORAA);

        _html += "<div id='cp-trans-require-contract-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-trans-require-contract") +
                 "      <div class='content-data-tabs' id='tabs-cp-trans-require-contract'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-trans-require-contract' alt='#tab1-cp-trans-require-contract' href='javascript:void(0)'>ตรวจสอบรายการแจ้ง</a></li>" +
                 "                  <li><a id='link-tab2-cp-trans-require-contract' alt='#tab2-cp-trans-require-contract' href='javascript:void(0)'>แจ้งชำระหนี้</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-trans-require-contract' alt='#tab3-cp-trans-require-contract' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-trans-require-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-trans-break-contract' value=''>" +
                 "                  <input type='hidden' id='trackingstatus-trans-break-contract-hidden' value='2'>" +
                 "                  <input type='hidden' id='trackingstatus-trans-break-contract-text-hidden' value='" + _trackingStatus.GetValue(0, 0) + "'>" +
                 "                  <input type='hidden' id='id-name-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='program-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-start-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-end-trans-break-contract-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcptransbreakcontract',true,'','','')>ค้นหา</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-trans-break-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-trans-break-contract-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>สถานะรายการแจ้ง</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order2'>" +
                 "                  <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order2-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order3'>" +
                 "                  <div class='box-search-condition-order-title'>คณะ</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order3-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order4'>" +
                 "                  <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order4-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-break-contract-condition-order' id='search-trans-break-contract-condition-order5'>" +
                 "                  <div class='box-search-condition-order-title'>ช่วงวันที่" + (_section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้ง</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-break-contract-condition-order5-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-trans-require-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-trans-repay-contract' value=''>" +
                 "                  <input type='hidden' id='repaystatus-trans-repay-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='repaystatus-trans-repay-contract-text-hidden' value=''>" +
                 "                  <input type='hidden' id='id-name-trans-repay-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-trans-repay-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='program-trans-repay-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-start-trans-repay-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-end-trans-repay-contract-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcptransrepaycontract',true,'','','')>ค้นหา</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-trans-repay-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-trans-repay-contract-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>สถานะการแจ้งชำระหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order2'>" +
                 "                  <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order2-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order3'>" +
                 "                  <div class='box-search-condition-order-title'>คณะ</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order3-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order4'>" +
                 "                  <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order4-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-trans-repay-contract-condition-order' id='search-trans-repay-contract-condition-order5'>" +
                 "                  <div class='box-search-condition-order-title'>ช่วงวันที่รับรายการแจ้ง</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-trans-repay-contract-condition-order5-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-trans-require-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-trans-require-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='tab1-table-head-cp-trans-require-contract-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col5'><div class='table-head-line1'>ส่งรายการแจ้ง</div><div>เมื่อ</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-trans-require-contract-col6'><div class='table-head-line1'>สถานะรายการแจ้ง</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailtrackingstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-require-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='tab2-table-head-repay-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col5'><div class='table-head-line1'>รับรายการแจ้ง</div><div>เมื่อ</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col6'><div class='table-head-line1'>ระยะเวลา</div><div>ผิดนัดชำระ</div><div>( วัน )</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-repay-col7'><div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div><div>&nbsp;</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-trans-require-contract-contents'></div>" +
                 "</div>" +
                 "<div id='cp-trans-require-contract-content'>" +
                 "  <div class='tab-content' id='tab1-cp-trans-require-contract-content'>" +
                 "      <div class='box4' id='list-data-trans-break-contract'></div>" +
                 "      <div id='nav-page-trans-break-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-require-contract-content'>" +
                 "      <div class='box4' id='list-data-trans-repay-contract'></div>" +
                 "      <div id='nav-page-trans-repay-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-trans-require-contract-content'>" +
                 "    <div class='box1' id='addupdate-data-trans-require-contract'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}