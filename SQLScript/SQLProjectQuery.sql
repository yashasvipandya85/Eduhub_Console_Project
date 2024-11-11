create database EduhubDB;
use EduhubDB;
create table Users(
UserId INT PRIMARY KEY,
Email varchar(30),
Password varchar(30),
Username varchar(30),
MobileNumber varchar(30),
UserRole varchar(30),
ProfileImage varchar(30)
);

INSERT INTO Users (UserId, Email, Password, Username, MobileNumber, UserRole, ProfileImage) VALUES
(1, 'Muskan@example.com', 'password123', 'Muskan', '1234567890', 'Teacher', 'Muskan.jpg'),
(2, 'Keshav@example.com', 'password456', 'Keshav', '0987654321', 'Student', 'Keshav.jpg'),
(3, 'Mohnish@example.com', 'password789', 'Mohnish', '1122334455', 'Teacher', 'Mohnish.jpg'),
(4, 'Nishu@example.com', 'password321', 'Nishu', '5566778899', 'Student', 'Nishu.jpg'),
(5, 'Gungun@example.com', 'password654', 'Gungun', '6677889900', 'Teacher', 'Gungun.jpg'),
(6, 'Ayushman@example.com', 'password987', 'Ayushman', '7788990011', 'Student', 'Ayushman.jpg'),
(7, 'Krishna@example.com', 'password111', 'Krishna', '8899001122', 'Teacher', 'Krishna.jpg');

select * from Users;


create table Courses(
CourseId INT,
Title varchar(30),
Description varchar(30),
CourseStartDate datetime2(7),
CourseEndDate datetime2(7),
UserId int,
Category varchar(30),
Level varchar(30)
);