using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Models
{
    public class MyMessages
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
