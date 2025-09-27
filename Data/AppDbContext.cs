using Microsoft.EntityFrameworkCore;
using NotificationsApi.Models;

namespace NotificationsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NotificationMessage> NotificationMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationMessage>(entity =>
            {
                entity.ToTable("notificationmessage");


                entity.Property(e => e.NotificationMessageID).HasColumnName("notificationmessageid");
                entity.Property(e => e.NotificationChannel).HasColumnName("notificationchannel");
                entity.Property(e => e.NotificationHeading).HasColumnName("notificationheading");
                entity.Property(e => e.NotificationBody).HasColumnName("notificationbody");
                entity.Property(e => e.NotificationFooter).HasColumnName("notificationfooter");
                entity.Property(e => e.NotificationSubject).HasColumnName("notificationsubject");
                entity.Property(e => e.RepeatEvery).HasColumnName("repeatevery");
                entity.Property(e => e.NoOfTimesToRepeat).HasColumnName("nooftimestorepeat");
                entity.Property(e => e.CreatedBy).HasColumnName("createdby");
                entity.Property(e => e.CreatedDate).HasColumnName("createddate");
                entity.Property(e => e.UpdatedBy).HasColumnName("updatedby");
                entity.Property(e => e.UpdatedDate).HasColumnName("updateddate");
                entity.Property(e => e.RepeatNotification).HasColumnName("repeatnotification");
                entity.Property(e => e.UseDocumentTemplate).HasColumnName("usedocumenttemplate");
                entity.Property(e => e.DocumentTemplateID).HasColumnName("documenttemplateid");
            });
        }

    }
}
