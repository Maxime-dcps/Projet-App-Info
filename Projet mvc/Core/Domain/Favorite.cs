namespace Projet_mvc.Core.Domain
{
    public class Favorite
    {
        public int UserId { get; set; }
        public int ListingId { get; set; }
        public DateTime FavoritedDate { get; set; }

        public User User { get; set; }
        public Listing Listing { get; set; }
    }
}
