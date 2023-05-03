using System;
using System.Collections.Generic;
using TollFeeCalculator;
namespace TollStation
{
    public static class TollStationConstants
    {
        public const int maxTollFeePerDay = 60;
        public const int tollTimeWindowMinutes = 60;
    }
    class Program
    {
        static void Main(string[] args)
        {

            TollDataHandler tollData = new TollDataHandler(TollStationConstants.maxTollFeePerDay, TollStationConstants.tollTimeWindowMinutes);
            TollCalculator tc = new TollCalculator(in tollData);
            Car car = new Car();

            var res = tc.GetTollFee(new DateTime(2013, 1, 1, 6, 28, 0), car);
        }
    }
}
