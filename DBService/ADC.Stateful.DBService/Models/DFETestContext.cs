using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ADC.Stateful.DBService.Models
{
    public partial class DFETestContext : DbContext
    {
        public DFETestContext()
        {
        }

        public DFETestContext(DbContextOptions<DFETestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SubmissionDataJson> SubmissionDataJson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DFETest;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("UseDevelopmentStorage=true");

                /* https://stackoverflow.com/questions/36401991/using-localdb-with-service-fabric
                 * 
                 * 1. With the SqlLocalDB.exe command line share your connection issuing this command:
                 *      sqllocaldb share MSSqlLocalDB SharedDB
                 *      
                 * 2. Open the LocalDB with SQL Server Management Studio and go to the /Security/Logins, add the
                 * NETWORK SERVICE local account and in User Mapping add it as dbo (dbo.dbowner) to your database
                 * 
                 * 3. Use the shared name in your connection string like this:
                 *      "Data Source=(localdb)\.\SharedDB;Initial Catalog=[YOUR DB];Integrated Security=SSPI;"
                 * 
                 * 4. Stop/start the localdb instance for the share to take effect:
                 *      SqlLocalDB.exe p mssqllocaldb
                 *      SqlLocalDB.exe s mssqllocaldb
                 *      SqlLocalDB.exe share mssqllocaldb SharedDB
                 */
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\.\SharedDB;Initial Catalog=DFETest;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubmissionDataJson>(entity =>
            {
                entity.HasKey(e => e.SubmissionDataId);

                entity.ToTable("submission_data_json");

                entity.Property(e => e.SubmissionDataId).HasColumnName("submission_data_id");

                entity.Property(e => e.CoAJsonString)
                    .HasColumnName("coa_json")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.DataSourceId).HasColumnName("data_source_id");

                entity.Property(e => e.SubmissionId).HasColumnName("submission_id");

                entity.Property(e => e.TrustId).HasColumnName("trust_id");
            });
        }
    }
}
