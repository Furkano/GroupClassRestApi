using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.PostRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.PostUseCase
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, BaseResponseDto<PostDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<CreatePostHandler> _logger;
        private readonly IMediator _mediator;
        public CreatePostHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<CreatePostHandler> logger,
            IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<PostDto>> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<PostDto> response = new BaseResponseDto<PostDto>();
            try
            {
                var post = new Post
                {
                    Body = request.Body,
                    Classid = request.Classid,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Title = request.Title,
                    Userid = request.Userid
                };
                await _repositoryWrapper.Post.Create(post);
                if (await _repositoryWrapper.SaveChangesAsync())
                {
                    response.Data = post.Adapt<PostDto>();
                }
                else
                {
                    response.Errors.Add("Veri tabanı kayıt esnasında bir sorun oluştu.");
                }
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Post oluşturulurken bir hata oluştu.");
            }
            return response;
        }
    }
}
