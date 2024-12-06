using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly Context _context;
        public PostController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            if (posts == null || posts.Count == 0)
            {
                return NotFound("Postingan tidak ditemukan.");
            }

            return Ok(posts); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Postingan tidak ditemukan.");
            }

            return Ok(post); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post updatedPost)
        {
            if (id != updatedPost.Id) 
            {
                return BadRequest("ID tidak cocok.");
            }

            var post = await _context.Posts.FindAsync(id); 
            if (post == null)
            {
                return NotFound("Postingan tidak ditemukan.");
            }
       
            post.Title = updatedPost.Title;
            await _context.SaveChangesAsync();

            return Ok(post); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id); 
            if (post == null)
            {
                return NotFound("Postingan tidak ditemukan.");
            }

            _context.Posts.Remove(post); 
            await _context.SaveChangesAsync();

            return Ok("Dihapus");
        }

    }
}
