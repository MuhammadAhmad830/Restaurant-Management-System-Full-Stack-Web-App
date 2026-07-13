using System.ComponentModel.DataAnnotations;

namespace ResturantManagmentSystemApp.Model
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; } = string.Empty; // e.g. Chicken, Flour, Oil

        [Required]
        public double Quantity { get; set; } // Mojooda stock

        [Required]
        public string Unit { get; set; } = "kg"; // kg, Liter, Packet

        [Required]
        public double MinStockLevel { get; set; } // Alert limit (e.g. 5kg)

        public decimal UnitPrice { get; set; } // Khareedari ka rate
    }
}