namespace Movie_.Core.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public MovieClass? Movie { get; set; }
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }
        // Primärnyckel (composite key)    
        public int Id { get; set; } // Alternativ: bara MovieId + ActorId som PK
    }
}
