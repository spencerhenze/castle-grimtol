using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Health { get; set; }
        public bool disguised = false;
        public List<Item> Inventory { get; set; }

        public Player()
        {
            Health = 100;
        }


    }
}