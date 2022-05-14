using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.API
{

    /// <summary>
    /// StaticesHandler 的摘要描述
    /// </summary>
    public class StaticesHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        mylist list = new mylist();
        QuestionayManager _mgr = new QuestionayManager();
        OtherManager _mgrO = new OtherManager();
        QuestionManager _mgrQ = new QuestionManager();

        List<QuestionModel> questionary = new List<QuestionModel>();
        List<string> listx = new List<string>(); //此問題的全部選項名稱
        List<string> listw = new List<string>(); //題目名稱
        List<string> listy = new List<string>(); //數值 = 分子 = 幾個使用者選擇該選項
        List<string> listv = new List<string>();//該題目所有回答加起來的總數
        List<Guid> listz = new List<Guid>(); //題目id
        public void ProcessRequest(HttpContext context)
        {
            Guid questionaryid = Guid.Parse(context.Request.QueryString["QID"]);
            //該問卷內所有題目
            questionary = _mgrQ.GetAllQuestion(questionaryid);
            questionary = questionary.FindAll(x => x.QQMode != "文字");
            //抓Table = Answer內所有的問題
            List<StatciModel> statci = _mgrO.FindStaticInfo(questionaryid);
            List<StatciModel> _statci = new List<StatciModel>();
            foreach (var item2 in statci)
            {
                for (var i = 0; i < questionary.Count; i++)
                {
                    if (item2.Question == questionary[i].Question)
                    {
                        _statci.Add(item2);
                    }
                }
            }
            int[] T = new int[0] { };
            foreach (var item in _statci)
            {
                //1.選項 = 此問題的全部選項名稱
                //2.數值 = 分子 =幾個使用者選擇該選項
                //3.題目名稱 = 此問題名稱                    
                Guid c = item.QuestionID;

                QuestionModel model = new QuestionModel();
                model = _mgrQ.GetQuestionAns(c);


                if (!listz.Contains(c) && model.QQMode != "文字")
                {
                    listx.Add(item.Answer);
                    listw.Add(item.Question);
                    listz.Add(item.QuestionID);
                    string xanswer = statci.Find(x => x.Answer == item.Answer).ToString();
                    List<string> _tmptxt = item.Answer.Split(';').ToList(); //該題所有選項
                    int _tmptxtcount = _tmptxt.Count; //該題選項數量
                    int v = 0;
                    string qq = "";
                    int h = 0;
                    string howmuch = "";
                    for (int x = 0; x < _tmptxtcount; x++)
                    {

                        string[] _tmpanswer = item.Answer.Split(';').ToArray();
                        List<string> _tmpanswerlist = _tmpanswer.Where(z => z != "").ToList();//使用者所選選項                                 
                        List<StatciModel> _thisAnsQ = _statci.FindAll(z => z.Question.Contains(item.Question));
                        foreach (var item3 in _thisAnsQ)
                        {
                            List<string> pp2 = item3.UserTextAnswer.Split(';').ToList();

                            for (int g = 0; g < pp2.Count - 1; g++)
                            {
                                if (_tmpanswer[x] == pp2[g])
                                {
                                    v++;
                                }
                            }

                        }
                        qq += v.ToString() + ",";
                        h += v;
                        v = 0;

                    }
                    howmuch = h.ToString();
                    listv.Add(howmuch);
                    listy.Add(qq);
                    howmuch = "";
                }

            }
            List<string> qqq = new List<string>();
            foreach (var ccc in listy)
            {

                qqq.Add(ccc.Remove(ccc.Length - 1));
            }
            List<string> bbb = new List<string>();
            foreach (var ccc in listx)
            {
                bbb.Add(ccc.Replace(";", ","));
            }
            list.vvalue = listv;
            list.wvalue = listw;
            list.xvalue = bbb;
            list.yvalue = qqq;
            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class mylist
    {
        public List<string> xvalue { get; set; }//選項
        public List<string> yvalue { get; set; }//數值
        public List<string> wvalue { get; set; }//題目
        public List<Guid> zvalue { get; set; }  //問題ID

        public List<string> vvalue { get; set; }//該選項的總數
    }



}