using Questionary.Helper;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionary.Managers
{
    public class QuestionayManager
    {
        #region "查"

        //用ID找ID

        //找出一個問眷
        public List<QuestionaryModel> GetOneQuestionay(Guid id)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Questionary
                    WHERE QID = @QID
                    and QDisplay = 1";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {


                        command.Parameters.AddWithValue("@QID", id);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
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
        //列出所有問卷
        public List<QuestionaryModel> GetALLQuestionay()
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Questionary
                    WHERE QDisplay = 1  
                    ORDER BY [QNumber] DESC;";
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
                            QuestionaryModel model = new QuestionaryModel()
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

        public List<QuestionaryModel> GetOneQuestionaryInfo(Guid ID)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Questionary
                    WHERE QID  =  @QID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", ID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
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
                                QuestionaryUrl = reader["QuestionUrl"] as string,
                                QuestionaryEditUrl = reader["QuestionEditUrl"] as string,

                            };
                            //model.StartTime_string = model.StartTime.ToString("yyyy-MM-dd");

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
                return null;
                throw (ex);
            }
        }



        //問卷的有標題的狀況下做查詢
        public List<QuestionaryModel> GetQuestionaryHaveTitle(string title, string time_start, string time_end)
        {
            List<QuestionaryModel> list = new List<QuestionaryModel>();
            string whereCondition = string.Empty;
            if (time_end != "")
            {
                whereCondition = "and [QEndTime] <=   @QEndTime ";
            }

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               $@"select * from Questionary where QTitle like  '%' + @title + '%' and 
                    [QStartTime] >= @QStartTime
                    {whereCondition}
                    and [QDisplay] = 1  
                    ORDER BY [QNumber] DESC";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@QStartTime", time_start);
                        command.Parameters.AddWithValue("@QEndTime", time_end);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
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
                            if (string.IsNullOrEmpty(model.EndTime_string) == false)
                            {
                                if (DateTime.Parse(model.EndTime_string) < DateTime.Now)
                                {
                                    model.Status = "已完結";
;
                                }
                            }
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



        //用日期列出搜尋結果
        public List<QuestionaryModel> GetQuestionaryWithTime(string time_start, string time_end)
        {

            List<QuestionaryModel> list = new List<QuestionaryModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               @"SELECT *
                    From Questionary
                    where [QStartTime] >= @QStartTime
                    and [QEndTime] <=   @QEndTime 
                    and QDisplay = 1 
                    ORDER BY [QNumber] DESC";

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
                            };
                            model.StartTime_string = model.StartTime.ToString("yyyy/MM/dd");
                            if (string.IsNullOrEmpty(model.EndTime_string) == false)
                            {
                                if (DateTime.Parse(model.EndTime_string) < DateTime.Now)
                                {
                                    model.Status = "已完結";

                                }
                            }
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
                return null;
                throw (ex);
            }
        }



        //用問卷標題找出Questionary Table內的資料
        public QuestionaryModel GetQuestionaryWithTitle(string title)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                 @" SELECT *
                    FROM Questionary
                    WHERE QTitle = @QTitle";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QTitle", title);

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
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

        public QuestionaryModel GetQuestionaryModel(Guid id)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Questionary
                    WHERE QID = @QID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {


                        command.Parameters.AddWithValue("@QID", id);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionaryModel model = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Title = reader["QTitle"] as string,
                                Content = reader["QContent"] as string,
                                Status = reader["QStatus"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Number = (int)reader["QNumber"],
                                QuestionaryUrl = reader["QuestionUrl"] as string,

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



        //用ID找出問卷 找一筆
        public QuestionaryModel GetQuestionaryWithID(Guid QID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                 @" SELECT *
                    FROM Questionary
                    WHERE QID = @QID";
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
                            QuestionaryModel model = new QuestionaryModel()
                            {
                                ID = (Guid)reader["QID"],
                                Number = (int)reader["QNumber"],
                                Title = reader["QTitle"] as string,
                                StartTime = (DateTime)reader["QStartTime"],
                                EndTime_string = reader["QEndTime"] as string,
                                Status = reader["QStatus"] as string,
                                Content = reader["QContent"] as string,
                                QuestionID = (Guid)reader["QID"]

                            };
                            model.StartTime_string = model.StartTime.ToString("yyyy/MM/dd");

                            if (string.IsNullOrEmpty(model.EndTime_string))
                            {
                                model.EndTime_string = "-";
                            }

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



        #endregion

        #region "刪除"

        //刪除一個問卷
        public bool DeleteQuestionary(Guid id)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"update Questionary set QDisplay = -1 where QID = @QID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QID", id);

                        conn.Open();

                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region "修"

        public bool UpDateQuestionary(QuestionaryModel model)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Questionary
                    SET   
                        QTitle = @QTitle,
                        QContent = @QContent,
                        QStartTime = @QStartTime,
                        QEndTime = @QEndTime,
                        QStatus  = @QStatus
                    WHERE
                        QID = @id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@id", model.ID);
                        command.Parameters.AddWithValue("@QTitle", model.Title);
                        command.Parameters.AddWithValue("@QContent", model.Content);
                        command.Parameters.AddWithValue("@QStartTime", model.StartTime);
                        command.Parameters.AddWithValue("@QEndTime", model.EndTime_string);
                        command.Parameters.AddWithValue("@QStatus", model.Status);
                       

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }

        }
        #endregion



        #region  "增"

        public bool CreateQuestionary(QuestionaryModel model)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" 
                    INSERT INTO Questionary
                        (QID, QTitle, QContent ,QStatus, QStartTime, QEndTime, QuestionUrl,QuestionEditUrl)
                    VALUES
                        (@QID, @QTitle , @QContent, @QStatus, @QStartTime, @QEndTime, @QuestionUrl,@QuestionEditUrl);" ;


            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", model.ID);
                        command.Parameters.AddWithValue("@QTitle", model.Title);
                        command.Parameters.AddWithValue("@QContent", model.Content);
                        command.Parameters.AddWithValue("@QStatus", model.Status);
                        command.Parameters.AddWithValue("@QStartTime", model.StartTime);
                        command.Parameters.AddWithValue("@QEndTime", model.EndTime_string);
                        command.Parameters.AddWithValue("@QuestionUrl", model.QuestionaryUrl);
                        command.Parameters.AddWithValue("@QuestionEditUrl", model.QuestionaryEditUrl);
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



    }
}