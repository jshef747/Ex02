namespace Ex02;

public class UserInterface
{
    private const int k_ExtraLinesForSetCursor = 2;
    private const int k_LeftCellStartIndex = 1;
    private const int k_RightCellStartIndex = 11;
    private const int k_PinsCellNumber = 0;
    private const int k_ResultCellNumber = 1;
    private const int k_UserFirstAnswerCellNumber = 2;
    private const string k_Row = "|         |       |";
    private const string k_Separator = "|=========|=======|";
    
    private static GameLogic m_GameLogic;
    
    public static void StartGame()
    {
        Console.WriteLine("Welcome to the game!");
        
        getTableSizeAndPrint();
        

        for(int currentGuess = 1; currentGuess <= m_GameLogic.NumberOfGuesses; currentGuess++)
        {
            string guess = promptAndProcessGuess(currentGuess);
            
            string feedback = m_GameLogic.GenerateGuessIndicator(guess, out GameLogic.eGameStateIndicator gameStateIndicator, currentGuess);
            
            if(gameStateIndicator == GameLogic.eGameStateIndicator.Won)
            {}////TODO fill this
            else if(gameStateIndicator == GameLogic.eGameStateIndicator.Lost)
            {}////TODO fill this
            
            rePrintTable(currentGuess);
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

        m_GameLogic = new GameLogic(int.Parse(numberOfGuesses));
        
        printTable();
    }

    private static void printTable()
    {
        Console.Clear();
        ////////////TODO//ConsoleUtils.Screen.Clear();
        Console.WriteLine("Current board status:");
        Console.WriteLine();
        for(int rowNumber = 0; rowNumber <= m_GameLogic.NumberOfGuesses; rowNumber++)
        {
            Console.WriteLine(k_Row);
            Console.WriteLine(k_Separator);
        }
        Console.WriteLine();
        
        printOnTableByCell(k_PinsCellNumber, "Pins:");
        printOnTableByCell(k_ResultCellNumber, "Result:");
        printOnTableByCell(k_UserFirstAnswerCellNumber, "####");
    }

    private static void printOnTableByCell(int i_CellNumber, string i_StringToPrint)
    {
        int oldLeft = Console.CursorLeft;
        int oldTop = Console.CursorTop;
        int newLeft = k_LeftCellStartIndex;
        int newTop = 0;
        string strToPrint = i_StringToPrint;

        if(i_CellNumber % 2 == 0)
        {
            newTop = k_ExtraLinesForSetCursor + i_CellNumber;
            if(i_CellNumber != k_PinsCellNumber)
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
            newLeft = k_RightCellStartIndex;
            newTop = k_ExtraLinesForSetCursor + (i_CellNumber - 1);
            if(i_CellNumber != k_ResultCellNumber)
            {
                int length = i_StringToPrint.Length;
                if(length > 0)
                {
                    string newStringToPrint = "";

                    for(int i = 0; i < length; i++)
                    {
                        newStringToPrint += i_StringToPrint[i];
                        if(i != GameLogic.GuessLength - 1)
                        {
                            newStringToPrint += " ";
                        }
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
        
        for (int i = 0; i < GameLogic.GuessLength; i++)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            Console.Write(key.KeyChar + " "); // show it on screen as they type
            
            // if entered enter, fill guess with spaces
            if (key.Key == ConsoleKey.Enter)
            {
                while(i < GameLogic.GuessLength)
                {
                    guess += ' ';
                    i++;
                }
            }
            else
            {
                guess += key.KeyChar;
            }
        }
        
        return guess;
    }

    private static void rePrintTable(int i_GuessNumber)
    {
        ///////// TODO reminder: use his majesties clear
        printTable();
        
        List<string> guesses = m_GameLogic.getGuessFromHistory();
        List<string> feedback = m_GameLogic.getFeedbackHistory();
        
        for(int i = 1; i <= i_GuessNumber; i++)
        {
            printOnTableByCell(i * 2, guesses[i-1]);
            printOnTableByCell(i*2 + 1, feedback[i-1]);
        }
    }
}