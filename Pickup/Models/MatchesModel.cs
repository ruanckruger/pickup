using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pickup.Models
{
    public class MatchesModel
    {
        public int matchID { get; set; }
        public string matchAdmin { get; set; }
        public string matchHost { get; set; }
        public string matchMap { get; set; }

        public int matchPlayerCount { get; set; }
        public List<PlayerModel> matchPlayers { get; set; }
    }
}