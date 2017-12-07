using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Messenger.Models;
using Messenger.Services;
using Microsoft.AspNet.Identity;

namespace Messenger.Web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private MessageService CreateMessageService()
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
            var model = service.GetNotes();

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
                TempData["SaveResult"] = "Your note was created";
                return RedirectToAction("Index");
            } 

            ModelState.AddModelError("", "Note could not be created");

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
            var service = CreateMessageService();
            var model = service.GetMessageById(id);

            return View(model);
        }


    }
}