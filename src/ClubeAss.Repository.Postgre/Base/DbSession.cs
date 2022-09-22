using ClubeAss.Domain.Interface.Repository.IBase;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace ClubeAss.Repository.Postegre.Base
{
    public sealed class DbSession : IDbSession
    {

        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(ILogger<DbSession> _logger, IDbConnection dbConnection)
        {
            try
            {
                Connection = dbConnection;
                Connection.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro para conectar no banco de dados");
                throw ex;
            }

        }

        public void Dispose() {
            Transaction?.Dispose();
            Connection?.Close();
            Connection?.Dispose();

        }
        
    }
}