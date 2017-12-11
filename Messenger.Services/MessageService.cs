using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<MessageListItem> GetMessages()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Messages
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new MessageListItem
                                {
                                    MessageId = e.MessageId,
                                    Title = e.Title,
                                    Content = e.Content,
                                    CreatedUtc = e.CreatedUtc
                                }
                            );
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
                        .Single(e => e.MessageId == messageId && e.OwnerId == _userId);
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
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Messages
                        .Single(e => e.MessageId == model.MessageId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                //this tells us how many rows are updated.
                return ctx.SaveChanges() == 1;
            }
        }

        //Need Service That Saves Note To The Other Users Table

        public bool DeleteMessage(int messageId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Messages
                        .Single(e => e.MessageId == messageId && e.OwnerId == _userId);
                //Mark for Deletion
                ctx.Messages.Remove(entity);
                //Only do one change 
                return ctx.SaveChanges() == 1;
            }
        }
    }

   
}
