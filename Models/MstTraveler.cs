using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class MstTraveler
{
    public int TravelerId { get; set; }

    public int BookingId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string? PassportNumber { get; set; }

    public DateOnly? PassportExpiryDate { get; set; }

    public string EmergencyContactName { get; set; } = null!;

    public string EmergencyContactNumber { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual Booking? Booking { get; set; } = null!;
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
