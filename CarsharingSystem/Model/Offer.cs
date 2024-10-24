﻿using CarsharingSystem.Abstractions;

namespace CarsharingSystem.Model;

public class Offer : ClassExtent<Offer>
{
    public double PricePerDay { get; set; }
    public string Description { get; set; }
    public int? MinimalAge { get; set; }
    public List<OfferReview> OfferReviews { get; set; } = new List<OfferReview>();
    public List<Address> Addresses { get; set; } = new List<Address>();// Have to ensure that the address is not null
    public List<Booking> Bookings { get; set; } = new List<Booking>();
    public Vehicle Vehicle { get; set; }
}