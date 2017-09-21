using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public interface IRoom
    {
        string Name { get; set; }
        string Description { get; set; }
        List<Item> Items { get; set; }
        Dictionary<string, Room> Exits { get; set; }


        void UseItem(Item item);

    }
}