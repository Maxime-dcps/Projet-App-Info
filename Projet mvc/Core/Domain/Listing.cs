namespace Projet_mvc.Core.Domain
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
