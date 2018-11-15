using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Pickup.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        public void Join(int matchId)
        {
            // Call the addNewMessageToPage method to update clients.

            using (var db = new PickupModel())
            {
                //var curUserId = User.Identity.GetUserId();
                var curUserId = Context.User.Identity.GetUserId();
                Player player = db.Players.FirstOrDefault(p => p.PlayerID == curUserId);
                //Match match = db.Matches.FirstOrDefault(m => m.MatchID == matchId);
                player.CurMatch = matchId;
                db.SaveChanges();
                var newPlayerCount = db.Players.Where(p => p.CurMatch == matchId).Count();
                Clients.All.userJoined(matchId, player.PlayerID, newPlayerCount);
            }
        }
        public void Leave()
        {
            // Call the addNewMessageToPage method to update clients.
            using (var db = new PickupModel())
            {
                //var curUserId = User.Identity.GetUserId();
                var curUserId = Context.User.Identity.GetUserId();
                Player player = db.Players.FirstOrDefault(p => p.PlayerID == curUserId);
                //Match match = db.Matches.FirstOrDefault(m => m.MatchID == matchId);
                var playerCurMatch = player.CurMatch;
                player.CurMatch = null;
                db.SaveChanges();

                var newPlayerCount = db.Players.Where(p => p.CurMatch == playerCurMatch).Count();
                Clients.All.userLeft(playerCurMatch, player.PlayerID, newPlayerCount);
            }
        }
        public void CreateGame(string Map)
        {
            // Call the addNewMessageToPage method to update clients.

            using (var db = new PickupModel())
            {
                Match match = new Match {
                    MatchMap = Map,
                    MatchAdmin = Context.User.Identity.GetUserId(),
                    MatchHost = Context.User.Identity.GetUserId()
                };
                db.Matches.Add(match);
                db.SaveChanges();
                Clients.All.gameCreated(match.MatchID);
            }
        }
        public void EndGame(int matchId)
        {
            using (var db = new PickupModel())
            {
                Match match = new Match
                {
                    MatchID = matchId 
                };
                List<Player> removePlayers = db.Players.Where(p => p.CurMatch == matchId).ToList();
                foreach(var remPlayer in removePlayers)
                {
                    remPlayer.CurMatch = null;
                }
               
                db.SaveChanges();
                db.Matches.Attach(match);
                db.Matches.Remove(match);
                db.SaveChanges();
                Clients.All.gameEnded(matchId);
            }
        }
    }
}