using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
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

namespace GroupClass.Core.Services.ClassUseCase
{
    public class GetClassWithCodeHandler : IRequestHandler<GetClassWithCodeRequest, BaseResponseDto<ClassDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetClassWithCodeHandler> _logger;
        //private readonly IMediator _mediator;

        public GetClassWithCodeHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<GetClassWithCodeHandler> logger
            //IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            //_mediator = mediator;
        }
        public async Task<BaseResponseDto<ClassDto>> Handle(GetClassWithCodeRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<ClassDto> response = new BaseResponseDto<ClassDto>();
            try
            {
                var codeClass = await _repositoryWrapper.Class.Where(p => p.AlphaNumericCode == request.AlphaNumericCode).FirstOrDefaultAsync();
                if (codeClass!=null)
                {
                    response.Data = codeClass.Adapt<ClassDto>();
                }
                else
                {
                    response.Errors.Add("Belirtilen kod ile eşleşen bir sınıf bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Class alpha numarası ile getirili iken bir hata oluştu.");
            }
            return response;
        }
    }
}
