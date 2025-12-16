using System.Collections.Generic;

namespace Monopolio.Models
{
    public class GameState
    {
        public List<Player> RegisteredPlayers { get; set; } = new List<Player>();
        public List<Player> PlayersInGame { get; set; } = new List<Player>();

        public bool GameInProgress { get; set; } = false;
        public int CurrentPlayerIndex { get; set; } = 0;

        public bool HasRolledThisTurn { get; set; } = false;
        public bool HasDrawnCardThisTurn { get; set; } = false;

        public int FreeParkMoney { get; set; } = 0;

        public Board? Board { get; set; } = null;

        public bool IsInPrison { get; set; } = false;
        public int PrisonTurns { get; set; } = 0;
        public int ConsecutiveDoubles { get; set; } = 0;
    }
}
