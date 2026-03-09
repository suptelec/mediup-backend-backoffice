using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Dtos;
public class MeasurementSystemMaintenanceScheduleDto
{
    public string Code { get; set; } = null!;
    public string InstallationLocation { get; set; } = null!;
    public int Semester { get; set; }
    public int Month { get; set; }
    public DateTime ScheduledDate { get; set; }
}
