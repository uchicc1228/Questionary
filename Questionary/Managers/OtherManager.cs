using Questionary.Helper;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;

namespace Questionary.Managers
{
    public class OtherManager
    {
        #region"分頁"
        public List<QuestionaryModel> PafinationONE(int pageSize, int pageIndex, out int totalRows)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"SELECT TOP  ({pageSize}) *
                     FROM Questionary 
                      WHERE    QNumber not IN 
                         (
                              SELECT TOP ({skip})  QNumber
                              FROM Questionary )
                        		ORDER BY QNumber DESC  		 
                    ";
            string commandCountText =
              $@" SELECT COUNT(QID) 
                                FROM Questionary;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionaryModel> retList = new List<QuestionaryModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            QuestionaryModel model = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Number = (int)reader["QNumber"],
                                Title = reader["QTitle"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Status = reader["QStatus"] as string,
                                Content = reader["QContent"] as string,
                                QuestionaryUrl = reader["QuestionUrl"] as string
                            };
                            model.StartTime_string = model.StartTime.ToString("yyyy/MM/dd");
                            if (string.IsNullOrEmpty(model.EndTime_string))
                            {
                                model.EndTime_string = "-";
                            }
                            list.Add(model);

                        }
                        reader.Close();
                        command.CommandText = commandCountText;
                        totalRows = (int)command.ExecuteScalar();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

   

        //分頁
        public List<QuestionaryModel> Pafination(string time_start, string time_end, int pageSize, int pageIndex, out int totalRows)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"SELECT TOP  {(pageSize)}  *
                    FROM Questionary 
                     WHERE    QNumber not IN 
                          (
                             SELECT TOP {(skip)}  QNumber
                             FROM Questionary     		    
	                   		 WHERE QNumber IN 
	                   		 ( SELECT QNumber FROM Questionary
	                   		    where [QStartTime] >= @QStartTime
                                and [QEndTime] <=  @QEndTime )
	                   		 ORDER BY QNumber DESC)	  		   
                                                                                                  
	                   	     and  [QStartTime] >= @QStartTime
                             and [QEndTime] <=    @QEndTime	
	                   		 ORDER BY QNumber DESC;   ";
            string commandCountText =
              $@" SELECT COUNT(QID) 
                                FROM Questionary;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QStartTime", time_start);
                        command.Parameters.AddWithValue("@QEndTime", time_end);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionaryModel> retList = new List<QuestionaryModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            QuestionaryModel model = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Number = (int)reader["QNumber"],
                                Title = reader["QTitle"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Status = reader["QStatus"] as string,
                                Content = reader["QContent"] as string,
                                QuestionaryUrl = reader["QuestionUrl"] as string
                            };
                            model.StartTime_string = model.StartTime.ToString("yyyy/MM/dd");
                            if (string.IsNullOrEmpty(model.EndTime_string))
                            {
                                model.EndTime_string = "-";
                            }
                            list.Add(model);

                        }
                        reader.Close();
                        command.CommandText = commandCountText;
                        totalRows = (int)command.ExecuteScalar();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        //有標題的分頁
        public List<QuestionaryModel> PafinationHasTitle(string title, string time_start, string time_end, int pageSize, int pageIndex)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@"SELECT TOP  {(pageSize)}  *
                    FROM Questionary 
                     WHERE    QNumber not IN 
                          (
                             SELECT TOP {(skip)}  QNumber
                             FROM Questionary     		    
	                   		 WHERE QNumber IN 
	                   		 ( SELECT QNumber FROM Questionary
	                   		    where  QTitle like  '%' + @Qtitle + '%' and 
                                [QStartTime] >= @QStartTime
                                and [QEndTime] <=  @QEndTime )
	                   									           ORDER BY QNumber DESC)	 
                             and  [QTitle] like   '%' + @Qtitle +'%'                                                                   
	                   	     and  [QStartTime] >= @QStartTime
                             and [QEndTime] <=    @QEndTime	
	                   		 ORDER BY QNumber DESC;   ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QStartTime", time_start);
                        command.Parameters.AddWithValue("@QEndTime", time_end);
                        command.Parameters.AddWithValue("@Qtitle", title);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<QuestionaryModel> retList = new List<QuestionaryModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            QuestionaryModel model = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Number = (int)reader["QNumber"],
                                Title = reader["QTitle"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Status = reader["QStatus"] as string,
                                Content = reader["QContent"] as string,
                                QuestionaryUrl = reader["QuestionUrl"] as string
                            };
                            model.StartTime_string = model.StartTime.ToString("yyyy/MM/dd");
                            if (string.IsNullOrEmpty(model.EndTime_string))
                            {
                                model.EndTime_string = "-";
                            }
                            list.Add(model);

                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }




        //內頁專用
        public List<UserInfoModel> GetMapList(int pageSize, int pageIndex)
        {
            List<UserInfoModel> list = new List<UserInfoModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT TOP  {pageSize}  *
                    FROM Person
                     WHERE  UserID not IN 
                          (
                             SELECT TOP {skip} UserID
                             FROM Person     		    
							 ORDER BY CreatTime DESC)	  		                                                            
	                   		 ORDER BY CreatTime DESC;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {
                                QID = (Guid)reader["QuestionaryID"],                    
                                UserName = reader["UserName"] as string,
                                UserWriteTime = (DateTime)reader["CreatTime"],
                                UserID = (Guid)reader["UserID"],

                            };
                            model.UserWriteTime_string = model.UserWriteTime.ToString("yyyy-MM-dd");
                            if (list.Find(x => x.UserID == model.UserID) == null)
                            {
                                list.Add(model);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        reader.Close();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }


        //內頁專用 列出有多少使用者那個頁籤
        public List<UserInfoModel> GetMapList(int pageSize, int pageIndex, Guid QuestionaryID)
        {
            List<UserInfoModel> list = new List<UserInfoModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT TOP  {pageSize}  *
                     FROM Person
                     WHERE  UserID not IN 
                          (
                             SELECT TOP {skip} UserID
                             FROM Person  
                             where QuestionaryID = @QuestionaryID
							 ORDER BY CreatTime DESC)	
                             and  QuestionaryID = @QuestionaryID
	                   		 ORDER BY CreatTime DESC;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QuestionaryID", QuestionaryID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {
                                QID = (Guid)reader["QuestionaryID"],
                                UserWriteTime = (DateTime)reader["CreatTime"],
                                UserID = (Guid)reader["UserID"],
                                UserName = reader["UserName"] as string

                            };
                            model.UserWriteTime_string = model.UserWriteTime.ToString("yyyy-MM-dd");
                            if (list.Find(x => x.UserID == model.UserID) == null)
                            {
                                list.Add(model);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        reader.Close();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        //列表頁專用(有標題)
        public List<QuestionaryModel> GetMapList(string keyword, int pageSize, int pageIndex, out int totalRows, string QStartTime, string QEndTime)
        {
            string date1 = "";
            string date2 = "";
            string date3 = "";
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
            if (skip < 0)
                skip = 0;

            string whereCondition = string.Empty;
  


            if (QEndTime != "")
            {
                date1 = "and [QEndTime] <=   @QEndTime and [QEndTime] != ''";
            }

            if (string.IsNullOrEmpty(QEndTime) == true)
            {
                date2 = "or [QEndTime] = ''  and QDisplay = '1'";
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                whereCondition = " AND QTitle LIKE '%'+@keyword+'%' ";

                if(QEndTime == "")
                {
                    date2 = date1;
                }
                
            }

            if(QStartTime != string.Empty)
            {
                date3 = "and [QStartTime] <=   @QStartTime ";
            }


            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT TOP {pageSize} *
                    FROM Questionary 
                    WHERE 
                        QID NOT IN 
                        (
                            SELECT TOP {skip} QID
                            FROM Questionary 
                            WHERE  [QStartTime] >= @QStartTime {date1} 
                             and QDisplay = '1' 
                                {whereCondition} 
                            ORDER BY QNumber DESC
                        )  
                            and [QStartTime] >= @QStartTime
                           {date1}
                            and QDisplay = '1' 
                        {whereCondition} 
                    ORDER BY QNumber DESC ";


            string commandCountText =
                $@" SELECT COUNT(QID) 
                    FROM Questionary
                    WHERE  QDisplay = '1' {date3} {date1}
                    {whereCondition}
                    ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);

                        }

                        command.Parameters.AddWithValue("@QEndTime", QEndTime);
                        command.Parameters.AddWithValue("@QStartTime", QStartTime);
                        // 參數化查詢
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionaryModel info = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Number = (int)reader["QNumber"],
                                Title = reader["QTitle"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Status = reader["QStatus"] as string,
                                Content = reader["QContent"] as string,
                                QuestionaryUrl = reader["QuestionUrl"] as string,
                                QuestionaryEditUrl = reader["QuestionEditUrl"] as string,
                            };
                            info.StartTime_string = info.StartTime.ToString("yyyy-MM-dd");
                            if (info.EndTime_string == "")
                            {
                                info.EndTime_string = "-";
                            }
                            list.Add(info);
                        }

                        reader.Close();

                        // 取得總筆數
                        command.CommandText = commandCountText;
                        totalRows = (int)command.ExecuteScalar();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public List<UserInfoModel> tottalrows()
        {
            List<UserInfoModel> list = new List<UserInfoModel>();
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT distinct USERID
                    FROM Person
                    ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {
                                UserID = (Guid)reader["USERID"],

                            };
                            list.Add(model);
                        }

                        return list;
                    }

                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        internal List<CSV_Model> GetAllQuestionCSV(Guid questionayGuid, List<UserInfoModel> userlist)
        {
            List<CSV_Model> list = new List<CSV_Model>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT 
                    Answers.UserName,
                    Answers.Question,
                    Answers.QID,
                    Answers.QuestionAnswer,
                    Answers.UserTextAnswer
                    FROM  Answers 
                    where Answers.QID  =@QID ";


            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", questionayGuid);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CSV_Model model = new CSV_Model()
                            {
                                UserName = reader["UserName"] as string,
                                QQuestion = reader["Question"] as string,
                                UserAnswer = reader["UserTextAnswer"] as string,

                            };
                            UserInfoModel model2 = userlist.Find(x => x.UserName == model.UserName);
                            if (model2 == null)
                            {
                                model.UserAge = "未填寫";
                                model.UserEmail = "未填寫";
                                list.Add(model);
                            }
                            else
                            {
                                model.UserAge = model2.UserAge;
                                model.UserEmail = model2.UserEmail;
                            }
                            if (model.UserAnswer == null)
                            {
                                model.UserAnswer = "未填寫";
                            }
                            list.Add(model);
                        }
                        reader.Close();
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        #endregion"分頁"

        #region "前台確認寫入資料"
        public bool ConfirmAnswer(UserInfoModel model)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" 
                    INSERT INTO UserManager
                        (QID, QuestionID, UserID,  UserName ,UserPhone, UserEmail, UserAge , UserTextAnswer,Number)
                    VALUES
                        (@QID, @QuestionID, @UserID,  @UserName, @UserPhone , @UserEmail, @UserAge , @UserTextAnswer,@Number);"
                                            +

                @" 
                    INSERT INTO Person
                        (QuestionaryID, UserID,UserName)
                    VALUES
                        (@QID,@UserID,@UserName);"

                                            +

                @" Insert INTO Answers 
                        (QID, QuestionID, UserID, UserName, UserTextAnswer, Question, QuestionAnswer)
                   VALUES 
                        (@QID, @QuestionID, @UserID, @UserName, @UserTextAnswer,@Question,@QuestionAnswer)";


            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QID", model.QID);
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@UserAnswer", model.UserAnswer);
                        command.Parameters.AddWithValue("@UserName", model.UserName);
                        command.Parameters.AddWithValue("@UserAge", model.UserAge);
                        command.Parameters.AddWithValue("@UserPhone", model.UserPhone);
                        command.Parameters.AddWithValue("@UserEmail", model.UserEmail);
                        command.Parameters.AddWithValue("@UserWriteTime", model.UserWriteTime);
                        command.Parameters.AddWithValue("@UserTextAnswer", model.UserTextAnswer);
                        command.Parameters.AddWithValue("@Number", model.Number);
                        command.Parameters.AddWithValue("@QuestionAnswer", model.QuestionAnswer);
                        command.Parameters.AddWithValue("@Question", model.UserQuestion);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {


                return false;
                throw (ex);
            }
        }

        #endregion

        #region "統計圖"
        public List<StatciModel> FindStaticInfo(Guid QID)
        {
            List<StatciModel> list = new List<StatciModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" Select *  
                   FROM  Answers 
                   WHERE QID = @QID 
                  ORDER BY Question ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", QID);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            StatciModel model = new StatciModel()
                            {
                                QID = (Guid)reader["QID"],
                                QuestionID = (Guid)reader["QuestionID"],
                                UserID = (Guid)reader["UserID"],
                                UserName = reader["UserName"] as string,
                                Question = reader["Question"] as string,
                                Answer = reader["QuestionAnswer"] as string,
                                UserTextAnswer = reader["UserTextAnswer"] as string
                            };

                            list.Add(model);
                        }
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);


            }
        }
        #endregion

        public QuestionModel GetBaseQuestion(string QQuestion)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                 @" SELECT *
                    FROM BaseQuestion
                    WHERE QQuestion = @QQuestion ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QQuestion", QQuestion);

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionModel model = new QuestionModel()
                            {
                                Question = reader["QQuestion"] as  string,

                            };
                            return model;

                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }




    }
}