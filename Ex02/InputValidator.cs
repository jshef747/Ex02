namespace Ex02;

public class InputValidator
{
    public static string? BadInputMessage { private set; get; }
    public static string InputInstructions = "Please type your next guess <A B C D> or 'Q' to quit";
    
    private static bool checkUniqCharacters(string i_Guess)
    {
        bool uniqChars = false;
        bool[] uniqFlagger = new bool[GameUtils.k_NumberOfValidCharacters];
        int countOfUniqChars = 0;
        foreach(char c in i_Guess)
        {
            uniqFlagger[c - GameUtils.k_FirstValidChar] = true;
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
            //TODO add the magic number
            BadInputMessage = $"Input must be {GameUtils.k_NumberOfLettersPerGuess} letters long!, please try again or press 'Q' to quit";
            isValid = false;
        }
        else if(!isValidCharacters(i_Guess))
        {
            BadInputMessage = $"Input must be {GameUtils.k_NumberOfLettersPerGuess} letters long! And consists of the letters between '{GameUtils.k_FirstValidChar}' and '{GameUtils.k_LastValidChar}', try again or press 'Q' to quit";
            isValid = false;
        }

        else if(!checkUniqCharacters(i_Guess))
        {
            BadInputMessage = $"Input must be {GameUtils.k_NumberOfLettersPerGuess} letters long! Consists of the letters between '{GameUtils.k_FirstValidChar}' and '{GameUtils.k_LastValidChar}' and each character different then the others, try again or press 'Q' to quit";
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
        else if(guessInt < GameUtils.k_MinimumNumberOfGuesses || guessInt > GameUtils.k_MaximumNumberOfGuesses)
        {
            BadInputMessage = $"The number of guesses must be between {GameUtils.k_MinimumNumberOfGuesses} and {GameUtils.k_MaximumNumberOfGuesses}! Please, try again";
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
        else if(i_CharInput[0] != GameUtils.k_Yes || i_CharInput[0] != GameUtils.k_No)
        {
            BadInputMessage = "Input must be a valid character! Y/N";
            isValid = false;
        }
        return isValid;
    }
    
    public static bool QuitPressed(string i_CharInput)
    {
        bool isValid = true;
        if(i_CharInput.Length != 1 || i_CharInput[0] != GameUtils.k_QuitChar )
        {
            BadInputMessage =
                $"Input must be {GameUtils.k_NumberOfLettersPerGuess} letters long! Consists of the letters between '{GameUtils.k_FirstValidChar}' and '{GameUtils.k_LastValidChar}' and each character different then the others, try again or press 'Q' to quit";
            isValid = false;
        }
        return isValid;
    }
}