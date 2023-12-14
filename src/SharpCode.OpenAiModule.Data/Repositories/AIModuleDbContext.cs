/*using EntityFrameworkCore.Triggers;
*//*using Microsoft.EntityFrameworkCore;*//*

namespace SharpCode.OpenAiModule.Data.Repositories;

public class AIModuleDbContext : DbContextWithTriggers
{
    public AIModuleDbContext(DbContextOptions<AIModuleDbContext> options)
        : base(options)
    {
    }

    protected AIModuleDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<AIModuleEntity>().ToTable("AIModule").HasKey(x => x.Id);
        //modelBuilder.Entity<AIModuleEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
    }
}
*/
