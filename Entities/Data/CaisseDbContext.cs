using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data;

public partial class CaisseDbContext : DbContext
{
    public CaisseDbContext()
    {
    }

    public CaisseDbContext(DbContextOptions<CaisseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Caisse> Caisses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Lignedevente> Lignedeventes { get; set; }

    public virtual DbSet<Modepaiement> Modepaiements { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<Vente> Ventes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Caisse;Username=postgres;Password=97919170");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("article_pkey");

            entity.ToTable("article");

            entity.HasIndex(e => e.NumeroIdentification, "article_numero_identification_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Libelle)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("libelle");
            entity.Property(e => e.NumeroIdentification).HasColumnName("numero_identification");
            entity.Property(e => e.Prix)
                .HasPrecision(10, 2)
                .HasColumnName("prix");
            entity.Property(e => e.Qte).HasColumnName("qte");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("article_id_category_fkey");
        });

        modelBuilder.Entity<Caisse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("caisse_pkey");

            entity.ToTable("caisse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Poste)
                .HasMaxLength(100)
                .HasColumnName("poste");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Lignedevente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lignedevente_pkey");

            entity.ToTable("lignedevente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdArticle).HasColumnName("id_article");
            entity.Property(e => e.IdVente).HasColumnName("id_vente");
            entity.Property(e => e.PrixTotal)
                .HasPrecision(10, 2)
                .HasColumnName("prix_total");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(10, 2)
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.Quantite).HasColumnName("quantite");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.Lignedeventes)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lignedevente_id_article_fkey");

            entity.HasOne(d => d.IdVenteNavigation).WithMany(p => p.Lignedeventes)
                .HasForeignKey(d => d.IdVente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lignedevente_id_vente_fkey");
        });

        modelBuilder.Entity<Modepaiement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modepaiement_pkey");

            entity.ToTable("modepaiement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Libelle)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paiement_pkey");

            entity.ToTable("paiement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AutorisationEnvoyee).HasColumnName("autorisation_envoyee");
            entity.Property(e => e.IdModepaiement).HasColumnName("id_modepaiement");
            entity.Property(e => e.IdVente).HasColumnName("id_vente");
            entity.Property(e => e.Montant)
                .HasPrecision(10, 2)
                .HasColumnName("montant");
            entity.Property(e => e.MontantRendu)
                .HasPrecision(5, 2)
                .HasColumnName("montant_rendu");
            entity.Property(e => e.NumeroPieceIdentite)
                .HasMaxLength(20)
                .HasColumnName("numero_piece_identite");

            entity.HasOne(d => d.IdModepaiementNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdModepaiement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paiement_id_modepaiement_fkey");

            entity.HasOne(d => d.IdVenteNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdVente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("paiement_id_vente_fkey");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("session_pkey");

            entity.ToTable("session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDebut)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_debut");
            entity.Property(e => e.DateFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_fin");
            entity.Property(e => e.IdCaisse).HasColumnName("id_caisse");
            entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");
            entity.Property(e => e.MontantInitial)
                .HasPrecision(5, 2)
                .HasColumnName("montant_initial");

            entity.HasOne(d => d.IdCaisseNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.IdCaisse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("session_id_caisse_fkey");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("session_id_utilisateur_fkey");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("utilisateur_pkey");

            entity.ToTable("utilisateur");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Prenom)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("prenom");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Vente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vente_pkey");

            entity.ToTable("vente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateVente)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_vente");
            entity.Property(e => e.IdSession).HasColumnName("id_session");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdSessionNavigation).WithMany(p => p.Ventes)
                .HasForeignKey(d => d.IdSession)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vente_id_session_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
