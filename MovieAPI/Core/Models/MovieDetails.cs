namespace Movie_.Core.Models
{
    public class MovieDetails
    {
        public int MovieDetailsId { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;

        // 1:1 till Movie
        public MovieClass Movie { get; set; } = null!;

    }
}
