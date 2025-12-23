using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Requests;
using MediUp.Domain.Dtos.Identity.User.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Interfaces.Apis;
[Headers("Authorization: Bearer")]
public interface IIdentityUsersApi
{
    [Post("/api/Users")]
    Task<ResultDto<UserResponseDto>> CreateUser([Body] CreateUserRequestDto dto);

}
