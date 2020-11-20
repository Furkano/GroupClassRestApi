using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.MemberRequest;
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

namespace GroupClass.Core.Services.MemberUseCase
{
    public class GetClassMembersHandler : IRequestHandler<GetClassMembersRequest, BaseResponseDto<List<Member>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetClassMembersHandler> _logger;

        public GetClassMembersHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<GetClassMembersHandler> logger
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<BaseResponseDto<List<Member>>> Handle(GetClassMembersRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<Member>> response = new BaseResponseDto<List<Member>>();
            try
            {
                var result = _repositoryWrapper.Member.Where(p => p.Classid == request.Classid)
                    .Include(p => p.Class)
                    .Include(p => p.User);
                if (result!=null)
                {
                    response.Data = await result.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Üyeler getirilirken bir hata oluştu.");
            }
            return response;
        }
    }
}
