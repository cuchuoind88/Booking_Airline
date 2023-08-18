using Booking_Airline.DTOs;

namespace Booking_Airline.Repository.EmailService
{
    public interface IEmailRepository
    {
        public void SendEmail(EmailDTO request);
    }
}
