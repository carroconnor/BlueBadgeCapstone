using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Messenger.Models;
using Messenger.Services;
using Microsoft.AspNet.Identity;

namespace Messenger.API.Controllers
{
    [Authorize]
    public class MessengerController : ApiController
    {
        public IHttpActionResult GetMessages()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var messageService = new MessageService(userId);
            var messages = messageService.GetMessages();
            return Ok(messages);
        }

        public IHttpActionResult Get(int id)
        {
            var messageService = new MessageService(Guid.Parse(User.Identity.GetUserId()));
            var message = messageService.GetMessageById(id);
            if (message == null) return NotFound();
            return Ok(message);
        }

        public IHttpActionResult Post(MessageCreate model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var messageService = new MessageService(Guid.Parse(User.Identity.GetUserId()));
            return Ok(messageService.CreateMessage(model));
        }

        public IHttpActionResult Put(MessageEdit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Make sure the message exists
            var messageService = new MessageService(Guid.Parse(User.Identity.GetUserId()));
            var temp = messageService.GetMessageById(model.MessageId);
            if (temp == null) return NotFound();

            //Attempt to update
            return Ok(messageService.UpdateMessage(model));
        }

        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Make sure the message exists
            var messageService = new MessageService(Guid.Parse(User.Identity.GetUserId()));
            var temp = messageService.GetMessageById(id);
            if (temp == null) return NotFound();

            //Attempt to delete
            return Ok(messageService.DeleteMessage(id));
        }

    }
}
