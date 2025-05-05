namespace Projet_mvc.Core.Domain
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FilePath { get; set; }
        public int ImageOrder { get; set; }
        public string AltText { get; set; }
        public int ListingId { get; set; }
        public DateTime UploadDate { get; set; }

    }
}
