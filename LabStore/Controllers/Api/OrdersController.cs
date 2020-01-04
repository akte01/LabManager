using AutoMapper;
using LabStore.Dtos;
using LabStore.Models;
using LabStore.Repositories;
using LabStore.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace LabStore.Controllers.Api
{
    public class OrdersController : ApiController
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

        [HttpGet]
        public IHttpActionResult GetOrders(string query = null)
        {
            var id = User.Identity.GetUserId();

            IQueryable<Order> ordersQuery = null;

            if (User.IsInRole(RoleName.CanManage))
            {
               ordersQuery = _uow.GetRepository<Order>().GetAllQuery().Include(o => o.Status)
                .Include(o => o.User);

                if (!String.IsNullOrWhiteSpace(query))
                    ordersQuery = ordersQuery.Where(o => o.UserId.Contains(query));
            }
            else
            {
               ordersQuery = _uow.GetRepository<Order>().GetAllQuery().Include(o => o.Status)
                .Include(o => o.User);

                ordersQuery = ordersQuery.Where(o => o.UserId == id);
            }

            var orderDtos = ordersQuery
                    .ToList()
                    .Select(Mapper.Map<Order, OrderDto>);

            return Ok(orderDtos);
        }

        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            var orderInDb = _uow.GetRepository<Order>().Get(r => r.Id == id);

            if (orderInDb == null)
                return NotFound();

            _uow.GetRepository<Order>().Remove(orderInDb);
            _uow.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ChangeStatus(ChangeStatusViewModel data)
        {
            if (data != null)
            {
                var orderInDb = _uow.GetRepository<Order>().Get(r => r.Id == data.Id);
                if (data.StatusId != 0)
                {
                    orderInDb.StatusId = data.StatusId;
                }
                _uow.SaveChanges();

                return Ok();
            }
            return NotFound();
        }
    }
}