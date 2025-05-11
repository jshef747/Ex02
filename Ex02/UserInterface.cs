namespace Ex02;
using System;

public class UserInterface
{
    private static int m_NumberOfGuesses = 4;
    private const int EXTRA_LINES = 2;
    private const string ROW = "|            |       |";
    private const string SEPARATOR = "|============|=======|";
    private const int GUESS_LENGTH = 4;
    
    public static void StartGame()
    {
        bool wonOrLost = false;
        
        Console.WriteLine("Welcome to the game!");
        
        getTableSizeAndPrint();

        for(int i = 1; i <= m_NumberOfGuesses && !wonOrLost; i++)
        {
            string guess = promptAndProcessGuess(i);
            //wonOrLost = checkGuess(guess);   // function from logic check
            writeFeedback(i, guess, "Bla");    // "bla" will return from logic check
        }
        
        Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit: ");
    }
    
    private static void getTableSizeAndPrint()
    {
        Console.WriteLine("Please enter the number of guesses: ");
        string numberOfGuesses = Console.ReadLine();
        while(!InputValidator.IsValidGuessNumber(numberOfGuesses))
        {
            Console.WriteLine(InputValidator.BadInputMessage);
            numberOfGuesses = Console.ReadLine();
        }
        
        m_NumberOfGuesses = int.Parse(numberOfGuesses);
        
        printTable();
    }

    private static void printTable()
    {
        Console.Clear();
        Console.WriteLine("Current board status:");
        Console.WriteLine();
        for(int rowNumber = 0; rowNumber <= m_NumberOfGuesses; rowNumber++)
        {
            Console.WriteLine(ROW);
            Console.WriteLine(SEPARATOR);
        }
        Console.WriteLine();
        
        printOnTableByCell(0, "Pins:");
        printOnTableByCell(1, "Result:");
    }

    private static void printOnTableByCell(int i_CellNumber, string i_StringToPrint)
    {
        int oldLeft = Console.CursorLeft;
        int oldTop = Console.CursorTop;
        int newLeft = 0;
        int newTop = 0;

        if(i_CellNumber % 2 == 0)
        {
            newLeft = 1;
            newTop = EXTRA_LINES + i_CellNumber;
        }
        else
        {
            newLeft = 14;
            newTop = EXTRA_LINES + (i_CellNumber - 1);
        }
        
        
        Console.SetCursorPosition(newLeft, newTop);
        Console.Write(i_StringToPrint);
        Console.SetCursorPosition(oldLeft, oldTop);
    }

    private static string promptAndProcessGuess(int i_GuessNumber)
    {
        int oldLeft = Console.CursorLeft;
        int oldTop = Console.CursorTop;
        string guess = "";
        
        Console.WriteLine(InputValidator.InputInstructions);
        
        guess = getGuessInput(i_GuessNumber);
        
        while(!InputValidator.IsValidGuessInput(guess))
        {
            Console.SetCursorPosition(oldLeft, oldTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(oldLeft, oldTop);
            
            Console.WriteLine(InputValidator.BadInputMessage);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(guess);
            Console.ResetColor();
            
            int newTop = Console.CursorTop;
            
            Console.SetCursorPosition(0, newTop);
            
            guess = getGuessInput(i_GuessNumber);
        }
        
        Console.SetCursorPosition(oldLeft, oldTop);

        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.WriteLine(new string(' ', Console.WindowWidth));
        
        Console.SetCursorPosition(oldLeft, oldTop);
        
        return guess;
    }

    private static string getGuessInput(int i_GuessNumber)
    {
        //int newLeft = 1;
        //int newTop = EXTRA_LINES + (i_GuessNumber)*2;
        string guess = "";
        
        //Console.SetCursorPosition(newLeft, newTop);
        
        for (int i = 0; i < GUESS_LENGTH; i++)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            Console.Write(key.KeyChar); // show it on screen as they type
            guess += key.KeyChar;
            
            // if he entered enter, fill guess with spaces
            if (key.Key == ConsoleKey.Enter)
            {
                while(i < GUESS_LENGTH)
                {
                    guess += ' ';
                    i++;
                }
            }
        }
        
        return guess;
    }

    private static void writeFeedback(int i_GuessNumber, string i_Guess, string i_Feedback)
    {
        printOnTableByCell(i_GuessNumber * 2, i_Guess);
        printOnTableByCell(i_GuessNumber*2 + 1, i_Feedback);
    }
}