using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.UserRequest;
using GroupClass.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.UserUseCase
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, BaseResponseDto<bool>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<UpdateUserHandler> _logger;
        private readonly IMediator _mediator;

        public UpdateUserHandler(IRepositoryWrapper repositoryWrapper, ILogger<UpdateUserHandler> logger, IMediator mediator)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<bool>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();
            try
            {
                var user = await _repositoryWrapper.User.Find(request.Id);
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.SchoolNumber = request.SchoolNumber;
                user.Email = request.Email;

                _repositoryWrapper.User.Update(user);
                if (await _repositoryWrapper.SaveChangesAsync())
                {
                    response.Data = true;
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
                response.Errors.Add("Kullanıcı oluşturulurken bir hata oluştu.");
            }
            return response;
        }
    }
}
