<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoPage.aspx.cs" Inherits="Sakei.NoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>不明錯誤，請聯繫站方</title>
    <style>
        /* #divMainReturn {
            height: 700px;
            width: 100%;
        }*/

        * {
            border: 0;
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            background: currentColor;
        }
     
        figure {
            font-size: 6px;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%,-50%);
            width: 64em;
        }

        figcaption {
            color: #fff;
            display: flex;
            align-content: space-between;
            flex-wrap: wrap;
            height: 17em;
        }

            figcaption span:before, .sad-mac:before {
                content: "";
                display: block;
                width: 1em;
                height: 1em;
                transform: translate(-1em,-1em);
            }

            figcaption span {
                display: inline-block;
                margin: 0 2em;
                width: 4em;
                height: 6em;
            }

        .sr-text {
            overflow: hidden;
            position: absolute;
            width: 0;
            height: 0;
        }
     
        ._0:before {
            box-shadow: 2em 1em, 3em 1em, 1em 2em, 1em 3em, 1em 4em, 1em 5em, 4em 2em, 4em 3em, 4em 4em, 4em 5em, 2em 4em, 3em 3em, 2em 6em, 3em 6em;
        }

        ._4:before {
            box-shadow: 1em 1em, 1em 2em, 1em 3em, 1em 4em, 4em 1em, 4em 2em, 4em 3em, 4em 4em, 2em 4em, 3em 4em, 4em 5em, 4em 6em;
        }

        .d:before {
            box-shadow: 1em 1em, 2em 1em, 3em 1em, 1em 2em, 4em 2em, 1em 3em, 4em 3em, 1em 4em, 4em 4em, 1em 5em, 4em 5em, 1em 6em, 2em 6em, 3em 6em;
        }

        .e:before {
            box-shadow: 1em 1em, 2em 1em, 3em 1em, 4em 1em, 1em 2em, 1em 3em, 2em 3em, 3em 3em, 1em 4em, 1em 5em, 1em 6em, 2em 6em, 3em 6em, 4em 6em;
        }

        .f:before {
            box-shadow: 1em 1em, 2em 1em, 3em 1em, 4em 1em, 1em 2em, 1em 3em, 2em 3em, 3em 3em, 1em 4em, 1em 5em, 1em 6em;
        }

        .n:before {
            box-shadow: 1em 1em, 1em 2em, 1em 3em, 1em 4em, 1em 5em, 1em 6em, 4em 1em, 4em 2em, 4em 3em, 4em 4em, 4em 5em, 4em 6em, 2em 3em, 3em 4em;
        }

        .o:before {
            box-shadow: 2em 1em, 3em 1em, 1em 2em, 1em 3em, 1em 4em, 1em 5em, 4em 2em, 4em 3em, 4em 4em, 4em 5em, 2em 6em, 3em 6em;
        }

        .r:before {
            box-shadow: 1em 1em, 2em 1em, 3em 1em, 4em 2em, 1em 2em, 1em 3em, 1em 4em, 2em 3em, 3em 3em, 1em 5em, 1em 6em, 4em 4em, 4em 5em, 4em 6em;
        }

        .t:before {
            box-shadow: 1em 1em, 2em 1em, 3em 1em, 2em 2em, 2em 3em, 2em 4em, 2em 5em, 2em 6em;
        }

        .u:before {
            box-shadow: 1em 1em, 1em 2em, 1em 3em, 1em 4em, 1em 5em, 4em 1em, 4em 2em, 4em 3em, 4em 4em, 4em 5em, 2em 6em, 3em 6em;
        }
       


        @media screen and (min-width: 720px) {
            figure {
                font-size: 7px;
            }
        }

        @media screen and (min-width: 1440px) {
            figure {
                font-size: 8px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="divMainReturn">
            <h1 runat="server" id="h1Nothing">沒有東西啦</h1>
            <h1>就跟你說沒有東西啦</h1>
        </div>--%>
        <figure>
            <div class="sad-mac"></div>
            <figcaption>
                <span class="sr-text">Error 404: Not Found</span>
                <span class="e"></span>
                <span class="r"></span>
                <span class="r"></span>
                <span class="o"></span>
                <span class="r"></span>
                <span class="_4"></span>
                <span class="_0"></span>
                <span class="_4"></span>
                <span class="n"></span>
                <span class="o"></span>
                <span class="t"></span>
                <span class="f"></span>
                <span class="o"></span>
                <span class="u"></span>
                <span class="n"></span>
                <span class="d"></span>
            </figcaption>
        </figure>
    </form>
</body>
</html>
