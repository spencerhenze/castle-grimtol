using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available = true;
        public bool Collectable = true;
        public bool AnyRoom = false;
        public List<Room> RelevantRooms = new List<Room>();


    }
}