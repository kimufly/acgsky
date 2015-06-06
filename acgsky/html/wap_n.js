(function ($) {
    var labelJsonpUrl = "http://db2.gamersky.com/LabelJsonpAjax.aspx";

    $.fn.supportMeInit = function (options) {
        return this.each(function () {

            var op = $.extend({
                itemId: parseInt($(this).attr("data-itemId")),
                field: $(this).attr("data-field")
            }, options);

            var $this = $(this);

            var isSupport = false;
            var tableName = $this.attr("data-table");
            if (!tableName) {
                tableName = 'PE_U_Video';
            }
            if ($.cookie("GamerSkySupport" + op.itemId)) {
                isSupport = true;
            }


            if (isSupport) {
                $("span.supportMe").css("cursor", "default");
            }
            else {
                $("span.supportMe").css("cursor", "pointer")
            }

            var jsondata = {
                type: "updatelabel", labelname: "读取支持反对率", attr: {
                    itemId: op.itemId,
                    field: op.field,
                    tableName: tableName
                }
            };

            $.ajax({
                type: "GET",
                url: labelJsonpUrl,
                dataType: "jsonp",
                data: {
                    jsondata: JSON2.stringify(jsondata)
                },
                success: function (responseJson) {
                    $this.text(responseJson.body);
                }
            });
        });
    },

    $.fn.SupportMe = function (options) {
        return this.each(function () {

            var $this = $(this);
            var op = $.extend({
                itemId: parseInt($(this).attr("data-itemId")),
                field: $(this).attr("data-field")
            }, options);

            var tableName = $this.attr("data-table");
            if (!tableName) {
                tableName = 'PE_U_Video';
            }

            var autoUpdate = $this.attr("data-auto");

            if (autoUpdate && autoUpdate == 'true') {
                var jsondata = {
                    type: "updatelabel", labelname: "WapDigg统计", attr: {
                        itemId: op.itemId,
                        field: op.field,
                        tableName: tableName
                    }
                };

                $.ajax({
                    type: "GET",
                    url: labelJsonpUrl,
                    dataType: "jsonp",
                    data: {
                        jsondata: JSON2.stringify(jsondata)
                    },
                    success: function (responseJson) {
                    }
                });
            }

            $(this).click(function (event) {
                var isSupport = false;
                if ($.cookie("WapGamerSkySupport" + op.itemId)) {
                    isSupport = true;
                }

                if (isSupport) {
                    return;
                }

                $.cookie("WapGamerSkySupport" + op.itemId, 1, { path: "/" });

                var jsondata = {
                    type: "updatelabel", labelname: "Digg统计", attr: {
                        itemId: op.itemId,
                        field: op.field,
                        tableName: tableName
                    }
                };

                $.ajax({
                    type: "GET",
                    url: labelJsonpUrl,
                    dataType: "jsonp",
                    data: {
                        jsondata: JSON2.stringify(jsondata)
                    },
                    success: function (responseJson) {
                        $this.supportMeInit();
                    }
                });

            });

            $this.supportMeInit();

        });
    }

    $.fn.ArticleList = function (options) {
        return this.each(function () {
            var $this = $(this);
            var start = $this.attr("startrow");
            var pagesize = $this.attr("pagesize");
            var nodeId = $this.attr("nodeId") == "" ? 1 : $this.attr("nodeId");
            var tagname = $this.attr("tagname");
            var value = $("#LoadMore").attr("value");
            $.ajax({
                url: '/WapAjax.aspx',
                data: { jsondata: value, tagname: tagname, nodeId: nodeId, pagesize: parseInt(pagesize) - 1, startrow: parseInt(start) + 1 },
                dataType: "jsonp",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        var pic = "http://image.gamersky.com/webimg13/wap/bg_default.gif";
                        var elitelever = "";
                        if (data[i].DefaultPicurl != "") {
                            pic = data[i].DefaultPicurl;
                        }
                        if (data[i].EliteLevel > 0) {
                            elitelever = "[推荐] ";
                        }

                        var fontHtml = data[i].Title;

                        if ($("#LoadMore").attr("value") == "strategylistmore") {
                            fontHtml = elitelever + data[i].Title;
                        }
                        if (data[i].TitleFontColor != "") {
                            if (data[i].TitleFontColor.indexOf("#") == 0) {
                                fontHtml = "<font color='" + data[i].TitleFontColor + "'>" + data[i].Title + "</font>";
                                if ($("#LoadMore").attr("value") == "strategylistmore") {
                                    fontHtml = "<font color='" + data[i].TitleFontColor + "'>" + elitelever + data[i].Title + "</font>";
                                }
                            }
                            else {
                                fontHtml = "<font class='" + data[i].TitleFontColor + "'>" + data[i].Title + "</font>";
                                if ($("#LoadMore").attr("value") == "strategylistmore") {
                                    fontHtml = "<font class='" + data[i].TitleFontColor + "'>" + elitelever + data[i].Title + "</font>";
                                }
                            }
                        }
                        var html = "<li><div class='Ps'><div class='Ps_1'><a href='/news/Content-" + data[i].GeneralId + ".html'><img src='" + pic + "' height='45' width='81' /></a></div><div class='Ps_2'><a href='/news/Content-" + data[i].GeneralId + ".html'><div class='tit'>" + fontHtml + "</div><div class='num'><span class='cy_comment' data-sid='" + data[i].GeneralId + "'>0</span>跟贴</div></a></div></div></li>";

                        if ($("#LoadMore").attr("value") == "strategylistmore") {
                            html = "<li><div class='lik'><a class='L_TXT' href='/gl/Content-" + data[i].GeneralId + ".html'>" + fontHtml + "</a></div></li>";
                        }
                        $this.append(html);

                        var cycm = "";

                        $(".cy_comment").each(function () {
                            if (cycm != "") {
                                cycm = cycm + ","
                            }
                            cycm = cycm + $(this).attr("data-sid");
                        });

                        if (cycm != "") {
                            $.ajax({
                                type: "GET",
                                url: "http://changyan.sohu.com/api/2/topic/count",
                                dataType: "jsonp",
                                data: {
                                    client_id: "cyqQwkOU4",
                                    topic_source_id: cycm
                                },
                                success: function (responseJson) {

                                    $(".cy_comment").each(function () {
                                        if (responseJson.result.hasOwnProperty($(this).attr("data-sid"))) {
                                            var cmobj = responseJson.result[$(this).attr("data-sid")];
                                            $(this).text(cmobj.comments);
                                        }
                                    });
                                }
                            });
                        }
                    }
                    if (data.length > 0) {
                        //同时增加页码
                        $this.attr("startrow", (parseInt(start) + parseInt(pagesize)));
                    }
                    else {
                        $(".MMore .More span").html("全部加载完成");
                    }

                }
            });
        });
    };

    $.fn.GamerskyUserPF = function (options) {
        return this.each(function () {
            var $this = $(this);
            $.ajax({
                type: "GET",
                dataType: "jsonp",
                url: "http://db2.gamersky.com/RatingJsonpAjax.aspx",
                data: { 'generalId': $this.attr("data-generalId"), 'Action': "grade" },
                success: function (data) {
                    if (data.EditorRating != "" && data.RatingUrl != "") {
                        $this.find("a span").html(data.EditorRating);
                    }
                    else {
                        $this.find("a span").html("--");
                    }

                }
            });


        });
    };

    $.fn.GamerskyPF = function (options) {
        return this.each(function () {
            var $this = $(this);
            var TT = $this.attr("data-generalid");
            if ($.cookie("WapPL" + TT) !== undefined) {
                var ckie = $.cookie("WapPL" + TT);
                $("#myScore").find("span").html(ckie);
                $("#myScore").find("font").html("您的评分");
            }
        });
    };

    $.fn.GamerskyLabelBody = function (options) {
        return this.each(function () {
            var $this = $(this);
            var op = {
                pageSize: parseInt($this.attr("pageSize")) == 0 ? 10 : parseInt($this.attr("pageSize")),
                Tag: $this.attr("Tag"),
                nodes: $this.attr("nodes"),
                currentpage: $this.attr("currentpage"),
                sourceId: $this.attr("sourceId"),
                bindStyle: $this.attr("bindStyle"),
                specials: $this.attr("specials"),
                models: $this.attr("models"),
                bindModel: $this.attr("bindModel"),
                titleLength: $this.attr("titleLength"),
                itemListOrderType: $this.attr("itemListOrderType"),
                dir: $this.attr("sourceId") == 'tit1' ? "news" : "gl"
            };
            var tempNewHtml = '<li><div class="lik"><a href="/' + op.dir + '/Content-{$ArticleID}.html" title="{$TitleOriginal}">{$TitleCss}</a></div></li>';
            var tempDowmHtml = '<li><div class="lik"><a href="/soft/Content-{$SoftID}.html" title="{$SoftNameOriginal}">{$SoftNameCss}</a></div></li>';
            var temp = '{PE.Label id="内容带图片的信息列表TAG数据源" sourceId="' + op.sourceId + '" bindStyle="' + op.bindStyle + '" usepage="true"  Tag="' + op.Tag + '" specials="' + op.specials + '" nodes="' + op.nodes + '" models="' + op.models + '"  bindModel="' + op.bindModel + '" titleLength="' + op.titleLength + '" itemListOrderType="' + op.itemListOrderType + '"/}{PE.Repeat id="' + op.sourceId + '"}';
            if (op.bindStyle == '通用下载式') {
                temp += tempDowmHtml;
            }
            else {
                temp += tempNewHtml;
            }
            temp += '{/PE.Repeat}';
            var json = {
                isCache: false, cacheTime: 60, templateKey: "Ku_Data_Source", templata: temp, page: op.currentpage, pageSize: op.pageSize
            };

            $.ajax({
                type: "GET",
                url: "/WapAjax.aspx",
                dataType: "jsonp",
                data: {
                    json: JSON2.stringify(json),
                    jsondata: "putlabelbody"
                },
                success: function (responseJson) {
                    $this.append(responseJson.body);
                }
            });

        });
    };

    $.fn.HotGamerVideo = function (options) {
        return this.each(function () {
            var $this = $(this);
            var start = $this.attr("startrow");
            var pagesize = $this.attr("pagesize");
            var nodeId = $this.attr("nodeId") == "" ? "0" : $this.attr("nodeId");
            $.ajax({
                url: '/WapAjax.aspx',
                data: { jsondata: $this.attr("value"), pagesize: parseInt(pagesize), startrow: parseInt(start), nodeId: nodeId },
                dataType: "jsonp",
                success: function (data) {
                    var html = "";
                    var defaultPic = "http://image.gamersky.com/webimg13/wap/bg_default.gif";
                    for (var i = 0; i < data.length; i++) {
                        //if (data.length < pagesize) {
                        //    $("#LoadMore span").html("全部加载完成");
                        //}
                        if (data[i].defaultPicUrl == '') {
                            data[i].defaultPicUrl = defaultPic;
                        }
                        if (data[i].editorRating == 0) {
                            data[i].editorRating = "--";
                        }
                        if ($this.attr("value") == "hotgamer" || $this.attr("value") == "newgamer" || $this.attr("value") == "ranking") {
                            html += "<li><div class='Pb'><div class='Pb_1'><a href='/ku/" + data[i].gameDir + "/'><img width='100' height='141' src='" + data[i].defaultPicUrl + "' /></a>" +
                            "</div><div class='Pb_2'><div class='tit'><a href='/ku/" + data[i].gameDir + "/'>" + data[i].title + "</a>" +
                            "</div><div class='txt'>游戏类型：<span>" + data[i].nodeName + "</span></div>" +
                            "<div class='tet'>游戏标签：<span>" + data[i].tag + "</span></div><div class='txt'>发售日期：<span>" + data[i].publicTime + "</span></div>" +
                           "<div class='tet'>本站评分：<span>" + data[i].editorRating+"</span></div><div class='txt'>游民指数：<span>" + data[i].popularity + "</span></div>" +
                            "<div class='tet'>游戏人气：<span>" + data[i].hits + "</span></div></div></div></li>";
                        }
                        else {
                            if ((i + 1) % 2 == 0) {
                                html += "<div class='Pic'><a href='/v/Content-" + data[i].generalId + ".html'>" +
                                "<img width='130' height='73' src='" + data[i].defaultPicUrl + "' /><br />" + data[i].title + "<div class='p'></div></a>" +
                                "</div></div></li>";
                            }
                            else {
                                html += "<li><div class='Pi'><div class='Pic'>" +
                                "<a href='/v/Content-" + data[i].generalId + ".html'><img width='130' height='73' src='" + data[i].defaultPicUrl + "' />" +
                                "<br />" + data[i].title + "<div class='p'></div></a>" +
                                "</div> ";
                            }
                            if ((i + 1) % 2 != 0 && (i + 1) == data.length) {
                                html += "<div class='Pic'><a href='/v/Content-" + data[i].generalId + ".html'><br /></a></div>";
                            }
                        }

                    }
                    $this.append(html);
                }
            });
        });
    };

    

    $(document).ready(function () {
        $("#Hon_Pic").HotGamerVideo();//首页热门游戏
        $("#Splendid_Video").HotGamerVideo();//所有视频模块
        $("#List_Pic").ArticleList();//所有文章列表模块
        $("#KuList .PicList").HotGamerVideo();//游戏库列表页
        $("#gamerskypf").GamerskyUserPF();//游戏评分
        $("#myScore").GamerskyPF();//游戏评分
        $(".TextList").GamerskyLabelBody();
        $(".supportMe").SupportMe();
        
    });
})(jQuery);

$(document).ready(function () {
    document.onkeydown = function (e) {
        //捕捉回车事件
        var ev = (typeof event != 'undefined') ? window.event : e;
        $(".back-to-top").hide("slow", function () { $(this).css("display", "none") });
    }

    $(".Scontent").on("keypress", function (event) {
        if (event.which == 13 && !event.shiftKey) {
            event.preventDefault();
            $(".Submit").click();
        }
    });
    //搜索
    $("#Search").click(function () {
        var searchText = $(".Sinput").val();
        var dir = $(".Sselect").attr("value");
        if (searchText != "" && searchText != "输入搜索内容") {
            window.location.href = "/" + dir + "/search-" + searchText + ".html";
        }
        //var iframeElements = document.getElementsByTagName("iframe")[i];
        //if (iframeElements.attributes("display") != "none") {
        //    iframeElements.removeAttribute("style");
        //}
    });
    //异步加载数据
    $("#LoadMore").click(function () {
        var value = $(this).attr("value");
        //热门视频
        if (value == "splendidvideo") {
            $("#Splendid_Video").attr("startrow", parseInt($("#Splendid_Video").attr("startrow")) + parseInt($("#Splendid_Video").attr("pagesize")));
            $("#Splendid_Video").HotGamerVideo();
            return;
        }
            //，游戏，排行
        else if (value == "newgamer" || value == "hotgamer" || value == "ranking") {
            var ulList = $(".PicList.block[value='" + value + "']");
            ulList.attr("startrow", parseInt(ulList.attr("startrow")) + parseInt(ulList.attr("pagesize")));
            ulList.HotGamerVideo();
        }
        else if (value == "listTemplate") {
            var ulList = $(".TextList.block");
            ulList.attr("currentpage", parseInt(ulList.attr("currentpage")) + 1);
            $(".TextList.block").GamerskyLabelBody();
        }
        else if (value == "loadmore") {
            $("#List_Pic").ArticleList();
        }
        else if (value == "strategylistmore") {
            $("#StrategyList").ArticleList();
        }
        else {
            $(".PicList.block").ArticleList();
        }
    });


    $("#SpecialMore").click(function () {
        
    });

    //导航搜索
    if ($("ul").hasClass("QZsearch") == true) {
        $(".Sselect").click(function () { $(".SS").attr("class") == "SS block" ? Slt("none") : Slt("block"); });
        $(".Sinput").focus(function () { Slt("none"); });
        function Slt(str) {
            var SH = $(".Sname").height(), SW = $(".Sname").width();
            $(".SS").attr("class", "SS " + str).css({ "width": SW, "top": SH, "left": -1 });
        }

        $(".SS li").each(function (i) {
            $(this).click(function () {
                $(".Sselect").text($(this).text());
                $(".Sselect").attr("value", $(this).find("a").attr("value"));
                Slt("none");
            });
        });
    }


    //首页 导航
    $(".NTBtn").click(function () { $(".Nav_NTBtn,.Mask").toggle(); });
    $(".NBBtn").click(function () { $(".Nav_NBBtn,.Mask").toggle(); $(this).html($(this).html() == '导航 ∨' ? '导航 ∧' : '导航 ∨'); });
    $("#myScore").click(function () {
        var TT = $(this).attr("data-generalid");
        var cookieKey = "WapPL" + TT;
        if ($.cookie(cookieKey) == undefined) {
            var PFw = ($(window).width() - $(".PFLayer").outerWidth()) / 2;
            var PFh = ($(window).height() - $(".PFLayer").outerHeight()) / 2;
            $(".PFLayer,.Mask").toggle();
            $(".PFLayer").css("left", PFw).css("top", PFh);
        }
    });
    $("a.tjnum").click(function () {
        var num = $(this).html();
        $("#myScore").find("span").html(num);
        $("#myScore").find("font").html("您的评分");
        $(".PFLayer,.Mask").toggle();
        //下面调用AJAX
        var TT = $("#myScore").attr("data-generalid");
        var dataType = $("#myScore").attr("data-type");
        var tips = $("#myScore").attr("data-tips");
        var cookieKey = "WapPL" + TT;
        if ($.cookie(cookieKey) !== undefined) {
            alert("对不起，您已经提交过评分！");
        }
        else {
            $.ajax({
                type: "GET",
                dataType: "jsonp",
                url: "http://db2.gamersky.com/RatingJsonpAjax.aspx",
                data: { 'Rating': JSON2.stringify({ "GenneralId": TT, 'Sorce': num, 'Type': dataType }), 'Action': "rating" },
                success: function (data) {
                    if (data.hasOwnProperty("status")) {
                        switch (data.status) {
                            case "err":
                                alert("提交" + tips + "错误！");
                                break;
                            case "existuser":
                            case "existip":
                                alert("已" + tips + "！");
                                break;
                            default:
                                break;
                        }
                    }
                    else {
                        $.cookie(cookieKey, num, { path: "/", expires: 365 });
                    }
                }
            });
        }
    });
    $(".Mask").click(function () {
        if ($(".Nav_NTBtn").css("display") !== "none") { $(".Nav_NTBtn").toggle(); }
        if ($(".Nav_NBBtn").css("display") !== "none") { $(".Nav_NBBtn").toggle(); }
        if ($(".PFLayer").css("display") !== "none") { $(".PFLayer").toggle(); }
        $(".Mask").toggle();
    });

    //首页 切换
    if ($("div").hasClass("Navhome") == true) {
        $(".Navhome .nh").each(function (i) {
            $(this).click(function () {
                $(".Navhome .nh").attr("class", "nh").eq(i).attr("class", "nh cur");
                $(".PicList").attr("class", "PicList none").eq(i).attr("class", "PicList block");
                $("#LoadMore").attr("value", $(".PicList.block").attr("value"));
            });
        });
    }

    //游戏库导航
    if ($("div").hasClass("Naving") == true) {
        var $Naving = $(".Naving"), Ntop = $Naving.offset().top;
        $(window).scroll(function () {
            if ($(window).scrollTop() >= Ntop) {
                $Naving.attr("style", "box-shadow:0 0 5px #888;position:fixed;top:0;left:0;z-index:10;");
            } else {
                $Naving.attr("style", "");
            }
        });
        /*导航切换*/
        $(".tab_item").each(function (i) {
            $(this).click(function () {
                if ($(this).html() == "全部") { $(".ABC").css("display", "block"); }
                if ($(this).html() == "推荐") { $(".ABC").css("display", "none"); }
                $(".tab_item").attr("class", "tab_item").eq(i).attr("class", "tab_item cur");
                $(".PicList").attr("class", "PicList none").eq(i).attr("class", "PicList block");
                $("#LoadMore").attr("value", $(this).attr("value"));
            });
        });
    }

    //攻略列表字母层
    if ($("div").hasClass("ABC") == true) {
        var ABCw = -$(".ABC .abc2").outerWidth();
        var ABCh = ($(window).outerHeight() - $(".ABC").outerHeight()) / 2;
        $(".ABC").attr("style", "top:" + ABCh + "px;right:" + ABCw + "px;");
        function shenzhan() {
            if ($(".abc1").attr("class") == "abc1") {
                $(".abc1").toggleClass("bgc"); $(".ABC").stop().animate({ "right": 0 }, 200);
            } else {
                $(".ABC").stop().animate({ "right": ABCw }, 100, function () { $(".abc1").toggleClass("bgc"); });
            }
        }
        $(".abc1,.sh,.zm").mousedown(function () {
            shenzhan();
            if ($(this).attr("class") == "zm") {
                $(this).addClass("cur").siblings(".zm").removeClass("cur");
                $('html,body').animate({ scrollTop: $("#" + $(this).html()).offset().top - 50 }, 1);
            }
        });
        $(".PicList").mousedown(function () { if ($(".abc1").attr("class") !== "abc1") { shenzhan(); } });
        $(window).scroll(function () { if ($(".abc1").attr("class") !== "abc1") { shenzhan(); } });
    }

    if ($("div").hasClass("Navku") == true) {
        //游戏库内容页 新闻+攻略+下载 切换
        $(".nku").each(function (i) {
            $(this).click(function () {
                $(".nku").attr("class", "nku").eq(i).attr("class", "nku cur");
                $(".TextList").attr("class", "TextList none").eq(i).attr("class", "TextList block");
            });
        });
        //游戏库内容页 最低配置+推荐配置 切换
        $(".npz").each(function (i) {
            $(this).click(function () {
                $(".npz").attr("class", "npz").eq(i).attr("class", "npz cur");
                $(".pz").attr("class", "pz none").eq(i).attr("class", "pz block");
            });
        });
        //视频内容页 详情+评论 切换
        $(".nsp").each(function (i) {
            $(this).click(function () {
                $(".nsp").attr("class", "nsp").eq(i).attr("class", "nsp cur");
                $(".XQPL").attr("class", "XQPL none").eq(i).attr("class", "XQPL block");
            });
        });
    }

    //滚动显示返回顶部图标
    $(window).scroll(function () {
        if ($("a").hasClass("back-to-top") == false) { $("body").prepend('<a class="back-to-top" href="#top" style="display:none;"><i class="ico-arrow-t">∧</i></a>'); }
        if ($(window).scrollTop() > 100 && $(window).scrollTop() < $(document).height() - $(window).height() - 100) {
            $(".back-to-top").fadeIn(400);
        } else {
            $(".back-to-top").fadeOut(400);
        }
    });

    //当点击跳转链接后，回到页面顶部位置
    $(".back-to-top").click(function () { $('body,html').animate({ scrollTop: 0 }, 400); return false; });

    //幻灯片
    if ($("div").hasClass("slider") == true) {
        var bullets = document.getElementById("position").getElementsByTagName("li");
        var banner = Swipe(document.getElementById("mySwipe"), {
            auto: 4000,
            continuous: true,
            disableScroll: false,
            callback: function (pos) {
                var i = bullets.length;
                while (i--) { bullets[i].className = ""; }
                bullets[pos].className = "cur";
            }
        });
    }

});
