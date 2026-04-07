class Player
{
    private readonly string _name;
    private readonly int _maxHealth;
    private readonly List<Item> _inventory;
    private int _health;
    private int _gold;
    private int _relics;
    private int _daysRemaining;
    private int _wardCharges;

    public Player(string name, int maxHealth, int startingGold, int daysRemaining)
    {
        _name = name;
        _maxHealth = maxHealth;
        _health = maxHealth;
        _gold = startingGold;
        _daysRemaining = daysRemaining;
        _relics = 0;
        _wardCharges = 0;
        _inventory = new List<Item>();
    }

    public string GetName()
    {
        return _name;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetGold()
    {
        return _gold;
    }

    public int GetRelics()
    {
        return _relics;
    }

    public int GetDaysRemaining()
    {
        return _daysRemaining;
    }

    public int GetWardCharges()
    {
        return _wardCharges;
    }

    public int GetInventoryCount()
    {
        return _inventory.Count;
    }

    public IReadOnlyList<Item> GetInventory()
    {
        return _inventory.AsReadOnly();
    }

    public bool IsDefeated()
    {
        return _health <= 0;
    }

    public bool IsOutOfDays()
    {
        return _daysRemaining <= 0;
    }

    public bool HasCollectedRelics(int target)
    {
        return _relics >= target;
    }

    public void AdvanceDay()
    {
        if (_daysRemaining > 0)
        {
            _daysRemaining--;
        }
    }

    public int Heal(int amount)
    {
        int previousHealth = _health;
        _health = Math.Min(_maxHealth, _health + amount);
        return _health - previousHealth;
    }

    public int TakeDamage(int amount)
    {
        int actualDamage = amount;

        if (_wardCharges > 0)
        {
            _wardCharges--;
            actualDamage = Math.Max(0, amount - 3);
        }

        _health = Math.Max(0, _health - actualDamage);
        return actualDamage;
    }

    public void AddGold(int amount)
    {
        _gold += amount;
    }

    public bool TrySpendGold(int amount)
    {
        if (amount > _gold)
        {
            return false;
        }

        _gold -= amount;
        return true;
    }

    public void AddRelic()
    {
        _relics++;
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }

    public void AddWardCharge(int amount)
    {
        _wardCharges += amount;
    }

    public string UseItem(int itemIndex)
    {
        Item item = _inventory[itemIndex];
        string result = item.Use(this);
        _inventory.RemoveAt(itemIndex);
        return $"{item.GetName()} used. {result}";
    }
}
