using Magic_Villa_VillaApi.Logging;
using Magic_Villa_VillaApi.Models;
using Magic_Villa_VillaApi.Models.DTO;
using Magic_Villa_VillaApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace Magic_Villa_VillaApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/UsersAuth")]
    [ApiVersionNeutral]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogging<UserController> _logger;
        protected APIResponse _response;

        public UserController(IUserRepository userRepo, ILogging<UserController> looger)
        {
            _userRepo = userRepo;
            _response = new APIResponse();
            _logger = looger;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var loginResponse = await _userRepo.Login(loginRequestDTO);

                if (loginResponse.User == null && string.IsNullOrEmpty(loginResponse.Token))
                {
                    throw new NullReferenceException("Username or password is not correct");
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = loginResponse;
                _logger.Log($"{loginResponse.User.Name} has been successfully logged in system", LoggingTypes.Info);
                return Ok(_response);
            }

            catch (NullReferenceException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(ex.Message);
                return BadRequest(_response);
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestModel model)
        {
            try
            {
                bool ifUserNameIsUnique = _userRepo.IsUniqueUser(model.UserName);

                if (!ifUserNameIsUnique)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors.Add("Username already exists");
                    return BadRequest(_response);
                }

                var user = await _userRepo.Register(model);

                if (user == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors.Add("Error while registration");
                    return BadRequest(_response);
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "User has been successfully registered!";
                return Ok(_response);
            }

            catch (NullReferenceException ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(ex.Message);
                return BadRequest(_response);
            }
        }
    }
}
