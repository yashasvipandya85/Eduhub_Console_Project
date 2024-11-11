using System.Data;
using System.Data.SqlClient;
namespace Eduhub_Repository_Console_Project
{
    public class EnquiryRepository:IEnquiryRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        private static DataSet ds;

        private static DataTable dt;

        public EnquiryRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con; 
        }
        public int AddEnquiry(Enquiry enquiry)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO Enquiry(UserId, CourseId, Subject, Message, EnquiryDate, Status) " +
                                   "VALUES(@UserId, @CourseId, @Subject, @Message, @EnquiryDate, @Status)";
                cmd.Parameters.AddWithValue("@UserId", enquiry.UserId);
                cmd.Parameters.AddWithValue("@CourseId", enquiry.CourseId);
                cmd.Parameters.AddWithValue("@Subject", enquiry.Subject);
                cmd.Parameters.AddWithValue("@Message", enquiry.Message);
                cmd.Parameters.AddWithValue("@EnquiryDate", enquiry.EnquiryDate);
                cmd.Parameters.AddWithValue("@Status", enquiry.Status);

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
        public DataSet GetEnquiriesByCourseId(int courseId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT EnquiryId, UserId, CourseId, Subject, Message, EnquiryDate, Status, Response " +
                                  "FROM Enquiry WHERE CourseId = @CourseId";
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public DataSet GetStudentEnquiries(int userId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT EnquiryId, UserId, CourseId, Subject, Message, EnquiryDate, Status, Response " +
                                  "FROM Enquiry WHERE UserId = @UserId";
                cmd.Parameters.AddWithValue("@UserId", userId);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public int RespondToEnquiry(int enquiryId, string response)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE Enquiry SET Response = @Response, Status = 'Closed' WHERE EnquiryId = @EnquiryId";
                cmd.Parameters.AddWithValue("@Response", response);
                cmd.Parameters.AddWithValue("@EnquiryId", enquiryId);
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
    }
}