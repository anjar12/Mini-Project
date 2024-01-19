using System;
namespace Shared.Interfaces
{
	public interface IUnitOfWorks : IDisposable
	{
        ITransactionRepositories transaction { get; }
        IMasterRepositories master { get; }
    }
}

