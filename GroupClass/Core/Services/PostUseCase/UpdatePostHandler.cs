﻿using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.PostRequest;
using GroupClass.Core.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.PostUseCase
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, BaseResponseDto<bool>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<UpdatePostHandler> _logger;
        private readonly IMediator _mediator;
        public UpdatePostHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<UpdatePostHandler> logger,
            IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<BaseResponseDto<bool>> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();
            try
            {
                var post = await _repositoryWrapper.Post.Find(request.Id);
                if (post != null)
                {
                    post.Body = request.Body;
                    post.Title = request.Title;
                    _repositoryWrapper.Post.Update(post);
                    if (await _repositoryWrapper.SaveChangesAsync())
                    {
                        response.Data = true;
                    }
                    else
                    {
                        response.Errors.Add("Veri tabanı kayıt esnasında bir sorun oluştu.");
                    }
                }
                else
                {
                    response.Errors.Add("Böyle bir post bulunamadı.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Post güncellenirken bir hata oluştu.");
            }
            return response;
        }
    
    }
}
