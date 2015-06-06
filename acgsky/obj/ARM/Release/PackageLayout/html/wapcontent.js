// JavaScript Document
 $(document).ready(function () {
            $("#SelectPage").change(function () {
                var id = $("#countn").attr("generalid");
                window.location.href = "Content-"+id+"_" + $("#SelectPage").val() + ".html";
            });

            for (var x = 0; x < $(".Mid_2 img").length; x++) {
                $(".Mid_2 img")[x].width = document.body.clientWidth;
            }

            for (var x = 0; x < document.getElementsByTagName("img").length; x++) {
                if(document.getElementsByTagName("img")[x].width>=document.body.clientWidth && document.getElementsByTagName("img")[x].id !="advertisement")
                {
                    document.getElementsByTagName("img")[x].width = document.body.clientWidth-20;
                }
            }

            for (var m = 0; m < document.getElementsByTagName("span").length; m++)
            {
                if (document.getElementsByTagName("span")[m].style.color == "rgb(255, 204, 0)")
                {
                    document.getElementsByTagName("span")[m].style.color = "#d26217";
                }
                if (document.getElementsByTagName("span")[m].color == "#ffcc00" || document.getElementsByTagName("span")[m].color == "rgb(255, 204, 0)")
                {
                    document.getElementsByTagName("span")[m].color = "#d26217";
                }
                //翠绿色替换
                if (document.getElementsByTagName("span")[m].color == "#00ee00")
                {
                    document.getElementsByTagName("span")[m].color = "#228B22";
                }
            }
            for (var t = 0; t < document.getElementsByTagName("strong").length; t++) {
                if (document.getElementsByTagName("strong")[t].style.color == "rgb(255, 204, 0)" ) {
                    document.getElementsByTagName("strong")[t].style.color = "#d26217";
                }
                if (document.getElementsByTagName("strong")[t].color == "#ffcc00" || document.getElementsByTagName("strong")[t].color == "rgb(255, 204, 0)") {
                    document.getElementsByTagName("strong")[t].color = "#d26217";
                }
                //翠绿色替换
                if (document.getElementsByTagName("strong")[t].color == "#00ee00") {
                    document.getElementsByTagName("strong")[t].color = "#228B22";
                }
            }

            for (var n = 0; n < document.getElementsByTagName("font").length; n++)
            {
                if (document.getElementsByTagName("font")[n].style.color == "rgb(255, 204, 0)") {
                    document.getElementsByTagName("font")[n].style.color = "#d26217";
                }
                if (document.getElementsByTagName("font")[n].color == "#ffcc00" || document.getElementsByTagName("font")[n].color == "rgb(255, 204, 0)") {
                    document.getElementsByTagName("font")[n].color = "#d26217";
                }
                //翠绿色替换
                if (document.getElementsByTagName("font")[n].color == "#00ee00") {
                    document.getElementsByTagName("font")[n].color = "#228B22";
                }
				//黄色替换
                if (document.getElementsByTagName("font")[n].color == "#FFFF00") {
                    document.getElementsByTagName("font")[n].color = "#ff6000";
                }
            }
            var iframeLength = document.getElementsByTagName("iframe").length;
            var embedLenfth = document.getElementsByTagName("embed").length;
            if (iframeLength > 0) {
                for (var i = 0; i < iframeLength; i++)
                {
                    var iframeElements = document.getElementsByTagName("iframe")[i];
					if(iframeElements.style["display"]!="none")
					{
						iframeElements.removeAttribute("style");
					}
					iframeElements.height = document.body.clientWidth * (9 / 16);
					iframeElements.width = document.body.clientWidth-20;
                }

            }
            if (embedLenfth > 0) {
                for (var n = 0; n < embedLenfth; n++)
                {
                    if(document.getElementsByTagName("embed")[n].style["display"]!="none")
                    {
                        document.getElementsByTagName("embed")[n].removeAttribute("style");
                    }
                    document.getElementsByTagName("embed")[n].height = document.body.clientWidth * (9 / 16);
                    document.getElementsByTagName("embed")[n].width = document.body.clientWidth-20;
                }

            }


        });
		//畅言评论自定义参数
			var _config = {
				sso:{
					onlySSO:true
				},
				registerUrl: 'http://u.gamersky.com/User/Register.aspx',
				hide_face: 1,
				showFloorNum:1
			};