namespace Pickup
{
    using Pickup.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Player
    {
        [Required]
        [StringLength(128)]
        [Key]
        public string PlayerID { get; set; }

        [Required]
        [StringLength(127)]
        public string Username { get; set; }

        [StringLength(127)]
        public string Name { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [StringLength(15)]
        public string PreferedRole { get; set; }

        public int? CurMatch { get; set; }

        [StringLength(15)]
        public string CurRole { get; set; }

        [Required]
        public bool IsAdmin { get; set; } = false;
        
        public virtual Match Match { get; set; }
    }
}
