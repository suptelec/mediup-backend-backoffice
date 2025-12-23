using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Enums;
public enum UserStatusType
{
    [Display(Name = "En proceso")]
    Inprogress,
    [Display(Name = "Activo")]
    Active,
    [Display(Name = "Inactivo")]
    Inactive,
    [Display(Name = "Eliminado")]
    Deleted
}
