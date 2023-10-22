using System.Diagnostics;
using ApiTest.Specflow.Drivers.Contracts;
using ApiTest.Specflow.Models.Booking;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow.Assist;

namespace ApiTest.Specflow.StepDefinitions
{
    [Binding]
    public sealed class BookingSteps : Steps
    {
        private readonly IBookingDriver bookingDriver;

        private RestResponse restResponse;
        private List<Bookings> bookingIds;

        public BookingSteps(IBookingDriver bookingDriver)
        {
            this.bookingDriver = bookingDriver;
            restResponse = null!;
        }

        [Given(@"I get booking id")]
        public async Task GivenIGetBookingId()
        {
            var response = await bookingDriver.GetBookingId();
            response.IsSuccessStatusCode.Should().BeTrue();
            bookingIds = JsonConvert.DeserializeObject<List<Bookings>>(response.Content);
        }
        
        [When(@"I get booking by id '(.*)'")]
        public async Task WhenIGetBookingById(int id)
        {
            restResponse = await bookingDriver.GetBookingById(id);
            restResponse.IsSuccessStatusCode.Should().BeTrue();
        }

        [Then(@"The result contains:")]
        public void ThenTheResultContains(Table table)
        {
            var expectedBooking = table.CreateInstance<Booking>();

            var booking = JsonConvert.DeserializeObject<Booking>(restResponse.Content);
            
            booking.Should().NotBeNull();
            expectedBooking.FirstName.Should().BeEquivalentTo(booking.FirstName);
            expectedBooking.LastName.Should().BeEquivalentTo(booking.LastName);
            expectedBooking.TotalPrice.Should().Be(booking.TotalPrice);
        }
    }
}