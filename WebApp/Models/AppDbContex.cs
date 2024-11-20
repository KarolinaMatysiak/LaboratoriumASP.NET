using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContex : IdentityDbContext<IdentityUser>
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
                              
                              base.OnModelCreating(modelBuilder);
                              var ADMIN_ID = Guid.NewGuid().ToString();
                              var ADMIN_ROLE_ID =Guid.NewGuid().ToString();
                              var USER_ID =Guid.NewGuid().ToString();
                              var USER_ROLE_ID =Guid.NewGuid().ToString();

                              modelBuilder.Entity<IdentityRole>()
                                  .HasData(
                                      new IdentityRole()
                                      {
                                          Id = ADMIN_ROLE_ID,
                                          Name = "admin",
                                          NormalizedName = "admin".ToUpper(),
                                          ConcurrencyStamp = ADMIN_ROLE_ID

                                      },

                                      new IdentityRole()
                                      {
                                          Id = USER_ROLE_ID,
                                          Name = "user",
                                          NormalizedName = "user".ToUpper(),
                                          ConcurrencyStamp = USER_ROLE_ID
                                      }
                                  );

                              var admin = new IdentityUser()
                              {
                                  Id = ADMIN_ID,
                                  UserName = "karol",
                                  NormalizedUserName = "karol".ToUpper(),
                                  Email = "karol.mruczek@microsoft.wsei.edu.pl",
                                  NormalizedEmail = "karol.mruczek@microsoft.wsei.edu.pl".ToUpper(),
                                  EmailConfirmed = true
                              };
                              
                              var user = new IdentityUser()
                              {
                                  Id = USER_ID,
                                  UserName = "karolina",
                                  NormalizedUserName = "karolina".ToUpper(),
                                  Email = "karolina.buczek@microsoft.wsei.edu.pl",
                                  NormalizedEmail = "karolina.buczek@microsoft.wsei.edu.pl".ToUpper(),
                                  EmailConfirmed = true
                              };

                              PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
                              
                              admin.PasswordHash = hasher.HashPassword(admin, "123!");
                              user.PasswordHash = hasher.HashPassword(user, "abc!");
                              modelBuilder.Entity<IdentityUser>()
                                  .HasData(admin, user);

                              modelBuilder.Entity<IdentityUserRole<string>>()
                                  .HasData(

                                      new IdentityUserRole<string>()
                                      {
                                          RoleId = ADMIN_ROLE_ID,
                                          UserId = ADMIN_ID
                                      },

                                      new IdentityUserRole<string>()
                                      {
                                          RoleId = USER_ROLE_ID,
                                          UserId = USER_ID
                                      },

                                      new IdentityUserRole<string>()
                                      {
                                          RoleId = USER_ROLE_ID,
                                          UserId = ADMIN_ID
                                      }
                                      
                                  );
                              
                              
                              
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