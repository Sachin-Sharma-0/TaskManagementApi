-- Query 1: Get all tasks assigned to a user
SELECT t.Id, t.Title, t.Description, u.Username
FROM Tasks t
JOIN Users u ON t.UserId = u.Id
WHERE t.UserId = 2;

-- Query 2: Get all comments on a task
SELECT tc.Id, tc.Comment, u.Username
FROM TaskComments tc
JOIN Users u ON tc.UserId = u.Id
WHERE tc.TaskId = 1;
