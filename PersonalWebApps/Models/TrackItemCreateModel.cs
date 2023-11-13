using System.ComponentModel.DataAnnotations;
using tracker_webapp.Repository;

namespace tracker_webapp.Models
{
    public class TrackItemCreateModel
    {
        public TrackItemCreateModel()
        {
            ID = -1;
            ItemName = string.Empty;
            CategoryId = -1;
            Title = string.Empty;
            Subject = string.Empty;
            IssueDate = DateTime.Parse("01-01-2023");
            ExpiryDate = DateTime.Parse("01-01-2023");
            Categories = new List<Category>();
        }
        public int ID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Subject { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public List<Category> Categories { get; set; }
    }
}
