using System.Net.Mime;
using CoursesApp.Models;
using CoursesApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApp.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/accounts")]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(UserLoginDto), 201, MediaTypeNames.Application.Json)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<User>> CreateAccount([FromBody] UserLoginDto dto)
        {
           return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserLoginDto>), 200, MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<UserLoginDto>>> GetAllUsers()
        {
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserLoginDto), 200, MediaTypeNames.Application.Json)]
        [ProducesResponseType(422)]
        [Route("{accountId}")]
        public async Task<ActionResult<UserLoginDto>> GetSingleAccount([FromRoute] string accountId)
        {
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserLoginDto), 201, MediaTypeNames.Application.Json)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<UserLoginDto>> UpdateAccount([FromBody] UserLoginDto dto)
        {
            
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        [Route("{accountId}")]
        public async Task<ActionResult> DeleteAccount([FromRoute] string accountId)
        {
            return NoContent();
        }
    }
}
