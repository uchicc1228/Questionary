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
    public partial class FrontQuestionaryQ : System.Web.UI.Page
    {
        QuestionaryModel model = new QuestionaryModel();
        QuestionManager _mgrQ = new QuestionManager();
        QuestionayManager _mgr = new QuestionayManager();
        private static QuestionaryModel _Qmodel;


        private static Guid _QID;
        #region "動態生成控制項所需變數"
         int _rdoi;
         int _chki;
         int _txti;
        static bool required;
         int _requiredrdo = 0;
         int _requiredchk = 0;
         int _requiredtxt = 0;
        int groupname = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _QID = Guid.Parse(Request.QueryString["ID"]);
                this.hfID.Value = _QID.ToString();
                _Qmodel = _mgr.GetQuestionaryModel(_QID);
                List<QuestionModel> _UserQuestion = _mgrQ.GetAllQuestion(_QID);
                this.lblTitle.Text =  _Qmodel.Title;
                this.lblContent.Text = _Qmodel.Content;

                foreach (var item in _UserQuestion)
                {
                    string necessary = "";
                    string[] arrayrdo = item.Answer.Split(';');
                    required = false;
                    switch (item.QQMode)
                    {
                        case ("單選"):
                            if (item.QIsNecessary == "必填")
                                necessary = "(必填)";
                            this.plcQuestion.Controls.Add(new Literal { ID = $"rdoltl{_rdoi}", Text = $"<br/><label id=Q{_rdoi}=>" + $"{item.Question}{necessary}" + "</label><br/>" });
                            if (item.QIsNecessary == "必填")
                            {
                                Panel plcrequired = new Panel()
                                {
                                    ID = $"rdorequired{_requiredrdo}",
                                    CssClass = "required"
                                };
                                this.plcQuestion.Controls.Add(plcrequired);
                               
                                required = true;
                            }

                            foreach (var itemRdo in arrayrdo)
                            {
                                buildRdo(itemRdo, required, $"rdorequired{_requiredrdo}", groupname, item.ANumber);
                            }
                            if (item.QIsNecessary == "必填")
                            {
                                _requiredrdo++;
                            }

                            groupname++;
                            break;


                        case ("複選"):
                            if (item.QIsNecessary == "必填")
                                necessary = "(必填)";
                            this.plcQuestion.Controls.Add(new Literal { ID = $"chkltl{_chki}", Text = $"<br/><label id=Q{_chki}=>" + $"{item.Question}{necessary}" + "</label><br/>" });
                            if (item.QIsNecessary == "必填")
                            {
                                necessary = "(必填)";
                                Panel plcrequired = new Panel()
                                {
                                    ID = $"chkrequired{_requiredchk}",
                                    CssClass = "required"
                                };
                                this.plcQuestion.Controls.Add(plcrequired);
                               
                                required = true;
                            }


                            foreach (var itemRdo in arrayrdo)
                            {
                                buildChk(itemRdo, required, $"chkrequired{_requiredchk}", item.ANumber);
                               
                            }
                            if (item.QIsNecessary == "必填")
                            {
                                _requiredchk++;
                            }

                            break;




                        case ("文字"):
                            if (item.QIsNecessary == "必填")
                                necessary = "(必填)";
                            this.plcQuestion.Controls.Add(new Literal { ID = $"txtltl{_txti}", Text = $"<br/><label id=Q{_txti}=>" + $"{item.Question}{necessary}" + "</label><br/>" });
                            if (item.QIsNecessary == "必填")
                            {

                                Panel plcrequired = new Panel()
                                {
                                    ID = $"txtrequired{_requiredtxt}",
                                    CssClass = "required"
                                };
                                this.plcQuestion.Controls.Add(plcrequired);
                               
                                 required = true;
                            }


                            foreach (var itemRdo in arrayrdo)
                            {
                                buildTxt(itemRdo, required, $"txtrequired{_requiredtxt}", item.ANumber);
                            }
                           
                            break;



                        default:
                            break;
                    }


                }
            }
        }
        private void buildTxt(string answer, bool required, string plcid, int Number)
        {
            TextBox txtBtn = new TextBox()
            {
                ID = $"A{Number}txt{_txti}",
                Text = answer,


            };

            if (required == false)
            {
                this.plcQuestion.Controls.Add(txtBtn);
            }
            else
            {
                FindControl(plcid).Controls.Add(txtBtn);
                _requiredtxt++;
            }
            _txti++;

        }

        private void buildChk(string answer, bool required, string plcid, int Number)
        {
            CheckBox ckBtn = new CheckBox()
            {
                ID = $"A{Number}chk{_chki}",
                Text = answer + "<br/>",


            };
            if (required == false)
            {
                this.plcQuestion.Controls.Add(ckBtn);

            }
            else
            {
                FindControl(plcid).Controls.Add(ckBtn);
            }
            _chki++;
        }

        private void buildRdo(string answer, bool required, string plcid, int groupname, int Number)
        {
            RadioButton rdoBtn = new RadioButton()
            {
                ID = $"A{Number}rdo{_rdoi}",
                Text = answer + "<br/>",
                GroupName = $"123{groupname}"
            };

            if (required == false)
            {
                this.plcQuestion.Controls.Add(rdoBtn);

            }
            else
            {
                FindControl(plcid).Controls.Add(rdoBtn);
            
            }
            _rdoi++;
        }
    }

}