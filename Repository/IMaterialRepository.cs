using System.Data;

namespace Eduhub_Repository_Console_Project
{
    public interface IMaterialRepository
    {
        int UploadMaterial(int materialid, int courseId, string title, string description, string url, string contype);
        DataSet GetMaterialsByCourse(int courseId);
    }
}