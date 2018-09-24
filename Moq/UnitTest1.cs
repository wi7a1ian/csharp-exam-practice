using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MoqTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CustomerTestFixture()
        {
            Mock<ICustomer> mockCustomerDl = new Mock<ICustomer>();

            // Setup our instance to return true when we verify IsActive property
            mockCustomerDl.SetupGet(prop => prop.IsActive).Returns(true);
            mockCustomerDl.Setup(meth => meth.DeActivate()).Returns(true);

            // Run it
            var blCustomer = new BusinessLayerCustomer(mockCustomerDl.Object);
            blCustomer.DeActivateCustomer();
        }

        [TestMethod]
        public void SendMailTestFixture()
        {
            //Create a mock object of a MailClient class which implements IMailClient
            var mockMailClient = new Moq.Mock<IMailClient>();

            //Mock the properties of MailClient
            //(Setup the properties of dummy class to use defautl values)
            mockMailClient.SetupProperty(client => client.Server, "chat.mail.com").SetupProperty(client => client.Port, "1212");

            //Configure dummy method so that it return true when it gets any string as parameters to the method
            mockMailClient.Setup(client => client.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            IMailer mailer = new DefaultMailer() { From = "from@mail.com", To = "to@mail.com", Subject = "Using Moq", Body = "Moq is awesome" };

            //Use the mock object of MailClient instead of actual object
            var result = mailer.SendMail(mockMailClient.Object);

            //Verify that it return true
            Assert.IsTrue(result);

            //Verify that the MailClient's SendMail methods gets called exactly once when string is passed as parameters
            mockMailClient.Verify(client => client.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
