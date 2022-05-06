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
    
    public partial class FrontStastic : System.Web.UI.Page
    {
        QuestionManager mgr = new QuestionManager();
        QuestionayManager questionayManager = new QuestionayManager();  
        QuestionaryModel model = new QuestionaryModel();    
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid _guid = Guid.Parse(Request.QueryString["QID"]);
            this.hfid.Value = Request.QueryString["QID"];
            model.ID = _guid;
            model = questionayManager.GetQuestionaryModel(model.ID);
            this.lbltitle.Text = model.Title;   


        }
    }
}