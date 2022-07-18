using System;
using System.Web.UI;

public partial class Signin: Page {
    void Page_Load(
        object sender,
        EventArgs e
    ) {
        bool loginResult = eCPDB.ChkLogin();

        if (loginResult)
            Response.Redirect("index.aspx");
    }
}