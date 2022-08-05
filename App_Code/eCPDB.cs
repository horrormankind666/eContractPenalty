/*
Description         : สำหรับจัดการฐานข้อมูล
Date Created        : ๐๖/๐๘/๒๕๕๕
Last Date Modified  : ๑๑/๐๗/๒๕๖๕
Create By           : Yutthaphoom Tawana
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class eCPDB {
    public const string CONNECTION_STRING = "Server=stddb.mahidol;Database=DB4Dev;User ID=A;Password=gv@12345;Connect Timeout=600;Asynchronous Processing=true;";
    /*
    public const string CONNECTION_STRING = "Server=stddb.mahidol;Database=Infinity;User ID=MUstudent53;Password=oydL7dKk53;Connect Timeout=600;Asynchronous Processing=true;";
    */
    /*
    public const string CONNECTION_STRING = "Server=stddb.mahidol;Database=MUStudent;User ID=MUstudent53;Password=oydL7dKk53;Asynchronous Processing=true;";
    public const string CONNECTION_STRING = "Server=smartdev-write.mahidol;Database=Infinity;User ID=A;Password=ryoT6Noidc9d;Connect Timeout=600;Asynchronous Processing=true;";
    public const string CONNECTION_STRING = "Server=.\\SQLEXPRESS;Database=eContractPenalty;User ID=sa;Password=123456;Asynchronous Processing=true;";
    */
    private const string STORE_PROC = "sp_ecpEContractPenalty";
    public static string[] _userSection = new string[] {"กองกฎหมาย", "กองบริหารการศึกษา", "กองคลัง"};
    public static string _username;
    public static string _password;

    /*
    private static void ConnectDB() {
        _eCPConn = new SqlConnection();

        try {
            if (_eCPConn.State == ConnectionState.Open)
                _eCPConn.Close();

            _eCPConn.ConnectionString = CONNECTION_STRING;
            _eCPConn.Open();
        }
        catch {
        }
    }    

    private static void DisConnectDB() {
        try {
            if (_eCPConn.State == ConnectionState.Open)
                _eCPConn.Close();

            _eCPConn = null;
        }
        catch {
        }
    }

    private static SqlCommand ConnectStoreProc(string _sqlCmd) {
        ConnectDB();
        
        SqlCommand _cmd = new SqlCommand(_sqlCmd);
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.CommandTimeout = _eCPConn.ConnectionTimeout;
        _cmd.Connection = _eCPConn;

        return _cmd;
    }
    */

    public static SqlConnection ConnectDB(string _connString) {
        SqlConnection _conn = new SqlConnection(_connString);

        return _conn;
    }

    public static DataSet ExecuteCommandStoredProcedure(params SqlParameter[] _values) {
        SqlConnection _conn = ConnectDB(CONNECTION_STRING);
        SqlCommand _cmd = new SqlCommand(STORE_PROC, _conn);
        DataSet _ds = new DataSet();

        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.CommandTimeout = 1000;

        if (_values != null && _values.Length > 0)
            _cmd.Parameters.AddRange(_values);

        try {
            _conn.Open();

            SqlDataAdapter _da = new SqlDataAdapter(_cmd);

            _ds = new DataSet();
            _da.Fill(_ds);
        }
        finally {
            _cmd.Dispose();

            _conn.Close();
            _conn.Dispose();
        }

        return _ds;
    }

    public static void ConnectStoreProcAddUpdate(string _sqlCmd) {
        SqlConnection _conn = ConnectDB(CONNECTION_STRING);
        SqlCommand _cmd = new SqlCommand(STORE_PROC, _conn);

        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.CommandTimeout = 1000;
        _cmd.Parameters.AddWithValue("@ordertable", 52);
        _cmd.Parameters.AddWithValue("@cmd", _sqlCmd);

        try {
            _conn.Open();
            _cmd.ExecuteNonQuery();
        }
        finally {
            _cmd.Dispose();

            _conn.Close();
            _conn.Dispose();
        }
    }
    
    public static string InsertTransactionLog(
        string _what,
        string _where,
        string _function,
        string _sqlCommand
    ) {
        string _command = String.Empty;
        string _whoIs = String.Empty;
        string _name = String.Empty;
        string _userid = eCPUtil.GetUserID();
        string[,] _data = eCPDB.ListDetailCPTabUser(_userid, "", "", "");

        _whoIs = _data[0, 1];
        _name = _data[0, 3];

        _command += "INSERT INTO ecpTransLog " +
                    "(WhoIs, Name, IP, WhatIs, WhereIs, FunctionIs, SQLCommand, WhenIs) " +
                    "VALUES " +
                    "(" +
                    "'" + _whoIs + "', " +
                    "'" + _name + "', " +
                    "'" + Util.GetIP() + "', " +
                    "'" + _what + "', " +
                    "'" + _where + "', " +
                    "'" + _function + "', " +
                    "'" + _sqlCommand + "', " +
                    "GETDATE()" +
                    ")";

        return _command;
    }

    public static bool ChkLogin() {
        bool _loginResult = false;
        string _sql = String.Empty;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

        if (_eCPCookie == null) {
            _loginResult = false;
        }
        else {
            int _rowCount;

            if ((String.IsNullOrEmpty(_eCPCookie["Authen"])) ||
                /*
                (String.IsNullOrEmpty(_eCPCookie["Username"])) ||
                (String.IsNullOrEmpty(_eCPCookie["Password"])) ||
                (String.IsNullOrEmpty(_eCPCookie["Name"])) ||
                */
                (String.IsNullOrEmpty(_eCPCookie["UserSection"])) ||
                (String.IsNullOrEmpty(_eCPCookie["UserLevel"])) ||
                (String.IsNullOrEmpty(_eCPCookie["Pid"]))) {
                Signout();
                _loginResult = false;
            }
            else {
                string _userid = eCPUtil.GetUserID();
                string[,] _data = ListDetailCPTabUser(_userid, "", "", "");

                _rowCount = _data.GetLength(0);

                if (_rowCount <= 0) {
                    Signout();
                    _loginResult = false;
                }
                else {
                    if (!_data[0, 9].Equals(_userid) ||
                        !_data[0, 4].Equals(_eCPCookie["UserSection"]) ||
                        !_data[0, 5].Equals(_eCPCookie["UserLevel"])) {
                        Signout();
                        _loginResult = false;
                    }
                    else {
                        _loginResult = true;
                    }
                }
            }
        }

        return _loginResult;
    }

    public static bool Signin(string _authen) {
        int _rowCount;
        bool _loginResult = false;
        string _sql = String.Empty;
        string _sqlCommand = String.Empty;
    
        Dictionary<string, string> _auth = eCPUtil.GetUsername(_authen);
        string _username = _auth["Username"];
        string _password = _auth["Password"];

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 1),
            new SqlParameter("@username", _username),
            new SqlParameter("@password", _password)
        );

        _rowCount = _ds.Tables[0].Rows.Count;
        
        if(_rowCount <= 0) {
            _loginResult = false;
        }
        else {
            DataRow _dr = _ds.Tables[0].Rows[0];

            if (!(_dr["Username"].ToString()).Equals(_username) || !(_dr["Password"].ToString()).Equals(_password)) {
                _loginResult = false;
            }
            else {
                HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
                /*
                _eCPCookie.Values.Add("Username", _dr["Username"].ToString());
                _eCPCookie.Values.Add("Password", _dr["Password"].ToString());
                _eCPCookie.Values.Add("Name", HttpContext.Current.Server.UrlEncode(_dr["Name"].ToString()));
                */
                _eCPCookie.Values.Add("Authen", eCPUtil.EncodeToBase64(new string(eCPUtil.EncodeToBase64(_dr["ID"].ToString()).Reverse().ToArray())));
                _eCPCookie.Values.Add("UserSection", _dr["UserSection"].ToString());
                _eCPCookie.Values.Add("UserLevel", _dr["UserLevel"].ToString());
                _eCPCookie.Values.Add("Pid", "0");
                                
                /*
                _eCPCookie.Expires = DateTime.Now.AddHours(1);
                */
                HttpContext.Current.Response.Cookies.Add(_eCPCookie);
                            
                _loginResult = true;

                _sqlCommand += " SELECT * FROM ecpTabUser WHERE (Username = ''" + _username + "'') AND (Password = ''" + _password + "'')";

                ConnectStoreProcAddUpdate(InsertTransactionLog("SIGN IN", "EContractParentAndStaff", "Signin", _sqlCommand));
            }
        }

        _ds.Dispose();

        return _loginResult;
    }

    public static void ClearUser() {
        _username = String.Empty;
        _password = String.Empty;
    }

    public static void Signout() {
        ConnectStoreProcAddUpdate(InsertTransactionLog("SIGN OUT", "", "Signout", ""));
        
        HttpContext.Current.Session.Abandon();

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie.Expires = DateTime.Now.AddDays(-1D);
        HttpContext.Current.Response.Cookies.Add(_eCPCookie);
        
        ClearUser();
    }

    public static int CountCPTabUser(HttpContext _c) {
        int _section;
        int _recordCount = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", _section),
            new SqlParameter("@userlevel", "User"),
            new SqlParameter("@name", (!String.IsNullOrEmpty(_c.Request["name"]) ? _c.Request["name"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountCPTabUser"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPTabUser(HttpContext _c) {
        int _section;
        int _i = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@section", _section),
            new SqlParameter("@userlevel", "User"),
            new SqlParameter("@name", (!String.IsNullOrEmpty(_c.Request["name"]) ? _c.Request["name"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 10];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["Username"].ToString();
            _data[_i, 2] = _dr["Password"].ToString();
            _data[_i, 3] = _dr["Name"].ToString();
            _data[_i, 4] = _dr["UserSection"].ToString();
            _data[_i, 5] = _dr["UserLevel"].ToString();
            _data[_i, 6] = _dr["PhoneNumber"].ToString();
            _data[_i, 7] = _dr["MobileNumber"].ToString();
            _data[_i, 8] = _dr["Email"].ToString();
            _data[_i, 9] = _dr["ID"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static DataSet ListCPTabUser() {
        int _section;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", _section),
            new SqlParameter("@userlevel", "User")
        );

        _ds.Dispose();

        return _ds;
    }

    public static string[,] ListDetailCPTabUser(
        string _userid,
        string _username,
        string _password,
        string _userlevel
    ) {
        int _section;
        int _i = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 36),
            new SqlParameter("@section", _section),
            new SqlParameter("@userid", _userid),
            new SqlParameter("@username", (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_password) ? _username : null)),
            new SqlParameter("@password", (!String.IsNullOrEmpty(_username) && !String.IsNullOrEmpty(_password) ? _password : null)),
            new SqlParameter("@userlevel", (!String.IsNullOrEmpty(_userlevel) ? _userlevel : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 10];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["Username"].ToString();
            _data[_i, 2] = _dr["Password"].ToString();
            _data[_i, 3] = _dr["Name"].ToString();
            _data[_i, 4] = _dr["UserSection"].ToString();
            _data[_i, 5] = _dr["UserLevel"].ToString();
            _data[_i, 6] = _dr["PhoneNumber"].ToString();
            _data[_i, 7] = _dr["MobileNumber"].ToString();
            _data[_i, 8] = _dr["Email"].ToString();
            _data[_i, 9] = _dr["ID"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static int CheckRepeatCPTabUser(
        HttpContext _c,
        string _column
    ) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 37),
            new SqlParameter("@username", (_column.Equals("username") ? _c.Request["username"] : null)),
            new SqlParameter("@usernameold", (_column.Equals("username") && !String.IsNullOrEmpty(_c.Request["usernameold"]) ? _c.Request["usernameold"] : null)),
            new SqlParameter("@password", (_column.Equals("password") ? _c.Request["password"] : null)),
            new SqlParameter("@passwordold", (_column.Equals("password") && !String.IsNullOrEmpty(_c.Request["passwordold"]) ? _c.Request["passwordold"] : null))
        );

        _recordCount = _ds.Tables[0].Rows.Count;

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPTabProgram(string _cp1id) {
        int _i = 0;
        string _sql = String.Empty;

        DataSet _ds = ExecuteCommandStoredProcedure(
           new SqlParameter("@ordertable", 23),
           new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 9];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["FacultyCode"].ToString();
            _data[_i, 2] = _dr["FactTName"].ToString();
            _data[_i, 3] = _dr["ProgramCode"].ToString();
            _data[_i, 4] = _dr["ProgTName"].ToString();
            _data[_i, 5] = _dr["MajorCode"].ToString();
            _data[_i, 6] = _dr["GroupNum"].ToString();
            _data[_i, 7] = _dr["Dlevel"].ToString();
            _data[_i, 8] = _dr["DlevelName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CheckRepeatCPTabProgram(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 24),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_c.Request["cp1id"]) ? _c.Request["cp1id"] : null)),
            new SqlParameter("@dlevel", _c.Request["dlevel"]),
            new SqlParameter("@faculty", _c.Request["faculty"]),
            new SqlParameter("@program", _c.Request["programcode"]),
            new SqlParameter("@major", _c.Request["majorcode"]),
            new SqlParameter("@groupnum", _c.Request["groupnum"])
        );
        
        _recordCount = _ds.Tables[0].Rows.Count;

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPTabCalDate(string _cpid) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 2),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cpid) ? _cpid : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 3];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["CalDateCondition"].ToString();
            _data[_i, 2] = _dr["PenaltyFormula"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPTabInterest(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 3),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 4];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["InContractInterest"].ToString();
            _data[_i, 2] = _dr["OutContractInterest"].ToString();
            _data[_i, 3] = _dr["UseContractInterest"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListSearchUseContractInterest() {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 3),
            new SqlParameter("@usecontractinterest", "1")
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 2];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["InContractInterest"].ToString();
            _data[_i, 1] = _dr["OutContractInterest"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPTabPayBreakContract(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 4),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 16];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["FacultyCode"].ToString();
            _data[_i, 2] = _dr["FactTName"].ToString();
            _data[_i, 3] = _dr["ProgramCode"].ToString();
            _data[_i, 4] = _dr["ProgTName"].ToString();
            _data[_i, 5] = _dr["MajorCode"].ToString();
            _data[_i, 6] = _dr["GroupNum"].ToString();
            _data[_i, 7] = _dr["AmountCash"].ToString();
            _data[_i, 8] = _dr["Dlevel"].ToString();
            _data[_i, 9] = _dr["DlevelName"].ToString();
            _data[_i, 10] = _dr["CaseGraduate"].ToString();
            _data[_i, 11] = _dr["CaseGraduateName"].ToString();
            _data[_i, 12] = _dr["IDCalDate"].ToString();
            _data[_i, 13] = _dr["CalDateCondition"].ToString();
            _data[_i, 14] = _dr["PenaltyFormula"].ToString();
            _data[_i, 15] = _dr["AmtIndemnitorYear"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CheckRepeatCPTabPayBreakContract(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 7),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_c.Request["cp1id"]) ? _c.Request["cp1id"] : null)),
            new SqlParameter("@casegraduate", _c.Request["casegraduate"]),
            new SqlParameter("@dlevel", _c.Request["dlevel"]),
            new SqlParameter("@faculty", _c.Request["faculty"]),
            new SqlParameter("@program", _c.Request["programcode"]),
            new SqlParameter("@major", _c.Request["majorcode"]),
            new SqlParameter("@groupnum", _c.Request["groupnum"])
        );
        
        _recordCount = _ds.Tables[0].Rows.Count;

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListSearchCPTabPayBreakContract(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 7),
            new SqlParameter("@casegraduate", _c.Request["casegraduate"]),
            new SqlParameter("@dlevel", _c.Request["dlevel"]),
            new SqlParameter("@faculty", _c.Request["faculty"]),
            new SqlParameter("@program", _c.Request["programcode"]),
            new SqlParameter("@major", _c.Request["majorcode"]),
            new SqlParameter("@groupnum", _c.Request["groupnum"])
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 4];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["CalDateCondition"].ToString();
            _data[_i, 2] = _dr["AmtIndemnitorYear"].ToString();
            _data[_i, 3] = _dr["AmountCash"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPTabScholarship(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 14),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 10];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["FacultyCode"].ToString();
            _data[_i, 2] = _dr["FactTName"].ToString();
            _data[_i, 3] = _dr["ProgramCode"].ToString();
            _data[_i, 4] = _dr["ProgTName"].ToString();
            _data[_i, 5] = _dr["MajorCode"].ToString();
            _data[_i, 6] = _dr["GroupNum"].ToString();
            _data[_i, 7] = _dr["ScholarshipMoney"].ToString();
            _data[_i, 8] = _dr["Dlevel"].ToString();
            _data[_i, 9] = _dr["DlevelName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CheckRepeatCPTabScholarship(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 15),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_c.Request["cp1id"]) ? _c.Request["cp1id"] : null)),
            new SqlParameter("@dlevel", _c.Request["dlevel"]),
            new SqlParameter("@faculty", _c.Request["faculty"]),
            new SqlParameter("@program", _c.Request["programcode"]),
            new SqlParameter("@major", _c.Request["majorcode"]),
            new SqlParameter("@groupnum", _c.Request["groupnum"])
        );

        _recordCount = _ds.Tables[0].Rows.Count;

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListSearchCPTabScholarship(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 15),
            new SqlParameter("@dlevel", _c.Request["dlevel"]),
            new SqlParameter("@faculty", _c.Request["faculty"]),
            new SqlParameter("@program", _c.Request["programcode"]),
            new SqlParameter("@major", _c.Request["majorcode"]),
            new SqlParameter("@groupnum", _c.Request["groupnum"])
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 2];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["ScholarshipMoney"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListTitlename() {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 27)
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 4];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["TitleCode"].ToString();
            _data[_i, 1] = _dr["TitleEName"].ToString();
            _data[_i, 2] = _dr["TitleTName"].ToString();
            _data[_i, 3] = _dr["Sex"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListFaculty(bool _cpTabProgram) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", (_cpTabProgram.Equals(false) ? 5 : 25))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 2];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["FacultyCode"].ToString();
            _data[_i, 1] = _dr["FactTName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListProgram(
        bool _cpTabProgram,
        string _dlevel,
        string _faculty
    ) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", (_cpTabProgram.Equals(false) ? 6 : 26)),
            new SqlParameter("@dlevel", (!String.IsNullOrEmpty(_faculty) ? (!String.IsNullOrEmpty(_dlevel) ? _dlevel : null) : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_faculty) ? _faculty : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 6];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ProgramCode"].ToString();
            _data[_i, 1] = _dr["MajorCode"].ToString();
            _data[_i, 2] = _dr["GroupNum"].ToString();
            _data[_i, 3] = _dr["ProgTName"].ToString();
            _data[_i, 4] = _dr["DLevel"].ToString();
            _data[_i, 5] = _dr["DLevelName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListProvince() {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 11)
        );
        
        string[,] _data = new string[_ds.Tables[0].Rows.Count, 2];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ProvinceID"].ToString();
            _data[_i, 1] = _dr["ProvinceTName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }
    
    public static int CountStudent(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountStudent"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListStudent(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 15];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["StudentID"].ToString();
            _data[_i, 2] = _dr["TitleCode"].ToString();
            _data[_i, 3] = _dr["TitleEName"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["ThaiFName"].ToString();
            _data[_i, 6] = _dr["ThaiLName"].ToString();
            _data[_i, 7] = _dr["FacultyCode"].ToString();
            _data[_i, 8] = _dr["FactTName"].ToString();
            _data[_i, 9] = _dr["ProgramCode"].ToString();
            _data[_i, 10] = _dr["ProgTName"].ToString();
            _data[_i, 11] = _dr["MajorCode"].ToString();
            _data[_i, 12] = _dr["GroupNum"].ToString();
            _data[_i, 13] = _dr["DLevel"].ToString();
            _data[_i, 14] = _dr["DLevelName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListProfileStudent(string _studentid) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 8),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_studentid) ? _studentid : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 26];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["StudentID"].ToString();
            _data[_i, 2] = _dr["TitleCode"].ToString();
            _data[_i, 3] = _dr["TitleEName"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstName"].ToString();
            _data[_i, 6] = _dr["LastName"].ToString();
            _data[_i, 7] = _dr["ThaiFName"].ToString();
            _data[_i, 8] = _dr["ThaiLName"].ToString();
            _data[_i, 9] = _dr["FacultyCode"].ToString();
            _data[_i, 10] = _dr["FactTName"].ToString();
            _data[_i, 11] = _dr["ProgramCode"].ToString();
            _data[_i, 12] = _dr["ProgTName"].ToString();
            _data[_i, 13] = _dr["MajorCode"].ToString();
            _data[_i, 14] = _dr["GroupNum"].ToString();
            _data[_i, 15] = _dr["DLevel"].ToString();
            _data[_i, 16] = _dr["DLevelName"].ToString();
            _data[_i, 17] = _dr["AdmissionDate"].ToString();
            _data[_i, 18] = _dr["GraduateDate"].ToString();
            _data[_i, 19] = _dr["ContractDate"].ToString();
            _data[_i, 20] = _dr["ContractDateAgreement"].ToString();
            _data[_i, 21] = _dr["GuarantorTitleTName"].ToString();
            _data[_i, 22] = _dr["GuarantorFirstName"].ToString();
            _data[_i, 23] = _dr["GuarantorLastName"].ToString();            
        }

        foreach (DataRow _dr in _ds.Tables[2].Rows) {
            _data[_i, 24] = _dr["FileName"].ToString();
            _data[_i, 25] = _dr["FolderName"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListSearchStudentCPTransBreakContract(string _studentid) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 9),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_studentid) ? _studentid : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 2];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["StudentID"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPTransBreakContract(HttpContext _c) {
        int _section;
        int _recordCount = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", _section),
            new SqlParameter("@statussend", (!String.IsNullOrEmpty(_c.Request["statussend"]) ? _c.Request["statussend"] : null)),
            new SqlParameter("@statusreceiver", (!String.IsNullOrEmpty(_c.Request["statusreceiver"]) ? _c.Request["statusreceiver"] : null)),
            new SqlParameter("@statusedit", (!String.IsNullOrEmpty(_c.Request["statusedit"]) ? _c.Request["statusedit"] : null)),
            new SqlParameter("@statuscancel", (!String.IsNullOrEmpty(_c.Request["statuscancel"]) ? _c.Request["statuscancel"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountCPTransBreakContract"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPTransBreakContract(HttpContext _c) {
        int _section;
        int _i = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@section", _section),
            new SqlParameter("@statussend", (!String.IsNullOrEmpty(_c.Request["statussend"]) ? _c.Request["statussend"] : null)),
            new SqlParameter("@statusreceiver", (!String.IsNullOrEmpty(_c.Request["statusreceiver"]) ? _c.Request["statusreceiver"] : null)),
            new SqlParameter("@statusedit", (!String.IsNullOrEmpty(_c.Request["statusedit"]) ? _c.Request["statusedit"] : null)),
            new SqlParameter("@statuscancel", (!String.IsNullOrEmpty(_c.Request["statuscancel"]) ? _c.Request["statuscancel"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 16];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["StudentID"].ToString();
            _data[_i, 3] = _dr["TitleTName"].ToString();
            _data[_i, 4] = _dr["FirstTName"].ToString();
            _data[_i, 5] = _dr["LastTName"].ToString();
            _data[_i, 6] = _dr["ProgramCode"].ToString();
            _data[_i, 7] = _dr["ProgramName"].ToString();
            _data[_i, 8] = _dr["GroupNum"].ToString();
            _data[_i, 9] = _dr["StatusSend"].ToString();
            _data[_i, 10] = _dr["StatusReceiver"].ToString();
            _data[_i, 11] = _dr["StatusEdit"].ToString();
            _data[_i, 12] = _dr["StatusCancel"].ToString();
            _data[_i, 13] = Util.ConvertDateTH(_dr["DateTimeCreate"].ToString());
            _data[_i, 14] = Util.ConvertDateTH(_dr["DateTimeSend"].ToString());
            _data[_i, 15] = eCPUtil._actionTrackingStatus[_section - 1, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string ChkTrackingStatusCPTransBreakContract(string _cp1id) {
        string _trackingStatus = String.Empty;
        int _section, _pid;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);
        _pid = int.Parse(_eCPCookie["Pid"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", _section),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        foreach (DataRow _dr in _ds.Tables[1].Rows)
            _trackingStatus = _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString();

        _ds.Dispose();

        return _trackingStatus;
    }

    public static string[,] ListDetailCPTransBreakContract(string _cp1id) {
        int _section;
        int _i = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 10),
            new SqlParameter("@section", _section),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 52];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["StudentID"].ToString();
            _data[_i, 3] = _dr["TitleCode"].ToString();
            _data[_i, 4] = _dr["TitleEName"].ToString();
            _data[_i, 5] = _dr["TitleTName"].ToString();
            _data[_i, 6] = _dr["FirstEName"].ToString();
            _data[_i, 7] = _dr["LastEName"].ToString();
            _data[_i, 8] = _dr["FirstTName"].ToString();
            _data[_i, 9] = _dr["LastTName"].ToString();
            _data[_i, 10] = _dr["ProgramCode"].ToString();
            _data[_i, 11] = _dr["ProgramName"].ToString();
            _data[_i, 12] = _dr["MajorCode"].ToString();
            _data[_i, 13] = _dr["FacultyCode"].ToString();
            _data[_i, 14] = _dr["FacultyName"].ToString();
            _data[_i, 15] = _dr["GroupNum"].ToString();
            _data[_i, 16] = _dr["DLevel"].ToString();
            _data[_i, 17] = _dr["DLevelName"].ToString();
            _data[_i, 18] = _dr["PursuantBook"].ToString();
            _data[_i, 19] = _dr["Pursuant"].ToString();
            _data[_i, 20] = _dr["PursuantBookDate"].ToString();
            _data[_i, 21] = _dr["Location"].ToString();
            _data[_i, 22] = _dr["InputDate"].ToString();
            _data[_i, 23] = _dr["StateLocation"].ToString();
            _data[_i, 24] = _dr["StateLocationDate"].ToString();
            _data[_i, 25] = _dr["ContractDate"].ToString();
            _data[_i, 26] = _dr["Guarantor"].ToString();
            _data[_i, 27] = _dr["ScholarFlag"].ToString();
            _data[_i, 28] = _dr["ScholarshipMoney"].ToString();
            _data[_i, 29] = _dr["ScholarshipYear"].ToString();
            _data[_i, 30] = _dr["ScholarshipMonth"].ToString();
            _data[_i, 31] = _dr["EducationDate"].ToString();
            _data[_i, 32] = _dr["GraduateDate"].ToString();
            _data[_i, 33] = _dr["CaseGraduate"].ToString();
            _data[_i, 34] = _dr["CivilFlag"].ToString();
            _data[_i, 35] = _dr["CalDateCondition"].ToString();
            _data[_i, 36] = _dr["IndemnitorYear"].ToString();
            _data[_i, 37] = _dr["IndemnitorCash"].ToString();

            _data1 = ListLastCommentOnCPTransBreakContract(_dr["ID"].ToString(), "E");
            _data[_i, 38] = _data1.GetLength(0) > 0 ? _data1[0, 2] : String.Empty;
            _data[_i, 49] = _data1.GetLength(0) > 0 ? _data1[0, 3] : String.Empty;

            _data1 = ListLastCommentOnCPTransBreakContract(_dr["ID"].ToString(), "C");
            _data[_i, 39] = _data1.GetLength(0) > 0 ? _data1[0, 2] : String.Empty;
            _data[_i, 50] = _data1.GetLength(0) > 0 ? _data1[0, 3] : String.Empty;

            _data[_i, 40] = _dr["StatusSend"].ToString();
            _data[_i, 41] = _dr["StatusReceiver"].ToString();
            _data[_i, 42] = _dr["StatusEdit"].ToString();
            _data[_i, 43] = _dr["StatusCancel"].ToString();            
            _data[_i, 44] = _dr["FileName"].ToString();
            _data[_i, 45] = _dr["FolderName"].ToString();
            _data[_i, 46] = _dr["ContractDateAgreement"].ToString();
            _data[_i, 47] = _dr["ContractForceStartDate"].ToString();
            _data[_i, 48] = _dr["ContractForceEndDate"].ToString();

            _data[_i, 51] = _dr["SetAmtIndemnitorYear"].ToString();
        }

        _ds.Dispose();

        return _data;
    }
    
    public static int CountRepay(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@statusrepay", (!String.IsNullOrEmpty(_c.Request["statusrepay"]) ? _c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!String.IsNullOrEmpty(_c.Request["statusreply"]) ? _c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!String.IsNullOrEmpty(_c.Request["replyresult"]) ? _c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_c.Request["statuspayment"]) ? _c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );
        
        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountRepay"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string SearchRepayStatusDetail(
        string _cp2id,
        string _statusRepay,
        string _statusPayment
    ) { 
        string _repayStatusDetail = String.Empty;
        string _repayStatusDetailOrder = String.Empty;
        string[] _iconRepayStatus = new string[4];

        if (!String.IsNullOrEmpty(_cp2id) && !String.IsNullOrEmpty(_statusRepay) && !String.IsNullOrEmpty(_statusPayment)) {
            if (_statusRepay.Equals("0")) {
                _repayStatusDetailOrder = "0";
                _iconRepayStatus[0] = eCPUtil._iconRepayStatus[0, 1];
                _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 0];
                _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 0];
                _iconRepayStatus[3] = eCPUtil._iconRepayStatus[3, 0];
            }

            if (_statusPayment.Equals("2")) {
                _repayStatusDetailOrder = "7";
                _iconRepayStatus[0] = eCPUtil._iconRepayStatus[0, 0];
                _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 0];
                _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 0];
                _iconRepayStatus[3] = eCPUtil._iconRepayStatus[3, 1];
            }

            if (_statusPayment.Equals("3")) {
                _repayStatusDetailOrder = "8";
                _iconRepayStatus[0] = eCPUtil._iconRepayStatus[0, 0];
                _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 0];
                _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 0];
                _iconRepayStatus[3] = eCPUtil._iconRepayStatus[3, 2];
            }                

            if ((_statusRepay.Equals("1") || _statusRepay.Equals("2")) && (_statusPayment.Equals("1"))) {
                _iconRepayStatus[0] = eCPUtil._iconRepayStatus[0, 0];
                _iconRepayStatus[3] = eCPUtil._iconRepayStatus[3, 0];

                DataSet _ds = ExecuteCommandStoredProcedure(
                    new SqlParameter("@ordertable", 13),
                    new SqlParameter("@cp2id", _cp2id)
                );

                foreach (DataRow _dr in _ds.Tables[0].Rows) {
                    if (_dr["StatusRepay"].ToString().Equals("1")) {
                        if (_dr["StatusReply"].ToString().Equals("1")) {
                            _repayStatusDetailOrder = "1";
                            _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 1];
                        }

                        if (_dr["StatusReply"].ToString().Equals("2") && _dr["ReplyResult"].ToString().Equals("1")) {
                            _repayStatusDetailOrder = "2";
                            _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 2];
                        }

                        if (_dr["StatusReply"].ToString().Equals("2") && _dr["ReplyResult"].ToString().Equals("2")) {
                            _repayStatusDetailOrder = "3";
                            _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 3];
                        }
                    }

                    _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 0];

                    if (_dr["StatusRepay"].ToString().Equals("2")) {
                        if (_dr["StatusReply"].ToString().Equals("1")) {
                            _repayStatusDetailOrder = "4";
                            _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 1];
                        }

                        if (_dr["StatusReply"].ToString().Equals("2") && _dr["ReplyResult"].ToString().Equals("1")) {
                            _repayStatusDetailOrder = "5";
                            _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 2];
                        }

                        if (_dr["StatusReply"].ToString().Equals("2") && _dr["ReplyResult"].ToString().Equals("2")) {
                            _repayStatusDetailOrder = "6";
                            _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 3];
                        }
                    }
                }

                _ds.Dispose();
            }
        }
        else {
            _repayStatusDetailOrder = "0";
            _iconRepayStatus[0] = eCPUtil._iconRepayStatus[0, 0];
            _iconRepayStatus[1] = eCPUtil._iconRepayStatus[1, 0];
            _iconRepayStatus[2] = eCPUtil._iconRepayStatus[2, 0];
            _iconRepayStatus[3] = eCPUtil._iconRepayStatus[3, 0];
        }     

        _repayStatusDetail = _repayStatusDetailOrder + ";" + _iconRepayStatus[0] + ";" + _iconRepayStatus[1] + ";" + _iconRepayStatus[2] + ";" + _iconRepayStatus[3];

        return _repayStatusDetail;
    }

    public static string[,] ListRepay(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@statusrepay", (!String.IsNullOrEmpty(_c.Request["statusrepay"]) ? _c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!String.IsNullOrEmpty(_c.Request["statusreply"]) ? _c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!String.IsNullOrEmpty(_c.Request["replyresult"]) ? _c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_c.Request["statuspayment"]) ? _c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 19];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = _dr["StatusRepay"].ToString();
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = SearchRepayStatusDetail(_dr["ID"].ToString(), _dr["StatusRepay"].ToString(), _dr["StatusPayment"].ToString());
            _data[_i, 17] = Util.ConvertDateTH(_dr["DateTimeReceiver"].ToString());
            _data[_i, 18] = eCPUtil._actionTrackingStatus[1, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListRepay1(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 53),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@statusrepay", (!String.IsNullOrEmpty(_c.Request["statusrepay"]) ? _c.Request["statusrepay"] : null)),
            new SqlParameter("@statusreply", (!String.IsNullOrEmpty(_c.Request["statusreply"]) ? _c.Request["statusreply"] : null)),
            new SqlParameter("@replyresult", (!String.IsNullOrEmpty(_c.Request["replyresult"]) ? _c.Request["replyresult"] : null)),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_c.Request["statuspayment"]) ? _c.Request["statuspayment"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 22];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = _dr["StatusRepay"].ToString();
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = SearchRepayStatusDetail(_dr["ID"].ToString(), _dr["StatusRepay"].ToString(), _dr["StatusPayment"].ToString());
            _data[_i, 17] = Util.ConvertDateTH(_dr["DateTimeReceiver"].ToString());
            _data[_i, 18] = eCPUtil._actionTrackingStatus[1, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];
            _data[_i, 19] = _dr["StatusReply"].ToString();
            _data[_i, 20] = _dr["ReplyResult"].ToString();
            _data[_i, 21] = _dr["ReplyDate"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailCPTransRequireContract(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 78];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["IndemnitorAddress"].ToString();
            _data[_i, 4] = _dr["ProvinceID"].ToString();
            _data[_i, 5] = _dr["ProvinceTName"].ToString();
            _data[_i, 6] = _dr["RequireDate"].ToString();
            _data[_i, 7] = _dr["ApproveDate"].ToString();
            _data[_i, 8] = _dr["ActualMonthScholarship"].ToString();
            _data[_i, 9] = _dr["ActualScholarship"].ToString();
            _data[_i, 10] = _dr["TotalPayScholarship"].ToString();
            _data[_i, 11] = _dr["ActualMonth"].ToString();
            _data[_i, 12] = _dr["ActualDay"].ToString();
            _data[_i, 13] = _dr["AllActualDate"].ToString();
            _data[_i, 14] = _dr["ActualDate"].ToString();
            _data[_i, 15] = _dr["RemainDate"].ToString();
            _data[_i, 16] = _dr["SubtotalPenalty"].ToString();
            _data[_i, 17] = _dr["TotalPenalty"].ToString();
            _data[_i, 18] = _dr["StatusRepay"].ToString();
            _data[_i, 19] = _dr["StudentID"].ToString();
            _data[_i, 20] = _dr["TitleTName"].ToString();
            _data[_i, 21] = _dr["FirstTName"].ToString();
            _data[_i, 22] = _dr["LastTName"].ToString();
            _data[_i, 23] = _dr["ProgramCode"].ToString();
            _data[_i, 24] = _dr["ProgramName"].ToString();
            _data[_i, 25] = _dr["MajorCode"].ToString();
            _data[_i, 26] = _dr["FacultyCode"].ToString();
            _data[_i, 27] = _dr["FacultyName"].ToString();
            _data[_i, 28] = _dr["GroupNum"].ToString();
            _data[_i, 29] = _dr["DLevel"].ToString();
            _data[_i, 30] = _dr["DLevelName"].ToString();
            _data[_i, 31] = _dr["PursuantBook"].ToString();
            _data[_i, 32] = _dr["Pursuant"].ToString();
            _data[_i, 33] = _dr["PursuantBookDate"].ToString();
            _data[_i, 34] = _dr["Location"].ToString();
            _data[_i, 35] = _dr["InputDate"].ToString();
            _data[_i, 36] = _dr["StateLocation"].ToString();
            _data[_i, 37] = _dr["StateLocationDate"].ToString();
            _data[_i, 38] = _dr["ContractDate"].ToString();
            _data[_i, 39] = _dr["Guarantor"].ToString();
            _data[_i, 40] = _dr["ScholarFlag"].ToString();
            _data[_i, 41] = _dr["ScholarshipMoney"].ToString();
            _data[_i, 42] = _dr["ScholarshipYear"].ToString();
            _data[_i, 43] = _dr["ScholarshipMonth"].ToString();
            _data[_i, 44] = _dr["EducationDate"].ToString();
            _data[_i, 45] = _dr["GraduateDate"].ToString();
            _data[_i, 46] = _dr["CaseGraduate"].ToString();
            _data[_i, 47] = _dr["CivilFlag"].ToString();
            _data[_i, 48] = _dr["CalDateCondition"].ToString();
            _data[_i, 49] = _dr["IndemnitorYear"].ToString();
            _data[_i, 50] = _dr["IndemnitorCash"].ToString();
            _data[_i, 51] = String.Empty;
            _data[_i, 52] = _dr["StatusSend"].ToString();
            _data[_i, 53] = _dr["StatusReceiver"].ToString();
            _data[_i, 54] = _dr["StatusEdit"].ToString();
            _data[_i, 55] = _dr["StatusCancel"].ToString();
            _data[_i, 56] = _dr["FileName"].ToString();
            _data[_i, 57] = _dr["FolderName"].ToString();
            _data[_i, 58] = _dr["StatusPayment"].ToString();

            _data1 = ListLastCommentOnCPTransBreakContract(_dr["BCID"].ToString(), "E");
            _data[_i, 59] = _data1.GetLength(0) > 0 ? _data1[0, 2] : String.Empty;
            _data[_i, 64] = _data1.GetLength(0) > 0 ? _data1[0, 3] : String.Empty;

            _data1 = ListLastCommentOnCPTransBreakContract(_dr["BCID"].ToString(), "C");
            _data[_i, 60] = _data1.GetLength(0) > 0 ? _data1[0, 2] : String.Empty;
            _data[_i, 65] = _data1.GetLength(0) > 0 ? _data1[0, 3] : String.Empty;

            _data[_i, 61] = _dr["ContractDateAgreement"].ToString();
            _data[_i, 62] = _dr["ContractForceStartDate"].ToString();
            _data[_i, 63] = _dr["ContractForceEndDate"].ToString();
            _data[_i, 66] = _dr["StudyLeave"].ToString();
            _data[_i, 67] = _dr["BeforeStudyLeaveStartDate"].ToString();
            _data[_i, 68] = _dr["BeforeStudyLeaveEndDate"].ToString();
            _data[_i, 69] = _dr["StudyLeaveStartDate"].ToString();
            _data[_i, 70] = _dr["StudyLeaveEndDate"].ToString();
            _data[_i, 71] = _dr["AfterStudyLeaveStartDate"].ToString();
            _data[_i, 72] = _dr["AfterStudyLeaveEndDate"].ToString();
            _data[_i, 73] = _dr["LawyerFullname"].ToString();
            _data[_i, 74] = _dr["LawyerPhoneNumber"].ToString();
            _data[_i, 75] = _dr["LawyerMobileNumber"].ToString();
            _data[_i, 76] = _dr["LawyerEmail"].ToString();
            _data[_i, 77] = _dr["SetAmtIndemnitorYear"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string ChkRepayStatusCPTransRequireContract(string _cp1id) {
        string _repayStatus = String.Empty;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 12),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        foreach (DataRow _dr in _ds.Tables[1].Rows)
            _repayStatus = _dr["StatusRepay"].ToString();

        _ds.Dispose();

        return _repayStatus;
    }

    public static string[,] ListCPTransRepayContract(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 16),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 14];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["TotalPenalty"].ToString();
            _data[_i, 2] = _dr["StatusRepay"].ToString();
            _data[_i, 3] = _dr["StatusReply"].ToString();
            _data[_i, 4] = _dr["ReplyResult"].ToString();
            _data[_i, 5] = _dr["RepayDate"].ToString();
            _data[_i, 6] = _dr["ReplyDate"].ToString();
            _data[_i, 7] = _dr["StatusPayment"].ToString();
            _data[_i, 8] = String.Empty;
            _data[_i, 9] = String.Empty;
            _data[_i, 10] = String.Empty;
            _data[_i, 11] = _dr["SubtotalPenalty"].ToString();
            
            _data1 = ListMaxReplyDate(_dr["ID"].ToString());

            if (_data1.GetLength(0) > 0) {
                if (_data1[0, 3].Equals("2") && _data1[0, 4].Equals("1"))
                    _data[_i, 8] = _data1[0, 5];

                _data[_i, 9] = _data1[0, 3];
                _data[_i, 10] = _data1[0, 4];
            }
            
            _data[_i, 12] = _dr["Pursuant"].ToString();
            _data[_i, 13] = _dr["PursuantBookDate"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListMaxReplyDate(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 17),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 6];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RCID"].ToString();
            _data[_i, 1] = _dr["StatusRepay"].ToString();
            _data[_i, 2] = _dr["RepayDate"].ToString();
            _data[_i, 3] = _dr["StatusReply"].ToString();
            _data[_i, 4] = _dr["ReplyResult"].ToString();
            _data[_i, 5] = _dr["ReplyDate"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string ChkRepayStatusCalInterestOverpayment(string _cp2id) {
        string[,] _data;
        string _result;
        string[] _repayDate;
        double[] _overpayment;
        
        _data = ListCPTransRepayContract(_cp2id);

        if (_data.GetLength(0) > 0) {
            //Status Payment
            if (!_data[0, 7].Equals("2")) {
                //Status Payment
                if (!_data[0, 7].Equals("3")) {
                    if (_data[0, 9].Equals("2")) {
                        if (_data[0, 10].Equals("1")) {
                            //Reply Date
                            if (!String.IsNullOrEmpty(_data[0, 8])) {
                                _repayDate = eCPUtil.RepayDate(_data[0, 8]);
                                IFormatProvider _provider = new System.Globalization.CultureInfo("th-TH");
                                DateTime _dateA = DateTime.Parse(_repayDate[2], _provider);
                                DateTime _dateB = DateTime.Parse(Util.ConvertDateTH(Util.CurrentDate("yyyy-MM-dd")), _provider);

                                _overpayment = Util.CalcDate(_dateA, _dateB);

                                if (!_overpayment[0].Equals(0)) {
                                    _result = "0";
                                }
                                else
                                    _result = "7";
                            }
                            else
                                _result = "6";
                        }
                        else
                            _result = "5";
                    }
                    else
                        _result = "4";
                }
                else
                    _result = "3";
            }
            else
                _result = "2";
        }
        else
            _result = "1";

        return _result;
    }

    public static string[,] ListCPTransRepayContractNoCurrentStatusRepay(
        string _cp2id,
        string _statusRepay
    ) { 
        int _i = 0;
        string[,] _data = new string[0, 0];

        if (!String.IsNullOrEmpty(_cp2id) && !String.IsNullOrEmpty(_statusRepay)) {
            DataSet _ds = ExecuteCommandStoredProcedure(
                new SqlParameter("@ordertable", 18),
                new SqlParameter("@cp2id", _cp2id),
                new SqlParameter("@statusrepay", _statusRepay)
            );

            _data = new string[_ds.Tables[0].Rows.Count, 8];

            foreach (DataRow _dr in _ds.Tables[0].Rows) {
                _data[_i, 0] = _dr["ID"].ToString();
                _data[_i, 1] = _dr["StatusRepay"].ToString();
                _data[_i, 2] = _dr["StatusReply"].ToString();
                _data[_i, 3] = _dr["ReplyResult"].ToString();
                _data[_i, 4] = _dr["RepayDate"].ToString();
                _data[_i, 5] = _dr["ReplyDate"].ToString();
                _data[_i, 6] = _dr["Pursuant"].ToString();
                _data[_i, 7] = _dr["PursuantBookDate"].ToString();

                _i++;
            }

            _ds.Dispose();
        }

        return _data;
    }

    public static int CountPaymentOnCPTransRequireContract(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_c.Request["statuspayment"]) ? _c.Request["statuspayment"] : null)),
            new SqlParameter("@statuspaymentrecord", (!String.IsNullOrEmpty(_c.Request["statuspaymentrecord"]) ? _c.Request["statuspaymentrecord"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountPayment"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListPaymentOnCPTransRequireContract(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_c.Request["statuspayment"]) ? _c.Request["statuspayment"] : null)),
            new SqlParameter("@statuspaymentrecord", (!String.IsNullOrEmpty(_c.Request["statuspaymentrecord"]) ? _c.Request["statuspaymentrecord"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 28];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = _dr["StatusRepay"].ToString();
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = _dr["FormatPayment"].ToString();
            _data[_i, 17] = _dr["ReplyDate"].ToString();
            _data[_i, 18] = _dr["TotalPayScholarship"].ToString();
            _data[_i, 19] = _dr["SubtotalPenalty"].ToString();
            _data[_i, 20] = _dr["TotalPenalty"].ToString();
            _data[_i, 21] = eCPUtil._actionTrackingStatus[1, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];
            _data[_i, 22] = _dr["ReplyDateHistory"].ToString();
            _data[_i, 23] = (!String.IsNullOrEmpty(_dr["TotalPayCapital"].ToString()) ? _dr["TotalPayCapital"].ToString() : "0");
            _data[_i, 24] = (!String.IsNullOrEmpty(_dr["TotalPayInterest"].ToString()) ? _dr["TotalPayInterest"].ToString() : "0");
            _data[_i, 25] = (!String.IsNullOrEmpty(_dr["TotalPay"].ToString()) ? _dr["TotalPay"].ToString() : "0");
            _data[_i, 26] = (!String.IsNullOrEmpty(_dr["TotalRemain"].ToString()) ? _dr["TotalRemain"].ToString() : "0");
            _data[_i, 27] = (!String.IsNullOrEmpty(_dr["RemainAccruedInterest"].ToString()) ? _dr["RemainAccruedInterest"].ToString() : "0");

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailPaymentOnCPTransRequireContract(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 38];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["TotalPayScholarship"].ToString();
            _data[_i, 4] = _dr["SubtotalPenalty"].ToString();
            _data[_i, 5] = _dr["TotalPenalty"].ToString();
            _data[_i, 6] = _dr["StatusRepay"].ToString();
            _data[_i, 7] = _dr["StatusPayment"].ToString();
            _data[_i, 8] = _dr["FormatPayment"].ToString();
            _data[_i, 9] = _dr["StudentID"].ToString();
            _data[_i, 10] = _dr["TitleTName"].ToString();
            _data[_i, 11] = _dr["FirstTName"].ToString();
            _data[_i, 12] = _dr["LastTName"].ToString();
            _data[_i, 13] = _dr["ProgramCode"].ToString();
            _data[_i, 14] = _dr["ProgramName"].ToString();
            _data[_i, 15] = _dr["MajorCode"].ToString();
            _data[_i, 16] = _dr["FacultyCode"].ToString();
            _data[_i, 17] = _dr["FacultyName"].ToString();
            _data[_i, 18] = _dr["GroupNum"].ToString();
            _data[_i, 19] = _dr["DLevel"].ToString();
            _data[_i, 20] = _dr["DLevelName"].ToString();
            _data[_i, 21] = _dr["FileName"].ToString();
            _data[_i, 22] = _dr["FolderName"].ToString();
            _data[_i, 23] = _dr["StatusReply"].ToString();
            _data[_i, 24] = _dr["ReplyDate"].ToString();
            _data[_i, 25] = String.Empty;
            _data[_i, 26] = String.Empty;
            _data[_i, 27] = String.Empty;

            _data1 = ListLastTransPayment(_dr["ID"].ToString());

            if (_data1.GetLength(0) > 0) {
                _data[_i, 25] = _data1[0, 2];
                _data[_i, 26] = _data1[0, 3];
                _data[_i, 27] = _data1[0, 4];
            }

            _data[_i, 28] = _dr["ContractDate"].ToString();
            _data[_i, 29] = _dr["LawyerFullname"].ToString();
            _data[_i, 30] = _dr["LawyerPhoneNumber"].ToString();
            _data[_i, 31] = _dr["LawyerMobileNumber"].ToString();
            _data[_i, 32] = _dr["LawyerEmail"].ToString();
            _data[_i, 33] = _dr["StatusPaymentRecord"].ToString();
            _data[_i, 34] = _dr["StatusPaymentRecordLawyerFullname"].ToString();
            _data[_i, 35] = _dr["StatusPaymentRecordLawyerPhoneNumber"].ToString();
            _data[_i, 36] = _dr["StatusPaymentRecordLawyerMobileNumber"].ToString();
            _data[_i, 37] = _dr["StatusPaymentRecordLawyerEmail"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListTransPayment(
        string _cp1id,
        string _dateStart,
        string _dateEnd
    ) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 21),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_dateStart) ? _dateStart : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_dateEnd) ? _dateEnd : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 15];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["DateTimePayment"].ToString();
            _data[_i, 4] = _dr["Capital"].ToString();
            _data[_i, 5] = _dr["Interest"].ToString();
            _data[_i, 6] = _dr["AccruedInterest"].ToString();
            _data[_i, 7] = _dr["TotalPayment"].ToString();
            _data[_i, 8] = _dr["PayCapital"].ToString();
            _data[_i, 9] = _dr["PayInterest"].ToString();
            _data[_i, 10] = _dr["TotalPay"].ToString();
            _data[_i, 11] = _dr["RemainCapital"].ToString();
            _data[_i, 12] = _dr["RemainAccruedInterest"].ToString();
            _data[_i, 13] = _dr["TotalRemain"].ToString();
            _data[_i, 14] = _dr["Channel"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailTransPayment(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 21),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 52];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["RCID"].ToString();
            _data[_i, 2] = _dr["CalInterestYesNo"].ToString();
            _data[_i, 3] = _dr["OverpaymentDateStart"].ToString();
            _data[_i, 4] = _dr["OverpaymentDateEnd"].ToString();
            _data[_i, 5] = _dr["OverpaymentYear"].ToString();
            _data[_i, 6] = _dr["OverpaymentDay"].ToString();
            _data[_i, 7] = _dr["OverpaymentInterest"].ToString();
            _data[_i, 8] = _dr["OverpaymentTotalInterest"].ToString();
            _data[_i, 9] = _dr["PayRepayDateStart"].ToString();
            _data[_i, 10] = _dr["PayRepayDateEnd"].ToString();
            _data[_i, 11] = _dr["PayRepayYear"].ToString();
            _data[_i, 12] = _dr["PayRepayDay"].ToString();
            _data[_i, 13] = _dr["PayRepayInterest"].ToString();
            _data[_i, 14] = _dr["PayRepayTotalInterest"].ToString();
            _data[_i, 15] = _dr["DateTimePayment"].ToString();
            _data[_i, 16] = _dr["Capital"].ToString();
            _data[_i, 17] = _dr["Interest"].ToString();
            _data[_i, 18] = _dr["TotalAccruedInterest"].ToString();            
            _data[_i, 19] = _dr["TotalPayment"].ToString();
            _data[_i, 20] = _dr["PayCapital"].ToString();
            _data[_i, 21] = _dr["PayInterest"].ToString();
            _data[_i, 22] = _dr["TotalPay"].ToString();
            _data[_i, 23] = _dr["RemainCapital"].ToString();
            _data[_i, 24] = _dr["AccruedInterest"].ToString();            
            _data[_i, 25] = _dr["RemainAccruedInterest"].ToString();
            _data[_i, 26] = _dr["TotalRemain"].ToString();
            _data[_i, 27] = _dr["Channel"].ToString();
            _data[_i, 28] = _dr["ReceiptNo"].ToString();
            _data[_i, 29] = _dr["ReceiptBookNo"].ToString();
            _data[_i, 30] = _dr["ReceiptDate"].ToString();
            _data[_i, 31] = _dr["ReceiptSendNo"].ToString();
            _data[_i, 32] = _dr["ReceiptFund"].ToString();
            _data[_i, 33] = _dr["ChequeNo"].ToString();
            _data[_i, 34] = _dr["ChequeBank"].ToString();
            _data[_i, 35] = _dr["ChequeBankBranch"].ToString();
            _data[_i, 36] = _dr["ChequeDate"].ToString();
            _data[_i, 37] = _dr["CashBank"].ToString();
            _data[_i, 38] = _dr["CashBankBranch"].ToString();
            _data[_i, 39] = _dr["CashBankAccount"].ToString();
            _data[_i, 40] = _dr["CashBankAccountNo"].ToString();
            _data[_i, 41] = _dr["CashBankDate"].ToString();
            _data[_i, 42] = _dr["ReceiptCopy"].ToString();
            _data[_i, 43] = _dr["FormatPayment"].ToString();
            _data[_i, 43] = _dr["FormatPayment"].ToString();
            _data[_i, 44] = _dr["SubtotalPenalty"].ToString();
            _data[_i, 45] = _dr["TotalPenalty"].ToString();
            _data[_i, 46] = _dr["OverpaymentTotalInterestBefore"].ToString();
            _data[_i, 47] = _dr["Overpay"].ToString();
            _data[_i, 48] = _dr["LawyerFullname"].ToString();
            _data[_i, 49] = _dr["LawyerPhoneNumber"].ToString();
            _data[_i, 50] = _dr["LawyerMobileNumber"].ToString();
            _data[_i, 51] = _dr["LawyerEmail"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListLastTransPayment(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 22),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 5];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["RCID"].ToString();
            _data[_i, 2] = _dr["DateTimePayment"].ToString();
            _data[_i, 3] = _dr["RemainAccruedInterest"].ToString();
            _data[_i, 4] = _dr["TotalRemain"].ToString();
        }

        _ds.Dispose();

        return _data;
    }
    
    public static string ListCPTransProsecution(string _cp2id) {
        JArray jsonArray = new JArray();

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 54),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = ListLastTransPayment(_cp2id);
        string _dateTimePayment = String.Empty;
        string _remainAccruedInterest = String.Empty;
        string _totalRemain = String.Empty;

        if (_data.GetLength(0) > 0) {
            _dateTimePayment = _data[0, 2];
            _remainAccruedInterest = _data[0, 3];
            _totalRemain = _data[0, 4];
        }

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            jsonArray.Add(new JObject() {
                {
                    "eCPTransRequireContract", new JObject() {
                        { "ID", _dr["ID"].ToString() },
                        { "BCID", _dr["BCID"].ToString() },
                        { "subtotalPenalty", _dr["SubtotalPenalty"].ToString() },
                        { "totalPenalty", _dr["TotalPenalty"].ToString() },
                        { "statusPayment", _dr["StatusPayment"].ToString() },
                        {
                            "lawyer", new JObject() {
                                { "fullName", _dr["LawyerFullname"].ToString() },
                                { "phoneNumber", _dr["LawyerPhoneNumber"].ToString() },
                                { "mobileNumber", _dr["LawyerMobileNumber"].ToString() },
                                { "email", _dr["LawyerEmail"].ToString() }
                            }
                        }
                    }
                },
                {
                    "eCPTransProsecution", new JObject() {
                        { "RCID", _dr["RCID"].ToString() },
                        {
                            "complaint", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", _dr["ComplaintLawyerFullname"].ToString() },
                                        { "phoneNumber", _dr["ComplaintLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", _dr["ComplaintLawyerMobileNumber"].ToString() },
                                        { "email", _dr["ComplaintLawyerEmail"].ToString() }
                                    }
                                },
                                { "blackCaseNo", _dr["ComplaintBlackCaseNo"].ToString() },
                                { "capital", _dr["ComplaintCapital"].ToString() },
                                { "interest", _dr["ComplaintInterest"].ToString() },
                                { "actionDate", eCPUtil.ThaiLongDate(_dr["ComplaintActionDate"].ToString()) }
                            }
                        },
                        {
                            "judgment", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", _dr["JudgmentLawyerFullname"].ToString() },
                                        { "phoneNumber", _dr["JudgmentLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", _dr["JudgmentLawyerMobileNumber"].ToString() },
                                        { "email", _dr["JudgmentLawyerEmail"].ToString() }
                                    }
                                },
                                { "redCaseNo", _dr["JudgmentRedCaseNo"].ToString() },
                                { "verdict", _dr["JudgmentVerdict"].ToString() },
                                { "copy", (!String.IsNullOrEmpty(_dr["JudgmentCopy"].ToString()) ? eCPUtil.DecodeFromBase64(_dr["JudgmentCopy"].ToString()) : _dr["JudgmentCopy"].ToString()) },
                                { "remark", _dr["JudgmentRemark"].ToString() },
                                { "actionDate", eCPUtil.ThaiLongDate(_dr["JudgmentActionDate"].ToString()) }
                            }
                        },
                        {
                            "execution", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", _dr["ExecutionLawyerFullname"].ToString() },
                                        { "phoneNumber", _dr["ExecutionLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", _dr["ExecutionLawyerMobileNumber"].ToString() },
                                        { "email", _dr["ExecutionLawyerEmail"].ToString() }
                                    }
                                },
                                { "date", _dr["ExecutionDate"].ToString() },
                                { "copy", (!String.IsNullOrEmpty(_dr["ExecutionCopy"].ToString()) ? eCPUtil.DecodeFromBase64(_dr["ExecutionCopy"].ToString()) : _dr["ExecutionCopy"].ToString()) },
                                { "actionDate", eCPUtil.ThaiLongDate(_dr["ExecutionActionDate"].ToString()) }
                            }
                        },
                        {
                            "executionWithdraw", new JObject() {
                                {
                                    "lawyer", new JObject() {
                                        { "fullName", _dr["ExecutionWithdrawLawyerFullname"].ToString() },
                                        { "phoneNumber", _dr["ExecutionWithdrawLawyerPhoneNumber"].ToString() },
                                        { "mobileNumber", _dr["ExecutionWithdrawLawyerMobileNumber"].ToString() },
                                        { "email", _dr["ExecutionWithdrawLawyerEmail"].ToString() }
                                    }
                                },
                                { "date", _dr["ExecutionWithdrawDate"].ToString() },
                                { "reason", _dr["ExecutionWithdrawReason"].ToString() },
                                { "copy", (!String.IsNullOrEmpty(_dr["ExecutionWithdrawCopy"].ToString()) ? eCPUtil.DecodeFromBase64(_dr["ExecutionWithdrawCopy"].ToString()) : _dr["ExecutionWithdrawCopy"].ToString()) },
                                { "actionDate", eCPUtil.ThaiLongDate(_dr["ExecutionWithdrawActionDate"].ToString()) }
                            }
                        }
                    }
                },
                {
                    "eCPTransPayment", new JObject() {
                        {
                            "lastPayment", new JObject() {
                                { "dateTimePayment", _dateTimePayment },
                                { "remainAccruedInterest", _remainAccruedInterest },
                                { "totalRemain", _totalRemain }
                            }
                        }
                    }
                }
            });
        }

        _ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static string GetPersonRecordsAddress(string _studentCode) {
        JArray jsonArray = new JArray();

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 55),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_studentCode) ? _studentCode : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            jsonArray.Add(new JObject() {
                { "perPersonID", _dr["id"].ToString() },
                { "idCard", _dr["idCard"].ToString() },
                {
                    "addressTypePermanent", new JObject() {
                        { "address", _dr["addressPermanent"].ToString() },
                        { "country", _dr["thCountryNamePermanent"].ToString() },
                        { "village", _dr["villagePermanent"].ToString() },
                        { "no", _dr["noPermanent"].ToString() },
                        { "moo", _dr["mooPermanent"].ToString() },
                        { "soi", _dr["soiPermanent"].ToString() },
                        { "road", _dr["roadPermanent"].ToString() },
                        { "subdistrict", _dr["thSubdistrictNamePermanent"].ToString() },
                        { "district", _dr["thDistrictNamePermanent"].ToString() },
                        { "province", _dr["thPlaceNamePermanent"].ToString() },
                        { "zipCode", _dr["zipCodePermanent"].ToString() },
                        { "phoneNumber", _dr["phoneNumberPermanent"].ToString() },
                        { "mobileNumber", _dr["mobileNumberPermanent"].ToString() }
                    }
                },
                {
                    "addressTypeCurrent", new JObject() {
                        { "address", _dr["addressCurrent"].ToString() },
                        { "country", _dr["thCountryNameCurrent"].ToString() },
                        { "village", _dr["villageCurrent"].ToString() },
                        { "no", _dr["noCurrent"].ToString() },
                        { "moo", _dr["mooCurrent"].ToString() },
                        { "soi", _dr["soiCurrent"].ToString() },
                        { "road", _dr["roadCurrent"].ToString() },
                        { "subdistrict", _dr["thSubdistrictNameCurrent"].ToString() },
                        { "district", _dr["thDistrictNameCurrent"].ToString() },
                        { "province", _dr["thPlaceNameCurrent"].ToString() },
                        { "zipCode", _dr["zipCodeCurrent"].ToString() },
                        { "phoneNumber", _dr["phoneNumberCurrent"].ToString() },
                        { "mobileNumber", _dr["mobileNumberCurrent"].ToString() }
                    }
                }
            });
        }

        _ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static string[,] ListLastCommentOnCPTransBreakContract(
        string _cpid,
        string _action
    ) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 20),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cpid) ? _cpid : null)),
            new SqlParameter("@actioncomment", (!String.IsNullOrEmpty(_action) ? _action : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 4];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["ID"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["Comment"].ToString();
            _data[_i, 3] = _dr["DateTimeReject"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportTableCalCapitalAndInterest(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportTableCalCapitalAndInterest"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPReportTableCalCapitalAndInterest(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 20];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = _dr["StatusRepay"].ToString();
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = _dr["FormatPayment"].ToString();
            _data[_i, 17] = _dr["TotalPenalty"].ToString();
            _data[_i, 18] = String.Empty;
            _data[_i, 19] = String.Empty;

            _data1 = ListSumPayOnPayment(_dr["ID"].ToString());

            if (_data1.GetLength(0) > 0)
                _data[_i, 18] = _data1[0, 1];

            _data1 = ListLastTransPayment(_dr["ID"].ToString());

            if (_data1.GetLength(0) > 0)
                _data[_i, 19] = _data1[0, 4];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListSumPayOnPayment(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 29),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 4];
 
        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RCID"].ToString();
            _data[_i, 1] = _dr["SumPayCapital"].ToString();
            _data[_i, 2] = _dr["SumPayInterest"].ToString();
            _data[_i, 3] = _dr["SumTotalPay"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailCPReportTableCalCapitalAndInterest(string _cp2id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 28),
            new SqlParameter("@cp2id", (!String.IsNullOrEmpty(_cp2id) ? _cp2id : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 26];
        string[,] _data1;

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["ID"].ToString();
            _data[_i, 2] = _dr["BCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["MajorCode"].ToString();
            _data[_i, 10] = _dr["FacultyCode"].ToString();
            _data[_i, 11] = _dr["FacultyName"].ToString();
            _data[_i, 12] = _dr["GroupNum"].ToString();
            _data[_i, 13] = _dr["DLevel"].ToString();
            _data[_i, 14] = _dr["DLevelName"].ToString();
            _data[_i, 15] = _dr["FileName"].ToString();
            _data[_i, 16] = _dr["FolderName"].ToString();
            _data[_i, 17] = _dr["StatusRepay"].ToString();
            _data[_i, 18] = _dr["StatusPayment"].ToString();
            _data[_i, 19] = _dr["FormatPayment"].ToString();
            _data[_i, 20] = _dr["TotalPenalty"].ToString();
            _data[_i, 21] = String.Empty;

            _data1 = ListLastTransPayment(_dr["ID"].ToString());

            if (_data1.GetLength(0) > 0)
                _data[_i, 21] = _data1[0, 4];

            _data[_i, 22] = _dr["LawyerFullname"].ToString();
            _data[_i, 23] = _dr["LawyerPhoneNumber"].ToString();
            _data[_i, 24] = _dr["LawyerMobileNumber"].ToString();
            _data[_i, 25] = _dr["LawyerEmail"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCalCPReportTableCalCapitalAndInterest(
        string _capital,
        string _interest,
        string _pay,
        string _paymentDate
    ) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 30),
            new SqlParameter("@capital", (!String.IsNullOrEmpty(_capital) ? _capital : null)),
            new SqlParameter("@interest", (!String.IsNullOrEmpty(_interest) ? _interest : null)),
            new SqlParameter("@pay", (!String.IsNullOrEmpty(_pay) ? _pay : null)),
            new SqlParameter("@paiddate", (!String.IsNullOrEmpty(_paymentDate) ? _paymentDate : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count + 1, 9];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["PaidPeriod"].ToString();
            _data[_i, 1] = _dr["Capital"].ToString();
            _data[_i, 2] = _dr["Paid"].ToString();
            _data[_i, 3] = _dr["Interest"].ToString();
            _data[_i, 4] = _dr["PayTotal"].ToString();
            _data[_i, 5] = _dr["PaidDate"].ToString();

            _i++;
        }

        foreach (DataRow _dr1 in _ds.Tables[1].Rows) {
            _data[_i, 6] = _dr1["SumPaid"].ToString();
            _data[_i, 7] = _dr1["SumInterest"].ToString();
            _data[_i, 8] = _dr1["SumPayTotal"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListSumCalCPReportTableCalCapitalAndInterest(
        string _capital,
        string _interest,
        string _pay,
        string _paymentDate
    ) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 30),
            new SqlParameter("@capital", (!String.IsNullOrEmpty(_capital) ? _capital : null)),
            new SqlParameter("@interest", (!String.IsNullOrEmpty(_interest) ? _interest : null)),
            new SqlParameter("@pay", (!String.IsNullOrEmpty(_pay) ? _pay : null)),
            new SqlParameter("@paiddate", (!String.IsNullOrEmpty(_paymentDate) ? _paymentDate : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 6];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["PaidPeriod"].ToString();
            _data[_i, 1] = _dr["Capital"].ToString();
            _data[_i, 2] = _dr["Paid"].ToString();
            _data[_i, 3] = _dr["Interest"].ToString();
            _data[_i, 4] = _dr["PayTotal"].ToString();
            _data[_i, 5] = _dr["PaidDate"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportStepOfWork(HttpContext _c) {
        int _section;
        int _recordCount = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@section", _section),
            new SqlParameter("@statusstepofwork", (!String.IsNullOrEmpty(_c.Request["statusstepofwork"]) ? _c.Request["statusstepofwork"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportStepOfWork"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPReportStepOfWork(HttpContext _c) {
        int _section;
        int _i = 0;

        HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
        _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
        _section = int.Parse(_eCPCookie["UserSection"]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@section", _section),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@statusstepofwork", (!String.IsNullOrEmpty(_c.Request["statusstepofwork"]) ? _c.Request["statusstepofwork"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 17];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = SearchRepayStatusDetail(_dr["RCID"].ToString(), _dr["StatusRepay"].ToString(), _dr["StatusPayment"].ToString());
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = eCPUtil._actionTrackingStatus[2, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountReportStepOfWorkOnStatisticRepayByProgram(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportStepOfWork"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListReportStepOfWorkOnStatisticRepayByProgram(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 31),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 17];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["StatusSend"].ToString();
            _data[_i, 11] = _dr["StatusReceiver"].ToString();
            _data[_i, 12] = _dr["StatusEdit"].ToString();
            _data[_i, 13] = _dr["StatusCancel"].ToString();
            _data[_i, 14] = SearchRepayStatusDetail(_dr["RCID"].ToString(), _dr["StatusRepay"].ToString(), _dr["StatusPayment"].ToString());
            _data[_i, 15] = _dr["StatusPayment"].ToString();
            _data[_i, 16] = eCPUtil._actionTrackingStatus[2, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPReportStatisticRepay() {
        int _i = 0;
        double _remain = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 32)
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 10];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["AcadamicYear"].ToString();
            _data[_i, 2] = !String.IsNullOrEmpty(_dr["CountProgram"].ToString()) ? _dr["CountProgram"].ToString() : "0";
            _data[_i, 3] = !String.IsNullOrEmpty(_dr["CountStudent"].ToString()) ? _dr["CountStudent"].ToString() : "0";
            _data[_i, 4] = !String.IsNullOrEmpty(_dr["CountStudentNoPayment"].ToString()) ? _dr["CountStudentNoPayment"].ToString() : "0";
            _data[_i, 5] = !String.IsNullOrEmpty(_dr["CountStudentPaymentComplete"].ToString()) ? _dr["CountStudentPaymentComplete"].ToString() : "0";
            _data[_i, 6] = !String.IsNullOrEmpty(_dr["CountStudentPaymentIncomplete"].ToString()) ? _dr["CountStudentPaymentIncomplete"].ToString() : "0";
            _data[_i, 7] = !String.IsNullOrEmpty(_dr["SumTotalPenalty"].ToString()) ? _dr["SumTotalPenalty"].ToString() : "0";
            _data[_i, 8] = !String.IsNullOrEmpty(_dr["SumTotalPay"].ToString()) ? _dr["SumTotalPay"].ToString() : "0";

            _remain = (double.Parse(_data[_i, 7]) - double.Parse(_data[_i, 8]));

            _data[_i, 9] = _remain > 0 ? _remain.ToString() : "0";

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPReportStatisticRepayByProgram(string _acadamicyear) {
        int _i = 0;
        double _remain = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 33),
            new SqlParameter("@acadamicyear", _acadamicyear)
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 15];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["AcadamicYear"].ToString();
            _data[_i, 2] = _dr["FacultyCode"].ToString();
            _data[_i, 3] = _dr["FacultyName"].ToString();
            _data[_i, 4] = _dr["ProgramCode"].ToString();
            _data[_i, 5] = _dr["ProgramName"].ToString();
            _data[_i, 6] = _dr["MajorCode"].ToString();
            _data[_i, 7] = _dr["GroupNum"].ToString();
            _data[_i, 8] = !String.IsNullOrEmpty(_dr["CountStudent"].ToString()) ? _dr["CountStudent"].ToString() : "0";
            _data[_i, 9] = !String.IsNullOrEmpty(_dr["CountStudentNoPayment"].ToString()) ? _dr["CountStudentNoPayment"].ToString() : "0";
            _data[_i, 10] = !String.IsNullOrEmpty(_dr["CountStudentPaymentComplete"].ToString()) ? _dr["CountStudentPaymentComplete"].ToString() : "0";
            _data[_i, 11] = !String.IsNullOrEmpty(_dr["CountStudentPaymentIncomplete"].ToString()) ? _dr["CountStudentPaymentIncomplete"].ToString() : "0";
            _data[_i, 12] = !String.IsNullOrEmpty(_dr["SumTotalPenalty"].ToString()) ? _dr["SumTotalPenalty"].ToString() : "0";
            _data[_i, 13] = !String.IsNullOrEmpty(_dr["SumTotalPay"].ToString()) ? _dr["SumTotalPay"].ToString() : "0";

            _remain = (double.Parse(_data[_i, 12]) - double.Parse(_data[_i, 13]));

            _data[_i, 14] = _remain > 0 ? _remain.ToString() : "0";

            _i++;
        }

        _ds.Dispose();

        return _data;
    }
    
    public static string[,] ListCPReportStatisticContract() {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 38)
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 5];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["AcadamicYear"].ToString();
            _data[_i, 2] = !String.IsNullOrEmpty(_dr["CountStudent"].ToString()) ? _dr["CountStudent"].ToString() : "0";
            _data[_i, 3] = !String.IsNullOrEmpty(_dr["CountStudentSignContract"].ToString()) ? _dr["CountStudentSignContract"].ToString() : "0";
            _data[_i, 4] = !String.IsNullOrEmpty(_dr["CountStudentContractPenalty"].ToString()) ? _dr["CountStudentContractPenalty"].ToString() : "0";

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountReportStudentSignContract(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 40),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );
        
        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportStudentSignContract"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListReportStudentSignContract(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 40),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 10];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["StudentID"].ToString();
            _data[_i, 2] = _dr["TitleTName"].ToString();
            _data[_i, 3] = _dr["ThaiFName"].ToString();
            _data[_i, 4] = _dr["ThaiLName"].ToString();
            _data[_i, 5] = _dr["GuarantorTitleTName"].ToString();
            _data[_i, 6] = _dr["GuarantorFirstName"].ToString();
            _data[_i, 7] = _dr["GuarantorLastName"].ToString();
            _data[_i, 8] = Util.ConvertDateTH(_dr["contractDateSignByStudent"].ToString());
            _data[_i, 9] = Util.ConvertTimeTH(_dr["contractDateSignByStudent"].ToString());

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListCPReportStatisticContractByProgram(string _acadamicyear) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 39),
            new SqlParameter("@acadamicyear", _acadamicyear)
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 11];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["AcadamicYear"].ToString();
            _data[_i, 2] = _dr["FacultyCode"].ToString();
            _data[_i, 3] = _dr["FactTName"].ToString();
            _data[_i, 4] = _dr["ProgramCode"].ToString();
            _data[_i, 5] = _dr["ProgTName"].ToString();
            _data[_i, 6] = _dr["MajorCode"].ToString();
            _data[_i, 7] = _dr["GroupNum"].ToString();
            _data[_i, 8] = !String.IsNullOrEmpty(_dr["CountStudent"].ToString()) ? _dr["CountStudent"].ToString() : "0";
            _data[_i, 9] = !String.IsNullOrEmpty(_dr["CountStudentSignContract"].ToString()) ? _dr["CountStudentSignContract"].ToString() : "0";
            _data[_i, 10] = !String.IsNullOrEmpty(_dr["CountStudentContractPenalty"].ToString()) ? _dr["CountStudentContractPenalty"].ToString() : "0";

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportNoticeRepayComplete(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportNoticeRepayComplete"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListReportNoticeRepayComplete(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 12];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["TotalPenalty"].ToString();            
            _data[_i, 11] = _dr["StatusPayment"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailReportNoticeRepayComplete(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 34),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 16];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["BCID"].ToString();
            _data[_i, 1] = _dr["RCID"].ToString();
            _data[_i, 2] = _dr["StudentID"].ToString();
            _data[_i, 3] = _dr["TitleTName"].ToString();
            _data[_i, 4] = _dr["FirstTName"].ToString();
            _data[_i, 5] = _dr["LastTName"].ToString();
            _data[_i, 6] = _dr["FacultyCode"].ToString();
            _data[_i, 7] = _dr["FacultyName"].ToString();
            _data[_i, 8] = _dr["ProgramCode"].ToString();
            _data[_i, 9] = _dr["ProgramName"].ToString();
            _data[_i, 10] = _dr["GroupNum"].ToString();
            _data[_i, 11] = _dr["GraduateDate"].ToString();
            _data[_i, 12] = _dr["IndemnitorYear"].ToString();
            _data[_i, 13] = _dr["IndemnitorAddress"].ToString();            
            _data[_i, 14] = _dr["TotalPenalty"].ToString();
            _data[_i, 15] = _dr["StatusPayment"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportNoticeClaimDebt(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportNoticeClaimDebt"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListReportNoticeClaimDebt(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 12];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["TotalPenalty"].ToString();
            _data[_i, 11] = SearchRepayStatusDetail(_dr["RCID"].ToString(), _dr["StatusRepay"].ToString(), _dr["StatusPayment"].ToString());

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListDetailReportNoticeClaimDebt(string _cp1id) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 35),
            new SqlParameter("@cp1id", (!String.IsNullOrEmpty(_cp1id) ? _cp1id : null))
        );
        
        string[,] _data = new string[_ds.Tables[1].Rows.Count, 27];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["BCID"].ToString();
            _data[_i, 1] = _dr["RCID"].ToString();
            _data[_i, 2] = _dr["StudentID"].ToString();
            _data[_i, 3] = _dr["TitleTName"].ToString();
            _data[_i, 4] = _dr["FirstTName"].ToString();
            _data[_i, 5] = _dr["LastTName"].ToString();
            _data[_i, 6] = _dr["FacultyCode"].ToString();
            _data[_i, 7] = _dr["FacultyName"].ToString();
            _data[_i, 8] = _dr["ProgramCode"].ToString();
            _data[_i, 9] = _dr["ProgramName"].ToString();
            _data[_i, 10] = _dr["GroupNum"].ToString();
            _data[_i, 11] = _dr["IndemnitorYear"].ToString();
            _data[_i, 12] = _dr["IndemnitorCash"].ToString();            
            _data[_i, 13] = _dr["ContractDate"].ToString();
            _data[_i, 14] = _dr["Guarantor"].ToString();
            _data[_i, 15] = _dr["ContractDateAgreement"].ToString();
            _data[_i, 16] = _dr["ApproveDate"].ToString();
            _data[_i, 17] = _dr["TotalPenalty"].ToString();
            _data[_i, 18] = _dr["SubtotalPenalty"].ToString();
            _data[_i, 19] = _dr["StudyLeave"].ToString();
            _data[_i, 20] = _dr["AfterStudyLeaveEndDate"].ToString();
            _data[_i, 21] = _dr["LawyerFullname"].ToString();
            _data[_i, 22] = _dr["LawyerPhoneNumber"].ToString();
            _data[_i, 23] = _dr["LawyerMobileNumber"].ToString();
            _data[_i, 24] = _dr["LawyerEmail"].ToString();
            _data[_i, 25] = _dr["StatusRepay"].ToString();
            _data[_i, 26] = _dr["StatusPayment"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportStatisticPaymentByDate(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 41),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!String.IsNullOrEmpty(_c.Request["formatpayment"]) ? _c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportStatisticPaymentByDate"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListReportStatisticPaymentByDate(HttpContext _c) {
        int _i = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 41),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!String.IsNullOrEmpty(_c.Request["formatpayment"]) ? _c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 14];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["TotalPenalty"].ToString();
            _data[_i, 11] = _dr["TotalPay"].ToString();
            _data[_i, 12] = _dr["FormatPayment"].ToString();
            _data[_i, 13] = _dr["FormatPaymentName"].ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportEContract(HttpContext _c) {
        int _recordCount = 0;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 42),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportEContract"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPReportEContract(HttpContext _c) {
        int _i = 0;
        string _pathEContract = "https://econtract.mahidol.ac.th/ElectronicContract/";
        string _path = String.Empty;
        string _p = String.Empty;
        string _yearFolder = String.Empty;
        string _fileDocA, _fileDocB, _fileDocC = String.Empty;

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 42),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@acadamicyear", (!String.IsNullOrEmpty(_c.Request["acadamicyear"]) ? _c.Request["acadamicyear"] : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 18];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["StudentID"].ToString();
            _data[_i, 2] = _dr["TitleTName"].ToString();
            _data[_i, 3] = _dr["ThaiFName"].ToString();
            _data[_i, 4] = _dr["ThaiLName"].ToString();
            _data[_i, 5] = _dr["ProgramCode"].ToString();
            _data[_i, 6] = _dr["ProgTName"].ToString();
            _data[_i, 7] = _dr["GroupNum"].ToString();
            /*
            if (_dr["operationType"].Equals("S") || _dr["operationType"].Equals("M")) {
                _data[_i, 8] = "1";
                _data[_i, 9] = "1";
                _data[_i, 10] = "1";
            }

            if (_dr["operationType"].Equals("O")) {
                _data[_i, 8] = (_dr["contractSignByStudent"].Equals(true) ? "1" : "0");
                _data[_i, 9] = (_dr["parentContractSignFlagF"].Equals(true) ? "1" : "0");
                _data[_i, 10] = (_dr["parentContractSignFlagM"].Equals(true) ? "1" : "0");
            }

            _p = _dr["ProgramCode"].ToString();

            if (_dr["ProgramCode"].Equals("NSNSB")) {
                switch (_dr["QuotaCode"].ToString())
                {
                    case "350":
                        _p = _p + "_Chulabhorn";
                        break;
                    case "391":
                        _p = _p + "_MT";
                        break;
                }
            }
            
            _yearFolder = DateTime.Parse(_dr["contractDateSignByStudent"].ToString()).Month >= 5 ? DateTime.Parse(_dr["contractDateSignByStudent"].ToString()).ToString("yyyy", new CultureInfo("th-TH")) : (DateTime.Parse(_dr["contractDateSignByStudent"].ToString()).AddYears(-1)).ToString("yyyy", new CultureInfo("th-TH"));

            _path = _pathEContract + _yearFolder + "/" + _p + "/";
            _fileDocA = _data[_i, 1] + _data[_i, 5] + "_A.pdf";
            _fileDocB = _data[_i, 1] + _data[_i, 5] + "_B.pdf";
            _fileDocC = _data[_i, 1] + _data[_i, 5] + "_C.pdf";
            */

            _data[_i, 8] = (!String.IsNullOrEmpty(_dr["contractPath"].ToString()) ? "1" : "0");
            _data[_i, 9] = (!String.IsNullOrEmpty(_dr["garranteePath"].ToString()) ? "1" : "0");
            _data[_i, 10] = (!String.IsNullOrEmpty(_dr["warranPath"].ToString()) ? "1" : "0");

            _path = _pathEContract;
            _fileDocA = _dr["contractPath"].ToString();
            _fileDocB = _dr["garranteePath"].ToString();
            _fileDocC = _dr["warranPath"].ToString();

            _data[_i, 11] = _path;
            _data[_i, 12] = _fileDocA;
            _data[_i, 13] = _fileDocB;
            _data[_i, 14] = _fileDocC;

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportDebtorContract(HttpContext _c) {
        int _recordCount = 0;
        int _orderTable = 0;

        switch (_c.Request["reportorder"])
        {
            case "reportdebtorcontract":
                _orderTable = 43;
                break;
            case "reportdebtorcontractpaid":
                _orderTable = 45;
                break;
            case "reportdebtorcontractremain":
                _orderTable = 47;
                break;
        }

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", _orderTable),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        _recordCount = _ds.Tables[0].Rows.Count;

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPReportDebtorContract(HttpContext _c) {
        int _i = 0;
        int _orderTable = 0;
        double _remain = 0;

        switch (_c.Request["reportorder"]) {
            case "reportdebtorcontract":
                _orderTable = 43;
                break;
            case "reportdebtorcontractpaid":
                _orderTable = 45;
                break;
            case "reportdebtorcontractremain":
                _orderTable = 47;
                break;
        }

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", _orderTable),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[0].Rows.Count, 14];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["FacultyCode"].ToString();
            _data[_i, 2] = _dr["FacultyName"].ToString();
            _data[_i, 3] = _dr["ProgramCode"].ToString();
            _data[_i, 4] = _dr["ProgramName"].ToString();
            _data[_i, 5] = _dr["MajorCode"].ToString();
            _data[_i, 6] = _dr["GroupNum"].ToString();
            _data[_i, 7] = _dr["DLevel"].ToString();
            _data[_i, 8] = _dr["DLevelName"].ToString();            
            _data[_i, 9] = _dr["CountStudentDebtor"].ToString();
            _data[_i, 10] = _dr["SumTotalPenalty"].ToString();
            _data[_i, 11] = _dr["SumTotalPayCapital"].ToString();
            _data[_i, 12] = _dr["SumTotalPayInterest"].ToString();

            _remain = (double.Parse(_data[_i, 10]) - double.Parse(_data[_i, 11]));

            _data[_i, 13] = _remain.ToString();

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static int CountCPReportDebtorContractByProgram(HttpContext _c) {
        int _recordCount = 0;
        int _orderTable = 0;

        switch (_c.Request["reportorder"]) {
            case "reportdebtorcontract":
                _orderTable = 44;
                break;
            case "reportdebtorcontractpaid":
                _orderTable = 46;
                break;
            case "reportdebtorcontractremain":
                _orderTable = 48;
                break;
        }

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", _orderTable),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!String.IsNullOrEmpty(_c.Request["formatpayment"]) ? _c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );
        
        foreach (DataRow _dr in _ds.Tables[0].Rows)
            _recordCount = int.Parse(_dr["CountReportDebtorContractByProgram"].ToString());

        _ds.Dispose();

        return _recordCount;
    }

    public static string[,] ListCPReportDebtorContractByProgram(HttpContext _c) {
        int _i = 0;
        int _orderTable = 0;
        double _remain = 0;

        switch (_c.Request["reportorder"]) {
            case "reportdebtorcontract":
                _orderTable = 44;
                break;
            case "reportdebtorcontractpaid":
                _orderTable = 46;
                break;
            case "reportdebtorcontractremain":
                _orderTable = 48;
                break;
        }

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", _orderTable),
            new SqlParameter("@startrow", eCPUtil.GetStartRow(_c.Request["startrow"])),
            new SqlParameter("@endrow", eCPUtil.GetEndRow(_c.Request["endrow"])),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_c.Request["studentid"]) ? _c.Request["studentid"] : null)),
            new SqlParameter("@faculty", (!String.IsNullOrEmpty(_c.Request["faculty"]) ? _c.Request["faculty"] : null)),
            new SqlParameter("@program", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["programcode"] : null)),
            new SqlParameter("@major", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["majorcode"] : null)),
            new SqlParameter("@groupnum", (!String.IsNullOrEmpty(_c.Request["programcode"]) && !String.IsNullOrEmpty(_c.Request["majorcode"]) && !String.IsNullOrEmpty(_c.Request["groupnum"]) ? _c.Request["groupnum"] : null)),
            new SqlParameter("@formatpayment", (!String.IsNullOrEmpty(_c.Request["formatpayment"]) ? _c.Request["formatpayment"] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_c.Request["datestart"]) ? _c.Request["datestart"] : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_c.Request["dateend"]) ? _c.Request["dateend"] : null))
        );

        string[,] _data = new string[_ds.Tables[1].Rows.Count, 23];

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 0] = _dr["RowNum"].ToString();
            _data[_i, 1] = _dr["BCID"].ToString();
            _data[_i, 2] = _dr["RCID"].ToString();
            _data[_i, 3] = _dr["StudentID"].ToString();
            _data[_i, 4] = _dr["TitleTName"].ToString();
            _data[_i, 5] = _dr["FirstTName"].ToString();
            _data[_i, 6] = _dr["LastTName"].ToString();
            _data[_i, 7] = _dr["ProgramCode"].ToString();
            _data[_i, 8] = _dr["ProgramName"].ToString();
            _data[_i, 9] = _dr["MajorCode"].ToString();
            _data[_i, 10] = _dr["GroupNum"].ToString();
            _data[_i, 11] = _dr["TotalPenalty"].ToString();
            _data[_i, 12] = (!String.IsNullOrEmpty(_dr["PayCapital"].ToString()) ? _dr["PayCapital"].ToString() : "0");
            _data[_i, 13] = (!String.IsNullOrEmpty(_dr["PayInterest"].ToString()) ? _dr["PayInterest"].ToString() : "0");
            
            _remain = (double.Parse(_data[_i, 11]) - double.Parse(_data[_i, 12]));

            _data[_i, 14] = _remain.ToString();
            _data[_i, 15] = _dr["ReplyDate"].ToString();
            _data[_i, 16] = _dr["FormatPayment"].ToString();
            _data[_i, 17] = _dr["FormatPaymentName"].ToString();
            _data[_i, 18] = _dr["StatusSend"].ToString();
            _data[_i, 19] = _dr["StatusReceiver"].ToString();
            _data[_i, 20] = _dr["StatusEdit"].ToString();
            _data[_i, 21] = _dr["StatusCancel"].ToString();
            _data[_i, 22] = eCPUtil._actionTrackingStatus[2, Util.FindIndexArray3D(0, eCPUtil._actionTrackingStatus, _dr["StatusSend"].ToString() + _dr["StatusReceiver"].ToString() + _dr["StatusEdit"].ToString() + _dr["StatusCancel"].ToString()) - 1, 1];

            _i++;
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListExportReportDebtorContract(string _exportSend) {
        int _i = 0;
        char[] _separator;

        _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _idName = _exportSendValue[2];

        _separator = new char[] { ';' };
        string[] _faculty = (!String.IsNullOrEmpty(_exportSendValue[3]) ? _exportSendValue[3].Split(_separator) : new string[0]);
        string[] _program = (!String.IsNullOrEmpty(_exportSendValue[4]) ? _exportSendValue[4].Split(_separator) : new string[0]);
        string[] _formatPayment = (!String.IsNullOrEmpty(_exportSendValue[5]) ? _exportSendValue[5].Split(_separator) : new string[0]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 49),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_idName) ? _idName : null)),
            new SqlParameter("@faculty", (_faculty.GetLength(0) > 0 && !String.IsNullOrEmpty(_faculty[0]) ? _faculty[0] : null)),
            new SqlParameter("@program", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[0] : null)),
            new SqlParameter("@major", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[2] : null)),
            new SqlParameter("@groupnum", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[3] : null)),
            new SqlParameter("@formatpayment", (_formatPayment.GetLength(0) > 0 && !String.IsNullOrEmpty(_formatPayment[0]) ? _formatPayment[0] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_dateStart) ? _dateStart : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_dateEnd) ? _dateEnd : null))
        );

        string[,] _data = new string[(_ds.Tables[0].Rows.Count + 1), 29];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["StudentID"].ToString();
            _data[_i, 1] = _dr["TitleTName"].ToString();
            _data[_i, 2] = _dr["FirstTName"].ToString();
            _data[_i, 3] = _dr["LastTName"].ToString();
            _data[_i, 4] = _dr["FacultyCode"].ToString();
            _data[_i, 5] = _dr["FacultyName"].ToString();
            _data[_i, 6] = _dr["ProgramCode"].ToString();
            _data[_i, 7] = _dr["ProgramName"].ToString();
            _data[_i, 8] = _dr["MajorCode"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["DLevel"].ToString();
            _data[_i, 11] = _dr["DLevelName"].ToString();
            _data[_i, 12] = _dr["CivilFlag"].ToString();
            _data[_i, 13] = _dr["IndemnitorYear"].ToString();
            _data[_i, 14] = _dr["AllActualDate"].ToString();
            _data[_i, 15] = _dr["IndemnitorCash"].ToString();
            _data[_i, 16] = _dr["RequireDate"].ToString();
            _data[_i, 17] = _dr["ApproveDate"].ToString();
            _data[_i, 18] = _dr["ActualDate"].ToString();
            _data[_i, 19] = _dr["RemainDate"].ToString();
            _data[_i, 20] = _dr["TotalPenalty"].ToString();
            _data[_i, 21] = _dr["ReplyDate"].ToString();
            _data[_i, 22] = _dr["FormatPayment"].ToString();
            _data[_i, 23] = _dr["FormatPaymentName"].ToString();
            _data[_i, 24] = _dr["StatusSend"].ToString();
            _data[_i, 25] = _dr["StatusReceiver"].ToString();
            _data[_i, 26] = _dr["StatusEdit"].ToString();
            _data[_i, 27] = _dr["StatusCancel"].ToString();

            _i++;
        }

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 28] = _dr["TotalPenalty"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListExportReportDebtorContractPaid(string _exportSend) {
        int _i = 0;
        char[] _separator;

        _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _idName = _exportSendValue[2];

        _separator = new char[] { ';' };
        string[] _faculty = (!String.IsNullOrEmpty(_exportSendValue[3]) ? _exportSendValue[3].Split(_separator) : new string[0]);
        string[] _program = (!String.IsNullOrEmpty(_exportSendValue[4]) ? _exportSendValue[4].Split(_separator) : new string[0]);
        string[] _formatPayment = (!String.IsNullOrEmpty(_exportSendValue[5]) ? _exportSendValue[5].Split(_separator) : new string[0]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 50),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_idName) ? _idName : null)),
            new SqlParameter("@faculty", (_faculty.GetLength(0) > 0 && !String.IsNullOrEmpty(_faculty[0]) ? _faculty[0] : null)),
            new SqlParameter("@program", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[0] : null)),
            new SqlParameter("@major", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[2] : null)),
            new SqlParameter("@groupnum", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[3] : null)),
            new SqlParameter("@formatpayment", (_formatPayment.GetLength(0) > 0 && !String.IsNullOrEmpty(_formatPayment[0]) ? _formatPayment[0] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_dateStart) ? _dateStart : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_dateEnd) ? _dateEnd : null))
        );

        string[,] _data = new string[(_ds.Tables[0].Rows.Count + 1), 46];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["StudentID"].ToString();
            _data[_i, 1] = _dr["TitleTName"].ToString();
            _data[_i, 2] = _dr["FirstTName"].ToString();
            _data[_i, 3] = _dr["LastTName"].ToString();
            _data[_i, 4] = _dr["FacultyCode"].ToString();
            _data[_i, 5] = _dr["FacultyName"].ToString();
            _data[_i, 6] = _dr["ProgramCode"].ToString();
            _data[_i, 7] = _dr["ProgramName"].ToString();
            _data[_i, 8] = _dr["MajorCode"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["DLevel"].ToString();
            _data[_i, 11] = _dr["DLevelName"].ToString();
            _data[_i, 12] = _dr["ReplyDate"].ToString();
            _data[_i, 13] = _dr["DateTimePayment"].ToString();
            _data[_i, 14] = _dr["Capital"].ToString();
            _data[_i, 15] = _dr["Interest"].ToString();            
            _data[_i, 16] = _dr["TotalAccruedInterest"].ToString();
            _data[_i, 17] = _dr["TotalInterest"].ToString();
            _data[_i, 18] = _dr["TotalPayment"].ToString();
            _data[_i, 19] = _dr["ReceiptDate"].ToString();
            _data[_i, 20] = _dr["ReceiptBookNo"].ToString();
            _data[_i, 21] = _dr["ReceiptNo"].ToString();
            _data[_i, 22] = _dr["PayCapital"].ToString();
            _data[_i, 23] = _dr["PayInterest"].ToString();
            _data[_i, 24] = _dr["TotalPay"].ToString();
            _data[_i, 25] = _dr["ReceiptSendNo"].ToString();
            _data[_i, 26] = _dr["ReceiptFund"].ToString();
            _data[_i, 27] = _dr["RemainCapital"].ToString();
            _data[_i, 28] = _dr["AccruedInterest"].ToString();
            _data[_i, 29] = _dr["RemainAccruedInterest"].ToString();
            _data[_i, 30] = _dr["TotalRemain"].ToString();
            _data[_i, 31] = _dr["FormatPayment"].ToString();
            _data[_i, 32] = _dr["FormatPaymentName"].ToString();
            _data[_i, 33] = _dr["StatusSend"].ToString();
            _data[_i, 34] = _dr["StatusReceiver"].ToString();
            _data[_i, 35] = _dr["StatusEdit"].ToString();
            _data[_i, 36] = _dr["StatusCancel"].ToString();

            _i++;
        }

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 37] = _dr["TotalCapital"].ToString();
            _data[_i, 38] = _dr["TotalInterest"].ToString();
            _data[_i, 39] = _dr["TotalPayment"].ToString();
            _data[_i, 40] = _dr["PayCapital"].ToString();
            _data[_i, 41] = _dr["PayInterest"].ToString();
            _data[_i, 42] = _dr["TotalPay"].ToString();
            _data[_i, 43] = _dr["RemainCapital"].ToString();
            _data[_i, 44] = _dr["RemainInterest"].ToString();
            _data[_i, 45] = _dr["TotalRemain"].ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string[,] ListExportReportDebtorContractRemain(string _exportSend) {
        int _i = 0;
        char[] _separator;
        double _totalRemainCapital = 0;
        double _totalAccruedInterest = 0;
        double _totalRemainAccruedInterest = 0;
        double _totalTotalRemain = 0;

        _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _dateStart = _exportSendValue[0];
        string _dateEnd = _exportSendValue[1];
        string _idName = _exportSendValue[2];

        _separator = new char[] { ';' };
        string[] _faculty = (!String.IsNullOrEmpty(_exportSendValue[3]) ? _exportSendValue[3].Split(_separator) : new string[0]);
        string[] _program = (!String.IsNullOrEmpty(_exportSendValue[4]) ? _exportSendValue[4].Split(_separator) : new string[0]);
        string[] _formatPayment = (!String.IsNullOrEmpty(_exportSendValue[5]) ? _exportSendValue[5].Split(_separator) : new string[0]);

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 51),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_idName) ? _idName : null)),
            new SqlParameter("@faculty", (_faculty.GetLength(0) > 0 && !String.IsNullOrEmpty(_faculty[0]) ? _faculty[0] : null)),
            new SqlParameter("@program", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[0] : null)),
            new SqlParameter("@major", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[2] : null)),
            new SqlParameter("@groupnum", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[3] : null)),
            new SqlParameter("@formatpayment", (_formatPayment.GetLength(0) > 0 && !String.IsNullOrEmpty(_formatPayment[0]) ? _formatPayment[0] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_dateStart) ? _dateStart : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_dateEnd) ? _dateEnd : null))
        );

        string[,] _data = new string[(_ds.Tables[0].Rows.Count + 1), 37];

        foreach (DataRow _dr in _ds.Tables[0].Rows) {
            _data[_i, 0] = _dr["StudentID"].ToString();
            _data[_i, 1] = _dr["TitleTName"].ToString();
            _data[_i, 2] = _dr["FirstTName"].ToString();
            _data[_i, 3] = _dr["LastTName"].ToString();
            _data[_i, 4] = _dr["FacultyCode"].ToString();
            _data[_i, 5] = _dr["FacultyName"].ToString();
            _data[_i, 6] = _dr["ProgramCode"].ToString();
            _data[_i, 7] = _dr["ProgramName"].ToString();
            _data[_i, 8] = _dr["MajorCode"].ToString();
            _data[_i, 9] = _dr["GroupNum"].ToString();
            _data[_i, 10] = _dr["DLevel"].ToString();
            _data[_i, 11] = _dr["DLevelName"].ToString();
            _data[_i, 12] = _dr["CivilFlag"].ToString();
            _data[_i, 13] = _dr["IndemnitorYear"].ToString();
            _data[_i, 14] = _dr["AllActualDate"].ToString();
            _data[_i, 15] = _dr["IndemnitorCash"].ToString();
            _data[_i, 16] = _dr["RequireDate"].ToString();
            _data[_i, 17] = _dr["ApproveDate"].ToString();
            _data[_i, 18] = _dr["ActualDate"].ToString();
            _data[_i, 19] = _dr["RemainDate"].ToString();
            _data[_i, 20] = _dr["TotalPenalty"].ToString();
            _data[_i, 21] = _dr["ReplyDate"].ToString();
            _data[_i, 22] = (!_dr["StatusPayment"].ToString().Equals("1") ? _dr["RemainCapital"].ToString() : _dr["TotalPenalty"].ToString());
            _data[_i, 23] = (!_dr["StatusPayment"].ToString().Equals("1") ? _dr["AccruedInterest"].ToString() : "0");
            _data[_i, 24] = (!_dr["StatusPayment"].ToString().Equals("1") ? _dr["RemainAccruedInterest"].ToString() : "0");
            _data[_i, 25] = (!_dr["StatusPayment"].ToString().Equals("1") ? _dr["TotalRemain"].ToString() : _dr["TotalPenalty"].ToString());
            _data[_i, 26] = _dr["FormatPayment"].ToString();
            _data[_i, 27] = _dr["FormatPaymentName"].ToString();
            _data[_i, 28] = _dr["StatusSend"].ToString();
            _data[_i, 29] = _dr["StatusReceiver"].ToString();
            _data[_i, 30] = _dr["StatusEdit"].ToString();
            _data[_i, 31] = _dr["StatusCancel"].ToString();

            _totalRemainCapital = _totalRemainCapital + double.Parse(_data[_i, 22]);
            _totalAccruedInterest = _totalAccruedInterest + double.Parse(_data[_i, 23]);
            _totalRemainAccruedInterest = _totalRemainAccruedInterest + double.Parse(_data[_i, 24]);
            _totalTotalRemain = _totalTotalRemain + double.Parse(_data[_i, 25]);

            _i++;
        }

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            _data[_i, 32] = _dr["TotalPenalty"].ToString();
            _data[_i, 33] = _totalRemainCapital.ToString();
            _data[_i, 34] = _totalAccruedInterest.ToString();
            _data[_i, 35] = _totalRemainAccruedInterest.ToString();
            _data[_i, 36] = _totalTotalRemain.ToString();
        }

        _ds.Dispose();

        return _data;
    }

    public static string ListExportReportDebtorContractBreakRequireRepayPayment(string _exportSend) {
        char[] _separator;

        _separator = new char[] { ':' };
        string[] _exportSendValue = _exportSend.Split(_separator);
        string _statusPayment = _exportSendValue[0];
        string _statusPaymentRecord = _exportSendValue[1];
        string _idName = _exportSendValue[2];

        _separator = new char[] { ';' };
        string[] _faculty = (!String.IsNullOrEmpty(_exportSendValue[3]) ? _exportSendValue[3].Split(_separator) : new string[0]);
        string[] _program = (!String.IsNullOrEmpty(_exportSendValue[4]) ? _exportSendValue[4].Split(_separator) : new string[0]);

        string _dateStart = _exportSendValue[5];
        string _dateEnd = _exportSendValue[6];

        JArray jsonArray = new JArray();

        DataSet _ds = ExecuteCommandStoredProcedure(
            new SqlParameter("@ordertable", 19),
            new SqlParameter("@statuspayment", (!String.IsNullOrEmpty(_statusPayment) ? _statusPayment : null)),
            new SqlParameter("@statuspaymentrecord", (!String.IsNullOrEmpty(_statusPaymentRecord) ? _statusPaymentRecord : null)),
            new SqlParameter("@studentid", (!String.IsNullOrEmpty(_idName) ? _idName : null)),
            new SqlParameter("@faculty", (_faculty.GetLength(0) > 0 && !String.IsNullOrEmpty(_faculty[0]) ? _faculty[0] : null)),
            new SqlParameter("@program", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[0] : null)),
            new SqlParameter("@major", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[2] : null)),
            new SqlParameter("@groupnum", (_program.GetLength(0) > 0 && !String.IsNullOrEmpty(_program[0]) && !String.IsNullOrEmpty(_program[2]) && !String.IsNullOrEmpty(_program[3]) ? _program[3] : null)),
            new SqlParameter("@datestart", (!String.IsNullOrEmpty(_dateStart) ? _dateStart : null)),
            new SqlParameter("@dateend", (!String.IsNullOrEmpty(_dateEnd) ? _dateEnd : null))
        );

        foreach (DataRow _dr in _ds.Tables[1].Rows) {
            jsonArray.Add(new JObject() {
                { "ID", _dr["ID"].ToString() },
                { "BCID", _dr["BCID"].ToString() },
                { "studentCode", _dr["StudentID"].ToString() },
                { "titleName", _dr["TitleTName"].ToString() },
                { "firstName", _dr["FirstTName"].ToString() },
                { "lastName", _dr["LastTName"].ToString() },
                { "facultyCode", _dr["FacultyCode"].ToString() },
                { "facultyName", _dr["FacultyName"].ToString() },
                { "programCode", _dr["ProgramCode"].ToString() },
                { "programName", _dr["ProgramName"].ToString() },
                { "majorCode", _dr["MajorCode"].ToString() },
                { "groupNum", _dr["GroupNum"].ToString() },
                { "contractDate", _dr["ContractDate"].ToString() },
                { "sendDate", _dr["DateTimeSend"].ToString() },
                { "receiverDate", _dr["DateTimeReceiver"].ToString() },
                { "replyDateHistory", _dr["ReplyDateHistory"].ToString() },
                { "subtotalPenalty", _dr["SubtotalPenalty"].ToString() },
                { "totalPenalty", _dr["TotalPenalty"].ToString() },
                { "totalPayCapital", (!String.IsNullOrEmpty(_dr["TotalPayCapital"].ToString()) ? _dr["TotalPayCapital"].ToString() : "0") },
                { "totalPayInterest", (!String.IsNullOrEmpty(_dr["TotalPayInterest"].ToString()) ? _dr["TotalPayInterest"].ToString() : "0") },
                { "totalPay", (!String.IsNullOrEmpty(_dr["TotalPay"].ToString()) ? _dr["TotalPay"].ToString() : "0") },
                { "totalRemain", (!String.IsNullOrEmpty(_dr["TotalRemain"].ToString()) ? _dr["TotalRemain"].ToString() : "0") },
                { "remainAccruedInterest", (!String.IsNullOrEmpty(_dr["RemainAccruedInterest"].ToString()) ? _dr["RemainAccruedInterest"].ToString() : "0") },
                { "statusPayment", _dr["StatusPayment"].ToString() },
                { "statusPaymentRecord", _dr["StatusPaymentRecord"].ToString() }
            });
        }

        _ds.Dispose();

        return JsonConvert.SerializeObject(jsonArray);
    }

    public static void AddUpdateData(HttpContext _c) {
        string _command = String.Empty;
        string _what = String.Empty;
        string _where = String.Empty;
        string _function = String.Empty;
        string _sqlCommand = String.Empty;

        if (_c.Request["cmd"].Equals("addcptabuser")) {
            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];
            
            _what = "INSERT";
            _where = "ecpTabUser";
            _function = "AddUpdateData, addcptabuser";
            _command += "INSERT INTO ecpTabUser " +
                        "(ID, Username, Password, Name, PhoneNumber, MobileNumber, Email, UserSection, UserLevel) " +
                        "VALUES " +
                        "(" +
                        "newid(), " +
                        "'" + _c.Request["username"] + "', " +
                        "'" + _c.Request["password"] + "', " +
                        "'" + _c.Request["name"] + "', " +
                        (String.IsNullOrEmpty(_c.Request["phonenumber"]) ? "NULL" : ("'" + _c.Request["phonenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["mobilenumber"]) ? "NULL" : ("'" + _c.Request["mobilenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["email"]) ? "NULL" : ("'" + _c.Request["email"] + "'")) + ", " +
                        "'" + _eCPCookie["UserSection"] + "', " +
                        "'User'" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptabuser")) {
            _what = "UPDATE";
            _where = "ecpTabUser";
            _function = "AddUpdateData, updatecptabuser";
            _command += "UPDATE ecpTabUser SET " +
                        "Username = '" + _c.Request["username"] + "', " +
                        "Password = '" + _c.Request["password"] + "', " +
                        "Name = '" + _c.Request["name"] + "', " +
                        "PhoneNumber = " + (String.IsNullOrEmpty(_c.Request["phonenumber"]) ? "NULL" : ("'" + _c.Request["phonenumber"] + "'")) + ", " +
                        "MobileNumber = " + (String.IsNullOrEmpty(_c.Request["mobilenumber"]) ? "NULL" : ("'" + _c.Request["mobilenumber"] + "'")) + ", " +
                        "Email = " + (String.IsNullOrEmpty(_c.Request["email"]) ? "NULL" : ("'" + _c.Request["email"] + "'")) + " " +
                        "WHERE (ID = '" + _c.Request["userid"] + "')";
        }

        if (_c.Request["cmd"].Equals("delcptabuser")) {
            _what = "DELETE";
            _where = "ecpTabUser";
            _function = "AddUpdateData, ecpTabUser";
            _command += "DELETE FROM ecpTabUser WHERE (ID = '" + _c.Request["userid"] + "')";
        }

        if (_c.Request["cmd"].Equals("addcptabprogram")) {
            _what = "INSERT";
            _where = "ecpTabProgram";
            _function = "AddUpdateData, addcptabprogram";
            _command += "INSERT INTO ecpTabProgram " +
                        "(ProgramCode, MajorCode, GroupNum, FacultyCode, Dlevel) " +
                        "VALUES " +
                        "(" +
                        "'" + _c.Request["programcode"] + "', " +
                        "'" + _c.Request["majorcode"] + "', " +
                        "'" + _c.Request["groupnum"] + "', " +
                        "'" + _c.Request["faculty"] + "', " +
                        "'" + _c.Request["dlevel"] + "'" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptabprogram")) {
            _what = "UPDATE";
            _where = "ecpTabProgram";
            _function = "AddUpdateData, updatecptabprogram";
            _command += "UPDATE ecpTabProgram SET " +
                        "ProgramCode = '" + _c.Request["programcode"] + "', " +
                        "MajorCode = '" + _c.Request["majorcode"] + "', " +
                        "GroupNum = '" + _c.Request["groupnum"] + "', " +
                        "FacultyCode = '" + _c.Request["faculty"] + "', " +
                        "Dlevel = '" + _c.Request["dlevel"] + "' " +
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("delcptabprogram")) {
            _what = "DELETE";
            _where = "ecpTabProgram";
            _function = "AddUpdateData, delcptabprogram";
            _command += "DELETE FROM ecpTabProgram WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("addcptabinterest")) {
            _what = "INSERT";
            _where = "ecpTabInterest";
            _function = "AddUpdateData, addcptabinterest";
            _command += "INSERT INTO ecpTabInterest (InContractInterest, OutContractInterest, UseContractInterest) " +
                        "VALUES "+
                        "(" + 
                        _c.Request["incontractinterest"] + ", " + 
                        _c.Request["outcontractinterest"] + ", " +
                        "0" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updateusecontractinterest")) {
            _what = "UPDATE";
            _where = "ecpTabInterest";
            _function = "AddUpdateData, updateusecontractinterest";
            _command += "UPDATE ecpTabInterest SET UseContractInterest = 0 " +
                        "UPDATE ecpTabInterest SET UseContractInterest = " + _c.Request["usecontractinterest"] + " WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("updatecptabinterest")) {
            _what = "UPDATE";
            _where = "ecpTabInterest";
            _function = "AddUpdateData, updatecptabinterest";
            _command += "UPDATE ecpTabInterest SET " +
                        "InContractInterest = " + _c.Request["incontractinterest"] + ", " +
                        "OutContractInterest = " + _c.Request["outcontractinterest"] + " " +
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("delcptabinterest")) {
            _what = "DELETE";
            _where = "ecpTabInterest";
            _function = "AddUpdateData, delcptabinterest";
            _command += "DELETE FROM ecpTabInterest WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("addcptabpaybreakcontract")) {
            _what = "INSERT";
            _where = "ecpTabPayBreakContract";
            _function = "AddUpdateData, addcptabpaybreakcontract";
            _command += "INSERT INTO ecpTabPayBreakContract " +
                        "(FacultyCode, ProgramCode, MajorCode, GroupNum, AmountCash, Dlevel, CaseGraduate, CalDateCondition, AmtIndemnitorYear) " +
                        "VALUES " +
                        "(" + 
                        "'" + _c.Request["faculty"] + "', " +
                        "'" + _c.Request["programcode"] + "', " +
                        "'" + _c.Request["majorcode"] + "', " +
                        "'" + _c.Request["groupnum"] + "', " +
                        _c.Request["amountcash"] + ", " +
                        "'" + _c.Request["dlevel"] + "', " +
                        _c.Request["casegraduate"] + ", " +
                        _c.Request["caldatecondition"] + ", " +
                        (String.IsNullOrEmpty(_c.Request["amtindemnitoryear"]) ? "0" : _c.Request["amtindemnitoryear"]) +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptabpaybreakcontract")) {
            _what = "UPDATE";
            _where = "ecpTabPayBreakContract";
            _function = "AddUpdateData, updatecptabpaybreakcontract";
            _command += "UPDATE ecpTabPayBreakContract SET " +
                        "FacultyCode = '" + _c.Request["faculty"] + "', " +
                        "ProgramCode = '" + _c.Request["programcode"] + "', " +
                        "MajorCode = '" + _c.Request["majorcode"] + "', " +
                        "GroupNum = '" + _c.Request["groupnum"] + "', " +
                        "AmountCash = " +_c.Request["amountcash"] + ", " +
                        "Dlevel = '" + _c.Request["dlevel"] + "', " +
                        "CaseGraduate = " + _c.Request["casegraduate"] + ", " +
                        "CalDateCondition = " + _c.Request["caldatecondition"] + ", " +
                        "AmtIndemnitorYear = " + (String.IsNullOrEmpty(_c.Request["amtindemnitoryear"]) ? "0" : _c.Request["amtindemnitoryear"]) + " " +
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("delcptabpaybreakcontract")) {
            _what = "DELETE";
            _where = "ecpTabPayBreakContract";
            _function = "AddUpdateData, delcptabpaybreakcontract";
            _command += "DELETE FROM ecpTabPayBreakContract WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("addcptabscholarship")) {
            _what = "INSERT";
            _where = "ecpTabScholarship";
            _function = "AddUpdateData, addcptabscholarship";
            _command += "INSERT INTO ecpTabScholarship " +
                        "(FacultyCode, ProgramCode, MajorCode, GroupNum, ScholarshipMoney, Dlevel) " +
                        "VALUES " +
                        "(" +
                        "'" + _c.Request["faculty"] + "', " +
                        "'" + _c.Request["programcode"] + "', " +
                        "'" + _c.Request["majorcode"] + "', " +
                        "'" + _c.Request["groupnum"] + "', " +
                        _c.Request["scholarshipmoney"] + ", " +
                        "'" + _c.Request["dlevel"] + "'" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptabscholarship")) {
            _what = "UPDATE";
            _where = "ecpTabScholarship";
            _function = "AddUpdateData, updatecptabscholarship";
            _command += "UPDATE ecpTabScholarship SET " +
                        "FacultyCode = '" + _c.Request["faculty"] + "', " +
                        "ProgramCode = '" + _c.Request["programcode"] + "', " +
                        "MajorCode = '" + _c.Request["majorcode"] + "', " +
                        "GroupNum = '" + _c.Request["groupnum"] + "', " +
                        "ScholarshipMoney = " + _c.Request["scholarshipmoney"] + ", " +
                        "Dlevel = '" + _c.Request["dlevel"] + "' " +
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("delcptabscholarship")) {
            _what = "DELETE";
            _where = "ecpTabScholarship";
            _function = "AddUpdateData, delcptabscholarship";
            _command += "DELETE FROM ecpTabScholarship WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("addcptransbreakcontract")) {
            _what = "INSERT";
            _where = "ecpTransBreakContract";
            _function = "AddUpdateData, addcptransbreakcontract";
            _command += "INSERT INTO ecpTransBreakContract " +
                        "(StudentID, TitleCode, TitleEName, TitleTName, FirstEName, LastEName, FirstTName, LastTName, FacultyCode, FacultyName, ProgramCode, ProgramName, MajorCode, GroupNum, DLevel, " +
                        " PursuantBook, Pursuant, PursuantBookDate, Location, InputDate, StateLocation, StateLocationDate, ContractDate, ContractDateAgreement, Guarantor, " +
                        " ScholarFlag, ScholarshipMoney, ScholarshipYear, ScholarshipMonth, EducationDate, GraduateDate, ContractForceStartDate, ContractForceEndDate, CaseGraduate, CivilFlag, " +
                        " CalDateCondition, IndemnitorYear, IndemnitorCash, StatusSend, StatusReceiver, StatusEdit, StatusCancel, " +
                        " DateTimeCreate)" +
                        "VALUES " +
                        "(" +
                        "'" + _c.Request["studentid"] + "', " +
                        "'" + _c.Request["titlecode"] + "', " +
                        "'" + _c.Request["titlenameeng"] + "', " +
                        "'" + _c.Request["titlenametha"] + "', " +
                        "'" + _c.Request["firstnameeng"] + "', " +
                        "'" + _c.Request["lastnameeng"] + "', " +
                        "'" + _c.Request["firstnametha"] + "', " +
                        "'" + _c.Request["lastnametha"] + "', " +
                        "'" + _c.Request["facultycode"] + "', " +
                        "'" + _c.Request["facultyname"] + "', " +
                        "'" + _c.Request["programcode"] + "', " +
                        "'" + _c.Request["programname"] + "', " +
                        "'" + _c.Request["majorcode"] + "', " +
                        "'" + _c.Request["groupnum"] + "', " +
                        "'" + _c.Request["dlevel"] + "', " +
                        "'" + _c.Request["pursuantbook"] + "', " +
                        "'" + _c.Request["pursuant"] + "', " +
                        "'" + _c.Request["pursuantbookdate"] + "', " +
                        "'" + _c.Request["location"] + "', " +
                        "'" + _c.Request["inputdate"] + "', " +
                        "'" + _c.Request["statelocation"] + "', " +
                        "'" + _c.Request["statelocationdate"] + "', " +
                        "'" + _c.Request["contractdate"] + "', " +
                        "'" + _c.Request["contractdateagreement"] + "', " +
                        "'" + _c.Request["guarantor"] + "', " +
                        _c.Request["scholarflag"] + ", " +
                        (String.IsNullOrEmpty(_c.Request["scholarshipmoney"]) ? "0" : _c.Request["scholarshipmoney"]) + ", " +
                        (String.IsNullOrEmpty(_c.Request["scholarshipyear"]) ? "0" : _c.Request["scholarshipyear"]) + ", " +
                        (String.IsNullOrEmpty(_c.Request["scholarshipmonth"]) ? "0" : _c.Request["scholarshipmonth"]) + ", " +
                        "'" + _c.Request["educationdate"] + "', " +
                        "'" + _c.Request["graduatedate"] + "', " +
                        "'" + _c.Request["contractforcedatestart"] + "', " +
                        (String.IsNullOrEmpty(_c.Request["contractforcedateend"]) ? "NULL" : ("'" + _c.Request["contractforcedateend"] + "'")) + ", " +
                        _c.Request["casegraduate"] + ", " +                        
                        _c.Request["civilflag"] + ", " +
                        _c.Request["caldatecondition"] + ", " +
                        (String.IsNullOrEmpty(_c.Request["indemnitoryear"]) ? "0" : _c.Request["indemnitoryear"]) + ", " +
                        _c.Request["indemnitorcash"] + ", " +
                        "1, " +
                        "1, " +
                        "1, " +
                        "1, " +
                        "GETDATE()" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptransbreakcontract")) {
            _what = "UPDATE";
            _where = "ecpTransBreakContract";
            _function = "AddUpdateData, updatecptransbreakcontract";
            _command += "UPDATE ecpTransBreakContract SET " +
                        "StudentID = '" + _c.Request["studentid"] + "', " +
                        "TitleCode = '" + _c.Request["titlecode"] + "', " +
                        "TitleEName = '" + _c.Request["titlenameeng"] + "', " +
                        "TitleTName = '" + _c.Request["titlenametha"] + "', " +
                        "FirstEName = '" + _c.Request["firstnameeng"] + "', " +
                        "LastEName = '" + _c.Request["lastnameeng"] + "', " +
                        "FirstTName = '" + _c.Request["firstnametha"] + "', " +
                        "LastTName = '" + _c.Request["lastnametha"] + "', " +
                        "FacultyCode = '" + _c.Request["facultycode"] + "', " +
                        "FacultyName = '" + _c.Request["facultyname"] + "', " +
                        "ProgramCode = '" + _c.Request["programcode"] + "', " +
                        "ProgramName = '" + _c.Request["programname"] + "', " +
                        "MajorCode = '" + _c.Request["majorcode"] + "', " +
                        "GroupNum = '" + _c.Request["groupnum"] + "', " +
                        "DLevel = '" + _c.Request["dlevel"] + "', " +
                        "PursuantBook = '" + _c.Request["pursuantbook"] + "', " +
                        "Pursuant = '" + _c.Request["pursuant"] + "', " +
                        "PursuantBookDate = '" + _c.Request["pursuantbookdate"] + "', " +
                        "Location = '" + _c.Request["location"] + "', " +
                        "InputDate = '" + _c.Request["inputdate"] + "', " +
                        "StateLocation = '" + _c.Request["statelocation"] + "', " +
                        "StateLocationDate = '" + _c.Request["statelocationdate"] + "', " +
                        "ContractDate = '" + _c.Request["contractdate"] + "', " +
                        "ContractDateAgreement = '" + _c.Request["contractdateagreement"] + "', " +
                        "Guarantor = '" + _c.Request["guarantor"] + "', " +
                        "ScholarFlag = " + _c.Request["scholarflag"] + ", " +
                        "ScholarshipMoney=" + (String.IsNullOrEmpty(_c.Request["scholarshipmoney"]) ? "0" : _c.Request["scholarshipmoney"]) + ", " +
                        "ScholarshipYear=" + (String.IsNullOrEmpty(_c.Request["scholarshipyear"]) ? "0" : _c.Request["scholarshipyear"]) + ", " +
                        "ScholarshipMonth=" + (String.IsNullOrEmpty(_c.Request["scholarshipmonth"]) ? "0" : _c.Request["scholarshipmonth"]) + ", " +
                        "EducationDate = '" + _c.Request["educationdate"] + "', " +
                        "GraduateDate = '" + _c.Request["graduatedate"] + "', " +
                        "ContractForceStartDate = '" + _c.Request["contractforcedatestart"] + "', " +
                        "ContractForceEndDate = " + (String.IsNullOrEmpty(_c.Request["contractforcedateend"]) ? "NULL" : ("'" + _c.Request["contractforcedateend"] + "'")) + ", " +
                        "CaseGraduate = " + _c.Request["casegraduate"] + ", " +
                        "CivilFlag = " + _c.Request["civilflag"] + ", " +
                        "CalDateCondition = " + _c.Request["caldatecondition"] + ", " +
                        "IndemnitorYear = " + (String.IsNullOrEmpty(_c.Request["indemnitoryear"]) ? "0" : _c.Request["indemnitoryear"]) + ", " +
                        "IndemnitorCash = " + _c.Request["indemnitorcash"] + ", " +
                        "StatusEdit = 1, " +
                        "DateTimeModify = GETDATE() " +
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("updatetrackingstatusbreakcontract")) {
            _what = "UPDATE";
            _where = "ecpTransBreakContract";
            _function = "AddUpdateData, updatetrackingstatusbreakcontract";
            _command += "UPDATE ecpTransBreakContract SET " +
                        (_c.Request["status"].Equals("send") ? "StatusSend = 2, DateTimeSend = GETDATE() " : "") +
                        (_c.Request["status"].Equals("edit") ? "StatusEdit = 2 " : "") +
                        (_c.Request["status"].Equals("cancel") ? "StatusCancel = 2, DateTimeCancel = GETDATE() " : "") + 
                        "WHERE ID = " + _c.Request["cp1id"];
        }

        if (_c.Request["cmd"].Equals("sendbreakcontract")) {
            char[] _separator = new char[] {';'};
            string[] _cpid = (_c.Request["cp1id"]).Split(_separator);
            int _i;

            for (_i = 0; _i < _cpid.Length; _i++)
            {
                _what = "UPDATE";
                _where = "ecpTransBreakContract";
                _function = "AddUpdateData, sendbreakcontract";
                _command += "UPDATE ecpTransBreakContract SET " +
                            "StatusSend = 2, " +
                            "DateTimeSend = GETDATE() " +
                            "WHERE ID = " + _cpid[_i] + "; ";
            }
        }

        if (_c.Request["cmd"].Equals("rejectcptransbreakcontract")) {
            _what = "UPDATE";
            _where = "ecpTransBreakContract";
            _function = "AddUpdateData, rejectcptransbreakcontract";
            _command += "UPDATE ecpTransBreakContract SET " +
                        (_c.Request["actioncomment"].Equals("E") ? "StatusEdit = 2 " : "") +
                        (_c.Request["actioncomment"].Equals("C") ? "StatusCancel = 2, DateTimeCancel = GETDATE() " : "") +                        
                        "WHERE ID = " + _c.Request["cp1id"] + "; " +
                        "INSERT INTO ecpTransReject " +
                        "(BCID, Action, Comment, DateTimeReject)" +
                        "VALUES " +
                        "(" +
                        _c.Request["cp1id"] + ", " +
                        "'" + _c.Request["actioncomment"] + "', " +
                        "'" + _c.Request["comment"] + "', " +
                        "GETDATE()" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("addcptransrequirecontract")) {
            _what = "UPDATE, INSERT";
            _where = "ecpTransBreakContract, ecpTransRequireContract";
            _function = "AddUpdateData, addcptransrequirecontract";
            _command += "UPDATE ecpTransBreakContract SET " +
                        "IndemnitorYear = " + (String.IsNullOrEmpty(_c.Request["indemnitoryear"]) ? "0" : _c.Request["indemnitoryear"]) + ", " +
                        "IndemnitorCash = " + _c.Request["indemnitorcash"] + ", " +
                        "StatusReceiver = 2, " +
                        "DateTimeReceiver = GETDATE() " +
                        "WHERE ID = " + _c.Request["cp1id"] + "; " +
                        "INSERT INTO ecpTransRequireContract ";

            if (_c.Request["casegraduate"].Equals("1")) {
                if (_c.Request["scholar"].Equals("1")) {
                    _command += "(BCID, ActualMonthScholarship, ActualScholarship, TotalPayScholarship, ActualMonth, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber,	LawyerEmail, StatusRepay, StatusPayment, FormatPayment)" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp1id"] + ", " +
                                _c.Request["actualmonthscholarship"] + ", " +
                                _c.Request["actualscholarship"] + ", " +
                                _c.Request["totalpayscholarship"] + ", " +
                                _c.Request["actualmonth"] + ", " +
                                _c.Request["actualday"] + ", " +
                                _c.Request["subtotalpenalty"] + ", " +
                                _c.Request["totalpenalty"] + ", " +
                                "'" + _c.Request["lawyerfullname"] + "', " +
                                (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "'" + _c.Request["lawyeremail"] + "', " +
                                "0, " +
                                "1, " +
                                "0)";
                }
                else {
                    _command += "(BCID, TotalPayScholarship, ActualMonth, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment)" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp1id"] + ", " +
                                _c.Request["totalpayscholarship"] + ", " +
                                _c.Request["actualmonth"] + ", " +
                                _c.Request["actualday"] + ", " +
                                _c.Request["subtotalpenalty"] + ", " +
                                _c.Request["totalpenalty"] + ", " +
                                "'" + _c.Request["lawyerfullname"] + "', " +
                                (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "'" + _c.Request["lawyeremail"] + "', " +
                                "0, " +
                                "1, " +
                                "0)";
                }
            }

            if (_c.Request["casegraduate"].Equals("2")) {
                if (_c.Request["civil"].Equals("1")) {
                    _command += "(BCID, IndemnitorAddress, Province, StudyLeave, RequireDate, ApproveDate, BeforeStudyLeaveStartDate, BeforeStudyLeaveEndDate, StudyLeaveStartDate, StudyLeaveEndDate, AfterStudyLeaveStartDate, AfterStudyLeaveEndDate, TotalPayScholarship, AllActualDate, ActualDate, RemainDate, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment)" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp1id"] + ", " +
                                "'" + _c.Request["indemnitoraddress"] + "', " +
                                "'" + _c.Request["province"] + "', " +
                                "'" + _c.Request["studyleave"] + "', " +
                                (String.IsNullOrEmpty(_c.Request["requiredate"]) ? "NULL" : ("'" + _c.Request["requiredate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["approvedate"]) ? "NULL" : ("'" + _c.Request["approvedate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["beforestudyleavestartdate"]) ? "NULL" : ("'" + _c.Request["beforestudyleavestartdate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["beforestudyleaveenddate"]) ? "NULL" : ("'" + _c.Request["beforestudyleaveenddate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["studyleavestartdate"]) ? "NULL" : ("'" + _c.Request["studyleavestartdate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["studyleaveenddate"]) ? "NULL" : ("'" + _c.Request["studyleaveenddate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["afterstudyleavestartdate"]) ? "NULL" : ("'" + _c.Request["afterstudyleavestartdate"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["afterstudyleaveenddate"]) ? "NULL" : ("'" + _c.Request["afterstudyleaveenddate"] + "'")) + ", " +
                                _c.Request["totalpayscholarship"] + ", " +
                                (String.IsNullOrEmpty(_c.Request["allactualdate"]) ? "NULL" : _c.Request["allactualdate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["actualdate"]) ? "NULL" : _c.Request["actualdate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["remaindate"]) ? "NULL" : _c.Request["remaindate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["actualday"]) ? "NULL" : _c.Request["actualday"]) + ", " +
                                _c.Request["subtotalpenalty"] + ", " +
                                _c.Request["totalpenalty"] + ", " +
                                "'" + _c.Request["lawyerfullname"] + "', " +
                                (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "'" + _c.Request["lawyeremail"] + "', " +
                                "0, " +
                                "1, " +
                                "0)";
                }
                else {
                    _command += "(BCID, TotalPayScholarship, AllActualDate, ActualDate, RemainDate, ActualDay, SubtotalPenalty, TotalPenalty, LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail, StatusRepay, StatusPayment, FormatPayment)" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp1id"] + ", " +
                                _c.Request["totalpayscholarship"] + ", " +
                                (String.IsNullOrEmpty(_c.Request["allactualdate"]) ? "NULL" : _c.Request["allactualdate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["actualdate"]) ? "NULL" : _c.Request["actualdate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["remaindate"]) ? "NULL" : _c.Request["remaindate"]) + ", " +
                                (String.IsNullOrEmpty(_c.Request["actualday"]) ? "NULL" : _c.Request["actualday"]) + ", " +
                                _c.Request["subtotalpenalty"] + ", " +
                                _c.Request["totalpenalty"] + ", " +
                                "'" + _c.Request["lawyerfullname"] + "', " +
                                (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "'" + _c.Request["lawyeremail"] + "', " +
                                "0, " +
                                "1, " +
                                "0)";
                }
            }
        }

        if (_c.Request["cmd"].Equals("updatecptransrequirecontract")) {
            _what = "UPDATE, UPDATE";
            _where = "ecpTransBreakContract, ecpTransRequireContract";
            _function = "AddUpdateData, updatecptransrequirecontract";
            _command += "UPDATE ecpTransBreakContract SET " +
                        "IndemnitorYear = " + (String.IsNullOrEmpty(_c.Request["indemnitoryear"]) ? "0" : _c.Request["indemnitoryear"]) + ", " +
                        "IndemnitorCash = " + _c.Request["indemnitorcash"] + ", " +
                        "StatusReceiver = 2, " +
                        "DateTimeModify = GETDATE() " +
                        "WHERE ID = " + _c.Request["cp1id"] + "; " +
                        "UPDATE ecpTransRequireContract SET ";

            if (_c.Request["casegraduate"].Equals("1")) {
                _command += "ActualMonthScholarship = " + (String.IsNullOrEmpty(_c.Request["actualmonthscholarship"]) ? "NULL" : _c.Request["actualmonthscholarship"]) + ", " +
                            "ActualScholarship = " + (String.IsNullOrEmpty(_c.Request["actualscholarship"]) ? "NULL" : _c.Request["actualscholarship"]) + ", " +
                            "TotalPayScholarship = " + _c.Request["totalpayscholarship"] + ", " +
                            "ActualMonth = " + _c.Request["actualmonth"] + ", " +
                            "ActualDay = " + _c.Request["actualday"] + ", " +
                            "SubtotalPenalty = " + _c.Request["subtotalpenalty"] + ", " +
                            "TotalPenalty = " + _c.Request["totalpenalty"] + ", " +
                            "LawyerFullname = '" + _c.Request["lawyerfullname"] + "', " +
                            "LawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                            "LawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                            "LawyerEmail = '" + _c.Request["lawyeremail"] + "', " +
                            "StatusRepay = 0, " +
                            "StatusPayment = 1, " +
                            "FormatPayment = 0 ";
            }
      
            if (_c.Request["casegraduate"].Equals("2")) {
                if (_c.Request["civil"].Equals("1")) {
                    _command += "IndemnitorAddress = '" + _c.Request["indemnitoraddress"] + "', " +
                                "Province = '" + _c.Request["province"] + "', " +
                                "StudyLeave = '" + _c.Request["studyleave"] + "', " +
                                "RequireDate = " + (String.IsNullOrEmpty(_c.Request["requiredate"]) ? "NULL" : ("'" + _c.Request["requiredate"] + "'")) + ", " +
                                "ApproveDate = " + (String.IsNullOrEmpty(_c.Request["approvedate"]) ? "NULL" : ("'" + _c.Request["approvedate"] + "'")) + ", " +
                                "BeforeStudyLeaveStartDate = " + (String.IsNullOrEmpty(_c.Request["beforestudyleavestartdate"]) ? "NULL" : ("'" + _c.Request["beforestudyleavestartdate"] + "'")) + ", " +
                                "BeforeStudyLeaveEndDate = " + (String.IsNullOrEmpty(_c.Request["beforestudyleaveenddate"]) ? "NULL" : ("'" + _c.Request["beforestudyleaveenddate"] + "'")) + ", " +
                                "StudyLeaveStartDate = " + (String.IsNullOrEmpty(_c.Request["studyleavestartdate"]) ? "NULL" : ("'" + _c.Request["studyleavestartdate"] + "'")) + ", " +
                                "StudyLeaveEndDate = " + (String.IsNullOrEmpty(_c.Request["studyleaveenddate"]) ? "NULL" : ("'" + _c.Request["studyleaveenddate"] + "'")) + ", " +
                                "AfterStudyLeaveStartDate = " + (String.IsNullOrEmpty(_c.Request["afterstudyleavestartdate"]) ? "NULL" : ("'" + _c.Request["afterstudyleavestartdate"] + "'")) + ", " +
                                "AfterStudyLeaveEndDate = " + (String.IsNullOrEmpty(_c.Request["afterstudyleaveenddate"]) ? "NULL" : ("'" + _c.Request["afterstudyleaveenddate"] + "'")) + ", " +
                                "TotalPayScholarship = " + _c.Request["totalpayscholarship"] + ", " +
                                "AllActualDate = " + (String.IsNullOrEmpty(_c.Request["allactualdate"]) ? "NULL" : _c.Request["allactualdate"]) + ", " +
                                "ActualDate = " + (String.IsNullOrEmpty(_c.Request["actualdate"]) ? "NULL" : _c.Request["actualdate"]) + ", " +
                                "RemainDate = " + (String.IsNullOrEmpty(_c.Request["remaindate"]) ? "NULL" : _c.Request["remaindate"]) + ", " +
                                "ActualDay = " + (String.IsNullOrEmpty(_c.Request["actualday"]) ? "NULL" : _c.Request["actualday"]) + ", " +
                                "SubtotalPenalty = " + _c.Request["subtotalpenalty"] + ", " +
                                "TotalPenalty = " + _c.Request["totalpenalty"] + ", " +
                                "LawyerFullname = '" + _c.Request["lawyerfullname"] + "', " +
                                "LawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                "LawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "LawyerEmail = '" + _c.Request["lawyeremail"] + "', " +
                                "StatusRepay = 0, " +
                                "StatusPayment = 1, " +
                                "FormatPayment = 0 ";
                }
                else {
                    _command += "TotalPayScholarship = " + _c.Request["totalpayscholarship"] + ", " +
                                "SubtotalPenalty = " + _c.Request["subtotalpenalty"] + ", " +
                                "TotalPenalty = " + _c.Request["totalpenalty"] + ", " +
                                "LawyerFullname = '" + _c.Request["lawyerfullname"] + "', " +
                                "LawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                                "LawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                                "LawyerEmail = '" + _c.Request["lawyeremail"] + "', " +
                                "StatusRepay = 0, " +
                                "StatusPayment = 1, " +
                                "FormatPayment = 0 ";
                }                        
            }
      
            _command += "WHERE ID = " + _c.Request["cp2id"];
        }

        if (_c.Request["cmd"].Equals("addcptransrepaycontract")) {
            _what = "UPDATE, INSERT";
            _where = "ecpTransRequireContract, ecpTransRepayContract";
            _function = "AddUpdateData, addcptransrepaycontract";
            _command += "UPDATE ecpTransRequireContract SET " +
                        "StatusRepay = " + _c.Request["statusrepay"] + " " +
                        "WHERE ID = " + _c.Request["cp2id"] + "; " +
                        "INSERT INTO ecpTransRepayContract " +
                        "(RCID, StatusRepay, StatusReply, RepayDate)" +
                        "VALUES " +
                        "(" +
                        _c.Request["cp2id"] + ", " +
                        _c.Request["statusrepay"] + ", " +
                        "1, " +
                        "'" + _c.Request["repaydate"] + "'" +
                        ")";
        }

        if (_c.Request["cmd"].Equals("updatecptransrepaycontract")) {
            _what = "UPDATE";
            _where = "ecpTransRepayContract";
            _function = "AddUpdateData, updatecptransrepaycontract";
            _command += "UPDATE ecpTransRepayContract SET ";

            if (!String.IsNullOrEmpty(_c.Request["replydate"]) && !String.IsNullOrEmpty(_c.Request["replyresult"])) {
                _command += "StatusReply = 2, " +
                            "ReplyResult = " + _c.Request["replyresult"] + ", " +
                            "ReplyDate = '" + _c.Request["replydate"] + "', ";
            }
                
            _command += "RepayDate = '" + _c.Request["repaydate"] + "', " +
                        "Pursuant = " + (String.IsNullOrEmpty(_c.Request["pursuant"]) ? "NULL" : ("'" + _c.Request["pursuant"] + "'")) + ", " +
                        "PursuantBookDate = " + (String.IsNullOrEmpty(_c.Request["pursuantbookdate"]) ? "NULL" : ("'" + _c.Request["pursuantbookdate"] + "'")) + " " +
                        "WHERE (RCID = " + _c.Request["cp2id"] + ") AND (StatusRepay = " + _c.Request["statusrepay"] + ")";
        }

        if (_c.Request["cmd"].Equals("updatestatuspaymentrecord")) {
            _what = "UPDATE";
            _where = "ecpTransRequireContract";
            _function = "UpdateData, updatestatuspaymentrecord";
            _command += "UPDATE ecpTransRequireContract SET " +
                        "StatusPaymentRecord = " + (String.IsNullOrEmpty(_c.Request["statuspaymentrecord"]) ? "NULL" : ("'" + _c.Request["statuspaymentrecord"]) + "'") + ", " +
                        "StatusPaymentRecordLawyerFullname = '" + _c.Request["statuspaymentrecordlawyerfullname"] + "', " +
                        "StatusPaymentRecordLawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request["statuspaymentrecordlawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["statuspaymentrecordlawyerphonenumber"] + "'")) + ", " +
                        "StatusPaymentRecordLawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request["statuspaymentrecordlawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["statuspaymentrecordlawyermobilenumber"] + "'")) + ", " +
                        "StatusPaymentRecordLawyerEmail = '" + _c.Request["statuspaymentrecordlawyeremail"] + "' " +
                        "WHERE ID = " + _c.Request["cp2id"];
        }

        if (_c.Request["cmd"].Equals("addcptranspaymentfullrepay")) {
            string capital = _c.Request["capital"];
            /*
            string totalAccruedInterest = _c.Request["totalaccruedinterest"];
            */
            string totalPayment = _c.Request["totalpayment"];
            string totalInterestOverpayment = _c.Request["overpaymenttotalinterest"];
            string pay = _c.Request["pay"];
            string overpay = _c.Request["overpay"];
            string[] payRemain = new string[5];
            string[] channelDetail = new string[2];
            /*
            payRemain = eCPUtil.CalChkBalance(capital, totalInterest, totalAccruedInterest, totalPayment, pay);
            */

            double totalPay;

            totalPay = (
                (!String.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0) +
                (!String.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0) +
                (!String.IsNullOrEmpty(overpay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(overpay))) : 0)
            );

            switch (int.Parse(_c.Request["channel"])) {
                case 1: 
                    channelDetail[0] = "ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
                case 2:
                    channelDetail[0] = "ChequeNo, ChequeBank, ChequeBankBranch, ChequeDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["chequeno"] + "', '" + _c.Request["chequebank"] + "', '" + _c.Request["chequebankbranch"] + "', '" + _c.Request["chequedate"] + "', '" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
                case 3:
                    channelDetail[0] = "CashBank, CashBankBranch, CashBankAccount, CashBankAccountNo, CashBankDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["cashbank"] + "', '" + _c.Request["cashbankbranch"] + "', '" + _c.Request["cashbankaccount"] + "', " + (String.IsNullOrEmpty(_c.Request["cashbankaccountno"]) ? "NULL" : ("'" + _c.Request["cashbankaccountno"] + "'")) + ", '" + _c.Request["cashbankdate"] + "', '" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
            }

            _what = "UPDATE, INSERT";
            _where = "ecpTransRequireContract, ecpTransPayment";
            _function = "AddUpdateData, addcptranspaymentfullrepay";
            _command += "UPDATE ecpTransRequireContract SET " +
                        "StatusPayment = 3, " +
                        "FormatPayment = 1 " +
                        "WHERE ID = " + _c.Request["cp2id"] + "; " +
                        "INSERT INTO ecpTransPayment " +
                        "(RCID, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + channelDetail[0] + ", LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail)" +
                        "VALUES " +
                        "(" +
                        _c.Request["cp2id"] + ", " +
                        totalInterestOverpayment + ", " +
                        "'" + _c.Request["datetimepayment"] + "', " +
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
                        _c.Request["channel"] + ", " +
                        channelDetail[1] + ", " +
                        "'" + _c.Request["lawyerfullname"] + "', " +
                        (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["lawyeremail"]) ? "NULL" : ("'" + _c.Request["lawyeremail"] + "'")) + ")";

            /*
            if (int.Parse(_c.Request["overpayment"]) > 0) {
                if (_c.Request["calinterestyesno"].Equals("Y")) {
                    _command += "(RCID, CalInterestYesNo, OverpaymentDateStart, OverpaymentDateEnd, OverpaymentYear, OverpaymentDay, OverpaymentInterest, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp2id"] + ", " +
                                "'" + _c.Request["calinterestyesno"] + "', " +
                                "'" + _c.Request["overpaymentdatestart"] + "', " +
                                "'" + _c.Request["overpaymentdateend"] + "', " +
                                _c.Request["overpaymentyear"] + ", " +
                                _c.Request["overpaymentday"] + ", " +
                                _c.Request["overpaymentinterest"] + ", " +
                                _c.Request["overpaymenttotalinterest"] + ", " +
                                "'" + _c.Request["datetimepayment"] + "', " +
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
                                channelDetail[1] + ")";
                }

                if (_c.Request["calinterestyesno"].Equals("N")) {
                    _command += "(RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp2id"] + ", " +
                                "'" + _c.Request["calinterestyesno"] + "', " +
                                "'" + _c.Request["datetimepayment"] + "', " +
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
                                _c.Request["channel"] + ", " +
                                channelDetail[1] + ")";
                }
            }

            if (int.Parse(_c.Request["overpayment"]) <= 0) {
                _command += "(RCID, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, Overpay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                            "VALUES " +
                            "(" +
                            _c.Request["cp2id"] + ", " +
                            "'" + _c.Request["datetimepayment"] + "', " +
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
                            _c.Request["channel"] + ", " +
                            channelDetail[1] + ")";
            }
            */
        }

        if (_c.Request["cmd"].Equals("addcptranspaymentpayrepay")) {
            string statusPayment = _c.Request["statuspayment"];            
            string capital = _c.Request["capital"];
            string totalAccruedInterest = _c.Request["totalaccruedinterest"];
            string totalInterestOverpaymentBefore = _c.Request["overpaymenttotalinterestbefore"];
            string totalInterestPayRepay = _c.Request["payrepaytotalinterest"];
            string totalInterestOverpayment = _c.Request["overpaymenttotalinterest"];
            string remainAccruedInterest = _c.Request["remainaccruedinterest"];
            /*
            string totalPayment = _c.Request["totalpayment"];
            */
            string pay = _c.Request["pay"];
            /*
            string[] payRemain = new string[5];
            */
            string[] channelDetail = new string[2];
            /*
            _payRemain = eCPUtil.CalChkBalance(_capital, _totalInterest, _totalAccruedInterest, _totalPayment, _pay);
            */

            double interest;
            double totalPayment;
            double payCapital;
            double payInterest;
            double remainCapital;            

            interest = (
                (!String.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) +
                (!String.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) +
                (!String.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
            );
            totalPayment = (
                (!String.IsNullOrEmpty(capital) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(capital))) : 0) +
                interest
            );
            payCapital = (
                (!String.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0) -
                (!String.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0) -
                (!String.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) -
                (!String.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) -
                (!String.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
            );
            payCapital = (payCapital < 0 ? 0 : payCapital);
            remainCapital = (
                (!String.IsNullOrEmpty(capital) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(capital))) : 0) -
                payCapital
            );
            remainCapital = (remainCapital < 0 ? 0 : remainCapital);
            /*
            remainAccruedInterest = (
                (
                    (!String.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0) +
                    (!String.IsNullOrEmpty(totalInterestOverpaymentBefore) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpaymentBefore))) : 0) +
                    (!String.IsNullOrEmpty(totalInterestPayRepay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestPayRepay))) : 0) +
                    (!String.IsNullOrEmpty(totalInterestOverpayment) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalInterestOverpayment))) : 0)
                ) -
                (!String.IsNullOrEmpty(pay) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(pay))) : 0)
            );
            remainAccruedInterest = (remainAccruedInterest < 0 ? 0 : remainAccruedInterest);
            */
            payInterest = (
                interest +
                (!String.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0)
                /*
                (
                    interest +
                    (!String.IsNullOrEmpty(totalAccruedInterest) ? double.Parse(eCPUtil.DoubleToString2Decimal(double.Parse(totalAccruedInterest))) : 0)
                ) -
                remainAccruedInterest
                */
            );

            switch (int.Parse(_c.Request["channel"])) {
                case 1:
                    channelDetail[0] = "ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
                case 2:
                    channelDetail[0] = "ChequeNo, ChequeBank, ChequeBankBranch, ChequeDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["chequeno"] + "', '" + _c.Request["chequebank"] + "', '" + _c.Request["chequebankbranch"] + "', '" + _c.Request["chequedate"] + "', '" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
                case 3:
                    channelDetail[0] = "CashBank, CashBankBranch, CashBankAccount, CashBankAccountNo, CashBankDate, ReceiptNo, ReceiptBookNo, ReceiptDate, ReceiptSendNo, ReceiptFund, ReceiptCopy";
                    channelDetail[1] = "'" + _c.Request["cashbank"] + "', '" + _c.Request["cashbankbranch"] + "', '" + _c.Request["cashbankaccount"] + "', " + (String.IsNullOrEmpty(_c.Request["cashbankaccountno"]) ? "NULL" : ("'" + _c.Request["cashbankaccountno"] + "'")) + ", '" + _c.Request["cashbankdate"] + "', '" + _c.Request["receiptno"] + "', '" + _c.Request["receiptbookno"] + "', '" + _c.Request["receiptdate"] + "', '" + _c.Request["receiptsendno"] + "', '" + _c.Request["receiptfund"] + "', " + (String.IsNullOrEmpty(_c.Request["receiptcopy"]) ? "NULL" : ("'" + _c.Request["receiptcopy"] + "'"));
                    break;
            }

            _what = "UPDATE, INSERT";
            _where = "ecpTransRequireContract, ecpTransPayment";
            _function = "AddUpdateData, addcptranspaymentpayrepay";
            _command += "UPDATE ecpTransRequireContract SET " +
                        "StatusPayment = " + ((remainCapital.Equals(0) && double.Parse(remainAccruedInterest).Equals(0)) ? "3" : "2") + ", " +
                        "FormatPayment = 2 " +
                        "WHERE ID = " + _c.Request["cp2id"] + "; " +
                        "INSERT INTO ecpTransPayment " +
                        "(RCID, OverpaymentTotalInterestBefore, OverpaymentTotalInterest, PayRepayTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + channelDetail[0] + ", LawyerFullname, LawyerPhoneNumber, LawyerMobileNumber, LawyerEmail)" +
                        "VALUES " +
                        "(" +
                        _c.Request["cp2id"] + ", " +
                        totalInterestOverpaymentBefore + ", " +
                        totalInterestOverpayment + ", " +
                        totalInterestPayRepay + ", " +
                        "'" + _c.Request["datetimepayment"] + "', " +
                        capital + ", " +
                        interest.ToString() + ", " +
                        totalAccruedInterest + ", " +
                        totalPayment.ToString() + ", " +
                        payCapital.ToString() + ", " +
                        payInterest.ToString() + ", " +
                        pay + ", " +
                        remainCapital.ToString() + ", " +
                        "0, " +
                        remainAccruedInterest + ", " +                        
                        remainCapital.ToString() + ", " +
                        _c.Request["channel"] + ", " +
                        channelDetail[1] + ", " +
                        "'" + _c.Request["lawyerfullname"] + "', " +
                        (String.IsNullOrEmpty(_c.Request["lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request["lawyerphonenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request["lawyermobilenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["lawyeremail"]) ? "NULL" : ("'" + _c.Request["lawyeremail"] + "'")) + ")";
            /*
            _what = "UPDATE, INSERT";
            _where = "ecpTransRequireContract, ecpTransPayment";
            _function = "AddUpdateData, addcptranspaymentpayrepay";
            _command += "UPDATE ecpTransRequireContract SET " +
                        "StatusPayment = " + (double.Parse(_payRemain[2]).Equals(0) ? "3" : "2") + ", " +
                        "FormatPayment = 2 " +
                        "WHERE ID = " + _c.Request["cp2id"] + "; " +
                        "INSERT INTO ecpTransPayment ";

            if (statusPayment.Equals("1")) {
                if (int.Parse(c.Request["overpayment"]) > 0) {
                    if (c.Request["calinterestyesno"].Equals("Y")) {
                        command += "(RCID, CalInterestYesNo, OverpaymentDateStart, OverpaymentDateEnd, OverpaymentYear, OverpaymentDay, OverpaymentInterest, OverpaymentTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                    "VALUES " +
                                    "(" +
                                    _c.Request["cp2id"] + ", " +
                                    "'" + _c.Request["calinterestyesno"] + "', " +
                                    "'" + _c.Request["overpaymentdatestart"] + "', " +
                                    "'" + _c.Request["overpaymentdateend"] + "', " +
                                    _c.Request["overpaymentyear"] + ", " +
                                    _c.Request["overpaymentday"] + ", " +
                                    _c.Request["overpaymentinterest"] + ", " +
                                    _c.Request["overpaymenttotalinterest"] + ", " +
                                    "'" + _c.Request["datetimepayment"] + "', " +
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
                                    _c.Request["channel"] + ", " +
                                    channelDetail[1] + ")";
                    }

                    if (_c.Request["calinterestyesno"].Equals("N")) {
                        command += "(RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                    "VALUES " +
                                    "(" +
                                    _c.Request["cp2id"] + ", " +
                                    "'" + _c.Request["calinterestyesno"] + "', " +
                                    "'" + _c.Request["datetimepayment"] + "', " +
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
                                    channelDetail[1] + ")";
                    }
                }
            }

            if (statusPayment.Equals("2") || int.Parse(_c.Request["overpayment"]) <= 0) {
                if (_c.Request["calinterestyesno"].Equals("Y")) {
                    command += "(RCID, CalInterestYesNo, PayRepayDateStart, PayRepayDateEnd, PayRepayYear, PayRepayDay, PayRepayInterest, PayRepayTotalInterest, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp2id"] + ", " +
                                "'" + _c.Request["calinterestyesno"] + "', " +
                                "'" + _c.Request["payrepaydatestart"] + "', " +
                                "'" + _c.Request["payrepaydateend"] + "', " +
                                _c.Request["payrepayyear"] + ", " +
                                _c.Request["payrepayday"] + ", " +
                                _c.Request["payrepayinterest"] + ", " +
                                _c.Request["payrepaytotalinterest"] + ", " +
                                "'" + _c.Request["datetimepayment"] + "', " +
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
                                _c.Request["channel"] + ", " +
                                _channelDetail[1] + ")";
                }

                if (_c.Request["calinterestyesno"].Equals("N")) {
                    command += "(RCID, CalInterestYesNo, DateTimePayment, Capital, Interest, TotalAccruedInterest, TotalPayment, PayCapital, PayInterest, TotalPay, RemainCapital, AccruedInterest, RemainAccruedInterest, TotalRemain, Channel, " + _channelDetail[0] + ")" +
                                "VALUES " +
                                "(" +
                                _c.Request["cp2id"] + ", " +
                                "'" + _c.Request["calinterestyesno"] + "', " +
                                "'" + _c.Request["datetimepayment"] + "', " +
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
                                _c.Request["channel"] + ", " +
                                channelDetail[1] + ")";
                }
            } 
            */
        }

        if (_c.Request["cmd"].Equals("delcptranspayment")) {
            string statusPayment = _c.Request["statuspayment"];
            int period = int.Parse(_c.Request["period"]);
            int periodTotal = int.Parse(_c.Request["periodtotal"]);

            if (periodTotal.Equals(1)) {
                _what = "UPDATE, DELETE";
                _where = "ecpTransRequireContract, ecpTransPayment";
                _function = "AddUpdateData, updatecptransrequirecontract, delcptranspayment";
                _command += "UPDATE ecpTransRequireContract SET " +
                            "StatusPayment = 1, " +
                            "FormatPayment = 0 " +
                            "WHERE ID = " + _c.Request["cp2id"] + "; ";
            }

            if (periodTotal > 1) {
                if (statusPayment.Equals("3")) {
                    _what = "UPDATE, DELETE";
                    _where = "ecpTransRequireContract, ecpTransPayment";
                    _function = "AddUpdateData, updatecptransrequirecontract, delcptranspayment";
                    _command += "UPDATE ecpTransRequireContract SET " +
                                "StatusPayment = 2 " +
                                "WHERE ID = " + _c.Request["cp2id"] + "; ";
                }
                else {
                    _what = "DELETE";
                    _where = "ecpTransPayment";
                    _function = "AddUpdateData, delcptranspayment";
                }
            }

            _command += "DELETE " +
                        "FROM ecpTransPayment " +
                        "WHERE (ID = " + _c.Request["ecpTransPaymentID"] + ") AND (RCID = " + _c.Request["cp2id"] + ")";
        }

        if (_c.Request["cmd"].Equals("addcptransprosecution")) {
            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string _userid = eCPUtil.GetUserID();
            string[] _documentetail = new string[2];

            switch (_c.Request["document"]) {
                case "complaint":
                    _documentetail[0] = "ComplaintLawyerFullname, ComplaintLawyerPhoneNumber, ComplaintLawyerMobileNumber, ComplaintLawyerEmail, ComplaintBlackCaseNo, ComplaintCapital, ComplaintInterest, ComplaintActionDate, ComplaintActionBy, ComplaintActionIP";
                    _documentetail[1] = 
                        (String.IsNullOrEmpty(_c.Request["complaintblackcaseno"]) ? "NULL" : ("'" + _c.Request["complaintblackcaseno"] + "'")) + ", " + 
                        (String.IsNullOrEmpty(_c.Request["complaintcapital"]) ? "NULL" : _c.Request["complaintcapital"]) + ", " + 
                        (String.IsNullOrEmpty(_c.Request["complaintinterest"]) ? "NULL" : _c.Request["complaintinterest"]) + ", " +
                        "GETDATE(), " +
                        "'" + _userid + "', " +
                        "dbo.fnc_GetIP()";
                    break;
                case "judgment":
                    _documentetail[0] = "JudgmentLawyerFullname, JudgmentLawyerPhoneNumber, JudgmentLawyerMobileNumber, JudgmentLawyerEmail, JudgmentRedCaseNo, JudgmentVerdict, JudgmentCopy, JudgmentRemark, JudgmentActionDate, JudgmentActionBy, JudgmentActionIP";
                    _documentetail[1] =
                        (String.IsNullOrEmpty(_c.Request["judgmentredcaseno"]) ? "NULL" : ("'" + _c.Request["judgmentredcaseno"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["judgmentverdict"]) ? "NULL" : ("'" + _c.Request["judgmentverdict"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["judgmentcopy"]) ? "NULL" : ("'" + _c.Request["judgmentcopy"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["judgmentremark"]) ? "NULL" : ("'" + _c.Request["judgmentremark"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + _userid + "', " +
                        "dbo.fnc_GetIP()";
                    break;
                case "execution":
                    _documentetail[0] = "ExecutionLawyerFullname, ExecutionLawyerPhoneNumber, ExecutionLawyerMobileNumber, ExecutionLawyerEmail, ExecutionDate, ExecutionCopy, ExecutionActionDate, ExecutionActionBy, ExecutionActionIP";
                    _documentetail[1] =
                        (String.IsNullOrEmpty(_c.Request["executiondate"]) ? "NULL" : ("'" + _c.Request["executiondate"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["executioncopy"]) ? "NULL" : ("'" + _c.Request["executioncopy"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + _userid + "', " +
                        "dbo.fnc_GetIP()";
                    break;
                case "executionwithdraw":
                    _documentetail[0] = "ExecutionWithdrawLawyerFullname, ExecutionWithdrawLawyerPhoneNumber, ExecutionWithdrawLawyerMobileNumber, ExecutionWithdrawLawyerEmail, ExecutionWithdrawDate, ExecutionWithdrawReason, ExecutionWithdrawCopy, ExecutionWithdrawActionDate, ExecutionWithdrawActionBy, ExecutionWithdrawActionIP";
                    _documentetail[1] =
                        (String.IsNullOrEmpty(_c.Request["executionwithdrawdate"]) ? "NULL" : ("'" + _c.Request["executionwithdrawdate"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["executionwithdrawreason"]) ? "NULL" : ("'" + _c.Request["executionwithdrawreason"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request["executionwithdrawcopy"]) ? "NULL" : ("'" + _c.Request["executionwithdrawcopy"] + "'")) + ", " +
                        "GETDATE(), " +
                        "'" + _userid + "', " +
                        "dbo.fnc_GetIP()";
                    break;
            }

            _what = "INSERT";
            _where = "ecpTransProsecution";
            _function = "AddUpdateData, addcptransprosecution";
            _command += "INSERT INTO ecpTransProsecution " +
                        "(RCID, " + _documentetail[0] + ") " +
                        "VALUES " +
                        "(" +
                        _c.Request["cp2id"] + ", " +
                        "'" + _c.Request[_c.Request["document"] + "lawyerfullname"] + "', " +
                        (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "'" + _c.Request[_c.Request["document"] + "lawyeremail"] + "', " +
                        _documentetail[1] +
                        ")";
        }


        if (_c.Request["cmd"].Equals("updatecptransprosecution")) {
            HttpCookie _eCPCookie = new HttpCookie("eCPCookie");
            _eCPCookie = HttpContext.Current.Request.Cookies["eCPCookie"];

            string _userid = eCPUtil.GetUserID();
            string _documentetail = String.Empty;

            switch (_c.Request["document"]) {
                case "complaint":
                    _documentetail =
                        "ComplaintLawyerFullname = '" + _c.Request[_c.Request["document"] + "lawyerfullname"] + "', " +
                        "ComplaintLawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ComplaintLawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ComplaintLawyerEmail = '" + _c.Request[_c.Request["document"] + "lawyeremail"] + "', " +
                        "ComplaintBlackCaseNo = " + (String.IsNullOrEmpty(_c.Request["complaintblackcaseno"]) ? "NULL" : ("'" + _c.Request["complaintblackcaseno"] + "'")) + ", " +
                        "ComplaintCapital = " + (String.IsNullOrEmpty(_c.Request["complaintcapital"]) ? "NULL" : _c.Request["complaintcapital"]) + ", " +
                        "ComplaintInterest = " + (String.IsNullOrEmpty(_c.Request["complaintinterest"]) ? "NULL" : _c.Request["complaintinterest"]) + ", " +
                        "ComplaintActionDate = GETDATE(), " +
                        "ComplaintActionBy = '" + _userid + "', " +
                        "ComplaintActionIP = dbo.fnc_GetIP()";
                    break;
                case "judgment":
                    _documentetail =
                        "JudgmentLawyerFullname = '" + _c.Request[_c.Request["document"] + "lawyerfullname"] + "', " +
                        "JudgmentLawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "JudgmentLawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "JudgmentLawyerEmail = '" + _c.Request[_c.Request["document"] + "lawyeremail"] + "', " +
                        "JudgmentRedCaseNo = " + (String.IsNullOrEmpty(_c.Request["judgmentredcaseno"]) ? "NULL" : ("'" + _c.Request["judgmentredcaseno"] + "'")) + ", " +
                        "JudgmentVerdict = " + (String.IsNullOrEmpty(_c.Request["judgmentverdict"]) ? "NULL" : ("'" + _c.Request["judgmentverdict"] + "'")) + ", " +
                        "JudgmentCopy = " + (String.IsNullOrEmpty(_c.Request["judgmentcopy"]) ? "NULL" : ("'" + _c.Request["judgmentcopy"] + "'")) + ", " +
                        "JudgmentRemark = " + (String.IsNullOrEmpty(_c.Request["judgmentremark"]) ? "NULL" : ("'" + _c.Request["judgmentremark"] + "'")) + ", " +
                        "JudgmentActionDate = GETDATE(), " +
                        "JudgmentActionBy = '" + _userid + "', " +
                        "JudgmentActionIP = dbo.fnc_GetIP()";
                    break;
                case "execution":
                    _documentetail =
                        "ExecutionLawyerFullname = '" + _c.Request[_c.Request["document"] + "lawyerfullname"] + "', " +
                        "ExecutionLawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ExecutionLawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ExecutionLawyerEmail = '" + _c.Request[_c.Request["document"] + "lawyeremail"] + "', " +
                        "ExecutionDate = " + (String.IsNullOrEmpty(_c.Request["executiondate"]) ? "NULL" : ("'" + _c.Request["executiondate"] + "'")) + ", " +
                        "ExecutionCopy = " + (String.IsNullOrEmpty(_c.Request["executioncopy"]) ? "NULL" : ("'" + _c.Request["executioncopy"] + "'")) + ", " +
                        "ExecutionActionDate = GETDATE(), " +
                        "ExecutionActionBy = '" + _userid + "', " +
                        "ExecutionActionIP = dbo.fnc_GetIP()";
                    break;
                case "executionwithdraw":
                    _documentetail =
                        "ExecutionWithdrawLawyerFullname = '" + _c.Request[_c.Request["document"] + "lawyerfullname"] + "', " +
                        "ExecutionWithdrawLawyerPhoneNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyerphonenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyerphonenumber"] + "'")) + ", " +
                        "ExecutionWithdrawLawyerMobileNumber = " + (String.IsNullOrEmpty(_c.Request[_c.Request["document"] + "lawyermobilenumber"]) ? "NULL" : ("'" + _c.Request[_c.Request["document"] + "lawyermobilenumber"] + "'")) + ", " +
                        "ExecutionWithdrawLawyerEmail = '" + _c.Request[_c.Request["document"] + "lawyeremail"] + "', " +
                        "ExecutionWithdrawDate = " + (String.IsNullOrEmpty(_c.Request["executionwithdrawdate"]) ? "NULL" : ("'" + _c.Request["executionwithdrawdate"] + "'")) + ", " +
                        "ExecutionWithdrawReason = " + (String.IsNullOrEmpty(_c.Request["executionwithdrawreason"]) ? "NULL" : ("'" + _c.Request["executionwithdrawreason"] + "'")) + ", " +
                        "ExecutionWithdrawCopy = " + (String.IsNullOrEmpty(_c.Request["executionwithdrawcopy"]) ? "NULL" : ("'" + _c.Request["executionwithdrawcopy"] + "'")) + ", " +
                        "ExecutionWithdrawActionDate = GETDATE(), " +
                        "ExecutionWithdrawActionBy = '" + _userid + "', " +
                        "ExecutionWithdrawActionIP = dbo.fnc_GetIP()";
                    break;
            }

            _what = "UPDATE";
            _where = "ecpTransProsecution";
            _function = "AddUpdateData, updatecptransprosecution";
            _command += "UPDATE ecpTransProsecution SET " +
                        _documentetail + " " +
                        "WHERE RCID = " + _c.Request["cp2id"];
        }

        _command = _command + "; " + InsertTransactionLog(_what, _where, _function, _command.Replace("'", "''"));

        ConnectStoreProcAddUpdate(_command);
    }
}