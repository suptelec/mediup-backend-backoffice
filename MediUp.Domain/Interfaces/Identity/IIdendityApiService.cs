using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces.Identity;
public interface IIdendityApiService
{
    Task<string> GetToken();
}

