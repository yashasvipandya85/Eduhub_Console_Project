use EduhubDB;
create table Feedback(
FeedbackId INT,
UserId INT,
CourseId INT,
StFeedback VARCHAR(50),
Date datetime2(7)
);
select * from Feedback;

INSERT INTO Feedback (FeedbackID, UserId, StFeedback, Date, CourseId)
VALUES
(1, 2, 'Great introduction to basic math concepts!', '2024-03-11', 1),
(2, 4, 'Very detailed and comprehensive grammar course', '2024-06-02', 3),
(3, 6, 'Enjoyed the practical sessions in chemistry', '2024-08-11', 5),
(4, 8, 'Programming fundamentals were well explained', '2024-10-21', 7),
(5, 9, 'Interesting insights into economic principles', '2024-11-26', 8);