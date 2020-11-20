using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
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
    public class ClassesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<BaseResponseDto<ClassDto>> CreateClassAsync(CreateClassRequest createClassRequest)
        {
            BaseResponseDto<ClassDto> response = await _mediator.Send(createClassRequest);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponseDto<bool>> DeleteClassAsync([FromRoute] int id)
        {
            DeleteClassRequest deleteClassRequest = new DeleteClassRequest();
            deleteClassRequest.Id = id;
            BaseResponseDto<bool> response = await _mediator.Send(deleteClassRequest);
            return response;
        }

        [HttpGet("GetClassWithCode/{AlphaNumericCode}")]
        public async Task<BaseResponseDto<ClassDto>> GetClassWithCodeAsync([FromRoute] string AlphaNumericCode)
        {
            GetClassWithCodeRequest getClassWithCodeRequest = new GetClassWithCodeRequest();
            getClassWithCodeRequest.AlphaNumericCode = AlphaNumericCode;
            BaseResponseDto<ClassDto> response = await _mediator.Send(getClassWithCodeRequest);
            return response;
        }

        [HttpGet("GetClassWithEducationYear/{EducationYear}")]
        public async Task<BaseResponseDto<List<Class>>> GetClassWithEducationYearAsync([FromRoute] string EducationYear)
        {
            GetClassWithEducationYearRequest getClassWithEducationYearRequest = new GetClassWithEducationYearRequest();
            getClassWithEducationYearRequest.EducationYear = EducationYear;
            BaseResponseDto<List<Class>> response = await _mediator.Send(getClassWithEducationYearRequest);
            return response;
        }

        [HttpGet("GetUserClass/{Userid}")]
        public async Task<BaseResponseDto<List<Class>>> GetUserClass([FromRoute] int Userid)
        {
            GetUserClassRequest getUserClassRequest = new GetUserClassRequest();
            getUserClassRequest.Userid = Userid;
            BaseResponseDto<List<Class>> response = await _mediator.Send(getUserClassRequest);
            return response;
        }
        [HttpGet("GetClassWithId/{id}")]
        public async Task<BaseResponseDto<Class>> GetClassWithIdAsync([FromRoute] int id)
        {
            GetClassWithIdRequest getClassWithIdRequest = new GetClassWithIdRequest();
            getClassWithIdRequest.Id = id;
            BaseResponseDto<Class> response = await _mediator.Send(getClassWithIdRequest);
            return response;
        }
    }
}
