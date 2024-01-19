using System;
using Shared.Data.Context;
using Shared.Interfaces;

namespace Shared.Repositories
{
	public class UnitOfWorks : IUnitOfWorks
	{
        private readonly ActuatorContext _context;
        public IMasterRepositories master { get; private set; }
        public ITransactionRepositories transaction { get; private set; }
        public UnitOfWorks(
            ActuatorContext context,
            ITransactionRepositories Transactional,
            IMasterRepositories Master
            )
        {
            _context = context;
            master = Master;
            transaction = Transactional;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

