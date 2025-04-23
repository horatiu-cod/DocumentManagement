using DocumentManagement.Application.Interfaces;
using DocumentManagement.Domain.Entities.Documents;
using DocumentManagement.Domain.Entities.Employees;
using DocumentManagement.Domain.Entities.Signatures;
using Microsoft.EntityFrameworkCore;

namespace DocumentManagement.Infrastructure.DataAccess.SignaturesContext
{
    public class SignatureDbContext(DbContextOptions<SignatureDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Signature> Signatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentEntity>()
                .HasMany(u => u.Signatures)
                .WithOne(u => u.Document)
                .HasForeignKey(s => s.IssuedFor)
                .IsRequired()
                .OnDelete( DeleteBehavior.Cascade);

            modelBuilder.Entity<DocumentEntity>()
                .Property(d => d.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Employee>()
                .HasMany(u => u.Signatures)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.IssuedBy);

            modelBuilder.Entity<Employee>()
                .HasMany(u => u.Documents)
                .WithOne(d => d.Employee)
                .HasForeignKey(d => d.OwnerId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default) => await SaveChangesAsync(cancellationToken);
    }
}
