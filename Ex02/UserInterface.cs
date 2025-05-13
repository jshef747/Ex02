
namespace Ex02;

public static class UserInterface
{
    private const int k_LengthOfFeedbackCell = 7;
    private const string k_Row = "|         |       |";
    private const string k_Separator = "|=========|=======|";
    
    private static GameLogic? s_MGameLogic;
    
    public static void StartGame()
    {
        Console.WriteLine("Welcome to the game!");

        bool play = true;

        while(play)
        {
            getTableSizeAndPrint();

            for(int currentGuess = 1; currentGuess <= s_MGameLogic!.NumberOfGuesses; currentGuess++)
            {
                string guess = promptAndProcessGuess();
            
                GameLogic.eGameStateIndicator gameStateIndicator = s_MGameLogic.GenerateGuessFeedback(guess, currentGuess);
                
                printTable(currentGuess);
                
                if(gameStateIndicator == GameLogic.eGameStateIndicator.Won)
                {
                    Console.WriteLine($"You guessed after {currentGuess} steps!");
                    play = askToPlayAgain();
                    break;
                }
                
                if(gameStateIndicator == GameLogic.eGameStateIndicator.Lost)
                {
                    Console.WriteLine("No more guesses allowed. You Lost.");
                    play = askToPlayAgain();
                    break;
                }
            }
        }
        
        ConsoleUtils.Screen.Clear();
        quitGame();
    }
    
    private static string readNonNullStringAndHandleQuit()
    {
        string? input = Console.ReadLine();
        while (input == null)
        {
            Console.WriteLine("Input cannot be null.");
            input = Console.ReadLine();
        }
        
        
        if(input.ToUpper() == GameUtils.k_Quit.ToString())
        {
            quitGame();
        }
        
        return input;
    }

    private static ConsoleKeyInfo readCharAndHandleQuit()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
            
        Console.Write(key.KeyChar); // show it on screen as they type
            
        if(Char.ToUpper(key.KeyChar) == GameUtils.k_Quit)
        {
            quitGame();
        }
        
        return key;
    }
    
    private static bool askToPlayAgain()
    {
        Console.WriteLine($"Would you like to start a new game? ({GameUtils.k_Yes}/{GameUtils.k_No})");
        ConsoleKeyInfo playAgain = readCharAndHandleQuit();

        while(!InputValidator.YesOrNo(playAgain.KeyChar.ToString()))
        {
            playAgain = readCharAndHandleQuit();
        }

        return s_MGameLogic!.PlayAgainOrNot(playAgain.KeyChar.ToString());
    }
    
    private static void getTableSizeAndPrint()
    {
        ConsoleUtils.Screen.Clear();
        Console.WriteLine(GameUtils.k_NumberOfGuessesInstructions);
        string numberOfGuesses = readNonNullStringAndHandleQuit();
        
        while(!InputValidator.IsValidGuessNumber(numberOfGuesses))
        {
            Console.WriteLine(InputValidator.BadInputMessage);
            numberOfGuesses = readNonNullStringAndHandleQuit();
        }

        if(s_MGameLogic == null)
        {
            s_MGameLogic = new GameLogic(int.Parse(numberOfGuesses));
        }
        else
        {
            s_MGameLogic.NumberOfGuesses =  int.Parse(numberOfGuesses);
        }
        
        printTable();
    }

    private static void printTable(int i_GuessNumber = 0)
    {
        List<string> guesses = s_MGameLogic!.GetGuessFromHistory();
        List<string> feedbacks = s_MGameLogic.GetFeedbackHistory();
        int currentGuessToPrintIndex = 0;
        
        ConsoleUtils.Screen.Clear();
        Console.WriteLine("Current board status:");
        Console.WriteLine();
        
        for(int rowNumber = 0; rowNumber <= s_MGameLogic.NumberOfGuesses; rowNumber++)
        {
            if(rowNumber == 0)  // print headline
            {
                Console.WriteLine("|Pins:    |Result:|");
            }
            else if(rowNumber == 1 && i_GuessNumber == 0)
            {
                Console.WriteLine("| # # # # |       |");
            }
            else if(rowNumber <= i_GuessNumber)  // print guess and feedback
            {
                string guess = guesses[currentGuessToPrintIndex];
                string feedback = feedbacks[currentGuessToPrintIndex];
                string formattedGuess = formatStrToPrint(0, guess);
                string formattedFeedback = formatStrToPrint(1, feedback);
                
                Console.WriteLine($"|{formattedGuess}|{formattedFeedback}|");
                
                currentGuessToPrintIndex++;
            }
            else // print empty line
            {
                Console.WriteLine(k_Row);
            }
            Console.WriteLine(k_Separator);
        }
        Console.WriteLine();
    }

    private static string formatStrToPrint(int i_GuessOrFeedbackFlag, string i_StringToPrint)
    {
        string strToPrint = "";

        if(i_GuessOrFeedbackFlag == 0)
        {
            strToPrint = string.Format(
                " {0} {1} {2} {3} ",
                i_StringToPrint[0],
                i_StringToPrint[1],
                i_StringToPrint[2], 
                i_StringToPrint[3]);
        }
        else
        {
            int length = i_StringToPrint.Length;
            
            if(length > 0) 
            { 
                string newStringToPrint = "";
                
                for(int i = 0; i < length; i++) 
                { 
                    newStringToPrint += i_StringToPrint[i];
                    if(i != GameUtils.k_NumberOfLettersPerGuess - 1)
                    {
                        newStringToPrint += " ";
                    } 
                }

                int numberOfSpacesToAdd = (k_LengthOfFeedbackCell - (length * 2));
                numberOfSpacesToAdd =  numberOfSpacesToAdd < 0 ? 0 : numberOfSpacesToAdd;
                newStringToPrint += new string(' ', numberOfSpacesToAdd);
                
                strToPrint = newStringToPrint;
            }
            
        }
        
        return strToPrint;
    }

    private static string promptAndProcessGuess()
    {
        int oldLeft = Console.CursorLeft;
        int oldTop = Console.CursorTop;
        
        Console.WriteLine(GameUtils.k_InputInstructions);
        
        string guess = getGuessInput();
        
        while(!InputValidator.IsValidGuessInput(guess))
        {
            clearGuessFromScreen(oldLeft, oldTop);
            
            Console.WriteLine(InputValidator.BadInputMessage);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0} {1} {2} {3}", guess[0], guess[1], guess[2], guess[3]);
            Console.ResetColor();
            
            int newTop = Console.CursorTop;
            Console.SetCursorPosition(0, newTop);
            
            guess = getGuessInput();
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

    private static string getGuessInput()
    {
        string guess = "";
        
        for (int i = 0; i < GameUtils.k_NumberOfLettersPerGuess; i++)
        {
            ConsoleKeyInfo key = readCharAndHandleQuit();
            Console.Write(" ");
            
            // if hit enter, fill guess with spaces
            if (key.Key == ConsoleKey.Enter)
            {
                while(i < GameUtils.k_NumberOfLettersPerGuess)
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

    private static void quitGame()
    {
        ConsoleUtils.Screen.Clear();
        Console.WriteLine("Goodbye.");
        Environment.Exit(0); // i dont know if allowd i think not
    }
    
}