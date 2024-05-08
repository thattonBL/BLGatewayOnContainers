using Message.Domain.MessageAggregate;

namespace Message.Infrastructure.Repositories
{
    public interface IMessageRepository
    {
        IUnitOfWork UnitOfWork { get; }

        RsiMessage Add(RsiMessage message);

        ReaMessage Add(ReaMessage message);
    }
}