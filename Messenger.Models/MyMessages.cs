using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Messenger.Data;

namespace Messenger.Models
{
    public class MyMessages
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        public override string ToString() => Title;
       
    }

    public class MessengerDbContext : DbContext
    {
        public DbSet<Message> MyMessages { get; set; }
    }
}
