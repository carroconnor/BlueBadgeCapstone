using System;
using System.Web.Mvc;
using Messenger.Models;
using Messenger.Services;
using Microsoft.AspNet.Identity;

namespace Messenger.Web.Controllers
{
    [Authorize]
    public class MyMessageController : Controller 
    {
        // GET: MyMessages
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MessageService(userId);
            var model = service.GetMyMessages();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageCreate model)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View();
        }
    }
}