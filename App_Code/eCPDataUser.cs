//eCPDataUser.cs      : สำหรับจัดการบัญชีผู้ใช้งาน
//Date Created        : ๒๘/๐๑/๒๕๕๖
//Last Date Modified  : ๑๐/๐๔/๒๕๖๔
//Create By           : Yutthaphoom Tawana

using System;
using System.Web;

public class eCPDataUser
{
    //สำหรับแสดงฟอร์มเพิ่มและแก้ไขบัญชีผู้ใช้งาน
    public static string AddUpdateCPTabUser(string _action, string[,] _data)
    {
        string _html = String.Empty;
        string _username = _action.Equals("update") ? _data[0, 1] : String.Empty;
        string _password = _action.Equals("update") ? _data[0, 2] : String.Empty;
        string _nameDefault = _action.Equals("update") ? _data[0, 3] : String.Empty;

        _html += "<div class='form-content' id='" + _action + "-cp-tab-user'>" +
                 "  <div id='addupdate-cp-tab-user'>" +
                 "      <input type='hidden' id='action' value='" + _action + "' />" +
                 "      <input type='hidden' id='username-hidden' value='" + _username + "' />" +
                 "      <input type='hidden' id='password-hidden' value='" + _password + "' />" +
                 "      <input type='hidden' id='name-hidden' value='" + _nameDefault + "' />" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='username-label'>" +
                 "                  <div class='form-label-style'>Username</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ Username</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='username-input'><input class='inputbox' type='text' id='username' onblur=Trim('username'); value='' style='width:200px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='password-label'>" +
                 "                  <div class='form-label-style'>Password</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ Password</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='password-input'><input class='inputbox' type='text' id='password' onblur=Trim('password') value='' style='width:200px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "      <div>" +
                 "          <div class='form-label-discription-style'>" +
                 "              <div id='name-label'>" +
                 "                  <div class='form-label-style'>ชื่อ</div>" +
                 "                  <div class='form-discription-style'>" +
                 "                      <div class='form-discription-line1-style'>กรุณาใส่ชื่อ</div>" +
                 "                  </div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='form-input-style'>" +
                 "              <div class='form-input-content' id='name-input'><input class='inputbox' type='text' id='name' onblur=Trim('name') value='' style='width:200px' /></div>" +
                 "          </div>" +
                 "      </div>" +
                 "      <div class='clear'></div>" +
                 "  </div>" +
                 "  <div class='button'>" +
                 "      <div class='button-style1' id='button-style11'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=ConfirmActionCPTabUser('" + _action + "')>บันทึก</a></li>" +
                 "              <li><a href='javascript:void(0)' onclick='ResetFrmCPTabUser(false)'>ล้าง</a></li>";

        if (_action.Equals("update"))
            _html += "          <li class='hide-button'><a href='javascript:void(0)' onclick=ConfirmActionCPTabUser('del')>ลบ</a></li>";

        _html += "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-user')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "      <div class='button-style1' id='button-style12'>" +
                 "          <ul>" +
                 "              <li><a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-user')>ปิด</a></li>" +
                 "          </ul>" +
                 "      </div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
    
    //สำหรับตรวจสอบการเพิ่มบัญชีผู้ใช้งาน
    public static string AddCPTabuser()
    {
        string _html = String.Empty;
        string[,] _data = new string[0, 0];

        _html += AddUpdateCPTabUser("add", _data);

        return _html;
    }

    //สำหรับตรวจสอบการแก้ไขบัญชีผู้ใช้งาน
    public static string UpdateCPTabUser(string _username, string _password)
    {
        string _html = String.Empty;
        string[,] _data;

        _data = eCPDB.ListDetailCPTabUser(_username, _password, "User");
        if (_data.GetLength(0) > 0)
            _html += AddUpdateCPTabUser("update", _data);

        return _html;
    }

    //สำหรับแสดงบัญชีผู้ใช้งาน
    public static string ListCPTabUser(HttpContext _c)
    {
        string _html = String.Empty;
        string _pageHtml = String.Empty;
        string[,] _data;
        string _highlight = String.Empty;
        string _callFunc = String.Empty;
        int _section;
        int _recordCount;
        int _i;
        int[] _resultPage = new int[2];
        int _currentPage;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        _recordCount = eCPDB.CountCPTabUser(_c);

        if (_recordCount > 0)
        {
            _data = eCPDB.ListCPTabUser(_c);

            _html += "<div class='table-content'>";

            for (_i = 0; _i < _data.GetLength(0); _i++)
            {
                _callFunc = "OpenTab('link-tab3-cp-tab-user','#tab3-cp-tab-user','ปรับปรุงบัญชีผู้ใช้งาน',false,'update','" + (_data[_i, 1] + ":" + _data[_i, 2]) + "','')";
                _highlight = (_i % 2) == 0 ? "highlight1" : "highlight2";
                _html += "<ul class='table-row-content " + _highlight + "' id='tab-user-" + _data[_i, 1] + "'>" +
                         "  <li id='tab1-table-content-cp-tab-user-col1' onclick=" + _callFunc + "><div>" + double.Parse(_data[_i, 0]).ToString("#,##0") + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-cp-tab-user-col2' onclick=" + _callFunc + "><div>" + _data[_i, 1] + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-cp-tab-user-col3' onclick=" + _callFunc + "><div>" + _data[_i, 3] + "</div></li>" +
                         "  <li class='table-col' id='tab1-table-content-cp-tab-user-col4' onclick=" + _callFunc + "><div>" + eCPDB._userSection[int.Parse(_data[_i, 4]) - 1] + "</div></li>" +
                         "</ul>";
            }

            _html += "</div>";

            _currentPage = String.IsNullOrEmpty(_c.Request["currentpage"]) ? 0 : int.Parse(_c.Request["currentpage"]);
            _resultPage = PageNavigate.CalPage(_recordCount, _currentPage, eCPUtil.ROW_PER_PAGE);
            _pageHtml += "<div class='content-data-top-bottom'>" +
                         "  <div>" + PageNavigate.PageNav(_recordCount, _resultPage, "tabuser", eCPUtil.ROW_PER_PAGE) + "</div>" +
                         "  <div class='clear'></div>" +
                         "</div>";
        }

        return "<recordcount>" + _recordCount.ToString("#,##0") + "<recordcount><list>" + _html + "<list><pagenav>" + _pageHtml + "<pagenav>";
    }
    
    //สำหรับแสดงหน้าหลักบัญชีผู้ใช้งาน
    public static string TabCPTabUser()
    {
        string _html = String.Empty;

        _html += "<div id='cp-tab-user-head'>" +
                 "  <div class='content-data-head'>" +
                        eCPUtil.ContentTitle("cp-tab-user") +
                 "      <div class='content-data-tabs' id='tabs-cp-tab-user'>" +
                 "          <div class='content-data-tabs-content'>" +
                 "              <ul>" +
                 "                  <li><a class='active' id='link-tab1-cp-tab-user' alt='#tab1-cp-tab-user' href='javascript:void(0)'>รายการบัญชีผู่ใช้งาน</a></li>" +
                 "                  <li><a id='link-tab2-cp-tab-user' alt='#tab2-cp-tab-user' href='javascript:void(0)'>เพิ่มบัญชีผู้ใช้งาน</a></li>" +
                 "                  <li class='tab-hidden'><a id='link-tab3-cp-tab-user' alt='#tab3-cp-tab-user' href='javascript:void(0)'></a></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='content-data-tab-head'>" +
                 "      <div class='tab-content' id='tab1-cp-tab-user-head'>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='content-data-tab-content'>" +
                 "              <div class='content-left'>" +
                 "                  <input type='hidden' id='search-tab-user' value=''>" +
                 "                  <input type='hidden' id='name-tab-user-hidden' value=''>" +
                 "                  <div class='button-style2'>" +
                 "                      <ul>" +
                 "                          <li><a href='javascript:void(0)' onclick=LoadForm(1,'searchcptabuser',true,'','','')>ค้นหา</a></li>" +
                 "                      </ul>" +
                 "                  </div>" +
                 "              </div>" +
                 "              <div class='content-right'>" +
                 "                  <div class='content-data-tab-content-msg' id='record-count-cp-tab-user'>ค้นหาพบ 0 รายการ</div>" +
                 "              </div>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "          <div class='tab-line'></div>" +
                 "          <div class='box-search-condition' id='search-tab-user-condition'>" +
                 "              <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
                 "              <div class='box-search-condition-order search-tab-user-condition-order' id='search-tab-user-condition-order1'>" +
                 "                  <div class='box-search-condition-order-title'>ชื่อ</div>" +
                 "                  <div class='box-search-condition-split-title-value'>:</div>" +
                 "                  <div class='box-search-condition-order-value' id='search-tab-user-condition-order1-value'></div>" +
                 "                  <div class='clear'></div>" +
                 "              </div>" +                 
                 "          </div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab2-cp-tab-user-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "      <div class='tab-content' id='tab3-cp-tab-user-head'>" +
                 "          <div class='tab-line'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab1-cp-tab-user-contents'>" +
                 "      <div class='box3'>" +
                 "          <div class='table-head'>" +
                 "              <ul>" +
                 "                  <li id='tab1-table-head-cp-tab-user-col1'><div class='table-head-line1'>ลำดับ</div><div>ที่</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-tab-user-col2'><div class='table-head-line1'>Username</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-tab-user-col3'><div class='table-head-line1'>ชื่อ</div></li>" +
                 "                  <li class='table-col' id='tab1-table-head-cp-tab-user-col4'><div class='table-head-line1'>หน่วยงาน</div></li>" +
                 "              </ul>" +
                 "          </div>" +
                 "          <div class='clear'></div>" +
                 "      </div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-user-contents'></div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-user-contents'></div>" +
                 "</div>" +
                 "<div id='cp-tab-user-content'>" +
                 "  <div class='tab-content' id='tab1-cp-tab-user-content'>" +
                 "      <div class='box4' id='list-data-tab-user'></div>" +
                 "      <div id='nav-page-tab-user'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab2-cp-tab-user-content'>" +
                 "      <div class='box1 addupdate-data-tab-user' id='add-data-tab-user'></div>" +
                 "  </div>" +
                 "  <div class='tab-content' id='tab3-cp-tab-user-content'>" +
                 "      <div class='box1 addupdate-data-tab-user' id='update-data-tab-user'></div>" +
                 "  </div>" +
                 "</div>";

        return _html;
    }
}