using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Messenger.Contracts;
using Messenger.Data;
using Messenger.Models;

namespace Messenger.Services
{
    public class MessageService : IMessageService
    {
        private readonly Guid _userId;

        public MessageService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMessage(MessageCreate model)
        {
            var entity =
                new Message()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Messages.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MyMessages> GetMyMessages()
        {

            using (var ctx = new ApplicationDbContext())
            {
                IDbSet<Message> messages = ctx.Messages;
                IDbSet<ApplicationUser> users = ctx.Users;

                var query =
                    messages
                        .Where(message => message.OwnerId == _userId)
                        .Join(
                            users,
                            message => message.OwnerId.ToString(),
                            user => user.Id.ToString(),
                            (message, user) =>
                                new MyMessages()
                                {
                                    Name = user.Name,
                                    MessageId = message.MessageId,
                                    Title = message.Title,
                                    Content = message.Content,
                                    CreatedUtc = message.CreatedUtc
                                }
                          );

                return query.ToArray();
            }
        }

        public IEnumerable<MessageListItem> GetMessages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                IDbSet<Message> messages = ctx.Messages;
                IDbSet<ApplicationUser> users = ctx.Users;

                var query =
                    ctx
                        .Messages
                        .Join(
                            users,
                            message => message.OwnerId.ToString(),
                            user => user.Id.ToString(),
                            (message, user) =>
                                new MessageListItem()
                                {
                                    Name = user.Name,
                                    MessageId = message.MessageId,
                                    Title = message.Title,
                                    Content = message.Content,
                                    CreatedUtc = message.CreatedUtc
                                });

                return query.ToArray();
            }
        }

        public MessageDetail GetMessageById(int messageId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Messages
                        .Single(e => e.MessageId == messageId);
                return
                    new MessageDetail
                    {
                        MessageId = entity.MessageId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateMessage(MessageEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Messages
                        .Single(e => e.MessageId == model.MessageId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMessage(int messageId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Messages
                        .Single(e => e.MessageId == messageId && e.OwnerId == _userId);
                ctx.Messages.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }


}
