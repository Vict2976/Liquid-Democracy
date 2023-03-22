﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(LiquidDemocracyContext))]
    partial class LiquidDemocracyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Repository.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateId"), 1L, 1);

                    b.Property<int>("ElectionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecievedVotes")
                        .HasColumnType("int");

                    b.HasKey("CandidateId");

                    b.HasIndex("ElectionId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Repository.Election", b =>
                {
                    b.Property<int>("ElectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElectionId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ElectionId");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("Repository.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LoggedInWithNemId")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Repository.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoteId"), 1L, 1);

                    b.Property<int>("BelongsToId")
                        .HasColumnType("int");

                    b.Property<string>("DocumentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElectionId")
                        .HasColumnType("int");

                    b.HasKey("VoteId");

                    b.HasIndex("BelongsToId");

                    b.HasIndex("ElectionId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Repository.VoteUsedOn", b =>
                {
                    b.Property<int>("VoteUsedOnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoteUsedOnId"), 1L, 1);

                    b.Property<int?>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int?>("DelegateId")
                        .HasColumnType("int");

                    b.Property<int>("VoteId")
                        .HasColumnType("int");

                    b.HasKey("VoteUsedOnId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("DelegateId");

                    b.HasIndex("VoteId");

                    b.ToTable("VoteUsedOns");
                });

            modelBuilder.Entity("Repository.Candidate", b =>
                {
                    b.HasOne("Repository.Election", "Election")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Election");
                });

            modelBuilder.Entity("Repository.Vote", b =>
                {
                    b.HasOne("Repository.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("BelongsToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Repository.Election", "Election")
                        .WithMany("Votes")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Election");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.VoteUsedOn", b =>
                {
                    b.HasOne("Repository.Candidate", "Candidate")
                        .WithMany("DelegatedVotes")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Repository.User", "Delegate")
                        .WithMany("DelegatedVotes")
                        .HasForeignKey("DelegateId");

                    b.HasOne("Repository.Vote", "Vote")
                        .WithMany("DelegatedVotes")
                        .HasForeignKey("VoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Delegate");

                    b.Navigation("Vote");
                });

            modelBuilder.Entity("Repository.Candidate", b =>
                {
                    b.Navigation("DelegatedVotes");
                });

            modelBuilder.Entity("Repository.Election", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Repository.User", b =>
                {
                    b.Navigation("DelegatedVotes");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Repository.Vote", b =>
                {
                    b.Navigation("DelegatedVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
