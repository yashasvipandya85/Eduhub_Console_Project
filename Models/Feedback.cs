namespace Eduhub_Repository_Console_Project
{
    public class FeedBack
    {
        public int? FeedbackId{ get; set; }
        public int? UserId{ get; set; }
        public int? CourseId { get; set; }
        public string? StFeedback { get; set; }
        public DateTime Date { get; set; }
    }
}