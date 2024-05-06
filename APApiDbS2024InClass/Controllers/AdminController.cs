using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;
/* using APApiDbS2024InClass.Model; // Ensure you have the correct namespace for your User model */

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : UserController
  {
    private readonly UserRepository _userRepository;
    public AdminController(UserRepository userRepository) : base(userRepository)
    {
      _userRepository = userRepository;
    }

    // GET: api/admin/users
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
      var users = await _userRepository.GetUsersByRole(); // Assuming GetUsersByRole returns all users if no role is specified
      return Ok(users);
    }

    // POST: api/admin/adduser
    [HttpPost("adduser")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
      if (user == null || !ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Task<bool> created = _userRepository.InsertUser(user);
      if (await created)
      {
        return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
      }
      else
      {
        return BadRequest("Unable to add user");
      }
    }

    // POST: api/admin/deleteuser/{id}
    [HttpPost("deleteuser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      if (! await _userRepository.DeleteUser(id))
        return BadRequest("Failed to delete user");

      return Ok("User deleted successfully");
    }

    // DELETE: api/admin/deletereview/{id}
    [HttpDelete("deletereview/{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      if (! await _userRepository.DeleteReview(id))
      {
        return NotFound($"Review with ID {id} not found.");
      }
      return Ok($"Review with ID {id} successfully deleted.");
    }
  }
}
