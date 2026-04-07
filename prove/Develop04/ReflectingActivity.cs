public class ReflectingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>();
    private readonly List<string> _unusedPrompts = new List<string>();
    private readonly List<string> _questions = new List<string>();
    private readonly List<string> _unusedQuestions = new List<string>();

    public ReflectingActivity()
        : base(
            "Reflecting Activity",
            "This activity will help you reflect on times in your life when you have shown strength " +
            "and resilience. This will help you recognize the power you have and how you can use it " +
            "in other aspects of your life.")
    {
        _prompts.Add("Think of a time when you stood up for someone else.");
        _prompts.Add("Think of a time when you did something really difficult.");
        _prompts.Add("Think of a time when you helped someone in need.");
        _prompts.Add("Think of a time when you did something truly selfless.");

        _questions.Add("Why was this experience meaningful to you?");
        _questions.Add("Have you ever done anything like this before?");
        _questions.Add("How did you get started?");
        _questions.Add("How did you feel when it was complete?");
        _questions.Add("What made this time different than other times when you were not as successful?");
        _questions.Add("What is your favorite thing about this experience?");
        _questions.Add("What could you learn from this experience that applies to other situations?");
        _questions.Add("What did you learn about yourself through this experience?");
        _questions.Add("How can you keep this experience in mind in the future?");

        ResetPrompts();
        ResetQuestions();
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetPrompt()} ---");
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

            Console.Write($"> {GetQuestion()} ");
            DisplaySpinner(Math.Min(8, remainingSeconds));
            Console.WriteLine();
            Console.WriteLine();
        }
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

    private string GetQuestion()
    {
        if (_unusedQuestions.Count == 0)
        {
            ResetQuestions();
        }

        int index = GetRandom().Next(_unusedQuestions.Count);
        string question = _unusedQuestions[index];
        _unusedQuestions.RemoveAt(index);
        return question;
    }

    private void ResetPrompts()
    {
        _unusedPrompts.Clear();
        _unusedPrompts.AddRange(_prompts);
    }

    private void ResetQuestions()
    {
        _unusedQuestions.Clear();
        _unusedQuestions.AddRange(_questions);
    }
}
