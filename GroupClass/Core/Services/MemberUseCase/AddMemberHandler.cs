using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.MemberRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.MemberUseCase
{
    public class AddMemberHandler : IRequestHandler<AddMemberRequest, BaseResponseDto<MemberDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<AddMemberHandler> _logger;
        private readonly IMediator _mediator;
        public AddMemberHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<AddMemberHandler> logger,
            IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<MemberDto>> Handle(AddMemberRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<MemberDto> response = new BaseResponseDto<MemberDto>();
            try
            {
                var addMember = new Member 
                { 
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Classid = request.Classid,
                    Userid = request.Userid
                };
                await _repositoryWrapper.Member.Create(addMember);
                if (await _repositoryWrapper.SaveChangesAsync())
                {
                    response.Data = addMember.Adapt<MemberDto>();
                }
                else
                {
                    response.Errors.Add("Veri tabanı kayıt esnasında bir sorun oluştu.");
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
