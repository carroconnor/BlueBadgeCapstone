using System.Web.Mvc;
using Messenger.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMessage.Controllers.MessageControllerTests
{
    [TestClass]
    public class CreateMessage : MessageControllerTestBase
    {
        [TestMethod]
        public void ShouldReturnRedirectToRouteResult()
        {
            //Act
            var result = Controller.Create(new MessageCreate());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ShouldCallCreateOnce()
        {
            var result = Controller.Create(new MessageCreate());

            Assert.AreEqual(1, Service.CreateMessageCallCount);
        }
    }
}
