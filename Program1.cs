using System.Collections;
// using System.Data;

// namespace Eduhub_Repository_Console_Project
// {
//     internal class Program1
//     {
//         static void Main(string[] args)
//         {
//             UserRepository Repo = new UserRepository();
//             User user;
//             int ID;
//             int result;
//             DataSet ds = new DataSet();
//             int entry;
//             bool Continue = true;
//             char reply;
//             Console.WriteLine("...........................");
//             while(Continue)
//         {
//             Console.WriteLine("Press 0 to Exit\nPress 1 For All teacher\nPress 2 For All Students\nPress 3 Add User\nPress 4 Update Profile");
//             entry = Convert.ToInt32(Console.ReadLine());
//             switch(entry)
//             {
//                 case 0:
//                     Environment.Exit(0);
//                     break;
//                 case 1:
//                     Console.WriteLine(".....Show all Teachers.....");
//                     ds = Repo.GetAllTeachers();
//                     foreach (DataRow row in ds.Tables[0].Rows)
//                     {
//                         Console.WriteLine($"Id:{row["userId"]} - Teacher name:{row["Username"]}");
//                     } 
//                     break;
//                 case 2:
//                     Console.WriteLine(".....Show all Students.....");
//                     ds = Repo.GetAllStudents();
//                     foreach (DataRow row in ds.Tables[0].Rows)
//                     {
//                         Console.WriteLine($"Id:{row["userId"]} - Student name:{row["Username"]}");
//                     } 
//                     break;   
//                 case 3:
//                     Console.WriteLine(".....Add New User.....");
//                     user = new User();
//                     Console.WriteLine("To add new user enter username");
//                     Console.WriteLine("Enter username: ");
//                     string name = Console.ReadLine();
//                     bool res = Repo.UserExists(name);
//                     if (res)
//                     {
//                         Console.WriteLine("Username already exits");
//                     }
//                     else
//                     {
//                         user.Username = name;
//                         Console.WriteLine("Enter Password");
//                         user.Password = Console.ReadLine();
//                         Console.WriteLine("Enter Email");
//                         user.Email = Console.ReadLine();
//                         Console.WriteLine("Enter Mobile Number");
//                         user.MobileNumber = Console.ReadLine();
//                         Console.WriteLine("Enter User Role");
//                         user.UserRole = Console.ReadLine();
//                         Console.WriteLine("Enter Profile Image");
//                         user.ProfileImage = Console.ReadLine();
//                         result = Repo.AddUser(user);
//                         if(result > 0)
//                         {
//                             Console.WriteLine("Record added successfully");
//                             ID = Repo.GetNewUserId();
//                             Console.WriteLine("Please note your user id is: " + ID);
//                         }
//                     }   
//                     break;
//                     case 4:
//                         Console.WriteLine(".....To update Profile of user.....");
//                         user = new User();
//                         Console.WriteLine("To update user enter username");
//                         Console.WriteLine("Enter username");
//                         user.Username = Console.ReadLine();
//                         Console.WriteLine("Enter Email");
//                         user.Email = Console.ReadLine();
//                         Console.WriteLine("Enter Password");
//                         user.Password = Console.ReadLine();
//                         Console.WriteLine("Enter Mobile Number");
//                         user.MobileNumber= Console.ReadLine();
//                         Console.WriteLine("Enter Profile Image");
//                         user.ProfileImage= Console.ReadLine();
//                         result = Repo.UpdateProfile(user);
//                         if(result > 0)
//                         {
//                             Console.WriteLine("User Profile Updated successfully");
//                         }
//                         else
//                         {
//                             Console.WriteLine("User Profile not updated");
//                         }
//                     break;
//                     default:
//                         Console.WriteLine("Invalid key pressed try again");
//                         break;
//                 }
//                 Console.WriteLine("Do you want to Continue? Y/N");
//                 reply = Convert.ToChar(Console.ReadLine());
//                 if(reply == 'Y')
//                 {
//                     Continue = true;
//                 }
//                 else if(reply == 'N')
//                 {
//                     Environment.Exit(0);
//                 }
//             }
//         }
//         }
        
//     }

