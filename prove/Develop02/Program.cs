public class Program
{
    private readonly Journal _journal;
    private readonly PromptGenerator _promptGenerator;
    private bool _isRunning;

    public Program()
    {
        _journal = new Journal();
        _promptGenerator = new PromptGenerator();
        _isRunning = true;
    }

    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        while (_isRunning)
        {
            int choice = ShowMenu();
            HandleChoice(choice);

            if (_isRunning)
            {
                Console.WriteLine();
            }
        }
    }

    public int ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("Select an option: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 5)
            {
                return choice;
            }

            Console.WriteLine("Please enter a number from 1 to 5.");
            Console.WriteLine();
        }
    }

    public void HandleChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                string prompt = _promptGenerator.GetRandomPrompt();
                Console.WriteLine($"Prompt: {prompt}");
                Console.Write("Response: ");
                string response = Console.ReadLine() ?? string.Empty;

                Entry entry = new Entry(DateTime.Now.ToString("yyyy-MM-dd"), prompt, response);
                _journal.AddEntry(entry);
                Console.WriteLine("Entry added.");
                break;

            case 2:
                _journal.DisplayAll();
                break;

            case 3:
                Console.Write("Enter filename to load: ");
                string loadFilename = (Console.ReadLine() ?? string.Empty).Trim();
                _journal.LoadFromFile(loadFilename);
                break;

            case 4:
                Console.Write("Enter filename to save: ");
                string saveFilename = (Console.ReadLine() ?? string.Empty).Trim();
                _journal.SaveToFile(saveFilename);
                break;

            case 5:
                _isRunning = false;
                Console.WriteLine("Goodbye.");
                break;
        }
    }
}
