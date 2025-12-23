using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces;
public interface ICurrentLoggedUser
{
    long Id { get; }
    string UserName { get; }
    string Email { get; }
    string FullName { get; }
    string? IpAddress { get; }
}
