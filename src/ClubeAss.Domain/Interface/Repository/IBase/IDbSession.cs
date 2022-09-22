using System;
using System.Data;

namespace ClubeAss.Domain.Interface.Repository.IBase
{
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
    }
}
