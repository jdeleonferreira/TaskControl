using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskControl.Entities;

namespace TaskControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskRolesController : ControllerBase
    {
        private readonly TaskControlContext _context;

        public TaskRolesController(TaskControlContext context)
        {
            _context = context;
        }

        // GET: api/TaskRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskRole>>> GetTaskRoles()
        {
            return await _context.TaskRoles.ToListAsync();
        }

        // GET: api/TaskRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskRole>> GetTaskRole(int id)
        {
            var taskRole = await _context.TaskRoles.FindAsync(id);

            if (taskRole == null)
            {
                return NotFound();
            }

            return taskRole;
        }

        // PUT: api/TaskRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskRole(int id, TaskRole taskRole)
        {
            if (id != taskRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskRoleExists(id))
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

        // POST: api/TaskRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskRole>> PostTaskRole(TaskRole taskRole)
        {
            _context.TaskRoles.Add(taskRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskRole", new { id = taskRole.Id }, taskRole);
        }

        // DELETE: api/TaskRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskRole(int id)
        {
            var taskRole = await _context.TaskRoles.FindAsync(id);
            if (taskRole == null)
            {
                return NotFound();
            }

            _context.TaskRoles.Remove(taskRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskRoleExists(int id)
        {
            return _context.TaskRoles.Any(e => e.Id == id);
        }
    }
}
