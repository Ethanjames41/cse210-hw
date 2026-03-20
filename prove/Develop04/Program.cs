public class Program
{
    private readonly Dictionary<string, int> _activityCounts = new Dictionary<string, int>();
    private int _totalMindfulSeconds;

    public static void Main()
    {
        Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        // Creativity / exceed requirements:
        // 1. The program keeps a session log of completed activities and total mindful seconds,
        //    then shows that running summary on the menu.
        // 2. Reflection and listing prompts/questions are cycled without repeats until each pool
        //    has been used once, which makes longer sessions feel less repetitive.
        bool isRunning = true;

        while (isRunning)
        {
            DisplayMenu();
            Console.Write("Select a choice from the menu: ");
            string choice = (Console.ReadLine() ?? string.Empty).Trim();

            if (choice == "4")
            {
                isRunning = false;
                continue;
            }

            Activity? activity = CreateActivity(choice);

            if (activity is null)
            {
                Console.WriteLine();
                Console.WriteLine("Please choose a valid option.");
                Thread.Sleep(1200);
                continue;
            }

            activity.Run();
            RecordCompletedActivity(activity);
        }

        Console.WriteLine();
        Console.WriteLine("Thank you for using the Mindfulness Program.");
    }

    private Activity? CreateActivity(string choice)
    {
        return choice switch
        {
            "1" => new BreathingActivity(),
            "2" => new ReflectingActivity(),
            "3" => new ListingActivity(),
            _ => null
        };
    }

    private void DisplayMenu()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }

        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Start breathing activity");
        Console.WriteLine("  2. Start reflecting activity");
        Console.WriteLine("  3. Start listing activity");
        Console.WriteLine("  4. Quit");
        Console.WriteLine();
        DisplaySessionSummary();
        Console.WriteLine();
    }

    private void DisplaySessionSummary()
    {
        if (_activityCounts.Count == 0)
        {
            Console.WriteLine("Session summary: No activities completed yet.");
            return;
        }

        int totalActivities = 0;

        foreach (int count in _activityCounts.Values)
        {
            totalActivities += count;
        }

        Console.WriteLine(
            $"Session summary: {totalActivities} activities completed for {_totalMindfulSeconds} mindful seconds.");

        foreach (KeyValuePair<string, int> entry in _activityCounts)
        {
            Console.WriteLine($"  {entry.Key}: {entry.Value}");
        }
    }

    private void RecordCompletedActivity(Activity activity)
    {
        string activityName = activity.GetName();

        if (!_activityCounts.ContainsKey(activityName))
        {
            _activityCounts[activityName] = 0;
        }

        _activityCounts[activityName]++;
        _totalMindfulSeconds += activity.GetDuration();
    }
}
