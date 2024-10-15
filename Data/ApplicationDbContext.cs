using BlazorForms.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Field = BlazorForms.Data.Models.Field;

namespace BlazorForms.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Template> Templates { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<FormResponse> FormResponses { get; set; }
    public DbSet<FieldResponse> FieldResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Template>()
            .HasMany(t => t.Fields)
            .WithOne(f => f.Template)
            .HasForeignKey(f => f.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Option>()
            .HasOne(e => e.Field)
            .WithMany(e => e.Options)
            .HasForeignKey(e => e.FieldId)
            .IsRequired();

        builder.Entity<Comment>()
            .HasOne(e => e.Template)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Like>()
            .HasOne(e => e.Template)
            .WithMany(e => e.Likes)
            .HasForeignKey(e => e.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FormResponse>()
            .HasOne(e => e.Template)
            .WithMany(e => e.Responses)
            .HasForeignKey(e => e.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FieldResponse>()
            .HasOne(e => e.FormResponse)
            .WithMany(e => e.FieldResponses)
            .HasForeignKey(e => e.FormResponseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}