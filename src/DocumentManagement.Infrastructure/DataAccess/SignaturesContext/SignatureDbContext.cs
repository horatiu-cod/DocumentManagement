using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Domain.Entities.Persons;
using DocumentManagement.Domain.Entities.Signatures;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.SignaturesContext;

public class SignatureDbContext(DbContextOptions<SignatureDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>()
            .HasMany(u => u.Signatures)
            .WithOne(u => u.Document)
            .HasForeignKey(s => s.IssuedFor)
            .IsRequired()
            .OnDelete( DeleteBehavior.Cascade);

        modelBuilder.Entity<Person>()
            .HasMany(u => u.Signatures)
            .WithOne(s => s.Person)
            .HasForeignKey(s => s.IssuedBy);

        modelBuilder.Entity<Person>()
            .HasMany(u => u.Documents)
            .WithOne(d => d.Person)
            .HasForeignKey(d => d.CreatedBy)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Signature> Signatures { get; set; }
}
