using System.Data;
using System.Data.SqlClient;
namespace Eduhub_Repository_Console_Project
{
    public class EnrollmentRepository:IEnrollmentRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        private static DataSet ds;

        private static DataTable dt;

        public EnrollmentRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public DataSet GetEnrollmentsByEducatorId(int educatorId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = @"SELECT e.EnrollmentId, e.UserId, e.CourseId, e.EnrollmentDate, e.Status, u.Username
                FROM Enrollments e
                INNER JOIN Courses c ON e.CourseId = c.CourseId
                INNER JOIN Users u ON e.UserId = u.UserId
                WHERE c.UserId = @EducatorId";
                cmd.Parameters.AddWithValue("@EducatorId", educatorId);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch(SqlException)
            {
                Console.WriteLine("ex.Message");
                return null;
            }
        }
        public int EnrollInCourse(int enrollId, int userId, int courseId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO Enrollments (EnrollmentId, UserId, CourseId, EnrollmentDate, Status) " +
                                   "VALUES (@EnrollmentId, @UserId, @CourseId, GETDATE(), 'Pending')"; 
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return 0;
        }
        public DataSet GetEnrolledCoursesByStudentId(int studentId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT Courses.CourseId, Courses.Title, Courses.Description, Courses.CourseStartDate, Courses.CourseEndDate, Courses.Level " +
                                  "FROM Enrollments "+
                                  "INNER JOIN Courses ON Enrollments.CourseId = Courses.CourseId " +
                                  "WHERE Enrollments.UserId = @StudentId AND Enrollments.Status = 'Accepted'";
                cmd.Parameters.AddWithValue("@StudentId", studentId);
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