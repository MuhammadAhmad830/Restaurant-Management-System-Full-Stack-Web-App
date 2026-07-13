using System;
using System.Collections.Generic;

namespace ResturantManagmentSystemApp.Model
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        Ready,
        Served,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        public int TableId { get; set; }
        public Table? Table { get; set; }

        // Date ke liye hum CreatedAt use karenge jo Records page par hai
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsPaid { get; set; } = false;

        public decimal TotalAmount { get; set; } = 0;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string? CustomerName { get; set; }

        // Sirf ek list rakhenge 'Items' ke naam se taake razor page par error na aaye
        public List<OrderItem> Items { get; set; } = new();
    }
}