using System.Data;
namespace Eduhub_Repository_Console_Project
{
    internal interface IUserRepository
    {
        DataSet GetAllTeachers();
        DataSet GetAllStudents();
        int UpdateProfile(User user);
        bool UserExists(string username);
        int AddUser(User newuser);
        int GetNewUserId();
        User Login(string username, string password);
    }
}