﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sat.Recruitment.Persistence;

namespace Sat.Recruitment.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230417204054_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sat.Recruitment.Persistence.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Peru 2464",
                            Email = "Juan@marmol.com",
                            Money = 1234m,
                            Name = "Juan",
                            Phone = "+5491154762312",
                            UserType = "Normal"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Alvear y Colombres",
                            Email = "Franco.Perez@gmail.com",
                            Money = 112234m,
                            Name = "Franco",
                            Phone = "+534645213542",
                            UserType = "Premium"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Garay y Otra Calle",
                            Email = "Agustina@gmail.com",
                            Money = 112234m,
                            Name = "Agustina",
                            Phone = "+534645213542",
                            UserType = "SuperUser"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
