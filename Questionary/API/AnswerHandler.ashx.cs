using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.API
{
    /// <summary>
    /// AnswerHandler 的摘要描述
    /// </summary>
    /// ///

    public class AnswerHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState //不加此介面讀不到session
    {

        static Guid QID;
        QuestionManager _mgrQ = new QuestionManager();
        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("POST", context.Request.HttpMethod) == 0 && !string.IsNullOrEmpty(context.Request.QueryString["QID"]))
            {
                QID = Guid.Parse(context.Request.QueryString["QID"]);

                //userInfo
                UserInfoModel _userInfo = new UserInfoModel()
                {
                    UserID = Guid.NewGuid(),
                    UserName = context.Request.Form["UserName"],
                    UserPhone = context.Request.Form["UserPhone"],
                    UserAge = context.Request.Form["UserAge"],
                    UserEmail = context.Request.Form["UserEmail"]
                };
                HttpContext.Current.Session["userInfo"] = _userInfo;

                //userAnswer          
                if (string.IsNullOrEmpty(context.Request.Form["Answer"]) == false)
                {
                    List<UserInfoModel> answerList = new List<UserInfoModel>();
                    //字串處理方法 我要留下
                    List<string> _userAnswer = context.Request.Form["Answer"].Split(' ').ToList();
                    List<string> _userQuestion = context.Request.Form["Question"].Split(' ').ToList();
                    QuestionModel question = new QuestionModel();
                    _userAnswer.RemoveAt(_userAnswer.Count - 1);
                    Guid QWEQWE = Guid.NewGuid();
                    foreach (var item2 in _userAnswer)
                    {
                        UserInfoModel answer = new UserInfoModel();
                        //找question或是questionID
                        if (item2.Contains("chk"))
                        {
                            string suck;
                            suck = item2.Remove(0,1);
                            question.ANumber = Convert.ToInt32(suck.Substring(0,1));
                            answer.Number = question.ANumber;
                            question.QQMode = "複選";
                            question = _mgrQ.FindQuestionID(QID,question.ANumber, question.QQMode);
                            answer.UserQuestion = question.Question;
                            

                        }
                        if (item2.Contains("rdo"))
                        {
                            string suck;
                            suck = item2.Remove(0, 1);
                            question.ANumber = Convert.ToInt32(suck.Substring(0, 1));
                            answer.Number = question.ANumber;
                            question.QQMode = "單選";
                            question = _mgrQ.FindQuestionID(QID,question.ANumber, question.QQMode);
                           

                        }
                        if (item2.Contains("txt"))
                        {  //A4Atxt0=47747




                            string suck;
                            suck = item2.Remove(0, 1);
                            question.ANumber = Convert.ToInt32(suck.Substring(0, 1));
                            answer.Number = question.ANumber;
                            question.QQMode = "文字";
                            question = _mgrQ.FindQuestionID(QID, question.ANumber, question.QQMode);

                        }
                        string finalanswer= string.Empty;
                        if (question.QQMode == "文字")
                        {

                            string[] _tmptxtans = item2.Split('=');
                            finalanswer = _tmptxtans[0].Remove(0, 1);
                            answer.UserAnswer = _tmptxtans[0];
                            answer.UserTextAnswer = _tmptxtans[1];
                        }
                        else
                        {
                            finalanswer = item2.Remove(0, 1);
                            answer.UserAnswer = finalanswer;
                        }
                        answer.QuestionID = question.QuestionID;
                        answer.UserQuestion = question.Question;
                        answer.UserID = QWEQWE;
                        answer.QID = QID;

                        answerList.Add(answer);
                    }



                    HttpContext.Current.Session["userAnswer"] = answerList;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("error");
                }



            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("error");
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