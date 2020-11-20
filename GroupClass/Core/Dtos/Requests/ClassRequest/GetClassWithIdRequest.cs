using GroupClass.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.ClassRequest
{
    public class GetClassWithIdRequest:IRequest<BaseResponseDto<Class>>
    {
        public int Id { get; set; }
    }
}
