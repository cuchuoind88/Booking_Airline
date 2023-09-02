using Booking_Airline.Models;
using Booking_Airline.Repository.ErrorService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Repository.AirportService
{
    public class AirportRepository : IAirportRepository
    {
        public ApplicationDbContext _context;
        public IErrorHandling _errorHandling;
        public AirportRepository( ApplicationDbContext context , IErrorHandling errorHandling)
        {
            _context = context;
            _errorHandling = errorHandling;

        }

        public Task<IActionResult> CreateAirport(Airport airport)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteAirport(int AirportId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAirport(int AirportId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetAllAirport()
        {
            var result  = await _context.Countries
                            .Select(c => new
                            {
                                c.Id,
                                c.contryName,
                                c.countryCode,
                                c.Active,
                                Airports = c.Airports.Select(a => new
                                {
                                    a.Id,
                                    a.AirportName,
                                    a.CountryId,
                                    a.AirportCode,
                                    a.AirportCity
                                }).ToList(),
                                NumberOfAirports = c.Airports.Count
                            })
                            .ToListAsync();

                    return new OkObjectResult(new
                    {
                        Message = "Query successful",
                        Airports = result
                    });
                }

        public Task<IActionResult> UpdateAirport(int AirportId)
        {
            throw new NotImplementedException();
        }
    }
}
