namespace WebApp.Models;

public class MemoryKomputerService : IKomputerService
{
    private readonly IDateTimeProvider _timeProvider;

    public MemoryKomputerService(IDateTimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }
    
    private Dictionary<int, Komputer> _items = new Dictionary<int, Komputer>();
    public int Add(Komputer item)
    {
        int id = _items.Keys.Count != 0 ? _items.Keys.Max() : 0;
        item.Id = id + 1;
        item.Created = _timeProvider.Now();
        _items.Add(item.Id, item);
        return item.Id;
    }

    public void Delete(int id)
    {
        _items.Remove(id);
    }

    public List<Komputer> FindAll()
    {
        return _items.Values.ToList();
    }

    public Komputer? FindById(int id)
    {
        return _items[id];
    }

    public void Update(Komputer item)
    {
        _items[item.Id] = item;
    }
}