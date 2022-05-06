using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class UserInfoModel
    {
        public Guid UserID { get; set; }
        public Guid QID { get; set; }
        public Guid QuestionID { get; set; }


        public DateTime UserWriteTime { get; set; }
        public string UserWriteTime_string { get; set; }


        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserAge { get; set; }
        public string QuestionAnswer { get; set; }
        public string UserAnswer { get; set; }
        public string UserTextAnswer { get; set; }
        public string UserQuestion { get; set; }
        public int Number { get; set; }












    }
}