using System;
using System.IO;
using System.Collections.Generic;
using LabMooGame;
using System.Text;

//Fokus namngivning och utbrytning

namespace MooGame;

//Remove Mainclass?
class MainClass
{
    const int MAX = 4;

    // moving out into a variable to make less vulnerable for changes


    // naming
    // funktioner/metoder
    // kommentarer
    // klasser
    // felhantering
    // clean tests

    public static void Main(string[] args)
    {
        IIO userIO = new ConsoleIO();

        bool playGame = true;
        userIO.Write("Enter your user name:\n");
        
        string userName = userIO.Read();

        while (playGame)
        {
            string filePath = "testresult.txt";

            string correctAnswer = GenerateWinningSequence();

            userIO.Write("New game:\n");

            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + correctAnswer + "\n");

            string userGuess = userIO.Read();

            int numberOfGuesses = 1;

            string bullsAndCows = CheckUserGuess(correctAnswer, userGuess);
            userIO.Write(bullsAndCows + "\n");
            while (bullsAndCows != "BBBB,")
            {
                numberOfGuesses++;
                userGuess = userIO.Read();
                userIO.Write(userGuess + "\n");
                bullsAndCows = CheckUserGuess(correctAnswer, userGuess);
                userIO.Write(bullsAndCows + "\n");
            }

            StreamWriter output = new StreamWriter(filePath, append: true);
            output.WriteLine(userName + "#&#" + numberOfGuesses);
            output.Close();

            DisplayHighScore();
            userIO.Write($"Correct, it took {numberOfGuesses} guesses\nContinue?");
        
            string keepPlaying = userIO.Read();
            if (keepPlaying != null && keepPlaying != "" && keepPlaying.Substring(0, 1) == "n")
            {
                playGame = false;
            }
        }
    }
    // Refactored method
    //RENAME TO GENERATENUMBERSEQUENCE 
    static string GenerateWinningSequence()
    {
        Random randomNumbers = new Random();

        string correctAnswer = string.Empty;
        for (int i = 0; i < MAX; i++)
        {
            int randomNumber = randomNumbers.Next(10);

            string generatedNumberSequence = randomNumber.ToString();
            while (correctAnswer.Contains(generatedNumberSequence))
            {
                randomNumber = randomNumbers.Next(10);
                generatedNumberSequence = randomNumber.ToString();
            }
            correctAnswer += generatedNumberSequence;
        }
        return correctAnswer;
    }

    //Rename and refactored method
    //handle error input
    static string CheckUserGuess(string correctAnswer, string guess)
    {
        int cows = 0, bulls = 0;
        //OBS felhantering här???? Är det del av uppgiften eller inte? Ändrar funktionalitet. FÅR lägga till try/catch
        //if (guess.Length <4 || )
        guess += "    ";     // if player entered less than 4 chars
        for (int answerIndex = 0; answerIndex < MAX; answerIndex++)
        {
            for (int guessIndex = 0; guessIndex < MAX; guessIndex++)
            {
                if (correctAnswer[answerIndex] == guess[guessIndex])
                {
                    if (answerIndex == guessIndex)
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

        String hintBulls = new('B', bulls);
        String hintCows = new('C', cows);
        StringBuilder hintResult = new();
        return hintResult.Append(hintBulls).Append(',').Append(hintCows).ToString();
    }

    // Static ?? or no? 
    static void DisplayHighScore()
    {
        string filePath = "testresult.txt";

        StreamReader input = new StreamReader(filePath);
        List<PlayerData> results = new List<PlayerData>();

        string line;
        while ((line = input.ReadLine()) != null)
        {
            string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
            string name = nameAndScore[0];
            int guesses = Convert.ToInt32(nameAndScore[1]);

            PlayerData playerData = new PlayerData(name, guesses);
            int pos = results.IndexOf(playerData);

            if (pos < 0)
            {
                results.Add(playerData);
            }
            else
            {
                results[pos].UpdateHighScoreBoard(guesses);
            }
        }
        results.Sort((p1, p2) => p1.GetAverageGuesses().CompareTo(p2.GetAverageGuesses()));
        Console.WriteLine("Player   games average");
        
        foreach (PlayerData player in results)
        {
            ///// OBS CONSOLE WRITELINE HERE
            ///// Removed the D after "{1,5}" since number of games doesn't need to be in decimal format
            ///// Also usin string interpolation to increase readability. 
            ///KÄLLA: "String interpolation provides a more readable, convenient syntax to format strings. It's easier to read than string composite formatting. "
            ///https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated

            Console.WriteLine($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
        }
        input.Close();
    }


}

// Class to store player related data
class PlayerData
{
    public string PlayerName { get; private set; }

    public int NumberOfGames { get; private set; }

    private int GuessesInTotal;


    public PlayerData(string playerName, int guesses)
    {
        this.PlayerName = playerName;
        NumberOfGames = 1;
        GuessesInTotal = guesses;
    }

    public void UpdateHighScoreBoard(int guesses)
    {
        GuessesInTotal += guesses;
        NumberOfGames++;
    }

    public double GetAverageGuesses()
    {
        return (double)GuessesInTotal / NumberOfGames;
    }

    //BRO WHEN ARE THESE USED
    public override bool Equals(Object player)
    {
        return PlayerName.Equals(((PlayerData)player).PlayerName);
    }

    // WHEN USED?
    public override int GetHashCode()
    {
        return PlayerName.GetHashCode();
    }
}
