using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class MstUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    [JsonIgnore]
    public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    [JsonIgnore]
    public virtual ICollection<MstCustomer> MstCustomers { get; set; } = new List<MstCustomer>();
    [JsonIgnore]
    public virtual ICollection<MstDestination> MstDestinations { get; set; } = new List<MstDestination>();
    [JsonIgnore]
    public virtual ICollection<MstPackage> MstPackages { get; set; } = new List<MstPackage>();
    [JsonIgnore]
    public virtual ICollection<MstTraveler> MstTravelers { get; set; } = new List<MstTraveler>();
    [JsonIgnore]
    public virtual ICollection<PackageDestination> PackageDestinations { get; set; } = new List<PackageDestination>();
    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
