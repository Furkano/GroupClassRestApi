using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.ClassRequest
{
    public class GetClassWithCodeRequest : IRequest<BaseResponseDto<ClassDto>>
    {
        public string AlphaNumericCode { get; set; }
    }
}
