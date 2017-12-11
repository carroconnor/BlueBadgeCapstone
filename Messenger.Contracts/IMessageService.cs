using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger.Models;

namespace Messenger.Contracts
{
    public interface IMessageService
    {
        bool CreateMessage(MessageCreate model);
        bool UpdateMessage(MessageEdit model);
        bool DeleteMessage(int messageId);
        MessageDetail GetMessageById(int messsageId);
    }
}
