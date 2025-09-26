using System.ComponentModel.DataAnnotations;

namespace ArsenalPlayers.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string? Position { get; set; }
        public int JerseyNumber { get; set; }
        public int GoalsScored { get; set; }
    }
}
