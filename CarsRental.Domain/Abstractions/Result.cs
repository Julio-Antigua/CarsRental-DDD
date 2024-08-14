using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Abstractions
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<T> Success<T>(T value)
            => new(value, true, Error.None);

        public static Result<T> Failure<T>(Error error)
            => new(default, false, error);

        public static Result<T> Create<T>(T value)
          => value is not null
            ? Success(value)
            : Failure<T>(Error.NullValue);

    }

   
    public class Result<T> : Result
    {
        private readonly T? _value;

        protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public T? Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The result of the error value is not admissible");

        public static implicit operator Result<T>(T value) => Create(value);
    }


}
