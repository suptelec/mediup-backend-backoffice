using MediUp.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Application.Interfaces;
public interface IValidatorService
{
    EmptyResultDto Validate<TRequest>(TRequest dto, [CallerMemberName] string callerMemberName = "");
    Task<EmptyResultDto> ValidateAsync<TRequest>(TRequest dto, [CallerMemberName] string callerMemberName = "");

}
