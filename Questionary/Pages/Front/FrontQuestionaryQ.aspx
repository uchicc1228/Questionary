<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontQuestionaryQ.aspx.cs" Inherits="Questionary.Pages.Front.FrontQuestionaryQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../JavaScript/jquery/jquery.js"></script>
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

        .basic-grey h1 > span {
            display: block;
            font-size: 15px;
            text-align: center;
        }

        .alllabel {
            position: relative;
            left: 30%;
            padding-right: 20px;
            font-size: 20px;
        }

        #lblEmail {
            position: relative;
            left: 30%;
            padding-right: 10px;
            font-size: 20px;
        }

        .alltext {
            position: relative;
            left: 30%;
            height: 25px;
            margin: 5px;
        }

        .divquestion {
            position: relative;
            left: 30%;
            font-size: 20px;
        }

            .divquestion > p {
                padding: 5px;
            }

        .aspNetDisabled {
            padding: 5px;
        }

        .title {
            font-size: 50px;
            text-align:center;
        }

        #lblContent{
            font-size : 30px;
            text-align:center;
        }
     
    </style>
</head>
<body>
    <form id="form1" runat="server" class="basic-grey">
        <div>
            <div class="title">
                <asp:Label runat="server" ID="lblTitle" CssClass="title"><span></span></asp:Label><br />
                <asp:Label runat="server" ID="lblContent" CssClass="content"><span></span></asp:Label>
                <asp:HiddenField runat="server" ID="hfID" />
            </div>

            <br />
            <label>
                <span class="alllabel">姓名</span>
                <asp:TextBox runat="server" ID="txtName" CssClass="alltext"></asp:TextBox><br />
            </label>
            <label>
                <span class="alllabel">手機</span>
                <asp:TextBox runat="server" ID="txtPhone" CssClass="alltext"></asp:TextBox><br />
            </label>
            <label>
                <span id="lblEmail" style="padding-right: 8px;">Email</span>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="alltext"></asp:TextBox><br />
            </label>
            <label>
                <span class="alllabel">年齡</span>
                <asp:TextBox runat="server" ID="txtAge" CssClass="alltext"></asp:TextBox><br />
            </label>
            <hr />
            <div class="divquestion">
                <asp:PlaceHolder runat="server" ID="plcQuestion"></asp:PlaceHolder>
                <br />
                <input runat="server" type="button" id="btnyes"  value="送出" />

            </div>
        </div>
    </form>
    <script>       
        $(document).ready(function () {
            $("#btnyes").click(function () {
                /*檢查必填*/
                var chk = ($("div[class='required'] > input[id^='AchkY']:checked")).length;
                var rdo = ($("div[class='required'] > input[id^='ArdoY']:checked")).length;
                var RdoListCount = $("div[id^='rdo'][class='required']").length;//1
                var ChkListCount = $("div[id^='chk'][class='required']").length;//1
                var txtListCount = $("div[id^='txt'][class='required']").length;//1
                var txtname = document.getElementById('txtName').value;
                var txtphone = document.getElementById('txtPhone').value;
                var txtemail = document.getElementById('txtEmail').value;
                var txtage = document.getElementById('txtAge').value;
                var item = $("label[id^='Q']").length;//3

                const rulesemil = /^([\w\.\-]){1,64}\@([\w\.\-]){1,64}$/;
                const rulesage = /[1-99]/;

                for (i = 0; i < RdoListCount; i++) {
                    var wee0 = $(`div[id='rdorequired${i}'] > input[type="radio"]:checked`).length;
                    if (wee0 == 0) {
                        alert('請注意單選項目');
                        return false;
                    }
                }
                for (k = 0; k < ChkListCount; k++) {
                    var wee1 = $(`div[id='chkrequired${k}'] > input[type="checkbox"]:checked`).length;
                    if (wee1 == 0) {
                        alert('請注意複選項目');
                        return false;
                    }
                }
                for (k = 0; k < txtListCount; k++) {
                    var wee1 = document.getElementById(`A3txt${k}`).value
                    if (wee1 == "") {
                        alert('請注意文字項目');
                        return false;
                    }
                }
               

                if (txtname == '') {
                    alert('姓名必填');
                    return false;
                }
                if (txtphone == '') {
                    alert('手機必填');
                    return false;
                }
                if (txtemail == '') {
                    alert('信箱必填');
                    return false;
                }
                if (txtage == '') {
                    alert('年齡必填');
                    return false;
                }
                if (rulesemil.test(txtemail) == false) {
                    alert('email怪怪的');
                    return false;
                }

                if (rulesage.test(txtage) == false){
                    alert('年齡怪怪的');
                    return false;
                }



              


              /*  /////////////////////////*/
                let questiontext = "";
                let A = "";
                for (z = 0; z < item; z++) {
                    let question = $("label[id^='Q']").get(`${z}`);
                    A = question.textContent;
                    questiontext = questiontext.concat(A, " ")

                }


                let ans = "";
                let AllAnswer = $("input[id^='A']").get();
                for (var item of AllAnswer) {
                    if (item.type == "radio" && item.checked) {
                        ans += item.id + " ";
                    }
                    if (item.type == "checkbox" && item.checked) {
                        ans += item.id + " ";
                    }
                    if (item.type == "text") {
                        ans += `${item.id}=${item.value}` + " ";
                    }
                }
                var cc = $("#hfID").val();
                var postData = {
                    "Answer": ans,
                    "UserName": $("#txtName").val(),
                    "UserPhone": $("#txtPhone").val(),
                    "UserEmail": $("#txtEmail").val(),
                    "UserAge": $("#txtAge").val(),
                    "Question": questiontext,
                };
                console.log('ans:' + ans);
                console.log('hfid:' + cc);
                console.log('data:' + postData);
                $.ajax({
                    url: "../../API/AnswerHandler.ashx?QID=" + $("#hfID").val(),
                    method: "POST",
                    data: postData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "success") {
                            window.location.href = "FrontQuestionConfirm.aspx?ID=" + $("#hfID").val();
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });





            });
        })
    </script>
</body>

</html>
