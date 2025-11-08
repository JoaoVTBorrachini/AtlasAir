using AtlasAir.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtlasAir.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string ReservationCode { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int SeatId { get; set; }
        public int FlightId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = new();

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; } = new();

        [ForeignKey("FlightId")]
        public Flight Flight { get; set; } = new();
    }
}
