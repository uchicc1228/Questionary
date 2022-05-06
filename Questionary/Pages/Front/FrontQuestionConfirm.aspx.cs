using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionary.Pages.Front
{
    public partial class FrontQuestionConfirm : System.Web.UI.Page
    {
        string[] rdoansarray;
        string[] chkansarray;
        private static List<string> _tmplistchk = new List<string>();
        private static List<string> _tmplistrdo = new List<string>();
        OtherManager _mgrO = new OtherManager();
        QuestionaryModel model = new QuestionaryModel();
        QuestionManager _mgrQ = new QuestionManager();
        QuestionayManager _mgr = new QuestionayManager();
        UserInfoModel anss = new UserInfoModel();
        private static List<UserInfoModel> result;
        private static QuestionaryModel _Qmodel;
        private static UserInfoModel _userinfo;

        private static List<UserInfoModel> _useranswer;
        private static Guid _QID;
        #region "動態生成控制項所需變數"
        static int rdoi;
        static int chki;
        static int txti;
        static string rdoans;
        static string chkans;
        static string txtans;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
           
                _QID = Guid.Parse(Request.QueryString["ID"]);
                _userinfo = HttpContext.Current.Session["userInfo"] as UserInfoModel;
                if(_userinfo == null)
                {
                    Response.Redirect("FrontIndex.aspx");
                    return;
                }
                _useranswer = HttpContext.Current.Session["userAnswer"] as List<UserInfoModel>;
                _Qmodel = _mgr.GetQuestionaryModel(_QID);
                this.lblTtile.Text = _Qmodel.Title;
                this.lblContent.Text = _Qmodel.Content;
                this.lblName.Text = _userinfo.UserName;
                this.lblEmail.Text = _userinfo.UserEmail;
                this.lblAge.Text = _userinfo.UserAge;
                this.lblPhone.Text = _userinfo.UserPhone;
                List<QuestionModel> _UserQuestion = _mgrQ.GetAllQuestion(_QID);
                List<UserInfoModel> chk = _useranswer.FindAll(x => x.UserAnswer.Contains("chk"));//使用者選擇的答案
                List<UserInfoModel> rdo = _useranswer.FindAll(x => x.UserAnswer.Contains("rdo"));//使用者選擇的答案
                List<UserInfoModel> txt = _useranswer.FindAll(x => x.UserAnswer.Contains("txt"));//使用者選擇的答案
                result = chk.Concat(rdo).Concat(txt).ToList();
                //製造題目
                foreach (var item in _UserQuestion)
                {
                    string necessary = "";
                    string[] arrayrdo = item.Answer.Split(';');
                    switch (item.QQMode)
                    {
                        case ("單選"):
                            if (item.QIsNecessary == "必填")
                                necessary = "(必填)";
                            this.plcQuestion.Controls.Add(new Literal { ID = $"rdiltl{rdoi}", Text = "<br/>" + $"{item.Question}{necessary}" + "<br/>" });

                            foreach (var itemRdo in arrayrdo)
                            {
                                buildRdo(itemRdo);
                            }
                            break;


                        case ("複選"):
                            this.plcQuestion.Controls.Add(new Literal { ID = $"chkltl{chki}", Text = "<br/>" + $"{item.Question}{necessary}" + "<br/>" });

                            foreach (var itemRdo in arrayrdo)
                            {
                                buildChk(itemRdo, _useranswer);
                            }
                            break;


                        case ("文字"):
                            this.plcQuestion.Controls.Add(new Literal { ID = $"txtltl{txti}", Text = "<br/>" + $"{item.Question}{necessary}" + "<br/>" });
                            foreach (var itemRdo in arrayrdo)
                            {
                                buildTxt(itemRdo);
                            }
                            break;
                        default:
                            break;
                    }
                }

                //顯示所選題目

                foreach (var item in chk)
                {
                    string id = item.UserAnswer.Remove(0, 1);
                    string _tmpchksans = "";
                    CheckBox aaa = this.form1.FindControl(id) as CheckBox;
                    aaa.Checked = true;
                    aaa.Enabled = false;
                    aaa.Visible = true;
                    _tmpchksans += aaa.Text;
                    string ans = "";
                    ans = _tmpchksans.Replace("<br/>", "");
                    chkans += ans + " ";



                    ans = _tmpchksans.Replace("<br/>", "");
                    string _wee = item.UserQuestion + "=" + item.UserAnswer;
                    _tmplistchk.Add(_wee);

                }

                foreach (var item in rdo)
                {
                    string id = item.UserAnswer.Remove(0, 1);
                    string _tmprdosans = "";
                    RadioButton aaa = this.form1.FindControl(id) as RadioButton;
                    aaa.Checked = true;
                    aaa.Enabled = false;
                    aaa.Visible = true;
                    _tmprdosans += aaa.Text;
                    string ans = "";
                    ans = _tmprdosans.Replace("<br/>", "");
                    rdoans += ans + " ";
                    string _wee = item.UserQuestion + "=" + item.UserAnswer;
                    _tmplistrdo.Add(_wee);


                }

                foreach (var item in txt)
                {

                    string id = item.UserAnswer.Remove(0, 2);
                    string _tmptxtsans = "";
                    TextBox aaa = this.form1.FindControl(id) as TextBox;
                    aaa.Enabled = false;
                    aaa.Visible = true;
                    aaa.Text = item.UserTextAnswer;
                    _tmptxtsans += aaa.Text;
                    string ans = "";
                    ans = _tmptxtsans.Replace("<br/>", "");
                    txtans += ans;



                }


            }
        }
        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            string _tmptext = "";
            string qq = "";
            // 題目 //所選答案
            //useranser = "1chk1"
            foreach (var item in _useranswer)
            {
               
                anss.QID = _QID;
                anss.UserID = item.UserID;
                anss.QuestionID = item.QuestionID;
                anss.UserQuestion = item.UserQuestion;
                anss.UserAnswer = item.UserAnswer;
                anss.UserName = _userinfo.UserName;
                anss.UserEmail = _userinfo.UserEmail;
                anss.UserAge = _userinfo.UserAge;
                anss.UserPhone = _userinfo.UserPhone;
                anss.UserWriteTime = DateTime.Now;
                //先找checkbox的question是否加入過 若加入過 continue;
                UserInfoModel momo = _mgrQ.findquestionID(anss.QuestionID);
                //找到該問題的所有選項
                QuestionModel weee = _mgrQ.GetQuestionAns(anss.QuestionID);
                anss.QuestionAnswer = weee.Answer;
                //所有的answer
                QuestionModel answer = _mgrQ.GetQuestionAns(anss.QuestionID);
                string[] qanswer = answer.Answer.Split(';'); //內容: 回答一 回答二 回答三
                if (rdoans != null)
                {
                    rdoansarray = rdoans.Split(' ');// 內容: 台北 回答三
                   
                }
                if (chkans != null)
                {
                    chkansarray = chkans.Split(' ');
                }

                List<string> tmplist = qanswer.ToList();
                if (item.UserAnswer.Contains("rdo"))
                {
                    if (momo != null)
                    {
                        if (momo.QuestionID == item.QuestionID && momo.UserID == item.UserID)
                        {
                            continue;
                        }
                    }
                    for (int i = 0; i < rdoansarray.Length - 1; i++)
                    {
                        rdoansarray = rdoansarray.Where(x => x != "").ToArray();
                        anss.Number = answer.QNumber;
                        qq = tmplist.Find(x => x.Contains(rdoansarray[i]));
                        anss.UserTextAnswer = qq + ";";
                        _mgrO.ConfirmAnswer(anss);
                        rdoans = rdoans.Replace(qq, "");
                        rdoansarray = rdoansarray.Where(x => x != "").ToArray();
                        break;

                    }
                }
                if (item.UserAnswer.Contains("chk"))
                {
                    if (momo != null)
                    {
                        if (momo.QuestionID == item.QuestionID && momo.UserID == item.UserID)
                        {
                            continue;
                        }
                    }

                    for (int i = 0; i < chkansarray.Length - 1; i++)
                    {
                        qq = tmplist.Find(x => x.Contains(chkansarray[i]));
                        if(qq != null &&!_tmptext.Contains(qq) )
                        {
                            _tmptext += qq + ";";
                        }
                        
                    }
                    anss.UserTextAnswer = _tmptext;
                    anss.Number = answer.QNumber;
                    _mgrO.ConfirmAnswer(anss);
                    qq = "";
                    _tmptext = "";

                }
                if (item.UserAnswer.Contains("txt"))
                {

                }

            }

            this.Session.Clear();
            this.Session["QuestionaryID"] = anss.QID;
            Response.Redirect("FrontStastic.aspx?QID=" + anss.QID);
        }
        private void buildTxt(string answer)
        {

            TextBox txtBtn = new TextBox()
            {
                ID = $"txt{txti}",
                Text = answer,
                Visible = false
            };
            this.plcQuestion.Controls.Add(txtBtn);
            txti++;
        }

        private void buildChk(string answer, List<UserInfoModel> list)
        {


            CheckBox ckBtn = new CheckBox()
            {
                ID = $"chk{chki}",
                Text = answer + "<br/>",
                Visible = false
            };
            this.plcQuestion.Controls.Add(ckBtn);
            chki++;
        }

        private void buildRdo(string answer)
        {
            RadioButton rdoBtn = new RadioButton()
            {
                ID = $"rdo{rdoi}",
                Text = answer + "<br/>",
                Visible = false
            };

            this.plcQuestion.Controls.Add(rdoBtn);
            rdoi++;
        }



        protected void btncancel_Click(object sender, EventArgs e)
        {
            string url = "FrontQuestionaryQ.aspx?ID=" + _QID;
            Response.Redirect("url");

        }
    }
}