using FlashMessage;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using System.Web.Mvc;

namespace LabStore.Controllers
{
    public class EquipmentController : Controller
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

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManage))
                return View("Index");
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ViewResult New()
        {
            var locations = _uow.GetRepository<EquipmentLocation>().GetAll();
            var viewModel = new EquipmentViewModel
            {
                EquipmentLocations = locations
            };
            return View("EquipmentForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Edit(int id)
        {
            var equipment = _uow.GetRepository<Equipment>().Get(e => e.Id == id);
            if (equipment == null)
                return HttpNotFound();
            var viewModel = new EquipmentViewModel(equipment)
            {
                EquipmentLocations = _uow.GetRepository<EquipmentLocation>().GetAll()
        };
            return View("EquipmentForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Save(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new EquipmentViewModel(equipment)
                {
                    EquipmentLocations = _uow.GetRepository<EquipmentLocation>().GetAll()
            };
                return View("EquipmentForm", viewModel);
            }

            bool equipmentExist = _uow.GetRepository<Equipment>().CheckIfExist(e => e.Name == equipment.Name && e.EquipmentLocationId == equipment.EquipmentLocationId);

            Equipment equipmentWithTheSameNameAndLocation = new Equipment();

            if (equipmentExist)
            {
                equipmentWithTheSameNameAndLocation = _uow.GetRepository<Equipment>().Get(e => e.Name == equipment.Name && e.EquipmentLocationId == equipment.EquipmentLocationId);
            }

            if (equipment.Id == 0)
            {
                if (!equipmentExist)
                {
                    _uow.GetRepository<Equipment>().Add(equipment);
                }
                else
                {
                    return RedirectToAction("New", "Equipment").WithError("Sprzęt o podanej nazwie istnieje już w danej lokalizacji!");
                }
            }
            else
            {
                var equipmentInDb = _uow.GetRepository<Equipment>().Get(e => e.Id == equipment.Id);

                if (!equipmentExist || (equipmentExist && equipmentWithTheSameNameAndLocation.Id == equipment.Id))
                {
                    equipmentInDb.Name = equipment.Name;
                    equipmentInDb.Amount = equipment.Amount;
                    equipmentInDb.EquipmentLocationId = equipment.EquipmentLocationId;
                    equipmentInDb.Comment = equipment.Comment;
                }
                else
                {
                    return RedirectToAction("New", "Equipment").WithError("Sprzęt o podanej nazwie istnieje już w danej lokalizacji!");
                }
            }
            
            _uow.SaveChanges();

            return RedirectToAction("Index", "Equipment");
        }
    }
}