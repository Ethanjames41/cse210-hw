class Riddle
{
    private readonly string _question;
    private readonly string[] _choices;
    private readonly int _correctChoiceIndex;

    public Riddle(string question, string[] choices, int correctChoiceIndex)
    {
        _question = question;
        _choices = choices;
        _correctChoiceIndex = correctChoiceIndex;
    }

    public string GetQuestion()
    {
        return _question;
    }

    public IReadOnlyList<string> GetChoices()
    {
        return _choices;
    }

    public bool IsCorrect(int choiceIndex)
    {
        return choiceIndex == _correctChoiceIndex;
    }
}
