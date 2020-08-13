using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BShip.Models
{
    public class GameCell
    {
        public int Id { get; set; }
        public bool HasShip { get; set; }
        public string ShipName { get; set; }
        public string UniqueId { get; set; }
        public string Color { get; set; }
        public bool Sinked { get; set; }

    }
}