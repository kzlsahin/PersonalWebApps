using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tracker_webapp.Data;
using tracker_webapp.Models;
using tracker_webapp.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Areas.Tracker.Controllers
{
    [Authorize]
    [Area("Tracker")]
    public class ScreenController : Controller
    {
        private readonly ItemsDBContext _dbContext;
        public ScreenController(ItemsDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: ScreenController
        public ActionResult Index()
        {
            var model = new ScreenModel();
            model.UserName = User.Identity?.Name ?? "";
            model.TrackItems = GetItems(model.UserName);
            return View(model);
        }

        // GET: ScreenController/Details/5
        public List<IGrouping<Category, TrackItem>> Details()
        {
            return GetItems(User.Identity?.Name ?? "");
        }
        private List<IGrouping<Category, TrackItem>> GetItems(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new List<IGrouping<Category, TrackItem>>();
            }
            var itemIds = _dbContext.UserToTrackItems.Where(x => x.UserId == userName).Select(x => x.TrackItem.ID).ToList();
            var items = _dbContext.TrackItems.Where(x => x.IsActive && itemIds.Contains(x.ID)).Include(x => x.Category).GroupBy(x => x.Category).ToList();
            return items;
        }

    }
}
