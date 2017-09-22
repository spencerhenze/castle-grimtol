using System;
using System.Collections.Generic;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> Commands = new List<string>();
        public bool playGame = false;

        public void Run()
        {


            while (playGame)
            {
                DisplayInfo(); // you are currently in the... room ...description
                System.Console.WriteLine("\nWhat would you like to do? (enter 'HELP' to see possible commands)\n");
                string[] UserCommands;
                string UserInput = Console.ReadLine().ToUpper();
                UserCommands = UserInput.Split(' ');


                switch (UserCommands[0])
                {
                    case "GO":
                        Go(UserCommands[1]);
                        break;
                    case "TAKE":
                        TakeItem(UserCommands[1]);
                        break;
                    case "USE":
                        UseItem(UserCommands[1]); //Finish this
                        break;
                    case "LOOK":
                        Look();
                        break;
                    case "INVENTORY":
                        DisplayInventory();
                        break;
                    case "HELP":
                        Commands.ForEach(command =>
                        {
                            Console.WriteLine(command);
                        });
                        break;

                    default:
                        System.Console.WriteLine("That's not a valid Command.");
                        break;
                }
            }
        }

        public void Reset()
        {

        }

        public void Setup()
        {
            //instantiate all the rooms
            Room WestHallway = new Room("West Hallway", "Description");
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
            //West Hallway
            WestHallway.Exits.Add("N", Barracks);
            WestHallway.Exits.Add("S", CapitainsQuarters);
            WestHallway.Exits.Add("E", CastleCourtyard);

            //Barracks
            Barracks.Exits.Add("S", WestHallway);

            //Capitain's Quarters
            CapitainsQuarters.Exits.Add("N", WestHallway);
            CapitainsQuarters.Exits.Add("E", SouthHallway);

            //South Hallway
            SouthHallway.Exits.Add("W", CapitainsQuarters);
            SouthHallway.Exits.Add("E", GuardRoom);
            SouthHallway.Exits.Add("N", CastleCourtyard);

            //Guard Room exits: west, north
            GuardRoom.Exits.Add("W", SouthHallway);
            GuardRoom.Exits.Add("N", Dungeon);

            //Dungeon
            Dungeon.Exits.Add("S", GuardRoom);

            //Castle Courtyard. Exits: N, S, W
            CastleCourtyard.Exits.Add("N", NorthHallway);
            CastleCourtyard.Exits.Add("W", WestHallway);
            CastleCourtyard.Exits.Add("S", SouthHallway);

            //North Hallway. Exits: N,S,E
            NorthHallway.Exits.Add("N", ThroneRoom);
            NorthHallway.Exits.Add("S", CastleCourtyard);
            NorthHallway.Exits.Add("E", SquireTower);

            //Squire Tower. Exits: N, W
            SquireTower.Exits.Add("N", WarRoom);
            SquireTower.Exits.Add("W", NorthHallway);

            //War Room. Exits: S
            WarRoom.Exits.Add("S", SquireTower);

            //Throne Room
            ThroneRoom.Exits.Add("S", NorthHallway);
            // Possibly add a window exit?


            //Build items to add to rooms
            //Barracks
            Item Uniform = new Item();
            Uniform.Name = "Uniform";
            Uniform.Description = "A guard uniform with which to disguise oneself";
            Item Bed1 = new Item();
            Bed1.Name = "Bed 1";
            Bed1.Description = "A good place to blend in";
            Item Bed2 = new Item();
            Bed2.Name = "Bed 2";
            Bed2.Description = "A well-known guard's sleeping place";

            //Capitain's Quarters
            Item Key = new Item();
            Key.Name = "Key";
            Key.Description = "Your access to victory";
            Item Note = new Item();
            Note.Name = "Note";
            Note.Description = "A note for the Gate Captain Ezio";
            Item VialPouch = new Item();
            VialPouch.Name = "Vial Pouch";
            VialPouch.Description = "A pouch of vials containing a green liquid";

            //Guard Room
            Item Hammer = new Item();
            Hammer.Name = "Hammer";
            Hammer.Description = "A tool whith which to pound on things";

            //Dungeon
            Item BrokenLock = new Item();
            BrokenLock.Name = "Broken Lock";
            BrokenLock.Description = "Proof of an escaped prisoner";

            //Squire Tower
            Item OverCoat = new Item();
            OverCoat.Name = "Overcoat";
            OverCoat.Description = "A good way to disguise someone. ...perhaps for an escape";

            //War Room
            Item Cup = new Item();
            Cup.Name = "Cup";
            Cup.Description = "Overly fancy (fit for a lord) goblet. Good for containing liquids...";

            //Add Items to their rooms
            Barracks.Items.Add(Uniform);
            Barracks.Items.Add(Bed1);
            Barracks.Items.Add(Bed2);

            CapitainsQuarters.Items.Add(Key);
            CapitainsQuarters.Items.Add(Note);
            CapitainsQuarters.Items.Add(VialPouch);

            GuardRoom.Items.Add(Hammer);

            Dungeon.Items.Add(BrokenLock);

            SquireTower.Items.Add(OverCoat);

            WarRoom.Items.Add(Cup);

            //Add commands
            Commands.Add("GO <N, S, E, W>: moves you to a different room");
            Commands.Add("TAKE <Item Name>: adds item to your inventory");
            Commands.Add("USE <Item Name>: uses item in your inventory");
            Commands.Add("LOOK: displays the room description");
            Commands.Add("INVENTORY: displays all items in your inventory");

            //Set CurrentRoom
            CurrentRoom = WestHallway;
            // DisplayOptions();
        }

        public void Go(string direction)
        {
            //validate
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                CurrentRoom = CurrentRoom.Exits[direction]; // need to read out the room name and description when a room is entered.
            }
            else
            {
                Console.WriteLine("Crap! Can't go that way.");
            }
        }


        public void UseItem(string itemName)
        {
            // Check to see if the player has the item
            for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
            {
                Item item = CurrentPlayer.Inventory[i];
                if (item.Name == itemName)
                {
                    //use item
                    System.Console.WriteLine($"\nUsing {item.Name}\n");
                    //TODO: this needs to actually do something.
                    return;
                }
            }
            System.Console.WriteLine("You don't have that item.");
        }

        public void TakeItem(string itemName)
        {
            for (int i = 0; i < CurrentRoom.Items.Count; i++)
            {
                Item item = CurrentRoom.Items[i];
                if (item.Name == itemName)
                {
                    CurrentPlayer.Inventory.Add(item);
                    {
                        Console.WriteLine($"{item.Name} added to your inventory");
                        return;
                    }
                }
                System.Console.WriteLine("Not a valid Item.");
            }
        }

        private void DisplayInfo()
        {
            Console.WriteLine($"You are now in the {CurrentRoom.Name}.");
            Console.WriteLine($"{CurrentRoom.Description}\n\n");
        }

        public void Look()
        {
            Console.WriteLine($"You are in the {CurrentRoom.Name}.");
            Console.WriteLine($"{CurrentRoom.Description}\n\n");
        }

        private void DisplayInventory()
        {
            int itemIndex = 1;
            CurrentPlayer.Inventory.ForEach(item =>
            {
                System.Console.WriteLine($"{item.Name}: {item.Description}");
                itemIndex++;
            });
        }
    }
}
