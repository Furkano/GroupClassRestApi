using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.UserRequest
{
    public class LoginUserRequest : IRequest<BaseResponseDto<ResponseLoginDto>>
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
