using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public class Car : Vehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}
