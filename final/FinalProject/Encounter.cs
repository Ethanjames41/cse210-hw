abstract class Encounter
{
    private readonly string _title;
    private readonly string _description;

    protected Encounter(string title, string description)
    {
        _title = title;
        _description = description;
    }

    public void Play(Player player, Random random, ExpeditionLog log)
    {
        ConsoleHelper.WriteSectionTitle(_title);
        Console.WriteLine(_description);
        Console.WriteLine();
        Resolve(player, random, log);
    }

    protected abstract void Resolve(Player player, Random random, ExpeditionLog log);
}
