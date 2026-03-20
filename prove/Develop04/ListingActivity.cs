public class ListingActivity : Activity
{
    private readonly TextPool _promptPool;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as " +
            "many things as you can in a certain area.")
    {
        _promptPool = new TextPool(
            new List<string>
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            },
            GetRandom());
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {_promptPool.GetNextItem()} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        DisplayCountDown(5);
        Console.WriteLine();
        Console.WriteLine("Start listing items:");

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string? item = Console.ReadLine();

            if (item is null)
            {
                break;
            }

            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item.Trim());
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items!");
    }
}
