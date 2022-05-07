using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionary.Pages.Back
{
    public partial class BaseQuestion : System.Web.UI.Page
    {
        QuestionaryModel model = new QuestionaryModel();
        QuestionModel _modelQ = new QuestionModel();
        QuestionayManager _mgr = new QuestionayManager();
        QuestionManager _mgrQ = new QuestionManager();
        static Guid _questionID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<QuestionModel> list = _mgrQ.GetAllBaseQuestion();
                this.ret1.DataSource = list;
                this.ret1.DataBind();
            }
           
        }


        //加入問題
        protected void btnconfirmQ_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Session["Question"] as string) == true)
            {
                Response.Write("<script>alert('若要加入問題請按加入問題,此按鈕為編輯問題後的完成鍵')</script>");
                return;
            }


            _modelQ.QuestionID = Guid.NewGuid();
            _modelQ.Question = this.txtQuestion.Text;
            _modelQ.Answer = this.txtanswer.Text;


            List<string> checkans = this.txtanswer.Text.Split(';').ToList();
            bool isRequest = checkans.GroupBy(i => i).Where(g => g.Count() > 1).Count() > 0;
            if (isRequest == true)
            {
                Response.Write("<script>alert('有重複的答案')</script>");
                return;
            }




            if (string.IsNullOrEmpty(this.txtQuestion.Text) != true)
            {
                _modelQ.Question = this.txtQuestion.Text;
             
            }
            else
            {
                Response.Write("<script>alert('尚未輸入題目')</script>");
                return;
            }



            if (this.checknecessary.Checked)
            {
                //必填為0
                _modelQ.QIsNecessary = "必填";
                this.Session["Necessary"] = "必填";

            }
            else
            {   //非必填為1
                _modelQ.QIsNecessary = "非必填";
                this.Session["Necessary"] = "非必填";
            }


            if (Convert.ToInt32(this.dowMode.SelectedValue) == 0)
            {
                //單選為0
                _modelQ.QQMode = "單選";
                this.Session["Mode"] = "單選";


            }
            else if (Convert.ToInt32(this.dowMode.SelectedValue) == 1)
            {
                //複選為1
                _modelQ.QQMode = "複選";
                this.Session["Mode"] = "複選";

            }
            else
            {
                _modelQ.QQMode = "文字";
                this.Session["Mode"] = "文字";

            }
            if (_modelQ.QQMode == "複選" && !txtanswer.Text.Contains(";"))
            {
                Response.Write("<script>alert('複選必須輸入兩個(含)以上答案')</script>");
                return;
            }

            if (string.IsNullOrEmpty(this.txtanswer.Text) != true & _modelQ.QQMode != "文字")
            {
                _modelQ.Answer = this.txtanswer.Text;
               
            }
            else if (_modelQ.QQMode == "文字")
            {
                _modelQ.Answer = this.txtanswer.Text;
                
            }

            else
            {
                Response.Write("<script>alert('因回答類型非文字方塊,需輸入問題回答')</script>");
                return;
            }


            if (_mgrQ.CreateBaseQuestion(_modelQ) == true)
            {
                Response.Write("<script>alert('問題新增成功！')</script>");
                List<QuestionModel> list = _mgrQ.GetAllBaseQuestion();
                this.ret1.DataSource = list;
                this.ret1.DataBind();
            }
            else
            {

                Response.Write("<script>alert'問題新增失敗！ 可能是存在相同問題')</script>");
                return;
            }

        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
        protected void btnFinalConfirm_Click(object sender, EventArgs e)
        {
           if(this.checknecessary.Checked == true)
            {
                this.Session["Necessary"] = "必填";
            }
            else
            {
                this.Session["Necessary"] = "非必填";
            }



            if (string.IsNullOrEmpty(this.Session["Question"] as string) == true)
            {
                Response.Write("<script>alert('若要加入問題請按加入問題,此按鈕為編輯問題後的完成鍵')</script>");
                return;
            }

            _modelQ.Question = this.Session["Question"] as string;
            _modelQ.Answer = this.Session["Answer"] as string;
            _modelQ.QIsNecessary = this.Session["Necessary"] as string;
            _modelQ.QQMode = this.Session["Mode"] as string;
            _modelQ.QuestionID = _questionID;

            //error


            //修改問題方法要寫成常用問題專用
            if (_mgrQ.UpdateBaseQuestion(_modelQ) == true)
            {
                Response.Write("<script>alert('編輯成功')</script>");
                

            }

            List<QuestionModel> list = _mgrQ.GetAllBaseQuestion();
            this.ret1.DataSource = list;
            this.ret1.DataBind();




        }
        protected void btnFinalCancek_Click(object sender, EventArgs e)
        {

        }
        protected void ret1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "btnEdit":
                    QuestionModel modelEdit = new QuestionModel();
                     _questionID = Guid.Parse(e.CommandArgument.ToString());
                   


                    modelEdit = _mgrQ.GetOneBaseQuestionInfo(_questionID);               
                    this.txtQuestion.Text = modelEdit.Question;
                    this.Session["Question"] = this.txtQuestion.Text;
                    this.txtanswer.Text = modelEdit.Answer;
                    this.Session["Answer"] = this.txtanswer.Text;
                    if (modelEdit.QQMode == "複選")
                    {
                        this.Session["Mode"] = "複選";
                        this.dowMode.SelectedIndex = 1;
                        this.txtanswer.Enabled = true;
                    }
                        
                    if (modelEdit.QQMode == "單選")
                    {
                        this.Session["Mode"] = "單選";
                        this.dowMode.SelectedIndex = 0;
                        this.txtanswer.Enabled = true;
                    }
                       
                    if (modelEdit.QQMode == "文字")
                    {
                        this.dowMode.SelectedIndex = 2;
                        this.txtanswer.Text = "";
                        this.txtanswer.Enabled= false;
                        this.Session["Mode"] = "文字";
                    }
                       
                    if (modelEdit.QIsNecessary == "必填")
                    {
                        this.checknecessary.Checked = true;
                        this.Session["Necessary"] = "必填";
                    }
                    else
                    {
                        this.checknecessary.Checked = false;
                        this.Session["Necessary"] = "非必填";
                    }
                        



                    break;

                case "btnDelete":
                    QuestionModel modelDelete = new QuestionModel();
                    string arrDelete = e.CommandArgument.ToString();
                    Guid _tempDeleteID = Guid.Parse(arrDelete);

                    if (_mgrQ.DeleteBaseQuestion(_tempDeleteID) == true)
                    {
                        Response.Write("<script>alert('刪除成功')</script>");

                    };
                    List<QuestionModel> list = _mgrQ.GetAllBaseQuestion();
                    this.ret1.DataSource = list;
                    this.ret1.DataBind();
                    break;

                default:

                    break;

            }

        }

        protected void dowMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(this.dowMode.SelectedValue) == 2)
            {
                this.txtanswer.Text = "";
                this.txtanswer.Enabled = false;
                this.Session["Mode"] = "文字";
                
            }
            if(Convert.ToInt32(this.dowMode.SelectedValue)==0)
            {
                this.Session["Mode"] = "單選";
                this.txtanswer.Enabled = true;
            }
            if (Convert.ToInt32(this.dowMode.SelectedValue) == 1)
            {
                this.Session["Mode"] = "複選";
                this.txtanswer.Enabled = true;
            }

        }

        protected void txtanswer_TextChanged(object sender, EventArgs e)
        {
            this.Session["Answer"] = this.txtanswer.Text; 
        }

        protected void txtQuestion_TextChanged(object sender, EventArgs e)
        {
            this.Session["Question"] = this.txtQuestion.Text;
        }
    }
}