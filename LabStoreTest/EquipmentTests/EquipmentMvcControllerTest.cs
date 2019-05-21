using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FlashMessage;
using LabStore.App_Start;
using LabStore.Controllers;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using LabStoreTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabStoreTest
{
    [TestClass]
    public class EquipmentMvcControllerTest
    {
        private IUnitOfWork _uow;
        EquipmentController _equipmentController;
        private MockUserIdentity _mockUserIdentity;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _equipmentController = new EquipmentController(_uow);
            _mockUserIdentity = new MockUserIdentity();
        }

        [TestMethod]
        public void IndexForUserTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _equipmentController.ControllerContext = mockControllerContext.Object;

            var result = _equipmentController.Index() as ViewResult;

            Assert.AreEqual("ReadOnlyList", result.ViewName);
        }

        [TestMethod]
        public void IndexForUserWithCanManageRoleTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUserWithRoleCanManage("userId");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _equipmentController.ControllerContext = mockControllerContext.Object;

            var result = _equipmentController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void NewTest()
        {
            var result = _equipmentController.New() as ViewResult;
            var equipment = (EquipmentViewModel)result.ViewData.Model;
            var equipmentLocations = equipment.EquipmentLocations;

            Assert.AreEqual("EquipmentForm", result.ViewName);
            Assert.AreEqual(null, equipment.Name);
            Assert.AreEqual(0, equipment.Amount);
            Assert.IsNotNull(equipment.EquipmentLocations);
            Assert.AreEqual(8, equipmentLocations.Count());
        }

        [TestMethod]
        public void EditIfEquipmentExistTest()
        {
            var result = _equipmentController.Edit(2) as ViewResult;
            var equipment = (EquipmentViewModel)result.ViewData.Model;
            var equipmentLocations = equipment.EquipmentLocations;

            Assert.AreEqual("EquipmentForm", result.ViewName);
            Assert.AreEqual("Gruszka na pipetę", equipment.Name);
            Assert.AreEqual(5, equipment.Amount);
            Assert.AreEqual(5, equipment.EquipmentLocationId);
            Assert.AreEqual(8, equipmentLocations.Count());
        }

        [TestMethod]
        public void EditIfEquipmentNotExistTest()
        {
            var result = _equipmentController.Edit(7);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void SaveNewIfModelIsValidTest()
        {
            var equipment = new Equipment()
            {
                Id = 0,
                Name = "pH-metr",
                Amount = 10,
                EquipmentLocationId = 1,
                Comment = ""
            };

            var result = _equipmentController.Save(equipment) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SaveNewIfModelIsNotValidTest()
        {
            var equipment = new Equipment()
            {
                Id = 0,
                Name = "",
                Amount = 10,
                EquipmentLocationId = 1,
                Comment = ""
            };
            _equipmentController.ModelState.AddModelError("key", "error message");

            var result = _equipmentController.Save(equipment) as ViewResult;

            Assert.AreEqual("EquipmentForm", result.ViewName);
        }

        [TestMethod]
        public void SaveNewIfEquipmentAlreadyExist()
        {
            var equipment = new Equipment()
            {
                Id = 0,
                Name = "Gruszka na pipetę",
                Amount = 5,
                EquipmentLocationId = 5,
                Comment = ""
            };

            var result = _equipmentController.Save(equipment);

            Assert.IsInstanceOfType(result, typeof(WrappedActionResultWithFlash<RedirectToRouteResult>));
        }
    }
}
