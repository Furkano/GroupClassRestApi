using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
using GroupClass.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.ClassUseCase
{
    public class DeleteClassHandler : IRequestHandler<DeleteClassRequest,BaseResponseDto<bool>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<DeleteClassHandler> _logger;
        private readonly IMediator _mediator;
        public DeleteClassHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<DeleteClassHandler> logger,
            IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<bool>> Handle(DeleteClassRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();
            try
            {
                var deletedClass = await _repositoryWrapper.Class.Find(request.Id);
                if (deletedClass == null)
                {
                    response.Errors.Add("Böyle bir sınıf bulunamadı.");
                }
                else
                {
                    _repositoryWrapper.Class.Delete(deletedClass.Id);
                    if (await _repositoryWrapper.SaveChangesAsync())
                    {
                        response.Data = true;
                    }
                    else
                    {
                        response.Errors.Add("Veri tabanı kayıt esnasında bir sorun oluştu.");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Class silinirken bir hata oluştu.");
            }
            return response;
        }
    }
}
