namespace Ex02;

public class GuessValidator
{
    private const char k_HitAndSameIndex = 'V';
    private const char k_HitAndWrongIndex = 'X'; 
    RandomGameWord m_RandomGameWord;

    public GuessValidator()
    {
        m_RandomGameWord = new RandomGameWord();
    }

    public void Reset()
    {
        m_RandomGameWord =  new RandomGameWord();
    }

    public string GenerateGuessIndicator(string i_Guess)
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
        return guessIndicator;
    }
}