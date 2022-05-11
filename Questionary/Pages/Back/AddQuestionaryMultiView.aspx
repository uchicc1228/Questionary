<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestionaryMultiView.aspx.cs" Inherits="Questionary.Pages.Back.AddQuestionaryMultiView" %>

<%@ Register Src="~/Pages/Back/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>
<%@ Register Src="~/ShareControls/LeftControls.ascx" TagPrefix="uc1" TagName="LeftControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後台管理-新增問卷</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"
        integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk"
        crossorigin="“anonymous" />
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

        .divTitle {
            padding-bottom: 100px;
        }

        .all {
            position: relative;
            width: 70%;
            text-align: left;
            padding: 0px;
            margin: 0px;
            left: 10%;
            float: left;
        }

        label {
            position: relative;
            left: 10px;
            padding-right: 10px;
        }

        .allbutton {
            position: relative;
            left: 15%;
        }

        .formbutton {
            position: relative;
            left: 20%;
        }

        .aspNetDisabled {
            padding: 5px;
        }

        .multi2 {
            padding-bottom: 100px;
        }

            .multi2 > table {
                color: dimgray;
                border: 0.5px;
            }

        .multi3 {
            padding-bottom: 100px;
        }

            .multi3 > table {
                color: dimgray;
                border: 0.5px;
                border-top: 0.5px solid black;
            }

        #left {
            float: left;
        }
        #lblContent{
           padding:3px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="divTitle">
            <asp:Literal runat="server" ID="ltlmesg"></asp:Literal>
            <uc1:LeftControls runat="server" ID="LeftControls" />
        </div>
        <div id="left">
            <a href="BackIndexReWrite.aspx">問卷管理</a><br />
            <a href="BaseQuestion.aspx">常用問題管理</a>
        </div>
        <div class="all">

            <div class="divBtn">

                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:LinkButton runat="server" ID="AddQuestionaryBtn" class="nav-link active" aria-current="page" OnClick="AddQuestionaryBtn_Click">新增問卷</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton runat="server" ID="EditQuestionBtn" class="nav-link active" aria-current="page" OnClick="EditQuestionBtn_Click">編輯問題</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton runat="server" ID="FilledInfoBtn" class="nav-link active" aria-current="page" OnClick="FilledInfoBtn_Click">填寫內容</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton runat="server" ID="StatisticsBtn" class="nav-link active" aria-current="page" OnClick="StatisticsBtn_Click">統計資料</asp:LinkButton>
                    </li>
                </ul>


            </div>

            <div class="divMulti">
                <asp:MultiView ID="MultiQuestionary" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ViewAddQuestionary" runat="server">
                        <uc1:LeftControls runat="server" ID="LeftControls2" Visible="true" />
                        <asp:Literal runat="server" ID="ltlmeess"></asp:Literal><br />
                        <label><b>問卷名稱</b></label>
                        <asp:TextBox runat="server" ID="TextQuestionaryTitle"></asp:TextBox><br />
                        <label style="float: left; padding-right: 15px;"><b>描述內容</b></label>
                        <asp:TextBox runat="server" ID="TextQuestionaryContent" Style="width: 250px;" TextMode="MultiLine" Rows="3" MaxLength="200"></asp:TextBox><br />
                        <label><b>開始時間</b></label>
                        <asp:TextBox runat="server" ID="TextQuestionaryStartTime" TextMode="Date"></asp:TextBox><br />
                        <label><b>結束時間</b></label>
                        <asp:TextBox runat="server" ID="TextQuestionaryEndTime" TextMode="Date"></asp:TextBox><br />
                        <label><b>啟用</b></label>
                        <asp:CheckBox runat="server" ID="ChkisEnable" Checked="true" value="0" />
                        <asp:Button runat="server" ID="Add" OnClick="Add_Click" Text="新增" CssClass="allbutton" />
                        <asp:Button runat="server" OnClick="Empty_Click" ID="Empty" Text="重填" CssClass="allbutton" Style="padding-left: 10px;" /><br />
                        <asp:Literal ID="ltlmsg" runat="server"></asp:Literal>
                    </asp:View>
                    <asp:View ID="ViewEditQuestion" runat="server">
                        <div>
                            <label><b>問卷名稱</b></label>
                            <asp:Label runat="server" ID="lblTitle"></asp:Label>
                            <br />


                            <label><b>描述內容</b></label>
                            <asp:Label runat="server" ID="lblContent"></asp:Label> <br />

                            <label><b>開始時間</b></label>
                            <asp:Label runat="server" ID="lblStartTime"></asp:Label><br />

                            <label><b>結束時間</b></label>
                            <asp:Label runat="server" ID="lblEndTime"></asp:Label><br />

                            <label><b>種類</b></label>
                            <asp:DropDownList runat="server" ID="dowList" AutoPostBack="true" OnSelectedIndexChanged="dowList_SelectedIndexChanged">
                            </asp:DropDownList><br />


                            <label><b>問題</b></label>
                            <asp:TextBox runat="server" ID="txtQuestion" placeholder="輸入問題" OnTextChanged="txtQuestion_TextChanged" AutoPostBack="true"></asp:TextBox>


                            <asp:DropDownList runat="server" ID="dowMode" AutoPostBack="true" OnSelectedIndexChanged="dowMode_SelectedIndexChanged">
                                <asp:ListItem Value="0">單選方塊</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">複選方塊</asp:ListItem>
                                <asp:ListItem Value="2">文字</asp:ListItem>
                            </asp:DropDownList><br />


                            <label><b>回答</b></label>
                            <asp:TextBox runat="server" ID="txtanswer" placeholder="輸入回答 多個回答請以;分隔" OnTextChanged="txtanswer_TextChanged" AutoPostBack="true"></asp:TextBox><br />


                            <label><b>是否必填</b></label>
                            <asp:CheckBox runat="server" ID="checknecessary" OnCheckedChanged="checknecessary_CheckedChanged" />
                            <asp:Button runat="server" ID="btnconfirmQ" Text="加入問題" OnClick="btnconfirmQ_Click" CssClass="allbutton" /><br />

                            <asp:Literal runat="server" ID="ltlmsg1">**多個回答請已;分開</asp:Literal><br />
                            <asp:Literal runat="server" ID="lglmsg2"> **若編輯完畢請按送出</asp:Literal>
                            <br />
                            <asp:Button runat="server" ID="btnFinalConfirm" Text="確認編輯" OnClick="btnFinalConfirm_Click" CssClass="formbutton" />
                            <asp:Button runat="server" ID="btnFinalCancek" Text="取消" OnClick="btnFinalCancek_Click" CssClass="formbutton" />
                            <div class="multi2">
                                <asp:Repeater runat="server" ID="ret1" OnItemCommand="ret1_ItemCommand">
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <asp:CheckBox runat="server" ID="CheckBox3" />
                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("QuestionID")%>' />
                                            </td>

                                            <td>
                                                <asp:Label runat="server" Visible="false" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "QuestionID") %>'></asp:Label>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "Question") %> </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "QQMode") %> </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "QIsNecessary") %> </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_Click" Text="編輯" CommandName="btnEdit" CommandArgument='<%# Eval("QID") +","+ Eval("Number") + "," + Eval("QuestionID")%>' /></td>
                                            <td>
                                                <asp:Button runat="server" ID="btnDelete" Text="刪除" CommandName="btnDelete" CommandArgument='<%# Eval("QID")+ ","+ Eval("QuestionID") %>' /></td>
                                        </tr>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table border="1" style="width: 80%; text-align: center;">
                                            <tr>
                                                <td></td>
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
                            <asp:Button runat="server" ID="btnsession" Text="session" Visible="false"  OnClick="btnsession_Click1" />
                                <asp:Button runat="server" ID="btnDel" Text="批量刪除" OnClick="btnDel_Click" />
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="ViewFilledInfo" runat="server">
                        <asp:Button runat="server" ID="getout" Text="匯出" OnClick="getout_Click" />
                        <asp:PlaceHolder runat="server" ID="plcall">
                            <div class="multi3">
                                <asp:Repeater runat="server" ID="repeaterGetout" OnItemCommand="repeaterGetout_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.ItemIndex + 1 %></td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "UserName") %> </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "UserWriteTime_string") %> </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btnUserID" Text="觀看統計" OnClick="btnUserID_Click" CommandName="btnUserID" CommandArgument='<%# Eval("UserID") + "," + Eval("QID")%>'></asp:LinkButton></td>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table border="1" style="width: 80%; text-align: center;">
                                            <tr>
                                                <td><b>#</b></td>
                                                <td><b>姓名</b></td>
                                                <td><b>填寫時間</b></td>
                                                <td><b>觀看細節</b></td>
                                            </tr>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </table>
                                   
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="plcone" Visible="false">
                  
                            <asp:Literal runat="server" ID="oneforLiteral"></asp:Literal>
                            <asp:Literal runat="server" ID="ltlPlcMsg"></asp:Literal>
                            <label>姓名</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_plconeUserName"></asp:TextBox>
                            <br />
                            <label>手機</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_plconeUserPhone"></asp:TextBox><br />
                            <label>eMail</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_plconeUsereMail"></asp:TextBox><br />
                            <label>年齡</label>
                            <asp:TextBox runat="server" Enabled="false" ID="txt_plconeUserAge"></asp:TextBox><br />


                        </asp:PlaceHolder>

                        <uc1:ucPager runat="server" ID="ucPager" />
                    </asp:View>
                    <asp:View ID="ViewStatistics" runat="server">
                        <asp:PlaceHolder ID="plcQuestion" runat="server"></asp:PlaceHolder>


                    </asp:View>
                </asp:MultiView>
            </div>
        </div>


    </form>
</body>
<script>

</script>
</html>
