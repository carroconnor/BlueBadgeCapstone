using System;
using System.ComponentModel.DataAnnotations;

namespace Messenger.Data
{
    public class Conversations
    {
        [Key]
        public int ConversationsId { get; set; }

        [Required]
        public string LastMessageText { get; set; }

        [Required]
        public string LastMessageUserName { get; set; }

        [Required]
        public DateTime Modified { get; set; }
    }
}
