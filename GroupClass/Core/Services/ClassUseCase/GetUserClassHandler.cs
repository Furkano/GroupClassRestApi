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
    public class GetUserClassHandler : IRequestHandler<GetUserClassRequest, BaseResponseDto<List<Class>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IRepositoryWrapper _repositoryWrapper2;
        private readonly ILogger<GetUserClassHandler> _logger;
        public GetUserClassHandler(
            IRepositoryWrapper repositoryWrapper,
            IRepositoryWrapper repositoryWrapper2,
            ILogger<GetUserClassHandler> logger
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _repositoryWrapper2 = repositoryWrapper2;
            _logger = logger;
        }
        public async Task<BaseResponseDto<List<Class>>> Handle(GetUserClassRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<Class>> response = new BaseResponseDto<List<Class>>();
            try
            {
                List<Class> classes = new List<Class>();
                var result = await _repositoryWrapper.Member.Where(p => p.Userid == request.Userid)
                    .Include(p => p.Class)
                    .Include(p => p.User)
                    .ToListAsync();
                
                result.ForEach( p =>
                {
                    classes.Add(p.Class);
                });
                response.Data = classes;
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
