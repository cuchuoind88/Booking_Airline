using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_Airline.Models
{
    public class FlightDetail
    {
        public Guid Id { get; set; }
        public Guid SourceAirportId { get; set; }
        public Airport SourceAirPort { get; set; }
        public Guid DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime {get; set; }
        public string  FilghtName { get; set; }
        public string AirlineType { get; set; }
        public ICollection<TicketPrice> TicketPrices { get; set; }
        public ICollection<SeatDetails> SeatDetails { get; set; }
        public ICollection<AddionalFoodService> AddionalFoodServices { get; set; }
    }
}
