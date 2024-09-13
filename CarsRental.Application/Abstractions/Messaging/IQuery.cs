using CarsRental.Domain.Abstractions;
using MediatR;

namespace CarsRental.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
