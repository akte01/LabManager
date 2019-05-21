using AutoMapper;
using LabStore.Dtos;
using LabStore.Models;
using LabStore.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace LabStore.Controllers.Api
{
    public class ReagentsController : ApiController
    {
        private IUnitOfWork _uow;

        public ReagentsController()
        {
            _uow = new UnitOfWork<EFContext>();
        }

        public ReagentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IHttpActionResult GetReagents(string query = null)
        {
            var reagentsQuery = _uow.GetRepository<Reagent>().GetAllQuery().Include(r => r.StorageLocation)
                .Include(r => r.Unit);

            if (!String.IsNullOrWhiteSpace(query))
                reagentsQuery = reagentsQuery.Where(r => r.Name.Contains(query));

            var reagentDtos = reagentsQuery
                .ToList()
                .Select(Mapper.Map<Reagent, ReagentDto>);

            return Ok(reagentDtos);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManage)]
        public IHttpActionResult DeleteReagent(int id)
        {
            var reagentInDb = _uow.GetRepository<Reagent>().Get(r => r.Id == id);

            if (reagentInDb == null)
                return NotFound();

            _uow.GetRepository<Reagent>().Remove(reagentInDb);
            _uow.SaveChanges();

            return Ok();
        }
    }
}
