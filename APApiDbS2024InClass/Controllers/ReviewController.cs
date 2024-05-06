using Microsoft.AspNetCore.Mvc;
using APApiDbS2024InClass.Model;
using APApiDbS2024InClass.DataRepository;
using System.Threading.Tasks;

namespace APApiDbS2024InClass.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ReviewController : Controller
  {
    private readonly IReviewRepository _reviewRepository;

    public ReviewController(IReviewRepository reviewRepository)
    {
      _reviewRepository = reviewRepository;
    }

    // POST: /Review
    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] Review review)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      await _reviewRepository.AddReview(review);
      return NoContent();  // Or return appropriate action result
    }

    // GET: /Review/{wineId}
    [HttpGet("{wineId}")]
    public async Task<IActionResult> GetReviewsByWineId(int wineId)
    {
      var reviews = await _reviewRepository.GetReviewsByWineId(wineId);
      return Ok(reviews);
    }

    // PUT: /Review
    [HttpPut]
    public async Task<IActionResult> UpdateReview([FromBody] Review review)
    {
      await _reviewRepository.UpdateReview(review);
      return Ok();
    }

    // DELETE: /Review/{reviewId}
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
      bool result = await _reviewRepository.DeleteReview(reviewId);
      if (result)
        return Ok();
      else
        return NotFound();
    }

  }
}
