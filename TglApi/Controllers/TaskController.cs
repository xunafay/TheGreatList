using Microsoft.AspNetCore.Mvc;
using TglApi.Services;
using TglApi.Models;

namespace TglApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;
    private readonly ITasksRepo _tasksRepo;

    public TasksController(ITasksRepo tasksRepo, ILogger<TasksController> logger)
    {
        _tasksRepo = tasksRepo;
        _logger = logger;
    }


    public IEnumerable<Models.Task> Get()
    {
        return _tasksRepo.GetTasks();
    }

    [HttpGet("{id}")]
    public Models.Task Get(int id)
    {
        return _tasksRepo.GetTask(id);
    }

    [HttpPost]
    public Models.Task Post(Models.Task task)
    {
        return _tasksRepo.AddTask(task);
    }

    [HttpPut]
    public Models.Task Put(Models.Task task)
    {
        return _tasksRepo.UpdateTask(task);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _tasksRepo.DeleteTask(id);
    }
}
