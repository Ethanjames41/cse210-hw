static class ItemFactory
{
    public static Item CreateRandomSupply(Random random)
    {
        if (random.Next(2) == 0)
        {
            return new HealingPotion();
        }

        return new WardCharm();
    }
}
