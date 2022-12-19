namespace TglApi.Services;

public interface ITasksRepo
{
    IEnumerable<Models.Task> GetTasks();
    Models.Task GetTask(int id);
    Models.Task AddTask(Models.Task task);
    Models.Task UpdateTask(Models.Task task);
    void DeleteTask(int id);
}

public class TasksRepo : ITasksRepo
{
    private readonly TglApiDbContext _dbContext;

    public TasksRepo(TglApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Models.Task> GetTasks()
    {
        return _dbContext.Tasks;
    }

    public Models.Task GetTask(int id)
    {
        return _dbContext.Tasks.Find(id);
    }

    public Models.Task AddTask(Models.Task task)
    {
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
        return task;
    }

    public Models.Task UpdateTask(Models.Task task)
    {
        _dbContext.Tasks.Update(task);
        _dbContext.SaveChanges();
        return task;
    }

    public void DeleteTask(int id)
    {
        var task = _dbContext.Tasks.Find(id);
        _dbContext.Tasks.Remove(task);
        _dbContext.SaveChanges();
    }
}