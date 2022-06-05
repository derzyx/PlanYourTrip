using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;

namespace PlanYourTrip_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly DataContext _context;

        public SubscriptionController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("User/{id}")]
        public async Task<ActionResult<List<int>>> GetUserSubscriptions(int id)
        {
            var subs = _context.Subscription
                .FromSqlRaw($"SELECT ObservedId FROM Subscription WHERE SubscriberId = {id}")
                .Select(x => x.ObservedId)
                .ToList();

            return Ok(subs);
        }

        // Checks if user is subscribed to another user
        [HttpPost]
        [Route("IsSubscribed")]
        public async Task<ActionResult<Subscription>> GetSubscription(SubscriptionDTO subscription)
        {
            var sub = _context.Subscription
                .FromSqlRaw($"SELECT * FROM Subscription WHERE SubscriberId = {subscription.SubscriberId} AND ObservedId = {subscription.ObservedId}")
                .FirstOrDefault();

            if (sub != null)
            {
                return Ok(sub);
            }
            else return BadRequest();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Subscription>> AddSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Subscription.Add(subscription);
                await _context.SaveChangesAsync();

                return Ok("Dodano do obserwowanych");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<ActionResult<Subscription>> DeleteSubscription(int id)
        {
            var sub = await _context.Subscription.FindAsync(id);
            if(sub == null)
            {
                return NotFound();
            }

            try
            {
                _context.Subscription.Remove(sub);
                await _context.SaveChangesAsync();

                return Ok("Usunięto z obserwacji");
            }
            catch (Exception)
            {
                return BadRequest("Coś poszło nie tak <br/>");
            }
        }
    }
}
