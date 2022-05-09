<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseQuestion.aspx.cs" Inherits="Questionary.Pages.Back.BaseQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
  
        body {
            width: 100%;
            height: 100%;
            background: #F7F7F7;
            padding: 25px 15px 25px 10px;
            font: 18px Georgia, "Times New Roman", Times, serif;
            color: #888;
            text-shadow: 1px 1px 1px #FFF;
            border: 1px solid #E4E4E4;
            background-position-x: center;
            overflow-x: hidden;
            background-repeat: no-repeat;
        }
         #qq {
         
            width: 150px;        
            padding-left:20px;
            border-right:1px solid gray;
        }


        .formbutton {
            position: relative;
            left: 20%;

        }

         .btnsearch {
            position: relative;
            left:20%;
            width:70%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>常用問題管理</h1>
          <div id="qq">
            <a href="BackIndexReWrite.aspx">問卷管理</a><br />
            <a href="BaseQuestion.aspx">常用問題管理</a>
        </div>
        <div class="btnsearch">
            <label>問題</label>
            <asp:TextBox runat="server" ID="txtQuestion" OnTextChanged="txtQuestion_TextChanged" AutoPostBack="true" placeholder="輸入問題"></asp:TextBox>
            <asp:DropDownList runat="server" ID="dowMode"  OnSelectedIndexChanged="dowMode_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">單選方塊</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">複選方塊</asp:ListItem>
                <asp:ListItem Value="2">文字</asp:ListItem>
            </asp:DropDownList><br />
            <label>回答</label>
            <asp:TextBox runat="server" ID="txtanswer" placeholder="輸入回答 多個回答請以,分隔" AutoPostBack="true" OnTextChanged="txtanswer_TextChanged"></asp:TextBox><br />
            <label>是否必填</label>
            <asp:CheckBox runat="server" ID="checknecessary" />

            <asp:Button runat="server" ID="btnconfirmQ" Text="加入問題" OnClick="btnconfirmQ_Click" /><br />

            <asp:Label runat="server" ID="lblmsg"></asp:Label>
            <asp:Literal runat="server" ID="ltlmsg1">&nbsp&nbsp**多個回答請已;分開</asp:Literal><br />
            <asp:Literal runat="server" ID="lglmsg2">&nbsp&nbsp**若編輯完畢請按送出</asp:Literal>

            <asp:Button runat="server" ID="btnFinalConfirm" Text="確認編輯" OnClick="btnFinalConfirm_Click" CssClass="formbutton" />
            <asp:Button runat="server" ID="btnFinalCancek" Text="取消" OnClick="btnFinalCancek_Click" CssClass="formbutton" />
        </div>
        <div class="btnsearch" style="">
            <asp:Repeater runat="server" ID="ret1" OnItemCommand="ret1_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" Visible="false" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "Number") %>'></asp:Label>
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "Question") %> </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "QQMode") %> </td>
                        <td><%# DataBinder.Eval(Container.DataItem, "QIsNecessary") %> </td>
                        <td>
                            <asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_Click" Text="編輯" CommandName="btnEdit" CommandArgument='<%# Eval("QuestionID")%>' /></td>
                        <td>
                            <asp:Button runat="server" ID="btnDelete" Text="刪除" CommandName="btnDelete" CommandArgument='<%# Eval("QuestionID") %>' /></td>
                    </tr>
                </ItemTemplate>
                <HeaderTemplate>
                    <table border="1" style="width: 80%; text-align: center;">
                        <tr>

                            <td><b>#</b></td>
                            <td><b>問題</b></td>
                            <td><b>模式</b></td>
                            <td><b>是否必填</b></td>
                            <td><b>編輯</b></td>
                            <td><b>刪除</b></td>
                        </tr>
                </HeaderTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


        </div>
    </form>
</body>
</html>
