using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCPCommunication.Data;
using TCPCommunication.Models;
using TCPCommunication.DTOs;

namespace TCPCommunication.Controllers
{
    [ApiController]
    [Route("api/Messages")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetAll()
        {
            var messages = await _context.Messages
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SentAt = m.SentAt,
                    UserId = m.UserId
                })
                .ToListAsync();

            return Ok(messages);
        }

        // GET: api/messages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> GetById(int id)
        {
            var message = await _context.Messages
                .Where(m => m.Id == id)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SentAt = m.SentAt,
                    UserId = m.UserId
                })
                .FirstOrDefaultAsync();

            if (message == null)
                return NotFound();

            return Ok(message);
        }

        // POST: api/messages
        [HttpPost]
        public async Task<ActionResult<MessageDto>> Create(MessageCreateDto dto)
        {
            var message = new Message
            {
                Content = dto.Content,
                UserId = dto.UserId,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var messageDto = new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                SentAt = message.SentAt,
                UserId = message.UserId
            };

            return CreatedAtAction(nameof(GetById), new { id = messageDto.Id }, messageDto);
        }

        // PUT: api/messages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MessageCreateDto dto)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            message.Content = dto.Content;
            message.UserId = dto.UserId; // (om du vill tillåta byta user)

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/messages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
