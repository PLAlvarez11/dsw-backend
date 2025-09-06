using MessageApi.Data;
using HelloApi.Models;
using HelloApi.Models.DTOs;
using MessageApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Services
{
    public interface IOrderService
    {
        Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto);
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly AppDbContext _context;

        public OrderService(IOrderRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<OrderReadDto> CreateOrderAsync(OrderCreateDto dto)
        {
            var person = await _context.Persons.FindAsync(dto.PersonId);
            if (person == null)
                throw new Exception("Persona no encontrada");

            var order = new Order
            {
                PersonId = dto.PersonId,
                OrderDate = DateTime.Now,
                OrderDetails = dto.Details.Select(d => new OrderDetail
                {
                    ItemId = d.ItemId,
                    Quantity = d.Quantity
                }).ToList()
            };

            await _repo.AddOrderAsync(order);

            return new OrderReadDto
            {
                Id = order.Id,
                PersonName = $"{person.FirstName} {person.LastName}",
                OrderDate = order.OrderDate,
                Details = order.OrderDetails.Select(od => new OrderDetailReadDto
                {
                    ItemName = _context.Items.First(i => i.Id == od.ItemId).Name,
                    Quantity = od.Quantity
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _repo.GetAllOrdersAsync();
            return orders.Select(o => new OrderReadDto
            {
                Id = o.Id,
                PersonName = $"{o.Person.FirstName} {o.Person.LastName}",
                OrderDate = o.OrderDate,
                Details = o.OrderDetails.Select(od => new OrderDetailReadDto
                {
                    ItemName = od.Item.Name,
                    Quantity = od.Quantity
                }).ToList()
            });
        }
    }
}
