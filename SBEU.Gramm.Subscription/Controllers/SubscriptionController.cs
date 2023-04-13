using SBEU.Gramm.DataLayer.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Subscription.Controllers
{
    [Route("[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly ApiDbContext _context;
        public SubscriptionController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _context.Users.Include(x=>x.SubscriptionPlan).FirstOrDefaultAsync(x=>x.Id==userId);
            if (user == null) return NotFound();
            
            return Json(user.SubscriptionPlan);
        }

        [HttpPatch("{userId}/{subId}")]
        public async Task<IActionResult> Update(string userId, int subId)
        {
            var user = await _context.Users.Include(x => x.SubscriptionPlan).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return NotFound(nameof(XIdentityUser));
            var sub = await _context.SubscriptionPlans.FindAsync(subId);
            if (sub == null) return NotFound(nameof(SubscriptionPlan));
            user.SubscriptionPlan = sub;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subPlans = _context.SubscriptionPlans.Where(x => x.Id >= 0 && x.Price>=0).ToList();
            return Json(subPlans);
        }
    }
}
