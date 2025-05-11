namespace Ex02;

public class UserInterface
{
    private const int EXTRA_LINES = 2;
    private const string ROW = "|         |       |";
    private const string SEPARATOR = "|=========|=======|";
    
    private const int GUESS_LENGTH = 4;
    private static DataBase m_GameDataBase;
    private static GuessValidator m_GuessValidator;
    
    public static void StartGame()
    {
        m_GameDataBase = new DataBase();
        m_GuessValidator = new GuessValidator();
        
        bool wonOrLost = false;
        
        Console.WriteLine("Welcome to the game!");
        
        getTableSizeAndPrint();

        for(int i = 1; i <= m_GameDataBase.NumberOfGuesses && !wonOrLost; i++)
        {
            string guess = promptAndProcessGuess(i);
            //wonOrLost = checkGuess(guess);   // function from logic check
            string feedback = m_GuessValidator.GenerateGuessIndicator(guess);
            addToDataBase(guess, feedback);    // "bla" will return from logic check
            rePrintTable(i);
        }
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
        
        m_GameDataBase.NumberOfGuesses = int.Parse(numberOfGuesses);
        
        printTable();
    }

    private static void printTable()
    {
        Console.Clear();
        ////////////TODO//ConsoleUtils.Screen.Clear();
        Console.WriteLine("Current board status:");
        Console.WriteLine();
        for(int rowNumber = 0; rowNumber <= m_GameDataBase.NumberOfGuesses; rowNumber++)
        {
            Console.WriteLine(ROW);
            Console.WriteLine(SEPARATOR);
        }
        Console.WriteLine();
        
        printOnTableByCell(0, "Pins:");
        printOnTableByCell(1, "Result:");
        printOnTableByCell(2, "####");
    }

    private static void printOnTableByCell(int i_CellNumber, string i_StringToPrint)
    {
        int oldLeft = Console.CursorLeft;
        int oldTop = Console.CursorTop;
        int newLeft = 0;
        int newTop = 0;
        string strToPrint = i_StringToPrint;

        if(i_CellNumber % 2 == 0)
        {
            newLeft = 1;
            newTop = EXTRA_LINES + i_CellNumber;
            if(i_CellNumber != 0)
            {
                strToPrint = string.Format(
                    " {0} {1} {2} {3} ",
                    i_StringToPrint[0],
                    i_StringToPrint[1],
                    i_StringToPrint[2],
                    i_StringToPrint[3]);
            }
        }
        else
        {
            newLeft = 11;
            newTop = EXTRA_LINES + (i_CellNumber - 1);
            if(i_CellNumber != 1)
            {
                int length = i_StringToPrint.Length;
                if(length > 0)
                {
                    string newStringToPrint = "";
                    
                    for(int i = 0; i < length; i++)
                    {
                        newStringToPrint += i_StringToPrint[i];
                        newStringToPrint += " ";
                    }

                    strToPrint = newStringToPrint;
                }
            }
        }
        
        
        Console.SetCursorPosition(newLeft, newTop);
        Console.Write(strToPrint);
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
            clearGuessFromScreen(oldLeft, oldTop);
            
            Console.WriteLine(InputValidator.BadInputMessage);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0} {1} {2} {3}", guess[0], guess[1], guess[2], guess[3]);
            Console.ResetColor();
            
            int newTop = Console.CursorTop;
            Console.SetCursorPosition(0, newTop);
            
            guess = getGuessInput(i_GuessNumber);
        }
        
        clearGuessFromScreen(oldLeft, oldTop);
        
        return guess;
    }

    private static void clearGuessFromScreen(int i_OldLeft, int i_OldTop)
    {
        Console.SetCursorPosition(i_OldLeft, i_OldTop);
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.WriteLine(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(i_OldLeft, i_OldTop);
    }

    private static string getGuessInput(int i_GuessNumber)
    {
        string guess = "";
        
        for (int i = 0; i < GUESS_LENGTH; i++)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            Console.Write(key.KeyChar + " "); // show it on screen as they type
            guess += key.KeyChar;
            
            // if entered enter, fill guess with spaces
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

    private static void addToDataBase(string i_Guess, string i_Feedback)
    {   
        m_GameDataBase.AddGuess(i_Guess);
        m_GameDataBase.AddFeedback(i_Feedback);
    }

    private static void rePrintTable(int i_GuessNumber)
    {
        ///////// TODO reminder: use his majesties clear
        printTable();
        List<string> guesses = m_GameDataBase.Guesses;
        List<string> feedback = m_GameDataBase.Feedback;
        
        for(int i = 1; i <= i_GuessNumber; i++)
        {
            printOnTableByCell(i * 2, guesses[i-1]);
            printOnTableByCell(i*2 + 1, feedback[i-1]);
        }
    }
}