using System;
using System.Web.UI;

public partial class Signin: Page {
    void Page_Load(
        object sender,
        EventArgs e
    ) {
        bool _loginResult;

        _loginResult = eCPDB.ChkLogin();

        if (_loginResult)
            Response.Redirect("index.aspx");
    }
}