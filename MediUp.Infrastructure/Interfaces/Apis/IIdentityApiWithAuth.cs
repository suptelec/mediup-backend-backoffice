using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Responses;
using MediUp.Domain.Enums;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Interfaces.Apis;
[Headers("Authorization: Bearer")]
public interface IIdentityApiWithAuth
{
    [Get("/api/Users")]
    Task<ResultDto<IdentityUserResponseDto>> GetUser();

    [Post("/api/Users/{id}/ActivateUser")]
    Task<EmptyResultDto> ActivateUserAsync([AliasAs("id")] long userId, [Query] UserStatusType? userStatus = null);

    [Post("/api/Users/{id}/InactivateUser")]
    Task<EmptyResultDto> InactivateUser(long id);

    [Delete("/api/Users/DeleteUser")]
    Task<EmptyResultDto> DeleteUser(long id);

}
