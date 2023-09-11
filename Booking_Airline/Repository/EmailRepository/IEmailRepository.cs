

using Booking_Airline.DTOs;

namespace Booking_Airline.Repository.EmailRepository
{
    public interface IEmailRepository
    {
        public void SendEmail(EmailDTO request);
    }
}
