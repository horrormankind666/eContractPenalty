function InitSlide(
    scrollId,
    slide,
    download,
    file
) {
    var slideSize = slide.length;
    var slideStart = 1;
    var slideEnd = slideSize;
    var slideNum = slideStart;

    GoToTopElement(scrollId);
    $(".slide-tool-content").html(SlideTool(slideSize, download, file));
    $(".slide-tool").html("::::::::::");
    $(".slide-contents img").attr("src", slide[slideNum - 1]);
    $(document).ready(function () {
        $(scrollId).scroll(function (e) {
            var scrollTop = $(scrollId).scrollTop();

            $(".slide-tool-content").hide();
            $(".slide-tools").css("top", (scrollTop > 0 ? scrollTop + "px" : "0px"));
        });

        $(".slide-tool").click(function () {
            if ($(".slide-tool-content").is(":hidden") == true)
                $(".slide-tool-content").show();
            else
                $(".slide-tool-content").hide();
        });

        $(".slide-first").click(function () {
            slideNum = slideStart;
            $(".slide-nav").val(slideNum);
            $(".slide-contents img").attr("src", slide[slideNum - 1]);
        });

        $(".slide-previous").click(function () {
            slideNum = (slideNum > slideStart ? (parseInt(slideNum) - 1) : slideStart);
            $(".slide-nav").val(slideNum);
            $(".slide-contents img").attr("src", slide[slideNum - 1]);
        });

        $(".slide-next").click(function () {
            slideNum = (slideNum < slideEnd ? (parseInt(slideNum) + 1) : slideEnd);
            $(".slide-nav").val(slideNum);
            $(".slide-contents img").attr("src", slide[slideNum - 1]);
        });

        $(".slide-last").click(function () {
            slideNum = slideEnd;
            $(".slide-nav").val(slideNum);
            $(".slide-contents img").attr("src", slide[slideNum - 1]);
        });

        $(".slide-nav").change(function () {
            slideNum = $(this).val();
            $(".slide-contents img").attr("src", slide[slideNum - 1]);
        });
    });
}

function SlideTool(
    slideSize,
    download,
    file
) {
    var html = "";

    html += (
        "<ul class='nav-slide'>" +
        "   <li>" +
        "       <a class='fix-width slide-first' href='javascript:void(0)'>&lt;&lt;</a>" +
        "   </li > " +
        "   <li>" +
        "       <a class='fix-width slide-previous' href='javascript:void(0)'>&lt;</a>" +
        "   </li > " +
        "   <li>" +
        "       <select class='slide-nav'>"
    );

    for (var i = 1; i <= slideSize; i++ ) {
        html += (
            "       <option value='" + i + "'>" + i + "</option>"
        );
    }

    html += (
        "       </select>" +
        "   </li>" +
        "   <li>" +
        "       <a class='fix-width slide-next' href='javascript:void(0)'>&gt;</a>" +
        "   </li > " +
        "   <li>" +
        "       <a class='fix-width slide-last' href='javascript:void(0)'>&gt;&gt;</a>" +
        "   </li> "
    );

    if (download == true)
        html += (
            "<li>" +
            "   <a href='" + file + "' target='_top'>&nbsp;ดาว์นโหลด&nbsp;</a>" +
            "</li>"
        );

    html += (
        "</ul>"
    );

    return html;
}
