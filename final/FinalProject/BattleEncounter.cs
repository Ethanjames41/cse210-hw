class BattleEncounter : Encounter
{
    private readonly string _enemyName;

    public BattleEncounter(string enemyName)
        : base("Battle Encounter", $"A {enemyName} rises from the ruins and blocks your path.")
    {
        _enemyName = enemyName;
    }

    protected override void Resolve(Player player, Random random, ExpeditionLog log)
    {
        Console.WriteLine("1. Charge in for a bigger reward");
        Console.WriteLine("2. Fight carefully for a safer outcome");
        Console.WriteLine();

        int choice = ConsoleHelper.PromptForIntInRange("Choose a battle plan: ", 1, 2);
        int successChance = choice == 1 ? 65 : 85;
        bool victory = random.Next(100) < successChance;

        if (victory)
        {
            int goldReward = choice == 1 ? random.Next(8, 14) : random.Next(4, 9);
            bool foundRelic = random.Next(100) < (choice == 1 ? 45 : 25);

            player.AddGold(goldReward);
            Console.WriteLine($"You defeated the {_enemyName} and gained {goldReward} gold.");

            string logEntry = $"{player.GetName()} defeated a {_enemyName} and earned {goldReward} gold";

            if (foundRelic)
            {
                player.AddRelic();
                Console.WriteLine("You also recovered a relic shard from the battlefield.");
                logEntry += " plus 1 relic shard.";
            }
            else
            {
                logEntry += ".";
            }

            log.AddEntry(logEntry);
            return;
        }

        int incomingDamage = choice == 1 ? random.Next(5, 9) : random.Next(2, 5);
        int actualDamage = player.TakeDamage(incomingDamage);

        if (actualDamage == 0)
        {
            Console.WriteLine($"The {_enemyName} struck back, but your ward absorbed the blow.");
        }
        else
        {
            Console.WriteLine($"The {_enemyName} got the better of you. You lost {actualDamage} health.");
        }

        log.AddEntry($"{player.GetName()} lost a fight against a {_enemyName} and took {actualDamage} damage.");
    }
}
