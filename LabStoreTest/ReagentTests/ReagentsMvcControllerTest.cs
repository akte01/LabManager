using System;
using AutoMapper;
using LabStore.App_Start;
using LabStore.Controllers;
using LabStore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security.Principal;
using LabStore.Models;
using LabStore.ViewModels;
using FlashMessage;
using LabStoreTest.Mock;
using Moq;

namespace LabStoreTest
{
    [TestClass]
    public class ReagentsMvcControllerTest
    {
        private IUnitOfWork _uow;
        ReagentsController _reagentController;
        private MockUserIdentity _mockUserIdentity;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _reagentController = new ReagentsController(_uow);
            _mockUserIdentity = new MockUserIdentity();
        }

        [TestMethod]
        public void IndexForUserTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _reagentController.ControllerContext = mockControllerContext.Object;

            var result = _reagentController.Index() as ViewResult;

            Assert.AreEqual("ReadOnlyList", result.ViewName);
        }

        [TestMethod]
        public void IndexForUserWithCanManageRoleTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUserWithRoleCanManage("userId");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _reagentController.ControllerContext = mockControllerContext.Object;

            var result = _reagentController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void NewTest()
        {
            var result = _reagentController.New() as ViewResult;          
            var reagent = (ReagentViewModel)result.ViewData.Model;
            var storageLocations = reagent.StorageLocations;
            var units = reagent.Units;

            Assert.AreEqual("ReagentForm", result.ViewName);
            Assert.AreEqual(null, reagent.Name);
            Assert.AreEqual(0, reagent.InitialAmount);
            Assert.IsNotNull(reagent.Units);
            Assert.IsNotNull(reagent.StorageLocations);
            Assert.AreEqual(6, storageLocations.Count());
            Assert.AreEqual(3, units.Count());
        }

        [TestMethod]
        public void EditIfReagentExistTest()
        {
            var result = _reagentController.Edit(2) as ViewResult;
            var reagent = (ReagentViewModel)result.ViewData.Model;
            var storageLocations = reagent.StorageLocations;
            var units = reagent.Units;

            Assert.AreEqual("ReagentForm", result.ViewName);
            Assert.AreEqual("Amonu azotan", reagent.Name);
            Assert.AreEqual(600, reagent.InitialAmount);
            Assert.AreEqual(1, reagent.StorageLocationId);
            Assert.AreEqual(1, reagent.UnitId);
            Assert.AreEqual(6, storageLocations.Count());
            Assert.AreEqual(3, units.Count());
        }

        [TestMethod]
        public void EditIfReagentNotExistTest()
        {
            var result = _reagentController.Edit(7);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void AddConsumptionIfDataIsNotNullTest()
        {
            var data= new ReagentAddConsumptionViewModel()
            {
                Id = 1,
                ConsumedAmount = 20
            };

            var result = _reagentController.AddConsumption(data) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AddConsumptionIfIsNullTest()
        {
            ReagentAddConsumptionViewModel data = null;

            var result = _reagentController.AddConsumption(data);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void SaveNewIfModelIsValidTest()
        {
            var reagent = new Reagent()
            {
                Id = 0,
                Name = "Azotan srebra",
                InitialAmount = 250,
                ConsumedAmount = 10,
                FinalAmount = 240,
                StorageLocationId = 1,
                UnitId = 1
            };

            var result = _reagentController.Save(reagent) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SaveNewIfModelIsNotValidTest()
        {
            var reagent = new Reagent()
            {
                Id = 0,
                Name = "",
                InitialAmount = 250,
                ConsumedAmount = 10,
                FinalAmount = 240,
                StorageLocationId = 1,
                UnitId = 1
            };
            _reagentController.ModelState.AddModelError("key", "error message");

            var result = _reagentController.Save(reagent) as ViewResult;

            Assert.AreEqual("ReagentForm", result.ViewName);
        }

        [TestMethod]
        public void SaveNewIfReagentAlreadyExist()
        {
            var reagent = new Reagent()
            {
                Id = 0,
                Name = "Amonu węglan",
                InitialAmount = 1000,
                ConsumedAmount = 250,
                FinalAmount = 750,
                StorageLocationId = 1,
                UnitId = 1
            };

            var result = _reagentController.Save(reagent);

            Assert.IsInstanceOfType(result, typeof(WrappedActionResultWithFlash<RedirectToRouteResult>));
        }
    }
}
