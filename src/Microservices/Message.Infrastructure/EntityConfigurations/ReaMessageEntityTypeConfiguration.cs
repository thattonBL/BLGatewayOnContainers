using Message.Domain.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Message.Infrastructure.EntityConfigurations;

public class ReaMessageEntityTypeConfiguration : IEntityTypeConfiguration<ReaMessage>
{
    public void Configure(EntityTypeBuilder<ReaMessage> builder)
    {
        builder.ToTable("REA");
        builder.HasKey(e => e.Id);

        builder.Ignore(e => e.DomainEvents);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();        
        
        builder.Property<string>("_dt_of_action")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("dt_of_action")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_request_response_flag")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("request_response_flag")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_failure_code")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("failure_code")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<int>("_container_id")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("container_id");

        builder.Property<string>("_text_message")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("text_message")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_stack_identity")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("stack_identity")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property<string>("_tray_identity")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("tray_identity")
            .HasMaxLength(50)
            .IsUnicode(false);

    }
}
