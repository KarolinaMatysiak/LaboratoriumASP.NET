using Microsoft.CodeAnalysis.Elfie.Serialization;


namespace WebApp.Models;

public class EFKomputerService : IKomputerService
{
//alt enter initialize constructor
    private AppDbContex _context;

    public EFKomputerService(AppDbContex context)
    {
        _context = context;
    }

    public int Add(Komputer model)
    {
        var e = _context.Komputery.Add(KomputerMapper.ToEntity(model));
        _context.SaveChanges();
        return e.Entity.Id;
    }

    public void Delete(int id)
    {
        _context.Komputery.Remove(new KomputerEntity() { Id = id });
        _context.SaveChanges();
    }

    public void Update(Komputer model)
    {
        _context.Komputery.Update(KomputerMapper.ToEntity(model));
        _context.SaveChanges();
    }

    public List<Komputer> FindAll()
    {
       return _context.Komputery.Select(e => KomputerMapper.FromEntity(e)).ToList();
       
    }

    public Komputer? FindById(int id)
    {
        var entity = _context.Komputery.Find(id);
        return entity != null ? KomputerMapper.FromEntity(entity) : null;
    }
}