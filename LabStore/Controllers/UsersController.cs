using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using System.Web.Mvc;

namespace LabStore.Controllers
{
    public class UsersController : Controller
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

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManage))
                return View("Index");
            return View("UserError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
                return View("UserForm", viewModel);
            }
                var userInDb = _uow.GetRepository<ApplicationUser>().Get(u => u.Id == user.Id);
                    userInDb.Name = user.Name;
                    userInDb.Surname = user.Surname;
                    userInDb.Email = user.Email;
            
            _uow.SaveChanges();

            if (User.IsInRole(RoleName.CanManage))
                return RedirectToAction("Index", "Users");
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit(string id)
        {
            var user = _uow.GetRepository<ApplicationUser>().Get(u => u.Id == id);
            if (user == null)
                return HttpNotFound();
            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
            return View("UserForm", viewModel);
        }
    }
}