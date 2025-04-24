-- migrations.sql
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(MAX) NOT NULL,
    Password NVARCHAR(MAX) NOT NULL,
    Role INT NOT NULL
);

CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(MAX) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE TaskComments (
    Id INT PRIMARY KEY IDENTITY,
    Comment NVARCHAR(MAX) NOT NULL,
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

INSERT INTO Users (Id, Username, Password, Role) VALUES
(1, 'admin', 'admin123', 0),
(2, 'user', 'user123', 1);

INSERT INTO Tasks (Id, Title, Description, UserId) VALUES
(1, 'Task 1', 'First task', 2),
(2, 'Task 2', 'Second task', 2),
(3, 'Admin Task', 'Admin task', 1);

INSERT INTO TaskComments (Id, Comment, TaskId, UserId) VALUES
(1, 'Looks good', 1, 1);
