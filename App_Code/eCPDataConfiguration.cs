/*
Description         : สำหรับการตั้งค่าระบบ
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๐๙/๐๔/๒๕๖๔
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Web;

public class eCPDataConfiguration
{
    public static string AddUpdateCPTabProgram(string _action, string[,] _data)
    {
        string _html = String.Empty;
        int _i;
        string _cp1id = _action.Equals("update") ? _data[0, 0] : String.Empty;
        string _dlevelDefault = _action.Equals("update") ? _data[0, 7] : "0";
        string _facultyDefault = _action.Equals("update") ? _data[0, 1] + ";" + _data[0, 2] : "0";
        string _programDefault = _action.Equals("update") ? _data[0, 3] + ";" + _data[0, 4] + ";" + _data[0, 5] + ";" + _data[0, 6] + ";" + _data[0, 7] + ";" + _data[0, 8] : "0";

        _html += "<div class='form-content' id='" + _action + "-cp-tab-program'>" +
                 "  <div id='addupdate-cp-tab-program'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +
                 "      <input type='hidden' id='dlevel-hidden' value='" + _dlevelDefault + "' />" +
                 "      <input type='hidden' id='faculty-hidden' value='" + _facultyDefault + "' />" +
                 "      <input type='hidden' id='program-hidden' value='" + _programDefault + "' />" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='dlevel-label'>" +
                 "                  <div class='form-label-style'>ระดับการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='dlevel-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='dlevel'>" +
                 "                          <option value='0'>เลือกระดับการศึกษา</option>";

        for (_i = 0; _i < eCPUtil._dlevel.GetLength(0); _i++)
        {
            _html += "                      <option value='" + eCPUtil._dlevel[_i, 1] + "'>" + eCPUtil._dlevel[_i, 0] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='faculty-program-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตรที่ให้มีการทำสัญญาการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ให้มีการทำสัญญาการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='faculty-program-input'>" +
                                    eCPUtil.ListFaculty(false, "faculty") +
                 "                  <div id='list-program'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabProgram('" + _action + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTabProgram(false)'>ล้าง</a></li>";

        if (_action.Equals("update"))
            _html += "          <li class='hide-button'><a href='javascript:void(0)' onclick=ConfirmActionCPTabProgram('del')>ลบ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-program')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-program')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddCPTabProgram()
    {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTabProgram("add", _data);

        return _html;
    }

    public static string UpdateCPTabProgram(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListCPTabProgram(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTabProgram("update", _data);

        return _html;
    }

    public static string ListCPTabProgram(string[,] _data)
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
                _groupNum = !_data[_i, 6].Equals("0") ? " ( กลุ่ม " + _data[_i, 6] + " )" : "";
                _callFunc = "OpenTab('link-tab3-cp-tab-program','#tab3-cp-tab-program','ปรับปรุงหลักสูตร',false,'update','" + _data[_i, 0] + "','')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";

                _html += "<ul class='table-row-content " + _highlight + "' id='program" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-tab-program-col1' onclick=" + _callFunc + "><div>" + _data[_i, 8] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-program-col2' onclick=" + _callFunc + "><div><span class='facultycode-col'>" + _data[_i, 1] + "</span>- " + _data[_i, 2] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-program-col3' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 3] + "</span>- " + _data[_i, 4] + _groupNum + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListUpdateCPTabProgram()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabProgram("");
        _recordCount = _data.GetLength(0);
        _html += ListCPTabProgram(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPTabProgram()
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabProgram("");
        _recordCount = _data.GetLength(0);

        _html += "<div id='cp-tab-program-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-program") +
                 "      <div class='content-data-tabs' id='tabs-cp-tab-program'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-tab-program' alt='#tab1-cp-tab-program' href='javascript:void(0)'>แสดงหลักสูตร</a></li>" +
                 "                  <li><a id='link-tab2-cp-tab-program' alt='#tab2-cp-tab-program' href='javascript:void(0)'>เพิ่มหลักสูตร</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-tab-program' alt='#tab3-cp-tab-program' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-tab-program-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-program'>ค้นหาพบ " + _recordCount.ToString("#,##0") + " รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-tab-program-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-tab-program-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-tab-program-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-tab-program-col1'>ระดับการศึกษา</li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-program-col2'>คณะ</li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-program-col3'>หลักสูตร</li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-program-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-program-contents'></div>" +
                 "</div>" +
                 "<div id='cp-tab-program-content'>" +
                 "  <div class='tab-content' id='tab1-cp-tab-program-content'>" +
                 "      <div class='box4' id='list-data-tab-program'>" + ListCPTabProgram(_data) + "</div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-program-content'>" +
                 "      <div class='box1 addupdate-data-tab-program' id='add-data-tab-program'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-program-content'>" +
                 "      <div class='box1 addupdate-data-tab-program' id='update-data-tab-program'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string DetailCPTabCalDate(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListCPTabCalDate(_cp1id);

        if (_data.GetLength(0) > 0)
        {
            _html += "<div class='form-content' id='detail-cp-tab-cal-date'>" +
                     "  <div>" +
                     "      <div class='form-label-discription-style'>" +
                     "          <div class='form-label-style'>" + _data[0, 1] + "</div>" +
                     "      </div>" +
                     "      <div class='form-input-style'><img src='Image/CalDateFormula" + _data[0, 2] + ".png' /></div>" +
                     "  </div>" +
                     "  <div class='button'>" +
                     "      <div class='button-style1'>" +
                     "          <ul>" +
                     "              <li><a href='javascript:void(0)' onclick=CloseFrm(true,'')>ปิด</a></li>" +
                     "          </ul>" +
                     "      </div>" +
                     "  </div>" +
                     "</div>";
        }

        return _html;
    }

    public static string ListCPTabCalDate(string[,] _data)
    {
        string _html = String.Empty;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _callFunc = "LoadForm(1,'detailcptabcaldate',true,'','" + _data[_i, 0] + "','cal-date" + _data[_i, 0] + "')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='cal-date" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-tab-cal-date-col1' onclick=" + _callFunc + "><div>" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-cal-date-col2' onclick=" + _callFunc + "><div>สูตรที่ " + _data[_i, 2] + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListCPTabCalDate()
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabCalDate("");
        _recordCount = _data.GetLength(0);

        _html += "<div id='cp-tab-cal-date-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-cal-date") +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div id='tab1-cp-trans-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'></div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count'>ค้นหาพบ " + _recordCount.ToString("#,##0") + " รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='box3'>" +
                 "      <div class='table-head'>" +
                 "          <ul>" +
                 "              <li id='table-head-cp-tab-cal-date-col1'>เงื่อนไขการคิดระยะเวลาตามสัญญา</li>" +
                 "              <li class='table-col' id='table-head-cp-tab-cal-date-col2'>สูตรคำนวณเงินชดใช้ตามสัญญา</li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "</div>" +
                 "<div id='cp-tab-cal-date-content'>" +
                 "  <div class='box4' id='list-data'>" + ListCPTabCalDate(_data) + "</div>" +
                 "</div>";

        return _html;
    }

    private static string AddUpdateCPTabInterest(string _action, string[,] _data)
    {
        string _html = String.Empty;
        string _cp1id = _action.Equals("update") ? _data[0, 0] : String.Empty;
        string _inContractInterestDefault = _action.Equals("update") ? double.Parse(_data[0, 1]).ToString("#,##0.00") : String.Empty;
        string _outContractInterestDefault = _action.Equals("update") ? double.Parse(_data[0, 2]).ToString("#,##0.00") : String.Empty;

        _html += "<div class='form-content' id='" + _action + "-cp-tab-interest'>" +
                 "  <div id='addupdate-cp-tab-interest'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +
                 "      <input type='hidden' id='in-contract-interest-hidden' value='" + _inContractInterestDefault + "' />" +
                 "      <input type='hidden' id='out-contract-interest-hidden' value='" + _outContractInterestDefault + "' />" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='in-contract-interest-label'>" +
                 "                  <div class='form-label-style'>ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ดอกเบี้ยเป็นอัตราร้อยละ ( ต่อปี )</div>" +
                 "                      <div class='form-discription-line2-style'>ใส่เป็นตัวเลขจุดทศนิยม 2 ตำแหน่ง</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='in-contract-interest-input'><input class='inputbox textbox-numeric' type='text' id='in-contract-interest' onblur=Trim('in-contract-interest');AddCommas('in-contract-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:200px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div id='clear-bottom'>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='out-contract-interest-label'>" +
                 "                  <div class='form-label-style'>ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ดอกเบี้ยเป็นอัตราร้อยละ ( ต่อปี )</div>" +
                 "                      <div class='form-discription-line2-style'>ใส่เป็นตัวเลขจุดทศนิยม 2 ตำแหน่ง</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='out-contract-interest-input'><input class='inputbox textbox-numeric' type='text' id='out-contract-interest' onblur=Trim('out-contract-interest');AddCommas('out-contract-interest',2) onkeyup='ExtractNumber(this,2,false)' onkeypress='return BlockNonNumbers(this,event,true,false)' value='' style='width:200px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabInterest('" + _action + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTabInterest(false)'>ล้าง</a></li>";

        if (_action.Equals("update"))
            _html += "          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabInterest('del')>ลบ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-interest')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-interest')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddCPTabInterest()
    {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTabInterest("add", _data);

        return _html;
    }

    public static string UpdateCPTabInterest(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListCPTabInterest(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTabInterest("update", _data);

        return _html;
    }

    public static string ListCPTabInterest(string[,] _data)
    {
        string _html = String.Empty;
        string _highlight = String.Empty;
        string _check = String.Empty;
        string _useContractInterest = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;        
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _callFunc = "OpenTab('link-tab3-cp-tab-interest','#tab3-cp-tab-interest','ปรับปรุงรายการดอกเบี้ย',false,'update','" + _data[_i, 0] + "','')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _useContractInterest = int.Parse(_data[_i, 3]).Equals(1) ? "&radic;" : "&nbsp;";
                _check = (int.Parse(_data[_i, 3])).Equals(1) ? "checked" : String.Empty;
                _html += "<ul class='table-row-content " + _highlight + "' id='contract-interest" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-tab-interest-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 1]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-interest-col2' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 2]).ToString("#,##0.00") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-interest-col3'><div><input class='checkbox' type='checkbox' name='use-contract-interest' " + _check + " onclick=SingleSelectCheckbox(this);UpdateUseContractInterest('" + _data[_i, 0] + "') /></div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListUpdateCPTabInterest()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabInterest("");
        _recordCount = _data.GetLength(0);
        _html += ListCPTabInterest(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPTabInterest()
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabInterest("");
        _recordCount = _data.GetLength(0);

        _html += "<div id='cp-tab-interest-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-interest") +
                 "      <div class='content-data-tabs' id='tabs-cp-tab-interest'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-tab-interest' alt='#tab1-cp-tab-interest' href='javascript:void(0)'>แสดงรายการดอกเบี้ย</a></li>" +
                 "                  <li><a id='link-tab2-cp-tab-interest' alt='#tab2-cp-tab-interest' href='javascript:void(0)'>เพิ่มรายการดอกเบี้ย</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-tab-interest' alt='#tab3-cp-tab-interest' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-tab-interest-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-interest'>ค้นหาพบ " + _recordCount.ToString("#,##0") + " รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-tab-interest-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-tab-interest-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-tab-interest-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-tab-interest-col1'><div class='table-head-line1'>ดอกเบี้ยจากการผิดนัดชำระที่กำหนดไว้ในสัญญา</div><div>อัตราร้อยละ ( ต่อปี )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-interest-col2'><div class='table-head-line1'>ดอกเบี้ยจากการผิดนัดชำระที่มิได้กำหนดไว้ในสัญญา</div><div>อัตราร้อยละ ( ต่อปี )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-interest-col3'><div class='table-head-line1'>สถานะการใช้งาน</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-interest-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-interest-contents'></div>" +
                 "</div>" +
                 "<div id='cp-tab-interest-content'>" +
                 "  <div class='tab-content' id='tab1-cp-tab-interest-content'>" +
                 "      <div class='box4' id='list-data-tab-interest'>" + ListCPTabInterest(_data) + "</div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-interest-content'>" +
                 "      <div class='box1 addupdate-data-tab-interest' id='add-data-tab-interest'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-interest-content'>" +
                 "      <div class='box1 addupdate-data-tab-interest' id='update-data-tab-interest'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddUpdateCPTabPayBreakContract(string _action, string[,] _data)
    {        
        string _html = String.Empty;
        int _i;
        string _cp1id = _action.Equals("update") ? _data[0, 0] : String.Empty;        
        string _dlevelDefault = _action.Equals("update") ? _data[0, 8] : "0";
        string _caseGgraduateDefault = _action.Equals("update") ? _data[0, 10] : "0";
        string _facultyDefault = _action.Equals("update") ? _data[0, 1] + ";" + _data[0, 2] : "0";
        string _programDefault = _action.Equals("update") ? _data[0, 3] + ";" + _data[0, 4] + ";" + _data[0, 5] + ";" + _data[0, 6] + ";" + _data[0, 8] + ";" + _data[0, 9] : "0";
        string _amountCashDefault = _action.Equals("update") ? double.Parse(_data[0, 7]).ToString("#,##0") : String.Empty;
        string _amtIndemnitorYearDefault = ((_action.Equals("update")) && (!_data[0, 15].Equals("0"))) ? double.Parse(_data[0, 15]).ToString("#,##0") : String.Empty;
        string _calDateConditionDefault = _action.Equals("update") ? _data[0, 12] : "0";

        _html += "<div class='form-content' id='" + _action + "-cp-tab-pay-break-contract'>" +
                 "  <div id='addupdate-cp-tab-pay-break-contract'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +
                 "      <input type='hidden' id='dlevel-hidden' value='" + _dlevelDefault + "' />" +
                 "      <input type='hidden' id='case-graduate-hidden' value='" + _caseGgraduateDefault + "' />" +
                 "      <input type='hidden' id='faculty-hidden' value='" + _facultyDefault + "' />" +
                 "      <input type='hidden' id='program-hidden' value='" + _programDefault + "' />" +
                 "      <input type='hidden' id='amount-cash-hidden' value='" + _amountCashDefault + "' />" +
                 "      <input type='hidden' id='amt-indemnitor-year-hidden' value='" + _amtIndemnitorYearDefault + "' />" +
                 "      <input type='hidden' id='cal-date-condition-hidden' value='" + _calDateConditionDefault + "' />" +                 
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='dlevel-label'>" +
                 "                  <div class='form-label-style'>ระดับการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='dlevel-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='dlevel'>" +
                 "                          <option value='0'>เลือกระดับการศึกษา</option>";

        for (_i = 0; _i < eCPUtil._dlevel.GetLength(0); _i++)
        {
            _html += "                      <option value='" + eCPUtil._dlevel[_i, 1] + "'>" + eCPUtil._dlevel[_i, 0] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='case-graduate-label'>" +
                 "                  <div class='form-label-style'>การชดใช้ตามสัญญากรณีก่อน / หลังสำเร็จการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกกรณีการชดใช้ตามสัญญา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='case-graduate-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='case-graduate'>" +
                 "                          <option value='0'>เลือกกรณีการชดใช้ตามสัญญา</option>";

        for (_i = 0; _i < eCPUtil._caseGraduate.GetLength(0); _i++)
        {
            _html += "                      <option value='" + (_i + 1) + "'>" + eCPUtil._caseGraduate[_i, 0] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='faculty-program-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตรที่ต้องทำสัญญา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ต้องทำสัญญา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='faculty-program-input'>" +
                                    eCPUtil.ListFaculty(true, "facultycptabprogram") +
                 "                  <div id='list-program'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='amount-cash-label'>" +
                 "                  <div class='form-label-style'>จำนวนเงินชดใช้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่จำนวนเงินชดใช้ ( บาท )</div>" +
                 "                      <div class='form-discription-line2-style'>กรณีก่อนสำเร็จการศึกษา ใส่จำนวนเงินชดใช้ต่อเดือน</div>" +
                 "                      <div class='form-discription-line3-style'>กรณีหลังสำเร็จการศึกษา ใส่จำนวนเงินชดใช้ตามสัญญา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='amount-cash-input'><input class='inputbox textbox-numeric' type='text' id='amount-cash' onblur=Trim('amount-cash');AddCommas('amount-cash',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='amt-indemnitor-year-label'>" +
                 "                  <div class='form-label-style'>ระยะเวลาทำงานชดใช้หลังสำเร็จการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ระยะเวลาทำงานชดใช้ ( ปี )</div>" +
                 "                      <div class='form-discription-line2-style'>กรณีหลังสำเร็จการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='amt-indemnitor-year-input'>" +
                 "                  <div id='set-amt-indemnitor-year'>" +
                 "                      <div>" +
                 "                          <div class='content-left' id='amt-indemnitor-year-no-input'><input class='radio' type='radio' name='set-amt-indemnitor-year' value='N' /></div>" +
                 "                          <div class='content-left' id='amt-indemnitor-year-no-label'>ไม่กำหนด</div>" +
                 "                      </div>" +
                 "                      <div class='clear'></div>" +
                 "                      <div>" +
                 "                          <div class='content-left' id='amt-indemnitor-year-yes-input'><input class='radio' type='radio' name='set-amt-indemnitor-year' value='Y' /></div>" +
                 "                          <div class='content-left' id='amt-indemnitor-year-yes-label'>กำหนด</div>" +
                 "                      </div>" +
                 "                      <div class='clear'></div>" +
                 "                  </div>" +
                 "                  <input class='inputbox textbox-numeric' type='text' id='amt-indemnitor-year' onblur=Trim('amt-indemnitor-year');AddCommas('amt-indemnitor-year',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' />" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div id='clear-bottom'>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='cal-date-condition-label'>" +
                 "                  <div class='form-label-style'>วิธีคิดและคำนวณเงินชดใช้</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกเงื่อนไขการคิดระยะเวลาตามสัญญาและ</div>" +
                 "                      <div class='form-discription-line2-style'>สูตรคำนวณเงินชดใช้ตามสัญญา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='cal-date-condition-input'>" +                                    
                 "                  <div>" +
                 "                      <div id='input-cal-date'>" + eCPUtil.ListCalDate("cal-date-condition") + "</div>" +
                 "                      <div id='view-cal-date'>" +
                 "                          <div class='button-style2'>" +
                 "                              <ul>" +
                 "                                  <li><a href='javascript:void(0)' onclick=ViewCalDate('')>ดูสูตรคำนวณ</a></li>" +
                 "                              </ul>" +
                 "                          </div>" +
                 "                      </div>" +
                 "                  </div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabPayBreakContract('" + _action + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTabPayBreakContract(false)'>ล้าง</a></li>";

        if (_action.Equals("update"))
            _html += "          <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabPayBreakContract('del')>ลบ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-pay-break-contract')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-pay-break-contract')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddCPTabPayBreakContract()
    {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTabPayBreakContract("add", _data);

        return _html;
    }

    public static string UpdateCPTabPayBreakContract(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListCPTabPayBreakContract(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTabPayBreakContract("update", _data);

        return _html;
    }

    public static string ListSearchCPTabPayBreakContract(HttpContext _c)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _error;

        _data = eCPDB.ListSearchCPTabPayBreakContract(_c);
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _error = 0;
            _html += _data[0, 1].ToString() + ";" + double.Parse(_data[0, 2]).ToString("#,##0") + ";" + double.Parse(_data[0, 3]).ToString("#,##0");
        }
        else
            _error = 1;

        return "<error>" + _error + "<error><list>" + _html + "<list>";
    }

    public static string ListCPTabPayBreakContract(string[,] _data)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {            
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _groupNum = !_data[_i, 6].Equals("0") ? " ( กลุ่ม " + _data[_i, 6] + " )" : "";
                _callFunc = "OpenTab('link-tab3-cp-tab-pay-break-contract','#tab3-cp-tab-pay-break-contract','ปรับปรุงเกณฑ์การชดใช้',false,'update','" + _data[_i, 0] + "','')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='pay-break-contract" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-tab-pay-break-contract-col1' onclick=" + _callFunc + "><div>" + _data[_i, 11] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-pay-break-contract-col2' onclick=" + _callFunc + "><div>" + _data[_i, 9] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-pay-break-contract-col3' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 3] + "</span>- " + _data[_i, 4] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-pay-break-contract-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 7]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-pay-break-contract-col5' onclick=" + _callFunc + "><div>" + (_data[_i, 15].Equals("0") ? "-" : double.Parse(_data[_i, 15]).ToString("#,##0")) + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-pay-break-contract-col6' onclick=" + _callFunc + "><div>วิธีคิดที่ " + _data[_i, 12] + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }

    public static string ListUpdateCPTabPayBreakContract()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabPayBreakContract("");
        _recordCount = _data.GetLength(0);
        _html += ListCPTabPayBreakContract(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPTabPayBreakContract()
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;        

        _data = eCPDB.ListCPTabPayBreakContract("");
        _recordCount = _data.GetLength(0);

        _html += "<div id='cp-tab-pay-break-contract-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-pay-break-contract") +
                 "      <div class='content-data-tabs' id='tabs-cp-tab-pay-break-contract'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-tab-pay-break-contract' alt='#tab1-cp-tab-pay-break-contract' href='javascript:void(0)'>แสดงเกณฑ์การชดใช้</a></li>" +
                 "                  <li><a id='link-tab2-cp-tab-pay-break-contract' alt='#tab2-cp-tab-pay-break-contract' href='javascript:void(0)'>เพิ่มเกณฑ์การชดใช้</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-tab-pay-break-contract' alt='#tab3-cp-tab-pay-break-contract' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-tab-pay-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-pay-break-contract'>ค้นหาพบ " + _recordCount.ToString("#,##0") + " รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-tab-pay-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-tab-pay-break-contract-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-tab-pay-break-contract-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-tab-pay-break-contract-col1'><div class='table-head-line1'>ก่อน / หลัง</div><div>สำเร็จการศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-pay-break-contract-col2'><div class='table-head-line1'>ระดับการศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-pay-break-contract-col3'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-pay-break-contract-col4'><div class='table-head-line1'>จำนวนเงินชดใช้</div><div>&nbsp;</div><div>( บาท )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-pay-break-contract-col5'><div class='table-head-line1'>ระยะเวลาชดใช้</div><div>หลังสำเร็จการศึกษา</div><div>( ปี )</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-pay-break-contract-col6'><div class='table-head-line1'>วิธีคิดและ</div><div>คำนวณเงินชดใช้</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-pay-break-contract-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-pay-break-contract-contents'></div>" +
                 "</div>" +
                 "<div id='cp-tab-pay-break-contract-content'>" +
                 "  <div class='tab-content' id='tab1-cp-tab-pay-break-contract-content'>" +
                 "      <div class='box4' id='list-data-tab-pay-break-contract'>" + ListCPTabPayBreakContract(_data) + "</div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-pay-break-contract-content'>" +
                 "      <div class='box1 addupdate-data-tab-pay-break-contract' id='add-data-tab-pay-break-contract'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-pay-break-contract-content'>" +
                 "      <div class='box1 addupdate-data-tab-pay-break-contract' id='update-data-tab-pay-break-contract'></div>" +
                 "  </div>" +                 
                 "</div>";

        return _html;
    }

    public static string AddUpdateCPTabScholarship(string _action, string[,] _data)
    {
        string _html = String.Empty;
        int _i;
        string _cp1id = _action.Equals("update") ? _data[0, 0] : String.Empty;
        string _dlevelDefault = _action.Equals("update") ? _data[0, 8] : "0";
        string _facultyDefault = _action.Equals("update") ? _data[0, 1] + ";" + _data[0, 2] : "0";
        string _programDefault = _action.Equals("update") ? _data[0, 3] + ";" + _data[0, 4] + ";" + _data[0, 5] + ";" + _data[0, 6] + ";" + _data[0, 8] + ";" + _data[0, 9] : "0";
        string _scholarshipMoneyDefault = _action.Equals("update") ? double.Parse(_data[0, 7]).ToString("#,##0") : String.Empty;

        _html += "<div class='form-content' id='" + _action + "-cp-tab-scholarship'>" +
                 "  <div id='addupdate-cp-tab-scholarship'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='cp1id' value='" + _cp1id + "' />" +
                 "      <input type='hidden' id='dlevel-hidden' value='" + _dlevelDefault + "' />" +
                 "      <input type='hidden' id='faculty-hidden' value='" + _facultyDefault + "' />" +
                 "      <input type='hidden' id='program-hidden' value='" + _programDefault + "' />" +
                 "      <input type='hidden' id='scholarship-money-hidden' value='" + _scholarshipMoneyDefault + "' />" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='dlevel-label'>" +
                 "                  <div class='form-label-style'>ระดับการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกระดับการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='dlevel-input'>" +
                 "                  <div class='combobox'>" +
                 "                      <select id='dlevel'>" +
                 "                          <option value='0'>เลือกระดับการศึกษา</option>";

        for (_i = 0; _i < eCPUtil._dlevel.GetLength(0); _i++)
        {
            _html += "                      <option value='" + eCPUtil._dlevel[_i, 1] + "'>" + eCPUtil._dlevel[_i, 0] + "</option>";
        }

        _html += "                      </select>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='faculty-program-label'>" +
                 "                  <div class='form-label-style'>คณะและหลักสูตรที่ให้ทุนการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาเลือกคณะและหลักสูตรที่ให้ทุนการศึกษา</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='faculty-program-input'>" +
                                    eCPUtil.ListFaculty(true, "facultycptabprogram") +
                 "                  <div id='list-program'></div>" +
                 "              </div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div id='clear-bottom'>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='scholarship-money-label'>" +
                 "                  <div class='form-label-style'>จำนวนเงินทุนการศึกษา</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่จำนวนเงินทุนการศึกษา ( บาท / หลักสูตร )</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='scholarship-money-input'><input class='inputbox textbox-numeric' type='text' id='scholarship-money' onblur=Trim('scholarship-money');AddCommas('scholarship-money',0) onkeyup='ExtractNumber(this,0,false)' onkeypress='return BlockNonNumbers(this,event,false,false)' value='' style='width:221px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabScholarship('" + _action + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTabScholarship(false)'>ล้าง</a></li>";

        if (_action.Equals("update"))
            _html += "          <li class='hide-button'><a href='javascript:void(0)' onclick=ConfirmActionCPTabScholarship('del')>ลบ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-scholarship')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-scholarship')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }

    public static string AddCPTabScholarship()
    {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTabScholarship("add", _data);

        return _html;
    }

    public static string UpdateCPTabScholarship(string _cp1id)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListCPTabScholarship(_cp1id);

        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTabScholarship("update", _data);

        return _html;
    }

    public static string ListSearchCPTabScholarship(HttpContext _c)
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;
        int _error;

        _data = eCPDB.ListSearchCPTabScholarship(_c);
        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _error = 0;
            _html += double.Parse(_data[0, 1]).ToString("#,##0");
        }
        else
            _error = 1;

        return "<error>" + _error + "<error><list>" + _html + "<list>";
    }

    public static string ListCPTabScholarship(string[,] _data)
    {
        string _html = String.Empty;
        string _groupNum = String.Empty;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _recordCount;
        int _i;

        _recordCount = _data.GetLength(0);

        if (_recordCount > 0)
        {
            _html += "<div class='table-content'>";

            for (_i = 0; _i < _recordCount; _i++)
            {
                _groupNum = !_data[_i, 6].Equals("0") ? " ( กลุ่ม " + _data[_i, 6] + " )" : "";
                _callFunc = "OpenTab('link-tab3-cp-tab-scholarship','#tab3-cp-tab-scholarship','ปรับปรุงรายการทุนการศึกษา',false,'update','" + _data[_i, 0] + "','')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='scholarship" + _data[_i, 0] + "'>" +
                         "  <li id='table-content-cp-tab-scholarship-col1' onclick=" + _callFunc + "><div>" + _data[_i, 9] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-scholarship-col2' onclick=" + _callFunc + "><div><span class='facultycode-col'>" + _data[_i, 1] + "</span>- " + _data[_i, 2] + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-scholarship-col3' onclick=" + _callFunc + "><div><span class='programcode-col'>" + _data[_i, 3] + "</span>- " + _data[_i, 4] + _groupNum + "</div></li>" +
                         "  <li class='table-col' id='table-content-cp-tab-scholarship-col4' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 7]).ToString("#,##0") + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";
        }

        return _html;
    }
    
    public static string ListUpdateCPTabScholarship()
    {
        string _html = String.Empty;
        string _return = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabScholarship("");
        _recordCount = _data.GetLength(0);
        _html += ListCPTabScholarship(_data);
        _return += "<recordcount>" + _recordCount + "<recordcount><list>" + _html + "<list>";

        return _return;
    }

    public static string TabCPTabScholarship()
    {
        string _html = String.Empty;
        string[,] _data;
        int _recordCount;

        _data = eCPDB.ListCPTabScholarship("");
        _recordCount = _data.GetLength(0);

        _html += "<div id='cp-tab-scholarship-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-scholarship") +
                 "      <div class='content-data-tabs' id='tabs-cp-tab-scholarship'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-tab-scholarship' alt='#tab1-cp-tab-scholarship' href='javascript:void(0)'>แสดงรายการทุนการศึกษา</a></li>" +
                 "                  <li><a id='link-tab2-cp-tab-scholarship' alt='#tab2-cp-tab-scholarship' href='javascript:void(0)'>เพิ่มรายการทุนการศึกษา</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-tab-scholarship' alt='#tab3-cp-tab-scholarship' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-tab-scholarship-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-scholarship'>ค้นหาพบ " + _recordCount.ToString("#,##0") + " รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-tab-scholarship-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-tab-scholarship-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-tab-scholarship-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='table-head-cp-tab-scholarship-col1'><div class='table-head-line1'>ระดับการศึกษา</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-scholarship-col2'><div class='table-head-line1'>คณะ</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-scholarship-col3'><div class='table-head-line1'>หลักสูตร</div></li>" +
                 "                  <li class='table-col' id='table-head-cp-tab-scholarship-col4'><div class='table-head-line1'>จำนวนเงินทุนการศึกษา</div><div>( บาท / หลักสูตร )</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-scholarship-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-scholarship-contents'></div>" +
                 "</div>" +
                 "<div id='cp-tab-scholarship-content'>" +
                 "  <div class='tab-content' id='tab1-cp-tab-scholarship-content'>" +
                 "      <div class='box4' id='list-data-tab-scholarship'>" + ListCPTabScholarship(_data) +"</div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-scholarship-content'>" +
                 "      <div class='box1 addupdate-data-tab-scholarship' id='add-data-tab-scholarship'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-scholarship-content'>" +
                 "      <div class='box1 addupdate-data-tab-scholarship' id='update-data-tab-scholarship'></div>" +
                 "  </div>" +
                 "</div>";        

        return _html;
    }

    public static string ListSearchScholarshipAndPayBreakContract(HttpContext _c)
    {
        string _html = String.Empty;
        string _result = String.Empty;
        string[,] _data;
        int _recordCount;

        _result = ";";

        if (_c.Request["scholar"].Equals("1"))
        {
            _data = eCPDB.ListSearchCPTabScholarship(_c);
            _recordCount = _data.GetLength(0);          
            _result = _recordCount > 0 ? double.Parse(_data[0, 1]).ToString("#,##0") + ";" : _result;
        }
        
        _html += _result;
        _result = ";;";

        if (!_c.Request["casegraduate"].Equals("0"))
        {
            _data = eCPDB.ListSearchCPTabPayBreakContract(_c);
            _recordCount = _data.GetLength(0);
            _result = _recordCount > 0 ? _data[0, 1].ToString() + ";" + double.Parse(_data[0, 2]).ToString("#,##0") + ";" + double.Parse(_data[0, 3]).ToString("#,##0") : _result;
        }

        _html += _result;

        return "<list>" + _html + "<list>";
    }
}