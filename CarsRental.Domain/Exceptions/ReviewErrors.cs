using CarsRental.Domain.Abstractions;

namespace CarsRental.Domain.Exceptions
{
    public static class ReviewErrors
    {
       public static readonly Error NotEligible = new(
            "Review.NotEligible",
            "This review and rating for the car is not eligible because it is not yet completed."
       );
    }
}
