using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.MemberRequest
{
    public class AddMemberRequest : IRequest<BaseResponseDto<MemberDto>>
    {
        public int Classid { get; set; }
        public int Userid { get; set; }
    }
}
