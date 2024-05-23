using GatewayGrpcService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GatewayGrpcService.Data;

public class GatewayGrpcContext : DbContext, IUnitOfWork
{
    public virtual DbSet<RSI> RSIs { get; set; }
    public virtual DbSet<Common> Commons { get; set; }

    public GatewayGrpcContext(DbContextOptions<GatewayGrpcContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Common>()
                .Property(e => e.msg_status)
                .IsUnicode(false);

        modelBuilder.Entity<Common>()
            .Property(e => e.msg_source)
            .IsUnicode(false);

        modelBuilder.Entity<Common>()
            .Property(e => e.prty)
            .IsUnicode(false);

        modelBuilder.Entity<Common>()
            .Property(e => e.ref_source)
            .IsUnicode(false);

        modelBuilder.Entity<Common>()
            .Property(e => e.ref_request_id)
            .IsUnicode(false);

        modelBuilder.Entity<messageTypeLookup>()
            .Property(e => e.type)
            .IsUnicode(false);

        modelBuilder.Entity<messageTypeLookup>()
            .HasMany(e => e.Commons)
            .WithOne(e => e.messageTypeLookup)
            .HasForeignKey(e => e.type)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RSI>()
                .Property(e => e.collection_code)
                .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.shelfmark)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.volume_number)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.storage_location_code)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.author)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.title)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.article_line1)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.article_line2)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.catalogue_record_url)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.further_details_url)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.dt_required)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.route)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.reading_room_staff_area)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.seat_number)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.reading_category)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.identifier)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.reader_name)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.operator_information)
            .IsUnicode(false);

        modelBuilder.Entity<RSI>()
            .Property(e => e.item_identity)
            .IsUnicode(false);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            //Add logging
            return false;
        }
        
        
        return true;
    }
}
