using System.ComponentModel.DataAnnotations;
namespace tracker_webapp.Repository
{
    public class TrackItem
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Category Category { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
    }

}
