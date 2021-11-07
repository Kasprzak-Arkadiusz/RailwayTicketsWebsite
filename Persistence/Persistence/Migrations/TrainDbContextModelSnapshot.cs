﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class TrainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Polish_CI_AS")
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

                    b.Property<bool>("IsOnHold")
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

            modelBuilder.Entity("Domain.Entities.ReturnedTicket", b =>
                {
                    b.HasOne("Domain.Entities.Ticket", "TicketNavigation")
                        .WithMany("ReturnedTickets")
                        .HasForeignKey("Ticket")
                        .HasConstraintName("fk_ReturnedTickets_Tickets")
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
