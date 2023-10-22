using System.Text.Json.Serialization;

namespace ApiTest.Specflow.Models.Booking
{
    public record Bookings
    {
        [JsonPropertyName("bookingid")] public int BookingId { get; set; }
    }
}
