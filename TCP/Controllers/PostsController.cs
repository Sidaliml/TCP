using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCPCommunication.Data;
using TCPCommunication.Models;

namespace TCPCommunication.Controllers
{
    [ApiController]
    [Route("api/Posts")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] string? filter, [FromQuery] string? sort)
        {
            var query = _context.Posts.AsQueryable();

            // 🔎 Filter på titel
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.Title.Contains(filter));
            }

            // ⬆️⬇️ Sortering efter CreatedAt
            query = sort switch
            {
                "asc" => query.OrderBy(p => p.CreatedAt),
                "desc" => query.OrderByDescending(p => p.CreatedAt),
                _ => query
            };

            var posts = await query
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.UserId
                })
                .ToListAsync();

            return Ok(posts);
        }

        // GET: api/posts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Content,
                    p.CreatedAt,
                    p.UserId
                })
                .FirstOrDefaultAsync();

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> Create(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }

        // PUT: api/posts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
