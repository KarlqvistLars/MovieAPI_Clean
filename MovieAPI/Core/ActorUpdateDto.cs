namespace Movie_.Core
{
    public class ActorUpdateDto
    {
        public int ActorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BirthYear { get; set; } = string.Empty;
        public List<string> Movies { get; set; } = new List<string>();
    }
}
