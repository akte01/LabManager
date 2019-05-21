using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using AutoMapper;
using LabStore.App_Start;
using LabStore.Controllers.Api;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabStoreTest
{
    [TestClass]
    public class UsersApiControllerTest
    {
        private IUnitOfWork _uow;
        UsersController _userController;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _userController = new UsersController(_uow);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var actionResult = _userController.GetUsers();
            var response = actionResult as OkNegotiatedContentResult<List<UserViewModel>>;
            var users = response.Content;
            var user = new UserViewModel();
            user = users.SingleOrDefault(r => r.Id == "abc123");

            Assert.IsNotNull(response);
            Assert.AreEqual(3, users.Count());

            Assert.AreEqual("Jan", user.Name);
            Assert.AreEqual("Kowalski", user.Surname);
            Assert.AreEqual("kowalski@labmanager.pl", user.Email);
        }

        [TestMethod]
        public void DeleteUserIfExistTest()
        {
            var response = _userController.DeleteUser("def456");
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteUserIfNotExistTest()
        {
            var result = _userController.DeleteUser("jkl101");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
