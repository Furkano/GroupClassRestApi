using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.UserRequest;
using GroupClass.Core.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.UserUseCase
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, BaseResponseDto<UserDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetUserHandler> _logger;

        public GetUserHandler(IRepositoryWrapper repositoryWrapper, ILogger<GetUserHandler> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<BaseResponseDto<UserDto>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<UserDto> response = new BaseResponseDto<UserDto>();
            try
            {
                var result = await _repositoryWrapper.User.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
                if (result==null)
                {
                    response.Errors.Add("Böyle bir Kullanıcı bulunmamakta.");
                }
                response.Data = result.Adapt<UserDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Kullanıcı oluşturulurken bir hata oluştu.");
            }
            return response;
        }
    }
}
