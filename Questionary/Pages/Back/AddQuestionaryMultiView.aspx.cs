using Questionary.Helper;
using Questionary.Managers;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionary.Pages.Back
{
    public partial class AddQuestionaryMultiView : System.Web.UI.Page
    {
        static mylist list = new mylist();
        List<QuestionModel> questionary = new List<QuestionModel>();
        static List<string> listx = new List<string>(); //1.選項 = 此問題的全部選項名稱
        static List<string> listw = new List<string>(); //1.選項 = 此問題的全部選項名稱
        static List<string> listy = new List<string>(); //2.數值 = 分子 =幾個使用者選擇該選項
        static List<string> listv = new List<string>();
        static List<Guid> listz = new List<Guid>(); //3.題目名稱 = 此問題名稱

        QuestionaryModel model = new QuestionaryModel();
        QuestionModel _modelQ = new QuestionModel();
        QuestionayManager _mgr = new QuestionayManager();
        QuestionManager _mgrQ = new QuestionManager();
        UserInfoManager _mgrU = new UserInfoManager();
        OtherManager _mgrO = new OtherManager();
        static Guid _questionayGuid;
        private const int _pageSize = 10;
        static List<UserInfoModel> num;
        #region "動態生成控制項所需變數"
        static int _qrdoi = 0;
        static int _qchki;
        static int _qtxti;
        #endregion

        int QNumber = 1;
        int i = 0;
        int k = 0;
        int j = 0;
        int Group = 0;
        /// <summary>
        ///  ActiveViewIndex為該問卷的 { 0 = 新增頁面} , { 1 = 編輯頁面}  , { 2 = 填寫資料}  ,  { 3 = 統計頁面}
        ///  主頁面點選新增按鈕 -> 0        | 
        ///  主頁面上點選問卷連結 -> 1  　  |              都是同連結 只是會帶上問卷ID(QID) 以及 0,1,2,3 
        ///  主頁面上點選統計連結 -> 3      |              ToDO = 將創建問卷時寫入的URL改為此往頁URL
        /// </summary>      
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["ID"] != null)
            {
                _questionayGuid = Guid.Parse(Request.QueryString["ID"]);
            }
         
            num = _mgrU.GetAllWriter(_questionayGuid);
            if (num.Count > 0)
            {
                TextQuestionaryTitle.Enabled = false;
                TextQuestionaryContent.Enabled = false;
                TextQuestionaryStartTime.Enabled = false;
                TextQuestionaryEndTime.Enabled = false;
                txtQuestion.Enabled = false;
                txtanswer.Enabled = false;
                this.Add.Enabled = false;
                this.Empty.Enabled = false;
                this.btnconfirmQ.Enabled = false;
                this.btnFinalConfirm.Enabled = false;
                this.btnFinalCancek.Enabled = false;

            }

            ///找ㄉ道! 編輯 找不到 新增 用session
            ///
            if (!IsPostBack && (Request.QueryString["ID"]) as string != null)
            {
                Session.Clear();
                _questionayGuid = Guid.Parse(Request.QueryString["ID"]);
                if (num.Count > 0)
                {

                    TextQuestionaryTitle.Enabled = false;
                    TextQuestionaryContent.Enabled = false;
                    TextQuestionaryStartTime.Enabled = false;
                    TextQuestionaryEndTime.Enabled = false;
                    txtQuestion.Enabled = false;
                    txtanswer.Enabled = false;
                    this.Add.Enabled = false;
                    this.Empty.Enabled = false;
                    this.btnconfirmQ.Enabled = false;
                    this.btnFinalConfirm.Enabled = false;
                    this.btnFinalCancek.Enabled = false;

                }



                this.Session["ID"] = _questionayGuid;
                List<QuestionaryModel> _listForEdit = _mgr.GetOneQuestionaryInfo(_questionayGuid);
                if (_listForEdit.Count == 1)
                {
                    this.Session["PageMode"] = "編輯";
                }
                else if (_listForEdit.Count == 0)
                {
                    this.Session["PageMode"] = "新增";
                }


            }




            //若QUERY有跳頁 就會自動道INDEX2
            //if (!IsPostBack && Request.QueryString["Index"] != null)
            //    this.MultiQuestionary.ActiveViewIndex = 2;
            if (!IsPostBack)
            {


                List<QuestionaryModel> _listForEdit = _mgr.GetOneQuestionaryInfo(_questionayGuid);
                if (this.Session["PageMode"] as string == "編輯")
                {
                    this.AddQuestionaryBtn.Text = "編輯問卷";
                    this.ltlmesg.Text = "<h1>此為編輯模式 您可以在此更改問卷基本狀態</h1>";
                    this.Session["Edit"] = "y";
                    this.Add.Text = "編輯";
                    QuestionaryModel _tmpQuestionaryModel = _mgr.GetQuestionaryWithID(_questionayGuid);
                    this.TextQuestionaryTitle.Text = _tmpQuestionaryModel.Title;
                    this.TextQuestionaryContent.Text = _tmpQuestionaryModel.Content;
                    this.TextQuestionaryStartTime.Text = _tmpQuestionaryModel.StartTime.ToString("yyyy-MM-dd");
                    this.TextQuestionaryEndTime.Text = _tmpQuestionaryModel.EndTime_string;
                    QNumber = _tmpQuestionaryModel.Number;
                    if (Request.QueryString["Index"] != null)
                    {
                        this.MultiQuestionary.ActiveViewIndex = 2;
                        string pageIndexText = this.Request.QueryString["Index"];
                        int pageIndex =
                            (string.IsNullOrWhiteSpace(pageIndexText))
                                ? 1
                                : Convert.ToInt32(pageIndexText);


                        var listwee = this._mgrO.GetMapList(_pageSize, pageIndex);

                        if (listwee.Count == 0)
                        {
                            ucPager.Visible = false;
                        }


                        this.ucPager._urlID = _questionayGuid.ToString();
                        List<UserInfoModel> totalRows = _mgrO.tottalrows();
                        this.ucPager.TotalRows = totalRows.Count();
                        this.ucPager.PageIndex = pageIndex;
                        this.ucPager.Bind();
                        if (totalRows.Count < 10)
                        {
                            ucPager.Visible = false;
                        }
                        this.repeaterGetout.DataSource = listwee;
                        this.repeaterGetout.DataBind();

                    }

                    ListItem _customedQuestion = new ListItem() { Text = "自訂問題", Value = "0" };
                    this.dowList.Items.Add(_customedQuestion);
                    List<QuestionModel> _ddlList_ = _mgrQ.GetDropDownListItem();
                    QuestionModel _item_;

                    for (int i = 0; i < _ddlList_.Count; i++)
                    {
                        _item_ = _ddlList_.ElementAt(i);
                        this.dowList.Items.Add(new ListItem() { Text = _item_.Question, Value = _item_.QuestionID.ToString() });
                    }
                    this.plcall.Visible = true;
                    this.Session.Remove("status");
                    return;
                }
                if (this.Session["PageMode"] as string == "新增")
                {
                    if (this.Session["Added"] as string == "Y")
                    {
                        this.MultiQuestionary.ActiveViewIndex = 1;
                        this.TextQuestionaryTitle.Enabled = false;
                        this.TextQuestionaryContent.Enabled = false;
                        this.TextQuestionaryStartTime.Enabled = false;
                        this.TextQuestionaryEndTime.Enabled = false;
                    }


                    this.ltlmesg.Text = "<h1>此為新增模式 您可以直接在此增加問卷</h1>";
                    string today = DateTime.Now.ToString("yyyy-MM-dd");
                    this.TextQuestionaryStartTime.Text = today;
                    this.ltlmsg.Text = "\t**若結束日期不填，則問卷有效期限為永遠";
                    //this.TextQuestionaryEndTime.Text = today;
                    //model.ID = Guid.Parse(Request.QueryString["ID"]);
                    List<QuestionModel> list = _mgrQ.GetAllQuestion(_questionayGuid);
                    this.ret1.DataSource = list;
                    this.ret1.DataBind();
                    ListItem customedQuestion = new ListItem() { Text = "自訂問題", Value = "0" };
                    this.dowList.Items.Add(customedQuestion);
                    List<QuestionModel> _ddlList = _mgrQ.GetDropDownListItem();
                    QuestionModel _item;
                    if (Request.QueryString["Index"] != null)
                    {
                        this.MultiQuestionary.ActiveViewIndex = 2;
                        string pageIndexText = this.Request.QueryString["Index"];
                        int pageIndex =
                            (string.IsNullOrWhiteSpace(pageIndexText))
                                ? 1
                                : Convert.ToInt32(pageIndexText);


                        var listwee = this._mgrO.GetMapList(_pageSize, pageIndex);

                        if(listwee.Count == 0)
                        {
                            ucPager.Visible = false;
                        }


                        this.ucPager._urlID = _questionayGuid.ToString();
                        List<UserInfoModel> totalRows = _mgrO.tottalrows();
                        this.ucPager.TotalRows = totalRows.Count();
                        this.ucPager.PageIndex = pageIndex;
                        this.ucPager.Bind();
                        if (totalRows.Count < 10)
                        {
                            ucPager.Visible = false;
                        }
                        this.repeaterGetout.DataSource = listwee;
                        this.repeaterGetout.DataBind();

                    }
                    for (int i = 0; i < _ddlList.Count; i++)
                    {
                        _item = _ddlList.ElementAt(i);

                        this.dowList.Items.Add(new ListItem() { Text = _item.Question, Value = _item.QuestionID.ToString() });

                    }
                }

            }



            //this.txtanswer.Enabled = true;








        }

        protected void AddQuestionaryBtn_Click(object sender, EventArgs e)
        {


            //view的問卷新增
            this.MultiQuestionary.ActiveViewIndex = 0;
            QuestionaryModel _tmpQuestionaryModel = _mgr.GetQuestionaryWithID(_questionayGuid);
            if (_tmpQuestionaryModel != null)
            {
                this.TextQuestionaryTitle.Text = _tmpQuestionaryModel.Title;
                this.TextQuestionaryContent.Text = _tmpQuestionaryModel.Content;
                this.TextQuestionaryStartTime.Text = _tmpQuestionaryModel.StartTime.ToString("yyyy-MM-dd");
                this.TextQuestionaryEndTime.Text = _tmpQuestionaryModel.EndTime_string;
            }



        }

        protected void EditQuestionBtn_Click(object sender, EventArgs e)
        {

            this.MultiQuestionary.ActiveViewIndex = 1;
            QuestionaryModel _tmpModel = _mgr.GetQuestionaryModel(_questionayGuid);
            if (_tmpModel == null)
            {
                this.Session["notAdd"] = "-1";
                this.ltlmeess.Text = "<h3>****請先新增問卷哦****</h3>";
                //Response.Write("<script>alert('請先新增問卷')</script>");
                this.MultiQuestionary.ActiveViewIndex = 0;
                return;
            }
            this.lblTitle.Text = _tmpModel.Title;
            this.lblContent.Text = _tmpModel.Content;
            this.lblStartTime.Text = _tmpModel.StartTime.ToString("yyyy/MM/dd");
            this.lblEndTime.Text = _tmpModel.EndTime_string;

            List<QuestionModel> _list = _mgrQ.GetAllQuestion(_questionayGuid);
            this.ret1.DataSource = _list;
            this.ret1.DataBind();




        }

        protected void FilledInfoBtn_Click(object sender, EventArgs e)
        {
            QuestionaryModel _tmpModel = _mgr.GetQuestionaryModel(_questionayGuid);
            if (_tmpModel == null)
            {
                this.Session["notAdd"] = "-1";
                this.ltlmeess.Text = "<h3>****請先新增問卷哦****</h3>";
                //Response.Write("<script>alert('請先新增問卷')</script>");
                this.MultiQuestionary.ActiveViewIndex = 0;
                return;
            }

            this.MultiQuestionary.ActiveViewIndex = 2;

            if (Request.QueryString["Index"] == null)
            {
                this.getout.Visible = true;
                this.ucPager.Visible = true;
                this.plcall.Visible = true;
                this.plcone.Visible = false;

                this.MultiQuestionary.ActiveViewIndex = 2;
                string pageIndexText = this.Request.QueryString["Index"];
                int pageIndex =
                    (string.IsNullOrWhiteSpace(pageIndexText))
                        ? 1
                        : Convert.ToInt32(pageIndexText);


                var listwee = this._mgrO.GetMapList(_pageSize, pageIndex, _questionayGuid);

                if (listwee.Count == 0 )
                {
                    Response.Write("<script>alert('尚無資料')</script>");
                    ucPager.Visible = false;
                }



                this.ucPager._urlID = _questionayGuid.ToString();
                List<UserInfoModel> totalRows = _mgrO.tottalrows();
                this.ucPager.TotalRows = totalRows.Count();
                this.ucPager.PageIndex = pageIndex;
                this.ucPager.Bind();
                if(totalRows.Count < 10)
                {
                    ucPager.Visible = false;
                }
                this.repeaterGetout.DataSource = listwee;
                this.repeaterGetout.DataBind();
            }


        }

        protected void StatisticsBtn_Click(object sender, EventArgs e)
        {

            this.MultiQuestionary.ActiveViewIndex = 3;
            QuestionaryModel _tmpModel = _mgr.GetQuestionaryModel(_questionayGuid);
            if (_tmpModel == null)
            {
                this.Session["notAdd"] = "-1";
                this.ltlmeess.Text = "<h3>****請先新增問卷哦****</h3>";
                //Response.Write("<script>alert('請先新增問卷')</script>");
                this.MultiQuestionary.ActiveViewIndex = 0;
                return;
            }
            List<QuestionModel> _tmpQModel = new List<QuestionModel>();
            _tmpQModel = _mgrQ.GetAllQuestionNoDesc(_questionayGuid);
            mylist mylistmodel = couculate(_questionayGuid);
            List<string> x = list.xvalue;//該題目選項
            List<string> y = list.yvalue;//該題目各選項分別的值
            List<string> v = list.vvalue;//該題目各選選項總數
            List<string> w = list.wvalue;//題目
            List<Guid> z = list.zvalue;
            if (_tmpQModel.Count == 0)
            {
                Response.Write("<script>alert('尚無資料')</script>");
            }



            //生成題目
            foreach (var item in _tmpQModel)
            {
                string necessary = "";
                string[] arrayrdo = item.Answer.Split(';');
                int xcount = 0;
                int ycount = 0;
                int vcount = 0;
                switch (item.QQMode)
                {

                    case ("單選"):
                        if (item.QIsNecessary == "必填")
                        {
                            necessary = "(必填)";
                        }
                        this.plcQuestion.Controls.Add(new Literal { ID = $"rdoltl{_qrdoi}", Text = $"<br/>{item.ANumber}.{item.Question}{necessary} <br/>" });
                        for (int L = 0; L < list.xvalue.Count; L++)
                        {
                            if (w[L] == item.Question)
                            {
                                string[] xx = x[L].Split(',');
                                string[] cc = y[L].Split(',');
                                int anscount = 0;
                                double totalvalue = Convert.ToInt32(v[vcount]);

                                foreach (var xitem in xx)
                                {
                                    double ansvalue = Convert.ToDouble(cc[anscount]);
                                    double percent = Math.Round(ansvalue / totalvalue * 100, 3);
                                    Literal literal = new Literal { ID = $"rdoans{xcount}", Text = $"{xitem}" + $"{cc[anscount]}" + $"({percent}" + "%)<br/>" };
                                    this.plcQuestion.Controls.Add(literal);
                                    anscount++;
                                    xcount++;
                                }

                            }
                        }

                        _qrdoi++;
                        break;

                    case ("複選"):
                        if (item.QIsNecessary == "必填")
                        {
                            necessary = "(必填)";
                        }
                        this.plcQuestion.Controls.Add(new Literal { ID = $"chkltl{_qchki}", Text = $" <br/>{item.ANumber}.{item.Question}{necessary}<br/>" });
                        for (int L = 0; L < list.xvalue.Count; L++)
                        {
                            if (w[L] == item.Question)
                            {
                                string[] xx = x[L].Split(',');
                                string[] cc = y[L].Split(',');
                                int anscount = 0;
                                double totalvalue = Convert.ToInt32(v[vcount]);

                                foreach (var xitem in xx)
                                {
                                    double ansvalue = Convert.ToDouble(cc[anscount]);
                                    double percent = Math.Round(ansvalue / totalvalue * 100, 3);
                                    Literal literal = new Literal { ID = $"rdoans{xcount}", Text = $"{xitem}" + $"{cc[anscount]}" + $"({percent}" + "%)<br/>" };
                                    this.plcQuestion.Controls.Add(literal);
                                    anscount++;
                                    xcount++;
                                }

                            }
                        }
                        _qchki++;

                        break;

                    case ("文字"):
                        if (item.QIsNecessary == "必填")
                        {
                            necessary = "(必填)";
                        }

                        this.plcQuestion.Controls.Add(new Literal { ID = $"txtltl{_qtxti}", Text = $"<br/>{item.ANumber}.{item.Question}{necessary}<br/>" });

                        _qtxti++;


                        break;

                    default:
                        break;
                }

            }


        }

        /// <summary>
        /// 這邊做創建問卷,Number為識別規格 不用插入 QuestionaryUrl 用戶言寫的前台 Edit為後台編輯(就是本頁面)
        /// 防呆1. 日期開始必須小於結束 2.所有欄位必填入 3.查詢有無相同問卷
        /// </summary>

        protected void Add_Click(object sender, EventArgs e)
        {
            if (this.Session["Edit"] as string == "y")
            {

                QuestionaryModel _EditModel = new QuestionaryModel();
                _EditModel.ID = Guid.Parse(Request.QueryString["ID"]);
                _EditModel.Title = this.TextQuestionaryTitle.Text;
                _EditModel.Content = this.TextQuestionaryContent.Text;
                _EditModel.StartTime = Convert.ToDateTime(this.TextQuestionaryStartTime.Text);
                _EditModel.EndTime_string = this.TextQuestionaryEndTime.Text;
                if (ChkisEnable.Checked)
                {
                    _EditModel.Status = "開放";
                }
                else
                {
                    _EditModel.Status = "關閉";
                    _EditModel.QDisplay = -1;
                }

                DateTime _start = Convert.ToDateTime(this.TextQuestionaryStartTime.Text);
                if (string.IsNullOrWhiteSpace(this.TextQuestionaryEndTime.Text) != true)
                {
                    DateTime end = Convert.ToDateTime(this.TextQuestionaryEndTime.Text);

                    if (DateTime.Compare(_start, end) > 0)
                    {
                        Response.Write("<script>alert('請注意日期先後順序')</script>");
                        return;
                    }
                }
                //防呆2
                if (string.IsNullOrWhiteSpace(this.TextQuestionaryTitle.Text) == true)
                {
                    Response.Write("<script>alert('標題必填')</script>");
                    return;
                }

                //防呆2
                if (string.IsNullOrWhiteSpace(this.TextQuestionaryContent.Text) == true)
                {
                    Response.Write("<script>alert('內容必填')</script>");
                    return;
                }




                //更新問卷資訊

                if (_mgr.UpDateQuestionary(_EditModel))
                {
                    Response.Write("<script>alert('編輯成功')</script>");
                    this.MultiQuestionary.ActiveViewIndex = 1;
                    this.TextQuestionaryTitle.Enabled = false;
                    this.TextQuestionaryContent.Enabled = false;
                    this.TextQuestionaryStartTime.Enabled = false;
                    this.TextQuestionaryEndTime.Enabled = false;
                    this.Add.Enabled = false;
                    _questionayGuid = Guid.Parse(Request.QueryString["ID"]);
                    QuestionaryModel _tmpModel = _mgr.GetQuestionaryModel(_questionayGuid);
                    if (_tmpModel != null)
                    {
                        this.lblTitle.Text = _tmpModel.Title;
                        this.lblContent.Text = _tmpModel.Content;
                        this.lblStartTime.Text = _tmpModel.StartTime.ToString("yyyy/MM/dd");
                        this.lblEndTime.Text = _tmpModel.EndTime_string;
                    }
                }
                else
                {
                    Response.Write("<script>alert('失敗，請聯繫管理員')</script>");
                }

                return;

            }



            //防呆1
            DateTime start = Convert.ToDateTime(this.TextQuestionaryStartTime.Text);
            if (string.IsNullOrWhiteSpace(this.TextQuestionaryEndTime.Text) != true)
            {
                DateTime end = Convert.ToDateTime(this.TextQuestionaryEndTime.Text);

                if (DateTime.Compare(start, end) > 0)
                {
                    Response.Write("<script>alert('請注意日期先後順序')</script>");
                    return;
                }
            }
            //防呆2
            if (string.IsNullOrWhiteSpace(this.TextQuestionaryTitle.Text) == true)
            {
                Response.Write("<script>alert('標題必填')</script>");
                return;
            }
            //防呆2
            if (string.IsNullOrWhiteSpace(this.TextQuestionaryContent.Text) == true)
            {
                Response.Write("<script>alert('內容必填')</script>");
                return;
            }

            //防呆3
            QuestionaryModel _temporary = _mgr.GetQuestionaryWithTitle(this.TextQuestionaryTitle.Text);
            if (_temporary != null)
            {
                Response.Write("<script>alert('已有相同名稱問卷')</script>");
                return;
            }


            if (DateTime.Parse(TextQuestionaryStartTime.Text) < DateTime.Now.AddDays(-1))
            {
                Response.Write("<script>alert('開始日期不能比今天早')</script>");
                return;
            }




            //新增    
            model.ID = _questionayGuid;
            model.Title = this.TextQuestionaryTitle.Text;
            model.Content = this.TextQuestionaryContent.Text;
            model.StartTime = Convert.ToDateTime(this.TextQuestionaryStartTime.Text);
            model.EndTime_string = (this.TextQuestionaryEndTime.Text);
            model.QuestionaryUrl = "../Front/FrontQuestionaryQ.aspx?ID=" + model.ID;
            model.QuestionaryEditUrl = "AddQuestionaryMultiView.aspx?ID=" + model.ID;


            if (this.ChkisEnable.Checked)
                model.Status = "啟用";
            else
                model.Status = "關閉";

            if (_mgr.CreateQuestionary(model) == true)
            {

                Response.Write("<script>alert('新增成功，自動幫你轉成新增問題')</script>");
                this.MultiQuestionary.ActiveViewIndex = 1;
                this.TextQuestionaryTitle.Enabled = false;
                this.TextQuestionaryContent.Enabled = false;
                this.TextQuestionaryStartTime.Enabled = false;
                this.TextQuestionaryEndTime.Enabled = false;
                this.Add.Enabled = false;
                this.Empty.Enabled = false;
                this.Session["Added"] = "Y";
                this.Session.Remove("notAdd");

           

                QuestionaryModel _tmpModel = _mgr.GetQuestionaryModel(model.ID);
                if (_tmpModel != null)
                {
                    this.lblTitle.Text = _tmpModel.Title;
                    this.lblContent.Text = _tmpModel.Content;
                    this.lblStartTime.Text = _tmpModel.StartTime.ToString("yyyy/MM/dd");
                    this.lblEndTime.Text = _tmpModel.EndTime_string;
                }
                this.ltlmsg.Text = "問卷已新增成功,請往新增問題頁籤進行動作。";
            }
            else
            {
                Response.Write("<script>alert('新增失敗，請重新進入頁面')</script>");
            }


        }

        /// <summary>
        /// 這邊是新增該問卷的問題
        /// </summary>
        protected void btnconfirmQ_Click(object sender, EventArgs e)
        {
            if (_mgrQ.GetAllQuestionqq(_questionayGuid) != -1)
            {
                _modelQ.QNumber = _mgrQ.GetAllQuestionqq(_questionayGuid) + 1;
            }
            else
            {
                _modelQ.QNumber = 0;
            }

            // 先找有沒有相同名稱的題目
            List<QuestionModel> _tmpModel = _mgrQ.GetAllQuestion(_questionayGuid);
            List<QuestionModel> _listq = _tmpModel.FindAll(x => x.Question == this.txtQuestion.Text);
            if (_listq.Count > 0)
            {
                Response.Write("<script>alert('已有相同題目')</script>");
                return;
            }

            List<string> checkans = this.txtanswer.Text.Split(';').ToList();
            bool isRequest = checkans.GroupBy( i  => i).Where(g => g.Count() >1 ).Count() >0;
            if(isRequest == true)
            {
                Response.Write("<script>alert('有重複的答案，或是您未輸入任何答案')</script>");
                return;
            }


            if (this.txtanswer.Text.Substring(this.txtanswer.Text.Length - 1, 1) == ";")
            {
                this.txtanswer.Text = this.txtanswer.Text.Remove(this.txtanswer.Text.Length - 1, 1);
            }



            //http://localhost:14351/Pages/Back/BackIndexReWrite.aspx


            _modelQ.QID = _questionayGuid;
            _modelQ.QCatrgory = "自訂";
            this.Session["Catrgory"] = _modelQ.QCatrgory;
            if (string.IsNullOrEmpty(this.txtQuestion.Text) != true)
            {
            



                _modelQ.Question = this.txtQuestion.Text;
                this.Session["Question"] = _modelQ.Question;
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
                this.Session["IsNecessary"] = _modelQ.QIsNecessary;
            }
            else
            {   //非必填為1
                _modelQ.QIsNecessary = "非必填";
                this.Session["IsNecessary"] = _modelQ.QIsNecessary;
            }
            if (Convert.ToInt32(this.dowMode.SelectedValue) == 0)
            {
                //單選為0
                _modelQ.QQMode = "單選";
                this.Session["Mode"] = _modelQ.QQMode;

            }
            else if (Convert.ToInt32(this.dowMode.SelectedValue) == 1)
            {
                //複選為1
                _modelQ.QQMode = "複選";
                this.Session["Mode"] = _modelQ.QQMode;
            }
            else
            {
                _modelQ.QQMode = "文字";
                this.Session["Mode"] = _modelQ.QQMode;

            }
            if (_modelQ.QQMode == "複選" && !txtanswer.Text.Contains(";"))
            {
                Response.Write("<script>alert('複選必須輸入兩個(含)以上答案')</script>");
                return;
            }

            if (string.IsNullOrEmpty(this.txtanswer.Text) != true & _modelQ.QQMode != "文字")
            {
                _modelQ.Answer = this.txtanswer.Text;
                this.Session["Answer"] = _modelQ.Answer;
            }
            else if (_modelQ.QQMode == "文字")
            {
                _modelQ.Answer = this.txtanswer.Text;
                this.Session["Answer"] = _modelQ.Answer;
            }
            else
            {
                Response.Write("<script>alert('因回答類型非文字方塊,需輸入問題回答')</script>");
                return;
            }




            this.Session["Number"] = QNumber;

            if (_mgrQ.CreateQuestion(_modelQ) == true)
            {
                Response.Write("<script>alert('問題新增成功！')</script>");
                List<QuestionModel> list = _mgrQ.GetAllQuestion(_modelQ.QID);
                this.ret1.DataSource = list;
                this.ret1.DataBind();
                QNumber++;
            }
            else
            {
                Response.Write("<script>alert'問題新增失敗！ 可能是存在相同問題')</script>");
                return;
            }






        }
        protected void ret1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "btnEdit":

                    num = _mgrU.GetAllWriter(_questionayGuid);

                    if (this.Session["PageMode"] as string == "編輯" && num.Count > 0)
                    {
                        Response.Write("<script>alert('該問卷已有填寫者，故無法變更問卷內容。')</script>");
                        return;
                    }
                    QuestionModel model2 = new QuestionModel();
                    string[] arr = e.CommandArgument.ToString().Split(',');
                    Guid id;
                    Guid.TryParse(arr[0], out id);
                    int number;
                    int.TryParse(arr[1], out number);

                    model2.QID = id;
                    model2.Number = number;
                    this.Session["Number"] = model2.Number;

                    model2 = _mgrQ.GetAllQuestionInfoSameNum(model2);
                    if (model2.QCatrgory == "自訂")
                    {
                        this.dowList.SelectedIndex = 0;
                    }
                    else
                    {
                        this.dowList.SelectedIndex = 1;
                    }

                    this.txtQuestion.Text = model2.Question;
                    this.Session["Question"] = this.txtQuestion.Text;

                    this.txtanswer.Text = model2.Answer;
                    this.Session["Answer"] = this.txtanswer.Text;
                    if (model2.QQMode == "複選")
                    {
                        this.dowMode.SelectedIndex = 1;
                        this.Session["Mode"] = "複選";
                    }

                    if (model2.QQMode == "單選")
                    {
                        this.dowMode.SelectedIndex = 0;
                        this.Session["Mode"] = "單選";
                    }

                    if (model2.QQMode == "文字")
                    {
                        this.dowMode.SelectedIndex = 2;
                        this.Session["Mode"] = "文字";
                    }

                    if (model2.QIsNecessary == "必填")
                    {
                        this.checknecessary.Checked = true;
                        this.Session["IsNecessary"] = "必填";

                    }
                    else
                    {
                        this.checknecessary.Checked = false;
                        this.Session["IsNecessary"] = "非必填";
                    }






                    break;

                case "btnDelete":
                    QuestionModel model3 = new QuestionModel();
                    string[] arr1 = e.CommandArgument.ToString().Split(',');
                    Guid id1;
                    Guid.TryParse(arr1[0], out id1);
                    Guid qid;
                    Guid.TryParse(arr1[1], out qid);
                    model3.QID = id1;
                    model3.QuestionID = qid;
                    model3.QDisplay = -1;
                    num = _mgrU.GetAllWriter(_questionayGuid);

                    if (this.Session["PageMode"] as string == "編輯" && num.Count > 0)
                    {
                        Response.Write("<script>alert('該問卷已有填寫者，故無法變更問卷內容。')</script>");
                        return;
                    }
                    if (_mgrQ.DeleteQuestion(model3) == true)
                    {
                        Response.Write("<script>alert('刪除成功')</script>");

                    };
                    List<QuestionModel> list = _mgrQ.GetAllQuestion(model3.QID);
                    this.ret1.DataSource = list;
                    this.ret1.DataBind();
                    break;

                default:

                    break;

            }

        }


        //每個問題右側的編輯按鈕
        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }


        //todo: 取消按紐 跳回上一頁並清空該頁資料
        protected void btnFinalCancek_Click(object sender, EventArgs e)
        {

        }

        // 編輯後的送出資料 此按鈕會更改DB內資訊
        protected void btnFinalConfirm_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.Session["Question"] as string) == true)
            {
                Response.Write("<script>alert('若要加入問題請按加入問題,此按鈕為編輯問題後的完成鍵')</script>");
                return;
            }
            this.MultiQuestionary.ActiveViewIndex = 1;
            _modelQ.QID = _questionayGuid;
            _modelQ.Question = this.Session["Question"] as string;
            _modelQ.Answer = this.Session["Answer"] as string;
            _modelQ.QCatrgory = "自訂";
            _modelQ.QIsNecessary = this.Session["IsNecessary"] as string;
            _modelQ.QQMode = this.Session["Mode"] as string;
            _modelQ.Number = (int)this.Session["Number"];
            //error


            //修改問題方法

            if (_mgrQ.UpdateQuestion(_modelQ) == true)
            {
                Response.Write("<script>alert('編輯成功')</script>");
                List<QuestionModel> list = _mgrQ.GetAllQuestion(_modelQ.QID);
                this.ret1.DataSource = list;
                this.ret1.DataBind();

            }

        }

        #region "textchanged事件"
        protected void txtQuestion_TextChanged(object sender, EventArgs e)
        {
            this.MultiQuestionary.ActiveViewIndex = 1;
            this.Session["Question"] = this.txtQuestion.Text;

        }

        protected void txtanswer_TextChanged(object sender, EventArgs e)
        {
            this.MultiQuestionary.ActiveViewIndex = 1;
            this.Session["Answer"] = this.txtanswer.Text;

        }
        #endregion

        #region "下拉式選單 複選方塊選定事件"
        protected void dowMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.MultiQuestionary.ActiveViewIndex = 1;
            if (dowMode.SelectedIndex == 0)
                this.Session["Mode"] = "單選";
            else if (dowMode.SelectedIndex == 1)
                this.Session["Mode"] = "複選";
            else if (dowMode.SelectedIndex == 2)
            {
                this.Session["Mode"] = "文字";
                this.txtanswer.Text = "";
                this.txtanswer.Enabled = false;
            }


            if (Convert.ToInt32(this.dowMode.SelectedValue) == 2)
            {
                this.txtanswer.Enabled = false;
            }
            else
            {
                this.txtanswer.Enabled = true;
            }
        }

        protected void checknecessary_CheckedChanged(object sender, EventArgs e)
        {
            //this.MultiQuestionary.ActiveViewIndex = 1;
            if (this.checknecessary.Checked)
            {
                this.Session["IsNecessary"] = "必填";
            }
            else
            {
                this.Session["IsNecessary"] = "非必填";
            }

        }
        #endregion

        protected void dowList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dowList.SelectedItem.Text.Contains("文字"))
            {
                this.txtanswer.Enabled = false;
                this.txtanswer.Text = "";
            }



            if (dowList.SelectedIndex != 0)
            {
                int _HowLong = this.dowList.SelectedValue.Count();
                string _QuestionID = this.dowList.SelectedValue.Substring(0, _HowLong);
                //利用這個questionID去查庫desu
                _modelQ = _mgrQ.GetOneBaseQuestionInfo(Guid.Parse(_QuestionID));

                this.txtQuestion.Text = _modelQ.Question;
                this.txtanswer.Text = _modelQ.Answer;
                //單複選
                if (_modelQ.QQMode == "單選")
                {
                    this.dowMode.SelectedIndex = 0;

                }
                else if (_modelQ.QQMode == "複選")
                {
                    this.dowMode.SelectedIndex = 1;

                }
                else
                {
                    this.dowMode.SelectedIndex = 2;
                    this.txtanswer.Enabled = false;
                }

                if(_modelQ.QIsNecessary == "必填")
                {
                    this.checknecessary.Checked = true;
                }
                else
                {
                    this.checknecessary.Checked = false;
                }





            }


        }

        protected void repeaterGetout_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            UserInfoModel _UserInfo = new UserInfoModel();
            List<QuestionModel> _UserQuestion = new List<QuestionModel>();
            switch (e.CommandName)
            {
                case "btnUserID":

        
                    this.getout.Enabled = false;
                    this.ucPager.Visible = false;
                    this.plcall.Visible = false;
                    this.plcone.Visible = true;
                    string[] arr = e.CommandArgument.ToString().Split(',');
                    Guid _UserID;
                    Guid _QuestionaryID;
                    Guid.TryParse(arr[0], out _UserID);
                    Guid.TryParse(arr[1], out _QuestionaryID);

                    _UserInfo = _mgrU.GetOneQuestionInfo(_UserID);
                    this.ltlPlcMsg.Text = "<h1>" + $"此為{_UserInfo.UserName}的填寫資訊" + "</h1>";
                    this.txt_plconeUserName.Text = _UserInfo.UserName;
                    this.txt_plconeUserAge.Text = _UserInfo.UserAge;
                    this.txt_plconeUsereMail.Text = _UserInfo.UserEmail;
                    this.txt_plconeUserPhone.Text = _UserInfo.UserPhone;
                    _UserQuestion = _mgrQ.GetAllQuestionNoDesc(_QuestionaryID);
                    int listcount = _UserQuestion.Count;
                    List<UserInfoModel> list = new List<UserInfoModel>();
                    list = _mgrQ.findquestion(_QuestionaryID, _UserID);
                    int xcount = 0;
                    List<QuestionModel> _tmplist = new List<QuestionModel>();
                    foreach (var item in list)
                    {
                        _tmplist = _UserQuestion.FindAll(b => b.QuestionID == item.QuestionID);
                        _tmplist.Sort();
                        foreach (var _item in _tmplist)
                        {
                            if (item.UserTextAnswer != "未作答")
                            {
                                item.UserTextAnswer = item.UserTextAnswer.Remove(item.UserTextAnswer.Count() - 1);
                            }
                            switch (_item.QQMode)
                            {
                                case "單選":
                                    //題目先
                                    Literal ltlrdoQ = new Literal() { ID = $"ltlrdoQ{QNumber}", Text = $"<br/><br/>{QNumber}.{_item.Question}<br/>" };
                                    this.plcone.Controls.Add(ltlrdoQ);
                                    //答案們
                                    string[] _tmpansrdo = _item.Answer.Split(';');
                                    foreach (var itemrdo in _tmpansrdo)
                                    {
                                        if (item.UserTextAnswer == itemrdo)
                                        {
                                            RadioButton _radiowee = new RadioButton() { ID = $"rdo{i}", Text = "<br/>" + itemrdo + "<br/>", Checked = true, Enabled = false };
                                            this.plcone.Controls.Add(_radiowee);
                                            i++;
                                        }
                                        else
                                        {
                                            RadioButton _radio = new RadioButton() { ID = $"rdo{i}", Text = "<br/>" + itemrdo + "<br/>", Enabled = false };
                                            this.plcone.Controls.Add(_radio);
                                            i++;
                                        }
                                    }
                                    QNumber++;
                                    break;

                                case "複選":
                                    Literal ltlchkQ = new Literal() { ID = $"ltlchkQ{QNumber}", Text = $"<br/><br/>{QNumber}.{_item.Question}<br/>" };
                                    this.plcone.Controls.Add(ltlchkQ);
                                    List<string> _questionans = _item.Answer.Split(';').ToList();//題目的選項
                                    List<string> _userans = item.UserTextAnswer.Split(';').ToList();//使用者的選項                                   
                                    foreach (var _chkitem in _questionans)
                                    {
                                        CheckBox _chk = new CheckBox() { ID = $"chk{k}", Text = $"<br/>{_chkitem}<br/>", Enabled = false, Checked = false };
                                        if (_userans.Contains(_chkitem) == true)
                                        {
                                            _chk.Checked = true;
                                        }
                                        this.plcone.Controls.Add(_chk);
                                    }
                                    QNumber++;
                                    break;

                                case "文字":
                                    Literal ltltxtQ = new Literal() { ID = $"ltltxtQ{QNumber}", Text = $"<br/><br/>{QNumber}.{_item.Question}<br/>" };
                                    this.plcone.Controls.Add(ltltxtQ);
                                    TextBox _txtwee = new TextBox() { ID = $"txt{j}", Text = item.UserTextAnswer, Enabled = false };
                                    this.plcone.Controls.Add(_txtwee);
                                    QNumber++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
            }
        }

        protected void btnsession_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Session.Keys)
            {
                Response.Write(item.ToString());
            }
        }

        protected void btnUserID_Click(object sender, EventArgs e)
        {

        }

        private void BuildTextBox(int i, string Question, string Answer, string necessary, int number)
        {

            this.plcone.Controls.Add(new Literal() { ID = "textltl" + i, Text = "<br/>" + $"{number}." + Question + necessary + ": <br/>" });
            this.plcone.Controls.Add(new TextBox() { ID = "txt" + i, Text = Answer, Enabled = false });

        }
        private void BuildCheckBox(int j, string Answer, string Question, string necessary)
        {
            this.plcone.Controls.Add(new Literal() { ID = "checkltl" + j, Text = "<br/>" + Question + necessary + ":" + "<br/>" });
            if (Answer == "未作答")
            {
                this.plcone.Controls.Add(new CheckBox() { ID = "chk" + j, Text = Answer, Enabled = false });
            }
            else
            {
                this.plcone.Controls.Add(new CheckBox() { ID = "chk" + j, Text = Answer, Checked = true, Enabled = false });
            }




        }
        private void BuildRadioBox(int k, string UserAnswer, string Question, string necessary)
        {
            this.plcone.Controls.Add(new Literal() { ID = "rdoltl" + i, Text = "<br/>" + Question + necessary + ": <br/>" });
            if (UserAnswer == "未作答")
            {

                this.plcone.Controls.Add(new RadioButton() { ID = "rdo" + k, Text = UserAnswer, GroupName = $"group{k}", Enabled = false });
            }
            else
            {
                this.plcone.Controls.Add(new RadioButton() { ID = "rdo" + k, Text = UserAnswer, GroupName = $"group{k}", Checked = true, Enabled = false });
            }



        }
        protected void getout_Click(object sender, EventArgs e)
        {

            //抓出該問卷的所有填寫的使用者 問題 問題的答案 的LIST

            List<UserInfoModel> _list = _mgrU.GetAllWriter(_questionayGuid);
            QuestionaryModel model = _mgr.GetQuestionaryModel(_questionayGuid);
            List<CSV_Model> list = new List<CSV_Model>();
            list = _mgrO.GetAllQuestionCSV(_questionayGuid, _list);
            string filepath = "";
            CSV_Helper cSV_Helper = new CSV_Helper();

            cSV_Helper.CreateFold(model.Title, out filepath);
            cSV_Helper.CSVGenerator<CSV_Model>(true, filepath, list);


        }

        protected void Empty_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl);
            if (this.Session["PageMode"] as string == "編輯" == true)
            {
                this.MultiQuestionary.ActiveViewIndex = 1;
                this.TextQuestionaryTitle.Enabled = true;
                this.TextQuestionaryTitle.Text = "";

                this.TextQuestionaryContent.Enabled = true;
                this.TextQuestionaryContent.Text = "";

                this.TextQuestionaryStartTime.Enabled = true;
                this.TextQuestionaryEndTime.Enabled = true;
                this.Add.Enabled = true;

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

        private mylist couculate(Guid QID)
        {
            Guid questionaryid = _questionayGuid;
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
                if (!listz.Contains(c))
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
                        List<StatciModel> _thisAnsQ = _statci.FindAll(z => z.Answer.Contains(item.Answer));
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
            list.zvalue = listz;

            return list;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

            num = _mgrU.GetAllWriter(_questionayGuid);

            if (this.Session["PageMode"] as string == "編輯" && num.Count > 0)
            {
                Response.Write("<script>alert('該問卷已有填寫者，故無法變更問卷內容。')</script>");
                return;
            }




            CheckBox checkbox = new CheckBox();                 //創建對象
            HiddenField id;                                     //創建對象
            for (int i = 0; i < ret1.Items.Count; i++)
            {
                checkbox = (CheckBox)ret1.Items[i].FindControl("CheckBox3");//取對象
                id = (HiddenField)ret1.Items[i].FindControl("HiddenField1");//取對象
                if (checkbox.Checked == true)                   //是否被選中
                {
                    Guid questionid = Guid.Parse(id.Value.ToString());  //賦值

                    _mgrQ.DeleteQuestion(questionid);

                }
            }
            List<QuestionModel> list = _mgrQ.GetAllQuestion(_questionayGuid);
            this.ret1.DataSource = list;
            this.ret1.DataBind();
        }

    }
}