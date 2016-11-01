using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;

namespace DotNetBay.Test.Storage
{
    public class EFMainRepositoryFactory : IRepositoryFactory
    {
        private EFMainRepository repository;

        public void Dispose()
        {
            repository.Database.Delete();
        }

        public IMainRepository CreateMainRepository()
        {
            repository = new EFMainRepository();
            return repository;
        }
    }
}
