<%@ WebHandler Language="C#" Class="eCPHandler" %>

//eCPHandler.ashx : สำหรับรับ request แล้วนำมา process แล้วส่ง response กลับไป
//Date Created : 06/08/2555
//Last Date Modified : 02/04/2556
//Create By : Yutthaphoom Tawana

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public class eCPHandler : IHttpHandler, IRequiresSessionState
{
    //สำหรับส่ง Request ไปตามเงื่อนไข
    public void ProcessRequest (HttpContext _context)
    {
        _context.Response.ContentType = "text/plain";
        string[] _browser = Util.BrowserCapabilities();
        bool _error = false;

        if (_error.Equals(false) && _browser[1].Equals("IE") && (int.Parse(_browser[3]) < 9))
        {
            _error = true;
            _context.Response.Write("<errorbrowser>1<errorbrowser>");
        }

        if (_error.Equals(false) && bool.Parse(_browser[13]).Equals(false))
        {
            _error = true;
            _context.Response.Write("<errorbrowser>2<errorbrowser>");
        }

        if (_error.Equals(false))
        {
            string _action = _context.Request["action"];

            switch (_action)
            {
                case "add":
                case "update":
                case "del"      : { AddUpdateData(_context); break; }
                case "form"     : { ShowForm(_context); break; }
                case "setpage"  : { SetPage(_context); break; }
                case "page"     : { ShowPage(_context); break; }
                case "signin"   : { Signin(_context); break; }
                case "signout"  : { Signout(); break; }
                case "list"     :
                case "combobox" : { ShowList(_context); break; }
                case "search"   : { ShowSearch(_context); break; }
                case "resize"   : { ImageProcess.ResizeImage(_context.Request["file"], int.Parse(_context.Request["width"]), int.Parse(_context.Request["height"])); break; }
                case "calculate": { ShowCalculate(_context); break; }
                case "print"    : { ShowPrint(_context); break; }
                case "econtract": { ShowDocEContract(_context); break; }
            }
        }
    }

    private static int _error;
    private static string _menuBar;
    private static int _menu;
    private static string _head;
    private static string _content;

    //สำหรับ Set ค่าของหน้าเว็บ เพื่อส่งกลับไปแสดงผล
    private string SetValuePageReturn()
    {
        string _return = "<error>" + _error + "<error><head>" + _head + "<head><menubar>" + _menuBar + "<menubar><menu>" + _menu + "<menu><content>" + _content + "<content>";

        return _return;
    }

    //สำหรับ Set ค่าของหน้าเว็บ ใน Cookies
    private void SetPage(HttpContext _c)
    {
        int _section;
        int _pid;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (_eCPCookie == null)
        {
            _section = 0;
            _pid = 0;
        }
        else
        {
            _section = int.Parse(_eCPCookie["UserSection"]);
            _pid = int.Parse(_eCPCookie["Pid"]);
        }

        _c.Response.Write("<section>" + _section + "<section><page>" + _pid + "<page>");
    }

    //สำหรับแสดงหน้าเว็บ
    private void ShowPage(HttpContext _c)
    {
        bool _loginResult;
        eCPUtil _util = new eCPUtil();

        _loginResult = eCPDB.ChkLogin();
        if (!_loginResult)
        {
            _error = 1;
            _head = String.Empty;
            _menuBar = String.Empty;
            _menu = 0;
            _content = eCPUtil.Signin();
        }
        else
        {
            _error = 0;
            _head = eCPUtil.Head();
            _menuBar = eCPUtil.MenuBar(_loginResult);
            _menu = eCPUtil._activeMenu[int.Parse(_c.Request["section"]) - 1, int.Parse(_c.Request["pid"]) - 1];
            _content = _util.GenPage(_loginResult, int.Parse(_c.Request["pid"]) - 1);
        }


        _c.Response.Write(SetValuePageReturn());
    }

    //สำหรับตรวจสอบการเข้าระบบ
    private void Signin(HttpContext _c)
    {
        bool _loginResult;

        _loginResult = eCPDB.Signin(_c.Request["username"], _c.Request["password"]);
        if (!_loginResult)
        {
            _error = 1;
        }
        else
            _error = 0;

        _c.Response.Write(SetValuePageReturn());
    }

    //สำหรับออกจากระบบ
    private void Signout()
    {
        eCPDB.Signout();
    }

    //สำหรับ Set ค่าที่ใช้ในแสดงฟอร์มต่าง ๆ ตามเงื่อนไข
    private void ShowForm(HttpContext _c)
    {
        string _frmOrder = _c.Request["frm"];
        string _frm = String.Empty;
        string _trackingStatus = String.Empty;
        string _action = String.Empty;
        string _from = String.Empty;
        int _width = 0;
        int _height = 0;
        string _title = String.Empty;

        switch (_frmOrder)
        {
            case "searchcptabuser"                          : {
                    _frm = eCPDataFormSearch.SearchCPTabUser();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-tab-user";
                    break;
                }
            case "addcptabuser"                             : { _frm = eCPDataUser.AddCPTabuser(); break; }
            case "updatecptabuser"                          : {
                    char[] _separator = new char[] { ':' };
                    string[] _userpass = (_c.Request["id"]).Split(_separator);

                    _frm = eCPDataUser.UpdateCPTabUser(_userpass[0], _userpass[1]);
                    break;
                }
            case "addcptabprogram"                          : { _frm = eCPDataConfiguration.AddCPTabProgram(); break; }
            case "updatecptabprogram"                       : { _frm = eCPDataConfiguration.UpdateCPTabProgram(_c.Request["id"]); break; }
            case "detailcptabcaldate"                       : {
                    _frm = eCPDataConfiguration.DetailCPTabCalDate(_c.Request["id"]);
                    _width = 750;
                    _height = 0;
                    _title = "detail-cp-tab-cal-date";
                    break;
                }
            case "addcptabinterest"                         : { _frm = eCPDataConfiguration.AddCPTabInterest(); break; }
            case "updatecptabinterest"                      : { _frm = eCPDataConfiguration.UpdateCPTabInterest(_c.Request["id"]); break; }
            case "addcptabpaybreakcontract"                 : { _frm = eCPDataConfiguration.AddCPTabPayBreakContract(); break; }
            case "updatecptabpaybreakcontract"              : { _frm = eCPDataConfiguration.UpdateCPTabPayBreakContract(_c.Request["id"]); break; }
            case "addcptabscholarship"                      : { _frm = eCPDataConfiguration.AddCPTabScholarship(); break; }
            case "updatecptabscholarship"                   : { _frm = eCPDataConfiguration.UpdateCPTabScholarship(_c.Request["id"]); break; }
            case "addprofilestudent"                        : {
                    _frm = eCPDataBreakContract.AddProfileStudent();
                    _width = 831;
                    _height = 0;
                    _title = "add-profile-student";
                    break;
                }
            case "searchstudentwithresult"                  : {
                    _frm = eCPDataFormSearch.SearchStudentWithResult();
                    _width = 900;
                    _height = 0;
                    _title = "search-student";
                    break;
                }
            case "addcptransbreakcontract"                  : { _frm = eCPDataBreakContract.AddCPTransBreakContract(); break; }
            case "updatecptransbreakcontract"               : { _frm = eCPDataBreakContract.UpdateCPTransBreakContract(_c.Request["id"]); break; }
            case "searchcptransbreakcontract"               : {
                    _frm =  eCPDataFormSearch.SearchCPTransBreakContract();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-trans-break-contract";
                    break;
                }
            case "detailtrackingstatus"                     : {
                    _frm = "<div id='status-tracking-explanation'></div>";
                    _width = 350;
                    _height = 0;
                    _title = "detail-tracking-status";
                    break;
                }
            case "detailcptransbreakcontract"               :
            case "detailcptransrequirecontract"             :
            case "detailcptransrequirerepaycontract"        :
            case "receivercptransbreakcontract"             :
            case "repaycptransrequirecontract"              :
            case "repaycptransrequirecontract1"             : {
                    _trackingStatus = _frmOrder.Equals("detailcptransbreakcontract") ? "v1" : _trackingStatus;
                    _trackingStatus = _frmOrder.Equals("detailcptransrequirecontract") ? "v2" : _trackingStatus;
                    _trackingStatus = _frmOrder.Equals("detailcptransrequirerepaycontract") ? "v3" : _trackingStatus;
                    _trackingStatus = _frmOrder.Equals("receivercptransbreakcontract") ? "a" : _trackingStatus;
                    _trackingStatus = _frmOrder.Equals("repaycptransrequirecontract") ? "r" : _trackingStatus;
                    _trackingStatus = _frmOrder.Equals("repaycptransrequirecontract1") ? "r1" : _trackingStatus;

                    if (_trackingStatus.Equals("v1") || _trackingStatus.Equals("a"))
                    {
                        _frm = eCPDataBreakContract.DetailCPTransBreakContract(_c.Request["id"], _trackingStatus);
                        _width = 800;
                        _height = 0;
                        _title = "detail-cp-trans-break-contract";
                    }

                    if (_trackingStatus.Equals("v2") || _trackingStatus.Equals("v3") || _trackingStatus.Equals("r") || _trackingStatus.Equals("r1"))
                    {
                        _frm = eCPDataRequireContract.DetailCPTransRequireContract(_c.Request["id"], _trackingStatus);
                        _width = 800;
                        _height = 0;
                        _title = (_trackingStatus.Equals("v2") || _trackingStatus.Equals("v3") ? "detail-cp-trans-require-contract" : "repay-cp-trans-require-contract");
                    }
                    break;
                }
            case "addcommenteditbreakcontract"              :
            case "addcommentcancelbreakcontract"            :
            case "addcommentcancelrequirecontract"          :
            case "addcommentcancelrepaycontract"            : {
                    _action = _frmOrder.Equals("addcommenteditbreakcontract") ? "E" : _action;
                    _action = _frmOrder.Equals("addcommentcancelbreakcontract") ? "C" : _action;
                    _action = _frmOrder.Equals("addcommentcancelrequirecontract") ? "C" : _action;
                    _action = _frmOrder.Equals("addcommentcancelrepaycontract") ? "C" : _action;

                    _from = _frmOrder.Equals("addcommenteditbreakcontract") ? "breakcontract" : _from;
                    _from = _frmOrder.Equals("addcommentcancelbreakcontract") ? "breakcontract" : _from;
                    _from = _frmOrder.Equals("addcommentcancelrequirecontract") ? "requirecontract" : _from;
                    _from = _frmOrder.Equals("addcommentcancelrepaycontract") ? "repaycontract" : _from;

                    _frm = eCPDataBreakContract.AddCommentBreakContract(_c.Request["id"], _action, _from);
                    _width = 480;
                    _height = 0;
                    _title = "add-comment-" + _action + "-break-contract";
                    break;
                }
            case "addcptransrequirecontract"                : { _frm = eCPDataRequireContract.AddCPTransRequireContract(_c.Request["id"]); break; }
            case "updatecptransrequirecontract"             : { _frm = eCPDataRequireContract.UpdateCPTransRequireContract(_c.Request["id"]); break; }
            case "detailrepaystatus"                        : {
                    _frm = "<div id='status-repay-explanation'></div>";
                    _width = 406;
                    _height = 0;
                    _title = "detail-repay-status";
                    break;
                }
            case "searchcptransrepaycontract"               : {
                    _frm = eCPDataFormSearch.SearchCPTransRepayContract();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-trans-repay-contract";
                    break;
                }
            case "addupdaterepaycontract"                   : {
                    _frm = eCPDataRepay.AddUpdateCPTransRepayContract(_c.Request["id"]);
                    _width = 680;
                    _height = 0;
                    _title = "addupdate-repay-contract";
                    break;
                }
            case "viewrepaycontract"                        : {
                    _frm = eCPDataRepay.ViewCPTransRepayContract(_c.Request["id"]);
                    _width = 680;
                    _height = 0;
                    _title = "addupdate-repay-contract";
                    break;
                }
            case "calinterest"                              : {
                    _frm = eCPDataRepay.DetailCalInterestOverpayment(_c.Request["id"]);
                    _width = 721;
                    _height = 0;
                    _title = "cal-interest";
                    break;
                }
            case "detailpaymentstatus"                      : {
                    _frm = "<div id='status-payment-explanation'></div>";
                    _width = 350;
                    _height = 146;
                    _title = "detail-payment-status";
                    break;
                }
            case "adddetailcptranspayment"                  : {
                    _frm = eCPDataPayment.TabAddDetailCPTransPayment(_c.Request["id"]);
                    _width = 900;
                    _height = 0;
                    _title = "adddetail-cp-trans-payment";
                    break;
                }
            case "searchcptranspayment"                     : {
                    _frm = eCPDataFormSearch.SearchCPTransPayment();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-trans-payment";
                    break;
                }
            case "selectformatpayment"                      : {
                    _frm = eCPDataPayment.SelectFormatPayment(_c.Request["id"]);
                    _width = 500;
                    _height = 0;
                    _title = "select-format-payment";
                    break;
                }
            case "detailcptranspayment"                     : { _frm = eCPDataPayment.AddDetailCPTransPayment(_c.Request["id"], "detail"); break; }
            case "detailtranspayment"                       : {
                    _frm = eCPDataPayment.DetailTransPayment(_c.Request["id"]);
                    _width = 650;
                    _height = 0;
                    _title = "detail-trans-payment";
                    break;
                }
            case "addcptranspaymentfullrepay"               : { _frm = eCPDataPayment.AddDetailCPTransPayment(_c.Request["id"], "addfullrepay"); break; }
            case "addcptranspaymentpayrepay"                : { _frm = eCPDataPayment.AddDetailCPTransPayment(_c.Request["id"], "addpayrepay"); break; }
            case "adddetailpaychannel"                      : {
                    _frm = eCPDataPayment.AddDetailPayChannel(_c.Request["id"]);
                    _width = 557;
                    _height = 0;
                    _title = "add-detail-pay-channel";
                    break;
                }
            case "chkbalance"                               : {
                    _frm = eCPDataPayment.ChkBalance();
                    _width = 557;
                    _height = 0;
                    _title = "chk-balance";
                    break;
                }
            case "searchcpreporttablecalcapitalandinterest" : {
                    _frm = eCPDataFormSearch.SearchCPReportTableCalCapitalAndInterest();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-table-cal-capital-and-interest";
                    break;
                }
            case "calreporttablecalcapitalandinterest"      : { _frm = eCPDataReportTableCalCapitalAndInterest.CalReportTableCalCapitalAndInterest(_c.Request["id"]); break; }
            case "searchcpreportstepofwork"                 : {
                    _frm = eCPDataFormSearch.SearchCPReportStepOfWork();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-step-of-work";
                    break;
                }
            case "reportstepofworkonstatisticrepaybyprogram": {
                    _frm = eCPDataReportStatisticRepay.ListReportStepOfWorkOnStatisticRepayByProgram();
                    _width = 750;
                    _height = 0;
                    _title = "report-step-of-work-on-statistic-repay-by-program";
                    break;
                }
            case "reportstudentonstatisticcontractbyprogram": {
                    _frm = eCPDataReportStatisticContract.ListReportStudentOnStatisticContractByProgram();
                    _width = 750;
                    _height = 0;
                    _title = "report-student-on-statistic-contract-by-program";
                    break;
                }
            case "searchcpreportnoticerepaycomplete"        : {
                    _frm = eCPDataFormSearch.SearchCPReportNoticeRepayComplete();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-notice-repay-complete";
                    break;
                }
            case "searchcpreportnoticeclaimdebt"            : {
                    _frm = eCPDataFormSearch.SearchCPReportNoticeClaimDebt();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-notice-claim-debt";
                    break;
                }
            case "searchcpreportpaymentbydate"              : {
                    _frm = eCPDataFormSearch.SearchCPReportStatisticPaymentByDate();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-statistic-payment-by-date";
                    break;
                }
            case "viewtranspaymentbydate"                   : {
                    _frm = eCPDataReportStatisticPaymentByDate.ViewTransPaymentByDate(_c.Request["id"]);
                    _width = 950;
                    _height = 0;
                    _title = "view-trans-payment-by-date";
                    break;
                }
            case "viewtranspayment"                         : {
                    _frm = eCPDataReportDebtorContract.ViewTransPayment(_c.Request["id"]);
                    _width = 950;
                    _height = 0;
                    _title = "view-trans-payment";
                    break;
                }
            case "manual"                                   : {
                    _frm = eCPUtil.Manual();
                    _width = 570;
                    _height = 0;
                    _title = "manual";
                    break;
                }
            case "detailecontractstatus"                    : {
                    _frm = "<div id='status-e-contract'></div>";
                    _width = 350;
                    _height = 117;
                    _title = "detail-e-contract-status";
                    break;
                }
            case "searchcpreportecontract"                  : {
                    _frm = eCPDataFormSearch.SearchCPReportEContract();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-e-contract";
                    break;
                }
            case "searchcpreportdebtorcontract"             : {
                    _frm = eCPDataFormSearch.SearchCPReportDebtorContract();
                    _width = 655;
                    _height = 0;
                    _title = "search-cp-report-debtor-contract";
                    break;
                }
            case "searchstudentdebtorcontractbyprogram"     : {
                    _frm = eCPDataFormSearch.SearchStudentDebtorContractByProgram();
                    _width = 750;
                    _height = 0;
                    _title = "search-cp-report-debtor-contract-by-program";
                    break;
                }

        }

        _c.Response.Write("<form>" + _frm + "<form><width>" + _width + "<width><height>" + _height + "<height><title>" + _title + "<title>");
    }

    //สำหรับ Insert, Update, Delete 
    private void AddUpdateData(HttpContext _c)
    {
        string _listUpdate = String.Empty;
        string _trackingStauts = String.Empty;
        bool _loginResult;
        _error = 0;

        _loginResult = eCPDB.ChkLogin();
        if (!_loginResult)
        {
            _error = 1;
        }
        else
        {
            if (_c.Request["cmd"].Equals("addcptabuser") || _c.Request["cmd"].Equals("updatecptabuser")) _error = eCPDB.CheckRepeatCPTabUser(_c, "username") > 0 ? 2 : (eCPDB.CheckRepeatCPTabUser(_c, "password") > 0 ? 3 : _error);
            if (_c.Request["cmd"].Equals("addcptabprogram") || _c.Request["cmd"].Equals("updatecptabprogram")) _error = eCPDB.CheckRepeatCPTabProgram(_c) > 0 ? 2 : _error;
            if (_c.Request["cmd"].Equals("addcptabpaybreakcontract") || _c.Request["cmd"].Equals("updatecptabpaybreakcontract")) _error = eCPDB.CheckRepeatCPTabPayBreakContract(_c) > 0 ? 2 : _error;
            if (_c.Request["cmd"].Equals("addcptabscholarship") || _c.Request["cmd"].Equals("updatecptabscholarship")) _error = eCPDB.CheckRepeatCPTabScholarship(_c) > 0 ? 2 : _error;

            if (_error == 0) eCPDB.AddUpdateData(_c);
        }

        _c.Response.Write("<error>" + _error + "<error>" + _listUpdate);
    }

    //สำหรับแสดงรายการที่ใช้แสดงใน List Box
    private void ShowList(HttpContext _c)
    {
        string _listOrder = _c.Request["list"];
        string _listData = String.Empty;

        switch (_listOrder)
        {
            case "program"                                 : { _listData = eCPUtil.ListProgram(false, "program", _c.Request["dlevel"], _c.Request["faculty"]); break; }
            case "programcptabprogram"                     :
            case "programtransbreakcontract"               :
            case "programtransrepaycontract"               :
            case "programtranspayment"                     :
            case "programreporttablecalcapitalandinterest" :
            case "programreportstepofwork"                 :
            case "programsearchstudent"                    :
            case "programprofilestudent"                   :
            case "programreportnoticerepaycomplete"        :
            case "programreportnoticeclaimdebt"            :
            case "programreportstatisticpaymentbydate"     :
            case "programreportecontract"                  : { _listData = eCPUtil.ListProgram(true, _listOrder, _c.Request["dlevel"], _c.Request["faculty"]); break; }
            case "cpprogram"                               : { _listData = eCPDataConfiguration.ListUpdateCPTabProgram(); break; }
            case "interest"                                : { _listData = eCPDataConfiguration.ListUpdateCPTabInterest(); break; }
            case "pay-break-contract"                      : { _listData = eCPDataConfiguration.ListUpdateCPTabPayBreakContract(); break; }
            case "scholarship"                             : { _listData = eCPDataConfiguration.ListUpdateCPTabScholarship(); break; }
            case "cpreportstatisticrepay"                  : { _listData = eCPDataReportStatisticRepay.ListUpdateCPReportStatisticRepay(); break; }
            case "cpreportstatisticrecontract"             : { _listData = eCPDataReportStatisticContract.ListUpdateCPReportStatisticContract(); break; }
        }

        _c.Response.Write(_listData);
    }

    //สำหรับแสดงรายการ
    private void ShowSearch(HttpContext _c)
    {
        string _searchFrom = _c.Request["from"];
        string _listData = String.Empty;

        switch (_searchFrom)
        {
            case "tabuser"                                   : { _listData = eCPDataUser.ListCPTabUser(_c); break;}
            case "studentwithresult"                         : { _listData = eCPDataFormSearch.ListSearchStudentWithResult(_c); break; }
            case "profilestudent"                            : { _listData = eCPDataFormSearch.ListProfileStudent(_c.Request["studentid"]); break; }
            case "scholarship"                               : { _listData = eCPDataConfiguration.ListSearchCPTabScholarship(_c); break; }
            case "paybreakcontract"                          : { _listData = eCPDataConfiguration.ListSearchCPTabPayBreakContract(_c); break; }
            case "scholarshipandpaybreakcontract"            : { _listData = eCPDataConfiguration.ListSearchScholarshipAndPayBreakContract(_c); break; }
            case "studenttransbreakcontract"                 : { _listData = eCPDataBreakContract.ListSearchStudentCPTransBreakContract(_c.Request["studentid"]); break; }
            case "transbreakcontract"                        : { _listData = eCPDataBreakContract.ListCPTransBreakContract(_c); break; }
            case "trackingstatustransbreakcontract"          : { _listData = eCPDataBreakContract.ListSearchTrackingStatusCPTransBreakContract(_c.Request["cp1id"]); break; }
            case "repaystatustransrequirecontract"           : { _listData = eCPDataRequireContract.ListSearchRepayStatusCPTransRequireContract(_c.Request["cp1id"]); break; }
            case "transrepaycontract"                        : { _listData = eCPDataRepay.ListRepay(_c); break; }
            case "repaystatuscalinterest"                    : { _listData = eCPDataRepay.ListSearchRepayStatusCalInterestOverpayment(_c.Request["cp2id"]); break; }
            case "transpayment"                              : { _listData = eCPDataPayment.ListPaymentOnCPTransRequireContract(_c); break; }
            case "formatpayment"                             : { _listData = eCPUtil._paymentFormat[int.Parse(_c.Request["formatpayment"]) - 1]; break; }
            case "reporttablecalcapitalandinterest"          : { _listData = eCPDataReportTableCalCapitalAndInterest.ListCPReportTableCalCapitalAndInterest(_c); break; }
            case "reportstepofwork"                          : { _listData = eCPDataReportStepOfWork.ListCPReportStepOfWork(_c); break; }
            case "reportstatisticrepaybyprogram"             : { _listData = eCPDataReportStatisticRepay.ListCPReportStatisticRepayByProgram(_c.Request["acadamicyear"]); break; }
            case "reportstepofworkonstatisticrepaybyprogram" : { _listData = eCPDataReportStatisticRepay.ListReportStepOfWorkOnStatisticRepayByProgram(_c); break; }
            case "reportstatisticcontractbyprogram"          : { _listData = eCPDataReportStatisticContract.ListCPReportStatisticContractByProgram(_c.Request["acadamicyear"]); break; }
            case "reportstudentonstatisticcontractbyprogram" : { _listData = eCPDataReportStatisticContract.ListReportStudentOnStatisticContractByProgram(_c); break; }
            case "reportnoticerepaycomplete"                 : { _listData = eCPDataReportNoticeRepayComplete.ListCPReportNoticeRepayComplete(_c); break; }
            case "reportnoticeclaimdebt"                     : { _listData = eCPDataReportNoticeClaimDebt.ListCPReportNoticeClaimDebt(_c); break; }
            case "reportstatisticpaymentbydate"              : { _listData = eCPDataReportStatisticPaymentByDate.ListCPReportStatisticPaymentByDate(_c); break; }
            case "reportecontract"                           : { _listData = eCPDataReportEContract.ListCPReportEContract(_c); break; }
            case "reportdebtorcontract"                      : { _listData = eCPDataReportDebtorContract.ListCPReportDebtorContract(_c); break; }
            case "reportdebtorcontractbyprogram"             : { _listData = eCPDataReportDebtorContract.ListCPReportDebtorContractByProgram(_c); break;}
        }

        _c.Response.Write(_listData);
    }

    //สำหรับการคำนวณต่าง ๆ ตามเงื่อนไข
    private void ShowCalculate(HttpContext _c)
    {
        string _cal = _c.Request["cal"];

        switch (_cal)
        {
            case "scholarshipandpenalty"            : { ShowCalScholarshipPenalty(_c); break; }
            case "interestoverpayment"              : { ShowCalInterestOverpayment(_c); break; }
            case "interestpayrepay"                 : { ShowCalInterestPayRepay(_c); break; }
            case "interestoverpaymentandpayrepay"   : { ShowCalInterestOverpaymentAndPayRepay(_c); break; }
            case "chkbalance"                       : { ShowCalChkBalance(_c); break; }
            case "totalpayment"                     : { ShowCalTotalPayment(_c); break; }
            case "reporttablecalcapitalandinterest" : { ShowCalReportTableCalCapitalAndInterest(_c); break; }
        }
    }

    //สำหรับคำนวณทุนการศึกษาที่ต้องชดใช้
    private void ShowCalScholarshipPenalty(HttpContext _c)
    {
        /*
        _send[0] = "scholar"
        _send[1] = "scholarshipmoney"
        _send[2] = "scholarshipyear"
        _send[3] = "scholarshipmonth"
        _send[4] = "allactualmonthscholarship"
        _send[5] = "casegraduate"
        _send[6] = "civil"
        _send[7] = "datestart"
        _send[8] = "dateend"
        _send[9] = "indemnitoryear"
        _send[10] = "indemnitorcash"
        _send[11] = "caldatecondition"
        //ปรับปรุงเมื่อ ๐๕/๐๔/๒๕๖๒
        //---------------------------------------------------------------------------------------------------
        _send[12] = "studyleave"
        _send[13] = "beforestudyleavestartdate"
        _send[14] = "beforestudyleaveenddate"
        _send[15] = "studyleavestartdate"
        _send[16] = "studyleaveenddate"
        _send[17] = "afterstudyleavestartdate"
        _send[18] = "afterstudyleaveenddate"
        //---------------------------------------------------------------------------------------------------
        */

        string _scholar = _c.Request["scholar"];
        string _scholarshipMoney = _c.Request["scholarshipmoney"];
        string _scholarshipYear = _c.Request["scholarshipyear"];
        string _scholarshipMonth = _c.Request["scholarshipmonth"];
        string _allActualMonthScholarship = _c.Request["allactualmonthscholarship"];
        string _caseGraduate = _c.Request["casegraduate"];
        string _civil = _c.Request["civil"];
        string _dateStart = _c.Request["datestart"];
        string _dateEnd = _c.Request["dateend"];
        string _indemnitorYear = _c.Request["indemnitoryear"];
        string _indemnitorCash = _c.Request["indemnitorcash"];
        string _calDateCondition = _c.Request["caldatecondition"];
        //ปรับปรุงเมื่อ ๐๕/๐๔/๒๕๖๒
        //---------------------------------------------------------------------------------------------------
        string _studyLeave = _c.Request["studyleave"];
        string _beforeStudyLeaveStartDate = _c.Request["beforestudyleavestartdate"];
        string _beforeStudyLeaveEndDate = _c.Request["beforestudyleaveenddate"];
        string _studyLeaveStartDate = _c.Request["studyleavestartdate"];
        string _studyLeaveEndDate = _c.Request["studyleaveenddate"];
        string _afterStudyLeaveStartDate = _c.Request["afterstudyleavestartdate"];
        string _afterStudyLeaveEndDate = _c.Request["afterstudyleaveenddate"];
        //---------------------------------------------------------------------------------------------------
        string _result = String.Empty;
        double[] _resultPayScholarship;
        double[] _resultPenalty;

        _resultPayScholarship = eCPUtil.CalPayScholarship(_scholar, _caseGraduate, _civil, _scholarshipMoney, _scholarshipYear, _scholarshipMonth, _allActualMonthScholarship);

        if (!_studyLeave.Equals("Y"))
            _resultPenalty = eCPUtil.CalPenalty(_scholar, _caseGraduate, _civil, _resultPayScholarship[1].ToString(), _scholarshipYear, _scholarshipMonth, _dateStart, _dateEnd, _indemnitorYear, _indemnitorCash, _calDateCondition, 0);
        else
        {
            _dateStart = _beforeStudyLeaveStartDate;
            _dateEnd = _beforeStudyLeaveEndDate;

            IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
            DateTime _dateA = DateTime.Parse(_afterStudyLeaveStartDate, _provider);
            DateTime _dateB = DateTime.Parse(_afterStudyLeaveEndDate, _provider);
            double[] _totalDaysAfterStudyLeave  = Util.CalcDate(_dateA, _dateB);

            _resultPenalty = eCPUtil.CalPenalty(_scholar, _caseGraduate, _civil, _resultPayScholarship[1].ToString(), _scholarshipYear, _scholarshipMonth, _dateStart, _dateEnd, _indemnitorYear, _indemnitorCash, _calDateCondition, _totalDaysAfterStudyLeave[0]);
        }

        _result += "<allactualscholarship>" + _resultPayScholarship[0].ToString("#,##0.00") + "<allactualscholarship>" +
                   "<totalpayscholarship>" + _resultPenalty[5].ToString("#,##0.00") + "<totalpayscholarship>" +
                   "<month>" + _resultPenalty[0].ToString("#,##0") + "<month>" +
                   "<day>" + _resultPenalty[1].ToString("#,##0") + "<day>" +
                   "<allactual>" + _resultPenalty[2].ToString("#,##0") + "<allactual>" +
                   "<actual>" + _resultPenalty[3].ToString("#,##0") + "<actual>" +
                   "<remain>" + _resultPenalty[4].ToString("#,##0") + "<remain>" +
                   "<totalpenalty>" + _resultPenalty[6].ToString("#,##0.00") + "<totalpenalty>" +
                   "<total>" + _resultPenalty[7].ToString("#,##0.00") + "<total>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณดอกเบี้ยผิดนัดชำระ
    private void ShowCalInterestOverpayment(HttpContext _c)
    {
        /*
        interestoverpayment
        _send[0] = "capital"
        _send[1] = "overpaymentyear"
        _send[2] = "overpaymentday"
        _send[3] = "overpaymentinterest"
        _send[4] = "overpaymentdatestart"
        _send[5] = "overpaymentdateend"
        _send[6] = "totalinterestpayrepay"
        _send[7] = "totalaccruedinterest"
        */

        string _capital = _c.Request["capital"];
        string _overpaymentYear = _c.Request["overpaymentyear"];
        string _overpaymentDay = _c.Request["overpaymentday"];
        string _overpaymentInterest = _c.Request["overpaymentinterest"];
        string _overpaymentDateStart = _c.Request["overpaymentdatestart"];
        string _overpaymentDateEnd = _c.Request["overpaymentdateend"];
        string _totalInterestPayRepay = _c.Request["totalinterestpayrepay"];
        string _totalAccruedInterest = _c.Request["totalaccruedinterest"];
        string _result = String.Empty;
        double[] _dayOverpayment;
        double _totalInterestOverpayment = 0;
        double _totalInterest = 0;
        double _totalPayment = 0;

        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _dateA = DateTime.Parse(_overpaymentDateStart, _provider);
        DateTime _dateB = DateTime.Parse(_overpaymentDateEnd, _provider);

        _dayOverpayment = Util.CalcDate(_dateA, _dateB);
        _totalInterestOverpayment = eCPUtil.CalInterestOverpayment((!_capital.Equals("0.00") ? _capital : _totalAccruedInterest), _dayOverpayment[4].ToString(), _dayOverpayment[5].ToString(), _overpaymentInterest, _overpaymentDateEnd);
        _totalInterestOverpayment = double.Parse(_totalInterestOverpayment.ToString("#,##0.00"));
        _totalInterest = _totalInterestOverpayment + double.Parse(_totalInterestPayRepay);
        _totalPayment = double.Parse(_capital) + _totalInterest + double.Parse(_totalAccruedInterest);
        //_totalPayment = Util.RoundStang(_totalPayment);
        //_totalInterest = Util.RoundStang(_totalInterest);        
        _totalInterest = double.Parse(_totalInterest.ToString("#,##0.00"));
        _totalPayment = double.Parse(_totalPayment.ToString("#,##0.00"));

        _result += "<overpaymentyear>" + _dayOverpayment[4].ToString("#,##0") + "<overpaymentyear>" +
                   "<overpaymentday>" + _dayOverpayment[5].ToString("#,##0") + "<overpaymentday>" +
                   "<totalinterestoverpayment>" + _totalInterestOverpayment.ToString("#,##0.00") + "<totalinterestoverpayment>" +
                   "<totalinterest>" + _totalInterest.ToString("#,##0.00") + "<totalinterest>" +
                   "<totalpayment>" + _totalPayment.ToString("#,##0.00") + "<totalpayment>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณดอกเบี้ยผ่อนชำระ
    private void ShowCalInterestPayRepay(HttpContext _c)
    {
        /*
        interestpayrepay
        _send[0] = "capital"
        _send[1] = "payrepayyear"
        _send[2] = "payrepayday"
        _send[3] = "payrepayinterest"
        _send[4] = "payrepaydatestart"
        _send[5] = "payrepaydateend"
        _send[6] = "totalinterestoverpayment"
        _send[7] = "totalaccruedinterest"
        */

        string _capital = _c.Request["capital"];
        string _payRepayYear = _c.Request["payrepayyear"];
        string _payRepayDay = _c.Request["payrepayday"];
        string _payRepayInterest = _c.Request["payrepayinterest"];
        string _payRepayDateStart = _c.Request["payrepaydatestart"];
        string _payRepayDateEnd = _c.Request["payrepaydateend"];
        string _totalInterestOverpayment = _c.Request["totalinterestoverpayment"];
        string _totalAccruedInterest = _c.Request["totalaccruedinterest"];
        string _result = String.Empty;
        double[] _dayPayRepay;
        double _totalInterestPayRepay = 0;
        double _totalInterest = 0;
        double _totalPayment = 0;

        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _dateA = DateTime.Parse(_payRepayDateStart, _provider);
        DateTime _dateB = DateTime.Parse(_payRepayDateEnd, _provider);

        _dayPayRepay = Util.CalcDate(_dateA, _dateB);
        _totalInterestPayRepay = eCPUtil.CalInterestOverpayment((!_capital.Equals("0.00") ? _capital : _totalAccruedInterest), _dayPayRepay[4].ToString(), _dayPayRepay[5].ToString(), _payRepayInterest, _payRepayDateEnd);
        _totalInterestPayRepay = double.Parse(_totalInterestPayRepay.ToString("#,##0.00"));
        _totalInterest = _totalInterestPayRepay + double.Parse(_totalInterestOverpayment);
        _totalPayment = double.Parse(_capital) + _totalInterest + double.Parse(_totalAccruedInterest);
        //_totalPayment = Util.RoundStang(_totalPayment);
        //_totalInterest = Util.RoundStang(_totalInterest);        
        _totalInterest = double.Parse(_totalInterest.ToString("#,##0.00"));
        _totalPayment = double.Parse(_totalPayment.ToString("#,##0.00"));

        _result += "<payrepayyear>" + _dayPayRepay[4].ToString("#,##0") + "<payrepayyear>" +
                   "<payrepayday>" + _dayPayRepay[5].ToString("#,##0") + "<payrepayday>" +
                   "<totalinterestpayrepay>" + _totalInterestPayRepay.ToString("#,##0.00") + "<totalinterestpayrepay>" +
                   "<totalinterest>" + _totalInterest.ToString("#,##0.00") + "<totalinterest>" +
                   "<totalpayment>" + _totalPayment.ToString("#,##0.00") + "<totalpayment>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณดอกเบี้ยผิดนัดชำระและดอกเบี้ยผ่อนชำระ
    private void ShowCalInterestOverpaymentAndPayRepay(HttpContext _c)
    {
        /*
        interestoverpaymentandpayrepay
        _send[0] = "capital"
        _send[1] = "overpaymentyear"
        _send[2] = "overpaymentday"
        _send[3] = "overpaymentinterest"
        _send[4] = "overpaymentdatestart"
        _send[5] = "overpaymentdateend"
        _send[6] = "payrepayyear"
        _send[7] = "payrepayday"
        _send[8] = "payrepayinterest"
        _send[9] = "payrepaydatestart"
        _send[10] = "payrepaydateend"
        */

        string _capital = _c.Request["capital"];
        string _overpaymentYear = _c.Request["overpaymentyear"];
        string _overpaymentDay = _c.Request["overpaymentday"];
        string _overpaymentInterest = _c.Request["overpaymentinterest"];
        string _overpaymentDateStart = _c.Request["overpaymentdatestart"];
        string _overpaymentDateEnd = _c.Request["overpaymentdateend"];
        string _payRepayYear = _c.Request["payrepayyear"];
        string _payRepayDay = _c.Request["payrepayday"];
        string _payRepayInterest = _c.Request["payrepayinterest"];
        string _payRepayDateStart = _c.Request["payrepaydatestart"];
        string _payRepayDateEnd = _c.Request["payrepaydateend"];
        string _result = String.Empty;
        double[] _dayOverpayment;
        double _totalInterestOverpayment = 0;
        double[] _dayPayRepay;
        double _totalInterestPayRepay = 0;
        double _totalInterest = 0;
        double _totalPayment = 0;
        IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
        DateTime _dateA;
        DateTime _dateB;

        _dateA = DateTime.Parse(_overpaymentDateStart, _provider);
        _dateB = DateTime.Parse(_overpaymentDateEnd, _provider);

        _dayOverpayment = Util.CalcDate(_dateA, _dateB);
        _totalInterestOverpayment = eCPUtil.CalInterestOverpayment(_capital, _dayOverpayment[4].ToString(), _dayOverpayment[5].ToString(), _overpaymentInterest, _overpaymentDateEnd);
        _totalInterestOverpayment = double.Parse(_totalInterestOverpayment.ToString("#,##0.00"));

        _dateA = DateTime.Parse(_payRepayDateStart, _provider);
        _dateB = DateTime.Parse(_payRepayDateEnd, _provider);

        _dayPayRepay = Util.CalcDate(_dateA, _dateB);
        _totalInterestPayRepay = eCPUtil.CalInterestOverpayment(_capital, _dayPayRepay[4].ToString(), _dayPayRepay[5].ToString(), _payRepayInterest, _payRepayDateEnd);
        _totalInterestPayRepay = double.Parse(_totalInterestPayRepay.ToString("#,##0.00"));

        _totalInterest = _totalInterestOverpayment + _totalInterestPayRepay;
        _totalPayment = double.Parse(_capital) + _totalInterest;
        //_totalPayment = Util.RoundStang(_totalPayment);
        //_totalInterest = Util.RoundStang(_totalInterest);                
        _totalInterest = double.Parse(_totalInterest.ToString("#,##0.00"));
        _totalPayment = double.Parse(_totalPayment.ToString("#,##0.00"));


        _result += "<overpaymentyear>" + _dayOverpayment[4].ToString("#,##0") + "<overpaymentyear>" +
                   "<overpaymentday>" + _dayOverpayment[5].ToString("#,##0") + "<overpaymentday>" +
                   "<totalinterestoverpayment>" + _totalInterestOverpayment.ToString("#,##0.00") + "<totalinterestoverpayment>" +
                   "<payrepayyear>" + _dayPayRepay[4].ToString("#,##0") + "<payrepayyear>" +
                   "<payrepayday>" + _dayPayRepay[5].ToString("#,##0") + "<payrepayday>" +
                   "<totalinterestpayrepay>" + _totalInterestPayRepay.ToString("#,##0.00") + "<totalinterestpayrepay>" +
                   "<totalinterest>" + _totalInterest.ToString("#,##0.00") + "<totalinterest>" +
                   "<totalpayment>" + _totalPayment.ToString("#,##0.00") + "<totalpayment>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณยอดคงเหลือ
    private void ShowCalChkBalance(HttpContext _c)
    {
        /*
        chkbalance
        _send[0] = "capital"
        _send[1] = "totalinterest"
        _send[2] = "totalaccruedinterest"
        _send[3] = "totalpayment"
        _send[4] = "pay"
        */

        string _capital = _c.Request["capital"];
        string _totalInterest = _c.Request["totalinterest"];
        string _totalAccruedInterest = _c.Request["totalaccruedinterest"];
        string _totalPayment = _c.Request["totalpayment"];
        string _pay = _c.Request["pay"];
        string[] _payRemain = new string[5];
        string _result = String.Empty;

        _payRemain = eCPUtil.CalChkBalance(_capital, _totalInterest, _totalAccruedInterest, _totalPayment, _pay);

        _result += "<capital>" + double.Parse(_capital).ToString("#,##0.00") + "<capital>" +
                   "<totalinterest>" + (!String.IsNullOrEmpty(_totalInterest) ? double.Parse(_totalInterest).ToString("#,##0.00") : _totalInterest) + "<totalinterest>" +
                   "<totalaccruedinterest>" + (!String.IsNullOrEmpty(_totalAccruedInterest) ? double.Parse(_totalAccruedInterest).ToString("#,##0.00") : _totalAccruedInterest) + "<totalaccruedinterest>" +
                   "<totalpayment>" + (!String.IsNullOrEmpty(_totalPayment) ? double.Parse(_totalPayment).ToString("#,##0.00") : _totalPayment) + "<totalpayment>" +
                   "<pay>" + (!String.IsNullOrEmpty(_pay) ? double.Parse(_pay).ToString("#,##0.00") : _pay) + "<pay>" +
                   "<paycapital>" + (!String.IsNullOrEmpty(_payRemain[0]) ? double.Parse(_payRemain[0]).ToString("#,##0.00") : _payRemain[0]) + "<paycapital>" +
                   "<payinterest>" + (!String.IsNullOrEmpty(_payRemain[1]) ? double.Parse(_payRemain[1]).ToString("#,##0.00") : _payRemain[1]) + "<payinterest>" +
                   "<remaincapital>" + (!String.IsNullOrEmpty(_payRemain[2]) ? double.Parse(_payRemain[2]).ToString("#,##0.00") : _payRemain[2]) + "<remaincapital>" +
                   "<accruedinterest>" + (!String.IsNullOrEmpty(_payRemain[3]) ? double.Parse(_payRemain[3]).ToString("#,##0.00") : _payRemain[3]) + "<accruedinterest>" +
                   "<remainaccruedinterest>" + (!String.IsNullOrEmpty(_payRemain[4]) ? double.Parse(_payRemain[4]).ToString("#,##0.00") : _payRemain[4]) + "<remainaccruedinterest>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณเงินที่ต้องชดใช้
    private void ShowCalTotalPayment(HttpContext _c)
    {
        /*
        totalpayment
        _send[0] = "capital"
        _send[1] = "totalinterest"
        _send[2] = "totalaccruedinterest"
        */

        double _capital = double.Parse(_c.Request["capital"]);
        double _totalInterest = double.Parse(_c.Request["totalinterest"]);
        double _totalAccruedInterest = double.Parse(_c.Request["totalaccruedinterest"]);
        double _totalPayment = _capital + _totalInterest + _totalAccruedInterest;
        string _result = String.Empty;

        _result += "<totalpayment>" + _totalPayment.ToString("#,##0.00") + "<totalpayment>";

        _c.Response.Write(_result);
    }

    //สำหรับคำนวณตารางเงินต้นและดอกเบี้ย
    private void ShowCalReportTableCalCapitalAndInterest(HttpContext _c)
    {
        /*
        reportrablecalcapitalandinterest
        _send[0] = "capital"
        _send[1] = "interest"
        _send[2] = "pay"
        _send[3] = "paymentdate"
        */

        string _capital = _c.Request["capital"];
        string _interest = _c.Request["interest"];
        string _pay = _c.Request["pay"];
        string _paymentDate = _c.Request["paymentdate"];
        string[,] _data;
        string _result = String.Empty;
        int _recordCount;

        _data = eCPDB.ListCalCPReportTableCalCapitalAndInterest(_capital, _interest, _pay, _paymentDate);
        _recordCount = _data.GetLength(0);
        _result += "<recordcount>" + (_recordCount - 1).ToString("#,##0") + "<recordcount>" +
                   "<list>" + eCPDataReportTableCalCapitalAndInterest.ListTableCalCapitalAndInterest(_data) + "<list>" +
                   "<sumpaycapital>" + double.Parse(_data[_recordCount - 1, 6]).ToString("#,##0.00") + "<sumpaycapital>" +
                   "<sumpayinterest>" + double.Parse(_data[_recordCount - 1, 7]).ToString("#,##0.00") + "<sumpayinterest>" +
                   "<sumtotalpay>" + double.Parse(_data[_recordCount - 1, 8]).ToString("#,##0.00") + "<sumtotalpay>";


        _c.Response.Write(_result);
    }

    //สำหรับส่งออกรายงาน
    private void ShowPrint(HttpContext _c)
    {
        //eCPDB.ConnectStoreProcAddUpdate(eCPDB.InsertTransactionLog("EXPORT", "", "SelectReportExport, " + Request.Form["export-order"], Request.Form["export-send"]));

        string _send = _c.Request["cp1id"] + ":" + _c.Request["action"];

        if (_c.Request["type"].Equals("pdf"))
        {
            switch (_c.Request["order"])
            {
                case "reporttablecalcapitalandinterest"  : { eCPDataReportTableCalCapitalAndInterest.ExportCPReportTableCalCapitalAndInterest(_send); break; }
                case "reportnoticecheckforreimbursement" : { eCPDataReportNoticeCheckForReimbursement.ExportCPReportNoticeCheckForReimbursement(_send); break; }
            }
        }

        if (_c.Request["type"].Equals("word"))
        {
            switch (_c.Request["order"])
            {
                case "reportnoticerepaycomplete" : { eCPDataReportNoticeRepayComplete.ExportCPReportNoticeRepayComplete(_send); break; }
                case "reportnoticeclaimdebt"     : { eCPDataReportNoticeClaimDebt.ExportCPReportNoticeClaimDebt(_send); break; }
            }
        }
    }

    private void ShowDocEContract(HttpContext _c)
    {
        //int _result = (Util.FileSiteExist(_c.Request["path"] + _c.Request["file"]).Equals(true) ? 0 : 1);
        int _result = 0;

        _c.Response.Write("<econtract>" + _result + "<econtract>");
    }

    public bool IsReusable
    {
        get { return false; }
    }
}