/*
Description         : สำหรับการแสดงรายงาน
Date Created        : ๐๙/๐๘/๒๕๕๕
Last Date Modified  : ๑๘/๐๕/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

public class eCPDataReport
{
    public static string ReplaceFacultyToShortProgram(string _faculty)
    {
        _faculty = _faculty.Replace("คณะ", "");

        return _faculty;
    }

    public static string ReplaceProgramToShortProgram(string _program)
    {
        _program = _program.Replace("ประกาศนียบัตร", "");
        _program = _program.Replace("บัณฑิต", "");
        _program = _program.Replace("ศาสตร", "ศาสตร์");

        return _program;
    }
}

public class eCPDataReportStatisticRepay
{
    public static string ListReportStepOfWorkOnStatisticRepayByProgram(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string _iconStatus1 = String.Empty;
        string[] _iconStatus2;
        string _iconStatus3 = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountReportStepOfWorkOnStatisticRepayByProgram(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportStepOfWorkOnStatisticRepayByProgram(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _trackingStatus = _data[_i, 10] + _data[_i, 11] + _data[_i, 12] + _data[_i, 13];
                _iconStatus1 = eCPUtil._iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil._iconTrackingStatus, _trackingStatus) - 1, 1];
                _iconStatus2 = _data[_i, 14].Split(new char[] { ';' });
                _iconStatus3 = eCPUtil._iconPaymentStatus[(!String.IsNullOrEmpty(_data[_i, 15]) ? int.Parse(_data[_i, 15]) - 1 : 0)];
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','" + _data[_i, 16] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-report-step-of-work-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col4' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status1-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus1 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col5' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status2-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus2[1] + "'></li>" +
                         "              <li class='" + _iconStatus2[2] + "'></li>" +
                         "              <li class='" + _iconStatus2[3] + "'></li>" +
                         "              <li class='" + _iconStatus2[4] + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col6' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status3-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus3 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportstepofworkonstatisticrepaybyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListReportStepOfWorkOnStatisticRepayByProgram()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='list-report-step-of-work-on-statistic-repay-by-program'>" +
                 "  <div id='detail-statistic-repay-by-program'>" +
                 "      <div class='content-left' id='detail-statistic-repay-by-program-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ปีการศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>หลักสูตร</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='detail-statistic-repay-by-program-input'>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-repay-by-program-acadamicyear'>&nbsp;</span></div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-repay-by-program-faculty'>&nbsp;</span></div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-repay-by-program-program'>&nbsp;</span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div id='search-step-of-work'>" +
                 "      <div class='form-label-discription-style'>" +
                 "          <div id='search-step-of-work-keyword1-label'>" +
                 "              <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                  <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='form-input-style'>" +
                 "          <div class='form-input-content' id='search-step-of-work-keyword1-input'><input class='inputbox' type='text' id='id-name-search-report-step-of-work' onblur=Trim('id-name-search-report-step-of-work'); value='' style='width:411px' /></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchReportStepOfWorkOnStatisticRepayByProgram()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmSearchReportStepOfWorkOnStatisticRepayByProgram()'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div id='list-report-step-of-work'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'></div>" +
                 "          <div class='content-right'><div class='content-data-tab-content-msg' id='record-count-report-step-of-work'>ค้นหาพบ 0 รายการ</div></div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-report-step-of-work-col1'><div class='table-head-line1'>ลำดับที่</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col4'><div class='table-head-line1'>สถานะรายการแจ้ง</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailtrackingstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col5'><div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailrepaystatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col6'><div class='table-head-line1'>สถานะการชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailpaymentstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "      <div>" +
                 "          <div id='box-list-data-search-report-step-of-work'><div id='list-data-search-report-step-of-work'></div></div>" +
                 "          <div id='nav-page-search-report-step-of-work'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string ListCPReportStatisticRepayByProgram(string _acadamicyear)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _data = eCPDB.ListCPReportStatisticRepayByProgram(_acadamicyear);
        _recordCount = _data.GetLength(0);
        
        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _groupNum = !_data[_i, 7].Equals("0") ? " ( กลุ่ม " + _data[_i, 7] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewReportStepOfWorkOnStatisticRepayByProgram('" + _data[_i, 0] + "','" + _data[_i, 2] + "','" + _data[_i, 3].Replace(" ", "&") + "','" + _data[_i, 4] + "','" + _data[_i, 5].Replace(" ", "&") + "','" + _data[_i, 6] + "','" + _data[_i, 7] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-statistic-repay-by-program" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-report-statistic-repay-by-program-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col2' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 4] + "</span>- " + _data[_i, 5] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col3' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 8]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 9]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 10]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 11]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col7' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 12]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col8' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 13]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-by-program-col9' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 14]).ToString("#,##0.00") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav><pagenav>";
    }

    public static string ListCPReportStatisticRepay(string[,] _data)
    {
        string _html = String.Empty;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _groupNum = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewReportStatisticRepayByProgram('" + _data[_i, 1] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-statistic-repay" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-statistic-repay-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col2' onclick=" + _callFunc + "><div>25" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col3' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 2]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 3]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 4]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 5]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col7' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 6]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col8' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 7]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col9' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 8]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-repay-col10' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 9]).ToString("#,##0.00") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListUpdateCPReportStatisticRepay()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPReportStatisticRepay();
        _recordCount = _data.GetLength(0);
        _html += ListCPReportStatisticRepay(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPReportStatisticRepay()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-statistic-repay-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-statistic-repay") +
                 "      <div class='content-data-tabs' id='tabs-cp-report-statistic-repay'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-report-statistic-repay' alt='#tab1-cp-report-statistic-repay' href='javascript:void(0)'>สถิติการผิดสัญญาและการชำระหนี้</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab2-cp-report-statistic-repay' alt='#tab2-cp-report-statistic-repay' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-report-statistic-repay-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'></div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-repay'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-report-statistic-repay-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='acadamicyear-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-report-step-of-work-on-statistic-repay-by-program-hidden' value=''>" +
                 "                  <input type='hidden' id='program-report-step-of-work-on-statistic-repay-by-program-hidden' value=''>" +
                 "              </div>" +
                 "              <div class='content-right'>" + 
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-repay-by-program'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-report-statistic-repay-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-statistic-repay-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col2'><div class='table-head-line1'>ปี</div><div>การศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col3'><div class='table-head-line1'>จำนวน</div><div>หลักสูตรที่มี</div><div>ผู้ผิดสัญญา</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col4'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col5'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่ยัง</div><div>ไม่ชำระหนี้</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col6'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่</div><div>ชำระหนี้ครบ</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col7'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่ยัง</div><div>ชำระหนี้ไม่ครบ</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col8'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col9'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-col10'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-statistic-repay-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-statistic-repay-by-program-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col2'><div class='table-head-line1'>หลักสูตรที่มีผู้ผิดสัญญา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col3'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col4'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่ยัง</div><div>ไม่ชำระหนี้</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col5'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่</div><div>ชำระหนี้ครบ</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col6'><div class='table-head-line1'>จำนวน</div><div>ผู้ผิดสัญญาที่ยัง</div><div>ชำระหนี้ไม่ครบ</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col7'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col8'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-repay-by-program-col9'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-statistic-repay-content'>" +
                 "  <div class='tab-content' id='tab1-cp-report-statistic-repay-content'>" +
                 "      <div class='box4' id='list-data-report-statistic-repay'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-statistic-repay-content'>" +
                 "      <div class='box4' id='list-data-report-statistic-repay-by-program'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportStatisticContract
{
    private static string ListReportStudentSignContract(HttpContext _c)
    {
        string _html = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountReportStudentSignContract(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportStudentSignContract(_c);
            
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                //_callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','" + _data[_i, 16] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-student-sign-contract" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-report-student-sign-contract-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-student-sign-contract-col2' onclick=" + _callFunc + "><div>" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-student-sign-contract-col3' onclick=" + _callFunc + "><div>" + _data[_i, 2] + _data[_i, 3] + " " + _data[_i, 4] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-student-sign-contract-col4' onclick=" + _callFunc + "><div>" + _data[_i, 5] + _data[_i, 6] + " " + _data[_i, 7] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-student-sign-contract-col5' onclick=" + _callFunc + "><div>" + _data[_i, 8] + " " + _data[_i, 9] + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportstudentonstatisticcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    private static string ListReportStudentContractPenalty(HttpContext _c)
    {
        string _html = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string _iconStatus1 = String.Empty;
        string[] _iconStatus2;
        string _iconStatus3 = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountReportStepOfWorkOnStatisticRepayByProgram(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportStepOfWorkOnStatisticRepayByProgram(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _trackingStatus = _data[_i, 10] + _data[_i, 11] + _data[_i, 12] + _data[_i, 13];
                _iconStatus1 = eCPUtil._iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil._iconTrackingStatus, _trackingStatus) - 1, 1];
                _iconStatus2 = _data[_i, 14].Split(new char[] { ';' });
                _iconStatus3 = eCPUtil._iconPaymentStatus[(!String.IsNullOrEmpty(_data[_i, 15]) ? int.Parse(_data[_i, 15]) - 1 : 0)];
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','" + _data[_i, 16] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-report-step-of-work-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col4' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status1-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus1 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col5' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status2-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus2[1] + "'></li>" +
                         "              <li class='" + _iconStatus2[2] + "'></li>" +
                         "              <li class='" + _iconStatus2[3] + "'></li>" +
                         "              <li class='" + _iconStatus2[4] + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-report-step-of-work-col6' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status3-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus3 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportstudentonstatisticcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListReportStudentOnStatisticContractByProgram(HttpContext _c)
    {
        string _result = String.Empty;
        int _searchTab = int.Parse(_c.Request["searchtab"]);

        if (_searchTab.Equals(1)) _result = ListReportStudentSignContract(_c);
        if (_searchTab.Equals(2)) _result = ListReportStudentContractPenalty(_c);

        return _result;
    }

    public static string ListReportStudentOnStatisticContractByProgram()
    {
        string _html = String.Empty;

        _html += "<div class='form-content' id='list-report-student-on-statistic-contract-by-program-head'>" +
                 "  <div id='detail-statistic-contract-by-program'>" +
                 "      <div class='content-left' id='detail-statistic-contract-by-program-label'>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>ปีการศึกษา</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                 "          <div class='form-label-discription-style'><div class='form-label-style'>หลักสูตร</div></div>" +
                 "      </div>" +
                 "      <div class='content-left' id='detail-statistic-contract-by-program-input'>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-contract-by-program-acadamicyear'>&nbsp;</span></div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-contract-by-program-faculty'>&nbsp;</span></div></div>" +
                 "          <div class='form-input-style'><div class='form-input-content'><span id='statistic-contract-by-program-program'>&nbsp;</span></div></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div id='search-student-contract'>" +
                 "      <div class='form-label-discription-style'>" +
                 "          <div id='search-student-contract-keyword1-label'>" +
                 "              <div class='form-label-style'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='form-discription-style'>" +
                 "                  <div class='form-discription-line1-style'>กรุณาใส่รหัสหรือชื่อ - นามสกุลของนักศึกษา</div>" +
                 "                  <div class='form-discription-line2-style'>ที่ต้องการค้นหา</div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='form-input-style'>" +
                 "          <div class='form-input-content' id='search-student-contract-keyword1-input'><input class='inputbox' type='text' id='id-name-search-report-student-contract' onblur=Trim('id-name-search-report-student-contract'); value='' style='width:411px' /></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='clear'></div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick='ValidateSearchReportStudentOnStatisticContractByProgram()'>ค้นหา</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmSearchReportStudentOnStatisticContractByProgram()'>ล้าง</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-head'>" +
                 "      <div class='content-data-tabs content-data-subtabs' id='tabs-report-student-on-statistic-contract-by-program'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li class='first-tab'><a class='active' id='link-tab1-report-student-on-statistic-contract-by-program' alt='#tab1-report-student-on-statistic-contract-by-program' href='javascript:void(0)'>นักศึกษาที่ทำสัญญาเสร็จสมบูรณ์ผ่านระบบ e-Contract</a></li>" +
                 "                  <li id='tab2-report-student-on-statistic-contract-by-program'><a id='link-tab2-report-student-on-statistic-contract-by-program' alt='#tab2-report-student-on-statistic-contract-by-program' href='javascript:void(0)'>นักศึกษาที่ผิดสัญญา</a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div>" +
                 "      <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'></div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-student-sign-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'></div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-student-contract-penalty'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-report-student-sign-contract-col1'><div class='table-head-line1'>ลำดับที่</div></li>" +
                 "                  <li class='table-col' id='table-head-report-student-sign-contract-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-report-student-sign-contract-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-report-student-sign-contract-col4'><div class='table-head-line1'>ผู้ค้ำประกัน</div></li>" +
                 "                  <li class='table-col' id='table-head-report-student-sign-contract-col5'><div class='table-head-line1'>วันที่ทำสัญญา</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-report-step-of-work-col1'><div class='table-head-line1'>ลำดับที่</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col4'><div class='table-head-line1'>สถานะรายการแจ้ง</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailtrackingstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col5'><div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailrepaystatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "                  <li class='table-col' id='table-head-report-step-of-work-col6'><div class='table-head-line1'>สถานะการชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(2,'detailpaymentstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='list-report-student-on-statistic-contract-by-program-content'>" +
                 "  <div class='subtab-content' id='tab1-report-student-on-statistic-contract-by-program-content'>" +
                 "      <div class='box4' id='box-list-data-student-sign-contract'><div id='list-data-student-sign-contract'></div></div>" +
                 "      <div id='nav-page-student-sign-contract'></div>" +
                 "  </div>" +
                 "  <div class='subtab-content' id='tab2-report-student-on-statistic-contract-by-program-content'>" +
                 "      <div class='box4' id='box-list-data-student-contract-penalty'><div id='list-data-student-contract-penalty'></div></div>" +
                 "      <div id='nav-page-student-contract-penalty'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string ListCPReportStatisticContractByProgram(string _acadamicyear)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _data = eCPDB.ListCPReportStatisticContractByProgram(_acadamicyear);
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _groupNum = !_data[_i, 7].Equals("0") ? " ( กลุ่ม " + _data[_i, 7] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewReportStudentOnStatisticContractContractByProgram('" + _data[_i, 0] + "','" + _data[_i, 2] + "','" + _data[_i, 3].Replace(" ", "&") + "','" + _data[_i, 4] + "','" + _data[_i, 5].Replace(" ", "&") + "','" + _data[_i, 6] + "','" + _data[_i, 7] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-statistic-contract-by-program" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-report-statistic-contract-by-program-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col2' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 4] + "</span>- " + _data[_i, 5] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col3' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 8]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 9]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-by-program-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 10]).ToString("#,##0") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav><pagenav>";
    }

    public static string ListCPReportStatisticContract(string[,] _data)
    {
        string _html = String.Empty;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _groupNum = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewReportStatisticContractByProgram('" + _data[_i, 1] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-statistic-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-statistic-contract-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-col2' onclick=" + _callFunc + "><div>25" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-col3' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 2]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 3]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-contract-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 4]).ToString("#,##0") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListUpdateCPReportStatisticContract()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPReportStatisticContract();
        _recordCount = _data.GetLength(0);
        _html += ListCPReportStatisticContract(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPReportStatisticContract()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-statistic-contract-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-statistic-contract") +
                 "      <div class='content-data-tabs' id='tabs-cp-report-statistic-contract'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-report-statistic-contract' alt='#tab1-cp-report-statistic-contract' href='javascript:void(0)'>สถิติการทำสัญญาและการผิดสัญญา</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab2-cp-report-statistic-contract' alt='#tab2-cp-report-statistic-contract' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-report-statistic-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'></div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-report-statistic-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='acadamicyear-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-report-student-on-statistic-contract-by-program-hidden' value=''>" +
                 "                  <input type='hidden' id='program-report-student-on-statistic-contract-by-program-hidden' value=''>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-contract-by-program'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-report-statistic-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-statistic-contract-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-col2'><div class='table-head-line1'>ปีการศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-col3'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ต้องทำสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-col4'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ทำสัญญาเสร็จสมบูรณ์</div><div>ผ่านระบบ e-Contract</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-col5'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ผิดสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-statistic-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-statistic-contract-by-program-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col2'><div class='table-head-line1'>หลักสูตรที่ให้มีการทำสัญญา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col3'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ต้องทำสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col4'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ทำสัญญาเสร็จสมบูรณ์</div><div>ผ่านระบบ e-Contract</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-statistic-contract-by-program-col5'><div class='table-head-line1'>จำนวนนักศึกษา</div><div>ที่ผิดสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-statistic-contract-content'>" +
                 "  <div class='tab-content' id='tab1-cp-report-statistic-contract-content'>" +
                 "      <div class='box4'><div id='list-data-report-statistic-contract'></div></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-statistic-contract-content'>" +
                 "      <div class='box4' id='list-data-report-statistic-contract-by-program'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportStepOfWork
{
    public static string ListCPReportStepOfWork(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        string _iconStatus1 = String.Empty;
        string[] _iconStatus2;
        string _iconStatus3 = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountCPReportStepOfWork(_c);

        if (_recordCount > 0)
        {            
            _data = eCPDB.ListCPReportStepOfWork(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {                                                               
                _trackingStatus = _data[_i, 10] + _data[_i, 11] + _data[_i, 12] + _data[_i, 13];                
                _iconStatus1 = eCPUtil._iconTrackingStatus[Util.FindIndexArray2D(0, eCPUtil._iconTrackingStatus, _trackingStatus) - 1, 1];
                _iconStatus2 = _data[_i, 14].Split(new char[] { ';' });
                _iconStatus3 = eCPUtil._iconPaymentStatus[(!String.IsNullOrEmpty(_data[_i, 15]) ? int.Parse(_data[_i, 15]) - 1 : 0)];
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','" + _data[_i, 16] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-step-of-work-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col5' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status1-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus1 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col6' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status2-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus2[1] + "'></li>" +
                         "              <li class='" + _iconStatus2[2] + "'></li>" +
                         "              <li class='" + _iconStatus2[3] + "'></li>" +
                         "              <li class='" + _iconStatus2[4] + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-cp-report-step-of-work-col7' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status3-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus3 + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportstepofwork", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportStepOfWork()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-step-of-work-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-step-of-work") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'>" +
                 "              <input type='hidden' id='search-report-step-of-work' value=''>" +
                 "              <input type='hidden' id='stepofworkstatus-report-step-of-work-hidden' value=''>" +
                 "              <input type='hidden' id='stepofworkstatus-report-step-of-work-text-hidden' value=''>" +
                 "              <input type='hidden' id='id-name-report-step-of-work-hidden' value=''>" +
                 "              <input type='hidden' id='faculty-report-step-of-work-hidden' value=''>" +
                 "              <input type='hidden' id='program-report-step-of-work-hidden' value=''>" +
                 "              <div class='button-style2'>" +
                 "                  <ul>" +
                 "                      <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportstepofwork',true,'','','')>ค้นหา</a></li>" +
                 "                  </ul>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='content-right'>" +
                 "              <div class='content-data-tab-content-msg' id='record-count-cp-report-step-of-work'>ค้นหาพบ 0 รายการ</div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box-search-condition' id='search-report-step-of-work-condition'>" +
                 "          <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "          <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order1'>" +
                 "              <div class='box-search-condition-order-title'>สถานะขั้นตอนการดำเนินงาน</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order1-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order2'>" +
                 "              <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order2-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order3'>" +
                 "              <div class='box-search-condition-order-title'>คณะ</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order3-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-step-of-work-condition-order' id='search-report-step-of-work-condition-order4'>" +
                 "              <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-step-of-work-condition-order4-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-report-step-of-work-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col5'><div class='table-head-line1'>สถานะรายการแจ้ง</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailtrackingstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col6'><div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-step-of-work-col7'><div class='table-head-line1'>สถานะการชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailpaymentstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-step-of-work-content'>" +
                 "  <div class='box4' id='list-data-report-step-of-work'></div>" +
                 "  <div id='nav-page-report-step-of-work'></div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportTableCalCapitalAndInterest
{
    public static void ExportCPReportTableCalCapitalAndInterest(string _exportSend)
    {
        char[] _separator = new char[] { '&', '.' };
        string[] _send = _exportSend.Split(_separator[0]);
        string _cp2id = _send[0];
        string[] _capital = new string[2];
        string[] _capitalSplitDec = new string[2];
        string[] _interest = new string[2];
        string[] _interestSplitDec = new string[2];
        string[] _pay = new string[2];
        string[] _paySplitDec = new string[2];
        string _paymentDate = _send[4];
        string[,] _data;
        string[,] _data1;
        int _i = 1, _j = 1;
        int _startRow = 1, _maxRow = 28, _row = 1;
        int[] _page = new int[2];
        float _topCell = 0;
        float _topText = 0;
        float _borderBottom = 0;

        _capital[0] = _send[1];
        _capitalSplitDec = _capital[0].Split(_separator[1]);
        _interest[0] = _send[2];
        _interestSplitDec = _interest[0].Split(_separator[1]);
        _pay[0] = _send[3];
        _paySplitDec = _pay[0].Split(_separator[1]);
        
        _capital[1] = double.Parse(_capitalSplitDec[0]).ToString("#,##0") + (_capitalSplitDec[1].Equals("00") ? ".-" : "." + ((_capitalSplitDec[1].Substring(_capitalSplitDec[1].Length - 1, 1)).Equals("0") ? _capitalSplitDec[1].Substring(0, 1) : _capitalSplitDec[1]));
        _interest[1] = double.Parse(_interestSplitDec[0]).ToString("#,##0") + (_interestSplitDec[1].Equals("00") ? "" : "." + ((_interestSplitDec[1].Substring(_interestSplitDec[1].Length - 1, 1)).Equals("0") ? _interestSplitDec[1].Substring(0, 1) : _interestSplitDec[1]));
        _pay[1] = double.Parse(_paySplitDec[0]).ToString("#,##0") + (_paySplitDec[1].Equals("00") ? ".-" : "." + ((_paySplitDec[1].Substring(_paySplitDec[1].Length - 1, 1)).Equals("0") ? _paySplitDec[1].Substring(0, 1) : _paySplitDec[1]));
        
        _data = eCPDB.ListDetailCPReportTableCalCapitalAndInterest(_cp2id);

        string _studentIDDefault = _data[0, 3];
        string _titleNameDefault = _data[0, 4];
        string _firstNameDefault = _data[0, 5];
        string _lastNameDefault = _data[0, 6];
        string _programNameDefault = _data[0, 8];
        string _groupNumDefault = _data[0, 12];

        _data1 = eCPDB.ListCalCPReportTableCalCapitalAndInterest(_capital[0], _interest[0], _pay[0], _paymentDate);
        _page = PageNavigate.CalPage(_data1.GetLength(0) - 1, 1, _maxRow);

        string _pdfFont = "Font/THSarabun.ttf";
        string _template = "ExportTemplate/Blank.pdf";
        string _saveFile = "TableCalCapitalAndInterest" + _studentIDDefault + ".pdf";

        ExportToPdf _exportToPdf = new ExportToPdf();
        _exportToPdf.ExportToPdfConnect(_template, "pdf", _saveFile);

        for (_i = 1; _i <= _page[1]; _i++)
        {
            if (_i > 1)
                _exportToPdf.PDFNewPage();
            
            _exportToPdf.FillForm(_pdfFont, 14, 1, "ตารางคำนวณเงินต้นและดอกเบี้ย", 50, 810, 500, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "ผู้ชดใช้เงินกรณีผิดสัญญาการศึกษา" + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : ""), 50, 792, 500, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, _titleNameDefault + _firstNameDefault + " " + _lastNameDefault, 50, 774, 500, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "จากเงินต้น " + _capital[1] + " บาท อัตราดอกเบี้ยร้อยละ " + _interest[1] + " ต่อปี", 50, 756, 500, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "ชำระเงินต้นเดือนละ " + _pay[1] + " บาททุกวันที่ " + double.Parse(_paymentDate.Substring(0, 2)).ToString("#0") + " ของเดือน", 50, 738, 500, 0);

            _exportToPdf.CreateTable(50, 702, 50, 42, 1, 1, 1, 1);
            _exportToPdf.CreateTable(100, 702, 80, 42, 0, 1, 1, 1);
            _exportToPdf.CreateTable(180, 702, 80, 42, 0, 1, 1, 1);
            _exportToPdf.CreateTable(260, 702, 80, 42, 0, 1, 1, 1);
            _exportToPdf.CreateTable(340, 702, 80, 42, 0, 1, 1, 1);
            _exportToPdf.CreateTable(420, 702, 65, 42, 0, 1, 1, 1);
            _exportToPdf.CreateTable(485, 702, 65, 42, 0, 1, 1, 1);

            _exportToPdf.FillForm(_pdfFont, 14, 1, "งวดที่ชำระ", 50, 702, 50, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "เงินต้น", 100, 702, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "คงเหลือ", 100, 684, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "รับชำระ", 180, 702, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "เงินต้น", 180, 684, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "จำนวนเงิน", 260, 702, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "ดอกเบี้ยรับ", 260, 684, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "รวมจำนวนเงิน", 340, 702, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "รับชำระ", 340, 684, 80, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "กำหนดชำระ", 420, 702, 65, 0);
            _exportToPdf.FillForm(_pdfFont, 14, 1, "วันที่รับชำระ", 485, 702, 65, 0);

            _topCell = 660;
            _topText = 662;

            for (_j = _startRow; (_j <= (_data1.GetLength(0) - 1) && _row <= _maxRow); _j++, _row++)
            {
                _borderBottom = (_j.Equals(_data1.GetLength(0) - 1) || _row.Equals(_maxRow) ? 1 : 0);

                _exportToPdf.CreateTable(50, _topCell, 50, 21, 1, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(100, _topCell, 80, 21, 0, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(180, _topCell, 80, 21, 0, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(260, _topCell, 80, 21, 0, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(340, _topCell, 80, 21, 0, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(420, _topCell, 65, 21, 0, 0, 1, _borderBottom);
                _exportToPdf.CreateTable(485, _topCell, 65, 21, 0, 0, 1, _borderBottom);

                _exportToPdf.FillForm(_pdfFont, 14, 1, double.Parse(_data1[_j - 1, 0]).ToString("#,##0"), 50, _topText, 50, 0);
                _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_j - 1, 1]).ToString("#,##0.00"), 100, _topText, 75, 0);
                _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_j - 1, 2]).ToString("#,##0.00"), 180, _topText, 75, 0);
                _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_j - 1, 3]).ToString("#,##0.00"), 260, _topText, 75, 0);
                _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_j - 1, 4]).ToString("#,##0.00"), 340, _topText, 75, 0);
                _exportToPdf.FillForm(_pdfFont, 14, 1, Util.ShortDateTH(_data1[_j - 1, 5]), 420, _topText, 65, 0);

                _topCell = _topCell - 21;
                _topText = _topText - 21;
            }
            
            _startRow = _j;
            _row = 1;

            _exportToPdf.FillForm(_pdfFont, 11, 0, "วันที่ " + Util.ThaiLongDate(Util.CurrentDate("yyyy-MM-dd")), 50, 30, 250, 0);
            _exportToPdf.FillForm(_pdfFont, 11, 2, "หน้า " + _i, 350, 30, 200, 0);
        }

        _exportToPdf.CreateTable(179, _topCell, 81, 21, 1, 0, 1, 1);
        _exportToPdf.CreateTable(260, _topCell, 80, 21, 0, 0, 1, 1);
        _exportToPdf.CreateTable(340, _topCell, 80, 21, 0, 0, 1, 1);

        _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_data1.GetLength(0) - 1, 6]).ToString("#,##0.00"), 179, _topText, 76, 0);
        _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_data1.GetLength(0) - 1, 7]).ToString("#,##0.00"), 260, _topText, 75, 0);
        _exportToPdf.FillForm(_pdfFont, 14, 2, double.Parse(_data1[_data1.GetLength(0) - 1, 8]).ToString("#,##0.00"), 340, _topText, 75, 0);

        _exportToPdf.ExportToPdfDisconnect();
    }
    
    public static string ListTableCalCapitalAndInterest(string[,] _data)
    {
        int _i;
        string _html = String.Empty;
        string _highlight = String.Empty;

        _html += "<div class='table-content'>";

        for (_i = 0; _i < (_data.GetLength(0) - 1); _i++)
        {            
            _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
            _html += "<ul class='table-row-content " + _highlight + "'>" +
                     "  <li id='table-content-report-table-cal-capital-and-interest-col1'><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col2'><div>" + double.Parse(_data[_i, 1]).ToString("###,##0.00") + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col3'><div>" + double.Parse(_data[_i, 2]).ToString("#,##0.00") + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col4'><div>" + double.Parse(_data[_i, 3]).ToString("#,##0.00") + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col5'><div>" + double.Parse(_data[_i, 4]).ToString("#,##0.00") + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col6'><div>" + _data[_i, 5] + "</div></li>" +
                     "  <li class='table-col' id='table-content-report-table-cal-capital-and-interest-col7'><div>666</div></li>" +
                     "</ul>";
        }

        _html += "</div>";

        return _html;
    }

    public static string CalReportTableCalCapitalAndInterest(string _cp2id)
    {
        string _html = String.Empty;
        string[] _contractInterest;
        string[,] _data;
        int _i;

        _data = eCPDB.ListDetailCPReportTableCalCapitalAndInterest(_cp2id);
        if (_data.GetLength(0) > 0)
        {
            string _studentIDDefault = _data[0, 3];
            string _titleNameDefault = _data[0, 4];
            string _firstNameDefault = _data[0, 5];
            string _lastNameDefault = _data[0, 6];
            string _facultyCodeDefault = _data[0, 10];
            string _facultyNameDefault = _data[0, 11];
            string _programCodeDefault = _data[0, 7];
            string _programNameDefault = _data[0, 8];
            string _groupNumDefault = _data[0, 12];
            string _dlevelDefault = _data[0, 14];
            string _pictureFileNameDefault = _data[0, 15];
            string _pictureFolderNameDefault = _data[0, 16];
            string _capital = (!String.IsNullOrEmpty(_data[0, 21]) ? double.Parse(_data[0, 21]).ToString("#,##0.00") : double.Parse(_data[0, 20]).ToString("#,##0.00")); 

            _contractInterest = eCPUtil.GetContractInterest();

            _html += "<div class='form-content' id='cal-report-table-cal-capital-and-interest'>" +
                     "  <input type='hidden' id='cp2id' value='" + _cp2id + "'>" +
                     "  <input type='hidden' id='capital-hidden' value='" + double.Parse(_capital).ToString("#,##0.00") + "' />" +
                     "  <input type='hidden' id='interest-hidden' value='" + double.Parse(_contractInterest[1]).ToString("#,##0.00") + "' />" +
                     "  <input type='hidden' id='capital-old' value='' />" +
                     "  <input type='hidden' id='interest-old' value='' />" +
                     "  <input type='hidden' id='pay-old' value='' />" +
                     "  <input type='hidden' id='period-old' value='' />" +
                     "  <input type='hidden' id='payment-date-old' value='' />" +
                     "  <input type='hidden' id='pay-least-hidden' value='" + eCPUtil.PAY_REPAY_LEAST.ToString("#,##0") + "' />" +
                     "  <input type='hidden' id='period-least-hidden' value='" + eCPUtil.PERIOD_REPAY_LEAST.ToString("#,##0") + "' />" +
                     "  <div id='profile-student'>" +
                     "      <div class='content-left' id='picture-student'><div><img src='Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "' /></div></div>" +
                     "      <div class='content-left' id='profile-student-label'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>หลักสูตร</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='profile-student-input'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _studentIDDefault + "&nbsp;" + _programCodeDefault.Substring(0, 4) + " / " + _programCodeDefault.Substring(4, 1) + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _titleNameDefault + _firstNameDefault + " " + _lastNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _dlevelDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _facultyCodeDefault + " - " + _facultyNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'><span>" + _programCodeDefault + " - " + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : "") + "</span></div></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div class='box3'></div>" +
                     "  <div id='set-condition'>" +
                     "      <div class='form-label-discription-style'>" +
                     "          <div id='set-condition-label'>" +
                     "              <div class='form-label-style'>กำหนดเงื่อนไขการคำนวณเงินต้นและดอกเบี้ย</div>" +
                     "              <div class='form-discription-style'>" +
                     "                  <div class='form-discription-line1-style'>กรุณาใส่เงื่อนไขเพื่อใช้แสดงตารางคำนวณเงินต้นและดอกเบี้ย</div>" +
                     "              </div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='form-input-style'>" +
                     "          <div class='form-input-content' id='set-condition-input'>" +
                     "              <div>" +
                     "                  <div class='content-left' id='capital-label'>จำนวนเงินต้นคงเหลือยกมา</div>" +
                     "                  <div class='content-left' id='capital-input'><input class='inputbox textbox-numeric' type='text' id='capital' onblur=Trim('capital');AddCommas('capital',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                     "                  <div class='content-left' id='capital-unit-label'>บาท</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='interest-label'>อัตราดอกเบี้ยร้อยละ</div>" +
                     "                  <div class='content-left' id='interest-input'><input class='inputbox textbox-numeric' type='text' id='interest' onblur=Trim('interest');AddCommas('interest',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                     "                  <div class='content-left' id='interest-unit-label'>ต่อปี</div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='condition-tablecalcapitalandinterest-label'>เงื่อนไขที่ใช้คำนวณ</div>" +
                     "                  <div class='content-left' id='condition-tablecalcapitalandinterest-input'>" +
                     "                      <div id='condition-select' class='combobox'>" +
                     "                          <select id='condition-tablecalcapitalandinterest'>" +
                     "                              <option value='0'>เลือกเงื่อนไขที่ใช้คำนวณ</option>";

            for (_i = 0; _i < eCPUtil._conditionTableCalCapitalAndInterest.GetLength(0); _i++)
            { 
                _html += "                          <option value='" + eCPUtil._conditionTableCalCapitalAndInterest[_i, 1] + "'>" + eCPUtil._conditionTableCalCapitalAndInterest[_i, 0] + "</option>";
            }

            _html += "                          </select>" +
                     "                      </div>" +
                     "                      <div id='condition-input'>" +
                     "                          <div id='condition-select-0'><input class='inputbox' type='text' id='pay-period' value='' style='width:100px' /></div>" +
                     "                          <div id='condition-select-1'>" +
                     "                              <div class='content-left' id='pay-input'><input class='inputbox textbox-numeric' type='text' id='pay' onblur=Trim('pay');AddCommas('pay',2); onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                     "                              <div class='content-left' id='pay-unit-label'>บาท</div>" +
                     "                          </div>" +
                     "                          <div class='clear'></div>" +
                     "                          <div id='condition-select-2'>" +
                     "                              <div class='content-left' id='period-input'><input class='inputbox textbox-numeric' type='text' id='period' onblur=Trim('period');AddCommas('period',0); onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:100px' /></div>" +
                     "                              <div class='content-left' id='period-unit-label'>งวด</div>" +
                     "                          </div>" +
                     "                          <div class='clear'></div>" +
                     "                      </div>" +
                     "                  </div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "              <div>" +
                     "                  <div class='content-left' id='payment-date-label'>เริ่มชำระตั้งแต่วันที่</div>" +
                     "                  <div class='content-left' id='payment-date-input'><input class='inputbox calendar' type='text' id='payment-date' readonly value='' /></div>" +
                     "              </div>" +
                     "              <div class='clear'></div>" +
                     "          </div>" +
                     "      </div>" +
                     "      <div class='clear'></div>" +
                     "  </div>" +
                     "  <div class='button' id='button-style11'>" +
                     "      <div class='button-style1'>" +
                     "          <ul>" +
                     "              <li><a href='javascript:void(0)' onclick='CalReportTableCalCapitalAndInterest()'>คำนวณ</a></li>" +
                     "              <li><a href='javascript:void(0)' onclick=ResetFrmCalReportTableCalCapitalAndInterest()>ล้าง</a></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div id='list-cp-report-table-cal-capital-and-interest'>" +
                     "      <div class='tab-line'></div>" +
                     "      <div class='content-data-tab-content'>" +
                     "          <div class='content-left'><div class='content-data-tab-content-msg'>ตารางคำนวณเงินต้นและดอกเบี้ย</div></div>" +
                     "          <div class='content-right'><div class='content-data-tab-content-msg' id='record-count-cal-table-cal-capital-and-interest'>ทั้งหมด 0 งวด</div></div>" +
                     "      </div>" +
                     "      <div class='clear'></div>" +
                     "      <div class='tab-line'></div>" +
                     "      <div class='box3'>" +
                     "          <div class='table-head'>" +
                     "              <ul>" +
                     "                  <li id='table-head-report-table-cal-capital-and-interest-col1'><div class='table-head-line1'>งวดที่ชำระ</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col2'><div class='table-head-line1'>เงินต้นคงเหลือ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col3'><div class='table-head-line1'>ชำระเงินต้น<div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col4'><div class='table-head-line1'>ชำระดอกเบี้ยรับ<div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col5'><div class='table-head-line1'>รวมเงินที่รับชำระ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col6'><div class='table-head-line1'>กำหนดชำระ</div></li>" +
                     "                  <li class='table-col' id='table-head-report-table-cal-capital-and-interest-col7'><div class='table-head-line1'>วันที่รับชำระ</div></li>" +
                     "              </ul>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>" +
                     "      <div id='box-list-table-cal-capital-and-interest'><div id='list-table-cal-capital-and-interest'></div></div>" +
                     "      <div id='sumtotal-table-cal-capital-and-interest'>" +
                     "          <div class='table-content'>" +
                     "              <ul class='table-row-content'>" +
                     "                  <li id='table-content-sumtotal-table-cal-capital-and-interest-col1'><div>รวมทั้งสิ้น</div></li>" +
                     "                  <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col2'><div id='sum-pay-capital'></div></li>" +
                     "                  <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col3'><div id='sum-pay-interest'></div></li>" +
                     "                  <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col4'><div id='sum-total-pay'></div></li>" +
                     "                  <li class='table-col' id='table-content-sumtotal-table-cal-capital-and-interest-col5'><div>&nbsp;</div></li>" +
                     "              </ul>" +
                     "          </div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='button' id='button-style12'>" +
                     "      <div class='button-style1'>" +
                     "          <ul>" +
                     "              <li><a href='javascript:void(0)' onclick='ExportReportTableCalCapitalAndInterest()'>พิมพ์</a></li>" +
                     "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'cal-report-table-cal-capital-and-interest')>ปิด</a></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "  </div>" +
                     "</div>" +
                     "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                     "<form id='export-setvalue' method='post' target='export-target'>" +
                     "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                     "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                     "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                     "</form>";                     
        }

        return _html;
    }

    public static string ListCPReportTableCalCapitalAndInterest(HttpContext _c)
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

        _recordCount = eCPDB.CountCPReportTableCalCapitalAndInterest(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListCPReportTableCalCapitalAndInterest(_c);
   
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "OpenTab('link-tab2-cp-report-table-cal-capital-and-interest','#tab2-cp-report-table-cal-capital-and-interest','ตารางคำนวณ',false,'','" + _data[_i, 1] + "','')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-table-cal-capital-and-interest" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-table-cal-capital-and-interest-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 17]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col6' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 18]) ? double.Parse(_data[_i, 18]).ToString("#,##0.00") : "0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-table-cal-capital-and-interest-col7' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 19]) ? double.Parse(_data[_i, 19]).ToString("#,##0.00") : double.Parse(_data[_i, 17]).ToString("#,##0.00")) + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reporttablecalcapitalandinterest", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }
    
    public static string TabCPReportTableCalCapitalAndInterest()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-table-cal-capital-and-interest-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-table-cal-capital-and-interest") +
                 "      <div class='content-data-tabs' id='tabs-cp-report-table-cal-capital-and-interest'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-report-table-cal-capital-and-interest' alt='#tab1-cp-report-table-cal-capital-and-interest' href='javascript:void(0)'>รายการชำระหนี้</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab2-cp-report-table-cal-capital-and-interest' alt='#tab2-cp-report-table-cal-capital-and-interest' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-report-table-cal-capital-and-interest' value=''>" +
                 "                  <input type='hidden' id='id-name-report-table-cal-capital-and-interest-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-report-table-cal-capital-and-interest-hidden' value=''>" +
                 "                  <input type='hidden' id='program-report-table-cal-capital-and-interest-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreporttablecalcapitalandinterest',true,'','','')>ค้นหา</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-table-cal-capital-and-interest'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-report-table-cal-capital-and-interest-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order2'>" +
                 "                  <div class='box-search-condition-order-title'>คณะ</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order2-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-report-table-cal-capital-and-interest-condition-order' id='search-report-table-cal-capital-and-interest-condition-order3'>" +
                 "                  <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-table-cal-capital-and-interest-condition-order3-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-table-cal-capital-and-interest-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col5'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col6'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-table-cal-capital-and-interest-col7'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้คงเหลือ</div><div>( บาท )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-contents'></div>" +
                 "</div>" +
                 "<div id='cp-report-table-cal-capital-and-interest-content'>" +
                 "  <div class='tab-content' id='tab1-cp-report-table-cal-capital-and-interest-content'>" +
                 "      <div class='box4' id='list-data-report-table-cal-capital-and-interest'></div>" +
                 "      <div id='nav-page-report-table-cal-capital-and-interest'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-table-cal-capital-and-interest-content'>" +
                 "      <div class='box1' id='cal-data-report-table-cal-capital-and-interest'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportNoticeRepayComplete
{
    private static string ExportCPReportNoticeRepayCompleteSection(int _section, string _font, string[,] _data)
    {
        string _html = String.Empty;

        if (_section.Equals(1))
        {
            _html += "<tr>" +
                     "  <td width='100%' height='110' align='center'><img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' /></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%' align='right'>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>โทร. ๐ ๒๘๔๙ ๔๕๗๓ โทรสาร ๐ ๒๘๔๙ ๔๕๕๘</div>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <div style='font:normal 15pt " + _font + ";'>ที่&nbsp;&nbsp;&nbsp;ศธ ๐๕๑๗/</div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>วันที่</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรื่อง</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>การชดใช้เงินแทนการปฏิบัติงานชดใช้ทุน</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(2))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;บัดนี้ " + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ซึ่งสำเร็จการศึกษาจาก" + _data[0, 7] + " เมื่อวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 11])) + " " +
                     "          ได้ชดใช้เงินแทนการปฏิบัติงานตามสัญญาฯ ซึ่งมหาวิทยาลัยคิดคำนวณแล้วเป็นเงินทั้งสิ้น " + Util.NumberArabicToThai(double.Parse(_data[0, 14]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 14]) + ") " +
                     "          โดย" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ได้นำเงินดังกล่าวมาชำระให้กับมหาวิทยาลัยมหิดลเรียบร้อยแล้ว" +
                     "      </p>" +
                     "  </td>" +
                     "</tr>";
        }
                    
        if (_section.Equals(3))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดทราบ" +
                     "      </p>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "          <tr>" +
                     "              <td width='200'></td>" +
                     "              <td width='400'>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ขอแสดงความนับถือ</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>(ชื่อ)</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ตำแหน่ง</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ตำแหน่ง</div>" +
                     "              </td>" +
                     "          </tr>" +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        return _html;
    }

    public static void ExportCPReportNoticeRepayComplete(string _exportSend)
    {
        string _html = String.Empty;
        string _width = "600";
        string _font = "TH SarabunPSK, TH Sarabun New";
        char[] _separator = new char[] { ';' };
        string[] _cp1id = _exportSend.Split(_separator);
        string[,] _data;
        int _i;

        for (_i = 1; _i <= _cp1id.Length; _i++)
        {
            _data = eCPDB.ListDetailReportNoticeRepayComplete(_cp1id[_i - 1]);

            _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                     "  <tr>" +
                     "      <td width='" + _width + "' valign='top'>" +
                     "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                    ExportCPReportNoticeRepayCompleteSection(1, _font, _data) +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'>" +
                     "                      <div>" +
                     "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                              <tr>" +
                     "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                     "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>ผู้อำนวยการ" + _data[0, 13] + "</div></td>" +
                     "                              </tr>" +
                     "                          </table>" +
                     "                      </div>" +
                     "                      <div>" +
                     "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                              <tr>" +
                     "                                  <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                     "                                  <td width='550'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือ" + _data[0, 13] + "</div></td>" +
                     "                              </tr>" +
                     "                          </table>" +
                     "                      </div>" +
                     "                      <div>" +
                     "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                              <tr>" +
                     "                                  <td width='98' valign='top'><div style='font:normal 15pt " + _font + ";'>สิ่งที่ส่งมาด้วย</div></td>" +
                     "                                  <td width='502'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาใบเสร็จรับเงิน จำนวน ๑ ฉบับ</div></td>" +
                     "                              </tr>" +
                     "                          </table>" +
                     "                      </div>" +
                     "                  </td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'>" +
                     "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง " + _data[0, 13] + " แจ้งการลาออกจากการปฏิบัติงานในระหว่างปฏิบัติงาน" +
                     "                          ชดใช้ทุนของ " + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ได้ทำสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + "ไว้กับมหาวิทยาลัยมหิดล ตามสัญญาฯ เมื่อสำเร็จการศึกษาหลักสูตร" + _data[0, 9] + "แล้ว " +
                     "                          ต้องปฏิบัติงานชดใช้ทุนเป็นเวลา " + (!String.IsNullOrEmpty(_data[0, 12]) && !_data[0, 12].Equals("0") ? Util.NumberArabicToThai(_data[0, 12]) : String.Empty) + " ปี นั้น" +
                     "                      </p>" +
                     "                  </td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                                    ExportCPReportNoticeRepayCompleteSection(2, _font, _data) +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                                    ExportCPReportNoticeRepayCompleteSection(3, _font, _data) +
                     "          </table>" +
                     "      </td>" +
                     "  </tr>" +
                     "  <tr>" +
                     "      <td width='" + _width + "' valign='top'>" +
                     "          <table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'>" +
                                    ExportCPReportNoticeRepayCompleteSection(1, _font, _data) +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='" + _width + "'>" +
                     "                      <div>" +
                     "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                              <tr>" +
                     "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                     "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>คณะกรรมการพิจารณาจัดสรรนักศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + "</div></td>" +
                     "                              </tr>" +
                     "                          </table>" +
                     "                      </div>" +
                     "                      <div>" +
                     "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                              <tr>" +
                     "                                  <td width='98' valign='top'><div style='font:normal 15pt " + _font + ";'>สิ่งที่ส่งมาด้วย</div></td>" +
                     "                                  <td width='502'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาใบเสร็จรับเงิน จำนวน ๑ ฉบับ</div></td>" +
                     "                              </tr>" +
                     "                          </table>" +
                     "                      </div>" +
                     "                  </td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'>" +
                     "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่ " + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ได้ทำสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + "ไว้กับมหาวิทยาลัยมหิดล " +
                     "                          ซึ่งตามสัญญาฯ เมื่อสำเร็จการศึกษาหลักสูตร" + _data[0, 9] + "แล้ว ต้องปฏิบัติงานชดใช้ทุนเป็นเวลา " + (!String.IsNullOrEmpty(_data[0, 12]) && !_data[0, 12].Equals("0") ? Util.NumberArabicToThai(_data[0, 12]) : String.Empty) + " ปี นั้น" +
                     "                      </p>" +
                     "                  </td>" +
                     "              </tr>" +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                                    ExportCPReportNoticeRepayCompleteSection(2, _font, _data) +
                     "              <tr>" +
                     "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "              </tr>" +
                                    ExportCPReportNoticeRepayCompleteSection(3, _font, _data) +
                     "          </table>" +
                     "      </td>" +
                     "  </tr>" +
                     "</table>";
        }

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=NoticeRepayComplete.doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        /*
        HttpContext.Current.Response.Write("<html>");
        HttpContext.Current.Response.Write("<head>");
        HttpContext.Current.Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
        HttpContext.Current.Response.Write("<meta name=ProgId content=Word.Document>");
        HttpContext.Current.Response.Write("</head>");
        HttpContext.Current.Response.Write("<body>");
        */
        HttpContext.Current.Response.Write(_html);
        /*
        HttpContext.Current.Response.Write("</body>");
        HttpContext.Current.Response.Write("</html>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        */
    }

    //สำหรับแสดงรายการแจ้งที่ผู้ผิดสัญญาชำระหนี้เรียบร้อยแล้ว เพื่อออกหนังสือแจ้งต้นสังกัดและคณะกรรมการพิจารณา
    public static string ListCPReportNoticeRepayComplete(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _iconStatus = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountCPReportNoticeRepayComplete(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportNoticeRepayComplete(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _iconStatus = eCPUtil._iconPaymentStatus[(!String.IsNullOrEmpty(_data[_i, 11]) ? int.Parse(_data[_i, 11]) - 1 : 0)];
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-notice-repay-complete-col1'><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col2'><div><input class='checkbox' type='checkbox' name='print-notice-repay-complete' onclick=UncheckRoot('check-uncheck-all') value='" + _data[_i, 1] + "' /></div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col3'><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col4'><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col5'><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col6'><div>" + double.Parse(_data[_i, 10]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-repay-complete-col7'>" +
                         "      <div class='icon-status-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportnoticerepaycomplete", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportNoticeRepayComplete()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-notice-repay-complete-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-notice-repay-complete") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'>" +
                 "              <input type='hidden' id='search-report-notice-repay-complete' value=''>" +
                 "              <input type='hidden' id='id-name-report-notice-repay-complete-hidden' value=''>" +
                 "              <input type='hidden' id='faculty-report-notice-repay-complete-hidden' value=''>" +
                 "              <input type='hidden' id='program-report-notice-repay-complete-hidden' value=''>" +
                 "              <div class='button-style2'>" +
                 "                  <ul>" +
                 "                      <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportnoticerepaycomplete',true,'','','')>ค้นหา</a></li>" +
                 "                      <li><a href='javascript:void(0)' onclick='ConfirmPrintNoticeRepayComplete()'>พิมพ์</a></li>" +
                 "                  </ul>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='content-right'>" +
                 "              <div class='content-data-tab-content-msg' id='record-count-cp-report-notice-repay-complete'>ค้นหาพบ 0 รายการ</div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box-search-condition' id='search-report-notice-repay-complete-condition'>" +
                 "          <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "          <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order1'>" +
                 "              <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order1-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order2'>" +
                 "              <div class='box-search-condition-order-title'>คณะ</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order2-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-notice-repay-complete-condition-order' id='search-report-notice-repay-complete-condition-order3'>" +
                 "              <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-repay-complete-condition-order3-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-report-notice-repay-complete-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col2'><div class='table-head-line1'>เลือก<div><input class='checkbox' type='checkbox' name='check-uncheck-all' id='check-uncheck-all' onclick=CheckUncheckAll('check-uncheck-all','print-notice-repay-complete') /></div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col3'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col4'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col5'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col6'><div class='table-head-line1'>ยอดเงินต้นที่ชดใช้</div><div>( บาท )</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-repay-complete-col7'><div class='table-head-line1'>สถานะการชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailpaymentstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>" +
                 "<div id='cp-report-notice-repay-complete-content'>" +
                 "  <div class='box4' id='list-data-report-notice-repay-complete'></div>" +
                 "  <div id='nav-page-report-notice-repay-complete'></div>" +
                 "</div>";                 

        return _html;
    }
}

public class eCPDataReportNoticeClaimDebt
{
    private static string ExportCPReportNoticeClaimDebtSection(int _section, string _font, Dictionary<string, string> _lawyer)
    {        
        string _html = String.Empty;
        string _lawyerFullname = String.Empty;
        string _lawyerPhoneNumber = String.Empty;

        if (_lawyer != null)
        {
            _lawyerFullname = _lawyer["Fullname"];
            _lawyerPhoneNumber = _lawyer["PhoneNumber"];
        }

        if (_section.Equals(1))
        {
            _html += "<tr>" +
                     "  <td width='100%' height='110' align='center'><img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' /></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%' align='right'>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>โทร. " + Util.NumberArabicToThai(_lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <div style='font:normal 15pt " + _font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘/</div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>วันที่</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรื่อง</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>ขอให้ชดใช้เงิน</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "  </td>" +
                      "</tr>";
        }

        if (_section.Equals(2))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดดำเนินการ" +
                     "      </p>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "          <tr>" +
                     "              <td width='200'></td>" +
                     "              <td width='400'>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ขอแสดงความนับถือ</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>(นายคณพศ เฟื่องฟุ้ง)</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ปฏิบัติหน้าที่แทนอธิการบดีมหาวิทยาลัยมหิดล</div>" +
                     "              </td>" +
                     "          </tr>" +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(3))
        {
            _html += "<tr>" +
                     "  <td width='100%' height='110' align='center'><img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' /></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%' align='right'>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>กองกฎหมาย สำนักงานอธิการบดี</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>โทร. " + Util.NumberArabicToThai(_lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <div style='font:normal 15pt " + _font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘.๐๑๙/</div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>วันที่</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(4))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "          <tr>" +
                     "              <td width='98'></td>" +
                     "              <td width='502' style='text-align: center'>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>(นาย/นาง/นางสาว" + _lawyerFullname + ")</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>นิติกร</div></td>" +
                     "              </td>" +
                     "          </tr>" +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        return _html;
    }
    
    private static string ExportCPReportNoticeClaimDebtTime1(string[,] _data)
    {
        string _html = String.Empty;
        string _width = "600";
        string _font = "TH SarabunPSK, TH Sarabun New";
        string _resignationDate = String.Empty;
   
        if (_data[0, 19].Equals("N"))
            _resignationDate = _data[0, 16];

        if (_data[0, 19].Equals("Y"))
            _resignationDate = _data[0, 20];

        Dictionary<string, string> _lawyer = new Dictionary<string, string>();
        _lawyer.Add("Fullname", (!String.IsNullOrEmpty(_data[0, 21]) ? _data[0, 21] : String.Empty));
        _lawyer.Add("FullnameWithoutNamePrefix", (!String.IsNullOrEmpty(_data[0, 21]) ? _data[0, 21].Replace("นาย", "").Replace("นางสาว", "").Replace("นาง", "") : String.Empty));
        _lawyer.Add("PhoneNumber", (!String.IsNullOrEmpty(_data[0, 22]) ? _data[0, 22] : _data[0, 23]));
        _lawyer.Add("Email", (!String.IsNullOrEmpty(_data[0, 24]) ? _data[0, 24] : String.Empty));

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td width='" + _width + "' valign='top'>" +
                 "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeClaimDebtSection(1, _font, _lawyer) +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" + 
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>สัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + " ฉบับลงวันที่ " +  Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 13])) + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่ท่านได้ทำสัญญาที่อ้างถึงผูกพันไว้กับมหาวิทยาลัยมหิดลว่าภายหลังจากสำเร็จการศึกษาตามหลักสูตร " +
                 "                          ท่านยินยอมปฏิบัติตามคำสั่งของสำนักงานคณะกรรมการข้าราชการพลเรือนและหรือ " +
                 "                          คณะกรรมการพิจารณาจัดสรรนักศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + "ผู้สำเร็จการศึกษาไปปฏิบัติงานในส่วนราชการหรือ " +
                 "                          องค์การของรัฐบาลต่าง ๆ ได้สั่งให้เข้ารับราชการหรือทำงาน และจะรับราชการหรือทำงานอยู่ต่อไปเป็นเวลา " +
                 "                          ไม่น้อยกว่า" + Util.ThaiNum(_data[0, 11]) + "ปีติดต่อกันไปนับตั้งแต่วันที่ได้กำหนดในคำสั่ง หากท่านไม่รับราชการหรือทำงาน ท่านยินยอม " +
                 "                          รับผิดชดใช้เงินจำนวน " + Util.NumberArabicToThai(double.Parse(_data[0, 12]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 12]) + ") หากรับราชการหรือทำงานไม่ครบตามกำหนดเวลา " +
                 "                          ท่านยินยอมรับผิดชดใช้เงินให้แก่มหาวิทยาลัยตามระยะเวลาที่ขาดโดยคิดคำนวณลดลงตามส่วนเฉลี่ยจากเงิน " +
                 "                          ที่ต้องชดใช้ดังกล่าว นั้น" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า การที่ท่านขอลาออกจากราชการ/การปฏิบัติงานตั้งแต่วันที่ " +
                                            (!String.IsNullOrEmpty(_resignationDate) ? Util.ThaiLongDateWithNumberTH(DateTime.Parse(Util.ConvertDateEN(_resignationDate)).AddDays(1).ToString()) : "") + " ถือว่าเป็นการปฏิบัติราชการ/ปฏิบัติงานไม่ครบกำหนดตามสัญญาที่ให้ไว้ เป็นเหตุให้ท่าน " +
                 "                          ต้องรับผิดชอบชดใช้เงินแก่มหาวิทยาลัยมหิดลเป็นจำนวนทั้งสิ้น <strong>" + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 18]) + ")</strong> " +
                 "                          ดังนั้นจึงขอให้ท่านนำเงินจำนวนดังกล่าวมาชำระภายใน ๓๐ วัน นับถัดจากวันที่ได้รับหนังสือฉบับนี้ โดยดำเนินการ ดังนี้" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๑. ชำระเงิน โดยนำฝากเงินเข้าบัญชี</strong>ธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" " +
                 "                          ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ หรือ<strong>โอนเงินเข้าบัญชี</strong>ธนาคารไทยพาณิชย์ " +
                 "                          ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี ๐๑๖-๒-๑๐๓๒๒-๓" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๒. กรณีชำระเงินเกินกำหนดระยะเวลาดังกล่าว</strong> (มีดอกเบี้ยผิดนัดชำระ) ให้ท่านติดต่อนิติกร " +
                 "                          ผู้รับผิดชอบ: คุณ" + _lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(_lawyer["PhoneNumber"]) + ") เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ก่อนแล้วจึงชำระเงิน" + 
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>๓. กรณีมีความจำเป็นที่ไม่อาจดำเนินการตามข้อ ๑. และ ๒ ได้</strong> ให้ท่านติดต่อขอชำระเงินสด " +
                 "                          ผ่านกองกฎหมาย สำนักงานอธิการบดี มหาวิทยาลัยมหิดล ภายในเวลา ๑๕.๐๐ น. ในวันทำการ" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ให้ท่านจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร (Fax) " +
                 "                          หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + _lawyer["Email"] + " หากล่วงเลย " +
                 "                          ระยะเวลาดังกล่าว มหาวิทยาลัยจำต้องคิดดอกเบี้ยผิดนัด และดำเนินการตามกฎหมายต่อไป" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportNoticeClaimDebtSection(2, _font, null) +
                 "          </table>" +
                 "      </td>" +
                 "  </tr>" +
                 "</table>";

        return _html;
    }

    //สำหรับส่งออกหนังสือทวงถามผู้ผิดสัญญาและผู้ค้ำประกันครั้งที่ 2 เป็นไฟล์ PDF
    private static string ExportCPReportNoticeClaimDebtTime2(string[,] _data, string _overpaymentDateStart)
    {
        string _html = String.Empty;
        string _width = "600";
        string _font = "TH SarabunPSK, TH Sarabun New";
        string[] _contractInterest = eCPUtil.GetContractInterest();
        string _cp2id = _data[0, 1];
        string _statusRepayDefault = _data[0, 25];
        string[,] _data1;
        string _replyDate = String.Empty;
        string _pursuant = String.Empty;
        string _pursuantBookDate = String.Empty;

        Dictionary<string, string> _lawyer = new Dictionary<string, string>();
        _lawyer.Add("Fullname", (!String.IsNullOrEmpty(_data[0, 21]) ? _data[0, 21] : String.Empty));
        _lawyer.Add("FullnameWithoutNamePrefix", (!String.IsNullOrEmpty(_data[0, 21]) ? _data[0, 21].Replace("นาย", "").Replace("นางสาว", "").Replace("นาง", "") : String.Empty));
        _lawyer.Add("PhoneNumber", (!String.IsNullOrEmpty(_data[0, 22]) ? _data[0, 22] : _data[0, 23]));
        _lawyer.Add("Email", (!String.IsNullOrEmpty(_data[0, 24]) ? _data[0, 24] : String.Empty));

        _data1 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, _statusRepayDefault);

        if (_data1.GetLength(0) > 0)
        {
            _replyDate = _data1[0, 5];
            _pursuant = _data1[0, 6];
            _pursuantBookDate = _data1[0, 7];
        }

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td width='" + _width + "' valign='top'>" +
                 "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeClaimDebtSection(1, _font, _lawyer) +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!String.IsNullOrEmpty(_pursuant) ? Util.NumberArabicToThai(_pursuant) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_pursuantBookDate)) : "") + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา " +
                 "                          เพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 13])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท " +
                 "                          (" + Util.ThaiBaht(_data[0, 18]) + ") ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือ " +
                 "                          ดังกล่าว และท่านได้รับหนังสือดังกล่าวแล้วเมื่อวันที่ " + (!String.IsNullOrEmpty(_replyDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_replyDate)) : "") + " ความละเอียดทราบแล้ว นั้น" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า บัดนี้ได้ล่วงเลยระยะเวลาตามที่กำหนดแล้ว ท่านยังมิได้ชำระเงินดังกล่าวแต่อย่างใด " +
                 "                          ในการนี้ จึงขอให้ท่านเร่งนำเงินจำนวน " + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 18]) + ") " +
                 "                          พร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(_contractInterest[1]) + " ต่อปีของต้นเงินจำนวนข้างต้น " +
                 "                          นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_overpaymentDateStart)) + " ซึ่งเป็นวันผิดนัด จนถึงวันที่ท่านชำระเสร็จสิ้น มาชำระให้แก่มหาวิทยาลัยมหิดลโดยเร็ว " +
                 "                          มิเช่นนั้นมหาวิทยาลัยมหิดลจำต้องดำเนินการตามกฎหมายต่อไป" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ขอให้ท่านติดต่อนิติกรผู้รับผิดชอบ: คุณ" + _lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(_lawyer["PhoneNumber"]) + ") " +
                 "                          เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ให้แก่มหาวิทยาลัยมหิดล แล้วจึงชำระเงินจำนวนดังกล่าวโดยนำฝากเงินเข้าบัญชีธนาคาร " +
                 "                          ไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ " +
                 "                          หรือโอนเงินเข้าบัญชีธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี " +
                 "                          ๐๑๖-๒-๑๐๓๒๒-๓ และจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร(Fax) " +
                 "                          หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + _lawyer["Email"] +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportNoticeClaimDebtSection(2, _font, null) +
                 "          </table>" +
                 "      </td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td width='" + _width + "' valign='top'>" +
                 "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeClaimDebtSection(1, _font, _lawyer) +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>" + _data[0, 14] + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>สัญญาค้ำประกัน ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 15])) +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='98' valign='top'><div style='font:normal 15pt " + _font + ";'>สิ่งที่ส่งมาด้วย</div></td>" +
                 "                                  <td width='502'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>สำเนาหนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!String.IsNullOrEmpty(_pursuant) ? Util.NumberArabicToThai(_pursuant) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_pursuantBookDate)) : "") + "</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามสัญญาที่อ้างถึง ท่านได้ทำสัญญาค้ำประกันผูกพันไว้ต่อมหาวิทยาลัยมหิดลว่า " +
                 "                          ถ้า" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ต้องรับผิดชดใช้เงินตามสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 13])) + " " + 
                 "                          แก่มหาวิทยาลัยแล้ว ท่านยินยอมชดใช้เงินตามจำนวนที่" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " " +
                 "                          ต้องรับผิดจนครบถ้วน ความละเอียดทราบแล้ว นั้น" +  
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดล ขอเรียนว่า " + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " ได้ปฏิบัติงานไม่ครบกำหนดตามสัญญาการเป็นนักศึกษาฯ " +
                 "                          เป็นเหตุให้ต้องรับผิดชดใช้เงินให้แก่มหาวิทยาลัยมหิดล เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 18]) + ") " +
                 "                          และมหาวิทยาลัยมหิดลได้มีหนังสือทวงถามให้" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " " +
                 "                          ชดใช้เงินภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือ รายละเอียดปรากฏตามสิ่งที่ส่งมาด้วย ซึ่ง" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " " +
                 "                          ได้รับหนังสือแล้วแต่กลับเพิกเฉยไม่ชำระเงินภายในกำหนด เป็นเหตุให้ท่านซึ่งเป็นผู้ค้ำประกันต้องรับผิดชดใช้เงินให้แก่มหาวิทยาลัยมหิดล " + 
                 "                          ในการนี้ จึงขอให้ท่านนำเงินจำนวน " + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 18]) + ") " +
                 "                          พร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(_contractInterest[1]) + " ต่อปีของต้นเงินจำนวนดังกล่าว นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_overpaymentDateStart)) + " " +
                 "                          ซึ่งเป็นวันผิดนัด จนถึงวันที่ท่านชำระเสร็จสิ้น มาชำระให้แก่มหาวิทยาลัยมหิดลโดยเร็ว " +
                 "                          มิเช่นนั้นมหาวิทยาลัยมหิดลจำต้องดำเนินการตามกฎหมายต่อไป" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ทั้งนี้ ขอให้ท่านติดต่อนิติกรผู้รับผิดชอบ: คุณ" + _lawyer["FullnameWithoutNamePrefix"] + " (" + Util.NumberArabicToThai(_lawyer["PhoneNumber"]) + ") " +
                 "                          เพื่อคำนวณจำนวนเงินที่ต้องชดใช้ให้แก่มหาวิทยาลัยมหิดล แล้วจึงชำระเงินจำนวนดังกล่าวโดยนำฝากเงินเข้าบัญชีธนาคารไทยพาณิชย์ " + 
                 "                          ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทกระแสรายวัน สาขาศิริราช เลขที่บัญชี ๐๑๖-๓-๐๐๓๒๕-๖ " +
                 "                          หรือโอนเงินเข้าบัญชีธนาคารไทยพาณิชย์ ชื่อบัญชี \"มหาวิทยาลัยมหิดล\" ประเภทออมทรัพย์ เลขที่บัญชี ๐๑๖-๒-๑๐๓๒๒-๓ " +
                 "                          และจัดส่งใบนำฝากเงิน โดยระบุชื่อ-สกุล ที่อยู่ หมายเลขโทรศัพท์ มาที่โทรสาร(Fax) " +
                 "                          หมายเลข ๐ ๒๘๔๙ ๖๒๖๕ หรือสแกนส่งทางไปรษณีย์อิเล็กทรอนิกส์: " + _lawyer["Email"] +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportNoticeClaimDebtSection(2, _font, null) +
                 "          </table>" +
                 "      </td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td width='" + _width + "' valign='top'>" +
                 "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportNoticeClaimDebtSection(3, _font, _lawyer) +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรื่อง</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>ขอให้ชดใช้เงินผิดสัญญาการเป็นนักศึกษาฯ ราย " + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " (ทวงถามครั้งที่ ๒)</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>ผู้อำนวยการกองกฎหมาย</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่มหาวิทยาลัยได้มีหนังสือที่ อว ๗๘/ " + (!String.IsNullOrEmpty(_pursuant) ? Util.NumberArabicToThai(_pursuant) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_pursuantBookDate)) : "") + " " +
                 "                          ถึง" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " เพื่อขอให้ชดใช้เงินกรณีปฏิบัติงานไม่ครบกำหนดตามสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data[0, 9]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data[0, 13])) + " " +
                 "                          เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(_data[0, 18]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data[0, 18]) + ") " +
                 "                          ให้แก่มหาวิทยาลัย ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว นั้น" +  
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;งานกฎหมายและนิติกรรมสัญญา ขอเรียนว่า หนังสือดังกล่าวมีผู้รับไว้โดยชอบแล้วเมื่อวันที่ " + (!String.IsNullOrEmpty(_replyDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_replyDate)) : "") + " " +
                 "                          รายละเอียดปรากฏตามใบตอบรับไปรษณีย์ลงทะเบียนในประเทศ บัดนี้ได้ล่วงเลยกำหนดระยะเวลาการชำระเงินแล้ว ยังไม่ปรากฏว่า" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " " +
                 "                          ได้ชำระเงินให้แก่มหาวิทยาลัยแต่อย่างใด ในการนี้ งานกฎหมายและนิติกรรมสัญญาจึงเห็นควรให้มหาวิทยาลัยมีหนังสือทวงถามถึง" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " (ครั้งที่ ๒) และ" + _data[0, 14] + " ในฐานะผู้ค้ำประกัน " +
                 "                          เพื่อดำเนินการชำระเงินจำนวนดังกล่าวพร้อมดอกเบี้ยผิดนัดในอัตราร้อยละ " + Util.NumberArabicToThai(_contractInterest[1]) + " ต่อปีของต้นเงิน นับตั้งแต่วันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_overpaymentDateStart)) + " " +
                 "                          ซึ่งเป็นวันผิดนัดจนถึงวันที่ชำระเสร็จสิ้น ให้แก่มหาวิทยาลัยมหิดลโดยเร็ว" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดพิจารณา หากเห็นชอบ " +
                 "                          ขอได้โปรดลงนามในหนังสือถึง" + _data[0, 3] + _data[0, 4] + " " + _data[0, 5] + " และ" + _data[0, 14] + " ตามที่เสนอมาพร้อมนี้" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportNoticeClaimDebtSection(4, _font, _lawyer) +
                 "          </table>" +
                 "      </td>" +
                 "  </tr>" +
                 "</table>";

        return _html;
    }

    public static void ExportCPReportNoticeClaimDebt(string _exportSend)
    {
        string _html = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _value = _exportSend.Split(_separator);
        string _cp1id = _value[0];        
        int _time = int.Parse(_value[1]);
        string _previousRepayDateEnd = _value[2];
        string _overpaymentDateStart = String.Empty;
        string[] _repayDate;
        string[,] _data;

        _data = eCPDB.ListDetailReportNoticeClaimDebt(_cp1id);

        switch (_time)
        {
            case 1:
                _html = ExportCPReportNoticeClaimDebtTime1(_data);
                break;
            case 2:
                if (!String.IsNullOrEmpty(_previousRepayDateEnd))
                {
                    _repayDate = eCPUtil.RepayDate(_previousRepayDateEnd);
                    _overpaymentDateStart = _repayDate[2];
                }
                _html = ExportCPReportNoticeClaimDebtTime2(_data, _overpaymentDateStart);
                break;
        }

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=NoticeClaimDebtTime" + _time.ToString() + ".doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Write(_html);
    }

    public static string ListCPReportNoticeClaimDebt(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string[] _iconStatus;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountCPReportNoticeClaimDebt(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportNoticeClaimDebt(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _iconStatus = _data[_i, 11].Split(new char[] { ';' });
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "LoadForm(1,'repaycptransrequirecontract1',true,''," + _data[_i, 1] + ",'repay" + _data[_i, 2] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='repay" + _data[_i, 2] + "'>" +
                         "  <li id='table-content-cp-report-notice-claim-debt-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-claim-debt-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-claim-debt-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-claim-debt-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-claim-debt-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 10]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-notice-claim-debt-col6' onclick=" + _callFunc + ">" +
                         "      <div class='icon-status-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus[1] + "'></li>" +
                         "              <li class='" + _iconStatus[2] + "'></li>" +
                         "              <li class='" + _iconStatus[3] + "'></li>" +
                         "              <li class='" + _iconStatus[4] + "'></li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportnoticeclaimdebt", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportNoticeClaimDebt()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-notice-claim-debt-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-notice-claim-debt") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'>" +
                 "              <input type='hidden' id='search-report-notice-claim-debt' value=''>" +
                 "              <input type='hidden' id='id-name-report-notice-claim-debt-hidden' value=''>" +
                 "              <input type='hidden' id='faculty-report-notice-claim-debt-hidden' value=''>" +
                 "              <input type='hidden' id='program-report-notice-claim-debt-hidden' value=''>" +
                 "              <div class='button-style2'>" +
                 "                  <ul>" +
                 "                      <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportnoticeclaimdebt',true,'','','')>ค้นหา</a></li>" +
                 "                  </ul>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='content-right'>" +
                 "              <div class='content-data-tab-content-msg' id='record-count-cp-report-notice-claim-debt'>ค้นหาพบ 0 รายการ</div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box-search-condition' id='search-report-notice-claim-debt-condition'>" +
                 "          <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "          <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order1'>" +
                 "              <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order1-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order2'>" +
                 "              <div class='box-search-condition-order-title'>คณะ</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order2-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-notice-claim-debt-condition-order' id='search-report-notice-claim-debt-condition-order3'>" +
                 "              <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-notice-claim-debt-condition-order3-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-report-notice-claim-debt-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-claim-debt-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-claim-debt-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-claim-debt-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-claim-debt-col5'><div class='table-head-line1'>ยอดเงินต้นที่ชดใช้</div><div>( บาท )</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-notice-claim-debt-col6'><div class='table-head-line1'>สถานะการแจ้งชำระหนี้</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailrepaystatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>" +
                 "<div id='cp-report-notice-claim-debt-content'>" +
                 "  <div class='box4' id='list-data-report-notice-claim-debt'></div>" +
                 "  <div id='nav-page-report-notice-claim-debt'></div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportNoticeCheckForReimbursement
{
    private static void ExportCPReportNoticeCheckForReimbursementV1(string _cp1id)
    {
        string _pdfFont = "Font/THSarabunBold.ttf";
        string _template = String.Empty;
        string _saveFile = "NoticeCheckForReimbursement.pdf";
        string[,] _data;

        _data = eCPDB.ListDetailCPTransBreakContract(_cp1id);

        string _caseGraduate = _data[0, 33];
        string _civil = _data[0, 34];

        if (_caseGraduate.Equals("1"))
            _template = "ExportTemplate/NoticeCheckForReimbursement.NotGraduate.pdf";

        if (_caseGraduate.Equals("2") && _civil.Equals("1"))
            _template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.Work.Resign.pdf";

        if (_caseGraduate.Equals("2") && _civil.Equals("2"))
            _template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.NotWorking.pdf";

        ExportToPdf _exportToPdf = new ExportToPdf();
        _exportToPdf.ExportToPdfConnect(_template, "pdf", _saveFile);
        _exportToPdf.FillForm(_pdfFont, 11, 0, _data[0, 18], 105, 809, 190, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 19], 309, 811, 138, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 20])), 470, 811, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 21], 154, 791, 45, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 22])), 221, 791, 77, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 23], 404, 791, 45, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 24])), 470, 791, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 5] + _data[0, 8] + " " + _data[0, 9], 63, 698, 158, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 2], 281, 698, 115, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, eCPDataReport.ReplaceProgramToShortProgram(_data[0, 11]), 67, 678, 120, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, eCPDataReport.ReplaceFacultyToShortProgram(_data[0, 14]), 210, 678, 114, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 25])), 471, 678, 82, 0);
        _exportToPdf.FillForm(_pdfFont, 11, 0, _data[0, 26], 107, 656, 92, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (int.Parse(DateTime.Parse(Util.ConvertDateEN(_data[0, 31])).ToString("yyyy")) + 543).ToString(), 313, 658, 48, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 32])), 460, 658, 94, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (_civil.Equals("1") && !_data[0, 36].Equals("0") ? (_data[0, 36] + " ปี") : String.Empty), 150, 638, 132, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (double.Parse(_data[0, 37]).ToString("#,##0.00") + " บาท"), 366, 638, 187, 0);
        _exportToPdf.ExportToPdfDisconnect();
    }

    public static void ExportCPReportNoticeCheckForReimbursementV2(string _cp1id)
    {
        string _pdfFont = "Font/THSarabunBold.ttf";
        string _template = String.Empty;
        string _saveFile = "NoticeCheckForReimbursement.pdf";
        string[,] _data;

        _data = eCPDB.ListDetailCPTransRequireContract(_cp1id);

        string _scholar = _data[0, 40];
        string _scholarshipMoney = _data[0, 41];
        string _scholarshipYear = _data[0, 42];
        string _scholarshipMonth = _data[0, 43];
        string _allActualMonthScholarship = _data[0, 8];
        string _caseGraduate = _data[0, 46];
        string _educationDate = _data[0, 44];
        string _graduateDate = _data[0, 45];
        string _civil = _data[0, 47];
        string _dateStart = String.Empty;
        string _dateEnd = String.Empty;
        string _indemnitorYear = _data[0, 49];
        string _indemnitorCash = _data[0, 50];
        string _calDateCondition = _data[0, 48];
        string _studyLeave = _data[0, 66];
        string _beforeStudyLeaveStartDate = _data[0, 67];
        string _beforeStudyLeaveEndDate = _data[0, 68];
        string _studyLeaveStartDate = _data[0, 69];
        string _studyLeaveEndDate = _data[0, 70];
        string _afterStudyLeaveStartDate = _data[0, 71];
        string _afterStudyLeaveEndDate = _data[0, 72];
        
        if (_caseGraduate.Equals("1"))
        {
            _template = "ExportTemplate/NoticeCheckForReimbursement.NotGraduate.pdf";
            _dateStart = _data[0, 62];
            _dateEnd = _data[0, 63];
        }

        if (_caseGraduate.Equals("2") && _civil.Equals("1"))
        {
            _template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.Work.Resign.pdf";
            _dateStart = _data[0, 6];
            _dateEnd = _data[0, 7];
        }

        if (_caseGraduate.Equals("2") && _civil.Equals("2"))
        {
            _template = "ExportTemplate/NoticeCheckForReimbursement.Graduate.NotWorking.pdf";
            _dateStart = _data[0, 44];
            _dateEnd = _data[0, 45];
        }

        ExportToPdf _exportToPdf = new ExportToPdf();
        _exportToPdf.ExportToPdfConnect(_template, "pdf", _saveFile);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 31], 105, 811, 190, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 32], 309, 811, 138, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 33])), 470, 811, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 34], 154, 791, 45, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 35])), 221, 791, 77, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 36], 404, 791, 45, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 37])), 470, 791, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 20] + _data[0, 21] + " " + _data[0, 22], 63, 698, 158, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 19], 281, 698, 115, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, eCPDataReport.ReplaceProgramToShortProgram(_data[0, 24]), 67, 678, 120, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, eCPDataReport.ReplaceFacultyToShortProgram(_data[0, 27]), 210, 678, 114, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 38])), 471, 678, 82, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 39], 107, 658, 92, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (int.Parse(DateTime.Parse(Util.ConvertDateEN(_data[0, 44])).ToString("yyyy")) + 543).ToString(), 313, 658, 48, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 45])), 460, 658, 94, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (_civil.Equals("1") && !_data[0, 49].Equals("0") ? (_data[0, 49] + " ปี") : String.Empty), 150, 638, 132, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, (double.Parse(_data[0, 50]).ToString("#,##0.00") + " บาท"), 366, 638, 187, 0);

        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 44])), 261, 479, 87, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, Util.ThaiLongDate(Util.ConvertDateEN(_data[0, 45])), 461, 479, 91, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 1, ((!String.IsNullOrEmpty(_data[0, 11]) && !_data[0, 11].Equals("0") ? (_data[0, 11] + " เดือน") : String.Empty) + " " + (!String.IsNullOrEmpty(_data[0, 12]) && !_data[0, 12].Equals("0") ? _data[0, 12] : String.Empty)), 203, 459, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 1, (!String.IsNullOrEmpty(_data[0, 13]) ? double.Parse(_data[0, 13]).ToString("#,##0") : String.Empty), 458, 459, 80, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 3], 209, 438, 344, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 5], 100, 418, 105, 0);

        if (_data[0, 66].Equals("N"))
        {            
            _exportToPdf.FillForm(_pdfFont, 13, 0, Util.LongDateTH(_data[0, 6]), 248, 418, 125, 0);
            _exportToPdf.FillForm(_pdfFont, 13, 0, Util.LongDateTH(_data[0, 7]), 408, 418, 145, 0);
        }

        if (_data[0, 66].Equals("Y"))
        {
            _exportToPdf.FillForm(_pdfFont, 13, 0, Util.LongDateTH(_data[0, 67]), 248, 418, 125, 0);
            _exportToPdf.FillForm(_pdfFont, 13, 0, Util.LongDateTH(_data[0, 68]), 408, 418, 145, 0);
        }
        
        _exportToPdf.FillForm(_pdfFont, 13, 1, (!String.IsNullOrEmpty(_data[0, 14]) ? double.Parse(_data[0, 14]).ToString("#,##0") : String.Empty), 212, 398, 58, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 1, (!String.IsNullOrEmpty(_data[0, 15]) ? double.Parse(_data[0, 15]).ToString("#,##0") : String.Empty), 465, 398, 73, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 20] + _data[0, 21] + " " + _data[0, 22], 258, 379, 296, 0);

        double[] _resultPayScholarship;
        double[] _resultPenalty;        
        double _iCash = 0;
        double _allActual = 0;
        double _actual = 0;
        double _actualMonth = 0;
        double _educationActual = 0;
        double _educationMonth = 0;
        double _educationDay = 0;
        int _dayLastMonth = 0;
        int _formular = int.Parse(_calDateCondition);
        string[] _penaltyFormularString = new string[3];

        _resultPayScholarship = eCPUtil.CalPayScholarship(_scholar, _caseGraduate, _civil, _scholarshipMoney, _scholarshipYear, _scholarshipMonth, _allActualMonthScholarship);
        _resultPenalty = eCPUtil.GetCalPenalty(_studyLeave, _beforeStudyLeaveStartDate, _beforeStudyLeaveEndDate, _afterStudyLeaveStartDate, _afterStudyLeaveEndDate, _scholar, _caseGraduate, _educationDate, _graduateDate, _civil, _resultPayScholarship[1].ToString(), _scholarshipYear, _scholarshipMonth, _dateStart, _dateEnd, _indemnitorYear, _indemnitorCash, _calDateCondition);

        if (_formular.Equals(1))
        {
            _iCash = _resultPenalty[8];
            _actualMonth = _resultPenalty[9];
            _penaltyFormularString = eCPUtil.PenaltyFormular1ToString(_iCash, _actualMonth).Split(';');

            _exportToPdf.FillForm(_pdfFont, 13, 1, _penaltyFormularString[0], 139, 359, 186, 0);
        }

        if (_formular.Equals(2))
        {
            _iCash = _resultPenalty[8];
            _educationMonth = _resultPenalty[10];
            _educationDay = _resultPenalty[11];
            _dayLastMonth = int.Parse(_resultPenalty[12].ToString());
            _penaltyFormularString = eCPUtil.PenaltyFormular2ToString(_iCash, _educationMonth, _educationDay, _dayLastMonth).Split(';');

            _exportToPdf.FillForm(_pdfFont, 12, 1, _penaltyFormularString[0], 139, 367, 98, 0);
            _exportToPdf.FillForm(_pdfFont, 12, 1, _penaltyFormularString[1], 237, 367, 87, 0);
            _exportToPdf.CreateTable(237, 351, 87, 1, 0, 1, 0, 0);
            _exportToPdf.FillForm(_pdfFont, 12, 1, _penaltyFormularString[2], 237, 357, 87, 0);
        }

        if (_formular.Equals(3))
        {
            _iCash = _resultPenalty[8];
            _allActual = _resultPenalty[2];
            _actual = _resultPenalty[13];
            _penaltyFormularString = eCPUtil.PenaltyFormular3ToString(_iCash, _allActual, _actual).Split(';');

            _exportToPdf.FillForm(_pdfFont, 13, 1, _penaltyFormularString[0], 139, 359, 186, 0);
            _exportToPdf.FillForm(_pdfFont, 13, 1, _penaltyFormularString[1], 139, 347, 186, 0);
        }

        if (_formular.Equals(4))
        {
            _iCash = _resultPenalty[8];
            _educationActual = _resultPenalty[14];
            _actual = _resultPenalty[3];
            _penaltyFormularString = eCPUtil.PenaltyFormular4ToString(_iCash, _educationActual, _actual).Split(';');

            _exportToPdf.FillForm(_pdfFont, 13, 1, _penaltyFormularString[0], 139, 359, 186, 0);
            _exportToPdf.FillForm(_pdfFont, 13, 1, _penaltyFormularString[1], 139, 347, 186, 0);
        }

        _exportToPdf.FillForm(_pdfFont, 13, 1, (!String.IsNullOrEmpty(_data[0, 16]) ? double.Parse(_data[0, 16]).ToString("#,##0.00") : String.Empty), 335, 359, 195, 0);
        _exportToPdf.FillForm(_pdfFont, 15, 1, Util.ThaiBaht(_data[0, 16]), 78, 320, 465, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 0, _data[0, 20] + _data[0, 21] + " " + _data[0, 22], 144, 298, 275, 0);
        _exportToPdf.FillForm(_pdfFont, 13, 1, _data[0, 73], 322, 138, 136, 0);
        _exportToPdf.ExportToPdfDisconnect();
    }
    
    public static void ExportCPReportNoticeCheckForReimbursement(string _exportSend)
    {
        char[] _separator = new char[] { ':' };
        string[] _cp1idAction = _exportSend.Split(_separator);
        string _cp1id = _cp1idAction[0];
        string _action = _cp1idAction[1];
        
        switch (_action)
        {
            case "v1":
                ExportCPReportNoticeCheckForReimbursementV1(_cp1id);
                break;
            case "v2":
                ExportCPReportNoticeCheckForReimbursementV2(_cp1id);
                break;
        }
    }
}

public class eCPDataReportStatisticPaymentByDate
{
    public static string ViewTransPaymentByDate(string _cp2idDate)
    {
        string _html = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _cp2idDate1 = _cp2idDate.Split(_separator);
        string _cp2id = _cp2idDate1[0];
        string _dateStart = _cp2idDate1[1];
        string _dateEnd = _cp2idDate1[2];
        string[,] _data, _data1;
        int _recordCount;

        _data = eCPDB.ListDetailPaymentOnCPTransRequireContract(_cp2id);

        if (_data.GetLength(0) > 0)
        {
            string _statusPayment = _data[0, 7];
            string _formatPayment = _data[0, 8];
            string _studentIDDefault = _data[0, 9];
            string _titleNameDefault = _data[0, 10];
            string _firstNameDefault = _data[0, 11];
            string _lastNameDefault = _data[0, 12];
            string _facultyCodeDefault = _data[0, 16];
            string _facultyNameDefault = _data[0, 17];
            string _programCodeDefault = _data[0, 13];
            string _programNameDefault = _data[0, 14];
            string _groupNumDefault = _data[0, 18];
            string _dlevelDefault = _data[0, 20];
            string _pictureFileNameDefault = _data[0, 21];
            string _pictureFolderNameDefault = _data[0, 22];

            _data1 = eCPDB.ListTransPayment(_cp2id, _dateStart, _dateEnd);
            _recordCount = _data1.GetLength(0);
            
            _html += "<div class='form-content' id='view-trans-payment-by-date-head'>" +
                     "  <input type='hidden' id='period-hidden' value=''>" +
                     "  <div id='profile-student'>" +
                     "      <div class='content-left' id='picture-student'><div><img src='Handler/eCPHandler.ashx?action=resize&file=" + eCPUtil.URL_PICTURE_STUDENT + _pictureFolderNameDefault + "/" + _pictureFileNameDefault + "&width=" + eCPUtil.WIDTH_PICTURE_STUDENT + "&height=" + eCPUtil.HEIGHT_PICTURE_STUDENT + "' /></div></div>" +
                     "      <div class='content-left' id='profile-student-label'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>รหัสนักศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ชื่อ - นามสกุล</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>ระดับการศึกษา</div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'>คณะ</div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'>หลักสูตร</div></div>" +
                     "      </div>" +
                     "      <div class='content-left' id='profile-student-input'>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _studentIDDefault + "&nbsp;" + _programCodeDefault.Substring(0, 4) + " / " + _programCodeDefault.Substring(4, 1) + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _titleNameDefault + _firstNameDefault + " " + _lastNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _dlevelDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style'><div class='form-label-style'><span>" + _facultyCodeDefault + " - " + _facultyNameDefault + "</span></div></div>" +
                     "          <div class='form-label-discription-style clear-bottom'><div class='form-label-style'><span>" + _programCodeDefault + " - " + _programNameDefault + (!_groupNumDefault.Equals("0") ? " ( กลุ่ม " + _groupNumDefault + " )" : "") + "</span></div></div>" +
                     "      </div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "</div>" +
                     "<div id='view-trans-payment-by-date-content'>" +
                     "  <div id='view-trans-payment-by-date'>" +
                     "      <div class='tab-line'></div>" +
                     "      <div class='content-data-tab-content'>" +
                     "          <div class='content-left'><div class='content-data-tab-content-msg'>ช่วงวันที่ชำระหนี้ " + (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd) ? ("ระหว่างวันที่ " + _dateStart + " ถึงวันที่ " + _dateEnd) : "ไม่กำหนด") + "</div></div>" +
                     "          <div class='content-right'><div class='content-data-tab-content-msg'>ค้นหาพบ " + _recordCount + " รายการ</div></div>" +
                     "      </div>" +
                     "      <div class='clear'></div>" +
                     "      <div class='tab-line'></div>" +
                     "      <div class='box3'>" +
                     "          <div class='table-head'>" +
                     "              <ul>" +
                     "                  <li id='table-head-trans-payment-col1'><div class='table-head-line1'>งวดที่</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col2'><div class='table-head-line1'>เงินต้น</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col3'><div class='table-head-line1'>ดอกเบี้ย</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col4'><div class='table-head-line1'>เงินต้น</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col5'><div class='table-head-line1'>ดอกเบี้ย</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col6'><div class='table-head-line1'>ยอดเงิน</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col7'><div class='table-head-line1'>เงินต้น</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col8'><div class='table-head-line1'>ดอกเบี้ย</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col9'><div class='table-head-line1'>ดอกเบี้ย</div><div>ค้างจ่าย</div><div>( บาท )</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col10'><div class='table-head-line1'>วันเดือนปี</div><div>ที่รับชำระหนี้</div></li>" +
                     "                  <li class='table-col' id='table-head-trans-payment-col11'><div class='table-head-line1'>จ่ายชำระด้วย</div></li>" +
                     "              </ul>" +
                     "          </div>" +
                     "          <div class='clear'></div>" +
                     "      </div>" +
                     "      <div id='box-list-trans-payment-by-date'><div id='list-trans-payment-by-date'>" + eCPDataPayment.ListTransPayment(_data1) + "</div></div>" +
                     "  </div>" +
                     "</div>";
        }

        return _html;
    }
    
    public static string ListCPReportStatisticPaymentByDate(HttpContext _c)
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

        _recordCount = eCPDB.CountCPReportStatisticPaymentByDate(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListReportStatisticPaymentByDate(_c);
            
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _groupNum = !_data[_i, 9].Equals("0") ? " ( กลุ่ม " + _data[_i, 9] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "LoadForm(1,'viewtranspaymentbydate',true,'','" + (_data[_i, 2] + ":" + _c.Request["datestart"] + ":" + _c.Request["dateend"]) + "','report-statistic-payment-by-date" + _data[_i, 0] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-statistic-payment-by-date" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-report-statistic-payment-by-date-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col4' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 7] + "</span>- " + _data[_i, 8] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 10]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 11]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-statistic-payment-by-date-col7' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 13]) ? _data[_i, 13] : "-") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportstatisticpaymentbydate", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportStatisticPaymentByDate()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-statistic-payment-by-date-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-statistic-payment-by-date") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'>" +
                 "              <input type='hidden' id='search-report-statistic-payment-by-date' value=''>" +
                 "              <input type='hidden' id='id-name-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <input type='hidden' id='faculty-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <input type='hidden' id='program-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <input type='hidden' id='format-payment-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <input type='hidden' id='date-start-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <input type='hidden' id='date-end-report-statistic-payment-by-date-hidden' value=''>" +
                 "              <div class='button-style2'>" +
                 "                  <ul>" +
                 "                      <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportpaymentbydate',true,'','','')>ค้นหา</a></li>" +
                 "                  </ul>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='content-right'>" +
                 "              <div class='content-data-tab-content-msg' id='record-count-cp-report-statistic-payment-by-date'>ค้นหาพบ 0 รายการ</div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box-search-condition' id='search-report-statistic-payment-by-date-condition'>" +
                 "          <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "          <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order1'>" +
                 "              <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order1-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order2'>" +
                 "              <div class='box-search-condition-order-title'>คณะ</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order2-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order3'>" +
                 "              <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order3-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order4'>" +
                 "              <div class='box-search-condition-order-title'>รูปแบบการชำะหนี้</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order4-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-statistic-payment-by-date-condition-order' id='search-report-statistic-payment-by-date-condition-order5'>" +
                 "              <div class='box-search-condition-order-title'>ช่วงวันที่ชำระหนี้</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-statistic-payment-by-date-condition-order5-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-report-statistic-payment-by-date-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col5'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>( บาท )</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col6'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>( บาท )</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-statistic-payment-by-date-col7'><div class='table-head-line1'>รูปแบบ</div><div>การชำระหนี้</div></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-statistic-payment-by-date-content'>" +
                 "  <div class='box4' id='list-data-report-statistic-payment-by-date'></div>" +
                 "  <div id='nav-page-report-statistic-payment-by-date'></div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportEContract
{    
    public static string ListCPReportEContract(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _iconStatus1 = String.Empty;
        string _iconStatus2 = String.Empty;
        string _iconStatus3 = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountCPReportEContract(_c);
     
        if (_recordCount > 0)
        {
            _data = eCPDB.ListCPReportEContract(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _iconStatus1 = eCPUtil._iconEContractStatus[Util.FindIndexArray2D(0, eCPUtil._iconEContractStatus, _data[_i, 8]) - 1, 1];
                _iconStatus2 = eCPUtil._iconEContractStatus[Util.FindIndexArray2D(0, eCPUtil._iconEContractStatus, _data[_i, 9]) - 1, 1];
                _iconStatus3 = eCPUtil._iconEContractStatus[Util.FindIndexArray2D(0, eCPUtil._iconEContractStatus, _data[_i, 10]) - 1, 1];
                _groupNum = !_data[_i, 7].Equals("0") ? " ( กลุ่ม " + _data[_i, 7] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-e-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-e-contract-col1' onclick><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col2' onclick><div>" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col3' onclick><div>" + _data[_i, 2] + _data[_i, 3] + " " + _data[_i, 4] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col4' onclick><div><span class='programcode-col'>" + _data[_i, 5] + "</span>- " + _data[_i, 6] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col5' onclick>" +
                         "      <div class='icon-status1-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus1 + "'>" + (_data[_i, 8].Equals("1") ? "<a href='javascript:void(0)' onclick=ShowDocEContract('" + _data[_i, 1] + "','" + _data[_i, 11] + "','" + _data[_i, 12] + "')></a>" : "") + "</li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col6' onclick>" +
                         "      <div class='icon-status2-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus2 + "'>" + (_data[_i, 9].Equals("1") ? "<a href='javascript:void(0)' onclick=ShowDocEContract('" + _data[_i, 1] + "','" + _data[_i, 11] + "','" + _data[_i, 13] + "')></a>" : "") + "</li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "  <li class='table-col' id='table-content-cp-report-e-contract-col7' onclick>" +
                         "      <div class='icon-status3-style'>" +
                         "          <ul>" +
                         "              <li class='" + _iconStatus3 + "'>" + (_data[_i, 10].Equals("1") ? "<a href='javascript:void(0)' onclick=ShowDocEContract('" + _data[_i, 1] + "','" + _data[_i, 11] + "','" + _data[_i, 14] + "')></a>" : "") + "</li>" +
                         "          </ul>" +
                         "      </div>" +
                         "  </li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportecontract", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }
        
        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportEContract()
    {
        string _html = String.Empty;

        _html += "<div id='cp-report-e-contract-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-report-e-contract") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='content-data-tab-content'>" +
                 "          <div class='content-left'>" +
                 "              <input type='hidden' id='search-report-e-contract' value=''>" +
                 "              <input type='hidden' id='acadamicyear-report-e-contract-hidden' value='" + int.Parse(DateTime.Parse(Util.CurrentDate("MM/dd/yyyy")).ToString("yyyy", new CultureInfo("th-TH"))) + "'>" +
                 "              <input type='hidden' id='id-name-report-e-contract-hidden' value=''>" +
                 "              <input type='hidden' id='faculty-report-e-contract-hidden' value=''>" +
                 "              <input type='hidden' id='program-report-e-contract-hidden' value=''>" +
                 "              <div class='button-style2'>" +
                 "                  <ul>" +
                 "                      <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportecontract',true,'','','')>ค้นหา</a></li>" +
                 "                  </ul>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='content-right'>" +
                 "              <div class='content-data-tab-content-msg' id='record-count-cp-report-e-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div class='tab-line'></div>" +
                 "      <div class='box-search-condition' id='search-report-e-contract-condition'>" +
                 "          <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "          <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order1'>" +
                 "              <div class='box-search-condition-order-title'>ปีการศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order1-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order2'>" +
                 "              <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order2-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order3'>" +
                 "              <div class='box-search-condition-order-title'>คณะ</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order3-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "          <div class='box-search-condition-order search-report-e-contract-condition-order' id='search-report-e-contract-condition-order4'>" +
                 "              <div class='box-search-condition-order-title'>หลักสูตร</div>" +
                 "              <div class='box-search-condition-split-title-value'>:</div>" +
                 "              <div class='box-search-condition-order-value' id='search-report-e-contract-condition-order4-value'></div>" +
                 "              <div class='clear'></div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-report-e-contract-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col4'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col5'><div class='table-head-line1'>สัญญานักศึกษา</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col6'><div class='table-head-line1'>หนังสือยินยอม ฯ</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "              <li class='table-col' id='table-head-cp-report-e-contract-col7'><div class='table-head-line1'>สัญญาค้ำประกัน</div><div><a href='javascript:void(0)' onclick=LoadForm(1,'detailecontractstatus',true,'','','')>( ความหมาย )</a></div></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-e-contract-content'>" +
                 "  <div class='box4' id='list-data-report-e-contract'></div>" +
                 "  <div id='nav-page-report-e-contract'></div>" +
                 "</div>";

        return _html;
    }
}

public class eCPDataReportDebtorContract
{    
    private static string ExportCPReportDebtorContractSearchCondition(string _exportSend, string _reportOrder)
    {
        string _html = String.Empty;
        string _font = "Cordia New";
        string _fontSize = "13";
        int _colSpan = 0;
        char[] _separator;

        _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _idName = _exportSendValue[2];

        _separator = new char[] { ';' };
        string[] _faculty = (!String.IsNullOrEmpty(_exportSendValue[3]) ? _exportSendValue[3].Split(_separator) : new string[0]);
        string[] _program = (!String.IsNullOrEmpty(_exportSendValue[4]) ? _exportSendValue[4].Split(_separator) : new string[0]);
        string[] _formatPayment = (!String.IsNullOrEmpty(_exportSendValue[5]) ? _exportSendValue[5].Split(_separator) : new string[0]);

        switch (_reportOrder)
        {
            case "reportdebtorcontract":
                _colSpan = 18;
                break;
            case "reportdebtorcontractpaid":
                _colSpan = 19;
                break;
            case "reportdebtorcontractremain":
                _colSpan = 21;
                break; 
        }

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>";

        if (!String.IsNullOrEmpty(_idName) || _faculty.GetLength(0) > 0 || _program.GetLength(0) > 0)
        {
            _html += "<tr>" +
                     "  <td align='left' colspan='" + _colSpan + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ค้นหาตามเงื่อนไข</div></td>" +
                     "</tr>";
            
            if (!String.IsNullOrEmpty(_idName))
            {
                _html += "<tr>" +
                         "  <td align='left' colspan='2'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รหัส / ชื่อ - นามสกุลนักศึกษา</div></td>" +
                         "  <td align='left' colspan='" + (_colSpan - 2) + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>: " + _idName + "</div></td>" +
                         "</tr>";
            }

            if (_faculty.GetLength(0) > 0)
            {
                _html += "<tr>" +
                         "  <td align='left' colspan='2'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คณะ</div></td>" +
                         "  <td align='left' colspan='" + (_colSpan - 2) + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>: " + _faculty[0] + " - " + _faculty[1] + "</div></td>" +
                         "</tr>";
            }

            if (_program.GetLength(0) > 0)
            {
                _html += "<tr>" +
                         "  <td align='left' colspan='2'><div style='font:normal " + _fontSize + "pt " + _font + ";'>หลักสูตร</div></td>" +
                         "  <td align='left' colspan='" + (_colSpan - 2) + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>: " + _program[0] + " - " + _program[1] + (!_program[3].Equals("0") ? " ( กลุ่ม " + _program[3] + " )" : "") + "</div></td>" +
                         "</tr>";
            }

            if (_formatPayment.GetLength(0) > 0)
            {
                _html += "<tr>" +
                         "  <td align='left' colspan='2'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รูปแบบการชำระหนี้</div></td>" +
                         "  <td align='left' colspan='" + (_colSpan - 2) + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>: " + _formatPayment[1] + "</div></td>" +
                         "</tr>";
            }

            _html += "<tr>" +
                     "  <td align='center' colspan='" + _colSpan + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                     "</tr>";
        }

        _html += "  <tr>" +
                 "      <td align='right' colspan='" + _colSpan + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รายงาน ณ วันที่ " + Util.ConvertDateTH(Util.CurrentDate("MM/dd/yyyy")) + " เวลา " + Util.ConvertTimeTH((DateTime.Now).ToString()) + "</div></td>" +
                 "  </tr>" +
                 "</table>";

        return _html;
    }

    public static void ExportCPReportDebtorContractRemain(string _exportSend)
    {
        string _html = String.Empty;
        string _font = "Cordia New";
        string _fontSize = "13";
        string _borderStyle1 = String.Empty;
        string _borderStyle2 = String.Empty;
        string _borderStyle3 = String.Empty;
        string _borderStyle4 = String.Empty;
        string _borderStyle5 = String.Empty;
        string _borderStyle6 = String.Empty;
        string _borderStyle7 = String.Empty;
        string _borderStyle8 = String.Empty;
        string _borderStyle9 = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _order = String.Empty;
        string _fullName = String.Empty;
        string _program = String.Empty;
        string _allActualDate = String.Empty;
        string _workDateStart = String.Empty;
        string _workDateEnd = String.Empty;
        string _requireDate = String.Empty;
        string _approveDate = String.Empty;
        string _actualDate = String.Empty;
        string _remainDate = String.Empty;
        string[,] _data;
        int _i, _j;

        _borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        _borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle3 = "border-left:thin solid #000000;";
        _borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle7 = "border-top:thin solid #000000;";
        _borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle9 = "border:thin solid #000000;";

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='21'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รายงานลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดลคงค้าง</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='21'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(_dateStart) + " ถึงวันที่ " + Util.LongDateTH(_dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='21'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>" +
                 ExportCPReportDebtorContractSearchCondition(_exportSend, "reportdebtorcontractremain") +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลำดับที่</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ชื่อ - สกุล</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>หลักสูตร</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงื่อนไขกำหนดชดใช้ตามสัญญา</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลาปฏิบัติงานชดใช้</div></td>" +
                 "      <td align='center' colspan='4' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คงเหลือระยะเวลาปฏิบัติงานตามสัญญา</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คิดเป็นเงินที่ต้องชดใช้จำนวน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>วันเดือนปีรับสภาพหนี้</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินที่ต้องชดใช้คงเหลือ</div></td>" +
                 "      <td align='center' style='" + _borderStyle2 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รูปแบบการชำระหนี้</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลา</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลา</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>จำนวนเงิน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เริ่มต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>สิ้นสุด</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คำนวณวัน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เริ่มต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>สิ้นสุด</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คำนวณวัน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คงเหลือ</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวมจำนวนเงิน</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( ตามที่ได้รับหนังสือ )</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวม</div></td>" +
                 "      <td align='center' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( ปี )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle6 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>";
        
        _data = eCPDB.ListExportReportDebtorContractRemain(_exportSend);
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");

        if (_data.GetLength(0) > 0)
        {
            _j = 1;
            for (_i = 0; _i < (_data.GetLength(0) - 1); _i++)
            {
                _order = (_j++).ToString("#,##0");
                _fullName = (_data[_i, 1] + _data[_i, 2] + " " + _data[_i, 3]);
                _program = (_data[_i, 6] + " - " + _data[_i, 7] + (!_data[_i, 9].Equals("0") ? "( กลุ่ม " + _data[_i, 9] + " )" : ""));
                _allActualDate = (_data[_i, 12].Equals("1") ? _data[_i, 14] : String.Empty);

                if (_data[_i, 12].Equals("1"))
                {
                    _allActualDate = _data[_i, 14];
                    _workDateStart = _data[_i, 16];
                    _workDateEnd = DateTime.Parse(_workDateStart, _provider).AddDays(double.Parse(_allActualDate)).ToString();
                    _requireDate = _data[_i, 16];
                    _approveDate = _data[_i, 17];
                    _actualDate = _data[_i, 18];
                    _remainDate = _data[_i, 19];
                }
                else
                {
                    _allActualDate = String.Empty;
                    _workDateStart = String.Empty;
                    _workDateEnd = String.Empty;
                    _requireDate = String.Empty;
                    _approveDate = String.Empty;
                    _actualDate = String.Empty;
                    _remainDate = String.Empty;
                }

                _html += "<tr>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _order + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _fullName + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _program + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 13]) ? double.Parse(_data[_i, 13]).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_allActualDate) ? double.Parse(_allActualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 15]) ? double.Parse(_data[_i, 15]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_workDateStart) ? Util.ConvertDateTH(DateTime.Parse(_workDateStart, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_workDateEnd) ? Util.ConvertDateTH(_workDateEnd) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_allActualDate) ? double.Parse(_allActualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_requireDate) ? Util.ConvertDateTH(DateTime.Parse(_requireDate, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_approveDate) ? Util.ConvertDateTH(DateTime.Parse(_approveDate, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_actualDate) ? double.Parse(_actualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_remainDate) ? double.Parse(_remainDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 20]) ? double.Parse(_data[_i, 20]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>0</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 20]) ? double.Parse(_data[_i, 20]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 21]) ? Util.ConvertDateTH(DateTime.Parse(_data[_i, 21], _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 22]) ? double.Parse(_data[_i, 22]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 24]) ? double.Parse(_data[_i, 24]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 25]) ? double.Parse(_data[_i, 25]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 27]) ? _data[_i, 27] : "-") + "</div></td>" +
                         "</tr>";
            }
        
            _html += "<tr>" +
                     "  <td align='right' colspan='13' style='" + _borderStyle7 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวม</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 32]) ? double.Parse(_data[_i, 32]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>0</div></td>" +
                     "  <td align='right' style='" + _borderStyle9 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 32]) ? double.Parse(_data[_i, 32]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='center' style='" + _borderStyle7 + "'></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 33]) ? double.Parse(_data[_i, 33]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 35]) ? double.Parse(_data[_i, 35]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle9 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 36]) ? double.Parse(_data[_i, 36]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='center' style='" + _borderStyle7 + "' style='border-right:0px;border-bottom:0px'></td>" +
                     "</tr>";
        }

        _html += "</table>" +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContractRemain.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Write(_html);
    }

    public static void ExportCPReportDebtorContractPaid(string _exportSend)
    {
        string _html = String.Empty;
        string _font = "Cordia New";
        string _fontSize = "13";
        string _borderStyle1 = String.Empty;
        string _borderStyle2 = String.Empty;
        string _borderStyle3 = String.Empty;
        string _borderStyle4 = String.Empty;
        string _borderStyle5 = String.Empty;
        string _borderStyle6 = String.Empty;
        string _borderStyle7 = String.Empty;
        string _borderStyle8 = String.Empty;
        string _borderStyle9 = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _studentIDOld = String.Empty;
        string _order = String.Empty;
        string _fullName = String.Empty;
        string _program = String.Empty;
        string[,] _data;
        int _i, _j;

        _borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        _borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle3 = "border-left:thin solid #000000;";
        _borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle7 = "border-top:thin solid #000000;";
        _borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle9 = "border:thin solid #000000;";

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='19'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รายงานการรับชำระเงินจากลูกหนี้ตามการผิดสัญญาการศึกษามหาวิทยาลัยมหิดล</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='19'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(_dateStart) + " ถึงวันที่ " + Util.LongDateTH(_dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='19'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>" +
                 ExportCPReportDebtorContractSearchCondition(_exportSend, "reportdebtorcontractpaid") +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลำดับที่</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ชื่อ - สกุล</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>หลักสูตร</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>วันเดือนปีรับสภาพหนี้</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>วันเดือนปีที่ชำระเงิน</div></td>" +
                 "      <td align='center' colspan='2' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คิดเป็นเงินที่ต้องชดใช้ยกมา</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวมจำนวนเงิน</div></td>" +
                 "      <td align='center' colspan='5' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รับชำระเงินชดใช้ ( รายละเอียดตามใบเสร็จรับเงิน )</div></td>" +
                 "      <td align='center' colspan='2' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>กองคลังรับ</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินที่ต้องชดใช้คงเหลือยกไป</div></td>" +
                 "      <td align='center' style='" + _borderStyle2 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รูปแบบการชำระหนี้</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( ตามที่ได้รับหนังสือ )</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ที่ต้องชดใช้</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>วันเดือนปี</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เล่มที่ / เลขที่</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวมรับชำระ</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เลขที่ใบสำคัญรับ</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>นำเข้ากองทุน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวม</div></td>" +
                 "      <td align='center' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle6 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>";

        _data = eCPDB.ListExportReportDebtorContractPaid(_exportSend);
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");

        if (_data.GetLength(0) > 0)
        {                        
            _j = 1;
            for (_i = 0; _i < (_data.GetLength(0) - 1); _i++)
            {                            
                if (!_data[_i, 0].Equals(_studentIDOld))
                {
                    _order = (_j++).ToString("#,##0");
                    _fullName = (_data[_i, 1] + _data[_i, 2] + " " + _data[_i, 3]);
                    _program = (_data[_i, 6] + " - " + _data[_i, 7] + (!_data[_i, 9].Equals("0") ? "( กลุ่ม " + _data[_i, 9] + " )" : ""));
                }
                else
                {
                    _order = "&nbsp;";
                    _fullName = "&nbsp;";
                    _program = "&nbsp;";
                }

                _html += "<tr>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _order + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _fullName + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _program + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 12]) ? Util.ConvertDateTH(DateTime.Parse(_data[_i, 12], _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 13]) ? Util.ConvertDateTH(DateTime.Parse(_data[_i, 13], _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 14]) ? double.Parse(_data[_i, 14]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 17]) ? double.Parse(_data[_i, 17]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 18]) ? double.Parse(_data[_i, 18]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 19]) ? Util.ConvertDateTH(DateTime.Parse(_data[_i, 19], _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 20]) && !String.IsNullOrEmpty(_data[_i, 21]) ? (_data[_i, 20] + " / " + _data[_i, 21]) : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 22]) ? double.Parse(_data[_i, 22]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 23]) ? double.Parse(_data[_i, 23]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 24]) ? double.Parse(_data[_i, 24]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 25]) ? _data[_i, 25] : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 26]) ? _data[_i, 26] : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 27]) ? double.Parse(_data[_i, 27]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 29]) ? double.Parse(_data[_i, 29]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 30]) ? double.Parse(_data[_i, 30]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 32]) ? _data[_i, 32] : "-") + "</div></td>" +
                         "</tr>";

                _studentIDOld = _data[_i, 0];
            }

            _html += "<tr>" +
                     "  <td align='right' colspan='5' style='" + _borderStyle7 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวม</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 37]) ? double.Parse(_data[_i, 37]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 38]) ? double.Parse(_data[_i, 38]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle9 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 39]) ? double.Parse(_data[_i, 39]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' colspan='2' style='" + _borderStyle7 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 40]) ? double.Parse(_data[_i, 40]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 41]) ? double.Parse(_data[_i, 41]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle9 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 42]) ? double.Parse(_data[_i, 42]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' colspan='2' style='" + _borderStyle7 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 43]) ? double.Parse(_data[_i, 43]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 44]) ? double.Parse(_data[_i, 44]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 45]) ? double.Parse(_data[_i, 45]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                     "</tr>";
        }

        _html += "</table>" +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='19'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='19'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div></td>" +
                 "      <td align='center' colspan='12'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='12'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div></td>" +
                 "      <td align='center' colspan='12'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='12'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContractPaid.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Write(_html);
    }

    public static void ExportCPReportDebtorContract(string _exportSend)
    {
        string _html = String.Empty;
        string _font = "Cordia New";
        string _fontSize = "13";
        string _borderStyle1 = String.Empty;
        string _borderStyle2 = String.Empty;
        string _borderStyle3 = String.Empty;
        string _borderStyle4 = String.Empty;
        string _borderStyle5 = String.Empty;
        string _borderStyle6 = String.Empty;
        string _borderStyle7 = String.Empty;
        string _borderStyle8 = String.Empty;
        string _borderStyle9 = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _order = String.Empty;
        string _fullName = String.Empty;
        string _program = String.Empty;
        string _allActualDate = String.Empty;
        string _workDateStart = String.Empty;
        string _workDateEnd = String.Empty;
        string _requireDate = String.Empty;
        string _approveDate = String.Empty;
        string _actualDate = String.Empty;
        string _remainDate = String.Empty;
        string[,] _data;
        int _i, _j;

        _borderStyle1 = "border-left:thin solid #000000;border-top:thin solid #000000;";
        _borderStyle2 = "border-left:thin solid #000000;border-top:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle3 = "border-left:thin solid #000000;";
        _borderStyle4 = "border-left:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle5 = "border-left:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle6 = "border-left:thin solid #000000;border-bottom:thin solid #000000;border-right:thin solid #000000;";
        _borderStyle7 = "border-top:thin solid #000000;";
        _borderStyle8 = "border-left:thin solid #000000;border-top:thin solid #000000;border-bottom:thin solid #000000;";
        _borderStyle9 = "border:thin solid #000000;";

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รายงานลูกหนี้ผิดสัญญาการศึกษามหาวิทยาลัยมหิดล</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd) ? ("วันที่รับสภาพหนี้ตั้งแต่วันที่ " + Util.LongDateTH(_dateStart) + " ถึงวันที่ " + Util.LongDateTH(_dateEnd)) : "ไม่กำหนดช่วงวันที่รับสภาพหนี้") + "</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>" +
                 ExportCPReportDebtorContractSearchCondition(_exportSend, "reportdebtorcontract") +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลำดับที่</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ชื่อ - สกุล</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>หลักสูตร</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงื่อนไขกำหนดชดใช้ตามสัญญา</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลาปฏิบัติงานชดใช้</div></td>" +
                 "      <td align='center' colspan='4' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คงเหลือระยะเวลาปฏิบัติงานตามสัญญา</div></td>" +
                 "      <td align='center' colspan='3' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คิดเป็นเงินที่ต้องชดใช้จำนวน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>วันเดือนปีรับสภาพหนี้</div></td>" +
                 "      <td align='center' style='" + _borderStyle2 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รูปแบบการชำระหนี้</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลา</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ระยะเวลา</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>จำนวนเงิน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เริ่มต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>สิ้นสุด</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คำนวณวัน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เริ่มต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>สิ้นสุด</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คำนวณวัน</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>คงเหลือ</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>เงินต้น</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ดอกเบี้ย</div></td>" +
                 "      <td align='center' style='" + _borderStyle1 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวมจำนวนเงิน</div></td>" +
                 "      <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( ตามที่ได้รับหนังสือ )</div></td>" +
                 "      <td align='center' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( ปี )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( วัน )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>( บาท )</div></td>" +
                 "      <td align='center' style='" + _borderStyle5 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='center' style='" + _borderStyle6 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>";
        
        _data = eCPDB.ListExportReportDebtorContract(_exportSend);
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");

        if (_data.GetLength(0) > 0)
        {
            _j = 1;
            for (_i = 0; _i < (_data.GetLength(0) - 1); _i++)
            {
                _order = (_j++).ToString("#,##0");
                _fullName = (_data[_i, 1] + _data[_i, 2] + " " + _data[_i, 3]);
                _program = (_data[_i, 6] + " - " + _data[_i, 7] + (!_data[_i, 9].Equals("0") ? "( กลุ่ม " + _data[_i, 9] + " )" : ""));
                _allActualDate = (_data[_i, 12].Equals("1") ? _data[_i, 14] : String.Empty);
                            
                if (_data[_i, 12].Equals("1"))
                {
                    _allActualDate = _data[_i, 14];
                    _workDateStart = _data[_i, 16];
                    _workDateEnd = DateTime.Parse(_workDateStart, _provider).AddDays(double.Parse(_allActualDate)).ToString();
                    _requireDate = _data[_i, 16];
                    _approveDate = _data[_i, 17];
                    _actualDate = _data[_i, 18];
                    _remainDate = _data[_i, 19];
                }
                else
                {
                    _allActualDate = String.Empty;
                    _workDateStart = String.Empty;
                    _workDateEnd = String.Empty;
                    _requireDate = String.Empty;
                    _approveDate = String.Empty;
                    _actualDate = String.Empty;
                    _remainDate = String.Empty;
                }

                _html += "<tr>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _order + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _fullName + "</div></td>" +
                         "  <td align='left' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + _program + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 13]) ? double.Parse(_data[_i, 13]).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_allActualDate) ? double.Parse(_allActualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 15]) ? double.Parse(_data[_i, 15]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_workDateStart) ? Util.ConvertDateTH(DateTime.Parse(_workDateStart, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_workDateEnd) ? Util.ConvertDateTH(_workDateEnd) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_allActualDate) ? double.Parse(_allActualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_requireDate) ? Util.ConvertDateTH(DateTime.Parse(_requireDate, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +                
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_approveDate) ? Util.ConvertDateTH(DateTime.Parse(_approveDate, _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_actualDate) ? double.Parse(_actualDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_remainDate) ? double.Parse(_remainDate).ToString("#,##0") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 20]) ? double.Parse(_data[_i, 20]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>0</div></td>" +
                         "  <td align='right' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 20]) ? double.Parse(_data[_i, 20]).ToString("#,##0.00") : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle3 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 21]) ? Util.ConvertDateTH(DateTime.Parse(_data[_i, 21], _provider).ToString("yyyy/MM/dd")) + "&nbsp;" : "-") + "</div></td>" +
                         "  <td align='center' style='" + _borderStyle4 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 23]) ? _data[_i, 23] : "-") + "</div></td>" +
                         "</tr>";
            }
        
            _html += "<tr>" +
                     "  <td align='right' colspan='13' style='" + _borderStyle7 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>รวม</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 28]) ? double.Parse(_data[_i, 28]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='right' style='" + _borderStyle8 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>0</div></td>" +
                     "  <td align='right' style='" + _borderStyle9 + "'><div style='font:normal " + _fontSize + "pt " + _font + ";'>" + (!String.IsNullOrEmpty(_data[_i, 28]) ? double.Parse(_data[_i, 28]).ToString("#,##0.00") : "-") + "</div></td>" +
                     "  <td align='center' style='" + _borderStyle7 + "' colspan='2' style='border-right:0px;border-bottom:0px'></td>" +
                     "</tr>";
        }

        _html += "</table>" +
                 "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center' colspan='18'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้จัดทำข้อมูล</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>ลงชื่อ................................................................ผู้อำนวยการกองกฎหมาย</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "  <tr>" +
                 "      <td align='center'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "      <td align='left' colspan='6'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(.................................................................)</div></td>" +
                 "      <td align='center' colspan='11'><div style='font:normal " + _fontSize + "pt " + _font + ";'>&nbsp;</div></td>" +
                 "  </tr>" +
                 "</table>";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DebtorContract.xls");
        HttpContext.Current.Response.ContentType = "application/msexcel";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Write(_html);
    }
 
    public static string ViewTransPayment(string _cp2idDate)
    {
        string _html = String.Empty;
        char[] _separator = new char[] { ':' };
        string[] _cp2idDate1 = _cp2idDate.Split(_separator);
        string _cp2id = _cp2idDate1[0];
        string _dateStart = _cp2idDate1[1];
        string _dateEnd = _cp2idDate1[2];
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListTransPayment(_cp2id, "", "");
        _recordCount = _data.GetLength(0);
        
        if (_recordCount > 0)
        {
            _html += "<div id='view-trans-payment'>" +
                     "  <input type='hidden' id='period-hidden' value=''>" +
                     "  <div class='tab-line'></div>" +
                     "  <div class='content-data-tab-content'>" +
                     "      <div class='content-left'><div class='content-data-tab-content-msg'>ช่วงวันที่รับสภาพหนี้ " + (!String.IsNullOrEmpty(_dateStart) && !String.IsNullOrEmpty(_dateEnd) ? ("ระหว่างวันที่ " + _dateStart + " ถึงวันที่ " + _dateEnd) : "ไม่กำหนด") + "</div></div>" +
                     "      <div class='content-right'><div class='content-data-tab-content-msg'>ค้นหาพบ " + _recordCount + " รายการ</div></div>" +
                     "  </div>" +
                     "  <div class='clear'></div>" +
                     "  <div class='tab-line'></div>" +
                     "  <div class='box3'>" +
                     "      <div class='table-head'>" +
                     "          <ul>" +
                     "              <li id='table-head-trans-payment-col1'><div class='table-head-line1'>งวดที่</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col2'><div class='table-head-line1'>เงินต้น</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col3'><div class='table-head-line1'>ดอกเบี้ย</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col4'><div class='table-head-line1'>เงินต้น</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col5'><div class='table-head-line1'>ดอกเบี้ย</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col6'><div class='table-head-line1'>ยอดเงิน</div><div>รับชำระ</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col7'><div class='table-head-line1'>เงินต้น</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col8'><div class='table-head-line1'>ดอกเบี้ย</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col9'><div class='table-head-line1'>ดอกเบี้ย</div><div>ค้างจ่าย</div><div>( บาท )</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col10'><div class='table-head-line1'>วันเดือนปี</div><div>ที่รับชำระหนี้</div></li>" +
                     "              <li class='table-col' id='table-head-trans-payment-col11'><div class='table-head-line1'>จ่ายชำระด้วย</div></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "      <div class='clear'></div>" +
                     "  </div>" +
                     "  <div id='box-list-trans-payment'><div id='list-trans-payment'>" + eCPDataPayment.ListTransPayment(_data) + "</div></div>" +
                     "</div>";
        }

        return _html;
    }

    public static string ListCPReportDebtorContractByProgram(HttpContext _c)
    {
        string _html = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        string _trackingStatus = String.Empty;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        _recordCount = eCPDB.CountCPReportDebtorContractByProgram(_c);        
        
        if (_recordCount > 0)
        {
            _data = eCPDB.ListCPReportDebtorContractByProgram(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _trackingStatus = _data[_i, 18] + _data[_i, 19] + _data[_i, 20] + _data[_i, 21];                
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewTrackingStatusViewTransBreakContract('" + _data[_i, 1] + "','" + _trackingStatus + "','v3')";
                _html += "<ul class='table-row-content " + _highlight + "' id='trans-break-contract" + _data[_i, 1] + "'>" +
                         "  <li id='table-content-cp-report-debtor-contract-by-program-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col2' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col3' onclick=" + _callFunc + "><div>" + _data[_i, 4] + _data[_i, 5] + " " + _data[_i, 6] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col4' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 15]) ? _data[_i, 15] : "-") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 11]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 12]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col7' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 13]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col8' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 14]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-by-program-col9' onclick=" + _callFunc + "><div>" + (!String.IsNullOrEmpty(_data[_i, 17]) ? _data[_i, 17] : "-") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "reportdebtorcontractbyprogram", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }

    public static string ListCPReportDebtorContract(HttpContext _c)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = eCPDB.CountCPReportDebtorContract(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListCPReportDebtorContract(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _groupNum = !_data[_i, 6].Equals("0") ? " ( กลุ่ม " + _data[_i, 6] + " )" : "";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _callFunc = "ViewReportDebtorContractByProgram('" + _data[_i, 1] + "','" + _data[_i, 2].Replace(" ", "&") + "','" + _data[_i, 3] + "','" + _data[_i, 4].Replace(" ", "&") + "','" + _data[_i, 5] + "','" + _data[_i, 6] + "','" + _data[_i, 7] + "','" + _data[_i, 8] + "')";
                _html += "<ul class='table-row-content " + _highlight + "' id='report-debtor-contract" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-report-debtor-contract-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col2' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 3] + "</span>- " + _data[_i, 4] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col3' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 9]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 10]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col5' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 11]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col6' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 12]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-report-debtor-contract-col7' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 13]).ToString("#,##0.00") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav><pagenav>";
    }

    public static string TabCPReportDebtorContract(string _reportOrder)
    {
        string _html = String.Empty;
        string _tabName = String.Empty;
        string _title = String.Empty;

        switch (_reportOrder)
        {
            case "reportdebtorcontract":
                _tabName = "ลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้";
                _title = "cp-report-debtor-contract";
                break;
            case "reportdebtorcontractpaid":
                _tabName = "การรับชำระเงินจากลูกหนี้ผิดสัญญาการศึกษาที่ยอมรับสภาพหนี้";
                _title = "cp-report-debtor-contract-paid";
                break;
            case "reportdebtorcontractremain":
                _tabName = "ลูกหนี้ผิดสัญญาการศึกษาคงค้างที่ยอมรับสภาพหนี้";
                _title = "cp-report-debtor-contract-remain";
                break;
        }

        _html += "<div id='cp-report-debtor-contract-head'>" +
                 "  <input type='hidden' id='report-debtor-contract-order' value='" + _reportOrder + "'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle(_title) +
                 "      <div class='content-data-tabs' id='tabs-cp-report-debtor-contract'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-report-debtor-contract' alt='#tab1-cp-report-debtor-contract' href='javascript:void(0)'>" + _tabName + "</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab2-cp-report-debtor-contract' alt='#tab2-cp-report-debtor-contract' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-report-debtor-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-report-debtor-contract' value=''>" +
                 "                  <input type='hidden' id='date-start-report-debtor-contract-hidden' value=''>" +
                 "                  <input type='hidden' id='date-end-report-debtor-contract-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcpreportdebtorcontract',true,'','','')>ค้นหา</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=PrintDebtorContract(1)>ส่งออก</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-debtor-contract'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-report-debtor-contract-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-report-debtor-contract-condition-order' id='search-report-debtor-contract-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>ช่วงวันที่รับสภาพหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-debtor-contract-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-report-debtor-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-report-debtor-contract-by-program' value=''>" +
                 "                  <input type='hidden' id='id-name-report-debtor-contract-by-program-hidden' value=''>" +
                 "                  <input type='hidden' id='faculty-report-debtor-contract-by-program-hidden' value=''>" +
                 "                  <input type='hidden' id='program-report-debtor-contract-by-program-hidden' value=''>" +
                 "                  <input type='hidden' id='format-payment-report-debtor-contract-by-program-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchstudentdebtorcontractbyprogram',true,'','','')>ค้นหา</a></li>" +
                 "                          <li><a href='javascript:void(0)' onclick=PrintDebtorContract(2)>ส่งออก</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-report-debtor-contract-by-program'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-report-debtor-contract-by-program-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>ช่วงวันที่รับสภาพหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order2'>" +
                 "                  <div class='box-search-condition-order-title'>รหัส / ชื่อ - นามสกุลนักศึกษา</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order2-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "              <div class='box-search-condition-order search-report-debtor-contract-by-program-condition-order' id='search-report-debtor-contract-by-program-condition-order3'>" +
                 "                  <div class='box-search-condition-order-title'>รูปแบบการชำระหนี้</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-report-debtor-contract-by-program-condition-order3-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-report-debtor-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-debtor-contract-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col2'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col3'><div class='table-head-line1'>จำนวนลูกหนี้</div><div>ผิดสัญญา</div><div>&nbsp;</div><div>( คน )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col4'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col5'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col6'><div class='table-head-line1'>ยอดดอกเบี้ย</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-col7'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-debtor-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-report-debtor-contract-by-program-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col2'><div class='table-head-line1'>รหัส</div><div>นักศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col3'><div class='table-head-line1'>ชื่อ - นามสกุล</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col4'><div class='table-head-line1'>รับสภาพหนี้</div><div>เมื่อ</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col5'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col6'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col7'><div class='table-head-line1'>ยอดดอกเบี้ย</div><div>ที่รับชำระ</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col8'><div class='table-head-line1'>ยอดเงินต้น</div><div>ที่ต้องชดใช้</div><div>คงเหลือ</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-report-debtor-contract-by-program-col9'><div class='table-head-line1'>รูปแบบ</div><div>การชำระหนี้</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-report-debtor-contract-content'>" +
                 "  <div class='tab-content' id='tab1-cp-report-debtor-contract-content'>" +
                 "      <div class='box4' id='list-data-report-debtor-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-report-debtor-contract-content'>" +
                 "      <div class='box4' id='list-data-report-debtor-contract-by-program'></div>" +
                 "      <div id='nav-page-report-debtor-contract-by-program'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<iframe class='export-target' id='export-target' name='export-target'></iframe>" +
                 "<form id='export-setvalue' method='post' target='export-target'>" +
                 "  <input id='export-send' name='export-send' value='' type='hidden' />" +
                 "  <input id='export-order' name='export-order' value='' type='hidden' />" +
                 "  <input id='export-type' name='export-type' value='' type='hidden' />" +
                 "</form>";                 

        return _html;
    }
}

public class eCPDataReportCertificateReimbursement
{
    private static string ExportCPReportCertificateReimbursementSection(int _section, string _font, Dictionary<string, string> _lawyer)
    {
        string _html = String.Empty;
        string _lawyerFullname = String.Empty;
        string _lawyerPhoneNumber = String.Empty;
    
        if (_lawyer != null)
        {
            _lawyerFullname = _lawyer["Fullname"];
            _lawyerPhoneNumber = _lawyer["PhoneNumber"];
        }

        if (_section.Equals(1))
        {
            _html += "<tr>" +
                     "  <td width='100%' height='110' align='center'><img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' /></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%' align='right'>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>กองกฎหมาย สำนักงานอธิการบดี</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>โทร. " + Util.NumberArabicToThai(_lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <div style='font:normal 15pt " + _font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘.๐๑๙/</div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>วันที่</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรื่อง</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>การชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(2))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดพิจารณาลงนามในหนังสือ ที่เสนอมาพร้อมนี้" +
                     "      </p>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(3))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "          <tr>" +
                     "              <td width='98'></td>" +
                     "              <td width='502' style='text-align: center'>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>(นาย/นาง/นางสาว" + _lawyerFullname + ")</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>นิติกร</div></td>" +
                     "              </td>" +
                     "          </tr>" +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(4))
        {
            _html += "<tr>" +
                     "  <td width='100%' height='110' align='center'><img src='" + Util.GetApplicationPath() + "/Image/LogoMU.png' /></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%' align='right'>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>สำนักงานอธิการบดี มหาวิทยาลัยมหิดล</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>๙๙๙ ถ.พุทธมณฑลสาย ๔ ต.ศาลายา</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>อ.พุทธมณฑล จ.นครปฐม ๗๓๑๗๐</div>" +
                     "      <div align='right' style='font:normal 15pt " + _font + ";'>โทร. " + Util.NumberArabicToThai(_lawyerPhoneNumber) + " โทรสาร ๐ ๒๘๔๙ ๖๒๖๕</div>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <div style='font:normal 15pt " + _font + ";'>ที่&nbsp;&nbsp;&nbsp;อว ๗๘/</div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>วันที่</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Util._longMonth[int.Parse(Util.CurrentDate("MM")) - 1, 0] + "&nbsp;&nbsp;" + Util.NumberArabicToThai((int.Parse(Util.CurrentDate("yyyy")) + 543).ToString()) + "</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "      <div>" +
                     "          <table border='0' cellpadding='0' cellspacing='0'>" +
                     "              <tr>" +
                     "                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรื่อง</div></td>" +
                     "                  <td width='550'><div style='font:normal 15pt " + _font + ";'>การชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษา</div></td>" +
                     "              </tr>" +
                     "          </table>" +
                     "      </div>" +
                     "  </td>" +
                     "</tr>";
        }

        if (_section.Equals(5))
        {
            _html += "<tr>" +
                     "  <td width='100%'>" +
                     "      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;จึงเรียนมาเพื่อโปรดทราบ" +
                     "      </p>" +
                     "  </td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "</tr>" +
                     "<tr>" +
                     "  <td width='100%'>" +
                     "      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "          <tr>" +
                     "              <td width='200'></td>" +
                     "              <td width='400'>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ขอแสดงความนับถือ</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div style='font:normal 15pt " + _font + ";'>&nbsp;</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>(นายคณพศ เฟื่องฟุ้ง)</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ผู้อำนวยการกองกฎหมาย</div>" +
                     "                  <div align='center' style='font:normal 15pt " + _font + ";'>ปฏิบัติหน้าที่แทนอธิการบดีมหาวิทยาลัยมหิดล</div>" +
                     "              </td>" +
                     "          </tr>" +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        return _html;
    }

    public static void ExportCPReportCertificateReimbursement(string _exportSend)
    {
        string _html = String.Empty;
        string _width = "600";
        string _font = "TH SarabunPSK, TH Sarabun New";
        char[] _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _cp2id = _exportSendValue[0];    
        string[,] _data1;

        _data1 = eCPDB.ListDetailPaymentOnCPTransRequireContract(_cp2id);
    
        Dictionary<string, string> _lawyer = new Dictionary<string, string>();
        _lawyer.Add("Fullname", (!String.IsNullOrEmpty(_data1[0, 29]) ? _data1[0, 29] : String.Empty));
        _lawyer.Add("PhoneNumber", (!String.IsNullOrEmpty(_data1[0, 30]) ? _data1[0, 30] : _data1[0, 31]));
        _lawyer.Add("Email", (!String.IsNullOrEmpty(_data1[0, 32]) ? _data1[0, 32] : String.Empty));

        _html += "<table align='center' border='0' cellpadding='0' cellspacing='0'>" +
                 "  <tr>" +
                 "      <td width='" + _width + "' valign='top'>" +
                 "          <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportCertificateReimbursementSection(1, _font, _lawyer) +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <div>" +
                 "                          <table border='0' cellpadding='0' cellspacing='0'>" +
                 "                              <tr>" +
                 "                                  <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                 "                                  <td width='550'><div style='font:normal 15pt " + _font + ";'>ผู้อำนวยการกองกฎหมาย</div></td>" +
                 "                              </tr>" +
                 "                          </table>" +
                 "                      </div>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามที่" + _data1[0, 10] + _data1[0, 11] + " " + _data1[0, 12] + " " +
                 "                          ได้ชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data1[0, 14]) + " ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data1[0, 28])) + " " +
                 "                          ให้แก่มหาวิทยาลัยครบถ้วนแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงินที่แนบมานี้ นั้น" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'>" +
                 "                      <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                 "                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ในการนี้ งานกฎหมายและนิติกรรมสัญญาจึงเห็นควรให้มหาวิทยาลัยออกหนังสือเพื่อเป็นหลักฐานการพ้นภาระผูกพันตามสัญญาฯ " +
                 "                          ให้แก่บุคคลดังกล่าวด้วย" +
                 "                      </p>" +
                 "                  </td>" +
                 "              </tr>" +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportCertificateReimbursementSection(2, _font, null) +
                 "              <tr>" +
                 "                  <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                 "              </tr>" +
                                ExportCPReportCertificateReimbursementSection(3, _font, _lawyer) +
                 "          </table>" +
                 "      </td>" +
                 "  </tr>";

        string _formatPayment = _data1[0, 8];
        string _pursuant = String.Empty;
        string _pursuantBookDate = String.Empty;
        string[,] _data2;

        _data2 = eCPDB.ListCPTransRepayContractNoCurrentStatusRepay(_cp2id, "2");
        if (_data2.GetLength(0) > 0)
        {
            _pursuant = _data2[0, 6];
            _pursuantBookDate = _data2[0, 7];
        }
    
        bool _overpayment = false;
        string _receiptNo = String.Empty;
        string _receiptBookNo = String.Empty;
        string _receiptDate = String.Empty;
        string[,] _data3;
        string[,] _data4;

        _data3 = eCPDB.ListTransPayment(_cp2id, "", "");

        if (_data3.GetLength(0) > 0)
        {
            _html += "<tr>" +
                     "  <td width='" + _width + "' valign='top'>" +
                     "      <table width='100%' border='0' cellpadding='0' cellspacing='0'>" +
                                ExportCPReportCertificateReimbursementSection(4, _font, _lawyer) +
                     "          <tr>" +
                     "              <td width='100%'>" +
                     "                  <div>" +
                     "                      <table border='0' cellpadding='0' cellspacing='0'>" +
                     "                          <tr>" +
                     "                              <td width='50'><div style='font:normal 15pt " + _font + ";'>เรียน</div></td>" +
                     "                              <td width='550'><div style='font:normal 15pt " + _font + ";'>" + _data1[0, 10] + _data1[0, 11] + " " + _data1[0, 12] + "</div></td>" +
                     "                          </tr>" +
                     "                      </table>" +
                     "                  </div>";
                     
            if (_formatPayment.Equals("1"))
            {
                _html += "              <div>" +
                         "                  <table border='0' cellpadding='0' cellspacing='0'>" +
                         "                      <tr>" +
                         "                          <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                         "                          <td width='550'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!String.IsNullOrEmpty(_pursuant) ? Util.NumberArabicToThai(_pursuant) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_pursuantBookDate)) : "") + "</div></td>" +
                         "                      </tr>" +
                         "                  </table>" +
                         "              </div>";                 

                _data4 = eCPDB.ListDetailTransPayment(_data3[0, 1]);

                if (_data4.GetLength(0) > 0)
                {
                    _overpayment = (!String.IsNullOrEmpty(_data4[0, 8]) ? true : false);
                    _receiptNo = (!String.IsNullOrEmpty(_data4[0, 28]) ? _data4[0, 28] : String.Empty);
                    _receiptBookNo = (!String.IsNullOrEmpty(_data4[0, 29]) ? _data4[0, 29] : String.Empty);
                    _receiptDate = (!String.IsNullOrEmpty(_data4[0, 30]) ? _data4[0, 30] : String.Empty);

                    _html += "          <div>" +
                             "              <table border='0' cellpadding='0' cellspacing='0'>" +
                             "                  <tr>" +
                             "                      <td width='98' valign='top'><div style='font:normal 15pt " + _font + ";'>สิ่งที่ส่งมาด้วย</div></td>" +
                             "                      <td width='502'><div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>ใบเสร็จรับเงิน เล่มที่ " + (!String.IsNullOrEmpty(_receiptBookNo) ? Util.NumberArabicToThai(_receiptBookNo) : "") + " เลขที่ " + (!String.IsNullOrEmpty(_receiptNo) ? Util.NumberArabicToThai(_receiptNo) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_receiptDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_receiptDate)) : "") + "</div></td>" +
                             "                  </tr>" +
                             "              </table>" +
                             "          </div>";
                }

                _html += "          </td>" +
                         "      </tr>" +
                         "      <tr>" +
                         "          <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                         "      </tr>" +
                         "      <tr>" +
                         "          <td width='100%'>" +
                         "              <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                         "                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data1[0, 14]) + " " +
                         "                  ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data1[0, 28])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(_data1[0, 4]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data1[0, 4]) + ") " +
                         "                  ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว นั้น" +
                         "              </p>" +
                         "          </td>" +
                         "      </tr>" +
                         "      <tr>" +
                         "          <td width='100%'>";

                if (_overpayment.Equals(false))
                {
                    _html += "          <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                             "              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นมาชำระให้แก่มหาวิทยาลัยมหิดลจนครบถ้วน " +
                             "              เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามสิ่งที่ส่งมาด้วย" +
                             "          </p>";
                }
                else
                {
                    _html += "          <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                             "              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นพร้อมทั้งดอกเบี้ยผิดนัดมาชำระให้แก่มหาวิทยาลัยจนครบถ้วน " +
                             "              เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงิน ที่แนบมาพร้อมนี้" +
                             "          </p>";
                }

                _html += "          </td>" +
                         "      </tr>";
            }

            if (_formatPayment.Equals("2"))
            {
                _html += "              <div>" +
                         "                  <table border='0' cellpadding='0' cellspacing='0'>" +
                         "                      <tr>" +
                         "                          <td width='50' valign='top'><div style='font:normal 15pt " + _font + ";'>อ้างถึง</div></td>" +
                         "                          <td width='550'>" +
                         "                              <div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>๑. หนังสือมหาวิทยาลัยมหิดล ที่ อว ๗๘/" + (!String.IsNullOrEmpty(_pursuant) ? Util.NumberArabicToThai(_pursuant) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_pursuantBookDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_pursuantBookDate)) : "") + "</div>" +
                         "                              <div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>๒. หนังสือรับสภาพหนี้ ฉบับลงวันที่</div>" +
                         "                          </td>" +
                         "                      </tr>" +
                         "                  </table>" +
                         "              </div>" +
                         "              <div>" +
                         "                  <table border='0' cellpadding='0' cellspacing='0'>" +
                         "                      <tr>" +
                         "                          <td width='98' valign='top'><div style='font:normal 15pt " + _font + ";'>สิ่งที่ส่งมาด้วย</div></td>" +
                         "                          <td width='502'>";

            int _i;

            for (_i = 0; _i < _data3.GetLength(0); _i++)
            {
                _data4 = eCPDB.ListDetailTransPayment(_data3[_i, 1]);

                if (_data4.GetLength(0) > 0)
                {
                    _receiptNo = (!String.IsNullOrEmpty(_data4[0, 28]) ? _data4[0, 28] : String.Empty);
                    _receiptBookNo = (!String.IsNullOrEmpty(_data4[0, 29]) ? _data4[0, 29] : String.Empty);
                    _receiptDate = (!String.IsNullOrEmpty(_data4[0, 30]) ? _data4[0, 30] : String.Empty);

                    _html += "                          <div style='font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" + Util.NumberArabicToThai((_i + 1).ToString()) + ". สำเนาใบเสร็จรับเงิน เล่มที่ " + (!String.IsNullOrEmpty(_receiptBookNo) ? Util.NumberArabicToThai(_receiptBookNo) : "") + " เลขที่ " + (!String.IsNullOrEmpty(_receiptNo) ? Util.NumberArabicToThai(_receiptNo) : "") + " ลงวันที่ " + (!String.IsNullOrEmpty(_receiptDate) ? Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_receiptDate)) : "") + "</div>";
                }
            }

            _html += "                              </td>" +
                     "                          </tr>" +
                     "                      </table>" +
                     "                  </div>" +
                     "              </td>" +
                     "          </tr>" +
                     "          <tr>" +
                     "              <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "          </tr>" +
                     "          <tr>" +
                     "              <td width='100%'>" +
                     "                  <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ตามหนังสือที่อ้างถึง ๑. มหาวิทยาลัยมหิดลแจ้งให้ท่านชดใช้เงินกรณีผิดสัญญาการเป็นนักศึกษาเพื่อศึกษาวิชา" + eCPDataReport.ReplaceProgramToShortProgram(_data1[0, 14]) + " " +
                     "                      ฉบับลงวันที่ " + Util.ThaiLongDateWithNumberTH(Util.ConvertDateEN(_data1[0, 28])) + " เป็นจำนวนเงิน " + Util.NumberArabicToThai(double.Parse(_data1[0, 4]).ToString("#,##0.00")) + " บาท (" + Util.ThaiBaht(_data1[0, 4]) + ") " +
                     "                      ให้แก่มหาวิทยาลัยมหิดล ภายใน ๓๐ วันนับถัดจากวันที่ได้รับหนังสือดังกล่าว และท่านได้ตกลงผ่อนชำระเงินให้แก่มหาวิทยาลัยตามหนังสือที่อ้างถึง ๒. นั้น" +
                     "                  </p>" +
                     "              </td>" +
                     "          </tr>" +
                     "          <tr>" +
                     "              <td width='100%'>" +
                     "                  <p style='text-wrap:normal;font:normal 15pt " + _font + ";text-align:justify;text-justify:inter-cluster;'>" +
                     "                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;มหาวิทยาลัยมหิดลตรวจสอบแล้ว ขอเรียนว่า การที่ท่านได้นำเงินจำนวนข้างต้นพร้อมทั้งดอกเบี้ยการผ่อนชำระเงินมาชำระให้แก่มหาวิทยาลัยจนครบถ้วน " +
                     "                      เป็นการพ้นภาระผูกพันตามสัญญาดังกล่าวแล้ว รายละเอียดปรากฏตามใบเสร็จรับเงิน ที่แนบมาพร้อมนี้" +
                     "                  </p>" +
                     "              </td>" +
                     "          </tr>";
            }

            _html += "          <tr>" +
                     "              <td width='100%'><div style='font:normal 15pt " + _font + ";'>&nbsp;</div></td>" +
                     "          </tr>" +
                                ExportCPReportCertificateReimbursementSection(5, _font, null) +
                     "      </table>" +
                     "  </td>" +
                     "</tr>";
        }

        _html += "</table>";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=CertificateReimbursement.doc");
        HttpContext.Current.Response.ContentType = "application/msword";
        HttpContext.Current.Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.Write(_html);    
    }
} 