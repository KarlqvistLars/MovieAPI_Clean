namespace Movie_.Core.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BirthYear { get; set; } = string.Empty;
        // M:M till Movies genom MovieActor
        public ICollection<MovieClass> Movies { get; set; } = new List<MovieClass>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();

    }
}
