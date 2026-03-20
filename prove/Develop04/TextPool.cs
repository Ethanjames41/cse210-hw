public class TextPool
{
    private readonly List<string> _items;
    private readonly List<string> _remainingItems;
    private readonly Random _random;

    public TextPool(IEnumerable<string> items, Random random)
    {
        _items = new List<string>(items);
        _remainingItems = new List<string>();
        _random = random;
        RefillRemainingItems();
    }

    public string GetNextItem()
    {
        if (_remainingItems.Count == 0)
        {
            RefillRemainingItems();
        }

        int selectedIndex = _random.Next(_remainingItems.Count);
        string selectedItem = _remainingItems[selectedIndex];
        _remainingItems.RemoveAt(selectedIndex);
        return selectedItem;
    }

    private void RefillRemainingItems()
    {
        _remainingItems.Clear();
        _remainingItems.AddRange(_items);
    }
}
