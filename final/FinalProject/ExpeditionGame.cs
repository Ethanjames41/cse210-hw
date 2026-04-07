class ExpeditionGame
{
    private const int _relicGoal = 3;
    private readonly Random _random;
    private readonly ExpeditionLog _log;
    private readonly List<Riddle> _riddles;
    private readonly string[] _enemyNames;
    private bool _quitEarly;
    private Player _player;

    public ExpeditionGame()
    {
        _random = new Random();
        _log = new ExpeditionLog();
        _riddles = CreateRiddles();
        _enemyNames = new string[]
        {
            "sand prowler",
            "stone guardian",
            "cave stalker",
            "ruin bandit"
        };
    }

    public void Start()
    {
        ConsoleHelper.ClearIfInteractive();
        ConsoleHelper.WriteSectionTitle("Ruins of the First Star");
        Console.WriteLine("Recover three relic shards before your expedition runs out of days.");
        Console.WriteLine("Explore the ruins, manage your health, and use your supplies wisely.");
        Console.WriteLine();

        string playerName = ConsoleHelper.PromptForRequiredString("What is your explorer's name? ");

        _player = new Player(playerName, 25, 10, 8);
        _player.AddItem(new HealingPotion());
        _log.AddEntry($"{playerName} began the expedition with 25 health, 10 gold, and one Healing Potion.");

        RunMainLoop();
        DisplayEnding();
    }

    private void RunMainLoop()
    {
        bool isPlaying = true;

        while (isPlaying &&
               !_player.IsDefeated() &&
               !_player.HasCollectedRelics(_relicGoal) &&
               !_player.IsOutOfDays())
        {
            Console.WriteLine();
            DisplayStatus();
            Console.WriteLine("1. Explore the ruins");
            Console.WriteLine("2. Rest at camp");
            Console.WriteLine("3. Use a backpack item");
            Console.WriteLine("4. View expedition log");
            Console.WriteLine("5. Quit");
            Console.WriteLine();

            int choice = ConsoleHelper.PromptForIntInRange("Choose an action: ", 1, 5);
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Explore();
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearIfInteractive();
                    break;

                case 2:
                    Rest();
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearIfInteractive();
                    break;

                case 3:
                    UseBackpackItem();
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearIfInteractive();
                    break;

                case 4:
                    _log.Display();
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearIfInteractive();
                    break;

                case 5:
                    _quitEarly = true;
                    isPlaying = false;
                    break;
            }
        }
    }

    private void DisplayStatus()
    {
        ConsoleHelper.WriteSectionTitle("Expedition Status");
        Console.WriteLine($"Explorer: {_player.GetName()}");
        Console.WriteLine($"Health: {_player.GetHealth()}/{_player.GetMaxHealth()}");
        Console.WriteLine($"Gold: {_player.GetGold()}");
        Console.WriteLine($"Relic Shards: {_player.GetRelics()}/{_relicGoal}");
        Console.WriteLine($"Days Remaining: {_player.GetDaysRemaining()}");
        Console.WriteLine($"Ward Charges: {_player.GetWardCharges()}");
        Console.WriteLine($"Backpack Items: {_player.GetInventoryCount()}");
        Console.WriteLine();
    }

    private void Explore()
    {
        _player.AdvanceDay();

        Encounter encounter = CreateRandomEncounter();
        encounter.Play(_player, _random, _log);

        if (_player.IsOutOfDays() && !_player.HasCollectedRelics(_relicGoal) && !_player.IsDefeated())
        {
            Console.WriteLine();
            Console.WriteLine("That was your final day in the ruins.");
        }
    }

    private void Rest()
    {
        _player.AdvanceDay();

        ConsoleHelper.WriteSectionTitle("Camp Rest");
        int healedAmount = _player.Heal(_random.Next(4, 8));

        if (healedAmount > 0)
        {
            Console.WriteLine($"You rest by the fire and recover {healedAmount} health.");
        }
        else
        {
            Console.WriteLine("You were already at full health, but the rest keeps your focus sharp.");
        }

        _log.AddEntry($"{_player.GetName()} rested at camp and recovered {healedAmount} health.");

        if (_player.IsOutOfDays() && !_player.HasCollectedRelics(_relicGoal))
        {
            Console.WriteLine("The sun sets on your final day.");
        }
    }

    private void UseBackpackItem()
    {
        IReadOnlyList<Item> inventory = _player.GetInventory();

        ConsoleHelper.WriteSectionTitle("Backpack");

        if (inventory.Count == 0)
        {
            Console.WriteLine("Your backpack is empty.");
            return;
        }

        for (int index = 0; index < inventory.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {inventory[index].GetDisplayText()}");
        }

        Console.WriteLine("0. Cancel");
        Console.WriteLine();

        int choice = ConsoleHelper.PromptForIntInRange("Choose an item to use: ", 0, inventory.Count);

        if (choice == 0)
        {
            Console.WriteLine("You close your backpack.");
            return;
        }

        string result = _player.UseItem(choice - 1);
        Console.WriteLine(result);
        _log.AddEntry($"{_player.GetName()} used an item. {result}");
    }

    private Encounter CreateRandomEncounter()
    {
        int roll = _random.Next(100);

        if (roll < 35)
        {
            string enemyName = _enemyNames[_random.Next(_enemyNames.Length)];
            return new BattleEncounter(enemyName);
        }

        if (roll < 60)
        {
            Riddle riddle = _riddles[_random.Next(_riddles.Count)];
            return new PuzzleEncounter(riddle);
        }

        if (roll < 80)
        {
            return new TreasureEncounter();
        }

        return new MerchantEncounter();
    }

    private void DisplayEnding()
    {
        ConsoleHelper.ClearIfInteractive();
        ConsoleHelper.WriteSectionTitle("Expedition Complete");

        if (_quitEarly)
        {
            Console.WriteLine("You ended the expedition early and returned home with what you had found.");
        }
        else if (_player.HasCollectedRelics(_relicGoal))
        {
            Console.WriteLine("You assembled all three relic shards and completed the expedition successfully.");
        }
        else if (_player.IsDefeated())
        {
            Console.WriteLine("Your expedition ended when your health reached zero.");
        }
        else if (_player.IsOutOfDays())
        {
            Console.WriteLine("Your team ran out of time before the relic was fully restored.");
        }

        Console.WriteLine();
        Console.WriteLine($"Explorer: {_player.GetName()}");
        Console.WriteLine($"Final Health: {_player.GetHealth()}/{_player.GetMaxHealth()}");
        Console.WriteLine($"Final Gold: {_player.GetGold()}");
        Console.WriteLine($"Relic Shards Collected: {_player.GetRelics()}");
    }

    private static List<Riddle> CreateRiddles()
    {
        return new List<Riddle>
        {
            new Riddle(
                "I have cities, but no houses. I have water, but no fish. What am I?",
                new string[] { "A map", "A dream", "A mirror" },
                0),
            new Riddle(
                "What gets wetter the more it dries?",
                new string[] { "Rain", "A towel", "Sand" },
                1),
            new Riddle(
                "What has many keys but cannot open a single door?",
                new string[] { "A lockbox", "A piano", "A crown" },
                1),
            new Riddle(
                "What can travel around the world while staying in one corner?",
                new string[] { "A stamp", "A shadow", "A compass" },
                0)
        };
    }
}
