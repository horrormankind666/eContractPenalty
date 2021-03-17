using System;

public partial class eCPPrinting : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    /*
    eCPDB.ConnectStoreProcAddUpdate(eCPDB.InsertTransactionLog("EXPORT", "", "SelectReportExport, " + Request.Form["export-order"], Request.Form["export-send"]));

    if (Request.Form["export-type"].Equals("pdf"))
    {
      switch (Request.Form["export-order"])
      {
        case "reporttablecalcapitalandinterest"   : { eCPDataReportTableCalCapitalAndInterest.ExportCPReportTableCalCapitalAndInterest(Request.Form["export-send"]); break; }
        case "reportnoticecheckforreimbursement"  : { eCPDataReportNoticeCheckForReimbursement.ExportCPReportNoticeCheckForReimbursement(Request.Form["export-send"]); break; }
      }
    }

    if (Request.Form["export-type"].Equals("word"))
    {
      switch (Request.Form["export-order"])
      {
        case "reportnoticerepaycomplete"  : { eCPDataReportNoticeRepayComplete.ExportCPReportNoticeRepayComplete(Request.Form["export-send"]); break; }
        case "reportnoticeclaimdebt"      : { eCPDataReportNoticeClaimDebt.ExportCPReportNoticeClaimDebt(Request.Form["export-send"]); break; }
      }
    }
    */
  }
}