<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ระบบคำนวณเงินผิดสัญญาการเป็นนักศึกษา</title>
    <link rel="Shortcut Icon" href="Image/MUFavicon.png" />
    <link href="jQuery/css/ui-lightness/jquery-ui-1.8.17.custom.css" rel="stylesheet" type="text/css" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/Slide.css" rel="stylesheet" type="text/css" />
    <script src="jQuery/js/jquery-1.7.1.min.js" language="javascript" type="text/javascript"></script>
    <script src="jQuery/js/jquery-ui-1.8.17.custom.min.js" language="javascript" type="text/javascript"></script>
    <script src="jQuery/js/jquery-ui-1.8.16.offset.datepicker.min.js" language="javascript" type="text/javascript"></script>
    <script src="jQuery/js/jquery-slimScroll.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/Autocomplete.js" language="javascript" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/5.0.3/jquery.inputmask.min.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/Util.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/Slide.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPUtil.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataFormSearch.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataUser.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataConfigulation.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataBreakContract.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataRequireContract.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataRepay.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataPayment.js" language="javascript" type="text/javascript"></script>
    <script src="JScript/eCPDataReport.js" language="javascript" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.min.js" language="javascript" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.8.335/pdf.sandbox.min.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <div class="body-main" align="center">
        <div class="main-indent">
            <div class="content-layout">            
                <div id="sticky">
                    <div class="head" id="head-content"></div>
                    <div class="menu-bar-main" id="menu-bar-content"></div>
                </div>
                <div id="top-page"></div>
                <div class="content" id="content-content"></div>
            </div>
        </div>
    </div>
    <div class="body-footer" align="center"><div class="footer-content"></div></div>
    <div id="dialog-loading" style="display:none"></div>
    <div id="dialog-message" style="display:none"></div>
    <div id="dialog-confirm" style="display:none"></div>
    <div id="dialog-form1" style="display:none;"></div>
    <div id="dialog-form2" style="display:none"></div>
    <div id="dialog-form3" style="display:none"></div>
</body>
<script language="javascript" type="text/javascript">
    $(window).resize(function () {
        $("#dialog-loading").dialog("option", "position", "center");
        $("#dialog-message").dialog("option", "position", "center");
        $("#dialog-confirm").dialog("option", "position", "center");
        $("#dialog-form1").dialog("option", "position", "center");
        $("#dialog-form2").dialog("option", "position", "center");
        $("#dialog-form3").dialog("option", "position", "center");

        $(SetPositionDialogForm("1"));
        $(SetPositionDialogForm("2"));
        $(SetPositionDialogForm("3"));

        if ($(".ui-autocomplete.ui-menu").is(":visible"))
            $(".ui-autocomplete.ui-menu").hide();

        if ($(".ui-datepicker").is(":visible"))
            $(".ui-datepicker").hide();

        $(RemoveSticky());
    });
    
    $(SetPage());
</script>
</html>
