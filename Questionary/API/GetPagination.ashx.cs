using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.API
{
    /// <summary>
    /// GetPagination 的摘要描述
    /// </summary>
    public class GetPagination : IHttpHandler
    {
        OtherManager _othermgr = new OtherManager();
        QuestionayManager _mgr = new QuestionayManager();
        QuestionaryModel _model = new QuestionaryModel();
        private const int _pageSize = 10;
        public void ProcessRequest(HttpContext context)
        {

            if (string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {

                if (!string.IsNullOrEmpty(context.Request.QueryString["GETPa"]))
                {
                    string pageIndexText = context.Request.QueryString["Index"];
                    int pageIndex =
                        (string.IsNullOrWhiteSpace(pageIndexText))
                            ? 1
                            : Convert.ToInt32(pageIndexText);
                    if (pageIndex == -1)
                        pageIndex = 0;
                    listcount pa = new listcount();
                    int totalRows = 0;
                    var listwee = this._othermgr.PafinationONE(_pageSize, pageIndex, out totalRows);
                    pa.totalRows = totalRows; //總比數
                    pa.pindex = pageIndex; //當前頁數
                    pa.pCount = totalRows / _pageSize;//總頁數(總比數除頁面大小)



                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(pa);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                    return;
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["Title"]))
                {
                    string Title = context.Request.QueryString["Title"];
                    string time_start = context.Request.QueryString["time_start"];
                    string time_end = context.Request.QueryString["time_end"];
                    string pageIndexText = context.Request.QueryString["INDEX"];
                    int pageIndex =
                        (string.IsNullOrWhiteSpace(pageIndexText))
                            ? 1
                            : Convert.ToInt32(pageIndexText);

                    //有標題ㄉ查詢
                    var model = _othermgr.PafinationHasTitle(Title, time_start, time_end, _pageSize, pageIndex);

                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                    return;



                }
                else
                {
                    string time_start = context.Request.QueryString["time_start"];
                    if (time_start == null)
                        time_start = "2022-01-01";
                    string time_end = context.Request.QueryString["time_end"];
                    if(time_end == null)
                    {
                        time_end = "2022-12-31";
                    }
                    string pageIndexText = context.Request.QueryString["INDEX"];

                    int pageIndex =
                        (string.IsNullOrWhiteSpace(pageIndexText))
                            ? 1
                            : Convert.ToInt32(pageIndexText);




                    //無標題ㄉ查詢
                    int totalRows = 0;
                    var model = _othermgr.Pafination(time_start, time_end, _pageSize, pageIndex, out totalRows);

                    string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(jsonText);
                    return;
                }
                

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        private class listcount
        {
            public int pCount { get; set; }  //總共頁數
            public int totalRows { get; set; }   //總比數
            public int pindex { get; set; }       //當前頁數
        }
    }
}