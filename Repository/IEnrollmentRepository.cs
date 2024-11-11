using System.Data;
namespace Eduhub_Repository_Console_Project
{
    public interface IEnrollmentRepository
    {
        DataSet GetEnrollmentsByEducatorId(int educatorId);
        int EnrollInCourse(int enrollId, int userId, int courseId);
        DataSet GetEnrolledCoursesByStudentId(int studentId);
    }
}