using FullStackAuth_WebAPI.Controllers;
using FullStackAuth_WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class ApplicationWithUserInterviewsNotesDto
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Archived { get; set; }
        public string Status { get; set; }
        public string Company { get; set; }
        public List<Interview> Interviews { get; set; }
        public List<Note> Notes { get; set; }
        public UserForDisplayDto Owner { get; set; }

    }
}
