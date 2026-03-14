public class Word
{
    private readonly string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }

        char[] hiddenCharacters = _text.ToCharArray();

        for (int i = 0; i < hiddenCharacters.Length; i++)
        {
            if (char.IsLetterOrDigit(hiddenCharacters[i]))
            {
                hiddenCharacters[i] = '_';
            }
        }

        return new string(hiddenCharacters);
    }
}
