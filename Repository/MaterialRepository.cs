using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Eduhub_Repository_Console_Project
{
    internal class MaterialRepository:IMaterialRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static DataSet ds;
        private static DataAdapter da;
        private static DataTable dt;
        public MaterialRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public int UploadMaterial(int materialid, int courseId, string title, string description, string url, string contype)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO Materials(MaterialId, CourseId, Title, Description, Url, UploadDate, ContentType) VALUES(@MaterialId, @CourseId, @Title, @Description, @Url, @UploadDate, @ContentType)";
                cmd.Parameters.AddWithValue("@MaterialId", materialid);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.Parameters.AddWithValue("Title", title);
                cmd.Parameters.AddWithValue("Description", description) ;
                cmd.Parameters.AddWithValue("@Url", url);
                cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ContentType", contype);
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
        public DataSet GetMaterialsByCourse(int courseId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText="SELECT MaterialId, Title, Url, UploadDate FROM Materials WHERE CourseId = @CourseId";
                cmd.Parameters.AddWithValue("CourseId", courseId);
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
