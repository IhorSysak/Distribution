using NLog;

namespace Distributor.WEB
{
    public class MyLogger : ILogger
    {
        private static MyLogger instance;
        private static Logger logger;
        private MyLogger() { }

        public static MyLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new MyLogger();
            }
            return instance;
        }

        private Logger GetLogger(string Logger)
        {
            if (MyLogger.logger == null)
            {
                MyLogger.logger = LogManager.GetLogger(Logger);
            }

            return MyLogger.logger;
        }

        public void Error(string message, string args)
        {
            if (args == null)
            {
                GetLogger("MyLogger").Error(message);
            }
            else
            {
                GetLogger("MyLogger").Error(message, args);
            }
        }

        public void Info(string message, string args)
        {
            if (args == null)
            {
                GetLogger("MyLogger").Info(message);
            }
            else
            {
                GetLogger("MyLogger").Info(message, args);
            }
        }
    }
}