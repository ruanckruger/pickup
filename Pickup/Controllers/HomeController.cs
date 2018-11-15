using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pickup.Models;

namespace Pickup.Controllers
{
    public class HomeController : Controller
    {
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
                            playerID = player.PlayerID,
                            playerCurRole = player.CurRole,
                            playerUsername = player.Username,
                            playerPicture = player.Picture
                        });
                        playerCount++;
                    }
                    matches.Add(new MatchesModel
                    {
                        matchID = match.MatchID,
                        matchMap = match.MatchMap,
                        matchHost = match.MatchHost,
                        matchAdmin = match.MatchAdmin,
                        matchPlayerCount = playerCount,
                        matchPlayers = curPlayers
                    });
                }
            }
            if (matches.Count == 0)
            {
                matches = null;
                return View(matches);
            }
            matches.ToList();
            //matches.Add(new MatchesModel {  matchID = "0", matchMap = "Hacienda", matchPlayerCount = 3, matchHost = "Daddy_COol", matchAdmin = "Daddy_COol",  matchPlayers = curPlayers});
            //matches.Add(new MatchModel { })
            return View(matches);
        }

            public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}