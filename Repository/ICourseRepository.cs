using System.Data;
namespace Eduhub_Repository_Console_Project
{
    internal interface ICourseRepository
    {
        DataSet GetAllCourses();
        DataSet GetCourseByID(int ID);
        Course GetCourseByUser(int userId);
        int AddCourse(Course course);
        int UpdateCourse(Course course);
        //DataSet GetEnrollmentsByEducatorId(int educatorId);
    }
}