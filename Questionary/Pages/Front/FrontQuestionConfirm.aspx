<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontQuestionConfirm.aspx.cs" Inherits="Questionary.Pages.Front.FrontQuestionConfirm" %>

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
            font: 25px Georgia, "Times New Roman", Times, serif;
            color: #888;
            text-shadow: 1px 1px 1px #FFF;
            border: 1px solid #E4E4E4;
            background-position-x: center;
            overflow-x: hidden;
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3><b>您輸入的資料為：</b></h3>
            <label>問卷標題：</label>
            <asp:Label runat="server" ID="lblTtile"></asp:Label>
            <br />
            <label>問卷內容：</label>
            <asp:Label runat="server" ID="lblContent"></asp:Label>
            <br />
            <label>填寫人姓名：</label>
            <asp:Label runat="server" ID="lblName"></asp:Label>
            <br />
            <label>填寫人信箱：</label>
            <asp:Label runat="server" ID="lblEmail"></asp:Label>
            <br />
            <label>填寫人年齡：</label>
            <asp:Label runat="server" ID="lblAge"></asp:Label>
            <br />
            <label>填寫人電話：</label><asp:Label runat="server" ID="lblPhone"></asp:Label>
            <asp:Literal runat="server" ID="ltlRdo"></asp:Literal>
            <asp:HiddenField ID="hfq" runat="server" />
        </div>
        <div class="divquestion">
                <asp:PlaceHolder runat="server" ID="plcQuestion"></asp:PlaceHolder>
                <br />           
            </div>
        <asp:Button runat="server" ID="btnconfirm" Text="確定並送出" OnClick="btnconfirm_Click"  />
        <br />
        <asp:Button runat="server" ID="btncancel" Text="取消" OnClick="btncancel_Click"  />
    </form>
</body>
</html>
