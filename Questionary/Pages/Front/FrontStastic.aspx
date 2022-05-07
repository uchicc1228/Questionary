<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontStastic.aspx.cs" Inherits="Questionary.Pages.Front.FrontStastic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>統計</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css"
        integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk"
        crossorigin="“anonymous" />
    <script src="../../JavaScript/bootstrap/bootstrap.min.js"></script>
    <script src="../../JavaScript/jquery/jquery.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js"></script>


    <style>
        #div01 {
            position: relative;
            left: 30%;
        }

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

        .weee{
           font-size:40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="qInfo">

            <asp:Label CssClass="weee" runat="server" ID="lbltitle"></asp:Label>



        </div>
        <asp:HiddenField ID="hfid" runat="server" />
        <div id="div01" style="width: 500px; height: 600px;">
        </div>
    </form>
    <script>      
        function randomRgbColor(num) {
            var all = "";
            for (var i = 0; i < num; i++) {
                var r = Math.floor(Math.random() * 256); //隨機生成256以內r值
                var g = Math.floor(Math.random() * 256); //隨機生成256以內g值
                var b = Math.floor(Math.random() * 256); //隨機生成256以內b值
                all += `rgb(${r},${g},${b}),`
            }
            return all;
        }
        function toPercent(num, total) {
            var k = "";
            for (var i = 0; i < num.length; i++) {
                k += (Math.round(num[i] / total * 10000) / 100.00) + ',';// 小数点后两位百分比
            }
            return k;
        }
        function creatCanvas(num) {
            var canvas = document.createElement(`canvas`);
            canvas.id = `CursorLayer${num}`;
            canvas.width = 500;
            canvas.height = 600;
            canvas.style.zIndex = 8;
            canvas.style.position = "relative";
            canvas.style.border = "1px solid black";
            var body = document.getElementById("div01");
            body.appendChild(canvas);
        }
        function buildPie(x, y, w, v, i) {
            var xx = x.split(',');
            var yy = y.split(',');
            var vv = toPercent(yy, v);
            var _tmpvv = vv.split(',');
            _tmpvv = _tmpvv.filter((element, index) => index < _tmpvv.length - 1);
            console.log(_tmpvv);
            console.log(yy);
            new Chart(`CursorLayer${i}`, {
                type: "pie",
                data: {
                    labels: xx,
                    datasets: [{
                        backgroundColor: ["rgb(35, 124, 12)", "rgb(69, 205, 129)", "rgb(87, 11, 30)", "rgb(213, 12, 30", "rgb(231,45,105"],
                        data: yy
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: w,
                    },
                    cutoutPercentage: 50
                }
            });

        }
        $(document).ready(function () {

            function BuildTable() {
                $.ajax({
                    url: "../../API/StaticesHandler.ashx?QID=" + $("#hfid").val(),
                    method: "GET",
                    dataType: "JSON",
                    success: function (allValues) {
                        var x = allValues.xvalue;
                        var y = allValues.yvalue;
                        var w = allValues.wvalue;
                        var v = allValues.vvalue;

                        if (x.length == 0) {
                            alert('尚未有使用者填寫問卷')
                            window.history.back();
                        }

                        for (var i = 0; i < x.length; i++) {

                            creatCanvas(i);
                            buildPie(x[i], y[i], w[i], v[i], i)

                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                })
            }
            BuildTable();



        })
    </script>
</body>
</html>
