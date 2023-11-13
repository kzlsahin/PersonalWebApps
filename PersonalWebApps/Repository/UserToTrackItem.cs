using System.ComponentModel.DataAnnotations;
namespace tracker_webapp.Repository
{
    public class UserToTrackItem
    {
        public int ID { get; set; }
        public string UserId { get; set; } // This property links to the user's name in Identity
        [Required]
        public TrackItem TrackItem { get; set; }
    }

}
