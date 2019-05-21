using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LabStore.Controllers
{
    public class OrdersController : Controller
    {
        private IUnitOfWork _uow;

        public OrdersController()
        {
            _uow = new UnitOfWork<EFContext>();
        }

        public OrdersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManage))
                return View("Index");
            return View("UserOrders");
        }

        public ActionResult NewItem(string id)
        {
            var units = _uow.GetRepository<Unit>().GetAll();
            var concentrationUnits = _uow.GetRepository<ConcentrationUnit>().GetAll();

            if (id != null)
            {
                var solutionOrders = _uow.GetRepository<SolutionOrder>().GetSelected(o => o.UserId == id);
                var solidOrders = _uow.GetRepository<SolidOrder>().GetSelected(o => o.UserId == id);
                var equipmentOrders = _uow.GetRepository<EquipmentOrder>().GetSelected(o => o.UserId == id);
                var orderBaseList = _uow.GetRepository<OrderBase>().GetSelected(o => o.UserId == id);

                var viewModel = new OrderViewModel
                {
                    UserId = User.Identity.GetUserId(),
                    Units = units,
                    ConcentrationUnits = concentrationUnits,
                    SolutionOrders = solutionOrders,
                    SolidOrders = solidOrders,
                    EquipmentOrders = equipmentOrders,
                    OrderBaseList = orderBaseList
                };
                return View("OrderForm", viewModel);
            }
            else
            {
                var solutionOrders = _uow.GetRepository<SolutionOrder>().GetSelected(o => o.UserId == "0");
                var solidOrders = _uow.GetRepository<SolidOrder>().GetSelected(o => o.UserId == "0");
                var equipmentOrders = _uow.GetRepository<EquipmentOrder>().GetSelected(o => o.UserId == "0");
                var orderBaseList = _uow.GetRepository<OrderBase>().GetSelected(o => o.UserId == "0");

                var viewModel = new OrderViewModel
                {
                    UserId = User.Identity.GetUserId(),
                    Units = units,
                    ConcentrationUnits = concentrationUnits,
                    SolutionOrders = solutionOrders,
                    SolidOrders = solidOrders,
                    EquipmentOrders = equipmentOrders,
                    OrderBaseList = orderBaseList
                };
                return View("OrderForm", viewModel);
            }
        }

        [HttpPost]
        public ActionResult ChangeStatus(ChangeStatusViewModel data)
        {
            if (data != null)
            {
                var orderInDb = _uow.GetRepository<Order>().Get(r => r.Id == data.Id);
                if (data.StatusId != 0)
                {
                    orderInDb.StatusId = data.StatusId;
                }
                _uow.SaveChanges();

                return RedirectToAction("Index", "Orders");
            }
            return HttpNotFound();
        }

        public ActionResult DeleteItem(string databaseName, int id)
        {
            switch (databaseName)
            {
                case "SolutionOrders":
                    var solutionOrder = _uow.GetRepository<SolutionOrder>().Get(o => o.Id == id);
                    _uow.GetRepository<SolutionOrder>().Remove(solutionOrder);
                    break;
                case "SolidOrders":
                    var solidOrder = _uow.GetRepository<SolidOrder>().Get(o => o.Id == id);
                    _uow.GetRepository<SolidOrder>().Remove(solidOrder);
                    break;
                case "EquipmentOrders":
                    var equipmentOrder = _uow.GetRepository<EquipmentOrder>().Get(o => o.Id == id);
                    _uow.GetRepository<EquipmentOrder>().Remove(equipmentOrder);
                    break;
            }

            _uow.SaveChanges();

            return RedirectToAction("NewItem", new { id = User.Identity.GetUserId() });
        }

        public ActionResult CancelOrder(string id)
        {
            var solutionOrders = _uow.GetRepository<SolutionOrder>().GetSelected(o => o.UserId == id);
            var solidOrders = _uow.GetRepository<SolidOrder>().GetSelected(o => o.UserId == id);
            var equipmentOrders = _uow.GetRepository<EquipmentOrder>().GetSelected(o => o.UserId == id);
            var orderBaseList = _uow.GetRepository<OrderBase>().GetSelected(o => o.UserId == id);

            _uow.GetRepository<OrderBase>().RemoveRange(orderBaseList);
            _uow.GetRepository<SolutionOrder>().RemoveRange(solutionOrders);
            _uow.GetRepository<SolidOrder>().RemoveRange(solidOrders);
            _uow.GetRepository<EquipmentOrder>().RemoveRange(equipmentOrders);

            _uow.SaveChanges();

            return RedirectToAction("NewItem");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(OrderViewModel orderItem)
        {
            var comment = "";
            if (!String.IsNullOrEmpty(orderItem.Comment))
            {
                comment = orderItem.Comment;
            }

            var orderBase = new OrderBase()
            {
                UserId = User.Identity.GetUserId(),
                DateFor = orderItem.DateFor,
                Grade = orderItem.Grade,
                Comment = comment
            };

            _uow.GetRepository<OrderBase>().Add(orderBase);

            if (orderItem.IsEquipment == true)
            {
                var equipmentOrder = new EquipmentOrder()
                {
                    UserId = User.Identity.GetUserId(),
                    Name = orderItem.EquipmentName,
                    Amount = orderItem.EquipmentAmount
                };

                _uow.GetRepository<EquipmentOrder>().Add(equipmentOrder);
            }

            if (orderItem.IsSolid == true)
            {
                var solidOrder = new SolidOrder()
                {
                    UserId = User.Identity.GetUserId(),
                    Name = orderItem.ReagentName,
                    Amount = orderItem.ReagentAmount,
                    UnitId = orderItem.UnitId
                };

                _uow.GetRepository<SolidOrder>().Add(solidOrder);
            }

            if ((orderItem.IsSolid == false) && (orderItem.IsEquipment == false))
            {
                var solutionOrder = new SolutionOrder()
                {
                    UserId = User.Identity.GetUserId(),
                    Name = orderItem.ReagentName,
                    Concentration = orderItem.Concentration,
                    ConcentrationUnitId = orderItem.ConcentrationUnitId,
                    Volume = orderItem.Volume
                };

                _uow.GetRepository<SolutionOrder>().Add(solutionOrder);
            }

            _uow.SaveChanges();

            return RedirectToAction("NewItem", new { id = User.Identity.GetUserId() });
        }

        public ActionResult Save(string id)
        {
            var solutionOrders = _uow.GetRepository<SolutionOrder>().GetSelected(o => o.UserId == id);
            var solidOrders = _uow.GetRepository<SolidOrder>().GetSelected(o => o.UserId == id);
            var equipmentOrders = _uow.GetRepository<EquipmentOrder>().GetSelected(o => o.UserId == id);
            var orderBaseList = _uow.GetRepository<OrderBase>().GetSelected(o => o.UserId == id);
            var content = GetContent(id);

            if (orderBaseList.Any())
            {
                var order = new Order()
                {
                    UserId = id,
                    DateFor = orderBaseList.First().DateFor,
                    Grade = orderBaseList.First().Grade,
                    Comment = orderBaseList.First().Comment,
                    Date = DateTime.UtcNow,
                    Content = content,
                    StatusId = 1,
                };

                _uow.GetRepository<Order>().Add(order);
                _uow.GetRepository<OrderBase>().RemoveRange(orderBaseList);
                _uow.GetRepository<SolutionOrder>().RemoveRange(solutionOrders);
                _uow.GetRepository<SolidOrder>().RemoveRange(solidOrders);
                _uow.GetRepository<EquipmentOrder>().RemoveRange(equipmentOrders);

                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("NewItem");
        }

        public ActionResult Edit(int id)
        {
            var order = _uow.GetRepository<Order>().Get(o => o.Id == id);
            if (order == null)
                return HttpNotFound();
            var viewModel = new OrderEditorViewModel(order);

            return View("OrderEditorForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEditedOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new OrderEditorViewModel(order);
                return View("OrderEditorForm", viewModel);
            }

            var orderInDb = _uow.GetRepository<Order>().Get(o => o.Id == order.Id);
            orderInDb.DateFor = order.DateFor;
            orderInDb.Grade = order.Grade;
            orderInDb.Content = order.Content;
            orderInDb.Comment = order.Comment + " (Edytowany: " + DateTime.UtcNow.ToShortDateString() + ")";

            _uow.SaveChanges();

            return RedirectToAction("Index", "Orders");
        }

        public string GetContent(string id)
        {
            var solutionOrders = _uow.GetRepository<SolutionOrder>().GetSelected(o => o.UserId == id);
            var solidOrders = _uow.GetRepository<SolidOrder>().GetSelected(o => o.UserId == id);
            var equipmentOrders = _uow.GetRepository<EquipmentOrder>().GetSelected(o => o.UserId == id);

            var units = _uow.GetRepository<Unit>().GetAll();
            var concentrationUnits = _uow.GetRepository<ConcentrationUnit>().GetAll();

            var solutions = "";
            var solids = "";
            var equipmentList = "";

            if (solutionOrders != null)
            {
                foreach (var solution in solutionOrders)
                {
                    solutions += (solution.Name + " " + solution.Concentration
                        + concentrationUnits.Single(o => o.Id == solution.ConcentrationUnitId).Name
                        + " " + solution.Volume + " mL; ");
                }
            }

            if (solidOrders != null)
            {
                foreach (var solid in solidOrders)
                {
                    solids += (solid.Name + " " + solid.Amount
                        + " " + units.Single(o => o.Id == solid.UnitId).Name + "; ");
                }
            }

            if (equipmentOrders != null)
            {
                foreach (var equipment in equipmentOrders)
                {
                    equipmentList += (equipment.Name + " " + equipment.Amount + " szt.; ");
                }
            }
            return "Roztwory: " + solutions + "Odczynniki stałe: " + solids + "Sprzęt: " + equipmentList;
        }
    }
}