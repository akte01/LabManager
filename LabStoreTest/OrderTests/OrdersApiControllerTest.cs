using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using System.Web.Mvc;
using AutoMapper;
using LabStore.App_Start;
using LabStore.Controllers.Api;
using LabStore.Dtos;
using LabStore.Models;
using LabStore.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabStoreTest.OrderTests
{
    [TestClass]
    public class OrdersApiControllerTest
    {
        private IUnitOfWork _uow;
        OrdersController _orderController;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _orderController = new OrdersController(_uow);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [TestMethod]
        public void GetReagentsWithCanManageRoleTest()
        {
            var roles = new[] { RoleName.CanManage };
            var identity = new GenericIdentity("kowalski@labmanager.pl");
            var principal = new GenericPrincipal(identity, roles);
            _orderController.User = principal;
    
            var actionResult = _orderController.GetOrders();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<OrderDto>>;
            var orders = response.Content;
            var order = new OrderDto();
            order = orders.SingleOrDefault(r => r.Id == 2);

            Assert.IsNotNull(response);
            Assert.AreEqual(2, orders.Count());
            Assert.AreEqual("Roztwory: Azotan srebra 0,10 mol/L 400 mL; Odczynniki stałe: Sprzęt:", order.Content);
            Assert.AreEqual("4at", order.Grade);
        }

        [TestMethod]
        public void GetReagentsForUserTest()
        {
            var identity = new GenericIdentity("kowalski@labmanager.pl");
            identity.AddClaim(
                new Claim(ClaimTypes.NameIdentifier.ToString(), "abc123"));         
            var principal = new GenericPrincipal(identity, null);
            _orderController.User = principal;

            var actionResult = _orderController.GetOrders();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<OrderDto>>;
            var orders = response.Content;
            var order = new OrderDto();
            order = orders.SingleOrDefault(r => r.Id == 1);

            Assert.IsNotNull(response);
            Assert.AreEqual(1, orders.Count());
            Assert.AreEqual("Roztwory: Odczynniki stałe: Sprzęt: aerometry 3 szt.; płyty żeliwne - podgrzewacze 2 szt.;", order.Content);
            Assert.AreEqual("3a", order.Grade);
        }

        [TestMethod]
        public void DeleteOrderIfExistTest()
        {
            var response = _orderController.DeleteOrder(1);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteOrderIfNotExistTest()
        {
            var result = _orderController.DeleteOrder(4);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
