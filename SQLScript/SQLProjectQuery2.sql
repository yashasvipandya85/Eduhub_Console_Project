use EduhubDB;
create table Enrollments(
EnrollmentId INT,
UserId INT,
CourseId INT,
EnrollmentDate datetime2(7),
Status varchar(30)
);

create table Materials(
MaterialId INT,
CourseId INT,
Title VARCHAR(30),
Description VARCHAR(30),
URL VARCHAR(30),
UploadDate datetime2(7),
ContentType VARCHAR(30)
);

create table Enquiry(
EnquiryId INT,
UserId INT,
CourseId INT,
Subject VARCHAR(30),
Message VARCHAR(30),
EnquiryDate datetime2(7),
Status VARCHAR(30),
Response VARCHAR(30)
);

create table Feedbacks(
FeedbackID INT,
UserId INT,
Feedback VARCHAR(50),
Date datetime2(7)
);