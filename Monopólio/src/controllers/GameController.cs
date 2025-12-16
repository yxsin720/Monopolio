using System;
using System.Collections.Generic;
using Monopolio.Models;

namespace Monopolio.Controllers
{
    public class GameController
    {
        private GameState state;
        private Board board;

        public GameController(GameState gameState)
        {
            state = gameState;
            board = BoardFactory.Create();
            state.Board = board;
        }

        public string RegisterPlayer(string name)
        {
            for (int i = 0; i < state.RegisteredPlayers.Count; i++)
            {
                if (state.RegisteredPlayers[i].Name == name)
                    return "Jogador existente.";
            }

            state.RegisteredPlayers.Add(new Player(name));
            return "Jogador registado com sucesso.";
        }

        public string ListPlayers()
        {
            if (state.RegisteredPlayers.Count == 0)
                return "Sem jogadores registados.";

            string result = "";

            for (int i = 0; i < state.RegisteredPlayers.Count; i++)
            {
                result += state.RegisteredPlayers[i].ToString();
                if (i < state.RegisteredPlayers.Count - 1)
                    result += Environment.NewLine;
            }

            return result;
        }

        public string StartGame(string[] names)
        {
            if (state.GameInProgress)
                return "Existe um jogo em curso.";

            if (names.Length < 2 || names.Length > 4)
                return "Instrução inválida.";

            state.PlayersInGame.Clear();

            for (int i = 0; i < names.Length; i++)
            {
                bool found = false;

                for (int j = 0; j < state.RegisteredPlayers.Count; j++)
                {
                    if (state.RegisteredPlayers[j].Name == names[i])
                    {
                        Player p = state.RegisteredPlayers[j];
                        p.Row = 3;
                        p.Col = 3;
                        p.Money = 1200;

                        state.PlayersInGame.Add(p);
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return "Jogador inexistente.";
            }

            state.GameInProgress = true;
            state.CurrentPlayerIndex = 0;
            state.HasRolledThisTurn = false;

            return "Jogo iniciado com sucesso.";
        }

        private bool IsPlayerTurn(string name)
        {
            return state.GameInProgress &&
                   state.PlayersInGame[state.CurrentPlayerIndex].Name == name;
        }

        public string LaunchDice(string name, int? d1 = null, int? d2 = null)
        {
            if (!state.GameInProgress)
                return "Não existe um jogo em curso.";

            if (!IsPlayerTurn(name))
                return "Não é a vez do jogador.";

            if (state.HasRolledThisTurn)
                return "O jogador já lançou os dados neste turno.";

            Player player = state.PlayersInGame[state.CurrentPlayerIndex];
            Random rnd = new Random();

            int dx = d1 ?? rnd.Next(-3, 4);
            while (dx == 0)
                dx = rnd.Next(-3, 4);

            int dy = d2 ?? rnd.Next(-3, 4);
            while (dy == 0)
                dy = rnd.Next(-3, 4);

            int newRow = player.Row - dy;
            int newCol = player.Col + dx;

            var wrapped = board.Wrap(newRow, newCol);
            player.Row = wrapped.Item1;
            player.Col = wrapped.Item2;

            state.HasRolledThisTurn = true;

            Space space = board.Get(player.Row, player.Col);
            string result = $"Saiu {dx}/{dy} - espaço {space.Name}. ";

            if (space.Name == "Chance" || space.Name == "Community")
                return result + "Espaço especial. Tirar carta.";

            if (space.Name == "BackToStart")
                return result + "Peça colocada no espaço Start.";

            if (space.Name == "Police")
                return result + "Jogador preso.";

            if (space.Name == "Prison")
                return result + "Jogador só de passagem.";

            if (space.Name == "FreePark")
                return result + "Jogador recebe ValorGuardadoNoFreePark.";

            if (space.Owner == null)
                return result + "Espaço sem dono.";

            if (space.Owner == player.Name)
                return result + "Espaço já comprado.";

            return result + "Espaço já comprado por outro jogador. Necessário pagar renda.";
        }

        public string BuySpace(string name)
        {
            if (!state.GameInProgress)
                return "Não existe um jogo em curso.";

            if (!IsPlayerTurn(name))
                return "Não é a vez do jogador.";

            if (!state.HasRolledThisTurn)
                return "O jogador ainda não lançou os dados.";

            Player player = state.PlayersInGame[state.CurrentPlayerIndex];
            Space space = board.Get(player.Row, player.Col);

            if (space.Owner != null)
                return "O espaço já se encontra comprado.";

            int price = PriceTable.GetPrice(space.Name);
            if (price == -1)
                return "Este espaço não está para venda.";

            if (player.Money < price)
                return "O jogador não tem dinheiro suficiente para adquirir o espaço.";

            player.Money -= price;
            space.Owner = player.Name;

            return "Espaço comprado.";
        }

        public string EndTurn(string name)
        {
            if (!state.GameInProgress)
                return "Não existe jogo em curso.";

            if (!IsPlayerTurn(name))
                return "Não é o turno do jogador indicado.";

            if (!state.HasRolledThisTurn)
                return "O jogador ainda tem ações a fazer.";

            state.HasRolledThisTurn = false;

            state.CurrentPlayerIndex++;
            if (state.CurrentPlayerIndex >= state.PlayersInGame.Count)
                state.CurrentPlayerIndex = 0;

            string next = state.PlayersInGame[state.CurrentPlayerIndex].Name;
            return "Turno terminado. Novo turno do jogador " + next + ".";
        }

        public Board GetBoard()
        {
            return board;
        }

        public List<Player> GetPlayers()
        {
            return state.PlayersInGame;
        }

        public string PayRent(string name)
        {
            if (!state.GameInProgress)
                return "Não existe um jogo em curso.";

            if (!IsPlayerTurn(name))
                return "Não é a vez do jogador.";

            if (!state.HasRolledThisTurn)
                return "O jogador ainda tem ações a fazer.";

            Player player = state.PlayersInGame[state.CurrentPlayerIndex];
            Space space = board.Get(player.Row, player.Col);

            if (space.Owner == null || space.Owner == "" || space.Owner == player.Name)
                return "Não é necessário pagar aluguer.";

            Player? owner = null;

            for (int i = 0; i < state.PlayersInGame.Count; i++)
            {
                if (state.PlayersInGame[i].Name == space.Owner)
                {
                    owner = state.PlayersInGame[i];
                    break;
                }
            }

            if (owner == null)
                return "Não é necessário pagar aluguer.";

            int price = PriceTable.GetPrice(space.Name);
            if (price == -1)
                return "Não é necessário pagar aluguer.";

            int houses = space.Houses;
            int rent = (int)(price * 0.25 + price * 0.75 * houses);

            if (player.Money < rent)
                return PlayerLoses(player);

            player.Money -= rent;
            owner.Money += rent;

            return "Aluguer pago.";
        }

        public string BuyHouse(string playerName, string spaceName)
        {
            if (!state.GameInProgress)
                return "Não existe um jogo em curso.";

            if (!IsPlayerTurn(playerName))
                return "Não é a vez do jogador.";

            if (!state.HasRolledThisTurn)
                return "O jogador ainda tem ações a fazer.";

            Player player = state.PlayersInGame[state.CurrentPlayerIndex];
            Space currentSpace = board.Get(player.Row, player.Col);

            if (currentSpace.Name != spaceName)
                return "Não é possível comprar casa no espaço indicado.";

            if (currentSpace.Owner != player.Name)
                return "Não é possível comprar casa no espaço indicado.";

            if (currentSpace.Houses >= 4)
                return "Não é possível comprar casa no espaço indicado.";

            int spacePrice = PriceTable.GetPrice(spaceName);
            if (spacePrice == -1)
                return "Não é possível comprar casa no espaço indicado.";

            string? color = PriceTable.GetColor(spaceName);
            if (color == null)
                return "Não é possível comprar casa no espaço indicado.";

            foreach (var s in GetAllSpaces())
            {
                if (PriceTable.GetColor(s.Name) == color && s.Owner != player.Name)
                    return "O jogador não possui todos os espaços da cor.";
            }

            int housePrice = (int)(spacePrice * 0.6);

            if (player.Money < housePrice)
                return "O jogador não possui dinheiro suficiente.";

            player.Money -= housePrice;
            currentSpace.Houses++;

            return "Casa adquirida.";
        }

        private List<Space> GetAllSpaces()
        {
            List<Space> spaces = new List<Space>();

            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    spaces.Add(board.Get(r, c));

            return spaces;
        }

        public string DrawCard(string name)
        {
            if (!state.GameInProgress)
                return "Não existe um jogo em curso.";

            if (!IsPlayerTurn(name))
                return "Não é a vez do jogador.";

            if (!state.HasRolledThisTurn)
                return "O jogador ainda tem ações a fazer.";

            if (state.HasDrawnCardThisTurn)
                return "A carta já foi tirada.";

            Player player = state.PlayersInGame[state.CurrentPlayerIndex];
            Space space = board.Get(player.Row, player.Col);

            if (space.Name != "Chance" && space.Name != "Community")
                return "Não é possível tirar carta neste espaço.";

            Random rnd = new Random();
            int roll = rnd.Next(1, 101);

            state.HasDrawnCardThisTurn = true;

            if (space.Name == "Chance")
            {
                if (roll <= 20)
                {
                    player.Money += 150;
                    return "Chance: O jogador recebe 150.";
                }
                else if (roll <= 30)
                {
                    player.Money += 200;
                    return "Chance: O jogador recebe 200.";
                }
                else if (roll <= 40)
                {
                    if (player.Money < 70)
                        return PlayerLoses(player);

                    player.Money -= 70;
                    state.FreeParkMoney += 70;
                    return "Chance: O jogador paga 70.";
                }
                else if (roll <= 60)
                {
                    MovePlayerTo("Start", player);
                    return "Chance: O jogador move-se para Start.";
                }
                else if (roll <= 80)
                {
                    MovePlayerTo("Police", player);
                    return "Chance: O jogador move-se para Police.";
                }
                else
                {
                    MovePlayerTo("FreePark", player);
                    return "Chance: O jogador move-se para FreePark.";
                }
            }

            if (space.Name == "Community")
            {
                if (roll <= 10)
                {
                    int total = 0;

                    foreach (var s in GetAllSpaces())
                        if (s.Owner == player.Name)
                            total += s.Houses * 20;

                    if (player.Money < total)
                        return PlayerLoses(player);

                    player.Money -= total;
                    state.FreeParkMoney += total;
                    return "Community: O jogador paga 20 por cada casa.";
                }
                else if (roll <= 20)
                {
                    int gain = 0;

                    for (int i = 0; i < state.PlayersInGame.Count; i++)
                    {
                        Player p = state.PlayersInGame[i];

                        if (p.Name != player.Name)
                        {
                            if (p.Money < 10)
                                return PlayerLoses(p);

                            p.Money -= 10;
                            state.FreeParkMoney += 10;
                            gain += 10;
                        }
                    }

                    player.Money += gain;
                    return "Community: O jogador recebe 10 de cada outro jogador.";
                }
                else if (roll <= 40)
                {
                    player.Money += 100;
                    return "Community: O jogador recebe 100.";
                }
                else if (roll <= 60)
                {
                    player.Money += 170;
                    return "Community: O jogador recebe 170.";
                }
                else if (roll <= 70)
                {
                    if (player.Money < 40)
                        return PlayerLoses(player);

                    player.Money -= 40;
                    state.FreeParkMoney += 40;
                    return "Community: O jogador paga 40.";
                }
                else if (roll <= 80)
                {
                    MovePlayerTo("Pink1", player);
                    return "Community: O jogador move-se para Pink1.";
                }
                else if (roll <= 90)
                {
                    MovePlayerTo("Teal2", player);
                    return "Community: O jogador move-se para Teal2.";
                }
                else
                {
                    MovePlayerTo("White2", player);
                    return "Community: O jogador move-se para White2.";
                }
            }

            return "Erro inesperado.";
        }

        private void MovePlayerTo(string spaceName, Player player)
        {
            for (int r = 0; r < Board.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    if (board.Get(r, c).Name == spaceName)
                    {
                        player.Row = r;
                        player.Col = c;
                        return;
                    }
                }
            }
        }

        private string PlayerLoses(Player player)
        {
            state.PlayersInGame.Remove(player);

            if (state.PlayersInGame.Count == 1)
            {
                return "O jogador " + player.Name + " perdeu o jogo. " +
                       "O jogador " + state.PlayersInGame[0].Name + " venceu o jogo.";
            }

            if (state.CurrentPlayerIndex >= state.PlayersInGame.Count)
                state.CurrentPlayerIndex = 0;

            state.HasRolledThisTurn = false;
            state.HasDrawnCardThisTurn = false;

            return "O jogador " + player.Name + " perdeu o jogo.";
        }
    }
}
