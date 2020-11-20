using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.ClassUseCase
{
    public class GetClassWithEducationYearHandler : IRequestHandler<GetClassWithEducationYearRequest, BaseResponseDto<List<Class>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<GetClassWithEducationYearHandler> _logger;
        //private readonly IMediator _mediator;
        
        public GetClassWithEducationYearHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<GetClassWithEducationYearHandler> logger
            //IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            //_mediator = mediator;
        }
        public async Task<BaseResponseDto<List<Class>>> Handle(GetClassWithEducationYearRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<Class>> response = new BaseResponseDto<List<Class>>();
            
            try
            {
                var result =  _repositoryWrapper.Class.Where(p => p.EducationYear == request.EducationYear)
                    .Include(p => p.Members);

                if (result != null)
                {
                    response.Data = await result.ToListAsync();
                }
                else
                {
                    response.Errors.Add("Belirtilen yıl ile eşleşen bir sınıf bulunamadı.");
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
