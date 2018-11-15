using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pickup.Models
{
    public class PlayerModel
    {
        public string playerID{ get; set; }
        public string playerUsername { get; set; }
        public int playerCurMatch { get; set; }
        public string playerCurRole { get; set; }
        public string playerPrefRole { get; set; }
        public byte[] playerPicture { get; set; }
        
    }
}