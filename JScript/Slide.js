function InitSlide(_scrollId, _slide, _download, _file) {
    var _slideSize = _slide.length;
    var _slideStart = 1;
    var _slideEnd = _slideSize;
    var _slideNum = _slideStart;

    GoToTopElement(_scrollId);
    $(".slide-tool-content").html(SlideTool(_slideSize, _download, _file));
    $(".slide-tool").html("::::::::::");
    $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
    $(document).ready(function () {
        $(_scrollId).scroll(function (e) {
            var _scrollTop = $(_scrollId).scrollTop();

            $(".slide-tool-content").hide();
            $(".slide-tools").css("top", (_scrollTop > 0) ? _scrollTop + "px" : "0px");
        });

        $(".slide-tool").click(function () {
            if ($(".slide-tool-content").is(":hidden") == true)
                $(".slide-tool-content").show();
            else
                $(".slide-tool-content").hide();
        });

        $(".slide-first").click(function () {
            _slideNum = _slideStart;
            $(".slide-nav").val(_slideNum);
            $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
        });

        $(".slide-previous").click(function () {
            _slideNum = (_slideNum > _slideStart) ? (parseInt(_slideNum) - 1) : _slideStart;
            $(".slide-nav").val(_slideNum);
            $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
        });

        $(".slide-next").click(function () {
            _slideNum = (_slideNum < _slideEnd) ? (parseInt(_slideNum) + 1) : _slideEnd;
            $(".slide-nav").val(_slideNum);
            $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
        });

        $(".slide-last").click(function () {
            _slideNum = _slideEnd;
            $(".slide-nav").val(_slideNum);
            $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
        });

        $(".slide-nav").change(function () {
            _slideNum = $(this).val();
            $(".slide-contents img").attr("src", _slide[_slideNum - 1]);
        });
    });
}

function SlideTool(_slideSize, _download, _file) {
    var _html = "";

    _html += "<ul class='nav-slide'>" +
             "  <li><a class='fix-width slide-first' href='javascript:void(0)'>&lt;&lt;</a></li>" +
             "  <li><a class='fix-width slide-previous' href='javascript:void(0)'>&lt;</a></li>" +
             "  <li>" +
             "      <select class='slide-nav'>";

    for (var _i = 1; _i <= _slideSize; _i++ )
    {
        _html += "      <option value='" + _i + "'>" + _i + "</option>";
    }

    _html += "      </select>" +
             "  </li>" +
             "  <li><a class='fix-width slide-next' href='javascript:void(0)'>&gt;</a></li>" +
             "  <li><a class='fix-width slide-last' href='javascript:void(0)'>&gt;&gt;</a></li>";

    if (_download == true)
        _html += "<li><a href='" + _file + "' target='_top'>&nbsp;ดาว์นโหลด&nbsp;</a></li>";

    _html += "</ul>";

    return _html;
}
