namespace CarsharingSystem.Model;

public interface IRenter
{
    public void AddOfferReview(OfferReview offerReview);

    public bool RemoveOfferReview(OfferReview review);

    public void AddBooking(Booking booking);

    public bool RemoveBooking(Booking booking);

    public void DeleteRenter();
}