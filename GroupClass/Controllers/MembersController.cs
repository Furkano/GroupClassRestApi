using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.MemberRequest;
using GroupClass.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponseDto<MemberDto>> AddMemberAsync([FromBody] AddMemberRequest addMemberRequest)
        {
            BaseResponseDto<MemberDto> response = await _mediator.Send(addMemberRequest);
            return response;
        }
        [HttpDelete("{id}")]
        public async Task<BaseResponseDto<bool>> DeleteMemberAsync([FromRoute] int id)
        {
            DeleteMemberRequest deleteMemberRequest = new DeleteMemberRequest();
            deleteMemberRequest.Id = id;
            BaseResponseDto<bool> response = await _mediator.Send(deleteMemberRequest);
            return response;
        }

        [HttpGet("GetClassMembers/{Classid}")]
        public async Task<BaseResponseDto<List<Member>>> GetClassMembersAsync([FromRoute]int Classid)
        {
            GetClassMembersRequest getClassMembersRequest = new GetClassMembersRequest();
            getClassMembersRequest.Classid = Classid;
            BaseResponseDto<List<Member>> response = await _mediator.Send(getClassMembersRequest);
            return response;
        }
    }
}
