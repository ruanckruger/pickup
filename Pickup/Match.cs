namespace Pickup
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Match()
        {
            Players = new HashSet<Player>();
        }

        public int MatchID { get; set; }

        [StringLength(200)]
        public string MatchMap { get; set; }

        //[Column(TypeName = "ntext")]
        //public string MatchPlayerCount { get; set; }

        [StringLength(127)]
        public string MatchHost { get; set; }

        [StringLength(127)]
        public string MatchAdmin { get; set; }

        //public string MatchStats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
    }
}
