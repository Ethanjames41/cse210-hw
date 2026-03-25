using System;
using System.Collections.Generic;
using System.IO;

class GoalManager
{
    private readonly List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool isRunning = true;

        while (isRunning)
        {
            TryClearConsole();
            DisplayPlayerInfo();
            DisplayMenu();

            int choice = PromptForIntInRange("Select a choice from the menu: ", 1, 6);
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    Pause();
                    break;

                case 2:
                    ListGoalDetails();
                    Pause();
                    break;

                case 3:
                    SaveGoals(PromptForString("Enter a filename to save to: "));
                    Pause();
                    break;

                case 4:
                    LoadGoals(PromptForString("Enter a filename to load from: "));
                    Pause();
                    break;

                case 5:
                    RecordEvent();
                    Pause();
                    break;

                case 6:
                    isRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;
            }
        }
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine();
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Create New Goal");
        Console.WriteLine("  2. List Goals");
        Console.WriteLine("  3. Save Goals");
        Console.WriteLine("  4. Load Goals");
        Console.WriteLine("  5. Record Event");
        Console.WriteLine("  6. Quit");
        Console.WriteLine();
    }

    private void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        Console.WriteLine("The goals are:");

        for (int index = 0; index < _goals.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {_goals[index].GetName()}");
        }
    }

    private void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        Console.WriteLine("The goals are:");

        for (int index = 0; index < _goals.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {_goals[index].GetDetailsString()}");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine();

        int goalType = PromptForIntInRange("Which type of goal would you like to create? ", 1, 3);
        string name = PromptForString("What is the name of your goal? ");
        string description = PromptForString("What is a short description of it? ");
        int points = PromptForPositiveInt("How many points is it worth? ");

        Goal goal;

        if (goalType == 1)
        {
            goal = new SimpleGoal(name, description, points);
        }
        else if (goalType == 2)
        {
            goal = new EternalGoal(name, description, points);
        }
        else
        {
            int targetCount = PromptForPositiveInt("How many times does this goal need to be accomplished for a bonus? ");
            int bonusPoints = PromptForPositiveInt("What is the bonus for accomplishing it that many times? ");
            goal = new ChecklistGoal(name, description, points, targetCount, bonusPoints);
        }

        _goals.Add(goal);
        Console.WriteLine("Goal created successfully.");
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("Create a goal before recording an event.");
            return;
        }

        ListGoalNames();
        Console.WriteLine();

        int goalNumber = PromptForIntInRange("Which goal did you accomplish? ", 1, _goals.Count);
        Goal goal = _goals[goalNumber - 1];
        int pointsEarned = goal.RecordEvent();

        if (pointsEarned <= 0)
        {
            Console.WriteLine("That goal is already complete, so no points were awarded.");
            return;
        }

        _score += pointsEarned;
        Console.WriteLine($"Congratulations! You have earned {pointsEarned} points.");
        Console.WriteLine($"You now have {_score} points.");
    }

    private void SaveGoals(string filename)
    {
        string path = ResolvePath(filename);

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"Goals saved to {path}");
    }

    private void LoadGoals(string filename)
    {
        string path = ResolvePath(filename);

        if (!File.Exists(path))
        {
            Console.WriteLine("That file could not be found.");
            return;
        }

        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0)
        {
            _score = 0;
            _goals.Clear();
            Console.WriteLine("The file was empty, so the goal list was cleared.");
            return;
        }

        List<Goal> loadedGoals = new List<Goal>();
        int loadedScore;

        if (!int.TryParse(lines[0], out loadedScore))
        {
            Console.WriteLine("The file format is invalid.");
            return;
        }

        for (int index = 1; index < lines.Length; index++)
        {
            string line = lines[index];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            try
            {
                loadedGoals.Add(CreateGoalFromData(parts));
            }
            catch (Exception)
            {
                Console.WriteLine($"Skipping invalid goal entry on line {index + 1}.");
            }
        }

        _score = loadedScore;
        _goals.Clear();
        _goals.AddRange(loadedGoals);
        Console.WriteLine($"Loaded {_goals.Count} goals from {path}");
    }

    private Goal CreateGoalFromData(string[] parts)
    {
        if (parts.Length == 0)
        {
            throw new InvalidDataException("Missing goal data.");
        }

        string goalType = parts[0];

        if (goalType == "SimpleGoal" && parts.Length >= 5)
        {
            return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
        }

        if (goalType == "EternalGoal" && parts.Length >= 4)
        {
            return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
        }

        if (goalType == "ChecklistGoal" && parts.Length >= 7)
        {
            return new ChecklistGoal(
                parts[1],
                parts[2],
                int.Parse(parts[3]),
                int.Parse(parts[4]),
                int.Parse(parts[5]),
                int.Parse(parts[6]));
        }

        throw new InvalidDataException($"Unsupported goal type: {goalType}");
    }

    private static string PromptForString(string prompt)
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

    private static int PromptForPositiveInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Please enter a whole number greater than zero.");
        }
    }

    private static int PromptForIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter a whole number between {min} and {max}.");
        }
    }

    private static string ResolvePath(string filename)
    {
        if (Path.IsPathRooted(filename))
        {
            return filename;
        }

        return Path.Combine(Environment.CurrentDirectory, filename);
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void TryClearConsole()
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
}
