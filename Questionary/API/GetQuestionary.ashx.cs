using Questionary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.API
{
    /// <summary>
    /// GetQuestionary 的摘要描述
    /// </summary>
    public class GetQuestionary : IHttpHandler
    {
        QuestionayManager _mgr = new QuestionayManager();
        public void ProcessRequest(HttpContext context)
        {

            if (!string.IsNullOrEmpty(context.Request.QueryString["Title"]))
            {

                //有標題ㄉ查詢
                string time_start = context.Request.QueryString["time_start"];
                string time_end = context.Request.QueryString["time_end"];
                string title = context.Request.QueryString["Title"];

                var model = _mgr.GetQuestionaryHaveTitle(title, time_start, time_end);
                var listTop2 = model.Take(10).ToList();
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(listTop2);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
            else
            {
                //無標題的查詢
                string time_start = context.Request.QueryString["time_start"];
                string time_end = context.Request.QueryString["time_end"];
                var model = _mgr.GetQuestionaryWithTime(time_start, time_end);
                var listTop2 = model.Take(10).ToList();
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(listTop2);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}