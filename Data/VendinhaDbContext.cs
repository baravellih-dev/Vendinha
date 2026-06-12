using Microsoft.EntityFrameworkCore;
using Vendinha_TrabalhoFinal.Models;

namespace Vendinha_TrabalhoFinal.Data
{
    public class VendinhaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Divida> Dividas => Set<Divida>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=vendinha.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var modelCliente = modelBuilder.Entity<Cliente>();
            var modelDivida = modelBuilder.Entity<Divida>();

            modelCliente.ToTable("clientes");

            modelCliente.Property(e => e.CPF).HasColumnName("cpf");
            modelCliente.Property(e => e.Nome).HasColumnName("nome");
            modelCliente.Property(e => e.DataNascimento).HasColumnName("datanascimento");
            modelCliente.Property(e => e.Email).HasColumnName("email");

            modelCliente.HasKey(e => e.CPF);


            modelDivida.ToTable("dividas");

            modelDivida.Property(e => e.Id).HasColumnName("id");
            modelDivida.Property(e => e.Valor).HasColumnName("valor");
            modelDivida.Property(e => e.Paga).HasColumnName("paga");
            modelDivida.Property(e => e.DataCriacao).HasColumnName("datacriacao");
            modelDivida.Property(e => e.DataPagamento).HasColumnName("datapagamento");

            modelDivida
                .HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey("clientecpf");

            modelDivida.HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}