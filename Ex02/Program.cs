namespace Ex02;

class Program
{
    public static void Main()
    {
        // string check = Console.ReadLine();
        // bool isValid = InputValidator.IsValidGuessInput(check);
        // string? why = InputValidator.BadInputMessage;
        // Console.WriteLine($"your input: {isValid}\nbad input reason: {why}");
        // check = Console.ReadLine();
        // isValid = InputValidator.IsValidGuessNumber(check);
        // why = InputValidator.BadInputMessage;
        // Console.WriteLine($"your input: {isValid}\nbad input reason: {why}");
        
        RandomGameWord randomGameWord = new RandomGameWord();
        Console.WriteLine(randomGameWord.RandomWord);
        string g = Console.ReadLine();
        string i = GuessValidator.GenerateGuessIndicator(randomGameWord, g);
        Console.WriteLine(i);
    }
}