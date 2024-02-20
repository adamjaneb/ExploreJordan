using ExploreJordan.Models;
using GradProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExploreJordan.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Car> Cars { get; set; }
        public DbSet<Souvenirs> Souvenirs { get; set; }
		public DbSet<Accommodation> Accommodation { get; set; }
		public DbSet<Tours> Tours { get; set; }
		public DbSet<Activities> Activities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }




        public DbSet<Traveler> Traveler { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
