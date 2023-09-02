using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_Airline.Models
{
    public class FlightDetail
    {
        public int Id { get; set; }
        public int SourceAirportId { get; set; }
        public Airport SourceAirPort { get; set; }
        public int DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime {get; set; }
        public string  FilghtName { get; set; }
        public string AirlineType { get; set; }
        public ICollection<TicketPrice> TickerPrices { get; set; }
        public ICollection<SeatDetails> SeatDetails { get; set; }
        public ICollection<AddionalFoodService> AddionalFoodServices { get; set; }
    }
}
