using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Questionary.Helper
{
    public class CSV_Helper
    {

        public void CreateFold(string title, out string filepath)
        {
            if (!Directory.Exists(@"C:\tmp"))
                Directory.CreateDirectory((@"C:\tmp"));
            if (!File.Exists($@"C:\tmp\helper{title}.csv"))
                File.Create($@"C:\tmp\helper{title}.csv").Close();
            filepath = $@"C:\tmp\helper{title}.csv";
            StreamReader streamReader = new StreamReader(filepath); 
            streamReader.Close();
        }


        public void CSVGenerator<T>(bool genColumn, string FilePath, List<T> data)
        {
            
            using (var file = new StreamWriter(new FileStream(FilePath,FileMode.Open),encoding:System.Text.Encoding.UTF8))
            {
               
                Type t = typeof(T);
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //是否要輸出屬性名稱
                if (genColumn)
                {
                    file.WriteLineAsync(string.Join(",", propInfos.Select(i => i.Name)));
                }
                foreach (var item in data)
                {
                    file.WriteLineAsync(string.Join(",", propInfos.Select(i => i.GetValue(item))));
                }
                file.Close();
            }
            
        }
    }
}