using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VMMC.Models;

public partial class VmmcContext : DbContext
{
    public VmmcContext()
    {
    }

    public VmmcContext(DbContextOptions<VmmcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessLevel> AccessLevels { get; set; }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<ManageClientDetailsPatient> ManageClientDetailsPatients { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserModulerole> UserModuleroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessLevel>(entity =>
        {
            entity.HasKey(e => e.Accessid).HasName("PK__AccessLe__78E5790138CBCB08");

            entity.ToTable("AccessLevel");

            entity.Property(e => e.Accessid).HasColumnName("accessid");
            entity.Property(e => e.Accesslevelid)
                .HasMaxLength(100)
                .HasColumnName("accesslevelid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Levelname)
                .HasMaxLength(100)
                .HasColumnName("levelname");
            entity.Property(e => e.Levelstatus).HasColumnName("levelstatus");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Updateddate)
                .HasColumnType("datetime")
                .HasColumnName("updateddate");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRegi__CBA1B257BA3BB25F");

            entity.ToTable("ApplicationUser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accesslevelid).HasColumnName("accesslevelid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("datetime")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Emailid)
                .HasMaxLength(100)
                .HasColumnName("emailid");
            entity.Property(e => e.Employeeno)
                .HasMaxLength(100)
                .HasColumnName("employeeno");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Officelocation)
                .HasMaxLength(200)
                .HasColumnName("officelocation");
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(100)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname ");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .HasColumnName("token");
            entity.Property(e => e.Updateddate)
                .HasColumnType("datetime")
                .HasColumnName("updateddate");
            entity.Property(e => e.Userid)
                .HasMaxLength(100)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<ManageClientDetailsPatient>(entity =>
        {
            entity.HasKey(e => e.Patientid).HasName("PK__ManageCl__A17101B420781A6E");

            entity.ToTable("ManageClientDetails_Patient");

            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Additionalcontactnumber)
                .HasMaxLength(100)
                .HasColumnName("additionalcontactnumber");
            entity.Property(e => e.Assylumnumber)
                .HasMaxLength(100)
                .HasColumnName("assylumnumber");
            entity.Property(e => e.Bookingrefno)
                .HasMaxLength(100)
                .HasColumnName("bookingrefno");
            entity.Property(e => e.Cellnumber)
                .HasMaxLength(100)
                .HasColumnName("cellnumber");
            entity.Property(e => e.Consenthivtest)
                .HasMaxLength(200)
                .HasColumnName("consenthivtest");
            entity.Property(e => e.Consentmmc)
                .HasMaxLength(200)
                .HasColumnName("consentmmc");
            entity.Property(e => e.Counsellorsname)
                .HasMaxLength(200)
                .HasColumnName("counsellorsname");
            entity.Property(e => e.Countryoforigin).HasColumnName("countryoforigin");
            entity.Property(e => e.Dataitemsfrom)
                .HasMaxLength(200)
                .HasColumnName("dataitemsfrom");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("datetime")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Folderno)
                .HasMaxLength(100)
                .HasColumnName("folderno");
            entity.Property(e => e.Followup).HasColumnName("followup");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Guardianage).HasColumnName("guardianage");
            entity.Property(e => e.Guardianconsent).HasColumnName("guardianconsent");
            entity.Property(e => e.Guardianname)
                .HasMaxLength(200)
                .HasColumnName("guardianname");
            entity.Property(e => e.Guardiansurname)
                .HasMaxLength(100)
                .HasColumnName("guardiansurname");
            entity.Property(e => e.Identificationid).HasColumnName("identificationid");
            entity.Property(e => e.Identitydocument)
                .HasMaxLength(150)
                .HasColumnName("identitydocument");
            entity.Property(e => e.Nextofkinname)
                .HasMaxLength(200)
                .HasColumnName("nextofkinname");
            entity.Property(e => e.Nextofkinphoneno)
                .HasMaxLength(100)
                .HasColumnName("nextofkinphoneno");
            entity.Property(e => e.Passportnumber)
                .HasMaxLength(150)
                .HasColumnName("passportnumber");
            entity.Property(e => e.Relationship)
                .HasMaxLength(100)
                .HasColumnName("relationship");
            entity.Property(e => e.SabirthCertificateno)
                .HasMaxLength(100)
                .HasColumnName("sabirthCertificateno");
            entity.Property(e => e.Saidnumber)
                .HasMaxLength(100)
                .HasColumnName("saidnumber");
            entity.Property(e => e.Specify)
                .HasMaxLength(100)
                .HasColumnName("specify");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Mid).HasName("PK__Modules__DF5032EC8874AD12");

            entity.Property(e => e.Mid).HasColumnName("mid");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Module1)
                .HasMaxLength(150)
                .HasColumnName("module");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Updateddate)
                .HasColumnType("datetime")
                .HasColumnName("updateddate");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F59B31879");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Moduleid).HasColumnName("moduleid");
            entity.Property(e => e.Roleid)
                .HasMaxLength(10)
                .HasColumnName("roleid");
            entity.Property(e => e.Rolename)
                .HasMaxLength(150)
                .HasColumnName("rolename");
            entity.Property(e => e.Rolestatus).HasColumnName("rolestatus");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Updateddate)
                .HasColumnType("datetime")
                .HasColumnName("updateddate");
        });

        modelBuilder.Entity<UserModulerole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserModu__3213E83F2AD85969");

            entity.ToTable("UserModulerole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accessroleid).HasColumnName("accessroleid");
            entity.Property(e => e.Moduleid).HasColumnName("moduleid");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
