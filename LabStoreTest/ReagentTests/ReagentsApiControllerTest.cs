using AutoMapper;
using LabStore.App_Start;
using LabStore.Controllers.Api;
using LabStore.Dtos;
using LabStore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace LabStoreTest
{
    [TestClass]
    public class ReagentsApiControllerTest
    {
        private IUnitOfWork _uow;
        ReagentsController _reagentController;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _reagentController = new ReagentsController(_uow);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }

        [TestMethod]
        public void GetReagentsTest()
        {         
            var actionResult = _reagentController.GetReagents();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<ReagentDto>>;
            var reagents = response.Content;
            var reagent = new ReagentDto();
            reagent = reagents.SingleOrDefault(r => r.Id == 1);
           
            Assert.IsNotNull(response);
            Assert.AreEqual(3, reagents.Count());

            Assert.AreEqual("Amonu chlorek", reagent.Name);
            Assert.AreEqual(300, reagent.FinalAmount);
            Assert.AreEqual(1, reagent.StorageLocationId);
        }

        [TestMethod]
        public void DeleteReagentIfExistTest()
        {
            var response = _reagentController.DeleteReagent(1);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteReagentIfNotExistTest()
        {
            var result = _reagentController.DeleteReagent(4);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
