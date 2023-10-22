using ApiTest.Specflow.Drivers.Contracts;
using ApiTest.Specflow.Models.Booking;
using RestSharp;
using Xunit.Abstractions;

namespace ApiTest.Specflow.Drivers
{
    public class BookingDriver : DriverBase, IBookingDriver
    {
        private readonly string baseUrl;

        public BookingDriver(ITestOutputHelper testOutputHelper, string baseUrl, string username, string password) : base(testOutputHelper, baseUrl, username, password)
        {
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Get booking by id
        /// GET - {TestApi}/booking/:id
        /// </summary>
        public async Task<RestResponse> GetBookingById(int id)
        {
            var url = $"{baseUrl}/booking/{id}";

            var response = await ExecuteGetAsync(url);

            return response;
        }

        /// <summary>
        /// Get booking by id
        /// GET - {TestApi}/booking/:id
        /// </summary>
        public async Task<RestResponse> GetBookingId()
        {
            var url = $"{baseUrl}/booking";

            var response = await ExecuteGetAsync(url);

            return response;
        }

    }
}
