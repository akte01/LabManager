using AutoMapper;
using LabStore.Dtos;
using LabStore.Models;
using LabStore.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace LabStore.Controllers.Api
{
    public class EquipmentController : ApiController
    {
        private IUnitOfWork _uow;

        public EquipmentController()
        {
            _uow = new UnitOfWork<EFContext>();
        }

        public EquipmentController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public IHttpActionResult GetEquipmentList(string query = null)
        {
            var equipmentQuery = _uow.GetRepository<Equipment>().GetAllQuery()
            .Include(e => e.EquipmentLocation);

            if (!string.IsNullOrWhiteSpace(query))
                equipmentQuery = equipmentQuery.Where(r => r.Name.Contains(query));

            var equipmentDtos = equipmentQuery
                .ToList()
                .Select(Mapper.Map<Equipment, EquipmentDto>);

            return Ok(equipmentDtos);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManage)]
        public IHttpActionResult DeleteEquipment(int id)
        {
            var equipmentInDb = _uow.GetRepository<Equipment>().Get(e => e.Id == id);

            if (equipmentInDb == null)
                return NotFound();

            _uow.GetRepository<Equipment>().Remove(equipmentInDb);
            _uow.SaveChanges();
            return Ok();
        }
    }
}
