using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContex : DbContext
{
    //klasa 

    public DbSet<KomputerEntity> Komputery { get; set; }
    

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
                Created = DateTime.Now



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
                Created = DateTime.Now


            });
    }
}