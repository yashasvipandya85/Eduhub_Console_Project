using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
namespace Eduhub_Repository_Console_Project
{
    internal class CourseRepository:ICourseRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        private static DataSet ds;

        private static DataTable dt;

        public CourseRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public DataSet GetAllCourses()
        {
            cmd.Parameters.Clear();
             cmd.CommandText = "select CourseId, Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level from Courses" ;
                              
            da = new SqlDataAdapter(cmd);
            //da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetCourseByID(int ID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select CourseId, Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level from Courses where CourseId=@CourseId";
                cmd.Parameters.AddWithValue("@CourseId", ID);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch(SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message);
                return null;
            }
        }
        public Course GetCourseByUser(int userId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select CourseId, Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level from Courses where UserId=@UserId";
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    return new Course
                    {
                        CourseId = Convert.ToInt32(reader["CourseId"]),
                        Title = reader["Title"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        CourseStartDate = Convert.ToDateTime(reader["CourseStartDate"]),
                        CourseEndDate = Convert.ToDateTime(reader["CourseEndDate"]),
                        UserId = Convert.ToInt32(reader["userId"]),
                        Category = reader["Category"]?.ToString(),
                        Level = reader["Level"]?.ToString()
                    };
                }
                return null;
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public int AddCourse(Course course)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Courses(CourseId, Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level) " + 
                                  "Values(@courseId, @title, @description, @startdate, @enddate, @userId, @category, @level)";
                cmd.Parameters.AddWithValue("@courseId", course.CourseId);
                cmd.Parameters.AddWithValue("@title", course.Title);
                cmd.Parameters.AddWithValue("@description", course.Description);
                cmd.Parameters.AddWithValue("@startdate", course.CourseStartDate);
                cmd.Parameters.AddWithValue("@enddate", course.CourseEndDate);
                cmd.Parameters.AddWithValue("@userId", course.UserId);
                cmd.Parameters.AddWithValue("@category", course.Category);
                cmd.Parameters.AddWithValue("@level", course.Level);
                con.Open();
                int result1 = cmd.ExecuteNonQuery();
                con.Close();
                return result1;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return 0;
        }
        public int UpdateCourse(Course course)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update Courses set Title=@title,Description=@description,CourseStartDate=@startdate,CourseEndDate=@enddate,UserId=@userId,Category=@category,Level=@level where CourseId=@CourseId";
                cmd.Parameters.AddWithValue("@CourseId",course.CourseId);
                cmd.Parameters.AddWithValue("@title", course.Title);
                cmd.Parameters.AddWithValue("@description", course.Description);
                cmd.Parameters.AddWithValue("@startdate", course.CourseStartDate);
                cmd.Parameters.AddWithValue("@enddate", course.CourseEndDate);
                cmd.Parameters.AddWithValue("@userId", course.UserId);
                cmd.Parameters.AddWithValue("@category", course.Category);
                cmd.Parameters.AddWithValue("@level", course.Level);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
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
        
    }   
}     
        