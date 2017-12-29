using System;
using Messenger.Contracts;
using Messenger.Models;

namespace UnitTestMessage.Controllers.MessageControllerTests
{
    public class FakeMessageService : IMessageService
    {
        public int CreateMessageCallCount { get; private set; }
        public bool CreateMessageReturnValue { private get; set; } = true;

        public bool CreateMessage(MessageCreate model)
        {
            CreateMessageCallCount++;

            return CreateMessageReturnValue;
        }

        public bool UpdateMessage(MessageEdit model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public MessageDetail GetMessageById(int messsageId)
        {
            throw new NotImplementedException();
        }
    }
}
