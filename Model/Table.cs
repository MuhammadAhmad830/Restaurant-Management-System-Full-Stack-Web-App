using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResturantManagmentSystemApp.Model
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required]
        public string TableName { get; set; } = string.Empty;

        public int Capacity { get; set; }

        // Live status: Kya table par koi baitha hai?
        public bool IsOccupied { get; set; } = false;

        // Reservation status: Kya ye table future ke liye book hai?
        [Column("IsReserved")]
        public bool IsReserved { get; set; } = false;

        [Column("ReservedFor")]
        public string? ReservedFor { get; set; }

        [Column("ReservationTime")]
        public DateTime? ReservationTime { get; set; }
    }
}