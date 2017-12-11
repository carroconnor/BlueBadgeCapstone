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
#if !DEBUG
			[RequireHttps]
#endif


    [Authorize]
	public class MessageController : Controller
	{
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
			var service = new MessageService(userId);
			return service;
		}

		public ActionResult Details(int id)
		{
			var service = CreateMessageService();
			var model = service.GetMessageById(id);

			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var service = CreateMessageService();
			var detail = service.GetMessageById(id);
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

			if(model.MessageId != id)
			{
				ModelState.AddModelError("", "ID Does Not Match");
				return View(model);
			}

			var service = CreateMessageService();

			if (service.UpdateMessage(model))
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
			var service = CreateMessageService();
			var model = service.GetMessageById(id);

			return View(model);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteMessage(int id)
		{
			var service = CreateMessageService();
			//TODO: Handle failure

			service.DeleteMessage(id);

			TempData["SaveResult"] = "Your Message Was Deleted";

			return RedirectToAction("Index");
		}


	}
}