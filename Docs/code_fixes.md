# TaskService Code Fixes

## Issues Identified
1. **Generic DbContext**:
   - Used `DbContext` instead of `TaskDbContext`, which lacks the `Tasks` DbSet.
   - Fixed by using `TaskDbContext` to access the `Tasks` DbSet properly.
2. **Incorrect Return Type in `GetAllTasks`**:
   - Returned `List<Task>` but called `ToListAsync`, which returns `Task<List<Task>>`.
   - Fixed by changing the return type to `Task<List<Task>>`.
3. **Missing Async/Await**:
   - `GetAllTasks` didn't await `ToListAsync`, causing a runtime error.
   - Fixed by adding `await` and marking the method as `async`.
4. **Missing Error Handling (Bonus)**:
   - `GetTask` didn't validate the `id` parameter.
   - Added check for `id <= 0` and threw `ArgumentException`.

## Fixed Code
See `Services/TaskService.cs` for the corrected implementation.