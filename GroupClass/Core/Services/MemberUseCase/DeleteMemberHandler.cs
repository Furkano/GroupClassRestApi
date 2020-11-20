using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.MemberRequest;
using GroupClass.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.MemberUseCase
{
    public class DeleteMemberHandler : IRequestHandler<DeleteMemberRequest, BaseResponseDto<bool>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<DeleteMemberHandler> _logger;
        private readonly IMediator _mediator;
        public DeleteMemberHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<DeleteMemberHandler> logger,
            IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<bool>> Handle(DeleteMemberRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();
            try
            {
                var deletedMember = await _repositoryWrapper.Class.Find(request.Id);
                if (deletedMember == null)
                {
                    response.Errors.Add("Böyle bir üye bulunamadı.");
                }
                else
                {
                    _repositoryWrapper.Class.Delete(deletedMember.Id);
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
                response.Errors.Add("Üye silinirken bir hata oluştu.");
            }
            return response;
        }
    }
}
