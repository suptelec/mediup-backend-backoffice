using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Responses;
using MediUp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces.Identity;
public interface IIdendityUserApiService
{
    Task<ResultDto<IdentityUserResponseDto>> GetUser();
    Task<EmptyResultDto> ActivateUser(long id, UserStatusType? userStatus = null);
    Task<EmptyResultDto> InactivateUser(long id);
    Task<EmptyResultDto> DeleteUser(long id);
}

