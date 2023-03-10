// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VoucherCK.Application;

#nullable disable

namespace VoucherCK.Application.Migrations
{
    [DbContext(typeof(CKContext))]
    partial class CKContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("VoucherCK.Application.BarCodeRedeems", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Barcode");

                    b.Property<DateTime>("RedeemAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("RedeemAt");

                    b.Property<string>("Voucher")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Voucher");

                    b.HasKey("Id");

                    b.ToTable("BarCodeRedeems");
                });
#pragma warning restore 612, 618
        }
    }
}
