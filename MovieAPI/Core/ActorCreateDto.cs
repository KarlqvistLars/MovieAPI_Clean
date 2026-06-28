namespace Movie.Core
{
    public class ActorCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string BirthYear { get; set; } = string.Empty;
        public List<string> Movies { get; set; } = new List<string>();
    }
}
