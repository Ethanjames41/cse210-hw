class HealingPotion : Item
{
    public HealingPotion()
        : base("Healing Potion", "restore up to 8 health")
    {
    }

    public override string Use(Player player)
    {
        int healedAmount = player.Heal(8);

        if (healedAmount == 0)
        {
            return "You were already at full health, so the potion had no effect.";
        }

        return $"You recovered {healedAmount} health.";
    }
}
