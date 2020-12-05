using Microsoft.EntityFrameworkCore;
using ModelLayer.Classes;

namespace DataLayer {
	public class EffizienzDBContext : DbContext {

		#region puplic properties
		public DbSet<Category> Categories { get; set; }
		public DbSet<Goal> Goals { get; set; }
		public DbSet<UserText> UserTexts { get; set; }
		public DbSet<WorkItem> WorkItems { get; set; }

		// TODO the PlanItems are not possible to Map to a DataSet
		#endregion

		#region constructor
#nullable disable
		public EffizienzDBContext() {
			Categories.Load();
			Goals.Load();
			UserTexts.Load();
			WorkItems.Load();
		}
#nullable enable
		#endregion

		#region methods
		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
			optionsBuilder.UseSqlite( "SQLite\\Effizienz-Data.db" );
			base.OnConfiguring( optionsBuilder );
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder ) {
			base.OnModelCreating( modelBuilder );
		}
		#endregion
	}
}
