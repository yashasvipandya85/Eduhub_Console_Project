using System.Data;
namespace Eduhub_Repository_Console_Project
{
    public interface IEnquiryRepository
    {
        int AddEnquiry(Enquiry enquiry);
        DataSet GetEnquiriesByCourseId(int courseId);
        DataSet GetStudentEnquiries(int userId);
    }
}