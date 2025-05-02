namespace Projet_mvc.Models.Listing
{
    public class ListingDetailViewModel
    {
        // Listing informations
        public Projet_mvc.Core.Domain.Listing ListingData { get; set; } // Complete name to avoid confusion with the name of the directory

        // Images and tags
        public List<ImageViewModel> Images { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
