using Microsoft.AspNet.Identity;
using Pickup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pickup.Controllers
{
    public class MatchesController : Controller
    {
        // GET: Matches
        //[Authorize]
        public ActionResult Index()
        {
            List<MatchesModel> matches = new List<MatchesModel>();
            using (var db = new PickupModel())
            {
                foreach (var match in db.Matches)
                {
                    int playerCount = 0;
                    List<PlayerModel> curPlayers = new List<PlayerModel>();
                    foreach (var player in db.Players.Where(n => n.CurMatch == match.MatchID))
                    {
                        curPlayers.Add(new PlayerModel
                        {
                            playerCurRole = player.CurRole,
                            playerUsername = player.Username
                        });
                        playerCount++;
                    }
                    matches.Add(new MatchesModel
                    {
                        matchID = match.MatchID,
                        //matchMap = match.MatchMap,
                        //matchHost = match.MatchHost,
                        //matchAdmin = match.MatchAdmin,
                        //matchPlayerCount = playerCount,
                        //matchPlayers = curPlayers
                    });
                }
            }
            if (matches.Count == 0)
            {
                return null;
            }
            //matches.Add(new MatchesModel {  matchID = "0", matchMap = "Hacienda", matchPlayerCount = 3, matchHost = "Daddy_COol", matchAdmin = "Daddy_COol",  matchPlayers = curPlayers});
            //matches.Add(new MatchModel { })
            return View(matches);
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult CreateMatch()
        {
            return PartialView();
        }
        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                var curUser = User.Identity.GetUserId();
                using (var db = new PickupModel())
                {
                    var userImage = db.Players.FirstOrDefault(p => p.PlayerID == curUser).Picture;
                    return new FileContentResult(userImage, "image/jpeg");
                }
            }
            return null;
        }
        public FileContentResult PlayerPhotos(string playerId)
        {
            using (var db = new PickupModel())
            {
                var userImage = db.Players.FirstOrDefault(p => p.PlayerID == playerId).Picture;
                return new FileContentResult(userImage, "image/jpeg");
            }
        }
        public ActionResult PlayerDisplay(string playerId, int matchId)
        {
            using (var db = new PickupModel())
            {
                var match = db.Matches.FirstOrDefault(m => m.MatchID == matchId);
                
                int playerCount = 0;
                List<PlayerModel> curPlayers = new List<PlayerModel>();

                var player = db.Players.FirstOrDefault(p => p.PlayerID == playerId);
                if (player != null)
                {
                    curPlayers.Add(new PlayerModel
                    {
                        playerCurRole = player.CurRole,
                        playerUsername = player.Username,
                        playerPicture = player.Picture,
                        playerID = player.PlayerID
                    });
                }
                MatchesModel matches = new MatchesModel
                {
                    matchID = match.MatchID,
                    matchMap = match.MatchMap,
                    matchHost = match.MatchHost,
                    matchAdmin = match.MatchAdmin,
                    matchPlayerCount = playerCount,
                    matchPlayers = curPlayers
                };
                return PartialView(matches);
            }
            
        }
        public ActionResult MatchInfo(int matchId)
        {
            using (var db = new PickupModel())
            {
                var match = db.Matches.FirstOrDefault(m => m.MatchID == matchId);

                int playerCount = 0;
                List<PlayerModel> curPlayers = new List<PlayerModel>();
                foreach (var player in db.Players.Where(n => n.CurMatch == match.MatchID))
                {
                    curPlayers.Add(new PlayerModel
                    {
                        playerID = player.PlayerID
                    });
                    playerCount++;
                }

                MatchesModel matches = new MatchesModel
                {
                    matchID = match.MatchID,
                    matchMap = match.MatchMap,
                    matchHost = match.MatchHost,
                    matchAdmin = match.MatchAdmin,
                    matchPlayerCount = playerCount,
                    matchPlayers = curPlayers
                };
                return PartialView(matches);
            }
        }

    }
}