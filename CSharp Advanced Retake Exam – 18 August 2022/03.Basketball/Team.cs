using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Basketball
{
    public class Team
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        public Team(string name, int openPositions, char group)
        {
            Name = name;
            OpenPositions = openPositions;
            Group = group;
        }

        public string Name { get; private set; }
        public int OpenPositions { get; private set; }
        public char Group { get; private set; }
        public int Count => players.Count;

        public string AddPlayer(Player player)
        {
            if (string.IsNullOrEmpty(player.Name) || string.IsNullOrEmpty(player.Position))
            {
                return "Invalid player's information.";
            }
            else if (OpenPositions == 0)
            {
                return "There are no more open positions.";
            }
            else if (player.Rating < 80)
            {
                return "Invalid player's rating.";
            }
            else
            {
                players.Add(player.Name, player);
                OpenPositions--;

                return $"Successfully added {player.Name} to the team. Remaining open positions: {OpenPositions}.";
            }
        }
        public bool RemovePlayer(string name)
        {
            if (!players.ContainsKey(name))
            {
                return false;
            }

            players.Remove(name);
            OpenPositions++;

            return true;
        }
        public int RemovePlayerByPosition(string position)
        {
            List<Player> tempPlayers = new List<Player>();

            foreach (Player player in players.Values)
            {
                if (player.Position == position)
                {
                    tempPlayers.Add(player);
                }
            }

            for (int i = 0; i < tempPlayers.Count; i++)
            {
                players.Remove(tempPlayers[i].Name);
            }

            OpenPositions += tempPlayers.Count;

            return tempPlayers.Count;
        }
        public Player RetirePlayer(string name)
        {
            if (!players.ContainsKey(name))
            {
                return null;
            }
            players[name].Retired = true;

            return players[name];
        }
        public List<Player> AwardPlayers(int games) => players.Values.Where(x => x.Games >= games).ToList();
        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Active players competing for Team {Name} from Group {Group}:");

            foreach (Player player in players.Values.Where(x => x.Retired == false))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
