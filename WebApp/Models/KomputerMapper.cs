namespace WebApp.Models;

public class KomputerMapper
{
    public static KomputerEntity ToEntity(Komputer arg)
    {
        return new KomputerEntity()
        {
            Id = arg.Id,
            Nazwa = arg.Nazwa,
            Producent = arg.Producent,
            Procesor = arg.Procesor,
            DataProdukcji = arg.DataProdukcji,
            KartaGraficzna = arg.KartaGraficzna,
            Category = arg.Category,
            Pamiec = arg.Pamiec,
            Organization = arg.Organization,
            OrganizationId = arg.OrganizationId,
                
            //bez created
        };
        
    }

    public static Komputer FromEntity(KomputerEntity arg)
    {
        return new Komputer()
        {
            Id = arg.Id,
            Nazwa = arg.Nazwa,
            Producent = arg.Producent,
            Procesor = arg.Procesor,
            DataProdukcji = arg.DataProdukcji,
            KartaGraficzna = arg.KartaGraficzna,
            Category = arg.Category,
            Pamiec = arg.Pamiec,
            Organization = arg.Organization,
            OrganizationId = arg.OrganizationId,
            //bez created
        };
    }
}