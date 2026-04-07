class WardCharm : Item
{
    public WardCharm()
        : base("Ward Charm", "reduce the next hit you take by 3")
    {
    }

    public override string Use(Player player)
    {
        player.AddWardCharge(1);
        return "A protective ward now surrounds you.";
    }
}
