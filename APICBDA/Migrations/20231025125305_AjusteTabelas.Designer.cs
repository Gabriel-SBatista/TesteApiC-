﻿// <auto-generated />
using APICBDA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APICBDA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231025125305_AjusteTabelas")]
    partial class AjusteTabelas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APICBDA.Models.Estilo", b =>
                {
                    b.Property<int>("EstiloId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstiloId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("EstiloId");

                    b.ToTable("Estilos");
                });

            modelBuilder.Entity("APICBDA.Models.Prova", b =>
                {
                    b.Property<int>("ProvaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProvaId"));

                    b.Property<short>("Distancia")
                        .HasColumnType("smallint");

                    b.Property<int>("EstiloId")
                        .HasColumnType("int");

                    b.Property<int>("SexoId")
                        .HasColumnType("int");

                    b.HasKey("ProvaId");

                    b.HasIndex("EstiloId");

                    b.HasIndex("SexoId");

                    b.ToTable("Provas");
                });

            modelBuilder.Entity("APICBDA.Models.Sexo", b =>
                {
                    b.Property<int>("SexoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SexoId"));

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SexoId");

                    b.ToTable("Sexos");
                });

            modelBuilder.Entity("APICBDA.Models.Prova", b =>
                {
                    b.HasOne("APICBDA.Models.Estilo", "Estilo")
                        .WithMany("Provas")
                        .HasForeignKey("EstiloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APICBDA.Models.Sexo", "Sexo")
                        .WithMany("Provas")
                        .HasForeignKey("SexoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estilo");

                    b.Navigation("Sexo");
                });

            modelBuilder.Entity("APICBDA.Models.Estilo", b =>
                {
                    b.Navigation("Provas");
                });

            modelBuilder.Entity("APICBDA.Models.Sexo", b =>
                {
                    b.Navigation("Provas");
                });
#pragma warning restore 612, 618
        }
    }
}
