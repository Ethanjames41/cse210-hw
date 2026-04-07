public class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>();
    private readonly List<string> _unusedPrompts = new List<string>();

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as " +
            "many things as you can in a certain area.")
    {
        _prompts.Add("Who are people that you appreciate?");
        _prompts.Add("What are personal strengths of yours?");
        _prompts.Add("Who are people that you have helped this week?");
        _prompts.Add("When have you felt the Holy Ghost this month?");
        _prompts.Add("Who are some of your personal heroes?");

        ResetPrompts();
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetPrompt()} ---");
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
            string item = Console.ReadLine();

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

    private string GetPrompt()
    {
        if (_unusedPrompts.Count == 0)
        {
            ResetPrompts();
        }

        int index = GetRandom().Next(_unusedPrompts.Count);
        string prompt = _unusedPrompts[index];
        _unusedPrompts.RemoveAt(index);
        return prompt;
    }

    private void ResetPrompts()
    {
        _unusedPrompts.Clear();
        _unusedPrompts.AddRange(_prompts);
    }
}
