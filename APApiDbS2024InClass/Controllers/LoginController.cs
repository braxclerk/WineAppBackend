using APApiDbS2024InClass.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;

namespace APApiDbS2024InClass.Controllers;
[Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly UserRepository _userRepository;

    public LoginController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] Login credentials)
    {
        if (await _userRepository.ValidateUser(credentials.Username, credentials.Password))
        {
            var userId = await _userRepository.GetUserIdByUserName(credentials.Username);
            // 1. Concatenate username and password with a semicolon
            var text = $"{credentials.Username}:{credentials.Password}";
            // 2. Base64 encode the above
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var encodedCredentials = Convert.ToBase64String(bytes);
            // 3. Prefix with Basic
            var headerValue = $"Basic {encodedCredentials}";
            return Ok(new { headerValue = headerValue, userId = userId});
        }
        else
        {
            return Unauthorized();
        }
    }
}
