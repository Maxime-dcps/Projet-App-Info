namespace Projet_mvc.Models.Home
{
    public class HomePageViewModel
    {
        public List<ListingSummaryViewModel> RecentListings { get; set; }

        public List<ListingSummaryViewModel> PopularListings { get; set; }

        //Ajouter une liste des tags populaires
    }
}
