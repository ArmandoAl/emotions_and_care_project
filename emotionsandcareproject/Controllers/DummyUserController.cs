using Business.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyUserController : ControllerBase
    {
        private readonly IDummyUserService _dummyUserService;

        /// <summary>
        /// Initializes a new instance of the DummyUserController class.
        /// </summary>
        /// <param name="dummyUserService">The service to use for business logic.</param>
        public DummyUserController(IDummyUserService dummyUserService)
        {
            _dummyUserService = dummyUserService;
        }

        /// <summary>
        /// Adds a new DummyUser.
        /// </summary>
        /// <param name="dummyUser">The DummyUser to add.</param>
        /// <returns>The ID of the added DummyUser, or 0 if failed.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] DummyUser dummyUser)
        {
            var result = _dummyUserService.Add(dummyUser);
            return result > 0 ? Ok(result) : BadRequest("Failed to add DummyUser");
        }

        /// <summary>
        /// Gets a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to retrieve.</param>
        /// <returns>The DummyUser if found, null otherwise.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _dummyUserService.Get(id);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// Deletes a DummyUser by its ID.
        /// </summary>
        /// <param name="id">The ID of the DummyUser to delete.</param>
        /// <returns>True if the DummyUser was deleted, false otherwise.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _dummyUserService.Delete(id);
            return result ? Ok() : NotFound();
        }

        /// <summary>
        /// Adds a badge to a specific dummy user.
        /// </summary>
        /// <param name="dummyUserId">The ID of the dummy user.</param>
        /// <param name="badgeId">The ID of the badge to add.</param>
        /// <returns>True if the badge was added successfully, false otherwise.</returns>
        [HttpPost("{dummyUserId}/badges/{badgeId}")]
        public IActionResult AddBadgeToDummy(int dummyUserId, int badgeId)
        {
            var result = _dummyUserService.AddBadgeToDummy(dummyUserId, badgeId);
            return result ? Ok() : BadRequest("Failed to add badge to dummy user");
        }

        /// <summary>
        /// Adds all available badges to all dummy users.
        /// </summary>
        /// <returns>True if all badges were added successfully, false otherwise.</returns>
        [HttpPost("badges/all")]
        public IActionResult AddAllBadgesToDummies()
        {
            var result = _dummyUserService.AddAllBadgesToDummies();
            return result ? Ok() : BadRequest("Failed to add badges to dummy users");
        }

        /// <summary>
        /// Validates the growth of user badges.
        /// </summary>
        /// <param name="userId">The ID of the user to check.</param>
        /// <returns>A list of booleans indicating whether the userBadge "isEarned" </returns>
        /// 

        [HttpGet("{userId}/badges/check")]
        public IActionResult CheckBadges(int userId)
        {
            var result = _dummyUserService.CheckBadges(userId);
            return result != null ? Ok(result) : NotFound();
        }
    }
} 