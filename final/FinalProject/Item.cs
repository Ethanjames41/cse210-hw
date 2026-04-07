abstract class Item
{
    private readonly string _name;
    private readonly string _description;

    protected Item(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDisplayText()
    {
        return $"{_name} ({_description})";
    }

    public abstract string Use(Player player);
}
