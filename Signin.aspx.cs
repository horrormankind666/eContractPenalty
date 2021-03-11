using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;

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