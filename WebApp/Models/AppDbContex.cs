using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContex : DbContext
{
    //klasa 

    public DbSet<KomputerEntity> Komputery { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    

    private string DbPath { get; set; }

    public AppDbContex()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "komputery.db");
        
        //join laczy sciezki, combain kombinuje (np. jak ktoras sciezka jest absolutna to nic nie robi, uzywamy gdy nie wiemy jaka jest struktura a chcemy zrobic jedna sciezke)
        //control i litera "o" - nadpisywanie metod
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //wlasne polaczenie z baza, connection string to uniwersalny string
        optionsBuilder.UseSqlite(connectionString: $"Data source={DbPath}");
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
                          {
                              modelBuilder.Entity<OrganizationEntity>()
                                  .ToTable("organizations")
                                  .HasData(
                                      new OrganizationEntity()
                                      {
                                          Id = 101,
                                          NIP = "234567",
                                          Name = "Firma",
                                          REGON = "756383292932",
                                      },
                                      
                              new OrganizationEntity()
                                  {
                                      Id = 102,
                                      NIP = "234547",
                                      Name = "WSEI",
                                      REGON = "756333296932",
                                  }
                                  );
                              
                              modelBuilder.Entity<OrganizationEntity>()
                                  .OwnsOne(o => o.Address) 
            .HasData(
            new {OrganizationEntityId =101, Street = "sw. Filipa 17", City = "Krakow"},
        new {OrganizationEntityId =102, Street = "Dworcowa 22", City = "Wroclaw"}
            );



        modelBuilder.Entity<KomputerEntity>()
            .Property(c => c.OrganizationId)
            .HasDefaultValue(101);

        modelBuilder.Entity<KomputerEntity>()
            .HasOne(e => e.Organization)
            .WithMany(o => o.Komputery)
            .HasForeignKey(e => e.OrganizationId);
        
        modelBuilder.Entity<KomputerEntity>().HasData(

            new KomputerEntity()
            {
                Id = 1,

                Producent = "Mateusz Matysiak",
                Category = Category.Urgent,
                Pamiec = 16,
                Procesor = "Intel i5",
                Nazwa = "Gaming Machine Pro",
                KartaGraficzna = "NVIDIA",
                DataProdukcji = new DateTime(year: 2000, month: 10, day: 4),
                Created = DateTime.Now,
                OrganizationId = 101,



            },

            new KomputerEntity()
            {
                Id = 2,
                
                Producent = "Karolina Bruzda",
                Category = Category.Low,
                Pamiec = 16,
                Procesor = "Intel i7",
                Nazwa = "Quiet-book",
                KartaGraficzna = "NVIDIA",
                DataProdukcji = new DateTime(year: 1997, month: 12, day: 22),
                Created = DateTime.Now,
                OrganizationId = 101,


            });
    }
}