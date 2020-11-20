using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.ClassRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.ClassUseCase
{
    public class CreateClassHandler : IRequestHandler<CreateClassRequest, BaseResponseDto<ClassDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<CreateClassHandler> _logger;
        //private readonly IMediator _mediator;
        private static Random _random;
        public CreateClassHandler(
            IRepositoryWrapper repositoryWrapper,
            ILogger<CreateClassHandler> logger
            //IMediator mediator
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            //_mediator = mediator;
        }
        public async Task<BaseResponseDto<ClassDto>> Handle(CreateClassRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<ClassDto> response = new BaseResponseDto<ClassDto>();
            try
            {
                System.Security.Claims.ClaimsPrincipal claims = new System.Security.Claims.ClaimsPrincipal();
                
                var identifier = claims.Claims.FirstOrDefault(predicate => predicate.Type == "id");
                if (identifier==null)
                {
                    response.Errors.Add("Yetkilendirme hatası");
                    response.Data = null;
                }
                else
                {
                    var user = await _repositoryWrapper.User.Find(int.Parse(identifier.ToString()));
                    if (user.UserRole!="Admin")
                    {
                        response.Errors.Add("Sınıf oluşturmak için yetkiniz bulunmamaktadır.");
                        response.Data = null;
                    }
                    else
                    {
                        var newClass = new Class
                        {
                            CreatedAt = DateTime.Now,
                            EducationYear = request.EducationYear,
                            ModifiedAt = DateTime.Now,
                            Name = request.Name,
                            AlphaNumericCode = AlphanumericCodeGenerator(8)
                        };
                        await _repositoryWrapper.Class.Create(newClass);
                        if (await _repositoryWrapper.SaveChangesAsync())
                        {
                            response.Data = newClass.Adapt<ClassDto>();
                        }
                        else
                        {
                            response.Errors.Add("Veri tabanı kayıt esnasında bir sorun oluştu.");
                        }
                    }
                }
                

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Class oluşturulurken bir hata oluştu.");
            }
            return response;
        }
        public static string AlphanumericCodeGenerator(int length)
        {
            _random = new Random();
            const string chars = "ABCDEFGHILKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
