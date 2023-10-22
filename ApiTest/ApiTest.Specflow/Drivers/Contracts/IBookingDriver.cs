using ApiTest.Specflow.Models.Booking;
using RestSharp;

namespace ApiTest.Specflow.Drivers.Contracts
{
    public interface IBookingDriver
    {
        Task<RestResponse> GetBookingById(int id);

        Task<RestResponse> GetBookingId();
    }
}
