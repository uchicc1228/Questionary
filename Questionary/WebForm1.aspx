<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Questionary.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="JavaScript/bootstrap/bootstrap.js"></script>
    <script src="JavaScript/jquery/jquery.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script>
        $(document).ready(function () {

            (function ($) {
                $.fn.tablepage = function (oObj, dCountOfPage) {

                    var dPageIndex = 1;
                    var dNowIndex = 1;
                    var sPageStr = "";
                    var dCount = 0;
                    var oSource = $(this);
                    var sNoSelColor = "white"; //背景色(未被選擇)
                    var sSelColor = "#CCCCCC"; //背景色(被選擇)
                    var sFontColor = "black";  //字體顏色

                    change_page_content();

                    function change_page_content() {
                        //取得資料筆數
                        dCount = oSource.children().children().length - 1;

                        //顯示頁碼
                        sPageStr = "<table><tr><td style='height:30px;'><b>第</b></td>";

                        dPageIndex = 1;

                        for (var i = 1; i <= dCount; i += dCountOfPage) {
                            if (dNowIndex == dPageIndex) {
                                sPageStr += "<td valign='top'><table style='width:20px;height:20px;cursor:pointer;color:" + sFontColor + ";border-collapse:collapse;border-style:solid;border-width:1px;border-color:" + sSelColor + ";background-color:" + sSelColor + "'><tr><th>" + (dPageIndex++) + "</th></tr></table></td>";
                            }
                            else {
                                sPageStr += "<td valign='top'><table style='width:20px;height:20px;cursor:pointer;color:" + sFontColor + ";border-collapse:collapse;border-style:solid;border-width:1px;border-color:" + sNoSelColor + ";background-color:" + sNoSelColor + "'><tr><th>" + (dPageIndex++) + "</th></tr></table></td>";
                            }
                        }

                        sPageStr += "<td><b>頁</b></td></tr></table>";

                        oObj.html(sPageStr);

                        dPageIndex = 1;

                        //過濾表格內容
                        oSource.children().children("tr").each(function () {

                            if (dPageIndex <= (((dNowIndex - 1) * dCountOfPage) + 1) || dPageIndex > ((dNowIndex * dCountOfPage) + 1)) {
                                $(this).hide();
                            }
                            else {
                                $(this).show();
                            }

                            dPageIndex++;
                        });

                        oSource.children().children("tr").first().show(); //head一定要顯示

                        //加入換頁事件
                        oObj.children().children().children().children().each(function () {

                            $(this).click(function () {

                                dNowIndex = $(this).find("tr").text();

                                if (dNowIndex > 0) {
                                    change_page_content();
                                }
                            });
                        });
                    }
                };
            })
        })    
    </script>
</body>
</html>
