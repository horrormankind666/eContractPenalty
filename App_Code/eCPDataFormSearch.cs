/*
Description         : สำหรับการแสดงฟอร์มการค้นหา
Date Created        : ๐๙/๐๘/๒๕๕๕
Last Date Modified  : ๐๙/๐๔/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Globalization;
using System.Web;

public class eCPDataFormSearch
{
    public static string SearchCPTabUser()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-cp-tab-user'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-tab-user-keyword1-label'>" +
                 "                  <div class='form-label-style'>ชื่อ</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ชื่อที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-tab-user-keyword1-input'><input class='inputbox' type='text' id='name-tab-user' onblur=Trim('name-tab-user'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPTabUser()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPTabUser('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string ListProfileStudent(string _studentid)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListProfileStudent(_studentid);

        if (_data.GetLength(0) > 0)
        {
            _html = "<list>" +
                    _data[0, 1]  + ";" +
                    _data[0, 2]  + ";" +
                    _data[0, 3]  + ";" +
                    _data[0, 4]  + ";" +
                    _data[0, 5]  + ";" +
                    _data[0, 6]  + ";" +
                    _data[0, 7]  + ";" +
                    _data[0, 8]  + ";" +
                    _data[0, 9]  + ";" +
                    _data[0, 10] + ";" +
                    _data[0, 11] + ";" +
                    _data[0, 12] + ";" +
                    _data[0, 13] + ";" +
                    _data[0, 14] + ";" +
                    _data[0, 15] + ";" +
                    _data[0, 16] + ";" +
                    _data[0, 17] + ";" +
                    _data[0, 18] + ";" +
                    _data[0, 19] + ";" +
                    _data[0, 20] + ";" +
                    _data[0, 21] + ";" +
                    _data[0, 22] + ";" +
                    _data[0, 23] + ";" +
                    ((!String.IsNullOrEmpty(_data[0, 24]) && !String.IsNullOrEmpty(_data[0, 13])) ? "Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _data[0, 25] + "/" + _data[0, 24] + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT : String.Empty) +
                    "<list>";
        }

        return _html;
    }

    public static string ListSearchStudentWithResult(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountStudent(_c);
    
        if (_recordCount > 0)
        {
            _data = eCPDB.ListStudent(_c);
      
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _groupNum = !_data[_i, 12].Equals("0") ? " ( กลุ่ม " + _data[_i, 12] + " )" : "";
                _callFunc = "ViewStudent('" + _data[_i, 1] + ";" + _data[_i, 2] + ";" + _data[_i, 3].Replace(" ", "&nbsp;") + ";" + _data[_i, 4].Replace(" ", "&nbsp;") + ";" + _data[_i, 5].Replace(" ", "&nbsp;") + ";" + _data[_i, 6].Replace(" ", "&nbsp;") + ";" + _data[_i, 7] + ";" + _data[_i, 8].Replace(" ", "&nbsp;") + ";" + _data[_i, 9] + ";" + _data[_i, 10].Replace(" ", "&nbsp;") + ";" + _data[_i, 11] + ";" + _data[_i, 12] + ";" + _data[_i, 13] + ";" + _data[_i, 14] + "')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='student" + _data[_i, 1] + "'>" +
                         "  <li id='tab1-table-content-search-student-with-result-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-search-student-with-result-col2' onclick=" + _callFunc + "><div>" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-search-student-with-result-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-search-student-with-result-col4' onclick=" + _callFunc + "><div><span class='facultycode-col-s'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-search-student-with-result-col5' onclick=" + _callFunc + "><div><span class='programcode-col-s'>" + _data[_i, 9] + "</span>- " + _data[_i, 10] + _groupNum + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "studentwithresult", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }
    
        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string SearchStudentWithResult()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-student-with-result'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-keyword1-input'><input class='inputbox' type='text' id='id-name-search-student' onblur=Trim('id-name-search-student'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-keyword23-label'>" +
                 "                  <div class='form-label-style'>คณะและลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-keyword23-input'>" +
                                    eCPUtil.ListFaculty(true, "facultysearchstudent") +
                 "                  <div id='list-program-search-student'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchStudentWithResult()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmSearchStudentWithResult()'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div id='list-search-student-with-result'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-right'><div class='content-data-tab-content-msg' id='record-count-student-with-result'>ค้นหาพบ 0 รายการ</div></div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='tab1-table-head-search-student-with-result-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-search-student-with-result-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-search-student-with-result-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-search-student-with-result-col4'><div class='table-head-line1'>คณะ</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-search-student-with-result-col5'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "      <div>" +
                 "          <div id='box-list-data-search-student-with-result'><div id='list-data-search-student-with-result'></div></div>" +
                 "          <div id='nav-page-search-student-with-result'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPTransBreakContract()
    {
        string _html = String.Empty;
        int _section;
        int _i;
        
        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        _html += "<div class='form-content' id='search-cp-trans-break-contract'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-break-contract-keyword1-label'>" +
                 "                  <div class='form-label-style'>สถานะรายการแจ้ง</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกสถานะรายการแจ้งที่ต้องการ</div>" +
                 "                      <div class='form-discription-line2-style'>ค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-break-contract-keyword1-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='trackingstatus-trans-break-contract'>" +
                 "                          <option value='0'>เลือกสถานะรายการแจ้ง</option>";            
                                
        Array _trackingStatus = (_section.Equals(1) ? eCPUtil._trackingStatusORLA : eCPUtil._trackingStatusORAA);

        for (_i = 0; _i < _trackingStatus.GetLength(0); _i++)
        {
            _html += "                      <option value='" + _trackingStatus.GetValue(_i, 1) + "'>" + _trackingStatus.GetValue(_i, 0) + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-break-contract-keyword2-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-break-contract-keyword2-input'><input class='inputbox' type='text' id='id-name-trans-break-contract' onblur=Trim('id-name-trans-break-contract'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-break-contract-keyword34-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-break-contract-keyword34-input'>" +
                                    eCPUtil.ListFaculty(true, "facultytransbreakcontract") +
                 "                  <div id='list-program-trans-break-contract'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-break-contract-keyword5-label'>" +
                 "                  <div class='form-label-style'>ช่วงวันที่" + (_section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้ง</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่" + (_section.Equals(1) ? "ส่ง" : "ทำ") + "รายการแจ้งนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ผิดสัญญาที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-break-contract-keyword5-input'>" +
                 "                  <div class='content-left' id='date-start-trans-break-contract-label'>ระหว่างวันที่</div>" +
                 "                  <div class='content-left' id='date-start-trans-break-contract-input'><input class='inputbox calendar' type='text' id='date-start-trans-break-contract' readonly value='' /></div>" +
                 "                  <div class='content-left' id='date-end-trans-break-contract-label'>ถึงวันที่</div>" +
                 "                  <div class='content-left' id='date-end-trans-break-contract-input'><input class='inputbox calendar' type='text' id='date-end-trans-break-contract' readonly value='' /></div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPTransBreakContract()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPTransBreakContract('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPTransRepayContract()
    {
        string _html = String.Empty;
        int _i;

        _html += "<div class='form-content' id='search-cp-trans-repay-contract'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-repay-contract-keyword1-label'>" +
                 "                  <div class='form-label-style'>สถานะการแจ้งชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกสถานะการแจ้งชำระหนี้ที่ต้องการ</div>" +
                 "                      <div class='form-discription-line2-style'>ค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-repay-contract-keyword1-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='repaystatus-trans-repay-contract'>" +
                 "                          <option value='0'>เลือกสถานะการแจ้งชำระหนี้</option>";

        for (_i = 0; _i < eCPUtil._repayStatusDetail.GetLength(0); _i++)
        {
            _html += "                      <option value='" + (_i + 1) + "'>" + eCPUtil._repayStatusDetail[_i] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-repay-contract-keyword2-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-repay-contract-keyword2-input'><input class='inputbox' type='text' id='id-name-trans-repay-contract' onblur=Trim('id-name-trans-repay-contract'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-repay-contract-keyword34-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-repay-contract-keyword34-input'>" +
                                    eCPUtil.ListFaculty(true, "facultytransrepaycontract") +
                 "                  <div id='list-program-trans-repay-contract'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-repay-contract-keyword5-label'>" +
                 "                  <div class='form-label-style'>ช่วงวันที่รับรายการแจ้ง</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับรายการแจ้งนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ผิดสัญญาที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-repay-contract-keyword5-input'>" +
                 "                  <div class='content-left' id='date-start-trans-repay-contract-label'>ระหว่างวันที่</div>" +
                 "                  <div class='content-left' id='date-start-trans-repay-contract-input'><input class='inputbox calendar' type='text' id='date-start-trans-repay-contract' readonly value='' /></div>" +
                 "                  <div class='content-left' id='date-end-trans-repay-contract-label'>ถึงวันที่</div>" +
                 "                  <div class='content-left' id='date-end-trans-repay-contract-input'><input class='inputbox calendar' type='text' id='date-end-trans-repay-contract' readonly value='' /></div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPTransRepayContract()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPTransRepayContract('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPTransPayment()
    {
        string _html = String.Empty;
        int _i;

        _html += "<div class='form-content' id='search-cp-trans-payment'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-payment-keyword1-label'>" +
                 "                  <div class='form-label-style'>สถานะการชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกสถานะการชำระหนี้ที่ต้องการ</div>" +
                 "                      <div class='form-discription-line2-style'>ค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-payment-keyword1-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='paymentstatus-trans-payment'>" +
                 "                          <option value='0'>เลือกสถานะการชำระหนี้</option>";

        for (_i = 0; _i < eCPUtil._paymentStatus.GetLength(0); _i++)
        {
            _html += "                      <option value='" + (_i + 1) + "'>" + eCPUtil._paymentStatus[_i] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-payment-keyword2-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-payment-keyword2-input'><input class='inputbox' type='text' id='id-name-trans-payment' onblur=Trim('id-name-trans-payment'); value='' style='width:357px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-payment-keyword34-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-payment-keyword34-input'>" +
                                    eCPUtil.ListFaculty(true, "facultytranspayment") +
                 "                  <div id='list-program-trans-payment'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-trans-payment-keyword5-label'>" +
                 "                  <div class='form-label-style'>ช่วงวันที่รับเอกสารการทวงถามตอบกลับครั้งที่ 1</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับเอกสารการทวงถามตอบกลับครั้งที่ 1</div>" +
                 "                      <div class='form-discription-line2-style'>หลังจากที่แจ้งชำระหนี้ให้ผู้ผิดสัญญาหรือผู้ค้ำประกัน</div>" +
                 "                      <div class='form-discription-line2-style'>หรือผู้ได้รับมอบหมายทราบ</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-trans-payment-keyword5-input'>" +
                 "                  <div class='content-left' id='date-start-trans-repay1-reply-label'>ระหว่างวันที่</div>" +
                 "                  <div class='content-left' id='date-start-trans-repay1-reply-input'><input class='inputbox calendar' type='text' id='date-start-trans-repay1-reply' readonly value='' /></div>" +
                 "                  <div class='content-left' id='date-end-trans-repay1-reply-label'>ถึงวันที่</div>" +
                 "                  <div class='content-left' id='date-end-trans-repay1-reply-input'><input class='inputbox calendar' type='text' id='date-end-trans-repay1-reply' readonly value='' /></div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPTransPayment()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPTransPayment('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportTableCalCapitalAndInterest()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-cp-report-table-cal-capital-and-interest'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-table-cal-capital-and-interest-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-table-cal-capital-and-interest-keyword1-input'><input class='inputbox' type='text' id='id-name-report-table-cal-capital-and-interest' onblur=Trim('id-name-report-table-cal-capital-and-interest'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-table-cal-capital-and-interest-keyword23-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-table-cal-capital-and-interest-keyword23-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreporttablecalcapitalandinterest") +
                 "                  <div id='list-program-report-table-cal-capital-and-interest'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportTableCalCapitalAndInterest()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportTableCalCapitalAndInterest('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportStepOfWork()
    {        
        string _html = String.Empty;
        int _i;

        _html += "<div class='form-content' id='search-cp-report-step-of-work'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-step-of-work-keyword1-label'>" +
                 "                  <div class='form-label-style'>สถานะขั้นตอนการดำเนินงาน</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกสถานะขั้นตอนการดำเนินงาน</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-step-of-work-keyword1-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='stepofworkstatus-report-step-of-work'>" +
                 "                          <option value='0'>เลือกสถานะขั้นตอนการดำเนินงาน</option>";

        for (_i = 0; _i < eCPUtil._stepOfWorkStatus.GetLength(0); _i++)
        {
            _html += "                      <option value='" + eCPUtil._stepOfWorkStatus.GetValue(_i, 1) + "'>" + eCPUtil._stepOfWorkStatus.GetValue(_i, 0) + "</option>";
        }
  
        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-step-of-work-keyword2-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-step-of-work-keyword2-input'><input class='inputbox' type='text' id='id-name-report-step-of-work' onblur=Trim('id-name-report-step-of-work'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-step-of-work-keyword34-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-step-of-work-keyword34-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreportstepofwork") +
                 "                  <div id='list-program-report-step-of-work'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportStepOfWork()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportStepOfWork('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportNoticeRepayComplete()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-cp-report-notice-repay-complete'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-notice-repay-complete-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-notice-repay-complete-keyword1-input'><input class='inputbox' type='text' id='id-name-report-notice-repay-complete' onblur=Trim('id-name-report-notice-repay-complete'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-notice-repay-complete-keyword23-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-notice-repay-complete-keyword23-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreportnoticerepaycomplete") +
                 "                  <div id='list-program-report-notice-repay-complete'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportNoticeRepayComplete()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportNoticeRepayComplete('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportNoticeClaimDebt()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-cp-report-notice-claim-debt'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-notice-claim-debt-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-notice-claim-debt-keyword1-input'><input class='inputbox' type='text' id='id-name-report-notice-claim-debt' onblur=Trim('id-name-report-notice-claim-debt'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-notice-claim-debt-keyword23-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-notice-claim-debt-keyword23-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreportnoticeclaimdebt") +
                 "                  <div id='list-program-report-notice-claim-debt'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportNoticeClaimDebt()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportNoticeClaimDebt('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportStatisticPaymentByDate()
    {
        string _html = String.Empty;
        int _i;
        
        _html += "<div class='form-content' id='search-cp-report-statistic-payment-by-date'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-statistic-payment-by-date-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword1-input'><input class='inputbox' type='text' id='id-name-report-statistic-payment-by-date' onblur=Trim('id-name-report-statistic-payment-by-date'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-statistic-payment-by-date-keyword23-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword23-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreportstatisticpaymentbydate") +
                 "                  <div id='list-program-report-statistic-payment-by-date'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-statistic-payment-by-date-keyword4-label'>" +
                 "                  <div class='form-label-style'>รูปแบบการชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกรูปการชำระหนี้ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword4-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='formatpaymentreportstatisticpaymentbydate'>" +
                 "                          <option value='0'>เลือกรูปแบบการชำระหนี้</option>";
                        
        for (_i = 0; _i < eCPUtil._paymentFormat.GetLength(0); _i++)
        {
            _html += "                      <option value='" + (_i + 1) + ";" + eCPUtil._paymentFormat[_i] + "'>" + eCPUtil._paymentFormat[_i] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-statistic-payment-by-date-keyword5-label'>" +
                 "                  <div class='form-label-style'>ช่วงวันที่ชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่ชำระหนี้ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-statistic-payment-by-date-keyword5-input'>" +
                 "                  <div class='content-left' id='date-start-report-statistic-payment-by-date-label'>ระหว่างวันที่</div>" +
                 "                  <div class='content-left' id='date-start-report-statistic-payment-by-date-input'><input class='inputbox calendar' type='text' id='date-start-report-statistic-payment-by-date' readonly value='' /></div>" +
                 "                  <div class='content-left' id='date-end-report-statistic-payment-by-date-label'>ถึงวันที่</div>" +
                 "                  <div class='content-left' id='date-end-report-statistic-payment-by-date-input'><input class='inputbox calendar' type='text' id='date-end-report-statistic-payment-by-date' readonly value='' /></div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportStatisticPaymentByDate()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportStatisticPaymentByDate('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportEContract()
    {        
        string _html = String.Empty;
        int _i;

        _html += "<div class='form-content' id='search-cp-report-e-contract'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-e-contract-keyword1-label'>" +
                 "                  <div class='form-label-style'>ปีการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกปีการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-e-contract-keyword1-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='acadamicyear-report-e-contract'>" +
                 "                          <option value='0'>เลือกปีการศึกษา</option>";

        for (_i = 2550; _i <= int.Parse(DateTime.Parse(Util.CurrentDate("MM/dd/yyyy")).ToString("yyyy", new CultureInfo("th-TH"))); _i++)
        {
            _html += "                      <option value='" + _i + "'>" + _i + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-e-contract-keyword2-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-e-contract-keyword2-input'><input class='inputbox' type='text' id='id-name-report-e-contract' onblur=Trim('id-name-report-e-contract'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-e-contract-keyword34-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตร</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-e-contract-keyword34-input'>" +
                                    eCPUtil.ListFaculty(true, "facultyreportecontract") +
                 "                  <div id='list-program-report-e-contract'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportEContract()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportEContract('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string SearchCPReportDebtorContract()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='search-cp-report-debtor-contract'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-cp-report-debtor-contract-keyword1-label'>" +
                 "                  <div class='form-label-style'>ช่วงวันที่<span class='report-debtor-contract-order1'>รับสภาพหนี้</span><span class='report-debtor-contract-order2'>ชำระหนี้</span></div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ช่วงวันที่รับสภาพหนี้ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-cp-report-debtor-contract-keyword1-input'>" +
                 "                  <div class='content-left' id='date-start-report-debtor-contract-label'>ระหว่างวันที่</div>" +
                 "                  <div class='content-left' id='date-start-report-debtor-contract-input'><input class='inputbox calendar' type='text' id='date-start-report-debtor-contract' readonly value='' /></div>" +
                 "                  <div class='content-left' id='date-end-report-debtor-contract-label'>ถึงวันที่</div>" +
                 "                  <div class='content-left' id='date-end-report-debtor-contract-input'><input class='inputbox calendar' type='text' id='date-end-report-debtor-contract' readonly value='' /></div>" +
                 "              </div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchCPReportDebtorContract()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchCPReportDebtorContract('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }    
    
    public static string SearchStudentDebtorContractByProgram()
    {        
        string _html = String.Empty;
        int _i;

        _html += "<div class='form-content' id='search-student-debtor-contract-by-program'>" +
                 "  <div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-debtor-contract-by-program-faculty-label'>" +
                 "                  <div class='form-label-style'>คณะ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-debtor-contract-by-program-faculty-input'><span id='student-debtor-contract-by-program-faculty'>&nbsp;</span></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-debtor-contract-by-program-program-label'>" +
                 "                  <div class='form-label-style'>หลักสูตร</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-debtor-contract-by-program-program-input'><span id='student-debtor-contract-by-program-program'>&nbsp;</span></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-debtor-contract-by-program-keyword1-label'>" +
                 "                  <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                      <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-debtor-contract-by-program-keyword1-input'><input class='inputbox' type='text' id='id-name-report-debtor-contract-by-program' onblur=Trim('id-name-report-debtor-contract-by-program'); value='' style='width:411px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='search-student-debtor-contract-by-program-keyword2-label'>" +
                 "                  <div class='form-label-style'>รูปแบบการชำระหนี้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกรูปการชำระหนี้ที่ต้องการค้นหา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='search-student-debtor-contract-by-program-keyword2-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='formatpaymentreportdebtorcontractbyprogram'>" +
                 "                          <option value='0'>เลือกรูปแบบการชำระหนี้</option>";
                        
        for (_i = 0; _i < eCPUtil._paymentFormat.GetLength(0); _i++)
        {
            _html += "                      <option value='" + (_i + 1) + ";" + eCPUtil._paymentFormat[_i] + "'>" + eCPUtil._paymentFormat[_i] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchStudentDebtorContractByProgram()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=ResetFrmSearchStudentDebtorContractByProgram('clear')>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}