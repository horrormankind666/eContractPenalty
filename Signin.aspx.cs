using System;

public partial class Signin : System.Web.UI.Page
{
    void Page_Load(object sender, EventArgs e)
    {
        bool _loginResult;

        _loginResult = eCPDB.ChkLogin();
        if (_loginResult)
            Response.Redirect("index.aspx");
    }
}