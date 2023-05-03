using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public class Motorbike : Vehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Motorbike;
        }
    }
}
