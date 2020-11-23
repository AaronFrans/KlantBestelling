using DataLayer;
using DomainLayer;
using DomainLayer.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestLayer.Controllers;
using RestLayer.Model;
using System;
using Xunit;

namespace RestTests
{
    public class ApiTests
    {
        private readonly Mock<IManager> mockManager;
        private readonly ClientController clientController;

        public ApiTests()
        {
            this.mockManager = new Mock<IManager>();
            this.clientController = new ClientController(mockManager.Object);
        }

        #region Client
        #region GET
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void GetClient_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.GetClient(2))
                .Returns(new Client("Homer", "0123456789") { Id = 2 });
            var result = clientController.GetClient(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void GetClient_ReturnsClientBody()
        {
            Client c = new Client("Homer", "0123456789") { Id = 2 };
            mockManager.Setup(repo => repo.GetClient(2))
                .Returns(c);

            var result = clientController.GetClient(2).Result as OkObjectResult;

            Assert.IsType<RClientOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 2, (result.Value as RClientOutput).Id);
            Assert.Equal(c.Addres, (result.Value as RClientOutput).Addres);
            Assert.Equal(c.Name, (result.Value as RClientOutput).Name);
            Assert.Empty((result.Value as RClientOutput).Orders);
        }
        /// <summary>
        /// Check for bad  request result.
        /// </summary>
        [Fact]
        public void GetClient_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.GetClient(2))
                .Throws(new Exception());
            var result = clientController.GetClient(2);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region POST
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void PostClient_ReturnsRequestType()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.AddClient(c)).Returns(1);
            mockManager.Setup(repo => repo.GetClient(1)).Returns(c);
            var result = clientController.PostClient(new RClientInput() { Name = "bla", Addres = "0123456789" });
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void PostClient_ReturnsClientBody()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.AddClient(c)).Returns(1);
            c.Id = 1;
            mockManager.Setup(repo => repo.GetClient(1)).Returns(c);

            var result = clientController.PostClient(new RClientInput() { Name = "bla", Addres = "0123456789" }).Result as CreatedAtActionResult;

            Assert.IsType<RClientOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 1, (result.Value as RClientOutput).Id);
            Assert.Equal(c.Addres, (result.Value as RClientOutput).Addres);
            Assert.Equal(c.Name, (result.Value as RClientOutput).Name);
            Assert.Empty((result.Value as RClientOutput).Orders);
        }
        /// <summary>
        /// Check for bad  request result.
        /// </summary>
        [Fact]
        public void PostClient_ReturnsBadRequest()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.AddClient(c)).Throws(new Exception());

            var result = clientController.PostClient(new RClientInput() { Name = "bla", Addres = "0123456789" });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region PUT
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void PutClient_ReturnsRequestType()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.UpdateClient(1, c));
            c.Id = 1;
            mockManager.Setup(repo => repo.GetClient(1))
                .Returns(c);
            var result = clientController.PutClient(1, new RClientInput() { Name = "bla", Addres = "0123456789", Id = 1 });
            Assert.IsType<OkObjectResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void PutClient_ReturnsClientBody()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.UpdateClient(1, c));
            c.Id = 1;
            mockManager.Setup(repo => repo.GetClient(1))
                .Returns(c);

            var result = clientController.PutClient(1, new RClientInput() { Name = "bla", Addres = "0123456789", Id = 1 }).Result as OkObjectResult;


            Assert.IsType<RClientOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 1, (result.Value as RClientOutput).Id);
            Assert.Equal(c.Addres, (result.Value as RClientOutput).Addres);
            Assert.Equal(c.Name, (result.Value as RClientOutput).Name);
            Assert.Empty((result.Value as RClientOutput).Orders);
        }
        /// <summary>
        /// Check for bad  request result.
        /// </summary>
        [Fact]
        public void PutClient_ReturnsBadRequest()
        {
            Client c = new Client("bla", "0123456789");
            mockManager.Setup(repo => repo.UpdateClient(1, c)).Throws(new Exception());

            var result = clientController.PutClient(1, new RClientInput() { Name = "bla", Addres = "0123456789" });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region DELETE
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void DeleteClient_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.DeleteClient(1));
            var result = clientController.DeleteClient(1);
            Assert.IsType<NoContentResult>(result);
        }
        /// <summary>
        /// Check for bad  request result.
        /// </summary>
        [Fact]
        public void DeleteClient_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.DeleteClient(1)).Throws(new Exception());

            var result = clientController.DeleteClient(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        /// <summary>
        /// Check for not found result.
        /// </summary>
        [Fact]
        public void DeleteClient_ReturnsNotFound()
        {
            mockManager.Setup(repo => repo.DeleteClient(1))
                .Throws(new Exception("Het gegeven klantId is niet in de database"));

            var result = clientController.DeleteClient(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
        #endregion

        #region Order
        #region GET
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void GetOrder_ReturnsRequestType()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });
            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.GetOrder(2, 1);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void GetOrder_ReturnsClientBody()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });

            o.Id = 1;
            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.GetOrder(2, 1).Result as OkObjectResult;

            Assert.IsType<ROrderOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 2 + Urls.OrderUrl + 1, (result.Value as ROrderOutput).Id);
            Assert.Equal(o.Amount, (result.Value as ROrderOutput).Amount);
            Assert.Equal(Urls.ClientUrl + 2, (result.Value as ROrderOutput).ClientId);
            Assert.Equal(o.Product.ToString(), (result.Value as ROrderOutput).Product);
        }
        /// <summary>
        /// Check for bad result.
        /// </summary>
        [Fact]
        public void GetOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.GetOrder(2, 1))
                .Throws(new Exception());
            var result = clientController.GetOrder(2, 1);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region POST
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void PostOrder_ReturnsRequestType()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });
            mockManager.Setup(repo => repo.MakeOrder(2, ProductType.Leffe, 5))
                .Returns(1);

            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.PostOrder(2, new ROrderInput() { ClientId = 2, Product = "Leffe", Amount = 5 });
            Assert.IsType<OkObjectResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void PostOrder_ReturnsClientBody()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });

            mockManager.Setup(repo => repo.MakeOrder(2, ProductType.Leffe, 5))
                .Returns(1);

            o.Id = 1;

            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.PostOrder(2, new ROrderInput() { ClientId = 2, Product = "Leffe", Amount = 5 }).Result as OkObjectResult;

            Assert.IsType<ROrderOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 2 + Urls.OrderUrl + 1, (result.Value as ROrderOutput).Id);
            Assert.Equal(o.Amount, (result.Value as ROrderOutput).Amount);
            Assert.Equal(Urls.ClientUrl + 2, (result.Value as ROrderOutput).ClientId);
            Assert.Equal(o.Product.ToString(), (result.Value as ROrderOutput).Product);
        }
        /// <summary>
        /// Check for bad result.
        /// </summary>
        [Fact]
        public void PostOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.MakeOrder(2, ProductType.Leffe, 5))
                .Throws(new Exception());
            var result = clientController.PostOrder(2, new ROrderInput() { ClientId = 2, Product = "Leffe", Amount = 5 });
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region PUT
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void PutOrder_ReturnsRequestType()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });
            mockManager.Setup(repo => repo.UpdateOrder(2, 1, ProductType.Leffe, 5));

            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.PutOrder(2, 1, new ROrderInput() { OrderId = 1, ClientId = 2, Product = "Leffe", Amount = 5 });
            Assert.IsType<OkObjectResult>(result.Result);
        }
        /// <summary>
        /// Check the returned type
        /// </summary>
        [Fact]
        public void PutOrder_ReturnsClientBody()
        {
            Order o = new Order(ProductType.Leffe, 5, new Client("Arr", "0123456789") { Id = 2 });

            mockManager.Setup(repo => repo.UpdateOrder(2, 1, ProductType.Leffe, 5));

            o.Id = 1;

            mockManager.Setup(repo => repo.GetOrder(1, 2))
                .Returns(o);

            var result = clientController.PutOrder(2, 1, new ROrderInput() { OrderId = 1, ClientId = 2, Product = "Leffe", Amount = 5 }).Result as OkObjectResult;

            Assert.IsType<ROrderOutput>(result.Value);
            Assert.Equal(Urls.ClientUrl + 2 + Urls.OrderUrl + 1, (result.Value as ROrderOutput).Id);
            Assert.Equal(o.Amount, (result.Value as ROrderOutput).Amount);
            Assert.Equal(Urls.ClientUrl + 2, (result.Value as ROrderOutput).ClientId);
            Assert.Equal(o.Product.ToString(), (result.Value as ROrderOutput).Product);
        }
        /// <summary>
        /// Check for bad result.
        /// </summary>
        [Fact]
        public void PutOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.UpdateOrder(2, 1, ProductType.Leffe, 5))
                .Throws(new Exception());

            var result = clientController.PostOrder(2, new ROrderInput() { ClientId = 2, Product = "Leffe", Amount = 5 });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
        #region DELETE
        /// <summary>
        /// Check the request return type
        /// </summary>
        [Fact]
        public void DeleteOrder_ReturnsRequestType()
        {
            mockManager.Setup(repo => repo.DeleteOrder(1))
                .Throws(new Exception("Het gegeven orderId is niet in de database"));
            var result = clientController.DeleteOrder(1);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        /// <summary>
        /// Check for bad  request result.
        /// </summary>
        [Fact]
        public void DeleteOrder_ReturnsBadRequest()
        {
            mockManager.Setup(repo => repo.DeleteOrder(1));
            var result = clientController.DeleteOrder(1);
            Assert.IsType<NoContentResult>(result);
        }
        /// <summary>
        /// Check for not found result.
        /// </summary>
        [Fact]
        public void DeleteOrder_ReturnsNotFound()
        {
            mockManager.Setup(repo => repo.DeleteOrder(1))
                .Throws(new Exception()); 
            var result = clientController.DeleteOrder(1);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion
        #endregion
    }
}
