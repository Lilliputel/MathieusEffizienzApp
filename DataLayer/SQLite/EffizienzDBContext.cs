using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System;
using System.Drawing;
using System.IO;

namespace DataLayer {
	public class EffizienzDBContext : DbContext {

		#region puplic properties
		public DbSet<Category> Categories { get; set; }
		public DbSet<Goal> Goals { get; set; }
		public DbSet<UserText> UserTexts { get; set; }
		public DbSet<WorkItem> WorkItems { get; set; }
		public DbSet<DateSpan> DateSpans { get; set; }
		public DbSet<DoubleTime> Times { get; set; }
		#endregion

		#region methods
		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
			string rootDirectory = Directory.GetParent( Directory.GetCurrentDirectory() )?.FullName ?? "";
			optionsBuilder.UseSqlite( "Data Source=" + Path.Combine( rootDirectory, "DataLayer", "SQLite", "Effizienz-Database.db" ) );
			base.OnConfiguring( optionsBuilder );
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder ) {
			// KEYS
			modelBuilder.Entity<DoubleTime>().HasKey( dt => new { dt.Day, dt.Start, dt.End } );
			modelBuilder.Entity<WorkItem>().HasKey( wi => new { wi.Start, wi.End, wi.Date } );
			// CONVERSION
			modelBuilder.Entity<UserText>()
				.Property( ut => ut.Color )
				.HasConversion( c => ColorTranslator.ToHtml( c ), c => ColorTranslator.FromHtml( c ) );
			modelBuilder.Entity<Category>()
				.Property( c => c.Archived )
				.HasConversion( new BoolToZeroOneConverter<int>() );
			modelBuilder.Entity<DoubleTime>()
				.Property( dt => dt.Day )
				.HasConversion( d => Enum.GetName( d ), d => Enum.Parse<DayOfWeek>( d ) );
			modelBuilder.Entity<Goal>()
				.Property( g => g.State )
				.HasConversion( d => Enum.GetName( d ), d => Enum.Parse<StateEnum>( d ) );
			base.OnModelCreating( modelBuilder );
		}
		#endregion
	}
}
