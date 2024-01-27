using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/applications
        [HttpGet, Authorize]
        public IActionResult GetAllUsersApplications()
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var application = _context.Applications.Where(a => a.OwnerId.Equals(userId)).Select(a => new ApplicationWithUserDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Archived = a.Archived,
                    Status = a.Status,
                    Company = a.Company,
                    Owner = new UserForDisplayDto
                    {
                        Id = a.Owner.Id,
                        FirstName = a.Owner.FirstName,
                        LastName = a.Owner.LastName,
                        UserName = a.Owner.UserName,
                    }
                }).ToList();

                return StatusCode(200, application);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/applications/1
        [HttpGet("{id}"), Authorize]
        public IActionResult GetUsersApplication(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var application = _context.Applications.Include(a => a.Owner).FirstOrDefault(a => a.Id == id && a.OwnerId == userId);
                if (application == null)
                {
                    return NotFound();
                }

                var result = new ApplicationWithUserDto
                {
                    Id = application.Id,
                    Title = application.Title,
                    Archived = application.Archived,
                    Status = application.Status,
                    Company = application.Company,
                    Owner = new UserForDisplayDto
                    {
                        Id = application.Owner.Id,
                        FirstName = application.Owner.FirstName,
                        LastName = application.Owner.LastName,
                        UserName = application.Owner.UserName,
                    }
                };

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/applications
        [HttpPost, Authorize]
        public IActionResult PostUsersApplications([FromBody] Application application)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                application.OwnerId = userId;

                _context.Applications.Add(application);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                return StatusCode(201, application);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/applications/5
        [HttpPut("{id}"), Authorize]
        public IActionResult PutEditUsersApplication(int id, [FromBody] Application data)
        {
            try
            {
                Application application = _context.Applications.Include(a => a.Owner).FirstOrDefault(a => a.Id == id);

                if (application == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || application.OwnerId != userId)
                {
                    return Unauthorized();
                }

                application.Title = data.Title;
                application.Archived = data.Archived;
                application.Status = data.Status;
                application.Company = data.Company;
                application.OwnerId = userId;
                application.Owner = _context.Users.Find(userId);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                var result = new ApplicationWithUserDto
                {
                    Id = application.Id,
                    Title = application.Title,
                    Archived = application.Archived,
                    Status = application.Status,
                    Company = application.Company,
                    Owner = new UserForDisplayDto
                    {
                        Id = application.Owner.Id,
                        FirstName = application.Owner.FirstName,
                        LastName = application.Owner.LastName,
                        UserName = application.Owner.UserName,
                    }
                };

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // PATCH api/applications/archive/1
        [HttpPatch("archive/{id}"), Authorize]
        public IActionResult PatchArchiveUsersApplication(int id)
        {
            try
            {
                Application application = _context.Applications.Include(a => a.Owner).FirstOrDefault(a => a.Id == id);

                if (application == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || application.OwnerId != userId)
                {
                    return Unauthorized();
                }
                application.Archived = !application.Archived;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                var result = new ApplicationWithUserDto
                {
                    Id = application.Id,
                    Title = application.Title,
                    Archived = application.Archived,
                    Status = application.Status,
                    Company = application.Company,
                    Owner = new UserForDisplayDto
                    {
                        Id = application.Owner.Id,
                        FirstName = application.Owner.FirstName,
                        LastName = application.Owner.LastName,
                        UserName = application.Owner.UserName,
                    }
                };

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PATCH api/applications/status/1
        [HttpPatch("status/{id}"), Authorize]
        public IActionResult PatchStatusUsersApplication(int id, [FromBody] ApplicationStatusDto data)
        {
            try
            {
                Application application = _context.Applications.Include(a => a.Owner).FirstOrDefault(a => a.Id == id);

                if (application == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");
                if (string.IsNullOrEmpty(userId) || application.OwnerId != userId)
                {
                    return Unauthorized();
                }
                application.Status = data.Status;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                var result = new ApplicationWithUserDto
                {
                    Id = application.Id,
                    Title = application.Title,
                    Archived = application.Archived,
                    Status = application.Status,
                    Company = application.Company,
                    Owner = new UserForDisplayDto
                    {
                        Id = application.Owner.Id,
                        FirstName = application.Owner.FirstName,
                        LastName = application.Owner.LastName,
                        UserName = application.Owner.UserName,
                    }
                };

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
