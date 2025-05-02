namespace Projet_mvc.Core.Domain
{
    public class Listing
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreationDate { get; set; }
        public string AuthorName { get; set; }
    }
}
