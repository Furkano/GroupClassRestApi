using GroupClass.Core.Dtos;
using GroupClass.Core.Dtos.Requests.UserRequest;
using GroupClass.Core.Interfaces;
using GroupClass.Core.Models;
using GroupClass.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;
        IRepositoryWrapper _repositoryWrapper;
        public UsersController(IMediator mediator, IRepositoryWrapper repositoryWrapper)
        {
            _mediator = mediator;_repositoryWrapper = repositoryWrapper;
        }
        [HttpGet]
        public async Task<BaseResponseDto<UserDto>> Get()
        {
            BaseResponseDto<UserDto> response = new BaseResponseDto<UserDto>();
            var identifier = User.Claims.FirstOrDefault(p => p.Type == "id");
            if (identifier==null)
            {
                response.Data = null;
                response.Errors.Add("User Claimdan getirilirken bir problem oluştu.");
            }
            else
            {
                GetUserRequest getUserRequest = new GetUserRequest();
                getUserRequest.Id = int.Parse(identifier.Value);
                response = await _mediator.Send(getUserRequest);
            }
            return response;
        }
        [HttpGet("getuser/{id}")]
        public async Task<User> Get2(int id)
        {
            var result = await _repositoryWrapper.User.Where(p => p.Id == id).FirstOrDefaultAsync();
            return result;
        }
        [HttpPost("Register")]
        public async Task<BaseResponseDto<UserDto>> CreateUserAsync([FromBody] CreateUserRequest createUserRequest)
        {
            BaseResponseDto<UserDto> response = await _mediator.Send(createUserRequest);

            return response;
        }
        [HttpPut]
        public async Task<BaseResponseDto<bool>> UpdateUserAsync([FromBody] UpdateUserRequest updateUserRequest)
        {
            BaseResponseDto<bool> response = await _mediator.Send(updateUserRequest);
            return response;
        }
        [HttpPost("Login")]
        public async Task<BaseResponseDto<ResponseLoginDto>> Login([FromBody] LoginUserRequest loginUserRequest)
        {
            BaseResponseDto<ResponseLoginDto> response = await _mediator.Send(loginUserRequest);
            return response;
        }
        
    }
}
