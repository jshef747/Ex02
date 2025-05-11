namespace Ex02;

public class GuessValidator
{
    private const char k_HitAndSameIndex = 'V';
    private const char k_HitAndWrongIndex = 'X';

    public static string GenerateGuessIndicator(RandomGameWord i_RandomGameWord, string i_Guess)
    {
        int numberOfV = 0;
        int numberOfX = 0;
        foreach(char c in i_Guess)
        {
            if(i_RandomGameWord.RandomWord.Contains(c) && i_RandomGameWord.RandomWord.IndexOf(c) == i_Guess.IndexOf(c))
            {
                numberOfV++;
            }
            else if(i_RandomGameWord.RandomWord.Contains(c))
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