public class Entry
{
    private readonly string _date;
    private readonly string _promptText;
    private readonly string _responseText;

    public Entry(string date, string promptText, string responseText)
    {
        _date = date;
        _promptText = promptText;
        _responseText = responseText;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine($"Response: {_responseText}");
    }

    public string ToStorageString()
    {
        string safeDate = _date.Replace("|", "/");
        string safePrompt = _promptText.Replace("|", "/");
        string safeResponse = _responseText.Replace("|", "/").Replace("\r", " ").Replace("\n", " ");

        return $"{safeDate}|{safePrompt}|{safeResponse}";
    }

    public static Entry FromStorageString(string line)
    {
        string[] parts = line.Split('|', 3);

        if (parts.Length != 3)
        {
            throw new FormatException("Invalid journal entry format.");
        }

        return new Entry(parts[0], parts[1], parts[2]);
    }
}
