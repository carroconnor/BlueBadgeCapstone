using System;
using Messenger.Contracts;
using Messenger.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMessage.Controllers.MessageControllerTests
{
    [TestClass]
    public abstract class MessageControllerTestBase
    {
        protected MessageController Controller;

        protected FakeMessageService Service;

        //Arrange
        //Act
        //Assert

        [TestInitialize]
        public virtual void Arrange()
        {
            Service = new FakeMessageService();

            Controller = new MessageController(
                new Lazy<IMessageService>(() => Service));

        }
    }
}
