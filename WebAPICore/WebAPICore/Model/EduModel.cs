using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPICore.Model
{
	public class EduModel : DbContext
	{
		public EduModel(DbContextOptions options) :base (options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<StudentSubject>()
			//.HasOne(e => e.Subject)
			//.WithMany(e => e.StudentsSubjects)
			//.HasForeignKey(e => e.Subj_Id)
			//.OnDelete(DeleteBehavior.SetNull);

			//modelBuilder.Entity<StudentSubject>()
			//			.HasOne(e => e.Student)
			//			.WithMany(e => e.StudentsSubjects)
			//			.HasForeignKey(e => e.Id)
			//			.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<StudentSubject>().HasKey(p => new { p.Id, p.Subj_Id });
			

			//modelBuilder.Entity<StudentSubject>()
			//			.HasOne(e => e.Student)
			//			.WithMany(e => e.StudentsSubjects)
			//			.HasForeignKey(e => e.Id)
			//			.OnDelete(DeleteBehavior.Restrict); // <= This entity has cascading behaviour on deletion


			//modelBuilder.Entity<StudentSubject>()
			//			.HasOne(e => e.Subject)
			//			.WithMany(e => e.StudentsSubjects)
			//			.HasForeignKey(e => e.Subj_Id)
			//			.OnDelete(DeleteBehavior.Restrict); // <= This entity has restricted behaviour on deletion
		}

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("data source=.;initial catalog=EduModel.WebAPICore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
		//}
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Instructor> Instructors { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Subject> Subjects { get; set; }
		public virtual DbSet<StudentSubject> StudentsSubjects { get; set; }
		//public virtual DbSet<Login> Logins { get; set; }
		
	}
}
