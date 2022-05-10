<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontIndex.aspx.cs" Inherits="Questionary.Pages.Front.FrontIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"
        integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk"
        crossorigin="“anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <script src="../../JavaScript/bootstrap/bootstrap.js"></script>
    <script src="../../JavaScript/jquery/jquery.js"></script>
    <script src="../../JavaScriptCode/Pagefront.js"></script>
    <script>


</script>
    <style>
        body {
            width: 100%;
            height: 100%;
            background: #F7F7F7;
            padding: 25px 15px 25px 10px;
            font: 18px Georgia, "Times New Roman", Times, serif;
            color: #888;
            background-position-x: center;
            overflow-x: hidden;
            background-repeat: no-repeat;
        }

        .divSearch {
            position: relative;
            left: 30%;
            width: 1000px;
            padding: 50px;
            height: 150px;
        }

        .btnsearch {
            position: relative;
            left: 10%;
        }

        .divlist {
            width: 80%;
            height: 100%;
            position: relative;
            padding-left: 100px;
            padding-right: 100px;
            left: 10%;
        }

        .list-group {
            position: relative;
            text-align: center;
            width: 100%;
            padding: 0px;
            margin: 0px;
        }

        * {
            margin: 0;
            padding: 0;
        }

        .list li {
            display: block;
        }

        .divmenu {
            position: relative;
            left: 0%;
        }

        .pagination {
            left: 35%;
            position: relative;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="Main">

            <div class="divmenu">
                <a href="FrontIndex.aspx" style="color: rgb(27, 27, 83)">
                    <h2>前台頁面</h2>
                </a>
            </div>
            <hr />
            <asp:HiddenField ID="hfid" runat="server" />
            <div class="divSearch">
                <label for="titlesearch" style="padding-right: 62px;">標題</label>
                <input type="text" id="titlesearch" placeholder="請輸入問卷標題" />
                <br />
                <label for="titlesearch" style="padding-right: 15px;">開始 / 結束</label>
                <input type="date" runat="server" id="txtCalender_start" />
                <input type="date" runat="server" id="txtCalender_end" />
                <input type="button" id="btnsearch" value="搜尋" />
                <literal id="ltl1"></literal>

            </div>
            <div class="divleft">
            </div>

            <div class="divlist">
                <div class="list-group" id="summarizing">
                </div>
            </div>
            <div class="divlist">
                <ul id="listContent">
                    <li></li>
                </ul>
            </div>
            <div id="pagination" class="divlist">

                <ul class="pagination"></ul>
            </div>
        </div>
    </form>

    <script src="../../JavaScriptCode/Pagefront.js"></script>
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
                        // 數據數量
                        var resultSize = objDataList.length
                        // 當前頁
                        var currentPage = objDataList.currentPage;
                        // 數據列表
                        var resultData = objDataList.dataSource;
                        var rowsText = `<thead>
                          <table class="table table-dark table-striped">
                  <thead>
                      <tr>     
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
                                        <td>${item.Number}</td>
                                        <td> <a href="${item.QuestionaryUrl}"> ${item.Title} </a> </td>
                                        <td> ${item.Status} </td>
                                        <td> ${item.StartTime_string} </td>
                                        <td> ${item.EndTime_string} </td>
                                        <td> <a href="FrontStastic.aspx?QID=${item.ID}">觀看統計</a></td>
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

            $(window).keydown(function (e) {
                var key = window.event?e.keyCode : e.which;
                if (key.toString() == '13') {
                    return false;
                }
            });

            //搜尋(標題&日期)
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
                    if (time_start > time_end && time_end !='') {
                        alert('請注意日期')
                        return false;
                    }
                    if (title == "") {
                        SearchData = { "All": "1" };
                        url = "../../API/GetAllQuestionary.ashx";
                        console.log(SearchData);
                        console.log(url);
                        List.loadList(1, SearchData, url);
                        /* return false;*/
                    }
                    SearchData = {
                        "Title": title,
                        "Time_Start": time_start,
                        "Time_End": time_end
                    }
                    url = "../../API/GetQuestionary.ashx";
                    List.loadList(1, SearchData, url);


                });
            });

            List.loadList(List.pageIndex, SearchData, url);


        })

        var List = {
            contentId: "listContent", //關聯列表ID
            pageId: "pagination",  //渲染分頁id
            pageIndex: 1, //第幾頁開始渲染
            pageSize: 10,//每頁渲染多少條數據<--改這個
            pageSum: 100,//共多少條數據
            refreshId: null
        };
        var SearchData =
        {
            "All": "1"
        }
        var url = "../../API/GetAllQuestionary.ashx";
        //data.count為總的數據條數，即共多少條數據
        List.loadList = function (pageIndex, SearchData, url) {
            var ajaxRequest = function () {
                $.ajax({
                    url: url,
                    method: "GET",
                    dataType: "JSON",
                    data: SearchData,
                    success: function (data) {                      
                        if (data.length == 0) {
                            if (url == "../../API/GetQuestionary.ashx") {
                                alert('查無資料。');
                                
                            }
                            if (url == "../../API/GetAllQuestionary.ashx") {
                                alert('目前無任何問卷。');
                            }

                        }
                        else {
                            if (pageIndex == 1) {
                                List.initPaginator(data.length, data);
                                $('#' + List.pageId).show();
                                $('.pagination li[data-page="1"]').addClass('active');
                                $('.pagination li.page-first').removeClass('active').addClass('disabled');
                            }
                            List.bindList(pageIndex, data.length, data);
                        }
                        
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            }
            ajaxRequest();
        };

        //數據遍歷
        List.bindList = function (pageIndex, count, data) {
            var start = (pageIndex - 1) * List.pageSize; // 數據開始點
            var content = `
                <table class="table table-dark table-striped ">
                  <thead>
                      <tr>
                          <th scope="col">#</th>
                          <th scope="col">問卷</th>
                          <th scope="col">狀態</th>
                          <th scope="col">開啟時間</th>
                          <th scope="col">結束時間</th>
                          <th scope="col">觀看統計</th>
                      </tr>
                  </thead>`;
            for (var index = start; index < pageIndex * List.pageSize && index < data.length; index++) { //遍历第几页内容，pageIndex也页面顺序
                content += List.getListItem(data[index]);
            }
            $('#' + List.contentId).html('');
            $('#' + List.contentId).append(content);
        };

        List.getListItem = function (item) {
            var content = '';
            if (item.Status == "關閉") {
                content +=
                    `
                    <tbody>
                      <tr>
                          <td>${item.Number}</td>
                          <td> ${item.Title}</td>
                          <td> ${item.Status} </td>
                          <td> ${item.StartTime_string} </td>
                          <td> ${item.EndTime_string} </td>
                          <td> <a href="FrontStastic.aspx?QID=${item.ID}">觀看統計</a></td>
                          <td><input type="hidden" class="hfID" name="hfID" value="${item.ID}"></td>
                      </tr>
                  `
            }
            else {
                content +=
                    `
                    <tbody>
                      <tr>
                          <td>${item.Number}</td>
                          <td> <a href="${item.QuestionaryUrl}">${item.Title}</a> </td>
                          <td> ${item.Status} </td>
                          <td> ${item.StartTime_string} </td>
                          <td> ${item.EndTime_string} </td>
                          <td> <a href="FrontStastic.aspx?QID=${item.ID}">觀看統計</a></td>
                          <td><input type="hidden" class="hfID" name="hfID" value="${item.ID}"></td>
                      </tr>
                  `
            }

            return content;
        };
        //分頁
        List.initPaginator = function (count, data) {
            var pageSize = List.pageSize;
            var pageTotal = Math.ceil(count / pageSize);
            var pageIndex = List.pageIndex;
            List.pageSum = pageTotal;
            var html = "";
            for (var i = pageIndex; i <= pageTotal; i++) {
                html += '<li class="page" data-page="' + i + '"><a href="javascript:;">' + i + '</a></li>';
            }
            html = '<li data-page="1" class="page-first"><a href="javascript:;">首頁</a></li>'
                + '<li data-page="" class="page-prev disabled"><a href="javascript:;" >上一頁</a></li>'
                + html
                + '<li data-page="2" class="page-next"><a href="javascript:;">下一頁</a></li>'
                + '<li data-page="' + pageTotal + '" class="page-last"><a href="javascript:;">末頁</a></li>';
            $('#' + List.pageId).find('ul').html(html);
        };

        //點擊頁碼更新數據
        $('#' + List.pageId).on('click', 'li', function (e) {
            var pageIndex = parseInt($(this).attr('data-page'));
            var activeIndex = parseInt($('.pagination li.active').attr('data-page'));//當前active狀態頁
            $('.pagination li').removeClass('active');
            //上一頁
            if ($(this).hasClass('page-prev')) {
                var cur = activeIndex - 1;
                if (cur <= 0) cur = 1;//避免超出頁面範圍
                List.loadList(cur, SearchData, url);
                $('.page-next,.page-last').removeClass('disabled');
                $('.pagination li.page[data-page=' + cur + ']').addClass('active');
                if (cur == 1) {
                    $('.page-prev,.page-first').addClass('disabled');
                    $('.page-prev').attr('data-page', '');
                } else {
                    $('.page-prev').attr('data-page', activeIndex - 2);
                    $('.page-next').attr('data-page', activeIndex);
                }
                //下一頁
            } else if ($(this).hasClass('page-next')) {
                var cur = activeIndex + 1;
                if (cur >= List.pageSum) cur = List.pageSum;//避免超出頁面範圍
                List.loadList(cur, SearchData, url);
                $('.page-prev,.page-first').removeClass('disabled');
                $('.pagination li.page[data-page=' + cur + ']').addClass('active');
                if (cur == List.pageSum) {
                    $('.page-next,.page-last').addClass('disabled');
                    $('.page-next').attr('data-page', '');
                } else {
                    $('.page-prev').attr('data-page', activeIndex);
                    $('.page-next').attr('data-page', activeIndex + 2);
                }

            } else if ((!$(this).hasClass('page-prev')) && (!$(this).hasClass('page-next'))) {
                $(this).addClass('active');
                List.loadList($(this).attr('data-page'), SearchData, url);
            }
            if (!($(this).hasClass('page-first')) && !($(this).hasClass('page-prev')) && !($(this).hasClass('page-last')) && !($(this).hasClass('page-next'))) {
                if (pageIndex == 1) {
                    $('.page-prev,.page-first').addClass('disabled');
                    $('.page-next,.page-last').removeClass('disabled');
                    $('.page-next').attr('data-page', '2');
                } else if (pageIndex == List.pageSum) {
                    $('.page-prev,.page-first').removeClass('disabled');
                    $('.page-next,.page-last').addClass('disabled');
                    $('.page-prev').attr('data-page', List.pageSum - 1);
                    $('.page-next').attr('data-page', '');
                } else {
                    $('.page-prev,.page-first').removeClass('disabled');
                    $('.page-next,.page-last').removeClass('disabled');
                    $('.page-prev').attr('data-page', pageIndex - 1);
                    $('.page-next').attr('data-page', pageIndex + 1);
                }
            }
        });


    </script>
</body>
</html>

