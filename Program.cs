using System;
using System.IO;
using System.Collections.Generic;

namespace MooGame
{
    class MainClass
    {

    // naming
    // funktioner/metoder
    // kommentarer
    // klasser
    // felhantering
    // clean tests

        public static void Main(string[] args)
        {
            //Renamed from playOn to be more specific
            bool playGame = true;
            Console.WriteLine("Enter your user name:\n");
            //Changed from "name" to userName for clarity
            string userName = Console.ReadLine();

            while (playGame)
            {
                //Renamed to correctAnswer instead of makeGoal (small letter)
                string correctAnswer = WinGame();


                Console.WriteLine("New game:\n");
                //comment out or remove next line to play real games!
                //Console.WriteLine("For practice, number is: " + correctAnswer + "\n");

                //Renamed to userGuess for clarity
                string userGuess = Console.ReadLine();

                //Renamed from nGuess for clarity
                int numberOfGuesses = 1;
                //Renamed from bbcc for clarity
                string bullsAndCows = CheckBullsAndCows(correctAnswer, userGuess);
                Console.WriteLine(bullsAndCows + "\n");
                while (bullsAndCows != "BBBB,")
                {
                    numberOfGuesses++;
                    userGuess = Console.ReadLine();
                    Console.WriteLine(userGuess + "\n");
                    bullsAndCows = CheckBullsAndCows(correctAnswer, userGuess);
                    Console.WriteLine(bullsAndCows + "\n");
                }
                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(userName + "#&#" + numberOfGuesses);
                output.Close();
                //Renamed from showTopList for clarity
                HighScore();
                Console.WriteLine("Correct, it took " + numberOfGuesses + " guesses\nContinue?");
                //renamed from answer for clarity
                string keepPlaying = Console.ReadLine();
                if (keepPlaying != null && keepPlaying != "" && keepPlaying.Substring(0, 1) == "n")
                {
                    playGame = false;
                }
            }
        }
        static string WinGame()
        {
            //Renamed from randomGenerator
            Random randomNumbers = new Random();
            //Renamed from goal
            string correctAnswer = "";
            // Magic number, change to a constant int called max.
            for (int i = 0; i < 4; i++)
            {
                //Changed from random
                int randomNumber = randomNumbers.Next(10);
                //Changedfrom randomDigit
                string generatedNumberSequence = "" + randomNumber;
                while (correctAnswer.Contains(generatedNumberSequence))
                {
                    randomNumber = randomNumbers.Next(10);
                    generatedNumberSequence = "" + randomNumber;
                }
                // shortened from correctAnswer = correctAnswer + generatedNumberSequence;
                correctAnswer += generatedNumberSequence;
            }
            return correctAnswer;
        }

        //Renamed from checkBC (small letter at the start) for clarity
        static string CheckBullsAndCows(string goal, string guess)
        {
            int cows = 0, bulls = 0;
            guess += "    ";     // if player entered less than 4 chars
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }

        //Renamed from showTopList (small letter) for clarity
        static void HighScore()
        {
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                //Renamed from pd for clarity
                PlayerData playerData = new PlayerData(name, guesses);
                int pos = results.IndexOf(playerData);
                if (pos < 0)
                {
                    results.Add(playerData);
                }
                else
                {
                    //Calling two separate methods now instead of one, breaking down responsabilities 
                    results[pos].IncreaseNumberOfGuesses(guesses);
                    results[pos].IncreaseNumberOfGames();
                }


            }
            results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            Console.WriteLine("Player   games average");
            //Renamed from p to player for clarity
            foreach (PlayerData player in results)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGames, player.Average()));
            }
            input.Close();
        }
    }

    // __________________________________________________________ CONTINUE HERE
    class PlayerData
    {
        public string Name { get; private set; }
        public int NumberOfGames { get; private set; }

        //changed this to private for clarity
        private int GuessesInTotal;


        public PlayerData(string name, int guesses)
        {
            this.Name = name;
            NumberOfGames = 1;
            GuessesInTotal = guesses;
        }

        //Separated and renamed these into two methods, one thing doing one thing. 
        public void IncreaseNumberOfGuesses(int guesses)
        {
            GuessesInTotal += guesses;
        }

        public void IncreaseNumberOfGames()
        {
            NumberOfGames++;
        }

        public double Average()
        {
            return (double)GuessesInTotal / NumberOfGames;
        }


        public override bool Equals(Object p)
        {
            return Name.Equals(((PlayerData)p).Name);
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
