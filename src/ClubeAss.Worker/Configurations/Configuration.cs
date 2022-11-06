namespace ClubeAss.API.Customer.Configurations
{

    public class Cache
    {
        public int TimeExpirationFromSeconds { get; set; }
    }

    public class Conexao
    {
        public int PGConexao { get; set; }
    }

    public class CircuitBreaker
    {
        public int DurationOfBreak { get; set; }
        public int ExceptionsAllowedBeforeBreaking { get; set; }
    }
}
