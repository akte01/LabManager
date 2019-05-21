using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace LabStore.Controllers.Api
{
    public class UsersController : ApiController
    {
        private IUnitOfWork _uow;

        public UsersController()
        {
            _uow = new UnitOfWork<EFContext>();
        }

        public UsersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IHttpActionResult GetUsers()
        {
            var users = _uow.GetRepository<ApplicationUser>().GetAll();
            var usersVM = users.Select(user => new UserViewModel{Id = user.Id, Name = user.Name, Surname = user.Surname, Email = user.Email }).ToList();
            return Ok(usersVM);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManage)]
        public IHttpActionResult DeleteUser(string id)
        {
            var adminId = User.Identity.GetUserId();

            if (id != adminId)
            {
                var userInDb = _uow.GetRepository<ApplicationUser>().Get(u => u.Id == id);

                if (userInDb == null)
                    return NotFound();

                var orders = _uow.GetRepository<Order>().GetSelected(o => o.UserId == id);

                if (orders.Any())
                {
                    _uow.GetRepository<Order>().RemoveRange(orders);
                }

                _uow.GetRepository<ApplicationUser>().Remove(userInDb);
                _uow.SaveChanges();
                return Ok();
            }
            else return Ok();
        }
    }
}
