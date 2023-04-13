using SBEU.Gramm.DataLayer.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SBEU.Gramm.DataLayer.DataBase.Entities;

namespace SBEU.Gramm.Subscription.Controllers
{
    public class OverrideController : Controller
    {
        private readonly AdminSubscriptionSuggestion _admin;
        private readonly ApiDbContext _context;
        public OverrideController(IOptionsMonitor<AdminSubscriptionSuggestion> optionsMonitor, ApiDbContext context)
        {
            _context = context;
            _admin = optionsMonitor.CurrentValue;
        }
        [HttpPost("{secret1}/{secret2}/{secret3}")]
        public async Task AddSubscription([FromBody] SubscriptionPlan plan,string secret1,string secret2, string secret3)
        {
            if (_admin.Secret1 == secret1 && _admin.Secret2 == secret2 && _admin.Secret3 == secret3)
            {
                _context.SubscriptionPlans.Add(plan);
                await _context.SaveChangesAsync();
                return;
            }

            throw new AccessViolationException();
        }
        [HttpGet("{secret1}/{secret2}/{secret3}")]
        public async Task<IActionResult> Get(string secret1, string secret2, string secret3)
        {
            if (_admin.Secret1 == secret1 && _admin.Secret2 == secret2 && _admin.Secret3 == secret3)
            {
                var subPlans = _context.SubscriptionPlans.ToList();
                return Json(subPlans);
            }

            throw new AccessViolationException();
        }
    }
}
