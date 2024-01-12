using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public bool Archived { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Company { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
