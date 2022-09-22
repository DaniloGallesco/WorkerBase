using System;

namespace ClubeAss.Domain.Repository.IBase
{
    public interface IUnitOfWork : IDisposable
    {

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}