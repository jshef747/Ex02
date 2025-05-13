namespace Ex02;

public class GuessHistory
{
    private int m_NumberOfGuesses;
    private List<string> m_Guesses;
    private List<string> m_Feedback;

    public GuessHistory()
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
    
    public void AddGuess(string i_Guess)
    {
        m_Guesses.Add(i_Guess);
    }
    
    public List<string> Feedback
    {
        get { return m_Feedback; }
    }
    
    public void AddFeedback(string i_Feedaback)
    {
        m_Feedback.Add(i_Feedaback);
    }
}