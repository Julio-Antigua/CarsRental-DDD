using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.ObjectsValue.Rentals
{
    public sealed record DateRange
    {
        private DateRange() { }

        public DateOnly Start { get; init; }
        public DateOnly End { get; init; }

        public int DayCounts => End.DayNumber - Start.DayNumber;

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            if (start > end)
            {
                throw new ApplicationException("The end date is before the start date");
            }

            return new DateRange
            {
                Start = start,
                End = end
            };
        }

    }
}
