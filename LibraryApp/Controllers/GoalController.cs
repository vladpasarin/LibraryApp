using LibraryApp.DTOs;
using LibraryApp.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalServices _service;

        public GoalController(IGoalServices service)
        {
            _service = service;
        }

        // GET: api/<GoalController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal(int id)
        {
            var goal = await _service.GetById(id);

            if (goal == null)
                return StatusCode(404);

            return Ok(goal);
        }

        // GET api/<GoalController>/5
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var goals = await _service.GetAll();

            if (goals == null)
                return StatusCode(500);

            return Ok(goals);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllGoalTypes()
        {
            var goalTypes = await _service.GetGoalTypes();

            if (goalTypes == null)
                return StatusCode(500);

            return Ok(goalTypes);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserGoals(int id)
        {
            var userGoals = await _service.GetUserGoals(id);

            if (userGoals == null)
                return StatusCode(404);

            return Ok(userGoals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal(GoalDto goalDto)
        {
            return Ok(await _service.CreateAsync(goalDto));
        }

        // PUT api/<GoalController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGoalProgress(int id)
        {
            return Ok(await _service.UpdateGoal(id));
        }

        // DELETE api/<GoalController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
