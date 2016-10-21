namespace StarsReloaded.Shared.Model
{
    public class GameState
    {
        public string Name { get; set; }

        public Galaxy Galaxy { get; set; }

        public int CurrentPlayerNum { get; set; }

        public PlayerRace[] PlayerRaces { get; set; }

        public PlayerRace CurrentPlayerRace => this.PlayerRaces[this.CurrentPlayerNum];
    }
}