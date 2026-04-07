public abstract class Activity
{
    private readonly string _name;
    private readonly string _description;
    private readonly Random _random;
    private int _duration;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _random = new Random();
    }

    public string GetName()
    {
        return _name;
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    protected abstract void PerformActivity();

    protected Random GetRandom()
    {
        return _random;
    }

    protected int GetRemainingSeconds(DateTime endTime)
    {
        double secondsRemaining = Math.Ceiling((endTime - DateTime.Now).TotalSeconds);
        return Math.Max(0, (int)secondsRemaining);
    }

    protected void DisplayStartingMessage()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }

        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = ReadPositiveInt();
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        DisplaySpinner(3);
        Console.WriteLine();
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        DisplaySpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        DisplaySpinner(3);
        Console.WriteLine();
    }

    protected void DisplaySpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int currentFrame = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(frames[currentFrame]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            currentFrame = (currentFrame + 1) % frames.Length;
        }
    }

    protected void DisplayCountDown(int seconds)
    {
        for (int remaining = seconds; remaining > 0; remaining--)
        {
            string displayValue = $"{remaining} ";
            Console.Write(displayValue);
            Thread.Sleep(1000);
            Console.Write(new string('\b', displayValue.Length));
            Console.Write(new string(' ', displayValue.Length));
            Console.Write(new string('\b', displayValue.Length));
        }

        Console.WriteLine();
    }

    private int ReadPositiveInt()
    {
        while (true)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out int duration) && duration > 0)
            {
                return duration;
            }

            Console.Write("Please enter a whole number greater than 0: ");
        }
    }
}
