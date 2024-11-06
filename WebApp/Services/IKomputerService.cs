
namespace WebApp.Models;

public interface IKomputerService
{
    
    
    int Add(Komputer book);
    void Delete(int id);
    void Update(Komputer book);
    List<Komputer> FindAll();
    Komputer? FindById(int id);
}