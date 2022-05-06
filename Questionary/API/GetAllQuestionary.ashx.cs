using Questionary.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.API
{
    /// <summary>
    /// GetAllQuestionary 的摘要描述
    /// </summary>
    public class GetAllQuestionary : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            QuestionayManager _mgr = new QuestionayManager();
            //列出所有
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0 && !string.IsNullOrEmpty(context.Request.QueryString["ALL"]))
            {
                var list = _mgr.GetALLQuestionay();
                var listTop10 = list.Take(10).ToList();
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(listTop10);
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