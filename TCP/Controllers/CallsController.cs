using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCPCommunication.Data;
using TCPCommunication.Models;

namespace TCPCommunication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CallsController(AppDbContext context)
        {
            _context = context;
        }

        // 🟩 GET: /api/Calls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var calls = await _context.Calls
                .Select(c => new
                {
                    c.Id,
                    c.Type,
                    c.Timestamp,
                    c.UserId
                })
                .ToListAsync();

            return Ok(calls);
        }

        // 🟩 GET: /api/Calls/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Call>> GetById(int id)
        {
            var call = await _context.Calls.FindAsync(id);

            if (call == null)
                return NotFound();

            return Ok(call);
        }

        // 🟩 POST: /api/Calls
        [HttpPost]
        public async Task<ActionResult<Call>> Create(Call call)
        {
            _context.Calls.Add(call);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = call.Id }, call);
        }

        // 🟩 PUT: /api/Calls/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Call updatedCall)
        {
            var call = await _context.Calls.FindAsync(id);
            if (call == null)
                return NotFound();

            call.Type = updatedCall.Type;
            call.UserId = updatedCall.UserId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🟩 DELETE: /api/Calls/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            if (call == null)
                return NotFound();

            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}