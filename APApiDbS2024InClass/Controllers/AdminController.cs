using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;
using APApiDbS2024InClass.Model;
using System.Threading.Tasks;

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/admin")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private readonly AdminRepository _adminRepository;

    public AdminController(AdminRepository adminRepository)
    {
      _adminRepository = adminRepository;
    }

    // GET: api/admin/users
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
      var users = await _adminRepository.GetAllUsersAsync();
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

      bool created = await _adminRepository.InsertUserAsync(user);
      if (created)
      {
        return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
      }
      else
      {
        return BadRequest("Unable to add user");
      }
    }

    // DELETE: api/admin/deleteuser/{id}
    [HttpDelete("deleteuser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      bool deleted = await _adminRepository.DeleteUserAsync(id);
      if (!deleted)
        return BadRequest("Failed to delete user");

      return Ok("User deleted successfully");
    }

    // DELETE: api/admin/deletereview/{id}
    [HttpDelete("deletereview/{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
      bool deleted = await _adminRepository.DeleteReviewAsync(id);
      if (!deleted)
      {
        return NotFound($"Review with ID {id} not found.");
      }
      return Ok($"Review with ID {id} successfully deleted.");
    }
  }
}
