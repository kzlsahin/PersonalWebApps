using System.ComponentModel.DataAnnotations;

namespace tracker_webapp.Repository
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
