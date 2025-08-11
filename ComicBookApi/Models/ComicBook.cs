namespace ComicBookApi.Models {
    public class ComicBook
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}