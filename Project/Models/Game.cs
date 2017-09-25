using System;
using System.Collections.Generic;
using System.Linq;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<string> Commands = new List<string>();
        public bool playGame = false;
        int Attempts = 0;
        bool CapitainsRoomEntered = false;

        public Game(Player player)
        {
            CurrentPlayer = player;
            CurrentPlayer.Score = 0;
            CurrentPlayer.Inventory = new List<Item>();
        }

        public void Run()
        {


            while (playGame)
            {
                Console.Clear();
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
                        UserCommands = null;
                        System.Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "USE":
                        UseItem(UserCommands[1]); //Finish this
                        UserCommands = null;
                        System.Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();

                        break;
                    case "LOOK":
                        Look();
                        System.Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();

                        break;
                    case "INVENTORY":
                        DisplayInventory();
                        UserCommands = null;
                        System.Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "HELP":
                        Commands.ForEach(command =>
                        {
                            Console.WriteLine(command);
                        });
                        System.Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    case "QUIT":
                        System.Console.WriteLine("\nSee ya! press any key to finish quitting the game.");
                        Console.ReadKey();
                        System.Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine("That's not a valid Command.");
                        break;
                }
            }
        }

        public void Reset()
        {
            CurrentPlayer.Health = 100;
            CurrentPlayer.Score = 0;
            Commands.Clear();
            CurrentPlayer.Inventory.Clear();
            CapitainsRoomEntered = false;
            Setup();
            DisplayInfo();
            Run();
        }

        public void Setup()
        {
            Attempts++;
            //instantiate all the rooms
            Room WestHallway = new Room("West Hallway", "You find yourself in a small hall. There doesnt appear to be anything of interest here.\n");
            Room SouthHallway = new Room("South Hallway", "You find yourself in a small hall. There doesnt appear to be anything of interest here.\n");
            Room NorthHallway = new Room("North Hallway", "You find yourself in a small hall. There doesnt appear to be anything of interest here.\n");
            Room Barracks = new Room("Barracks", "You see a room with several sleeping guards, The room smells of sweaty men. You see two beds (Bed1 and Bed2) The bed closest to you (Bed1) is empty and there are several uniforms tossed about.\n");
            Room GuardRoom = new Room("Guard Room", "Pushing open the door of the guard room you look around and notice the room is empty, There are a few small tools in the corner and a chair propped against the wall near the that likely leads to the dungeon.\n");
            Room Dungeon = new Room("Dungeon", "As you descend the stairs to the dungeon you notice a harsh chill to the air. Landing at the base of the stairs you see the remains of what appears to be a previous prisoner.\n");
            Room CapitainsQuarters = new Room("Capitain's Quarters", "As you approach the captains Quarters you swallow hard and notice your lips are dry, Stepping into the room you see a few small tables and maps of the countryside sprawled out.\n");
            Room SquireTower = new Room("Squire Tower", "As you finish climbing the stairs to the squire tower you see an empty and bed and messy room. You see a large crossbow propped against the wall in the corner.\n");
            Room WarRoom = new Room("War Room", "Steping into the war room you see several maps spread across tables. On the maps many of the villages have been marked for purification. You also notice several dishes of prepared food to the side; perhaps the war council will be meeting soon.\n");
            Room ThroneRoom = new Room("Throne Room", "As you unlock the door and swing it wide you see an enormous hall stretching out before you. At the opposite end of the hall sitting on his throne you see the dark lord. The Dark Lord shouts at you demanding why you dared to interrupt him during his Ritual of Evil Summoning... Dumbfounded you mutter an incoherent response. Becoming more enraged the Dark Lord complains that you just ruined his concentration and he will now have to start the ritual over... Quickly striding towards you he smirks at least I know have a sacrificial volunteer...\n");
            Room CastleCourtyard = new Room("Castle Courtyard", "You step into the large castle courtyard there is a flowing fountain in the middle of the grounds and a few guards patrolling the area.\n");
            Room Any = new Room("ANY", "");

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
            Uniform.Name = "UNIFORM";
            Uniform.Description = "A guard uniform with which to disguise oneself";
            Uniform.RelevantRooms.Add(Barracks);
            Uniform.AnyRoom = true;
            Item Bed1 = new Item();
            Bed1.Name = "BED1";
            Bed1.Description = "A good place to blend in";
            Bed1.Collectable = false;
            Item Bed2 = new Item();
            Bed2.Name = "BED2";
            Bed2.Description = "A well-known guard's sleeping place";
            Bed2.Collectable = false;

            //Capitain's Quarters
            Item Key = new Item();
            Key.Name = "KEY";
            Key.Description = "Your access to victory";
            Key.RelevantRooms.Add(ThroneRoom);
            Key.Available = false;
            Key.Collectable = false;
            // Item Note = new Item();
            // Note.Name = "NOTE";
            // Note.Description = "A note for the Gate Captain Ezio";
            // Note.Collectable = false;
            // Note.RelevantRooms.Add(SquireTower);
            Item CapitainsPurse = new Item();
            CapitainsPurse.Name = "PURSE";
            CapitainsPurse.Available = false;
            CapitainsPurse.Collectable = false;
            CapitainsPurse.Description = "A purse full of money";
            CapitainsPurse.RelevantRooms.Add(Any);

            //Guard Room
            Item Hammer = new Item();
            Hammer.Name = "HAMMER";
            Hammer.Description = "A tool whith which to pound on things";
            Hammer.RelevantRooms.Add(Dungeon);

            //Dungeon
            Item BrokenLock = new Item();
            BrokenLock.Name = "BROKEN_LOCK";
            BrokenLock.Description = "Proof of an escaped prisoner";
            BrokenLock.RelevantRooms.Add(CapitainsQuarters);
            BrokenLock.Collectable = false;

            //Squire Tower
            // Item OverCoat = new Item();
            // OverCoat.Name = "OVERCOAT";
            // OverCoat.Description = "A good way to disguise someone. ...perhaps for an escape";
            // OverCoat.RelevantRooms.Add(Dungeon);

            Item CrossBow = new Item();
            CrossBow.Name = "CROSSBOW";
            CrossBow.Description = "A deadly weapon.";
            CrossBow.RelevantRooms.Add(ThroneRoom);

            //War Room
            // Item Cup = new Item();
            // Cup.Name = "CUP";
            // Cup.Description = "Overly fancy (fit for a lord) goblet. Good for containing liquids...";
            // Cup.RelevantRooms.Add(ThroneRoom);

            //Throne Room
            Item Window = new Item();
            Window.Name = "WINDOW";
            Window.Collectable = false;
            Window.Description = "A good place to escape";

            //Add Items to their rooms
            Barracks.Items.Add(Uniform);
            Barracks.Items.Add(Bed1);
            Barracks.Items.Add(Bed2);

            CapitainsQuarters.Items.Add(Key);
            // CapitainsQuarters.Items.Add(Note);
            CapitainsQuarters.Items.Add(CapitainsPurse);

            GuardRoom.Items.Add(Hammer);

            Dungeon.Items.Add(BrokenLock);

            // SquireTower.Items.Add(OverCoat);

            // WarRoom.Items.Add(Cup);

            SquireTower.Items.Add(CrossBow);

            ThroneRoom.Items.Add(Window);

            //Add commands
            Commands.Add("GO <N, S, E, W>: moves you to a different room");
            Commands.Add("TAKE <Item Name>: adds item to your inventory");
            Commands.Add("USE <Item Name>: uses item in your inventory");
            Commands.Add("LOOK: displays the room description");
            Commands.Add("INVENTORY: displays all items in your inventory");
            Commands.Add("QUIT: Admit that you are a weakling and quit the game");

            //Set CurrentRoom
            CurrentRoom = WestHallway;
            // DisplayOptions();
        }

        public void Go(string direction)
        {
            //validate
            if (CurrentRoom.Exits.ContainsKey(direction))
            {
                //Make sure they have the key to enter the throne room
                if (CurrentRoom.Exits[direction].Name == "Throne Room")
                {
                    for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
                    {
                        Item inventoryItem = CurrentPlayer.Inventory[i];
                        if (inventoryItem.Name == "KEY")
                        {
                            CurrentRoom = CurrentRoom.Exits[direction];
                            Console.Clear();
                            DisplayInfo();
                            RoomCheck();
                            return;
                        }
                    }

                    System.Console.WriteLine("\nBlast! This room appears to be locked. If only you had the key...");
                    System.Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    return;

                }
                // For any other room, just update the current room
                CurrentRoom = CurrentRoom.Exits[direction]; // need to read out the room name and description when a room is entered.
                RoomCheck();
            }
            else
            {
                Console.WriteLine("Crap! Can't go that way.");
                Console.WriteLine("Press any key to try again");
                Console.ReadKey();
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
                    //Item found in inventory.
                    //Check to see if it can be used in this room
                    if (item.RelevantRooms.Contains(CurrentRoom) || item.AnyRoom)
                    {
                        //use item
                        System.Console.WriteLine($"\nUsing {item.Name}\n");
                        switch (item.Name)
                        {
                            case "UNIFORM":
                                CurrentPlayer.disguised = true;
                                System.Console.WriteLine("You're incognito. You can now move about the castle disguised as a guard.");
                                break;
                            case "HAMMER":
                                // CurrentRoom.Items.Find(entry => item.Name == "BROKEN LOCK").Collectable = true;
                                for (int j = 0; j < CurrentRoom.Items.Count; j++)
                                {
                                    Item roomItem = CurrentRoom.Items[j];
                                    if (roomItem.Name == "BROKEN_LOCK")
                                    {
                                        roomItem.Collectable = true;
                                        System.Console.WriteLine("You should be able to pick up the broken lock now");
                                    }
                                }
                                break;
                            case "BROKEN_LOCK":
                                if (CapitainsRoomEntered)
                                {
                                    Console.Clear();
                                    System.Console.WriteLine("\"What?! an escaped prisoner...?! When did this happen? Quick take this! (He slams a silver key on the table) Keep this quiet. If that prisoner really has escaped it will be both our heads. I'll need to go grab a random surf and lock him up in the dungeon just in case anyone checks to see if the prisoner is there.\" (CAPTAIN) \"I'll go rouse the guards.\" (The captain runs to the door north heading for the Barracks)");
                                    // Enable key for pick-up
                                    for (int j = 0; j < CurrentRoom.Items.Count; j++)
                                    {
                                        Item roomItem = CurrentRoom.Items[j];
                                        if (roomItem.Name == "KEY")
                                        {
                                            roomItem.Collectable = true;
                                            roomItem.Available = true;
                                            System.Console.WriteLine("\n\nYou should be able to pick up the KEY now.");
                                        }
                                        if (roomItem.Name == "PURSE")
                                        {
                                            roomItem.Collectable = true;
                                            roomItem.Available = true;
                                        }
                                    }


                                    System.Console.WriteLine("\nPress any key to continue...");
                                    Console.ReadKey();

                                    if (!CurrentPlayer.Inventory.Any(weapon => item.Name.Contains("PURSE")))
                                    {
                                        Console.Clear();
                                        System.Console.WriteLine("With the captain gone you look around and notice he left his coin purse on the desk. There appears to be a decent amount of money in it.\n");
                                    }
                                }
                                break;
                            case "CROSSBOW":
                                Console.Clear();
                                System.Console.WriteLine("\"Come at me you evil jerk!\" you yell as you whip out the crossbow that was hiding behind your back.\n\nShocked the dark lord haults for a second. Just long enough for you to take aim. As his surprise turns to rage and he prepares to charge you, you somehow loose a three arrow burst directly into his face. At first he seems mostly unaffected by the shots, but suddenly goes limp and crashes to the floor, sliding a few feet on his face and landing directly at your feet...");
                                System.Console.WriteLine("\n\nYou've done it! You've killed the dark lord! ...now you've gotta get out of here!!! (HINT: there's a window to the N!");
                                CurrentPlayer.Score += 200;
                                break;
                        }

                    }
                    else
                    {
                        System.Console.WriteLine($"You cant use the {item.Name} in this room.");
                    }
                    return;
                }
            }
            // if it is just a room item
            for (int i = 0; i < CurrentRoom.Items.Count; i++)
            {
                Item item = CurrentRoom.Items[i];
                if (item.Name == itemName)
                {
                    switch (item.Name)
                    {
                        case "BED1":
                            Console.Clear();
                            System.Console.WriteLine("You climb into the bed and pretend to be asleep. A few minutes later several guards walk into the room. One approaches you to wake you... (GUARD)" + "Hey Get Up! it's your turn for watch, Go relieve Shigeru in the Guard Room!\" Quickly you climb out of the bed");
                            CurrentPlayer.Score += 50;
                            System.Console.WriteLine("Nice Work! You've earned 50 points!");
                            return;
                        case "BED2":
                            CurrentPlayer.Health = 0;
                            Console.Clear();
                            DisplayInfo();
                            System.Console.WriteLine("\n(GUARD) \"What do you think your doing? Hey your not Leroy, Quick Jenkins sieze him....\" Jenkins a bit over-zelous swings his sword cleaving you in half... ");
                            System.Console.WriteLine("\nDang... You're dead.");
                            TryAgain();
                            return;
                        case "WINDOW":
                            Console.Clear();
                            System.Console.WriteLine($"Congratulations {CurrentPlayer.Name}! You've escaped and returned to your family after successfully killing the dark lord! You've won the game!!!");
                            System.Console.WriteLine($"\nYour score was: {CurrentPlayer.Score}.");
                            TryAgain();
                            break;
                    }
                }
            }
            System.Console.WriteLine("You don't have that item or item is not in the room.");
        }

        public void TakeItem(string itemName)
        {
            itemName.ToUpper();
            // Find Item
            for (int i = 0; i < CurrentRoom.Items.Count; i++)
            {
                Item item = CurrentRoom.Items[i];
                if (item.Name == itemName)
                {
                    if (item.Collectable)
                    {
                        CurrentPlayer.Inventory.Add(item);
                        Console.WriteLine($"\nNice! {item.Name} added to your inventory.");
                        // instruct user to use the uniform immediately
                        if (item.Name == "UNIFORM")
                        {
                            Console.WriteLine("Use this immediately!");
                        }
                        // remove the item from the list of available items in the room
                        CurrentRoom.Items.Remove(item);
                        if (item.Name == "PURSE")
                        {
                            System.Console.WriteLine("\nChaching! Your family should be set for a while now.");
                            CurrentPlayer.Score += 100;
                        }
                        return;
                    }
                    else
                    {
                        System.Console.WriteLine("This item can't be picked up.");
                        return;
                    }
                }
            }
            // Item not found
            System.Console.WriteLine("Not a valid Item.\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DisplayInfo()
        {
            Console.WriteLine($"Player: {CurrentPlayer.Name}       Health: {CurrentPlayer.Health}       Score: {CurrentPlayer.Score}\n");
            Console.WriteLine($"You are now in the {CurrentRoom.Name}.");
            Console.WriteLine($"{CurrentRoom.Description}");
            Console.WriteLine("Exits:");
            foreach (KeyValuePair<string, Room> exit in CurrentRoom.Exits)
            {
                System.Console.WriteLine($"{exit.Key}: {exit.Value.Name}");
            }
            if (CurrentRoom.Items.Count > 0)
            {
                Console.WriteLine("\nItems in Room:");
                CurrentRoom.Items.ForEach(item =>
                {
                    if (item.Available)
                    {
                        Console.WriteLine(item.Name);
                    }
                });
            }
            else
            {
                Console.WriteLine("\nNo Items in this room");
            }
        }

        public void Look()
        {
            Console.Clear();
            Console.WriteLine($"You are in the {CurrentRoom.Name}.");
            Console.WriteLine($"{CurrentRoom.Description}\n\n");
        }

        private void DisplayInventory()
        {
            int itemIndex = 1;
            Console.WriteLine();
            if (CurrentPlayer.Inventory.Count > 0)
            {
                System.Console.WriteLine("Your Items:");
                CurrentPlayer.Inventory.ForEach(item =>
                {
                    System.Console.WriteLine($"* {item.Name}: {item.Description}");
                    itemIndex++;
                });
            }
            else
            {
                System.Console.WriteLine("You have no items yet.");
            }
        }

        private void TryAgain()
        {
            bool showMenu = true;
            while (showMenu)
            {
                Console.WriteLine("\nDo you want to play again? (Y/N)");
                string Decision = Console.ReadLine().ToUpper();
                if (Decision == "Y")
                {
                    Reset();
                }
                if (Decision == "N")
                {
                    Environment.Exit(0);
                }
                else
                {
                    System.Console.WriteLine("Sorry, that's not a valid option. Try again.");
                }
            }
        }

        private void RoomCheck()
        {
            // System.Console.WriteLine($"made it to room check. Current room is: {CurrentRoom.Name}");
            if (CurrentPlayer.disguised)
            {

                switch (CurrentRoom.Name)
                {
                    case "Capitain's Quarters":
                        if (CurrentPlayer.Inventory.Any(item => item.Name.Contains("BROKEN_LOCK")) && CapitainsRoomEntered)
                        {
                            Console.Clear();
                            System.Console.WriteLine("\"\nWhat are you doing back here? I told you to stay in the Guard Room\" \"You'd better have a good reason to be back here!\"\nHINT: Use broken lock!");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        else if (!CurrentPlayer.Inventory.Any(item => item.Name.Contains("BROKEN_LOCK")) && CapitainsRoomEntered)
                        {
                            Console.Clear();
                            System.Console.WriteLine("\"What are you doing back here? I told you to stay in the Guard Room!\" \"Dah!\"");
                            System.Console.WriteLine("\nThe Capitain is pissed and slices your throat. You die.");
                            TryAgain();
                        }

                        else if (!CapitainsRoomEntered)
                        {
                            Console.Clear();
                            System.Console.WriteLine("The captain on shift greets you (CAPTAIN) \"New recruit huh. Well lets stick you in the guard room you can't screw things up there. Go relieve (He pauses and glancing at his reports) private Miyamoto.\"");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                            CapitainsRoomEntered = true;
                            return;
                        }
                        break;
                    case "Throne Room":
                        if (CurrentPlayer.Inventory.Any(item => item.Name.Contains("CROSSBOW")))
                        {
                            return;
                        }
                        else
                        {
                            System.Console.WriteLine("\nThe dark lord gradually pickus up speed and ferocity and you stand there unarmed and terrified. Plunging his jewel encrusted dagger into your heart your world slowly fades away.");
                            System.Console.WriteLine("\nYou have failed the mission.");
                            TryAgain();
                            return;
                        }
                }
            }
            else // Uniform not used
            {
                if (CurrentRoom.Name == "Barracks")  // if they're entering the Barracks, they're fine. 
                {
                    return;
                }
                Console.Clear();
                System.Console.WriteLine("Oh no! A guard spotted you, (Guard) \"Hey! You're not supposed to be here!\" He comes running at you. Ten others join him. You get slashed to death...\n");
                TryAgain();
            }
        }
    }
}
