using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Dtos.Requests.PostRequest
{
    public class CreatePostRequest : IRequest<BaseResponseDto<PostDto>>
    {
        public int Userid { get; set; }
        public int Classid { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
