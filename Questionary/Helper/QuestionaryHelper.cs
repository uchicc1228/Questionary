using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionary.Helper
{
    public class QuestionaryHelper
    {
        //目前沒用到
        public List<QuestionaryModel> PaginationList(List<QuestionaryModel> qq, int pageIndex)
        {
            var pagesize = 2;
            int skip = pagesize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            return qq.Skip(skip).Take(2).ToList();

        }
    }
}