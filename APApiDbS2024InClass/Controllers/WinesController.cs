using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/Wines")]
  public class WinesController : ControllerBase
  {
    private readonly WineRepository _wineRepository;  // Assuming you have a similar repository setup for wines

    public WinesController(WineRepository wineRepository)
    {
      _wineRepository = wineRepository;
    }

    // GET: api/wines
    [HttpGet]
    public IEnumerable<Wine> Get()
    {
      return _wineRepository.GetWines();
    }

    // GET api/wines/5
    [HttpGet("{id}")]
    public Wine Get(int id)
    {
      return _wineRepository.GetWineById(id);
    }

    // POST api/wines
    [HttpPost]
    public void Post([FromBody] Wine wine)
    {
      _wineRepository.InsertWine(wine);
    }

    // PUT api/wines/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Wine wine)
    {
      _wineRepository.UpdateWine(id, wine);
    }

    [HttpDelete("{wineId}")]
    //[Authorize(Roles = "admin")] // Restrict this endpoint to users with the 'admin' role.
    public async Task<IActionResult> DeleteWine(int wineId)
    {
      if (await _wineRepository.DeleteWine(wineId))
      {
        return Ok("Wine deleted successfully.");
      }
      return NotFound("Wine not found.");
    }

  }
}

