namespace Projet_mvc.Models.Listing
{
    public class ListingDetailViewModel
    {
        // Listing informations
        public int ListingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }   // Indicates if the listing is available for purchase
        public DateTime CreationDate { get; set; }

        // Author informations
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        // Images and tags
        public List<ImageViewModel> Images { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
