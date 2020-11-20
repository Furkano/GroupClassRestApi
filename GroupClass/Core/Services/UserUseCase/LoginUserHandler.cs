using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.UserRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GroupClass.Core.Services.UserUseCase
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, BaseResponseDto<ResponseLoginDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<LoginUserHandler> _logger;
        //private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        public LoginUserHandler(
            IRepositoryWrapper repositoryWrapper, 
            ILogger<LoginUserHandler> logger, 
            //IMediator mediator,
            IConfiguration config
            )
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            //_mediator = mediator;
            _config = config;
        }
        public async Task<BaseResponseDto<ResponseLoginDto>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<ResponseLoginDto> response = new BaseResponseDto<ResponseLoginDto>();
            ResponseLoginDto responseLoginDto = new ResponseLoginDto();
            try
            {
                var user = await _repositoryWrapper.User.Where(p => (p.Email == request.Email && p.Password == GetHash(request.Password))).FirstOrDefaultAsync();
                if (user !=null)
                {
                    //response.Data.User = new UserDto(user);
                    //response.Data.Token= GenerateJWT(user);
                    responseLoginDto.User = new UserDto(user);
                    responseLoginDto.Token = GenerateJWT(user);
                    
                    response.Data = responseLoginDto;
                }
                else
                {
                    response.Errors.Add("Böyle bir kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.Message);
                response.Errors.Add(ex.InnerException.Message);
                response.Errors.Add("Kullanıcı giriş yaparken bir hata oluştu.");
            }
            return response;
        }
        private string GetHash(string text)
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
        string GenerateJWT(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", userInfo.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Sub, userInfo.Firstname),
                //new Claim("lastName", userInfo.Lastname.ToString()),
                //new Claim("role",userInfo.UserRole),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
