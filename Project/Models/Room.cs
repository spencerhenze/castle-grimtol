using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }

        public Dictionary<string, Room> Exits { get; set; }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>(); //Instantiate the list when you instantiate the class.
            Exits = new Dictionary<string, Room>();
        }

        public void UseItem(Item item)
        {

        }
    }
}