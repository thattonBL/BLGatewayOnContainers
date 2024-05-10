using Message.Domain.MessageAggregate;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository    {
        private readonly MessageContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public MessageRepository(MessageContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RsiMessage Add(RsiMessage message)
        {
            //need to add new Queue, Common and RSI message not just RSI
            return _context.RSIs.Add(message).Entity;
        }

        public ReaMessage Add(ReaMessage message)
        {
            //need to add new Queue, Common and REA message not just RSI
            return _context.REAs.Add(message).Entity;
        }

        public Common AddCommon(int RsiId)
        {
            var newCommon = new Common { dt_created = DateTime.Now, msg_target = RsiId, type = 1 };
            return _context.Commons.Add(newCommon).Entity;
        }
    }
}
