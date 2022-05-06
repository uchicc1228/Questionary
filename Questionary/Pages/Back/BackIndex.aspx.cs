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
    public partial class BackIndex : System.Web.UI.Page
    {
        QuestionayManager _mgr = new QuestionayManager();
        List<QuestionaryModel> list = new List<QuestionaryModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //想要做Time_end +1  
            string Time = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtCalender_start.Value = Time;
            this.txtCalender_end.Value = "2022-12-31";

        }

        protected void btnAddList_Click(object sender, EventArgs e)
        {
            Guid ID = Guid.NewGuid();
            Response.Redirect("AddQuestionaryMultiView.aspx?ID=" + ID.ToString());
        }
    }
}