class PuzzleEncounter : Encounter
{
    private readonly Riddle _riddle;

    public PuzzleEncounter(Riddle riddle)
        : base("Rune Puzzle", "Ancient runes glow to life as you enter a sealed chamber.")
    {
        _riddle = riddle;
    }

    protected override void Resolve(Player player, Random random, ExpeditionLog log)
    {
        Console.WriteLine(_riddle.GetQuestion());
        Console.WriteLine();

        IReadOnlyList<string> choices = _riddle.GetChoices();

        for (int index = 0; index < choices.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {choices[index]}");
        }

        Console.WriteLine();

        int answer = ConsoleHelper.PromptForIntInRange("Choose your answer: ", 1, choices.Count);

        if (_riddle.IsCorrect(answer - 1))
        {
            int goldReward = random.Next(2, 6);
            player.AddGold(goldReward);
            player.AddRelic();

            Console.WriteLine($"The chamber opens. You gained 1 relic shard and {goldReward} gold.");
            log.AddEntry($"{player.GetName()} solved a rune puzzle and gained 1 relic shard plus {goldReward} gold.");
            return;
        }

        int damage = player.TakeDamage(4);

        if (damage == 0)
        {
            Console.WriteLine("The puzzle retaliates, but your ward absorbs the magic.");
        }
        else
        {
            Console.WriteLine($"The runes flare and strike back. You lost {damage} health.");
        }

        log.AddEntry($"{player.GetName()} missed a rune puzzle and took {damage} damage.");
    }
}
