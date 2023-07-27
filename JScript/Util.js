var msgLoading
var monthNames = ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"];

function SetMsgLoading(val) {
    msgLoading = val;
}

function LoadAjax(
    param,
    url,
    method,
    loading,
    close,
    callbackFunc
) {
    $.ajax({
        beforeSend: function () {
            if (loading == true)
                DialogLoading(msgLoading);
        },
        /*
        complete: function () {
            if (close == true)
                $("#dialog-loading").dialog("close");
        },
        */
        async: true,
        type: method,
        url: url,
        data: param,
        dataType: "html",
        charset: "utf-8",
        success: function (data) {
            if (close == true)
                $("#dialog-loading").dialog("close");

            var dataErrorBrowser = data.split("<errorbrowser>");
            var error = false;
            var msg;

            if (error == false &&
                dataErrorBrowser[1] == "1") {
                error = true;
                msg = "ไม่สนับสนุน IE6, IE7 และ IE8";
            }

            if (error == false &&
                dataErrorBrowser[1] == "2") {
                error = true;
                msg = "ไม่ได้เปิดใช้งาน Cookies";
            }

            if (error == true) {
                DialogMessage(msg, "", false, "");
                return;
            }

            callbackFunc(data);
        },
        error: function (
            xhr,
            ajaxOptions,
            thrownError
        ) {
            DialogConfirm("ประมวลผลไม่สำเร็จ");
            $("#dialog-confirm").dialog({
                buttons: {
                    "ตกลง": function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    location.reload();
                }
            });
        }
    });
}

function Trim(id) {
    return $("#" + id).val($.trim($("#" + id).val()));
}

function TextToEntities(text) {
    var entities = "";

    for (var i = 0; i < text.length; i++) {
        if (text.charAt(i) == "&") {
            entities += "%26";
        }
        else {
            if (text.charAt(i) == "+") {
                entities += "%2b";
            }
            else
                entities += text.charAt(i);
        }
    }

    return entities;
}

function IsEnglishCharacter(strString) {
    var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var strChar;
    var blnResult = true;

    if (strString.length == 0)
        return false;

    for (var i = 0; i < strString.length && blnResult == true; i++) {
        strChar = strString.charAt(i);

        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }

    return blnResult;
}

function IsNumeric(strString) {
    var strValidChars = "0123456789";
    var strChar;
    var blnResult = true;

    if (strString.length == 0)
        return false;

    for (var i = 0; i < strString.length && blnResult == true; i++) {
        strChar = strString.charAt(i);

        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }

    return blnResult;
}

function EmailCheck(emailStr) {
    var emailPat = /^(.+)@(.+)$/;
    var specialChars = "\\(\\)<>@,;:\\\\\\\"\\.\\[\\]";
    var validChars = ("\[^\\s" + specialChars + "\]");
    var quotedUser = "(\"[^\"]*\")";
    var ipDomainPat = /^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
    var atom = (validChars + '+');
    var word = ("(" + atom + "|" + quotedUser + ")");
    var userPat = new RegExp("^" + word + "(\\." + word + ")*$");
    var domainPat = new RegExp("^" + atom + "(\\." + atom + ")*$");
    var matchArray = emailStr.match(emailPat);

    if (matchArray == null) {
        return false;
    }

    var user = matchArray[1];
    var domain = matchArray[2];

    if (user.match(userPat) == null) {
        return false;
    }

    var ipArray = domain.match(ipDomainPat);

    if (ipArray != null) {
        for (var i = 1; i <= 4; i++) {
            if (ipArray[i] > 255) {
                return false;
            }
        }

        return true;
    }

    var domainArray = domain.match(domainPat);


    if (domainArray == null) {
        return false;
    }

    var atomPat = new RegExp(atom, "g");
    var domArr = domain.match(atomPat);
    var len = domArr.length;

    if (domArr[domArr.length - 1].length < 2 ||
        domArr[domArr.length - 1].length > 3) {
        return false;
    }

    if (len < 2) {
        return false
    }

    return true;
}

function UrlCheck(urlStr) {
    var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;

    return RegExp.test(urlStr);
}

function DaysInFebruary(year) {
    return ((year % 4 == 0) && (!(year % 100 == 0) || (year % 400 == 0)) ? 29 : 28);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31;

        if (i == 4 ||
            i == 6 ||
            i == 9 ||
            i == 11) {
            this[i] = 30;
        }

        if (i == 2) {
            this[i] = 29;
        }
    }

    return this;
}

function IsDate(
    day,
    month,
    year
) {
    var daysInMonth = DaysArray(12);
    var date = (day + "-" + month + "-" + year);

    if (date != "00-00-0000") {
        if (day == "00")
            return false;

        if (month == "00")
            return false;

        if (year == "0000")
            return false;

        if ((parseInt(month) == 2 && day > DaysInFebruary(year)) ||
            day > daysInMonth[parseInt(month)])
            return false;
    }

    return true;
}

function ResetFrm() {
    $("input:text").val("");
    $("input:hidden").val("");
    $("input:password").val("");
    $("input:radio").attr("checked", false);
    $("input:checkbox").attr("checked", false);
}

function InitTextSelect() {
    $(function () {
        $("input:text").focus(function () {
            if ($(this).is(":disabled") == false)
                $(this).select();
        });
        $("input:password").focus(function () {
            if ($(this).is(":disabled") == false)
                $(this).select();
        });
        $("input:text").mouseup(function (e) {
            if ($(this).is(":disabled") == false)
                e.preventDefault();
        });
        $("input:password").mouseup(function (e) {
            if ($(this).is(":disabled") == false)
                e.preventDefault();
        });
    });
}

function StoreCaret(text) {
    if (typeof (text.createTextRange) != "undefined")
        text.caretPos = document.selection.createRange().duplicate();
}

function ReplaceText(
    text,
    textArea
) {
    if (typeof (textArea.caretPos) != "undefined" &&
        textArea.createTextRange) {
        var caretPos = textArea.caretPos;

        caretPos.text = (caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? (text + ' ') : text);
        caretPos.select();
    }
    else {
        if (typeof (textArea.selectionStart) != "undefined") {
            var begin = textArea.value.substr(0, textArea.selectionStart);
            var end = textArea.value.substr(textArea.selectionEnd);
            var scrollPos = textArea.scrollTop;

            textArea.value = (begin + text + end);

            if (textArea.setSelectionRange) {
                textArea.focus();
                textArea.setSelectionRange((begin.length + text.length), (begin.length + text.length));
            }

            textArea.scrollTop = scrollPos;
        }
        else {
            textArea.value += text;
            textArea.focus(textArea.value.length - 1);
        }
    }
}

function SurroundText(
    text1,
    text2,
    textArea
) {
    if (typeof (textArea.caretPos) != "undefined" &&
        textArea.createTextRange) {
        var caretPos = textArea.caretPos, tempLength = caretPos.text.length;

        caretPos.text = (caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? (text1 + caretPos.text + text2 + ' ') : (text1 + caretPos.text + text2));

        if (tempLength == 0) {
            caretPos.moveStart("character", -text2.length);
            caretPos.moveEnd("character", -text2.length);
            caretPos.select();
        }
        else
            textArea.focus(caretPos);
    }
    else {
        if (typeof (textArea.selectionStart) != "undefined") {
            var begin = textArea.value.substr(0, textArea.selectionStart);
            var selection = textArea.value.substr(textArea.selectionStart, (textArea.selectionEnd - textArea.selectionStart));
            var end = textArea.value.substr(textArea.selectionEnd);
            var newCursorPos = textArea.selectionStart;
            var scrollPos = textArea.scrollTop;

            textArea.value = (begin + text1 + selection + text2 + end);

            if (textArea.setSelectionRange) {
                if (selection.length == 0)
                    textArea.setSelectionRange((newCursorPos + text1.length), (newCursorPos + text1.length));
                else
                    textArea.setSelectionRange(newCursorPos, (newCursorPos + text1.length + selection.length + text2.length));

                textArea.focus();
            }

            textArea.scrollTop = scrollPos;
        }
        else {
            textArea.value += (text1 + text2);
            textArea.focus(textArea.value.length - 1);
        }
    }
}

function SelectToList(
    objChoose,
    objList
) {
    var choose = $("#" + objChoose).val();

    if (choose.length > 0) {
        var obj = $("#" + objList);

        if (obj.length > 0) {
            for (var i = 0; i < obj.length; i++) {
                if (choose == obj.options[i].value)
                    return;
            }
        }

        var opt = document.createElement("OPTION");
        obj.options.add(opt);
        opt.innerHTML = choose;
        opt.value = choose;
    }
}

function ClearList(
    obj,
    mode
) {
    var objCombo = $("#" + obj);

    if (mode == 1) {
        if (objCombo.options.selectedIndex >= 0)
            objCombo.options[objCombo.options.selectedIndex] = null;
    }

    if (mode == 2) {
        for (var i = (objCombo.length - 1); i >= 0; i--) {
            objCombo.options[i] = null;
        }
    }
}

function DeleteList(obj) {
    var objCombo = $("#" + obj);

    if (objCombo.options.selectedIndex >= 0)
        objCombo.options[objCombo.options.selectedIndex] = null;
}

function GroupValueList(obj) {
    var result = "";

    for (var i = 0; i < obj.length; i++) {
        if (result.length > 0) {
            result = (result + ";" + obj.options[i].value);
        }
        else {
            result = obj.options[i].value;
        }
    }

    return result;
}

function CopyList(
    objSource,
    objDestination
) {
    ClearList(objDestination, 2);

    objCombo = $("#" + objSource);

    for (var i = 0; i < objCombo.length; i++) {
        var tmp = objCombo.options[i].value;
        var data = tmp.split(";");
        var obj = $("#" + objDestination);
        var opt = document.createElement("OPTION");
        obj.options.add(opt);
        opt.innerHTML = data[1];
        opt.value = data[0];
    }
}

function GetPageSize() {
    var xPage, yPage;

    if (self.innerHeight) {
        xPage = self.innerWidth;
        yPage = self.innerHeight;
    }
    else {
        if (document.documentElement &&
            document.documentElement.clientHeight) {
            xPage = document.documentElement.clientWidth;
            yPage = document.documentElement.clientHeight;
        }
        else {
            if (document.body) {
                xPage = document.body.clientWidth;
                yPage = document.body.clientHeight;
            }
        }
    }

    return new Array(xPage, yPage);
}

function GetPageScroll() {
    var yScroll;

    if (self.pageYOffset) {
        yScroll = self.pageYOffset;
    }
    else {
        if (document.documentElement &&
            document.documentElement.scrollTop) {
            yScroll = document.documentElement.scrollTop;
        }
        else {
            if (document.body) {
                yScroll = document.body.scrollTop;
            }
        }
    }

    return new Array("", yScroll);
}

function GoToElement(anchor) {
    var ele = $("#" + anchor);
    var offset = ele.offset();
    
    if (ele.length > 0)
        $("html, body").animate({
            scrollTop: (offset.top - 154)
        }, 500);
}

function GoToTopElement(anchor) {
    $(anchor).animate({
        scrollTop: 0
    }, 500);
}

function InitCombobox(
    id,
    valueDefault,
    valueNew,
    widthInput,
    widthList
) {
    $(document).ready(function () {
        $("#" + id).val(valueDefault);
        $("#" + id).combobox();
        $("." + id + "-combobox-input").css({
            background: "#FFFFFF",
            width: widthInput + "px"
        });
        $("." + id + "-combobox-input").bind("autocompleteopen", function (event, ui) {
            $(".ui-autocomplete.ui-menu").width(widthList + "px");
        });
    });

    ComboboxSetSelectedValue(id, valueNew);
    InitComboboxOnClick(id);
    InitTextSelect();
}

function TextboxDisable(id) {
    $(id).attr("readonly", "readonly");
    $(id).addClass("textbox-disable");
}

function TextboxEnable(id) {
    $(id).removeAttr("readonly");
    $(id).removeClass("textbox-disable");
}

function ComboboxDisable(id) {
    $("." + id + "-combobox-input").css({
        background: "#CCCCCC",
        color: "#000000"
    });
    $("." + id + "-combobox-input").attr("readonly", "readonly");
    $("." + id + "-combobox-input").autocomplete({
        disabled: true
    });
}

function ComboboxEnable(id) {
    $("." + id + "-combobox-input").css({
        background: "#FFFFFF",
        color: "#000000"
    });    
    $("." + id + "-combobox-input").removeAttr("readonly");
    $("." + id + "-combobox-input").autocomplete({
        disabled: false
    });
}

function ComboboxSetSelectedValue(
    id,
    value
) {
    $("#" + id).val(value);
    $("." + id + "-combobox-input").val($("#" + id + " option:selected").text());
}

function ComboboxGetSelectedValue(id) {
    var val = ($("#" + id + " option:selected").text().toLowerCase() == $("." + id + "-combobox-input").val().toLowerCase() ? $("#" + id).val() : null);

    return val;
}

function HiddenButton() {
    $("#clear-bottom .form-label-discription-style").addClass("clear-bottom");
    $("#clear-bottom .form-input-style").addClass("clear-bottom");
    $(".button").hide();
}

function ButtonDisable(
    idButton,
    classButtonDisable
) {
    $(idButton).addClass(classButtonDisable);
    $(idButton + " a").removeAttr("onclick");
}

function LinkDisable(idLink) {
    $(idLink).addClass("link-disable");
    $(idLink).removeAttr("onclick");
}

function CalendarDisable(id) {
    TextboxDisable(id);
    $(id).datepicker("disable");
}

function CalendarEnable(id) {
    TextboxEnable(id);
    $(id).attr("readonly", "true");
    $(id).datepicker("enable");
}

function InitCalendar(calendarID) {
    $(document).ready(function () {
        $(calendarID).datepicker("destroy");
        $(calendarID).datepicker({
            buttonImage: "Image/DatePicker.png",
            buttonImageOnly: true,
            showOn: "button",
            prevText: "",
            nextText: "",
            changeMonth: true,
            changeYear: true,
            yearRange: "-26:+1",
            dateFormat: "dd/mm/yy",
            isBuddhist: true,
            dayNames: ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์"],
            dayNamesMin: ["อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."],
            monthNames: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"],
            monthNamesShort: ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."]
        });
        $("img.ui-datepicker-trigger").css({
            "cursor": "pointer",
            "vertical-align": "bottom",
            "margin-left": "2px"
        });
    });
}

function InitCalendarFromTo(
    fromID,
    fromFix,
    toID,
    toFix
) {
    $(document).ready(function () {
        if (fromFix == false) {
            $(fromID).datepicker("destroy");
            $(fromID).datepicker({
                buttonImage: "Image/DatePicker.png",
                buttonImageOnly: true,
                showOn: "button",
                prevText: "",
                nextText: "",
                changeMonth: true,
                changeYear: true,
                yearRange: "-26:+1",
                dateFormat: "dd/mm/yy",
                isBuddhist: true,
                dayNames: ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์"],
                dayNamesMin: ["อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."],
                monthNames: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"],
                monthNamesShort: ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."],
                beforeShow: function () {
                    if ($(toID).val().length > 0) {
                        $(fromID).datepicker("option", "maxDate", $(toID).val()); 
                        $("img.ui-datepicker-trigger").css({
                            "cursor": "pointer",
                            "vertical-align": "bottom",
                            "margin-left": "2px"
                        });
                    }
                },
                /*
                onSelect: function (selectedDate) {
                    if (toFix == false) {
                        $(toID).datepicker("option", "minDate", selectedDate);
                        $("img.ui-datepicker-trigger").css({
                            "cursor": "pointer",
                            "vertical-align": "bottom",
                            "margin-left": "2px"
                        });
                    }
                }
                */
            });
            $("img.ui-datepicker-trigger").css({
                "cursor": "pointer",
                "vertical-align": "bottom",
                "margin-left": "2px"
            });
        }
        
        if (toFix == false) {
            $(toID).datepicker("destroy");
            $(toID).datepicker({
                buttonImage: "Image/DatePicker.png",
                buttonImageOnly: true,
                showOn: "button",
                prevText: "",
                nextText: "",
                changeMonth: true,
                changeYear: true,
                yearRange: "-26:+1",
                dateFormat: "dd/mm/yy",
                isBuddhist: true,
                dayNames: ["อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์"],
                dayNamesMin: ["อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."],
                monthNames: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"],
                monthNamesShort: ["ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค."],
                beforeShow: function () {
                    if ($(fromID).val().length > 0) {
                        $(toID).datepicker("option", "minDate", $(fromID).val());
                        $("img.ui-datepicker-trigger").css({
                            "cursor": "pointer",
                            "vertical-align": "bottom",
                            "margin-left": "2px"
                        });
                    }
                },
                /*
                onSelect: function (selectedDate) {
                    if (fromFix == false) {
                        $(fromID).datepicker("option", "maxDate", selectedDate);
                        $("img.ui-datepicker-trigger").css({
                            "cursor": "pointer",
                            "vertical-align": "bottom",
                            "margin-left": "2px"
                        });
                    }
                }
                */
            });
            $("img.ui-datepicker-trigger").css({
                "cursor": "pointer",
                "vertical-align": "bottom",
                "margin-left": "2px"
            });
        }
    });
}

function StickyRelocate() {
    var windowTop = $(window).scrollTop();
    var divTop = $("#sticky-anchor").offset().top;

    windowTop = (windowTop + 35);

    if (windowTop >= divTop) {
        $("#sticky").addClass("stick");
    }
    else
        $("#sticky").removeClass("stick");
}

function RemoveSticky() {
    if ($("#sticky-anchor").length > 0 &&
        $("#sticky").length > 0)
        $("#sticky").removeClass("stick").next().css("padding-top", "0px");

    GoToElement("top-page");
}    

function InitSticky() {
    if ($("#sticky-anchor").length > 0 &&
        $("#sticky").length > 0) {
        RemoveSticky();

        $(document).ready(function () {
            var aboveHeight = (($(".head").outerHeight() + $(".menu-bar-main").outerHeight() + $(".content-data-head").outerHeight()) + 10);

            $(window).scroll(function () {
                if ($(window).scrollTop() > aboveHeight) {
                    $("#sticky").addClass("stick").next().css("padding-top", $("#sticky").outerHeight() + "px");
                }
                else {
                    $("#sticky").removeClass("stick").next().css("padding-top", "0px");
                }
            });
        });
    }
}

function ExtractNumber(
    obj,
    decimalPlaces,
    allowNegative
) {
    var temp = obj.value;
    var reg0Str = "[0-9]*";

    if (decimalPlaces > 0) {
        reg0Str += ("\\.?[0-9]{0," + decimalPlaces + "}");
    }
    else
        if (decimalPlaces < 0) {
            reg0Str += "\\.?[0-9]*";
        }
    
    reg0Str = (allowNegative ? ("^-?" + reg0Str) : ("^" + reg0Str));
    reg0Str = (reg0Str + "$");
    var reg0 = new RegExp(reg0Str);

    if (reg0.test(temp))
        return true;
    
    var reg1Str = ("[^0-9" + (decimalPlaces != 0 ? "." : "") + (allowNegative ? "-" : "") + "]");
    var reg1 = new RegExp(reg1Str, "g");

    temp = temp.replace(reg1, "");

    if (allowNegative) {
        var hasNegative = (temp.length > 0 && temp.charAt(0) == "-");
        var reg2 = /-/g;

        temp = temp.replace(reg2, "");
        
        if (hasNegative)
            temp = ("-" + temp);
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);

        if (reg3Array != null) {
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);

            reg3Right = reg3Right.replace(reg3, "");
            reg3Right = (decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right);
            temp = (temp.substring(0, reg3Array.index) + "." + reg3Right);
        }
    }

    obj.value = temp;
}

function BlockNonNumbers(
    obj,
    e,
    allowDecimal,
    allowNegative
) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else
        if (e.which) {
            key = e.which;
            isCtrl = e.ctrlKey;
        }

    if (isNaN(key))
        return true;

    keychar = String.fromCharCode(key);

    if (key == 8 ||
        isCtrl)
        return true;

    reg = /\d/;

    var isFirstN = (allowNegative ? (keychar == "-" && obj.value.indexOf("-") == -1) : false);
    var isFirstD = (allowDecimal ? (keychar == "." && obj.value.indexOf(".") == -1) : false);

    return isFirstN || isFirstD || reg.test(keychar);
}

function AddCommas(
    obj,
    decimalPlaces
) {
    var nStr = (parseFloat(DelCommas(obj).length > 0 ? DelCommas(obj) : "0").toString());
    nStr += "";    
    var x = nStr.split(".");
    x1 = x[0];
    var i, j;
    var x2 = (x.length > 1 ? ("." + x[1]) : "");

    if (x2.length > 0)
        x1 = (x1.length == 0 ? "0" : x1);

    if (parseInt(x1) == 0)
        x1 = "0";

    if (x1.length > 0 &&
        decimalPlaces != null &&
        decimalPlaces != 0) {        
        if (x2.length > 0) {
            if (x[1].length < decimalPlaces) {
                i = (decimalPlaces - x[1].length);

                for(j = 0; j < i; j++) {
                    x[1] = (x[1] + "0");
                }
            }

            x2 = ("." + x[1]);
        }
        else {
            x2 = ".";

            for(i = 0; i < decimalPlaces; i++) {
                x2 = (x2 + "0");
            }
        }
    }

    var rgx = /(\d+)(\d{3})/;

    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, ("$1" + "," + "$2"));
    }

    $("#" + obj).val(x1 + x2);

    return (x1 + x2);
}

function DelCommas(obj) {
    var nStr = $("#" + obj).val();   
    nStr += "";

    for (var i = 0; i < nStr.length; i++)
        nStr = nStr.replace(",", "");

    return nStr;
}

function SingleSelectCheckbox(elem) {
    var elems = document.getElementsByTagName("input");
    var currentState = elem.checked;
    var elemsLength = elems.length;

    for(var i = 0; i < elemsLength; i++) {
        if (elems[i].type === "checkbox") {
            elems[i].checked = false;   
        }
    }

    elem.checked = currentState;
}

function UncheckRoot(checkboxRoot) {
    if ($("#" + checkboxRoot).is(":checked") == true) {
        elem = $("input[name=" + checkboxRoot + "]:checkbox");
        elem.attr("checked", false);
    }
}

function CheckUncheckAll(
    checkboxRoot,
    checkboxChild
) {
    elem = $("input[name=" + checkboxChild + "]:checkbox");
    elem.attr("checked", $("#" + checkboxRoot).is(":checked"));
}

function HideShowObj(id) {
    if ($("#" + id).css("display") == "none")
        $("#" + id).show("fade")
    else
        $("#" + id).hide({
            effect: "drop",
            direction: "down",
            distance: 100,
            duration: 500
        });
}

function GetDateObject(str) {
    if (str.length > 0) {
        var arr = str.split("/");
        
        return (arr[2] + "-" + arr[1] + "-" + arr[0]);
    }
    else
        return str;
}

function DateDiff(
    date1,
    date2,
    interval
) {
    var second = 1000;
    var minute = (second * 60);
    var hour = (minute * 60);
    var day = (hour * 24);
    var week = (day * 7);

    date1 = new Date(date1);
    date2 = new Date(date2);
    
    var timeDiff = (date2 - date1);

    if (isNaN(timeDiff))
        return NaN;
    
    switch (interval) {
        case "years":
            return (date2.getFullYear() - date1.getFullYear());
        case "months":
            return ((date2.getFullYear() * 12 + date2.getMonth()) - (date1.getFullYear() * 12 + date1.getMonth()));
        case "weeks":
            return Math.floor(timeDiff / week);
        case "days":
            return Math.floor(timeDiff / day);
        case "hours":
            return Math.floor(timeDiff / hour);
        case "minutes":
            return Math.floor(timeDiff / minute);
        case "seconds":
            return Math.floor(timeDiff / second);
        default:
            return undefined;
    }
}

function DialogLoading(loadingMsg) {
    $("#dialog-loading").html(loadingMsg);
    $("#dialog-loading").dialog({
        dialogClass: "class-dialog-loading",
        position: "center",
        modal: true,
        resizable: false,
        draggable: false,
        width: 450,
        height: 151
    });
}

function DialogMessage(
    alertMsg,
    focus,
    closeFrm,
    idActive
) {
    $("#dialog-message").html(alertMsg);
    $("#dialog-message").dialog({
        dialogClass: "class-dialog-message",
        position: "center",
        modal: true,
        resizable: false,
        draggable: true,
        width: 450,
        height: 151,
        /*
        maxHeight: 151,
        */
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
                
                if (closeFrm == true)
                    $("#dialog-form1").dialog("close");
            
                $(focus).focus();

                if (idActive.length > 0)
                    $("#" + idActive).removeClass("active");
            }
        }
    });
}

function DialogConfirm(confirmMsg) {
    $("#dialog-confirm").html(confirmMsg);
    $("#dialog-confirm").dialog({
        dialogClass: "class-dialog-confirm",
        position: "center",
        modal: true,
        resizable: false,
        draggable: true,
        width: 450,
        height: 151
    });
}

function DialogForm(
    frmIndex,
    frm,
    width,
    height,
    title,
    idActive
) {
    $("body").css("overflow", "hidden");

    if (frmIndex == 2)
        $(".dialog-overlay1").css("overflow-y", "hidden");

    if (frmIndex == 3)
        $(".dialog-overlay2").css("overflow-y", "hidden");

    $("#dialog-form" + frmIndex).html(frm);
    $("#dialog-form" + frmIndex).dialog({
        dialogClass: ("class-dialog-form class-dialog-form-" + title),
        position: "center",
        modal: true,
        resizable: false,
        draggable: true,
        width: width,
        height: (height == 0 ? "auto" : height),
        /*
        show: { 
            effect: "fade"
        },        
        hide: {
            effect: "drop",
            direction: "down",
            distance: 100,
            duration: 500
        },
        */
        open: function () {
            var dialogObj = $("#dialog-form" + frmIndex).closest("div[role='dialog']");
            var dialogOverlay = ("dialog-overlay" + frmIndex);
            var lastOverlayZIndex = $(".ui-widget-overlay:eq(" + ($(".ui-widget-overlay").length - 1) + ")").css("z-index");

            dialogObj.wrap("<div class='" + dialogOverlay + "'>");
            $("." + dialogOverlay).css("z-index", (parseInt(lastOverlayZIndex) + 1));

            if (dialogObj.height() <= $("." + dialogOverlay).height()) {
                dialogObj.position({
                    my: "center",
                    at: "center",
                    of: ("." + dialogOverlay)
                });
            }
            else
                dialogObj.css("top", "0");

            dialogObj.css("position", "absolute");
        },
        close: function () {
            if (idActive.length > 0)
                $("#" + idActive).removeClass("active");

            $(".combobox-input").autocomplete("close");
            $(".dialog-overlay" + frmIndex).remove();
            
            if (frmIndex == 1)
                $("body").css("overflow", "auto");

            if (frmIndex == 2)
                $(".dialog-overlay1").css("overflow-y", "auto");

            if (frmIndex == 3)
                $(".dialog-overlay2").css("overflow-y", "auto");
        }
    });      
}

function SetPositionDialogForm(frmIndex) {
    var dialogObj = $("#dialog-form" + frmIndex).closest("div[role='dialog']");
    var dialogOverlay = ("dialog-overlay" + frmIndex);

    if (dialogObj.height() > $("." + dialogOverlay).height())
        dialogObj.css("top", "0");
}