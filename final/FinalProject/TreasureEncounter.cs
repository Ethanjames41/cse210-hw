class TreasureEncounter : Encounter
{
    public TreasureEncounter()
        : base("Treasure Cache", "You discover three sealed caches behind a collapsed wall.")
    {
    }

    protected override void Resolve(Player player, Random random, ExpeditionLog log)
    {
        int cacheChoice = ConsoleHelper.PromptForIntInRange("Choose a cache to open (1-3): ", 1, 3);
        int roll = random.Next(100);

        if (roll < 25)
        {
            player.AddRelic();
            Console.WriteLine($"Cache {cacheChoice} held an ancient relic shard.");
            log.AddEntry($"{player.GetName()} opened cache {cacheChoice} and found 1 relic shard.");
            return;
        }

        if (roll < 65)
        {
            int goldReward = random.Next(5, 11);
            player.AddGold(goldReward);
            Console.WriteLine($"Cache {cacheChoice} contained {goldReward} gold.");
            log.AddEntry($"{player.GetName()} opened cache {cacheChoice} and found {goldReward} gold.");
            return;
        }

        Item item = ItemFactory.CreateRandomSupply(random);
        player.AddItem(item);
        Console.WriteLine($"Cache {cacheChoice} contained a {item.GetName()}.");
        log.AddEntry($"{player.GetName()} opened cache {cacheChoice} and found a {item.GetName()}.");
    }
}
