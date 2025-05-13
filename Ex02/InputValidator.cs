namespace Ex02;

public static class InputValidator
{
    public static string? BadInputMessage { private set; get; }
    
    private static bool checkUniqueCharacters(string i_Guess)
    {
        bool[] uniqueFlagger = new bool[GameUtils.k_NumberOfValidCharacters];
        int countOfUniqChars = 0;
        
        foreach(char c in i_Guess)
        {
            uniqueFlagger[c - GameUtils.k_FirstValidChar] = true;
        }

        foreach(bool flag in uniqueFlagger)
        {
            if(flag)
            {
                countOfUniqChars++;
            }
        }

        return countOfUniqChars == 4;
    }

    private static bool isValidCharacters(string i_Guess)
    {
        bool validGuessChars = true;
        
        foreach(char c in i_Guess)
        {
            if(c < GameUtils.k_FirstValidChar || c > GameUtils.k_LastValidChar)
            {
                validGuessChars = false;
            }
        }
        
        return validGuessChars;
    }
    
    public static bool IsValidGuessInput(string i_Guess)
    {
        bool isValid = true;
        
        if(i_Guess.Length != GameUtils.k_NumberOfLettersPerGuess)
        {
            BadInputMessage = $"Input must be {GameUtils.k_NumberOfLettersPerGuess} letters long!"
                              + $" Please try again or press '{GameUtils.k_Quit}' to quit.";
            isValid = false;
        }
        else if(!isValidCharacters(i_Guess))
        {
            BadInputMessage = $"Input must consist the letters between '{GameUtils.k_FirstValidChar}'"
                              + $" and '{GameUtils.k_LastValidChar}' only!"
                              + $" Please try again or press '{GameUtils.k_Quit}' to quit.";
            isValid = false;
        }
        else if(!checkUniqueCharacters(i_Guess))
        {
            BadInputMessage = $"Each character must be different than the others!"
                              + $" Please try again or press '{GameUtils.k_Quit}' to quit.";
            isValid = false;
        }
        
        return isValid;
    }
    
    public static bool IsValidGuessNumber(string i_Guess)
    {
        bool isValid = int.TryParse(i_Guess, out int guessInt);
        
        if(!isValid)
        {
            BadInputMessage = $"The number of guesses must be an integer! "
                              + $"Please try again or press '{GameUtils.k_Quit}' to quit.";
            isValid = false;
        }
        else if(guessInt < GameUtils.k_MinimumNumberOfGuesses || guessInt > GameUtils.k_MaximumNumberOfGuesses)
        {
            BadInputMessage = $"The number of guesses must be between {GameUtils.k_MinimumNumberOfGuesses}"
                              + $" and {GameUtils.k_MaximumNumberOfGuesses}! "
                              + $"Please try again or press '{GameUtils.k_Quit}' to quit.";
            isValid = false;
        }
        
        return isValid;
    }

    public static bool YesOrNo(string i_CharInput)
    {
        bool isValid = true;
        
        if(i_CharInput.Length != 1)
        {
            BadInputMessage = $"Input must be a single character! ({GameUtils.k_Yes}/{GameUtils.k_No})";
            isValid = false;
        }
        else if(i_CharInput[0].ToString() != GameUtils.k_Yes && i_CharInput[0].ToString() != GameUtils.k_No)
        {
            BadInputMessage = $"Input must be a valid character! ({GameUtils.k_Yes}/{GameUtils.k_No})";
            isValid = false;
        }
        
        return isValid;
    }
}