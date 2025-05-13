namespace Ex02;

public class GameLogic
{
    private const char k_HitAndSameIndex = 'V';
    private const char k_HitAndWrongIndex = 'X'; 
    public int NumberOfGuesses { set; get; }

    public const int GuessLength = 4;
    private RandomGameWord m_RandomGameWord;
    private GuessHistory m_GuessHistory;

    
    public enum eGameStateIndicator
    {
        Won,
        Lost,
        Continue
    }


    private class RandomGameWord
    {
        private enum eValidCharacters
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }

        private const int k_ValidCharactersLength = 8;
        private const int k_RandomWordLength = 4;

        public string? RandomWord { private set; get; }

        public RandomGameWord()
        {
            GenerateRandomWord();
        }

        public void GenerateRandomWord()
        {
            Random random = new Random();
            List<char> charsForRandomWord = new List<char>();
            while (charsForRandomWord.Count < k_RandomWordLength)
            {
                int randomValue = random.Next(0, k_ValidCharactersLength);
                eValidCharacters character = (eValidCharacters)randomValue;
                char charToBeCheckedAndAdded = character.ToString()[0];
                if (!charsForRandomWord.Contains(charToBeCheckedAndAdded))
                {
                    charsForRandomWord.Add(charToBeCheckedAndAdded);
                }
            }
            RandomWord = new string(charsForRandomWord.ToArray());
        }
    }


    public GameLogic(int numberOfGuess)
    {
        m_RandomGameWord = new RandomGameWord();
        m_GuessHistory = new GuessHistory();
        NumberOfGuesses = numberOfGuess;
    }

    public void Reset()
    {
        m_RandomGameWord.GenerateRandomWord();
    }

    public string GenerateGuessIndicator(string i_Guess, out eGameStateIndicator IoGameStateIndicator, int i_currentGuessNumber)
    {
        int numberOfV = 0;
        int numberOfX = 0;
        foreach(char c in i_Guess)
        {
            if(m_RandomGameWord.RandomWord.Contains(c) && m_RandomGameWord.RandomWord.IndexOf(c) == i_Guess.IndexOf(c))
            {
                numberOfV++;
            }
            else if(m_RandomGameWord.RandomWord.Contains(c))
            {
                numberOfX++;
            }
        }
        List<char> charsForGuessIndicator = new List<char>();
        for(int i = 0; i < numberOfV; i++)
        {
            charsForGuessIndicator.Add(k_HitAndSameIndex);
        }

        for(int i = 0; i < numberOfX; i++)
        {
            charsForGuessIndicator.Add(k_HitAndWrongIndex);
        }
        string guessIndicator = new string(charsForGuessIndicator.ToArray());
        if(numberOfV == GuessLength)
        {
            IoGameStateIndicator = eGameStateIndicator.Won;
        }
        else if(i_currentGuessNumber < NumberOfGuesses)
        {
            IoGameStateIndicator = eGameStateIndicator.Continue;
        }
        else
        {
            IoGameStateIndicator = eGameStateIndicator.Lost;
        }
        m_GuessHistory.AddGuess(i_Guess);
        m_GuessHistory.AddFeedback(guessIndicator);
        return guessIndicator;
    }

    public List<string> getGuessFromHistory()
    {
        return m_GuessHistory.Guesses;
    }

    public List<string> getFeedbackHistory()
    {
        return m_GuessHistory.Feedback;
    }

}

