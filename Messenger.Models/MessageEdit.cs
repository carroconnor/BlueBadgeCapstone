﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Models
{
    public class MessageEdit
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
