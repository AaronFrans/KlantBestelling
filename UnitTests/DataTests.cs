using DataLayer;
using DataLayer.DataLayerClasses;
using DomainLayer.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DataTests
    {
        /// <summary>
        /// Test The mapper in the DataLayer for transforming domain into data objects.
        /// </summary>
        [TestMethod]
        public void MapperTest_ToDataObjects()
        {
            Client client = new Client("Test name", "Test addres");

            DClient dClient = Mapper.ToDClient(client);

            dClient.Id.Should().Be(client.Id);
            dClient.Addres.Should().Be(client.Addres);
            dClient.Name.Should().Be(client.Name);
            dClient.Orders.Count.Should().Be(0);

            Order order = new Order(ProductType.Duvel, 5, client);

            DOrder dOrder = Mapper.ToDOrder(order);

            dOrder.Id.Should().Be(order.Id);
            dOrder.Product.Should().Be(order.Product);
            dOrder.Amount.Should().Be(order.Amount);

            client.AddOrder(ProductType.Leffe, 10);

            dClient = Mapper.ToDClient(client);
            dClient.Orders.Count.Should().Be(1);
            dClient.Orders[0].Id.Should().Be(client.Orders[0].Id);
            dClient.Orders[0].Product.Should().Be(client.Orders[0].Product);
            dClient.Orders[0].Amount.Should().Be(client.Orders[0].Amount);
        }

        /// <summary>
        /// Test The mapper in the DataLayer for transforming data into domain objects.
        /// </summary>
        [TestMethod]
        public void MapperTest_ToDomainObjects()
        {
            DClient dClient = new DClient("Test", "Test address");

            Client client = Mapper.ToClient(dClient);

            client.Id.Should().Be(dClient.Id);
            client.Addres.Should().Be(dClient.Addres);
            client.Name.Should().Be(dClient.Name);
            client.Orders.Count.Should().Be(0);

            DOrder dOrder = new DOrder(ProductType.Duvel, 10);

            Order order = Mapper.ToOrder(dOrder, client);

            order.Id.Should().Be(dOrder.Id);
            order.Product.Should().Be(dOrder.Product);
            order.Amount.Should().Be(dOrder.Amount);
            order.Client.Id.Should().Be(client.Id);
            order.Client.Addres.Should().Be(client.Addres);
            order.Client.Name.Should().Be(client.Name);
            order.Client.Orders.Count.Should().Be(0);

            client.AddOrder(ProductType.Leffe, 6);

            Client client2 = Mapper.ToClient(Mapper.ToDClient(client));

            client2.Id.Should().Be(client.Id);
            client2.Addres.Should().Be(client.Addres);
            client2.Name.Should().Be(client.Name);
            client2.Orders.Count.Should().Be(1);
            client2.Orders[0].Id.Should().Be(client.Orders[0].Id);
            client2.Orders[0].Product.Should().Be(client.Orders[0].Product);
            client2.Orders[0].Amount.Should().Be(client.Orders[0].Amount);
        }

        /// <summary>
        /// Test whether adding clients to the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_AddClient()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(false));

            Client client = new Client("Test name", "Test addres");

            Action act = () => uow.Clients.AddClient(client);

            act.Should().NotThrow<Exception>();

            uow.Complete();

            act = () => uow.Clients.AddClient(client);

            act.Should().Throw<DataException>().WithMessage("Client is al in databank.");

            Client client2 = new Client("Test name2", "Test addres2");

            act = () => uow.Clients.AddClient(client2);

            act.Should().NotThrow<Exception>();
            uow.Complete();

            Client client3 = new Client("Test name3", "Test addres3");

            uow.Clients.AddClient(client3);
            uow.Complete();
        }


        /// <summary>
        /// Test whether updating clients to the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_UpdateClient()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(true));

            Client client = new Client("Test name Update", "Test addres Update");

            Action act = () => uow.Clients.UpdateClient(4, client);

            act.Should().Throw<DataException>().WithMessage("Het gegeven klantId is niet in de database");

            act = () => uow.Clients.UpdateClient(2, client);

            act.Should().NotThrow<DataException>();

            uow.Complete();

            Client client2 = uow.Clients.GetClient(2);

            client2.Should().Be(client);

            uow.Complete();
        }

        /// <summary>
        /// Test whether deleting clients to the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_RemoveClient()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(true));

            Action act = () => uow.Clients.DeleteClient(5);

            act.Should().Throw<DataException>().WithMessage("Het gegeven klantId is niet in de database");


            act = () => uow.Clients.DeleteClient(2);

            act.Should().Throw<DataException>().WithMessage("Er zijn nog bestellingen gelinkt aan de client");

            act = () => uow.Clients.DeleteClient(1);

            act.Should().NotThrow<DataException>();

            uow.Complete();

            act = () => uow.Clients.GetClient(1);

            act.Should().Throw<DataException>().WithMessage("Het gegeven klantId is niet in de database");
        }


        /// <summary>
        /// Test whether making orders in the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_MakeOrder()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(true));

            ProductType product = ProductType.Duvel;
            int amount = 4;

            Action act = () => uow.Orders.MakeOrder(10, product, amount);

            act.Should().Throw<DataException>().WithMessage("The given clientId is not in the database");

            uow.Orders.MakeOrder(2, product, amount);

            uow.Orders.MakeOrder(2, product, amount);

            ProductType product2 = ProductType.Leffe;

            uow.Orders.MakeOrder(2, product2, amount);

            ProductType product3 = ProductType.Westmalle;

            uow.Orders.MakeOrder(2, product3, amount);

            uow.Orders.MakeOrder(3, product2, amount);

            uow.Complete();
        }

        /// <summary>
        /// Test whether making orders in the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_UpdateOrder()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(true));

            ProductType product = ProductType.Orval;
            int amount = -1;

            int clientId = 5;
            int orderId = 10;

            Action act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().Throw<DataException>().WithMessage("Het gegeven orderId is niet in de database");

            orderId = 1;

            act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().Throw<DataException>().WithMessage("Het gegeven klantId is niet in de database");

            clientId = 2;

            act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().Throw<DataException>().WithMessage("De gegeven klant heeft geen bestelling van het gegeven type.");

            product = ProductType.Duvel;

            act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().Throw<DataException>().WithMessage("De hoeveelheid moet groter zijn dan 0");

            amount = 10;

            act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().NotThrow<DataException>();

            uow.Complete();

            var order = uow.Orders.GetOrder(orderId, clientId);

            order.Amount.Should().Be(amount);

            product = ProductType.Westmalle;

            act = () => uow.Orders.UpdateOrder(clientId, orderId, product, amount);

            act.Should().Throw<DataException>().WithMessage("Het product van het gegeven orderId is niet hetzelfde als het meegegeven product");


            act = () => uow.Orders.UpdateOrder(3, 1, ProductType.Leffe, 5);

            act.Should().Throw<DataException>().WithMessage("Het gegeven klantId heeft geen order met het gegeven orderId");

        }



        /// <summary>
        /// Test whether deleting order to the database works.
        /// </summary>
        [TestMethod]
        public void DataTest_RemoveOrder()
        {
            UnitOfWork uow = new UnitOfWork(new ClientOrderContextTest(true));

            Action act = () => uow.Orders.DeleteOrder(5);

            act.Should().Throw<DataException>().WithMessage("Het gegeven orderId is niet in de database");

            act = () => uow.Orders.DeleteOrder(2);

            act.Should().NotThrow<DataException>();

            uow.Complete();

            act = () => uow.Orders.GetOrder(2, 2);

            act.Should().Throw<DataException>().WithMessage("Het gegeven orderId is niet in de database");
        }

    }
}