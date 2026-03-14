public class Journal
{
    private readonly List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No journal entries available.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename)
    {
        filename = filename.Trim();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("A filename is required to save the journal.");
            return;
        }

        try
        {
            using StreamWriter writer = new StreamWriter(filename);

            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToStorageString());
            }

            Console.WriteLine($"Saved {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} to {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not save journal: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        filename = filename.Trim();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("A filename is required to load the journal.");
            return;
        }

        if (!File.Exists(filename))
        {
            Console.WriteLine($"File not found: {filename}");
            return;
        }

        try
        {
            List<Entry> loadedEntries = new List<Entry>();

            foreach (string line in File.ReadLines(filename))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                loadedEntries.Add(Entry.FromStorageString(line));
            }

            _entries.Clear();
            _entries.AddRange(loadedEntries);
            Console.WriteLine($"Loaded {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} from {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not load journal: {ex.Message}");
        }
    }
}
