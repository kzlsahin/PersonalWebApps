using Microsoft.EntityFrameworkCore.Metadata.Internal;
using tracker_webapp.Repository;

namespace tracker_webapp.Models
{
    public class TrackItemRowModel
    {
        public TrackItemRowModel(TrackItem item, Func<DateTime, ValidityStatus> checkValidity)
        {
            ID = item.ID;
            Name = item.Name;
            Title = item.Title;
            Subject = item.Subject;
            Category = item.Category;
            IssueDate = item.IssueDate;
            ExpiryDate = item.ExpiryDate;
            Status = checkValidity(item.ExpiryDate);
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public Category Category { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ValidityStatus Status { get; set; }
    }
}
