public class PromptGenerator
{
    private readonly List<string> _prompts;
    private readonly Random _random;

    public PromptGenerator()
    {
        _prompts = new List<string>
        {
            "What was the best part of your day?",
            "Who was the most interesting person you interacted with today?",
            "What is one thing you learned today?",
            "What made you smile today?",
            "What challenge did you handle well today?",
            "What are you grateful for right now?"
        };
        _random = new Random();
    }

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
