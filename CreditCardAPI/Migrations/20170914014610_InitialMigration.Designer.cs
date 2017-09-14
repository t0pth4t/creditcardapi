using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CreditCardAPI;

namespace CreditCardAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170914014610_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("CreditCardAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CashOutId");

                    b.Property<int?>("PrincipalId");

                    b.HasKey("Id");

                    b.HasIndex("CashOutId");

                    b.HasIndex("PrincipalId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Credit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("LedgerId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Debit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("LedgerId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("Debits");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Ledger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Ledger");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Ledger");
                });

            modelBuilder.Entity("CreditCardAPI.Models.CashOut", b =>
                {
                    b.HasBaseType("CreditCardAPI.Models.Ledger");

                    b.Property<int>("AccountId");

                    b.ToTable("CashOut");

                    b.HasDiscriminator().HasValue("CashOut");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Principal", b =>
                {
                    b.HasBaseType("CreditCardAPI.Models.Ledger");

                    b.Property<int>("AccountId");

                    b.ToTable("Principal");

                    b.HasDiscriminator().HasValue("Principal");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Account", b =>
                {
                    b.HasOne("CreditCardAPI.Models.Ledger", "CashOut")
                        .WithMany()
                        .HasForeignKey("CashOutId");

                    b.HasOne("CreditCardAPI.Models.Ledger", "Principal")
                        .WithMany()
                        .HasForeignKey("PrincipalId");
                });

            modelBuilder.Entity("CreditCardAPI.Models.Credit", b =>
                {
                    b.HasOne("CreditCardAPI.Models.Ledger")
                        .WithMany("Credits")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CreditCardAPI.Models.Debit", b =>
                {
                    b.HasOne("CreditCardAPI.Models.Ledger")
                        .WithMany("Debits")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
