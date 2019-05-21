using System;
using System.Web.Mvc;
using LabStore.Controllers;
using LabStore.Repositories;
using LabStore.ViewModels;
using LabStoreTest.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabStoreTest
{
    [TestClass]
    public class UsersMvcControllerTest
    {
        private IUnitOfWork _uow;
        UsersController _userController;
        private MockUserIdentity _mockUserIdentity;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _userController = new UsersController(_uow);
            _mockUserIdentity = new MockUserIdentity();
        }

        [TestMethod]
        public void IndexForUserTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _userController.ControllerContext = mockControllerContext.Object;

            var result = _userController.Index() as ViewResult;

            Assert.AreEqual("UserError", result.ViewName);
        }

        [TestMethod]
        public void IndexForUserWithCanManageRoleTest()
        {
            var user = _mockUserIdentity.CreateLoggedInUserWithRoleCanManage("userId");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            _userController.ControllerContext = mockControllerContext.Object;

            var result = _userController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void EditIfUserExistTest()
        {
            var result = _userController.Edit("def456") as ViewResult;
            var user = (UserViewModel)result.ViewData.Model;

            Assert.AreEqual("UserForm", result.ViewName);
            Assert.AreEqual("Krystyna", user.Name);
            Assert.AreEqual("Nowak", user.Surname);
            Assert.AreEqual("nowak@labmanager.pl", user.Email);

        }

        [TestMethod]
        public void EditIfUserNotExistTest()
        {
            var result = _userController.Edit("jkl101");

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void SaveIfModelIsNotValidTest()
        {
            var user = new UserViewModel()
            {
                Id = "abc123",
                Name = "Jan",
                Surname = "Nowak",
                Email = "nowak2@labmanager"
            };

            _userController.ModelState.AddModelError("key", "error message");

            var result = _userController.Save(user) as ViewResult;

            Assert.AreEqual("UserForm", result.ViewName);
        }

        [TestMethod]
        public void SaveIfModelIsValidForUserCanManageRoleTest()
        {
            var userToSet = _mockUserIdentity.CreateLoggedInUserWithRoleCanManage("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(userToSet);
            _userController.ControllerContext = mockControllerContext.Object;

            var user = new UserViewModel()
            {
                Id = "abc123",
                Name = "Jan",
                Surname = "Nowak",
                Email = "nowak2@labmanager"
            };

            var result = _userController.Save(user) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Users", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void SaveIfModelIsValidForUserTest()
        {
            var userToSet = _mockUserIdentity.CreateLoggedInUser("abc123");
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(userToSet);
            _userController.ControllerContext = mockControllerContext.Object;

            var user = new UserViewModel()
            {
                Id = "abc123",
                Name = "Jan",
                Surname = "Nowak",
                Email = "nowak2@labmanager"
            };

            var result = _userController.Save(user) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Manage", result.RouteValues["controller"]);
        }
    }
}
