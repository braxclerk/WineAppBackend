using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.DataRepository;

namespace APApiDbS2024InClass.Controllers
{
  [Route("api/[controller]")]
  public class WinesController : Controller
  {
    private readonly WineRepository _repository = new WineRepository();

    // GET: api/wines
    [HttpGet]
    public IEnumerable<Wine> Get()
    {
      return _repository.GetWines();
    }

    // GET api/wines/5
    [HttpGet("{id}")]
    public Wine Get(int id)
    {
      return _repository.GetWineById(id);
    }

    // POST api/wines
    [HttpPost]
    public void Post([FromBody] Wine wine)
    {
      _repository.InsertWine(wine);
    }

    // PUT api/wines/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Wine wine)
    {
      _repository.UpdateWine(id, wine);
    }

    // DELETE api/wines/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      _repository.DeleteWine(id);
    }
  }
}
