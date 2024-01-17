using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class ApplicationWithUserDto
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Archived { get; set; }
        public string Status { get; set; }
        public string Company { get; set; }
        public UserForDisplayDto Owner { get; set; }

    }
}
