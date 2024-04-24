using MediatR;
using Message.Domain.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace Message.Infrastructure;

public class MessageContext : DbContext, IUnitOfWork
{
    public virtual DbSet<Common> Commons { get; set; }
    public virtual DbSet<messageTypeLookup> messageTypeLookups { get; set; }
    public virtual DbSet<REA> REAs { get; set; }
    public virtual DbSet<REC> RECs { get; set; }
    public virtual DbSet<RIR> RIRs { get; set; }
    public virtual DbSet<RSI> RSIs { get; set; }
    public virtual DbSet<Queue> Queues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Queue>()
            .HasKey(e => e.id);
        
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

        modelBuilder.Entity<Common>()
            .HasMany(e => e.Queues)
            .WithOne(e => e.Common)
            .HasForeignKey(e => e.msg_target);

        modelBuilder.Entity<messageTypeLookup>()
            .Property(e => e.type)
            .IsUnicode(false);

        modelBuilder.Entity<messageTypeLookup>()
            .HasMany(e => e.Commons)
            .WithOne(e => e.messageTypeLookup)
            .HasForeignKey(e => e.type)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<messageTypeLookup>()
            .HasMany(e => e.Queues)
            .WithOne(e => e.messageTypeLookup)
            .HasForeignKey(e => e.type);

        modelBuilder.Entity<REA>()
            .Property(e => e.dt_of_action)
            .IsUnicode(false);

        modelBuilder.Entity<REA>()
            .Property(e => e.request_response_flag)
            .IsUnicode(false);

        modelBuilder.Entity<REA>()
            .Property(e => e.failure_code)
            .IsUnicode(false);

        modelBuilder.Entity<REA>()
            .Property(e => e.text_message)
            .IsUnicode(false);

        modelBuilder.Entity<REA>()
            .Property(e => e.stack_identity)
            .IsUnicode(false);

        modelBuilder.Entity<REA>()
            .Property(e => e.tray_identity)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.dt_of_action)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.request_response_flag)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.failure_code)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.text_message)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.stack_identity)
            .IsUnicode(false);

        modelBuilder.Entity<REC>()
            .Property(e => e.tray_identity)
            .IsUnicode(false);

        modelBuilder.Entity<RIR>()
            .Property(e => e.outcome)
            .IsUnicode(false);

        modelBuilder.Entity<RIR>()
            .Property(e => e.reason)
            .IsUnicode(false);

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

    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;
    public MessageContext(DbContextOptions<MessageContext> options) : base(options) { }
    public MessageContext(DbContextOptions<MessageContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

}

public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<MessageContext>
{
    public MessageContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MessageContext>()
            .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.OrderingDb;Integrated Security=true");

        return new MessageContext(optionsBuilder.Options, new NoMediator());
    }

    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return default;
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            return default;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default);
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.CompletedTask;
        }
    }
}
