using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public enum VehicleType
    {
        Car,
        Truck,
        Motorbike,
        Tractor,
        Emergency,
        Diplomat,
        Foreign,
        Military
    }
    public interface Vehicle
    {
        VehicleType GetVehicleType();
    }
}
