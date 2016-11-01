using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;
using DotNetBay.Interfaces;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository
    {
        private MainDbContext dbContext;

        public EFMainRepository()
        {
            dbContext = new MainDbContext();
        }

        public Database Database
        {
            get { return dbContext.Database; }
        }

        public IQueryable<Auction> GetAuctions()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Member> GetMembers()
        {
            throw new NotImplementedException();
        }

        public Auction Add(Auction auction)
        {
            throw new NotImplementedException();
        }

        public Auction Update(Auction auction)
        {
            throw new NotImplementedException();
        }

        public Bid Add(Bid bid)
        {
            throw new NotImplementedException();
        }

        public Bid GetBidByTransactionId(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Member Add(Member member)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
