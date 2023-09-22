using Booking_Airline.DTOs.CountryDTOs;
using Booking_Airline.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Booking_Airline.Repository.CountriesRepository
{
    public interface ICountryRepository
    {
       
        public Task<List<CountryWithAirportsDTO>> GetAllCountryWithAirport();
        public void CreateCountry(Country country);
    }
}
