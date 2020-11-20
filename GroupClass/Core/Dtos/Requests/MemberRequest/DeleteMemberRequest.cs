using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.MemberRequest
{
    public class DeleteMemberRequest:IRequest<BaseResponseDto<bool>>
    {
        public int Id { get; set; }
    }
}
