using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/notes/1
        [HttpGet("{id}"), Authorize]
        public IActionResult GetUsersNotes(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var notes = _context.Notes.Include(n => n.Job).Where(n => n.JobId == id && n.Job.OwnerId == userId).ToList();
                if (notes.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return StatusCode(200, notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/notes
        [HttpPost, Authorize]
        public IActionResult PostUsersNote([FromBody] Note note)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                if(_context.Applications.FirstOrDefault(a => a.Id == note.JobId) == null)
                {
                    return NotFound();
                }

                _context.Notes.Add(note);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();


                return StatusCode(201, note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/notes/5
        [HttpPut("{id}"), Authorize]
        public IActionResult PutEditNote(int id, [FromBody] Note data)
        {
            try
            {
                Note note = _context.Notes.Include(n => n.Job).FirstOrDefault(n => n.Id == id);
                Application application = _context.Applications.Find(data.JobId);

                if (note == null || application == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || note.Job.OwnerId != userId)
                {
                    return Unauthorized();
                }

                note.Title = data.Title;
                note.TimeStamp = data.TimeStamp;
                note.Text = data.Text;
                note.JobId = data.JobId;
                note.Job = application;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();


                return StatusCode(201, note);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
