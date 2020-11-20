using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.PostRequest
{
    public class DeletePostRequest : IRequest<BaseResponseDto<bool>>
    {
        public int Id { get; set; }
    }
}
