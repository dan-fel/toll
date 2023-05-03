namespace TollFeeCalculator
{
    using HourlyTollFeesMap = Dictionary<Tuple<TimeSpan, TimeSpan>, int>;
    using TollHourSpan = Tuple<TimeSpan, TimeSpan>;
    public class TollDataHandler
    {
        public TollDataHandler(int maxTollFeePerDay, int tollVisitTimeWindow)
        {
            _maxTollFeePerDay = maxTollFeePerDay;
            _tollVisitTimeWindow = tollVisitTimeWindow;
            _tollFreeDates = LoadTollFreeDates();
            _hourlyTollFees = LoadTollFees();
            _tollFreeVehicles = LoadTollFreeVehicles();
        }

        public int GetMaxTollFeePerDay()
        {
            return _maxTollFeePerDay;
        }

        public int GetTollVisitTimeWindow()
        {
            return _tollVisitTimeWindow;
        }

        public ref HourlyTollFeesMap GetHourlyTollFeesMap()
        {
            return ref _hourlyTollFees;
        }

        public ref List<DateTime> GetTollFreeDates()
        {
            return ref _tollFreeDates;
        }

        public ref HashSet<VehicleType> GetTollFreeVehicles()
        {
            return ref _tollFreeVehicles;
        }

        private List<DateTime> LoadTollFreeDates()
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Add(new DateTime(2013, 1, 1));
            dates.Add(new DateTime(2013, 3, 28));
            dates.Add(new DateTime(2013, 3, 29));
            dates.Add(new DateTime(2013, 4, 1));
            dates.Add(new DateTime(2013, 4, 30));
            dates.Add(new DateTime(2013, 5, 1));
            dates.Add(new DateTime(2013, 5, 8));
            dates.Add(new DateTime(2013, 5, 9));
            dates.Add(new DateTime(2013, 6, 5));
            dates.Add(new DateTime(2013, 6, 6));
            dates.Add(new DateTime(2013, 7, 21));
            int daysInJuly = DateTime.DaysInMonth(2013, 7);
            for (int i = 1; i <= daysInJuly; i++)
            {
                dates.Add(new DateTime(2013, 7, i));
            }
            dates.Add(new DateTime(2013, 11, 1));
            dates.Add(new DateTime(2013, 12, 24));
            dates.Add(new DateTime(2013, 12, 25));
            dates.Add(new DateTime(2013, 12, 26));
            dates.Add(new DateTime(2013, 12, 31));

            return dates;
        }

        private HourlyTollFeesMap LoadTollFees()
        {
            HourlyTollFeesMap hourlyTollFees = new HourlyTollFeesMap();
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0)), 8);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0)), 13);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0)), 18);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0)), 13);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0)), 8);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0)), 13);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0)), 18);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0)), 13);
            hourlyTollFees.Add(new TollHourSpan(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0)), 8);

            return hourlyTollFees;
        }

        private HashSet<VehicleType> LoadTollFreeVehicles()
        {
            var tollFreeVehicleTypes = new HashSet<VehicleType>
            {
            VehicleType.Motorbike,
            VehicleType.Tractor,
            VehicleType.Emergency,
            VehicleType.Diplomat,
            VehicleType.Foreign,
            VehicleType.Military
            };

            return tollFreeVehicleTypes;

        }


        private readonly int _maxTollFeePerDay;
        private readonly int _tollVisitTimeWindow;
        private HashSet<VehicleType> _tollFreeVehicles;
        private List<DateTime> _tollFreeDates;
        private HourlyTollFeesMap _hourlyTollFees;
    }
}
