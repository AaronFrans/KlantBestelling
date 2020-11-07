using DomainLayer;
using DomainLayer.Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static DomainLayer.Domain.ProductEnum;

namespace UnitTests
{
    [TestClass]
    public class DomainTests
    {
        /// <summary>
        /// Test all logic in the Client Constructor.
        /// </summary>
        [TestMethod]
        public void ClientConstructor_Exceptions()
        {
            string name = "";
            string addres = "5345";
            Action act = () => new Client(name, addres);

            act.Should().Throw<DomainException>().WithMessage("Een naam mag niet leeg zijn");

            name = "Test Name";

            act = () => new Client(name, addres);

            act.Should().Throw<DomainException>().WithMessage("Een adres moet minstens 10 karakters lang zijn");

            addres = "0123456789";

            act = () => new Client(name, addres);

            act.Should().NotThrow<DomainException>();
        }

        /// <summary>
        /// Test all logic in the Order Constructor.
        /// </summary>
        [TestMethod]
        public void OrderConstructor_Exception()
        {
            string clientName = "Test Name";
            string clientAddres = "0123456789";
            Client client = new Client(clientName, clientAddres);

            int orderAmount = -1;
            ProductType orderProductType = ProductType.Duvel;

            Action act = () => new Order(orderProductType, orderAmount, client);
            act.Should().Throw<DomainException>().WithMessage("Het aantal moet groeter zijn dan 1");

            orderAmount = 5;

            act = () => new Order(orderProductType, orderAmount, client);
            act.Should().NotThrow<DomainException>();
        }

        /// <summary>
        /// Test all logic in the Client methods.
        /// </summary>
        [TestMethod]
        public void ClientMethods_Tests()
        {
            string clientName = "Test Name";
            string clientAddres = "0123456789";
            Client client = new Client(clientName, clientAddres);

            int expectedAmount = 10;
            ProductType expectedType = ProductType.Duvel;
            int expectedLenght = 1;

            client.AddOrder(ProductType.Duvel, 10);

            client.Orders.Count.Should().Be(expectedLenght);
            client.Orders[0].Product.Should().Be(expectedType);
            client.Orders[0].Amount.Should().Be(expectedAmount);

            client.AddOrder(ProductType.Duvel, 5);

            expectedAmount += 5;

            client.Orders.Count.Should().Be(expectedLenght);
            client.Orders[0].Product.Should().Be(expectedType);
            client.Orders[0].Amount.Should().Be(expectedAmount);

            client.AddOrder(ProductType.Leffe, 5);

            expectedLenght += 1;
            ProductType expectedTypeTwo = ProductType.Leffe;
            int expectedAmountTwo = 5;

            client.Orders.Count.Should().Be(expectedLenght);
            client.Orders[0].Product.Should().Be(expectedType);
            client.Orders[0].Amount.Should().Be(expectedAmount);
            client.Orders[1].Product.Should().Be(expectedTypeTwo);
            client.Orders[1].Amount.Should().Be(expectedAmountTwo);
        }

        /// <summary>
        /// Test the equals for Client objects.
        /// </summary>
        [TestMethod]
        public void ClientEquals_Tests()
        {
            Client c1 = new Client("Test", "Test Addres");
            Client c2 = new Client("Test", "Test Addres");

            bool result = c1.Equals(c2);

            result.Should().BeTrue();
        }
    }
}
