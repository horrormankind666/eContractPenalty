<%@ WebHandler Language="C#" Class="eCPHandler" %>

/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๐๕/๐๗/๒๕๖๖>
Description : <สำหรับรับ request แล้วนำมา process แล้วส่ง response กลับไป>
=============================================
*/

using System;
using System.Web;
using System.Web.SessionState;

public class eCPHandler: IHttpHandler, IRequiresSessionState {
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string[] browser = Util.BrowserCapabilities();
        bool error = false;

        if (error.Equals(false) &&
            browser[1].Equals("IE") &&
            int.Parse(browser[3]) < 9) {
            error = true;
            context.Response.Write("<errorbrowser>1<errorbrowser>");
        }

        if (error.Equals(false) &&
            bool.Parse(browser[13]).Equals(false)) {
            error = true;
            context.Response.Write("<errorbrowser>2<errorbrowser>");
        }

        if (error.Equals(false)) {
            string action = context.Request["action"];

            switch (action) {
                case "add":
                case "update":
                case "del":
                    AddUpdateData(context);
                    break;
                case "form":
                    ShowForm(context);
                    break;
                case "setpage":
                    SetPage(context);
                    break;
                case "page":
                    ShowPage(context);
                    break;
                /*
                case "signin":
                    Signin(context);
                    break;
                */
                case "signout":
                    Signout();
                    break;
                case "list":
                case "combobox":
                    ShowList(context);
                    break;
                case "search":
                    ShowSearch(context);
                    break;
                case "resize":
                    ImageProcess.ResizeImage(context.Request["file"], int.Parse(context.Request["width"]), int.Parse(context.Request["height"]));
                    break;
                case "calculate":
                    ShowCalculate(context);
                    break;
                case "print":
                    ShowPrint(context);
                    break;
                case "econtract":
                    ShowDocEContract(context);
                    break;
            }
        }
    }

    private static int error;
    private static int menu;
    private static string menuBar;
    private static string head;
    private static string content;

    private string SetValuePageReturn() {
        return (
            "<error>" + error + "<error>" +
            "<head>" + head + "<head>" +
            "<menubar>" + menuBar + "<menubar>" +
            "<menu>" + menu + "<menu>" +
            "<content>" + content + "<content>"
        );
    }

    private void SetPage(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = 0;
        int pid = 0;

        if (eCPCookie == null) {
            section = 0;
            pid = 0;
        }
        else {
            section = int.Parse(eCPCookie["UserSection"]);
            pid = int.Parse(eCPCookie["Pid"]);
        }

        c.Response.Write(
            "<section>" + section + "<section>" +
            "<page>" + pid + "<page>"
        );
    }

    private void ShowPage(HttpContext c) {
        int loginResult = eCPDB.ChkLogin();
        eCPUtil util = new eCPUtil();

        if (!loginResult.Equals(0)) {
            error = loginResult;
            head = eCPUtil.Head();
            menuBar = string.Empty;
            menu = 0;
            content = string.Empty;
            /*
            content = eCPUtil.Signin();
            */
        }
        else {
            int section = (c.Request["section"].Equals("0") ? 1 : int.Parse(c.Request["section"]));
            int pid = (c.Request["pid"].Equals("0") ? 1 : int.Parse(c.Request["pid"]));

            error = loginResult;
            head = eCPUtil.Head();
            menuBar = eCPUtil.MenuBar(true);
            menu = eCPUtil.activeMenu[(section - 1), (pid - 1)];
            content = util.GenPage(pid - 1);
        }

        c.Response.Write(SetValuePageReturn());
    }
    /*
    private void Signin(HttpContext c)    {
        bool loginResult = eCPDB.Signin(c.Request["authen"]);

        if (!loginResult)
            error = 1;
        else
            error = 0;

        c.Response.Write(SetValuePageReturn());
    }
    */
    private void Signout() {
        eCPDB.Signout();
    }

    private void ShowForm(HttpContext c) {
        string frmOrder = c.Request["frm"];
        string frm = string.Empty;
        string trackingStatus = string.Empty;
        string action = string.Empty;
        string from = string.Empty;
        string title = string.Empty;
        int width = 0;
        int height = 0;

        switch (frmOrder) {
            case "searchcptabuser":
                frm = eCPDataFormSearch.SearchCPTabUser();
                width = 655;
                height = 0;
                title = "search-cp-tab-user";
                break;
            case "addcptabuser":
                frm = eCPDataUser.AddCPTabuser();
                break;
            case "updatecptabuser":
                frm = eCPDataUser.UpdateCPTabUser(c.Request["id"]);
                break;
            case "addcptabprogram":
                frm = eCPDataConfiguration.AddCPTabProgram();
                break;
            case "updatecptabprogram":
                frm = eCPDataConfiguration.UpdateCPTabProgram(c.Request["id"]);
                break;
            case "detailcptabcaldate":
                frm = eCPDataConfiguration.DetailCPTabCalDate(c.Request["id"]);
                width = 750;
                height = 0;
                title = "detail-cp-tab-cal-date";
                break;
            case "addcptabinterest":
                frm = eCPDataConfiguration.AddCPTabInterest();
                break;
            case "updatecptabinterest":
                frm = eCPDataConfiguration.UpdateCPTabInterest(c.Request["id"]);
                break;
            case "addcptabpaybreakcontract":
                frm = eCPDataConfiguration.AddCPTabPayBreakContract();
                break;
            case "updatecptabpaybreakcontract":
                frm = eCPDataConfiguration.UpdateCPTabPayBreakContract(c.Request["id"]);
                break;
            case "addcptabscholarship":
                frm = eCPDataConfiguration.AddCPTabScholarship();
                break;
            case "updatecptabscholarship":
                frm = eCPDataConfiguration.UpdateCPTabScholarship(c.Request["id"]);
                break;
            case "addprofilestudent":
                frm = eCPDataBreakContract.AddProfileStudent();
                width = 831;
                height = 0;
                title = "add-profile-student";
                break;
            case "searchstudentwithresult":
                frm = eCPDataFormSearch.SearchStudentWithResult();
                width = 900;
                height = 0;
                title = "search-student";
                break;
            case "addcptransbreakcontract":
                frm = eCPDataBreakContract.AddCPTransBreakContract();
                break;
            case "updatecptransbreakcontract":
                frm = eCPDataBreakContract.UpdateCPTransBreakContract(c.Request["id"]);
                break;
            case "searchcptransbreakcontract":
                frm =  eCPDataFormSearch.SearchCPTransBreakContract();
                width = 655;
                height = 0;
                title = "search-cp-trans-break-contract";
                break;
            case "detailtrackingstatus":
                frm = "<div id='status-tracking-explanation'></div>";
                width = 350;
                height = 0;
                title = "detail-tracking-status";
                break;
            case "detailcptransbreakcontract":
            case "detailcptransrequirecontract":
            case "detailcptransrequirerepaycontract":
            case "receivercptransbreakcontract":
            case "repaycptransrequirecontract":
            case "repaycptransrequirecontract1":
                trackingStatus = (frmOrder.Equals("detailcptransbreakcontract") ? "v1" : trackingStatus);
                trackingStatus = (frmOrder.Equals("detailcptransrequirecontract") ? "v2" : trackingStatus);
                trackingStatus = (frmOrder.Equals("detailcptransrequirerepaycontract") ? "v3" : trackingStatus);
                trackingStatus = (frmOrder.Equals("receivercptransbreakcontract") ? "a" : trackingStatus);
                trackingStatus = (frmOrder.Equals("repaycptransrequirecontract") ? "r" : trackingStatus);
                trackingStatus = (frmOrder.Equals("repaycptransrequirecontract1") ? "r1" : trackingStatus);

                if (trackingStatus.Equals("v1") ||
                    trackingStatus.Equals("a")) {
                    frm = eCPDataBreakContract.DetailCPTransBreakContract(c.Request["id"], trackingStatus);
                    width = 900;
                    height = 0;
                    title = "detail-cp-trans-break-contract";
                }

                if (trackingStatus.Equals("v2") ||
                    trackingStatus.Equals("v3") ||
                    trackingStatus.Equals("r") ||
                    trackingStatus.Equals("r1")) {
                    frm = eCPDataRequireContract.DetailCPTransRequireContract(c.Request["id"], trackingStatus);
                    width = 900;
                    height = 0;
                    title = (trackingStatus.Equals("v2") || trackingStatus.Equals("v3") ? "detail-cp-trans-require-contract" : "repay-cp-trans-require-contract");
                }
                break;
            case "addcommenteditbreakcontract":
            case "addcommentcancelbreakcontract":
            case "addcommentcancelrequirecontract":
            case "addcommentcancelrepaycontract":
                action = (frmOrder.Equals("addcommenteditbreakcontract") ? "E" : action);
                action = (frmOrder.Equals("addcommentcancelbreakcontract") ? "C" : action);
                action = (frmOrder.Equals("addcommentcancelrequirecontract") ? "C" : action);
                action = (frmOrder.Equals("addcommentcancelrepaycontract") ? "C" : action);

                from = (frmOrder.Equals("addcommenteditbreakcontract") ? "breakcontract" : from);
                from = (frmOrder.Equals("addcommentcancelbreakcontract") ? "breakcontract" : from);
                from = (frmOrder.Equals("addcommentcancelrequirecontract") ? "requirecontract" : from);
                from = (frmOrder.Equals("addcommentcancelrepaycontract") ? "repaycontract" : from);

                frm = eCPDataBreakContract.AddCommentBreakContract(c.Request["id"], action, from);
                width = 480;
                height = 0;
                title = ("add-comment-" + action + "-break-contract");
                break;
            case "addcptransrequirecontract":
                frm = eCPDataRequireContract.AddCPTransRequireContract(c.Request["id"]);
                break;
            case "updatecptransrequirecontract":
                frm = eCPDataRequireContract.UpdateCPTransRequireContract(c.Request["id"]);
                break;
            case "detailrepaystatus":
                frm = "<div id='status-repay-explanation'></div>";
                width = 406;
                height = 0;
                title = "detail-repay-status";
                break;
            case "searchcptransrepaycontract":
                frm = eCPDataFormSearch.SearchCPTransRepayContract();
                width = 655;
                height = 0;
                title = "search-cp-trans-repay-contract";
                break;
            case "addupdaterepaycontract":
                frm = eCPDataRepay.AddUpdateCPTransRepayContract(c.Request["id"]);
                width = 680;
                height = 0;
                title = "addupdate-repay-contract";
                break;
            case "viewrepaycontract":
                frm = eCPDataRepay.ViewCPTransRepayContract(c.Request["id"]);
                width = 680;
                height = 0;
                title = "addupdate-repay-contract";
                break;
            case "calinterest":
                frm = eCPDataRepay.DetailCalInterestOverpayment(c.Request["id"]);
                width = 721;
                height = 0;
                title = "cal-interest";
                break;
            case "detailpaymentstatus":
                frm = "<div id='status-payment-explanation'></div>";
                width = 350;
                height = 146;
                title = "detail-payment-status";
                break;
            case "adddetailcptranspayment":
                frm = eCPDataPayment.TabAddDetailCPTransPayment(c.Request["id"]);
                width = 900;
                height = 0;
                title = "adddetail-cp-trans-payment";
                break;
            case "searchcptranspayment":
                frm = eCPDataFormSearch.SearchCPTransPayment();
                width = 655;
                height = 0;
                title = "search-cp-trans-payment";
                break;
            case "selectformatpayment":
                frm = eCPDataPayment.SelectFormatPayment(c.Request["id"]);
                width = 500;
                height = 0;
                title = "select-format-payment";
                break;
            case "detailcptranspayment":
                frm = eCPDataPayment.AddDetailCPTransPayment(c.Request["id"], "detail");
                break;
            case "detailtranspayment":
                frm = eCPDataPayment.DetailTransPayment(c.Request["id"]);
                width = 990;
                height = 0;
                title = "detail-trans-payment";
                break;
            case "addcptranspaymentfullrepay":
                frm = eCPDataPayment.AddDetailCPTransPayment(c.Request["id"], "addfullrepay");
                break;
            case "addcptranspaymentpayrepay":
                frm = eCPDataPayment.AddDetailCPTransPayment(c.Request["id"], "addpayrepay");
                break;
            case "addupdatecptransprosecution":
                frm = eCPDataProsecution.AddUpdateCPTransProsecution(c.Request["id"]);
                break;
            case "adddetailpaychannel":
                frm = eCPDataPayment.AddDetailPayChannel(c.Request["id"]);
                width = 557;
                height = 0;
                title = "add-detail-pay-channel";
                break;
            case "chkbalance":
                frm = eCPDataPayment.ChkBalance();
                width = 557;
                height = 0;
                title = "chk-balance";
                break;
            case "searchcpreporttablecalcapitalandinterest":
                frm = eCPDataFormSearch.SearchCPReportTableCalCapitalAndInterest();
                width = 655;
                height = 0;
                title = "search-cp-report-table-cal-capital-and-interest";
                break;
            case "calreporttablecalcapitalandinterest":
                frm = eCPDataReportTableCalCapitalAndInterest.CalReportTableCalCapitalAndInterest(c.Request["id"]);
                break;
            case "searchcpreportstepofwork":
                frm = eCPDataFormSearch.SearchCPReportStepOfWork();
                width = 655;
                height = 0;
                title = "search-cp-report-step-of-work";
                break;
            case "reportstepofworkonstatisticrepaybyprogram":
                frm = eCPDataReportStatisticRepay.ListReportStepOfWorkOnStatisticRepayByProgram();
                width = 750;
                height = 0;
                title = "report-step-of-work-on-statistic-repay-by-program";
                break;
            case "reportstudentonstatisticcontractbyprogram":
                frm = eCPDataReportStatisticContract.ListReportStudentOnStatisticContractByProgram();
                width = 750;
                height = 0;
                title = "report-student-on-statistic-contract-by-program";
                break;
            case "searchcpreportnoticerepaycomplete":
                frm = eCPDataFormSearch.SearchCPReportNoticeRepayComplete();
                width = 655;
                height = 0;
                title = "search-cp-report-notice-repay-complete";
                break;
            case "searchcpreportnoticeclaimdebt":
                frm = eCPDataFormSearch.SearchCPReportNoticeClaimDebt();
                width = 655;
                height = 0;
                title = "search-cp-report-notice-claim-debt";
                break;
            case "searchcpreportpaymentbydate":
                frm = eCPDataFormSearch.SearchCPReportStatisticPaymentByDate();
                width = 655;
                height = 0;
                title = "search-cp-report-statistic-payment-by-date";
                break;
            case "viewtranspaymentbydate":
                frm = eCPDataReportStatisticPaymentByDate.ViewTransPaymentByDate(c.Request["id"]);
                width = 950;
                height = 0;
                title = "view-trans-payment-by-date";
                break;
            case "viewtranspayment":
                frm = eCPDataReportDebtorContract.ViewTransPayment(c.Request["id"]);
                width = 950;
                height = 0;
                title = "view-trans-payment";
                break;
            case "manual":
                frm = eCPUtil.Manual();
                width = 570;
                height = 0;
                title = "manual";
                break;
            case "detailecontractstatus":
                frm = "<div id='status-e-contract'></div>";
                width = 350;
                height = 117;
                title = "detail-e-contract-status";
                break;
            case "searchcpreportecontract":
                frm = eCPDataFormSearch.SearchCPReportEContract();
                width = 655;
                height = 0;
                title = "search-cp-report-e-contract";
                break;
            case "searchcpreportdebtorcontract":
                frm = eCPDataFormSearch.SearchCPReportDebtorContract();
                width = 655;
                height = 0;
                title = "search-cp-report-debtor-contract";
                break;
            case "searchstudentdebtorcontractbyprogram":
                frm = eCPDataFormSearch.SearchStudentDebtorContractByProgram();
                width = 750;
                height = 0;
                title = "search-cp-report-debtor-contract-by-program";
                break;
        }

        c.Response.Write(
            "<form>" + frm + "<form>" +
            "<width>" + width + "<width>" +
            "<height>" + height + "<height>" +
            "<title>" + title + "<title>"
        );
    }

    private void AddUpdateData(HttpContext c) {
        string listUpdate = string.Empty;
        int loginResult = eCPDB.ChkLogin();

        error = 0;

        if (!loginResult.Equals(0))
            error = 1;
        else {
            if (c.Request["cmd"].Equals("addcptabuser") ||
                c.Request["cmd"].Equals("updatecptabuser"))
                error = (eCPDB.CheckRepeatCPTabUser(c, "username") > 0 ? 2 : error);
                /*
                error = (eCPDB.CheckRepeatCPTabUser(c, "username") > 0 ? 2 : (eCPDB.CheckRepeatCPTabUser(c, "password") > 0 ? 3 : error));
                */

            if (c.Request["cmd"].Equals("addcptabprogram") ||
                c.Request["cmd"].Equals("updatecptabprogram"))
                error = (eCPDB.CheckRepeatCPTabProgram(c) > 0 ? 2 : error);

            if (c.Request["cmd"].Equals("addcptabpaybreakcontract") ||
                c.Request["cmd"].Equals("updatecptabpaybreakcontract"))
                error = (eCPDB.CheckRepeatCPTabPayBreakContract(c) > 0 ? 2 : error);

            if (c.Request["cmd"].Equals("addcptabscholarship") ||
                c.Request["cmd"].Equals("updatecptabscholarship"))
                error = (eCPDB.CheckRepeatCPTabScholarship(c) > 0 ? 2 : error);

            if (error == 0)
                eCPDB.AddUpdateData(c);
        }

        c.Response.Write(
            "<error>" + error + "<error>" +
            listUpdate
        );
    }

    private void ShowList(HttpContext c) {
        string listOrder = c.Request["list"];
        string listData = string.Empty;

        switch (listOrder) {
            case "program":
                listData = eCPUtil.ListProgram(false, "program", c.Request["dlevel"], c.Request["faculty"]);
                break;
            case "programcptabprogram":
            case "programtransbreakcontract":
            case "programtransrepaycontract":
            case "programtranspayment":
            case "programreporttablecalcapitalandinterest":
            case "programreportstepofwork":
            case "programsearchstudent":
            case "programprofilestudent":
            case "programreportnoticerepaycomplete":
            case "programreportnoticeclaimdebt":
            case "programreportstatisticpaymentbydate":
            case "programreportecontract":
                listData = eCPUtil.ListProgram(true, listOrder, c.Request["dlevel"], c.Request["faculty"]);
                break;
            case "cpprogram":
                listData = eCPDataConfiguration.ListUpdateCPTabProgram();
                break;
            case "interest":
                listData = eCPDataConfiguration.ListUpdateCPTabInterest();
                break;
            case "pay-break-contract":
                listData = eCPDataConfiguration.ListUpdateCPTabPayBreakContract();
                break;
            case "scholarship":
                listData = eCPDataConfiguration.ListUpdateCPTabScholarship();
                break;
            case "cpreportstatisticrepay":
                listData = eCPDataReportStatisticRepay.ListUpdateCPReportStatisticRepay();
                break;
            case "cpreportstatisticrecontract":
                listData = eCPDataReportStatisticContract.ListUpdateCPReportStatisticContract();
                break;
        }

        c.Response.Write(listData);
    }

    private void ShowSearch(HttpContext c) {
        string searchFrom = c.Request["from"];
        string listData = string.Empty;

        switch (searchFrom) {
            case "tabuser":
                listData = eCPDataUser.ListCPTabUser(c);
                break;
            case "studentwithresult":
                listData = eCPDataFormSearch.ListSearchStudentWithResult(c);
                break;
            case "profilestudent":
                listData = eCPDataFormSearch.ListProfileStudent(c.Request["studentid"]);
                break;
            case "scholarship":
                listData = eCPDataConfiguration.ListSearchCPTabScholarship(c);
                break;
            case "paybreakcontract":
                listData = eCPDataConfiguration.ListSearchCPTabPayBreakContract(c);
                break;
            case "scholarshipandpaybreakcontract":
                listData = eCPDataConfiguration.ListSearchScholarshipAndPayBreakContract(c);
                break;
            case "studenttransbreakcontract":
                listData = eCPDataBreakContract.ListSearchStudentCPTransBreakContract(c.Request["studentid"]);
                break;
            case "transbreakcontract":
                listData = eCPDataBreakContract.ListCPTransBreakContract(c);
                break;
            case "trackingstatustransbreakcontract":
                listData = eCPDataBreakContract.ListSearchTrackingStatusCPTransBreakContract(c.Request["cp1id"]);
                break;
            case "repaystatustransrequirecontract":
                listData = eCPDataRequireContract.ListSearchRepayStatusCPTransRequireContract(c.Request["cp1id"]);
                break;
            case "transrepaycontract":
                listData = eCPDataRepay.ListRepay(c);
                break;
            case "repaystatuscalinterest":
                listData = eCPDataRepay.ListSearchRepayStatusCalInterestOverpayment(c.Request["cp2id"]);
                break;
            case "transpayment":
                listData = eCPDataPayment.ListPaymentOnCPTransRequireContract(c);
                break;
            case "formatpayment":
                listData = eCPUtil.paymentFormat[int.Parse(c.Request["formatpayment"]) - 1];
                break;
            case "reporttablecalcapitalandinterest":
                listData = eCPDataReportTableCalCapitalAndInterest.ListCPReportTableCalCapitalAndInterest(c);
                break;
            case "reportstepofwork":
                listData = eCPDataReportStepOfWork.ListCPReportStepOfWork(c);
                break;
            case "reportstatisticrepaybyprogram":
                listData = eCPDataReportStatisticRepay.ListCPReportStatisticRepayByProgram(c.Request["acadamicyear"]);
                break;
            case "reportstepofworkonstatisticrepaybyprogram":
                listData = eCPDataReportStatisticRepay.ListReportStepOfWorkOnStatisticRepayByProgram(c);
                break;
            case "reportstatisticcontractbyprogram":
                listData = eCPDataReportStatisticContract.ListCPReportStatisticContractByProgram(c.Request["acadamicyear"]);
                break;
            case "reportstudentonstatisticcontractbyprogram":
                listData = eCPDataReportStatisticContract.ListReportStudentOnStatisticContractByProgram(c);
                break;
            case "reportnoticerepaycomplete":
                listData = eCPDataReportNoticeRepayComplete.ListCPReportNoticeRepayComplete(c);
                break;
            case "reportnoticeclaimdebt":
                listData = eCPDataReportNoticeClaimDebt.ListCPReportNoticeClaimDebt(c);
                break;
            case "reportstatisticpaymentbydate":
                listData = eCPDataReportStatisticPaymentByDate.ListCPReportStatisticPaymentByDate(c);
                break;
            case "reportecontract":
                listData = eCPDataReportEContract.ListCPReportEContract(c);
                break;
            case "reportdebtorcontract":
                listData = eCPDataReportDebtorContract.ListCPReportDebtorContract(c);
                break;
            case "reportdebtorcontractbyprogram":
                listData = eCPDataReportDebtorContract.ListCPReportDebtorContractByProgram(c);
                break;
        }

        c.Response.Write(listData);
    }

    private void ShowCalculate(HttpContext c) {
        string cal = c.Request["cal"];

        switch (cal) {
            case "scholarshipandpenalty":
                ShowCalScholarshipPenalty(c);
                break;
            case "interestoverpayment":
                ShowCalInterestOverpayment(c);
                break;
            case "interestpayrepay":
                ShowCalInterestPayRepay(c);
                break;
            case "interestoverpaymentandpayrepay":
                ShowCalInterestOverpaymentAndPayRepay(c);
                break;
            case "chkbalance":
                ShowCalChkBalance(c);
                break;
            case "totalpayment":
                ShowCalTotalPayment(c);
                break;
            case "reporttablecalcapitalandinterest":
                ShowCalReportTableCalCapitalAndInterest(c);
                break;
        }
    }

    private void ShowCalScholarshipPenalty(HttpContext c) {
        string scholar = c.Request["scholar"];
        string scholarshipMoney = c.Request["scholarshipmoney"];
        string scholarshipYear = c.Request["scholarshipyear"];
        string scholarshipMonth = c.Request["scholarshipmonth"];
        string allActualMonthScholarship = c.Request["allactualmonthscholarship"];
        string caseGraduate = c.Request["casegraduate"];
        string educationDate = c.Request["educationdate"];
        string graduateDate = c.Request["graduatedate"];
        string civil = c.Request["civil"];
        string dateStart = c.Request["datestart"];
        string dateEnd = c.Request["dateend"];
        string indemnitorYear = c.Request["indemnitoryear"];
        string indemnitorCash = c.Request["indemnitorcash"];
        string calDateCondition = c.Request["caldatecondition"];
        string studyLeave = c.Request["studyleave"];
        string beforeStudyLeaveStartDate = c.Request["beforestudyleavestartdate"];
        string beforeStudyLeaveEndDate = c.Request["beforestudyleaveenddate"];
        string studyLeaveStartDate = c.Request["studyleavestartdate"];
        string studyLeaveEndDate = c.Request["studyleaveenddate"];
        string afterStudyLeaveStartDate = c.Request["afterstudyleavestartdate"];
        string afterStudyLeaveEndDate = c.Request["afterstudyleaveenddate"];
        double[] resultPayScholarship = eCPUtil.CalPayScholarship(scholar, caseGraduate, civil, scholarshipMoney, scholarshipYear, scholarshipMonth, allActualMonthScholarship);
        double[] resultPenalty = eCPUtil.GetCalPenalty(studyLeave, beforeStudyLeaveStartDate, beforeStudyLeaveEndDate, afterStudyLeaveStartDate, afterStudyLeaveEndDate, scholar, caseGraduate, educationDate, graduateDate, civil, resultPayScholarship[1].ToString(), scholarshipYear, scholarshipMonth, dateStart, dateEnd, indemnitorYear, indemnitorCash, calDateCondition);

        c.Response.Write(
            "<allactualscholarship>" + resultPayScholarship[0].ToString("#,##0.00") + "<allactualscholarship>" +
            "<totalpayscholarship>" + resultPenalty[5].ToString("#,##0.00") + "<totalpayscholarship>" +
            "<month>" + resultPenalty[0].ToString("#,##0") + "<month>" +
            "<day>" + resultPenalty[1].ToString("#,##0") + "<day>" +
            "<allactual>" + resultPenalty[2].ToString("#,##0") + "<allactual>" +
            "<actual>" + resultPenalty[3].ToString("#,##0") + "<actual>" +
            "<remain>" + resultPenalty[4].ToString("#,##0") + "<remain>" +
            "<totalpenalty>" + resultPenalty[6].ToString("#,##0.00") + "<totalpenalty>" +
            "<total>" + resultPenalty[7].ToString("#,##0.00") + "<total>"
        );
    }

    private void ShowCalInterestOverpayment(HttpContext c) {
        /*
        interestoverpayment
        send[0] = "capital"
        send[1] = "overpaymentyear"
        send[2] = "overpaymentday"
        send[3] = "overpaymentinterest"
        send[4] = "overpaymentdatestart"
        send[5] = "overpaymentdateend"
        send[6] = "totalinterestpayrepay"
        send[7] = "totalaccruedinterest"
        */

        string capital = c.Request["capital"];
        string overpaymentYear = c.Request["overpaymentyear"];
        string overpaymentDay = c.Request["overpaymentday"];
        string overpaymentInterest = c.Request["overpaymentinterest"];
        string overpaymentDateStart = c.Request["overpaymentdatestart"];
        string overpaymentDateEnd = c.Request["overpaymentdateend"];
        string totalInterestPayRepay = c.Request["totalinterestpayrepay"];
        string totalAccruedInterest = c.Request["totalaccruedinterest"];
        double[] dayOverpayment;
        double totalInterestOverpayment = 0;
        double totalInterest = 0;
        double totalPayment = 0;

        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA = DateTime.Parse(overpaymentDateStart, provider);
        DateTime dateB = DateTime.Parse(overpaymentDateEnd, provider);

        dayOverpayment = Util.CalcDate(dateA, dateB);
        totalInterestOverpayment = eCPUtil.CalInterestOverpayment((!capital.Equals("0.00") ? capital : totalAccruedInterest), dayOverpayment[4].ToString(), dayOverpayment[5].ToString(), overpaymentInterest, overpaymentDateEnd);
        totalInterestOverpayment = double.Parse(totalInterestOverpayment.ToString("#,##0.00"));
        totalInterest = (totalInterestOverpayment + double.Parse(totalInterestPayRepay));
        totalPayment = (double.Parse(capital) + totalInterest + double.Parse(totalAccruedInterest));
        /*
        totalPayment = Util.RoundStang(totalPayment);
        totalInterest = Util.RoundStang(totalInterest);
        */
        totalInterest = double.Parse(totalInterest.ToString("#,##0.00"));
        totalPayment = double.Parse(totalPayment.ToString("#,##0.00"));

        c.Response.Write(
            "<overpaymentyear>" + dayOverpayment[4].ToString("#,##0") + "<overpaymentyear>" +
            "<overpaymentday>" + dayOverpayment[5].ToString("#,##0") + "<overpaymentday>" +
            "<totalinterestoverpayment>" + totalInterestOverpayment.ToString("#,##0.00") + "<totalinterestoverpayment>" +
            "<totalinterest>" + totalInterest.ToString("#,##0.00") + "<totalinterest>" +
            "<totalpayment>" + totalPayment.ToString("#,##0.00") + "<totalpayment>"
        );
    }

    private void ShowCalInterestPayRepay(HttpContext c) {
        /*
        interestpayrepay
        send[0] = "capital"
        send[1] = "payrepayyear"
        send[2] = "payrepayday"
        send[3] = "payrepayinterest"
        send[4] = "payrepaydatestart"
        send[5] = "payrepaydateend"
        send[6] = "totalinterestoverpayment"
        send[7] = "totalaccruedinterest"
        */

        string capital = c.Request["capital"];
        string payRepayYear = c.Request["payrepayyear"];
        string payRepayDay = c.Request["payrepayday"];
        string payRepayInterest = c.Request["payrepayinterest"];
        string payRepayDateStart = c.Request["payrepaydatestart"];
        string payRepayDateEnd = c.Request["payrepaydateend"];
        string totalInterestOverpayment = c.Request["totalinterestoverpayment"];
        string totalAccruedInterest = c.Request["totalaccruedinterest"];
        double[] dayPayRepay;
        double totalInterestPayRepay = 0;
        double totalInterest = 0;
        double totalPayment = 0;

        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA = DateTime.Parse(payRepayDateStart, provider);
        DateTime dateB = DateTime.Parse(payRepayDateEnd, provider);

        dayPayRepay = Util.CalcDate(dateA, dateB);
        totalInterestPayRepay = eCPUtil.CalInterestOverpayment((!capital.Equals("0.00") ? capital : totalAccruedInterest), dayPayRepay[4].ToString(), dayPayRepay[5].ToString(), payRepayInterest, payRepayDateEnd);
        totalInterestPayRepay = double.Parse(totalInterestPayRepay.ToString("#,##0.00"));
        totalInterest = (totalInterestPayRepay + double.Parse(totalInterestOverpayment));
        totalPayment = (double.Parse(capital) + totalInterest + double.Parse(totalAccruedInterest));
        /*
        totalPayment = Util.RoundStang(totalPayment);
        totalInterest = Util.RoundStang(totalInterest);
        */
        totalInterest = double.Parse(totalInterest.ToString("#,##0.00"));
        totalPayment = double.Parse(totalPayment.ToString("#,##0.00"));

        c.Response.Write(
            "<payrepayyear>" + dayPayRepay[4].ToString("#,##0") + "<payrepayyear>" +
            "<payrepayday>" + dayPayRepay[5].ToString("#,##0") + "<payrepayday>" +
            "<totalinterestpayrepay>" + totalInterestPayRepay.ToString("#,##0.00") + "<totalinterestpayrepay>" +
            "<totalinterest>" + totalInterest.ToString("#,##0.00") + "<totalinterest>" +
            "<totalpayment>" + totalPayment.ToString("#,##0.00") + "<totalpayment>"
        );
    }

    private void ShowCalInterestOverpaymentAndPayRepay(HttpContext c) {
        /*
        interestoverpaymentandpayrepay
        send[0] = "capital"
        send[1] = "overpaymentyear"
        send[2] = "overpaymentday"
        send[3] = "overpaymentinterest"
        send[4] = "overpaymentdatestart"
        send[5] = "overpaymentdateend"
        send[6] = "payrepayyear"
        send[7] = "payrepayday"
        send[8] = "payrepayinterest"
        send[9] = "payrepaydatestart"
        send[10] = "payrepaydateend"
        */

        string capital = c.Request["capital"];
        string overpaymentYear = c.Request["overpaymentyear"];
        string overpaymentDay = c.Request["overpaymentday"];
        string overpaymentInterest = c.Request["overpaymentinterest"];
        string overpaymentDateStart = c.Request["overpaymentdatestart"];
        string overpaymentDateEnd = c.Request["overpaymentdateend"];
        string payRepayYear = c.Request["payrepayyear"];
        string payRepayDay = c.Request["payrepayday"];
        string payRepayInterest = c.Request["payrepayinterest"];
        string payRepayDateStart = c.Request["payrepaydatestart"];
        string payRepayDateEnd = c.Request["payrepaydateend"];
        double[] dayOverpayment;
        double totalInterestOverpayment = 0;
        double[] dayPayRepay;
        double totalInterestPayRepay = 0;
        double totalInterest = 0;
        double totalPayment = 0;

        IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
        DateTime dateA = DateTime.Parse(overpaymentDateStart, provider);
        DateTime dateB = DateTime.Parse(overpaymentDateEnd, provider);

        dayOverpayment = Util.CalcDate(dateA, dateB);
        totalInterestOverpayment = eCPUtil.CalInterestOverpayment(capital, dayOverpayment[4].ToString(), dayOverpayment[5].ToString(), overpaymentInterest, overpaymentDateEnd);
        totalInterestOverpayment = double.Parse(totalInterestOverpayment.ToString("#,##0.00"));

        dateA = DateTime.Parse(payRepayDateStart, provider);
        dateB = DateTime.Parse(payRepayDateEnd, provider);

        dayPayRepay = Util.CalcDate(dateA, dateB);
        totalInterestPayRepay = eCPUtil.CalInterestOverpayment(capital, dayPayRepay[4].ToString(), dayPayRepay[5].ToString(), payRepayInterest, payRepayDateEnd);
        totalInterestPayRepay = double.Parse(totalInterestPayRepay.ToString("#,##0.00"));

        totalInterest = (totalInterestOverpayment + totalInterestPayRepay);
        totalPayment = (double.Parse(capital) + totalInterest);
        /*
        totalPayment = Util.RoundStang(totalPayment);
        totalInterest = Util.RoundStang(totalInterest);
        */
        totalInterest = double.Parse(totalInterest.ToString("#,##0.00"));
        totalPayment = double.Parse(totalPayment.ToString("#,##0.00"));


        c.Response.Write(
            "<overpaymentyear>" + dayOverpayment[4].ToString("#,##0") + "<overpaymentyear>" +
            "<overpaymentday>" + dayOverpayment[5].ToString("#,##0") + "<overpaymentday>" +
            "<totalinterestoverpayment>" + totalInterestOverpayment.ToString("#,##0.00") + "<totalinterestoverpayment>" +
            "<payrepayyear>" + dayPayRepay[4].ToString("#,##0") + "<payrepayyear>" +
            "<payrepayday>" + dayPayRepay[5].ToString("#,##0") + "<payrepayday>" +
            "<totalinterestpayrepay>" + totalInterestPayRepay.ToString("#,##0.00") + "<totalinterestpayrepay>" +
            "<totalinterest>" + totalInterest.ToString("#,##0.00") + "<totalinterest>" +
            "<totalpayment>" + totalPayment.ToString("#,##0.00") + "<totalpayment>"
        );
    }

    private void ShowCalChkBalance(HttpContext c) {
        /*
        chkbalance
        send[0] = "capital"
        send[1] = "totalinterest"
        send[2] = "totalaccruedinterest"
        send[3] = "totalpayment"
        send[4] = "pay"
        */

        string capital = c.Request["capital"];
        string totalInterest = c.Request["totalinterest"];
        string totalAccruedInterest = c.Request["totalaccruedinterest"];
        string totalPayment = c.Request["totalpayment"];
        string pay = c.Request["pay"];
        string[] payRemain = eCPUtil.CalChkBalance(capital, totalInterest, totalAccruedInterest, totalPayment, pay);
        string result = string.Empty;

        c.Response.Write(
            "<capital>" + double.Parse(capital).ToString("#,##0.00") + "<capital>" +
            "<totalinterest>" + (!string.IsNullOrEmpty(totalInterest) ? double.Parse(totalInterest).ToString("#,##0.00") : totalInterest) + "<totalinterest>" +
            "<totalaccruedinterest>" + (!string.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(totalAccruedInterest).ToString("#,##0.00") : totalAccruedInterest) + "<totalaccruedinterest>" +
            "<totalpayment>" + (!string.IsNullOrEmpty(totalPayment) ? double.Parse(totalPayment).ToString("#,##0.00") : totalPayment) + "<totalpayment>" +
            "<pay>" + (!string.IsNullOrEmpty(pay) ? double.Parse(pay).ToString("#,##0.00") : pay) + "<pay>" +
            "<paycapital>" + (!string.IsNullOrEmpty(payRemain[0]) ? double.Parse(payRemain[0]).ToString("#,##0.00") : payRemain[0]) + "<paycapital>" +
            "<payinterest>" + (!string.IsNullOrEmpty(payRemain[1]) ? double.Parse(payRemain[1]).ToString("#,##0.00") : payRemain[1]) + "<payinterest>" +
            "<remaincapital>" + (!string.IsNullOrEmpty(payRemain[2]) ? double.Parse(payRemain[2]).ToString("#,##0.00") : payRemain[2]) + "<remaincapital>" +
            "<accruedinterest>" + (!string.IsNullOrEmpty(payRemain[3]) ? double.Parse(payRemain[3]).ToString("#,##0.00") : payRemain[3]) + "<accruedinterest>" +
            "<remainaccruedinterest>" + (!string.IsNullOrEmpty(payRemain[4]) ? double.Parse(payRemain[4]).ToString("#,##0.00") : payRemain[4]) + "<remainaccruedinterest>"
        );
    }

    private void ShowCalTotalPayment(HttpContext c) {
        /*
        totalpayment
        send[0] = "capital"
        send[1] = "totalinterest"
        send[2] = "totalaccruedinterest"
        */

        double capital = double.Parse(c.Request["capital"]);
        double totalInterest = double.Parse(c.Request["totalinterest"]);
        double totalAccruedInterest = double.Parse(c.Request["totalaccruedinterest"]);
        double totalPayment = (capital + totalInterest + totalAccruedInterest);

        c.Response.Write("<totalpayment>" + totalPayment.ToString("#,##0.00") + "<totalpayment>");
    }

    private void ShowCalReportTableCalCapitalAndInterest(HttpContext c) {
        /*
        reportrablecalcapitalandinterest
        send[0] = "capital"
        send[1] = "interest"
        send[2] = "pay"
        send[3] = "paymentdate"
        */

        string capital = c.Request["capital"];
        string interest = c.Request["interest"];
        string pay = c.Request["pay"];
        string paymentDate = c.Request["paymentdate"];
        string[,] data = eCPDB.ListCalCPReportTableCalCapitalAndInterest(capital, interest, pay, paymentDate);
        int recordCount = data.GetLength(0);

        c.Response.Write(
            "<recordcount>" + (recordCount - 1).ToString("#,##0") + "<recordcount>" +
            "<list>" + eCPDataReportTableCalCapitalAndInterest.ListTableCalCapitalAndInterest(data) + "<list>" +
            "<sumpaycapital>" + double.Parse(data[(recordCount - 1), 6]).ToString("#,##0.00") + "<sumpaycapital>" +
            "<sumpayinterest>" + double.Parse(data[(recordCount - 1), 7]).ToString("#,##0.00") + "<sumpayinterest>" +
            "<sumtotalpay>" + double.Parse(data[(recordCount - 1), 8]).ToString("#,##0.00") + "<sumtotalpay>"
        );
    }

    private void ShowPrint(HttpContext c) {
        /*
        eCPDB.ConnectStoreProcAddUpdate(eCPDB.InsertTransactionLog("EXPORT", "", "SelectReportExport, " + Request.Form["export-order"], Request.Form["export-send"]));
        */

        string send = (c.Request["cp1id"] + ":" + c.Request["action"]);

        if (c.Request["type"].Equals("pdf")) {
            switch (c.Request["order"]) {
                case "reporttablecalcapitalandinterest":
                    eCPDataReportTableCalCapitalAndInterest.ExportCPReportTableCalCapitalAndInterest(send);
                    break;
                case "reportnoticecheckforreimbursement":
                    eCPDataReportNoticeCheckForReimbursement.ExportCPReportNoticeCheckForReimbursement(send);
                    break;
            }
        }

        if (c.Request["type"].Equals("word")) {
            switch (c.Request["order"]) {
                case "reportnoticerepaycomplete":
                    eCPDataReportNoticeRepayComplete.ExportCPReportNoticeRepayComplete(send);
                    break;
                case "reportnoticeclaimdebt":
                    eCPDataReportNoticeClaimDebt.ExportCPReportNoticeClaimDebt(send);
                    break;
            }
        }
    }

    private void ShowDocEContract(HttpContext c) {
        /*
        int result = (Util.FileSiteExist(c.Request["path"] + (c.Request["file"]).Equals(true) ? 0 : 1));
        */
        int result = 0;

        c.Response.Write("<econtract>" + result + "<econtract>");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}