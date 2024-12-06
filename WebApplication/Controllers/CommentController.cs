using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly Context _context;
        public CommentController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            if (comments == null || comments.Count == 0)
            {
                return NotFound("Komentar tidak ditemukan."); 
            }

            return Ok(comments); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Komentar tidak ditemukan."); 
            }

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, Comment updatedComment)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Komentar tidak ditemukan.");
            }

            
            comment.Body = updatedComment.Body;
            comment.Name = updatedComment.Name;
            comment.Email = updatedComment.Email;

            await _context.SaveChangesAsync(); 

            return Ok(comment); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Komentar tidak ditemukan."); 
            }

            _context.Comments.Remove(comment); 
            await _context.SaveChangesAsync(); 

            return Ok("Dihapus"); 
        }
    }
}
