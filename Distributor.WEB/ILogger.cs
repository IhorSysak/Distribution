namespace Distributor.WEB
{
    public interface ILogger
    {
        void Info(string message, string args = null);
        void Error(string message, string args = null);
    }
}
