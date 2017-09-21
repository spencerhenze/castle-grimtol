using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }

        public void Reset()
        {

        }

        public void Setup()
        {
            //instantiate all the rooms
            Room EastHallway = new Room("East Hallway", "Description");
            Room SouthHallway = new Room("South Hallway", "Description");
            Room NorthHallway = new Room("North Hallway", "Description");
            Room Barracks = new Room("Barracks", "Description");
            Room GuardRoom = new Room("Guard Room", "Description");
            Room Dungeon = new Room("Dungeon", "Description");
            Room CapitainsQuarters = new Room("Capitain's Quarters", "Description");
            Room SquireTower = new Room("Squire Tower", "Description");
            Room WarRoom = new Room("War Room", "Description");
            Room ThroneRoom = new Room("Throne Room", "Description");
            Room CastleCourtyard = new Room("Castle Courtyard", "Description");

    
            //then set the exits: Dungeon.Exits.Add("S", "GuardRoom")
            //East Hallway
            EastHallway.Exits.Add("N", Barracks);
            EastHallway.Exits.Add("S", CapitainsQuarters);
            EastHallway.Exits.Add("E", CastleCourtyard);

            //Barracks
            Barracks.Exits.Add("S", EastHallway);

            //Capitain's Quarters
            CapitainsQuarters.Exits.Add("N", EastHallway);
            CapitainsQuarters.Exits.Add("E", SouthHallway);

            //South Hallway
            SouthHallway.Exits.Add("W", CapitainsQuarters);
            SouthHallway.Exits.Add("E", GuardRoom);
            SouthHallway.Exits.Add("N", CastleCourtyard);
            


            //Build items to add to rooms





            CurrentRoom = Barracks;

        }

        public void Go(string direction)
        {
            //validate
            CurrentRoom = CurrentRoom.Exits[direction];
        }

        // Make a "Go" method that takes in an exit direction, finds the room at that value in the exits dictionary, and sets the CurrentRoom to that value

        public void UseItem(string itemName)
        {

        }
    }
}