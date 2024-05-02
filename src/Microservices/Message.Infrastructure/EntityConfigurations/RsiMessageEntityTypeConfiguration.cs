using Message.Domain.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Message.Infrastructure.EntityConfigurations;

public class RsiMessageEntityTypeConfiguration : IEntityTypeConfiguration<RsiMessage>
{
    public void Configure(EntityTypeBuilder<RsiMessage> builder)
    {
        builder.ToTable("RSI");
        builder.HasKey(e => e.Id);

        builder.Ignore(e => e.DomainEvents);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();        
        
        builder.Property<string>("_collection_code")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("collection_code")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_shelfmark")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("shelfmark")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_storage_location_code")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("storage_location_code")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_author")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("author")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<DateTime>("_publication_date")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("publication_date");

        builder.Property<DateTime>("_periodical_date")
            .UsePropertyAccessMode (PropertyAccessMode.Field)
            .HasColumnName("periodical_date");

        builder.Property<string>("_title")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("title")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_article_line1")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("article_line1")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_article_line2")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("article_line2")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_catalogue_record_url")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("catalogue_record_url")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_further_details_url")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("further_details_url")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_dt_required")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("dt_required")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_route")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("route")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_reading_room_staff_area")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("reading_room_staff_area")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_seat_number")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("seat_number")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_reading_category")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("reading_category")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_identifier")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("identifier")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_reader_name")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("reader_name")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<int>("_reader_type")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("reader_type");

        builder.Property<string>("_operator_information")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("operator_information")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_item_identity")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("item_identity")
            .HasMaxLength(50)
            .IsUnicode(false);
    }
}
