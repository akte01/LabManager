using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using AutoMapper;
using LabStore.App_Start;
using LabStore.Controllers.Api;
using LabStore.Dtos;
using LabStore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabStoreTest
{
    [TestClass]
    public class EquipmentApiControllerTest
    {
        private IUnitOfWork _uow;
        EquipmentController _equipmentController;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _equipmentController = new EquipmentController(_uow);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [TestMethod]
        public void GetequipmentTest()
        {
            var actionResult = _equipmentController.GetEquipmentList();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<EquipmentDto>>;
            var equipmentList = response.Content;
            var equipment = new EquipmentDto();
            equipment = equipmentList.SingleOrDefault(r => r.Id == 1);

            Assert.IsNotNull(response);
            Assert.AreEqual(3, equipmentList.Count());

            Assert.AreEqual("Mieszadła magnetyczne", equipment.Name);
            Assert.AreEqual(10, equipment.Amount);
            Assert.AreEqual(1, equipment.EquipmentLocationId);
        }

        [TestMethod]
        public void DeleteEquipmentIfExistTest()
        {
            var response = _equipmentController.DeleteEquipment(1);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteEquipmentIfNotExistTest()
        {
            var result = _equipmentController.DeleteEquipment(4);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
