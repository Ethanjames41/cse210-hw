abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetPoints()
    {
        return _points;
    }

    public virtual string GetDetailsString()
    {
        string status = IsComplete() ? "X" : " ";
        return $"[{status}] {_name} ({_description})";
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();
}
