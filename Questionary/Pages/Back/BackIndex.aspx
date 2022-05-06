<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackIndex.aspx.cs" Inherits="Questionary.Pages.Back.BackIndex" %>

<%@ Register Src="~/ucleft.ascx" TagPrefix="uc1" TagName="ucleft" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後台頁面管理頁面</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"
        integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk"
        crossorigin="“anonymous" />
    <script src="../../JavaScript/bootstrap/bootstrap.js"></script>
    <script src="../../JavaScript/jquery/jquery.js"></script>
     <style>
        
        body {
            margin: 0px;
            background-repeat: no-repeat;
            background-size: 100%;
            background-position-x: center;
            overflow-x: hidden;
        }
        
        .divSearch {
            position: relative;
            left: 20%;
            width: 1000px;
            padding: 20px;
            height: 150px;
        }

        .btnsearch {
            position: relative;
            float: right;
        }

        .divlist {
            width: 80%;
            height: 100%;
            left: 18%;
            position: relative;
        }



        .list-group {
            position: relative;
            text-align: center;
            width: 100%;
            padding: 0px;
            margin: 0px;
        }

        .pagination {
           position: relative;
            left:40%;

        }

        .page-link {
            width: 100px;
            text-align: center;
            float: left;
            position:relative;
            left:35%;
        }
        .divtitle{
             
        }
        .divmenu{
            
            position:relative;
            left:0%;
        }
        .btnall{
            position:relative;
            left:10%;
            margin-top:20px;
           
        }

    </style>

</head>
<body>
    <form id="form1" runat="server" class="weee">
        <div class="Main">

            <div class="divtitle">
                <h1>問卷管理</h1>
            </div>
  
            <hr />
            <a href="BackIndex.aspx">問卷管理</a><br />
            <a href="BaseQuestion.aspx">後台問卷管理</a>
            <div class="divSearch">
                <label for="titlesearch" style="padding-right: 62px;">標題</label>
                <input type="text" id="titlesearch" placeholder="請輸入問卷標題" />
                <br />
                <label for="titlesearch" style="padding-right: 15px;">開始 / 結束</label>
                <input type="date" runat="server" id="txtCalender_start" />
                <input type="date" runat="server" id="txtCalender_end" />
                <input type="button" id="btnsearch" value="搜尋"  />
                <literal id="ltl1"></literal><br />
                <button id="btn1" class="btnall" >刪除問卷</button>
                <asp:Button runat="server" ID="btnAddList" CssClass="btnall"  OnClick="btnAddList_Click" Text="新增問卷" />
            </div>
            <div class="divlist">
                <div class="list-group" id="summarizing">
                </div>
            </div>
            <div class="Pagination">

                <label class="page-link" id="a" style="color: black; background-color: darkgrey;">1</label>
                <label class="page-link" id="b" style="color: black; background-color: darkgrey;">2</label>
                <label class="page-link" id="c" style="color: black; background-color: darkgrey;">3</label>
                <label class="page-link" id="d" style="color: black; background-color: darkgrey;">4</label>
                <label class="page-link" id="e" style="color: black; background-color: darkgrey;">5</label>
            </div>
        </div>
    </form>
    <script src="../../JavaScriptCode/Page.js"></script>
    <script>  
        $(document).ready(function () {
            //列出所有
            function BuildTable() {
                $.ajax({
                    url: "../../API/GetAllQuestionary.ashx",
                    method: "GET",
                    dataType: "JSON",
                    data: {
                        "ALL": 123
                    },
                    success: function (objDataList) {
                        console.log(objDataList);

                        var rowsText = `<thead>
                                <table class="table table-dark table-striped">
                        <thead>
                            <tr>
                                <th scope="col"></th>
                                <th scope="col">#</th>
                                <th scope="col">問卷</th>
                                <th scope="col">狀態</th>
                                <th scope="col">開啟時間</th>
                                <th scope="col">結束時間</th>
                                <th scope="col">觀看統計</th>
                            </tr>
                        </thead>`;
                        for (var item of objDataList) {
                            rowsText +=
                                `  
                        <tbody>
                            <tr>
                                <td><input type="checkbox"  name="checkboxQQ" value="${item.ID}" /></td>
                                <td>${item.Number}</td>
                                <td> <a href="${item.QuestionaryEditUrl}"> ${item.Title} </a> </td>
                                <td> ${item.Status} </td>
                                <td> ${item.StartTime_string} </td>
                                <td> ${item.EndTime_string} </td>
                                <td><a href="https://www.google.com/?hl=zh_tw">google</a></td>
                                <td><input type="hidden" class="hfID" name="hfID" value="${item.ID}"></td>
                                  
                            </tr>
                        </tbody>
              
                        `;
                        }
                        $("#summarizing").empty();
                        $("#summarizing").append("<table>" + rowsText + "</table>");
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            }
            BuildTable();
            //分頁

            //刪除
            var checkfunction = (function () {
                $('input[name=checkboxQQ]').on('change', function () {

                    var vals = $('input[name=hoge]:checked').map(function () {
                        return $(this).val();
                    }).get();
                    console.log(vals);
                });
            })
            $("#btn1").click(function () {
                var vals = $('input[name=checkboxQQ]:checked').map(function () {
                    return $(this).val();
                }).get();


                $.ajax({
                    url: "../../API/DeleteQuestionary.ashx?Action=Delete",
                    method: "POST",
                    data: { "ID": vals },
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "OK") {
                            BuildTable();
                            alert("刪除成功");
                            return;
                        }
                        if (txtMsg == "NULL") {
                            alert("刪除失敗，請聯絡管理員。");
                        }
                        else {
                            alert("請選擇問卷");
                            return;
                        }

                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });

            });
            BuildTable();
            BuildTable();
            $(function () {
                //在關鍵字textbox按下Enter，執行查詢
                $("input[name=keyword]").keyup(function (event) {
                    if (event.keyCode === 13) {
                        $("#btnsearch").click();
                    }
                });

                $("#btnsearch").click(function () {
                    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
                    var time_start = $("#txtCalender_start").val();
                    var time_end = $("#txtCalender_end").val();
                    var ID = $("#hfID").val();
                    if (time_start > time_end) {
                        alert('哪有人開始日期比結束日期大的')
                        return false;
                    }
                    if (title == "") {
                        alert("您未輸入任何標題文字，所以幫您選出日期內所有問卷");
                        /* return false;*/
                    }
                    $.ajax({
                        url: "../../API/GetQuestionary.ashx",
                        method: "GET",
                        data: {

                            "Title": title,
                            "Time_Start": time_start,
                            "Time_End": time_end
                        },
                        dataType: "JSON",
                        success: function (objDataList) {
                            //alert(time_start)
                            //alert(time_end)
                            console.log(objDataList);

                            var rowsText = `<thead>
                                <table class="table table-dark table-striped">
                        <thead>
                            <tr>
                                <th scope="col"></th>
                                <th scope="col">#</th>
                                <th scope="col">問卷</th>
                                <th scope="col">狀態</th>
                                <th scope="col">開啟時間</th>
                                <th scope="col">結束時間</th>
                                <th scope="col">觀看統計</th>
                            </tr>
                        </thead>`;
                            for (var item of objDataList) {
                                rowsText +=
                                    `  
                        <tbody>
                            <tr>
                                <td><input type="checkbox" name="checkboxQQ"  value="${item.ID}" /></td>
                                <td>${item.Number}</td>
                                <td> <a href="${item.QuestionUrl}">${item.Title}</a> </td>
                                <td> ${item.Status} </td>
                                <td> ${item.StartTime_string} </td>
                                <td> ${item.EndTime_string} </td>
                                <td><a href="https://www.google.com/?hl=zh_tw">google</a></td>
                                <td><input type="hidden" class="hfID" name="hfID" value="${item.ID}"></td>
                                  
                            </tr>
                        </tbody>
              
                        `;
                            }
                            $("#summarizing").empty();
                            $("#summarizing").append("<table>" + rowsText + "</table>");
                        },
                        error: function (msg) {
                            console.log(msg);
                            alert("通訊失敗，請聯絡管理員。");
                        }
                    });
                });
            });


        })

    </script>
</body>
</html>
