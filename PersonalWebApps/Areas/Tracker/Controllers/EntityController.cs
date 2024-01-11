using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tracker_webapp.Data;
using tracker_webapp.Models;
using tracker_webapp.Repository;

namespace Areas.Tracker.Controllers
{
    [Authorize]
    [Area("Tracker")]
    public class EntityController : Controller
    {
        private readonly ItemsDBContext _dbContext;
        public EntityController(ItemsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index(int itemID=-1)
        {
            var model = new TrackItemCreateModel();
            var userName = User.Identity?.Name ?? "";
            if (itemID > 0)
            {
                if (GetTrackItem(userName, itemID, out TrackItem item))
                {
                    model.ID = itemID;
                    model.Categories = GetCategories();
                    model.CategoryId = item.Category.ID;
                    model.ItemName = item.Name;
                    model.Title = item.Title;
                    model.Subject = item.Subject;
                    model.IssueDate = item.IssueDate;
                    model.ExpiryDate = item.ExpiryDate;
                    ViewBag.Action = "Edit";
                    return View(model);
                }
            }
            ViewBag.Action = "Create";
            return IndexView(model);
        }

        // POST: EntityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] TrackItemCreateModel formItem)
        {
            ViewBag.Action = "Create";
            if (!ModelState.IsValid)
            {
                return IndexView(formItem);
            }
            try
            {
                var category = _dbContext.Categories.Find(formItem.CategoryId);
                if (category == null)
                {
                    return IndexView(formItem);
                }
                var item = new TrackItem()
                {
                    Category = category,
                    Name = formItem.ItemName ?? "",
                    Title = formItem.Title ?? "",
                    Subject = formItem.Subject ?? "",
                    IssueDate = formItem.IssueDate,
                    ExpiryDate = formItem.ExpiryDate,
                    IsActive = true
                };
                _dbContext.TrackItems.Add(item);
                _dbContext.UserToTrackItems.Add(new UserToTrackItem() { UserId = User.Identity?.Name ?? "", TrackItem = item });
                _dbContext.SaveChanges();
                return Redirect("/Tracker/Screen");
            }
            catch (Exception ex)
            {
                return IndexView(formItem);
            }
        }

        // POST: EntityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] TrackItemCreateModel formItem)
        {
            ViewBag.Action = "Edit";
            var userName = User.Identity?.Name ?? "";
            if (!ModelState.IsValid)
            {
                return IndexView(formItem);
            }
            try
            {
                if (GetTrackItem(userName, formItem.ID, out TrackItem item))
                {
                    var category = _dbContext.Categories.Find(formItem.CategoryId);
                    if (category == null)
                    {
                        IndexView(formItem);
                    }
                    item.Category = category;
                    item.Name = formItem.ItemName ?? "";
                    item.Title = formItem.Title ?? "";
                    item.Subject = formItem.Subject ?? "";
                    item.IssueDate = formItem.IssueDate;
                    item.ExpiryDate = formItem.ExpiryDate;
                    item.IsActive = true;
                    _dbContext.SaveChanges();
                }
                return Redirect("/Tracker/Screen");
            }
            catch (Exception ex)
            {
                return IndexView(formItem);
            }
        }

        // POST: EntityController/Delete/5
        [HttpGet]
        public IResult Delete([FromQuery] int ID)
        {
            var userName = User.Identity?.Name ?? "";
            if (string.IsNullOrEmpty(userName))
            {
                return Results.Unauthorized();
            }
            try
            {
                if (GetTrackItem(userName, ID, out TrackItem item))
                {
                    item.IsActive = false;
                    _dbContext.SaveChanges();
                }
                return Results.Redirect("/Tracker/Screen");
            }
            catch (Exception ex)
            {
                return Results.UnprocessableEntity();
            }
        }

        private List<Category> GetCategories()
        {
            var items = _dbContext.Categories.ToList();
            return items;
        }
        private bool GetTrackItem(string userName, int itemID, out TrackItem item)
        {
            var itemIds = _dbContext.UserToTrackItems.Where(x => x.UserId == userName).Select(x => x.TrackItem.ID).ToList();
            if (itemIds.Count > 0 && itemIds.Contains(itemID))
            {
                item = _dbContext.TrackItems.Find(itemID) ?? new();
                return true;
            }
            item = new TrackItem();
            return false;
        }

        private ActionResult IndexView(TrackItemCreateModel formItem)
        {
            formItem.Categories = GetCategories();
            return View("~/Areas/Tracker/Views/Entity/Index.cshtml", formItem);
        }
    }
}
