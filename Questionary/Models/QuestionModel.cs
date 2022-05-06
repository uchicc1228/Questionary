using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class QuestionModel
    {
        /// <summary>
        /// Table Question
        /// </summary>

        public Guid QID { get; set; }
        public Guid QuestionID { get; set; }
        public int Number { get; set; }
        public string Question { get; set; }

        public int QDisplay{ get; set; } 
        public string Answer { get; set; }

        public string QIsNecessary { get; set; }

        public string QQMode { get; set; }

       
        public string QCatrgory { get; set; }

        public int QNumber { get; set; }
        public  int  ANumber    { get; set; }
        public string UserAnswer { get; set; }
    }
}