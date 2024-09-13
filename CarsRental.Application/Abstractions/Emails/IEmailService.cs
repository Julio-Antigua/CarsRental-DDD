using CarsRental.Domain.ObjectsValue.Users;

namespace CarsRental.Application.Abstractions.Emails
{
    public interface IEmailService
    {
        Task SendAsync(Email recipent, string subejct, string body);
    }
}
