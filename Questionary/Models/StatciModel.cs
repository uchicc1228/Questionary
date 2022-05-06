using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class StatciModel
    {
       public Guid QID { get; set; }
        public Guid QuestionID { get; set; }
        public Guid UserID { get; set; }

        public string UserName { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }  

        public string UserTextAnswer { get; set; }

    }
}