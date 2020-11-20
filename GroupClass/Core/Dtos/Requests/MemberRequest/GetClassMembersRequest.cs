using GroupClass.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.MemberRequest
{
    public class GetClassMembersRequest : IRequest<BaseResponseDto<List<Member>>>
    {
        public int Classid { get; set; }
    }
}
