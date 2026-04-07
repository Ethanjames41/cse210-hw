static class ConsoleHelper
{
    public static void ClearIfInteractive()
    {
        try
        {
            if (!Console.IsInputRedirected && !Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
        catch (IOException)
        {
        }
    }

    public static string PromptForRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Please enter a value.");
        }
    }

    public static int PromptForIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a whole number between {min} and {max}.");
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    public static void WriteSectionTitle(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
    }
}
