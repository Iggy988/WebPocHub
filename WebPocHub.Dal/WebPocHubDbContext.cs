using Microsoft.EntityFrameworkCore;
using Models;

namespace WebPocHub.Dal;

public class WebPocHubDbContext : DbContext
{
    //default constractor
    public WebPocHubDbContext()
    {
        
    }
    //constructor with
    public WebPocHubDbContext(DbContextOptions options) : base(options) 
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee() { EmplyeeId = 1, EmployeeName = "Igor Rodic", Address = "Vojvode Momcila 123", City = "Banja Luka", Country = "Bosnia and Herzegovina", ZipCode= "78000", Phone= "065787945", Email="igor@email.com", Skillsets="DBA", Avatar = "/images/igor.png"  },
            new Employee() { EmplyeeId = 2, EmployeeName = "Nina Rodic", Address = "Stepe Stepanovica 132", City = "Banja Luka", Country = "Bosnia and Herzegovina", ZipCode= "78000", Phone= "065123123", Email="nina@email.com", Skillsets="People Managment", Avatar = "/images/nina.png"  },
            new Employee() { EmplyeeId = 3, EmployeeName = "Pero Peric", Address = "Zivojina Misica 231", City = "Belgrade", Country = "Serbia", ZipCode= "11000", Phone= "063123451", Email="pero@email.com", Skillsets="Consultant", Avatar = "/images/pero.png"  }
            );

        modelBuilder.Entity<Role>().HasData(
            new Role() { RoleId = 1, RoleName = "Employee", RoleDescription = "Employee of WebPocHub Organization!" },
            new Role() { RoleId = 2, RoleName = "Hr", RoleDescription = "Hr of WebPocHub Organization!" }
        );
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
}