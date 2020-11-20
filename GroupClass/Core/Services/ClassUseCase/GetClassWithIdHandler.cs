using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.ClassUseCase
{
    public class GetClassWithIdHandler : IRequestHandler<GetClassWithIdRequest, BaseResponseDto<Class>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetClassWithIdHandler> _logger;
        public GetClassWithIdHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<GetClassWithIdHandler> logger
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<BaseResponseDto<Class>> Handle(GetClassWithIdRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Class> response = new BaseResponseDto<Class>();
            try
            {
                
                var result = await _repositoryWrapper.Class.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

                
                response.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Class oluşturulurken bir hata oluştu.");
            }
            return response;
        }
    }
}
