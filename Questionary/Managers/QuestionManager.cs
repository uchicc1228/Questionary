using Questionary.Helper;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionary.Managers
{
    public class QuestionManager
    {
        #region "查"
        //用ID找出Question Table內的資料
        public QuestionModel GetQuestionWithID(Guid ID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                 @" SELECT *
                    FROM Question
                    WHERE QID = @QID";
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
                            QuestionModel model = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                QuestionID = (Guid)reader["QNumber"],
                                
                            };

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


        //用QuestionID 找 USERANSWER{
        public QuestionModel GetQuestionAns(Guid QuestionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                 @" SELECT QAnswer,QQMode
                    FROM Question
                    WHERE QuestionID = @QuestionID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", QuestionID);

                        conn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionModel model = new QuestionModel()
                            {
                                QQMode = reader["QQMode"] as string,
                                Answer = reader["QAnswer"] as string

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
        //用ID找出該問題內所有東西
        public List<QuestionModel> GetAllQuestion(Guid QID)
        {
            List<QuestionModel> list = new List<QuestionModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Question
                    WHERE QID  =  @QID and QDisplay = 1
                    ORDER BY [QNumber] ;";
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
                            QuestionModel model = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                Question = reader["QQuestion"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                Number = (int)reader["QNumber"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["QAnswer"] as string,
                                ANumber = (int)reader["ANumber"],
                            };
                            if (string.IsNullOrEmpty(model.UserAnswer) == true)
                            {
                                model.UserAnswer = "未作答";
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
        public List<QuestionModel> GetAllQuestionNoDesc(Guid QID)
        {
            List<QuestionModel> list = new List<QuestionModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Question
                    WHERE QID  =  @QID and QDisplay = 1
                    ORDER BY [QNumber];";
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
                            QuestionModel model = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                Question = reader["QQuestion"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                Number = (int)reader["QNumber"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["QAnswer"] as string,
                                ANumber = (int)reader["ANumber"],
                            };
                            if (string.IsNullOrEmpty(model.UserAnswer) == true)
                            {
                                model.UserAnswer = "未作答";
                            }
                            model.ANumber++;
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
        //找出該問卷最大的問題號碼數
        public int GetAllQuestionqq(Guid QID)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Question
                    WHERE QID  =  @QID
                    ORDER BY [QNumber] DESC;";
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
                            QuestionModel model = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                Question = reader["QQuestion"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                Number = (int)reader["QNumber"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["QAnswer"] as string,

                                ANumber = (int)reader["ANumber"],
                                QNumber = (int)reader["QNumber"]
                            };
                            return model.QNumber;
                        }
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        public QuestionModel FindQuestionID(Guid QID, int QNumber, string QQMode)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Question
                    WHERE QNumber  =  @QNumber and QQMode = @QQMode and QID = @QID
                    ORDER BY [QNumber] DESC;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", QID);
                        command.Parameters.AddWithValue("@QQMode", QQMode);
                        command.Parameters.AddWithValue("@QNumber", QNumber);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionModel model = new QuestionModel()
                            {
                                QID = (Guid)reader["QID"],
                                Question = reader["QQuestion"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                Number = (int)reader["QNumber"],
                                QuestionID = (Guid)reader["QuestionID"],
                                Answer = reader["QAnswer"] as string,


                            };
                            if (string.IsNullOrEmpty(model.UserAnswer) == true)
                            {
                                model.UserAnswer = "未作答";
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

        public QuestionModel GetAllQuestionInfo(string QQuestion)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Question
                    WHERE QQuestion = @QQuestion;";
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
                                QID = (Guid)reader["QID"],
                                Question = reader["QQuestion"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                QuestionID = (Guid)reader["QuestionID"],

                            };
                            return model;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);


            }
        }

        public QuestionModel GetAllQuestionInfoSameNum(QuestionModel model)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               @"  SELECT *
                    FROM Question
                    WHERE QID  =  @QID and
                    QNumber = @QNumber;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", model.QID);
                        command.Parameters.AddWithValue("@QNumber", model.Number);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            model.QID = (Guid)reader["QID"];
                            model.Question = reader["QQuestion"] as string;
                            model.QQMode = reader["QQMode"] as string;
                            model.QIsNecessary = reader["QIsNecessary"] as string;
                            model.QCatrgory = reader["QCatrgory"] as string;
                            model.Number = (int)reader["QNumber"];
                            model.Answer = reader["QAnswer"] as string;
                            return model;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);


            }
        }

        public UserInfoModel findquestionID(Guid QuestionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               @"   SELECT *
                    FROM Answers
                    WHERE QuestionID = @QuestionID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", QuestionID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {

                                QuestionID = (Guid)reader["QuestionID"],
                                UserID = (Guid)reader["UserID"]
                            };
                            return model;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);


            }
        }


        public List<UserInfoModel> findquestion(Guid QID, Guid UserID)
        {
            List<UserInfoModel> list = new List<UserInfoModel>();
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               @"   SELECT *
                    FROM Answers
                    WHERE QID = @QID and UserID = @UserID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QID", QID);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {

                                QuestionID = (Guid)reader["QuestionID"],
                                UserID = (Guid)reader["UserID"],
                                QID = (Guid)reader["QID"],
                                UserTextAnswer = reader["UserTextAnswer"] as string,
                                UserQuestion = reader["Question"] as string,
                                QuestionAnswer = reader["QuestionAnswer"] as string,
                                UserName = reader["UserName"] as string
                            };
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


        #endregion
        #region "刪除"
        public bool DeleteQuestion(QuestionModel model)

        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" Update  question set	QDisplay = @QDisplay where QuestionID = @QuestionID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@QDisplay", model.QDisplay);

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

        public bool DeleteQuestion(Guid questionid)

        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" Update  question set	QDisplay = -1 where QuestionID = @QuestionID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QuestionID", questionid);

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
        public bool DeleteBaseQuestion(Guid QuestionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"Delete from BaseQuestion 
                 where	QuestionID = @QuestionID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", QuestionID);
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
        #region "修"
        //更新問題
        public bool UpdateQuestion(QuestionModel model)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Question
                    SET 
                        QQuestion = @QQuestion,
                        QAnswer = @QAnswer,
                        QIsNecessary = @QIsNecessary,
                        QQMode = @QQMode,
                        QCatrgory = @QCatrgory                           
                    WHERE
                        QID = @id  and
                        QNumber = @QNumber";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {


                        command.Parameters.AddWithValue("@id", model.QID);
                        command.Parameters.AddWithValue("@QQuestion", model.Question);
                        command.Parameters.AddWithValue("@QAnswer", model.Answer);
                        command.Parameters.AddWithValue("@QIsNecessary", model.QIsNecessary);
                        command.Parameters.AddWithValue("@QQMode", model.QQMode);
                        command.Parameters.AddWithValue("@QCatrgory", model.QCatrgory);
                        command.Parameters.AddWithValue("@QNumber", model.Number);
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


        public bool UpdateBaseQuestion(QuestionModel model)
        {

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" UPDATE BaseQuestion
                   SET 
                       QQuestion = @QQuestion,
                       QAnswer = @QAnswer,
                       QIsNecessary = @QIsNecessary,
                       QQMode = @QQMode

                   WHERE
                       QuestionID = @QuestionID";
            

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {                
                        command.Parameters.AddWithValue("@QAnswer", model.Answer);
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@QIsNecessary", model.QIsNecessary);
                        command.Parameters.AddWithValue("@QQMode", model.QQMode);
                        command.Parameters.AddWithValue("@QQuestion", model.Question);
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
        #region  "增"
        //創建問題
        public bool CreateQuestion(QuestionModel model)
        {
            model.QuestionID = Guid.NewGuid();
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" 
                    INSERT INTO Question
                        (QID, QuestionID, QQuestion, QAnswer, QIsNecessary ,QQMode, QCatrgory, QNumber,ANumber)
                    VALUES
                        (@QID, @QuestionID, @QQuestion, @QAnswer, @QIsNecessary, @QQMode , @QCatrgory,@QNumber,@ANumber);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {

                        command.Parameters.AddWithValue("@QID", model.QID);
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@QQuestion", model.Question);
                        command.Parameters.AddWithValue("@QAnswer", model.Answer);
                        command.Parameters.AddWithValue("@QIsNecessary", model.QIsNecessary);
                        command.Parameters.AddWithValue("@QQMode", model.QQMode);
                        command.Parameters.AddWithValue("@QCatrgory", model.QCatrgory);
                        command.Parameters.AddWithValue("@QNumber", model.QNumber);
                        command.Parameters.AddWithValue("@ANumber", model.QNumber);
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



        //創建常用問題
        public bool CreateBaseQuestion(QuestionModel model)
        {
            model.QuestionID = Guid.NewGuid();
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" 
                    INSERT INTO BaseQuestion
                        (QuestionID, QQuestion, QAnswer, QIsNecessary ,QQMode)
                    VALUES
                        (@QuestionID, @QQuestion, @QAnswer, @QIsNecessary, @QQMode);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {


                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@QQuestion", model.Question);
                        command.Parameters.AddWithValue("@QAnswer", model.Answer);
                        command.Parameters.AddWithValue("@QIsNecessary", model.QIsNecessary);
                        command.Parameters.AddWithValue("@QQMode", model.QQMode);



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





        #region "常用問題增刪修查"

        /// <summary>
        /// 常用問題管理拉出來放到dropdownlist內
        /// </summary>
        /// <returns></returns>
        public List<QuestionModel> GetDropDownListItem()
        {
            List<QuestionModel> list = new List<QuestionModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM BaseQuestion
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
                            QuestionModel model = new QuestionModel()
                            {
                                QuestionID = (Guid)reader["QuestionID"],
                                Question = reader["QQuestion"] as string,

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
        //常用問題專用 找出所有的常用問題
        public List<QuestionModel> GetAllBaseQuestion()
        {
            List<QuestionModel> list = new List<QuestionModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM BaseQuestion 
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
                            QuestionModel model = new QuestionModel()
                            {
                                Question = reader["QQuestion"] as string,
                                Answer = reader["QAnswer"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                QuestionID = (Guid)reader["QuestionID"]
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

        //找出一筆常用問題的資訊 
        public QuestionModel GetOneBaseQuestionInfo(Guid _questionID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
               @"   SELECT *
                    FROM BaseQuestion
                    WHERE QuestionID  =  @QuestionID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@QuestionID", _questionID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            QuestionModel model = new QuestionModel()
                            {

                                Question = reader["QQuestion"] as string,
                                Answer = reader["QAnswer"] as string,
                                QQMode = reader["QQMode"] as string,
                                QIsNecessary = reader["QIsNecessary"] as string,
                                QuestionID = (Guid)reader["QuestionID"],
                                QCatrgory = reader["QCatrgory"] as string

                            };
                            return model;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
        }
        #endregion
    }
}