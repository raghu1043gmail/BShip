using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BShip.Models
{
    public class Ship
    {
        public int Size { get; }
        public string Name { get; set; }
        public string Color { get; set; }
        public Ship(int size)
        {
            Size = size;
        }
    }
    public class Battleship : Ship
    {
        public Battleship(string name) : base(5) 
        {
            Name = name;
            Color = "lightblue";
        }
    }

    public class Destroyer : Ship
    {
        public Destroyer(string name) : base(4) 
        {
            Name = name;
            Color = "orange";
        }
    }
}