using System;
using System.Globalization;
using System.Linq;
using TollFeeCalculator;

public class TollCalculator
{
    public TollCalculator(in TollDataHandler tollData)
    {
        _tollData = tollData;
    }

    /**
     * Calculate the total toll fee for one day.
     * This function assumes the dates are in chronological order.
     *
     * @param vehicle - The vehicle
     * @param dates   - Date and time of all passes on one day
     * @return - The total toll fee for that day
     */
    public int GetTotalTollFeeForDay(in Vehicle vehicle, in List<DateTime> dates)
    {
        if (dates == null || dates.Count == 0) return 0;

        int totalFee = 0;

        DateTime prevTollVisit = dates[0];

        foreach (var tollVisit in dates)
        {
            if (totalFee >= _tollData.GetMaxTollFeePerDay())
            {
                return _tollData.GetMaxTollFeePerDay();
            }

            var prevFee = GetTollFee(prevTollVisit, vehicle);
            var currFee = GetTollFee(tollVisit, vehicle);

            if (vehicleRecentlyPassedToll(prevTollVisit, tollVisit))
            {
                if (totalFee > 0) totalFee -= prevFee;
                if (currFee >= prevFee) prevFee = currFee;
                totalFee += prevFee;
            }
            else
            {
                totalFee += currFee;
            }

            prevTollVisit = tollVisit;
        }
        return totalFee;
    }
    public bool vehicleRecentlyPassedToll(DateTime firstVisit, DateTime secondVisit)
    {
        return (firstVisit.Date == secondVisit.Date) && Math.Abs((firstVisit - secondVisit).TotalMinutes) < _tollData.GetTollVisitTimeWindow(); ;
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _tollData.GetTollFreeVehicles().Contains(vehicle.GetVehicleType());
    }

    public int GetTollFee(DateTime date, in Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        TimeSpan CurrentTime = new TimeSpan(date.Hour, date.Minute, 0);

        foreach (var timeSpan in _tollData.GetHourlyTollFeesMap())
        {
            if (timeSpan.Key.Item1 <= CurrentTime && CurrentTime <= timeSpan.Key.Item2)
            {
                return timeSpan.Value;
            }
        }
        return 0;
    }

    public Boolean IsTollFreeDate(DateTime date)
    {
        Boolean IsWeekendDay = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        if (_tollData.GetTollFreeDates().Contains(date.Date) || IsWeekendDay) return true;
        return false;
    }

    private readonly TollDataHandler _tollData;
}
