using GroupClass.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.ClassRequest
{
    public class GetClassWithEducationYearRequest : IRequest<BaseResponseDto<List<Class>>>
    {
        public string EducationYear { get; set; }
    }
}
