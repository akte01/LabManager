using FlashMessage;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using System.Web.Mvc;

namespace LabStore.Controllers
{
    public class ReagentsController : Controller
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

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManage))
                return View("Index");
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ViewResult New()
        {
            var units = _uow.GetRepository<Unit>().GetAll();
            var locations = _uow.GetRepository<StorageLocation>().GetAll();
            var viewModel = new ReagentViewModel
            {
                Units = units,
                StorageLocations = locations
            };
            return View("ReagentForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Edit(int id)
        {
            var reagent = _uow.GetRepository<Reagent>().Get(r => r.Id == id);
            if (reagent == null)
                return HttpNotFound();
            var viewModel = new ReagentViewModel(reagent)
            {
                Units = _uow.GetRepository<Unit>().GetAll(),
                StorageLocations = _uow.GetRepository<StorageLocation>().GetAll()
            };
            return View("ReagentForm", viewModel);
        }

        [HttpPost]
        public ActionResult AddConsumption(ReagentAddConsumptionViewModel data)
        {
            if (data != null)
            {
                var reagentInDb = _uow.GetRepository<Reagent>().Get(r => r.Id == data.Id);

                var consumedAmount = reagentInDb.ConsumedAmount + data.ConsumedAmount;
                var finalAmount = reagentInDb.FinalAmount - data.ConsumedAmount;
                reagentInDb.FinalAmount = finalAmount;
                reagentInDb.ConsumedAmount = consumedAmount;

                _uow.SaveChanges();

                return RedirectToAction("Index", "Reagents");
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Save(Reagent reagent)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ReagentViewModel(reagent)
                {
                    Units = _uow.GetRepository<Unit>().GetAll(),
                    StorageLocations = _uow.GetRepository<StorageLocation>().GetAll()
                };
                return View("ReagentForm", viewModel);
            }

            bool reagentExist = _uow.GetRepository<Reagent>().CheckIfExist(r => r.Name == reagent.Name && r.StorageLocationId == reagent.StorageLocationId);

            Reagent reagentWithTheSameNameAndLocation = new Reagent();

            if (reagentExist)
            {
                reagentWithTheSameNameAndLocation = _uow.GetRepository<Reagent>().Get(r => r.Name == reagent.Name && r.StorageLocationId == reagent.StorageLocationId);
            }

            if (reagent.Id == 0)
            {
                if (!reagentExist)
                {
                    _uow.GetRepository<Reagent>().Add(reagent);
                }
                else
                {
                    return RedirectToAction("New", "Reagents").WithError("Odczynnik o podanej nazwie istnieje już w danej lokalizacji!");
                }
            }
            else
            {
                var reagentInDb = _uow.GetRepository<Reagent>().Get(r => r.Id == reagent.Id);

                if (!reagentExist || (reagentExist && reagentWithTheSameNameAndLocation.Id == reagent.Id))
                {
                    reagentInDb.Name = reagent.Name;
                    reagentInDb.InitialAmount = reagent.InitialAmount;
                    reagentInDb.ConsumedAmount = reagent.ConsumedAmount;
                    reagentInDb.FinalAmount = reagent.FinalAmount;
                    reagentInDb.UnitId = reagent.UnitId;
                    reagentInDb.StorageLocationId = reagent.StorageLocationId;
                    reagentInDb.Comment = reagent.Comment;
                }
                else
                {
                    return RedirectToAction("New", "Reagents").WithError("Odczynnik o podanej nazwie istnieje już w danej lokalizacji!");
                }
            }
                _uow.SaveChanges();

            return RedirectToAction("Index", "Reagents");
        }
    }
}