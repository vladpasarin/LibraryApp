using LibraryApp.DTOs;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChallengeController : ControllerBase
    {
        private readonly IUserChallengeService _service;

        public UserChallengeController(IUserChallengeService service)
        {
            _service = service;
        }

        // GET api/<UserChallengeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpGet("challenge={id}/user={userId}")]
        public async Task<IActionResult> GetByUserAndChallenge(int id, int userId)
        {
            return Ok(await _service.GetByUserAndChallenge(userId, id));
        }

        // POST api/<UserChallengeController>
        [HttpPost]
        public async Task<IActionResult> Add(UserChallengeDto userChallengeDto)
        {
            return Ok(await _service.CreateAsync(userChallengeDto));
        }

        // PUT api/<UserChallengeController>/5
        [HttpPut("getProgress/{id}/{userId}")]
        public async Task<IActionResult> UpdateChallengeProgress(int id, int userId)
        {
            var userChallenge = await _service.GetByUserAndChallenge(userId, id);
            return Ok(await _service.UpdateProgress(userChallenge.Id, userId));
        }

        // DELETE api/<UserChallengeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
