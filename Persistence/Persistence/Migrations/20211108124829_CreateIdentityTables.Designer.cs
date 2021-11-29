﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211108124829_CreateIdentityTables")]
    partial class CreateIdentityTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.ReturnedTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfReturn")
                        .HasColumnType("date")
                        .HasColumnName("dateOfReturn");

                    b.Property<string>("GenericReasonOfReturn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("genericReasonOfReturn");

                    b.Property<string>("PersonalReasonOfReturn")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("personalReasonOfReturn");

                    b.Property<int>("Ticket")
                        .HasColumnType("int")
                        .HasColumnName("ticket");

                    b.HasKey("Id");

                    b.HasIndex("Ticket");

                    b.ToTable("ReturnedTickets");
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("ArrivalTimeInMinutesPastMidnight")
                        .HasColumnType("smallint")
                        .HasColumnName("arrivalTimeInMinutesPastMidnight");

                    b.Property<short>("DepartureTimeInMinutesPastMidnight")
                        .HasColumnType("smallint")
                        .HasColumnName("departureTimeInMinutesPastMidnight");

                    b.Property<int>("FinalStation")
                        .HasColumnType("int")
                        .HasColumnName("finalStation");

                    b.Property<bool>("IsSuspended")
                        .HasColumnType("bit")
                        .HasColumnName("isOnHold");

                    b.Property<int>("StartingStation")
                        .HasColumnType("int")
                        .HasColumnName("startingStation");

                    b.HasKey("Id");

                    b.HasIndex("FinalStation");

                    b.HasIndex("StartingStation");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Domain.Entities.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Car")
                        .HasColumnType("tinyint")
                        .HasColumnName("car");

                    b.Property<bool?>("IsFree")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("isFree")
                        .HasDefaultValueSql("((1))");

                    b.Property<byte>("Number")
                        .HasColumnType("tinyint")
                        .HasColumnName("number");

                    b.Property<int>("Train")
                        .HasColumnType("int")
                        .HasColumnName("train");

                    b.HasKey("Id");

                    b.HasIndex("Train");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Domain.Entities.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DayOfDeparture")
                        .HasColumnType("date")
                        .HasColumnName("dayOfDeparture");

                    b.Property<int>("Owner")
                        .HasColumnType("int")
                        .HasColumnName("owner");

                    b.Property<int>("Route")
                        .HasColumnType("int")
                        .HasColumnName("route");

                    b.Property<int>("Seat")
                        .HasColumnType("int")
                        .HasColumnName("seat");

                    b.Property<int>("Train")
                        .HasColumnType("int")
                        .HasColumnName("train");

                    b.HasKey("Id");

                    b.HasIndex("Route");

                    b.HasIndex("Seat");

                    b.HasIndex("Train");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("NumberOfCars")
                        .HasColumnType("tinyint")
                        .HasColumnName("numberOfCars");

                    b.Property<short>("NumberOfSeats")
                        .HasColumnType("smallint")
                        .HasColumnName("numberOfSeats");

                    b.Property<short>("TrainId")
                        .HasColumnType("smallint")
                        .HasColumnName("trainId");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Infrastructure.Identity.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Entities.ReturnedTicket", b =>
                {
                    b.HasOne("Domain.Entities.Ticket", "TicketNavigation")
                        .WithMany("ReturnedTickets")
                        .HasForeignKey("Ticket")
                        .HasConstraintName("fk_ReturnedTickets_Tickets")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TicketNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.HasOne("Domain.Entities.Station", "FinalStationNavigation")
                        .WithMany("RouteFinalStationNavigations")
                        .HasForeignKey("FinalStation")
                        .HasConstraintName("fk_finalStation_Stations")
                        .IsRequired();

                    b.HasOne("Domain.Entities.Station", "StartingStationNavigation")
                        .WithMany("RouteStartingStationNavigations")
                        .HasForeignKey("StartingStation")
                        .HasConstraintName("fk_startingStation_Stations")
                        .IsRequired();

                    b.Navigation("FinalStationNavigation");

                    b.Navigation("StartingStationNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Seat", b =>
                {
                    b.HasOne("Domain.Entities.Train", "TrainNavigation")
                        .WithMany("Seats")
                        .HasForeignKey("Train")
                        .HasConstraintName("fk_Seats_Trains")
                        .IsRequired();

                    b.Navigation("TrainNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Domain.Entities.Route", "RouteNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("Route")
                        .HasConstraintName("fk_Tickets_Routes")
                        .IsRequired();

                    b.HasOne("Domain.Entities.Seat", "SeatNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("Seat")
                        .HasConstraintName("fk_Tickets_Seats")
                        .IsRequired();

                    b.HasOne("Domain.Entities.Train", "TrainNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("Train")
                        .HasConstraintName("fk_Tickets_Trains")
                        .IsRequired();

                    b.Navigation("RouteNavigation");

                    b.Navigation("SeatNavigation");

                    b.Navigation("TrainNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Route", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.Station", b =>
                {
                    b.Navigation("RouteFinalStationNavigations");

                    b.Navigation("RouteStartingStationNavigations");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Navigation("ReturnedTickets");
                });

            modelBuilder.Entity("Domain.Entities.Train", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
