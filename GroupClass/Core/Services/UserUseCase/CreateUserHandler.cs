using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.UserRequest;
using GroupClass.Core.Events;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Common;

namespace GroupClass.Core.Services.UserUseCase
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, BaseResponseDto<UserDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IMediator _mediator;

        public CreateUserHandler(IRepositoryWrapper repositoryWrapper, ILogger<CreateUserHandler> logger, IMediator mediator)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<BaseResponseDto<UserDto>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<UserDto> response = new BaseResponseDto<UserDto>();
            try
            {
                var user = new User
                {
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Password = getHash(request.Password),
                    Email = request.Email,
                    ActivationCode = "123asd133",
                    ProfileImageUrl = "asdasdasdasd654f6asdf",
                    SchoolNumber = request.SchoolNumber,
                    UserRole = Policies.User
                };
                await _repositoryWrapper.User.Create(user);
                await _repositoryWrapper.SaveChangesAsync();    
                response.Data = user.Adapt<UserDto>();
                
                //await _mediator.Publish(new NewUserCreatedEvent(user.Firstname));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Kullanıcı oluşturulurken bir hata oluştu.");
            }
            return response;
        }
        private string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
