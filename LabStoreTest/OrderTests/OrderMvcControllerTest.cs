using LabStore.Controllers;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using LabStoreTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LabStoreTest.OrderTests
{
    [TestClass]
    public class OrderMvcControllerTest
    {
        private IUnitOfWork _uow;
        OrdersController _orderController;
        private MockUserIdentity _mockUserIdentity;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _orderController = new OrdersController(_uow);
            _mockUserIdentity = new MockUserIdentity();
        }

        [TestMethod]
        public void IndexForUserTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _orderController.ControllerContext = mockControllerContext.Object;

            var result = _orderController.Index() as ViewResult;

            Assert.AreEqual("UserOrders", result.ViewName);
        }


        [TestMethod]
        public void IndexForUserWithCanManageRoleTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUserWithRoleCanManage("userId");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _orderController.ControllerContext = mockControllerContext.Object;

            var result = _orderController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void NewItemIfIdIsNullTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("0");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _orderController.ControllerContext = mockControllerContext.Object;

            var result = _orderController.NewItem(null) as ViewResult;
            var orderItem = (OrderViewModel)result.ViewData.Model;
            var concentrationUnits = orderItem.ConcentrationUnits;
            var units = orderItem.Units;

            Assert.AreEqual("OrderForm", result.ViewName);
            Assert.AreEqual("", orderItem.SolidOrders.FirstOrDefault().Name);
            Assert.AreEqual(0, orderItem.EquipmentOrders.FirstOrDefault().Amount);
            Assert.IsNotNull(orderItem.Units);
            Assert.IsNotNull(orderItem.ConcentrationUnits);
            Assert.AreEqual(2, concentrationUnits.Count());
            Assert.AreEqual(3, units.Count());
        }

        [TestMethod]
        public void NewItemIfIdIsNotNullTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _orderController.ControllerContext = mockControllerContext.Object;

            var result = _orderController.NewItem("abc123") as ViewResult;
            var orderItem = (OrderViewModel)result.ViewData.Model;
            var concentrationUnits = orderItem.ConcentrationUnits;
            var units = orderItem.Units;
            var solidOrders = orderItem.SolidOrders;
            var equipmentOrders = orderItem.EquipmentOrders;


            Assert.AreEqual("OrderForm", result.ViewName);
            Assert.AreEqual("Amonu chlorek", solidOrders.FirstOrDefault().Name);
            Assert.AreEqual(5, equipmentOrders.FirstOrDefault().Amount);
            Assert.IsNotNull(solidOrders);
            Assert.IsNotNull(units);
            Assert.IsNotNull(concentrationUnits);
            Assert.IsNotNull(equipmentOrders);
            Assert.AreEqual(1, equipmentOrders.Count());
            Assert.AreEqual(1, solidOrders.Count());
            Assert.AreEqual(2, concentrationUnits.Count());
            Assert.AreEqual(3, units.Count());
        }

        [TestMethod]
        public void ChangeStatusIfDataIsNotNullTest()
        {
            var data = new ChangeStatusViewModel()
            {
                Id = 2,
                StatusId = 2
            };

            var result = _orderController.ChangeStatus(data) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ChangeStatusIfDataIsNullTest()
        {
            ChangeStatusViewModel data = null;

            var result = _orderController.ChangeStatus(data);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _orderController.ControllerContext = mockControllerContext.Object;

            var databaseName = "SolidOrders";
            var id = 1;

            var result = _orderController.DeleteItem(databaseName, id) as RedirectToRouteResult;

            Assert.AreEqual("NewItem", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SaveOrderIfIsNotEmptyTest()
        {
            var result = _orderController.Save("abc123") as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditIfOrderIsNullTest()
        {
            var result = _orderController.Edit(5);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditIfOrderIsNotNullTest()
        {
            var result = _orderController.Edit(1) as ViewResult;
            var orderItem = (OrderEditorViewModel)result.ViewData.Model;

            Assert.AreEqual("OrderEditorForm", result.ViewName);
            Assert.AreEqual("Roztwory: Odczynniki stałe: Sprzęt: aerometry 3 szt.; płyty żeliwne - podgrzewacze 2 szt.;", orderItem.Content);
            Assert.AreEqual("3a", orderItem.Grade);
        }

        [TestMethod]
        public void SaveEditedOrderIfModelIsNotValidTest()
        {
            var order = new Order()
            {
                Id = 1
            };
            _orderController.ModelState.AddModelError("key", "error message");

            var result = _orderController.SaveEditedOrder(order) as ViewResult;

            Assert.AreEqual("OrderEditorForm", result.ViewName);
        }

        [TestMethod]
        public void SaveEditedOrderIfModelIsValidTest()
        {
            var order = new Order()
            {
                Id = 1,
                Date = new DateTime(2019, 03, 27, 10, 22, 49),
                Content = "Nowa zawartość",
                StatusId = 1,
                DateFor = new DateTime(2019, 03, 29, 11, 00, 00),
                Grade = "3a",
                UserId = "abc123",
                Comment = ""
            };
            var result = _orderController.SaveEditedOrder(order) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
