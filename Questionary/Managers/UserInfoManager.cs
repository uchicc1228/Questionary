using Questionary.Helper;
using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Questionary.Managers
{
    public class UserInfoManager
    {
        #region "刪除"
        #endregion
        #region "修"
        #endregion
        #region  "增"
        #endregion
        public List<UserInfoModel> GetAllWriter(Guid QuestionaryID)
        {
            List<UserInfoModel> list = new List<UserInfoModel>();

            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM UserManager
                    WHERE QID = @QuestionaryID
                    ORDER BY Number DESC  ";
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
                                QID = (Guid)reader["QID"],
                                Number = (int)reader["Number"],
                                UserName = reader["UserName"] as string,
                                UserWriteTime = (DateTime)reader["UserWriteTime"],
                                UserID = (Guid)reader["UserID"],
                                QuestionID = (Guid)reader["QuestionID"],
                                UserAge = reader["UserAge"] as string,
                                UserEmail = reader["UserEmail"] as  string,

                            };
                            model.UserWriteTime_string = model.UserWriteTime.ToString("yyyy-MM-dd");
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

        public UserInfoModel GetOneQuestionInfo(Guid UserID)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @" SELECT  *
                   FROM UserManager
                   WHERE [UserID] = @UserID";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            UserInfoModel model = new UserInfoModel()
                            {
                                QID = (Guid)reader["QID"],
                                UserID = (Guid)reader["UserID"],
                                UserName = reader["UserName"] as string,
                                UserAge = reader["UserAge"] as string,
                                UserEmail = reader["UserEmail"] as string,
                                UserPhone = reader["UserPhone"] as string,
                                UserWriteTime = (DateTime)reader["UserWriteTime"],
                                Number = (int)reader["Number"],
                                
                            }; 
                            model.UserWriteTime_string = model.UserWriteTime.ToString("yyyy-MM-dd");
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