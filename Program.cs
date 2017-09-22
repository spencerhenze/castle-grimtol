using System;
using CastleGrimtol.Project;

namespace CastleGrimtol.Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            //some user input
            System.Console.WriteLine("Welcome to Castle Grimtol!\n\nThis game is not for the faint of heart. If you are pregnant or nursing or have high blood pressure, or are prone to fainting, you might want to find another game.\n\nReady to start the game? (Y/N)");
            string playGame = Console.ReadLine().ToUpper();

            if(playGame == "Y")
            {
                Player NewPlayer = new Player();
                //start game
                Console.Clear();
                System.Console.WriteLine("What is your name?\n");
                NewPlayer.Name = Console.ReadLine();
                Game newGame = new Game(NewPlayer);
                newGame.Setup();
                Console.Clear();
                Console.WriteLine($"Brave Young Warrior {NewPlayer.Name}, our forces are failing and the enemy grows stronger everyday. I fear if we don't act now our people will be driven from their homes. These dark times have left us with one final course of action. We must cut the head off of the snake by assasinating the Dark Lord of Grimtol... Our sources have identified a small tunnel that leads into the rear of the castle.\n\nOnce you sneak through the tunnel you will need to find a way to disguise yourself and kill the Dark Lord. We don't know exactly how so you'll need to use your wit and cunning to think of something.\n\nGood Luck brave one.\n\nPress ENTER to sneak through the tunnel");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Whew! You successfully snuck through the tunnel.");
                newGame.playGame = true;
                newGame.Run();
                // newGame.Go(direction);    ?
            }
            else if(playGame == "N")
            {
                Console.Clear();
                System.Console.WriteLine("That's ok. It's best to take care of your health.\n ...even if it does mean the total destruction of the kingdom of Grimtol.");
                System.Console.WriteLine("\nPress any key to finish deserting the game");
                Console.ReadKey();
            }
            else
            {
                System.Console.WriteLine("...?");
            }
            
            //MyGame.Go(directioin from the user)
        }
    }
}
