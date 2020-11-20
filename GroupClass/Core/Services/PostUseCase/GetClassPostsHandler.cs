using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.PostRequest;
using GroupClass.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.PostUseCase
{
    public class GetClassPostsHandler : IRequestHandler<GetClassPostsRequest, BaseResponseDto<List<PostDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetClassPostsHandler> _logger;

        public GetClassPostsHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<GetClassPostsHandler> logger
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<BaseResponseDto<List<PostDto>>> Handle(GetClassPostsRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<PostDto>> response = new BaseResponseDto<List<PostDto>>();
            try
            {
                var result = _repositoryWrapper.Post.Where(p => p.Classid == request.Classid)
                    .Include(p => p.Class)
                    .Include(p => p.User)
                    .Select(p => new PostDto(p));
                if (result != null)
                {
                    response.Data = await result.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Üye oluşturulurken bir hata oluştu.");
            }
            return response;
        }
    }
}
