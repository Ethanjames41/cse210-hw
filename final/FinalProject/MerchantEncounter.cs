class MerchantEncounter : Encounter
{
    public MerchantEncounter()
        : base("Traveling Merchant", "A lantern-lit trader offers supplies for the road ahead.")
    {
    }

    protected override void Resolve(Player player, Random random, ExpeditionLog log)
    {
        bool sale = random.Next(100) < 35;
        int potionCost = sale ? 4 : 5;
        int wardCharmCost = sale ? 6 : 7;

        if (sale)
        {
            Console.WriteLine("The merchant is running a one-day sale.");
            Console.WriteLine();
        }

        Console.WriteLine($"1. Buy Healing Potion ({potionCost} gold)");
        Console.WriteLine($"2. Buy Ward Charm ({wardCharmCost} gold)");
        Console.WriteLine("3. Leave");
        Console.WriteLine();

        int choice = ConsoleHelper.PromptForIntInRange("What would you like to do? ", 1, 3);

        if (choice == 3)
        {
            Console.WriteLine("You save your gold and move on.");
            log.AddEntry($"{player.GetName()} met a merchant but bought nothing.");
            return;
        }

        Item item = choice == 1 ? new HealingPotion() : new WardCharm();
        int cost = choice == 1 ? potionCost : wardCharmCost;

        if (!player.TrySpendGold(cost))
        {
            Console.WriteLine("You do not have enough gold for that purchase.");
            log.AddEntry($"{player.GetName()} met a merchant but could not afford the goods.");
            return;
        }

        player.AddItem(item);
        Console.WriteLine($"You bought a {item.GetName()}.");
        log.AddEntry($"{player.GetName()} bought a {item.GetName()} for {cost} gold.");
    }
}
