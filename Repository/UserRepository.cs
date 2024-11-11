using System.Data;
using System.Data.SqlClient;
using System.Windows.Markup;
using System.Xml;
namespace Eduhub_Repository_Console_Project

{
    internal class UserRepository:IUserRepository
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataAdapter da;
        private static DataSet ds;
        private static DataTable dt;
        public UserRepository()
        {
            string connect = "data source=(LocalDB)\\MSSQLLocaldb;initial catalog=EduhubDB;Integrated Security=true;TrustServerCertificate=true";
            con = new SqlConnection(connect);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public int AddUser(User newuser)
        {
            try
            {
                int newUserId = GetNewUserId() + 1;
                
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into Users(UserId, Username, Password, Email, MobileNumber, UserRole, ProfileImage) " + 
                                  "Values(@userId, @username, @password, @email, @mobile, @role, @profileimg)";
                cmd.Parameters.AddWithValue("@userId", newUserId);
                cmd.Parameters.AddWithValue("@username", newuser.Username);
                cmd.Parameters.AddWithValue("@password", newuser.Password);
                cmd.Parameters.AddWithValue("@email", newuser.Email);
                cmd.Parameters.AddWithValue("@mobile", newuser.MobileNumber);
                cmd.Parameters.AddWithValue("@role", newuser.UserRole);
                cmd.Parameters.AddWithValue("@profileimg", newuser.ProfileImage);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return 0;
        }
        public DataSet GetAllTeachers()
        {
            cmd.CommandText = "select UserId, Username, Email, MobileNumber, UserRole, ProfileImage " + 
                              "from Users where UserRole = 'Teacher'";
            da = new SqlDataAdapter(cmd);
            //da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet GetAllStudents()
        {
             cmd.CommandText = "select UserId, Username, Email, MobileNumber, UserRole, ProfileImage " + 
                                " from Users where UserRole = 'Student'";
            da = new SqlDataAdapter(cmd);
            //da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public bool UserExists(string username)
        {
            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            return count > 0;
        }
        public int GetNewUserId()
        {
            cmd.CommandText = "SELECT MAX(UserId) FROM Users";
            con.Open();
            int newUserId = (int)cmd.ExecuteScalar();
            con.Close();
            return newUserId;
        }
        public int UpdateProfile(User user)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "update Users set Email=@email,Password=@password,MobileNumber=@mobile,ProfileImage=@profileimg where Username=@username";
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@mobile", user.MobileNumber);
                cmd.Parameters.AddWithValue("@profileimg", user.ProfileImage);
                cmd.Parameters.AddWithValue("@username", user.Username);
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
        public User Login(string username, string password)
        {
            User? Loginuser = null;
            cmd.Parameters.Clear();
            cmd.CommandText= "select UserId, Username, Password, Email, MobileNumber, UserRole from Users where Username=@username AND Password=@password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            da = new SqlDataAdapter();
            ds = new DataSet();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(ds);
            dt = ds.Tables[0];

            if(dt.Rows.Count > 0)
            {
                Loginuser = new User();
                Loginuser.UserId = (int)dt.Rows[0]["UserId"];
                Loginuser.Username = dt.Rows[0]["Username"].ToString();
                Loginuser.Password = dt.Rows[0]["Password"].ToString();
                Loginuser.Email = dt.Rows[0]["Email"].ToString();
                Loginuser.MobileNumber = dt.Rows[0]["MobileNumber"].ToString();
                Loginuser.UserRole = dt.Rows[0]["UserRole"].ToString();
            }
            return Loginuser;
        }
    }
}
