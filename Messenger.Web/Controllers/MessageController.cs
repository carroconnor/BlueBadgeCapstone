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
    public class MessageController : Controller
    {
        private readonly Lazy<IMessageService> _messageService;

        public MessageController()
        {
            _messageService = new Lazy<IMessageService>(() =>

                new MessageService(Guid.Parse(User.Identity.GetUserId()))
            );
        }

        public MessageController(Lazy<IMessageService> messageService)
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
            var model = service.GetMessages();

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

            var service = CreateMessageMethod();

            if (service.CreateMessage(model))
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
            //var name = User.Identity.Name;
            var service = new MessageService(userId);

            return service;
        }

        public ActionResult Details(int id)
        {
            var service = CreateMessageService();
            var model = service.GetMessageById(id);

            return View(model);
        }
    }
}