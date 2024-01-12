using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public string Text { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        public Application Job { get; set; }
    }
}
