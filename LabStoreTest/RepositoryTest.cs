using System;
using LabStore.Models;
using LabStore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace LabStoreTest
{
    [TestClass]
    public class RepositoryTest
    {
       private IUnitOfWork _uow;
       IEnumerable<Reagent> reagents;
       IEnumerable<Order> orders;
 
       [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
        }
        
        [TestMethod]
        public void GetAllQueryReagentsTest()
        {
            var reagentsQuery = _uow.GetRepository<Reagent>().GetAllQuery();
            reagents = reagentsQuery.ToList();
            var reagent = new Reagent();
            reagent = reagents.SingleOrDefault(r => r.Id == 1);

            Assert.IsNotNull(reagents);
            Assert.AreEqual(3, reagents.Count());
            Assert.AreEqual("Amonu chlorek", reagent.Name);
        }

        [TestMethod]
        public void GetAllQueryOrderTest()
        {
            var ordersQuery = _uow.GetRepository<Order>().GetAllQuery();
            orders = ordersQuery.ToList();
            var order = new Order();
            order = orders.SingleOrDefault(o => o.Id == 2);

            Assert.IsNotNull(orders);
            Assert.AreEqual(2, orders.Count());
            Assert.AreEqual("4at", order.Grade);
            Assert.AreEqual("def456", order.UserId);
        }

        [TestMethod]
        public void GetReagentByIdTest()
        {
            var reagent = _uow.GetRepository<Reagent>().Get(r => r.Id == 3);

            Assert.AreEqual("Amonu węglan", reagent.Name);
            Assert.AreEqual(1, reagent.StorageLocationId);
        }

        [TestMethod]
        public void GetReagentByNameTest()
        {
            var reagent = _uow.GetRepository<Reagent>().Get(r => r.Name == "Amonu azotan");

            Assert.AreEqual(2, reagent.Id);
            Assert.AreEqual(500, reagent.FinalAmount);
        }

        [TestMethod]
        public void GetUnitByIdTest()
        {
            var unit = _uow.GetRepository<Unit>().Get(u => u.Id == 1);

            Assert.AreEqual("g", unit.Name);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var storageLocations = _uow.GetRepository<StorageLocation>().GetAll();
            var storageLocation = new StorageLocation();
            storageLocation = storageLocations.SingleOrDefault(r => r.Id == 4);

            Assert.AreEqual(6, storageLocations.Count());
            Assert.AreEqual("Magazyn, P. Lab.", storageLocation.Name);
        }

        [TestMethod]
        public void AddItemTest()
        {
            var reagent = new Reagent()
            {
                Id = 4,
                Name = "Azotan srebra",
                InitialAmount = 1000,
                ConsumedAmount = 10,
                FinalAmount = 990,
                StorageLocationId = 1,
                UnitId = 1
            };

            _uow.GetRepository<Reagent>().Add(reagent);

            var reagentAdded = _uow.GetRepository<Reagent>().Get(r => r.Id == 4);
      
            Assert.AreEqual("Azotan srebra", reagentAdded.Name);
            Assert.AreEqual(1000, reagentAdded.InitialAmount);
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            var reagent = _uow.GetRepository<Reagent>().Get(r => r.Id == 3);
            _uow.GetRepository<Reagent>().Remove(reagent);

            reagents = _uow.GetRepository<Reagent>().GetAll();
            var reagentRemoved = _uow.GetRepository<Reagent>().Get(u => u.Id == 3);

            Assert.IsNull(reagentRemoved);
            Assert.AreEqual(2, reagents.Count());
        }

        [TestMethod]
        public void CheckIfExistTest()
        {
            var response = _uow.GetRepository<Reagent>().CheckIfExist(r => r.Name == "Amonu azotan" && r.StorageLocationId == 1);
            Assert.IsTrue(response);

            response = _uow.GetRepository<Reagent>().CheckIfExist(r => r.Name == "Amonu azotan" && r.StorageLocationId == 2);
            Assert.IsFalse(response);
        }

    }
}
