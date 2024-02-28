using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using Path_Explorer.Models.AbstractModel;
using Audit.EntityFramework;
using Path_Explorer.Models.Entities;
using Path_Explorer.Infrastructure.Abstractions;

namespace Path_Explorer.DAL.Context
{
    public class PathExplorerSqlLiteDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public PathExplorerSqlLiteDbContext(DbContextOptions<PathExplorerSqlLiteDbContext> options,  IDateTimeService dateTimeService) : base(options)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _dateTimeService = dateTimeService;
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuizDetail> QuizDetails { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            DbContextHelper.SoftDeleteAutomaticBuilder(builder); //Add Query Filter for SoftDelete
            DbContextHelper.UniqueKeyAutomaticBuilder(builder); // Unique key and composite Key automation

            builder.Entity<Building>(entity => {
                entity.ToTable("Buildings");

                entity.Property(e => e.Id)
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .UseIdentityAlwaysColumn();

                // Each building can have many questions
                entity.HasMany(e => e.Questions)
                    .WithOne(e => e.Building)
                    .HasForeignKey(rc => rc.BuildingId);
                   // .IsRequired();
            });

            builder.Entity<Question>(entity => {
                entity.ToTable("Questions");

                entity.Property(e => e.Id)
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .UseIdentityAlwaysColumn();

                // Each question can have many options
                entity.HasMany(e => e.Options)
                    .WithOne(e => e.Question)
                    .HasForeignKey(rc => rc.QuestionId)
                    .IsRequired();
            });
            builder.Entity<QuestionOption>(entity => {
                entity.ToTable("QuestionOptions");

                entity.Property(e => e.Id)
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .UseIdentityAlwaysColumn();
            });
            builder.Entity<Quiz>(entity => {
                entity.ToTable("Quizzes");

                entity.Property(e => e.Id)
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .UseIdentityAlwaysColumn();

                // Each quiz can have many quizdetails
                entity.HasMany(e => e.QuizDetails)
                    .WithOne(e => e.Quiz)
                    .HasForeignKey(rc => rc.QuizId)
                    .IsRequired();
            });
            builder.Entity<QuizDetail>(entity => {
                entity.ToTable("QuizDetails");

                entity.Property(e => e.Id)
                      .IsRequired()
                      .ValueGeneratedOnAdd()
                      .UseIdentityAlwaysColumn();
            });

            //builder.HasDefaultSchema("Core");

        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseAudit>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Status = "CREATE";
                        entry.Entity.DateCreated = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy =  "SYSTEM";
                        break;
                    case EntityState.Modified:
                        entry.Entity.Status = entry.Entity.SoftDeleted ? "DELETED" : "UPDATE";
                        entry.Entity.DateUpdated = _dateTimeService.NowUtc;
                        entry.Entity.UpdatedBy = "SYSTEM";
                        break;
                }
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
