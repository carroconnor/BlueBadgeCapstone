using System.ComponentModel.DataAnnotations;

namespace Messenger.Models
{
    public class MessageCreate
    {
        [MaxLength(280)]
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(280)]
        public string Content { get; set; }

        public override string ToString() => Title;
    }
}
