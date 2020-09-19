using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimGame
{
    public class Nim
    {
        private enum Players { User, Computer };

        static void Main(string[] args)
        {
            NimGame();
        }

        private static void NimGame()
        {
            string Input;
            bool Exit = false;

            PrintIntroduction();

            do
            {
                StartGame();

                Console.WriteLine("Thank you for playing! Do you want to play again? Enter \'yes\' to continue.");
                Input = Console.ReadLine();

                if (!Input.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    Exit = true;

            } while (!Exit);
        }

        private static void PrintIntroduction()
        {
            Console.WriteLine("The Game of Nim!\n"
                + "In this game, there contains several heaps of objects, each containing a unique number of objects.\n"
                + "You and the computer AI will each take turns removing at least one object at a time\n"
                + "from the heap you specify until no heaps contain any objects.\n"
                + "The player who removes the last object from the last heap standing loses.\n\n");
        }

        private static void StartGame()
        {
            Players CurrentPlayer = Players.User;
            NimAi Computer = new NimAi();
            int HeapNumber = 0, ObjectsToRemove = 0;
            bool Winner = false;
            heap[] Heaps = new heap[GetIntInRange(0, 10, "How many heaps do you want this game to have?")];

            InitializeHeapsizes(Heaps);
            CurrentPlayer = GetFirstPlayer();

            while (!Winner)
            {
                PrintHeaps(Heaps);
                Console.WriteLine();
                if (CurrentPlayer == Players.User)
                {
                    GetPlayerMove(Heaps, out HeapNumber, out ObjectsToRemove);
                    Heaps[HeapNumber].Count -= ObjectsToRemove;
                    StateMove(CurrentPlayer, HeapNumber, ObjectsToRemove);
                }
                else // This means that the player is the computer.
                {
                    Computer.GetMove(Heaps, out HeapNumber, out ObjectsToRemove);
                    Heaps[HeapNumber].Count -= ObjectsToRemove;
                    StateMove(CurrentPlayer, HeapNumber, ObjectsToRemove);
                }

                Winner = CheckWhoIsWinner(Heaps);

                if (Winner)
                    AnnounceWinner(CurrentPlayer);
                else
                    if (CurrentPlayer == Players.User)
                    CurrentPlayer = Players.Computer;
                else
                    CurrentPlayer = Players.User;
            }

        }

        private static void StateMove(Players CurrentPlayer, int HeapNumber, int ObjectsToRemove)
        {
            string PlayerDescription = "";

            if (CurrentPlayer == Players.User)
            {
                PlayerDescription = "You have";
            }
            else
            {
                PlayerDescription = "The Computer has";
            }
            Console.WriteLine(PlayerDescription + " taken {0} objects from heap {1}.", ObjectsToRemove, HeapNumber + 1);
        }

        private static void AnnounceWinner(Players CurrentPlayer)
        {
            if (CurrentPlayer == Players.User)
            {
                Console.WriteLine("Congrats! You have won the Game of Nim!");
            }
            else
            {
                Console.WriteLine("Damn! The Computer has beaten you. Good game. Better luck next time!");
            }

        }

        private static bool CheckWhoIsWinner(heap[] Heaps)
        {
            foreach (heap heap in Heaps)
            {
                if (heap.Count > 0)
                    return false;
            }

            return true;
        }

        private static void GetPlayerMove(heap[] Heaps, out int HeapNumber, out int ObjectsToRemove)
        {
            bool ValidHeap = false;

            do
            {
                HeapNumber = GetIntInRange(1, Heaps.Length, "Please enter the heap you wish to remove objects from.") - 1;

                if (Heaps[HeapNumber].Count == 0)
                {

                    Console.WriteLine("You may only select a heap that still has objects in it!");
                }
                else
                {
                    ValidHeap = true;
                }
            } while (!ValidHeap);

            ObjectsToRemove = GetIntInRange(1, Heaps[HeapNumber].Count,
                String.Format("Please enter how many objects you wish to remove from the heap {0}.", HeapNumber + 1));

            Console.WriteLine();
        }

        private static Players GetFirstPlayer()
        {
            string Input;
            Players FirstPlayer;
            Console.WriteLine("Do you want to have the first move?");
            Console.WriteLine("Enter \'yes\' to play first, otherwise you will go second.");
            Input = Console.ReadLine();

            if (Input.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("You will go first.");
                FirstPlayer = Players.User;
            }
            else
            {
                Console.WriteLine("The computer will go first.");
                FirstPlayer = Players.Computer;
            }

            Console.WriteLine();
            return FirstPlayer;
        }

        private static void PrintHeaps(heap[] Heaps)
        {
            for (int i = 0; i < Heaps.Length; i++)
            {
                Console.WriteLine("heap {0}: {1}\t {2,10}", i + 1, Heaps[i].Count, Heaps[i].ToString());
            }
            Console.WriteLine();
        }

        private static void InitializeHeapsizes(heap[] Heaps)
        {
            for (int i = 0; i < Heaps.Length; i++)
            {
                Heaps[i] = new heap(GetIntInRange(0, int.MaxValue,
                    String.Format("Please enter how many objects you wish there to be in heap {0}.", i + 1)));
            }
            Console.WriteLine();
        }

        private static int GetIntInRange(int Min, int Max, string message)
        {

            int Number = 0;
            bool ValidInput = false;

            if (Min > Max)
                throw new ArgumentException("The minimum value must be less than the maximum value!");

            while (!ValidInput)
            {
                Console.WriteLine(message);
                if (!int.TryParse(Console.ReadLine(), out Number))
                {

                    Console.WriteLine("You can only enter a whole Number.");
                }
                else if (Number < Min || Number > Max)
                {

                    Console.WriteLine("You can only enter a number between {0} and {1}.", Min, Max);
                }
                else
                {
                    ValidInput = true;
                }
            }
            return Number;
        }
    }
}
