using MediUp.Domain.Enums;

namespace MediUp.Domain.Entities;
public class EnergyMeasurementRegister : BaseEntity
{
    public EnergyMeasurementRegisterType Type { get; set; }

    public string Name { get; set; } = null!;

    public string? UnitOfMeasure { get; set; }
}
