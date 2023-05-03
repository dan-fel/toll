using TollFeeCalculator;
namespace TollCalcTest;

[TestClass]
public class TollFeeCalculatorTest
{
    private const int _maxTollFeePerDay = 60;
    private const int _tollTimeWindowMinutes = 60;

    private readonly TollDataHandler _tollData = new TollDataHandler(_maxTollFeePerDay, _tollTimeWindowMinutes);

    public TollFeeCalculatorTest()
    {

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
        List<DateTime> dateTimes = new List<DateTime>();
        Car car = new Car();

        dateTimes.Add(new DateTime(2013, 2, 1, 7, 20, 0));
        dateTimes.Add(new DateTime(2013, 2, 1, 7, 30, 0));
        dateTimes.Add(new DateTime(2013, 2, 1, 7, 40, 0));
        dateTimes.Add(new DateTime(2013, 2, 1, 8, 3, 0));

        Assert.AreEqual(18, tc.GetTotalTollFeeForDay(car, in dateTimes));

    }
}
