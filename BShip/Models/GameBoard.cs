using BShip.Utils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BShip.Models
{
    public class GameBoard
    {
        public List<GameCell> GameCells { get; set; }
        public GameBoard()
        {
            GameCells =
             Enumerable.Range(1, 100).Select(index => new GameCell
             {
                 Id = index,
                 HasShip = false
             }).ToList();

            GameCells = DeployShip(GameCells, new Battleship("HMS Queen Elizabeth"));
            GameCells = DeployShip(GameCells, new Destroyer("HMS Duncan"));
            GameCells = DeployShip(GameCells, new Destroyer("HMS Defender"));
        }

        private List<GameCell> DeployShip(List<GameCell> GameCells, Ship ship)
        {
            var rand = new Random();
            var vertical = rand.Next(0, 2) == 1;

            while (true)
            {
                List<int> validCells = new List<int>();
                //check vertical direction
                if (vertical)
                {
                    var number = rand.Next(1, 100);

                    //with in the range
                    if (!(number + (10 * ship.Size) > 100))
                    {

                        //check if the cells already had any ship deployed
                        for (int i = number; i < number + (10 * ship.Size); i += 10)
                        {
                            validCells.Add(i);
                        }
                        if (FlagCells(validCells, ship))
                        {
                            break;
                        }
                    }
                }
                //check horizontal direction
                else
                {
                    var number = rand.Next(1, 100);
                    //get nearest number which is multiple of 10
                    var roundUpNumber = Helper.RoundUp(number);
                    if (number + ship.Size < roundUpNumber)
                    {
                        //check if the cells already had any ship
                        for (int i = number; i < number + (ship.Size); i++)
                        {
                            validCells.Add(i);
                        }

                        if (FlagCells(validCells, ship))
                        {
                            break;
                        }

                    }

                }


            }
            return GameCells;
        }

        private bool FlagCells(List<int> validCells, Ship ship)
        {
            if (validCells.Count == ship.Size && GameCells.Where(x => validCells.Contains(x.Id) && x.HasShip.Equals(false)).Count() == ship.Size)
            {
                var deployedCells = GameCells.Where(x => validCells.Contains(x.Id)).ToList();
                var uniqueId = Guid.NewGuid().ToString();
                foreach (var itm in deployedCells)
                {
                    itm.HasShip = true;
                    itm.ShipName = ship.Name;
                    itm.UniqueId = uniqueId;
                    itm.Color = ship.Color;
                }
                return true;
            }
            else
                return false;

        }


    }
}