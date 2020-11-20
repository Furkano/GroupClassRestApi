using GroupClass.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos
{
    public class ResponseLoginDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }

        public ResponseLoginDto()
        {
            User = new UserDto();
        }
    }
}
