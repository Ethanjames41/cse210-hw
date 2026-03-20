public class ReflectingActivity : Activity
{
    private readonly TextPool _promptPool;
    private readonly TextPool _questionPool;

    public ReflectingActivity()
        : base(
            "Reflecting Activity",
            "This activity will help you reflect on times in your life when you have shown strength " +
            "and resilience. This will help you recognize the power you have and how you can use it " +
            "in other aspects of your life.")
    {
        _promptPool = new TextPool(
            new List<string>
            {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            },
            GetRandom());

        _questionPool = new TextPool(
            new List<string>
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            },
            GetRandom());
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {_promptPool.GetNextItem()} ---");
        Console.WriteLine();
        Console.Write("When you have something in mind, press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        DisplayCountDown(5);
        Console.WriteLine();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            int remainingSeconds = GetRemainingSeconds(endTime);

            if (remainingSeconds == 0)
            {
                break;
            }

            Console.Write($"> {_questionPool.GetNextItem()} ");
            DisplaySpinner(Math.Min(8, remainingSeconds));
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
