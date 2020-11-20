using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.PostRequest;
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
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponseDto<PostDto>> Post([FromBody] CreatePostRequest createPostRequest)
        {
            BaseResponseDto<PostDto> response = await _mediator.Send(createPostRequest);
            return response;
        }
        [HttpPut]
        public  async Task<BaseResponseDto<bool>> UpdatePostAsync([FromBody] UpdatePostRequest updatePostRequest)
        {
            BaseResponseDto<bool> response = await _mediator.Send(updatePostRequest);
            return response;
        }
        [HttpDelete("{id}")]
        
        public async Task<BaseResponseDto<bool>> DeletePostAsync([FromRoute] int id)
        {
            DeletePostRequest deletePostRequest = new DeletePostRequest();
            deletePostRequest.Id = id;
            BaseResponseDto<bool> response = await _mediator.Send(deletePostRequest);
            return response;
        }
        [HttpGet("GetClassPosts/{Classid}")]
        public async Task<BaseResponseDto<List<PostDto>>> GetClassPostsAsync([FromRoute]int Classid)
        {
            GetClassPostsRequest getClassPostsRequest = new GetClassPostsRequest();
            getClassPostsRequest.Classid = Classid;
            BaseResponseDto<List<PostDto>> response = await _mediator.Send(getClassPostsRequest);
            return response;
        }
    }
}
