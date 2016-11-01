using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Interfaces;
using DotNetBay.Data.EF;

namespace DotNetBay.Test.Storage
{
    class EFMainRepositoryTests : MainRepositoryTestBase
    {
        public EFMainRepositoryTests()
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override IRepositoryFactory CreateFactory()
        {
            return new EFMainRepositoryFactory();
        }
    }
}
