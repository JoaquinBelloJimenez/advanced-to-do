using Microsoft.AspNetCore.Mvc;
using TodoListApi.Models;
using TodoListApi.Repositories;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;

        public TasksController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserTask>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserTask>>> GetTaskById(int id)
        {
            var returnedTask = await _taskRepository.GetTaskByIdAsync(id);
            if(returnedTask == null)
            {
                return NotFound();
            }

            return Ok(returnedTask);
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask(UserTask task)
        {
            var taskId = await _taskRepository.CreateTaskAsync(task);
            task.Id = taskId;
            return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UserTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            await _taskRepository.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
