using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Questionary.ShareControls
{
    public partial class ucPager : System.Web.UI.UserControl
    {
        
        /// <summary> 目前頁數 </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary> 一頁幾筆 </summary>
        public int PageSize { get; set; } = 10;

        /// <summary> 共幾筆 </summary>
        public int TotalRows { get; set; } = 0;

        /// <summary> 分頁用的 QueryString Key </summary>
        public string IndexName { get; set; } = "Index";


        public string _urlID = "";


        private string _url = null;

        /// <summary> 要跳至哪個 URL (預設為本頁) </summary>
        public string Url
        {
            get
            {
                if (this._url == null)
                    return Request.Url.LocalPath;
                else
                    return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public void Bind()
        {
            NameValueCollection collection = new NameValueCollection();
            this.Bind(collection);
        }

        public void Bind(string paramKey, string paramValue)
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add(paramKey, paramValue);
            this.Bind(collection);
        }

        public void Bind(NameValueCollection collection)
        {
            int pageCount = (this.TotalRows / this.PageSize);
            if ((this.TotalRows % this.PageSize) > 0)
                pageCount += 1;

            string url = this.Url;


            // 第一頁，上一頁，下一頁，最未頁
            int prevIndex = ((this.PageIndex - 1) > 0) ? this.PageIndex - 1 : 1;
            int nextIndex = ((this.PageIndex + 1) < pageCount) ? this.PageIndex + 1 : pageCount;
            this.aLinkFirst.HRef = url + "?" + this.BuildQueryString(collection, 1) ;
            this.aLinkPrev.HRef = url + "?" + this.BuildQueryString(collection, prevIndex);
            this.aLinkNext.HRef = url + "?" + this.BuildQueryString(collection, nextIndex);
            this.aLinkLast.HRef = url + "?" + this.BuildQueryString(collection, pageCount);

            this.aLinkPage1.HRef = url + "?" + this.BuildQueryString(collection, this.PageIndex - 2) ;
            this.aLinkPage1.InnerText = (this.PageIndex - 2).ToString();
            if (this.PageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?" + this.BuildQueryString(collection, this.PageIndex - 1)  ;
            this.aLinkPage2.InnerText = (this.PageIndex - 1).ToString();
            if (this.PageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = this.PageIndex.ToString();

            this.aLinkPage4.HRef = url + "?" + this.BuildQueryString(collection, this.PageIndex + 1)  ;
            this.aLinkPage4.InnerText = (this.PageIndex + 1).ToString();
            if ((this.PageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?" + this.BuildQueryString(collection, this.PageIndex + 2)  ;
            this.aLinkPage5.InnerText = (this.PageIndex + 2).ToString();
            if ((this.PageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;
        }

        private string BuildQueryString(NameValueCollection collection, int currentPageIndex)
        {
            collection.Remove(this.IndexName);
            collection.Add(this.IndexName, currentPageIndex.ToString());

            return BuildQueryString(collection);
        }

        /// <summary> 組合 QueryString </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private string BuildQueryString(NameValueCollection collection)
        {
            List<string> paramList = new List<string>();
            // &key=value
            foreach (string key in collection.AllKeys)
            {
                if (collection.GetValues(key) == null)
                    continue;

                foreach (string val in collection.GetValues(key))
                {
                    paramList.Add($"{key}={val}");
                }
            }
            const string connStr = "&";
            string result = string.Join(connStr, paramList);
            return result;
        }
    }
}