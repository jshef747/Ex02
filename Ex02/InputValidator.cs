namespace Ex02;

public class InputValidator
{
    
    private const char k_Yes = 'Y';
    private const char k_No = 'N';
    private const char k_Quit = 'Q';
    private const char k_FirstValidChar = 'A';
    private const char k_LastValidChar = 'H';
    private const int k_NumOfLettersPerGuess = 4;
    private const int k_NumOfValidCharactersForGuess = 8;
    private const int k_MinNumberOfGuesses = 4;
    private const int k_MaxNumberOfGuesses = 10;
    public static string? BadInputMessage { private set; get; }
    public static string InputInstructions = "Please type your next guess <A B C D> or 'Q' to quit";
    
    private static bool checkUniqCharacters(string i_Guess)
    {
        bool uniqChars = false;
        bool[] uniqFlagger = new bool[k_NumOfValidCharactersForGuess];
        int countOfUniqChars = 0;
        foreach(char c in i_Guess)
        {
            uniqFlagger[c - k_FirstValidChar] = true;
        }

        foreach(bool flag in uniqFlagger)
        {
            if(flag)
            {
                countOfUniqChars++;
            }
        }

        uniqChars = (countOfUniqChars == 4);
        return uniqChars;

    }

    private static bool isValidCharacters(string i_Guess)
    {
        bool validGuessChars = true;
        foreach(char c in i_Guess)
        {
            if(c < k_FirstValidChar || c > k_LastValidChar)
            {
                validGuessChars = false;
            }
        }
        return validGuessChars;
    }
    
    
    public static bool IsValidGuessInput(string i_Guess)
    {
        bool isValid = true;
        if(i_Guess.Length != k_NumOfLettersPerGuess)
        {
            BadInputMessage = "Input must be 4 letters long!, please try again or press 'Q' to quit";
            isValid = false;
        }
        else if(!isValidCharacters(i_Guess))
        {
            BadInputMessage = "Input must be 4 letters long! And consists of the letters between 'A' and 'H', try again or press 'Q' to quit";
            isValid = false;
        }

        else if(!checkUniqCharacters(i_Guess))
        {
            BadInputMessage = "Input must be 4 letters long! Consists of the letters between 'A' and 'H' and each character different then the others, try again or press 'Q' to quit";
            isValid = false;
        }
        return isValid;
    }
    
    public static bool IsValidGuessNumber(string i_Guess)
    {
        bool isValid = int.TryParse(i_Guess, out int guessInt);
        if(!isValid)
        {
            BadInputMessage = "The number of guesses must be an integer! Please, try again";
            isValid = false;
        }
        else if(guessInt < k_MinNumberOfGuesses || guessInt > k_MaxNumberOfGuesses)
        {
            BadInputMessage = "The number of guesses must be between 4 and 10! Please, try again";
            isValid = false;
        }
        return isValid;
    }

    public static bool YesOrNo(string i_CharInput)
    {
        bool isValid = true;
        if(i_CharInput.Length != 1)
        {
            BadInputMessage = "Input must be a single character! Y/N";
            isValid = false;
        }
        else if(i_CharInput[0] != k_Yes || i_CharInput[0] != k_No)
        {
            BadInputMessage = "Input must be a valid character! Y/N";
            isValid = false;
        }
        return isValid;
    }
    
    public static bool QuitPressed(string i_CharInput)
    {
        bool isValid = true;
        if(i_CharInput.Length != 1 || i_CharInput[0] != k_Quit )
        {
            BadInputMessage =
                "Input must be 4 letters long! Consists of the letters between 'A' and 'H' and each character different then the others, try again or press 'Q' to quit";
            isValid = false;
        }
        return isValid;
    }
}