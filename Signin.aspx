<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signin.aspx.cs" Inherits="Signin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>เข้าสู่ระบบคำนวณเงินผิดสัญญาการเป็นนักศึกษา</title>
    <link rel="Shortcut Icon" href="Image/MUFavicon.ico" />
    <link href="jQuery/css/ui-lightness/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/Slide.css" rel="stylesheet" type="text/css" />    
    <script src="jQuery/js/jquery-1.7.1.min.js" language="javascript" type="text/javascript"></script>
    <script src="jQuery/js/jquery-ui-1.8.17.custom.min.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/Util.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/Slide.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPUtil.js" language="javascript" type="text/javascript"></script>    
</head>
<body>
<div class="body-main" align="center">
    <div class="main-indent">
        <div class="content-layout">
            <div id="top-page"></div>
            <div id="content-content"></div>
        </div>
    </div>
</div>
<div class="body-footer" align="center"><div class="footer-content"></div></div>
<div id="dialog-loading" style="display:none"></div>
<div id="dialog-message" style="display:none"></div>
<div id="dialog-form1" style="display:none;"></div>
</body>
<script language="javascript" type="text/javascript">
    $(window).resize(function () {
        $("#dialog-loading").dialog("option", "position", "center");
        $("#dialog-message").dialog("option", "position", "center");
        $("#dialog-form1").dialog("option", "position", "center");
    });

    $(LoadSignin());
</script>
</html>
