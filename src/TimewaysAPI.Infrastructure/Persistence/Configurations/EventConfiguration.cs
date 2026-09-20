using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimewaysAPI.Domain.Entities;

namespace TimewaysAPI.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(@event => @event.Id);

        builder.Property(@event => @event.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(@event => @event.Description)
            .HasMaxLength(2000);

        builder.Property(@event => @event.StartAt)
            .IsRequired();

        builder.Property(@event => @event.EndAt)
            .IsRequired();

        builder.Property(@event => @event.IsAllDay)
            .IsRequired();

        builder.Property(@event => @event.Location)
            .HasMaxLength(500);

        builder.HasOne(@event => @event.Owner)
            .WithMany(user => user.Events)
            .HasForeignKey(@event => @event.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(@event => new
        {
            @event.OwnerId,
            @event.StartAt
        });
    }
}