using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Models
{
    public class MessageCreate
    {
        [MaxLength(8000)]
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }

        public override string ToString() => Title;
    }
}
