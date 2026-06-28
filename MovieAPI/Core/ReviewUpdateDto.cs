namespace Movie_.Core
{
    public class ReviewUpdateDto
    {
        public int ReviewId { get; set; }
        public string? ReviewerName { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // Rating (1–5)
    }
}
