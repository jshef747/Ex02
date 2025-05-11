namespace Ex02;

public class DataBase
{
    private int m_NumberOfGuesses;
    private List<string> m_Guesses;
    private List<string> m_Feedback;

    public DataBase()
    {
        m_NumberOfGuesses = 0;
        m_Guesses = new List<string>();
        m_Feedback = new List<string>();
    }
    
    public int NumberOfGuesses
    {
        get { return m_NumberOfGuesses; }
        set { m_NumberOfGuesses = value; }
    }
    
    public List<string> Guesses
    {
        get { return m_Guesses; }
    }
    
    public void AddGuess(string guess)
    {
        m_Guesses.Add(guess);
    }
    
    public List<string> Feedback
    {
        get { return m_Feedback; }
    }
    
    public void AddFeedback(string feedaback)
    {
        m_Feedback.Add(feedaback);
    }
}