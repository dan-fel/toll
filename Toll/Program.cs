using System;
using System.Collections.Generic;
using TollFeeCalculator;
namespace TollStation
{
    public static class TollStationConstants
    {
        public const int MaxTollFeePerDay = 60;
        public const int TollTimeWindowMinutes = 60;
    }
    class Program
    {
        static void Main(string[] args)
        {

            TollDataHandler tollData = new TollDataHandler(TollStationConstants.MaxTollFeePerDay, TollStationConstants.TollTimeWindowMinutes);
            TollCalculator tc = new TollCalculator(in tollData);
            Car car = new Car();

            var res = tc.GetTollFee(new DateTime(2013, 1, 1, 6, 28, 0), car);

            Console.WriteLine(res);
        }
    }
}
