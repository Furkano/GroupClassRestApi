using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.PostRequest
{
    public class GetClassPostsRequest : IRequest<BaseResponseDto<List<PostDto>>>
    {
        public int Classid { get; set; }
    }
}
