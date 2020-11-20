using GroupClass.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.UserRequest
{
    public class GetUserRequest: IRequest<BaseResponseDto<UserDto>>
    {
        public int Id { get; set; }
    }
}
