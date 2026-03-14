public class Program
{
    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        // Creativity: the program chooses from a small library of scriptures
        // instead of using only one fixed verse, and hidden words keep their
        // punctuation visible to make the memorization display easier to read.
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(
                new Reference("Mosiah", 2, 17),
                "When ye are in the service of your fellow beings ye are only in the service of your God."),
            new Scripture(
                new Reference("Doctrine and Covenants", 18, 10),
                "Remember the worth of souls is great in the sight of God.")
        };

        Scripture scripture = scriptures[Random.Shared.Next(scriptures.Count)];

        while (true)
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden. Great work!");
                break;
            }

            Console.Write("Press Enter to hide more words or type 'quit' to exit: ");
            string userInput = (Console.ReadLine() ?? string.Empty).Trim();

            if (userInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Goodbye.");
                break;
            }

            scripture.HideRandomWords(3);
        }
    }
}
