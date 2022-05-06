using Questionary.Managers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Questionary.API
{
    /// <summary>
    /// DeleteQuestionary 的摘要描述
    /// </summary>
    public class DeleteQuestionary : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            QuestionayManager _mgr = new QuestionayManager();
            NameValueCollection IDcol = new NameValueCollection();

            if (string.Compare("POST", context.Request.HttpMethod) == 0 &&
                string.Compare("DELETE", context.Request.QueryString["Action"], true) == 0)
            {
                
                
                IDcol = context.Request.Form;
                string ID = IDcol.ToString().Trim();
                if (ID.Contains("&"))
                {
                  
                    foreach (string IDstr in ID.Split('&'))
                    {
                        string qq = IDstr.ToString();
                        string newqq = qq.Remove(0, 9);
                        if (_mgr.GetOneQuestionay(Guid.Parse(newqq)) == null)
                        {
                            context.Response.ContentType = "text/plain";
                            context.Response.Write("NULL");
                            break;
                        }
                        else
                        {
                            if (_mgr.DeleteQuestionary(Guid.Parse(newqq)) == true)
                            {
                                context.Response.ContentType = "text/plain";
                                context.Response.Write("OK");

                            }

                        }


                    }

                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(ID) != true)
                    {
                        string removedID = ID.Remove(0, 9);
                        if (_mgr.GetOneQuestionay(Guid.Parse(removedID)) == null)
                        {
                            context.Response.ContentType = "text/plain";
                            context.Response.Write("NULL");
                            return;
                        }
                        else
                        {
                            if (_mgr.DeleteQuestionary(Guid.Parse(removedID)) == true)
                            {
                                context.Response.ContentType = "text/plain";
                                context.Response.Write("OK");
                                return;
                            }

                        }
                    }

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
    }
}