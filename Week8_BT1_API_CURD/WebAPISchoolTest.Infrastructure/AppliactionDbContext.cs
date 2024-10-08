﻿using Microsoft.EntityFrameworkCore;
using WebAPISchoolTest.Domain.SeedWork;
using WebAPISchoolTest.Infrastructure.EntityConfigurations.Classes;
using WebAPISchoolTest.Infrastructure.EntityConfigurations.Students;
using WebAPISchoolTest.Infrastructure.EntityConfigurations.Teachers;
using WebAPISchoolTest.Infrastructure.EntityConfigurations.Users;



namespace WebAPISchoolTest.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClassEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

        }
    }
}
