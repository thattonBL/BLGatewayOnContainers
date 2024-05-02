using ABRSWebApi.Application.IntegrationEvents;
using EventBus.Extensions;
using MediatR;
using Message.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABRSWebApi.Application.Behaviours;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly MessageContext _dbContext;
    private readonly IMessageIntegrationEventService _messageIntegrationEventService;

    public TransactionBehavior(MessageContext dbContext,
        IMessageIntegrationEventService messageIntegrationEventService,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(MessageContext));
        _messageIntegrationEventService = messageIntegrationEventService ?? throw new ArgumentException(nameof(messageIntegrationEventService));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    //Acts as a generic wrapper around the command handler "Handle" methods and defines how the db transaction to the MessageContext is managed
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();
        try
        {
            //Causes execution to wait until database is ready for a new transaction to take place
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            //I am guessing this is kind of like an SQL transaction in C# with EF Core and defines how any database commit to Message Context occurs
            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await _dbContext.BeginTransactionAsync();
                using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                {
                    _logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }
                //finally we dispatch the integration events safe inthe knowledge the changes are committed to the database
                await _messageIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}

