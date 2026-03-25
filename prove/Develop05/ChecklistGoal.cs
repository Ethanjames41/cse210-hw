class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _bonusPoints;
    private int _amountCompleted;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : this(name, description, points, targetCount, bonusPoints, 0)
    {
    }

    public ChecklistGoal(
        string name,
        string description,
        int points,
        int targetCount,
        int bonusPoints,
        int amountCompleted)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;

        if (IsComplete())
        {
            return _points + _bonusPoints;
        }

        return _points;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _targetCount;
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} -- Completed {_amountCompleted}/{_targetCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_targetCount}|{_bonusPoints}|{_amountCompleted}";
    }
}
