<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eCPPrinting.aspx.cs" Inherits="eCPPrinting" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
<%
    if (Request.Form["export-type"].Equals("pdf")) {
        switch (Request.Form["export-order"]) {
            case "reporttablecalcapitalandinterest": 
                eCPDataReportTableCalCapitalAndInterest.ExportCPReportTableCalCapitalAndInterest(Request.Form["export-send"]);
                break;
            case "reportnoticecheckforreimbursement":
                eCPDataReportNoticeCheckForReimbursement.ExportCPReportNoticeCheckForReimbursement(Request.Form["export-send"]);
                break;
        }
    }

    if (Request.Form["export-type"].Equals("word")) {
        switch (Request.Form["export-order"]) {
            case "reportnoticerepaycomplete":
                eCPDataReportNoticeRepayComplete.ExportCPReportNoticeRepayComplete(Request.Form["export-send"]);
                break;
            case "reportnoticeclaimdebt":
                eCPDataReportNoticeClaimDebt.ExportCPReportNoticeClaimDebt(Request.Form["export-send"]);
                break;
            case "reportcertificatereimbursement":
                eCPDataReportCertificateReimbursement.ExportCPReportCertificateReimbursement(Request.Form["export-send"]);
                break;
        }
    }

    if (Request.Form["export-type"].Equals("excel")) {
        switch (Request.Form["export-order"]) {
            case "reportdebtorcontract":
                eCPDataReportDebtorContract.ExportCPReportDebtorContract(Request.Form["export-send"]);
                break;
            case "reportdebtorcontractpaid":
                eCPDataReportDebtorContract.ExportCPReportDebtorContractPaid(Request.Form["export-send"]);
                break;
            case "reportdebtorcontractremain":
                eCPDataReportDebtorContract.ExportCPReportDebtorContractRemain(Request.Form["export-send"]);
                break;
            case "reportdebtorcontractbreakrequirerepaypayment":
                eCPDataReportDebtorContract.ExportCPReportDebtorContractBreakRequireRepayPayment(Request.Form["export-send"]);
                break;
            case "reportformrequestcreateandupdatedebtor":
                eCPDataReportFormRequestCreateAndUpdateDebtor.ExportCPReportFormRequestCreateAndUpdateDebtor(Request.Form["export-send"]);
                break;
        }
    }
%>
</body>
</html>