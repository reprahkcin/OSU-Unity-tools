
public class MCAnswer
{
    public string value;
    public bool isCorrect;
    public string reason;

    public MCAnswer()
    {
    }
    public MCAnswer(string value, bool isCorrect, string reason)
    {
        this.value = value;
        this.isCorrect = isCorrect;
        this.reason = reason;
    }
}
