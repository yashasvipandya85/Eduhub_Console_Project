using System;
using System.Data;
using System.Data.SqlClient;
namespace Eduhub_Repository_Console_Project
{
    internal class FeedbackRepository:IFeedbackRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        private static DataSet ds;

        public FeedbackRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public int AddFeedback(int userId, int courseId, string feedback)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO Feedback(UserId, CourseId, StFeedback, Date) " +
                                  "VALUES(@UserID, @CourseId, @StFeedback, @Date)";
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("@StFeedback", feedback);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        public DataSet GetFeedbackByEducatorId(int educatorId1)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT fb.FeedbackId, fb.UserId, fb.CourseId, fb.StFeedback, fb.Date, u.Username " +
                                  "FROM Feedback fb " +
                                  "INNER JOIN Courses c ON fb.CourseId = c.CourseId " +
                                  "INNER JOIN Users u ON fb.UserId = u.UserId " +
                                  "WHERE c.UserId = @EducatorId";

                cmd.Parameters.AddWithValue("@EducatorId", educatorId1);
               
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public DataSet GetFeedbackByStudentId(int studentId, int courseId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT FeedbackId, UserId, CourseId, StFeedback, Date FROM Feedback " +
                                  "WHERE UserId = @StudentId AND CourseId = @CourseId";
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }   
}