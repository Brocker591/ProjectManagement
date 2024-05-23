using Task.Api.Exceptions;

namespace Task.Api.Repositories
{
    public class WorkTaskRepository(WorkTaskContext dbContect)
    {
        public async Task<WorkTask> CreateWorkTask(WorkTask workTask, CancellationToken cancellationToken = default)
        {
            workTask.Id = Guid.NewGuid();
            dbContect.WorkTasks.Add(workTask);
            await dbContect.SaveChangesAsync(cancellationToken);

            return workTask;
        }

        public async Task<List<WorkTask>> GetWorkTasks(CancellationToken cancellationToken = default)
        {
            var worktasks = await dbContect.WorkTasks.ToListAsync(cancellationToken);
            return worktasks;
        }

        public async Task<WorkTask> GetWorkTask(Guid workTaskId, CancellationToken cancellationToken = default)
        {
            var worktask = await dbContect.WorkTasks.FirstOrDefaultAsync(x => x.Id == workTaskId);
            if(worktask == null)
                throw new WorkTaskNotFoundException(workTaskId);

            return worktask;
        }
    }
}
