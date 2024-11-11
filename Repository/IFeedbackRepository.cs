using System.Data;
namespace Eduhub_Repository_Console_Project
{
    public interface IFeedbackRepository
    {
        int AddFeedback(int userId, int courseId, string feedback);
        DataSet GetFeedbackByEducatorId(int educatorId1);
        DataSet GetFeedbackByStudentId(int studentId, int courseId);
    }
}