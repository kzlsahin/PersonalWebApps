using tracker_webapp.Repository;

namespace tracker_webapp.Models
{
    public class ScreenModel
    {
        public List<IGrouping<Category, TrackItem>> TrackItems { get; set; }
        public string UserName { get; set; }
        public ValidityStatus GetValiditySatus(DateTime ExpiryDate)
        {
            TimeSpan warningSpan = TimeSpan.FromDays(90);
            var today = DateTime.Now;
            if (ExpiryDate < today)
            {
                return ValidityStatus.Expired;
            }
            else if (ExpiryDate - today < warningSpan)
            {
                return ValidityStatus.Close;
            }
            else
            {
                return ValidityStatus.Valid;
            }
        }

        public string CautionCategoryIdTemp(string cautionCategoryName)
        {
            return $"{cautionCategoryName}-validity - indicator";
        }
    }
}
