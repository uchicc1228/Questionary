<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackIndexReWrite.aspx.cs" Inherits="Questionary.Pages.Back.BackIndexReWrite" %>

<%@ Register Src="~/Pages/Back/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../JavaScript/jquery/jquery.js"></script>
    <script src="../../JavaScript/bootstrap/bootstrap.js"></script>
    <style>
        div {
            border: 0px solid black;
        }

       body {
            width: 100%;
            height: 100%;
            background: #F7F7F7;
            padding: 25px 15px 25px 10px;
            font: 18px Georgia, "Times New Roman", Times, serif;
            color: #888;
            text-shadow: 1px 1px 1px #FFF;
            background-position-x: center;
            overflow-x: hidden;
            background-repeat: no-repeat;
        }

        .divSearch {
            position: relative;
            left: 30%;
            width: 500px;
            padding: 20px;
            height: 100px;
            margin-bottom : 50px;
        }

        .btnsearch {
            position: relative;
            float: right;
        }


        #divlist {
            width: 80%;
            height: 100%;
            left: 14%;
            position: relative;
            float: left;
        }

        #qq {
            float: left;
            width: 150px;
         
            padding-left:20px;
            border-right:1px solid gray;
        }


        .btnall {
            position: relative;
            left: 10%;
            margin-top: 20px;
        }

        #divpager{
            padding-top:10px;
           position:relative;
           left:40%;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server" class="weee">
        <div class="divtitle">
            <h1>問卷管理</h1>
        </div>
        <hr />
        <div class="divSearch">
             <label for="titlesearch" style="padding-right: 62px;">標題</label>
            <asp:TextBox ID="titlesearch" runat="server" placeholder="請輸入問卷標題"></asp:TextBox> <br />
            <label for="titlesearch" style="padding-right: 15px;">開始 / 結束</label>
            <asp:TextBox TextMode="Date" runat="server" ID="txtCalender_start"></asp:TextBox>
            <asp:TextBox TextMode="Date" runat="server" ID="txtCalender_end"></asp:TextBox>
            <asp:Button ID="btnsearch" Text="搜尋" OnClick="btnsearch_Click" runat="server" />
            <literal id="ltl1"></literal>
            <br />
            <asp:Button ID="btnDel" CssClass="btnall" runat="server" OnClick="btnDel_Click" Text="刪除問卷" OnClientClick="return deletethese()" />
            <asp:Button runat="server" ID="btnAddList" CssClass="btnall" Text="新增問卷" OnClick="btnAddList_Click" />
        </div>
        <div id="qq">
            <a href="BackIndexReWrite.aspx">問卷管理</a><br />
            <a href="BaseQuestion.aspx">常用問題管理</a>
        </div>
        <div id="divlist">
            <asp:Repeater runat="server" ID="rptList">
                <HeaderTemplate>
                    <table border="1" style="width: 80%; text-align: center;">
                        <tr>
                            <td><b></b></td>
                            <td><b>#</b></td>
                            <td><b>問卷</b></td>
                            <td><b>狀態</b></td>
                            <td><b>開始時間</b></td>
                            <td><b>結束時間</b></td>
                            <td><b>觀看統計</b></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBox3" />
                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("ID")%>' />
                        </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "Number") %> </td>
                        <td><a href="<%# DataBinder.Eval(Container.DataItem, "QuestionaryEditUrl")%>"><%# DataBinder.Eval(Container.DataItem, "Title") %></a></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "Status") %> </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "StartTime_string") %> </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "EndTime_string") %> </td>
                        <td><a href="../Front/FrontStastic.aspx?QID=<%# DataBinder.Eval(Container.DataItem, "ID")%>">觀看統計</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>            
                </FooterTemplate>
            </asp:Repeater>
            <div  id="divpager" >
                <uc1:ucPager runat="server" ID="ucPager" />
            </div>
            <asp:PlaceHolder runat="server" ID="plcEmpty" Visible="false">
                <p>尚未有資料 </p>
                  
            </asp:PlaceHolder>
          

        </div>
    </form>
    <script>
        function deletethese() {
            var yes = confirm('這麼做會一次刪除所有選擇問卷,您確定嗎?')
            if (yes) {
            } else {
                return false;
            }
        }
        $(document).ready(function () {
            $("#btnsearch").click(function () {
                var title = $("input[placeholder='請輸入問卷標題'").val().trim();
                var time_start = $("#txtCalender_start").val();
                var time_end = $("#txtCalender_end").val();
                console.log(time_start);
                if (time_start > time_end) {
                    alert('哪有人開始日期比結束日期大的')
                    return false;
                }          
            });
  
        })
    </script>
</body>
</html>
