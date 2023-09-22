using Booking_Airline.DTOs.AirportDTOs;
using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Repository.CountriesRepository
{
    public class ContryRepository : RepositoryBase<Country> , ICountryRepository
    {
        public ContryRepository(ApplicationDbContext context) : base(context) { }

        public void CreateCountry(Country country)
        {
             Create(country);
        }

        public async Task<List<CountryWithAirportsDTO>> GetAllCountryWithAirport()
        {
            return await RepositoryContext.Set<Country>()
          .Select(c => new CountryWithAirportsDTO(
              c.Id,
              c.contryName,
              c.countryCode,
              c.Airports.Select(a => new AirportDTO(
                  a.Id,
                  a.AirportName,
                  a.AirportCode,
                  a.AirportCity
              )).ToList(),
              c.Airports.Count
          ))
          .ToListAsync();
        }

        
    }
}
