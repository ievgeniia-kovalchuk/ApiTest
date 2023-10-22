using System.Text.Json.Serialization;

namespace ApiTest.Specflow.Models.Booking
{
    public record Booking
    {
        [JsonPropertyName("firstname")] public string FirstName { get; init; }

        [JsonPropertyName("lastname")] public string LastName { get; init; }

        [JsonPropertyName("totalprice")] public float TotalPrice { get; init; }

        [JsonPropertyName("depositpaid")] public bool DepositPaid { get; init; }

        [JsonPropertyName("bookingdates")] public BookingDates BookingDates { get; init; }

        [JsonPropertyName("additionalneeds")] public string AdditionalNeeds { get; init; }

    }

    public record BookingDates
    {
        [JsonPropertyName("checkin")] public string Checkin { get; set; }

        [JsonPropertyName("checkout")] public string Checkout { get; set; }
    }
}
