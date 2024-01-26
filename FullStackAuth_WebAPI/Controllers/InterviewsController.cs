using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InterviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/interviews
        [HttpGet, Authorize]
        public IActionResult GetAllUsersInterviews()
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var interviews = _context.Interviews.Include(i => i.Job).Where(i => i.Job.OwnerId.Equals(userId)).ToList();

                return StatusCode(200, interviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/interviews/1
        [HttpGet("{id}"), Authorize]
        public IActionResult GetUsersInterviews(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var interviews = _context.Interviews.Include(i => i.Job).Where(i => i.JobId == id && i.Job.OwnerId == userId).ToList();
                if (interviews.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return StatusCode(200, interviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/interviews
        [HttpPost, Authorize]
        public IActionResult PostUsersInterview([FromBody] Interview interview)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                if (_context.Applications.FirstOrDefault(a => a.Id == interview.JobId) == null)
                {
                    return NotFound();
                }

                _context.Interviews.Add(interview);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();


                return StatusCode(201, interview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/interviews/5
        [HttpPut("{id}"), Authorize]
        public IActionResult PutEditInterview(int id, [FromBody] Interview data)
        {
            try
            {
                Interview interview = _context.Interviews.Include(i => i.Job).FirstOrDefault(i => i.Id == id);
                Application application = _context.Applications.Find(data.JobId);

                if (interview == null || application == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || interview.Job.OwnerId != userId)
                {
                    return Unauthorized();
                }

                interview.Type = data.Type;
                interview.Interviewer = data.Interviewer;
                interview.StartDate = data.StartDate;
                interview.EndDate = data.EndDate;
                interview.Details = data.Details;
                interview.JobId = data.JobId;
                interview.Job = application;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();


                return StatusCode(201, interview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
