using System.Text.Json.Serialization;

namespace ResturantManagmentSystemApp.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore] // ← Ye sabse zaroori hai, ye loop ko break karega
        public Order? Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string MenuItemName => MenuItem?.Name ?? "Unknown Item";
    }
}