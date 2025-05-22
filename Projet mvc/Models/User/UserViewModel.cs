using ListingModel = Projet_mvc.Core.Domain.Listing;
using DomainUser = Projet_mvc.Core.Domain.User;

namespace Projet_mvc.Models.User
{
    public class UserViewModel
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime Creation_Date { get; set; }


        public DomainUser User { get; set; }
        public List<ListingModel> Listings { get; set; }
        public List<ListingModel> FavoriteListings { get; set; }
    }
}
