using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dealer.Models
{
    public class Saved
    {
        public string Id { get; set; }
        public string ID_Auto { get; set; }

        public virtual DbSet<Saved> Saveds { get; set; }
    }
}