//using Booking_Airline.Models;

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Booking_Airline.Repository.AirportService
//{
//    public class AirportRepository : IAirportRepository
//    {
//        public ApplicationDbContext _context;
//        public IErrorHandling _errorHandling;
//        public AirportRepository(ApplicationDbContext context, IErrorHandling errorHandling)
//        {
//            _context = context;
//            _errorHandling = errorHandling;

//        }

//        public async Task<IActionResult> CreateAirport(Airport airport)
//        {
//            try
//            {
//                _context.Airports.Add(airport);
//                await _context.SaveChangesAsync();
//                return new OkObjectResult(new
//                {
//                    Message = "Create Airport successfull <3"
//                });
//            }
//            catch (Exception ex)
//            {
//                // Xử lý lỗi nếu có
//                return _errorHandling.GetBadRequestResult($"Error: {ex.Message}", 500);
//            }

//        }

//        public async Task<IActionResult> DeleteAirport(int AirportId)
//        {
//            try
//            {
//                var airportToDelete = await _context.Airports.FindAsync(AirportId);

//                if (airportToDelete == null)
//                {
//                    // Sân bay không tồn tại, trả về lỗi
//                    return _errorHandling.GetBadRequestResult("Airport Not Fond", 404);
//                }
//                // Tìm tất cả các FlightDetail có SourceAirportId hoặc DestinationAirportId trùng với AirportId
//                var relatedFlightDetails = await _context.FlightDetails
//                    .Where(fd => fd.SourceAirportId == AirportId || fd.DestinationAirportId == AirportId)
//                    .ToListAsync();
//                // Xóa tất cả các FlightDetail liên quan
//                _context.FlightDetails.RemoveRange(relatedFlightDetails);

//                // Xóa sân bay
//                _context.Airports.Remove(airportToDelete);

//                await _context.SaveChangesAsync();
//                return new OkObjectResult(new
//                {
//                    Message = "No Content "
//                });
//            }
//            catch (Exception ex)
//            {
//                return _errorHandling.GetBadRequestResult($"Error: {ex.Message}", 500);
//            }

//        }

//        public async Task<IActionResult> GetAirport(int AirportId)
//        {
//            try
//            {
//                var airport = await _context.Airports.FindAsync(AirportId);

//                if (airport == null)
//                {
//                    // Sân bay không tồn tại, trả về lỗi
//                    return _errorHandling.GetBadRequestResult("Airport Not Fond", 404);
//                }
//                return new JsonResult(new
//                {
//                    Message = "Get Airport successfull <3",
//                    Airport = airport
//                });
//            }
//            catch (Exception ex)
//            {
//                return _errorHandling.GetBadRequestResult($"Error: {ex.Message}", 500);
//            }
//        }

//        public async Task<IActionResult> GetAllAirport()
//        {   //Projection query
//            var result = await _context.Countries
//                            .Select(c => new
//                            {
//                                c.Id,
//                                c.contryName,
//                                c.countryCode,
//                                Airports = c.Airports.Select(a => new
//                                {
//                                    a.Id,
//                                    a.AirportName,
//                                    a.CountryId,
//                                    a.AirportCode,
//                                    a.AirportCity
//                                }).ToList(),
//                                NumberOfAirports = c.Airports.Count
//                            })
//                            .ToListAsync();

//            return new OkObjectResult(new
//            {
//                Message = "Query successful",
//                Airports = result
//            });
//        }

//        public async Task<IActionResult> UpdateAirport(int AirportId, Airport updatedAirport)
//        {
//            try
//            {
//                // Tìm sân bay cần cập nhật dựa trên AirportId
//                var existingAirport = await _context.Airports.FindAsync(AirportId);

//                if (existingAirport == null)
//                {
//                    // Sân bay không tồn tại, trả về lỗi
//                    return _errorHandling.GetBadRequestResult("Airport Not Fond", 404);
//                }

//                // Cập nhật thông tin của sân bay từ updatedAirport
//                existingAirport.AirportName = updatedAirport.AirportName;
//                //existingAirport.CountryId = updatedAirport.CountryId;
//                //existingAirport.AirportCode = updatedAirport.AirportCode;
//                existingAirport.AirportCity = updatedAirport.AirportCity;

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();

//                return new OkObjectResult(new
//                {
//                    Message = "Query successful",
//                    Airports = existingAirport
//                });
//            }
//            catch (Exception ex)
//            {
//                // Xử lý lỗi nếu có
//                return _errorHandling.GetBadRequestResult($"Error: {ex.Message}", 500);
//            }
//        }
//    }
//}
