namespace Projet_mvc.Models.Home
{
    public class HomePageViewModel
    {
        public List<ListingSummaryViewModel> RecentListings { get; set; }

        public List<ListingSummaryViewModel> PopularListings { get; set; }

        public List<TagViewModel> PopularTags { get; set; }
    }
}
