using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> _tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Learn Docker", IsCompleted = false },
            new TaskItem { Id = 2, Title = "Learn Terraform", IsCompleted = false }
        };

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll() => Ok(_tasks);

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            task.Id = _tasks.Count + 1;
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, TaskItem updated)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            task.Title = updated.Title;
            task.IsCompleted = updated.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            _tasks.Remove(task);
            return NoContent();
        }
    }
}