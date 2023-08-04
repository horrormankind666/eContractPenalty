/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๐๖/๐๘/๒๕๕๕>
Modify date : <๐๔/๐๘/๒๕๖๖>
Description : <สำหรับจัดการฐานข้อมูล>
=============================================
*/

using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FinService;
using System.Web.UI;

public class eCPDB {
    public const string CONNECTION_STRING = "Server=smartdev-write.mahidol;Database=Infinity;User ID=A;Password=ryoT6Noidc9d;Asynchronous Processing=true;";    
    /*
    public const string CONNECTION_STRING = "Server=10.41.117.18;Database=Infinity;User ID=yutthaphoom.taw;Password=F=8fu,u=yp;Asynchronous Processing=true;";
    public const string CONNECTION_STRING = "Server=stddb2023.mahidol;Database=Infinity;User ID=eContractPenalty;Password=!2023!#eC0ntr@ctPen@lty#;Asynchronous Processing=true;";
    */
    private const string STORE_PROC = "sp_ecpEContractPenalty";
    public static string[] userSection = new string[] {"กองกฎหมาย", "กองบริหารการศึกษา", "กองคลัง"};
    public static string username;
    public static string password;

    public static SqlConnection ConnectDB(string connString) {
        SqlConnection conn = new SqlConnection(connString);

        return conn;
    }

    public static DataSet ExecuteCommandStoredProcedure(params SqlParameter[] values) {
        SqlConnection conn = ConnectDB(CONNECTION_STRING);
        SqlCommand cmd = new SqlCommand(STORE_PROC, conn) {
            CommandType = CommandType.StoredProcedure,
            CommandTimeout = 1000
        };
        DataSet ds = new DataSet();

        if (values != null &&
            values.Length > 0)
            cmd.Parameters.AddRange(values);

        try {
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);
        }
        finally {
            cmd.Dispose();

            conn.Close();
            conn.Dispose();
        }

        return ds;
    }

    public static void ConnectStoreProcAddUpdate(string sqlCmd) {
        SqlConnection conn = ConnectDB(CONNECTION_STRING);
        SqlCommand cmd = new SqlCommand(STORE_PROC, conn) {
            CommandType = CommandType.StoredProcedure,
            CommandTimeout = 1000,
        };

        cmd.Parameters.AddWithValue("@ordertable", 52);
        cmd.Parameters.AddWithValue("@cmd", sqlCmd);

        try {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        finally {
            cmd.Dispose();

            conn.Close();
            conn.Dispose();
        }
    }
    
    public static string InsertTransactionLog(
        string what,
        string where,
        string function,
        string sqlCommand
    ) {
        string whoIs = string.Empty;
        string name = string.Empty;
        string userID = eCPUtil.GetUserID();
        string[,] dataUser = ListDetailCPTabUser(userID, "", "", "");
        
        if (dataUser.GetLength(0) > 0) {
            whoIs = dataUser[0, 1];
            name = dataUser[0, 3];
        }

        string command = (
            "INSERT INTO ecpTransLog " +
            "(WhoIs, Name, IP, WhatIs, WhereIs, FunctionIs, SQLCommand, WhenIs) " +
            "VALUES " +
            "(" +
            "'" + (!string.IsNullOrEmpty(whoIs) ? whoIs : userID) + "', " +
            (!string.IsNullOrEmpty(name) ? ("'" + name  + "'") : "null") + ", " +
            "'" + Util.GetIP() + "', " +
            (!string.IsNullOrEmpty(what) ? ("'" + what + "'") : "null") + ", " +
            (!string.IsNullOrEmpty(where) ? ("'" + where + "'") : "null") + ", " +
            (!string.IsNullOrEmpty(function) ? ("'" + function + "'") : "null") + ", " +
            (!string.IsNullOrEmpty(sqlCommand) ? ("'" + sqlCommand + "'") : "null") + ", " +
            "GETDATE()" +
            ")"
        );

        return command;
    }

    public static int ChkLogin() {
        HttpCookie finserviceCookie = HttpContext.Current.Request.Cookies[eCPUtil.USERTYPE_STAFF];
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        string username = string.Empty;
        int loginResult = 0;

        if (finserviceCookie != null) {
            Finservice finServiceAuthen = new Finservice();
            DataSet dsFinService = finServiceAuthen.info(finserviceCookie["result"]);

            if (dsFinService.Tables[0].Rows.Count > 0) {
                DataRow drFinService = dsFinService.Tables[0].Rows[0];
                username = drFinService["username"].ToString();
            }

            dsFinService.Dispose();

            bool signinResult;

            if (eCPCookie == null) {
                signinResult = Signin(username);
                loginResult = (signinResult.Equals(true) ? 0 : 2);
                eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
            }
            else {
                eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

                if ((string.IsNullOrEmpty(eCPCookie["Authen"])) ||
                    (string.IsNullOrEmpty(eCPCookie["UserSection"])) ||
                    (string.IsNullOrEmpty(eCPCookie["UserLevel"])) ||
                    (string.IsNullOrEmpty(eCPCookie["Pid"]))) {
                    Signout();
                    loginResult = 1;
                }
                else {
                    string userID = eCPUtil.GetUserID();
                    string[,] dataUser = ListDetailCPTabUser(userID, "", "", "");
                    int rowCount = dataUser.GetLength(0);

                    if (rowCount <= 0) {
                        Signout();
                        loginResult = 1;
                    }
                    else {
                        if (!username.Equals(dataUser[0, 1]) ||
                            !dataUser[0, 9].Equals(userID) ||
                            !dataUser[0, 4].Equals(eCPCookie["UserSection"]) ||
                            !dataUser[0, 5].Equals(eCPCookie["UserLevel"])) {
                            Signout();
                            loginResult = 1;
                        }
                        else
                            loginResult = 0;
                    }
                }

                eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
            }
        }

        if (finserviceCookie == null) {
            if (eCPCookie != null)
                Signout();

            loginResult = 1;
        }

        return loginResult;
    }

    public static bool Signin(string username) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 1),
            new SqlParameter("@username", username)
        );

        int rowCount = ds.Tables[0].Rows.Count;
        bool loginResult;

        if (rowCount <= 0) {
            loginResult = false;
        }
        else {
            DataRow dr = ds.Tables[0].Rows[0];

            HttpCookie eCPCookie = new HttpCookie("eCPCookie");
            eCPCookie.Values.Add("Authen", eCPUtil.EncodeToBase64(new string(eCPUtil.EncodeToBase64(dr["ID"].ToString()).Reverse().ToArray())));
            eCPCookie.Values.Add("UserSection", dr["UserSection"].ToString());
            eCPCookie.Values.Add("UserLevel", dr["UserLevel"].ToString());
            eCPCookie.Values.Add("Pid", "0");
                                
            HttpContext.Current.Response.Cookies.Add(eCPCookie);
                            
            loginResult = true;

            string sqlCommand = (
                "SELECT * " +
                "FROM   ecpTabUser " +
                "WHERE  (Username = ''" + username + "'')"
            );

            ConnectStoreProcAddUpdate(InsertTransactionLog("SIGN IN", "EContractParentAndStaff", "Signin", sqlCommand));
        }

        ds.Dispose();

        return loginResult;        
    }

    /*
    public static bool Signin(string authen) {           
        Dictionary<string, string> auth = eCPUtil.GetUsername(authen);
        string username = auth["Username"];
        string password = auth["Password"];

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 1),
            new SqlParameter("@username", username),
            new SqlParameter("@password", password)
        );

        int rowCount = ds.Tables[0].Rows.Count;
        bool loginResult;

        if (rowCount <= 0) {
            loginResult = false;
        }
        else {
            DataRow dr = ds.Tables[0].Rows[0];

            if (!dr["Username"].ToString().Equals(username) ||
                !dr["Password"].ToString().Equals(password)) {
                loginResult = false;
            }
            else {
                HttpCookie eCPCookie = new HttpCookie("eCPCookie");
                eCPCookie.Values.Add("Authen", eCPUtil.EncodeToBase64(new string(eCPUtil.EncodeToBase64(dr["ID"].ToString()).Reverse().ToArray())));
                eCPCookie.Values.Add("UserSection", dr["UserSection"].ToString());
                eCPCookie.Values.Add("UserLevel", dr["UserLevel"].ToString());
                eCPCookie.Values.Add("Pid", "0");
                                
                HttpContext.Current.Response.Cookies.Add(eCPCookie);
                            
                loginResult = true;

                string sqlCommand = (
                    "SELECT * " +
                    "FROM   ecpTabUser " +
                    "WHERE  (Username = ''" + username + "'') AND " +
                    "       (Password = ''" + password + "'')"
                );

                ConnectStoreProcAddUpdate(InsertTransactionLog("SIGN IN", "EContractParentAndStaff", "Signin", sqlCommand));
            }
        }

        ds.Dispose();

        return loginResult;
    }
    */

    public static void ClearUser() {
        username = string.Empty;
        password = string.Empty;
    }

    public static void Signout() {
        ConnectStoreProcAddUpdate(InsertTransactionLog("SIGN OUT", "", "Signout", ""));
        
        HttpContext.Current.Session.Abandon();

        HttpCookie eCPCookie = new HttpCookie("eCPCookie") {
            Expires = DateTime.Now.AddDays(-1D)
        };
        HttpContext.Current.Response.Cookies.Add(eCPCookie);
        
        ClearUser();
    }

    public static int CountCPTabUser(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", section),
            new SqlParameter("@userlevel", "User"),
            new SqlParameter("@name", (!string.IsNullOrEmpty(c.Request["name"]) ? c.Request["name"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountCPTabUser"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPTabUser(HttpContext c) {        
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@section", section),
            new SqlParameter("@userlevel", "User"),
            new SqlParameter("@name", (!string.IsNullOrEmpty(c.Request["name"]) ? c.Request["name"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 10];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["Username"].ToString();
            data[i, 2] = dr["Password"].ToString();
            data[i, 3] = dr["Name"].ToString();
            data[i, 4] = dr["UserSection"].ToString();
            data[i, 5] = dr["UserLevel"].ToString();
            data[i, 6] = dr["PhoneNumber"].ToString();
            data[i, 7] = dr["MobileNumber"].ToString();
            data[i, 8] = dr["Email"].ToString();
            data[i, 9] = dr["ID"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static DataSet ListCPTabUser() {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", section),
            new SqlParameter("@userlevel", "User")
        );

        ds.Dispose();

        return ds;
    }

    public static string[,] ListDetailCPTabUser(
        string userid,
        string username,
        string password,
        string userlevel
    ) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", section),
            new SqlParameter("@userid", userid),
            new SqlParameter("@username", (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) ? username : null)),
            new SqlParameter("@password", (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) ? password : null)),
            new SqlParameter("@userlevel", (!string.IsNullOrEmpty(userlevel) ? userlevel : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 10];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["Username"].ToString();
            data[i, 2] = dr["Password"].ToString();
            data[i, 3] = dr["Name"].ToString();
            data[i, 4] = dr["UserSection"].ToString();
            data[i, 5] = dr["UserLevel"].ToString();
            data[i, 6] = dr["PhoneNumber"].ToString();
            data[i, 7] = dr["MobileNumber"].ToString();
            data[i, 8] = dr["Email"].ToString();
            data[i, 9] = dr["ID"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static int CheckRepeatCPTabUser(
        HttpContext c,
        string column
    ) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 37),
            new SqlParameter("@username", (column.Equals("username") ? c.Request["username"] : null)),
            new SqlParameter("@usernameold", (column.Equals("username") && !string.IsNullOrEmpty(c.Request["usernameold"]) ? c.Request["usernameold"] : null)),
            new SqlParameter("@password", (column.Equals("password") ? c.Request["password"] : null)),
            new SqlParameter("@passwordold", (column.Equals("password") && !string.IsNullOrEmpty(c.Request["passwordold"]) ? c.Request["passwordold"] : null))
        );

        int recordCount = ds.Tables[0].Rows.Count;

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPTabProgram(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 23),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 9];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["FacultyCode"].ToString();
            data[i, 2] = dr["FactTName"].ToString();
            data[i, 3] = dr["ProgramCode"].ToString();
            data[i, 4] = dr["ProgTName"].ToString();
            data[i, 5] = dr["MajorCode"].ToString();
            data[i, 6] = dr["GroupNum"].ToString();
            data[i, 7] = dr["Dlevel"].ToString();
            data[i, 8] = dr["DlevelName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CheckRepeatCPTabProgram(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 24),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(c.Request["cp1id"]) ? c.Request["cp1id"] : null)),
            new SqlParameter("@dlevel", c.Request["dlevel"]),
            new SqlParameter("@faculty", c.Request["faculty"]),
            new SqlParameter("@program", c.Request["programcode"]),
            new SqlParameter("@major", c.Request["majorcode"]),
            new SqlParameter("@groupnum", c.Request["groupnum"])
        );
        
        int recordCount = ds.Tables[0].Rows.Count;

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPTabCalDate(string cpid) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 2),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cpid) ? cpid : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 3];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["CalDateCondition"].ToString();
            data[i, 2] = dr["PenaltyFormula"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPTabInterest(string cp1id) {  
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 3),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 4];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["InContractInterest"].ToString();
            data[i, 2] = dr["OutContractInterest"].ToString();
            data[i, 3] = dr["UseContractInterest"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListSearchUseContractInterest() {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 3),
            new SqlParameter("@usecontractinterest", "1")
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 2];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["InContractInterest"].ToString();
            data[i, 1] = dr["OutContractInterest"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPTabPayBreakContract(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 4),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 16];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["FacultyCode"].ToString();
            data[i, 2] = dr["FactTName"].ToString();
            data[i, 3] = dr["ProgramCode"].ToString();
            data[i, 4] = dr["ProgTName"].ToString();
            data[i, 5] = dr["MajorCode"].ToString();
            data[i, 6] = dr["GroupNum"].ToString();
            data[i, 7] = dr["AmountCash"].ToString();
            data[i, 8] = dr["Dlevel"].ToString();
            data[i, 9] = dr["DlevelName"].ToString();
            data[i, 10] = dr["CaseGraduate"].ToString();
            data[i, 11] = dr["CaseGraduateName"].ToString();
            data[i, 12] = dr["IDCalDate"].ToString();
            data[i, 13] = dr["CalDateCondition"].ToString();
            data[i, 14] = dr["PenaltyFormula"].ToString();
            data[i, 15] = dr["AmtIndemnitorYear"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CheckRepeatCPTabPayBreakContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 7),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(c.Request["cp1id"]) ? c.Request["cp1id"] : null)),
            new SqlParameter("@casegraduate", c.Request["casegraduate"]),
            new SqlParameter("@dlevel", c.Request["dlevel"]),
            new SqlParameter("@faculty", c.Request["faculty"]),
            new SqlParameter("@program", c.Request["programcode"]),
            new SqlParameter("@major", c.Request["majorcode"]),
            new SqlParameter("@groupnum", c.Request["groupnum"])
        );
        
        int recordCount = ds.Tables[0].Rows.Count;

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListSearchCPTabPayBreakContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 7),
            new SqlParameter("@casegraduate", c.Request["casegraduate"]),
            new SqlParameter("@dlevel", c.Request["dlevel"]),
            new SqlParameter("@faculty", c.Request["faculty"]),
            new SqlParameter("@program", c.Request["programcode"]),
            new SqlParameter("@major", c.Request["majorcode"]),
            new SqlParameter("@groupnum", c.Request["groupnum"])
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 4];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["CalDateCondition"].ToString();
            data[i, 2] = dr["AmtIndemnitorYear"].ToString();
            data[i, 3] = dr["AmountCash"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPTabScholarship(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 14),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 10];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["FacultyCode"].ToString();
            data[i, 2] = dr["FactTName"].ToString();
            data[i, 3] = dr["ProgramCode"].ToString();
            data[i, 4] = dr["ProgTName"].ToString();
            data[i, 5] = dr["MajorCode"].ToString();
            data[i, 6] = dr["GroupNum"].ToString();
            data[i, 7] = dr["ScholarshipMoney"].ToString();
            data[i, 8] = dr["Dlevel"].ToString();
            data[i, 9] = dr["DlevelName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CheckRepeatCPTabScholarship(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 15),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(c.Request["cp1id"]) ? c.Request["cp1id"] : null)),
            new SqlParameter("@dlevel", c.Request["dlevel"]),
            new SqlParameter("@faculty", c.Request["faculty"]),
            new SqlParameter("@program", c.Request["programcode"]),
            new SqlParameter("@major", c.Request["majorcode"]),
            new SqlParameter("@groupnum", c.Request["groupnum"])
        );

        int recordCount = ds.Tables[0].Rows.Count;

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListSearchCPTabScholarship(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 15),
            new SqlParameter("@dlevel", c.Request["dlevel"]),
            new SqlParameter("@faculty", c.Request["faculty"]),
            new SqlParameter("@program", c.Request["programcode"]),
            new SqlParameter("@major", c.Request["majorcode"]),
            new SqlParameter("@groupnum", c.Request["groupnum"])
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 2];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["ScholarshipMoney"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListTitlename() {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 27)
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 4];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["TitleCode"].ToString();
            data[i, 1] = dr["TitleEName"].ToString();
            data[i, 2] = dr["TitleTName"].ToString();
            data[i, 3] = dr["Sex"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListFaculty(bool cpTabProgram) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", (cpTabProgram.Equals(false) ? 5 : 25))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 2];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["FacultyCode"].ToString();
            data[i, 1] = dr["FactTName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListProgram(
        bool cpTabProgram,
        string dlevel,
        string faculty
    ) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", (cpTabProgram.Equals(false) ? 6 : 26)),
            new SqlParameter("@dlevel", (!string.IsNullOrEmpty(faculty) ? (!string.IsNullOrEmpty(dlevel) ? dlevel : null) : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(faculty) ? faculty : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 6];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ProgramCode"].ToString();
            data[i, 1] = dr["MajorCode"].ToString();
            data[i, 2] = dr["GroupNum"].ToString();
            data[i, 3] = dr["ProgTName"].ToString();
            data[i, 4] = dr["DLevel"].ToString();
            data[i, 5] = dr["DLevelName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListProvince() {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 11)
        );
        
        string[,] data = new string[ds.Tables[0].Rows.Count, 2];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ProvinceID"].ToString();
            data[i, 1] = dr["ProvinceTName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }
    
    public static int CountStudent(HttpContext c) {
        int recordCount = 0;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountStudent"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListStudent(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 15];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["StudentID"].ToString();
            data[i, 2] = dr["TitleCode"].ToString();
            data[i, 3] = dr["TitleEName"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["ThaiFName"].ToString();
            data[i, 6] = dr["ThaiLName"].ToString();
            data[i, 7] = dr["FacultyCode"].ToString();
            data[i, 8] = dr["FactTName"].ToString();
            data[i, 9] = dr["ProgramCode"].ToString();
            data[i, 10] = dr["ProgTName"].ToString();
            data[i, 11] = dr["MajorCode"].ToString();
            data[i, 12] = dr["GroupNum"].ToString();
            data[i, 13] = dr["DLevel"].ToString();
            data[i, 14] = dr["DLevelName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListProfileStudent(string studentid) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(studentid) ? studentid : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 26];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["StudentID"].ToString();
            data[i, 2] = dr["TitleCode"].ToString();
            data[i, 3] = dr["TitleEName"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstName"].ToString();
            data[i, 6] = dr["LastName"].ToString();
            data[i, 7] = dr["ThaiFName"].ToString();
            data[i, 8] = dr["ThaiLName"].ToString();
            data[i, 9] = dr["FacultyCode"].ToString();
            data[i, 10] = dr["FactTName"].ToString();
            data[i, 11] = dr["ProgramCode"].ToString();
            data[i, 12] = dr["ProgTName"].ToString();
            data[i, 13] = dr["MajorCode"].ToString();
            data[i, 14] = dr["GroupNum"].ToString();
            data[i, 15] = dr["DLevel"].ToString();
            data[i, 16] = dr["DLevelName"].ToString();
            data[i, 17] = dr["AdmissionDate"].ToString();
            data[i, 18] = dr["GraduateDate"].ToString();
            data[i, 19] = dr["ContractDate"].ToString();
            data[i, 20] = dr["ContractDateAgreement"].ToString();
            data[i, 21] = dr["GuarantorTitleTName"].ToString();
            data[i, 22] = dr["GuarantorFirstName"].ToString();
            data[i, 23] = dr["GuarantorLastName"].ToString();            
        }

        foreach (DataRow dr in ds.Tables[2].Rows) {
            data[i, 24] = dr["FileName"].ToString();
            data[i, 25] = dr["FolderName"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListSearchStudentCPTransBreakContract(string studentid) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 9),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(studentid) ? studentid : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 2];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["StudentID"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPTransBreakContract(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        if (section.Equals(1) &&
            eCPUtil.pageOrder[(section - 1), (pid - 1)].Equals("CPTransBreakContract"))
            section = 2;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", section),
            new SqlParameter("@statussend", (!string.IsNullOrEmpty(c.Request["statussend"]) ? c.Request["statussend"] : null)),
            new SqlParameter("@statusreceiver", (!string.IsNullOrEmpty(c.Request["statusreceiver"]) ? c.Request["statusreceiver"] : null)),
            new SqlParameter("@statusedit", (!string.IsNullOrEmpty(c.Request["statusedit"]) ? c.Request["statusedit"] : null)),
            new SqlParameter("@statuscancel", (!string.IsNullOrEmpty(c.Request["statuscancel"]) ? c.Request["statuscancel"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountCPTransBreakContract"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPTransBreakContract(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        if (section.Equals(1) &&
            eCPUtil.pageOrder[(section - 1), (pid - 1)].Equals("CPTransBreakContract"))
            section = 2;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@section", section),
            new SqlParameter("@statussend", (!string.IsNullOrEmpty(c.Request["statussend"]) ? c.Request["statussend"] : null)),
            new SqlParameter("@statusreceiver", (!string.IsNullOrEmpty(c.Request["statusreceiver"]) ? c.Request["statusreceiver"] : null)),
            new SqlParameter("@statusedit", (!string.IsNullOrEmpty(c.Request["statusedit"]) ? c.Request["statusedit"] : null)),
            new SqlParameter("@statuscancel", (!string.IsNullOrEmpty(c.Request["statuscancel"]) ? c.Request["statuscancel"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 16];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["StudentID"].ToString();
            data[i, 3] = dr["TitleTName"].ToString();
            data[i, 4] = dr["FirstTName"].ToString();
            data[i, 5] = dr["LastTName"].ToString();
            data[i, 6] = dr["ProgramCode"].ToString();
            data[i, 7] = dr["ProgramName"].ToString();
            data[i, 8] = dr["GroupNum"].ToString();
            data[i, 9] = dr["StatusSend"].ToString();
            data[i, 10] = dr["StatusReceiver"].ToString();
            data[i, 11] = dr["StatusEdit"].ToString();
            data[i, 12] = dr["StatusCancel"].ToString();
            data[i, 13] = Util.ConvertDateTH(dr["DateTimeCreate"].ToString());
            data[i, 14] = Util.ConvertDateTH(dr["DateTimeSend"].ToString());
            data[i, 15] = eCPUtil.actionTrackingStatus[(section - 1), (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string ChkTrackingStatusCPTransBreakContract(string cp1id) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        if (section.Equals(1) &&
            eCPUtil.pageOrder[(section - 1), (pid - 1)].Equals("CPTransBreakContract"))
            section = 2;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", section),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string trackingStatus = string.Empty;

        foreach (DataRow dr in ds.Tables[1].Rows)
            trackingStatus = (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString());

        ds.Dispose();

        return trackingStatus;
    }

    public static string[,] ListDetailCPTransBreakContract(string cp1id) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);
        int pid = int.Parse(eCPCookie["Pid"]);

        if (section.Equals(1) &&
            eCPUtil.pageOrder[(section - 1), (pid - 1)].Equals("CPTransBreakContract"))
            section = 2;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", section),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 52];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["StudentID"].ToString();
            data[i, 3] = dr["TitleCode"].ToString();
            data[i, 4] = dr["TitleEName"].ToString();
            data[i, 5] = dr["TitleTName"].ToString();
            data[i, 6] = dr["FirstEName"].ToString();
            data[i, 7] = dr["LastEName"].ToString();
            data[i, 8] = dr["FirstTName"].ToString();
            data[i, 9] = dr["LastTName"].ToString();
            data[i, 10] = dr["ProgramCode"].ToString();
            data[i, 11] = dr["ProgramName"].ToString();
            data[i, 12] = dr["MajorCode"].ToString();
            data[i, 13] = dr["FacultyCode"].ToString();
            data[i, 14] = dr["FacultyName"].ToString();
            data[i, 15] = dr["GroupNum"].ToString();
            data[i, 16] = dr["DLevel"].ToString();
            data[i, 17] = dr["DLevelName"].ToString();
            data[i, 18] = dr["PursuantBook"].ToString();
            data[i, 19] = dr["Pursuant"].ToString();
            data[i, 20] = dr["PursuantBookDate"].ToString();
            data[i, 21] = dr["Location"].ToString();
            data[i, 22] = dr["InputDate"].ToString();
            data[i, 23] = dr["StateLocation"].ToString();
            data[i, 24] = dr["StateLocationDate"].ToString();
            data[i, 25] = dr["ContractDate"].ToString();
            data[i, 26] = dr["Guarantor"].ToString();
            data[i, 27] = dr["ScholarFlag"].ToString();
            data[i, 28] = dr["ScholarshipMoney"].ToString();
            data[i, 29] = dr["ScholarshipYear"].ToString();
            data[i, 30] = dr["ScholarshipMonth"].ToString();
            data[i, 31] = dr["EducationDate"].ToString();
            data[i, 32] = dr["GraduateDate"].ToString();
            data[i, 33] = dr["CaseGraduate"].ToString();
            data[i, 34] = dr["CivilFlag"].ToString();
            data[i, 35] = dr["CalDateCondition"].ToString();
            data[i, 36] = dr["IndemnitorYear"].ToString();
            data[i, 37] = dr["IndemnitorCash"].ToString();

            data1 = ListLastCommentOnCPTransBreakContract(dr["ID"].ToString(), "E");
            data[i, 38] = (data1.GetLength(0) > 0 ? data1[0, 2] : string.Empty);
            data[i, 49] = (data1.GetLength(0) > 0 ? data1[0, 3] : string.Empty);

            data1 = ListLastCommentOnCPTransBreakContract(dr["ID"].ToString(), "C");
            data[i, 39] = (data1.GetLength(0) > 0 ? data1[0, 2] : string.Empty);
            data[i, 50] = (data1.GetLength(0) > 0 ? data1[0, 3] : string.Empty);

            data[i, 40] = dr["StatusSend"].ToString();
            data[i, 41] = dr["StatusReceiver"].ToString();
            data[i, 42] = dr["StatusEdit"].ToString();
            data[i, 43] = dr["StatusCancel"].ToString();            
            data[i, 44] = dr["FileName"].ToString();
            data[i, 45] = dr["FolderName"].ToString();
            data[i, 46] = dr["ContractDateAgreement"].ToString();
            data[i, 47] = dr["ContractForceStartDate"].ToString();
            data[i, 48] = dr["ContractForceEndDate"].ToString();

            data[i, 51] = dr["SetAmtIndemnitorYear"].ToString();
        }

        ds.Dispose();

        return data;
    }
    
    public static int CountRepay(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@statusrepay", (!string.IsNullOrEmpty(c.Request["statusrepay"]) ? c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!string.IsNullOrEmpty(c.Request["statusreply"]) ? c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!string.IsNullOrEmpty(c.Request["replyresult"]) ? c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(c.Request["statuspayment"]) ? c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountRepay"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string SearchRepayStatusDetail(
        string cp2id,
        string statusRepay,
        string statusPayment
    ) { 
        string repayStatusDetailOrder = string.Empty;
        string[] iconRepayStatus = new string[4];

        if (!string.IsNullOrEmpty(cp2id) &&
            !string.IsNullOrEmpty(statusRepay) &&
            !string.IsNullOrEmpty(statusPayment)) {
            if (statusRepay.Equals("0")) {
                repayStatusDetailOrder = "0";
                iconRepayStatus[0] = eCPUtil.iconRepayStatus[0, 1];
                iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 0];
                iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 0];
                iconRepayStatus[3] = eCPUtil.iconRepayStatus[3, 0];
            }

            if (statusPayment.Equals("2")) {
                repayStatusDetailOrder = "7";
                iconRepayStatus[0] = eCPUtil.iconRepayStatus[0, 0];
                iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 0];
                iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 0];
                iconRepayStatus[3] = eCPUtil.iconRepayStatus[3, 1];
            }

            if (statusPayment.Equals("3")) {
                repayStatusDetailOrder = "8";
                iconRepayStatus[0] = eCPUtil.iconRepayStatus[0, 0];
                iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 0];
                iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 0];
                iconRepayStatus[3] = eCPUtil.iconRepayStatus[3, 2];
            }                

            if ((statusRepay.Equals("1") || statusRepay.Equals("2")) &&
                statusPayment.Equals("1")) {
                iconRepayStatus[0] = eCPUtil.iconRepayStatus[0, 0];
                iconRepayStatus[3] = eCPUtil.iconRepayStatus[3, 0];

                DataSet ds = ExecuteCommandStoredProcedure(
                    new SqlParameter("@ordertable", 13),
                    new SqlParameter("@cp2id", cp2id)
                );

                foreach (DataRow dr in ds.Tables[0].Rows) {
                    if (dr["StatusRepay"].ToString().Equals("1")) {
                        if (dr["StatusReply"].ToString().Equals("1")) {
                            repayStatusDetailOrder = "1";
                            iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 1];
                        }

                        if (dr["StatusReply"].ToString().Equals("2") &&
                            dr["ReplyResult"].ToString().Equals("1")) {
                            repayStatusDetailOrder = "2";
                            iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 2];
                        }

                        if (dr["StatusReply"].ToString().Equals("2") &&
                            dr["ReplyResult"].ToString().Equals("2")) {
                            repayStatusDetailOrder = "3";
                            iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 3];
                        }
                    }

                    iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 0];

                    if (dr["StatusRepay"].ToString().Equals("2")) {
                        if (dr["StatusReply"].ToString().Equals("1")) {
                            repayStatusDetailOrder = "4";
                            iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 1];
                        }

                        if (dr["StatusReply"].ToString().Equals("2") &&
                            dr["ReplyResult"].ToString().Equals("1")) {
                            repayStatusDetailOrder = "5";
                            iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 2];
                        }

                        if (dr["StatusReply"].ToString().Equals("2") &&
                            dr["ReplyResult"].ToString().Equals("2")) {
                            repayStatusDetailOrder = "6";
                            iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 3];
                        }
                    }
                }

                ds.Dispose();
            }
        }
        else {
            repayStatusDetailOrder = "0";
            iconRepayStatus[0] = eCPUtil.iconRepayStatus[0, 0];
            iconRepayStatus[1] = eCPUtil.iconRepayStatus[1, 0];
            iconRepayStatus[2] = eCPUtil.iconRepayStatus[2, 0];
            iconRepayStatus[3] = eCPUtil.iconRepayStatus[3, 0];
        }     

        string repayStatusDetail = (repayStatusDetailOrder + ";" + iconRepayStatus[0] + ";" + iconRepayStatus[1] + ";" + iconRepayStatus[2] + ";" + iconRepayStatus[3]);

        return repayStatusDetail;
    }

    public static string[,] ListRepay(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@statusrepay", (!string.IsNullOrEmpty(c.Request["statusrepay"]) ? c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!string.IsNullOrEmpty(c.Request["statusreply"]) ? c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!string.IsNullOrEmpty(c.Request["replyresult"]) ? c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(c.Request["statuspayment"]) ? c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 19];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = dr["StatusRepay"].ToString();
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = SearchRepayStatusDetail(dr["ID"].ToString(), dr["StatusRepay"].ToString(), dr["StatusPayment"].ToString());
            data[i, 17] = Util.ConvertDateTH(dr["DateTimeReceiver"].ToString());
            data[i, 18] = eCPUtil.actionTrackingStatus[1, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListRepay1(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 53),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@statusrepay", (!string.IsNullOrEmpty(c.Request["statusrepay"]) ? c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!string.IsNullOrEmpty(c.Request["statusreply"]) ? c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!string.IsNullOrEmpty(c.Request["replyresult"]) ? c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(c.Request["statuspayment"]) ? c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 22];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = dr["StatusRepay"].ToString();
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = SearchRepayStatusDetail(dr["ID"].ToString(), dr["StatusRepay"].ToString(), dr["StatusPayment"].ToString());
            data[i, 17] = Util.ConvertDateTH(dr["DateTimeReceiver"].ToString());
            data[i, 18] = eCPUtil.actionTrackingStatus[1, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];
            data[i, 19] = dr["StatusReply"].ToString();
            data[i, 20] = dr["ReplyResult"].ToString();
            data[i, 21] = dr["ReplyDate"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailCPTransRequireContract(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 78];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["IndemnitorAddress"].ToString();
            data[i, 4] = dr["ProvinceID"].ToString();
            data[i, 5] = dr["ProvinceTName"].ToString();
            data[i, 6] = dr["RequireDate"].ToString();
            data[i, 7] = dr["ApproveDate"].ToString();
            data[i, 8] = dr["ActualMonthScholarship"].ToString();
            data[i, 9] = dr["ActualScholarship"].ToString();
            data[i, 10] = dr["TotalPayScholarship"].ToString();
            data[i, 11] = dr["ActualMonth"].ToString();
            data[i, 12] = dr["ActualDay"].ToString();
            data[i, 13] = dr["AllActualDate"].ToString();
            data[i, 14] = dr["ActualDate"].ToString();
            data[i, 15] = dr["RemainDate"].ToString();
            data[i, 16] = dr["SubtotalPenalty"].ToString();
            data[i, 17] = dr["TotalPenalty"].ToString();
            data[i, 18] = dr["StatusRepay"].ToString();
            data[i, 19] = dr["StudentID"].ToString();
            data[i, 20] = dr["TitleTName"].ToString();
            data[i, 21] = dr["FirstTName"].ToString();
            data[i, 22] = dr["LastTName"].ToString();
            data[i, 23] = dr["ProgramCode"].ToString();
            data[i, 24] = dr["ProgramName"].ToString();
            data[i, 25] = dr["MajorCode"].ToString();
            data[i, 26] = dr["FacultyCode"].ToString();
            data[i, 27] = dr["FacultyName"].ToString();
            data[i, 28] = dr["GroupNum"].ToString();
            data[i, 29] = dr["DLevel"].ToString();
            data[i, 30] = dr["DLevelName"].ToString();
            data[i, 31] = dr["PursuantBook"].ToString();
            data[i, 32] = dr["Pursuant"].ToString();
            data[i, 33] = dr["PursuantBookDate"].ToString();
            data[i, 34] = dr["Location"].ToString();
            data[i, 35] = dr["InputDate"].ToString();
            data[i, 36] = dr["StateLocation"].ToString();
            data[i, 37] = dr["StateLocationDate"].ToString();
            data[i, 38] = dr["ContractDate"].ToString();
            data[i, 39] = dr["Guarantor"].ToString();
            data[i, 40] = dr["ScholarFlag"].ToString();
            data[i, 41] = dr["ScholarshipMoney"].ToString();
            data[i, 42] = dr["ScholarshipYear"].ToString();
            data[i, 43] = dr["ScholarshipMonth"].ToString();
            data[i, 44] = dr["EducationDate"].ToString();
            data[i, 45] = dr["GraduateDate"].ToString();
            data[i, 46] = dr["CaseGraduate"].ToString();
            data[i, 47] = dr["CivilFlag"].ToString();
            data[i, 48] = dr["CalDateCondition"].ToString();
            data[i, 49] = dr["IndemnitorYear"].ToString();
            data[i, 50] = dr["IndemnitorCash"].ToString();
            data[i, 51] = string.Empty;
            data[i, 52] = dr["StatusSend"].ToString();
            data[i, 53] = dr["StatusReceiver"].ToString();
            data[i, 54] = dr["StatusEdit"].ToString();
            data[i, 55] = dr["StatusCancel"].ToString();
            data[i, 56] = dr["FileName"].ToString();
            data[i, 57] = dr["FolderName"].ToString();
            data[i, 58] = dr["StatusPayment"].ToString();

            data1 = ListLastCommentOnCPTransBreakContract(dr["BCID"].ToString(), "E");
            data[i, 59] = (data1.GetLength(0) > 0 ? data1[0, 2] : string.Empty);
            data[i, 64] = (data1.GetLength(0) > 0 ? data1[0, 3] : string.Empty);

            data1 = ListLastCommentOnCPTransBreakContract(dr["BCID"].ToString(), "C");
            data[i, 60] = (data1.GetLength(0) > 0 ? data1[0, 2] : string.Empty);
            data[i, 65] = (data1.GetLength(0) > 0 ? data1[0, 3] : string.Empty);

            data[i, 61] = dr["ContractDateAgreement"].ToString();
            data[i, 62] = dr["ContractForceStartDate"].ToString();
            data[i, 63] = dr["ContractForceEndDate"].ToString();
            data[i, 66] = dr["StudyLeave"].ToString();
            data[i, 67] = dr["BeforeStudyLeaveStartDate"].ToString();
            data[i, 68] = dr["BeforeStudyLeaveEndDate"].ToString();
            data[i, 69] = dr["StudyLeaveStartDate"].ToString();
            data[i, 70] = dr["StudyLeaveEndDate"].ToString();
            data[i, 71] = dr["AfterStudyLeaveStartDate"].ToString();
            data[i, 72] = dr["AfterStudyLeaveEndDate"].ToString();
            data[i, 73] = dr["LawyerFullname"].ToString();
            data[i, 74] = dr["LawyerPhoneNumber"].ToString();
            data[i, 75] = dr["LawyerMobileNumber"].ToString();
            data[i, 76] = dr["LawyerEmail"].ToString();
            data[i, 77] = dr["SetAmtIndemnitorYear"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string ChkRepayStatusCPTransRequireContract(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string repayStatus = string.Empty;

        foreach (DataRow dr in ds.Tables[1].Rows)
            repayStatus = dr["StatusRepay"].ToString();

        ds.Dispose();

        return repayStatus;
    }

    public static string[,] ListCPTransRepayContract(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 16),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 14];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["TotalPenalty"].ToString();
            data[i, 2] = dr["StatusRepay"].ToString();
            data[i, 3] = dr["StatusReply"].ToString();
            data[i, 4] = dr["ReplyResult"].ToString();
            data[i, 5] = dr["RepayDate"].ToString();
            data[i, 6] = dr["ReplyDate"].ToString();
            data[i, 7] = dr["StatusPayment"].ToString();
            data[i, 8] = string.Empty;
            data[i, 9] = string.Empty;
            data[i, 10] = string.Empty;
            data[i, 11] = dr["SubtotalPenalty"].ToString();
            
            data1 = ListMaxReplyDate(dr["ID"].ToString());

            if (data1.GetLength(0) > 0) {
                if (data1[0, 3].Equals("2") &&
                    data1[0, 4].Equals("1"))
                    data[i, 8] = data1[0, 5];

                data[i, 9] = data1[0, 3];
                data[i, 10] = data1[0, 4];
            }
            
            data[i, 12] = dr["Pursuant"].ToString();
            data[i, 13] = dr["PursuantBookDate"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListMaxReplyDate(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 17),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 6];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RCID"].ToString();
            data[i, 1] = dr["StatusRepay"].ToString();
            data[i, 2] = dr["RepayDate"].ToString();
            data[i, 3] = dr["StatusReply"].ToString();
            data[i, 4] = dr["ReplyResult"].ToString();
            data[i, 5] = dr["ReplyDate"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string ChkRepayStatusCalInterestOverpayment(string cp2id) {
        string result;               
        string[,] data = ListCPTransRepayContract(cp2id);

        if (data.GetLength(0) > 0) {
            /*
            Status Payment
            */
            if (!data[0, 7].Equals("2")) {
                /*
                Status Payment
                */
                if (!data[0, 7].Equals("3")) {
                    if (data[0, 9].Equals("2")) {
                        if (data[0, 10].Equals("1")) {
                            /*
                            Reply Date
                            */
                            if (!string.IsNullOrEmpty(data[0, 8])) {
                                string[] repayDate = eCPUtil.RepayDate(data[0, 8]);
                                IFormatProvider provider = new System.Globalization.CultureInfo("th-TH");
                                DateTime dateA = DateTime.Parse(repayDate[2], provider);
                                DateTime dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), provider);
                                double[] overpayment = Util.CalcDate(dateA, dateB);

                                if (!overpayment[0].Equals(0)) {
                                    result = "0";
                                }
                                else
                                    result = "7";
                            }
                            else
                                result = "6";
                        }
                        else
                            result = "5";
                    }
                    else
                        result = "4";
                }
                else
                    result = "3";
            }
            else
                result = "2";
        }
        else
            result = "1";

        return result;
    }

    public static string[,] ListCPTransRepayContractNoCurrentStatusRepay(
        string cp2id,
        string statusRepay
    ) {         
        string[,] data = new string[0, 0];

        if (!string.IsNullOrEmpty(cp2id) &&
            !string.IsNullOrEmpty(statusRepay)) {
            DataSet ds = ExecuteCommandStoredProcedure(
                new SqlParameter("@ordertable", 18),
                new SqlParameter("@cp2id", cp2id),
                new SqlParameter("@statusrepay", statusRepay)
            );

            data = new string[ds.Tables[0].Rows.Count, 8];
            int i = 0;

            foreach (DataRow dr in ds.Tables[0].Rows) {
                data[i, 0] = dr["ID"].ToString();
                data[i, 1] = dr["StatusRepay"].ToString();
                data[i, 2] = dr["StatusReply"].ToString();
                data[i, 3] = dr["ReplyResult"].ToString();
                data[i, 4] = dr["RepayDate"].ToString();
                data[i, 5] = dr["ReplyDate"].ToString();
                data[i, 6] = dr["Pursuant"].ToString();
                data[i, 7] = dr["PursuantBookDate"].ToString();

                i++;
            }

            ds.Dispose();
        }

        return data;
    }

    public static int CountPaymentOnCPTransRequireContract(HttpContext c) {
        int recordCount = 0;

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(c.Request["statuspayment"]) ? c.Request["statuspayment"] : null)),
            new SqlParameter("@statuspaymentrecord", (!string.IsNullOrEmpty(c.Request["statuspaymentrecord"]) ? c.Request["statuspaymentrecord"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountPayment"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListPaymentOnCPTransRequireContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(c.Request["statuspayment"]) ? c.Request["statuspayment"] : null)),
            new SqlParameter("@statuspaymentrecord", (!string.IsNullOrEmpty(c.Request["statuspaymentrecord"]) ? c.Request["statuspaymentrecord"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 28];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = dr["StatusRepay"].ToString();
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = dr["FormatPayment"].ToString();
            data[i, 17] = dr["ReplyDate"].ToString();
            data[i, 18] = dr["TotalPayScholarship"].ToString();
            data[i, 19] = dr["SubtotalPenalty"].ToString();
            data[i, 20] = dr["TotalPenalty"].ToString();
            data[i, 21] = eCPUtil.actionTrackingStatus[1, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];
            data[i, 22] = dr["ReplyDateHistory"].ToString();
            data[i, 23] = (!string.IsNullOrEmpty(dr["TotalPayCapital"].ToString()) ? dr["TotalPayCapital"].ToString() : "0");
            data[i, 24] = (!string.IsNullOrEmpty(dr["TotalPayInterest"].ToString()) ? dr["TotalPayInterest"].ToString() : "0");
            data[i, 25] = (!string.IsNullOrEmpty(dr["TotalPay"].ToString()) ? dr["TotalPay"].ToString() : "0");
            data[i, 26] = (!string.IsNullOrEmpty(dr["TotalRemain"].ToString()) ? dr["TotalRemain"].ToString() : "0");
            data[i, 27] = (!string.IsNullOrEmpty(dr["RemainAccruedInterest"].ToString()) ? dr["RemainAccruedInterest"].ToString() : "0");

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailPaymentOnCPTransRequireContract(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 38];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["TotalPayScholarship"].ToString();
            data[i, 4] = dr["SubtotalPenalty"].ToString();
            data[i, 5] = dr["TotalPenalty"].ToString();
            data[i, 6] = dr["StatusRepay"].ToString();
            data[i, 7] = dr["StatusPayment"].ToString();
            data[i, 8] = dr["FormatPayment"].ToString();
            data[i, 9] = dr["StudentID"].ToString();
            data[i, 10] = dr["TitleTName"].ToString();
            data[i, 11] = dr["FirstTName"].ToString();
            data[i, 12] = dr["LastTName"].ToString();
            data[i, 13] = dr["ProgramCode"].ToString();
            data[i, 14] = dr["ProgramName"].ToString();
            data[i, 15] = dr["MajorCode"].ToString();
            data[i, 16] = dr["FacultyCode"].ToString();
            data[i, 17] = dr["FacultyName"].ToString();
            data[i, 18] = dr["GroupNum"].ToString();
            data[i, 19] = dr["DLevel"].ToString();
            data[i, 20] = dr["DLevelName"].ToString();
            data[i, 21] = dr["FileName"].ToString();
            data[i, 22] = dr["FolderName"].ToString();
            data[i, 23] = dr["StatusReply"].ToString();
            data[i, 24] = dr["ReplyDate"].ToString();
            data[i, 25] = string.Empty;
            data[i, 26] = string.Empty;
            data[i, 27] = string.Empty;

            data1 = ListLastTransPayment(dr["ID"].ToString());

            if (data1.GetLength(0) > 0) {
                data[i, 25] = data1[0, 2];
                data[i, 26] = data1[0, 3];
                data[i, 27] = data1[0, 4];
            }

            data[i, 28] = dr["ContractDate"].ToString();
            data[i, 29] = dr["LawyerFullname"].ToString();
            data[i, 30] = dr["LawyerPhoneNumber"].ToString();
            data[i, 31] = dr["LawyerMobileNumber"].ToString();
            data[i, 32] = dr["LawyerEmail"].ToString();
            data[i, 33] = dr["StatusPaymentRecord"].ToString();
            data[i, 34] = dr["StatusPaymentRecordLawyerFullname"].ToString();
            data[i, 35] = dr["StatusPaymentRecordLawyerPhoneNumber"].ToString();
            data[i, 36] = dr["StatusPaymentRecordLawyerMobileNumber"].ToString();
            data[i, 37] = dr["StatusPaymentRecordLawyerEmail"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListTransPayment(
        string cp1id,
        string dateStart,
        string dateEnd
    ) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 21),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(dateStart) ? dateStart : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(dateEnd) ? dateEnd : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 15];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["DateTimePayment"].ToString();
            data[i, 4] = dr["Capital"].ToString();
            data[i, 5] = dr["Interest"].ToString();
            data[i, 6] = dr["AccruedInterest"].ToString();
            data[i, 7] = dr["TotalPayment"].ToString();
            data[i, 8] = dr["PayCapital"].ToString();
            data[i, 9] = dr["PayInterest"].ToString();
            data[i, 10] = dr["TotalPay"].ToString();
            data[i, 11] = dr["RemainCapital"].ToString();
            data[i, 12] = dr["RemainAccruedInterest"].ToString();
            data[i, 13] = dr["TotalRemain"].ToString();
            data[i, 14] = dr["Channel"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailTransPayment(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 21),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 52];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["RCID"].ToString();
            data[i, 2] = dr["CalInterestYesNo"].ToString();
            data[i, 3] = dr["OverpaymentDateStart"].ToString();
            data[i, 4] = dr["OverpaymentDateEnd"].ToString();
            data[i, 5] = dr["OverpaymentYear"].ToString();
            data[i, 6] = dr["OverpaymentDay"].ToString();
            data[i, 7] = dr["OverpaymentInterest"].ToString();
            data[i, 8] = dr["OverpaymentTotalInterest"].ToString();
            data[i, 9] = dr["PayRepayDateStart"].ToString();
            data[i, 10] = dr["PayRepayDateEnd"].ToString();
            data[i, 11] = dr["PayRepayYear"].ToString();
            data[i, 12] = dr["PayRepayDay"].ToString();
            data[i, 13] = dr["PayRepayInterest"].ToString();
            data[i, 14] = dr["PayRepayTotalInterest"].ToString();
            data[i, 15] = dr["DateTimePayment"].ToString();
            data[i, 16] = dr["Capital"].ToString();
            data[i, 17] = dr["Interest"].ToString();
            data[i, 18] = dr["TotalAccruedInterest"].ToString();            
            data[i, 19] = dr["TotalPayment"].ToString();
            data[i, 20] = dr["PayCapital"].ToString();
            data[i, 21] = dr["PayInterest"].ToString();
            data[i, 22] = dr["TotalPay"].ToString();
            data[i, 23] = dr["RemainCapital"].ToString();
            data[i, 24] = dr["AccruedInterest"].ToString();            
            data[i, 25] = dr["RemainAccruedInterest"].ToString();
            data[i, 26] = dr["TotalRemain"].ToString();
            data[i, 27] = dr["Channel"].ToString();
            data[i, 28] = dr["ReceiptNo"].ToString();
            data[i, 29] = dr["ReceiptBookNo"].ToString();
            data[i, 30] = dr["ReceiptDate"].ToString();
            data[i, 31] = dr["ReceiptSendNo"].ToString();
            data[i, 32] = dr["ReceiptFund"].ToString();
            data[i, 33] = dr["ChequeNo"].ToString();
            data[i, 34] = dr["ChequeBank"].ToString();
            data[i, 35] = dr["ChequeBankBranch"].ToString();
            data[i, 36] = dr["ChequeDate"].ToString();
            data[i, 37] = dr["CashBank"].ToString();
            data[i, 38] = dr["CashBankBranch"].ToString();
            data[i, 39] = dr["CashBankAccount"].ToString();
            data[i, 40] = dr["CashBankAccountNo"].ToString();
            data[i, 41] = dr["CashBankDate"].ToString();
            data[i, 42] = dr["ReceiptCopy"].ToString();
            data[i, 43] = dr["FormatPayment"].ToString();
            data[i, 44] = dr["SubtotalPenalty"].ToString();
            data[i, 45] = dr["TotalPenalty"].ToString();
            data[i, 46] = dr["OverpaymentTotalInterestBefore"].ToString();
            data[i, 47] = dr["Overpay"].ToString();
            data[i, 48] = dr["LawyerFullname"].ToString();
            data[i, 49] = dr["LawyerPhoneNumber"].ToString();
            data[i, 50] = dr["LawyerMobileNumber"].ToString();
            data[i, 51] = dr["LawyerEmail"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListLastTransPayment(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 22),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 5];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["RCID"].ToString();
            data[i, 2] = dr["DateTimePayment"].ToString();
            data[i, 3] = dr["RemainAccruedInterest"].ToString();
            data[i, 4] = dr["TotalRemain"].ToString();
        }

        ds.Dispose();

        return data;
    }
    
    public static string ListCPTransProsecution(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 54),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = ListLastTransPayment(cp2id);
        string dateTimePayment = string.Empty;
        string remainAccruedInterest = string.Empty;
        string totalRemain = string.Empty;

        if (data.GetLength(0) > 0) {
            dateTimePayment = data[0, 2];
            remainAccruedInterest = data[0, 3];
            totalRemain = data[0, 4];
        }

        JArray jsonArray = new JArray();

        foreach (DataRow dr in ds.Tables[0].Rows) {
            jsonArray.Add(new JObject() {
                {
                    "eCPTransRequireContract", new JObject() {
                        { "ID", dr["ID"].ToString() },
                        { "BCID", dr["BCID"].ToString() },
                        { "subtotalPenalty", dr["SubtotalPenalty"].ToString() },
                        { "totalPenalty", dr["TotalPenalty"].ToString() },
                        { "statusPayment", dr["StatusPayment"].ToString() },
                        {
                            "lawyer", new JObject() {
                                { "fullName", dr["LawyerFullname"].ToString() },
                                { "phoneNumber", dr["LawyerPhoneNumber"].ToString() },
                                { "mobileNumber", dr["LawyerMobileNumber"].ToString() },
                                { "email", dr["LawyerEmail"].ToString() }
                            }
                        }
                    }
                },
                {
                    "eCPTransProsecution", new JObject() {
                        { "RCID", dr["RCID"].ToString() },
                        {
                            "complaint", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", dr["ComplaintLawyerFullname"].ToString() },
                                        { "phoneNumber", dr["ComplaintLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", dr["ComplaintLawyerMobileNumber"].ToString() },
                                        { "email", dr["ComplaintLawyerEmail"].ToString() }
                                    }
                                },
                                { "blackCaseNo", dr["ComplaintBlackCaseNo"].ToString() },
                                { "capital", dr["ComplaintCapital"].ToString() },
                                { "interest", dr["ComplaintInterest"].ToString() },
                                { "actionDate", eCPUtil.ThaiLongDate(dr["ComplaintActionDate"].ToString()) }
                            }
                        },
                        {
                            "judgment", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", dr["JudgmentLawyerFullname"].ToString() },
                                        { "phoneNumber", dr["JudgmentLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", dr["JudgmentLawyerMobileNumber"].ToString() },
                                        { "email", dr["JudgmentLawyerEmail"].ToString() }
                                    }
                                },
                                { "redCaseNo", dr["JudgmentRedCaseNo"].ToString() },
                                { "verdict", dr["JudgmentVerdict"].ToString() },
                                { "copy", (!string.IsNullOrEmpty(dr["JudgmentCopy"].ToString()) ? eCPUtil.DecodeFromBase64(dr["JudgmentCopy"].ToString()) : dr["JudgmentCopy"].ToString()) },
                                { "remark", dr["JudgmentRemark"].ToString() },
                                { "actionDate", eCPUtil.ThaiLongDate(dr["JudgmentActionDate"].ToString()) }
                            }
                        },
                        {
                            "execution", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", dr["ExecutionLawyerFullname"].ToString() },
                                        { "phoneNumber", dr["ExecutionLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", dr["ExecutionLawyerMobileNumber"].ToString() },
                                        { "email", dr["ExecutionLawyerEmail"].ToString() }
                                    }
                                },
                                { "date", dr["ExecutionDate"].ToString() },
                                { "copy", (!string.IsNullOrEmpty(dr["ExecutionCopy"].ToString()) ? eCPUtil.DecodeFromBase64(dr["ExecutionCopy"].ToString()) : dr["ExecutionCopy"].ToString()) },
                                { "actionDate", eCPUtil.ThaiLongDate(dr["ExecutionActionDate"].ToString()) }
                            }
                        },
                        {
                            "executionWithdraw", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", dr["ExecutionWithdrawLawyerFullname"].ToString() },
                                        { "phoneNumber", dr["ExecutionWithdrawLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", dr["ExecutionWithdrawLawyerMobileNumber"].ToString() },
                                        { "email", dr["ExecutionWithdrawLawyerEmail"].ToString() }
                                    }
                                },
                                { "date", dr["ExecutionWithdrawDate"].ToString() },
                                { "reason", dr["ExecutionWithdrawReason"].ToString() },
                                { "copy", (!string.IsNullOrEmpty(dr["ExecutionWithdrawCopy"].ToString()) ? eCPUtil.DecodeFromBase64(dr["ExecutionWithdrawCopy"].ToString()) : dr["ExecutionWithdrawCopy"].ToString()) },
                                { "actionDate", eCPUtil.ThaiLongDate(dr["ExecutionWithdrawActionDate"].ToString()) }
                            }
                        }
                    }
                },
                {
                    "eCPTransPayment", new JObject() {
                        {
                            "lastPayment", new JObject() {
                                { "dateTimePayment", dateTimePayment },
                                { "remainAccruedInterest", remainAccruedInterest },
                                { "totalRemain", totalRemain }
                            }
                        }
                    }
                }
            });
        }

        ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static string GetPersonRecordsAddress(string studentCode) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 55),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(studentCode) ? studentCode : null))
        );

        JArray jsonArray = new JArray();

        foreach (DataRow dr in ds.Tables[0].Rows) {
            jsonArray.Add(new JObject() {
                { "perPersonID", dr["id"].ToString() },
                { "idCard", dr["idCard"].ToString() },
                {
                    "addressTypePermanent", new JObject() {
                        { "address", dr["addressPermanent"].ToString() },
                        { "country", dr["thCountryNamePermanent"].ToString() },
                        { "village", dr["villagePermanent"].ToString() },
                        { "no", dr["noPermanent"].ToString() },
                        { "moo", dr["mooPermanent"].ToString() },
                        { "soi", dr["soiPermanent"].ToString() },
                        { "road", dr["roadPermanent"].ToString() },
                        { "subdistrict", dr["thSubdistrictNamePermanent"].ToString() },
                        { "district", dr["thDistrictNamePermanent"].ToString() },
                        { "province", dr["thPlaceNamePermanent"].ToString() },
                        { "zipCode", dr["zipCodePermanent"].ToString() },
                        { "phoneNumber", dr["phoneNumberPermanent"].ToString() },
                        { "mobileNumber", dr["mobileNumberPermanent"].ToString() }
                    }
                },
                {
                    "addressTypeCurrent", new JObject() {
                        { "address", dr["addressCurrent"].ToString() },
                        { "country", dr["thCountryNameCurrent"].ToString() },
                        { "village", dr["villageCurrent"].ToString() },
                        { "no", dr["noCurrent"].ToString() },
                        { "moo", dr["mooCurrent"].ToString() },
                        { "soi", dr["soiCurrent"].ToString() },
                        { "road", dr["roadCurrent"].ToString() },
                        { "subdistrict", dr["thSubdistrictNameCurrent"].ToString() },
                        { "district", dr["thDistrictNameCurrent"].ToString() },
                        { "province", dr["thPlaceNameCurrent"].ToString() },
                        { "zipCode", dr["zipCodeCurrent"].ToString() },
                        { "phoneNumber", dr["phoneNumberCurrent"].ToString() },
                        { "mobileNumber", dr["mobileNumberCurrent"].ToString() }
                    }
                }
            });
        }

        ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static string[,] ListLastCommentOnCPTransBreakContract(
        string cpid,
        string action
    ) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 20),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cpid) ? cpid : null)),
            new SqlParameter("@actioncomment", (!string.IsNullOrEmpty(action) ? action : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 4];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["ID"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["Comment"].ToString();
            data[i, 3] = dr["DateTimeReject"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportTableCalCapitalAndInterest(HttpContext c) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportTableCalCapitalAndInterest"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPReportTableCalCapitalAndInterest(HttpContext c) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 20];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = dr["StatusRepay"].ToString();
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = dr["FormatPayment"].ToString();
            data[i, 17] = dr["TotalPenalty"].ToString();
            data[i, 18] = string.Empty;
            data[i, 19] = string.Empty;

            data1 = ListSumPayOnPayment(dr["ID"].ToString());

            if (data1.GetLength(0) > 0)
                data[i, 18] = data1[0, 1];

            data1 = ListLastTransPayment(dr["ID"].ToString());

            if (data1.GetLength(0) > 0)
                data[i, 19] = data1[0, 4];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListSumPayOnPayment(string cp2id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 29),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 4];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RCID"].ToString();
            data[i, 1] = dr["SumPayCapital"].ToString();
            data[i, 2] = dr["SumPayInterest"].ToString();
            data[i, 3] = dr["SumTotalPay"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailCPReportTableCalCapitalAndInterest(string cp2id) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@cp2id", (!string.IsNullOrEmpty(cp2id) ? cp2id : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 26];
        string[,] data1;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["ID"].ToString();
            data[i, 2] = dr["BCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["MajorCode"].ToString();
            data[i, 10] = dr["FacultyCode"].ToString();
            data[i, 11] = dr["FacultyName"].ToString();
            data[i, 12] = dr["GroupNum"].ToString();
            data[i, 13] = dr["DLevel"].ToString();
            data[i, 14] = dr["DLevelName"].ToString();
            data[i, 15] = dr["FileName"].ToString();
            data[i, 16] = dr["FolderName"].ToString();
            data[i, 17] = dr["StatusRepay"].ToString();
            data[i, 18] = dr["StatusPayment"].ToString();
            data[i, 19] = dr["FormatPayment"].ToString();
            data[i, 20] = dr["TotalPenalty"].ToString();
            data[i, 21] = string.Empty;

            data1 = ListLastTransPayment(dr["ID"].ToString());

            if (data1.GetLength(0) > 0)
                data[i, 21] = data1[0, 4];

            data[i, 22] = dr["LawyerFullname"].ToString();
            data[i, 23] = dr["LawyerPhoneNumber"].ToString();
            data[i, 24] = dr["LawyerMobileNumber"].ToString();
            data[i, 25] = dr["LawyerEmail"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCalCPReportTableCalCapitalAndInterest(
        string capital,
        string interest,
        string pay,
        string paymentDate
    ) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 30),
            new SqlParameter("@capital", (!string.IsNullOrEmpty(capital) ? capital : null)),
            new SqlParameter("@interest", (!string.IsNullOrEmpty(interest) ? interest : null)),
            new SqlParameter("@pay", (!string.IsNullOrEmpty(pay) ? pay : null)),
            new SqlParameter("@paiddate", (!string.IsNullOrEmpty(paymentDate) ? paymentDate : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count + 1, 9];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["PaidPeriod"].ToString();
            data[i, 1] = dr["Capital"].ToString();
            data[i, 2] = dr["Paid"].ToString();
            data[i, 3] = dr["Interest"].ToString();
            data[i, 4] = dr["PayTotal"].ToString();
            data[i, 5] = dr["PaidDate"].ToString();

            i++;
        }

        foreach (DataRow dr1 in ds.Tables[1].Rows) {
            data[i, 6] = dr1["SumPaid"].ToString();
            data[i, 7] = dr1["SumInterest"].ToString();
            data[i, 8] = dr1["SumPayTotal"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListSumCalCPReportTableCalCapitalAndInterest(
        string capital,
        string interest,
        string pay,
        string paymentDate
    ) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 30),
            new SqlParameter("@capital", (!string.IsNullOrEmpty(capital) ? capital : null)),
            new SqlParameter("@interest", (!string.IsNullOrEmpty(interest) ? interest : null)),
            new SqlParameter("@pay", (!string.IsNullOrEmpty(pay) ? pay : null)),
            new SqlParameter("@paiddate", (!string.IsNullOrEmpty(paymentDate) ? paymentDate : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 6];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["PaidPeriod"].ToString();
            data[i, 1] = dr["Capital"].ToString();
            data[i, 2] = dr["Paid"].ToString();
            data[i, 3] = dr["Interest"].ToString();
            data[i, 4] = dr["PayTotal"].ToString();
            data[i, 5] = dr["PaidDate"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportStepOfWork(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@section", section),
            new SqlParameter("@statusstepofwork", (!string.IsNullOrEmpty(c.Request["statusstepofwork"]) ? c.Request["statusstepofwork"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportStepOfWork"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPReportStepOfWork(HttpContext c) {
        HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        int section = int.Parse(eCPCookie["UserSection"]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@section", section),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@statusstepofwork", (!string.IsNullOrEmpty(c.Request["statusstepofwork"]) ? c.Request["statusstepofwork"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 17];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = SearchRepayStatusDetail(dr["RCID"].ToString(), dr["StatusRepay"].ToString(), dr["StatusPayment"].ToString());
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = eCPUtil.actionTrackingStatus[2, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountReportStepOfWorkOnStatisticRepayByProgram(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportStepOfWork"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListReportStepOfWorkOnStatisticRepayByProgram(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 17];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["StatusSend"].ToString();
            data[i, 11] = dr["StatusReceiver"].ToString();
            data[i, 12] = dr["StatusEdit"].ToString();
            data[i, 13] = dr["StatusCancel"].ToString();
            data[i, 14] = SearchRepayStatusDetail(dr["RCID"].ToString(), dr["StatusRepay"].ToString(), dr["StatusPayment"].ToString());
            data[i, 15] = dr["StatusPayment"].ToString();
            data[i, 16] = eCPUtil.actionTrackingStatus[2, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPReportStatisticRepay() {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 32)
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 10];
        int i = 0;
        double remain;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["AcadamicYear"].ToString();
            data[i, 2] = (!string.IsNullOrEmpty(dr["CountProgram"].ToString()) ? dr["CountProgram"].ToString() : "0");
            data[i, 3] = (!string.IsNullOrEmpty(dr["CountStudent"].ToString()) ? dr["CountStudent"].ToString() : "0");
            data[i, 4] = (!string.IsNullOrEmpty(dr["CountStudentNoPayment"].ToString()) ? dr["CountStudentNoPayment"].ToString() : "0");
            data[i, 5] = (!string.IsNullOrEmpty(dr["CountStudentPaymentComplete"].ToString()) ? dr["CountStudentPaymentComplete"].ToString() : "0");
            data[i, 6] = (!string.IsNullOrEmpty(dr["CountStudentPaymentIncomplete"].ToString()) ? dr["CountStudentPaymentIncomplete"].ToString() : "0");
            data[i, 7] = (!string.IsNullOrEmpty(dr["SumTotalPenalty"].ToString()) ? dr["SumTotalPenalty"].ToString() : "0");
            data[i, 8] = (!string.IsNullOrEmpty(dr["SumTotalPay"].ToString()) ? dr["SumTotalPay"].ToString() : "0");

            remain = (double.Parse(data[i, 7]) - double.Parse(data[i, 8]));

            data[i, 9] = (remain > 0 ? remain.ToString() : "0");

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPReportStatisticRepayByProgram(string acadamicyear) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 33),
            new SqlParameter("@acadamicyear", acadamicyear)
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 15];
        int i = 0;
        double remain;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["AcadamicYear"].ToString();
            data[i, 2] = dr["FacultyCode"].ToString();
            data[i, 3] = dr["FacultyName"].ToString();
            data[i, 4] = dr["ProgramCode"].ToString();
            data[i, 5] = dr["ProgramName"].ToString();
            data[i, 6] = dr["MajorCode"].ToString();
            data[i, 7] = dr["GroupNum"].ToString();
            data[i, 8] = (!string.IsNullOrEmpty(dr["CountStudent"].ToString()) ? dr["CountStudent"].ToString() : "0");
            data[i, 9] = (!string.IsNullOrEmpty(dr["CountStudentNoPayment"].ToString()) ? dr["CountStudentNoPayment"].ToString() : "0");
            data[i, 10] = (!string.IsNullOrEmpty(dr["CountStudentPaymentComplete"].ToString()) ? dr["CountStudentPaymentComplete"].ToString() : "0");
            data[i, 11] = (!string.IsNullOrEmpty(dr["CountStudentPaymentIncomplete"].ToString()) ? dr["CountStudentPaymentIncomplete"].ToString() : "0");
            data[i, 12] = (!string.IsNullOrEmpty(dr["SumTotalPenalty"].ToString()) ? dr["SumTotalPenalty"].ToString() : "0");
            data[i, 13] = (!string.IsNullOrEmpty(dr["SumTotalPay"].ToString()) ? dr["SumTotalPay"].ToString() : "0");

            remain = (double.Parse(data[i, 12]) - double.Parse(data[i, 13]));

            data[i, 14] = (remain > 0 ? remain.ToString() : "0");

            i++;
        }

        ds.Dispose();

        return data;
    }
    
    public static string[,] ListCPReportStatisticContract() {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 38)
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 5];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["AcadamicYear"].ToString();
            data[i, 2] = (!string.IsNullOrEmpty(dr["CountStudent"].ToString()) ? dr["CountStudent"].ToString() : "0");
            data[i, 3] = (!string.IsNullOrEmpty(dr["CountStudentSignContract"].ToString()) ? dr["CountStudentSignContract"].ToString() : "0");
            data[i, 4] = (!string.IsNullOrEmpty(dr["CountStudentContractPenalty"].ToString()) ? dr["CountStudentContractPenalty"].ToString() : "0");

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountReportStudentSignContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 40),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportStudentSignContract"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListReportStudentSignContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 40),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 10];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["StudentID"].ToString();
            data[i, 2] = dr["TitleTName"].ToString();
            data[i, 3] = dr["ThaiFName"].ToString();
            data[i, 4] = dr["ThaiLName"].ToString();
            data[i, 5] = dr["GuarantorTitleTName"].ToString();
            data[i, 6] = dr["GuarantorFirstName"].ToString();
            data[i, 7] = dr["GuarantorLastName"].ToString();
            data[i, 8] = Util.ConvertDateTH(dr["contractDateSignByStudent"].ToString());
            data[i, 9] = Util.ConvertTimeTH(dr["contractDateSignByStudent"].ToString());

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListCPReportStatisticContractByProgram(string acadamicyear) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 39),
            new SqlParameter("@acadamicyear", acadamicyear)
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 11];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["AcadamicYear"].ToString();
            data[i, 2] = dr["FacultyCode"].ToString();
            data[i, 3] = dr["FactTName"].ToString();
            data[i, 4] = dr["ProgramCode"].ToString();
            data[i, 5] = dr["ProgTName"].ToString();
            data[i, 6] = dr["MajorCode"].ToString();
            data[i, 7] = dr["GroupNum"].ToString();
            data[i, 8] = (!string.IsNullOrEmpty(dr["CountStudent"].ToString()) ? dr["CountStudent"].ToString() : "0");
            data[i, 9] = (!string.IsNullOrEmpty(dr["CountStudentSignContract"].ToString()) ? dr["CountStudentSignContract"].ToString() : "0");
            data[i, 10] = (!string.IsNullOrEmpty(dr["CountStudentContractPenalty"].ToString()) ? dr["CountStudentContractPenalty"].ToString() : "0");

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportNoticeRepayComplete(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportNoticeRepayComplete"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListReportNoticeRepayComplete(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 12];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["TotalPenalty"].ToString();            
            data[i, 11] = dr["StatusPayment"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailReportNoticeRepayComplete(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 16];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["BCID"].ToString();
            data[i, 1] = dr["RCID"].ToString();
            data[i, 2] = dr["StudentID"].ToString();
            data[i, 3] = dr["TitleTName"].ToString();
            data[i, 4] = dr["FirstTName"].ToString();
            data[i, 5] = dr["LastTName"].ToString();
            data[i, 6] = dr["FacultyCode"].ToString();
            data[i, 7] = dr["FacultyName"].ToString();
            data[i, 8] = dr["ProgramCode"].ToString();
            data[i, 9] = dr["ProgramName"].ToString();
            data[i, 10] = dr["GroupNum"].ToString();
            data[i, 11] = dr["GraduateDate"].ToString();
            data[i, 12] = dr["IndemnitorYear"].ToString();
            data[i, 13] = dr["IndemnitorAddress"].ToString();            
            data[i, 14] = dr["TotalPenalty"].ToString();
            data[i, 15] = dr["StatusPayment"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportNoticeClaimDebt(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportNoticeClaimDebt"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListReportNoticeClaimDebt(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 12];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["TotalPenalty"].ToString();
            data[i, 11] = SearchRepayStatusDetail(dr["RCID"].ToString(), dr["StatusRepay"].ToString(), dr["StatusPayment"].ToString());

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListDetailReportNoticeClaimDebt(string cp1id) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@cp1id", (!string.IsNullOrEmpty(cp1id) ? cp1id : null))
        );
        
        string[,] data = new string[ds.Tables[1].Rows.Count, 27];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["BCID"].ToString();
            data[i, 1] = dr["RCID"].ToString();
            data[i, 2] = dr["StudentID"].ToString();
            data[i, 3] = dr["TitleTName"].ToString();
            data[i, 4] = dr["FirstTName"].ToString();
            data[i, 5] = dr["LastTName"].ToString();
            data[i, 6] = dr["FacultyCode"].ToString();
            data[i, 7] = dr["FacultyName"].ToString();
            data[i, 8] = dr["ProgramCode"].ToString();
            data[i, 9] = dr["ProgramName"].ToString();
            data[i, 10] = dr["GroupNum"].ToString();
            data[i, 11] = dr["IndemnitorYear"].ToString();
            data[i, 12] = dr["IndemnitorCash"].ToString();            
            data[i, 13] = dr["ContractDate"].ToString();
            data[i, 14] = dr["Guarantor"].ToString();
            data[i, 15] = dr["ContractDateAgreement"].ToString();
            data[i, 16] = dr["ApproveDate"].ToString();
            data[i, 17] = dr["TotalPenalty"].ToString();
            data[i, 18] = dr["SubtotalPenalty"].ToString();
            data[i, 19] = dr["StudyLeave"].ToString();
            data[i, 20] = dr["AfterStudyLeaveEndDate"].ToString();
            data[i, 21] = dr["LawyerFullname"].ToString();
            data[i, 22] = dr["LawyerPhoneNumber"].ToString();
            data[i, 23] = dr["LawyerMobileNumber"].ToString();
            data[i, 24] = dr["LawyerEmail"].ToString();
            data[i, 25] = dr["StatusRepay"].ToString();
            data[i, 26] = dr["StatusPayment"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportStatisticPaymentByDate(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 41),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!string.IsNullOrEmpty(c.Request["formatpayment"]) ? c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportStatisticPaymentByDate"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListReportStatisticPaymentByDate(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 41),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!string.IsNullOrEmpty(c.Request["formatpayment"]) ? c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 14];
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["TotalPenalty"].ToString();
            data[i, 11] = dr["TotalPay"].ToString();
            data[i, 12] = dr["FormatPayment"].ToString();
            data[i, 13] = dr["FormatPaymentName"].ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportEContract(HttpContext c) {
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 42),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportEContract"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPReportEContract(HttpContext c) {        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 42),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!string.IsNullOrEmpty(c.Request["acadamicyear"]) ? c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 18];        
        string pathEContract = "https://econtract.mahidol.ac.th/ElectronicContract/";
        string path;
        string fileDocA, fileDocB, fileDocC;
        int i = 0;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["StudentID"].ToString();
            data[i, 2] = dr["TitleTName"].ToString();
            data[i, 3] = dr["ThaiFName"].ToString();
            data[i, 4] = dr["ThaiLName"].ToString();
            data[i, 5] = dr["ProgramCode"].ToString();
            data[i, 6] = dr["ProgTName"].ToString();
            data[i, 7] = dr["GroupNum"].ToString();
            /*
            if (dr["operationType"].Equals("S") ||
                dr["operationType"].Equals("M")) {
                data[i, 8] = "1";
                data[i, 9] = "1";
                data[i, 10] = "1";
            }

            if (dr["operationType"].Equals("O")) {
                data[i, 8] = (dr["contractSignByStudent"].Equals(true) ? "1" : "0");
                data[i, 9] = (dr["parentContractSignFlagF"].Equals(true) ? "1" : "0");
                data[i, 10] = (dr["parentContractSignFlagM"].Equals(true) ? "1" : "0");
            }

            p = dr["ProgramCode"].ToString();

            if (dr["ProgramCode"].Equals("NSNSB")) {
                switch (dr["QuotaCode"].ToString()) {
                    case "350":
                        p = p + "_Chulabhorn";
                        break;
                    case "391":
                        p = p + "_MT";
                        break;
                }
            }
            
            yearFolder = (DateTime.Parse(dr["contractDateSignByStudent"].ToString()).Month >= 5 ? DateTime.Parse(dr["contractDateSignByStudent"].ToString()).ToString("yyyy", new CultureInfo("th-TH")) : (DateTime.Parse(dr["contractDateSignByStudent"].ToString()).AddYears(-1)).ToString("yyyy", new CultureInfo("th-TH")));

            path = (pathEContract + _yearFolder + "/" + _p + "/");
            fileDocA = (data[i, 1] + data[i, 5] + "_A.pdf");
            fileDocB = (data[i, 1] + data[i, 5] + "_B.pdf");
            fileDocC = (data[i, 1] + data[i, 5] + "_C.pdf");
            */

            data[i, 8] = (!string.IsNullOrEmpty(dr["contractPath"].ToString()) ? "1" : "0");
            data[i, 9] = (!string.IsNullOrEmpty(dr["garranteePath"].ToString()) ? "1" : "0");
            data[i, 10] = (!string.IsNullOrEmpty(dr["warranPath"].ToString()) ? "1" : "0");

            path = pathEContract;
            fileDocA = dr["contractPath"].ToString();
            fileDocB = dr["garranteePath"].ToString();
            fileDocC = dr["warranPath"].ToString();

            data[i, 11] = path;
            data[i, 12] = fileDocA;
            data[i, 13] = fileDocB;
            data[i, 14] = fileDocC;

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportDebtorContract(HttpContext c) {
        int orderTable = 0;

        switch (c.Request["reportorder"]) {
            case "reportdebtorcontract":
                orderTable = 43;
                break;
            case "reportdebtorcontractpaid":
                orderTable = 45;
                break;
            case "reportdebtorcontractremain":
                orderTable = 47;
                break;
        }

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", orderTable),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        int recordCount = ds.Tables[0].Rows.Count;

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPReportDebtorContract(HttpContext c) {        
        int orderTable = 0;        

        switch (c.Request["reportorder"]) {
            case "reportdebtorcontract":
                orderTable = 43;
                break;
            case "reportdebtorcontractpaid":
                orderTable = 45;
                break;
            case "reportdebtorcontractremain":
                orderTable = 47;
                break;
        }

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", orderTable),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[0].Rows.Count, 14];
        int i = 0;
        double remain;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["FacultyCode"].ToString();
            data[i, 2] = dr["FacultyName"].ToString();
            data[i, 3] = dr["ProgramCode"].ToString();
            data[i, 4] = dr["ProgramName"].ToString();
            data[i, 5] = dr["MajorCode"].ToString();
            data[i, 6] = dr["GroupNum"].ToString();
            data[i, 7] = dr["DLevel"].ToString();
            data[i, 8] = dr["DLevelName"].ToString();            
            data[i, 9] = dr["CountStudentDebtor"].ToString();
            data[i, 10] = dr["SumTotalPenalty"].ToString();
            data[i, 11] = dr["SumTotalPayCapital"].ToString();
            data[i, 12] = dr["SumTotalPayInterest"].ToString();

            remain = (double.Parse(data[i, 10]) - double.Parse(data[i, 11]));

            data[i, 13] = remain.ToString();

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static int CountCPReportDebtorContractByProgram(HttpContext c) {        
        int orderTable = 0;

        switch (c.Request["reportorder"]) {
            case "reportdebtorcontract":
                orderTable = 44;
                break;
            case "reportdebtorcontractpaid":
                orderTable = 46;
                break;
            case "reportdebtorcontractremain":
                orderTable = 48;
                break;
        }

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", orderTable),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!string.IsNullOrEmpty(c.Request["formatpayment"]) ? c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        int recordCount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
            recordCount = int.Parse(dr["CountReportDebtorContractByProgram"].ToString());

        ds.Dispose();

        return recordCount;
    }

    public static string[,] ListCPReportDebtorContractByProgram(HttpContext c) {        
        int orderTable = 0;        

        switch (c.Request["reportorder"]) {
            case "reportdebtorcontract":
                orderTable = 44;
                break;
            case "reportdebtorcontractpaid":
                orderTable = 46;
                break;
            case "reportdebtorcontractremain":
                orderTable = 48;
                break;
        }

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", orderTable),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(c.Request["endrow"])),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(c.Request["studentid"]) ? c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!string.IsNullOrEmpty(c.Request["faculty"]) ? c.Request["faculty"] : null)),
            new SqlParameter("@program", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["programcode"] : null)),
            new SqlParameter("@major", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!string.IsNullOrEmpty(c.Request["programcode"]) && !string.IsNullOrEmpty(c.Request["majorcode"]) && !string.IsNullOrEmpty(c.Request["groupnum"]) ? c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!string.IsNullOrEmpty(c.Request["formatpayment"]) ? c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(c.Request["datestart"]) ? c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(c.Request["dateend"]) ? c.Request["dateend"] : null))
        );

        string[,] data = new string[ds.Tables[1].Rows.Count, 23];
        int i = 0;
        double remain;

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 0] = dr["RowNum"].ToString();
            data[i, 1] = dr["BCID"].ToString();
            data[i, 2] = dr["RCID"].ToString();
            data[i, 3] = dr["StudentID"].ToString();
            data[i, 4] = dr["TitleTName"].ToString();
            data[i, 5] = dr["FirstTName"].ToString();
            data[i, 6] = dr["LastTName"].ToString();
            data[i, 7] = dr["ProgramCode"].ToString();
            data[i, 8] = dr["ProgramName"].ToString();
            data[i, 9] = dr["MajorCode"].ToString();
            data[i, 10] = dr["GroupNum"].ToString();
            data[i, 11] = dr["TotalPenalty"].ToString();
            data[i, 12] = (!string.IsNullOrEmpty(dr["PayCapital"].ToString()) ? dr["PayCapital"].ToString() : "0");
            data[i, 13] = (!string.IsNullOrEmpty(dr["PayInterest"].ToString()) ? dr["PayInterest"].ToString() : "0");
            
            remain = (double.Parse(data[i, 11]) - double.Parse(data[i, 12]));

            data[i, 14] = remain.ToString();
            data[i, 15] = dr["ReplyDate"].ToString();
            data[i, 16] = dr["FormatPayment"].ToString();
            data[i, 17] = dr["FormatPaymentName"].ToString();
            data[i, 18] = dr["StatusSend"].ToString();
            data[i, 19] = dr["StatusReceiver"].ToString();
            data[i, 20] = dr["StatusEdit"].ToString();
            data[i, 21] = dr["StatusCancel"].ToString();
            data[i, 22] = eCPUtil.actionTrackingStatus[2, (Util.FindIndexArray3D(0, eCPUtil.actionTrackingStatus, (dr["StatusSend"].ToString() + dr["StatusReceiver"].ToString() + dr["StatusEdit"].ToString() + dr["StatusCancel"].ToString())) - 1), 1];

            i++;
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListExportReportDebtorContract(string exportSend) {
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];
        string idName = exportSendValue[2];

        separator = new char[] { ';' };
        string[] faculty = (!string.IsNullOrEmpty(exportSendValue[3]) ? exportSendValue[3].Split(separator) : new string[0]);
        string[] program = (!string.IsNullOrEmpty(exportSendValue[4]) ? exportSendValue[4].Split(separator) : new string[0]);
        string[] formatPayment = (!string.IsNullOrEmpty(exportSendValue[5]) ? exportSendValue[5].Split(separator) : new string[0]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 49),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(idName) ? idName : null)),
            new SqlParameter("@faculty", (faculty.GetLength(0) > 0 && !string.IsNullOrEmpty(faculty[0]) ? faculty[0] : null)),
            new SqlParameter("@program", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[0] : null)),
            new SqlParameter("@major", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[2] : null)),
            new SqlParameter("@groupnum", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[3] : null)),
            new SqlParameter("@formatpayment", (formatPayment.GetLength(0) > 0 && !string.IsNullOrEmpty(formatPayment[0]) ? formatPayment[0] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(dateStart) ? dateStart : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(dateEnd) ? dateEnd : null))
        );

        string[,] data = new string[(ds.Tables[0].Rows.Count + 1), 29];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["StudentID"].ToString();
            data[i, 1] = dr["TitleTName"].ToString();
            data[i, 2] = dr["FirstTName"].ToString();
            data[i, 3] = dr["LastTName"].ToString();
            data[i, 4] = dr["FacultyCode"].ToString();
            data[i, 5] = dr["FacultyName"].ToString();
            data[i, 6] = dr["ProgramCode"].ToString();
            data[i, 7] = dr["ProgramName"].ToString();
            data[i, 8] = dr["MajorCode"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["DLevel"].ToString();
            data[i, 11] = dr["DLevelName"].ToString();
            data[i, 12] = dr["CivilFlag"].ToString();
            data[i, 13] = dr["IndemnitorYear"].ToString();
            data[i, 14] = dr["AllActualDate"].ToString();
            data[i, 15] = dr["IndemnitorCash"].ToString();
            data[i, 16] = dr["RequireDate"].ToString();
            data[i, 17] = dr["ApproveDate"].ToString();
            data[i, 18] = dr["ActualDate"].ToString();
            data[i, 19] = dr["RemainDate"].ToString();
            data[i, 20] = dr["TotalPenalty"].ToString();
            data[i, 21] = dr["ReplyDate"].ToString();
            data[i, 22] = dr["FormatPayment"].ToString();
            data[i, 23] = dr["FormatPaymentName"].ToString();
            data[i, 24] = dr["StatusSend"].ToString();
            data[i, 25] = dr["StatusReceiver"].ToString();
            data[i, 26] = dr["StatusEdit"].ToString();
            data[i, 27] = dr["StatusCancel"].ToString();

            i++;
        }

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 28] = dr["TotalPenalty"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListExportReportDebtorContractPaid(string exportSend) {
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];
        string idName = exportSendValue[2];

        separator = new char[] { ';' };
        string[] faculty = (!string.IsNullOrEmpty(exportSendValue[3]) ? exportSendValue[3].Split(separator) : new string[0]);
        string[] program = (!string.IsNullOrEmpty(exportSendValue[4]) ? exportSendValue[4].Split(separator) : new string[0]);
        string[] formatPayment = (!string.IsNullOrEmpty(exportSendValue[5]) ? exportSendValue[5].Split(separator) : new string[0]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 50),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(idName) ? idName : null)),
            new SqlParameter("@faculty", (faculty.GetLength(0) > 0 && !string.IsNullOrEmpty(faculty[0]) ? faculty[0] : null)),
            new SqlParameter("@program", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[0] : null)),
            new SqlParameter("@major", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[2] : null)),
            new SqlParameter("@groupnum", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[3] : null)),
            new SqlParameter("@formatpayment", (formatPayment.GetLength(0) > 0 && !string.IsNullOrEmpty(formatPayment[0]) ? formatPayment[0] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(dateStart) ? dateStart : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(dateEnd) ? dateEnd : null))
        );

        string[,] data = new string[(ds.Tables[0].Rows.Count + 1), 46];
        int i = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["StudentID"].ToString();
            data[i, 1] = dr["TitleTName"].ToString();
            data[i, 2] = dr["FirstTName"].ToString();
            data[i, 3] = dr["LastTName"].ToString();
            data[i, 4] = dr["FacultyCode"].ToString();
            data[i, 5] = dr["FacultyName"].ToString();
            data[i, 6] = dr["ProgramCode"].ToString();
            data[i, 7] = dr["ProgramName"].ToString();
            data[i, 8] = dr["MajorCode"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["DLevel"].ToString();
            data[i, 11] = dr["DLevelName"].ToString();
            data[i, 12] = dr["ReplyDate"].ToString();
            data[i, 13] = dr["DateTimePayment"].ToString();
            data[i, 14] = dr["Capital"].ToString();
            data[i, 15] = dr["Interest"].ToString();            
            data[i, 16] = dr["TotalAccruedInterest"].ToString();
            data[i, 17] = dr["TotalInterest"].ToString();
            data[i, 18] = dr["TotalPayment"].ToString();
            data[i, 19] = dr["ReceiptDate"].ToString();
            data[i, 20] = dr["ReceiptBookNo"].ToString();
            data[i, 21] = dr["ReceiptNo"].ToString();
            data[i, 22] = dr["PayCapital"].ToString();
            data[i, 23] = dr["PayInterest"].ToString();
            data[i, 24] = dr["TotalPay"].ToString();
            data[i, 25] = dr["ReceiptSendNo"].ToString();
            data[i, 26] = dr["ReceiptFund"].ToString();
            data[i, 27] = dr["RemainCapital"].ToString();
            data[i, 28] = dr["AccruedInterest"].ToString();
            data[i, 29] = dr["RemainAccruedInterest"].ToString();
            data[i, 30] = dr["TotalRemain"].ToString();
            data[i, 31] = dr["FormatPayment"].ToString();
            data[i, 32] = dr["FormatPaymentName"].ToString();
            data[i, 33] = dr["StatusSend"].ToString();
            data[i, 34] = dr["StatusReceiver"].ToString();
            data[i, 35] = dr["StatusEdit"].ToString();
            data[i, 36] = dr["StatusCancel"].ToString();

            i++;
        }

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 37] = dr["TotalCapital"].ToString();
            data[i, 38] = dr["TotalInterest"].ToString();
            data[i, 39] = dr["TotalPayment"].ToString();
            data[i, 40] = dr["PayCapital"].ToString();
            data[i, 41] = dr["PayInterest"].ToString();
            data[i, 42] = dr["TotalPay"].ToString();
            data[i, 43] = dr["RemainCapital"].ToString();
            data[i, 44] = dr["RemainInterest"].ToString();
            data[i, 45] = dr["TotalRemain"].ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string[,] ListExportReportDebtorContractRemain(string exportSend) {        
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string dateStart = exportSendValue[0];
        string dateEnd = exportSendValue[1];
        string idName = exportSendValue[2];

        separator = new char[] { ';' };
        string[] faculty = (!string.IsNullOrEmpty(exportSendValue[3]) ? exportSendValue[3].Split(separator) : new string[0]);
        string[] program = (!string.IsNullOrEmpty(exportSendValue[4]) ? exportSendValue[4].Split(separator) : new string[0]);
        string[] formatPayment = (!string.IsNullOrEmpty(exportSendValue[5]) ? exportSendValue[5].Split(separator) : new string[0]);

        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 51),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(idName) ? idName : null)),
            new SqlParameter("@faculty", (faculty.GetLength(0) > 0 && !string.IsNullOrEmpty(faculty[0]) ? faculty[0] : null)),
            new SqlParameter("@program", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[0] : null)),
            new SqlParameter("@major", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[2] : null)),
            new SqlParameter("@groupnum", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[3] : null)),
            new SqlParameter("@formatpayment", (formatPayment.GetLength(0) > 0 && !string.IsNullOrEmpty(formatPayment[0]) ? formatPayment[0] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(dateStart) ? dateStart : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(dateEnd) ? dateEnd : null))
        );

        string[,] data = new string[(ds.Tables[0].Rows.Count + 1), 37];
        int i = 0;
        double totalRemainCapital = 0;
        double totalAccruedInterest = 0;
        double totalRemainAccruedInterest = 0;
        double totalTotalRemain = 0;

        foreach (DataRow dr in ds.Tables[0].Rows) {
            data[i, 0] = dr["StudentID"].ToString();
            data[i, 1] = dr["TitleTName"].ToString();
            data[i, 2] = dr["FirstTName"].ToString();
            data[i, 3] = dr["LastTName"].ToString();
            data[i, 4] = dr["FacultyCode"].ToString();
            data[i, 5] = dr["FacultyName"].ToString();
            data[i, 6] = dr["ProgramCode"].ToString();
            data[i, 7] = dr["ProgramName"].ToString();
            data[i, 8] = dr["MajorCode"].ToString();
            data[i, 9] = dr["GroupNum"].ToString();
            data[i, 10] = dr["DLevel"].ToString();
            data[i, 11] = dr["DLevelName"].ToString();
            data[i, 12] = dr["CivilFlag"].ToString();
            data[i, 13] = dr["IndemnitorYear"].ToString();
            data[i, 14] = dr["AllActualDate"].ToString();
            data[i, 15] = dr["IndemnitorCash"].ToString();
            data[i, 16] = dr["RequireDate"].ToString();
            data[i, 17] = dr["ApproveDate"].ToString();
            data[i, 18] = dr["ActualDate"].ToString();
            data[i, 19] = dr["RemainDate"].ToString();
            data[i, 20] = dr["TotalPenalty"].ToString();
            data[i, 21] = dr["ReplyDate"].ToString();
            data[i, 22] = (!dr["StatusPayment"].ToString().Equals("1") ? dr["RemainCapital"].ToString() : dr["TotalPenalty"].ToString());
            data[i, 23] = (!dr["StatusPayment"].ToString().Equals("1") ? dr["AccruedInterest"].ToString() : "0");
            data[i, 24] = (!dr["StatusPayment"].ToString().Equals("1") ? dr["RemainAccruedInterest"].ToString() : "0");
            data[i, 25] = (!dr["StatusPayment"].ToString().Equals("1") ? dr["TotalRemain"].ToString() : dr["TotalPenalty"].ToString());
            data[i, 26] = dr["FormatPayment"].ToString();
            data[i, 27] = dr["FormatPaymentName"].ToString();
            data[i, 28] = dr["StatusSend"].ToString();
            data[i, 29] = dr["StatusReceiver"].ToString();
            data[i, 30] = dr["StatusEdit"].ToString();
            data[i, 31] = dr["StatusCancel"].ToString();

            totalRemainCapital += double.Parse(data[i, 22]);
            totalAccruedInterest += double.Parse(data[i, 23]);
            totalRemainAccruedInterest += double.Parse(data[i, 24]);
            totalTotalRemain += double.Parse(data[i, 25]);

            i++;
        }

        foreach (DataRow dr in ds.Tables[1].Rows) {
            data[i, 32] = dr["TotalPenalty"].ToString();
            data[i, 33] = totalRemainCapital.ToString();
            data[i, 34] = totalAccruedInterest.ToString();
            data[i, 35] = totalRemainAccruedInterest.ToString();
            data[i, 36] = totalTotalRemain.ToString();
        }

        ds.Dispose();

        return data;
    }

    public static string ListExportReportDebtorContractBreakRequireRepayPayment(string exportSend) {
        char[] separator = new char[] { ':' };
        string[] exportSendValue = exportSend.Split(separator);
        string statusPayment = exportSendValue[0];
        string statusPaymentRecord = exportSendValue[1];
        string idName = exportSendValue[2];

        separator = new char[] { ';' };
        string[] faculty = (!string.IsNullOrEmpty(exportSendValue[3]) ? exportSendValue[3].Split(separator) : new string[0]);
        string[] program = (!string.IsNullOrEmpty(exportSendValue[4]) ? exportSendValue[4].Split(separator) : new string[0]);

        string dateStart = exportSendValue[5];
        string dateEnd = exportSendValue[6];
        
        DataSet ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@statuspayment", (!string.IsNullOrEmpty(statusPayment) ? statusPayment : null)),
            new SqlParameter("@statuspaymentrecord", (!string.IsNullOrEmpty(statusPaymentRecord) ? statusPaymentRecord : null)),
            new SqlParameter("@studentid", (!string.IsNullOrEmpty(idName) ? idName : null)),
            new SqlParameter("@faculty", (faculty.GetLength(0) > 0 && !string.IsNullOrEmpty(faculty[0]) ? faculty[0] : null)),
            new SqlParameter("@program", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[0] : null)),
            new SqlParameter("@major", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[2] : null)),
            new SqlParameter("@groupnum", (program.GetLength(0) > 0 && !string.IsNullOrEmpty(program[0]) && !string.IsNullOrEmpty(program[2]) && !string.IsNullOrEmpty(program[3]) ? program[3] : null)),
            new SqlParameter("@datestart", (!string.IsNullOrEmpty(dateStart) ? dateStart : null)),
            new SqlParameter("@dateend", (!string.IsNullOrEmpty(dateEnd) ? dateEnd : null))
        );

        JArray jsonArray = new JArray();

        foreach (DataRow dr in ds.Tables[1].Rows) {
            jsonArray.Add(new JObject() {
                { "ID", dr["ID"].ToString() },
                { "BCID", dr["BCID"].ToString() },
                { "studentCode", dr["StudentID"].ToString() },
                { "titleName", dr["TitleTName"].ToString() },
                { "firstName", dr["FirstTName"].ToString() },
                { "lastName", dr["LastTName"].ToString() },
                { "facultyCode", dr["FacultyCode"].ToString() },
                { "facultyName", dr["FacultyName"].ToString() },
                { "programCode", dr["ProgramCode"].ToString() },
                { "programName", dr["ProgramName"].ToString() },
                { "majorCode", dr["MajorCode"].ToString() },
                { "groupNum", dr["GroupNum"].ToString() },
                { "contractDate", dr["ContractDate"].ToString() },
                { "sendDate", dr["DateTimeSend"].ToString() },
                { "receiverDate", dr["DateTimeReceiver"].ToString() },
                { "replyDateHistory", dr["ReplyDateHistory"].ToString() },
                { "subtotalPenalty", dr["SubtotalPenalty"].ToString() },
                { "totalPenalty", dr["TotalPenalty"].ToString() },
                { "totalPayCapital", (!string.IsNullOrEmpty(dr["TotalPayCapital"].ToString()) ? dr["TotalPayCapital"].ToString() : "0") },
                { "totalPayInterest", (!string.IsNullOrEmpty(dr["TotalPayInterest"].ToString()) ? dr["TotalPayInterest"].ToString() : "0") },
                { "totalPay", (!string.IsNullOrEmpty(dr["TotalPay"].ToString()) ? dr["TotalPay"].ToString() : "0") },
                { "totalRemain", (!string.IsNullOrEmpty(dr["TotalRemain"].ToString()) ? dr["TotalRemain"].ToString() : "0") },
                { "remainAccruedInterest", (!string.IsNullOrEmpty(dr["RemainAccruedInterest"].ToString()) ? dr["RemainAccruedInterest"].ToString() : "0") },
                { "statusPayment", dr["StatusPayment"].ToString() },
                { "statusPaymentRecord", dr["StatusPaymentRecord"].ToString() }
            });
        }

        ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static void AddUpdateData(HttpContext c) {
        string command = string.Empty;
        string what = string.Empty;
        string where = string.Empty;
        string function = string.Empty;
        string sqlCommand = string.Empty;

        if (c.Request["cmd"].Equals("addcptabuser")) {
            HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
            
            what = "INSERT";
            where = "ecpTabUser";
            function = "AddUpdateData, addcptabuser";
            command += (
                "INSERT INTO ecpTabUser " +
                "(" +
                "ID, Username, Password, Name, PhoneNumber, MobileNumber, Email, UserSection, UserLevel" +
                ")" +
                "VALUES " +
                "(" +
                "newid(), " +
                "'" + c.Request["username"] + "', " +
                "null, " +
                /*
                "'" + c.Request["password"] + "', " +
                */
                "'" + c.Request["name"] + "', " +
                (string.IsNullOrEmpty(c.Request["phonenumber"]) ? "NULL" : ("'" + c.Request["phonenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["mobilenumber"]) ? "NULL" : ("'" + c.Request["mobilenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["email"]) ? "NULL" : ("'" + c.Request["email"] + "'")) + ", " +
                "'" + eCPCookie["UserSection"] + "', " +
                "'User'" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptabuser")) {
            what = "UPDATE";
            where = "ecpTabUser";
            function = "AddUpdateData, updatecptabuser";
            command += (
                "UPDATE ecpTabUser SET " +
                "Username = '" + c.Request["username"] + "', " +
                "Password = null, " +
                /*
                "Password = '" + c.Request["password"] + "', " +
                */
                "Name = '" + c.Request["name"] + "', " +
                "PhoneNumber = " + (string.IsNullOrEmpty(c.Request["phonenumber"]) ? "NULL" : ("'" + c.Request["phonenumber"] + "'")) + ", " +
                "MobileNumber = " + (string.IsNullOrEmpty(c.Request["mobilenumber"]) ? "NULL" : ("'" + c.Request["mobilenumber"] + "'")) + ", " +
                "Email = " + (string.IsNullOrEmpty(c.Request["email"]) ? "NULL" : ("'" + c.Request["email"] + "'")) + " " +
                "WHERE (ID = '" + c.Request["userid"] + "')"
            );
        }

        if (c.Request["cmd"].Equals("delcptabuser")) {
            what = "DELETE";
            where = "ecpTabUser";
            function = "AddUpdateData, ecpTabUser";
            command += (
                "DELETE " +
                "FROM ecpTabUser " +
                "WHERE (ID = '" + c.Request["userid"] + "')"
            );
        }

        if (c.Request["cmd"].Equals("addcptabprogram")) {
            what = "INSERT";
            where = "ecpTabProgram";
            function = "AddUpdateData, addcptabprogram";
            command += (
                "INSERT INTO ecpTabProgram " +
                "(" +
                "ProgramCode, MajorCode, GroupNum, FacultyCode, Dlevel" +
                ")" +
                "VALUES " +
                "(" +
                "'" + c.Request["programcode"] + "', " +
                "'" + c.Request["majorcode"] + "', " +
                "'" + c.Request["groupnum"] + "', " +
                "'" + c.Request["faculty"] + "', " +
                "'" + c.Request["dlevel"] + "'" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptabprogram")) {
            what = "UPDATE";
            where = "ecpTabProgram";
            function = "AddUpdateData, updatecptabprogram";
            command += (
                "UPDATE ecpTabProgram SET " +
                "ProgramCode = '" + c.Request["programcode"] + "', " +
                "MajorCode = '" + c.Request["majorcode"] + "', " +
                "GroupNum = '" + c.Request["groupnum"] + "', " +
                "FacultyCode = '" + c.Request["faculty"] + "', " +
                "Dlevel = '" + c.Request["dlevel"] + "' " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("delcptabprogram")) {
            what = "DELETE";
            where = "ecpTabProgram";
            function = "AddUpdateData, delcptabprogram";
            command += (
                "DELETE " +
                "FROM ecpTabProgram " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptabinterest")) {
            what = "INSERT";
            where = "ecpTabInterest";
            function = "AddUpdateData, addcptabinterest";
            command += (
                "INSERT INTO ecpTabInterest " +
                "(" +
                "InContractInterest, OutContractInterest, UseContractInterest" +
                ")" +
                "VALUES "+
                "(" + 
                c.Request["incontractinterest"] + ", " + 
                c.Request["outcontractinterest"] + ", " +
                "0" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updateusecontractinterest")) {
            what = "UPDATE";
            where = "ecpTabInterest";
            function = "AddUpdateData, updateusecontractinterest";
            command += (
                "UPDATE ecpTabInterest SET UseContractInterest = 0 " +
                "UPDATE ecpTabInterest SET UseContractInterest = " + c.Request["usecontractinterest"] + " WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("updatecptabinterest")) {
            what = "UPDATE";
            where = "ecpTabInterest";
            function = "AddUpdateData, updatecptabinterest";
            command += (
                "UPDATE ecpTabInterest SET " +
                "InContractInterest = " + c.Request["incontractinterest"] + ", " +
                "OutContractInterest = " + c.Request["outcontractinterest"] + " " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("delcptabinterest")) {
            what = "DELETE";
            where = "ecpTabInterest";
            function = "AddUpdateData, delcptabinterest";
            command += (
                "DELETE " +
                "FROM ecpTabInterest " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptabpaybreakcontract")) {
            what = "INSERT";
            where = "ecpTabPayBreakContract";
            function = "AddUpdateData, addcptabpaybreakcontract";
            command += (
                "INSERT INTO ecpTabPayBreakContract " +
                "(" +
                "FacultyCode, ProgramCode, MajorCode, GroupNum, AmountCash, Dlevel, CaseGraduate, CalDateCondition, AmtIndemnitorYear" +
                ")" +
                "VALUES " +
                "(" + 
                "'" + c.Request["faculty"] + "', " +
                "'" + c.Request["programcode"] + "', " +
                "'" + c.Request["majorcode"] + "', " +
                "'" + c.Request["groupnum"] + "', " +
                c.Request["amountcash"] + ", " +
                "'" + c.Request["dlevel"] + "', " +
                c.Request["casegraduate"] + ", " +
                c.Request["caldatecondition"] + ", " +
                (string.IsNullOrEmpty(c.Request["amtindemnitoryear"]) ? "0" : c.Request["amtindemnitoryear"]) +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptabpaybreakcontract")) {
            what = "UPDATE";
            where = "ecpTabPayBreakContract";
            function = "AddUpdateData, updatecptabpaybreakcontract";
            command += (
                "UPDATE ecpTabPayBreakContract SET " +
                "FacultyCode = '" + c.Request["faculty"] + "', " +
                "ProgramCode = '" + c.Request["programcode"] + "', " +
                "MajorCode = '" + c.Request["majorcode"] + "', " +
                "GroupNum = '" + c.Request["groupnum"] + "', " +
                "AmountCash = " + c.Request["amountcash"] + ", " +
                "Dlevel = '" + c.Request["dlevel"] + "', " +
                "CaseGraduate = " + c.Request["casegraduate"] + ", " +
                "CalDateCondition = " + c.Request["caldatecondition"] + ", " +
                "AmtIndemnitorYear = " + (string.IsNullOrEmpty(c.Request["amtindemnitoryear"]) ? "0" : c.Request["amtindemnitoryear"]) + " " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("delcptabpaybreakcontract")) {
            what = "DELETE";
            where = "ecpTabPayBreakContract";
            function = "AddUpdateData, delcptabpaybreakcontract";
            command += (
                "DELETE " +
                "FROM ecpTabPayBreakContract " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptabscholarship")) {
            what = "INSERT";
            where = "ecpTabScholarship";
            function = "AddUpdateData, addcptabscholarship";
            command += (
                "INSERT INTO ecpTabScholarship " +
                "(" +
                "FacultyCode, ProgramCode, MajorCode, GroupNum, ScholarshipMoney, Dlevel" +
                ")" +
                "VALUES " +
                "(" +
                "'" + c.Request["faculty"] + "', " +
                "'" + c.Request["programcode"] + "', " +
                "'" + c.Request["majorcode"] + "', " +
                "'" + c.Request["groupnum"] + "', " +
                c.Request["scholarshipmoney"] + ", " +
                "'" + c.Request["dlevel"] + "'" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptabscholarship")) {
            what = "UPDATE";
            where = "ecpTabScholarship";
            function = "AddUpdateData, updatecptabscholarship";
            command += (
                "UPDATE ecpTabScholarship SET " +
                "FacultyCode = '" + c.Request["faculty"] + "', " +
                "ProgramCode = '" + c.Request["programcode"] + "', " +
                "MajorCode = '" + c.Request["majorcode"] + "', " +
                "GroupNum = '" + c.Request["groupnum"] + "', " +
                "ScholarshipMoney = " + c.Request["scholarshipmoney"] + ", " +
                "Dlevel = '" + c.Request["dlevel"] + "' " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("delcptabscholarship")) {
            what = "DELETE";
            where = "ecpTabScholarship";
            function = "AddUpdateData, delcptabscholarship";
            command += (
                "DELETE " +
                "FROM ecpTabScholarship " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptransbreakcontract")) {
            what = "INSERT";
            where = "ecpTransBreakContract";
            function = "AddUpdateData, addcptransbreakcontract";
            command += (
                "INSERT INTO ecpTransBreakContract " +
                "(" +
                "StudentID, TitleCode, TitleEName, TitleTName, FirstEName, LastEName, FirstTName, LastTName, FacultyCode, FacultyName, ProgramCode, ProgramName, MajorCode, GroupNum, DLevel, " +
                "PursuantBook, Pursuant, PursuantBookDate, Location, InputDate, StateLocation, StateLocationDate, ContractDate, ContractDateAgreement, Guarantor, " +
                "ScholarFlag, ScholarshipMoney, ScholarshipYear, ScholarshipMonth, EducationDate, GraduateDate, ContractForceStartDate, ContractForceEndDate, CaseGraduate, CivilFlag, " +
                "CalDateCondition, IndemnitorYear, IndemnitorCash, StatusSend, StatusReceiver, StatusEdit, StatusCancel, " +
                "DateTimeCreate" +
                ")" +
                "VALUES " +
                "(" +
                "'" + c.Request["studentid"] + "', " +
                "'" + c.Request["titlecode"] + "', " +
                "'" + c.Request["titlenameeng"] + "', " +
                "'" + c.Request["titlenametha"] + "', " +
                "'" + c.Request["firstnameeng"] + "', " +
                "'" + c.Request["lastnameeng"] + "', " +
                "'" + c.Request["firstnametha"] + "', " +
                "'" + c.Request["lastnametha"] + "', " +
                "'" + c.Request["facultycode"] + "', " +
                "'" + c.Request["facultyname"] + "', " +
                "'" + c.Request["programcode"] + "', " +
                "'" + c.Request["programname"] + "', " +
                "'" + c.Request["majorcode"] + "', " +
                "'" + c.Request["groupnum"] + "', " +
                "'" + c.Request["dlevel"] + "', " +
                "'" + c.Request["pursuantbook"] + "', " +
                "'" + c.Request["pursuant"] + "', " +
                "'" + c.Request["pursuantbookdate"] + "', " +
                "'" + c.Request["location"] + "', " +
                "'" + c.Request["inputdate"] + "', " +
                "'" + c.Request["statelocation"] + "', " +
                "'" + c.Request["statelocationdate"] + "', " +
                "'" + c.Request["contractdate"] + "', " +
                "'" + c.Request["contractdateagreement"] + "', " +
                "'" + c.Request["guarantor"] + "', " +
                c.Request["scholarflag"] + ", " +
                (string.IsNullOrEmpty(c.Request["scholarshipmoney"]) ? "0" : c.Request["scholarshipmoney"]) + ", " +
                (string.IsNullOrEmpty(c.Request["scholarshipyear"]) ? "0" : c.Request["scholarshipyear"]) + ", " +
                (string.IsNullOrEmpty(c.Request["scholarshipmonth"]) ? "0" : c.Request["scholarshipmonth"]) + ", " +
                "'" + c.Request["educationdate"] + "', " +
                "'" + c.Request["graduatedate"] + "', " +
                "'" + c.Request["contractforcedatestart"] + "', " +
                (string.IsNullOrEmpty(c.Request["contractforcedateend"]) ? "NULL" : ("'" + c.Request["contractforcedateend"] + "'")) + ", " +
                c.Request["casegraduate"] + ", " +                        
                c.Request["civilflag"] + ", " +
                c.Request["caldatecondition"] + ", " +
                (string.IsNullOrEmpty(c.Request["indemnitoryear"]) ? "0" : c.Request["indemnitoryear"]) + ", " +
                c.Request["indemnitorcash"] + ", " +
                "1, " +
                "1, " +
                "1, " +
                "1, " +
                "GETDATE()" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptransbreakcontract")) {
            what = "UPDATE";
            where = "ecpTransBreakContract";
            function = "AddUpdateData, updatecptransbreakcontract";
            command += (
                "UPDATE ecpTransBreakContract SET " +
                "StudentID = '" + c.Request["studentid"] + "', " +
                "TitleCode = '" + c.Request["titlecode"] + "', " +
                "TitleEName = '" + c.Request["titlenameeng"] + "', " +
                "TitleTName = '" + c.Request["titlenametha"] + "', " +
                "FirstEName = '" + c.Request["firstnameeng"] + "', " +
                "LastEName = '" + c.Request["lastnameeng"] + "', " +
                "FirstTName = '" + c.Request["firstnametha"] + "', " +
                "LastTName = '" + c.Request["lastnametha"] + "', " +
                "FacultyCode = '" + c.Request["facultycode"] + "', " +
                "FacultyName = '" + c.Request["facultyname"] + "', " +
                "ProgramCode = '" + c.Request["programcode"] + "', " +
                "ProgramName = '" + c.Request["programname"] + "', " +
                "MajorCode = '" + c.Request["majorcode"] + "', " +
                "GroupNum = '" + c.Request["groupnum"] + "', " +
                "DLevel = '" + c.Request["dlevel"] + "', " +
                "PursuantBook = '" + c.Request["pursuantbook"] + "', " +
                "Pursuant = '" + c.Request["pursuant"] + "', " +
                "PursuantBookDate = '" + c.Request["pursuantbookdate"] + "', " +
                "Location = '" + c.Request["location"] + "', " +
                "InputDate = '" + c.Request["inputdate"] + "', " +
                "StateLocation = '" + c.Request["statelocation"] + "', " +
                "StateLocationDate = '" + c.Request["statelocationdate"] + "', " +
                "ContractDate = '" + c.Request["contractdate"] + "', " +
                "ContractDateAgreement = '" + c.Request["contractdateagreement"] + "', " +
                "Guarantor = '" + c.Request["guarantor"] + "', " +
                "ScholarFlag = " + c.Request["scholarflag"] + ", " +
                "ScholarshipMoney=" + (string.IsNullOrEmpty(c.Request["scholarshipmoney"]) ? "0" : c.Request["scholarshipmoney"]) + ", " +
                "ScholarshipYear=" + (string.IsNullOrEmpty(c.Request["scholarshipyear"]) ? "0" : c.Request["scholarshipyear"]) + ", " +
                "ScholarshipMonth=" + (string.IsNullOrEmpty(c.Request["scholarshipmonth"]) ? "0" : c.Request["scholarshipmonth"]) + ", " +
                "EducationDate = '" + c.Request["educationdate"] + "', " +
                "GraduateDate = '" + c.Request["graduatedate"] + "', " +
                "ContractForceStartDate = '" + c.Request["contractforcedatestart"] + "', " +
                "ContractForceEndDate = " + (string.IsNullOrEmpty(c.Request["contractforcedateend"]) ? "NULL" : ("'" + c.Request["contractforcedateend"] + "'")) + ", " +
                "CaseGraduate = " + c.Request["casegraduate"] + ", " +
                "CivilFlag = " + c.Request["civilflag"] + ", " +
                "CalDateCondition = " + c.Request["caldatecondition"] + ", " +
                "IndemnitorYear = " + (string.IsNullOrEmpty(c.Request["indemnitoryear"]) ? "0" : c.Request["indemnitoryear"]) + ", " +
                "IndemnitorCash = " + c.Request["indemnitorcash"] + ", " +
                "StatusEdit = 1, " +
                "DateTimeModify = GETDATE() " +
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("updatetrackingstatusbreakcontract")) {
            what = "UPDATE";
            where = "ecpTransBreakContract";
            function = "AddUpdateData, updatetrackingstatusbreakcontract";
            command += (
                "UPDATE ecpTransBreakContract SET " +
                (c.Request["status"].Equals("send") ? "StatusSend = 2, DateTimeSend = GETDATE() " : "") +
                (c.Request["status"].Equals("edit") ? "StatusEdit = 2 " : "") +
                (c.Request["status"].Equals("cancel") ? "StatusCancel = 2, DateTimeCancel = GETDATE() " : "") + 
                "WHERE ID = " + c.Request["cp1id"]
            );
        }

        if (c.Request["cmd"].Equals("sendbreakcontract")) {
            char[] separator = new char[] {';'};
            string[] cpid = (c.Request["cp1id"]).Split(separator);
            int i;

            for (i = 0; i < cpid.Length; i++) {
                what = "UPDATE";
                where = "ecpTransBreakContract";
                function = "AddUpdateData, sendbreakcontract";
                command += (
                    "UPDATE ecpTransBreakContract SET " +
                    "StatusSend = 2, " +
                    "DateTimeSend = GETDATE() " +
                    "WHERE ID = " + cpid[i] + "; "
                );
            }
        }

        if (c.Request["cmd"].Equals("rejectcptransbreakcontract")) {
            what = "UPDATE";
            where = "ecpTransBreakContract";
            function = "AddUpdateData, rejectcptransbreakcontract";
            command += (
                "UPDATE ecpTransBreakContract SET " +
                (c.Request["actioncomment"].Equals("E") ? "StatusEdit = 2 " : "") +
                (c.Request["actioncomment"].Equals("C") ? "StatusCancel = 2, DateTimeCancel = GETDATE() " : "") +                        
                "WHERE ID = " + c.Request["cp1id"] + "; " +
                "INSERT INTO ecpTransReject " +
                "(" +
                "BCID, Action, Comment, DateTimeReject" +
                ")" +
                "VALUES " +
                "(" +
                c.Request["cp1id"] + ", " +
                "'" + c.Request["actioncomment"] + "', " +
                "'" + c.Request["comment"] + "', " +
                "GETDATE()" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("addcptransrequirecontract")) {
            what = "UPDATE, INSERT";
            where = "ecpTransBreakContract, ecpTransRequireContract";
            function = "AddUpdateData, addcptransrequirecontract";
            command += (
                "UPDATE ecpTransBreakContract SET " +
                "IndemnitorYear = " + (string.IsNullOrEmpty(c.Request["indemnitoryear"]) ? "0" : c.Request["indemnitoryear"]) + ", " +
                "IndemnitorCash = " + c.Request["indemnitorcash"] + ", " +
                "StatusReceiver = 2, " +
                "DateTimeReceiver = GETDATE() " +
                "WHERE ID = " + c.Request["cp1id"] + "; " +
                "INSERT INTO ecpTransRequireContract "
            );

            if (c.Request["casegraduate"].Equals("1")) {
                if (c.Request["scholar"].Equals("1")) {
                    command += (
                        "(" +
                        "BCID, ActualMonthScholarship, ActualScholarship, TotalPayScholarship, ActualMonth, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment" +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp1id"] + ", " +
                        c.Request["actualmonthscholarship"] + ", " +
                        c.Request["actualscholarship"] + ", " +
                        c.Request["totalpayscholarship"] + ", " +
                        c.Request["actualmonth"] + ", " +
                        c.Request["actualday"] + ", " +
                        c.Request["subtotalpenalty"] + ", " +
                        c.Request["totalpenalty"] + ", " +
                        "'" + c.Request["lawyerfullname"] + "', " +
                        (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "'" + c.Request["lawyeremail"] + "', " +
                        "0, " +
                        "1, " +
                        "0" +
                        ")"
                    );
                }
                else {
                    command += (
                        "(" +
                        "BCID, TotalPayScholarship, ActualMonth, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment" +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp1id"] + ", " +
                        c.Request["totalpayscholarship"] + ", " +
                        c.Request["actualmonth"] + ", " +
                        c.Request["actualday"] + ", " +
                        c.Request["subtotalpenalty"] + ", " +
                        c.Request["totalpenalty"] + ", " +
                        "'" + c.Request["lawyerfullname"] + "', " +
                        (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "'" + c.Request["lawyeremail"] + "', " +
                        "0, " +
                        "1, " +
                        "0" +
                        ")"
                    );
                }
            }

            if (c.Request["casegraduate"].Equals("2")) {
                if (c.Request["civil"].Equals("1")) {
                    command += (
                        "(" +
                        "BCID, IndemnitorAddress, Province, StudyLeave, RequireDate, ApproveDate, BeforeStudyLeaveStartDate, BeforeStudyLeaveEndDate, StudyLeaveStartDate, StudyLeaveEndDate, AfterStudyLeaveStartDate, AfterStudyLeaveEndDate, TotalPayScholarship, AllActualDate, ActualDate, RemainDate, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment" +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp1id"] + ", " +
                        "'" + c.Request["indemnitoraddress"] + "', " +
                        "'" + c.Request["province"] + "', " +
                        "'" + c.Request["studyleave"] + "', " +
                        (string.IsNullOrEmpty(c.Request["requiredate"]) ? "NULL" : ("'" + c.Request["requiredate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["approvedate"]) ? "NULL" : ("'" + c.Request["approvedate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["beforestudyleavestartdate"]) ? "NULL" : ("'" + c.Request["beforestudyleavestartdate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["beforestudyleaveenddate"]) ? "NULL" : ("'" + c.Request["beforestudyleaveenddate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["studyleavestartdate"]) ? "NULL" : ("'" + c.Request["studyleavestartdate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["studyleaveenddate"]) ? "NULL" : ("'" + c.Request["studyleaveenddate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["afterstudyleavestartdate"]) ? "NULL" : ("'" + c.Request["afterstudyleavestartdate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["afterstudyleaveenddate"]) ? "NULL" : ("'" + c.Request["afterstudyleaveenddate"] + "'")) + ", " +
                        c.Request["totalpayscholarship"] + ", " +
                        (string.IsNullOrEmpty(c.Request["allactualdate"]) ? "NULL" : c.Request["allactualdate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["actualdate"]) ? "NULL" : c.Request["actualdate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["remaindate"]) ? "NULL" : c.Request["remaindate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["actualday"]) ? "NULL" : c.Request["actualday"]) + ", " +
                        c.Request["subtotalpenalty"] + ", " +
                        c.Request["totalpenalty"] + ", " +
                        "'" + c.Request["lawyerfullname"] + "', " +
                        (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "'" + c.Request["lawyeremail"] + "', " +
                        "0, " +
                        "1, " +
                        "0" +
                        ")"
                    );
                }
                else {
                    command += (
                        "(" +
                        "BCID, TotalPayScholarship, AllActualDate, ActualDate, RemainDate, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment" +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp1id"] + ", " +
                        c.Request["totalpayscholarship"] + ", " +
                        (string.IsNullOrEmpty(c.Request["allactualdate"]) ? "NULL" : c.Request["allactualdate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["actualdate"]) ? "NULL" : c.Request["actualdate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["remaindate"]) ? "NULL" : c.Request["remaindate"]) + ", " +
                        (string.IsNullOrEmpty(c.Request["actualday"]) ? "NULL" : c.Request["actualday"]) + ", " +
                        c.Request["subtotalpenalty"] + ", " +
                        c.Request["totalpenalty"] + ", " +
                        "'" + c.Request["lawyerfullname"] + "', " +
                        (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "'" + c.Request["lawyeremail"] + "', " +
                        "0, " +
                        "1, " +
                        "0" +
                        ")"
                    );
                }
            }
        }

        if (c.Request["cmd"].Equals("updatecptransrequirecontract")) {
            what = "UPDATE, UPDATE";
            where = "ecpTransBreakContract, ecpTransRequireContract";
            function = "AddUpdateData, updatecptransrequirecontract";
            command += (
                "UPDATE ecpTransBreakContract SET " +
                "IndemnitorYear = " + (string.IsNullOrEmpty(c.Request["indemnitoryear"]) ? "0" : c.Request["indemnitoryear"]) + ", " +
                "IndemnitorCash = " + c.Request["indemnitorcash"] + ", " +
                "StatusReceiver = 2, " +
                "DateTimeModify = GETDATE() " +
                "WHERE ID = " + c.Request["cp1id"] + "; " +
                "UPDATE ecpTransRequireContract SET "
            );

            if (c.Request["casegraduate"].Equals("1")) {
                command += (
                    "ActualMonthScholarship = " + (string.IsNullOrEmpty(c.Request["actualmonthscholarship"]) ? "NULL" : c.Request["actualmonthscholarship"]) + ", " +
                    "ActualScholarship = " + (string.IsNullOrEmpty(c.Request["actualscholarship"]) ? "NULL" : c.Request["actualscholarship"]) + ", " +
                    "TotalPayScholarship = " + c.Request["totalpayscholarship"] + ", " +
                    "ActualMonth = " + c.Request["actualmonth"] + ", " +
                    "ActualDay = " + c.Request["actualday"] + ", " +
                    "SubtotalPenalty = " + c.Request["subtotalpenalty"] + ", " +
                    "TotalPenalty = " + c.Request["totalpenalty"] + ", " +
                    "LawyerFullname = '" + c.Request["lawyerfullname"] + "', " +
                    "LawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                    "LawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                    "LawyerEmail = '" + c.Request["lawyeremail"] + "', " +
                    "StatusRepay = 0, " +
                    "StatusPayment = 1, " +
                    "FormatPayment = 0 "
                );
            }
      
            if (c.Request["casegraduate"].Equals("2")) {
                if (c.Request["civil"].Equals("1")) {
                    command += (
                        "IndemnitorAddress = '" + c.Request["indemnitoraddress"] + "', " +
                        "Province = '" + c.Request["province"] + "', " +
                        "StudyLeave = '" + c.Request["studyleave"] + "', " +
                        "RequireDate = " + (string.IsNullOrEmpty(c.Request["requiredate"]) ? "NULL" : ("'" + c.Request["requiredate"] + "'")) + ", " +
                        "ApproveDate = " + (string.IsNullOrEmpty(c.Request["approvedate"]) ? "NULL" : ("'" + c.Request["approvedate"] + "'")) + ", " +
                        "BeforeStudyLeaveStartDate = " + (string.IsNullOrEmpty(c.Request["beforestudyleavestartdate"]) ? "NULL" : ("'" + c.Request["beforestudyleavestartdate"] + "'")) + ", " +
                        "BeforeStudyLeaveEndDate = " + (string.IsNullOrEmpty(c.Request["beforestudyleaveenddate"]) ? "NULL" : ("'" + c.Request["beforestudyleaveenddate"] + "'")) + ", " +
                        "StudyLeaveStartDate = " + (string.IsNullOrEmpty(c.Request["studyleavestartdate"]) ? "NULL" : ("'" + c.Request["studyleavestartdate"] + "'")) + ", " +
                        "StudyLeaveEndDate = " + (string.IsNullOrEmpty(c.Request["studyleaveenddate"]) ? "NULL" : ("'" + c.Request["studyleaveenddate"] + "'")) + ", " +
                        "AfterStudyLeaveStartDate = " + (string.IsNullOrEmpty(c.Request["afterstudyleavestartdate"]) ? "NULL" : ("'" + c.Request["afterstudyleavestartdate"] + "'")) + ", " +
                        "AfterStudyLeaveEndDate = " + (string.IsNullOrEmpty(c.Request["afterstudyleaveenddate"]) ? "NULL" : ("'" + c.Request["afterstudyleaveenddate"] + "'")) + ", " +
                        "TotalPayScholarship = " + c.Request["totalpayscholarship"] + ", " +
                        "AllActualDate = " + (string.IsNullOrEmpty(c.Request["allactualdate"]) ? "NULL" : c.Request["allactualdate"]) + ", " +
                        "ActualDate = " + (string.IsNullOrEmpty(c.Request["actualdate"]) ? "NULL" : c.Request["actualdate"]) + ", " +
                        "RemainDate = " + (string.IsNullOrEmpty(c.Request["remaindate"]) ? "NULL" : c.Request["remaindate"]) + ", " +
                        "ActualDay = " + (string.IsNullOrEmpty(c.Request["actualday"]) ? "NULL" : c.Request["actualday"]) + ", " +
                        "SubtotalPenalty = " + c.Request["subtotalpenalty"] + ", " +
                        "TotalPenalty = " + c.Request["totalpenalty"] + ", " +
                        "LawyerFullname = '" + c.Request["lawyerfullname"] + "', " +
                        "LawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        "LawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "LawyerEmail = '" + c.Request["lawyeremail"] + "', " +
                        "StatusRepay = 0, " +
                        "StatusPayment = 1, " +
                        "FormatPayment = 0 "
                    );
                }
                else {
                    command += (
                        "TotalPayScholarship = " + c.Request["totalpayscholarship"] + ", " +
                        "SubtotalPenalty = " + c.Request["subtotalpenalty"] + ", " +
                        "TotalPenalty = " + c.Request["totalpenalty"] + ", " +
                        "LawyerFullname = '" + c.Request["lawyerfullname"] + "', " +
                        "LawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                        "LawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                        "LawyerEmail = '" + c.Request["lawyeremail"] + "', " +
                        "StatusRepay = 0, " +
                        "StatusPayment = 1, " +
                        "FormatPayment = 0 "
                    );
                }
            }
      
            command += (
                "WHERE ID = " + c.Request["cp2id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptransrepaycontract")) {
            what = "UPDATE, INSERT";
            where = "ecpTransRequireContract, ecpTransRepayContract";
            function = "AddUpdateData, addcptransrepaycontract";
            command += (
                "UPDATE ecpTransRequireContract SET " +
                "StatusRepay = " + c.Request["statusrepay"] + " " +
                "WHERE ID = " + c.Request["cp2id"] + "; " +
                "INSERT INTO ecpTransRepayContract " +
                "(" +
                "RCID, StatusRepay, StatusReply, RepayDate" +
                ")" +
                "VALUES " +
                "(" +
                c.Request["cp2id"] + ", " +
                c.Request["statusrepay"] + ", " +
                "1, " +
                "'" + c.Request["repaydate"] + "'" +
                ")"
            );
        }

        if (c.Request["cmd"].Equals("updatecptransrepaycontract")) {
            what = "UPDATE";
            where = "ecpTransRepayContract";
            function = "AddUpdateData, updatecptransrepaycontract";
            command += (
                "UPDATE ecpTransRepayContract SET "
            );

            if (!string.IsNullOrEmpty(c.Request["replydate"]) &&
                !string.IsNullOrEmpty(c.Request["replyresult"])) {
                command += (
                    "StatusReply = 2, " +
                    "ReplyResult = " + c.Request["replyresult"] + ", " +
                    "ReplyDate = '" + c.Request["replydate"] + "', "
                );
            }
                
            command += (
                "RepayDate = '" + c.Request["repaydate"] + "', " +
                "Pursuant = " + (string.IsNullOrEmpty(c.Request["pursuant"]) ? "NULL" : ("'" + c.Request["pursuant"] + "'")) + ", " +
                "PursuantBookDate = " + (string.IsNullOrEmpty(c.Request["pursuantbookdate"]) ? "NULL" : ("'" + c.Request["pursuantbookdate"] + "'")) + " " +
                "WHERE (RCID = " + c.Request["cp2id"] + ") AND (StatusRepay = " + c.Request["statusrepay"] + ")"
            );
        }

        if (c.Request["cmd"].Equals("updatestatuspaymentrecord")) {
            what = "UPDATE";
            where = "ecpTransRequireContract";
            function = "UpdateData, updatestatuspaymentrecord";
            command += (
                "UPDATE ecpTransRequireContract SET " +
                "StatusPaymentRecord = " + (string.IsNullOrEmpty(c.Request["statuspaymentrecord"]) ? "NULL" : ("'" + c.Request["statuspaymentrecord"]) + "'") + ", " +
                "StatusPaymentRecordLawyerFullname = '" + c.Request["statuspaymentrecordlawyerfullname"] + "', " +
                "StatusPaymentRecordLawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request["statuspaymentrecordlawyerphonenumber"]) ? "NULL" : ("'" + c.Request["statuspaymentrecordlawyerphonenumber"] + "'")) + ", " +
                "StatusPaymentRecordLawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request["statuspaymentrecordlawyermobilenumber"]) ? "NULL" : ("'" + c.Request["statuspaymentrecordlawyermobilenumber"] + "'")) + ", " +
                "StatusPaymentRecordLawyerEmail = '" + c.Request["statuspaymentrecordlawyeremail"] + "' " +
                "WHERE ID = " + c.Request["cp2id"]
            );
        }

        if (c.Request["cmd"].Equals("addcptranspaymentfullrepay")) {
            string capital = c.Request["capital"];
            /*
            string totalAccruedInterest = c.Request["totalaccruedinterest"];
            */
            string totalPayment = c.Request["totalpayment"];
            string totalInterestOverpayment = c.Request["overpaymenttotalinterest"];
            string pay = c.Request["pay"];
            string overpay = c.Request["overpay"];
            string[] payRemain = new string[5];
            string[] channelDetail = new string[2];
            /*
            payRemain = eCPUtil.CalChkBalance(capital, totalInterest, totalAccruedInterest, totalPayment, pay);
            */

            double totalPay;

            totalPay = (
                (!string.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0) +
                (!string.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0) +
                (!string.IsNullOrEmpty(overpay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(overpay))) : 0)
            );

            switch (int.Parse(c.Request["channel"])) {
                case 1: 
                    channelDetail[0] = "ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
                case 2:
                    channelDetail[0] = "ChequeNo, ChequeBank, ChequeBankBranch, ChequeDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["chequeno"] + "', '" + c.Request["chequebank"] + "', '" + c.Request["chequebankbranch"] + "', '" + c.Request["chequedate"] + "', '" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
                case 3:
                    channelDetail[0] = "CashBank, CashBankBranch, CashBankAccount, CashBankAccountNo, CashBankDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["cashbank"] + "', '" + c.Request["cashbankbranch"] + "', '" + c.Request["cashbankaccount"] + "', " + (string.IsNullOrEmpty(c.Request["cashbankaccountno"]) ? "NULL" : ("'" + c.Request["cashbankaccountno"] + "'")) + ", '" + c.Request["cashbankdate"] + "', '" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
            }

            what = "UPDATE, INSERT";
            where = "ecpTransRequireContract, ecpTransPayment";
            function = "AddUpdateData, addcptranspaymentfullrepay";
            command += (
                "UPDATE ecpTransRequireContract SET " +
                "StatusPayment = 3, " +
                "FormatPayment = 1 " +
                "WHERE ID = " + c.Request["cp2id"] + "; " +
                "INSERT INTO ecpTransPayment " +
                "(" +
                "RCID, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + channelDetail[0] + ", LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail" +
                ")" +
                "VALUES " +
                "(" +
                c.Request["cp2id"] + ", " +
                totalInterestOverpayment + ", " +
                "'" + c.Request["datetimepayment"] + "', " +
                capital + ", " +
                totalInterestOverpayment + ", " +
                "0, " +
                totalPayment + ", " +
                capital + ", " +
                totalInterestOverpayment + ", " +
                totalPay + ", " +
                overpay + ", " +
                "0, " +
                "0, " +
                "0, " +
                "0, " +
                c.Request["channel"] + ", " +
                channelDetail[1] + ", " +
                "'" + c.Request["lawyerfullname"] + "', " +
                (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["lawyeremail"]) ? "NULL" : ("'" + c.Request["lawyeremail"] + "'")) + ")"
            );

            /*
            if (int.Parse(c.Request["overpayment"]) > 0) {
                if (c.Request["calinterestyesno"].Equals("Y")) {
                    command += (
                        "(" +
                        "RCID, CalInterestYesNo, OverpaymentDateStart, OverpaymentDateEnd, OverpaymentYear, OverpaymentDay, OverpaymentInterest, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp2id"] + ", " +
                        "'" + c.Request["calinterestyesno"] + "', " +
                        "'" + c.Request["overpaymentdatestart"] + "', " +
                        "'" + c.Request["overpaymentdateend"] + "', " +
                        c.Request["overpaymentyear"] + ", " +
                        c.Request["overpaymentday"] + ", " +
                        c.Request["overpaymentinterest"] + ", " +
                        c.Request["overpaymenttotalinterest"] + ", " +
                        "'" + c.Request["datetimepayment"] + "', " +
                        capital + ", " +
                        totalInterest + ", " +
                        totalAccruedInterest + ", " +
                        totalPayment + ", " +
                        payRemain[0] + ", " +
                        payRemain[1] + ", " +
                        pay + ", " +
                        overpay + ", " +
                        payRemain[2] + ", " +
                        payRemain[3] + ", " +
                        payRemain[4] + ", " +
                        payRemain[2] + ", " +
                        c.Request["channel"] + ", " +
                        channelDetail[1] +
                        ")"
                    );
                }

                if (c.Request["calinterestyesno"].Equals("N")) {
                    command += (
                        "(" +
                        "RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp2id"] + ", " +
                        "'" + c.Request["calinterestyesno"] + "', " +
                        "'" + c.Request["datetimepayment"] + "', " +
                        capital + ", " +
                        totalInterest + ", " +
                        totalAccruedInterest + ", " +
                        totalPayment + ", " +
                        payRemain[0] + ", " +
                        payRemain[1] + ", " +
                        pay + ", " +
                        overpay + ", " +
                        payRemain[2] + ", " +
                        payRemain[3] + ", " +
                        payRemain[4] + ", " +
                        payRemain[2] + ", " +
                        c.Request["channel"] + ", " +
                        channelDetail[1] +
                        ")"
                    );
                }
            }

            if (int.Parse(c.Request["overpayment"]) <= 0) {
                command += (
                    "(" +
                    "RCID, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                    ")" +
                    "VALUES " +
                    "(" +
                    c.Request["cp2id"] + ", " +
                    "'" + c.Request["datetimepayment"] + "', " +
                    capital + ", " +
                    totalInterest + ", " +
                    totalAccruedInterest + ", " +
                    totalPayment + ", " +
                    payRemain[0] + ", " +
                    payRemain[1] + ", " +
                    pay + ", " +
                    overpay + ", " +
                    payRemain[2] + ", " +
                    payRemain[3] + ", " +
                    payRemain[4] + ", " +
                    payRemain[2] + ", " +
                    c.Request["channel"] + ", " +
                    channelDetail[1] +
                    ")"
                );
            }
            */
        }

        if (c.Request["cmd"].Equals("addcptranspaymentpayrepay")) {
            string statusPayment = c.Request["statuspayment"];            
            string capital = c.Request["capital"];
            string totalAccruedInterest = c.Request["totalaccruedinterest"];
            string totalInterestOverpaymentBefore = c.Request["overpaymenttotalinterestbefore"];
            string totalInterestPayRepay = c.Request["payrepaytotalinterest"];
            string totalInterestOverpayment = c.Request["overpaymenttotalinterest"];
            string remainAccruedInterest = c.Request["remainaccruedinterest"];
            /*
            string totalPayment = c.Request["totalpayment"];
            */
            string pay = c.Request["pay"];
            string overpay = c.Request["overpay"];
            /*
            string[] payRemain = new string[5];
            */
            string[] channelDetail = new string[2];
            /*
            payRemain = eCPUtil.CalChkBalance(capital, totalInterest, totalAccruedInterest, totalPayment, pay);
            */

            double interest;
            double totalPayment;
            double payCapital;
            double payInterest;
            double remainCapital;            

            interest = (
                (!string.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) +
                (!string.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) +
                (!string.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
            );
            totalPayment = (
                (!string.IsNullOrEmpty(capital) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(capital))) : 0) +
                interest +
                (!string.IsNullOrEmpty(overpay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(overpay))) : 0)
            );
            /*
            totalPayment = (totalPayment > double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : totalPayment);
            */
            payCapital = (
                (!string.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0) -
                (!string.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0) -
                (!string.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) -
                (!string.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) -
                (!string.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
            );
            payCapital = (payCapital < 0 ? 0 : payCapital);
            payCapital -= (!string.IsNullOrEmpty(overpay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(overpay))) : 0);
            remainCapital = (
                (!string.IsNullOrEmpty(capital) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(capital))) : 0) -
                payCapital
            );
            remainCapital = (remainCapital < 0 ? 0 : remainCapital);
            /*
            remainAccruedInterest = (
                (
                    (!string.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0) +
                    (!string.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) +
                    (!string.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) +
                    (!string.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
                ) -
                (!string.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0)
            );
            remainAccruedInterest = (remainAccruedInterest < 0 ? 0 : remainAccruedInterest);
            */
            payInterest = (
                interest +
                (!string.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0)
                /*
                (
                    interest +
                    (!string.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0)
                ) -
                remainAccruedInterest
                */
            );

            switch (int.Parse(c.Request["channel"])) {
                case 1:
                    channelDetail[0] = "ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
                case 2:
                    channelDetail[0] = "ChequeNo, ChequeBank, ChequeBankBranch, ChequeDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["chequeno"] + "', '" + c.Request["chequebank"] + "', '" + c.Request["chequebankbranch"] + "', '" + c.Request["chequedate"] + "', '" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
                case 3:
                    channelDetail[0] = "CashBank, CashBankBranch, CashBankAccount, CashBankAccountNo, CashBankDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = ("'" + c.Request["cashbank"] + "', '" + c.Request["cashbankbranch"] + "', '" + c.Request["cashbankaccount"] + "', " + (string.IsNullOrEmpty(c.Request["cashbankaccountno"]) ? "NULL" : ("'" + c.Request["cashbankaccountno"] + "'")) + ", '" + c.Request["cashbankdate"] + "', '" + c.Request["receiptno"] + "', '" + c.Request["receiptbookno"] + "', '" + c.Request["receiptdate"] + "', '" + c.Request["receiptsendno"] + "', '" + c.Request["receiptfund"] + "', " + (string.IsNullOrEmpty(c.Request["receiptcopy"]) ? "NULL" : ("'" + c.Request["receiptcopy"] + "'")));
                    break;
            }

            what = "UPDATE, INSERT";
            where = "ecpTransRequireContract, ecpTransPayment";
            function = "AddUpdateData, addcptranspaymentpayrepay";
            command += (
                "UPDATE ecpTransRequireContract SET " +
                "StatusPayment = " + ((remainCapital.Equals(0) && double.Parse(remainAccruedInterest).Equals(0)) ? "3" : "2") + ", " +
                "FormatPayment = 2 " +
                "WHERE ID = " + c.Request["cp2id"] + "; " +
                "INSERT INTO ecpTransPayment " +
                "(" +
                "RCID, OverpaymentTotalInterestBefore, OverpaymentTotalInterest, PayRepayTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + channelDetail[0] + ", LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail" +
                ")" +
                "VALUES " +
                "(" +
                c.Request["cp2id"] + ", " +
                totalInterestOverpaymentBefore + ", " +
                totalInterestOverpayment + ", " +
                totalInterestPayRepay + ", " +
                "'" + c.Request["datetimepayment"] + "', " +
                capital + ", " +
                interest.ToString() + ", " +
                totalAccruedInterest + ", " +
                totalPayment.ToString() + ", " +
                payCapital.ToString() + ", " +
                payInterest.ToString() + ", " +
                pay + ", " +
                overpay + ", " +
                remainCapital.ToString() + ", " +
                "0, " +
                remainAccruedInterest + ", " +                        
                remainCapital.ToString() + ", " +
                c.Request["channel"] + ", " +
                channelDetail[1] + ", " +
                "'" + c.Request["lawyerfullname"] + "', " +
                (string.IsNullOrEmpty(c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + c.Request["lawyerphonenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + c.Request["lawyermobilenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request["lawyeremail"]) ? "NULL" : ("'" + c.Request["lawyeremail"] + "'")) + ")"
            );
            /*
            what = "UPDATE, INSERT";
            where = "ecpTransRequireContract, ecpTransPayment";
            function = "AddUpdateData, addcptranspaymentpayrepay";
            command += (
                "UPDATE ecpTransRequireContract SET " +
                "StatusPayment = " + (double.Parse(_payRemain[2]).Equals(0) ? "3" : "2") + ", " +
                "FormatPayment = 2 " +
                "WHERE ID = " + _c.Request["cp2id"] + "; " +
                "INSERT INTO ecpTransPayment "
            );

            if (statusPayment.Equals("1")) {
                if (int.Parse(c.Request["overpayment"]) > 0) {
                    if (c.Request["calinterestyesno"].Equals("Y")) {
                        command += (
                            "(" +
                            "RCID, CalInterestYesNo, OverpaymentDateStart, OverpaymentDateEnd, OverpaymentYear, OverpaymentDay, OverpaymentInterest, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                            ")" +
                            "VALUES " +
                            "(" +
                            c.Request["cp2id"] + ", " +
                            "'" + c.Request["calinterestyesno"] + "', " +
                            "'" + c.Request["overpaymentdatestart"] + "', " +
                            "'" + c.Request["overpaymentdateend"] + "', " +
                            c.Request["overpaymentyear"] + ", " +
                            c.Request["overpaymentday"] + ", " +
                            c.Request["overpaymentinterest"] + ", " +
                            c.Request["overpaymenttotalinterest"] + ", " +
                            "'" + c.Request["datetimepayment"] + "', " +
                            capital + ", " +
                            totalInterest + ", " +
                            totalAccruedInterest + ", " +
                            totalPayment + ", " +
                            payRemain[0] + ", " +
                            payRemain[1] + ", " +
                            pay + ", " +
                            payRemain[2] + ", " +
                            payRemain[3] + ", " +
                            payRemain[4] + ", " +
                            payRemain[2] + ", " +
                            c.Request["channel"] + ", " +
                            channelDetail[1] +
                            ")"
                        );
                    }

                    if (c.Request["calinterestyesno"].Equals("N")) {
                        command += (
                            "(" +
                            "RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                            ")" +
                            "VALUES " +
                            "(" +
                            c.Request["cp2id"] + ", " +
                            "'" + c.Request["calinterestyesno"] + "', " +
                            "'" + c.Request["datetimepayment"] + "', " +
                            capital + ", " +
                            totalInterest + ", " +
                            totalAccruedInterest + ", " +
                            totalPayment + ", " +
                            payRemain[0] + ", " +
                            payRemain[1] + ", " +
                            pay + ", " +
                            payRemain[2] + ", " +
                            payRemain[3] + ", " +
                            payRemain[4] + ", " +
                            payRemain[2] + ", " +
                            c.Request["channel"] + ", " +
                            channelDetail[1] +
                            ")"
                        );
                    }
                }
            }

            if (statusPayment.Equals("2") ||
                int.Parse(_c.Request["overpayment"]) <= 0) {
                if (c.Request["calinterestyesno"].Equals("Y")) {
                    command += (
                        "(" +
                        "RCID, CalInterestYesNo, PayRepayDateStart, PayRepayDateEnd, PayRepayYear, PayRepayDay, PayRepayInterest, PayRepayTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp2id"] + ", " +
                        "'" + c.Request["calinterestyesno"] + "', " +
                        "'" + c.Request["payrepaydatestart"] + "', " +
                        "'" + c.Request["payrepaydateend"] + "', " +
                        c.Request["payrepayyear"] + ", " +
                        c.Request["payrepayday"] + ", " +
                        c.Request["payrepayinterest"] + ", " +
                        c.Request["payrepaytotalinterest"] + ", " +
                        "'" + c.Request["datetimepayment"] + "', " +
                        capital + ", " +
                        totalInterest + ", " +
                        totalAccruedInterest + ", " +
                        totalPayment + ", " +
                        payRemain[0] + ", " +
                        payRemain[1] + ", " +
                        pay + ", " +
                        payRemain[2] + ", " +
                        payRemain[3] + ", " +
                        payRemain[4] + ", " +
                        payRemain[2] + ", " +
                        c.Request["channel"] + ", " +
                        channelDetail[1] +
                        ")"
                    );
                }

                if (c.Request["calinterestyesno"].Equals("N")) {
                    command += (
                        "(" +
                        "RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] +
                        ")" +
                        "VALUES " +
                        "(" +
                        c.Request["cp2id"] + ", " +
                        "'" + c.Request["calinterestyesno"] + "', " +
                        "'" + c.Request["datetimepayment"] + "', " +
                        capital + ", " +
                        totalInterest + ", " +
                        totalAccruedInterest + ", " +
                        totalPayment + ", " +
                        payRemain[0] + ", " +
                        payRemain[1] + ", " +
                        pay + ", " +
                        payRemain[2] + ", " +
                        payRemain[3] + ", " +
                        payRemain[4] + ", " +
                        payRemain[2] + ", " +
                        c.Request["channel"] + ", " +
                        channelDetail[1] +
                        ")"
                    );
                }
            } 
            */
        }

        if (c.Request["cmd"].Equals("delcptranspayment")) {
            string statusPayment = c.Request["statuspayment"];
            int period = int.Parse(c.Request["period"]);
            int periodTotal = int.Parse(c.Request["periodtotal"]);

            if (periodTotal.Equals(1)) {
                what = "UPDATE, DELETE";
                where = "ecpTransRequireContract, ecpTransPayment";
                function = "AddUpdateData, updatecptransrequirecontract, delcptranspayment";
                command += (
                    "UPDATE ecpTransRequireContract SET " +
                    "StatusPayment = 1, " +
                    "FormatPayment = 0 " +
                    "WHERE ID = " + c.Request["cp2id"] + "; "
                );
            }

            if (periodTotal > 1) {
                if (statusPayment.Equals("3")) {
                    what = "UPDATE, DELETE";
                    where = "ecpTransRequireContract, ecpTransPayment";
                    function = "AddUpdateData, updatecptransrequirecontract, delcptranspayment";
                    command += (
                        "UPDATE ecpTransRequireContract SET " +
                        "StatusPayment = 2 " +
                        "WHERE ID = " + c.Request["cp2id"] + "; "
                    );
                }
                else {
                    what = "DELETE";
                    where = "ecpTransPayment";
                    function = "AddUpdateData, delcptranspayment";
                }
            }

            command += (
                "DELETE " +
                "FROM ecpTransPayment " +
                "WHERE (ID = " + c.Request["ecpTransPaymentID"] + ") AND (RCID = " + c.Request["cp2id"] + ")"
            );
        }

        if (c.Request["cmd"].Equals("addcptransprosecution")) {
            HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string userid = eCPUtil.GetUserID();
            string[] documentetail = new string[2];

            switch (c.Request["document"]) {
                case "complaint":
                    documentetail[0] = "ComplaintLawyerFullname, ComplaintLawyerPhoneNumber, ComplaintLawyerMobileNumber, ComplaintLawyerEmail, ComplaintBlackCaseNo, ComplaintCapital, ComplaintInterest, ComplaintActionDate, ComplaintActionBy, ComplaintActionIP";
                    documentetail[1] = (
                        (string.IsNullOrEmpty(c.Request["complaintblackcaseno"]) ? "NULL" : ("'" + c.Request["complaintblackcaseno"] + "'")) + ", " + 
                        (string.IsNullOrEmpty(c.Request["complaintcapital"]) ? "NULL" : c.Request["complaintcapital"]) + ", " + 
                        (string.IsNullOrEmpty(c.Request["complaintinterest"]) ? "NULL" : c.Request["complaintinterest"]) + ", " +
                        "GETDATE(), " +
                        "'" + userid + "', " +
                        "dbo.fnc_sysGetIP()"
                    );
                    break;
                case "judgment":
                    documentetail[0] = "JudgmentLawyerFullname, JudgmentLawyerPhoneNumber, JudgmentLawyerMobileNumber, JudgmentLawyerEmail, JudgmentRedCaseNo, JudgmentVerdict, JudgmentCopy, JudgmentRemark, JudgmentActionDate, JudgmentActionBy, JudgmentActionIP";
                    documentetail[1] = (
                        (string.IsNullOrEmpty(c.Request["judgmentredcaseno"]) ? "NULL" : ("'" + c.Request["judgmentredcaseno"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["judgmentverdict"]) ? "NULL" : ("'" + c.Request["judgmentverdict"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["judgmentcopy"]) ? "NULL" : ("'" + c.Request["judgmentcopy"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["judgmentremark"]) ? "NULL" : ("'" + c.Request["judgmentremark"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + userid + "', " +
                        "dbo.fnc_sysGetIP()"
                    );
                    break;
                case "execution":
                    documentetail[0] = "ExecutionLawyerFullname, ExecutionLawyerPhoneNumber, ExecutionLawyerMobileNumber, ExecutionLawyerEmail, ExecutionDate, ExecutionCopy, ExecutionActionDate, ExecutionActionBy, ExecutionActionIP";
                    documentetail[1] = (
                        (string.IsNullOrEmpty(c.Request["executiondate"]) ? "NULL" : ("'" + c.Request["executiondate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["executioncopy"]) ? "NULL" : ("'" + c.Request["executioncopy"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + userid + "', " +
                        "dbo.fnc_sysGetIP()"
                    );
                    break;
                case "executionwithdraw":
                    documentetail[0] = "ExecutionWithdrawLawyerFullname, ExecutionWithdrawLawyerPhoneNumber, ExecutionWithdrawLawyerMobileNumber, ExecutionWithdrawLawyerEmail, ExecutionWithdrawDate, ExecutionWithdrawReason, ExecutionWithdrawCopy, ExecutionWithdrawActionDate, ExecutionWithdrawActionBy, ExecutionWithdrawActionIP";
                    documentetail[1] = (
                        (string.IsNullOrEmpty(c.Request["executionwithdrawdate"]) ? "NULL" : ("'" + c.Request["executionwithdrawdate"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["executionwithdrawreason"]) ? "NULL" : ("'" + c.Request["executionwithdrawreason"] + "'")) + ", " +
                        (string.IsNullOrEmpty(c.Request["executionwithdrawcopy"]) ? "NULL" : ("'" + c.Request["executionwithdrawcopy"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + userid + "', " +
                        "dbo.fnc_sysGetIP()"
                    );
                    break;
            }

            what = "INSERT";
            where = "ecpTransProsecution";
            function = "AddUpdateData, addcptransprosecution";
            command += (
                "INSERT INTO ecpTransProsecution " +
                "(" +
                "RCID, " + documentetail[0] +
                ")" +
                "VALUES " +
                "(" +
                c.Request["cp2id"] + ", " +
                "'" + c.Request[c.Request["document"] + "lawyerfullname"] + "', " +
                (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                "'" + c.Request[c.Request["document"] + "lawyeremail"] + "', " +
                documentetail[1] +
                ")"
            );
        }


        if (c.Request["cmd"].Equals("updatecptransprosecution")) {
            HttpCookie eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string userid = eCPUtil.GetUserID();
            string documentetail = string.Empty;

            switch (c.Request["document"]) {
                case "complaint":
                    documentetail = (
                        "ComplaintLawyerFullname = '" + c.Request[c.Request["document"] + "lawyerfullname"] + "', " +
                        "ComplaintLawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ComplaintLawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ComplaintLawyerEmail = '" + c.Request[c.Request["document"] + "lawyeremail"] + "', " +
                        "ComplaintBlackCaseNo = " + (string.IsNullOrEmpty(c.Request["complaintblackcaseno"]) ? "NULL" : ("'" + c.Request["complaintblackcaseno"] + "'")) + ", " +
                        "ComplaintCapital = " + (string.IsNullOrEmpty(c.Request["complaintcapital"]) ? "NULL" : c.Request["complaintcapital"]) + ", " +
                        "ComplaintInterest = " + (string.IsNullOrEmpty(c.Request["complaintinterest"]) ? "NULL" : c.Request["complaintinterest"]) + ", " +
                        "ComplaintActionDate = GETDATE(), " +
                        "ComplaintActionBy = '" + userid + "', " +
                        "ComplaintActionIP = dbo.fnc_sysGetIP()"
                    );
                    break;
                case "judgment":
                    documentetail = (
                        "JudgmentLawyerFullname = '" + c.Request[c.Request["document"] + "lawyerfullname"] + "', " +
                        "JudgmentLawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "JudgmentLawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "JudgmentLawyerEmail = '" + c.Request[c.Request["document"] + "lawyeremail"] + "', " +
                        "JudgmentRedCaseNo = " + (string.IsNullOrEmpty(c.Request["judgmentredcaseno"]) ? "NULL" : ("'" + c.Request["judgmentredcaseno"] + "'")) + ", " +
                        "JudgmentVerdict = " + (string.IsNullOrEmpty(c.Request["judgmentverdict"]) ? "NULL" : ("'" + c.Request["judgmentverdict"] + "'")) + ", " +
                        "JudgmentCopy = " + (string.IsNullOrEmpty(c.Request["judgmentcopy"]) ? "NULL" : ("'" + c.Request["judgmentcopy"] + "'")) + ", " +
                        "JudgmentRemark = " + (string.IsNullOrEmpty(c.Request["judgmentremark"]) ? "NULL" : ("'" + c.Request["judgmentremark"] + "'")) + ", " +
                        "JudgmentActionDate = GETDATE(), " +
                        "JudgmentActionBy = '" + userid + "', " +
                        "JudgmentActionIP = dbo.fnc_sysGetIP()"
                    );
                    break;
                case "execution":
                    documentetail = (
                        "ExecutionLawyerFullname = '" + c.Request[c.Request["document"] + "lawyerfullname"] + "', " +
                        "ExecutionLawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ExecutionLawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ExecutionLawyerEmail = '" + c.Request[c.Request["document"] + "lawyeremail"] + "', " +
                        "ExecutionDate = " + (string.IsNullOrEmpty(c.Request["executiondate"]) ? "NULL" : ("'" + c.Request["executiondate"] + "'")) + ", " +
                        "ExecutionCopy = " + (string.IsNullOrEmpty(c.Request["executioncopy"]) ? "NULL" : ("'" + c.Request["executioncopy"] + "'")) + ", " +
                        "ExecutionActionDate = GETDATE(), " +
                        "ExecutionActionBy = '" + userid + "', " +
                        "ExecutionActionIP = dbo.fnc_sysGetIP()"
                    );
                    break;
                case "executionwithdraw":
                    documentetail = (
                        "ExecutionWithdrawLawyerFullname = '" + c.Request[c.Request["document"] + "lawyerfullname"] + "', " +
                        "ExecutionWithdrawLawyerPhoneNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ExecutionWithdrawLawyerMobileNumber = " + (string.IsNullOrEmpty(c.Request[c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + c.Request[c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ExecutionWithdrawLawyerEmail = '" + c.Request[c.Request["document"] + "lawyeremail"] + "', " +
                        "ExecutionWithdrawDate = " + (string.IsNullOrEmpty(c.Request["executionwithdrawdate"]) ? "NULL" : ("'" + c.Request["executionwithdrawdate"] + "'")) + ", " +
                        "ExecutionWithdrawReason = " + (string.IsNullOrEmpty(c.Request["executionwithdrawreason"]) ? "NULL" : ("'" + c.Request["executionwithdrawreason"] + "'")) + ", " +
                        "ExecutionWithdrawCopy = " + (string.IsNullOrEmpty(c.Request["executionwithdrawcopy"]) ? "NULL" : ("'" + c.Request["executionwithdrawcopy"] + "'")) + ", " +
                        "ExecutionWithdrawActionDate = GETDATE(), " +
                        "ExecutionWithdrawActionBy = '" + userid + "', " +
                        "ExecutionWithdrawActionIP = dbo.fnc_sysGetIP()"
                    );
                    break;
            }

            what = "UPDATE";
            where = "ecpTransProsecution";
            function = "AddUpdateData, updatecptransprosecution";
            command += (
                "UPDATE ecpTransProsecution SET " +
                documentetail + " " +
                "WHERE RCID = " + c.Request["cp2id"]
            );
        }

        command = (command + "; " + InsertTransactionLog(what, where, function, command.Replace("'", "''")));

        ConnectStoreProcAddUpdate(command);
    }
}