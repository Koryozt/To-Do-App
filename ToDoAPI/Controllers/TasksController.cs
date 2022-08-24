using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TasksController(ToDoContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasks()
        {
            try
            {
                if(_context.Tasks == null)
                    return NotFound();

                return await _context.Tasks.ToListAsync();
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("by_priority")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasksByPriority()
        {
            try
            {
                if (_context.Tasks == null)
                    return NotFound();
                List<Tasks> TaskList = await _context.Tasks.ToListAsync(); 
                return TaskList.OrderByDescending(TaskSet => TaskSet.Priority).ToList();
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetSingleTask(int id)
        {
            try
            {
                if (_context.Tasks == null)
                    return NotFound();

                Tasks? Task = await _context.Tasks.FindAsync(id);

                if (Task is null)
                    return NotFound();

                return Task;
            }
            catch(Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        } 

        [HttpPost]
        public async Task<IActionResult> CreateNewTask([FromBody] Tasks NewTask)
        {
            try
            {
                await _context.AddAsync(NewTask);
                await _context.SaveChangesAsync();

                return Ok(NewTask);
            }
            catch (System.Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] Tasks EditedTask)
        {
            if (id != EditedTask.TaskID)
            {
                return BadRequest();
            }

            try
            {
				_context.Update(EditedTask);
				await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }

            Tasks? Task = await _context.Tasks.FindAsync(id);
            
            if (Task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(Task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int ID)
        {
            if (_context.Tasks == null)
                return false;

            return _context.Tasks.Any(e => e.TaskID == ID);
        }
    }
}
