using TollFeeCalculator;
namespace TollCalcTest;

[TestClass]
public class TollFeeCalculatorTest
{
    private const int _maxTollFeePerDay = 60;
    private const int _tollTimeWindowMinutes = 60;

    private readonly TollDataHandler _tollData = new TollDataHandler(_maxTollFeePerDay, _tollTimeWindowMinutes);

    [TestMethod]
    public void TollFreeDatesShallBeFree()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        Car car = new Car();

        foreach (var date in _tollData.GetTollFreeDates())
        {
            Assert.AreEqual(true, tc.IsTollFreeDate(date));
        }
    }

    [TestMethod]
    public void CarOnTollFreeDateShallNotPayTollFee()
    {

        TollCalculator tc = new TollCalculator(in _tollData);
        Car car = new Car();

        foreach (var date in _tollData.GetTollFreeDates())
        {
            Console.WriteLine(date);
            Assert.AreEqual(tc.GetTollFee(date, car), 0);
        }
    }

    [TestMethod]
    public void MotorBikeShallNotPayTollFee()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        // Ideally we would create all the different types here and test.
        Motorbike mb = new Motorbike();

        foreach (var date in _tollData.GetHourlyTollFeesMap().Keys)
        {
            Console.WriteLine(date);
            Assert.AreEqual(tc.GetTollFee(new DateTime(2013, 3, 28, 12, 38, 0), mb), 0);
        }
    }

    [TestMethod]
    public void CarEntersSeveralTollsWithinAnHourShallOnlyPayMaxFeeInThatHour()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = new List<DateTime>();
        Car car = new Car();

        tollVisitTimes.Add(new DateTime(2013, 2, 1, 7, 20, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 7, 30, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 7, 40, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 8, 3, 0));

        Assert.AreEqual(18, tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void CarEntersSeveralTollsOverDayShallPayMaxFee()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = new List<DateTime>();
        Car car = new Car();

        tollVisitTimes.Add(new DateTime(2013, 2, 1, 7, 20, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 9, 30, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 11, 40, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 14, 3, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 15, 3, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 16, 3, 0));

        Assert.AreEqual(_tollData.GetMaxTollFeePerDay(), tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void CarEntersSeveralTollsOverDayShallPayAccumulatedFee()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = new List<DateTime>();
        Car car = new Car();

        tollVisitTimes.Add(new DateTime(2013, 2, 1, 7, 20, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 11, 40, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 15, 3, 0));
        tollVisitTimes.Add(new DateTime(2013, 2, 1, 16, 3, 0));

        var accumulatedFeeCost = tc.GetTollFee(tollVisitTimes[0], car) +
                                tc.GetTollFee(tollVisitTimes[1], car) +
                                tc.GetTollFee(tollVisitTimes[2], car) +
                                tc.GetTollFee(tollVisitTimes[3], car);

        Assert.AreEqual(accumulatedFeeCost, tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void VehicleWithNoTollVisitsShallNotPayAnyFee()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = new List<DateTime>();
        Car car = new Car();

        Assert.AreEqual(0, tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void NullTollVisitsShallReturnZero()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = null!;
        Car car = new Car();

        Assert.AreEqual(0, tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void CarWithTwoVisitsOnExactSameTimeShallOnlyPayOnce()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        List<DateTime> tollVisitTimes = new List<DateTime>(); ;
        Car car = new Car();

        var visit = new DateTime(2013, 2, 1, 7, 20, 0);
        tollVisitTimes.Add(visit);
        tollVisitTimes.Add(visit);

        Assert.AreEqual(tc.GetTollFee(visit, car), tc.GetTotalTollFeeForDay(car, in tollVisitTimes));
    }

    [TestMethod]
    public void TollFreeVehiclesShallNotPayTollFee()
    {
        TollCalculator tc = new TollCalculator(in _tollData);
        var visit = new DateTime(2013, 2, 1, 7, 20, 0);

        // .. use each Vehicle type to test.
    }

}
