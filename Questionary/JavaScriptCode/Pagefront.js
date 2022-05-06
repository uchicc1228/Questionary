$("#0").click(function () {
    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
    var time_start = $("#txtCalender_start").val();
    var time_end = $("#txtCalender_end").val();
    var ID = $("#hfID").val();

    $.ajax({
        url: "../../API/GetPagination.ashx",
        method: "GET",
        data: {
            "INDEX": '0',
            "Title": title,
            "Time_Start": time_start,
            "Time_End": time_end
            
        },
        dataType: "JSON",
        success: function (objDataList) {
            //alert(time_start)
            //alert(time_end)
            if (objDataList.length == 0) {
                alert('此頁無資料')
            }
            console.log(objDataList);

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
                                <td> <a href="https://www.google.com/?hl=zh_tw">${item.Title}</a> </td>
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


$("#1").click(function () {
    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
    var time_start = $("#txtCalender_start").val();
    var time_end = $("#txtCalender_end").val();
    var ID = $("#hfID").val();
    $.ajax({
        url: "../../API/GetPagination.ashx",
        method: "GET",
        data: {
            "INDEX": '2',
            "Title": title,
            "Time_Start": time_start,
            "Time_End": time_end
        },
        dataType: "JSON",
        success: function (objDataList) {
            //alert(time_start)
            //alert(time_end)
            if (objDataList.length == 0) {
                alert('此頁無資料')
            }
            console.log(objDataList);

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
                                <td> <a href="https://www.google.com/?hl=zh_tw">${item.Title}</a> </td>
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


$("#a2").click(function () {
    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
    var time_start = $("#txtCalender_start").val();
    var time_end = $("#txtCalender_end").val();
    var ID = $("#hfID").val();

    $.ajax({
        url: "../../API/GetPagination.ashx",
        method: "GET",
        data: {
            "INDEX": '3',       
            "Title": title,
            "Time_Start": time_start,
            "Time_End": time_end
        },
        dataType: "JSON",
        success: function (objDataList) {
            //alert(time_start)
            //alert(time_end)
            if (objDataList.length == 0) {
                alert('此頁無資料')
            }
            console.log(objDataList);

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
                                <td> <a href="https://www.google.com/?hl=zh_tw">${item.Title}</a> </td>
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


$("#a3").click(function () {
    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
    var time_start = $("#txtCalender_start").val();
    var time_end = $("#txtCalender_end").val();
    var ID = $("#hfID").val();
    $.ajax({
        url: "../../API/GetPagination.ashx",
        method: "GET",
        data: {
            "INDEX": '4',
            "Title": title,
            "Time_Start": time_start,
            "Time_End": time_end
        },
        dataType: "JSON",
        success: function (objDataList) {
            //alert(time_start)
            //alert(time_end)
            if (objDataList.length == 0) {
                alert('此頁無資料')
            }
            console.log(objDataList);

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
                                <td> <a href="https://www.google.com/?hl=zh_tw">${item.Title}</a> </td>
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



$("#a4").click(function () {
    var title = $("input[placeholder='請輸入問卷標題'").val().trim();
    var time_start = $("#txtCalender_start").val();
    var time_end = $("#txtCalender_end").val();
    var ID = $("#hfID").val();
    $.ajax({
        url: "../../API/GetPagination.ashx",
        method: "GET",
        data: {
            "INDEX": '5',
            "Title": title,
            "Time_Start": time_start,
            "Time_End": time_end
        },
        dataType: "JSON",
        success: function (objDataList) {
            //alert(time_start)
            //alert(time_end)
            if (objDataList.length == 0) {
                alert('此頁無資料')
            }
            console.log(objDataList);

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
                                <td> <a href="https://www.google.com/?hl=zh_tw">${item.Title}</a> </td>
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

