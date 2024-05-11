using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APApiDbS2024InClass.DataRepository;
using APApiDbS2024InClass.Model;
using System.Threading.Tasks;

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/Users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
      _userRepository = userRepository;
    }

       [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
      var newUser = await _userRepository.RegisterUser(user);
      return newUser != null ? Ok(newUser) : BadRequest("Failed to register user.");
    }


    [HttpGet("role/{roleId}")]
    public async Task<IActionResult> GetUsersByRole(string roleId)
    {
      var users = await _userRepository.GetUsersByRole(roleId);
      return Ok(users);
    }


 

    [HttpPut("update/{id}")]
    //[Authorize] // Ensure that user is authorized and updating their own profile
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
      if (id != user.Id)
      {
        return BadRequest("Mismatch between route ID and body ID.");
      }

      bool updated = await _userRepository.UpdateUser(user);
      return updated ? Ok("User updated successfully.") : NotFound("User not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      bool deleted = await _userRepository.DeleteUser(id);
      if (deleted)
      {
        return Ok("User deleted successfully.");
      }
      else
      {
        return NotFound("User not found.");
      }
    }
  }
}