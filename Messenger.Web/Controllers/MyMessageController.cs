using System;
using System.Web.Mvc;
using Messenger.Contracts;
using Messenger.Models;
using Messenger.Services;
using Microsoft.AspNet.Identity;

namespace Messenger.Web.Controllers
{
#if !DEBUG
			[RequireHttps]
#endif


    [Authorize]
    public class MyMessageController : Controller
    {
        private readonly Lazy<IMessageService> _messageService;

        public MyMessageController()
        {
            _messageService = new Lazy<IMessageService>(() =>

                new MessageService(Guid.Parse(User.Identity.GetUserId()))
            );
        }

        public MyMessageController(Lazy<IMessageService> messageService)
        {
            _messageService = messageService;
        }

        public MessageService CreateMessageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MessageService(userId);

            return service;
        }

        // GET: Message
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MessageService(userId);
            var model = service.GetMyMessages();

            return View(model);
        }

        //Create Method

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_messageService.Value.CreateMessage(model))
            {
                TempData["SaveResult"] = "Your Message Was Sent";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Message Could Not Be Created");

            return View(model);
        }

        private MessageService CreateMessageMethod()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MessageService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var model = _messageService.Value.GetMessageById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detail = _messageService.Value.GetMessageById(id);
            var model =
                new MessageEdit
                {
                    MessageId = detail.MessageId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }


        //make it so they can only update title
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MessageEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MessageId != id)
            {
                ModelState.AddModelError("", "ID Does Not Match");
                return View(model);
            }

            if (_messageService.Value.UpdateMessage(model))
            {
                TempData["SaveResult"] = "Your Message Was Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Message Could Not Be Updated");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var model = _messageService.Value.GetMessageById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMessage(int id)
        {
            _messageService.Value.DeleteMessage(id);

            TempData["SaveResult"] = "Your Message Was Deleted";
            ModelState.AddModelError("", "Your Message Could Not Be Updated");

            return RedirectToAction("Index");
        }


    }
}