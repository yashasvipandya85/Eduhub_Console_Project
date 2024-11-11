using System.Collections;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml.XPath;
namespace Eduhub_Repository_Console_Project
{
    internal class Program
    {
        public static void Main()
        {
            UserRepository userRepository = new UserRepository();
            Console.WriteLine(".....Login Page.....");
            Console.WriteLine("Enter Username");
            string? username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string? password = Console.ReadLine();
            User user = userRepository.Login(username, password);
            string role = string.Empty;
            if(user != null)
            {
                Console.WriteLine($"User exists\n{user.Username}-{user.Password} - {user.Email}-{user.MobileNumber}");
                role = user.UserRole;
                if(role == "Teacher")
                {
                    Console.WriteLine(".........Educator Dashboard.........");
                    EducatorDashboard();
                }
                else
                {
                    Console.WriteLine(".........Student Dashboard.........");
                    StudentDashboard(user.UserId);
                }
            }
            else 
            Console.WriteLine("User does not exist");
        }
        public static void EducatorDashboard()
        {
            CourseRepository courseobj = new CourseRepository();
            EnrollmentRepository enrollmentRepo = new EnrollmentRepository();
            FeedbackRepository feedbackRepo = new FeedbackRepository();
            int menu;
            
            Console.WriteLine("Press 1 For AllCourses\nPress 2 For MyCourse\nPress 3 Add New Course\nPress 4 Enrollment\nPress 5 Enquiry\nPress 6 Material\nPress 7 FeedBack");
            Console.Write("Enter a choice: ");
            menu = Convert.ToInt32(Console.ReadLine());
            switch(menu)
            {
                case 1:
                Console.WriteLine(".....Display all courses.....");
                DataSet allcourses = courseobj.GetAllCourses();
                if(allcourses != null)
                {
                    foreach (DataRow row in allcourses.Tables[0].Rows)
                    {
                        Console.WriteLine($"{row["CourseId"]}: {row["Title"]} - {row["Description"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No Course available");
                }
                    
                break;
                case 2:
                Console.WriteLine(".....Display MyCourses.....");
                Console.WriteLine("Enter your User ID");
                int userId = Convert.ToInt32(Console.ReadLine());
                Course result = courseobj.GetCourseByUser(userId);
                if(result != null)
                {
                    Console.WriteLine($"{result.CourseId}: {result.Title} - {result.Description}");
                }
                else
                {
                    Console.WriteLine("No course found for this user");
                }
                break;
                case 3:
                Console.WriteLine(".....Add New Course.....");
                Course newCourse = new Course();
                Console.WriteLine("Enter Course Title");
                newCourse.Title = Console.ReadLine();

                Console.WriteLine("Enter Course Description");
                newCourse.Description = Console.ReadLine();

                Console.WriteLine("Enter Course Start Date (yyyy-mm-dd)");
                newCourse.CourseStartDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Course End Date");
                newCourse.CourseEndDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter your User ID");
                newCourse.UserId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Course Category");
                newCourse.Category = Console.ReadLine();

                Console.WriteLine("Enter Course Level");
                newCourse.Level = Console.ReadLine();

                int result1 = courseobj.AddCourse(newCourse);
                if(result1 != 0)
                {
                    Console.WriteLine("Course added successfully");
                }
                else
                {
                    Console.WriteLine("Failed to add course");
                }
                break;
                case 4:
                Console.WriteLine(".....View Enrollments.....");
                Console.WriteLine("Enter your Educator User ID");
                int educatorId = Convert.ToInt32(Console.ReadLine());
                DataSet enrollmentData = enrollmentRepo.GetEnrollmentsByEducatorId(educatorId);
                if(enrollmentData != null && enrollmentData.Tables.Count > 0 && enrollmentData.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow row in enrollmentData.Tables[0].Rows)
                    {
                        Console.WriteLine($"Enrollment ID: {row["EnrollmentId"]}, StudentId: {row["UserId"]} CourseId: {row["CourseId"]}, " +
                        $"Date: {row["EnrollmentDate"]}, Status: {row["Status"]}, Student Name: {row["Username"]}");                                                                                 
                    }
                }
                else
                {
                    Console.WriteLine("No enrollment found for your courses");
                }
                break;
                case 5:
                Console.WriteLine(".....Enquiry.....");
                Console.WriteLine("1. View Enquiries");
                Console.WriteLine("2. Respond to Enquiries");
                Console.WriteLine("Enter your choice");
                EnquiryRepository enquiryRepo = new EnquiryRepository();

                int enquiryChoice = Convert.ToInt32(Console.ReadLine());
                switch(enquiryChoice)
                {
                    case 1:
                    Console.WriteLine("....View Enquiries....");
                    Console.WriteLine("Enter User ID");
                    int educatorId2 = Convert.ToInt32(Console.ReadLine());
                    
                    DataSet enquiries = enquiryRepo.GetEnquiriesByCourseId(educatorId2);
                    if(enquiries != null && enquiries.Tables[0].Rows.Count > 0)
                    {
                        Console.WriteLine("Enquiries related to your courses");
                        foreach(DataRow row in enquiries.Tables[0].Rows)
                        {
                            Console.WriteLine($"Enquiry Id : {row["EnquiryId"]} Course ID : {row["CourseId"]} " +
                            $"Message : {row["Message"]} Date : {row["EnquiryDate"]} Status : {row["Status"]} Response : {row["Response"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No enquiries found for your course");
                    }
                    break;
                    case 2:
                    Console.WriteLine("Enter the Enquiry Id you want to respond to");
                    int enquiryId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter your response");
                    string response = Console.ReadLine();
                    Console.WriteLine("Enter your response");
                    int result3 = enquiryRepo.RespondToEnquiry(enquiryId, response);
                    if(result3 > 0)
                    {
                        Console.WriteLine("Response submitted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed to submit the response");
                    }
                    break;
                    default:
                    Console.WriteLine("Invalid choice");
                    break;
                }
                break;
                case 6:
                Console.WriteLine(".....Upload Material.....");
                Console.WriteLine("Enter Material Id");
                int materialid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Course Id");
                int courseId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Material title");
                string title = Console.ReadLine();
                Console.WriteLine("Enter Description");
                string description = Console.ReadLine();
                Console.WriteLine("Enter Material URL: ");
                string url = Console.ReadLine();
                Console.WriteLine("Enter Content Type");
                string contype = Console.ReadLine();
                MaterialRepository materialRepo = new MaterialRepository();
                int result4 = materialRepo.UploadMaterial(materialid, courseId, title, description, url, contype);
                if(result4 > 0)
                {
                    Console.WriteLine("Material Uploaded successfully");
                }
                else
                {
                    Console.WriteLine("Failed to upload material");
                }
                break;
                case 7:
                Console.WriteLine("......Feedback......");
                Console.WriteLine("Enter your User ID: ");
                int educatorId1 = Convert.ToInt32(Console.ReadLine());
                DataSet feedbackdata = feedbackRepo.GetFeedbackByEducatorId(educatorId1);
                if(feedbackdata != null && feedbackdata.Tables.Count > 0 && feedbackdata.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("Feedback for your courses");
                    foreach(DataRow row in feedbackdata.Tables[0].Rows)
                    {
                        Console.WriteLine($"FeedbackId: {row["FeedbackId"]} StudentId: {row["UserId"]} CourseId: {row["CourseId"]}, " +
                        $"Feedback: {row["StFeedback"]} Date: {row["Date"]} Student Name: {row["Username"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No feedback found for your courses");
                }
                break;
            }  
        }
        public static void StudentDashboard(int studentId)
        {
            CourseRepository courseobj = new CourseRepository();
            EnrollmentRepository enrollmentRepo = new EnrollmentRepository();
            FeedbackRepository feedbackRepo = new FeedbackRepository();
            int menu;
            Console.WriteLine("Press 1 To View Available Courses\nPress 2 To Enroll in a Course\nPress 3 Submit Enquiry\nPress 4 View My Courses\nPress 5 View Material\nPress 6 Feedback");
            Console.Write("Enter a choice: ");
            menu = Convert.ToInt32(Console.ReadLine());
            switch(menu)
            {
                case 1:
                Console.WriteLine(".....Display Courses.....");
                
                DataSet allCourses = courseobj.GetAllCourses();
                if(allCourses != null)
                {
                    foreach(DataRow row in allCourses.Tables[0].Rows)
                    {
                        Console.Write($"Course Id : {row["CourseId"]} Title : {row["Title"]} Description : {row["Description"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No courses available");
                }
                break;
                case 2:
                Console.WriteLine("......Enroll in Course......");
                Console.WriteLine("Enter Student user id");
                int userId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Course Id to enroll");
                int courseId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Enrollment Id");
                int enrollId = Convert.ToInt32(Console.ReadLine());
                int enrollments = enrollmentRepo.EnrollInCourse(courseId, userId, enrollId);
                if(enrollments > 0)
                {
                    Console.WriteLine("Enrollment request submitted successfully");
                }
                else
                {
                    Console.WriteLine("Failed to submit enrollment request");
                }
                break;
                case 3:
                Console.WriteLine("......Enquiry......");
                Console.WriteLine("1. View my enquiries");
                Console.WriteLine("2. Submit a new enquiry");
                Console.WriteLine("Enter your choice");
                int stdEnquiryChoice = Convert.ToInt32(Console.ReadLine());
                EnquiryRepository enquiryRepo = new EnquiryRepository();
                switch(stdEnquiryChoice)
                {
                    case 1:
                    Console.WriteLine("Enter your Student ID: ");
                    int studentId1 = Convert.ToInt32(Console.ReadLine());
                    DataSet stdEnquiries = enquiryRepo.GetStudentEnquiries(studentId1);
                    if(stdEnquiries != null && stdEnquiries.Tables[0].Rows.Count > 0)
                    {
                        Console.WriteLine("Your enquires");
                        foreach(DataRow row in stdEnquiries.Tables[0].Rows)
                        {
                            Console.WriteLine($"Enquiry Id : {row["EnquiryId"]} UserId : {row["UserId"]} Course ID : {row["CourseId"]} " +
                            $"Subject : {row["Subject"]} Message : {row["Message"]} Date : {row["EnquiryDate"]} Status : {row["Status"]} Response : {row["Response"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have no enquiries");
                    }
                    break;
                    case 2:
                    Console.WriteLine("Submit a new query");
                    Console.WriteLine("Enter your Student ID: ");
                    int newStudentId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the Course ID");
                    int newcourseId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Subject");
                    string newSubject = Console.ReadLine();
                    Console.WriteLine("Enter your message");
                    string message = Console.ReadLine();
                    Enquiry newEnquiry = new Enquiry
                    {
                        UserId = newStudentId,
                        CourseId = newcourseId,
                        Subject = newSubject,
                        Message = message,
                        EnquiryDate = DateTime.Now,
                        Status = "Open"
                    };
                    int addresult = enquiryRepo.AddEnquiry(newEnquiry);
                    if(addresult > 0)
                    {
                        Console.WriteLine("Enquiry Submitted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed to submit enquiry");
                    }
                    break;
                    default:
                    Console.WriteLine("Invalid Choice");
                    break;
                }
                break;
                case 4:
                Console.WriteLine("......My Courses......");
                Console.WriteLine("Enter your Student ID");
                int studentId2 = Convert.ToInt32(Console.ReadLine());
                EnrollmentRepository enrollRepo = new EnrollmentRepository();
                DataSet myCourses = enrollRepo.GetEnrolledCoursesByStudentId(studentId2);
                if(myCourses != null && myCourses.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("You are enrolled in follwing courses: ");
                    foreach(DataRow row in myCourses.Tables[0].Rows)
                    {
                        Console.WriteLine($"Course ID: {row["CourseId"]} Title: {row["Title"]} Description: {row["Description"]} " +
                        $"Start date: {row["CourseStartDate"]} End Date: {row["CourseEndDate"]} Level: {row["Level"]} ");
                    }
                }
                break;
                
                case 5:
                Console.WriteLine("......View Material......");
                Console.WriteLine("Enter CourseId");
                int courseId1 = Convert.ToInt32(Console.ReadLine());
                MaterialRepository materialRepo = new MaterialRepository();
                DataSet materials = materialRepo.GetMaterialsByCourse(courseId1);
                if(materials != null && materials.Tables.Count > 0 && materials.Tables[0].Rows.Count > 0)
                {
                    Console.WriteLine("Course Materials");
                    foreach(DataRow row in materials.Tables[0].Rows)
                    {
                        Console.WriteLine($"Title: {row["Title"]} URL: {row["Url"]} Uploaded on: {row["UploadDate"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No materials find for this code");
                }
                break;
                case 6:
                Console.WriteLine("......Feedback......");
                Console.WriteLine("1. View Feedback for a course");
                Console.WriteLine("2. Post Feedback");
                Console.WriteLine("Select an option");
                int feedbackChoice = Convert.ToInt32(Console.ReadLine());
                if(feedbackChoice == 1)
                {
                    Console.WriteLine("Enter your student user id");
                    int studentid = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter CourseId");
                    int courseid = Convert.ToInt32(Console.ReadLine());
                    DataSet feedbacks = feedbackRepo.GetFeedbackByStudentId(studentid, courseid);
                    if(feedbacks != null && feedbacks.Tables.Count > 0 && feedbacks.Tables[0].Rows.Count > 0)
                    {
                        foreach(DataRow row in feedbacks.Tables[0].Rows)
                        {
                            Console.WriteLine($"FeedbackId: {row["FeedbackId"]} Feedback: {row["StFeedback"]} Date: {row["Date"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No feedback found for this course");
                    }

                }
                else if(feedbackChoice == 2)
                {
                    Console.WriteLine("Enter your student user id");
                    int studentidForPost = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter CourseId");
                    int courseidForPost = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter your feedback: ");
                    string feedback = Console.ReadLine();
                    int result = feedbackRepo.AddFeedback(studentidForPost, courseidForPost, feedback);
                    if(result > 0)
                    {
                        Console.WriteLine("Feedback submitted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed to submit feedback");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid Option");
                }
                break;
                default:
                Console.WriteLine("Invalid Option. Please try again");
                break;
            }
            
        }
        
    }
}
   
