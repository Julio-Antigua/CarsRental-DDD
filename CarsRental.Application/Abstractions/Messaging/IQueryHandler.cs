using CarsRental.Domain.Abstractions;
using MediatR;

namespace CarsRental.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    {

    }
}
