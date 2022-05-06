using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class QuestionaryModel
    {
        //問卷的model
        public Guid ID { get; set; }
        public Guid QuestionID { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime StartTime { get; set; }

        public string StartTime_string { get; set; }

        public string EndTime_string { get; set; }


        public string Status { get; set; }

        public string QuestionaryUrl { get; set; }

        public string QuestionaryEditUrl { get; set; }

        //是否顯示 使用於阮刪除
       public int QDisplay { get; set; }

    }
}