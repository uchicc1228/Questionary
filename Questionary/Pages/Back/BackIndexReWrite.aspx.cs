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
    public partial class BackIndexReWrite : System.Web.UI.Page
    {
        QuestionayManager _mgr = new QuestionayManager();
        OtherManager _mgrO = new OtherManager();
        private const int _pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {


            string pageIndexText = this.Request.QueryString["Index"];
            int pageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);


            if (!this.IsPostBack)
            {
         
                //timesearch
                if (string.IsNullOrWhiteSpace(this.Request.QueryString["Start"]) == false && string.IsNullOrWhiteSpace(this.Request.QueryString["End"]) == false)
                {
                    string _keyword = this.Session["word"] as string;
                    if (!string.IsNullOrWhiteSpace(_keyword))
                        this.titlesearch.Text = _keyword;
                    string timestart = this.Request.QueryString["Start"];
                    string timeend = this.Request.QueryString["End"];
                    this.titlesearch.Text = _keyword;
                    this.txtCalender_start.Text = this.Request.QueryString["Start"];
                    this.txtCalender_end.Text = this.Request.QueryString["End"];
                    int _totalRows = 0;
                    var _list = _mgrO.GetMapList(_keyword, _pageSize, pageIndex, out _totalRows, timestart, timeend);
                    this.ucPager.TotalRows = _totalRows;
                    this.ucPager.PageIndex = pageIndex;
                    this.ucPager.Bind("keyword", _keyword);
                    if (_list.Count == 0)
                    {
                        this.plcEmpty.Visible = true;
                        this.rptList.Visible = false;
                        this.ucPager.Visible = false;
                        this.Session.Clear();
                    }
                    else
                    {
                        this.plcEmpty.Visible = false;
                        this.rptList.Visible = true;

                        this.rptList.DataSource = _list;
                        this.rptList.DataBind();
                        this.Session.Clear();
                    }


                    return;
                }






                string keyword = this.Session["word"] as string;
                if (!string.IsNullOrWhiteSpace(keyword))
                    this.titlesearch.Text = keyword;
                int totalRows = 0;
                keyword = this.titlesearch.Text;
                var list = this._mgrO.GetMapList(keyword, _pageSize, pageIndex, out totalRows,this.txtCalender_start.Text,this.txtCalender_end.Text);
                this.ucPager.TotalRows = totalRows;
                this.ucPager.PageIndex = pageIndex;
                this.ucPager.Bind("keyword", keyword);

                if (list.Count == 0)
                {
                    this.plcEmpty.Visible = true;
                    this.rptList.Visible = false;
                    this.ucPager.Visible = false;
                    this.Session.Clear();
                }
                else
                {
                    this.plcEmpty.Visible = false;
                    this.rptList.Visible = true;

                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                    this.Session.Clear();
                }













           








            }

        }

        //批量刪除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            CheckBox checkbox = new CheckBox();                 //創建對象
            HiddenField id;                                     //創建對象
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                checkbox = (CheckBox)rptList.Items[i].FindControl("CheckBox3");//取對象
                id = (HiddenField)rptList.Items[i].FindControl("HiddenField1");//取對象
                if (checkbox.Checked == true)                   //是否被選中
                {
                    Guid questionid = Guid.Parse(id.Value.ToString());  //賦值

                    _mgr.DeleteQuestionary(questionid);

                }
            }

            if (string.IsNullOrEmpty(this.titlesearch.Text) == false && this.txtCalender_start.Text != null && this.txtCalender_start.Text != null)
            {
                string url = this.Request.Url.LocalPath + "?Start=" + this.txtCalender_start.Text + "&" + "End=" + this.txtCalender_end.Text + "&" + "?keyword=" + this.titlesearch.Text;
                this.Session["word"] = this.titlesearch.Text;
                this.Response.Redirect(url);

            }
            if (string.IsNullOrEmpty(this.titlesearch.Text) == true && this.txtCalender_start.Text != null)
            {
                string url = this.Request.Url.LocalPath + "?Start=" + this.txtCalender_start.Text + "&" + "End=" + this.txtCalender_end.Text;
                this.Response.Redirect(url);

            }


            if (string.IsNullOrEmpty(this.titlesearch.Text) == false)
            {
                string url = this.Request.Url.LocalPath + "?keyword=" + this.titlesearch.Text;
                this.Response.Redirect(url);
            }



        }

        protected void btnAddList_Click(object sender, EventArgs e)
        {
            Guid ID = Guid.NewGuid();
            Response.Redirect("AddQuestionaryMultiView.aspx?ID=" + ID.ToString());
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.titlesearch.Text) == false && this.txtCalender_start.Text != null && this.txtCalender_start.Text != null)
            {
                string url = this.Request.Url.LocalPath + "?Start=" + this.txtCalender_start.Text + "&" + "End=" + this.txtCalender_end.Text + "&" + "?keyword=" + this.titlesearch.Text;
                this.Session["word"] = this.titlesearch.Text;
                this.Response.Redirect(url);

            }
            if (string.IsNullOrEmpty(this.titlesearch.Text) == true && this.txtCalender_start.Text !=null )
            {
                string url = this.Request.Url.LocalPath + "?Start=" + this.txtCalender_start.Text + "&"+"End=" + this.txtCalender_end.Text;
                this.Response.Redirect(url);

            }


            if (string.IsNullOrEmpty(this.titlesearch.Text) == false)
            {
                string url = this.Request.Url.LocalPath + "?keyword=" + this.titlesearch.Text;
                this.Response.Redirect(url);
            }


        }
    }
}