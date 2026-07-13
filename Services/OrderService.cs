using Microsoft.EntityFrameworkCore;
using ResturantManagmentSystemApp.Model;

namespace ResturantManagmentSystemApp.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        // Sirf AppDbContext hona chahiye filhal
        public OrderService(AppDbContext context)
        {
            _context = context;
        }


        // 🟢 1. Create New Order
        public async Task<Order> CreateOrder(int tableId, string? customerName = null)
        {
            var order = new Order
            {
                TableId = tableId,
                CustomerName = customerName,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.Now, // Changed: OrderDate -> CreatedAt
                IsPaid = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        // 🟡 2. Confirm Order (Kitchen + Table occupied)
        public async Task ConfirmOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Status = OrderStatus.Confirmed;

            var table = await _context.Tables.FindAsync(order.TableId);
            if (table != null)
            {
                table.IsOccupied = true;
            }

            await _context.SaveChangesAsync();
        }

        // 🔵 3. Mark Order as Ready
        public async Task MarkAsReady(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Status = OrderStatus.Ready;

            await _context.SaveChangesAsync();
        }

        // 🔴 4. Complete Order (Table free + paid optional)
        public async Task CompleteOrder(int orderId, bool isPaid = true)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.Status = OrderStatus.Completed;
            order.IsPaid = isPaid;

            var table = await _context.Tables.FindAsync(order.TableId);
            if (table != null)
            {
                table.IsOccupied = false;
            }

            await _context.SaveChangesAsync();
        }

        // 📜 5. Get All Orders (History)
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Table)           // Table details ke liye
                .Include(o => o.Items)           // Order ke items ke liye
                    .ThenInclude(i => i.MenuItem) // HAR ITEM KA NAAM LOAD KARNE KE LIYE (Zaroori)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        // 🔍 6. Get Single Order Detail
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.MenuItem) // ← Ye "Unknown Item" ko theek karega
                .AsNoTracking() // ← Ye memory leak aur crash ko rokne mein madad karta hai
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }

    }
