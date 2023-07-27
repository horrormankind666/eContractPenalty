/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๒๘/๐๑/๒๕๕๖>
Modify date : <๐๕/๐๗/๒๕๖๖>
Description : <สำหรับจัดการบัญชีผู้ใช้งาน>
=============================================
*/

using System;
using System.Web;

public class eCPDataUser {
    public static string AddUpdateCPTabUser(
        string action,
        string[,] data
    ) {
        string html = string.Empty;
        string userid = (action.Equals("update") ? data[0, 9] : string.Empty);
        string username = (action.Equals("update") ? data[0, 1] : string.Empty);
        /*
        string password = (action.Equals("update") ? data[0, 2] : string.Empty);
        */
        string nameDefault = (action.Equals("update") ? data[0, 3] : string.Empty);
        string phoneNumberDefault = (action.Equals("update") ? data[0, 6] : string.Empty);
        string mobileNumberDefault = (action.Equals("update") ? data[0, 7] : string.Empty);
        string emailDefault = (action.Equals("update") ? data[0, 8] : string.Empty);

        html += (
            "<div class='form-content' id='" + action + "-cp-tab-user'>" +
            "   <div id='addupdate-cp-tab-user'>" +
            "       <input type='hidden' id='action' value='" + action + "' />" +
            "       <input type='hidden' id='userid-hidden' value='" + userid + "' />" +
            "       <input type='hidden' id='username-hidden' value='" + username + "' />" +
            /*
            "       <input type='hidden' id='password-hidden' value='" + password + "' />" +
            */
            "       <input type='hidden' id='name-hidden' value='" + nameDefault + "' />" +
            "       <input type='hidden' id='phonenumber-hidden' value='" + phoneNumberDefault + "' />" +
            "       <input type='hidden' id='mobilenumber-hidden' value='" + mobileNumberDefault + "' />" +
            "       <input type='hidden' id='email-hidden' value='" + emailDefault + "' />" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='username-label'>" +
            "                   <div class='form-label-style'>Username</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ Username</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='username-input'>" +
            "                   <input class='inputbox' type='text' id='username' onblur=Trim('username'); value='' style='width:237px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            /*
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='password-label'>" +
            "                   <div class='form-label-style'>Password</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ Password</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='password-input'>" +
            "                   <input class='inputbox' type='text' id='password' onblur=Trim('password') value='' style='width:237px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            */
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='name-label'>" +
            "                   <div class='form-label-style'>ชื่อ</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่ชื่อ</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='name-input'>" +
            "                   <input class='inputbox' type='text' id='name' onblur=Trim('name') value='' style='width:237px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='name-label'>" +
            "                   <div class='form-label-style'>หมายเลขโทรศัพท์</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่หมายเลขโทรศัพท์</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='phonenumber-input'>" +
            "                   <input class='inputbox' type='text' id='phonenumber' value='' style='width:147px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='name-label'>" +
            "                   <div class='form-label-style'>หมายเลขโทรศัพท์มือถือ</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่หมายเลขโทรศัพท์มือถือ</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='phonenumber-input'>" +
            "                   <input class='inputbox' type='text' id='mobilenumber' value='' style='width:147px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "       <div>" +
            "           <div class='form-label-discription-style'>" +
            "               <div id='name-label'>" +
            "                   <div class='form-label-style'>อีเมล์</div>" +
            "                   <div class='form-discription-style'>" +
            "                       <div class='form-discription-line1-style'>กรุณาใส่อีเมล์</div>" +
            "                   </div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='form-input-style'>" +
            "               <div class='form-input-content' id='email-input'>" +
            "                   <input class='inputbox' type='text' id='email' value='' style='width:329px' />" +
            "               </div>" +
            "           </div>" +
            "       </div>" +
            "       <div class='clear'></div>" +
            "   </div>" +
            "   <div class='button'>" +
            "       <div class='button-style1' id='button-style11'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=ConfirmActionCPTabUser('" + action + "')>บันทึก</a>" +
            "               </li>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick='ResetFrmCPTabUser(false)'>ล้าง</a>" +
            "               </li>"
        );

        if (action.Equals("update"))
            html += (
                "           <li class='hide-button'>" +
                "               <a href='javascript:void(0)' onclick=ConfirmActionCPTabUser('del')>ลบ</a>" +
                "           </li>"
            );

        html += (
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-user')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "       <div class='button-style1' id='button-style12'>" +
            "           <ul>" +
            "               <li>" +
            "                   <a href='javascript:void(0)' onclick=CloseFrm(false,'addupdate-cp-tab-user')>ปิด</a>" +
            "               </li>" +
            "           </ul>" +
            "       </div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
    
    public static string AddCPTabuser() {
        string html = string.Empty;
        string[,] data = new string[0, 0];

        html += AddUpdateCPTabUser("add", data);

        return html;
    }

    public static string UpdateCPTabUser(string userid) {
        string html = string.Empty;
        string[,] data = eCPDB.ListDetailCPTabUser(userid, "", "", "User");

        if (data.GetLength(0) > 0)
            html += AddUpdateCPTabUser("update", data);

        return html;
    }
    
    public static string ListCPTabUser(HttpContext c) {
        string html = string.Empty;
        string pageHtml = string.Empty;                        
        int recordCount = eCPDB.CountCPTabUser(c);       

        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        if (recordCount > 0) {
            string[,] data = eCPDB.ListCPTabUser(c);
            string highlight;
            string[] phoneNumber = new string[2];
            string callFunc;
            int j;

            html += (
                "<div class='table-content'>"
            );

            for (int i = 0; i < data.GetLength(0); i++) {
                j = 0;

                Array.Clear(phoneNumber, 0, phoneNumber.Length);

                if (!string.IsNullOrEmpty(data[i, 6])) {
                    phoneNumber[j] = data[i, 6];
                    j++;
                }

                if (!string.IsNullOrEmpty(data[i, 7]))
                    phoneNumber[j] = data[i, 7];

                highlight = ((i % 2) == 0 ? "highlight1" : "highlight2");
                callFunc = ("OpenTab('link-tab3-cp-tab-user','#tab3-cp-tab-user','ปรับปรุงบัญชีผู้ใช้งาน',false,'update','" + data[i, 9] + "','')");

                html += (
                    "<ul class='table-row-content " + highlight + "' id='tab-user-" + data[i, 1] + "'>" +
                    "   <li id='tab1-table-content-cp-tab-user-col1' onclick=" + callFunc + ">" +
                    "       <div>" + double.Parse(data[i, 0]).ToString("#,##0") + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-cp-tab-user-col2' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 1] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-cp-tab-user-col3' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 3] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-cp-tab-user-col4' onclick=" + callFunc + ">" +
                    "       <div>" + string.Join("<br />", phoneNumber) + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-cp-tab-user-col5' onclick=" + callFunc + ">" +
                    "       <div>" + data[i, 8] + "</div>" +
                    "   </li>" +
                    "   <li class='table-col' id='tab1-table-content-cp-tab-user-col6' onclick=" + callFunc + ">" +
                    "       <div>" + eCPDB.userSection[int.Parse(data[i, 4]) - 1] + "</div>" +
                    "   </li>" +
                    "</ul>"
                );
            }

            html += (
                "</div>"
            );

            int currentPage = (string.IsNullOrEmpty(c.Request["currentpage"]) ? 0 : int.Parse(c.Request["currentpage"]));
            int[] resultPage = PageNavigate.CalPage(recordCount, currentPage, eCPUtil.ROW_PER_PAGE);

            pageHtml += (
                "<div class='content-data-top-bottom'>" +
                "   <div>" + PageNavigate.PageNav(recordCount, resultPage, "tabuser", eCPUtil.ROW_PER_PAGE) + "</div>" +
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
    
    public static string TabCPTabUser() {
        string html = string.Empty;

        html += (
            "<div id='cp-tab-user-head'>" +
            "   <div class='content-data-head'>" +
                    eCPUtil.ContentTitle("cp-tab-user") +
            "       <div class='content-data-tabs' id='tabs-cp-tab-user'>" +
            "           <div class='content-data-tabs-content'>" +
            "               <ul>" +
            "                   <li>" +
            "                       <a class='active' id='link-tab1-cp-tab-user' alt='#tab1-cp-tab-user' href='javascript:void(0)'>รายการบัญชีผู่ใช้งาน</a>" +
            "                   </li>" +
            "                   <li>" +
            "                       <a id='link-tab2-cp-tab-user' alt='#tab2-cp-tab-user' href='javascript:void(0)'>เพิ่มบัญชีผู้ใช้งาน</a>" +
            "                   </li>" +
            "                   <li class='tab-hidden'>" +
            "                       <a id='link-tab3-cp-tab-user' alt='#tab3-cp-tab-user' href='javascript:void(0)'></a>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='content-data-tab-head'>" +
            "       <div class='tab-content' id='tab1-cp-tab-user-head'>" +
            "           <div class='tab-line'></div>" +
            "           <div class='content-data-tab-content'>" +
            "               <div class='content-left'>" +
            "                   <input type='hidden' id='search-tab-user' value=''>" +
            "                   <input type='hidden' id='name-tab-user-hidden' value=''>" +
            "                   <div class='button-style2'>" +
            "                       <ul>" +
            "                           <li>" +
            "                               <a href='javascript:void(0)' onclick=LoadForm(1,'searchcptabuser',true,'','','')>ค้นหา</a>" +
            "                           </li>" +
            "                       </ul>" +
            "                   </div>" +
            "               </div>" +
            "               <div class='content-right'>" +
            "                   <div class='content-data-tab-content-msg' id='record-count-cp-tab-user'>ค้นหาพบ 0 รายการ</div>" +
            "               </div>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "           <div class='tab-line'></div>" +
            "           <div class='box-search-condition' id='search-tab-user-condition'>" +
            "               <div class='box-search-condition-title'>ค้นหาตามเงื่อนไข</div>" +
            "               <div class='box-search-condition-order search-tab-user-condition-order' id='search-tab-user-condition-order1'>" +
            "                   <div class='box-search-condition-order-title'>ชื่อ</div>" +
            "                   <div class='box-search-condition-split-title-value'>:</div>" +
            "                   <div class='box-search-condition-order-value' id='search-tab-user-condition-order1-value'></div>" +
            "                   <div class='clear'></div>" +
            "               </div>" +                 
            "           </div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab2-cp-tab-user-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "       <div class='tab-content' id='tab3-cp-tab-user-head'>" +
            "           <div class='tab-line'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab1-cp-tab-user-contents'>" +
            "       <div class='box3'>" +
            "           <div class='table-head'>" +
            "               <ul>" +
            "                   <li id='tab1-table-head-cp-tab-user-col1'>" +
            "                       <div class='table-head-line1'>ลำดับ</div>" +
            "                       <div>ที่</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-tab-user-col2'>" +
            "                       <div class='table-head-line1'>Username</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-tab-user-col3'>" +
            "                       <div class='table-head-line1'>ชื่อ</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-tab-user-col4'>" +
            "                       <div class='table-head-line1'>หมายเลขโทรศัพท์</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-tab-user-col5'>" +
            "                       <div class='table-head-line1'>อีเมล์</div>" +
            "                   </li>" +
            "                   <li class='table-col' id='tab1-table-head-cp-tab-user-col6'>" +
            "                       <div class='table-head-line1'>หน่วยงาน</div>" +
            "                   </li>" +
            "               </ul>" +
            "           </div>" +
            "           <div class='clear'></div>" +
            "       </div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-user-contents'></div>" +
            "   <div class='tab-content' id='tab3-cp-tab-user-contents'></div>" +
            "</div>" +
            "<div id='cp-tab-user-content'>" +
            "   <div class='tab-content' id='tab1-cp-tab-user-content'>" +
            "       <div class='box4' id='list-data-tab-user'></div>" +
            "       <div id='nav-page-tab-user'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab2-cp-tab-user-content'>" +
            "       <div class='box1 addupdate-data-tab-user' id='add-data-tab-user'></div>" +
            "   </div>" +
            "   <div class='tab-content' id='tab3-cp-tab-user-content'>" +
            "       <div class='box1 addupdate-data-tab-user' id='update-data-tab-user'></div>" +
            "   </div>" +
            "</div>"
        );

        return html;
    }
}