using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;
using APApiDbS2024InClass.Model; // Ensure you have the correct namespace for your User model

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : UserController
  {
    private readonly AdminRepository _adminRepository; // Use AdminRepository for admin-specific actions

    public AdminController(AdminRepository adminRepository)
    {
      _adminRepository = adminRepository;
    }

    // GET: api/admin/users
    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
      var users = _adminRepository.GetUsersByRole(); // Assuming GetUsersByRole returns all users if no role is specified
      return Ok(users);
    }

    // POST: api/admin/adduser
    [HttpPost("adduser")]
    public IActionResult AddUser([FromBody] User user)
    {
      if (user == null || !ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      bool created = _adminRepository.InsertUser(user);
      if (created)
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
    public IActionResult DeleteUser(int id)
    {
      if (!_adminRepository.DeleteUser(id))
        return BadRequest("Failed to delete user");

      return Ok("User deleted successfully");
    }

    // DELETE: api/admin/deletereview/{id}
    [HttpDelete("deletereview/{id}")]
    public IActionResult DeleteReview(int id)
    {
      if (!_adminRepository.DeleteReview(id))
      {
        return NotFound($"Review with ID {id} not found.");
      }
      return Ok($"Review with ID {id} successfully deleted.");
    }
  }
}
