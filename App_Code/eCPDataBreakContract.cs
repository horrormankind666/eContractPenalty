/*
Description         : สำหรับการทำรายการแจ้ง
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๑๒/๑๑/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Collections;
using System.Web;

public class eCPDataBreakContract {
    public static string AddCommentBreakContract(
        string _cp1id,
        string _action,
        string _from
    ) {
        string _html = String.Empty;

        _html += "<div class='form-content' id='add-comment-break-contract'>" +
                 "  <div class='form-input-style'>" +
                 "      <div class='form-input-content' id='comment-reject-input'>" +
                 "          <textarea class='textareabox' id='comment-reject' onblur=Trim('comment-reject') style='width:452px;height:90px;'></textarea>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmAddCommentBreakContract('" + _cp1id + "','" + _action + "','" + _from + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCommentBreakContract()'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddProfileStudent() {
        string _html = String.Empty;

        _html += "<div class='form-content' id='add-profile-students'>" +
                 "  <div id='add-profile-student'>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='profile-student-id-label'>" +
                 "                  <div class='form-label-style'>รหัสนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสนักศึกษาที่ต้องการ หรือกดปุ่ม</div>" +
                 "                      <div class='form-discription-line2-style'>ค้นหานักศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='profile-student-id-input'>" +
                 "                  <div><input class='inputbox' type='text' id='profile-student-id' onblur=Trim('profile-student-id') onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' maxlength='7' value='' style='width:84px' /></div>" +
                 "                  <div id='profile-student-search'>" +
                 "                      <div class='button-style2'>" +
                 "                          <ul>" +
                 "                              <li><a href='javascript:void(0)' onclick=LoadForm(2,'searchstudentwithresult',true,'','','')>ค้นหานักศึกษา</a></li>" +
                 "                          </ul>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='profile-student-fullname-label'>" +
                 "                  <div class='form-label-style'>ชื่อ - นามสกุล</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคำนำหน้าชื่อ และใส่ชื่อกับนามสกุล</div>" +
                 "                      <div class='form-discription-line2-style'>ของนักศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='profile-student-fullname-input'>" +
                 "                  <div>" +
                 "                      <div class='content-left' id='profile-student-titlename-input'>" + eCPUtil.ListTitleName("titlename") + "</div>" +
                 "                      <div class='content-left' id='profile-student-firstname-label'>ชื่อ</div>" +
                 "                      <div class='content-left' id='profile-student-firstname-input'><input class='inputbox' type='text' id='profile-student-firstname' onblur=Trim('profile-student-firstname'); value='' style='width:163px' /></div>" +
                 "                      <div class='content-left' id='profile-student-lastname-label'>นามสกุล</div>" +
                 "                      <div class='content-left' id='profile-student-lastname-input'><input class='inputbox' type='text' id='profile-student-lastname' onblur=Trim('profile-student-lastname'); value='' style='width:154px' /></div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='profile-student-faculty-program-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรของนักศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='profile-student-faculty-program-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyprofilestudent") +
                 "                  <div id='list-program-profile-student'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ViewStudentInTransBreakContract()'>ตกลง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmAddProfileStudent()'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    private static string AddUpdateCPTransBreakContract(
        string _action,
        string[,] _data
    ) {
        string _html = String.Empty;
        int _i;       
        string _cp1id = _action.Equals("update") ? _data[0, 1] : String.Empty;
        string _studentFullIDDefault = _action.Equals("update") ? _data[0, 2] + " " + _data[0, 10].Substring(0, 4) + " / " + _data[0, 10].Substring(4, 1) : String.Empty;
        string _studentFullnameThaDefault = _action.Equals("update") ? _data[0, 5] + _data[0, 8] + " " + _data[0, 9] : String.Empty;
        string _studentFullnameEngDefault = _action.Equals("update") ? ((!String.IsNullOrEmpty(_data[0, 6]) && !String.IsNullOrEmpty(_data[0, 7])) ? _data[0, 4] + _data[0, 6] + " " + _data[0, 7] : "-") : String.Empty;
        string _studentDLevelDefault = _action.Equals("update") ? _data[0, 17] : String.Empty;
        string _studentFacultyDefault = _action.Equals("update") ? _data[0, 13] + " - " + _data[0, 14] : String.Empty;
        string _studentProgramDefault = _action.Equals("update") ? _data[0, 10] + " - " + _data[0, 11] + (!_data[0, 15].Equals("0") ? " ( กลุ่ม " + _data[0, 15] + " )" : "") : String.Empty;
        string _studentIDDefault = _action.Equals("update") ? _data[0, 2] : String.Empty;
        string _titleNameDefault = _action.Equals("update") ? _data[0, 3] + ";" + _data[0, 4] + ";" + _data[0, 5] : String.Empty;
        string _firstNameThaDefault = _action.Equals("update") ? _data[0, 8] : String.Empty;
        string _lastNameThaDefault = _action.Equals("update") ? _data[0, 9] : String.Empty;
        string _firstNameEngDefault = _action.Equals("update") ? _data[0, 6] : String.Empty;
        string _lastNameEngDefault = _action.Equals("update") ? _data[0, 7] : String.Empty;
        string _facultyDefault = _action.Equals("update") ? _data[0, 13] + ";" + _data[0, 14] : String.Empty;
        string _programDefault = _action.Equals("update") ? _data[0, 10] + ";" + _data[0, 11] + ";" + _data[0, 12] + ";" + _data[0, 15] + ";" + _data[0, 16] + ";" + _data[0, 17] : String.Empty;
        string _majorCodeDefault = _action.Equals("update") ? _data[0, 12] : String.Empty;
        string _groupNumDefault = _action.Equals("update") ? _data[0, 15] : String.Empty;
        string _dlevelDefault = _action.Equals("update") ? _data[0, 16] : String.Empty;
        string _pictureFileNameDefault = _action.Equals("update") ? _data[0, 44] : String.Empty;
        string _pictureFolderNameDefault = _action.Equals("update") ? _data[0, 45] : String.Empty;
        string _pursuantBookDefault = _action.Equals("update") ? _data[0, 18] : String.Empty;
        string _pursuantDefault = _action.Equals("update") ? _data[0, 19] : String.Empty;
        string _pursuantBookDateDefault = _action.Equals("update") ? _data[0, 20] : String.Empty;
        string _locationDefault = _action.Equals("update") ? _data[0, 21] : String.Empty;
        string _inputDateDefault = _action.Equals("update") ? _data[0, 22] : String.Empty;
        string _stateLocationDefault = _action.Equals("update") ? _data[0, 23] : String.Empty;
        string _stateLocationDateDefault = _action.Equals("update") ? _data[0, 24] : String.Empty;
        string _contractDateDefault = _action.Equals("update") ? _data[0, 25] : String.Empty;
        string _contractDateAgreementDefault = _action.Equals("update") ? _data[0, 46] : String.Empty;
        string _guarantorDefault = _action.Equals("update") ? _data[0, 26] : String.Empty;
        string _scholarDefault = _action.Equals("update") ? _data[0, 27] : "0";
        string _scholarshipMoneyDefault = ((_action.Equals("update")) && (!_data[0, 28].Equals("0"))) ? double.Parse(_data[0, 28]).ToString("#,##0") : String.Empty;
        string _scholarshipYearDefault = ((_action.Equals("update")) && (!_data[0, 29].Equals("0"))) ? double.Parse(_data[0, 29]).ToString("#,##0") : String.Empty;
        string _scholarshipMonthDefault = ((_action.Equals("update")) && (!_data[0, 30].Equals("0"))) ? double.Parse(_data[0, 30]).ToString("#,##0") : String.Empty;
        string _educationDateStartDefault = _action.Equals("update") ? _data[0, 31] : String.Empty;
        string _educationDateEndDefault = _action.Equals("update") ? _data[0, 32] : String.Empty;
        string _caseGraduateBreakContractDefault = _action.Equals("update") ? _data[0, 33] : "0";
        string _civilDefault = _action.Equals("update") ? _data[0, 34] : "0";
        string _contractForceDateStartDefault = _action.Equals("update") ? _data[0, 47] : String.Empty;
        string _contractForceDateEndDefault = _action.Equals("update") ? _data[0, 48] : String.Empty;
        string _calDateCondition = _action.Equals("update") ? _data[0, 35] : String.Empty;
        string _setAmtIndemnitorYear = _action.Equals("update") ? _data[0, 51] : String.Empty;
        string _indemnitorYearDefault = ((_action.Equals("update")) && (!_data[0, 36].Equals("0"))) ? double.Parse(_data[0, 36]).ToString("#,##0") : String.Empty;
        string _indemnitorCashDefault = _action.Equals("update") ? double.Parse(_data[0, 37]).ToString("#,##0") : String.Empty;
        string _commentEditDefault = _action.Equals("update") ? _data[0, 38] : String.Empty;
        string _rejectEditDateDefault = _action.Equals("update") ? _data[0, 49] : String.Empty;
        string _statusEdit = _action.Equals("update") ? _data[0, 42] : String.Empty;
        string _trackingStatus = _action.Equals("update") ? (_data[0, 40] + _data[0, 41] + _data[0, 42] + _data[0, 43]) : String.Empty;    

        _html += "<div class='form-content' id='" + _action + "-cp-trans-break-contract'>" +
                 "  <div id='addupdate-cp-trans-break-contract'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +                 
                 "      <input type='hidden' id='picture-student-hidden' value='" + ((!String.IsNullOrEmpty(_pictureFileNameDefault)) ? "Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT : String.Empty) + "' />" +
                 "      <input type='hidden' id='student-fullid-hidden' value='" + _studentFullIDDefault + "' />" +
                 "      <input type='hidden' id='student-fullname-tha-hidden' value='" + _studentFullnameThaDefault + "' />" +
                 "      <input type='hidden' id='student-fullname-eng-hidden' value='" + _studentFullnameEngDefault + "' />" +
                 "      <input type='hidden' id='student-dlevel-hidden' value='" + _studentDLevelDefault + "' />" +
                 "      <input type='hidden' id='student-faculty-hidden' value='" + _studentFacultyDefault + "' />" +
                 "      <input type='hidden' id='student-program-hidden' value='" + _studentProgramDefault + "' />" +
                 "      <input type='hidden' id='studentid-hidden' value='" + _studentIDDefault + "' />" +
                 "      <input type='hidden' id='titlename-hidden' value='" + _titleNameDefault + "' />" +
                 "      <input type='hidden' id='firstname-tha-hidden' value='" + _firstNameThaDefault + "' />" +
                 "      <input type='hidden' id='lastname-tha-hidden' value='" + _lastNameThaDefault + "' />" +
                 "      <input type='hidden' id='firstname-eng-hidden' value='" + _firstNameEngDefault + "' />" +
                 "      <input type='hidden' id='lastname-eng-hidden' value='" + _lastNameEngDefault + "' />" +
                 "      <input type='hidden' id='faculty-hidden' value='" + _facultyDefault + "' />" +
                 "      <input type='hidden' id='program-hidden' value='" + _programDefault + "' />" +
                 "      <input type='hidden' id='dlevel-hidden' value='" + _dlevelDefault + "' />" +
                 "      <input type='hidden' id='profile-student-id-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-titlename-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-firstname-tha-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-lastname-tha-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-firstname-eng-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-lastname-eng-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-faculty-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-program-hidden' value='' />" +
                 "      <input type='hidden' id='profile-student-dlevel-hidden' value='' />" +
                 "      <input type='hidden' id='trackingstatus' value='" + _trackingStatus + "' />" +
                 "      <input type='hidden' id='pursuant-book-hidden' value='" + _pursuantBookDefault + "' />" +
                 "      <input type='hidden' id='pursuant-hidden' value='" + _pursuantDefault + "' />" +
                 "      <input type='hidden' id='pursuant-book-date-hidden' value='" + _pursuantBookDateDefault + "' />" +
                 "      <input type='hidden' id='location-hidden' value='" + _locationDefault + "' />" +
                 "      <input type='hidden' id='input-date-hidden' value='" + _inputDateDefault + "' />" +
                 "      <input type='hidden' id='state-location-hidden' value='" + _stateLocationDefault + "' />" +
                 "      <input type='hidden' id='state-location-date-hidden' value='" + _stateLocationDateDefault + "' />" +
                 "      <input type='hidden' id='contract-date-hidden' value='" + _contractDateDefault + "' />" +
                 "      <input type='hidden' id='contract-date-agreement-hidden' value='" + _contractDateAgreementDefault + "' />" +
                 "      <input type='hidden' id='guarantor-hidden' value='" + _guarantorDefault + "' />" +
                 "      <input type='hidden' id='scholar-hidden' value='" + _scholarDefault + "' />" +
                 "      <input type='hidden' id='scholarship-money-hidden' value='" + _scholarshipMoneyDefault + "' />" +
                 "      <input type='hidden' id='scholarship-year-hidden' value='" + _scholarshipYearDefault + "' />" +
                 "      <input type='hidden' id='scholarship-month-hidden' value='" + _scholarshipMonthDefault + "' />" +
                 "      <input type='hidden' id='education-date-start-hidden' value='" + _educationDateStartDefault + "' />" +
                 "      <input type='hidden' id='education-date-end-hidden' value='" + _educationDateEndDefault + "' />" +
                 "      <input type='hidden' id='case-graduate-break-contract-hidden' value='" + _caseGraduateBreakContractDefault + "' />" +
                 "      <input type='hidden' id='civil-hidden' value='" + _civilDefault + "' />" +
                 "      <input type='hidden' id='contract-force-date-start-hidden' value='" + _contractForceDateStartDefault + "' />" +
                 "      <input type='hidden' id='contract-force-date-end-hidden' value='" + _contractForceDateEndDefault + "' />" +
                 "      <input type='hidden' id='cal-date-condition-hidden' value='" + _calDateCondition + "' />" +
                 "      <input type='hidden' id='set-amt-indemnitor-year' value='" + _setAmtIndemnitorYear  + "' />" +
                 "      <input type='hidden' id='indemnitor-year-hidden' value='" + _indemnitorYearDefault + "' />" +
                 "      <input type='hidden' id='indemnitor-cash-hidden' value='" + _indemnitorCashDefault + "' />" +
                 "      <input type='hidden' id='comment-hidden' value='" + _commentEditDefault + "' />" +
                 "      <div>" + 
                 "          <div id='profile-student'>" +
                 "              <div class='content-left' id='picture-add-student'>" +
                 "                  <div id='picture-student'></div>" +
                 "                  <div id='add-student'><a id='link-add-student' href='javascript:void(0)' onclick=LoadForm(1,'addprofilestudent',true,'','','')>บันทึกข้อมูลนักศึกษา</a></div>" +
                 "              </div>" +
                 "              <div class='content-left' id='profile-student-label'>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล ( ภาษาไทย )</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล ( ภาษาอังกฤษ )</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                 "                  <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>หลักสูตร</div></div>" +
                 "              </div>" +
                 "              <div class='content-left' id='profile-student-input'>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span id='student-id'></span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span id='student-fullname-tha'></span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span id='student-fullname-eng'></span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span id='student-dlevel'></span></div></div>" +
                 "                  <div class='form-label-discription-style'><div class='form-label-style'><span id='student-faculty'></span></div></div>" +
                 "                  <div class='form-label-discription-style clear-bottom'><div class='form-label-style'><span id='student-program'></span></div></div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='box3'></div>" +
                 "          <div id='pursuant-detail'>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style clear-bottom'>" +
                 "                      <div id='pursuant-detail-label'>" +
                 "                          <div class='form-label-style'>รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้น</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาใส่รายละเอียดการรับเรื่องจากหน่วยงานชั้นต้น</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style clear-bottom'>" +
                 "                      <div class='form-input-content' id='pursuant-detail-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='pursuant-book-label'>ตามหนังสือ</div>" +
                 "                              <div class='content-left' id='pursuant-book-input'><input class='inputbox' type='text' id='pursuant-book' onblur=Trim('pursuant-book'); value='' style='width:376px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='pursuant-label'>เลขที่หนังสือ</div>" +
                 "                              <div class='content-left' id='pursuant-input'><input class='inputbox' type='text' id='pursuant' onblur=Trim('pursuant'); value='' style='width:250px' /></div>" +
                 "                              <div class='content-left' id='pursuant-book-date-label'>วันที่</div>" +
                 "                              <div class='content-left' id='pursuant-book-date-input'><input class='inputbox calendar' type='text' id='pursuant-book-date' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='location-label'>ม.มหิดลรับที่</div>" +
                 "                              <div class='content-left' id='location-input'><input class='inputbox' type='text' id='location' onblur=Trim('location'); value='' style='width:250px' /></div>" +
                 "                              <div class='content-left' id='input-date-label'>วันที่</div>" +
                 "                              <div class='content-left' id='input-date-input'><input class='inputbox calendar' type='text' id='input-date' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='state-location-label'>กบศ.รับที่</div>" +
                 "                              <div class='content-left' id='state-location-input'><input class='inputbox' type='text' id='state-location' onblur=Trim('state-location'); value='' style='width:250px' /></div>" +
                 "                              <div class='content-left' id='state-location-date-label'>วันที่</div>" +
                 "                              <div class='content-left' id='state-location-date-input'><input class='inputbox calendar' type='text' id='state-location-date' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box3'></div>" +
                 "          <div id='break-contract'>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='contract-date-guarantor-label'>" +
                 "                          <div class='form-label-style'>สัญญานักศึกษา / สัญญาค้ำประกัน</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาใส่วันที่ของสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชาในคณะข้างต้น และกรุณาใส่</div>" +
                 "                              <div class='form-discription-line2-style'>วันที่ของสัญญาค้ำประกันและชื่อผู้ค้ำประกัน</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='contract-date-guarantor-input'>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='contract-date-label'>สัญญานักศึกษาลงวันที่</div>" +
                 "                              <div class='content-left' id='contract-date-input'><input class='inputbox calendar' type='text' id='contract-date' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='contract-date-agreement-label'>สัญญาค้ำประกันลงวันที่</div>" +
                 "                              <div class='content-left' id='contract-date-agreement-input'><input class='inputbox calendar' type='text' id='contract-date-agreement' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='guarantor-label'>ชื่อผู้ค้ำประกัน คือ</div>" +
                 "                              <div class='content-left' id='guarantor-input'><input class='inputbox' type='text' id='guarantor' onblur=Trim('guarantor'); value='' style='width:320px' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                      <div class='clear'></div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='scholar-label'>" +
                 "                          <div class='form-label-style'>สถานะการได้รับทุนการศึกษา</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาเลือกสถานะการได้รับทุนการศึกษา และใส่รายละเอียดของทุนการศึกษา กรณี</div>" +
                 "                              <div class='form-discription-line2-style'>ที่ได้รับทุนการศึกษา</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='scholar-input'>" +
                 "                          <div id='status-scholar-input'>" +
                 "                              <div class='combobox'>" +
                 "                                  <select id='scholar'>" +
                 "                                      <option value='0'>เลือกสถานะการได้รับทุนการศึกษา</option>";

        for (_i = 0; _i < eCPUtil._scholar.GetLength(0); _i++) {
            _html += "                                  <option value='" + (_i + 1) + "'>" + eCPUtil._scholar[_i] + "</option>";
        }

        _html += "                                  </select>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='scholarship-money-label'>จำนวนเงินทุนการศึกษา</div>" +
                 "                              <div class='content-left' id='scholarship-money-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-money' onblur=Trim('scholarship-money');AddCommas('scholarship-money',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:115px' /></div>" +
                 "                              <div class='content-left' id='scholarship-money-unit-label'>บาท / หลักสูตร</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='scholarship-year-month-label'>ระยะเวลาที่ได้รับทุน</div>" +
                 "                              <div class='content-left' id='scholarship-year-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-year' onblur=Trim('scholarship-year');AddCommas('scholarship-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:43px' /></div>" +
                 "                              <div class='content-left' id='scholarship-year-unit-label'>ปี</div>" +
                 "                              <div class='content-left' id='scholarship-month-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-month' onblur=Trim('scholarship-month');AddCommas('scholarship-month',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:43px' /></div>" +
                 "                              <div class='content-left' id='scholarship-month-unit-label'>เดือน</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='case-graduate-label'>" +
                 "                          <div class='form-label-style'>สถานะการสำเร็จการศึกษา</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาเลือกสถานะการสำเร็จการศึกษา และใส่วันที่เริ่มต้นเข้าศึกษาและวันที่สำเร็จ</div>" +
                 "                              <div class='form-discription-line2-style'>การศึกษา หรือวันที่พ้นสภาพนักศึกษา</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='case-graduate-input'>" +
                 "                          <div id='case-graduate-break-contract-input'>" +
                 "                              <div class='combobox'>" +
                 "                                  <select id='case-graduate-break-contract'>" +
                 "                                      <option value='0'>เลือกสถานะการสำเร็จการศึกษา</option>";

        for (_i = 0; _i < eCPUtil._caseGraduate.GetLength(0); _i++) {
            _html += "                                  <option value='" + (_i + 1) + "'>" + eCPUtil._caseGraduate[_i, 1] + "</option>";
        }

        _html += "                                  </select>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                          <div>" +
                 "                              <div class='content-left' id='education-date-start-label'>เริ่มต้นเข้าศึกษาเมื่อวันที่</div>" +
                 "                              <div class='content-left' id='education-date-start-input'><input class='inputbox calendar' type='text' id='education-date-start' readonly value='' /></div>" +
                 "                              <div class='content-left' id='education-date-end-label'>ถึงวันที่</div>" +
                 "                              <div class='content-left' id='education-date-end-input'><input class='inputbox calendar' type='text' id='education-date-end' readonly value='' /></div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='case-civil-label'>" +
                 "                          <div class='form-label-style'>สถานะการรับราชการ</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาเลือกสถานะการปฏิบัติงานชดใช้ กรณีสำเร็จการศึกษา</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='case-civil-input'>" +
                 "                          <div id='case-civil-break-contract-input'>" +
                 "                              <div class='combobox'>" +
                 "                                  <select id='civil'>" +
                 "                                      <option value='0'>เลือกสถานะการปฏิบัติงานชดใช้</option>";

        for (_i = 0; _i < eCPUtil._civil.GetLength(0); _i++) {
            _html += "                                  <option value='" + (_i + 1) + "'>" + eCPUtil._civil[_i] + "</option>";
        }

        _html += "                                  </select>" +
                 "                              </div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style'>" +
                 "                      <div id='contract-force-date-label'>" +
                 "                          <div class='form-label-style'>วันที่สัญญามีผลบังคับใช้</div>" +
                 "                          <div class='form-discription-style'>" +
                 "                              <div class='form-discription-line1-style'>กรุณาใส่วันที่สัญญามีผลบังคับใช้ ( เพื่อนำไปคำนวณระยะเวลาที่ใช้ศึกษา กรณีไม่</div>" +
                 "                              <div class='form-discription-line2-style'>สำเร็จการศึกษา กรณีนักศึกษาหลักสูตรพยาบาลศาสตรบัณฑิต ถ้าไม่สำเร็จการ</div>" +
                 "                              <div class='form-discription-line3-style'>ศึกษาให้สัญญามีผลบังคับตั้งแต่ปี 3 )</div>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style'>" +
                 "                      <div class='form-input-content' id='contract-force-date-input'>" +
                 "                          <div class='content-left' id='contract-force-date-start-label'>สัญญามีผลบังคับใช้วันที่</div>" +
                 "                          <div class='content-left' id='contract-force-date-start-input'><input class='inputbox calendar' type='text' id='contract-force-date-start' readonly value='' /></div>" +
                 "                          <div class='content-left' id='contract-force-date-end-label'>ถึงวันที่</div>" +
                 "                          <div class='content-left' id='contract-force-date-end-input'><input class='inputbox calendar' type='text' id='contract-force-date-end' readonly value='' /></div>" +
                 "                      </div>" +
                 "                      <div class='clear'></div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "              <div>" +
                 "                  <div class='form-label-discription-style " + (_statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
                 "                      <div id='indemnitor-label'>" +
                 "                          <div class='form-label-style'>การขอชดใช้ตามสัญญา</div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='form-input-style " + (_statusEdit.Equals("2") ? "clear-bottom" : "") + "'>" +
                 "                      <div class='form-input-content' id='indemnitor-input'>" +
                 "                          <div>" +
                 "                              <input type='hidden' id='cal-date-condition' value='' />" +
                 "                              <div class='content-left' id='indemnitor-year-label'>ทำงานชดใช้เป็นเวลา</div>" +
                 "                              <div class='content-left' id='indemnitor-year-input'><input class='inputbox textbox-numeric' type='text' id='indemnitor-year' value='' style='width:43px' /></div>" +
                 "                              <div class='content-left' id='indemnitor-year-unit-label'>ปี</div>" +
                 "                              <div class='content-left' id='indemnitor-cash-label'>หรือชดใช้เป็นเงินจำนวน</div>" +
                 "                              <div class='content-left' id='indemnitor-cash-input'><input class='inputbox textbox-numeric' type='text' id='indemnitor-cash' value='' style='width:121px' /></div>" +
                 "                              <div class='content-left' id='indemnitor-cash-unit-label'>บาท</div>" +
                 "                          </div>" +
                 "                          <div class='clear'></div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>";                 

        if (_statusEdit.Equals("2")) {
            _html += "      <div class='box3'></div>" +
                     "      <div id='comment-detail'>" +
                     "          <div>" +
                     "              <div class='form-label-discription-style'>" +
                     "                  <div id='comment-label'>" +
                     "                      <div class='form-label-style'>สาเหตุการส่งกลับแก้ไขรายการแจ้ง</div>" +
                     "                      <div class='form-discription-style'>" +
                     "                          <div class='form-discription-line1-style'>รายงานสาเหตุหรือเหตุผลที่ส่งรายการแจ้งกลับมาแก้ไข</div>" +
                     "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "              <div class='form-input-style'>" +
                     "                  <div class='form-input-content' id='comment-input'>" +
                     "                      <div class='textareabox' id='comments'><div id='comment'>ส่งกลับแก้ไขรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(_rejectEditDateDefault)) + " สาเหตุ<span id='comment-message'></span></div></div>" +
                     "                  </div>" +
                     "              </div>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>";
        }

        _html += "      </div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTransBreakContract('" + _action + "')>บันทึก</a></li>";

        if (_action.Equals("update"))
            _html += "          <li><a href='javascript:void(0)' onclick=LoadForm(1,'addcommentcancelbreakcontract',true,'','" + _cp1id + "','')>ยกเลิกรายการ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTransBreakContract(false)'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-break-contract')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li id='button-status-p'><a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + _cp1id + "','v1')>พิมพ์แบบตรวจสอบ</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-trans-break-contract')>ปิด</a></li>" +
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

    public static string AddCPTransBreakContract() {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTransBreakContract("add", _data);

        return _html;
    }

    public static string UpdateCPTransBreakContract(string _cp1id) {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransBreakContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTransBreakContract("update", _data);

        return _html;
    }

    public static string DetailCPTransBreakRequireContract(
        string _cp1id,
        string[,] _data,
        string _status
    ) {
        string _html = String.Empty;       
        string _studentIDDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 2] : _data[0, 19];
        string _titleNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 5] : _data[0, 20];
        string _firstNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 8] : _data[0, 21];
        string _lastNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 9] : _data[0, 22];
        string _facultyCodeDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 13] : _data[0, 26];
        string _facultyNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 14] : _data[0, 27];
        string _programCodeDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 10] : _data[0, 23];
        string _programNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 11] : _data[0, 24];
        string _groupNumDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 15] : _data[0, 28];
        string _dlevelDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 17] : _data[0, 30];
        string _pictureFileNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 44] : _data[0, 56];
        string _pictureFolderNameDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 45] : _data[0, 57];
        string _pursuantBookDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 18] : _data[0, 31];
        string _pursuantDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 19] : _data[0, 32];
        string _pursuantBookDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 20] : _data[0, 33];
        string _locationDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 21] : _data[0, 34];
        string _inputDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 22] : _data[0, 35];
        string _stateLocationDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 23] : _data[0, 36];
        string _stateLocationDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 24] : _data[0, 37];
        string _contractDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 25] : _data[0, 38];
        string _contractDateAgreementDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 46] : _data[0, 61];
        string _guarantorDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 26] : _data[0, 39];
        string _scholarDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 27] : _data[0, 40];
        string _scholarshipMoneyDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 28] : _data[0, 41];
        string _scholarshipYearDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 29] : _data[0, 42];
        string _scholarshipMonthDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 30] : _data[0, 43];
        string _educationDateStartDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 31] : _data[0, 44];
        string _educationDateEndDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 32] : _data[0, 45];
        string _caseGraduateBreakContractDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 33] : _data[0, 46];
        string _civilDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 34] : _data[0, 47];
        string _contractForceDateStartDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 47] : _data[0, 62];
        string _contractForceDateEndDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 48] : _data[0, 63];   
        string _indemnitorYearDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 36] : _data[0, 49];
        string _indemnitorCashDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 37] : _data[0, 50];
        string _commentEditDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 38] : _data[0, 59];
        string _rejectEditDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 49] : _data[0, 64];
        string _commentCancelDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 39] : _data[0, 60];
        string _rejectCancelDateDefault = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 50] : _data[0, 65];
        string _statusEdit = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 42] : _data[0, 54];
        string _statusCancel = (_status.Equals("v1") || _status.Equals("a")) ? _data[0, 43] : _data[0, 55];
        string _trackingStatus = (_status.Equals("v1") || _status.Equals("a")) ? (_data[0, 40] + _data[0, 41] + _data[0, 42] + _data[0, 43]) : (_data[0, 52] + _data[0, 53] + _data[0, 54] + _data[0, 55]);
        string _cp2id = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 1] : String.Empty;
        string _setAmtIndemnitorYear = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 77] : String.Empty;
        string _indemnitorAddressDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 3] : String.Empty;
        string _provinceTNameDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 5] : String.Empty;
        string _studyLeaveDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 66] : String.Empty;
        string _requireDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 6] : String.Empty;
        string _approveDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 7] : String.Empty;
        string _beforeStudyLeaveStartDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 67] : String.Empty;
        string _beforeStudyLeaveEndDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 68] : String.Empty;
        string _studyLeaveStartDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 69] : String.Empty;
        string _studyLeaveEndDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 70] : String.Empty;
        string _afterStudyLeaveStartDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 71] : String.Empty;
        string _afterStudyLeaveEndDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 72] : String.Empty;
        string _actualMonthScholarshipDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 8] : String.Empty;
        string _actualScholarshipDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 9] : String.Empty;
        string _totalPayScholarshipDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 10] : String.Empty;
        string _actualMonthDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 11] : String.Empty;
        string _actualDayDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 12] : String.Empty;
        string _allActualDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 13] : String.Empty;
        string _actualDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 14] : String.Empty;
        string _remainDateDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 15] : String.Empty;
        string _subtotalPenaltyDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 16] : String.Empty;
        string _totalPenaltyDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 17] : String.Empty;
        string _lawyerFullnameDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 73] : String.Empty;
        string _lawyerPhoneNumberDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 74] : String.Empty;
        string _lawyerMobileNumberDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 75] : String.Empty;
        string _lawyerEmailDefault = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 76] : String.Empty;
        string _lawyerDefault = String.Empty;
        string _statusRepay = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 18] : String.Empty;
        string _statusPayment = (_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1")) ? _data[0, 58] : String.Empty;
        string[] _statusRepayCurrent;

        ArrayList _lawyerPhoneNumber = new ArrayList();

        if (!String.IsNullOrEmpty(_lawyerPhoneNumberDefault))
            _lawyerPhoneNumber.Add(_lawyerPhoneNumberDefault);

        if (!String.IsNullOrEmpty(_lawyerMobileNumberDefault))
            _lawyerPhoneNumber.Add(_lawyerMobileNumberDefault);


        if (!String.IsNullOrEmpty(_lawyerFullnameDefault) && (!String.IsNullOrEmpty(_lawyerPhoneNumberDefault) || !String.IsNullOrEmpty(_lawyerMobileNumberDefault) && !String.IsNullOrEmpty(_lawyerEmailDefault))) {
            _lawyerDefault += "คุณ<span>" + _lawyerFullnameDefault + "</span>" + (_lawyerPhoneNumber.Count > 0 ? (" ( <span>" + String.Join(", ", _lawyerPhoneNumber.ToArray()) + "</span> )") : String.Empty) +
                              " อีเมล์ <span>" + _lawyerEmailDefault + "</span>";
        }

        _html += "<div class='form-content' id='detail-cp-trans-break-require-contract'>" +
                 "  <input type='hidden' id='trackingstatus' value='" + _trackingStatus + "' />" +
                 "  <div id='pursuant-detail-detail'>" +
                 "      <div class='form-input-style'>" +
                 "          <div class='form-input-content form-label-style'>" +
                 "              <div class='content-left' id='pursuant-book-detail-label'>ตามหนังสือ</div>" +
                 "              <div class='content-left' id='pursuant-book-detail-input'><span>" + _pursuantBookDefault + "</span></div>" +
                 "              <div class='content-left' id='pursuant-detail-label'>ที่</div>" +
                 "              <div class='content-left' id='pursuant-detail-input'><span>" + _pursuantDefault + "</span></div>" +
                 "              <div class='content-left' id='pursuant-book-date-detail-label'>วันที่</div>" +
                 "              <div class='content-left' id='pursuant-book-date-detail-input'><span>" + Util.LongDateTH(_pursuantBookDateDefault) + "</span></div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "      <div class='form-input-style'>" +
                 "          <div class='form-input-content form-label-style'>" +
                 "              <div class='content-left' id='location-detail-label'>ม.มหิดลรับที่</div>" +
                 "              <div class='content-left' id='location-detail-input'><span>" + _locationDefault + "</span></div>" +
                 "              <div class='content-left' id='input-date-detail-label'>วันที่</div>" +
                 "              <div class='content-left' id='input-date-detail-input'><span>" + Util.LongDateTH(_inputDateDefault) + "</span></div>" +
                 "              <div class='content-left' id='state-location-detail-label'>กบศ. รับที่</div>" +
                 "              <div class='content-left' id='state-location-detail-input'><span>" + _stateLocationDefault + "</span></div>" +
                 "              <div class='content-left' id='state-location-date-detail-label'>วันที่</div>" +
                 "              <div class='content-left' id='state-location-date-input'><span>" + Util.LongDateTH(_stateLocationDateDefault) + "</span></div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'></div>" +
                 "  <div id='profile-student'>" +
                 "      <div class='content-left " + (_status.Equals("a") ? "status-a" : String.Empty) + "' id='picture-student'><div><img src='Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "' /></div></div>" +
                 "      <div class='content-left " + (_status.Equals("a") ? "status-a" : String.Empty) + "' id='profile-student-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                 "          <div class='form-label-discription-style " + (_status.Equals("a") ? "clear-bottom" : String.Empty) + "'><div class='form-label-style'>หลักสูตร</div></div>";

        if (!_status.Equals("a"))
            _html += "      <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>นิติกรผู้รับผิดชอบ</div></div>";

        _html += "      </div>" +
                 "      <div class='content-left' id='profile-student-input'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _studentIDDefault + "&nbsp;" + _programCodeDefault.Substring(0, 4) + " / " + _programCodeDefault.Substring(4, 1) + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _titleNameDefault + _firstNameDefault + " " + _lastNameDefault + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _dlevelDefault + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _facultyCodeDefault + " - " + _facultyNameDefault + "</span></div></div>" +
                 "          <div class='form-label-discription-style " + (_status.Equals("a") ? "clear-bottom" : String.Empty) + "'><div class='form-label-style'><span>" + _programCodeDefault + " - " + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : "") + "</span></div></div>";

        if (!_status.Equals("a"))
            _html += "      <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>" + _lawyerDefault + "</div></div>";

        _html += "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='box3'></div>" +
                 "  <div id='break-contract-detail'>" +
                 "      <div class='content-left' id='break-contract-detail-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>สัญญานักศึกษาลงวันที่</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>สัญญาค้ำประกันลงวันที่ / ผู้ค้ำประกัน</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>สถานะการได้รับทุนการศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>สถานะการสำเร็จการศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>วันที่สัญญามีผลบังคับใช้</div></div>" +
                 "          <div class='form-label-discription-style " + ((_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1") || _statusCancel.Equals("2") || _statusEdit.Equals("2")) ? "clear-bottom" : "") + "'><div class='form-label-style'>สถานะการปฏิบัติงานชดใช้ / การขอชดใช้ตามสัญญา</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='break-contract-detail-input'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + Util.LongDateTH(_contractDateDefault) + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + (!String.IsNullOrEmpty(_contractDateAgreementDefault) ? Util.LongDateTH(_contractDateAgreementDefault) : "-") + "</span> / <span>" + _guarantorDefault + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + eCPUtil._scholar[int.Parse(_scholarDefault) - 1] + "</span> " + (_scholarDefault.Equals("1") ? "จำนวนเงิน <span>" + double.Parse(_scholarshipMoneyDefault).ToString("#,##0") + "</span> บาท / หลักสูตร ระยะเวลา <span>" + (!_scholarshipYearDefault.Equals("0") ? double.Parse(_scholarshipYearDefault).ToString("#,##0") : "-") + "</span> ปี <span>" + (!_scholarshipMonthDefault.Equals("0") ? double.Parse(_scholarshipMonthDefault).ToString("#,##0") : "-") + "</span> เดือน" : "") + "</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + eCPUtil._caseGraduate[int.Parse(_caseGraduateBreakContractDefault) - 1, 1] + "</span> เริ่มเข้าศึกษาเมื่อวันที่ <span>" + Util.LongDateTH(_educationDateStartDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_educationDateEndDefault) + "</span></div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>สัญญาเริ่มมีผลบังคับใช้เมื่อวันที่ <span>" + (!String.IsNullOrEmpty(_contractForceDateStartDefault) ? Util.LongDateTH(_contractForceDateStartDefault) : "-") + "</span> ถึงวันที่ <span>" + (!String.IsNullOrEmpty(_contractForceDateEndDefault) ? Util.LongDateTH(_contractForceDateEndDefault) : "-") + "</span></div></div>" +
                 "          <div class='form-label-discription-style " + ((_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1") || _statusCancel.Equals("2") || _statusEdit.Equals("2")) ? "clear-bottom" : "") + "'><div class='form-label-style'><span>" + (!_civilDefault.Equals("0") ? eCPUtil._civil[int.Parse(_civilDefault) - 1] : "-") + "</span> / ทำงานชดใช้เป็นเวลา <span>" + (!_indemnitorYearDefault.Equals("0") ? double.Parse(_indemnitorYearDefault).ToString("#,##0") : "-") + "</span> ปี หรือชดใช้เงินจำนวน <span>" + double.Parse(_indemnitorCashDefault).ToString("#,##0") + "</span> บาท</div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>";

        if ((_status.Equals("v2") || _status.Equals("v3") || _status.Equals("r") || _status.Equals("r1"))) {
            _statusRepayCurrent = (eCPDB.SearchRepayStatusDetail(_cp2id, _statusRepay, _statusPayment)).Split(new char[] { ';' });

            if (_caseGraduateBreakContractDefault.Equals("1")) {
                _html += "<div class='box3'></div>" +
                         "<div id='cal-contract-penalty1'>" +
                         "  <div class='content-left' id='cal-contract-penalty1-label'>" +
                         "      <div class='form-label-discription-style'>" +
                         "          <div class='form-label-style'>เงินชดใช้</div>" +
                         "          <div class='form-discription-style'>" +
                         "              <div class='form-discription-line1-style'>คำนวณเงินทุนการศึกษาที่ต้องชดใช้กรณีนักศึกษารับทุนการ</div>" +
                         "              <div class='form-discription-line2-style'>ศึกษา และคำนวณเงินที่ต้องชดใช้ตามระยะเวลาที่เข้าศึกษา</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "  <div class='content-left' id='cal-contract-penalty1-input'>" +
                         "      <div class='form-input-style'>" +
                         "          <div class='form-input-content'>" +
                         "              <div>ระยะเวลาที่ชดใช้ทุนการศึกษา <span>" + (!String.IsNullOrEmpty(_actualMonthScholarshipDefault) ? double.Parse(_actualMonthScholarshipDefault).ToString("#,##0") : "-") + "</span> เดือน เป็นเงิน <span>" + (!String.IsNullOrEmpty(_actualScholarshipDefault) ? double.Parse(_actualScholarshipDefault).ToString("#,##0.00") : "-") + "</span> บาท / เดือน</div>" +
                         "              <div class='form-input-content-line'>ยอดเงินทุนการศึกษาที่ชดใช้ <span>" + double.Parse(_totalPayScholarshipDefault).ToString("#,##0.00") + "</span> บาท</div>" +
                         "              <div class='form-input-content-line'>ระยะเวลาที่ใช้ในการศึกษา <span>" + double.Parse(_actualMonthDefault).ToString("#,##0") + "</span> เดือน <span>" + double.Parse(_actualDayDefault).ToString("#,##0") + "</span> วัน</div>" +
                         "              <div class='form-input-content-line'>ยอดเงินค่าปรับผิดสัญญา <span>" + double.Parse(_subtotalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div>" +
                         "              <div class='form-input-content-line'>ยอดเงินที่ต้องรับผิดชอบชดใช้ <span>" + double.Parse(_totalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "</div>" +
                         "<div class='clear'></div>";
            }
                        
            if (_caseGraduateBreakContractDefault.Equals("2")) {
                _html += "<div class='box3'></div>";

                if (_civilDefault.Equals("1")) {
                    string _startDate = String.Empty;
                    string _endDate = String.Empty;
                    string _afterStudyLeave = String.Empty;

                    if (_studyLeaveDefault.Equals("N")) {
                        _startDate = _requireDateDefault;
                        _endDate = _approveDateDefault;
                    }

                    if (_studyLeaveDefault.Equals("Y")) {
                        _startDate = _beforeStudyLeaveStartDateDefault;
                        _endDate = _beforeStudyLeaveEndDateDefault;
                        _afterStudyLeave = ("<div class='form-input-content-line'>" + eCPUtil._studyLeave[1] + " และกลับเข้าปฏิบัติงาน</div><div class='form-input-content-line'>ตั้งแต่วันที่ <span>" + Util.LongDateTH(_afterStudyLeaveStartDateDefault) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_afterStudyLeaveEndDateDefault) + "</span></div>");
                    }

                    _html += "<div id='indemnitor-work'>" +
                             "  <div class='content-left study-leave-" + _studyLeaveDefault.ToLower() + "' id='indemnitor-work-label'>" +
                             "      <div class='form-label-discription-style'><div class='form-label-style'>รายละเอียดข้อมูลการทำงานชดใช้</div></div>" +
                             "  </div>" +
                             "  <div class='content-left study-leave-" + _studyLeaveDefault.ToLower() + "' id='indemnitor-work-input'>" +
                             "      <div class='form-input-style'>" +
                             "          <div class='form-input-content'>" +
                             "              <div>ทำงานชดใช้ที่ <span>" + _indemnitorAddressDefault + "</span></div>" +
                             "              <div class='form-input-content-line'>จังหวัด <span>" + _provinceTNameDefault + "</span> ตั้งแต่วันที่ <span>" + Util.LongDateTH(_startDate) + "</span> ถึงวันที่ <span>" + Util.LongDateTH(_endDate) + "</span></div>" +
                                            _afterStudyLeave +
                             "          </div>" +
                             "      </div>" +
                             "  </div>" +
                             "</div>" +
                             "<div class='clear'></div>";
                }

                _html += "<div id='cal-contract-penalty2'>" +
                         "  <div class='content-left' id='cal-contract-penalty2-label'>" +
                         "      <div class='form-label-discription-style'>" +
                         "          <div class='form-label-style'>เงินชดใช้</div>" +
                         "          <div class='form-discription-style'>" +
                         "              <div class='form-discription-line1-style'>คำนวณเงินทุนการศึกษาที่ต้องชดใช้ กรณีนักศึกษารับทุนการ</div>" +
                         "              <div class='form-discription-line2-style'>ศึกษาและคำนวณเงินที่ต้องชดใช้แทนการปฏิบัติงานส่วนที่ขาด</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "  <div class='content-left' id='cal-contract-penalty2-input'>" +
                         "      <div class='form-input-style'>" +
                         "          <div class='form-input-content'>" +
                         "              <div>ยอดเงินทุนการศึกษาที่ชดใช้ <span>" + double.Parse(_totalPayScholarshipDefault).ToString("#,##0.00") + "</span> บาท</div>";

                if (_setAmtIndemnitorYear.Equals("Y"))
                    _html += "          <div class='form-input-content-line'>ระยะเวลาที่ต้องปฏิบัติงานชดใช้ <span>" + (!String.IsNullOrEmpty(_allActualDateDefault) ? double.Parse(_allActualDateDefault).ToString("#,##0") : "-") + "</span> วัน ปฏิบัติงานชดใช้แล้ว <span>" + (!String.IsNullOrEmpty(_actualDateDefault) ? double.Parse(_actualDateDefault).ToString("#,##0") : "-") + "</span> วัน ขาด <span>" + (!String.IsNullOrEmpty(_remainDateDefault) ? double.Parse(_remainDateDefault).ToString("#,##0") : "-") + "</span> วัน</div>";

                if (_setAmtIndemnitorYear.Equals("N")) {
                    _html += "          <div class='form-input-content-line'>ระยะเวลาที่ใช้ในการศึกษา <span>" + (!String.IsNullOrEmpty(_actualDayDefault) ? double.Parse(_actualDayDefault).ToString("#,##0") : "-") + "</span> วัน</div>";

                    if (_civilDefault.Equals("1"))
                        _html += "      <div class='form-input-content-line'>ระยะเวลาที่ปฏิบัติงานชดใช้ <span>" + (!String.IsNullOrEmpty(_actualDateDefault) ? double.Parse(_actualDateDefault).ToString("#,##0") : "-") + "</span> วัน</div>";
                }

                _html += "              <div class='form-input-content-line'>ยอดเงินค่าปรับผิดสัญญา <span>" + double.Parse(_subtotalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div>" +
                         "              <div class='form-input-content-line'>ยอดเงินที่ต้องรับผิดชอบชดใช้ <span>" + double.Parse(_totalPenaltyDefault).ToString("#,##0.00") + "</span> บาท</div>" +
                         "          </div>" +
                         "      </div>" +
                         "  </div>" +
                         "</div>" +
                         "<div class='clear'></div>";
            }

            _html += "<div id='status-repay'>" +
                     "  <div class='content-left' id='status-repay-label'>" +
                     "      <div class='form-label-discription-style " + ((_statusCancel.Equals("2") || _statusEdit.Equals("2")) ? "clear-bottom" : "") + "'><div class='form-label-style'>สถานะการแจ้งชำระหนี้</div></div>" +
                     "  </div>" +
                     "  <div class='content-left' id='status-repay-input'>" +
                     "      <div class='form-label-discription-style " + ((_statusCancel.Equals("2") || _statusEdit.Equals("2")) ? "clear-bottom" : "") + "'><div class='form-label-style'><span>" + eCPUtil._repayStatusDetail[int.Parse(_statusRepayCurrent[0])] + "</span></div></div>" +
                     "  </div>" +
                     "</div>" + 
                     "<div class='clear'></div>";
        }

        if (_statusCancel.Equals("2") || _statusEdit.Equals("2")) {
            _html += "<div class='box3'></div>" +
                     "<div id='commenteditcancel-detail'>" +
                     "  <div class='content-left' id='commenteditcancel-label'>" +
                     "      <div class='form-label-discription-style'><div class='form-label-style'>" + (_statusCancel.Equals("2") ? "สาเหตุการยกเลิกรายการแจ้ง" : (_statusEdit.Equals("2") ? "สาเหตุการแก้ไขรายการแจ้ง" : "")) + "</div></div>" +
                     "  </div>" +
                     "  <div class='content-left' id='commenteditcancel-input'>" +
                     "      <div class='form-label-discription-style'><div class='form-label-style'><div class='textareabox' id='commentseditcancel'><div id='commenteditcancel'>" + (_statusCancel.Equals("2") ? ("ยกเลิกรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(_rejectCancelDateDefault)) + " สาเหตุ" + _commentCancelDefault) : (_statusEdit.Equals("2") ? ("ส่งกลับแก้ไขรายการแจ้งเมื่อวันที่ " + Util.LongDateTH(Util.ConvertDateTH(_rejectEditDateDefault)) + " สาเหตุ" + _commentEditDefault) : "")) + "</div></div></div></div>" +
                     "  </div>" +
                     "</div>" +
                     "<div class='clear'></div>";
        }

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        _html += "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style1-" + _eCPCookie["UserSection"] + _status + _statusEdit + _statusCancel + _statusPayment + "'>" +
                 "          <ul>";

        if (_status.Equals("a")) {
            _html += "          <li><a href='javascript:void(0)' onclick=ReceiverCPTransBreakContract('" + _cp1id + "','" + _trackingStatus + "')>รับรายการ</a></li>" +
                     "          <li><a href='javascript:void(0)' onclick=LoadForm(2,'addcommenteditbreakcontract',true,'','" + _cp1id + "','')>ส่งกลับแก้ไข</a></li>";
        }
 
        if (_status.Equals("r")) {
            _html += "          <li id='button-status-r'><a href='javascript:void(0)' onclick=LoadForm(2,'addupdaterepaycontract',true,'','" + _cp1id + "','')>รายละเอียดการแจ้งชำระหนี้</a></li>" +
                     "          <li id='button-status-i'><a href='javascript:void(0)' onclick=ViewCalInterestOverpayment('" + _cp2id + "')>คำนวณดอกเบี้ย</a></li>" +
                     "          <li><a href='javascript:void(0)' onclick=LoadForm(2,'addcommentcancelrepaycontract',true,'','" + _cp1id + "','')>ยกเลิกรายการ</a></li>";
        }

        if (_status.Equals("r1"))
            _html += "          <li id='button-status-r'><a href='javascript:void(0)' onclick=LoadForm(2,'viewrepaycontract',true,'','" + _cp1id + "','')>รายละเอียดการแจ้งชำระหนี้</a></li>";

        if (_eCPCookie["UserSection"].Equals("1") && _statusEdit.Equals("1") && _statusCancel.Equals("1") && (_status.Equals("v1") || _status.Equals("v2")))
            _html += "          <li id='button-status-" + _status + "'><a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + _cp1id + "','" + _status + "')>พิมพ์แบบตรวจสอบ</a></li>";

        if (_eCPCookie["UserSection"].Equals("2") && _statusCancel.Equals("1") && _status.Equals("v1"))
            _html += "          <li id='button-status-" + _status + "'><a href='javascript:void(0)' onclick=PrintNoticeCheckForReimbursement('" + _cp1id + "','" + _status + "')>พิมพ์แบบตรวจสอบ</a></li>";

        if (_status.Equals("v3"))
            _html += "          <li id='button-status-v3'><a href='javascript:void(0)' onclick=ViewTransPaymentReportDebtorContractByProgram('" + _cp2id + "')>รายละเอียดการชำระหนี้</a></li>";
  
        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target' src='#'></iframe>" +
                 "<form id='export-setvalue' method='post'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";

        return _html;
    }

    public static string DetailCPTransBreakContract(
        string _cp1id,
        string _status
    ) {
        string _html = String.Empty;
        string _trackingStatus = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTransBreakContract(_cp1id);

        if (_data.GetLength(0) > 0) {
            _html = DetailCPTransBreakRequireContract(_cp1id, _data, _status);
        }

        return _html;
    }

    public static string ListSearchStudentCPTransBreakContract(string _studentid) {
        string[,] _data;
        int _recordCount;
        int _error;

        _data = eCPDB.ListSearchStudentCPTransBreakContract(_studentid);
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0) {
            _error = 1;
        }
        else
            _error = 0;

        return "<error>" + _error + "<error>";
    }

    public static string ListSearchTrackingStatusCPTransBreakContract(string _cp1id) {
        string _trackingStatus = String.Empty;

        _trackingStatus = eCPDB.ChkTrackingStatusCPTransBreakContract(_cp1id);

        return "<trackingstatus>" + _trackingStatus + "<trackingstatus>";
    }

    public static string ListCPTransBreakContract(HttpContext _c) {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string _iconStatus = String.Empty;
        int _section;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);
        
        _recordCount = eCPDB.CountCPTransBreakContract(_c);

        if (_recordCount > 0) {
            _data = eCPDB.ListCPTransBreakContract(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++) {
                _groupNum = !_data[_i, 8].Equals("0") ? " ( กลุ่ม " + _data[_i, 8] + " )" : "";
                _trackingStatus = _data[_i, 9] + _data[_i, 10] + _data[_i, 11] + _data[_i, 12];
                _callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','" + _data[_i, 15] + "')";
                _iconStatus = eCPUtil._iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil._iconTrackingStatus, _trackingStatus) - 1, 1];
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";

                if (_section.Equals(1)) {
                    _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                             "  <li id='tab1-table-content-cp-trans-require-contract-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                             "  <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col2' onclick=" + _callFunc + "><div>" + _data[_i, 2] + "</div></li>" +
                             "  <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col3' onclick=" + _callFunc + "><div>" + _data[_i, 3] + _data[_i, 4] + " " + _data[_i, 5] + "</div></li>" +
                             "  <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 6] + "</span>- " + _data[_i, 7] + _groupNum + "</div></li>" +
                             "  <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col5' onclick=" + _callFunc + "><div>" + _data[_i, 14] + "</div></li>" +
                             "  <li class='table-col' id='tab1-table-content-cp-trans-require-contract-col6' onclick=" + _callFunc + ">" +
                             "      <div class='icon-status-style'>" +
                             "          <ul>" +
                             "              <li class='" + _iconStatus + "'></li>" +
                             "          </ul>" +
                             "      </div>" +
                             "  </li>" +
                             "</ul>";
                }

                if (_section.Equals(2)) {
                    _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                             "  <li id='tab2-table-content-cp-trans-break-contract-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col2' " + (!_trackingStatus.Equals("1111") ? "onclick=" + _callFunc : "") + "><div>" + (_trackingStatus.Equals("1111") ? "<input class='checkbox' type='checkbox' name='send-break-contract' onclick=UncheckRoot('check-uncheck-all') value='" + _data[_i, 1] + "' />" : "") + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col3' onclick=" + _callFunc + "><div>" + _data[_i, 2] + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col4' onclick=" + _callFunc + "><div>" + _data[_i, 3] + _data[_i, 4] + " " + _data[_i, 5] + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col5' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 6] + "</span>- " + _data[_i, 7] + _groupNum + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col6' onclick=" + _callFunc + "><div>" + _data[_i, 13] + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col7' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 14]) ? _data[_i, 14] : "-") + "</div></li>" +
                             "  <li class='table-col' id='tab2-table-content-cp-trans-break-contract-col8' onclick=" + _callFunc + ">" +
                             "      <div class='icon-status-style'>" +
                             "          <ul>" +
                             "              <li class='" + _iconStatus + "'></li>" +
                             "          </ul>" +
                             "      </div>" +
                             "  </li>" +
                             "</ul>";
                }
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "transbreakcontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string TabCPTransBreakContract() {
        string _html = String.Empty;
        int _section;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        Array _trackingStatus = (_section.Equals(1) ? eCPUtil._trackingStatusORLA : eCPUtil._trackingStatusORAA);

        _html += "<div id='cp-trans-break-contract-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-trans-break-contract") +
                 "      <div class='content-data-tabs' id='tabs-cp-trans-break-contract'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-trans-break-contract' alt='#tab1-cp-trans-break-contract' href='javascript:void(0)'>ตรวจสอบรายการแจ้ง</a></li>" +
                 "                  <li><a id='link-tab2-cp-trans-break-contract' alt='#tab2-cp-trans-break-contract' href='javascript:void(0)'>เพิ่มรายการแจ้ง</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-trans-break-contract' alt='#tab3-cp-trans-break-contract' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-trans-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-trans-break-contract' value=''>" +
                 "                  <input type='hidden' id='trackingstatus-trans-break-contract-hidden' value='6'>" +
                 "                  <input type='hidden' id='trackingstatus-trans-break-contract-text-hidden' value='" + _trackingStatus.GetValue(5, 0) + "'>" +
                 "                  <input type='hidden' id='id-name-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='program-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-start-trans-break-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-end-trans-break-contract-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcptransbreakcontract',true,'','','')>ค้นหา</a></li>" +
                 "                          <li><a id='button-send' href='javascript:void(0)' onclick='ConfirmSendBreakContract()'>ส่ง</a></li>" +
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
                 "      <div class='tab-content' id='tab2-cp-trans-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-trans-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-trans-break-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='tab2-table-head-cp-trans-break-contract-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col2'><div class='table-head-line1'>ส่ง<div><input class='checkbox' type='checkbox' name='check-uncheck-all' id='check-uncheck-all' onclick=CheckUncheckAll('check-uncheck-all','send-break-contract') /></div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col3'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col4'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col5'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col6'><div class='table-head-line1'>ทำรายการแจ้ง</div><div>เมื่อ</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col7'><div class='table-head-line1'>ส่งรายการแจ้ง</div><div>เมื่อ</div></li>" +
                 "                  <li class='table-col' id='tab2-table-head-cp-trans-break-contract-col8'><div class='table-head-line1'>สถานะรายการแจ้ง</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailtrackingstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-break-contract-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-trans-break-contract-contents'></div>" +
                 "</div>" +
                 "<div id='cp-trans-break-contract-content'>" +
                 "  <div class='tab-content' id='tab1-cp-trans-break-contract-content'>" +
                 "      <div class='box4' id='list-data-trans-break-contract'></div>" +
                 "      <div id='nav-page-trans-break-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-trans-break-contract-content'>" +
                 "      <div class='box1 addupdate-data-trans-break-contract' id='add-data-trans-break-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-trans-break-contract-content'>" +
                 "      <div class='box1 addupdate-data-trans-break-contract' id='update-data-trans-break-contract'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}
