using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Travelore.Model;
using Task = Travelore.Model.Task;

namespace Travelore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepo = taskRepository;
        }

        [HttpGet]
        public IEnumerable<Task> GetTasks()
        {
            return _taskRepo.GetTasks();
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            return _taskRepo.GetByTaskId(id);
        }

        [HttpPost]
        public ActionResult NewTask(Task t)
        {
            Task task = new Task()
            {
                Description = t.Description,
                EndDate = t.EndDate,
                DoneTask = false,
            };
            _taskRepo.Add(task);
            _taskRepo.SaveChanges();
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            Task taskDel = _taskRepo.GetByTaskId(id);
            _taskRepo.Delete(taskDel);
            _taskRepo.SaveChanges();
            return NoContent();
        }
    }
}