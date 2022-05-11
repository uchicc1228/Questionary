<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="Questionary.ShareControls.ucPager" %>
<div class="Pager">
    <a runat="server" visible="false" id="aLinkFirst" href="AddQuestionaryMultiView.aspx?Index=1">第一頁 </a>
    <a runat="server" visible="false" id="aLinkPrev" href="AddQuestionaryMultiView.aspx?Index=1">上一頁 </a>

    <a runat="server" id="aLinkPage1" href="AddQuestionaryMultiView.aspx?Index=1">1 </a>
    <a runat="server" id="aLinkPage2" href="">2</a>
    <a runat="server" id="aLinkPage3" href="AddQuestionaryMultiView.aspx?Index=3">3</a>
    <a runat="server" id="aLinkPage4" href="AddQuestionaryMultiView.aspx?Index=4">4</a>
    <a runat="server" id="aLinkPage5" href="AddQuestionaryMultiView.aspx?Index=5">5</a>



    <a runat="server" visible="false" id="aLinkNext" href="AddQuestionaryMultiView.aspx?Index=3">下一頁 </a>
    <a runat="server"  visible="false" id="aLinkLast" href="AddQuestionaryMultiView.aspx?Index=10">最未頁 </a>
</div>
