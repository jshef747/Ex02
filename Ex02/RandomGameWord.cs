namespace Ex02;

public class RandomGameWord
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
        while(charsForRandomWord.Count < k_RandomWordLength)
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