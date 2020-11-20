using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.UserRequest
{
    public class CreateUserRequest:IRequest<BaseResponseDto<UserDto>>
    {
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string SchoolNumber { get; set; }
    }
}
