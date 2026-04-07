class ExpeditionLog
{
    private readonly List<string> _entries;

    public ExpeditionLog()
    {
        _entries = new List<string>();
    }

    public void AddEntry(string entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        ConsoleHelper.WriteSectionTitle("Expedition Log");

        if (_entries.Count == 0)
        {
            Console.WriteLine("No expedition events have been recorded yet.");
            return;
        }

        for (int index = 0; index < _entries.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {_entries[index]}");
        }
    }
}
