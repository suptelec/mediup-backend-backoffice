using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Enums;
public enum EnergyMeasurementRegisterType
{
    ActiveEnergyDelivered = 1,
    ActiveEnergyReceived = 2,
    ReactiveEnergyDelivered = 3,
    ReactiveEnergyReceived = 4,
    ApparentEnergyDelivered = 5,
    IntegrationPeriod = 6,
    AverageVoltage = 7,
    Frequency = 8
}
