var _msgLoading

function SetMsgLoading(_val)
{
    _msgLoading = _val;
}

function LoadAjax(_param, _url, _method, _loading, _close, _callbackFunc)
{
    $.ajax({
        beforeSend: function () {
            if (_loading == true)
                DialogLoading(_msgLoading);
        },
        /*complete: function () {
            if (_close == true)
                $("#dialog-loading").dialog("close");
        },*/
        async: true,
        type: _method,
        url: _url,
        data: _param,
        dataType: "html",
        charset: "utf-8",
        success: function (_data) {
            if (_close == true)
                $("#dialog-loading").dialog("close");

            var _dataErrorBrowser = _data.split("<errorbrowser>");
            var _error = false;
            var _msg;

            if (_error == false && _dataErrorBrowser[1] == "1") { _error = true; _msg = "ไม่สนับสนุน IE6, IE7 และ IE8"; }
            if (_error == false && _dataErrorBrowser[1] == "2") { _error = true; _msg = "ไม่ได้เปิดใช้งาน Cookies"; }

            if (_error == true)
            {
                DialogMessage(_msg, "", false, "");
                return;
            }

            _callbackFunc(_data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
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

            //DialogMessage(xhr.status + " - " + thrownError, "", false);
            //location.reload();
        }
    });
}

function Trim(_id)
{
    return $("#" + _id).val($.trim($("#" + _id).val()));
}

function TextToEntities(_text)
{
    var _entities = "";

    for (var _i = 0; _i < _text.length; _i++)
    {
        if (_text.charAt(i) == "&") { _entities += "%26"; }
        else
            {
                if (_text.charAt(i) == "+") { _entities += "%2b"; }
                else
                    _entities += _text.charAt(i);
            }
    }

    return _entities;
}

function IsEnglishCharacter(_strString)
{
    var _strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var _strChar;
    var _blnResult = true;

    if (_strString.length == 0) return false;

    for (_i = 0; _i < _strString.length && _blnResult == true; _i++) {
        _strChar = _strString.charAt(_i);
        if (_strValidChars.indexOf(_strChar) == -1) {
            _blnResult = false;
        }
    }

    return _blnResult;
}

function IsNumeric(_strString)
{
    var _strValidChars = "0123456789";
    var _strChar;
    var _blnResult = true;

    if (_strString.length == 0) return false;

    for (_i = 0; _i < _strString.length && _blnResult == true; _i++)
    {
        _strChar = _strString.charAt(_i);
        if (_strValidChars.indexOf(_strChar) == -1)
        {
            _blnResult = false;
        }
    }
    return _blnResult;
}

function EmailCheck(_emailStr)
{
    var _emailPat = /^(.+)@(.+)$/;
    var _specialChars = "\\(\\)<>@,;:\\\\\\\"\\.\\[\\]";
    var _validChars = "\[^\\s" + _specialChars + "\]";
    var _quotedUser = "(\"[^\"]*\")";
    var _ipDomainPat = /^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
    var _atom = _validChars + '+';
    var _word = "(" + _atom + "|" + _quotedUser + ")";
    var _userPat = new RegExp("^" + _word + "(\\." + _word + ")*$");
    var _domainPat = new RegExp("^" + _atom + "(\\." + _atom + ")*$");

    var _matchArray = _emailStr.match(_emailPat);
    if (_matchArray == null)
    {
        return false;
    }

    var _user = _matchArray[1];
    var _domain = _matchArray[2];

    if (_user.match(_userPat) == null)
    {
        return false;
    }

    var _ipArray = _domain.match(_ipDomainPat);
    if (_ipArray != null)
    {
        for (var _i = 1; _i <= 4; _i++)
        {
            if (_ipArray[i] > 255)
            {
                return false;
            }
        }
        return true;
    }

    var _domainArray = _domain.match(_domainPat);
    if (_domainArray == null)
    {
        return false;
    }

    var _atomPat = new RegExp(_atom, "g");
    var _domArr = _domain.match(_atomPat);
    var _len = _domArr.length;
    if (_domArr[_domArr.length - 1].length < 2 || _domArr[_domArr.length - 1].length > 3)
    {
        return false;
    }

    if (_len < 2)
    {
        return false
    }

    return true;
}

function UrlCheck(_urlStr)
{
    var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/;

    return RegExp.test(_urlStr);
}

function DaysInFebruary(_year)
{
    return (((_year % 4 == 0) && ((!(_year % 100 == 0)) || (_year % 400 == 0))) ? 29 : 28);
}

function DaysArray(_n)
{
    for (var _i = 1; _i <= _n; _i++)
    {
        this[_i] = 31;
        if (_i == 4 || _i == 6 || _i == 9 || _i == 11) { this[_i] = 30; }
        if (_i == 2) { this[_i] = 29; }
    }
    return this;
}

function IsDate(_day, _month, _year)
{
    var _daysInMonth = DaysArray(12);
    var _date = _day + "-" + _month + "-" + _year;

    if (_date != "00-00-0000")
    {
        if (_day == "00") return false;
        if (_month == "00") return false;
        if (_year == "0000") return false;
        if (((parseInt(_month) == 2) && (_day > DaysInFebruary(_year))) || (_day > _daysInMonth[parseInt(_month)]))
        {
            return false;
        }
    }
    return true;
}

function ResetFrm()
{
    $("input:text").val("");
    $("input:hidden").val("");
    $("input:password").val("");
    $("input:radio").attr("checked", false);
    $("input:checkbox").attr("checked", false);
}

function InitTextSelect()
{
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

function StoreCaret(_text)
{
    if (typeof (_text.createTextRange) != "undefined")
        _text.caretPos = document.selection.createRange().duplicate();
}

function ReplaceText(_text, _textArea)
{
    if (typeof (_textArea.caretPos) != "undefined" && _textArea.createTextRange)
    {
        var _caretPos = _textArea.caretPos;

        _caretPos.text = _caretPos.text.charAt(_caretPos.text.length - 1) == ' ' ? _text + ' ' : _text;
        _caretPos.select();
    }
    else
        {
            if (typeof (_textArea.selectionStart) != "undefined")
            {
                var _begin = _textArea.value.substr(0, _textArea.selectionStart);
                var _end = _textArea.value.substr(_textArea.selectionEnd);
                var _scrollPos = _textArea.scrollTop;

                _textArea.value = _begin + _text + _end;

                if (_textArea.setSelectionRange)
                {
                    _textArea.focus();
                    _textArea.setSelectionRange(_begin.length + _text.length, _begin.length + _text.length);
                }
                _textArea.scrollTop = _scrollPos;
            }
            else
                {
                    _textArea.value += text;
                    _textArea.focus(_textArea.value.length - 1);
                }
        }
}

function SurroundText(_text1, _text2, _textArea)
{
    if (typeof (_textArea.caretPos) != "undefined" && _textArea.createTextRange)
    {
        var _caretPos = _textArea.caretPos, _tempLength = _caretPos.text.length;

        _caretPos.text = _caretPos.text.charAt(_caretPos.text.length - 1) == ' ' ? _text1 + _caretPos.text + _text2 + ' ' : _text1 + _caretPos.text + _text2;

        if (_tempLength == 0)
        {
            _caretPos.moveStart("character", -_text2.length);
            _caretPos.moveEnd("character", -_text2.length);
            _caretPos.select();
        }
        else
            _textArea.focus(_caretPos);
    }
    else
        {
            if (typeof (_textArea.selectionStart) != "undefined")
            {
                var _begin = _textArea.value.substr(0, _textArea.selectionStart);
                var _selection = _textArea.value.substr(_textArea.selectionStart, _textArea.selectionEnd - _textArea.selectionStart);
                var _end = _textArea.value.substr(_textArea.selectionEnd);
                var _newCursorPos = _textArea.selectionStart;
                var _scrollPos = _textArea.scrollTop;

                _textArea.value = _begin + _text1 + _selection + _text2 + _end;

                if (_textArea.setSelectionRange)
                {
                    if (_selection.length == 0)
                        _textArea.setSelectionRange(_newCursorPos + _text1.length, _newCursorPos + _text1.length);
                    else
                        _textArea.setSelectionRange(_newCursorPos, _newCursorPos + _text1.length + _selection.length + _text2.length);
                    _textArea.focus();
            }
            _textArea.scrollTop = _scrollPos;
        }
        else
            {
                _textArea.value += _text1 + _text2;
                _textArea.focus(_textArea.value.length - 1);
            }
    }
}

function SelectToList(_objChoose, _objList)
{
    var _choose = $("#" + _objChoose).val();

    if (_choose.length > 0)
    {
        var _obj = $("#" + _objList);
        var _i;

        if (_obj.length > 0)
        {
            for (_i = 0; _i < _obj.length; _i++)
            {
                if (_choose == _obj.options[i].value) return;
            }
        }

        var _opt = document.createElement("OPTION");
        _obj.options.add(_opt);
        _opt.innerHTML = _choose;
        _opt.value = _choose;
    }
}

function ClearList(_obj, _mode)
{
    var _objCombo = $("#" + _obj);

    if (_mode == 1)
    {
        if (_objCombo.options.selectedIndex >= 0) _objCombo.options[_objCombo.options.selectedIndex] = null;
    }

    if (_mode == 2)
    {
        for (_i = _objCombo.length - 1; _i >= 0; _i--)
        {
            _objCombo.options[i] = null;
        }
    }
}

function DeleteList(_obj)
{
    var _objCombo = $("#" + _obj);

    if (_objCombo.options.selectedIndex >= 0) _objCombo.options[_objCombo.options.selectedIndex] = null;
}

function GroupValueList(_obj)
{
    var _result = "";

    for (var _i = 0; _i < _obj.length; i++)
    {
        if (_result.length > 0) { _result = _result + ";" + _obj.options[_i].value; } else { _result = _obj.options[_i].value; }
    }

    return _result;
}

function CopyList(_objSource, _objDestination)
{
    ClearList(_objDestination, 2);

    _objCombo = $("#" + _objSource);
    for (_i = 0; _i < _objCombo.length; _i++)
    {
        var _tmp = _objCombo.options[i].value;
        var _data = _tmp.split(";");
        var _obj = $("#" + _objDestination);
        var _opt = document.createElement("OPTION");
        _obj.options.add(_opt);
        _opt.innerHTML = _data[1];
        _opt.value = _data[0];
    }
}

function GetPageSize()
{
    var _xPage, _yPage;

    if (self.innerHeight)
    {
        _xPage = self.innerWidth;
        _yPage = self.innerHeight;
    }
    else
        {
            if (document.documentElement && document.documentElement.clientHeight)
            {
                _xPage = document.documentElement.clientWidth;
                _yPage = document.documentElement.clientHeight;
            }
            else
                {
                    if (document.body)
                    {
                        _xPage = document.body.clientWidth;
                        _yPage = document.body.clientHeight;
                    }
                }
        }

    return new Array(_xPage, _yPage);
}

function GetPageScroll()
{
    var _yScroll;

    if (self.pageYOffset)
    {
        _yScroll = self.pageYOffset;
    }
    else
        {
            if (document.documentElement && document.documentElement.scrollTop)
            {
                _yScroll = document.documentElement.scrollTop;
            }
            else
                {
                    if (document.body)
                    {
                        _yScroll = document.body.scrollTop;
                    }
                }
        }

    return new Array("", _yScroll);
}

function GoToElement(_anchor)
{
    var _ele = $("#" + _anchor);
    var _offset = _ele.offset();

    if ($("#" + _anchor).length > 0) $(_anchor).animate({ scrollTop: _offset.top }, 500);
}

function GoToTopElement(_anchor)
{
    $(_anchor).animate({ scrollTop: 0 }, 500);
}

function InitCombobox(_id, _valueDefault, _valueNew, _widthInput, _widthList)
{
    $(document).ready(function () {
        $("#" + _id).val(_valueDefault);
        $("#" + _id).combobox();
        $("." + _id + "-combobox-input").css({ background: "#FFFFFF", width: _widthInput + "px" });
        $("." + _id + "-combobox-input").bind("autocompleteopen", function (event, ui) {
            $(".ui-autocomplete.ui-menu").width(_widthList + "px");
        });
    });

    ComboboxSetSelectedValue(_id, _valueNew);
    InitComboboxOnClick(_id);    
    InitTextSelect();
}

function TextboxDisable(_id)
{
    //$(_id).attr("disabled", "disabled");
    $(_id).attr("readonly", "readonly");
    $(_id).addClass("textbox-disable");
}

function TextboxEnable(_id)
{
    //$(_id).removeAttr("disabled");
    $(_id).removeAttr("readonly");
    $(_id).removeClass("textbox-disable");
}

function ComboboxDisable(_id)
{
    $("." + _id + "-combobox-input").css({ background: "#CCCCCC", color: "#000000" });
    //$("." + _id + "-combobox-input").attr("disabled", "disabled");
    $("." + _id + "-combobox-input").attr("readonly", "readonly");
    $("." + _id + "-combobox-input").autocomplete({ disabled: true });
}

function ComboboxEnable(_id)
{
    $("." + _id + "-combobox-input").css({ background: "#FFFFFF", color: "#000000" });
    //$("." + _id + "-combobox-input").removeAttr("disabled");
    $("." + _id + "-combobox-input").removeAttr("readonly");
    $("." + _id + "-combobox-input").autocomplete({ disabled: false });
}

function ComboboxSetSelectedValue(_id, _value)
{
    $("#" + _id).val(_value);
    $("." + _id + "-combobox-input").val($("#" + _id + " option:selected").text());
}

function ComboboxGetSelectedValue(_id)
{
    var _val = ($("#" + _id + " option:selected").text().toLowerCase() == $("." + _id + "-combobox-input").val().toLowerCase()) ? $("#" + _id).val() : null;

    return _val;
}

function HiddenButton()
{
    $("#clear-bottom .form-label-discription-style").addClass("clear-bottom");
    $("#clear-bottom .form-input-style").addClass("clear-bottom");
    $(".button").hide();
}

function ButtonDisable(_idButton, _classButtonDisable)
{
    $(_idButton).addClass(_classButtonDisable);
    $(_idButton + " a").removeAttr("onclick");
}

function LinkDisable(_idLink)
{
    $(_idLink).addClass("link-disable");
    $(_idLink).removeAttr("onclick");
}

function CalendarDisable(_id)
{
    TextboxDisable(_id);
    $(_id).datepicker("disable");
}

function CalendarEnable(_id)
{
    TextboxEnable(_id);
    $(_id).attr("readonly", "true");
    $(_id).datepicker("enable");

}

function InitCalendar(_calendarID)
{
    $(document).ready(function () {
        $(_calendarID).datepicker("destroy");
        $(_calendarID).datepicker({
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
        $("img.ui-datepicker-trigger").css({"cursor" : "pointer", "vertical-align" : "bottom", "margin-left" : "2px"});
    });
}

function InitCalendarFromTo(_fromID, _fromFix, _toID, _toFix)
{
    $(document).ready(function () {
        if (_fromFix == false)
        {
            $(_fromID).datepicker("destroy");
            $(_fromID).datepicker({
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
                    if ($(_toID).val().length > 0)
                    {
                        $(_fromID).datepicker("option", "maxDate", $(_toID).val()); 
                        $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
                    }
                },
                /*onSelect: function (selectedDate) {
                    if (_toFix == false)
                    {
                        $(_toID).datepicker("option", "minDate", selectedDate);
                        $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
                    }
                }*/
            });
            $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
        }
        
        if (_toFix == false)
        {
            $(_toID).datepicker("destroy");
            $(_toID).datepicker({
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
                    if ($(_fromID).val().length > 0)
                    {                        
                        $(_toID).datepicker("option", "minDate", $(_fromID).val());
                        $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
                    }
                },
                /*onSelect: function (selectedDate) {
                    if (_fromFix == false)
                    {                        
                        $(_fromID).datepicker("option", "maxDate", selectedDate);
                        $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
                    }
                }*/
            });
            $("img.ui-datepicker-trigger").css({ "cursor": "pointer", "vertical-align": "bottom", "margin-left": "2px" });
        }
    });
}

function StickyRelocate()
{
    var _windowTop = $(window).scrollTop();
    var _divTop = $("#sticky-anchor").offset().top;

    _windowTop = _windowTop + 35;

    if (_windowTop >= _divTop)
    {
        $("#sticky").addClass("stick");        
    }
    else
        $("#sticky").removeClass("stick");
}

function RemoveSticky()
{
    if (($("#sticky-anchor").length > 0) && ($("#sticky").length > 0)) $("#sticky").removeClass("stick").next().css("padding-top", "0px");
    GoToElement("top-page");
}    

function InitSticky()
{
    if (($("#sticky-anchor").length > 0) && ($("#sticky").length > 0))
    {
        RemoveSticky();

        $(document).ready(function () {
            var _aboveHeight = ($(".head").outerHeight() + $(".menu-bar-main").outerHeight() + $(".content-data-head").outerHeight()) + 10;
            $(window).scroll(function () {
                if ($(window).scrollTop() > _aboveHeight)
                {
                    $("#sticky").addClass("stick").next().css("padding-top", $("#sticky").outerHeight() + "px");
                }
                else
                    {
                        $("#sticky").removeClass("stick").next().css("padding-top", "0px");
                    }
            });
        });
    }
}

function ExtractNumber(_obj, _decimalPlaces, _allowNegative)
{
    var _temp = _obj.value;
    var _reg0Str = "[0-9]*";

    if (_decimalPlaces > 0)
    {
        _reg0Str += "\\.?[0-9]{0," + _decimalPlaces + "}";
    }
    else
        if (_decimalPlaces < 0)
        {
            _reg0Str += "\\.?[0-9]*";
        }
    
    _reg0Str = _allowNegative ? "^-?" + _reg0Str : "^" + _reg0Str;
    _reg0Str = _reg0Str + "$";
    var _reg0 = new RegExp(_reg0Str);

    if (_reg0.test(_temp)) return true;
    
    var _reg1Str = "[^0-9" + (_decimalPlaces != 0 ? "." : "") + (_allowNegative ? "-" : "") + "]";
    var _reg1 = new RegExp(_reg1Str, "g");
    _temp = _temp.replace(_reg1, "");

    if (_allowNegative)
    {
        var _hasNegative = _temp.length > 0 && _temp.charAt(0) == "-";
        var _reg2 = /-/g;
        _temp = _temp.replace(_reg2, "");
        
        if (_hasNegative) _temp = "-" + _temp;
    }

    if (_decimalPlaces != 0)
    {
        var _reg3 = /\./g;
        var _reg3Array = _reg3.exec(_temp);
        if (_reg3Array != null)
        {
            var _reg3Right = _temp.substring(_reg3Array.index + _reg3Array[0].length);
            _reg3Right = _reg3Right.replace(_reg3, "");
            _reg3Right = _decimalPlaces > 0 ? _reg3Right.substring(0, _decimalPlaces) : _reg3Right;
            _temp = _temp.substring(0, _reg3Array.index) + "." + _reg3Right;
        }
    }

    _obj.value = _temp;
}

function BlockNonNumbers(_obj, _e, _allowDecimal, _allowNegative)
{
    var _key;
    var _isCtrl = false;
    var _keychar;
    var _reg;

    if (window.event)
    {
        _key = _e.keyCode;
        _isCtrl = window.event.ctrlKey
    }
    else
        if (_e.which)
        {
            _key = _e.which;
            _isCtrl = _e.ctrlKey;
        }

    if (isNaN(_key)) return true;

    _keychar = String.fromCharCode(_key);

    if (_key == 8 || _isCtrl) return true;

    _reg = /\d/;
    var _isFirstN = _allowNegative ? _keychar == "-" && _obj.value.indexOf("-") == -1 : false;
    var _isFirstD = _allowDecimal ? _keychar == "." && _obj.value.indexOf(".") == -1 : false;

    return _isFirstN || _isFirstD || _reg.test(_keychar);
}

function AddCommas(_obj, _decimalPlaces)
{
    var _nStr = parseFloat(DelCommas(_obj).length > 0 ? DelCommas(_obj) : "0").toString();
    _nStr += "";    
    var _x = _nStr.split(".");
    _x1 = _x[0];
    var _i, _j;
    var _x2 = _x.length > 1 ? "." + _x[1] : "";

    if (_x2.length > 0) _x1 = _x1.length == 0 ? "0" : _x1;
    if (parseInt(_x1) == 0) _x1 = "0";
    if (_x1.length > 0 && _decimalPlaces != null && _decimalPlaces != 0)
    {        
        if (_x2.length > 0)
        {
            if (_x[1].length < _decimalPlaces)
            {
                _i = _decimalPlaces - _x[1].length;

                for(_j = 0; _j < _i; _j++)
                {
                    _x[1] = _x[1] + "0";
                }
            }

            _x2 = "." + _x[1];
        }
        else
            {
                _x2 = ".";

                for(_i = 0; _i < _decimalPlaces; _i++)
                {
                    _x2 = _x2 + "0";
                }
            }
    }

    var _rgx = /(\d+)(\d{3})/;

    while (_rgx.test(_x1))
    {
        _x1 = _x1.replace(_rgx, "$1" + "," + "$2");
    }

    $("#" + _obj).val(_x1 + _x2);

    return _x1 + _x2;
}

function DelCommas(_obj)
{
    var _nStr = $("#" + _obj).val();   
    _nStr += "";

    for (var _i = 0; _i < _nStr.length; _i++)
        _nStr = _nStr.replace(",", "");

    return _nStr;
}

function SingleSelectCheckbox(_elem)
{
  var _elems = document.getElementsByTagName("input");
  var _currentState = _elem.checked;
  var _elemsLength = _elems.length;

  for(var _i = 0; _i < _elemsLength; _i++)
  {
    if (_elems[_i].type === "checkbox")
    {
       _elems[_i].checked = false;   
    }
  }

  _elem.checked = _currentState;
}

function UncheckRoot(_checkboxRoot)
{
    if ($("#" + _checkboxRoot).is(":checked") == true)
    {
        _elem = $("input[name=" + _checkboxRoot + "]:checkbox");
        _elem.attr("checked", false);
    }
}

function CheckUncheckAll(_checkboxRoot, _checkboxChild)
{
    _elem = $("input[name=" + _checkboxChild + "]:checkbox");
    _elem.attr("checked", $("#" + _checkboxRoot).is(":checked"));
}

function HideShowObj(_id)
{
    if ($("#" + _id).css("display") == "none")
        $("#" + _id).show("fade")
    else
        $("#" + _id).hide({
            effect: "drop",
            direction: "down",
            distance: 100,
            duration: 500
        });
}

function GetDateObject(_str)
{
    if (_str.length > 0)
    {
        var _arr = _str.split("/");
        
        return (_arr[2] + "-" + _arr[1] + "-" + _arr[0]);
    }
    else
        return _str;
}

function DateDiff(_date1, _date2, _interval)
{
    var _second = 1000;
    var _minute = _second * 60;
    var _hour = _minute * 60;
    var _day = _hour * 24;
    var _week = _day * 7;

    _date1 = new Date(_date1);
    _date2 = new Date(_date2);
    
    var _timeDiff = _date2 - _date1;

    if (isNaN(_timeDiff)) return NaN;
    
    switch (_interval)
    {
        case "years"  : return _date2.getFullYear() - _date1.getFullYear();
        case "months" : return ((_date2.getFullYear() * 12 + _date2.getMonth()) - (_date1.getFullYear() * 12 + _date1.getMonth()));
        case "weeks"  : return Math.floor(_timeDiff / _week);
        case "days"   : return Math.floor(_timeDiff / _day);
        case "hours"  : return Math.floor(_timeDiff / _hour);
        case "minutes": return Math.floor(_timeDiff / _minute);
        case "seconds": return Math.floor(_timeDiff / _second);
        default       : return undefined;
    }
}

function DialogLoading(_loadingMsg)
{
    $("#dialog-loading").html(_loadingMsg);
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

function DialogMessage(_alertMsg, _focus, _closeFrm, _idActive)
{   
    $("#dialog-message").html(_alertMsg);
    $("#dialog-message").dialog({
        dialogClass: "class-dialog-message",
        position: "center",
        modal: true,
        resizable: false,
        draggable: true,
        width: 450,
        height: 151,
        /*maxHeight: 151,*/
        buttons: {
            "ตกลง": function () {
                $(this).dialog("close");
                
                if (_closeFrm == true) $("#dialog-form1").dialog("close");
                //GoToElement(_focus);
                $(_focus).focus();
                if (_idActive.length > 0) $("#" + _idActive).removeClass("active");
                //$(_focus).select();
            }
        }
    });
}

function DialogConfirm(_confirmMsg)
{
    $("#dialog-confirm").html(_confirmMsg);
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

function DialogForm(_frmIndex, _frm, _width, _height, _title, _idActive)
{
    $("body").css("overflow", "hidden");
    if (_frmIndex == 2) $(".dialog-overlay1").css("overflow-y", "hidden");
    if (_frmIndex == 3) $(".dialog-overlay2").css("overflow-y", "hidden");
    $("#dialog-form" + _frmIndex).html(_frm);
    $("#dialog-form" + _frmIndex).dialog({
        dialogClass: "class-dialog-form class-dialog-form-" + _title,
        position: "center",
        modal: true,
        resizable: false,
        draggable: true,
        width: _width,
        height: (_height == 0 ? "auto" : _height),
        /*
        show: { effect: "fade" },        
        hide: {
        effect: "drop",
        direction: "down",
        distance: 100,
        duration: 500
        },*/
        open: function () {
            var _dialogObj = $("#dialog-form" + _frmIndex).closest("div[role='dialog']");
            var _dialogOverlay = ("dialog-overlay" + _frmIndex);
            var _lastOverlayZIndex = $(".ui-widget-overlay:eq(" + ($(".ui-widget-overlay").length - 1) + ")").css("z-index");

            _dialogObj.wrap("<div class='" + _dialogOverlay + "'>");
            $("." + _dialogOverlay).css("z-index", (parseInt(_lastOverlayZIndex) + 1));

            if (_dialogObj.height() <= $("." + _dialogOverlay).height())
            {
                _dialogObj.position({
                    my: "center",
                    at: "center",
                    of: ("." + _dialogOverlay)
                });
            }
            else
                _dialogObj.css("top", "0");

            _dialogObj.css("position", "absolute");
        },
        close: function () {
            if (_idActive.length > 0) $("#" + _idActive).removeClass("active");
            $(".combobox-input").autocomplete("close");
            $(".dialog-overlay" + _frmIndex).remove();
            
            if (_frmIndex == 1) $("body").css("overflow", "auto");
            if (_frmIndex == 2) $(".dialog-overlay1").css("overflow-y", "auto");
            if (_frmIndex == 3) $(".dialog-overlay2").css("overflow-y", "auto");
        }
    });      
}

function SetPositionDialogForm(_frmIndex)
{
    var _dialogObj = $("#dialog-form" + _frmIndex).closest("div[role='dialog']");
    var _dialogOverlay = ("dialog-overlay" + _frmIndex);

    if (_dialogObj.height() > $("." + _dialogOverlay).height())
        _dialogObj.css("top", "0");                       
}