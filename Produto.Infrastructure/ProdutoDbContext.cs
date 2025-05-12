using Microsoft.EntityFrameworkCore;
using Produto.Domain;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Produto.Infrastructure.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options)
        {
        }
        public DbSet<ProdutoCore> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProdutoCore>(entity =>
            {
                entity.ToTable("NPRODUTOS");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.NOME).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DESCRICAO).HasMaxLength(500);
                entity.Property(e => e.VALOR).IsRequired();
            });
        }
    }
}
